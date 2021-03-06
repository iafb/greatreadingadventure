﻿using GRA.SRP.DAL;
using GRA.Tools;
using SRPApp.Classes;
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRA.SRP.Events
{
    public partial class Details : BaseSRPPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request["PID"]))
            {
                Session["ProgramID"] = Request["PID"].ToString();
            }
            if (!IsPostBack)
            {
                if (Session["ProgramID"] == null)
                {
                    try
                    {
                        int PID = Programs.GetDefaultProgramID();
                        Session["ProgramID"] = PID.ToString();
                    }
                    catch
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }

            TranslateStrings(this);

            eventBackLink.NavigateUrl = "~/Events/";

            DAL.Event evnt = null;
            int eventId = 0;
            string displayEvent = Request.QueryString["EventId"];
            if (!string.IsNullOrEmpty(displayEvent)
               && int.TryParse(displayEvent.ToString(), out eventId))
            {
                evnt = DAL.Event.GetEvent(eventId);
                if (evnt != null && evnt.HiddenFromPublic)
                {
                    evnt = null;
                }
                if (evnt != null)
                {
                    SchemaOrgLibrary mdLib = new SchemaOrgLibrary();
                    SchemaOrgEvent mvEvt = new SchemaOrgEvent
                    {
                        Name = evnt.EventTitle,
                        StartDate = evnt.EventDate
                    };

                    eventTitle.Text = evnt.EventTitle;
                    this.Title = string.Format("Event Details: {0}", eventTitle.Text);

                    eventWhen.Text = DAL.Event.DisplayEventDateTime(evnt);

                    eventWhere.Visible = false;
                    eventWhereLink.Visible = false;
                    atLabel.Visible = false;

                    if (evnt.BranchID > 0)
                    {
                        var codeObject = DAL.Codes.FetchObject(evnt.BranchID);
                        if (codeObject != null)
                        {
                            eventWhere.Text
                                = mdLib.Name
                                = codeObject.Description;
                            eventWhereLink.Text = string.Format("{0} <span class=\"glyphicon glyphicon-new-window hidden-print\"></span>",
                                codeObject.Description);

                            eventWhere.Visible = true;
                            atLabel.Visible = true;
                            eventWhereLink.Visible = false;
                        }
                        var crosswalk = DAL.LibraryCrosswalk.FetchObjectByLibraryID(evnt.BranchID);
                        if (crosswalk != null)
                        {
                            if (!string.IsNullOrEmpty(eventWhere.Text)
                                && !string.IsNullOrEmpty(crosswalk.BranchAddress))
                            {
                                eventWhereMapLink.Visible = true;
                                eventWhereMapLink.NavigateUrl = string.Format(WebTools.BranchMapLinkStub,
                                    crosswalk.BranchAddress);
                            }

                            if (!string.IsNullOrEmpty(eventWhere.Text)
                               && !string.IsNullOrEmpty(crosswalk.BranchLink))
                            {
                                eventWhereLink.NavigateUrl = crosswalk.BranchLink;
                                eventWhere.Visible = false;
                                eventWhereLink.Visible = true;
                                atLabel.Visible = true;
                            }

                            mdLib.Address = crosswalk.BranchAddress;
                            mdLib.Telephone = crosswalk.BranchTelephone;
                            mdLib.Url = crosswalk.BranchLink;
                        }
                    }

                    if (string.IsNullOrEmpty(mdLib.Name))
                    {
                        this.MetaDescription = string.Format("Details about the event: {0} - {1}",
                            mdLib.Name,
                            GetResourceString("system-name"));
                    }
                    else
                    {
                        this.MetaDescription = string.Format("Details about the event: {0} at {1} - {2}",
                            eventTitle.Text,
                            eventWhere.Text,
                            GetResourceString("system-name"));
                    }

                    if (!string.IsNullOrWhiteSpace(evnt.ExternalLinkToEvent))
                    {
                        eventLinkPanel.Visible = true;
                        ExternalLink.NavigateUrl = evnt.ExternalLinkToEvent;
                        ExternalLink.Text = string.Format(eventTitle.Text);
                    }
                    else
                    {
                        eventLinkPanel.Visible = false;
                    }
                    eventDescription.Text = Server.HtmlDecode(evnt.HTML);
                    var cf = DAL.CustomEventFields.FetchObject();
                    if (!string.IsNullOrWhiteSpace(evnt.Custom1)
                       && !string.IsNullOrWhiteSpace(cf.Label1))
                    {
                        eventCustom1Panel.Visible = true;
                        eventCustomLabel1.Text = cf.Label1;
                        eventCustomValue1.Text = evnt.Custom1;
                    }
                    else
                    {
                        eventCustom1Panel.Visible = false;
                    }
                    if (!string.IsNullOrWhiteSpace(evnt.Custom2)
                       && !string.IsNullOrWhiteSpace(cf.Label2))
                    {
                        eventCustom2Panel.Visible = true;
                        eventCustomLabel2.Text = cf.Label2;
                        eventCustomValue2.Text = evnt.Custom2;
                    }
                    else
                    {
                        eventCustom2Panel.Visible = false;
                    }
                    if (!string.IsNullOrWhiteSpace(evnt.Custom3)
                       && !string.IsNullOrWhiteSpace(cf.Label3))
                    {
                        eventCustom3Panel.Visible = true;
                        eventCustomLabel3.Text = cf.Label3;
                        eventCustomValue3.Text = evnt.Custom3;
                    }
                    else
                    {
                        eventCustom3Panel.Visible = false;
                    }
                    eventDetails.Visible = true;

                    mvEvt.Location = mdLib;
                    try
                    {
                        Microdata.Text = new WebTools().BuildEventJsonld(mvEvt);
                    }
                    catch (Exception ex)
                    {
                        this.Log().Error("Problem creating microdata in event detail for {0}: {1} - {2}",
                            evnt.EID,
                            ex.Message,
                            ex.StackTrace);
                    }

                    // begin social
                    var wt = new WebTools();

                    var systemName = StringResources.getStringOrNull("system-name");
                    var fbDescription = StringResources.getStringOrNull("facebook-description");
                    var hashtags = StringResources.getStringOrNull("socialmedia-hashtags");

                    var title = string.Format("{0} event: {1}",
                        systemName,
                        evnt.EventTitle);
                    string description = string.Format("I'm thinking about attending this {0} event: {1}!",
                        systemName,
                        evnt.EventTitle);
                    string twitDescrip = string.Format("Check out this {0} event: {1}!",
                        systemName,
                        evnt.EventTitle);
                    if (twitDescrip.Length > 118)
                    {
                        // if it's longer than this it won't fit with the url, shorten it
                        twitDescrip = string.Format("Check this out: {0}!",
                            evnt.EventTitle);
                    }

                    var baseUrl = WebTools.GetBaseUrl(Request);
                    var eventDetailsUrl = string.Format("{0}/Events/Details.aspx?EventId={1}",
                        baseUrl,
                        evnt.EID);
                    string bannerPath = new GRA.Logic.Banner().FullMetadataBannerPath(baseUrl,
                        Session,
                        Server);

                    wt.AddOgMetadata(Metadata,
                        title,
                        wt.BuildFacebookDescription(description, hashtags, fbDescription),
                        bannerPath,
                        eventDetailsUrl,
                        facebookApp: GetResourceString("facebook-appid"));

                    wt.AddTwitterMetadata(Metadata,
                        title,
                        description,
                        bannerPath,
                        twitterUsername: GetResourceString("twitter-username"));

                    TwitterShare.NavigateUrl = wt.GetTwitterLink(twitDescrip,
                        Server.UrlEncode(eventDetailsUrl),
                        hashtags);
                    TwitterShare.Visible = true;
                    FacebookShare.NavigateUrl
                        = wt.GetFacebookLink(Server.UrlEncode(eventDetailsUrl));
                    FacebookShare.Visible = true;
                    //end social
                }
            }

            if (evnt == null)
            {
                eventDetails.Visible = false;
                var cph = Page.Master.FindControl("HeaderContent") as ContentPlaceHolder;
                if (cph != null)
                {
                    cph.Controls.Add(new HtmlMeta
                    {
                        Name = "robots",
                        Content = "noindex"
                    });
                }
                Session[SessionKey.PatronMessage] = "Could not find details on that event.";
                Session[SessionKey.PatronMessageLevel] = PatronMessageLevels.Warning;
                Session[SessionKey.PatronMessageGlyphicon] = "exclamation-sign";

            }
        }
    }
}