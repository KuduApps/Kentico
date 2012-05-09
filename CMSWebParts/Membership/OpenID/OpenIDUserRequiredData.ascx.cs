using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.URLRewritingEngine;
using CMS.EventLog;
using CMS.EmailEngine;
using CMS.WebAnalytics;
using CMS.MembershipProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Membership_OpenID_OpenIDUserRequiredData : CMSAbstractWebPart
{
    #region "Constants"

    protected const string SESSION_NAME_USERDATA = "OpenIDAuthenticatedUserData";
    protected const string SESSION_NAME_URL = "OpenIDProviderURL";

    #endregion


    #region "Variables"

    private string mDefaultTargetUrl = null;
    private string userProviderUrl = null;
    private CMSOpenIDHelper openIDhelper = null;

    #endregion


    #region "Public properties"

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
    /// Gets or sets the value which enables abitity of new user to set password.
    /// </summary>
    public bool AllowFormsAuthentication
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AllowFormsAuthentication"), false);
        }
        set
        {
            SetValue("AllowFormsAuthentication", value);
            plcPasswordNew.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which enables abitity join OpenID with existing account.
    /// </summary>
    public bool AllowExistingUser
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AllowExistingUser"), true);
        }
        set
        {
            SetValue("AllowExistingUser", value);
            plcPasswordNew.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the default target url (rediredction when the user is logged in).
    /// </summary>
    public string DefaultTargetUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DefaultTargetUrl"), mDefaultTargetUrl);
        }
        set
        {
            SetValue("DefaultTargetUrl", value);
            mDefaultTargetUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines the behaviour if no OpenID user stored in SESSION.
    /// </summary>
    public bool HideForNoOpenID
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideForNoOpenID"), true);
        }
        set
        {
            SetValue("HideForNoOpenID", value);
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
            return ValidationHelper.GetDouble(this.GetValue("ConversionValue"), 0);
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
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (!this.StopProcessing)
        {
            plcError.Visible = false;

            // Check renamed DLL library
            if (!CMSOpenIDHelper.CheckOpenIdDLL())
            {
                // Error label is displayed when OpenID library is not enabled
                lblError.Text = ResHelper.GetString("mem.openid.library");
                plcError.Visible = true;
                plcContent.Visible = false;
            }

            // Check if OpenID module is enabled
            if (!SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSEnableOpenID") && !this.plcError.Visible)
            {
                // Error label is displayed only in Design mode
                if (CMSContext.ViewMode == ViewModeEnum.Design)
                {
                    StringBuilder parameter = new StringBuilder();
                    parameter.Append(GetString("header.sitemanager") + " -> ");
                    parameter.Append(GetString("settingscategory.cmssettings") + " -> ");
                    parameter.Append(GetString("settingscategory.cmsmembership") + " -> ");
                    parameter.Append(GetString("settingscategory.cmsmembershipauthentication") + " -> ");
                    parameter.Append(GetString("settingscategory.cmsopenid"));
                    if (CMSContext.CurrentUser.UserSiteManagerAdmin)
                    {
                        // Make it link for SiteManager Admin
                        parameter.Insert(0, "<a href=\"" + URLHelper.GetAbsoluteUrl("~/CMSSiteManager/default.aspx?section=settings") + "\" target=\"_top\">");
                        parameter.Append("</a>");
                    }

                    lblError.Text = String.Format(GetString("mem.openid.disabled"), parameter.ToString());
                    plcError.Visible = true;
                    plcContent.Visible = false;
                }
                // In other modes is webpart hidden
                else
                {
                    this.Visible = false;
                }
            }

            // Display webpart when no error occured
            if (!plcError.Visible && this.Visible)
            {
                if (!CMSContext.CurrentUser.IsAuthenticated())
                {
                    plcPasswordNew.Visible = this.AllowFormsAuthentication;
                    pnlExistingUser.Visible = this.AllowExistingUser;

                    // Initialize OpenID session
                    openIDhelper = new CMSOpenIDHelper();
                    openIDhelper.Initialize(SessionHelper.GetValue(SESSION_NAME_USERDATA));

                    userProviderUrl = ValidationHelper.GetString(SessionHelper.GetValue(SESSION_NAME_URL), null);

                    // Check that OpenID is not already registered
                    if (openIDhelper.GetResponseObject() != null)
                    {
                        UserInfo ui = OpenIDUserInfoProvider.GetUserInfoByOpenID(openIDhelper.ClaimedIdentifier);

                        // OpenID is already registered to some user
                        if (ui != null)
                        {
                            plcContent.Visible = false;
                            plcError.Visible = true;
                            lblError.Text = GetString("mem.openid.openidregistered");
                        }
                    }

                    // There is no OpenID response object stored in session - hide all
                    if (openIDhelper.GetResponseObject() == null && HideForNoOpenID)
                    {
                        this.Visible = false;
                    }
                    else if (!RequestHelper.IsPostBack())
                    {
                        LoadData();
                    }
                }
                // Hide webpart for authenticated users
                else
                {
                    this.Visible = false;
                }
            }
        }
        // Hide control when StopProcessing = TRUE
        else
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Loads textboxes with provider-supplied data.
    /// </summary>
    private void LoadData()
    {
        if (!String.IsNullOrEmpty(openIDhelper.Nickname))
        {
            txtUserNameNew.Text = txtUserName.Text = openIDhelper.Nickname;
        }
        if (!String.IsNullOrEmpty(openIDhelper.Email))
        {
            txtEmail.Text = openIDhelper.Email;
        }
    }


    /// <summary>
    /// Handles btnOkExist click, joins existing user with OpenID.
    /// </summary>
    protected void btnOkExist_Click(object sender, EventArgs e)
    {
        // OpenID response object must be retrieved from session
        if ((openIDhelper != null) && (openIDhelper.GetResponseObject() != null))
        {
            if (txtUserName.Text != String.Empty)
            {
                // Try to authenticate user
                UserInfo ui = UserInfoProvider.AuthenticateUser(txtUserName.Text, txtPassword.Text, CMSContext.CurrentSiteName);

                // Check banned IPs
                BannedIPInfoProvider.CheckIPandRedirect(CMSContext.CurrentSiteName, BanControlEnum.Login);

                if (ui != null)
                {
                    // Check if user is not already registered with different OpenID provider
                    string openID = OpenIDUserInfoProvider.GetOpenIDByUserID(ui.UserID);
                    if (String.IsNullOrEmpty(openID))
                    {
                        // Add OpenID token to user
                        OpenIDUserInfoProvider.AddOpenIDToUser(openIDhelper.ClaimedIdentifier, userProviderUrl, ui.UserID);

                        // Remove user info from session
                        SessionHelper.Remove(SESSION_NAME_USERDATA);
                        SessionHelper.Remove(SESSION_NAME_URL);

                        // Set authentication cookie and redirect to page
                        SetAuthCookieAndRedirect(ui);
                    }
                    // User is already registered under different OpenID provider
                    else
                    {
                        lblError.Text = GetString("mem.openid.alreadyregistered");
                        plcError.Visible = true;
                    }
                }
                else // Invalid credentials
                {
                    lblError.Text = GetString("Login_FailureText");
                    plcError.Visible = true;
                }
            }
            else // User did not fill the form
            {
                lblError.Text = GetString("mem.openid.fillloginform");
                plcError.Visible = true;
            }
        }
    }


    /// <summary>
    /// Handles btnOkNew click, creates new user and joins it with openID token.
    /// </summary>
    protected void btnOkNew_Click(object sender, EventArgs e)
    {
        if ((openIDhelper != null) && (openIDhelper.GetResponseObject() != null))
        {
            // Validate entered values
            string errorMessage = new Validator().IsRegularExp(txtUserNameNew.Text, "^([a-zA-Z0-9_\\-\\.@]+)$", GetString("mem.openid.fillcorrectusername"))
                .IsEmail(txtEmail.Text, GetString("mem.openid.fillvalidemail")).Result;
            string siteName = CMSContext.CurrentSiteName;
            string password = passStrength.Text;

            // If password is enabled to set, check it
            if (plcPasswordNew.Visible && (errorMessage == String.Empty))
            {
                if (password == String.Empty)
                {
                    errorMessage = GetString("mem.liveid.specifyyourpass");
                }
                else if (password != txtConfirmPassword.Text.Trim())
                {
                    errorMessage = GetString("webparts_membership_registrationform.passwordonotmatch");
                }

                // Check policy
                if (!passStrength.IsValid())
                {
                    errorMessage = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                }
            }

            // Check whether email is unique if it is required
            if (string.IsNullOrEmpty(errorMessage) && !UserInfoProvider.IsEmailUnique(txtEmail.Text.Trim(), siteName, 0))
            {
                errorMessage = GetString("UserInfo.EmailAlreadyExist");
            }

            // Check reserved names
            if (string.IsNullOrEmpty(errorMessage) && UserInfoProvider.NameIsReserved(siteName, txtUserNameNew.Text.Trim()))
            {
                errorMessage = GetString("Webparts_Membership_RegistrationForm.UserNameReserved").Replace("%%name%%", HTMLHelper.HTMLEncode(txtUserNameNew.Text.Trim()));
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                // Check if user with given username already exists
                UserInfo ui = UserInfoProvider.GetUserInfo(txtUserNameNew.Text.Trim());

                // User with given username is already registered
                if (ui != null)
                {
                    plcError.Visible = true;
                    lblError.Text = GetString("mem.openid.usernameregistered");
                }
                else
                {
                    string error = this.DisplayMessage;
                    // Register new user
                    ui = UserInfoProvider.AuthenticateOpenIDUser(openIDhelper.ClaimedIdentifier, ValidationHelper.GetString(SessionHelper.GetValue(SESSION_NAME_URL), null), siteName, true, false, ref error);
                    this.DisplayMessage = error;

                    // If user successfuly created
                    if (ui != null)
                    {
                        // Set additional information
                        ui.UserName = ui.UserNickName = ui.FullName = txtUserNameNew.Text.Trim();
                        ui.Email = txtEmail.Text;

                        // Load values submited by OpenID provider
                        // Load date of birth
                        if (openIDhelper.BirthDate != DateTime.MinValue)
                        {
                            ui.UserSettings.UserDateOfBirth = openIDhelper.BirthDate;
                        }
                        // Load default country
                        if (openIDhelper.Culture != null)
                        {
                            ui.PreferredCultureCode = openIDhelper.Culture.Name;
                        }
                        // Nick name
                        if (!String.IsNullOrEmpty(openIDhelper.Nickname))
                        {
                            ui.UserSettings.UserNickName = openIDhelper.Nickname;
                        }
                        // User gender
                        if (openIDhelper.UserGender != null)
                        {
                            ui.UserSettings.UserGender = (int)openIDhelper.UserGender;
                        }
                        UserInfoProvider.SetUserInfo(ui);

                        // Set password
                        if (plcPasswordNew.Visible)
                        {
                            UserInfoProvider.SetPassword(ui, password);

                            // If user can choose password then is not considered external(external user can't login in common way)
                            ui.IsExternal = false;
                        }

                        // Additional information which was provided by OpenID provider to user account
                        // Birth date
                        if (openIDhelper.BirthDate != DateTime.MinValue)
                        {
                            ui.UserSettings.UserDateOfBirth = openIDhelper.BirthDate;
                        }

                        // Full name
                        if (!String.IsNullOrEmpty(openIDhelper.FullName))
                        {
                            ui.FullName = openIDhelper.FullName;
                        }

                        // Nick name
                        if (!String.IsNullOrEmpty(openIDhelper.Nickname))
                        {
                            ui.UserNickName = openIDhelper.Nickname;
                        }

                        // Set user
                        UserInfoProvider.SetUserInfo(ui);

                        // Clear used session
                        SessionHelper.Remove(SESSION_NAME_URL);
                        SessionHelper.Remove(SESSION_NAME_USERDATA);

                        UserInfoProvider.SendRegistrationEmails(ui, this.ApprovalPage, password, true, this.SendWelcomeEmail);

                        // Notify administrator
                        bool requiresConfirmation = SettingsKeyProvider.GetBoolValue(siteName + ".CMSRegistrationEmailConfirmation");
                        if (!requiresConfirmation && this.NotifyAdministrator && (this.FromAddress != String.Empty) && (this.ToAddress != String.Empty))
                        {
                            UserInfoProvider.NotifyAdministrator(ui, this.FromAddress, this.ToAddress);
                        }

                        // Log registration into analytics
                        UserInfoProvider.TrackUserRegistration(this.TrackConversionName, this.ConversionValue, siteName, ui);

                        // Log activity
                        if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.UserRegistrationEnabled(siteName)
                            && ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
                        {
                            int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
                            ModuleCommands.OnlineMarketingUpdateContactFromExternalData(ui, contactId);
                            TreeNode currentDoc = CMSContext.CurrentDocument;
                            ActivityLogProvider.LogRegistrationActivity(contactId,
                            ui, URLHelper.CurrentRelativePath, currentDoc.NodeID, siteName, CMSContext.Campaign, currentDoc.DocumentCulture);
                        }

                        // Set authentication cookie and redirect to page
                        SetAuthCookieAndRedirect(ui);

                        if (!String.IsNullOrEmpty(this.DisplayMessage))
                        {
                            lblInfo.Visible = true;
                            lblInfo.Text = this.DisplayMessage;
                            plcForm.Visible = false;
                        }
                        else
                        {
                            URLHelper.Redirect(ResolveUrl("~/Default.aspx"));
                        }
                    }
                }
            }
            // Validation failed - display error message
            else
            {
                lblError.Text = errorMessage;
                plcError.Visible = true;
            }
        }
    }


    /// <summary>
    /// Helper method, set authentication cookie and redirect to return URL or default page.
    /// </summary>
    /// <param name="ui">User info</param>
    private void SetAuthCookieAndRedirect(UserInfo ui)
    {
        // Create autentification cookie
        if (ui.Enabled)
        {
            UserInfoProvider.SetAuthCookieWithUserData(ui.UserName, true, Session.Timeout, new string[] { "openidlogin" });

            // Log activity
            string siteName = CMSContext.CurrentSiteName;
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.UserLoginEnabled(siteName))
            {
                int contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                ActivityLogHelper.UpdateContactLastLogon(contactId);
                if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                {
                    TreeNode currentDoc = CMSContext.CurrentDocument;
                    ActivityLogProvider.LogLoginActivity(contactId,
                        ui, URLHelper.CurrentRelativePath, currentDoc.NodeID, siteName, CMSContext.Campaign, currentDoc.DocumentCulture);
                }
            }

            string returnUrl = QueryHelper.GetString("returnurl", null);

            // Redirect to ReturnURL
            if (!String.IsNullOrEmpty(returnUrl))
            {
                URLHelper.Redirect(ResolveUrl(HttpUtility.UrlDecode(returnUrl)));
            }
            // Redirect to default page    
            else if (!String.IsNullOrEmpty(this.DefaultTargetUrl))
            {
                URLHelper.Redirect(ResolveUrl(this.DefaultTargetUrl));
            }
            // Otherwise refresh current page
            else
            {
                URLHelper.Redirect(URLRewriter.CurrentURL);
            }
        }
    }

    #endregion
}
