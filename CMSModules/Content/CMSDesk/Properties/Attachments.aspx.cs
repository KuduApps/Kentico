using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

[RegisterTitle("content.ui.propertiesattachments")]
public partial class CMSModules_Content_CMSDesk_Properties_Attachments : CMSPropertiesPage
{
    #region "Variables"

    protected bool hasModifyPermission = true;
    protected TreeProvider tree = null;
    protected TreeNode treeNode = null;
    protected int nodeId = 0;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Attachments"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Attachments");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register WebDAV script
        if (CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName) && RequestHelper.IsWindowsAuthentication())
        {
            // Register scripts
            string script = "function RefreshForm(){" + Page.ClientScript.GetPostBackEventReference(btnRefresh, "") + " }";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshForm", ScriptHelper.GetScript(script));

            ScriptHelper.RegisterWebDAV(Page);
        }

        UIContext.PropertyTab = PropertyTabEnum.Attachments;

        nodeId = QueryHelper.GetInteger("nodeid", 0);
        if (nodeId > 0)
        {
            tree = new TreeProvider(CMSContext.CurrentUser);
            treeNode = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, tree);

            bool displaySplitMode = CMSContext.DisplaySplitMode;
            if ((treeNode == null) && displaySplitMode)
            {
                URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
            }

            // Set edited document
            EditedDocument = treeNode;
            menuElem.Node = treeNode;

            if (menuElem.Node != null)
            {
                // Check read permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(menuElem.Node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), menuElem.Node.NodeAliasPath));
                }
                // Check modify permissions
                else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(menuElem.Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    hasModifyPermission = false;
                    lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), menuElem.Node.NodeAliasPath);
                }
                ucAttachments.ActualNode = treeNode;
                ucAttachments.DocumentID = menuElem.Node.DocumentID;
                ucAttachments.NodeParentNodeID = menuElem.Node.NodeParentID;
                ucAttachments.NodeClassName = menuElem.Node.NodeClassName;

                // Resize attachment due to site settings
                string siteName = CMSContext.CurrentSiteName;
                ucAttachments.ResizeToHeight = ImageHelper.GetAutoResizeToHeight(siteName);
                ucAttachments.ResizeToWidth = ImageHelper.GetAutoResizeToWidth(siteName);
                ucAttachments.ResizeToMaxSideSize = ImageHelper.GetAutoResizeToMaxSideSize(siteName);
                ucAttachments.PageSize = "10,25,50,100,##ALL##";
            }

            // Register js synchronization script for split mode
            if (displaySplitMode)
            {
                RegisterSplitModeSync(true, false);
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ucAttachments.VersionHistoryID = menuElem.Node.DocumentCheckedOutVersionHistoryID;
        ucAttachments.Enabled = (menuElem.AllowEdit && hasModifyPermission && menuElem.AllowSave);
        lblInfo.Text = menuElem.WorkflowInfo;
        pnlAttachments.Visible = (lblInfo.Text != string.Empty);
    }


    /// <summary>
    /// Refresh button click event handler.
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (treeNode != null)
        {
            // Check permission to modify document
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(treeNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
            {
                // Get curent step
                WorkflowManager wm = new WorkflowManager(tree);
                WorkflowStepInfo currentStep = wm.GetStepInfo(treeNode);
                string currentStepName = currentStep.StepName.ToLower();
                bool wasArchived = currentStepName == "archived";

                // Move to edit step
                DocumentHelper.MoveDocumentToEditStep(treeNode, tree);

                // Refresh frames and tree
                string script = "if(window.FramesRefresh){FramesRefresh(" + wasArchived.ToString().ToLower() + ", " + treeNode.NodeID + ");}";
                ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshAction", ScriptHelper.GetScript(script));
            }
        }
    }

    #endregion
}
