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

public partial class CMSModules_Forums_Tools_Posts_ForumPost_View : CMSForumsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int postID = QueryHelper.GetInteger("postid", 0);
        int forumID = QueryHelper.GetInteger("forumId", 0);
        ForumContext.CheckSite(0, forumID, postID);

        this.postView.PostID = postID;
        this.postView.Reply = QueryHelper.GetInteger("reply", 0);
        this.postView.ForumID = forumID;
        this.postView.ListingPost = QueryHelper.GetString("listingpost", String.Empty);
        this.postView.IsLiveSite = false;
        
        // Register back to listing script
        if (!String.IsNullOrEmpty(this.postView.ListingPost))
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "BackToListing", ScriptHelper.GetScript(
                "function BackToListing() { location.href = '" + ResolveUrl("~/CMSModules/Forums/Tools/Posts/ForumPost_Listing.aspx?postid=" + ScriptHelper.GetString( this.postView.ListingPost,false)) + "'; }\n"));
        }

        // Intialize master page
        InitializeMasterPage();
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
