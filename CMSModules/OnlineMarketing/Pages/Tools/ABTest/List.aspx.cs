using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_OnlineMarketing_Pages_Tools_AbTest_List : CMSABTestPage
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

        if (!AnalyticsHelper.TrackConversionsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblTrackConversionsDisabled.Text = GetString("webanalytics.tackconversionsdisabled");
        }

        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = ResHelper.GetString("abtesting.abtest.new");        
        actions[0, 3] = ResolveUrl("new.aspx");       
        actions[0, 5] = GetImageUrl("Objects/OM_AbTest/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;

        listElem.ShowFilter = true;
    }

    #endregion
}
