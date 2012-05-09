using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.MessageBoard;
using CMS.UIControls;
using CMS.CMSHelper;

using TreeElemNode = System.Web.UI.WebControls.TreeNode;

public partial class CMSModules_MessageBoards_Content_Properties_Tree : CMSContentMessageBoardsPage
{
    #region "Private variables"

    private string navUrl = null;
    private string groupNavUrl = null;
    private int docId = 0;
    private bool selectedSet = false;
    private string selectedNodeName = String.Empty;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize page
        navUrl = ResolveUrl("~/CMSModules/MessageBoards/Tools/Boards/Board_Edit.aspx") + "?changemaster=0";
        groupNavUrl = ResolveUrl("~/CMSModules/Groups/Tools/MessageBoards/Boards/Board_Edit.aspx") + "?changemaster=0";
        PageStatusContainer.Controls.Add(new LiteralControl("<div class=\"TreeMenu TreeMenuPadding\" style=\"height: 25px;\">&nbsp;</div>"));

        // Delete action
        string[,] actions = new string[1, 11];
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("general.delete");
        actions[0, 2] = "return confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ");";
        actions[0, 5] = GetImageUrl("Design/Controls/UniGrid/Actions/delete.png");
        actions[0, 6] = "delete";
        actions[0, 8] = "true";
        actionsElem.ActionPerformed += HeaderActions_ActionPerformed;
        actionsElem.Actions = actions;

        // Ensure RTL
        treeElem.LineImagesFolder = GetImageUrl(CultureHelper.IsUICultureRTL() ? "RTL/Design/Controls/Tree/" : "Design/Controls/Tree/", false, true);

        selectedNodeName = QueryHelper.GetString("selectednodename", string.Empty);

        RegisterScripts();
        PopulateTree();
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "delete":
                // Check 'Modify' permission
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.MessageBoards", "Modify"))
                {
                    RedirectToAccessDenied("CMS.MessageBoards", "Modify");
                }

                BoardInfoProvider.DeleteBoardInfo(ValidationHelper.GetInteger(hdnBoardId.Value, 0));
                ltlScript.Text += ScriptHelper.GetScript("parent.frames['main'].location.href = '" + ResolveUrl("~/CMSPages/blank.htm") + "'");
                ltlScript.Text += ScriptHelper.GetScript("window.location.replace(window.location);");
                break;
        }
    }


    #region "Private methods"

    /// <summary>
    /// Populates the tree with the data.
    /// </summary>
    private void PopulateTree()
    {
        // Create root node
        TreeElemNode rootNode = new TreeElemNode();
        rootNode.Text = "<span class=\"ContentTreeItem\" \"><span class=\"Name\">" + GetString("board.header.messageboards") + "</span></span>";
        rootNode.Expanded = true;
        treeElem.Nodes.Add(rootNode);

        // Populate the tree
        docId = QueryHelper.GetInteger("documentid", 0);
        if (docId > 0)
        {
            DataSet ds = BoardInfoProvider.GetMessageBoards("BoardDocumentID = " + docId, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AddNode(Convert.ToString(dr["BoardDisplayName"]), ValidationHelper.GetInteger(dr["BoardId"], -1), ValidationHelper.GetInteger(dr["BoardGroupID"], 0) > 0);
                }
            }
        }
    }


    /// <summary>
    /// Registers all necessary scripts.
    /// </summary>
    private void RegisterScripts()
    {
        ltlScript.Text += ScriptHelper.GetScript(
            "var hiddenField = document.getElementById('" + hdnBoardId.ClientID + "');" +
            "var currentNode = document.getElementById('treeSelectedNode');" +
            "var currentNodeName = \"\";" +
            "" +
            "treeUrl = '" + ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/MessageBoards/tree.aspx") + "';" +
            "function SelectNode(nodeName, nodeElem, boardId, navUrl)" +
            "{" +
            "    if ((currentNode != null) && (nodeElem != null))" +
            "    {" +
            "        currentNode.className = 'ContentTreeItem';" +
            "    }" +
            "    " +
            "    parent.frames['main'].location.href = navUrl + '&boardid=' + boardId;" +
            "    currentNodeName = nodeName;" +
            "    " +
            "    if (nodeElem != null)" +
            "    {" +
            "        currentNode = nodeElem;" +
            "        if (currentNode != null)" +
            "        {" +
            "            currentNode.className = 'ContentTreeSelectedItem';" +
            "        }" +
            "    }" +
            "    if (hiddenField != null) {" +
            "        hiddenField.value = boardId;" +
            "    }" +
            "}");
    }


    /// <summary>
    /// Adds node to the root node.
    /// </summary>
    /// <param name="nodeName">Name of node</param>
    /// <param name="boardId">Message board identifier</param>
    private void AddNode(string nodeName, int boardId, bool group)
    {
        TreeElemNode newNode = new TreeElemNode();
        string cssClass = "ContentTreeItem";
        string elemId = string.Empty;

        string url = group ? groupNavUrl : navUrl;

        // Select proper node
        if ((!selectedSet) && String.IsNullOrEmpty(selectedNodeName))
        {
            if (!RequestHelper.IsPostBack())
            {
                hdnBoardId.Value = boardId.ToString();
            }
            ltlScript.Text += ScriptHelper.GetScript("parent.frames['main'].location.href = '" + url + "&boardid=" + boardId + "'");
            selectedSet = true;
            cssClass = "ContentTreeSelectedItem";
            elemId = "id=\"treeSelectedNode\"";
        }
        if (selectedNodeName == nodeName)
        {
            cssClass = "ContentTreeSelectedItem";
            elemId = "id=\"treeSelectedNode\"";
        }

        newNode.Text = "<span class=\"" + cssClass + "\" " + elemId + " onclick=\"SelectNode(" + ScriptHelper.GetString(HttpUtility.UrlEncode(nodeName)) + ", this, " + boardId + ", '" + url + "');\"><span class=\"Name\">" + HTMLHelper.HTMLEncode(nodeName) + "</span></span>";
        newNode.ImageUrl = GetImageUrl("Objects/Board_Board/list.png");
        newNode.NavigateUrl = "#";

        treeElem.Nodes[0].ChildNodes.Add(newNode);

        return;
    }

    #endregion
}
