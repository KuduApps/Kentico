using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

// Edited object
[EditedObject(AnalyticsObjectType.CAMPAIGN, "campaignid")]

// Create empty title for context help
[Title("", "", "campaign_general")]

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_Header : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int campaignID = QueryHelper.GetInteger("campaignId", 0);
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'campaign_general');";
        tabs[0, 2] = "Tab_General.aspx?campaignid=" + campaignID;

        tabs[1, 0] = GetString("analytics_codename.goals");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'campaign_goals');";
        tabs[1, 2] = "Tab_Goals.aspx?campaignid=" + campaignID;

        tabs[2, 0] = GetString("conversion.conversion.list"); ;
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'campaign_conversions');";
        tabs[2, 2] = "Tab_Conversions.aspx?campaignid=" + campaignID;

        String listUrl = "~/CMSModules/WebAnalytics/Pages/Tools/Campaign/List.aspx";
        if (QueryHelper.GetBoolean("DisplayReport", false))
        {
            tabs[3, 0] = GetString("general.reports");
            tabs[3, 1] = "SetHelpTopic('helpTopic', 'campaign_report');";
            tabs[3, 2] = "Tab_Reports.aspx?campaignid=" + campaignID;
            listUrl += "?displayreport=true";
        }

        // Prepare the breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("campaign.campaign.list");
        breadcrumbs[0, 1] = listUrl;
        breadcrumbs[0, 2] = "_parent";

        // Add campaign name
        CampaignInfo ci = EditedObject as CampaignInfo;
        if (ci != null)
        {
            breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(ci.CampaignDisplayName);
        }

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "content";
    }
}
