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
using CMS.CMSHelper;
using CMS.Blogs;
using CMS.SiteProvider;
using CMS.EmailEngine;
using CMS.EventLog;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.URLRewritingEngine;
using CMS.PortalEngine;
using CMS.Controls;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Blogs_Controls_BlogCommentView : CMSAdminEditControl
{
    private BlogProperties mBlogProperties = new BlogProperties();
    private string mCommentDetailControlPath = "~/CMSModules/Blogs/Controls/BlogCommentDetail.ascx";
    private bool mReloadPageAfterAction = false;
    private bool mDisplayTrackbacks = false;

    private bool isUserAuthorized = false;

    private TreeNode mPostNode = null;

    private string mAliasPath = null;
    private string mCulture = null;
    private string mSiteName = null;
    private string mSeparator = null;

    protected string mAbuseReportRoles = null;
    protected SecurityAccessEnum mAbuseReportSecurityAccess = SecurityAccessEnum.AllUsers;
    protected int mAbuseReportOwnerID = 0;
    protected int mTrackbackURLSize = 0;


    #region "Public properties"

    /// <summary>
    /// FALSE - whole page is reloaded after any action occures (insert, edit, delete, approve comment), FALSE - only comment list is reloaded after action occures.
    /// </summary>
    public bool ReloadPageAfterAction
    {
        get
        {
            return mReloadPageAfterAction;
        }
        set
        {
            mReloadPageAfterAction = value;
        }
    }


    /// <summary>
    /// Post alias path.
    /// </summary>
    public string AliasPath
    {
        get
        {
            if (mAliasPath == null)
            {
                mAliasPath = CMSContext.CurrentPageInfo.NodeAliasPath;
            }
            return mAliasPath;
        }
        set
        {
            mAliasPath = value;
        }
    }


    /// <summary>
    /// Post culture.
    /// </summary>
    public string Culture
    {
        get
        {
            if (mCulture == null)
            {
                mCulture = CMSContext.PreferredCultureCode;
            }
            return mCulture;
        }
        set
        {
            mCulture = value;
        }
    }


    /// <summary>
    /// Post SiteName.
    /// </summary>
    public string SiteName
    {
        get
        {
            if (mSiteName == null)
            {
                mSiteName = CMSContext.CurrentSiteName;
            }
            return mSiteName;
        }
        set
        {
            mSiteName = value;
        }
    }


    /// <summary>
    /// Comment separator.
    /// </summary>
    public string Separator
    {
        get
        {
            return mSeparator;
        }
        set
        {
            mSeparator = value;
        }
    }


    /// <summary>
    /// Post document node.
    /// </summary>
    public TreeNode PostNode
    {
        get
        {
            if (mPostNode == null)
            {
                this.SetContext();

                // Get the document
                TreeProvider tree = new TreeProvider();
                mPostNode = tree.SelectSingleNode(this.SiteName, this.AliasPath, this.Culture, false, "CMS.BlogPost", true);
                if ((mPostNode != null) && (CMSContext.ViewMode != ViewModeEnum.LiveSite))
                {
                    mPostNode = DocumentHelper.GetDocument(mPostNode, tree);
                }

                this.ReleaseContext();
            }
            return mPostNode;
        }
        set
        {
            mPostNode = value;
        }
    }


    /// <summary>
    /// Blog properties.
    /// </summary>
    public BlogProperties BlogProperties
    {
        get
        {
            return mBlogProperties;
        }
        set
        {
            mBlogProperties = value;
        }
    }


    /// <summary>
    /// Comment detail control path.
    /// </summary>
    public string CommentDetailControlPath
    {
        get
        {
            return mCommentDetailControlPath;
        }
        set
        {
            mCommentDetailControlPath = value;
        }
    }


    /// <summary>
    /// Indicates whether post comments can be added to the post.
    /// </summary>
    public bool AreCommentsOpened
    {
        get
        {
            bool isOpened = false;

            // Get current post document info            
            if (this.PostNode != null)
            {
                CurrentUserInfo currrentUser = CMSContext.CurrentUser;

                if (!ValidationHelper.GetBoolean(this.PostNode.GetValue("BlogPostAllowComments"), false))
                {
                    // Comments are not allowed for current post
                    isOpened = false;
                }
                else
                {
                    // Check new comment dialog expiration
                    switch (this.BlogProperties.OpenCommentsFor)
                    {
                        case BlogProperties.OPEN_COMMENTS_ALWAYS:
                            isOpened = true;
                            break;

                        case BlogProperties.OPEN_COMMENTS_DISABLE:
                            isOpened = false;
                            break;

                        default:
                            DateTime postDate = ValidationHelper.GetDateTime(this.PostNode.GetValue("BlogPostDate"), DataHelper.DATETIME_NOT_SELECTED);
                            isOpened = (DateTime.Now <= postDate.AddDays(this.BlogProperties.OpenCommentsFor));

                            break;
                    }

                    if (currrentUser.IsPublic())
                    {
                        isOpened = (isOpened && this.BlogProperties.AllowAnonymousComments);
                    }
                }
            }

            return isOpened;
        }
    }


    /// <summary>
    /// Gets or sets list of roles (separated by ';') which are allowed to report abuse (in combination with SecurityAccess.AuthorizedRoles).
    /// </summary>
    public string AbuseReportRoles
    {
        get
        {
            return mAbuseReportRoles;
        }
        set
        {
            mAbuseReportRoles = value;
        }
    }


    /// <summary>
    /// Gets or sets the security access for report abuse link.
    /// </summary>
    public SecurityAccessEnum AbuseReportSecurityAccess
    {
        get
        {
            return mAbuseReportSecurityAccess;
        }
        set
        {
            mAbuseReportSecurityAccess = value;
        }
    }


    /// <summary>
    /// Gets or sets the owner ID (in combination with SecurityAccess.Owner).
    /// </summary>
    public int AbuseReportOwnerID
    {
        get
        {
            return mAbuseReportOwnerID;
        }
        set
        {
            mAbuseReportOwnerID = value;
        }
    }


    /// <summary>
    /// Gets or sets displaying trackbacks.
    /// </summary>
    public bool DisplayTrackbacks
    {
        get
        {
            return mDisplayTrackbacks;
        }
        set
        {
            mDisplayTrackbacks = value;
        }
    }


    /// <summary>
    /// Gets or sets number of characters after which the trackback URL is automatically wprapped, otherwise it is not wrapped which can break the design when url is too long.
    /// </summary>
    public int TrackbackURLSize
    {
        get
        {
            return mTrackbackURLSize;
        }
        set
        {
            mTrackbackURLSize = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.BlogProperties.StopProcessing)
        {
            this.Visible = false;
            return;
        }

        lblTitle.Text = GetString("Blog.CommentView.Comments");

        SetupControl();


        if (this.PostNode != null)
        {
            // Check permissions for blog
            if (this.BlogProperties.CheckPermissions)
            {
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(this.PostNode, NodePermissionsEnum.Read) != AuthorizationResultEnum.Allowed)
                {
                    this.Visible = false;
                }
            }

            if (this.Visible)
            {
                rptComments.ItemDataBound += new RepeaterItemEventHandler(rptComments_ItemDataBound);
                ctrlCommentEdit.OnAfterCommentSaved += new OnAfterCommentSavedEventHandler(ctrlCommentEdit_OnAfterCommentSaved);
                ctrlCommentEdit.OnBeforeCommentSaved += new OnBeforeCommentSavedEventHandler(ctrlCommentEdit_OnBeforeCommentSaved);
                ctrlCommentEdit.UseCaptcha = this.BlogProperties.UseCaptcha;
                ctrlCommentEdit.EnableSubscriptions = this.BlogProperties.EnableSubscriptions;
                ctrlCommentEdit.RequireEmails = this.BlogProperties.RequireEmails;
                ctrlCommentEdit.ClearFormAfterSave = true;

                pnlSubscription.Visible = this.BlogProperties.EnableSubscriptions;
                plcBtnSubscribe.Visible = this.BlogProperties.EnableSubscriptions;
                elemSubscription.DocumentID = this.PostNode.DocumentID;
                elemSubscription.NodeID = this.PostNode.NodeID;
                elemSubscription.Culture = this.PostNode.DocumentCulture;
            }

            // Check if trackback should displayed
            if (this.DisplayTrackbacks && this.BlogProperties.EnableTrackbacks)
            {
                pnlTrackbackURL.Visible = true;
                lblURLValue.Text = TextHelper.EnsureMaximumLineLength(URLHelper.GetAbsoluteUrl(BlogHelper.GetBlogPostTrackbackUrl(this.PostNode.NodeGUID, this.PostNode.DocumentName, this.PostNode.DocumentCulture)), this.TrackbackURLSize);
            }
        }

        // Make sure info label is dispalyed to the user when saved succesfully
        if (QueryHelper.GetBoolean("saved", false))
        {
            ctrlCommentEdit.CommentSavedText = GetString("Blog.CommentView.CommentSaved");
        }

        if (this.Visible)
        {
            ReloadComments();
        }
    }


    void ctrlCommentEdit_OnBeforeCommentSaved()
    {
        // Set information text after comment is saved
        if (!isUserAuthorized && this.BlogProperties.ModerateComments)
        {
            ctrlCommentEdit.CommentSavedText = GetString("Blog.CommentView.ModeratedCommentSaved");
        }
    }


    /// <summary>
    /// Reloads comment list after new comment is added.
    /// </summary>    
    void ctrlCommentEdit_OnAfterCommentSaved(BlogCommentInfo commentObj)
    {
        // If comments are moderated there is no need to reload whole page and comment count immediately
        if (!isUserAuthorized && this.BlogProperties.ModerateComments)
        {
            // Reload comment list only
            ReloadComments();
        }
        else
        {
            string url = URLRewriter.CurrentURL;

            if (commentObj.CommentApproved)
            {
                url = URLHelper.RemoveParameterFromUrl(url, "saveda");
                url = URLHelper.AddParameterToUrl(url, "saved", "1");
            }
            else
            {
                url = URLHelper.RemoveParameterFromUrl(url, "saved");
                url = URLHelper.AddParameterToUrl(url, "saveda", "1");
            }

            // Reload whole page
            URLHelper.Redirect(url);
        }
    }


    void mBlogComment_OnCommentAction(string actionName, object actionArgument)
    {
        // Get comment ID
        int commentId = ValidationHelper.GetInteger(actionArgument, 0);
        BlogCommentInfo bci = null;
        CurrentUserInfo currrentUser = CMSContext.CurrentUser;

        switch (actionName.ToLower())
        {
            case "delete":
                // Check 'Manage' permission
                if (!isUserAuthorized)
                {
                    AccessDenied("cms.blog", "Manage");
                }

                // Delete comment
                BlogCommentInfoProvider.DeleteBlogCommentInfo(commentId);

                ReloadData();

                break;

            case "approve":
                // Check 'Manage' permission
                if (!isUserAuthorized)
                {
                    AccessDenied("cms.blog", "Manage");
                }

                // Set comment as 'approved'
                bci = BlogCommentInfoProvider.GetBlogCommentInfo(commentId);
                if ((bci != null) && (currrentUser != null))
                {
                    bci.CommentApprovedByUserID = currrentUser.UserID;
                    bci.CommentApproved = true;
                    BlogCommentInfoProvider.SetBlogCommentInfo(bci);
                }

                ReloadData();
                break;

            case "reject":
                // Check 'Manage' permission
                if (!isUserAuthorized)
                {
                    AccessDenied("cms.blog", "Manage");
                }

                // Set comment as 'rejected'
                bci = BlogCommentInfoProvider.GetBlogCommentInfo(commentId);
                if (bci != null)
                {
                    bci.CommentApprovedByUserID = 0;
                    bci.CommentApproved = false;
                    BlogCommentInfoProvider.SetBlogCommentInfo(bci);
                }

                ReloadData();
                break;
        }
    }


    /// <summary>
    /// Reload.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        // Reload whole page
        if (ReloadPageAfterAction)
        {
            URLHelper.Redirect(URLRewriter.CurrentURL);
        }
        // Reload comment list only
        else
        {
            ReloadComments();
        }
    }


    /// <summary>
    /// Reloads comment list.
    /// </summary>
    public void ReloadComments()
    {
        this.SetContext();

        this.pnlComment.Visible = this.AreCommentsOpened;

        if (this.PostNode != null)
        {
            CurrentUserInfo currentUser = CMSContext.CurrentUser;

            // Check permissions for blog
            if (this.BlogProperties.CheckPermissions)
            {
                if (currentUser.IsAuthorizedPerDocument(this.PostNode, NodePermissionsEnum.Read) != AuthorizationResultEnum.Allowed)
                {
                    this.Visible = false;
                    return;
                }
            }

            ctrlCommentEdit.PostDocumentId = this.PostNode.DocumentID;
            ctrlCommentEdit.PostNodeId = this.PostNode.NodeID;
            ctrlCommentEdit.PostCulture = this.PostNode.DocumentCulture;

            if (!this.BlogProperties.StopProcessing)
            {
                // Get parent blog
                bool selectOnlyPublished = (CMSContext.ViewMode == ViewModeEnum.LiveSite);
                TreeNode blogNode = BlogHelper.GetParentBlog(this.AliasPath, this.SiteName, selectOnlyPublished);

                // Determine whether user is authorized to manage comments
                isUserAuthorized = BlogHelper.IsUserAuthorizedToManageComments(blogNode);

                // Get all post comments
                rptComments.DataSource = BlogCommentInfoProvider.GetPostComments(this.PostNode.DocumentID, !isUserAuthorized, isUserAuthorized, DisplayTrackbacks);
                rptComments.DataBind();
            }
        }

        this.ReleaseContext();
    }


    /// <summary>
    /// Adds comment list item.
    /// </summary>
    void rptComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (lblInfo.Visible)
        {
            lblInfo.Visible = false;
        }

        if (e.Item.DataItem != null)
        {
            // Create new comment info object
            BlogCommentInfo bci = new BlogCommentInfo(((DataRowView)e.Item.DataItem).Row);

            // Load 'BlogCommentDetail.ascx' control
            BlogCommentDetail mBlogComment = (BlogCommentDetail)this.LoadControl(mCommentDetailControlPath);

            // Set control data and properties
            mBlogComment.ID = "blogElem";
            mBlogComment.BlogpPoperties = mBlogProperties;
            mBlogComment.CommentsDataRow = ((DataRowView)e.Item.DataItem).Row;
            mBlogComment.IsLiveSite = this.IsLiveSite;

            // Initialiaze comment 'Action buttons'
            mBlogComment.OnCommentAction += new OnCommentActionEventHandler(mBlogComment_OnCommentAction);
            mBlogComment.ShowRejectButton = (isUserAuthorized && bci.CommentApproved);
            mBlogComment.ShowApproveButton = (isUserAuthorized && !bci.CommentApproved);
            mBlogComment.ShowDeleteButton = (mBlogProperties.ShowDeleteButton && isUserAuthorized);
            mBlogComment.ShowEditButton = (mBlogProperties.ShowEditButton && isUserAuthorized);

            // Abuse report security properties 
            mBlogComment.AbuseReportSecurityAccess = this.AbuseReportSecurityAccess;
            mBlogComment.AbuseReportRoles = this.AbuseReportRoles;
            mBlogComment.AbuseReportOwnerID = this.AbuseReportOwnerID;

            // Add loaded control as comment list item
            e.Item.Controls.Clear();
            e.Item.Controls.Add(mBlogComment);
            if (this.Separator != null)
            {
                e.Item.Controls.Add(new LiteralControl(this.Separator));
            }
        }
    }


    /// <summary>
    /// Initializes control.
    /// </summary>
    private void SetupControl()
    {
        btnLeaveMessage.Attributes.Add("onclick", "ShowSubscription(0, '" + hdnSelSubsTab.ClientID + "','" + pnlComment.ClientID + "','" +
            pnlSubscription.ClientID + "'); return false; ");
        btnSubscribe.Attributes.Add("onclick", " ShowSubscription(1, '" + hdnSelSubsTab.ClientID + "','" + pnlComment.ClientID + "','" +
            pnlSubscription.ClientID + "'); return false; ");

        // Show/hide appropriate control based on current selection form hidden field
        if (ValidationHelper.GetInteger(hdnSelSubsTab.Value, 0) == 0)
        {
            pnlComment.Style.Remove("display");
            pnlComment.Style.Add("display", "block");
            pnlSubscription.Style.Remove("display");
            pnlSubscription.Style.Add("display", "none");
        }
        else
        {
            pnlSubscription.Style.Remove("display");
            pnlSubscription.Style.Add("display", "block");
            pnlComment.Style.Remove("display");
            pnlComment.Style.Add("display", "none");
        }
    }
}
