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
using CMS.SiteProvider;
using CMS.EmailEngine;
using CMS.EventLog;
using CMS.DataEngine;
using CMS.WebAnalytics;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.SettingsProvider;

public partial class CMSWebParts_Membership_Registration_RegistrationForm : CMSAbstractWebPart
{
    #region "Text properties"

    /// <summary>
    /// Gets or sets the Skin ID.
    /// </summary>
    public override string SkinID
    {
        get
        {
            return base.SkinID;
        }
        set
        {
            base.SkinID = value;
            SetSkinID(value);
        }
    }


    /// <summary>
    /// Gets or sets the first name text.
    /// </summary>
    public string FirstNameText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("FirstNameText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.FirstName$}"));
        }
        set
        {
            this.SetValue("FirstNameText", value);
            lblFirstName.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the last name text.
    /// </summary>
    public string LastNameText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("LastNameText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.LastName$}"));
        }
        set
        {
            this.SetValue("LastNameText", value);
            lblLastName.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the e-mail text.
    /// </summary>
    public string EmailText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("EmailText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.Email$}"));
        }
        set
        {
            this.SetValue("EmailText", value);
            lblEmail.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the password text.
    /// </summary>
    public string PasswordText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("PasswordText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.Password$}"));
        }
        set
        {
            this.SetValue("PasswordText", value);
            lblPassword.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the confirmation password text.
    /// </summary>
    public string ConfirmPasswordText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ConfirmPasswordText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.ConfirmPassword$}"));
        }
        set
        {
            this.SetValue("ConfirmPasswordText", value);
            lblConfirmPassword.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the button text.
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
            btnOk.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the captcha label text.
    /// </summary>
    public string CaptchaText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("CaptchaText"), ResHelper.LocalizeString("{$Webparts_Membership_RegistrationForm.Captcha$}"));
        }
        set
        {
            this.SetValue("CaptchaText", value);
            lblCaptcha.Text = value;
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
    /// Gets or sets value that indicates whether the captcha image should be displayed.
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
            plcCaptcha.Visible = value;
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


    /// <summary>
    /// Gets or sets the password minimal length.
    /// </summary>
    public int PasswordMinLength
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PasswordMinLength"), 0);
        }
        set
        {
            this.SetValue("PasswordMinLength", 0);
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
            if (value.Length > 400)
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


    #region "Methods"

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
            // Do not process
            rfvFirstName.Enabled = false;
            rfvEmail.Enabled = false;
            rfvConfirmPassword.Enabled = false;
            rfvLastName.Enabled = false;
        }
        else
        {
            // Set default visibility
            pnlForm.Visible = true;
            lblText.Visible = false;

            // Set texts
            lblFirstName.Text = this.FirstNameText;
            lblLastName.Text = this.LastNameText;
            lblEmail.Text = this.EmailText;
            lblPassword.Text = this.PasswordText;
            lblConfirmPassword.Text = this.ConfirmPasswordText;
            btnOk.Text = this.ButtonText;
            lblCaptcha.Text = this.CaptchaText;

            // Set required field validators texts
            rfvFirstName.ErrorMessage = GetString("Webparts_Membership_RegistrationForm.rfvFirstName");
            rfvLastName.ErrorMessage = GetString("Webparts_Membership_RegistrationForm.rfvLastName");
            rfvEmail.ErrorMessage = GetString("Webparts_Membership_RegistrationForm.rfvEmail");
            rfvConfirmPassword.ErrorMessage = GetString("Webparts_Membership_RegistrationForm.rfvConfirmPassword");

            // Add unique validation form
            rfvFirstName.ValidationGroup = ClientID + "_registration";
            rfvLastName.ValidationGroup = ClientID + "_registration";
            rfvEmail.ValidationGroup = ClientID + "_registration";
            passStrength.ValidationGroup = ClientID + "_registration";
            rfvConfirmPassword.ValidationGroup = ClientID + "_registration";
            btnOk.ValidationGroup = ClientID + "_registration";


            // Set SkinID
            if (!this.StandAlone && (this.PageCycle < PageCycleEnum.Initialized))
            {
                SetSkinID(this.SkinID);
            }

            plcCaptcha.Visible = this.DisplayCaptcha;

            // WAI validation
            lblPassword.AssociatedControlClientID = passStrength.InputClientID;
        }
    }


    /// <summary>
    /// Sets SkinID.
    /// </summary>
    void SetSkinID(string skinId)
    {
        if (skinId != "")
        {
            lblFirstName.SkinID = skinId;
            lblLastName.SkinID = skinId;
            lblEmail.SkinID = skinId;
            lblPassword.SkinID = skinId;
            lblConfirmPassword.SkinID = skinId;
            txtFirstName.SkinID = skinId;
            txtLastName.SkinID = skinId;
            txtEmail.SkinID = skinId;
            passStrength.SkinID = skinId;
            txtConfirmPassword.SkinID = skinId;
            btnOk.SkinID = skinId;
        }
    }


    /// <summary>
    /// OK click handler (Proceed registration).
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if ((this.PageManager.ViewMode == ViewModeEnum.Design) || (this.HideOnCurrentPage) || (!this.IsVisible))
        {
            // Do not process
        }
        else
        {
            String siteName = CMSContext.CurrentSiteName;

            #region "Banned IPs"

            // Ban IP addresses which are blocked for registration
            if (!BannedIPInfoProvider.IsAllowed(siteName, BanControlEnum.Registration))
            {
                lblError.Visible = true;
                lblError.Text = GetString("banip.ipisbannedregistration");
                return;
            }

            #endregion


            #region "Check Email & password"

            // Check whether user with same email does not exist 
            UserInfo ui = UserInfoProvider.GetUserInfo(txtEmail.Text);
            SiteInfo si = CMSContext.CurrentSite;
            UserInfo siteui = UserInfoProvider.GetUserInfo(UserInfoProvider.EnsureSitePrefixUserName(txtEmail.Text,si));

            if ((ui != null) || (siteui != null))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserAlreadyExists").Replace("%%name%%", HTMLHelper.HTMLEncode(txtEmail.Text));
                return;
            }

            // Check whether password is same
            if (passStrength.Text != txtConfirmPassword.Text)
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.PassworDoNotMatch");
                return;
            }

            if ((this.PasswordMinLength > 0) && (passStrength.Text.Length < this.PasswordMinLength))
            {
                lblError.Visible = true;
                lblError.Text = String.Format(GetString("Webparts_Membership_RegistrationForm.PasswordMinLength"), this.PasswordMinLength.ToString());
                return;
            }

            if (!passStrength.IsValid())
            {
                lblError.Visible = true;
                lblError.Text = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                return;
            }

            if (!ValidationHelper.IsEmail(txtEmail.Text.ToLower()))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.EmailIsNotValid");
                return;
            }

            #endregion


            #region "Captcha"

            // Check if captcha is required
            if (this.DisplayCaptcha)
            {
                // Verifiy captcha text
                if (!scCaptcha.IsValid())
                {
                    // Display error message if catcha text is not valid
                    lblError.Visible = true;
                    lblError.Text = GetString("Webparts_Membership_RegistrationForm.captchaError");
                    return;
                }
                else
                {
                    // Generate new captcha
                    scCaptcha.GenerateNew();
                }
            }

            #endregion


            #region "User properties"

            ui = new UserInfo();
            ui.PreferredCultureCode = "";
            ui.Email = txtEmail.Text.Trim();
            ui.FirstName = txtFirstName.Text.Trim();
            ui.FullName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
            ui.LastName = txtLastName.Text.Trim();
            ui.MiddleName = "";
            
            // User name as put by user (no site prefix included)
            String plainUserName = txtEmail.Text.Trim();
            ui.UserName = plainUserName;
            
            // Ensure site prefixes
            if (UserInfoProvider.UserNameSitePrefixEnabled(siteName))
            {
                ui.UserName = UserInfoProvider.EnsureSitePrefixUserName(txtEmail.Text.Trim(), si);
            }

            ui.Enabled = this.EnableUserAfterRegistration;
            ui.IsEditor = false;
            ui.IsGlobalAdministrator = false;
            ui.UserURLReferrer = CMSContext.CurrentUser.URLReferrer;
            ui.UserCampaign = CMSContext.Campaign;

            ui.UserSettings.UserRegistrationInfo.IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            ui.UserSettings.UserRegistrationInfo.Agent = HttpContext.Current.Request.UserAgent;

            // Check whether confirmation is required
            bool requiresConfirmation = SettingsKeyProvider.GetBoolValue(siteName + ".CMSRegistrationEmailConfirmation");
            bool requiresAdminApprove = false;

            if (!requiresConfirmation)
            {
                // If confirmation is not required check whether administration approval is reqiures
                if ((requiresAdminApprove = SettingsKeyProvider.GetBoolValue(siteName + ".CMSRegistrationAdministratorApproval")))
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

            #endregion


            #region "Reserved names"

            // Check for reserved user names like administrator, sysadmin, ...
            if (UserInfoProvider.NameIsReserved(siteName, plainUserName))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserNameReserved").Replace("%%name%%", HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(ui.UserName, true)));
                return;
            }

            if (UserInfoProvider.NameIsReserved(siteName, plainUserName))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserNameReserved").Replace("%%name%%", HTMLHelper.HTMLEncode(ui.UserNickName));
                return;
            }

            #endregion


            #region "License limitations"

            // Check limitations for Global administrator
            if (ui.IsGlobalAdministrator)
            {
                if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.GlobalAdmininistrators, VersionActionEnum.Insert, false))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("License.MaxItemsReachedGlobal");
                    return;
                }
            }

            // Check limitations for editors
            if (ui.IsEditor)
            {
                if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Editors, VersionActionEnum.Insert, false))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("License.MaxItemsReachedEditor");
                    return;
                }
            }

            // Check limitations for site members
            if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.SiteMembers, VersionActionEnum.Insert, false))
            {
                lblError.Visible = true;
                lblError.Text = GetString("License.MaxItemsReachedSiteMember");
                return;
            }

            #endregion


            // Check whether email is unique if it is required
            string checkSites = (String.IsNullOrEmpty(this.AssignToSites)) ? siteName : this.AssignToSites;
            if (!UserInfoProvider.IsEmailUnique(txtEmail.Text.Trim(), checkSites, 0))
            {
                lblError.Visible = true;
                lblError.Text = GetString("UserInfo.EmailAlreadyExist");
                return;
            }

            // Set password            
            UserInfoProvider.SetPassword(ui, passStrength.Text);

            #region "Welcome Emails (confirmation, waiting for approval)"

            bool error = false;
            EventLogProvider ev = new EventLogProvider();
            EmailTemplateInfo template = null;

            string emailSubject = null;
            // Send welcome message with username and password, with confirmation link, user must confirm registration
            if (requiresConfirmation)
            {
                template = EmailTemplateProvider.GetEmailTemplate("RegistrationConfirmation", siteName);
                emailSubject = EmailHelper.GetSubject(template, GetString("RegistrationForm.RegistrationConfirmationEmailSubject"));
            }
            // Send welcome message with username and password, with information that user must be approved by administrator
            else if (this.SendWelcomeEmail)
            {
                if (requiresAdminApprove)
                {
                    template = EmailTemplateProvider.GetEmailTemplate("Membership.RegistrationWaitingForApproval", siteName);
                    emailSubject = EmailHelper.GetSubject(template, GetString("RegistrationForm.RegistrationWaitingForApprovalSubject"));
                }
                // Send welcome message with username and password, user can logon directly
                else
                {
                    template = EmailTemplateProvider.GetEmailTemplate("Membership.Registration", siteName);
                    emailSubject = EmailHelper.GetSubject(template, GetString("RegistrationForm.RegistrationSubject"));
                }
            }

            if (template != null)
            {
                // Rretrieve contact ID for confirmation e-mail
                int contactId = 0;
                if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName))
                {
                    // Check if loggin registration activity is enabled
                    if (ActivitySettingsHelper.UserRegistrationEnabled(siteName))
                    {
                        if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                        {
                            contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                        }
                    }
                }

                // Prepare macro replacements
                string[,] replacements = new string[6, 2];
                replacements[0, 0] = "confirmaddress";
                replacements[0, 1] = (this.ApprovalPage != String.Empty) ? URLHelper.GetAbsoluteUrl(this.ApprovalPage) : URLHelper.GetAbsoluteUrl("~/CMSPages/Dialogs/UserRegistration.aspx");
                replacements[0, 1] += "?userguid=" + ui.UserGUID + (contactId > 0?"&contactid=" + contactId.ToString():String.Empty);
                replacements[1, 0] = "username";
                replacements[1, 1] = plainUserName;
                replacements[2, 0] = "password";
                replacements[2, 1] = passStrength.Text;
                replacements[3, 0] = "Email";
                replacements[3, 1] = txtEmail.Text;
                replacements[4, 0] = "FirstName";
                replacements[4, 1] = txtFirstName.Text;
                replacements[5, 0] = "LastName";
                replacements[5, 1] = txtLastName.Text;

                // Set resolver
                ContextResolver resolver = CMSContext.CurrentResolver;
                resolver.SourceParameters = replacements;
                resolver.EncodeResolvedValues = true;

                // Email message
                EmailMessage email = new EmailMessage();
                email.EmailFormat = EmailFormatEnum.Default;
                email.Recipients = ui.Email;

                email.From = EmailHelper.GetSender(template, SettingsKeyProvider.GetStringValue(siteName + ".CMSNoreplyEmailAddress"));
                email.Body = resolver.ResolveMacros(template.TemplateText);

                resolver.EncodeResolvedValues = false;
                email.PlainTextBody = resolver.ResolveMacros(template.TemplatePlainText);
                email.Subject = resolver.ResolveMacros(emailSubject);

                email.CcRecipients = template.TemplateCc;
                email.BccRecipients = template.TemplateBcc;

                try
                {
                    MetaFileInfoProvider.ResolveMetaFileImages(email, template.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                    // Send the e-mail immediately
                    EmailSender.SendEmail(siteName, email, true);
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

            // Notify administrator if enabled and e-mail confirmation is not required
            if (!requiresConfirmation && this.NotifyAdministrator && (this.FromAddress != String.Empty) && (this.ToAddress != String.Empty))
            {
                EmailTemplateInfo mEmailTemplate = null;

                if (requiresAdminApprove)
                {
                    mEmailTemplate = EmailTemplateProvider.GetEmailTemplate("Registration.Approve", siteName);
                }
                else
                {
                    mEmailTemplate = EmailTemplateProvider.GetEmailTemplate("Registration.New", siteName);
                }

                if (mEmailTemplate == null)
                {
                    // Log missing e-mail template
                    ev.LogEvent("E", DateTime.Now, "RegistrationForm", "GetEmailTemplate", HTTPHelper.GetAbsoluteUri());
                }
                else
                {
                    string[,] replacements = new string[4, 2];
                    replacements[0, 0] = "firstname";
                    replacements[0, 1] = ui.FirstName;
                    replacements[1, 0] = "lastname";
                    replacements[1, 1] = ui.LastName;
                    replacements[2, 0] = "email";
                    replacements[2, 1] = ui.Email;
                    replacements[3, 0] = "username";
                    replacements[3, 1] = plainUserName;

                    ContextResolver resolver = CMSContext.CurrentResolver;
                    resolver.SourceParameters = replacements;
                    resolver.EncodeResolvedValues = true;

                    EmailMessage message = new EmailMessage();

                    message.EmailFormat = EmailFormatEnum.Default;
                    message.From = EmailHelper.GetSender(mEmailTemplate, this.FromAddress);
                    message.Recipients = this.ToAddress;
                    message.Body = resolver.ResolveMacros(mEmailTemplate.TemplateText);

                    resolver.EncodeResolvedValues = false;
                    message.PlainTextBody = resolver.ResolveMacros(mEmailTemplate.TemplatePlainText);
                    message.Subject = resolver.ResolveMacros(EmailHelper.GetSubject(mEmailTemplate, GetString("RegistrationForm.EmailSubject")));

                    message.CcRecipients = mEmailTemplate.TemplateCc;
                    message.BccRecipients = mEmailTemplate.TemplateBcc;

                    try
                    {
                        // Attach template meta-files to e-mail
                        MetaFileInfoProvider.ResolveMetaFileImages(message, mEmailTemplate.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                        EmailSender.SendEmail(siteName, message);
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
                if (AnalyticsHelper.AnalyticsEnabled(siteName) && AnalyticsHelper.TrackConversionsEnabled(siteName) && !AnalyticsHelper.IsIPExcluded(siteName, HTTPHelper.UserHostAddress))
                {
                    // Log conversion
                    HitLogProvider.LogConversions(siteName, CMSContext.PreferredCultureCode, this.TrackConversionName, 0, ConversionValue);
                }
            }

            // Log registered user if confirmation is not required
            if (!requiresConfirmation)
            {
                AnalyticsHelper.LogRegisteredUser(siteName, ui);
            }

            #endregion

            #region "On-line marketing - activity"

            // Log registered user if confirmation is not required
            if (!requiresConfirmation)
            {
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
                    if (ui.Enabled && ActivitySettingsHelper.UserLoginEnabled(siteName) )
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

            #region "Roles & authentication"

            string[] roleList = this.AssignRoles.Split(';');
            string[] siteList;

            // If AssignToSites field set
            if (!String.IsNullOrEmpty(this.AssignToSites))
            {
                siteList = this.AssignToSites.Split(';');
            }
            else // If not set user current site 
            {
                siteList = new string[] { siteName };
            }

            foreach (string sn in siteList)
            {
                // Add new user to the current site
                UserInfoProvider.AddUserToSite(ui.UserName, sn);
                foreach (string roleName in roleList)
                {
                    if (!String.IsNullOrEmpty(roleName))
                    {
                        String s = roleName.StartsWith(".") ? "" : siteName;

                        // Add user to desired roles
                        if (RoleInfoProvider.RoleExists(roleName, s))
                        {
                            UserInfoProvider.AddUserToRole(ui.UserName, roleName, s);
                        }
                    }
                }
            }

            if (this.DisplayMessage.Trim() != String.Empty)
            {
                pnlForm.Visible = false;
                lblText.Visible = true;
                lblText.Text = this.DisplayMessage;
            }
            else
            {
                if (ui.Enabled)
                {
                    CMSContext.AuthenticateUser(ui.UserName, true);
                }

                if (this.RedirectToURL != String.Empty)
                {
                    URLHelper.Redirect(this.RedirectToURL);
                }

                else if (QueryHelper.GetString("ReturnURL", "") != String.Empty)
                {
                    string url = QueryHelper.GetString("ReturnURL", "");

                    // Do url decode 
                    url = Server.UrlDecode(url);

                    // Check that url is relative path or hash is ok
                    if (url.StartsWith("~") || url.StartsWith("/") || QueryHelper.ValidateHash("hash"))
                    {
                        URLHelper.Redirect(url);
                    }
                    // Absolute path with wrong hash
                    else
                    {
                        URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("general.badhashtitle") + "&text=" + ResHelper.GetString("general.badhashtext")));
                    }
                }
            }

            #endregion

            lblError.Visible = false;
        }
    }

    #endregion
}
