using System;
using System.Data;
using System.Text;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_tree : SiteManagerPage, IPostBackEventHandler
{
    #region "Variables"

    private int nodeId = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Requested action.
    /// </summary>
    protected string Action
    {
        get
        {
            return ValidationHelper.GetString(Request.Form["hdnAction"], "");
        }
    }


    /// <summary>
    /// Action parameter 1.
    /// </summary>
    protected string Param1
    {
        get
        {
            return ValidationHelper.GetString(Request.Form["hdnParam1"], "");
        }
    }


    /// <summary>
    /// Action parameter 2.
    /// </summary>
    protected string Param2
    {
        get
        {
            return ValidationHelper.GetString(Request.Form["hdnParam2"], "");
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        siteName.Value = QueryHelper.GetString("sitename", string.Empty);

        treeContent.NodeTextTemplate = "<span class=\"ContentTreeItem\" onclick=\"SelectNode(##NODEID##, this); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        treeContent.SelectedNodeTextTemplate = "<span class=\"ContentTreeSelectedItem\" onclick=\"SelectNode(##NODEID##, this); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        treeContent.MaxTreeNodeText = "<span class=\"ContentTreeItem\" onclick=\"Listing(##PARENTNODEID##, this); return false;\"><span class=\"Name\" style=\"font-style: italic;\">" + GetString("ContentTree.SeeListing") + "</span></span>";

        treeContent.SiteName = siteName.Value;

        if (!RequestHelper.IsCallback())
        {
            // If nodeId set, init the list of the nodes to expand
            int expandNodeId = QueryHelper.GetInteger("expandnodeid", 0);
            treeContent.ExpandNodeID = expandNodeId;

            // Current Node ID
            nodeId = QueryHelper.GetInteger("nodeid", 0);
            if (nodeId == 0)
            {
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                TreeNode node = tree.SelectSingleNode(siteName.Value, "/", TreeProvider.ALL_CULTURES);
                if (node != null)
                {
                    nodeId = node.NodeID;
                }
            }

            treeContent.NodeID = nodeId;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsCallback())
        {
            ScriptHelper.RegisterJQuery(this);
            StringBuilder startupScript = new StringBuilder();
            startupScript.Append(
            @"  treeUrl = '", ResolveUrl("~/CMSModules/ImportExport/SiteManager/NewSite/DefineSiteStructure/Tree.aspx"), @"';
            currentNodeId = ", nodeId, @";
            currentNode = $j('.ContentTreeSelectedItem').get(0);
            function ProcessRequest(action, param1, param2)
            {
                document.getElementById('hdnAction').value = action;
                document.getElementById('hdnParam1').value = param1;
                document.getElementById('hdnParam2').value = param2; "
                    , ClientScript.GetPostBackEventReference(this, null), @";
            }
            ");
            ScriptHelper.RegisterStartupScript(this, typeof(string), "startupScript", ScriptHelper.GetScript(startupScript.ToString()));
        }
        ScriptHelper.RegisterScriptFile(this, @"~/CMSModules/ImportExport/SiteManager/NewSite/DefineSiteStructure/tree.js");
    }

    #endregion


    #region "Postback handling"

    public void RaisePostBackEvent(string eventArgument)
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Current Node ID
        int nodeId = ValidationHelper.GetInteger(Param1, 0);

        TreeProvider tree = new TreeProvider(currentUser);
        EventLogProvider log = new EventLogProvider();

        string documentName = "";
        string action = Action.ToLower();

        // Process the request
        switch (action)
        {
            case "moveup":
            case "movedown":
                // Move the document up (document order)
                try
                {
                    if (nodeId == 0)
                    {
                        AddAlert(GetString("ContentRequest.ErrorMissingSource"));
                        return;
                    }

                    // Get document to move
                    TreeNode node = tree.SelectSingleNode(nodeId);

                    // Check the permissions for document
                    if (currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
                    {
                        switch (action)
                        {
                            case "moveup":
                                node = tree.MoveNodeUp(nodeId);
                                break;

                            case "movedown":
                                node = tree.MoveNodeDown(nodeId);
                                break;
                        }

                        string siteName = CMSContext.CurrentSiteName;
                        if (SettingsKeyProvider.GetBoolValue(siteName + ".CMSStagingLogChanges"))
                        {
                            // Load all nodes under parent node
                            if (node != null)
                            {
                                string parentPath = TreePathUtils.GetParentPath(node.NodeAliasPath);

                                DataSet ds = tree.SelectNodes(siteName, parentPath.TrimEnd('/') + "/%", TreeProvider.ALL_CULTURES, true, null, null, null, 1);

                                // Check if data source is not empty
                                if (!DataHelper.DataSourceIsEmpty(ds))
                                {
                                    // Go through all nodes
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        // Update child nodes
                                        int logNodeId = ValidationHelper.GetInteger(dr["NodeID"], 0);
                                        string culture = ValidationHelper.GetString(dr["DocumentCulture"], "");
                                        string className = ValidationHelper.GetString(dr["ClassName"], "");

                                        TreeNode tn = tree.SelectSingleNode(logNodeId, culture, className);
                                        DocumentSynchronizationHelper.LogDocumentChange(tn, TaskTypeEnum.UpdateDocument, tree);
                                    }
                                }
                            }
                        }

                        // Move the node
                        if (node != null)
                        {
                            documentName = node.DocumentName;

                            treeContent.ExpandNodeID = node.NodeParentID;
                            treeContent.NodeID = node.NodeID;
                        }
                        else
                        {
                            AddAlert(GetString("ContentRequest.MoveFailed"));
                        }
                    }
                    else
                    {
                        AddAlert(GetString("ContentRequest.MoveDenied"));
                    }
                }
                catch (Exception ex)
                {
                    log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "MOVE", currentUser.UserID, currentUser.UserName, nodeId, documentName, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSite.SiteID, HTTPHelper.GetAbsoluteUri());
                    AddAlert(GetString("ContentRequest.MoveFailed") + " : " + ex.Message);
                }
                break;

            case "delete":
                // Delete the document
                try
                {
                    if (nodeId == 0)
                    {
                        AddAlert(GetString("DefineSiteStructure.ErrorMissingSource"));
                        return;
                    }

                    // Get the node
                    TreeNode node = tree.SelectSingleNode(nodeId);

                    // Delete the node
                    if (node != null)
                    {
                        treeContent.NodeID = node.NodeParentID;

                        node.Delete();

                        // Delete search index for given node
                        if (SearchIndexInfoProvider.SearchEnabled)
                        {
                            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Delete, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
                        }

                        if (node.NodeAliasPath == "/")
                        {
                            // Refresh root document
                            treeContent.NodeID = node.NodeID;
                            AddScript("SelectNode(" + node.NodeID + "); \n");
                        }
                        else
                        {
                            AddScript("SelectNode(" + node.NodeParentID + "); \n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddAlert(GetString("DefineSiteStructure.DeleteFailed") + " : " + ex.Message);
                }
                break;
        }
    }

    #endregion


    #region "Script methods"

    /// <summary>
    /// Adds the alert message to the output request window.
    /// </summary>
    /// <param name="message">Message to display</param>
    private void AddAlert(string message)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), message.GetHashCode().ToString(), ScriptHelper.GetAlertScript(message));
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), ScriptHelper.GetScript(script));
    }

    #endregion
}