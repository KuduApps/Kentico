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
using CMS.PortalEngine;
using CMS.PortalControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Blogs_NewBlog : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Path in the content tree where new blog should be created.
    /// </summary>
    public string BlogParentPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BlogParentPath"), "");
        }
        set
        {
            this.SetValue("BlogParentPath", value);
        }
    }


    /// <summary>
    /// Indicates if user should be redirected to the blog after the blog is created.
    /// </summary>
    public bool RedirectToNewBlog
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RedirectToNewBlog"), false);
        }
        set
        {
            this.SetValue("RedirectToNewBlog", value);
        }
    }


    /// <summary>
    /// Blog side columnt text.
    /// </summary>
    public string BlogSideColumnText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BlogSideColumnText"), "");
        }
        set
        {
            this.SetValue("BlogSideColumnText", value);
        }
    }


    /// <summary>
    /// Blog teaser.
    /// </summary>
    public Guid BlogTeaser
    {
        get
        {
            return ValidationHelper.GetGuid(this.GetValue("BlogTeaser"), Guid.Empty);
        }
        set
        {
            this.SetValue("BlogTeaser", value);
        }
    }


    /// <summary>
    /// Email address where new comments should be sent.
    /// </summary>
    public string BlogSendCommentsToEmail
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BlogSendCommentsToEmail"), "");
        }
        set
        {
            this.SetValue("BlogSendCommentsToEmail", value);
        }
    }


    /// <summary>
    /// Indicates if blog comments are opened (0 - not opened, -1 - always opened, X - number of days the comments are opened after the post is published).
    /// </summary>
    public int BlogOpenCommentsFor
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("BlogOpenCommentsFor"), -1);
        }
        set
        {
            this.SetValue("BlogOpenCommentsFor", value);
        }
    }


    /// <summary>
    /// Indicates if new comments requir to be moderated before publishing.
    /// </summary>
    public bool BlogModerateComments
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BlogModerateComments"), false);
        }
        set
        {
            this.SetValue("BlogModerateComments", value);
        }
    }


    /// <summary>
    /// Indicates if security control should be used when inserting new comment.
    /// </summary>
    public bool BlogUseCAPTCHAForComments
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BlogUseCAPTCHAForComments"), false);
        }
        set
        {
            this.SetValue("BlogUseCAPTCHAForComments", value);
        }
    }


    /// <summary>
    /// Indicates anonymous users can insert comments.
    /// </summary>
    public bool BlogAllowAnonymousComments
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BlogAllowAnonymousComments"), false);
        }
        set
        {
            this.SetValue("BlogAllowAnonymousComments", value);
        }
    }


    /// <summary>
    /// User which are allowed to moderate blog comments. Format [username1];[username2];...
    /// </summary>
    public string BlogModerators
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BlogModerators"), "");
        }
        set
        {
            this.SetValue("BlogModerators", value);
        }
    }


    /// <summary>
    /// Page template which is applied to a new blog. If not specified, page template of the parent document is applied.
    /// </summary>
    public string NewBlogTemplate
    {
        get
        {
            object value = this.GetValue("NewBlogTemplate");
            if (value == null)
            {
                return null;
            }
            else
            {
                return Convert.ToString(value);
            }
        }
        set
        {
            this.SetValue("NewBlogTemplate", value);
        }
    }


    /// <summary>
    /// Indicates if permissions are to be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CheckPermissions"), false);
        }
        set
        {
            SetValue("CheckPermissions", value);
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
            newBlog.StopProcessing = true;
        }
        else
        {
            // Set new blog properties
            newBlog.BlogParentPath = this.BlogParentPath;
            newBlog.RedirectToNewBlog = this.RedirectToNewBlog;
            newBlog.BlogAllowAnonymousComments = this.BlogAllowAnonymousComments;
            newBlog.BlogModerateComments = this.BlogModerateComments;
            newBlog.BlogOpenCommentsFor = this.BlogOpenCommentsFor;
            newBlog.BlogSendCommentsToEmail = this.BlogSendCommentsToEmail;
            newBlog.BlogSideColumnText = this.BlogSideColumnText;
            newBlog.BlogTeaser = this.BlogTeaser;
            newBlog.BlogUseCAPTCHAForComments = this.BlogUseCAPTCHAForComments;
            newBlog.BlogModerators = this.BlogModerators;
            newBlog.NewBlogTemplate = this.NewBlogTemplate;
            newBlog.CheckPermissions = CheckPermissions;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }
}
