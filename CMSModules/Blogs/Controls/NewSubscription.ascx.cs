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

using CMS.Blogs;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Blogs_Controls_NewSubscription : CMSUserControl
{
    #region "Private variables"

    private BlogProperties mBlogProperties = null;
    int mDocumentId = 0;

    #endregion


    #region "Public properties"
    
    /// <summary>
    /// Document ID.
    /// </summary>
    public int DocumentID
    {
        get 
        { 
            return mDocumentId; 
        }
        set 
        {
            mDocumentId = value; 
        }
    }


    /// <summary>
    /// Gets or sets docuemnt node ID.
    /// </summary>
    public int NodeID
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets document culture.
    /// </summary>
    public string Culture
    {
        get;
        set;
    }


    /// <summary>
    /// Properties passed from the upper control.
    /// </summary>
    public BlogProperties BlogProperties 
    {
        get 
        {
            return this.mBlogProperties;
        }
        set 
        {
            this.mBlogProperties = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string valGroup = this.UniqueID;

        lblEmail.ResourceString = "blog.subscription.email";
        btnOk.ResourceString = "blog.subscription.subscribe";
        btnOk.ValidationGroup = valGroup;

        rfvEmailRequired.ErrorMessage = GetString("blog.subscription.noemail");
        rfvEmailRequired.ValidationGroup = valGroup;
                
        this.revEmailValid.ValidationGroup = valGroup;
        this.revEmailValid.ErrorMessage = GetString("general.correctemailformat");
        this.revEmailValid.ValidationExpression = ValidationHelper.EmailRegExp.ToString();
    }


    /// <summary>
    /// Pre-fill user e-mail.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsPostBack())
        {
            // Pre-fill user e-mail address to empty textbox for the first time
            if ((txtEmail.Text.Trim() == "") && (CMSContext.CurrentUser != null))
            {
                txtEmail.Text = CMSContext.CurrentUser.Email;
            }
        }
    }


    /// <summary>
    /// OK click handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check banned IP
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        // Check input fields
        string email = txtEmail.Text.Trim();
        string result = new Validator().NotEmpty(email, rfvEmailRequired.ErrorMessage)
            .IsEmail(email, GetString("general.correctemailformat")).Result;

        // Try to subscribe new subscriber
        if (result == "")
        {
            if (this.DocumentID > 0)
            {
                BlogPostSubscriptionInfo bpsi = BlogPostSubscriptionInfoProvider.GetBlogPostSubscriptionInfo(email, this.DocumentID);

                // Check for duplicit subscriptions
                if (bpsi == null)
                {
                    bpsi = new BlogPostSubscriptionInfo();
                    bpsi.SubscriptionPostDocumentID = this.DocumentID;
                    bpsi.SubscriptionEmail = email;

                    // Update user id for logged users (except the public users)
                    if ((CMSContext.CurrentUser != null) && (!CMSContext.CurrentUser.IsPublic()))
                    {
                        bpsi.SubscriptionUserID = CMSContext.CurrentUser.UserID;
                    }

                    BlogPostSubscriptionInfoProvider.SetBlogPostSubscriptionInfo(bpsi);

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("blog.subscription.beensubscribed");

                    // Clear form after successful subscription
                    txtEmail.Text = "";

                    LogActivity(bpsi, this.NodeID, this.Culture);
                }
                else
                {
                    result = GetString("blog.subscription.emailexists");
                }
            }
            else
            {
                result = GetString("general.invalidid");
            }
        }

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion

    /// <summary>
    /// Logs activity.
    /// </summary>
    /// <param name="bpsi">Blog subscription info</param>
    /// <param name="nodeId">Docuemnt node ID</param>
    /// <param name="culture">Document culture</param>
    private void LogActivity(BlogPostSubscriptionInfo bpsi, int nodeId, string culture)
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
                int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
                Dictionary<string, object> contactData = new Dictionary<string, object>();
                contactData.Add("ContactEmail", bpsi.SubscriptionEmail);
                ModuleCommands.OnlineMarketingUpdateContactFromExternalSource(contactData, false, contactId);

                var data = new ActivityData()
                {
                    ContactID = contactId,
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
}
