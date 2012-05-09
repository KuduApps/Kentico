using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_WebAnalytics_Tools_AnalyticsMultilingualHeader : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string multilingualReportName = QueryHelper.GetString("multilingualreportcodename", String.Empty);
        // Remove reportcodename parameter
        string multilingualUrl = URLHelper.RemoveParameterFromUrl("Analytics_Report.aspx" + URLHelper.Url.Query, "reportCodeName");
        // Remove multilingualReportCodeName parameter
        multilingualUrl = URLHelper.RemoveParameterFromUrl(multilingualUrl, "multilingualReportCodeName");
        // Add multilingualReportCodeName as reportcodename parameter
        multilingualUrl = URLHelper.AddParameterToUrl(multilingualUrl, "reportCodeName", multilingualReportName);

        String stat = QueryHelper.GetString("statCodeName", String.Empty);

        // Add tabs
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.all");
        tabs[0, 1] = "SetHelpTopic('helpTopic', '" + stat + "');";
        tabs[0, 2] = "Analytics_Report.aspx" + URLHelper.Url.Query + "&displaytitle=0";

        tabs[1, 0] = GetString("general.multilingual");
        tabs[1, 1] = "SetHelpTopic('helpTopic', '" + stat + "_multilingual');";
        tabs[1, 2] = multilingualUrl + "&displaytitle=0";

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "reportContent";

        // Header title and image - based on stat code name
        string statCodeName = QueryHelper.GetString("statCodeName", String.Empty);

        CurrentMaster.Title.TitleText = GetString("analytics_codename." + statCodeName);

        string dataCodeName = QueryHelper.GetString("dataCodeName", String.Empty);
        string imageUrl = GetImageUrl("CMSModules/CMS_WebAnalytics/Details/" + dataCodeName + ".png");
        if (!FileHelper.FileExists(imageUrl))
        {
            imageUrl = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
        }

        CurrentMaster.Title.TitleImage = imageUrl;

        // Context help
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = stat;
    }
}

