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

public partial class CMSModules_OnlineMarketing_Pages_Content_MVTVariant_List : CMSMVTestContentPage
{
    #region "Methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo cui = CMSContext.CurrentUser;

        // Check UI Permissions
        if (!cui.IsAuthorizedPerUIElement("CMS.Content", "OnlineMarketing.MVTVariants"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "OnlineMarketing.MVTVariants");
        }

        if (!cui.IsAuthorizedPerResource("cms.mvtest", "Read"))
        {
            RedirectToAccessDenied(String.Format(GetString("general.permissionresource"), "Read", "MVT testing"));
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

        // Set NodeID in order to check the access to the document
        listElem.NodeID = NodeID;

        // Get the PageTemplateID of the current node
        if (Node != null)
        {
            listElem.PageTemplateID = Node.DocumentPageTemplateID;
        }
    }

    #endregion
}
