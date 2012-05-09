using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.Forums;
using CMS.CMSHelper;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Forums_Controls_Posts_PostListing : CMSAdminEditControl
{
    #region "Variables & enums"

    private int mPostId = 0;
    private int mForumId = 0;
    private int mCommunityGroupID = 0;
    private ForumPostInfo mPostInfo = null;

    private CurrentUserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;

    protected enum Action
    {
        Approve = 0,
        ApproveSubTree = 1,
        Reject = 2,
        RejectSubTree = 3,
        Delete = 4
    }

    protected enum What
    {
        Selected = 0,
        All = 1
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets ID of the parent post, its child-posts are displayed.
    /// </summary>
    public int PostId
    {
        get
        {
            return mPostId;
        }
        set
        {
            mPostId = value;
            mPostInfo = null;
        }
    }


    /// <summary>
    /// Gets or sets forums id. Is is used when postid is 0 = forum thread should be displayed.
    /// </summary>
    public int ForumId
    {
        get
        {
            return mForumId;
        }
        set
        {
            mForumId = value;
        }
    }


    /// <summary>
    /// Gets community group ID.
    /// </summary>
    private int CommunityGroupID
    {
        get
        {
            // Community group id is not set
            if ((mCommunityGroupID == 0) && (ForumId != 0))
            {
                ForumInfo fi = ForumInfoProvider.GetForumInfo(ForumId);
                if (fi != null)
                {
                    ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
                    if (fgi != null)
                    {
                        mCommunityGroupID = fgi.GroupGroupID;
                    }
                }
            }

            return mCommunityGroupID;
        }
    }


    /// <summary>
    /// Gets or sets post info.
    /// </summary>
    public ForumPostInfo PostInfo
    {
        get
        {
            if ((mPostInfo == null) && (this.PostId > 0))
            {
                mPostInfo = ForumPostInfoProvider.GetForumPostInfo(this.PostId);

                // Update post id
                if (mPostInfo != null)
                {
                    mPostId = mPostInfo.PostId;
                }
            }

            return mPostInfo;
        }
        set
        {
            mPostInfo = value;

            // Update post id
            if (mPostInfo != null)
            {
                mPostId = mPostInfo.PostId;
            }
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        bool reloadTree = QueryHelper.GetBoolean("reloadtree", true);
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "PostListingScripts",
            ScriptHelper.GetScript(
            "function ViewPost(postId) { \n" +
            "   location.href = 'ForumPost_View.aspx?postid=' + postId+'&listingpost=" + this.PostId + ";" + this.ForumId + "'; \n" +
            "} \n" +
            "function SelectPost(postId, forumId) { \n" +
            "   location.href = 'ForumPost_Listing.aspx?postid=' + postId + ';' + forumId; \n" +
            "} \n" +

            "function SelectInTree(postId, force) { \n" +
            "   var treeFrame = parent.frames['posts_tree']; \n" +
            "   if (treeFrame != null) { \n" +
            "      // Refresh tree if necessary \n" +
            "      if ((treeFrame.selectedPostId != postId) || force) { \n" +
            "          treeFrame.RefreshTree(postId); \n" +
            "      } \n" +
            "   } \n" +
            "} \n" +
            "SelectInTree(" + this.PostId + ", false); \n"
        ));


        if (!RequestHelper.IsPostBack())
        {
            DataHelper.FillListControlWithEnum(typeof(Action), drpAction, "Forums.ListingActions.", null);
            drpAction.Items.Insert(0, new ListItem(GetString("general.SelectAction"), "-1"));
            //DataHelper.FillListControlWithEnum(typeof(What), drpWhat, "Forums.ListingWhat.", null);            
        }

        string where = (this.PostId > 0) ? "PostParentID = " + this.PostId : "PostParentID IS NULL AND PostForumID=" + this.ForumId;
        gridPosts.WhereCondition = where;

        gridPosts.ZeroRowsText = GetString("forums.listing.nochildposts");
        gridPosts.FilteredZeroRowsText = GetString("forums.listing.nochildpostssearch");

        //gridPosts.OnDataReload += gridPosts_OnDataReload;
        gridPosts.OnExternalDataBound += gridPosts_OnExternalDataBound;
        gridPosts.OnAction += gridPosts_OnAction;
        btnOk.Click += btnOk_Click;

        currentUserInfo = CMSContext.CurrentUser;
        currentSiteInfo = CMSContext.CurrentSite;
    }


    /// <summary>
    /// OnPreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Hide action panel if no data
        pnlFooter.Visible = !DataHelper.DataSourceIsEmpty(gridPosts.GridView.DataSource);
    }

    #endregion


    #region "Grid events"

    /// <summary>
    /// External data binding handler.
    /// </summary>
    protected object gridPosts_OnExternalDataBound(object sender, string sourceName, object parameter)
    {        
        sourceName = sourceName.ToLower();
        switch (sourceName)
        {
            case "subposts":
                DataRowView data = ((GridViewRow)parameter).DataItem as DataRowView;
                int subposts = ValidationHelper.GetInteger(data["PostThreadPostsAbsolute"], 0);
                int postid = ValidationHelper.GetInteger(data["PostId"], 0);
                int postForumId = ValidationHelper.GetInteger(data["PostForumId"], 0);
                int postLevel = ValidationHelper.GetInteger(data["PostLevel"], 0);
                ImageButton btnSubPosts = sender as ImageButton;

                // Hide button if post has no child, thread counts itself = 1 means no subposts
                if ((subposts == 0) || ((subposts == 1) && (postLevel == 0)))
                {
                    btnSubPosts.Visible = false;
                }
                else
                {
                    btnSubPosts.ImageUrl = GetImageUrl("/CMSModules/CMS_Forums/subposts.png");
                    btnSubPosts.OnClientClick = "SelectPost(" + postid + "," + postForumId + ");return false;";
                }
                break;

            case "postapproved":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
        }

        return parameter;
    }


    protected void gridPosts_OnAction(string actionName, object actionArgument)
    {
        int postId = ValidationHelper.GetInteger(actionArgument, 0);
        if (actionName.ToLower() == "delete")
        {
            if (CommunityGroupID == 0)
            {
                // Check forums modify permissions
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
                {
                    RedirectToAccessDenied("cms.forums", CMSAdminControl.PERMISSION_MODIFY);
                }
            }
            else
            {
                // Check group permissions
                CMSGroupPage.CheckGroupPermissions(CommunityGroupID, CMSAdminControl.PERMISSION_MANAGE); 
            }

            ForumPostInfoProvider.DeleteForumPostInfo(postId);
        }
    }

    #endregion


    #region "Multiple action handling"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (CommunityGroupID == 0)
        {
            // Check forums modify permissions
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
            {
                RedirectToAccessDenied("cms.forums", CMSAdminControl.PERMISSION_MODIFY);
            }
        }
        else
        {
            // Check group permissions
            CMSGroupPage.CheckGroupPermissions(CommunityGroupID, CMSAdminControl.PERMISSION_MANAGE);
        }

        Action action = (Action)ValidationHelper.GetInteger(drpAction.SelectedValue, 0);
        What what = What.Selected; //(What)ValidationHelper.GetInteger(drpWhat.SelectedValue, 0);

        List<int> items = new List<int>();
        switch (what)
        {
            // Only selected posts
            case What.Selected:
                foreach (object item in gridPosts.SelectedItems)
                {
                    items.Add(ValidationHelper.GetInteger(item, 0));
                }
                break;

            // On posts in unigrid
            case What.All:
                DataSet ds = gridPosts.GridView.DataSource as DataSet;
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int postId = ValidationHelper.GetInteger(dr["PostId"], 0);
                        items.Add(postId);
                    }
                }
                break;
        }

        // For all specified forum posts
        foreach (int postId in items)
        {
            ForumPostInfo fpi = ForumPostInfoProvider.GetForumPostInfo(postId);
            if (fpi != null)
            {
                switch (action)
                {
                    // Approve post
                    case Action.Approve:
                        fpi.Approve();
                        break;

                    // Approve subtree
                    case Action.ApproveSubTree:
                        fpi.Approve(CMSContext.CurrentUser.UserID, true);
                        break;

                    // Reject post
                    case Action.Reject:
                        fpi.Reject();
                        break;

                    // Reject subtree
                    case Action.RejectSubTree:
                        fpi.Reject(true);
                        break;

                    // Delete post
                    case Action.Delete:
                        ForumPostInfoProvider.DeleteForumPostInfo(fpi);
                        break;
                }
            }
        }

        // If something happened
        if (items.Count > 0)
        {
            // Get rid of selection
            gridPosts.ResetSelection();

            // Reload unigrid to see changes
            gridPosts.ReloadData();

            // Force refresh post tree
            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "RefreshPostTree", ScriptHelper.GetScript("SelectInTree(" + this.PostId + ", true);"));
        }
    }

    #endregion
}
