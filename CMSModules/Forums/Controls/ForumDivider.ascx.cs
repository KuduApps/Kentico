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
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Forums_Controls_ForumDivider : ForumViewer, INamingContainer
{
    #region "Private variables"

    // Default path to the forum layouts directory
    private const string defaultPath = "~/CMSModules/Forums/Controls/Layouts/";
    // Current control
    private Control ctrl = null;
    // Indicates starting mode, 0 - Group, 1 - Forum
    private int startingMode = -1;
    // Current forum state
    ForumStateEnum currentState = ForumStateEnum.Unknown;
    // group name
    private string mGroupName = "";
    // forum name
    private string mForumName = "";
    // Indicates whether current control is search results control
    private bool mSearchResult = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether current control is search result.
    /// </summary>
    public bool SearchResult
    {
        get
        {
            return mSearchResult;
        }
        set
        {
            mSearchResult = value;
        }
    }


    /// <summary>
    /// Gets or sets the forum id.
    /// </summary>
    public override int ForumID
    {
        get
        {
            return base.ForumID;
        }
        set
        {
            base.ForumID = value;
            startingMode = 1;
            ForumContext.ForumID = value;
        }
    }


    /// <summary>
    /// Gets or sets the group id.
    /// </summary>
    public override int GroupID
    {
        get
        {
            return base.GroupID;
        }
        set
        {
            if (!this.StopProcessing && !ForumContext.StopViewerLoading)
            {
                base.GroupID = value;
                ForumContext.GroupID = value;
                startingMode = 0;
                // Check whether forum is defined
                if ((ForumContext.CurrentForum != null) && (currentState != ForumStateEnum.Forums))
                {
                    // For nested level call request to single display
                    if ((ForumContext.CurrentGroup != null) && (ForumContext.CurrentForum.ForumGroupID == ForumContext.CurrentGroup.GroupID))
                    {
                        ForumContext.DisplayOnlyMe(this);
                        ForumContext.StopViewerLoading = true;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the community group id.
    /// </summary>
    public override int CommunityGroupID
    {
        get
        {
            return base.CommunityGroupID;
        }
        set
        {
            base.CommunityGroupID = value;
            ForumContext.CommunityGroupID = value;
        }
    }


    /// <summary>
    /// Gets or sets current group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            return mGroupName;
        }
        set
        {
            ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(value, this.SiteID, this.CommunityGroupID);
            if (fgi != null)
            {
                this.GroupID = fgi.GroupID;
            }
            mGroupName = value;
        }
    }


    /// <summary>
    /// Gets or sets current forum name.
    /// </summary>
    public string ForumName
    {
        get
        {
            return mForumName;
        }
        set
        {
            ForumInfo fi = ForumInfoProvider.GetForumInfo(value, SiteID, CommunityGroupID);
            if (fi != null)
            {
                this.ForumID = fi.ForumID;
            }

            if ((!String.IsNullOrEmpty(value)) && (value == "adhocforumgroup" || value == "ad_hoc_forum"))
            {
                this.IsAdHocForum = true;
                fi = ForumInfoProvider.GetForumInfoByDocument(CMSContext.CurrentDocument.DocumentID);
                if (fi != null)
                {
                    this.ForumID = fi.ForumID;
                }
                else
                {
                    this.ForumID = -1;
                }
            }
            startingMode = 1;
            mForumName = value;
        }
    }

    #endregion


    #region "Methods"


    /// <summary>
    /// Check permissions.
    /// </summary>
    /// <param name="state">Current state</param>
    public ForumStateEnum CheckPermissions(ForumStateEnum state)
    {
        // Return original state for selected types
        if ((state == ForumStateEnum.Forums) || (state == ForumStateEnum.Unknown) || (state == ForumStateEnum.Search))
        {
            return state;
        }

        // If forum doesn't exist display default
        if (ForumContext.CurrentForum == null)
        {
            return ForumStateEnum.Forums;
        }

        // If forum is closed => hide
        if ((!IsAdHocForum) && (!ForumContext.CurrentForum.ForumOpen))
        {
            return ForumStateEnum.Forums;
        }

        // Sets threads state for every action if forum is locked
        if (ForumContext.CurrentForum.ForumIsLocked)
        {
            switch (state)
            {
                case ForumStateEnum.NewSubscription:
                case ForumStateEnum.NewThread:
                case ForumStateEnum.ReplyToPost:
                case ForumStateEnum.SubscribeToPost:
                case ForumStateEnum.Attachments:
                    return ForumStateEnum.Threads;
            }
        }

        // If user is global admin, forum admin, community admin or modrator
        if (ForumContext.UserIsModerator(ForumContext.CurrentForum.ForumID, this.CommunityGroupID))
        {
            return state;
        }

        // Sets thread state for locked post 
        if ((ForumContext.CurrentThread != null) && (ForumContext.CurrentThread.PostIsLocked))
        {
            if (!ForumContext.UserIsModerator(ForumContext.CurrentForum.ForumID, this.CommunityGroupID))
            {
                switch (state)
                {
                    case ForumStateEnum.NewSubscription:
                    case ForumStateEnum.SubscribeToPost:
                    case ForumStateEnum.NewThread:
                    case ForumStateEnum.ReplyToPost:
                    case ForumStateEnum.Attachments:
                        return ForumStateEnum.Thread;
                }
            }
        }

        bool hasPermissions = true;

        // Check permissions for action
        switch (state)
        {
            case ForumStateEnum.ReplyToPost:
                hasPermissions = ForumViewer.CheckPermission("Reply", ForumContext.CurrentForum.AllowReply, ForumContext.CurrentForum.ForumGroupID, ForumContext.CurrentForum.ForumID);
                break;
            case ForumStateEnum.NewThread:
                hasPermissions = ForumViewer.CheckPermission("Post", ForumContext.CurrentForum.AllowPost, ForumContext.CurrentForum.ForumGroupID, ForumContext.CurrentForum.ForumID);
                break;
            case ForumStateEnum.Attachments:
                hasPermissions = ForumViewer.CheckPermission("AttachFiles", ForumContext.CurrentForum.AllowAttachFiles, ForumContext.CurrentForum.ForumGroupID, ForumContext.CurrentForum.ForumID);
                break;
            case ForumStateEnum.TopicMove:
                hasPermissions = ForumContext.UserIsModerator(ForumContext.CurrentForum.ForumID, this.CommunityGroupID);
                break;
            case ForumStateEnum.SubscribeToPost:
            case ForumStateEnum.NewSubscription:
                hasPermissions = ForumViewer.CheckPermission("Subscribe", ForumContext.CurrentForum.AllowSubscribe, ForumContext.CurrentForum.ForumGroupID, ForumContext.CurrentForum.ForumID) && this.EnableSubscription;
                break;
            case ForumStateEnum.EditPost:
                hasPermissions = ForumContext.UserIsModerator(ForumContext.CurrentForum.ForumID, this.CommunityGroupID) || (ForumContext.CurrentForum.ForumAuthorEdit && (ForumContext.CurrentPost != null && !CMSContext.CurrentUser.IsPublic() && (ForumContext.CurrentPost.PostUserID == CMSContext.CurrentUser.UserID)));
                break;
        }

        // Check ForumAccess permission
        if (ForumContext.CurrentForum != null)
        {
            hasPermissions = hasPermissions && ForumViewer.CheckPermission("AccessToForum", ForumContext.CurrentForum.AllowAccess, ForumContext.CurrentForum.ForumGroupID, ForumContext.CurrentForum.ForumID);
        }

        // Check whether user has permissions for selected state
        if (!hasPermissions)
        {
            // Check whether public user should be redirected to logon page
            if (this.RedirectUnauthorized && CMSContext.CurrentUser.IsPublic())
            {
                URLHelper.Redirect(URLHelper.AddParameterToUrl(ResolveUrl(this.LogonPageURL), "returnurl", HttpUtility.UrlEncode(URLHelper.CurrentURL)));
            }
            else if (!String.IsNullOrEmpty(this.AccessDeniedPageURL))
            {
                URLHelper.Redirect(URLHelper.AddParameterToUrl(ResolveUrl(this.AccessDeniedPageURL), "returnurl", HttpUtility.UrlEncode(URLHelper.CurrentURL)));
            }
            // Sets state with dependence on current settings
            else
            {
                if (startingMode == 0)
                {
                    return ForumStateEnum.Forums;
                }
                else
                {
                    return ForumStateEnum.AccessDenied;
                }
            }
        }

        return state;
    }


    /// <summary>
    /// OnInit - handle single display request.
    /// </summary>
    /// <param name="e">Event args</param>
    protected override void OnInit(EventArgs e)
    {
        // Add current viewer to the viewers collection
        ForumContext.ForumViewers.Add(this);
        base.OnInit(e);
    }


    /// <summary>
    /// Page_Load - Set properties for current control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check stop processing
            if (this.StopProcessing)
            {
                return;
            }

            EnsureUnsubscription();

            if (!this.SearchResult)
            {
                ForumContext.GroupID = this.GroupID;
                ForumContext.ForumID = this.ForumID;
                ForumContext.CommunityGroupID = this.CommunityGroupID;
            }

            // If search result control, check whether search is performed
            if (this.SearchResult && (ForumContext.CurrentState != ForumStateEnum.Search))
            {
                return;
            }

            // Get current state
            currentState = CheckPermissions(ForumContext.CurrentState);

            if (currentState != ForumStateEnum.Search)
            {
                // Group
                if (startingMode == 0)
                {
                    // Check whether forum is defined
                    if ((ForumContext.CurrentForum != null) && (currentState != ForumStateEnum.Forums))
                    {
                        // For nested level call request to single display
                        if ((ForumContext.CurrentGroup != null) && (ForumContext.CurrentForum.ForumGroupID == ForumContext.CurrentGroup.GroupID))
                        {
                            ForumContext.DisplayOnlyMe(this);
                        }
                        else
                        {
                            // Hide current forum because ids not match
                            return;
                        }

                    }
                }
                // Forum
                else if (startingMode == 1)
                {
                    // Hide all others forums
                    ForumContext.DisplayOnlyMe(this);
                }
                else
                {
                    // Hide forum because none of mandatory property was set
                    return;
                }
            }


            // Display correspondent control with dependence on current mode
            switch (currentState)
            {
                // Threads
                case ForumStateEnum.Threads:
                    ctrl = LoadControl(defaultPath + ForumLayout + "/Threads.ascx");
                    ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "threadsElem", ctrl);
                    break;

                // Thread
                case ForumStateEnum.Thread:
                    // Log thread views
                    if ((ForumContext.CurrentThread != null) && (!QueryHelper.Contains("tpage")))
                    {
                        ThreadViewCounter.LogThreadView(ForumContext.CurrentThread.PostId);
                    }
                    ctrl = LoadControl(defaultPath + ForumLayout + "/Thread.ascx");
                    ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "threadElem", ctrl);
                    break;

                // New post, reply or edit post
                case ForumStateEnum.NewThread:
                case ForumStateEnum.ReplyToPost:
                case ForumStateEnum.EditPost:
                    ctrl = LoadControl(defaultPath + ForumLayout + "/ThreadEdit.ascx");
                    ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "editElem", ctrl);
                    break;

                // Subscription to forum or subscription to post
                case ForumStateEnum.NewSubscription:
                case ForumStateEnum.SubscribeToPost:
                    ctrl = LoadControl(defaultPath + ForumLayout + "/SubscriptionEdit.ascx");
                    ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "subscriptionElem", ctrl);
                    break;

                // Forums
                case ForumStateEnum.Forums:
                    ctrl = LoadControl(defaultPath + ForumLayout + "/Forums.ascx");
                    ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "forumsElem", ctrl);
                    break;

                case ForumStateEnum.Attachments:
                    ctrl = LoadControl(defaultPath + ForumLayout + "/Attachments.ascx");
                    ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "attachmentElem", ctrl);
                    break;

                case ForumStateEnum.Search:
                    if (this.SearchResult)
                    {
                        ctrl = LoadControl(defaultPath + ForumLayout + "/SearchResults.ascx");
                        ctrl.ID = ControlsHelper.GetUniqueID(this.plcForum, "searchElem", ctrl);
                    }
                    else
                    {
                        return;
                    }
                    break;

                case ForumStateEnum.AccessDenied:
                    this.Visible = false;
                    return;

                // Unknown
                case ForumStateEnum.Unknown:
                    if (!this.SearchResult)
                    {
                        throw new Exception("[Forum divider]: Unknown forum state.");
                    }
                    return;
            }

            // Clear controls collection
            plcForum.Controls.Clear();

            // Add loaded control to the control collection
            plcForum.Controls.Add(ctrl);


            // Get forum viewer control
            ForumViewer frmv = ctrl as ForumViewer;

            // If control exists set forum properties
            if (frmv != null)
            {
                CopyValues(frmv);
            }
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }


    /// <summary>
    /// Redirect to the unsubscription page if current url contains valid unbscription informations.
    /// </summary>
    protected void EnsureUnsubscription()
    {
        Guid unsubGuid = QueryHelper.GetGuid("unsubscribe", Guid.Empty);
        if (unsubGuid != Guid.Empty)
        {
            int forumId = QueryHelper.GetInteger("forumid", 0);
            if (forumId > 0)
            {
                ForumSubscriptionInfo fsi = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(unsubGuid);
                if (fsi != null)
                {
                    ForumInfo fi = ForumInfoProvider.GetForumInfo(forumId);
                    if ((fi != null) && (fsi.SubscriptionForumID == fi.ForumID))
                    {
                        URLHelper.Redirect("~/CMSModules/Forums/CMSPages/unsubscribe.aspx?subGuid=" + unsubGuid.ToString());
                    }
                }
            }
        }
    }

    #endregion
}
