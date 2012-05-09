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

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Blogs_Controls_NewBlog : CMSUserControl
{
    #region "Variables"

    string mBlogParentPath = "";
    string mBlogSideColumnText = "";
    Guid mBlogTeaser = Guid.Empty;
    int mBlogOpenCommentsFor = -1; // blog is opened "Always" by default
    string mBlogSendCommentsToEmail = "";
    bool mBlogAllowAnonymousComments = true;
    bool mBlogUseCAPTCHAForComments = false;
    bool mBlogModerateComments = false;
    string mBlogModerators = "";
    bool mRedirectToNewBlog = false;
    string mNewBlogTemplate = null;
    private bool mCheckPermissions = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Path in the content tree where new blog should be created.
    /// </summary>
    public string BlogParentPath
    {
        get
        {
            return mBlogParentPath;
        }
        set
        {
            mBlogParentPath = value;
        }
    }


    /// <summary>
    /// Indicates if user should be redirected to the blog after the blog it is created.
    /// </summary>
    public bool RedirectToNewBlog
    {
        get
        {
            return mRedirectToNewBlog;
        }
        set
        {
            mRedirectToNewBlog = value;
        }
    }


    /// <summary>
    /// Blog side columnt text.
    /// </summary>
    public string BlogSideColumnText
    {
        get
        {
            return mBlogSideColumnText;
        }
        set
        {
            mBlogSideColumnText = value;
        }
    }


    /// <summary>
    /// Blog teaser.
    /// </summary>
    public Guid BlogTeaser
    {
        get
        {
            return mBlogTeaser;
        }
        set
        {
            mBlogTeaser = value;
        }
    }


    /// <summary>
    /// Email address where new comments should be sent.
    /// </summary>
    public string BlogSendCommentsToEmail
    {
        get
        {
            return mBlogSendCommentsToEmail;
        }
        set
        {
            mBlogSendCommentsToEmail = value;
        }
    }


    /// <summary>
    /// Indicates if blog comments are opened (0 - not opened, -1 - always opened, X - number of days the comments are opened after the post is published).
    /// </summary>
    public int BlogOpenCommentsFor
    {
        get
        {
            return mBlogOpenCommentsFor;
        }
        set
        {
            mBlogOpenCommentsFor = value;
        }
    }


    /// <summary>
    /// Indicates if new comments requir to be moderated before publishing.
    /// </summary>
    public bool BlogModerateComments
    {
        get
        {
            return mBlogModerateComments;
        }
        set
        {
            mBlogModerateComments = value;
        }
    }


    /// <summary>
    /// Indicates if security control should be used when inserting new comment.
    /// </summary>
    public bool BlogUseCAPTCHAForComments
    {
        get
        {
            return mBlogUseCAPTCHAForComments;
        }
        set
        {
            mBlogUseCAPTCHAForComments = value;
        }
    }


    /// <summary>
    /// Indicates anonymous users can insert comments.
    /// </summary>
    public bool BlogAllowAnonymousComments
    {
        get
        {
            return mBlogAllowAnonymousComments;
        }
        set
        {
            mBlogAllowAnonymousComments = value;
        }
    }


    /// <summary>
    /// Users which are allowed to moderate blog comments. Format [username1];[username2];...
    /// </summary>
    public string BlogModerators
    {
        get
        {
            return mBlogModerators;
        }
        set
        {
            mBlogModerators = value;
        }
    }


    /// <summary>
    /// Page template which is applied to a new blog. If not specified, page template of the parent document is applied.
    /// </summary>
    public string NewBlogTemplate
    {
        get
        {
            return mNewBlogTemplate;
        }
        set
        {
            mNewBlogTemplate = value;
        }
    }


    /// <summary>
    /// Indicates whether permissions are to be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return mCheckPermissions;
        }
        set
        {
            mCheckPermissions = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
        }
        else
        {
            // Initialize controls
            lblName.Text = GetString("Blogs.NewBlog.Name");
            lblDescription.Text = GetString("Blogs.NewBlog.Description");
            btnOk.Text = GetString("General.OK");
            rfvName.ErrorMessage = GetString("Blogs.NewBlog.NameEmpty");
            btnOk.Click += new EventHandler(btnOk_Click);
        }
    }


    void btnOk_Click(object sender, EventArgs e)
    {
        // Validate all required data for new blog
        string errorMessage = ValidateData();

        if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Blogs, VersionActionEnum.Insert))
        {
            errorMessage = GetString("cmsdesk.bloglicenselimits");
        }

        // Get current user
        CurrentUserInfo user = CMSContext.CurrentUser;

        if (errorMessage == "")
        {
            // Get parent node for new blog
            TreeProvider tree = new TreeProvider(user);
            TreeNode parent = tree.SelectSingleNode(CMSContext.CurrentSiteName, this.BlogParentPath.TrimEnd('%'), TreeProvider.ALL_CULTURES);
            if (parent != null)
            {
                if (!CheckPermissions || user.IsAuthorizedToCreateNewDocument(parent, "cms.blog"))
                {
                    bool useParentNodeGroupID = tree.UseParentNodeGroupID;
                    TreeNode blogNode = null;
                    try
                    {
                        // Reflect group document
                        tree.UseParentNodeGroupID = true;

                        // Initialize and create new blog node
                        blogNode = TreeNode.New("cms.blog", tree);
                        blogNode.SetValue("BlogName", txtName.Text.Trim());
                        blogNode.SetValue("BlogDescription", txtDescription.Text.Trim());
                        blogNode.SetValue("BlogAllowAnonymousComments", this.BlogAllowAnonymousComments);
                        blogNode.SetValue("BlogModerateComments", this.BlogModerateComments);
                        blogNode.SetValue("BlogOpenCommentsFor", this.BlogOpenCommentsFor);
                        blogNode.SetValue("BlogSendCommentsToEmail", this.BlogSendCommentsToEmail);
                        blogNode.SetValue("BlogSideColumnText", this.BlogSideColumnText);
                        blogNode.SetValue("BlogUseCAPTCHAForComments", this.BlogUseCAPTCHAForComments);
                        blogNode.SetValue("BlogModerators", this.BlogModerators);
                        if (this.BlogTeaser == Guid.Empty)
                        {
                            blogNode.SetValue("BlogTeaser", null);
                        }
                        else
                        {
                            blogNode.SetValue("BlogTeaser", this.BlogTeaser);
                        }

                        blogNode.SetValue("NodeOwner", user.UserID);

                        if (this.NewBlogTemplate != null)
                        {
                            // Get the BlogMonth class to set the default page template
                            if (this.NewBlogTemplate == "")
                            {
                                DataClassInfo blogClass = DataClassInfoProvider.GetDataClass("CMS.Blog");
                                if (blogClass != null)
                                {
                                    blogNode.DocumentPageTemplateID = blogClass.ClassDefaultPageTemplateID;
                                }
                            }
                            else
                            {
                                // Set the selected template
                                PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(this.NewBlogTemplate);
                                if (pti != null)
                                {
                                    blogNode.DocumentPageTemplateID = pti.PageTemplateId;
                                }
                            }
                        }

                        // Trackbacks are denied by default
                        blogNode.SetValue("BlogEnableTrackbacks", false);

                        blogNode.DocumentName = txtName.Text.Trim();
                        blogNode.DocumentCulture = CMSContext.CurrentUser.PreferredCultureCode;
                        DocumentHelper.InsertDocument(blogNode, parent, tree);
                    }
                    finally
                    {
                        tree.UseParentNodeGroupID = useParentNodeGroupID;
                    }

                    if (this.RedirectToNewBlog)
                    {
                        // Redirect to the new blog
                        URLHelper.Redirect(CMSContext.GetUrl(blogNode.NodeAliasPath));
                    }
                    else
                    {
                        // Display info message
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
                else
                {
                    // Not authorized to create blog
                    errorMessage = GetString("accessdenied.notallowedtocreatedocument");
                }
            }
            else
            {
                // Parent node was not found
                errorMessage = GetString("Blogs.NewBlog.PathNotFound");
            }
        }

        if (errorMessage != "")
        {
            // Display error message
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Validates form data and returns error message if some error occurs.
    /// </summary>
    private string ValidateData()
    {
        if (txtName.Text.Trim() == "")
        {
            // Blog name is empty
            return rfvName.ErrorMessage;
        }
        if (this.BlogParentPath.TrimEnd('%') == "")
        {
            // Path where blog should be created is empty
            return GetString("Blogs.NewBlog.PathEmpty");
        }
        if (CMSContext.CurrentUser.IsPublic())
        {
            // Anonymous user is not allowed to create blog
            return GetString("Blogs.NewBlog.AnonymousUser");
        }
        return "";
    }
}
