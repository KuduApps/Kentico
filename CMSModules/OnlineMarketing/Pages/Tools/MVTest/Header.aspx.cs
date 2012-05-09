using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.OnlineMarketing;

public partial class CMSModules_OnlineMarketing_Pages_Tools_MVTest_Header : CMSMVTestPage, IPostBackEventHandler
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        string help = QueryHelper.GetString("help", "overview");
        string page = GetPageURL(QueryHelper.GetString("page", "overview"));
        string displayTabName = GetString(QueryHelper.GetString("displayTab", "general.overview"));

        // Register script for manual update tab when clicke edit from overview
        string postBackRef = ScriptHelper.GetScript("function updateTabHeader () {" + ControlsHelper.GetPostBackEventReference(this, "") + "}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "HeaderChanger", postBackRef);

        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.report");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'mvtest_" + help + "');";
        tabs[0, 2] = page + URLHelper.Url.Query;

        tabs[1, 0] = GetString("mvtest.list");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'mvtest_list');";
        tabs[1, 2] = "List.aspx";

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "content";

        PageTitle title = this.CurrentMaster.Title;
        title.TitleText = GetString("mvtest.list") + " - " + displayTabName;

        // Icon        
        string imageUrl = GetImageUrl("CMSModules/CMS_WebAnalytics/Details/" + help + ".png");
        if (!FileHelper.FileExists(imageUrl))
        {
            imageUrl = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
        }

        title.TitleImage = imageUrl;   

        title.HelpTopicName = "mvtest_" + help;
        title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Gets page url from constant.
    /// </summary>
    /// <param name="page">Page constant</param>
    private string GetPageURL(string page)
    {
        switch (page.ToLower())
        {
            case "mvtreport":
                return "mvtreport.aspx";

            default:
                return "overview.aspx";
        }
    }


    /// <summary>
    /// Raised on postback.
    /// </summary>
    /// <param name="eventArgument">__EVENTARGUMENT</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        CurrentMaster.Tabs.SelectedTab = 1;
    }

    #endregion
}