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
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_Pages_Content_ContentPersonalizationVariant_List : CMSPropertiesPage
{
    #region "Page events"

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        CurrentUserInfo cui = CMSContext.CurrentUser;
        // Check UI Permissions
        if ((cui == null) || !cui.IsAuthorizedPerUIElement("CMS.Content", "Properties.Variants"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Variants");
        }

        // Check module availability on site
        if (!ResourceSiteInfoProvider.IsResourceOnSite("cms.contentpersonalization", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.ContentPersonalization");
        }

        // Check license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ContentPersonalization);
        }

        // Check the Read permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.contentpersonalization", "Read"))
        {
            RedirectToAccessDenied(String.Format(GetString("general.permissionresource"), "Read", "Content personalization"));
        }
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set NodeID in order to check the access to the document
        listElem.NodeID = QueryHelper.GetInteger("nodeid", 0);

        // Remove MoveUp, MoveDown buttons
        listElem.Grid.GridActions.Actions.RemoveRange(2, 2);

        // Display disabled information
        if (!SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSContentPersonalizationEnabled"))
        {
            this.pnlWarning.Visible = true;
            this.lblWarning.Text = ResHelper.GetString("cp.disabled");
        }
        
        int nodeid = QueryHelper.GetInteger("nodeid", 0);
        bool displaySplitMode = false;

        if (nodeid > 0)
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode treeNode = DocumentHelper.GetDocument(nodeid, CMSContext.PreferredCultureCode, tree);

            displaySplitMode = CMSContext.DisplaySplitMode;
            if ((treeNode == null) && displaySplitMode)
            {
                URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
            }
        }

        // Register js synchronization script for split mode
        if (displaySplitMode)
        {
            RegisterSplitModeSync(true, false);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Set selected tab
        UIContext.PropertyTab = PropertyTabEnum.Variants;
    }

    #endregion
}
