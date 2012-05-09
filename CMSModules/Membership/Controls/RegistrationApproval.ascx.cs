using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.EmailEngine;
using CMS.EventLog;
using CMS.WebAnalytics;
using CMS.UIControls;
using CMS.TreeEngine;

public partial class CMSModules_Membership_Controls_RegistrationApproval : CMSUserControl
{
    #region "Variables"

    private string mFromAddress;
    private string mSuccessfulApprovalText;
    private string mUnsuccessfulApprovalText;
    private string mUserDeletedText;
    private string mAdministratorEmail;
    private string mWaitingForApprovalText;
    private bool mNotifyAdministrator = false;

    #endregion


    #region "Public properties"



    /// <summary>
    /// Gets or sets the value that indicates whether administrator should be informed about new user.
    /// </summary>
    public bool NotifyAdministrator
    {
        get
        {
            return mNotifyAdministrator;
        }
        set
        {
            mNotifyAdministrator = value;
        }
    }



    /// <summary>
    /// Gets or sets the administrator e-mail address.
    /// </summary>
    public string AdministratorEmail
    {
        get
        {
            return mAdministratorEmail;
        }
        set
        {
            mAdministratorEmail = value;
        }
    }


    /// <summary>
    /// Gets or sets waiting for approval text.
    /// </summary>
    public string WaitingForApprovalText
    {
        get
        {
            if (String.IsNullOrEmpty(mWaitingForApprovalText))
            {
                return SuccessfulApprovalText;
            }

            return mWaitingForApprovalText;
        }
        set
        {
            mWaitingForApprovalText = value;
        }
    }


    /// <summary>
    /// Gets or sets email address of sender.
    /// </summary>
    public string FromAddress
    {
        get
        {
            return mFromAddress;
        }
        set
        {
            mFromAddress = value;
        }
    }


    /// <summary>
    /// Gets or sets Successful Approval Text.
    /// </summary>
    public string SuccessfulApprovalText
    {
        get
        {
            return mSuccessfulApprovalText;
        }
        set
        {
            mSuccessfulApprovalText = value;
        }
    }


    /// <summary>
    /// Gets or sets Unsuccesfull Approval text.
    /// </summary>
    public string UnsuccessfulApprovalText
    {
        get
        {
            if (!String.IsNullOrEmpty(mUnsuccessfulApprovalText))
            {
                return mUnsuccessfulApprovalText;
            }
            else
            {
                return GetString("mem.reg.UnsuccessfulApprovalText");
            }
        }
        set
        {
            mUnsuccessfulApprovalText = value;
        }
    }


    /// <summary>
    /// Gets or sets ui deleted text.
    /// </summary>
    public string UserDeletedText
    {
        get
        {
            if (!String.IsNullOrEmpty(mUserDeletedText))
            {
                return mUserDeletedText;
            }
            else
            {
                return GetString("mem.reg.UserDeletedText");
            }
        }
        set
        {
            mUserDeletedText = value;
        }
    }

    #endregion


    /// <summary>
    /// Page Load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // If StopProcessing flag is set, do nothing
        if (StopProcessing)
        {
            Visible = false;
            return;
        }

        Guid userGuid = QueryHelper.GetGuid("userguid", Guid.Empty);

        if (userGuid != Guid.Empty)
        {
            #region "Request validity"

            UserInfo ui = UserInfoProvider.GetUserInfoByGUID(userGuid);

            // ui was not found, probably late activation try
            if (ui == null)
            {
                lblInfo.Text = UserDeletedText;
                return;
            }

            // ui has been already activated
            if ((ui.UserSettings.UserActivationDate > DateTimeHelper.ZERO_TIME) || ui.UserSettings.UserWaitingForApproval || ui.UserEnabled)
            {
                lblInfo.Text = UnsuccessfulApprovalText;
                return;
            }

            #endregion

            string siteName = null;
            bool administrationApproval = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSRegistrationAdministratorApproval");
            lblInfo.Text = SuccessfulApprovalText;

            // Admin approve is not required, enable ui
            if (!administrationApproval)
            {
                lblInfo.Text = (!String.IsNullOrEmpty(SuccessfulApprovalText)) ? SuccessfulApprovalText : GetString("mem.reg.SuccessfulApprovalText");

                // Enable ui
                ui.UserSettings.UserActivationDate = DateTime.Now;
                ui.Enabled = true;

                // ui is confirmed and enabled, could be logged into statistics
                siteName = CMSContext.CurrentSiteName;
                AnalyticsHelper.LogRegisteredUser(siteName, ui);
            }
            // ui must wait for admin approval
            else
            {
                lblInfo.Text = (!String.IsNullOrEmpty(WaitingForApprovalText)) ? WaitingForApprovalText : ResHelper.GetString("mem.reg.SuccessfulApprovalWaitingForAdministratorApproval");

                // Mark for admin approval
                ui.UserSettings.UserWaitingForApproval = true;
            }

            // Save changes
            UserInfoProvider.SetUserInfo(ui);

            #region "Log activity"

            // Log registration activity
            siteName = CMSContext.CurrentSiteName;
            if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName))
            {
                if (ActivitySettingsHelper.UserRegistrationEnabled(siteName))
                {
                    if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                    {
                        // Try to get contact ID from confirmation link (if any)
                        int contactId = QueryHelper.GetInteger("contactid", 0);
                        if (contactId <= 0)
                        {
                            // Contact ID not found => get new ID according to user info
                            contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                        }
                        TreeNode currDoc = CMSContext.CurrentDocument;
                        ActivityLogProvider.LogRegistrationActivity(contactId,
                            ui, URLHelper.CurrentRelativePath, (currDoc != null) ? currDoc.DocumentID : 0, siteName, CMSContext.Campaign, (currDoc != null) ? currDoc.DocumentCulture : null);
                    }
                }
            }

            #endregion


            #region "Administrator notification email"

            // Notify administrator if enabled and email confirmation is not required
            if ((!String.IsNullOrEmpty(AdministratorEmail)) && (administrationApproval || NotifyAdministrator))
            {
                EmailTemplateInfo template = null;

                if (administrationApproval)
                {
                    template = EmailTemplateProvider.GetEmailTemplate("Registration.Approve", CMSContext.CurrentSiteName);
                }
                else
                {
                    template = EmailTemplateProvider.GetEmailTemplate("Registration.New", CMSContext.CurrentSiteName);
                }

                EventLogProvider ev = new EventLogProvider();

                if (template == null)
                {
                    ev.LogEvent("E", DateTime.Now, "RegistrationForm", "GetEmailTemplate", HTTPHelper.GetAbsoluteUri());
                }
                //email template ok
                else
                {
                    // Prepare macro replacements
                    string[,] replacements = new string[4, 2];
                    replacements[0, 0] = "firstname";
                    replacements[0, 1] = ui.FirstName;
                    replacements[1, 0] = "lastname";
                    replacements[1, 1] = ui.LastName;
                    replacements[2, 0] = "email";
                    replacements[2, 1] = ui.Email;
                    replacements[3, 0] = "username";
                    replacements[3, 1] = ui.UserName;

                    // Set resolver
                    ContextResolver resolver = CMSContext.CurrentResolver;
                    resolver.SourceParameters = replacements;
                    resolver.EncodeResolvedValues = true;

                    // Email message
                    EmailMessage email = new EmailMessage();
                    email.EmailFormat = EmailFormatEnum.Default;
                    email.Recipients = AdministratorEmail;

                    // Get e-mail sender and subject from template, if used
                    email.From = EmailHelper.GetSender(template, (!String.IsNullOrEmpty(FromAddress)) ? FromAddress : SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress"));

                    email.Body = resolver.ResolveMacros(template.TemplateText);

                    resolver.EncodeResolvedValues = false;
                    email.PlainTextBody = resolver.ResolveMacros(template.TemplatePlainText);

                    string emailSubject = EmailHelper.GetSubject(template, GetString("RegistrationForm.EmailSubject"));
                    email.Subject = resolver.ResolveMacros(emailSubject);

                    email.CcRecipients = template.TemplateCc;
                    email.BccRecipients = template.TemplateBcc;

                    try
                    {
                        MetaFileInfoProvider.ResolveMetaFileImages(email, template.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                        // Send the e-mail immediately
                        EmailSender.SendEmail(CMSContext.CurrentSiteName, email, true);
                    }
                    catch
                    {
                        ev.LogEvent("E", DateTime.Now, "Membership", "RegistrationApprovalEmail", CMSContext.CurrentSite.SiteID);
                    }
                }
            }

            #endregion
        }
        else
        {
            Visible = false;
        }
    }
}
