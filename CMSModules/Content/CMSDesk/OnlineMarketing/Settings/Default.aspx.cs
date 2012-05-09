using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

[RegisterTitle("general.settings")]
public partial class CMSModules_Content_CMSDesk_OnlineMarketing_Settings_Default : CMSAnalyticsContentPage
{
    protected string mSave;
    protected int nodeID = 0;
    TreeNode node = null;
    TreeProvider tree = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI Analytics.Settings
        CurrentUserInfo ui = CMSContext.CurrentUser;
        if (!ui.IsAuthorizedPerUIElement("CMS.Content", "Analytics.Settings"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Analytics.Settings");
        }     
        
        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName))
        {
            this.pnlWarning.Visible = true;
            this.lblWarning.Text = ResHelper.GetString("WebAnalytics.Disabled");
        }

        ucConversionSelector.SelectionMode = SelectionModeEnum.SingleTextBox;
        ucConversionSelector.IsLiveSite = false;

        nodeID = QueryHelper.GetInteger("nodeid", 0);
        mSave = GetString("general.save");

        UIContext.AnalyticsTab = AnalyticsTabEnum.Settings;

        tree = new TreeProvider(CMSContext.CurrentUser);
        node = tree.SelectSingleNode(nodeID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture);

        if (ui.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
        {
            RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
        }

        // Check modify permissions
        else if (ui.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            lblInfo.Visible = true;
            lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);

            // Disable save button
            btnSave.Enabled = false;
            usSelectCampaign.Enabled = false;
        }

        if (node != null)
        {
            if (!URLHelper.IsPostback())
            {
                ReloadData();
            }
        }
    }


    /// <summary>
    /// Reload data from node to controls.
    /// </summary>
    private void ReloadData()
    {
        usSelectCampaign.Value = node.DocumentCampaign;
        ucConversionSelector.Value = node.DocumentTrackConversionName;
        txtConversionValue.Text = node.DocumentConversionValue.ToString();
    }


    protected void btnSave_Click(object sender, EventArgs ea)
    {
        // Check modify permissions
        CurrentUserInfo ui = CMSContext.CurrentUser;

        if (node != null)
        {
            // Check permissions
            if (ui.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }

            string conversionValue = ValidationHelper.GetString(ucConversionSelector.Value, String.Empty).Trim();

            if (!ucConversionSelector.IsValid())
            {
                lblError.Text = ucConversionSelector.ValidationError;
                lblError.Visible = true;
                return;
            }

            if (!usSelectCampaign.IsValid())
            {
                lblError.Visible = true;
                lblError.Text = usSelectCampaign.ValidationError;
                return;
            }

            node.DocumentCampaign = ValidationHelper.GetString(usSelectCampaign.Value, String.Empty).Trim();
            node.DocumentConversionValue = txtConversionValue.Text;
            node.DocumentTrackConversionName = conversionValue;            
            node.Update();

            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");

            // Update search index
            if ((node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
            {
                SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
            }

            // Log synchronization
            DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
        }
    } 
}

