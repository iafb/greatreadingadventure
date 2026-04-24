using System;
using System.IO;
using System.Threading.Tasks;
using GRA.Controllers.ViewModel.Share;
using GRA.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GRA.Controllers
{
    public class ShareController : Base.UserController
    {
        private readonly LanguageService _languageService;
        private readonly ILogger<ShareController> _logger;
        private readonly SegmentService _segmentService;
        private readonly SiteService _siteService;

        public ShareController(ILogger<ShareController> logger,
            ServiceFacade.Controller context,
            LanguageService languageService,
            SegmentService segmentService,
            SiteService siteService) : base(context)
        {
            ArgumentNullException.ThrowIfNull(languageService);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(segmentService);
            ArgumentNullException.ThrowIfNull(siteService);

            _languageService = languageService;
            _logger = logger;
            _segmentService = segmentService;
            _siteService = siteService;

            PageTitle = "Share";
        }

        public async Task<IActionResult> Avatar(string id)
        {
            if (await GetSiteSettingBoolAsync(SiteSettingKey.Avatars.DisableSharing))
            {
                return NotFound();
            }

            var site = await GetCurrentSiteAsync();

            var filePath = _pathResolver
                .ResolveContentFilePath($"site{site.Id}/useravatars/{id}.png");
            if (System.IO.File.Exists(filePath))
            {
                var siteUrl = await _siteLookupService.GetSiteLinkAsync(site.Id);
                var contentPath = _pathResolver
                    .ResolveContentPath($"site{site.Id}/useravatars/{id}.png");
                var imageUrl = Path.Combine(siteUrl.ToString(), contentPath)
                    .Replace("\\", "/", StringComparison.OrdinalIgnoreCase);
                var viewModel = new ShareAvatarViewModel()
                {
                    ImageUrl = imageUrl,
                    Social = new Domain.Model.Social
                    {
                        Description = site.AvatarCardDescription,
                        ImageLink = imageUrl
                    }
                };

                var (isSet, setValue) = await _siteLookupService.GetSiteSettingIntAsync(
                        site.Id,
                        SiteSettingKey.Avatars.ShareImageAltText);
                if (isSet)
                {
                    var languageId = await _languageService
                    .GetLanguageIdAsync(_userContextProvider.GetCurrentCulture()?.Name);

                    viewModel.ShareImageAltText = await _segmentService.GetTextAsync(
                        setValue,
                        languageId);
                }

                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
