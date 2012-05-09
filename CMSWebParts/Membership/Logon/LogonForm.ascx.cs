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
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.DataEngine;
using CMS.EmailEngine;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.URLRewritingEngine;
using CMS.WebAnalytics;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Membership_Logon_LogonForm : CMSAbstractWebPart
{
    #region "Private properties"

    private string mDefaultTargetUrl = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether retrieving of forgotten password is enabled.
    /// </summary>
    public bool AllowPasswordRetrieval
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowPasswordRetrieval"), true);
        }
        set
        {
            this.SetValue("AllowPasswordRetrieval", value);
            this.lnkPasswdRetrieval.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the sender e-mail (from).
    /// </summary>
    public string SendEmailFrom
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SendEmailFrom"), SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSendPasswordEmailsFrom"));
        }
        set
        {
            this.SetValue("SendEmailFrom", value);
        }
    }


    /// <summary>
    /// Gets or sets the default target url (rediredction when the user is logged in).
    /// </summary>
    public string DefaultTargetUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DefaultTargetUrl"), mDefaultTargetUrl);
        }
        set
        {
            this.SetValue("DefaultTargetUrl", value);
            this.mDefaultTargetUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the SkinID of the logon form.
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
    /// Gets or sets the logon failure text.
    /// </summary>
    public string FailureText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FailureText"), "");
        }
        set
        {
            if (value.ToString().Trim() != "")
            {
                this.SetValue("FailureText", value);
                this.Login1.FailureText = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets reset password url - this url is sent to user in e-mail when he wants to reset his password.
    /// </summary>
    public string ResetPasswordURL
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ResetPasswordURL"), UserInfoProvider.GetResetPasswordUrl(CMSContext.CurrentSiteName));
        }
        set
        {
            this.SetValue("ResetPasswordURL", value);
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
            this.rqValue.Visible = false;
            // Do not process
        }
        else
        {
            // Set failure text
            if (this.FailureText != "")
            {
                Login1.FailureText = ResHelper.LocalizeString(this.FailureText);
            }
            else
            {
                Login1.FailureText = GetString("Login_FailureText");
            }

            // Set strings
            lnkPasswdRetrieval.Text = GetString("LogonForm.lnkPasswordRetrieval");
            lblPasswdRetrieval.Text = GetString("LogonForm.lblPasswordRetrieval");
            btnPasswdRetrieval.Text = GetString("LogonForm.btnPasswordRetrieval");
            rqValue.ErrorMessage = GetString("LogonForm.rqValue");
            rqValue.ValidationGroup = ClientID + "_PasswordRetrieval";

            // Set logon strings
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
            LocalizedCheckBox chkItem = (LocalizedCheckBox)Login1.FindControl("chkRememberMe");
            if (chkItem != null)
            {
                chkItem.Text = "{$LogonForm.RememberMe$}";
            }
            LocalizedButton btnItem = (LocalizedButton)Login1.FindControl("LoginButton");
            if (btnItem != null)
            {
                btnItem.Text = "{$LogonForm.LogOnButton$}";
                btnItem.ValidationGroup = ClientID + "_Logon";
            }

            RequiredFieldValidator rfv = (RequiredFieldValidator)Login1.FindControl("rfvUserNameRequired");
            if (rfv != null)
            {
                rfv.ValidationGroup = ClientID + "_Logon";
                rfv.ToolTip = GetString("LogonForm.NameRequired");
            }

            this.lnkPasswdRetrieval.Visible = this.AllowPasswordRetrieval;

            Login1.LoggedIn += new EventHandler(Login1_LoggedIn);
            Login1.LoggingIn += new LoginCancelEventHandler(Login1_LoggingIn);

            btnPasswdRetrieval.Click += new EventHandler(btnPasswdRetrieval_Click);
            btnPasswdRetrieval.ValidationGroup = ClientID + "_PasswordRetrieval";

            if (!RequestHelper.IsPostBack())
            {
                Login1.UserName = ValidationHelper.GetString(Request.QueryString["username"], "");
                // Set SkinID properties
                if (!this.StandAlone && (this.PageCycle < PageCycleEnum.Initialized) && (ValidationHelper.GetString(this.Page.StyleSheetTheme, "") == ""))
                {
                    SetSkinID(this.SkinID);
                }
            }

            if (!this.AllowPasswordRetrieval)
            {
                pnlPasswdRetrieval.Visible = false;
            }
        }
    }


    /// <summary>
    /// OnLoad override (show hide password retrieval).
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (hdnPasswDisplayed.Value == "")
        {
            this.pnlPasswdRetrieval.Attributes.Add("style", "display:none;");
        }
        else
        {
            this.pnlPasswdRetrieval.Attributes.Add("style", "display:block;");
        }
    }


    /// <summary>
    /// Sets SkinId to all controls in logon form.
    /// </summary>
    void SetSkinID(string skinId)
    {
        if (skinId != "")
        {
            Login1.SkinID = skinId;

            LocalizedLabel lbl = (LocalizedLabel)this.Login1.FindControl("lblUserName");
            if (lbl != null)
            {
                lbl.SkinID = skinId;
            }
            lbl = (LocalizedLabel)this.Login1.FindControl("lblPassword");
            if (lbl != null)
            {
                lbl.SkinID = skinId;
            }

            TextBox txt = (TextBox)this.Login1.FindControl("UserName");
            if (txt != null)
            {
                txt.SkinID = skinId;
            }
            txt = (TextBox)this.Login1.FindControl("Password");
            if (txt != null)
            {
                txt.SkinID = skinId;
            }

            LocalizedCheckBox chk = (LocalizedCheckBox)this.Login1.FindControl("chkRememberMe");
            if (chk != null)
            {
                chk.SkinID = skinId;
            }

            LocalizedButton btn = (LocalizedButton)this.Login1.FindControl("LoginButton");
            if (btn != null)
            {
                btn.SkinID = skinId;
            }
        }
    }


    /// <summary>
    /// Applies given stylesheet skin.
    /// </summary>
    public override void ApplyStyleSheetSkin(Page page)
    {
        SetSkinID(this.SkinID);

        base.ApplyStyleSheetSkin(page);
    }


    /// <summary>
    /// Retrieve the user password.
    /// </summary>
    void btnPasswdRetrieval_Click(object sender, EventArgs e)
    {
        string value = txtPasswordRetrieval.Text.Trim();

        if (value != String.Empty)
        {
            bool success;
            lblResult.Text = UserInfoProvider.ForgottenEmailRequest(value, CMSContext.CurrentSiteName, "LOGONFORM", this.SendEmailFrom, CMSContext.CurrentResolver, ResetPasswordURL, out success);
            lblResult.Visible = true;

            this.pnlPasswdRetrieval.Attributes.Add("style", "display:block;");
        }
    }


    /// <summary>
    /// Logged in handler.
    /// </summary>
    void Login1_LoggedIn(object sender, EventArgs e)
    {
        // Set view mode to live site after login to prevent bar with "Close preview mode"
        CMSContext.ViewMode = CMS.PortalEngine.ViewModeEnum.LiveSite;

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

        // Get user name (test site prefix too)
        UserInfo ui = UserInfoProvider.GetUserInfoForSitePrefix(userName, CMSContext.CurrentSite);

        // Check whether safe user name is required and if so get safe username
        if (RequestHelper.IsMixedAuthentication() && UserInfoProvider.UseSafeUserName)
        {
            // Get info on the authenticated user            
            if (ui == null)
            {
                // User stored with safe name
                userName = ValidationHelper.GetSafeUserName(this.Login1.UserName, CMSContext.CurrentSiteName);

                // Find user by safe name
                ui = UserInfoProvider.GetUserInfoForSitePrefix(userName, CMSContext.CurrentSite);
                if (ui != null)
                {
                    // Authenticate user by site or global safe username
                    CMSContext.AuthenticateUser(ui.UserName, this.Login1.RememberMeSet);
                }
            }
        }

        if (ui != null)
        {
            // If user name is site prefixed, authenticate user manually 
            if (UserInfoProvider.IsSitePrefixedUser(ui.UserName))
            {
                CMSContext.AuthenticateUser(ui.UserName, this.Login1.RememberMeSet);
            }

            // Log activity
            string siteName = CMSContext.CurrentSiteName;
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.UserLoginEnabled(siteName))
            {
                int contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                ActivityLogHelper.UpdateContactLastLogon(contactId);
                if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                {
                    TreeNode currentDoc = CMSContext.CurrentDocument;
                    ActivityLogProvider.LogLoginActivity(contactId, ui, URLHelper.CurrentRelativePath,
                        (currentDoc != null ? currentDoc.NodeID : 0), CMSContext.CurrentSiteName, CMSContext.Campaign, (currentDoc != null ? currentDoc.DocumentCulture : null));
                }
            }
        }

        // Redirect user to the return url, or if is not defined redirect to the default target url
        string url = QueryHelper.GetString("ReturnURL", string.Empty);
        if (!string.IsNullOrEmpty(url))
        {
            if (url.StartsWith("~") || url.StartsWith("/") || QueryHelper.ValidateHash("hash"))
            {
                URLHelper.Redirect(ResolveUrl(ValidationHelper.GetString(Request.QueryString["ReturnURL"], "")));
            }
            else
            {
                URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("general.badhashtitle") + "&text=" + ResHelper.GetString("general.badhashtext")));
            }
        }
        else
        {
            if (this.DefaultTargetUrl != "")
            {
                URLHelper.Redirect(ResolveUrl(this.DefaultTargetUrl));
            }
            else
            {
                URLHelper.Redirect(URLRewriter.CurrentURL);
            }
        }
    }


    /// <summary>
    /// Ligging in handler.
    /// </summary>
    void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
    {
        // Ban IP addresses which are blocked for login
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.Login))
        {
            e.Cancel = true;

            LocalizedLiteral failureLit = Login1.FindControl("FailureText") as LocalizedLiteral;
            if (failureLit != null)
            {
                failureLit.Visible = true;
                failureLit.Text = GetString("banip.ipisbannedlogin");
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
    /// On prerender event.
    /// </summary>
    /// <param name="e">Arguments.</param>
    protected override void OnPreRender(EventArgs e)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("function ShowPasswdRetrieve(id){var hdnDispl = document.getElementById('" + this.hdnPasswDisplayed.ClientID + "'); style=document.getElementById(id).style;style.display=(style.display == 'block')?'none':'block'; if(hdnDispl != null){ if(style.display == 'block'){hdnDispl.value='1';}else{hdnDispl.value='';}}}");
        this.lnkPasswdRetrieval.Attributes.Add("onclick", "ShowPasswdRetrieve('" + pnlPasswdRetrieval.ClientID + "'); return false;");

        base.OnPreRender(e);
    }

    ///<summary>
    /// Overrides the generation of the SPAN tag with custom tag.
    ///</summary>
    protected HtmlTextWriterTag TagKey
    {
        get
        {
            if (CMSContext.CurrentSite != null)
            {
                if (SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSControlElement").ToLower().Trim() == "div")
                {
                    return HtmlTextWriterTag.Div;
                }
                else
                {
                    return HtmlTextWriterTag.Span;
                }
            }
            return HtmlTextWriterTag.Span;
        }
    }
}
