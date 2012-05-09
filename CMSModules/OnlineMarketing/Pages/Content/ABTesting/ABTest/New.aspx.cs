using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABTest_New : CMSABTestContentPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        String siteName = CMSContext.CurrentSiteName;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }

        if (!ABTestInfoProvider.ABTestingEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblABTestingDisabled.Text = GetString("abtesting.disabled");
        }

        // Prepare the breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("abtesting.abtest.list");
        breadcrumbs[0, 1] = "~/CMSModules/OnlineMarketing/Pages/Content/ABTesting/ABTest/List.aspx?nodeid=" + NodeID;
        breadcrumbs[1, 0] = GetString("abtesting.abtest.new");

        // Set the title
        PageTitle title = this.CurrentMaster.Title;
        title.Breadcrumbs = breadcrumbs;
        title.HelpTopicName = "abtest_general";

        editElem.AliasPath = QueryHelper.GetString("AliasPath", String.Empty);

        this.editElem.OnSaved += new EventHandler(editElem_OnSaved);
    }


    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Frameset.aspx?saved=1&abTestId=" + editElem.ItemID + "&nodeID=" + NodeID);
    }

    #endregion
}