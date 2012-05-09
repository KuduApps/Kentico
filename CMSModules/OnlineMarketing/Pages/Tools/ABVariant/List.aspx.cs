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

public partial class CMSModules_OnlineMarketing_Pages_Tools_ABVariant_List : CMSABTestPage
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

        int testID = QueryHelper.GetInteger("abTestId", 0);
        listElem.TestID = testID;
        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = ResHelper.GetString("abtesting.variant.new");        
        actions[0, 3] = ResolveUrl("Edit.aspx?abtestID="+testID);       
        actions[0, 5] = GetImageUrl("Objects/CMS_Variant/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;
    }

    #endregion
}
