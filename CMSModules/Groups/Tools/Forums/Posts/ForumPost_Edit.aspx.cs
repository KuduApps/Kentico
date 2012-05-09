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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Posts_ForumPost_Edit : CMSGroupForumPage
{
    private int postId = 0;
    private string listingParameter = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentForumPost = "";

        // get forumPost id from querystring
        postId = QueryHelper.GetInteger("postid", 0);
        ForumPostInfo forumPostObj = ForumPostInfoProvider.GetForumPostInfo(postId);
        if (forumPostObj != null)
        {
            currentForumPost = HTMLHelper.HTMLEncode(forumPostObj.PostSubject);
        }

        CurrentMaster.PanelContent.CssClass = String.Empty;

        string listingPost = QueryHelper.GetString("listingpost", null);
        if (!String.IsNullOrEmpty(listingPost))
        {
            listingParameter = "&listingpost=" + HTMLHelper.HTMLEncode(listingPost);
        }

        this.postEdit.EditPostID = postId;
        this.postEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(postEdit_OnCheckPermissions);
        this.postEdit.OnSaved += new EventHandler(postEdit_OnSaved);
        this.postEdit.IsLiveSite = false;

        this.InitializeMasterPage(currentForumPost);
    }


    protected void postEdit_OnSaved(object sender, EventArgs e)
    {
        ForumPostInfo forumPostObj = ForumPostInfoProvider.GetForumPostInfo(postId);
        if (forumPostObj != null)
        {
            ltlScript.Text += ScriptHelper.GetScript("parent.frames['posts_tree'].location.href = 'ForumPost_Tree.aspx?postid=" + forumPostObj.PostId + "&forumid=" + forumPostObj.PostForumID + "';");
            ltlScript.Text += ScriptHelper.GetScript("parent.frames['posts_edit'].location.href = 'ForumPost_View.aspx?postid=" + forumPostObj.PostId + listingParameter + "';");

        }
    }


    protected void postEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        ForumPostInfo fpi = ForumPostInfoProvider.GetForumPostInfo(this.postEdit.EditPostID);
        if (fpi != null)
        {
            ForumInfo fi = ForumInfoProvider.GetForumInfo(fpi.PostForumID);
            if (fi != null)
            {
                ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
                if (fgi != null)
                {
                    groupId = fgi.GroupGroupID;
                }
            }
        }

        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage(string currentForumPost)
    {
        this.Title = "Forum Post edit";

        // initializes page title control		
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("ForumPost_Edit.ItemListLink");
        tabs[0, 1] = "~/CMSModules/Groups/Tools/Forums/Posts/ForumPost_View.aspx?postid=" + postId + listingParameter;
        tabs[0, 2] = "";
        tabs[1, 0] = currentForumPost;
        tabs[1, 1] = "";
        tabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = tabs;

        this.CurrentMaster.Title.TitleText = GetString("ForumPost_Edit.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Forums_ForumPost/object.png");
    }
}
