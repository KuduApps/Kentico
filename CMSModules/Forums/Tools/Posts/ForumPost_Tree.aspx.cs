using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Forums_Tools_Posts_ForumPost_Tree : CMSForumsPage
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        treeElem.TreeView.CssClass = "ContentTree";
        treeElem.ItemCssClass = "ContentTreeItem";
        treeElem.SelectedItemCssClass = "ContentTreeSelectedItem";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int postId = QueryHelper.GetInteger("postid", 0);
        int forumId = QueryHelper.GetInteger("forumid", 0);

        ForumContext.CheckSite(0, forumId, postId);

        if (postId > 0)
        {
            treeElem.Selected = postId;
        }

        ltlScript.Text = "var selectedPostId = " + postId + ";";
        ltlScript.Text += "function ShowPost(showId){ selectedPostId = showId; \n if(showId==-1) {parent.frames['posts_edit'].location.href = 'ForumPost_View.aspx?forumid=' + " + forumId + ";} else {parent.frames['posts_edit'].location.href = 'ForumPost_View.aspx?postid=' + showId;}} \n";
        ltlScript.Text += "var currentNode = document.getElementById('treeSelectedNode');\n function SelectForumNode(nodeElem){\n if(currentNode != null)  { currentNode.className = 'ContentTreeItem'; } \n if ( nodeElem != null )\n { currentNode = nodeElem;  currentNode.className = 'ContentTreeSelectedItem'; } \n}";

        ltlScript.Text += @"
        // Display listing
        function Listing(postId) {
            if (postId == null) {
                parent.frames['posts_edit'].location.href = 'ForumPost_Listing.aspx?postid=0;" + forumId + @"';
            } else {
                parent.frames['posts_edit'].location.href = 'ForumPost_Listing.aspx?postid='+postId;
            }
        }";


        ltlScript.Text += @"
        // Refresh tree and select post
        function RefreshTree(postId) {
            location.replace('ForumPost_Tree.aspx?postid='+postId+'&forumid=" + forumId + @"');
        }";

        // Wrap with script tag
        ltlScript.Text = ScriptHelper.GetScript(ltlScript.Text);

        // "Click here for more" template
        treeElem.MaxTreeNodeText = "<span class=\"ContentTreeItem\" onclick=\"Listing(##PARENTNODEID##); return false;\"><span class=\"Name\" style=\"font-style: italic;\">" + GetString("general.seelisting") + "</span></span>";

        this.treeElem.ForumID = forumId;

        // Setup the treeview                
        this.treeElem.AdministrationMode = true;
        this.treeElem.SelectOnlyApproved = false;
        this.treeElem.UseMaxPostNodes = true;
        this.treeElem.IsLiveSite = false;

        this.treeElem.OnGetPostIconUrl += new CMSModules_Forums_Controls_PostTree.GetIconEventHandler(PostTree1_OnGetPostIconUrl);        
    }


    /// <summary>
    /// Sets icon handler.
    /// </summary>
    string PostTree1_OnGetPostIconUrl(CMS.Forums.ForumPostTreeNode node)
    {
        string imageUrl = "";

        if (node != null)
        {
            imageUrl = GetImageUrl("CMSModules/CMS_Forums/post16.png");
            if (!ValidationHelper.GetBoolean(((DataRow)node.ItemData)["PostApproved"], false))
            {
                imageUrl = GetImageUrl("CMSModules/CMS_Forums/rejected16.png");
            }
        }

        return imageUrl;
    }
}
