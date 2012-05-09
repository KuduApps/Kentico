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

public partial class CMSModules_Forums_Tools_Posts_ForumPost_Listing : CMSForumsPage
{
    private int postId = 0;
    private int forumId = 0;
    private ForumPostInfo postInfo = null;

    protected void Page_Load(object sender, EventArgs e)
    {        
        string currentForumPost = "";

        string[] post = QueryHelper.GetString("postid", "").Split(';');
        postId = ValidationHelper.GetInteger(post[0], 0);
        ForumContext.CheckSite(0, 0, postId);

        if (post.Length >= 2)
        {
            forumId = ValidationHelper.GetInteger(post[1], 0);
            this.postListing.ForumId = forumId;
        }

        // Show current post
        postInfo = ForumPostInfoProvider.GetForumPostInfo(postId);
        if (postInfo != null)
        {
            currentForumPost = HTMLHelper.HTMLEncode(postInfo.PostSubject);
            this.postListing.PostInfo = postInfo;
        }
        // If not post, show current forum
        else if (forumId > 0)
        {
            ForumInfo fi = ForumInfoProvider.GetForumInfo(forumId);
            if (fi != null)
            {
                currentForumPost = fi.ForumDisplayName;
            }
        }

        this.postListing.IsLiveSite = false;

        this.InitializeMasterPage(currentForumPost);
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage(string currentForumPost)
    {
        this.Title = "Forum post listing";
        CurrentMaster.Title.TitleText = GetString("Forums.Listing.Title");        
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Forums/postlisting.png");

        if (postInfo != null)
        {
            string[,] actions = new string[1, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("Forums.ParentPost");
            actions[0, 2] = null;
            actions[0, 3] = "javascript:SelectPost(" + postInfo.PostParentID + ", " + postInfo.PostForumID + ");";
            actions[0, 4] = null;            
            actions[0, 5] = GetImageUrl("CMSModules/CMS_Forums/postparent.png");

            CurrentMaster.HeaderActions.Actions = actions;
        }
    }
}
