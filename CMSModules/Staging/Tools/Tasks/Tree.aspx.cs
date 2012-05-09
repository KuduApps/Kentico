using System;

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Staging_Tools_Tasks_Tree : CMSStagingTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // check 'Manage servers' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageDocumentsTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDocumentsTasks");
        }

        ltlScript.Text = ScriptHelper.GetScript("treeUrl = '" + ResolveUrl("~/CMSModules/Staging/Tools/Tasks/tree.aspx") + "';");

        treeContent.NodeTextTemplate = "<span id=\"##NODEID##\" class=\"ContentTreeItem\" onclick=\"SelectNode(##NODEID##); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        treeContent.SelectedNodeTextTemplate = "<span id=\"##NODEID##\" class=\"ContentTreeSelectedItem\" onclick=\"SelectNode(##NODEID##); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        treeContent.MaxTreeNodeText = "<span id=\"##NODEID##\" class=\"ContentTreeItem\" onclick=\"SelectNode(##PARENTNODEID##, true); return false;\"><span class=\"Name\" style=\"font-style: italic;\">" + GetString("ContentTree.SeeListing") + "</span></span>";
        // If no node specified, select root node id
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        if (nodeId <= 0)
        {
            // Get the root node
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode rootNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", TreeProvider.ALL_CULTURES, false, null, false);
            if (rootNode != null)
            {
                nodeId = rootNode.NodeID;
            }
        }

        // If nodeId set, init the list of the nodes to expand
        int expandNodeId = QueryHelper.GetInteger("expandnodeid", 0);
        treeContent.ExpandNodeID = expandNodeId;

        // Current Node ID
        treeContent.NodeID = nodeId;

        // Setup the current node script
        if (nodeId > 0)
        {
            ltlScript.Text += ScriptHelper.GetScript("    currentNodeId = " + nodeId + ";");
        }
    }
}
