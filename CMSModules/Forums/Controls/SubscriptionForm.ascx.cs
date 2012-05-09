using System;
using System.Web;

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.PortalEngine;
using System.Collections.Generic;

public partial class CMSModules_Forums_Controls_SubscriptionForm : ForumViewer
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region "Resources"

        // Set string values
        rfvEmail.ErrorMessage = GetString("Forums_WebInterface_ForumNewPost.emailErrorMsg");

        // Regular expression to validate email (e-mail is not required)
        rfvEmail.ValidationExpression = @"^([\w0-9_-]+(\.[\w0-9_-]+)*@[\w0-9_-]+(\.[\w0-9_-]+)+)*$";
        rfvEmailRequired.ErrorMessage = GetString("Forums_WebInterface_ForumNewPost.emailRequireErrorMsg");

        btnOk.Text = GetString("General.Ok");
        btnCancel.Text = GetString("General.Cancel");

        #endregion

        // Pre-fill user email
        if (!RequestHelper.IsPostBack())
        {
            txtEmail.Text = CMSContext.CurrentUser.Email;
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
        string result = new Validator().NotEmpty(txtEmail.Text, rfvEmailRequired.ErrorMessage).IsRegularExp(txtEmail.Text, @"^([\w0-9_-]+(\.[\w0-9_-]+)*@[\w0-9_-]+(\.[\w0-9_-]+)+)*$", rfvEmail.ErrorMessage).Result;

        if (result == "")
        {
            // For selected forum and only if subscribtion is enabled
            if ((ForumContext.CurrentForum != null) && ((ForumContext.CurrentState == ForumStateEnum.SubscribeToPost) || (ForumContext.CurrentState == ForumStateEnum.NewSubscription)))
            {
                // Check permissions
                if (!this.IsAvailable(ForumContext.CurrentForum, ForumActionType.SubscribeToForum))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("ForumNewPost.PermissionDenied");
                    return;
                }
                
                // Create new subscribtion
                ForumSubscriptionInfo fsi = new ForumSubscriptionInfo();
                fsi.SubscriptionForumID = ForumContext.CurrentForum.ForumID;
                fsi.SubscriptionEmail = HTMLHelper.HTMLEncode(txtEmail.Text.Trim());
                fsi.SubscriptionGUID = Guid.NewGuid();

                if (ForumContext.CurrentSubscribeThread != null)
                {
                    fsi.SubscriptionPostID = ForumContext.CurrentSubscribeThread.PostId;
                }

                if (CMSContext.CurrentUser != null)
                {
                    fsi.SubscriptionUserID = CMSContext.CurrentUser.UserID;
                }

                // Check whethger user is not subscribed
                if (ForumSubscriptionInfoProvider.IsSubscribed(txtEmail.Text.Trim(), fsi.SubscriptionForumID, fsi.SubscriptionPostID))
                {
                    lblError.Text = GetString("ForumSubscibe.SubscriptionExists");
                    lblError.Visible = true;
                    return;
                }
                
                ForumSubscriptionInfoProvider.SetForumSubscriptionInfo(fsi);

                LogSubscriptionActivity(fsi, ForumContext.CurrentForum);
            }
        }

        URLHelper.Redirect(ClearURL());
    }


    /// <summary>
    /// Cancel click nahdler.
    /// </summary>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(ClearURL());
    }


    /// <summary>
    /// Logs activity.
    /// </summary>
    /// <param name="bci">Forum subscription</param>
    private void LogSubscriptionActivity(ForumSubscriptionInfo fsi, ForumInfo fi)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (fsi == null) || (fi == null) || !fi.ForumLogActivity || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
        || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) || !ActivitySettingsHelper.ForumPostSubscriptionEnabled(siteName) )
        {
            return;
        }

        int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
        Dictionary<string, object> contactData = new Dictionary<string, object>();
        contactData.Add("ContactEmail", fsi.SubscriptionEmail);
        ModuleCommands.OnlineMarketingUpdateContactFromExternalSource(contactData, false, contactId);
        var data = new ActivityData()
        {
            ContactID = contactId,
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.SUBSCRIPTION_FORUM_POST,
            TitleData = fi.ForumName,
            ItemID = fi.ForumID,
            ItemDetailID = fsi.SubscriptionID,
            URL = URLHelper.CurrentRelativePath,
            NodeID = CMSContext.CurrentDocument.NodeID,
            Culture = CMSContext.CurrentDocument.DocumentCulture,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }
}
