using System;
using System.Web;

using CMS.CMSHelper;
using CMS.EmailEngine;
using CMS.EventLog;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.WebAnalytics;

public partial class CMSWebParts_Membership_Registration_CustomRegistrationForm : CMSAbstractWebPart
{
    #region "Layout properties"

    /// <summary>
    /// Full alternative form name ('classname.formname') for usersettingsinfo.
    /// Default value is cms.user.RegistrationForm
    /// </summary>
    public string AlternativeForm
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AlternativeForm"), "cms.user.RegistrationForm");
        }
        set
        {
            this.SetValue("AlternativeForm", value);
            formUser.AlternativeFormFullName = value;
        }
    }

    #endregion


    #region "Text properties"

    /// <summary>
    /// Gets or sets submit button text.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ButtonText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.Button$}"));
        }

        set
        {
            this.SetValue("ButtonText", value);
            btnRegister.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets registration approval page URL.
    /// </summary>
    public string ApprovalPage
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ApprovalPage"), "");
        }
        set
        {
            this.SetValue("ApprovalPage", value);
        }
    }

    #endregion


    #region "Registration properties"


    /// <summary>
    /// Gets or sets the value that indicates whether email to user should be sent.
    /// </summary>
    public bool SendWelcomeEmail
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SendWelcomeEmail"), true);
        }
        set
        {
            this.SetValue("SendWelcomeEmail", value);
        }
    }


    /// <summary>
    /// Determines whether the captcha image should be displayed.
    /// </summary>
    public bool DisplayCaptcha
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayCaptcha"), false);
        }

        set
        {
            this.SetValue("DisplayCaptcha", value);
        }
    }


    /// <summary>
    /// Gets or sets the message which is displayed after registration failed.
    /// </summary>
    public string RegistrationErrorMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RegistrationErrorMessage"), "");
        }
        set
        {
            this.SetValue("RegistrationErrorMessage", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether user is enabled after registration.
    /// </summary>
    public bool EnableUserAfterRegistration
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableUserAfterRegistration"), true);
        }
        set
        {
            this.SetValue("EnableUserAfterRegistration", value);
        }
    }


    /// <summary>
    /// Gets or sets the sender email (from).
    /// </summary>
    public string FromAddress
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("FromAddress"), SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress"));
        }
        set
        {
            this.SetValue("FromAddress", value);
        }
    }


    /// <summary>
    /// Gets or sets the recipient email (to).
    /// </summary>
    public string ToAddress
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ToAddress"), SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSAdminEmailAddress"));
        }
        set
        {
            this.SetValue("ToAddress", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether after successful registration is 
    /// notification email sent to the administrator 
    /// </summary>
    public bool NotifyAdministrator
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("NotifyAdministrator"), false);
        }
        set
        {
            this.SetValue("NotifyAdministrator", value);
        }
    }


    /// <summary>
    /// Gets or sets the roles where is user assigned after successful registration.
    /// </summary>
    public string AssignRoles
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AssignToRoles"), "");
        }
        set
        {
            this.SetValue("AssignToRoles", value);
        }
    }


    /// <summary>
    /// Gets or sets the sites where is user assigned after successful registration.
    /// </summary>
    public string AssignToSites
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AssignToSites"), "");
        }
        set
        {
            this.SetValue("AssignToSites", value);
        }
    }


    /// <summary>
    /// Gets or sets the message which is displayed after successful registration.
    /// </summary>
    public string DisplayMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DisplayMessage"), "");
        }
        set
        {
            this.SetValue("DisplayMessage", value);
        }
    }


    /// <summary>
    /// Gets or set the url where is user redirected after successful registration.
    /// </summary>
    public string RedirectToURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RedirectToURL"), "");
        }
        set
        {
            this.SetValue("RedirectToURL", value);
        }
    }


    /// <summary>
    /// Gets or sets the default starting alias path for newly registered user.
    /// </summary>
    public string StartingAliasPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("StartingAliasPath"), "");
        }
        set
        {
            this.SetValue("StartingAliasPath", value);
        }
    }

    #endregion


    #region "Conversion properties"

    /// <summary>
    /// Gets or sets the conversion track name used after successful registration.
    /// </summary>
    public string TrackConversionName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TrackConversionName"), "");
        }
        set
        {
            if ((value != null) && (value.Length > 400))
            {
                value = value.Substring(0, 400);
            }
            this.SetValue("TrackConversionName", value);
        }
    }


    /// <summary>
    /// Gets or sets the conversion value used after successful registration.
    /// </summary>
    public double ConversionValue
    {
        get
        {           
            return ValidationHelper.GetDouble(GetValue("ConversionValue"), 0);
        }
        set
        {
            this.SetValue("ConversionValue", value);
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
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Set default visibility
            pnlRegForm.Visible = true;
            lblInfo.Visible = false;

            // WAI validation
            lblCaptcha.AssociatedControlClientID = captchaElem.InputClientID;

            // Get alternative form info
            AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(AlternativeForm);
            if (afi != null)
            {
                formUser.AlternativeFormFullName = this.AlternativeForm;
                formUser.Info = new UserInfo();
                formUser.Info.ClearData();
                formUser.ClearAfterSave = false;
                formUser.Visible = true;
                formUser.ValidationErrorMessage = this.RegistrationErrorMessage;
                formUser.IsLiveSite = true;
                // Reload form if not in PortalEngine environment and if post back
                if ((this.StandAlone) && (RequestHelper.IsPostBack()))
                {
                    formUser.ReloadData();
                }

                captchaElem.Visible = this.DisplayCaptcha;
                lblCaptcha.Visible = this.DisplayCaptcha;
                plcCaptcha.Visible = this.DisplayCaptcha;

                btnRegister.Text = this.ButtonText;
                btnRegister.Click += btnRegister_Click;

                formUser.OnBeforeSave += formUser_OnBeforeSave;

                lblInfo.CssClass = "EditingFormInfoLabel";
                lblError.CssClass = "EditingFormErrorLabel";

                if (formUser.BasicForm != null)
                {
                    // Set the live site context
                    formUser.BasicForm.ControlContext.ContextName = CMS.SiteProvider.ControlContext.LIVE_SITE;
                }
            }
            else
            {
                lblError.Text = String.Format(GetString("altform.formdoesntexists"), AlternativeForm);
                lblError.Visible = true;
                pnlRegForm.Visible = false;
            }
        }
    }


    /// <summary>
    /// Page pre-render event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        // Hide default form submit button
        if ((formUser != null) && (formUser.BasicForm != null))
        {
            formUser.BasicForm.SubmitButton.Visible = false;
        }
    }


    /// <summary>
    /// OnBeforeSave is checked if user has specified username or e-mail.
    /// </summary>
    void formUser_OnBeforeSave(object sender, EventArgs e)
    {
        // Check if user name is filled.
        string userName = ValidationHelper.GetString(formUser.BasicForm.Data.GetValue("UserName"), "");
        if (string.IsNullOrEmpty(userName))
        {
            userName = ValidationHelper.GetString(formUser.BasicForm.Data.GetValue("Email"), "");
        }

        // If user name and e-mail aren't filled stop processing and display error.
        if (string.IsNullOrEmpty(userName))
        {
            formUser.StopProcessing = true;
            lblError.Visible = true;
            lblError.Text = GetString("customregistrationform.usernameandemail");
        }
        else
        {
            formUser.BasicForm.Data.SetValue("UserName", userName);
        }
    }


    /// <summary>
    /// OK click handler (Proceed registration).
    /// </summary>
    void btnRegister_Click(object sender, EventArgs e)
    {
        if ((this.PageManager.ViewMode == ViewModeEnum.Design) || (this.HideOnCurrentPage) || (!this.IsVisible))
        {
            // Do not process
        }
        else
        {
            // Ban IP addresses which are blocked for registration
            if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.Registration))
            {
                lblError.Visible = true;
                lblError.Text = GetString("banip.ipisbannedregistration");
                return;
            }

            // Check if captcha is required
            if (this.DisplayCaptcha)
            {
                // Verify captcha text
                if (!captchaElem.IsValid())
                {
                    // Display error message if catcha text is not valid
                    lblError.Visible = true;
                    lblError.Text = GetString("Webparts_Membership_RegistrationForm.captchaError");
                    return;
                }
                else
                {
                    // Generate new code and clear captcha textbox if cpatcha code is valid
                    captchaElem.GenerateNew();
                }
            }

            string userName = String.Empty;
            string nickName = String.Empty;
            string firstName = String.Empty;
            string lastName = String.Empty;
            string emailValue = String.Empty;

            // Check duplicit user
            // 1. Find appropriate control and get its value (i.e. user name)
            // 2. Try to find user info
            EditingFormControl txtUserName = formUser.BasicForm.FieldEditingControls["UserName"] as EditingFormControl;
            if (txtUserName != null)
            {
                userName = ValidationHelper.GetString(txtUserName.Value, String.Empty);
            }

            EditingFormControl txtNickName = formUser.BasicForm.FieldEditingControls["UserNickName"] as EditingFormControl;
            if (txtNickName != null)
            {
                nickName = ValidationHelper.GetString(txtNickName.Value, String.Empty);
            }

            EditingFormControl txtEmail = formUser.BasicForm.FieldEditingControls["Email"] as EditingFormControl;
            if (txtEmail != null)
            {
                emailValue = ValidationHelper.GetString(txtEmail.Value, String.Empty);
            }

            EditingFormControl txtFirstName = formUser.BasicForm.FieldEditingControls["FirstName"] as EditingFormControl;
            if (txtFirstName != null)
            {
                firstName = ValidationHelper.GetString(txtFirstName.Value, String.Empty);
            }

            EditingFormControl txtLastName = formUser.BasicForm.FieldEditingControls["LastName"] as EditingFormControl;
            if (txtLastName != null)
            {
                lastName = ValidationHelper.GetString(txtLastName.Value, String.Empty);
            }

            // Test if "global" or "site" user exists. 
            SiteInfo si = CMSContext.CurrentSite;
            UserInfo siteui = UserInfoProvider.GetUserInfo(UserInfoProvider.EnsureSitePrefixUserName(userName,si));
            if ((UserInfoProvider.GetUserInfo(userName) != null) || (siteui != null))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserAlreadyExists").Replace("%%name%%", HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, true)));
                return;
            }

            // Check for reserved user names like administrator, sysadmin, ...
            if (UserInfoProvider.NameIsReserved(CMSContext.CurrentSiteName, userName))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserNameReserved").Replace("%%name%%", HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, true)));
                return;
            }

            if (UserInfoProvider.NameIsReserved(CMSContext.CurrentSiteName, nickName))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserNameReserved").Replace("%%name%%", HTMLHelper.HTMLEncode(nickName));
                return;
            }

            // Check limitations for site members
            if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.SiteMembers, VersionActionEnum.Insert, false))
            {
                lblError.Visible = true;
                lblError.Text = GetString("License.MaxItemsReachedSiteMember");
                return;
            }

            // Check whether email is unique if it is required
            string checkSites = (String.IsNullOrEmpty(this.AssignToSites)) ? CMSContext.CurrentSiteName : this.AssignToSites;
            if (!UserInfoProvider.IsEmailUnique(emailValue, checkSites, 0))
            {
                lblError.Visible = true;
                lblError.Text = GetString("UserInfo.EmailAlreadyExist");
                return;
            }

            // Validate and save form with new user data
            if (!formUser.Save())
            {
                // Return if saving failed
                return;
            }

            // Get user info from form
            UserInfo ui = (UserInfo)formUser.Info;

            // Add user prefix if settings is on
            // Ensure site prefixes
            if (UserInfoProvider.UserNameSitePrefixEnabled(CMSContext.CurrentSiteName))
            {
                ui.UserName = UserInfoProvider.EnsureSitePrefixUserName(userName, si);
            }

            ui.PreferredCultureCode = "";
            ui.Enabled = this.EnableUserAfterRegistration;
            ui.IsEditor = false;
            ui.IsGlobalAdministrator = false;
            ui.UserURLReferrer = CMSContext.CurrentUser.URLReferrer;
            ui.UserCampaign = CMSContext.Campaign;

            // Fill optionally full user name
            if (String.IsNullOrEmpty(ui.FullName))
            {
                string fullName = "";
                if (ui.FirstName.Trim() != "")
                {
                    fullName += ui.FirstName;
                }

                if (ui.MiddleName.Trim() != "")
                {
                    fullName += " " + ui.MiddleName;
                }

                if (ui.LastName.Trim() != "")
                {
                    fullName += " " + ui.LastName;
                }
                ui.FullName = fullName;
            }

            // Ensure nick name
            if (ui.UserNickName.Trim() == "")
            {
                ui.UserNickName = Functions.GetFormattedUserName(ui.UserName, true);
            }

            ui.UserSettings.UserRegistrationInfo.IPAddress = HTTPHelper.UserHostAddress;
            ui.UserSettings.UserRegistrationInfo.Agent = HttpContext.Current.Request.UserAgent;
            ui.UserSettings.UserLogActivities = true;
            ui.UserSettings.UserShowSplashScreen = true;

            // Check whether confirmation is required
            bool requiresConfirmation = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSRegistrationEmailConfirmation");
            bool requiresAdminApprove = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSRegistrationAdministratorApproval");
            if (!requiresConfirmation)
            {
                // If confirmation is not required check whether administration approval is reqiures
                if (requiresAdminApprove)
                {
                    ui.Enabled = false;
                    ui.UserSettings.UserWaitingForApproval = true;
                }
            }
            else
            {
                // EnableUserAfterRegistration is overrided by requiresConfirmation - user needs to be confirmed before enable
                ui.Enabled = false;
            }

            // Set user's starting alias path
            if (!String.IsNullOrEmpty(this.StartingAliasPath))
            {
                ui.UserStartingAliasPath = CMSContext.ResolveCurrentPath(this.StartingAliasPath);
            }

            // Get user password and save it in apropriate format after form save
            string password = ValidationHelper.GetString(ui.GetValue("UserPassword"), String.Empty);
            UserInfoProvider.SetPassword(ui, password);

            #region "Welcome Emails (confirmation, waiting for approval)"

            bool error = false;
            EventLogProvider ev = new EventLogProvider();
            EmailTemplateInfo template = null;

            // Prepare macro replacements
            string[,] replacements = new string[6, 2];
            replacements[0, 0] = "confirmaddress";
            replacements[0, 1] = (this.ApprovalPage != String.Empty) ? URLHelper.GetAbsoluteUrl(this.ApprovalPage) + "?userguid=" + ui.UserGUID : URLHelper.GetAbsoluteUrl("~/CMSPages/Dialogs/UserRegistration.aspx") + "?userguid=" + ui.UserGUID;
            replacements[1, 0] = "username";
            replacements[1, 1] = userName;
            replacements[2, 0] = "password";
            replacements[2, 1] = password;
            replacements[3, 0] = "Email";
            replacements[3, 1] = emailValue;
            replacements[4, 0] = "FirstName";
            replacements[4, 1] = firstName;
            replacements[5, 0] = "LastName";
            replacements[5, 1] = lastName;

            // Set resolver
            ContextResolver resolver = CMSContext.CurrentResolver;
            resolver.SourceParameters = replacements;

            // Email message
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.EmailFormat = EmailFormatEnum.Default;
            emailMessage.Recipients = ui.Email;

            // Send welcome message with username and password, with confirmation link, user must confirm registration
            if (requiresConfirmation)
            {
                template = EmailTemplateProvider.GetEmailTemplate("RegistrationConfirmation", CMSContext.CurrentSiteName);
                emailMessage.Subject = GetString("RegistrationForm.RegistrationConfirmationEmailSubject");
            }
            // Send welcome message with username and password, with information that user must be approved by administrator
            else if (this.SendWelcomeEmail)
            {
                if (requiresAdminApprove)
                {
                    template = EmailTemplateProvider.GetEmailTemplate("Membership.RegistrationWaitingForApproval", CMSContext.CurrentSiteName);
                    emailMessage.Subject = GetString("RegistrationForm.RegistrationWaitingForApprovalSubject");
                }
                // Send welcome message with username and password, user can logon directly
                else
                {
                    template = EmailTemplateProvider.GetEmailTemplate("Membership.Registration", CMSContext.CurrentSiteName);
                    emailMessage.Subject = GetString("RegistrationForm.RegistrationSubject");
                }
            }

            if (template != null)
            {
                emailMessage.From = EmailHelper.GetSender(template, SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress"));
                // Enable macro encoding for body
                resolver.EncodeResolvedValues = true;
                emailMessage.Body = resolver.ResolveMacros(template.TemplateText);
                // Disable macro encoding for plaintext body and subject
                resolver.EncodeResolvedValues = false;
                emailMessage.PlainTextBody = resolver.ResolveMacros(template.TemplatePlainText);
                emailMessage.Subject = resolver.ResolveMacros(EmailHelper.GetSubject(template, emailMessage.Subject));

                emailMessage.CcRecipients = template.TemplateCc;
                emailMessage.BccRecipients = template.TemplateBcc;

                try
                {
                    MetaFileInfoProvider.ResolveMetaFileImages(emailMessage, template.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                    // Send the e-mail immediately
                    EmailSender.SendEmail(CMSContext.CurrentSiteName, emailMessage, true);
                }
                catch (Exception ex)
                {
                    ev.LogEvent("E", "RegistrationForm - SendEmail", ex);
                    error = true;
                }
            }

            // If there was some error, user must be deleted
            if (error)
            {
                lblError.Visible = true;
                lblError.Text = GetString("RegistrationForm.UserWasNotCreated");

                // Email was not send, user can't be approved - delete it
                UserInfoProvider.DeleteUser(ui);
                return;
            }

            #endregion

            #region "Administrator notification email"

            // Notify administrator if enabled and email confirmation is not required
            if (!requiresConfirmation && this.NotifyAdministrator && (this.FromAddress != String.Empty) && (this.ToAddress != String.Empty))
            {
                EmailTemplateInfo mEmailTemplate = null;

                if (requiresAdminApprove)
                {
                    mEmailTemplate = EmailTemplateProvider.GetEmailTemplate("Registration.Approve", CMSContext.CurrentSiteName);
                }
                else
                {
                    mEmailTemplate = EmailTemplateProvider.GetEmailTemplate("Registration.New", CMSContext.CurrentSiteName);
                }

                if (mEmailTemplate == null)
                {
                    ev.LogEvent("E", DateTime.Now, "RegistrationForm", "GetEmailTemplate", HTTPHelper.GetAbsoluteUri());
                }
                //email template ok
                else
                {
                    replacements = new string[4, 2];
                    replacements[0, 0] = "firstname";
                    replacements[0, 1] = ui.FirstName;
                    replacements[1, 0] = "lastname";
                    replacements[1, 1] = ui.LastName;
                    replacements[2, 0] = "email";
                    replacements[2, 1] = ui.Email;
                    replacements[3, 0] = "username";
                    replacements[3, 1] = userName;

                    // Set resolver
                    resolver = CMSContext.CurrentResolver;
                    resolver.SourceParameters = replacements;
                    // Enable macro encoding for body
                    resolver.EncodeResolvedValues = true;

                    EmailMessage message = new EmailMessage();
                    message.EmailFormat = EmailFormatEnum.Default;
                    message.From = EmailHelper.GetSender(mEmailTemplate, this.FromAddress);
                    message.Recipients = this.ToAddress;
                    message.Body = resolver.ResolveMacros(mEmailTemplate.TemplateText);
                    // Disable macro encoding for plaintext body and subject
                    resolver.EncodeResolvedValues = false;
                    message.Subject = resolver.ResolveMacros(EmailHelper.GetSubject(mEmailTemplate, GetString("RegistrationForm.EmailSubject")));
                    message.PlainTextBody = resolver.ResolveMacros(mEmailTemplate.TemplatePlainText);

                    message.CcRecipients = mEmailTemplate.TemplateCc;
                    message.BccRecipients = mEmailTemplate.TemplateBcc;

                    try
                    {
                        // Attach template meta-files to e-mail
                        MetaFileInfoProvider.ResolveMetaFileImages(message, mEmailTemplate.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                        EmailSender.SendEmail(CMSContext.CurrentSiteName, message);
                    }
                    catch
                    {
                        ev.LogEvent("E", DateTime.Now, "Membership", "RegistrationEmail", CMSContext.CurrentSite.SiteID);
                    }
                }
            }

            #endregion

            #region "Web analytics"

            // Track successful registration conversion
            if (this.TrackConversionName != String.Empty)
            {
                string siteName = CMSContext.CurrentSiteName;

                if (AnalyticsHelper.AnalyticsEnabled(siteName) && AnalyticsHelper.TrackConversionsEnabled(siteName) && !AnalyticsHelper.IsIPExcluded(siteName, HTTPHelper.UserHostAddress))
                {
                    HitLogProvider.LogConversions(siteName, CMSContext.PreferredCultureCode, this.TrackConversionName, 0, ConversionValue);
                }
            }

            // Log registered user if confirmation is not required
            if (!requiresConfirmation)
            {
                AnalyticsHelper.LogRegisteredUser(CMSContext.CurrentSiteName, ui);
            }

            #endregion

            #region "On-line marketing - activity"

            // Log registered user if confirmation is not required
            if (!requiresConfirmation)
            {
                string siteName = CMSContext.CurrentSiteName;
                if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName))
                {
                    int contactId = 0;
                    // Log registration activity
                    if (ActivitySettingsHelper.UserRegistrationEnabled(siteName))
                    {
                        if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                        {
                            contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                            ActivityLogProvider.LogRegistrationActivity(contactId,
                                ui, URLHelper.CurrentRelativePath, CMSContext.CurrentDocument.DocumentID, siteName, CMSContext.Campaign, CMSContext.CurrentDocument.DocumentCulture);
                        }
                    }

                    // Log login activity
                    if (ui.Enabled && ActivitySettingsHelper.UserLoginEnabled(siteName))
                    {
                        if (contactId <= 0)
                        {
                            contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                        }
                        ActivityLogHelper.UpdateContactLastLogon(contactId);    // Update last logon time
                        if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                        {
                            ActivityLogProvider.LogLoginActivity(contactId,
                                ui, URLHelper.CurrentRelativePath, CMSContext.CurrentDocument.DocumentID, siteName, CMSContext.Campaign, CMSContext.CurrentDocument.DocumentCulture);
                        }
                    }
                }
            }

            #endregion

            #region "Site and roles addition and authentication"

            string[] roleList = this.AssignRoles.Split(';');
            string[] siteList;

            // If AssignToSites field set
            if (!String.IsNullOrEmpty(this.AssignToSites))
            {
                siteList = this.AssignToSites.Split(';');
            }
            else // If not set user current site 
            {
                siteList = new string[] { CMSContext.CurrentSiteName };
            }

            foreach (string siteName in siteList)
            {
                // Add new user to the current site
                UserInfoProvider.AddUserToSite(ui.UserName, siteName);
                foreach (string roleName in roleList)
                {
                    if (!String.IsNullOrEmpty(roleName))
                    {
                        String sn = roleName.StartsWith(".") ? "" : siteName;

                        // Add user to desired roles
                        if (RoleInfoProvider.RoleExists(roleName, sn))
                        {
                            UserInfoProvider.AddUserToRole(ui.UserName, roleName, sn);
                        }
                    }
                }
            }

            if (this.DisplayMessage.Trim() != String.Empty)
            {
                pnlRegForm.Visible = false;
                lblInfo.Visible = true;
                lblInfo.Text = this.DisplayMessage;
            }
            else
            {
                if (ui.Enabled)
                {
                    CMSContext.AuthenticateUser(ui.UserName, true);
                }

                string returnUrl = QueryHelper.GetString("ReturnURL", "");
                if (!String.IsNullOrEmpty(returnUrl) && (returnUrl.StartsWith("~") || returnUrl.StartsWith("/") || QueryHelper.ValidateHash("hash")))
                {
                    URLHelper.Redirect(HttpUtility.UrlDecode(returnUrl));
                }
                else if (this.RedirectToURL != String.Empty)
                {
                    URLHelper.Redirect(this.RedirectToURL);
                }
            }

            #endregion

            lblError.Visible = false;
        }
    }
}