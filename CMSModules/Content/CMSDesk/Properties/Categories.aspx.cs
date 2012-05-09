using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

[RegisterTitle("content.ui.propertiescategories")]
public partial class CMSModules_Content_CMSDesk_Properties_Categories : CMSPropertiesPage
{
    #region "Variables"

    protected bool hasModifyPermission = true;
    protected TreeProvider tree = null;
    protected TreeNode node = null;
    protected bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Categories"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Categories");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts
        ScriptHelper.RegisterProgress(this.Page);

        UIContext.PropertyTab = PropertyTabEnum.Categories;

        // UI settings
        lblCategoryInfo.Text = GetString("Categories.DocumentAssignedTo");
        categoriesElem.DisplaySavedMessage = false;
        categoriesElem.OnAfterSave += categoriesElem_OnAfterSave;
        categoriesElem.UniSelector.OnSelectionChanged += categoriesElem_OnSelectionChanged;

        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        if (nodeId > 0)
        {
            tree = new TreeProvider(CMSContext.CurrentUser);
            node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, false);

            // Redirect to page 'New culture version' in split mode. It must be before setting EditedDocument.
            if ((node == null) && displaySplitMode)
            {
                URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
            }
            // Set edited document
            EditedDocument = node;

            if (node != null)
            {
                // Check read permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
                }
                // Check modify permissions
                else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    hasModifyPermission = false;
                    pnlUserCatgerories.Enabled = false;

                    // Disable selector
                    categoriesElem.Enabled = false;

                    lblCategoryInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                    lblCategoryInfo.Visible = true;
                }
                // Display all global categories in administration UI
                categoriesElem.UserID = CMSContext.CurrentUser.UserID;
                categoriesElem.DocumentID = node.DocumentID;

                // Register js synchronization script for split mode
                if (displaySplitMode)
                {
                    RegisterSplitModeSync(true, false);
                }
            }
        }
    }
   

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        pnlMenu.Visible = true;

        // Display 'The changes were saved' message
        if(QueryHelper.GetBoolean("saved", false))
        {
            lblCategoryInfo.Text = GetString("general.changessaved");
            lblCategoryInfo.Visible = true;
        }
    }

    #endregion


    #region "Handlers"

    void categoriesElem_OnAfterSave()
    {
        if (hasModifyPermission)
        {
            // Log the synchronization
            DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
        }
    }


    void categoriesElem_OnSelectionChanged(object sender, EventArgs e)
    {
        if (hasModifyPermission)
        {
            categoriesElem.Save();
        }
    }

    #endregion
}
