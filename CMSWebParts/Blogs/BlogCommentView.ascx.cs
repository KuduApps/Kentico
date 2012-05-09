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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.Blogs;
using CMS.PortalEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;
using CMS.SettingsProvider;

public partial class CMSWebParts_Blogs_BlogCommentView : CMSAbstractWebPart
{
    #region "Properties" 

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.commentView.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates whether permissions should be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.commentView.BlogProperties.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            this.commentView.BlogProperties.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Indicates whether 'Edit' button should be displayed in comment view while editing comments on the live site.
    /// </summary>
    public bool ShowEditButton
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowEditButton"), this.commentView.BlogProperties.ShowEditButton);
        }
        set
        {
            this.SetValue("ShowEditButton", value);
            this.commentView.BlogProperties.ShowEditButton = value;
        }
    }


    /// <summary>
    /// Indicates whether 'Delete' button should be displayed in comment view while editing comments on the live site.
    /// </summary>
    public bool ShowDeleteButton
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowDeleteButton"), this.commentView.BlogProperties.ShowDeleteButton);
        }
        set
        {
            this.SetValue("ShowDeleteButton", value);
            this.commentView.BlogProperties.ShowDeleteButton = value;
        }
    }


    /// <summary>
    /// Indicates whether user pictures should be displayed in comment detail.
    /// </summary>
    public bool EnableUserPictures
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableUserPictures"), this.commentView.BlogProperties.EnableUserPictures);
        }
        set
        {
            this.SetValue("EnableUserPictures", value);
            this.commentView.BlogProperties.EnableUserPictures = value;
        }
    }


    /// <summary>
    /// User picture max width.
    /// </summary>
    public int UserPictureMaxWidth
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("UserPictureMaxWidth"), this.commentView.BlogProperties.UserPictureMaxWidth);
        }
        set
        {
            this.SetValue("UserPictureMaxWidth", value);
            this.commentView.BlogProperties.UserPictureMaxWidth = value;
        }
    }


    /// <summary>
    /// User picture max height.
    /// </summary>
    public int UserPictureMaxHeight
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("UserPictureMaxHeight"), this.commentView.BlogProperties.UserPictureMaxHeight);
        }
        set
        {
            this.SetValue("UserPictureMaxHeight", value);
            this.commentView.BlogProperties.UserPictureMaxHeight = value;
        }
    }


    /// <summary>
    /// Blog comment separator.
    /// </summary>
    public string CommentSeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CommentSeparator"), this.commentView.Separator);
        }
        set
        {
            this.SetValue("CommentSeparator", value);
            this.commentView.Separator = value;
        }
    }


    /// <summary>
    /// Gets or sets list of roles (separated by ';') which are allowed to report abuse (in combination with SecurityAccess.AuthorizedRoles).
    /// </summary>
    public string AbuseReportRoles
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AbuseReportRoles"), this.commentView.AbuseReportRoles);
        }
        set
        {
            this.SetValue("AbuseReportRoles", value);
            this.commentView.AbuseReportRoles = value;
        }
    }


    /// <summary>
    /// Gets or sets the security access for report abuse link.
    /// </summary>
    public SecurityAccessEnum AbuseReportAccess
    {
        get
        {
            return (SecurityAccessEnum)ValidationHelper.GetInteger(this.GetValue("AbuseReportAccess"), (int)this.commentView.AbuseReportSecurityAccess);
        }
        set
        {
            this.SetValue("AbuseReportAccess", value);
            this.commentView.AbuseReportSecurityAccess = value;
        }
    }


    /// <summary>
    /// Indicates whether trackback URL and comments should be displayed.
    /// </summary>
    public bool DisplayTrackbacks
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayTrackbacks"), true);
        }
        set
        {
            this.SetValue("DisplayTrackbacks", value);
            this.commentView.DisplayTrackbacks = value;
        }
    }


    /// <summary>
    /// Gets or sets number of characters after which the trackback URL is automatically wprapped, otherwise it is not wrapped which can break the design when url is too long.
    /// </summary>
    public int TrackbackURLSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("TrackbackURLSize"), 0);
        }
        set
        {
            this.SetValue("TrackbackURLSize", value);
            this.commentView.TrackbackURLSize = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.commentView.StopProcessing = true;
            this.commentView.BlogProperties.StopProcessing = true;
        }
        else
        {
            this.commentView.ControlContext = this.ControlContext;

            // Get current page info
            PageInfo currentPage = CMSContext.CurrentPageInfo;

            bool selectOnlyPublished = (this.PageManager.ViewMode == ViewModeEnum.LiveSite);

            // Get current document
            this.commentView.PostNode = CMSContext.CurrentDocument;

            // Get current parent blog
            TreeNode blogNode = BlogHelper.GetParentBlog(currentPage.NodeAliasPath, CMSContext.CurrentSiteName, selectOnlyPublished);
            
            // If blog node exists, set comment view properties
            if (blogNode != null)
            {
                this.commentView.BlogProperties.AllowAnonymousComments = ValidationHelper.GetBoolean(blogNode.GetValue("BlogAllowAnonymousComments"), true);
                this.commentView.BlogProperties.ModerateComments = ValidationHelper.GetBoolean(blogNode.GetValue("BlogModerateComments"), false);
                this.commentView.BlogProperties.OpenCommentsFor = ValidationHelper.GetInteger(blogNode.GetValue("BlogOpenCommentsFor"), BlogProperties.OPEN_COMMENTS_ALWAYS);
                this.commentView.BlogProperties.SendCommentsToEmail = ValidationHelper.GetString(blogNode.GetValue("BlogSendCommentsToEmail"), "");
                this.commentView.BlogProperties.UseCaptcha = ValidationHelper.GetBoolean(blogNode.GetValue("BlogUseCAPTCHAForComments"), true);
                this.commentView.BlogProperties.RequireEmails = ValidationHelper.GetBoolean(blogNode.GetValue("BlogRequireEmails"), false);
                this.commentView.BlogProperties.EnableSubscriptions = ValidationHelper.GetBoolean(blogNode.GetValue("BlogEnableSubscriptions"), false);
                this.commentView.BlogProperties.CheckPermissions = this.CheckPermissions;
                this.commentView.BlogProperties.ShowDeleteButton = this.ShowDeleteButton;
                this.commentView.BlogProperties.ShowEditButton = this.ShowEditButton;
                this.commentView.BlogProperties.EnableUserPictures = this.EnableUserPictures;
                this.commentView.BlogProperties.UserPictureMaxHeight = this.UserPictureMaxHeight;
                this.commentView.BlogProperties.UserPictureMaxWidth = this.UserPictureMaxWidth;
                this.commentView.BlogProperties.EnableTrackbacks = ValidationHelper.GetBoolean(blogNode.GetValue("BlogEnableTrackbacks"), true);
                this.commentView.Separator = this.CommentSeparator;
                this.commentView.ReloadPageAfterAction = true;
                this.commentView.AbuseReportOwnerID = blogNode.NodeOwner;
                this.commentView.AbuseReportRoles = this.AbuseReportRoles;
                this.commentView.AbuseReportSecurityAccess = this.AbuseReportAccess;
                this.commentView.DisplayTrackbacks = this.DisplayTrackbacks;
                this.commentView.TrackbackURLSize = this.TrackbackURLSize;
            }
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        this.SetupControl();
        this.commentView.ReloadComments();
    }
}
