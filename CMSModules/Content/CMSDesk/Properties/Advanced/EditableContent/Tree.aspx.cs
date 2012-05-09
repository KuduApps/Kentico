using System;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

using TreeElemNode = System.Web.UI.WebControls.TreeNode;
using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Tree : CMSModalPage
{
    #region "Private & protected variables"

    protected int nodeId = 0;
    private string selectedNodeType = null;
    private string selectedNodeName = null;
    private CurrentUserInfo currentUser = null;
    protected TreeNode node = null;
    protected TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private WorkflowInfo wi = null;
    private VersionManager mVersionManager = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Checks whether current user is authorized to modify editable content.
    /// </summary>
    /// <returns>True if authorized.</returns>
    protected bool IsAuthorizedToModify
    {
        get
        {
            return (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed);
        }
    }


    /// <summary>
    /// Indicates if document is checkouted by current user.
    /// </summary>
    protected bool DocumentIsCheckOuted
    {
        get
        {
            int checkoutUserId = node.DocumentCheckedOutByUserID;
            if (!AutoCheck || (checkoutUserId > 0))
            {
                return (checkoutUserId == currentUser.UserID);
            }
            return true;
        }
    }


    /// <summary>
    /// Indicates if document uses workflow.
    /// </summary>
    protected bool UsesWorkflow
    {
        get
        {
            return (WorkflowManager.GetNodeWorkflow(node) != null);
        }
    }


    /// <summary>
    /// Indicates if check-in/check-out functionality is automatic
    /// </summary>
    protected bool AutoCheck
    {
        get
        {
            if (node != null)
            {
                // Get workflow info
                wi = WorkflowManager.GetNodeWorkflow(node);

                // Check if the document uses workflow
                if (wi != null)
                {
                    return !wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Gets Workflow manager instance.
    /// </summary>
    protected WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(TreeProvider));
        }
    }


    /// <summary>
    /// Gets Version manager instance.
    /// </summary>
    protected VersionManager VersionManager
    {
        get
        {
            return mVersionManager ?? (mVersionManager = new VersionManager(TreeProvider));
        }
    }


    /// <summary>
    /// Tree provider instance.
    /// </summary>
    protected TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CMSContext.CurrentUser));
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check 'read' permissions
        if (!user.IsAuthorizedPerResource("CMS.Content", "Read"))
        {
            RedirectToAccessDenied("CMS.Content", "Read");
        }

        // Check UIProfile
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "Properties.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.General");
        }

        if (!user.IsAuthorizedPerUIElement("CMS.Content", "General.Advanced"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "General.Advanced");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;
        CurrentMaster.FrameResizer.Visible = false;

        // Get query parameters
        selectedNodeType = QueryHelper.GetString("selectednodetype", "webpart");
        selectedNodeName = QueryHelper.GetString("selectednodename", string.Empty);
        hdnCurrentNodeType.Value = selectedNodeType;
        hdnCurrentNodeName.Value = selectedNodeName;
        nodeId = QueryHelper.GetInteger("nodeid", 0);
        // Get node
        node = DocumentHelper.GetDocument(nodeId, currentUser.PreferredCultureCode, TreeProvider);
        // Set edited document
        EditedDocument = node;

        // Images
        if (hdnCurrentNodeType.Value == "region")
        {
            imgNewItem.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/addeditableitemsmall.png");
            imgNewItem.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/addeditableitemsmalldisabled.png");
        }
        else
        {
            imgNewItem.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/editablewebpart.png");
            imgNewItem.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/editablewebpartdisabled.png");
        }
        imgDeleteItem.ImageUrl = GetImageUrl("Objects/CMS_WebPart/delete.png");
        imgDeleteItem.DisabledImageUrl = GetImageUrl("Objects/CMS_WebPart/deletedisabled.png");

        string selectNodeName = QueryHelper.GetString("selectednodename", null);
        if (!string.IsNullOrEmpty(selectedNodeName))
        {
            bool enabled = UsesWorkflow ? (DocumentIsCheckOuted && IsAuthorizedToModify) : IsAuthorizedToModify;
            pnlDeleteItem.Enabled = enabled;
            if (enabled)
            {
                lnkDeleteItem.CssClass = "NewItemLink";
                lnkDeleteItem.Attributes.Add("onclick", "return DeleteItem();");
            }
        }
        else
        {
            pnlDeleteItem.Enabled = false;
        }

        if (IsAuthorizedToModify)
        {
            lnkNewItem.CssClass = "NewItemLink";
            lnkNewItem.Attributes.Add("onclick", "return CreateNew();");
            pnlNewItem.Enabled = true;
        }
        else
        {
            lnkNewItem.CssClass = "NewItemLinkDisabled";
            pnlNewItem.Enabled = false;
        }

        // Resource strings
        lnkDeleteItem.Text = GetString("Development-WebPart_Tree.DeleteItem");
        lnkNewItem.Text = GetString("Development-WebPart_Tree.NewWebPart");

        string imageUrl = "Design/Controls/Tree/";

        // Initialize page
        if (CultureHelper.IsUICultureRTL())
        {
            imageUrl = GetImageUrl("RTL/" + imageUrl, false, false);
        }
        else
        {
            imageUrl = GetImageUrl(imageUrl, false, false);
        }
        webpartsTree.LineImagesFolder = imageUrl;
        regionsTree.LineImagesFolder = imageUrl;

        if (node != null)
        {
            string webpartRootAttributes = "class=\"ContentTreeItem\"";
            string regionRootAttributes = "class=\"ContentTreeItem\"";

            if (string.IsNullOrEmpty(selectedNodeName))
            {
                switch (selectedNodeType)
                {
                    case "webpart":
                        webpartRootAttributes = "class=\"ContentTreeSelectedItem\" id=\"treeSelectedNode\"";
                        regionRootAttributes = "class=\"ContentTreeItem\" ";
                        break;

                    case "region":
                        webpartRootAttributes = "class=\"ContentTreeItem\" ";
                        regionRootAttributes = "class=\"ContentTreeSelectedItem\" id=\"treeSelectedNode\"";
                        break;
                    default:
                        webpartRootAttributes = "class=\"ContentTreeSelectedItem\" id=\"treeSelectedNode\"";
                        regionRootAttributes = "class=\"ContentTreeItem\" ";
                        break;
                }
            }
            else
            {
                webpartRootAttributes = "class=\"ContentTreeSelectedItem\" id=\"treeSelectedNode\"";
                regionRootAttributes = "class=\"ContentTreeItem\" ";
            }

            // Create tree menus
            TreeElemNode rootWebpartNode = new TreeElemNode();
            rootWebpartNode.Text = "<span " + webpartRootAttributes + " onclick=\"SelectNode('','webpart', this);\"><span class=\"Name\">" + GetString("EditableWebparts.Root") + "</span></span>";
            rootWebpartNode.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/editablewebparts.png");
            rootWebpartNode.Expanded = true;
            rootWebpartNode.NavigateUrl = "#";

            TreeElemNode rootRegionNode = new TreeElemNode();
            rootRegionNode.Text = "<span " + regionRootAttributes + " onclick=\"SelectNode('','region', this);\"><span class=\"Name\">" + GetString("EditableRegions.Root") + "</span></span>";
            rootRegionNode.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/editableregions.png");
            rootRegionNode.Expanded = true;
            rootRegionNode.NavigateUrl = "#";

            // Editable web parts
            webpartsTree.Nodes.Add(rootWebpartNode);
            if (node.DocumentContent.EditableWebParts.Count > 0)
            {
                foreach (DictionaryEntry webPart in node.DocumentContent.EditableWebParts)
                {
                    string key = webPart.Key.ToString();
                    string name = MultiKeyHashtable.GetFirstKey(key);
                    AddNode(rootWebpartNode, name, "webpart");
                }
            }

            // Editable regions
            regionsTree.Nodes.Add(rootRegionNode);
            if (node.DocumentContent.EditableRegions.Count > 0)
            {
                foreach (DictionaryEntry region in node.DocumentContent.EditableRegions)
                {
                    string key = region.Key.ToString();
                    string name = MultiKeyHashtable.GetFirstKey(key);
                    AddNode(rootRegionNode, name, "region");
                }
            }
        }

        // Delete item if requested from querystring
        string nodeType = QueryHelper.GetString("nodetype", null);
        string nodeName = QueryHelper.GetString("nodename", null);
        if (!RequestHelper.IsPostBack() && !String.IsNullOrEmpty(nodeType) && QueryHelper.GetBoolean("deleteItem", false))
        {
            DeleteItem(nodeType, nodeName);
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Adds node to parent node.
    /// </summary>
    /// <param name="parentNode">Parent node</param>
    /// <param name="nodeName">Name of node</param>
    /// <param name="nodeKey">Node key</param>
    /// <param name="nodeType">Type of node</param>
    private void AddNode(TreeElemNode parentNode, string nodeName, string nodeType)
    {
        TreeElemNode newNode = new TreeElemNode();
        string cssClass = "ContentTreeItem";
        string elemId = string.Empty;

        // Select proper node
        if (selectedNodeName == nodeName)
        {
            if (webpartsTree.Nodes.Count > 0)
            {
                webpartsTree.Nodes[0].Text = webpartsTree.Nodes[0].Text.Replace("ContentTreeSelectedItem", "ContentTreeItem").Replace("treeSelectedNode", string.Empty);
            }
            else if (regionsTree.Nodes.Count > 0)
            {
                regionsTree.Nodes[0].Text = regionsTree.Nodes[0].Text.Replace("ContentTreeSelectedItem", "ContentTreeItem").Replace("treeSelectedNode", string.Empty);
            }
            if (nodeType == selectedNodeType)
            {
                cssClass = "ContentTreeSelectedItem";
                elemId = "id=\"treeSelectedNode\"";
            }
        }
        newNode.Text = "<span class=\"" + cssClass + "\" " + elemId + " onclick=\"SelectNode(" + ScriptHelper.GetString(nodeName) + ", " + ScriptHelper.GetString(nodeType) + ", this);\"><span class=\"Name\">" + HTMLHelper.HTMLEncode(nodeName) + "</span></span>";
        newNode.NavigateUrl = "#";
        parentNode.ChildNodes.Add(newNode);
    }


    /// <summary>
    /// Deletes item.
    /// </summary>
    protected void DeleteItem(string nodeType, string nodeName)
    {
        if (IsAuthorizedToModify)
        {
            // Remove key from hashtable
            switch (nodeType)
            {
                case "webpart":
                    node.DocumentContent.EditableWebParts.Remove(nodeName);
                    break;

                case "region":
                    node.DocumentContent.EditableRegions.Remove(nodeName);
                    break;
            }

            // Save node
            SaveNode();

            // Refresh
            ltlScript.Text += ScriptHelper.GetScript("document.location.replace('" + ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/EditableContent/tree.aspx") + "?nodeid=" + nodeId + "&selectednodetype=" + nodeType + "'); parent.frames['main'].SelectNode('','" + nodeType + "');");
        }
        else
        {
            ltlScript.Text += ScriptHelper.GetAlertScript(string.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath));
        }
    }


    /// <summary>
    /// Saves node, ensures workflow.
    /// </summary>
    protected void SaveNode()
    {
        // Get content
        string content = node.DocumentContent.GetContentXml();

        // Save content
        if (node != null)
        {
            // If not using check-in/check-out, check out automatically
            if (AutoCheck)
            {
                if (!node.IsCheckedOut)
                {
                    // Check out
                    VersionManager.CheckOut(node, node.IsPublished, true);
                }
            }
            node.UpdateOriginalValues();

            node.SetValue("DocumentContent", content);
            DocumentHelper.UpdateDocument(node, TreeProvider);

            // Check in the document
            if (AutoCheck)
            {
                VersionManager.CheckIn(node, null, null);
            }
        }
    }

    #endregion
}
