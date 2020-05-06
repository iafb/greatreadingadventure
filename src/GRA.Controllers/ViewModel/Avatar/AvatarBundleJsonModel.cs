using System.Collections.Generic;

namespace GRA.Controllers.ViewModel.Avatar
{
    public class AvatarBundleJsonModel
    {
        public ICollection<AvatarBundle> Bundles { get; set; }

        public class AvatarBundle
        {
            public int Id { get; set; }
            public ICollection<AvatarItem> Items { get; set; }
        }

        public class AvatarItem
        {
            public int Id { get; set; }
            public string DateCreated { get; set; }
        }
    }
}
