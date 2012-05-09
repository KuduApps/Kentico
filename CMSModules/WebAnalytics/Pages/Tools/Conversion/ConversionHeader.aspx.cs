using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_WebAnalytics_Pages_Tools_Conversion_ConversionHeader : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string displayTabName = GetString("general.overview");
        string postBackRef = ScriptHelper.GetScript("function updateTabHeader () {" + ControlsHelper.GetPostBackEventReference(this, "") + "}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "HeaderChanger", postBackRef);

        string[,] tabs = new string[2, 4];
        tabs[0, 0] = displayTabName;
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'conversions_overview');";
        tabs[0, 2] = ResolveUrl("~/CMSModules/WebAnalytics/Tools/Analytics_Report.aspx" + URLHelper.Url.Query + "&displayTitle=0");

        tabs[1, 0] = GetString("analytics_codename.conversion");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'conversions_list');";
        tabs[1, 2] = "list.aspx";

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "content";

        PageTitle title = this.CurrentMaster.Title;
        title.TitleText = GetString("abtesting.conversions");
        title.TitleImage = GetImageUrl("Objects/Analytics_Conversion/object.png");
        title.HelpTopicName = "conversion";
        title.HelpName = "helpTopic";

        // Register script for unimenu button selection
        AddMenuButtonSelectScript(this, "Conversions", null, "menu");
    }
}

