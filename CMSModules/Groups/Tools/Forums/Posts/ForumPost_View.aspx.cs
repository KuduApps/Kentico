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
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Posts_ForumPost_View : CMSGroupForumPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.postView.PostID = QueryHelper.GetInteger("postid", 0);
        this.postView.Reply = QueryHelper.GetInteger("reply", 0);
        this.postView.ForumID = QueryHelper.GetInteger("forumId", 0);
        this.postView.ListingPost = QueryHelper.GetString("listingpost", String.Empty);
        this.postView.IsLiveSite = false;

        // Register back to listing script
        if (!String.IsNullOrEmpty(this.postView.ListingPost))
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "BackToListing", ScriptHelper.GetScript(
                "function BackToListing() { location.href = '" + ResolveUrl("~/CMSModules/Groups/Tools/Forums/Posts/ForumPost_Listing.aspx?postid=" + ScriptHelper.GetString(this.postView.ListingPost, false)) + "'; }\n"));
        }

        this.postView.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(postView_OnCheckPermissions);

        InitializeMasterPage();
    }


    protected void postView_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        ForumPostInfo fpi = ForumPostInfoProvider.GetForumPostInfo(postView.PostID);
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

        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    /// <summary>
    /// Initializes MasterPage.
    /// </summary>
    protected void InitializeMasterPage()
    {
        this.Title = "Forum Post View";
        string listingParam = null;

        if (!String.IsNullOrEmpty(this.postView.ListingPost))
        {
            listingParam = "+ '&listingpost=" + HTMLHelper.HTMLEncode(postView.ListingPost) + "'";
        }

        // Register script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EditPost",
        ScriptHelper.GetScript("function EditPost(postId) { " +
            "if ( postId != 0 ) { parent.frames['posts_edit'].location.href = 'ForumPost_Edit.aspx?postid=' + postId" + listingParam + ";}}"));
    }
}
