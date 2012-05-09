using System;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Text;
using System.Web;
using System.Xml;

using CMS.PortalControls;
using CMS.SettingsProvider;
using CMS.MembershipProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Membership_LinkedIn_LinkedInLogon : CMSAbstractWebPart
{
    #region "Variables"

    private LinkedInHelper linkedInHelper = null;

    #endregion


    #region "Constants"

    protected const string FILES_LOCATION = "~/CMSWebparts/Membership/LinkedIn/LinkedInLogon_files/";
    protected const string SESSION_NAME_USERDATA = "LinkedInUserData";

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if birth date is required in registration process.
    /// </summary>
    public bool RequireBirthDate
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("BirthDate"), true);
        }
        set
        {
            SetValue("BirthDate", value);
        }
    }


    /// <summary>
    /// Indicates if first name is required in registration process.
    /// </summary>
    public bool RequireFirstName
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("FirstName"), true);
        }
        set
        {
            SetValue("FirstName", value);
        }
    }


    /// <summary>
    /// Indicates if last name is required in registration process.
    /// </summary>
    public bool RequireLastName
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("LastName"), true);
        }
        set
        {
            SetValue("LastName", value);
        }
    }


    /// <summary>
    /// Gets or sets sign in button image URL.
    /// </summary>
    public string SignInImageURL
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SignInImageURL"), GetImageUrl(FILES_LOCATION + "signin.png"));
        }
        set
        {
            SetValue("SignInImageURL", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether to show sign out button.
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
    /// Gets or sets the value that buttons will be used instead of link buttons.
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
            return DataHelper.GetNotEmpty(GetValue("SignOutImageURL"), GetImageUrl(FILES_LOCATION + "signout.png"));
        }
        set
        {
            SetValue("SignOutImageURL", value);
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


    #region "Page events"

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
            // Check renamed DLL library
            if (CMSOpenIDHelper.CheckOpenIdDLL())
            {
                // Check if LinkedIn module is enabled
                if (LinkedInHelper.LinkedInIsAvailable(CMSContext.CurrentSiteName))
                {
                    DisplayButtons();
                    linkedInHelper = new LinkedInHelper();
                    CheckStatus();
                }
                else
                {
                    // Error label is displayed in Design mode when LinkedIn is disabled
                    if (CMSContext.ViewMode == ViewModeEnum.Design)
                    {
                        StringBuilder parameter = new StringBuilder();
                        parameter.Append(GetString("header.sitemanager") + " -> ");
                        parameter.Append(GetString("settingscategory.cmssettings") + " -> ");
                        parameter.Append(GetString("settingscategory.cmsmembership") + " -> ");
                        parameter.Append(GetString("settingscategory.cmsmembershipauthentication") + " -> ");
                        parameter.Append(GetString("settingscategory.cmslinkedin"));
                        if (CMSContext.CurrentUser.UserSiteManagerAdmin)
                        {
                            // Make it link for SiteManager Admin
                            parameter.Insert(0, "<a href=\"" + URLHelper.GetAbsoluteUrl("~/CMSSiteManager/default.aspx?section=settings") + "\" target=\"_top\">");
                            parameter.Append("</a>");
                        }

                        lblError.Text = String.Format(GetString("mem.linkedin.disabled"), parameter.ToString());
                        lblError.Visible = true;
                    }
                    else
                    {
                        this.Visible = false;
                    }
                }
            }
            // Error label is displayed in Design mode when LinkedIn library is not loaded
            else
            {
                lblError.ResourceString = "mem.openid.library";
                lblError.Visible = true;
            }
        }
        else
        {
            this.Visible = false;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Checks status of current user.
    /// </summary>
    protected void CheckStatus()
    {
        // Get current site name
        string siteName = CMSContext.CurrentSiteName;
        string error = null;

        // Check return URL
        string returnUrl = QueryHelper.GetString("returnurl", null);
        returnUrl = HttpUtility.UrlDecode(returnUrl);

        // Get current URL
        string currentUrl = URLHelper.CurrentURL;
        currentUrl = URLHelper.RemoveParameterFromUrl(currentUrl, "oauth_token");
        currentUrl = URLHelper.RemoveParameterFromUrl(currentUrl, "oauth_verifier");

        // Get LinkedIn response status
        switch (linkedInHelper.CheckStatus(RequireFirstName, RequireLastName, RequireBirthDate, null))
        {
            // User is authenticated
            case CMSOpenIDHelper.RESPONSE_AUTHENTICATED:
                // LinkedIn profile Id not found  = save new user
                if (UserInfoProvider.GetUserInfoByLinkedInID(linkedInHelper.MemberId) == null)
                {
                    string additionalInfoPage = SettingsKeyProvider.GetStringValue(siteName + ".CMSRequiredLinkedInPage").Trim();

                    // No page set, user can be created
                    if (String.IsNullOrEmpty(additionalInfoPage))
                    {
                        // Register new user
                        UserInfo ui = UserInfoProvider.AuthenticateLinkedInUser(linkedInHelper.MemberId, linkedInHelper.FirstName, linkedInHelper.LastName, siteName, true, true, ref error);

                        // If user was successfuly created
                        if (ui != null)
                        {

                            if (linkedInHelper.BirthDate != DateTimeHelper.ZERO_TIME)
                            {
                                ui.UserSettings.UserDateOfBirth = linkedInHelper.BirthDate;
                            }

                            UserInfoProvider.SetUserInfo(ui);

                            // If user is enabled
                            if (ui.Enabled)
                            {
                                // Create autentification cookie
                                UserInfoProvider.SetAuthCookieWithUserData(ui.UserName, true, Session.Timeout, new string[] { "linkedinlogin" });
                                // Log activity
                                if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.UserLoginEnabled(siteName))
                                {
                                    int contactId = ModuleCommands.OnlineMarketingGetUserLoginContactID(ui);
                                    ActivityLogHelper.UpdateContactLastLogon(contactId);
                                    if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
                                    {
                                        TreeNode currentDoc = CMSContext.CurrentDocument;
                                        ActivityLogProvider.LogLoginActivity(contactId, ui, URLHelper.CurrentRelativePath,
                                            (currentDoc != null ? currentDoc.NodeID : 0), siteName, CMSContext.Campaign, (currentDoc != null ? currentDoc.DocumentCulture : null));
                                    }
                                }
                            }

                            // Notify administrator
                            if (this.NotifyAdministrator && !String.IsNullOrEmpty(this.FromAddress) && !String.IsNullOrEmpty(this.ToAddress))
                            {
                                UserInfoProvider.NotifyAdministrator(ui, this.FromAddress, this.ToAddress);
                            }

                            // Send registration e-mails
                            // E-mail confirmation is not required as user already provided confirmation by successful login using OpenID
                            UserInfoProvider.SendRegistrationEmails(ui, null, null, false, false);

                            // Log registration into analytics
                            UserInfoProvider.TrackUserRegistration(this.TrackConversionName, this.ConversionValue, siteName, ui);

                            // Log activity
                            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser)
                                && ActivitySettingsHelper.UserRegistrationEnabled(siteName))
                            {
                                int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
                                ModuleCommands.OnlineMarketingUpdateContactFromExternalData(ui, contactId);
                                TreeNode currentDoc = CMSContext.CurrentDocument;
                                ActivityLogProvider.LogRegistrationActivity(contactId,
                                ui, URLHelper.CurrentRelativePath, currentDoc.NodeID, siteName, CMSContext.Campaign, currentDoc.DocumentCulture);
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
                                URLHelper.Redirect(currentUrl);
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
                        // Store user object in session for additional use
                        SessionHelper.SetValue(SESSION_NAME_USERDATA, linkedInHelper.LinkedInResponse);

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
                // LinkedIn profile id is in DB
                else
                {
                    // Login existing user
                    UserInfo ui = UserInfoProvider.AuthenticateLinkedInUser(linkedInHelper.MemberId, linkedInHelper.FirstName, linkedInHelper.LastName, siteName, false, true, ref error);

                    if ((ui != null) && (ui.Enabled))
                    {
                        // Create autentification cookie
                        UserInfoProvider.SetAuthCookieWithUserData(ui.UserName, true, Session.Timeout, new string[] { "linkedinlogin" });

                        // Log activity
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

                        // Redirect user
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            URLHelper.Redirect(URLHelper.GetAbsoluteUrl(returnUrl));
                        }
                        else
                        {
                            URLHelper.Redirect(currentUrl);
                        }
                    }
                    // Display error which occured during authentication process
                    else if (!String.IsNullOrEmpty(error))
                    {
                        lblError.Text = error;
                        lblError.Visible = true;
                    }
                    // Otherwise is user disabled
                    else
                    {
                        lblError.Text = GetString("membership.userdisabled");
                        lblError.Visible = true;
                    }
                }
                break;

            // No authentication, do nothing
            case LinkedInHelper.RESPONSE_NOTAUTHENTICATED:
                break;
        }
    }


    /// <summary>
    /// Displays buttons depending on web part settings.
    /// </summary>
    protected void DisplayButtons()
    {
        // If user is already authenticated 
        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            if (this.ShowSignOut)
            {
                // If text is set use text/button link
                if (!string.IsNullOrEmpty(SignOutText))
                {
                    // Button link
                    if (ShowAsButton)
                    {
                        btnSignOut.Text = this.SignOutText;
                        btnSignOut.Visible = true;
                    }
                    // Text link
                    else
                    {
                        btnSignOutLink.Text = this.SignOutText;
                        btnSignOutLink.Visible = true;
                    }
                }
                // Image link
                else
                {
                    btnSignOutImage.ImageUrl = ResolveUrl(SignOutImageURL);
                    btnSignOutImage.Visible = true;
                    btnSignOutImage.ToolTip = GetString("webparts_membership_signoutbutton.signout");
                    btnSignOut.Text = GetString("webparts_membership_signoutbutton.signout");
                }
            }
        }
        else
        {
            // If text is set use text/button link
            if (!string.IsNullOrEmpty(SignInText))
            {
                // Button link
                if (ShowAsButton)
                {
                    btnSignIn.Text = this.SignInText;
                    btnSignIn.Visible = true;
                }
                // Text link
                else
                {
                    btnSignInLink.Text = this.SignInText;
                    btnSignInLink.Visible = true;
                }
            }
            // Image link
            else
            {
                btnSignInImage.ImageUrl = ResolveUrl(SignInImageURL);
                btnSignInImage.Visible = true;
                btnSignInImage.ToolTip = GetString("webparts_membership_signoutbutton.signin");
                btnSignIn.Text = GetString("webparts_membership_signoutbutton.signin");
            }
        }
    }


    /// <summary>
    /// Sign in button event.
    /// </summary>
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        linkedInHelper.SendRequest();
    }


    /// <summary>
    /// Sign out button event.
    /// </summary>
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            // Sign out from CMS
            FormsAuthentication.SignOut();
            CMSContext.CurrentUser = null;
            CMSContext.ClearShoppingCart();

            Response.Cache.SetNoStore();

            // Clear used session
            SessionHelper.Remove(SESSION_NAME_USERDATA);

            // Redirect to return URL
            string returnUrl = QueryHelper.GetString("returnurl", URLHelper.CurrentURL);
            URLHelper.Redirect(URLHelper.GetAbsoluteUrl(HttpUtility.UrlDecode(returnUrl)));
        }
    }

    #endregion
}