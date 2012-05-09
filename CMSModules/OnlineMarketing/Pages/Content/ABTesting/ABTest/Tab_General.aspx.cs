using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.OnlineMarketing;

public partial class CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABTest_Tab_General : CMSABTestContentPage
{
    #region "Methods"

    protected override void OnInit(EventArgs e)
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

        // Get the ID from query string
        this.editElem.ABTestID = QueryHelper.GetInteger("abTestId", 0);
        this.editElem.ShowAliasPath = false;

        base.OnInit(e);
    }

    #endregion
}