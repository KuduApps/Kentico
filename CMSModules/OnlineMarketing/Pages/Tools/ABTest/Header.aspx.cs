using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_OnlineMarketing_Pages_Tools_AbTest_Header : CMSABTestPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int abTestId = QueryHelper.GetInteger("abTestId", 0);

        ABTestInfo abTest = ABTestInfoProvider.GetABTestInfo(abTestId);
        if (abTest != null)
        {
            // Prepare the tabs
            string[,] tabs = new string[2, 4];
            tabs[0, 0] = GetString("general.general");
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'abtest_general');";
            tabs[0, 2] = "Tab_General.aspx?abTestId=" + abTestId;

            tabs[1, 0] = GetString("abtesting.variant.list");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'abtest_variants');";
            tabs[1, 2] = ResolveUrl("~/CMSModules/OnlineMarketing/Pages/Tools/ABVariant/List.aspx?abTestId=" + abTestId);

            // Prepare the breadcrumbs
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("abtesting.abtest.list");
            breadcrumbs[0, 1] = "~/CMSModules/OnlineMarketing/Pages/Tools/AbTest/List.aspx";
            breadcrumbs[0, 2] = "_parent";
            breadcrumbs[1, 0] = abTest.ABTestName.ToString();

            // Set the tabs
            ICMSMasterPage master = this.CurrentMaster;
            master.Tabs.Tabs = tabs;
            master.Tabs.UrlTarget = "content";

            // Set the title
            PageTitle title = this.CurrentMaster.Title;
            title.Breadcrumbs = breadcrumbs;
            title.HelpTopicName = "abtest_general";
            title.HelpName = "helpTopic";
        }
    }

    #endregion
}