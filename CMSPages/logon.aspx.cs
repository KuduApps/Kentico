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
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.OutputFilter;

public partial class CMSPages_logon : CMSPage
{
    #region "Variables"

    private LocalizedLabel mFailureLabel = null;
    private bool? mShowForgottenPassword = null;

    public string tablePadding = string.Empty;
    public string labelClass = "LoginLabel";
    public string textboxClass = "LoginTextBox";

    #endregion


    #region "Properties"

    /// <summary>
    /// Failure text label.
    /// </summary>
    public LocalizedLabel FailureLabel
    {
        get
        {
            if (mFailureLabel == null)
            {
                mFailureLabel = (LocalizedLabel)Login1.FindControl("FailureText");
            }
            return mFailureLabel;
        }
    }


    /// <summary>
    /// Returns whether is page in forgotten password "mode"
    /// </summary>
    private bool IsForgottenPassword
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["ForgottenPassword"], false);
        }
        set
        {
            ViewState["ForgottenPassword"] = value;
        }
    }


    /// <summary>
    /// Gets or sets whether link to forgotten password is shown on logon page.
    /// </summary>
    public bool ShowForgottenPassword
    {
        get
        {
            if (mShowForgottenPassword == null)
            {
                mShowForgottenPassword = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSShowForgottenPassLink"], true);
            }

            return mShowForgottenPassword.Value;
        }
        set
        {
            mShowForgottenPassword = value;
        }
    }


    /// <summary>
    /// Gets return URL for logon page
    /// </summary>
    public string ReturnUrl
    {
        get
        {
            return QueryHelper.GetString("returnurl", string.Empty);
        }
    }


    /// <summary>
    /// Indicates if logon for Site Manager
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            return ReturnUrl.ToLower().Contains("cmssitemanager");
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        SetBrowserClass();
        AddNoCacheTag();

        // Ensure the refresh script
        bool sameWindow = (FormStateHelper.PreservePostbackValuesOnLogin && (FormStateHelper.GetSavedState() != null));
        string defaultCondition = "((top.frames['cmsheader'] != null) || (top.frames['propheader'] != null))";
        if (sameWindow)
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "TopWindow", ScriptHelper.GetScript("if (((document.body.clientWidth < 510) || (document.body.clientHeight < 300)) && " + defaultCondition + ") { var loginUrl = top.window.location.href.replace(/\\/\\(\\w\\([^\\)]+\\)\\)/g, ''); if (loginUrl.indexOf('restorepost=0') < 0) { if (loginUrl.indexOf('?') >= 0) { loginUrl = loginUrl + '&restorepost=0'; } else { loginUrl = loginUrl + '?restorepost=0'; } } top.window.location.replace(loginUrl); }"));
        }
        else
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TopWindow", ScriptHelper.GetScript("if " + defaultCondition + " { top.window.location.replace(top.window.location.href.replace(/\\/\\(\\w\\([^\\)]+\\)\\)/g, '')); }"));
        }

        // Enable caps lock check
        if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSUseCapsLockNotification"], true))
        {
            RegisterCAPSLOCKScript();
            TextBox txtPassword = (TextBox)Login1.FindControl("Password");
            if (txtPassword != null)
            {
                txtPassword.Attributes.Add("onkeypress", "CheckCapsLock(event)");
            }
        }

        // Site manager prefix
        string smPrefix = IsSiteManager ? "SM/" : null;

        // Initialize help
        CMSAdminControls_UI_PageElements_Help ucHelp = (CMSAdminControls_UI_PageElements_Help)Login1.FindControl("ucHelp");
        ucHelp.Tooltip = GetString("logon.helptooltip");
        ucHelp.IconUrl = GetImageUrl("Others/LogonForm/" + smPrefix + "HelpButton.png");
        ucHelp.TopicName = "LogonTroubleshooting";

        LocalizedLabel lblItem = (LocalizedLabel)Login1.FindControl("lblUserName");
        if (lblItem != null)
        {
            lblItem.Text = "{$LogonForm.UserName$}";
        }
        lblItem = (LocalizedLabel)Login1.FindControl("lblPassword");
        if (lblItem != null)
        {
            lblItem.Text = "{$LogonForm.Password$}";
        }

        // Display culture link due to value of the key stored in the web.config file
        bool showCultureSelector = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSShowLogonCultureSelector"], true);
        if (showCultureSelector)
        {
            ImageButton lnkLanguage = (ImageButton)Login1.FindControl("lnkLanguage");
            if (lnkLanguage != null)
            {
                lnkLanguage.Visible = true;

                // Ensure language selection panel functionality
                HtmlGenericControl pnlLanguage = (HtmlGenericControl)Login1.FindControl("pnlLanguage");
                if (pnlLanguage != null)
                {
                    lnkLanguage.ImageUrl = GetImageUrl("Others/LogonForm/" + smPrefix + "LanguageButton.png");
                    lnkLanguage.AlternateText = GetString("LogonForm.SelectLanguage");
                    lnkLanguage.ToolTip = GetString("LogonForm.SelectLanguage");

                    this.ltlScript.Text = ScriptHelper.GetScript("function ShowLanguage(id){var panel=document.getElementById(id);if(panel!=null){panel.style.display=(panel.style.display == 'block')?'none':'block';}}");
                    lnkLanguage.Attributes.Add("onclick", "ShowLanguage('" + pnlLanguage.ClientID + "'); return false;");
                }
            }
        }

        // Set up forgotten password link
        if (ShowForgottenPassword)
        {
            ImageButton lnkPassword = (ImageButton)Login1.FindControl("lnkPassword");
            if (lnkPassword != null)
            {
                lnkPassword.Visible = true;
                lnkPassword.ImageUrl = GetImageUrl("Others/LogonForm/" + smPrefix + "ForgottenPassword.png");
                lnkPassword.AlternateText = GetString("LogonForm.ForgottenPassword");
                lnkPassword.ToolTip = GetString("LogonForm.ForgottenPassword");
                lnkPassword.Click += new ImageClickEventHandler(lnkPassword_Click);
            }
        }

        LocalizedCheckBox chkItem = (LocalizedCheckBox)Login1.FindControl("chkRememberMe");
        if (chkItem != null)
        {
            chkItem.Text = "{$LogonForm.RememberMe$}";
        }

        LocalizedButton btnItem = (LocalizedButton)Login1.FindControl("LoginButton");
        if (btnItem != null)
        {
            btnItem.Text = "{$LogonForm.LogOnButton$}";
            btnItem.Click += new EventHandler(btnItem_Click);
        }

        RequiredFieldValidator rfvUserName = (RequiredFieldValidator)Login1.FindControl("rfvUserNameRequired");
        if (rfvUserName != null)
        {
            rfvUserName.ToolTip = GetString("LogonForm.NameRequired");
            rfvUserName.ErrorMessage = GetString("LogonForm.NameRequired");
        }

        // Load UI cultures for the dropdown list
        if (!RequestHelper.IsPostBack())
        {
            LoadCultures();
        }

        Label lblVersion = (Label)this.FindControl("lblVersion");
        if (lblVersion != null)
        {
            // Show version only if right key is inserted
            string versionKey = QueryHelper.GetString("versionkey", string.Empty);
            if (EncryptionHelper.VerifyVersionRSA(versionKey))
            {
                lblVersion.Text = ResHelper.GetString("Footer.Version") + "&nbsp;" + CMSContext.GetFriendlySystemVersion(true);
            }
        }

        Login1.LoggingIn += new LoginCancelEventHandler(Login1_LoggingIn);
        Login1.LoggedIn += new EventHandler(Login1_LoggedIn);
        Login1.LoginError += new EventHandler(Login1_LoginError);

        if (!RequestHelper.IsPostBack())
        {
            Login1.UserName = QueryHelper.GetString("username", String.Empty);
        }

        // Ensure username textbox focus
        TextBox txtUserName = (TextBox)Login1.FindControl("UserName");
        if (txtUserName != null)
        {
            ScriptHelper.RegisterStartupScript(this, GetType(), "SetFocus", ScriptHelper.GetScript("var txt=document.getElementById('" + txtUserName.ClientID + "');if(txt!=null){txt.focus();}"));
        }

        // Ensure different class for Site Manager
        if (IsSiteManager)
        {
            pnlMainContainer.CssClass += " SM";
        }

        if (QueryHelper.GetBoolean("forgottenpassword", false))
        {
            SetForgottenPasswordMode();
        }
    }


    void btnItem_Click(object sender, EventArgs e)
    {
        // Check if should send password
        if (IsForgottenPassword)
        {
            SetForgottenPasswordMode();

            TextBox txtUserName = (TextBox)Login1.FindControl("UserName");
            LocalizedLabel lblForgottenResult = (LocalizedLabel)Login1.FindControl("lblForgottenResult");
            if (txtUserName != null)
            {
                // Reset password
                string siteName = CMSContext.CurrentSiteName;
                lblForgottenResult.Visible = true;
                bool success;
                lblForgottenResult.Text = UserInfoProvider.ForgottenEmailRequest(txtUserName.Text.Trim(), siteName, "Logon page", SettingsKeyProvider.GetStringValue(siteName + ".CMSSendPasswordEmailsFrom"), null, UserInfoProvider.GetResetPasswordUrl(siteName), out success);
            }
        }
    }


    void lnkPassword_Click(object sender, ImageClickEventArgs e)
    {
        if (!IsForgottenPassword)
        {
            SetForgottenPasswordMode();
        }
        else
        {            
            URLHelper.Redirect(URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, "forgottenpassword"));
        }
    }


    void Login1_LoginError(object sender, EventArgs e)
    {
        if (this.FailureLabel != null)
        {
            FailureLabel.Text = GetString("Login_FailureText");
        }
    }


    void Login1_LoggedIn(object sender, EventArgs e)
    {
        // Ensure response cookie
        CookieHelper.EnsureResponseCookie(FormsAuthentication.FormsCookieName);

        // Set cookie expiration
        if (Login1.RememberMeSet)
        {
            CookieHelper.ChangeCookieExpiration(FormsAuthentication.FormsCookieName, DateTime.Now.AddYears(1), false);
        }
        else
        {
            // Extend the expiration of the authentication cookie if required
            if (!UserInfoProvider.UseSessionCookies && (HttpContext.Current != null) && (HttpContext.Current.Session != null))
            {
                CookieHelper.ChangeCookieExpiration(FormsAuthentication.FormsCookieName, DateTime.Now.AddMinutes(Session.Timeout), false);
            }
        }

        // Current username
        string userName = Login1.UserName;

        // Get info on the authenticated user
        UserInfo ui = UserInfoProvider.GetUserInfo(userName);

        // Check whether safe user name is required and if so get safe username
        if (RequestHelper.IsMixedAuthentication() && UserInfoProvider.UseSafeUserName && (ui == null))
        {
            userName = ValidationHelper.GetSafeUserName(this.Login1.UserName, CMSContext.CurrentSiteName);
            CMSContext.AuthenticateUser(userName, this.Login1.RememberMeSet);
        }

        // Set culture
        DropDownList drpCulture = (DropDownList)Login1.FindControl("drpCulture");
        if (drpCulture != null)
        {
            string selectedCulture = drpCulture.SelectedValue;

            // Not the default culture
            if (selectedCulture != "")
            {
                // Update the user
                ui.PreferredUICultureCode = selectedCulture;
                UserInfoProvider.SetUserInfo(ui);

                // Update current user
                CMSContext.CurrentUser.PreferredUICultureCode = selectedCulture;
            }
        }

        // Splash screen handling    
        bool splashScreenEnabled = false;
        LicenseKeyInfo lki = LicenseKeyInfoProvider.GetLicenseKeyInfo(URLHelper.GetCurrentDomain());
        if ((lki != null) && lki.IsTrial && ui.UserSettings.UserShowSplashScreen)
        {
            if (lki.ExpirationDate != DateTimeHelper.ZERO_TIME)
            {
                // Display splash screen only if using trial license
                splashScreenEnabled = true;
            }
        }

        // Splash screen
        string returnUrl = ReturnUrl;

        // Return url is not specified or is relative path or hash is valid
        if (string.IsNullOrEmpty(returnUrl) || returnUrl.StartsWith("~") || returnUrl.StartsWith("/") || QueryHelper.ValidateHash("hash"))
        {
            if (splashScreenEnabled && ui.UserSettings.UserShowSplashScreen && SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSShowSplashScreen"))
            {
                if ((!String.IsNullOrEmpty(CMSContext.CurrentSiteName)) && (returnUrl.Contains("cmsdesk") || IsSiteManager))
                {
                    URLHelper.Redirect(ResolveUrl("~/CMSSiteManager/SplashScreen.aspx?continueurl=" + returnUrl));
                }
            }

            // Destination page URL
            if (returnUrl.Contains("restorepost"))
            {
                // Delete the saved state
                SavedFormState state = FormStateHelper.GetSavedState();
                if (state != null)
                {
                    state.Delete();
                }

                returnUrl = URLHelper.RemoveParameterFromUrl(returnUrl, "restorepost");
                URLHelper.Redirect(ResolveUrl(returnUrl));
            }
        }
        else
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("general.badhashtitle") + "&text=" + ResHelper.GetString("general.badhashtext")));
        }

    }


    void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
    {
        // Ban IP addresses which are blocked for login
        if ((CMSContext.CurrentSite != null) && !BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.Login))
        {
            e.Cancel = true;

            if (FailureLabel != null)
            {
                FailureLabel.Visible = true;
                FailureLabel.Text = GetString("banip.ipisbannedlogin");
            }
        }

        if (((CheckBox)Login1.FindControl("chkRememberMe")).Checked)
        {
            Login1.RememberMeSet = true;
        }
        else
        {
            Login1.RememberMeSet = false;
        }
    }


    /// <summary>
    /// Load UI cultures for the dropdown list.
    /// </summary>
    private void LoadCultures()
    {
        DropDownList drpCulture = (DropDownList)Login1.FindControl("drpCulture");
        if (drpCulture != null)
        {
            DataSet ds = UICultureInfoProvider.GetAllUICultures();
            DataView dvCultures = ds.Tables[0].DefaultView;
            dvCultures.Sort = "UICultureName ASC";

            drpCulture.DataValueField = "UICultureCode";
            drpCulture.DataTextField = "UICultureName";
            drpCulture.DataSource = dvCultures;
            drpCulture.DataBind();

            // Add default value
            drpCulture.Items.Insert(0, new ListItem(GetString("LogonForm.DefaultCulture"), ""));

            LocalizedLabel lblCulture = (LocalizedLabel)Login1.FindControl("lblCulture");
            if (lblCulture != null)
            {
                lblCulture.AssociatedControlID = drpCulture.ID;
                lblCulture.Text = ResHelper.GetString("general.select");
                lblCulture.Display = false;
            }
        }
    }


    /// <summary>
    /// Registers the script to handle the CAPSLOCK check.
    /// </summary>
    private void RegisterCAPSLOCKScript()
    {
        string script =
            "function OnCapslockOn() {\n " +
                "document.getElementById('" + FailureLabel.ClientID + "').innerHTML = " + ScriptHelper.GetString(GetString("General.Capslock")) + "; \n" +
                "} \n" +

            "function OnCapslockOff() {\n " +
                    "var elem = document.getElementById('" + FailureLabel.ClientID + "');\n" +
                    "if(elem.innerHTML != ''){elem.innerHTML = '';} \n" +
                "} \n";

        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "CapsLockHandling", ScriptHelper.GetScript(script) + ScriptHelper.CapslockScript);
    }


    /// <summary>
    /// Sets forgotten password mode.
    /// </summary>
    private void SetForgottenPasswordMode()
    {
        LocalizedButton btnItem = (LocalizedButton)Login1.FindControl("LoginButton");
        if (btnItem != null)
        {
            btnItem.ResourceString = "LogonForm.SendRequest";
            btnItem.CommandName = string.Empty;
        }

        TextBox txtPassword = (TextBox)Login1.FindControl("Password");
        if (txtPassword != null)
        {
            txtPassword.Visible = false;
        }

        LocalizedLabel lblItem = (LocalizedLabel)Login1.FindControl("lblPassword");
        if (lblItem != null)
        {
            lblItem.Visible = false;
        }

        CheckBox chkRemeber = (CheckBox)Login1.FindControl("chkRememberMe");
        if (chkRemeber != null)
        {
            chkRemeber.Visible = false;
        }

        LocalizedLabel lblLogOn = (LocalizedLabel)Login1.FindControl("lblLogOn");
        if (lblLogOn != null)
        {
            lblLogOn.ResourceString = "logonform.lnkpasswordretrieval";
        }

        LocalizedLabel lblUserName = (LocalizedLabel)Login1.FindControl("lblUserName");
        if (lblUserName != null)
        {
            lblUserName.ResourceString = "logonform.lblpasswordretrieval";
        }

        ImageButton lnkPassword = (ImageButton)Login1.FindControl("lnkPassword");
        if (lnkPassword != null)
        {
            lnkPassword.ToolTip = GetString("logonform.logonbutton");
        }

        tablePadding = "TablePadding";
        labelClass += " PasswordLabel";

        IsForgottenPassword = true;
    }
}
