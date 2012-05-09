using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;


using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Blogs;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Blogs_Controls_BlogCommentEdit : CMSUserControl
{
    #region "Variables"

    private int mPostDocumentId = 0;
    private int mCommentId = 0;
    private string mOkButtonText = null;
    private string mCommentSavedText = null;
    private bool mClearFormAfterSave = false;
    private bool mUseCaptcha = false;
    private bool mAdvancedMode = false;
    private bool mEnableSubscriptions = false;
    private bool mRequireEmails = false;
    protected string mValidationGroup = null;

    public event OnBeforeCommentSavedEventHandler OnBeforeCommentSaved;
    public event OnAfterCommentSavedEventHandler OnAfterCommentSaved;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Post document ID. Set when creating new comment to the post.
    /// </summary>
    public int PostDocumentId
    {
        get
        {
            return mPostDocumentId;
        }
        set
        {
            mPostDocumentId = value;
        }
    }


    /// <summary>
    /// Gets or sets document node ID.
    /// </summary>
    public int PostNodeId
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets document culture.
    /// </summary>
    public string PostCulture
    {
        get;
        set;
    }


    /// <summary>
    /// Comment ID. Set when editing existing comment.
    /// </summary>
    public int CommentId
    {
        get
        {
            return mCommentId;
        }
        set
        {
            mCommentId = value;
        }
    }


    /// <summary>
    /// Ok button text.
    /// </summary>
    public string OkButtonText
    {
        get
        {
            return mOkButtonText;
        }
        set
        {
            mOkButtonText = value;
        }
    }


    /// <summary>
    /// Comment saved text.
    /// </summary>
    public string CommentSavedText
    {
        get
        {
            return mCommentSavedText;
        }
        set
        {
            mCommentSavedText = value;
        }
    }


    /// <summary>
    /// Indicates whether form fields should be cleared after data are saved.
    /// </summary>
    public bool ClearFormAfterSave
    {
        get
        {
            return mClearFormAfterSave;
        }
        set
        {
            mClearFormAfterSave = value;
        }
    }


    /// <summary>
    /// Indicates whether security code control should be displayed.
    /// </summary>
    public bool UseCaptcha
    {
        get
        {
            return mUseCaptcha;
        }
        set
        {
            mUseCaptcha = value;
        }
    }


    /// <summary>
    /// Indicates whether advanced mode controls should be displayed.
    /// </summary>
    public bool AdvancedMode
    {
        get
        {
            return this.mAdvancedMode;
        }
        set
        {
            this.mAdvancedMode = value;
        }
    }


    /// <summary>
    /// Indicates whether subscription is allowed.
    /// </summary>
    public bool EnableSubscriptions
    {
        get
        {
            return mEnableSubscriptions;
        }
        set
        {
            mEnableSubscriptions = value;
        }
    }


    /// <summary>
    /// Indicates whether e-mail is required.
    /// </summary>
    public bool RequireEmails
    {
        get
        {
            return mRequireEmails;
        }
        set
        {
            mRequireEmails = value;
        }
    }


    /// <summary>
    /// Indicates if buttons should be displayed.
    /// </summary>
    public bool DisplayButtons
    {
        get
        {
            return plcButtons.Visible;
        }
        set
        {
            plcButtons.Visible = value;
        }
    }


    /// <summary>
    /// Validation group of controls.
    /// </summary>
    public string ValidationGroup
    {
        get
        {
            if (mValidationGroup == null)
            {
                mValidationGroup = "CommentEdit_" + Guid.NewGuid().ToString("N");
            }

            return mValidationGroup;
        }
        set
        {
            mValidationGroup = value;
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whether the control is displayed within the insert mode.
    /// </summary>
    private bool IsInsertMode
    {
        get
        {
            // Insert mode
            if (mPostDocumentId > 0)
            {
                return true;
            }
            // Edit mode
            else
            {
                return false;
            }
        }
    }


    /// <summary>
    /// CSS used for the live site mode.
    /// </summary>
    protected string LiveSiteCss
    {
        get
        {
            if (CMSContext.ViewMode != ViewModeEnum.LiveSite)
            {
                return "BlogBreakLine";
            }
            return null;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        mOkButtonText = GetString("general.add");
        mCommentSavedText = GetString("Blog.CommentEdit.CommentSaved");

        // Basic control initialization
        lblName.Text = GetString("Blog.CommentEdit.lblName");
        lblUrl.Text = GetString("Blog.CommentEdit.lblUrl");
        lblComments.Text = GetString("Blog.CommentEdit.lblComments");
        btnOk.Text = mOkButtonText;

        // Validators initialization
        rfvComments.ErrorMessage = GetString("Blog.CommentEdit.CommentEmpty");
        rfvName.ErrorMessage = GetString("Blog.CommentEdit.NameEmpty");
        revEmailValid.ErrorMessage = GetString("general.correctemailformat");
        rfvEmail.ErrorMessage = GetString("blog.commentedit.rfvemail");
        rfvEmail.Enabled = RequireEmails;

        // Show or hide security code control
        plcCaptcha.Visible = mUseCaptcha;

        lblApproved.Text = GetString("Blog.CommentEdit.Approved");
        lblSpam.Text = GetString("Blog.CommentEdit.Spam");
        lblInserted.Text = GetString("Blog.CommentEdit.Inserted");

        revEmailValid.ValidationExpression = ValidationHelper.EmailRegExp.ToString();

        plcChkSubscribe.Visible = EnableSubscriptions;

        revEmailValid.ValidationGroup = ValidationGroup;
        rfvComments.ValidationGroup = ValidationGroup;
        rfvEmail.ValidationGroup = ValidationGroup;
        rfvName.ValidationGroup = ValidationGroup;
        btnOk.ValidationGroup = ValidationGroup;

        if (!RequestHelper.IsPostBack())
        {
            CurrentUserInfo currentUser = CMSContext.CurrentUser;

            // New comment
            if (IsInsertMode)
            {
                // Prefill current user fullname
                if ((currentUser != null) && (!currentUser.IsPublic()))
                {
                    txtName.Text = currentUser.UserNickName != "" ? currentUser.UserNickName : currentUser.FullName;
                    txtEmail.Text = currentUser.Email;
                }
            }
            // Existing comment
            else
            {
                LoadCommentData();
            }

            // Advaced mode in CMSDesk
            if (this.AdvancedMode)
            {
                this.plcAdvancedMode.Visible = true;
            }
        }

        // Ensure information is displayed to the user
        bool savedRequiresApprove = QueryHelper.GetBoolean("saveda", false);
        bool saved = QueryHelper.GetBoolean("saved", false);
        if (saved || savedRequiresApprove)
        {
            if (savedRequiresApprove)
            {
                CommentSavedText = GetString("blog.comments.requiresmoderationafteraction");
            }

            lblInfo.Text = CommentSavedText;
            lblInfo.Visible = true;
        }
    }


    /// <summary>
    /// Fill form with the comment data.
    /// </summary>
    protected void LoadCommentData()
    {
        // Get comment info from database
        BlogCommentInfo bci = BlogCommentInfoProvider.GetBlogCommentInfo(mCommentId);
        if (bci != null)
        {
            txtName.Text = HttpUtility.HtmlDecode(bci.CommentUserName);
            txtUrl.Text = HttpUtility.HtmlDecode(bci.CommentUrl);
            txtComments.Text = HttpUtility.HtmlDecode(bci.CommentText);
            txtEmail.Text = bci.CommentEmail;
            chkApproved.Checked = bci.CommentApproved;
            chkSpam.Checked = bci.CommentIsSpam;

            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && (CMSContext.CurrentUser != null))
            {
                lblInsertedDate.Text = CMSContext.ConvertDateTime(bci.CommentDate, this).ToString();
            }
            else
            {
                lblInsertedDate.Text = bci.CommentDate.ToString();
            }
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        PerformAction();
    }


    public void PerformAction()
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        if (OnBeforeCommentSaved != null)
        {
            OnBeforeCommentSaved();
        }

        BlogCommentInfo bci = null;

        // Validate form
        string errorMessage = ValidateForm();
        if (errorMessage == "")
        {
            // Check flooding when message being inserted through the LiveSite
            if (IsLiveSite && FloodProtectionHelper.CheckFlooding(CMSContext.CurrentSiteName, CMSContext.CurrentUser))
            {
                lblError.Visible = true;
                lblError.Text = GetString("General.FloodProtection");
                return;
            }

            CurrentUserInfo currentUser = CMSContext.CurrentUser;

            // Create new comment
            if (IsInsertMode)
            {
                bci = new BlogCommentInfo();
                bci.CommentDate = DateTime.Now;
                bci.CommentPostDocumentID = mPostDocumentId;
                if (!currentUser.IsPublic())
                {
                    bci.CommentUserID = currentUser.UserID;
                }
                bci.CommentIsTrackback = false;
            }
            // Get existing comment
            else
            {
                bci = BlogCommentInfoProvider.GetBlogCommentInfo(mCommentId);
            }

            // Update basic comment properties
            if (bci != null)
            {
                // Add http:// if needed
                string url = HTMLHelper.HTMLEncode(txtUrl.Text.Trim());
                if (url != "")
                {
                    if ((!url.ToLower().StartsWith("http://")) && (!url.ToLower().StartsWith("https://")))
                    {
                        url = "http://" + url;
                    }
                }

                bci.CommentIsSpam = chkSpam.Checked;
                bci.CommentApproved = chkApproved.Checked;
                bci.CommentUserName = HTMLHelper.HTMLEncode(txtName.Text.Trim());
                bci.CommentUrl = url;
                bci.CommentText = HTMLHelper.HTMLEncode(txtComments.Text.Trim());
                bci.CommentUrl = bci.CommentUrl.ToLower().Replace("javascript", "_javascript");
                bci.CommentEmail = HTMLHelper.HTMLEncode(txtEmail.Text.Trim());
            }

            if (IsInsertMode)
            {
                // Auto approve owner comments
                if (bci != null)
                {
                    TreeNode blogNode = BlogHelper.GetParentBlog(bci.CommentPostDocumentID, false);
                    if ((currentUser != null) && (blogNode != null))
                    {
                        bool isAuthorized = BlogHelper.IsUserAuthorizedToManageComments(blogNode);
                        if (isAuthorized)
                        {
                            bci.CommentApprovedByUserID = blogNode.NodeOwner;
                            bci.CommentApproved = true;
                        }
                        else
                        {
                            // Is blog moderated ?
                            bool moderated = ValidationHelper.GetBoolean(blogNode.GetValue("BlogModerateComments"), false);

                            bci.CommentApprovedByUserID = 0;
                            bci.CommentApproved = !moderated;
                        }
                    }
                }
            }

            // Perform bad words check
            if (!BadWordInfoProvider.CanUseBadWords(CMSContext.CurrentUser, CMSContext.CurrentSiteName))
            {
                if (bci != null)
                {
                    // Prepare columns to check
                    Dictionary<string, int> columns = new Dictionary<string, int>();
                    columns.Add("CommentText", 0);
                    columns.Add("CommentUserName", 200);

                    // Perform bad words to check
                    errorMessage = BadWordsHelper.CheckBadWords(bci, columns, "CommentApproved", "CommentApprovedByUserID", bci.CommentText, CMSContext.CurrentUser.UserID);
                }
            }

            if (errorMessage == string.Empty)
            {
                if (bci != null)
                {
                    if (!ValidateComment(bci))
                    {
                        // Show error message
                        lblError.Visible = true;
                        lblError.Text = GetString("Blog.CommentEdit.EmptyBadWord");
                    }
                    else
                    {
                        // Subscribe new subscriber
                        if (chkSubscribe.Checked)
                        {
                            BlogPostSubscriptionInfo bpsi = BlogPostSubscriptionInfoProvider.GetBlogPostSubscriptionInfo(txtEmail.Text, mPostDocumentId);

                            if (bpsi == null)
                            {
                                bpsi = new BlogPostSubscriptionInfo();
                                bpsi.SubscriptionEmail = txtEmail.Text;
                                bpsi.SubscriptionPostDocumentID = mPostDocumentId;
                                bpsi.SubscriptionUserID = bci.CommentUserID;
                                BlogPostSubscriptionInfoProvider.SetBlogPostSubscriptionInfo(bpsi);
                                LogRegistrationActivity(bpsi, this.PostNodeId, this.PostCulture);
                            }
                            else
                            {
                                errorMessage = GetString("blog.subscription.emailexists");
                            }
                        }

                        if (errorMessage == "")
                        {
                            // Save changes to database
                            BlogCommentInfoProvider.SetBlogCommentInfo(bci);

                            if (!bci.CommentApproved)
                            {
                                CommentSavedText = GetString("blog.comments.requiresmoderationafteraction");
                            }

                            // Inform user
                            lblInfo.Visible = true;
                            lblInfo.Text = CommentSavedText;

                            // Clear form when required
                            if (mClearFormAfterSave)
                            {
                                txtComments.Text = "";
                                txtUrl.Text = "";
                                ctrlCaptcha.Value = "";
                            }

                            LogCommentActivity(bci, this.PostNodeId, this.PostCulture);

                            if (OnAfterCommentSaved != null)
                            {
                                OnAfterCommentSaved(bci);
                            }
                        }
                    }
                }
            }
        }

        if (errorMessage != "")
        {
            // Show error message
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
        else
        {
            // Regenerate captcha code when comment saved successfully
            ctrlCaptcha.GenerateNew();
        }
    }


    private static bool ValidateComment(BlogCommentInfo commentInfo)
    {
        if ((commentInfo.CommentText != null) && (commentInfo.CommentUserName != null))
        {
            return (commentInfo.CommentText.Trim() != "") && (commentInfo.CommentUserName.Trim() != "");
        }
        return false;
    }


    /// <summary>
    /// Logs activity.
    /// </summary>
    /// <param name="bci">Blog comment info</param>
    /// <param name="nodeId">Docuemnt node ID</param>
    /// <param name="culture">Docuemnt culture</param>
    private void LogCommentActivity(BlogCommentInfo bci, int nodeId, string culture)
    {
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (bci == null) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        string siteName = CMSContext.CurrentSiteName;
        if (!ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.BlogPostCommentsEnabled(siteName))
        {
            return;
        }

        if (bci.CommentPostDocumentID > 0)
        {
            // Load blog post settings and check if logging is enabled for current post
            TreeProvider tree = new TreeProvider();
            TreeNode blogPost = DocumentHelper.GetDocument(bci.CommentPostDocumentID, tree);

            if ((blogPost != null) && ValidationHelper.GetBoolean(blogPost.GetValue("BlogLogActivity"), false))
            {
                TreeNode blogNode = BlogHelper.GetParentBlog(bci.CommentPostDocumentID, false);
                string blogName = null;
                if (blogNode != null)
                {
                    blogName = blogNode.DocumentName;
                }

                int contactID = ModuleCommands.OnlineMarketingGetCurrentContactID();
                var data = new ActivityData()
                {
                    ContactID = contactID,
                    SiteID = CMSContext.CurrentSiteID,
                    Type = PredefinedActivityType.BLOG_COMMENT,
                    TitleData = blogName,
                    ItemID = bci.CommentID,
                    URL = URLHelper.CurrentRelativePath,
                    ItemDetailID = (blogNode != null ? blogNode.NodeID : 0),
                    NodeID = nodeId,
                    Culture = culture,
                    Campaign = CMSContext.Campaign
                };
                ActivityLogProvider.LogActivity(data);

                Dictionary<string, object> contactData = new Dictionary<string,object>();
                contactData.Add("ContactEmail", bci.CommentEmail);
                contactData.Add("ContactLastName", bci.CommentUserName);
                contactData.Add("ContactWebSite", bci.CommentUrl);
                ModuleCommands.OnlineMarketingUpdateContactFromExternalSource(contactData, false, contactID);
            }
        }
    }


    /// <summary>
    /// Logs registration activity.
    /// </summary>
    /// <param name="bpsi">Blog subscription info</param>
    /// <param name="nodeId">Docuemnt node ID</param>
    /// <param name="culture">Document culture</param>
    private void LogRegistrationActivity(BlogPostSubscriptionInfo bpsi, int nodeId, string culture)
    {
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (bpsi == null) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        string siteName = CMSContext.CurrentSiteName;
        if (!ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) ||
            !ActivitySettingsHelper.BlogPostSubscriptionEnabled(siteName))
        {
            return;
        }

        if (bpsi.SubscriptionPostDocumentID > 0)
        {
            TreeProvider tree = new TreeProvider();
            TreeNode blogPost = DocumentHelper.GetDocument(bpsi.SubscriptionPostDocumentID, tree);

            if ((blogPost != null) && ValidationHelper.GetBoolean(blogPost.GetValue("BlogLogActivity"), false))
            {
                string blogName = null;
                TreeNode blogNode = BlogHelper.GetParentBlog(bpsi.SubscriptionPostDocumentID, false);
                if (blogNode != null)
                {
                    blogName = blogNode.DocumentName;
                }

                // Update contact info according to subscribtion
                var data = new ActivityData()
                {
                    ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
                    SiteID = CMSContext.CurrentSiteID,
                    Type = PredefinedActivityType.SUBSCRIPTION_BLOG_POST,
                    ItemDetailID = (blogNode != null ? blogNode.NodeID : 0),
                    TitleData = bpsi.SubscriptionEmail,
                    URL = URLHelper.CurrentRelativePath,
                    NodeID = nodeId,
                    Value = TextHelper.LimitLength(blogName, 250),
                    Culture = culture,
                    Campaign = CMSContext.Campaign
                };
                ActivityLogProvider.LogActivity(data);
            }
        }
    }



    /// <summary>
    /// Validates form.
    /// </summary>
    protected string ValidateForm()
    {
        string errorMessage = new Validator().NotEmpty(txtComments.Text.Trim(), rfvComments.ErrorMessage).NotEmpty(txtName.Text.Trim(), rfvName.ErrorMessage).Result;

        if ((mUseCaptcha) && (errorMessage == ""))
        {
            // Check whether security code is correct
            if (!ctrlCaptcha.IsValid())
            {
                errorMessage = ctrlCaptcha.ValidationError;
            }
        }

        // Check if e-mail address is required
        if ((errorMessage == "") && (RequireEmails || chkSubscribe.Checked))
        {
            errorMessage = new Validator().NotEmpty(txtEmail.Text, GetString("blog.subscription.noemail")).Result;
        }

        // Check e-mail address format if some e-mail adrress is specified
        if ((errorMessage == "") && !String.IsNullOrEmpty(txtEmail.Text))
        {
            errorMessage = new Validator().IsEmail(txtEmail.Text, GetString("general.correctemailformat")).Result;
        }

        return errorMessage;
    }
}
