using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

// Edited object
[EditedObject(OnlineMarketingObjectType.MVTVARIANT, "variantid")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "mvtvariant.list", "~/CMSModules/OnlineMarketing/Pages/Content/MVTVariant/List.aspx?nodeid={?nodeid?}", null)]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]
[Breadcrumb(1, ResourceString = "mvtvariant.new", NewObject = true)]

// Context help
[Help("mvtvariant_edit")]
public partial class CMSModules_OnlineMarketing_Pages_Content_MVTVariant_Edit : CMSMVTestContentPage
{
    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI Permissions
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "OnlineMarketing.MVTVariants")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "OnlineMarketing.MVTVariants");
        }

        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.MVTesting);
        }

        String siteName = CMSContext.CurrentSiteName;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblWADisabled.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }

        if (!MVTestInfoProvider.MVTestingEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblMVTestingDisabled.Text = GetString("mvt.disabled");
        }
    }

    #endregion
}
