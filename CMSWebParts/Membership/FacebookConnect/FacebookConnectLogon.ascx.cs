using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.MembershipProvider;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Membership_FacebookConnect_FacebookConnectLogon : CMSAbstractWebPart
{
    #region "Constants"

    protected const string SESSION_NAME_USERDATA = "facebookid";
    protected const string CONFIRMATION_URLPARAMETER = "confirmed";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets sign in text.
    /// </summary>
    public string SignInText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SignInText"), "");
        }
        set
        {
            SetValue("SignInText", value);
        }
    }


    /// <summary>
    /// Get or sets size of Facebook Connect login button
    /// Allowed values: 'small', 'medium', 'large', 'xlarge'
    /// </summary>
    public string Size
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Size"), "medium");
        }
        set
        {
            SetValue("Size", value);
        }
    }


    /// <summary>
    /// Get or sets length of Facebook Connect login button
    /// Allowed values: 'short', 'long'
    /// </summary>
    public string Length
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Length"), "short");
        }
        set
        {
            SetValue("Length", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether to show sign out link.
    /// </summary>
    public bool LatestVersion
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("LatestVersion"), false);
        }
        set
        {
            SetValue("LatestVersion", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether to show sign out link.
    /// </summary>
    public bool ShowSignOut
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowSignOut"), false);
        }
        set
        {
            SetValue("ShowSignOut", value);
        }
    }


    /// <summary>
    /// Gets or sets sign out text.
    /// </summary>
    public string SignOutText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SignOutText"), "");
        }
        set
        {
            SetValue("SignOutText", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that buttons will be used instead of links.
    /// </summary>
    public bool ShowAsButton
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAsButton"), false);
        }
        set
        {
            SetValue("ShowAsButton", value);
        }
    }


    /// <summary>
    /// Gets or sets sign out button image URL.
    /// </summary>
    public string SignOutImageURL
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SignOutImageURL"), null);
        }
        set
        {
            SetValue("SignOutImageURL", value);
        }
    }


    /// <summary>
    /// Returns attribute activating "latest version" mode if "latest version" mode is requested.
    /// </summary>
    protected string LatestVersionAttribute
    {
        get
        {
            return (LatestVersion ? "v=\"2\"" : "");
        }
    }


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


    /// <summary>
    /// Gets or sets the value that indicates whether after successful registration is 
    /// notification email sent to the administrator.
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
    /// Gets the sender email (from).
    /// </summary>
    private string FromAddress
    {
        get
        {
            return SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress");
        }
    }


    /// <summary>
    /// Gets the recipient email (to).
    /// </summary>
    private string ToAddress
    {
        get
        {
            return SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSAdminEmailAddress");
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
        if (StopProcessing)
        {
            this.Visible = false;
        }
        else
        {
            if (QueryHelper.GetInteger("logout", 0) > 0)
            {
                // Sign out from CMS
                FormsAuthentication.SignOut();
                CMSContext.ClearShoppingCart();
                CMSContext.CurrentUser = null;
                Response.Cache.SetNoStore();
                URLHelper.Redirect(URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, "logout"));
                return;
            }

            string currentSiteName = CMSContext.CurrentSiteName;
            if (!String.IsNullOrEmpty(currentSiteName) && SettingsKeyProvider.GetBoolValue(currentSiteName + ".CMSEnableFacebookConnect"))
            {
                // Check Facebook Connect settings
                if (!FacebookConnectHelper.FacebookIsAvailable(currentSiteName))
                {
                    // Display warning message in "Design mode"
                    if (DisplayMessage())
                    {
                        return;
                    }

                    this.Visible = false;
                    return;
                }

                // Try to retrieve return URL from query
                string returnUrl = QueryHelper.GetString("returnurl", null);

                // Init Facebook Connect
                if (this.Page is ContentPage)
                {
                    // Adding XML namespace
                    ((ContentPage)this.Page).XmlNamespace = FacebookConnectHelper.GetFacebookXmlNamespace();
                }
                // Init FB connect
                ltlScript.Text = FacebookConnectHelper.GetFacebookInitScriptForSite(currentSiteName);
                // Return URL
                string currentUrl = URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "logout", "1");
                string additionalScript = "window.location.href=" + ScriptHelper.GetString(URLHelper.GetAbsoluteUrl(currentUrl)) + "; return false;";
                // Logout script for FB connect
                string logoutScript = FacebookConnectHelper.GetFacebookLogoutScriptForSignOut(URLHelper.CurrentURL, FacebookConnectHelper.GetFacebookApiKey(currentSiteName), additionalScript);

                string facebookUserId = "";
                bool facebookCookiesValid = FacebookConnectHelper.GetFacebookSessionInfo(currentSiteName, out facebookUserId) == FacebookValidationEnum.ValidSignature;

                // If user is already authenticated 
                if (CMSContext.CurrentUser.IsAuthenticated())
                {
                    // Is user logged in using Facebook Connect?
                    if (!facebookCookiesValid || ((CMSContext.CurrentUser.UserSettings != null) &&
                        (CMSContext.CurrentUser.UserSettings.UserFacebookID != facebookUserId)))
                    {  // no, user is not logged in by Facebook Connect
                        logoutScript = additionalScript;
                    }

                    // Hide Facebook Connect button
                    plcFBButton.Visible = false;

                    // If signout should be visible and user has FacebookID registered
                    if (ShowSignOut && !String.IsNullOrEmpty(CMSContext.CurrentUser.UserSettings.UserFacebookID))
                    {
                        // If only text is set use text/button link
                        if (!String.IsNullOrEmpty(SignOutText))
                        {
                            // Button link
                            if (ShowAsButton)
                            {
                                btnSignOut.OnClientClick = logoutScript;
                                btnSignOut.Text = SignOutText;
                                btnSignOut.Visible = true;
                            }
                            // Text link
                            else
                            {
                                lnkSignOutLink.Text = SignOutText;
                                lnkSignOutLink.Visible = true;
                                lnkSignOutLink.Attributes.Add("onclick", logoutScript);
                                lnkSignOutLink.Attributes.Add("style", "cursor:pointer;");
                            }
                        }
                        // Image link
                        else
                        {
                            string signOutImageUrl = SignOutImageURL;
                            // Use default image if none is specified
                            if (String.IsNullOrEmpty(signOutImageUrl))
                            {
                                signOutImageUrl = GetImageUrl("Others/FacebookConnect/signout.gif");
                            }
                            imgSignOut.ImageUrl = ResolveUrl(signOutImageUrl);
                            imgSignOut.Visible = true;
                            imgSignOut.AlternateText = GetString("webparts_membership_signoutbutton.signout");
                            lnkSignOutImageBtn.Visible = true;
                            lnkSignOutImageBtn.Attributes.Add("onclick", logoutScript);
                            lnkSignOutImageBtn.Attributes.Add("style", "cursor:pointer;");
                        }
                    }
                    else
                    {
                        Visible = false;
                    }
                }
                // Sign In
                else
                {
                    if ((QueryHelper.GetInteger(CONFIRMATION_URLPARAMETER, 0) > 0) && facebookCookiesValid)
                    {
                        if (!String.IsNullOrEmpty(facebookUserId))
                        {
                            UserInfo ui = UserInfoProvider.GetUserInfoByFacebookConnectID(facebookUserId);
                            // Claimed Facebook ID is in DB
                            if (ui != null)
                            {
                                // Login existing user
                                if ((ui != null) && ui.Enabled)
                                {
                                    // Ban IP addresses which are blocked for login
                                    BannedIPInfoProvider.CheckIPandRedirect(currentSiteName, BanControlEnum.Login);

                                    // Create autentification cookie
                                    UserInfoProvider.SetAuthCookieWithUserData(ui.UserName, true, Session.Timeout, new string[] { "facebooklogon" });
                                    UserInfoProvider.SetPreferredCultures(ui);

                                    // Log activity
                                    if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(currentSiteName) && ActivitySettingsHelper.UserLoginEnabled(currentSiteName))
                                    {
                                        int contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                                        ActivityLogHelper.UpdateContactLastLogon(contactId);
                                        if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                                        {
                                            TreeNode currDoc = CMSContext.CurrentDocument;
                                            ActivityLogProvider.LogLoginActivity(contactId,
                                                ui, URLHelper.CurrentRelativePath, currDoc.NodeID, currentSiteName, CMSContext.Campaign, currDoc.DocumentCulture);
                                        }
                                    }

                                    // Redirect user
                                    if (String.IsNullOrEmpty(returnUrl))
                                    {
                                        returnUrl = URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, CONFIRMATION_URLPARAMETER);
                                    }

                                    URLHelper.Redirect(returnUrl);
                                }
                                // Otherwise is user disabled
                                else
                                {
                                    lblError.Text = GetString("membership.userdisabled");
                                    lblError.Visible = true;
                                }
                            }
                            // Claimed Facebook ID not found  = save new user
                            else
                            {
                                // Check whether additional user info page is set
                                string additionalInfoPage = SettingsKeyProvider.GetStringValue(currentSiteName + ".CMSRequiredFacebookPage").Trim();

                                // No page set, user can be created
                                if (String.IsNullOrEmpty(additionalInfoPage))
                                {
                                    // Register new user
                                    string error = null;
                                    ui = UserInfoProvider.AuthenticateFacebookConnectUser(facebookUserId, currentSiteName, false, true, ref error);

                                    // If user was found or successfuly created
                                    if (ui != null)
                                    {
                                        // If user is enabled
                                        if (ui.Enabled)
                                        {
                                            // Create authentification cookie
                                            UserInfoProvider.SetAuthCookieWithUserData(ui.UserName, true, Session.Timeout, new string[] { "facebooklogon" });
                                            // Log activity
                                            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(currentSiteName) && ActivitySettingsHelper.UserLoginEnabled(currentSiteName))
                                            {
                                                int contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                                                ActivityLogHelper.UpdateContactLastLogon(contactId);
                                                if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                                                {
                                                    TreeNode currDoc = CMSContext.CurrentDocument;
                                                    ActivityLogProvider.LogLoginActivity(contactId,
                                                        ui, URLHelper.CurrentRelativePath, currDoc.NodeID, currentSiteName, CMSContext.Campaign, currDoc.DocumentCulture);
                                                }
                                            }
                                        }

                                        // Send registration e-mails
                                        // E-mail confirmation is not required as user already provided confirmation by successful login using Facebook connect
                                        UserInfoProvider.SendRegistrationEmails(ui, null, null, false, false);

                                        // Notify administrator
                                        if (this.NotifyAdministrator && !String.IsNullOrEmpty(this.FromAddress) && !String.IsNullOrEmpty(this.ToAddress))
                                        {
                                            UserInfoProvider.NotifyAdministrator(ui, this.FromAddress, this.ToAddress);
                                        }

                                        // Log registration into analytics
                                        UserInfoProvider.TrackUserRegistration(this.TrackConversionName, this.ConversionValue, currentSiteName, ui);

                                        // Log activity
                                        if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(currentSiteName) && ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui)
                                            && ActivitySettingsHelper.UserRegistrationEnabled(currentSiteName))
                                        {
                                            int contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                                            ModuleCommands.OnlineMarketingUpdateContactFromExternalData(ui, contactId);
                                            TreeNode currDoc = CMSContext.CurrentDocument;
                                            ActivityLogProvider.LogRegistrationActivity(contactId,
                                            ui, URLHelper.CurrentRelativePath, currDoc.NodeID, currentSiteName, CMSContext.Campaign, currDoc.DocumentCulture);
                                        }
                                    }

                                    // Redirect when authentication was succesfull
                                    if (String.IsNullOrEmpty(error))
                                    {
                                        if (!String.IsNullOrEmpty(returnUrl))
                                        {
                                            URLHelper.Redirect(URLHelper.GetAbsoluteUrl(returnUrl));
                                        }
                                        else
                                        {
                                            URLHelper.Redirect(URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, CONFIRMATION_URLPARAMETER));
                                        }
                                    }
                                    // Display error otherwise
                                    else
                                    {
                                        lblError.Text = error;
                                        lblError.Visible = true;
                                    }
                                }
                                // Additional information page is set
                                else
                                {
                                    // Store user object in session for additional info page
                                    SessionHelper.SetValue(SESSION_NAME_USERDATA, facebookUserId);

                                    // Redirect to additional info page
                                    string targetURL = URLHelper.GetAbsoluteUrl(additionalInfoPage);

                                    if (!String.IsNullOrEmpty(returnUrl))
                                    {
                                        // Add return URL to parameter
                                        targetURL = URLHelper.AddParameterToUrl(targetURL, "returnurl", HttpUtility.UrlEncode(returnUrl));
                                    }
                                    URLHelper.Redirect(targetURL);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Show warning message in "Design mode"
                this.Visible = DisplayMessage();
            }
        }
    }


    /// <summary>
    /// Displays warning message in "Design mode".
    /// </summary>
    private bool DisplayMessage()
    {
        // Error label is displayed in Design mode when Facebook Connect is disabled
        if (CMSContext.ViewMode == ViewModeEnum.Design)
        {
            StringBuilder parameter = new StringBuilder();
            parameter.Append(GetString("header.sitemanager") + " -> ");
            parameter.Append(GetString("settingscategory.cmssettings") + " -> ");
            parameter.Append(GetString("settingscategory.cmsmembership") + " -> ");
            parameter.Append(GetString("settingscategory.cmsmembershipauthentication") + " -> ");
            parameter.Append(GetString("settingscategory.cmsfacebookconnect"));
            if (CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                // Make it link for SiteManager Admin
                parameter.Insert(0, "<a href=\"" + URLHelper.GetAbsoluteUrl("~/CMSSiteManager/default.aspx?section=settings") + "\" target=\"_top\">");
                parameter.Append("</a>");
            }

            lblError.Text = String.Format(GetString("mem.facebook.disabled"), parameter.ToString());
            lblError.Visible = true;
            plcFBButton.Visible = false;
            imgSignOut.Visible = false;
            lnkSignOutImageBtn.Visible = false;
            lnkSignOutLink.Visible = false;
        }

        return lblError.Visible;
    }

    #endregion
}
