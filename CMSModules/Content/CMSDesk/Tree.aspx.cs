using System;
using System.Web.UI;
using System.Data;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SiteProvider;

public partial class CMSModules_Content_CMSDesk_Tree : CMSContentPage, IPostBackEventHandler
{
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


    /// <summary>
    /// Scrollbar position.
    /// </summary>
    protected int ScrollPosition
    {
        get
        {
            return ValidationHelper.GetInteger(Request.Form["hdnScroll"], 0);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the dialog script
        ScriptHelper.RegisterScriptFile(this, @"~/CMSModules/Content/CMSDesk/tree.js");
        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterJQuery(Page);

        AddScript(string.Format("treeUrl = '{0}'; var contentDir = '{1}';\n",
            ResolveUrl("~/CMSModules/Content/CMSDesk/Tree.aspx"),
            ResolveUrl("~/CMSModules/Content/CMSDesk/")));

        treeContent.NodeTextTemplate = string.Format("{0}<span class=\"ContentTreeItem\" onclick=\"SelectNode(##NODEID##, this); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>{1}", ContextMenuContainer.GetStartTag("nodeMenu", "##NODEID##", false, false), ContextMenuContainer.GetEndTag(false));
        treeContent.SelectedNodeTextTemplate = string.Format("{0}<span id=\"treeSelectedNode\" class=\"ContentTreeSelectedItem\" onclick=\"SelectNode(##NODEID##, this); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>{1}", ContextMenuContainer.GetStartTag("nodeMenu", "##NODEID##", false, false), ContextMenuContainer.GetEndTag(false));
        treeContent.MaxTreeNodeText = string.Format("<span class=\"ContentTreeItem\" onclick=\"Listing(##PARENTNODEID##, this); return false;\"><span class=\"Name\" style=\"font-style: italic;\">{0}</span></span>", GetString("ContentTree.SeeListing"));
        treeContent.SelectPublishedData = false;

        if (!string.IsNullOrEmpty(CMSContext.CurrentUser.UserStartingAliasPath))
        {
            treeContent.Path = CMSContext.CurrentUser.UserStartingAliasPath;
        }

        if (!Page.IsCallback)
        {
            // If nodeId set, init the list of the nodes to expand
            int expandNodeId = QueryHelper.GetInteger("expandnodeid", 0);
            int nodeId = QueryHelper.GetInteger("nodeid", 0);

            if (RequestHelper.IsPostBack())
            {
                nodeId = ValidationHelper.GetInteger(Param1, 0);
                if (Action.ToLower() == "refresh")
                {
                    expandNodeId = ValidationHelper.GetInteger(Param2, 0);
                }
            }

            // Current Node ID
            treeContent.NodeID = nodeId;
            treeContent.ExpandNodeID = expandNodeId;

            string script = null;

            // Setup the current node script
            if ((nodeId > 0) && !RequestHelper.IsPostBack())
            {
                script += string.Format("currentNodeId = {0};\n", nodeId);
            }

            script +=
@"function ProcessRequest(action, param1, param2){
    var elm = jQuery('#" + pnlTreeArea.ClientID + @"');
    jQuery('#hdnScroll').val(elm.scrollTop());
    jQuery('#hdnAction').val(action); 
    jQuery('#hdnParam1').val(param1); 
    jQuery('#hdnParam2').val(param2);" +
    ClientScript.GetPostBackEventReference(this, null) + "}\n";

            AddScript(script);

            imgRefresh.ImageUrl = GetImageUrl("CMSModules/CMS_Content/ContextMenu/Refresh.png");
            imgRefresh.AlternateText = GetString("general.refresh");
            imgRefresh.Attributes.Add("onclick", "RefreshTree(" + nodeId + ",null)");
            AddScript(string.Format("var refreshIconID = \"{0}\";", imgRefresh.ClientID));
        }
    }


    public void RaisePostBackEvent(string eventArgument)
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Current Node ID
        int nodeId = ValidationHelper.GetInteger(Param1, 0);

        TreeProvider tree = new TreeProvider(currentUser);
        EventLogProvider log = new EventLogProvider();

        string documentName = string.Empty;
        string action = Action.ToLower();
        string siteName = CMSContext.CurrentSiteName;

        // Process the request
        switch (action)
        {
            case "refresh":
                treeContent.NodeID = nodeId;
                AddScript("currentNodeId = " + nodeId + ";\n");

                break;

            case "moveup":
            case "movedown":
            case "movetop":
            case "movebottom":
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

                            case "movetop":
                                node = tree.SelectSingleNode(nodeId);
                                tree.SetNodeOrder(nodeId, DocumentOrderEnum.First);
                                break;

                            case "movebottom":
                                node = tree.SelectSingleNode(nodeId);
                                tree.SetNodeOrder(nodeId, DocumentOrderEnum.Last);
                                break;
                        }

                        if (node != null)
                        {
                            // Log the synchronization tasks for the entire tree level
                            if (SettingsKeyProvider.GetBoolValue(siteName + ".CMSStagingLogChanges"))
                            {
                                // Log the synchronization tasks for the entire tree level
                                DocumentSynchronizationHelper.LogDocumentChangeOrder(siteName, node.NodeAliasPath, tree);
                            }

                            // Select the document in the tree
                            documentName = node.DocumentName;

                            treeContent.ExpandNodeID = node.NodeParentID;
                            treeContent.NodeID = node.NodeID;
                            AddScript("currentNodeId = " + node.NodeID + ";\n");
                        }
                        else
                        {
                            AddAlert(GetString("ContentRequest.MoveFailed"));
                        }
                    }
                    else
                    {
                        // Select the document in the tree
                        treeContent.NodeID = nodeId;

                        AddAlert(GetString("ContentRequest.MoveDenied"));
                    }
                }
                catch (Exception ex)
                {
                    log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "MOVE", currentUser.UserID, currentUser.UserName, nodeId, documentName, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSite.SiteID, HTTPHelper.GetAbsoluteUri());
                    AddAlert(GetString("ContentRequest.MoveFailed") + " : " + ex.Message);
                }
                break;

            case "setculture":
                // Set the preferred culture code
                try
                {
                    // Set the culture code
                    string language = ValidationHelper.GetString(Param2, "");
                    if (!string.IsNullOrEmpty(language))
                    {
                        CMSContext.PreferredCultureCode = language;
                    }

                    // Refresh the document
                    if (nodeId > 0)
                    {
                        treeContent.NodeID = nodeId;

                        AddScript("SelectNode(" + nodeId + "); \n");
                    }
                }
                catch (Exception ex)
                {
                    log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "SETCULTURE", currentUser.UserID, currentUser.UserName, nodeId, documentName, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSite.SiteID, HTTPHelper.GetAbsoluteUri());
                    AddAlert(GetString("ContentRequest.ErrorChangeLanguage"));
                }
                break;

            // Sorting
            case "sortalphaasc":
            case "sortalphadesc":
            case "sortdateasc":
            case "sortdatedesc":
                // Set the preferred culture code
                try
                {
                    // Get document to sort
                    TreeNode node = tree.SelectSingleNode(nodeId);

                    // Check the permissions for document
                    if ((currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
                        && (currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.ExploreTree) == AuthorizationResultEnum.Allowed))
                    {
                        switch (action)
                        {
                            case "sortalphaasc":
                                tree.OrderNodesAlphabetically(nodeId, true);
                                break;

                            case "sortalphadesc":
                                tree.OrderNodesAlphabetically(nodeId, false);
                                break;

                            case "sortdateasc":
                                tree.OrderNodesByDate(nodeId, true);
                                break;

                            case "sortdatedesc":
                                tree.OrderNodesByDate(nodeId, false);
                                break;
                        }

                        // Log the synchronization tasks for the entire tree level
                        if (SettingsKeyProvider.GetBoolValue(siteName + ".CMSStagingLogChanges"))
                        {
                            // Log the synchronization tasks for the entire tree level
                            string fakeAlias = node.NodeAliasPath.TrimEnd('/') + "/child";
                            DocumentSynchronizationHelper.LogDocumentChangeOrder(siteName, fakeAlias, tree);
                        }
                    }
                    else
                    {
                        AddAlert(GetString("ContentRequest.SortDenied"));
                    }

                    // Refresh the tree
                    if (nodeId > 0)
                    {
                        treeContent.ExpandNodeID = nodeId;
                        treeContent.NodeID = nodeId;
                        AddScript("SelectNode(" + nodeId + "); \n");
                    }
                }
                catch (Exception ex)
                {
                    log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "SORT", currentUser.UserID, currentUser.UserName, nodeId, documentName, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSite.SiteID, HTTPHelper.GetAbsoluteUri());
                    AddAlert(GetString("ContentRequest.ErrorSort"));
                }
                break;
        }

        // Maintain scrollbar position
        string script =
@"var elm = jQuery('#handle_" + nodeId + @"');
  var pnl = jQuery('#" + pnlTreeArea.ClientID + @"');
  var origScroll = " + ScrollPosition + @";
  var elmOff = elm.offset();
  var elmPos = (elmOff == null) ? 0 : elmOff.top;
  var scroll = ((elmPos < origScroll) || (elmPos > (origScroll + pnl.height())));
  pnl.scrollTop(origScroll);
  if(scroll){pnl.animate({ scrollTop: elmPos - 20 }, 300);};";

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "MaintainScrollbar", script, true);
    }


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