using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;


public partial class CMSModules_OnlineMarketing_Pages_Tools_AbTest_ABTest_Header : CMSABTestPage, IPostBackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = QueryHelper.GetString("page", "overview").ToLower();
        string page = GetPageURL(type);
        string displayTabName = GetString(QueryHelper.GetString("displayTab", "general.overview"));
        string postBackRef = ScriptHelper.GetScript("function updateTabHeader () {" + ControlsHelper.GetPostBackEventReference(this, "") + "}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "HeaderChanger", postBackRef);

        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.report");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'abtest_" + type + "');";
        tabs[0, 2] = page;

        tabs[1, 0] = GetString("abtesting.abtest.list");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'abtest_list');";
        tabs[1, 2] = "list.aspx";

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "content";

        PageTitle title = this.CurrentMaster.Title;
        title.TitleText = GetString("abtesting.abtest.list") + " - " + displayTabName;
        
        // Icon        
        string imageUrl = GetImageUrl("CMSModules/CMS_WebAnalytics/Details/" + type + ".png");
        if (!FileHelper.FileExists(imageUrl))
        {
            imageUrl = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
        }

        title.TitleImage = imageUrl;          
        title.HelpTopicName = "abtest_" + type;
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
            case "conversionsvalue":
                return "conversionsvalue.aspx";

            case "conversionscount":
                return "conversionscount.aspx";

            case "conversionsrate":
                return "conversionsrate.aspx";

            case "conversionsbyvariations":
                return "conversionsbyvariations.aspx";

            case "conversionssourcepages":
                return "ConversionsSourcePages.aspx";

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
}
