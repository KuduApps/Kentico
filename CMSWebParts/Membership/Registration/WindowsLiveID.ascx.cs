using System;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.MembershipProvider;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Membership_Registration_WindowsLiveID : CMSAbstractWebPart
{
    private static string loginPage = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSecuredAreasLogonPage");

    #region "Public properties"

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
    /// Gets or sets sign in button image URL.
    /// </summary>
    public string SignInImageURL
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SignInImageURL"), GetImageUrl("Others/LiveID/signin.gif"));
        }
        set
        {
            SetValue("SignInImageURL", value);
        }
    }


    /// <summary>
    /// Gets or sets sign out button image URL.
    /// </summary>
    public string SignOutImageURL
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SignOutImageURL"), GetImageUrl("Others/LiveID/signout.gif"));
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
            return ValidationHelper.GetString(GetValue("TrackConversionName"), SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSLiveIDConversionName"));
        }
        set
        {
            if ((value != null) && (value.Length > 400))
            {
                value = value.Substring(0, 400);
            }
            SetValue("TrackConversionName", value);
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
            SetValue("ConversionValue", value);
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
            // Do nothing
        }
        else
        {
            if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSEnableWindowsLiveID"))
            {
                string siteName = CMSContext.CurrentSiteName;
                if (!string.IsNullOrEmpty(siteName))
                {
                    // Get LiveID settings
                    string appId = SettingsKeyProvider.GetStringValue(siteName + ".CMSApplicationID");
                    string secret = SettingsKeyProvider.GetStringValue(siteName + ".CMSApplicationSecret");

                    if (!WindowsLiveLogin.UseServerSideAuthorization)
                    {
                        // Add windows live ID script
                        ScriptHelper.RegisterClientScriptInclude(Page, typeof(string), "WLScript", "https://js.live.net/v5.0/wl.js");

                        // Add login functions 
                        String loginLiveIDClientScript = @"

                            function signUserIn() {
                                var scopesArr = ['wl.signin'];
                                WL.login({ scope: scopesArr });
                            }
                    
                            function refreshLiveID(param)
                            {
                                " + ControlsHelper.GetPostBackEventReference(btnHidden, "#").Replace("'#'", "param") + @" 
                            }                                       
                        ";

                        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ClientInitLiveIDScript", ScriptHelper.GetScript(loginLiveIDClientScript));
                    }

                    // Check valid Windows LiveID parameters
                    if ((appId == string.Empty) || (secret == string.Empty))
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("liveid.incorrectsettings");
                        return;
                    }

                    WindowsLiveLogin wll = new WindowsLiveLogin(appId, secret);

                    // If user is already authenticated 
                    if (CMSContext.CurrentUser.IsAuthenticated())
                    {
                        // If signout should be visible and user has LiveID registered
                        if ((ShowSignOut) && (!String.IsNullOrEmpty(CMSContext.CurrentUser.UserSettings.WindowsLiveID)))
                        {
                            // Get data from auth cookie 
                            string[] userData = UserInfoProvider.GetUserDataFromAuthCookie();

                            // Check if user has truly logged in by LiveID
                            if ((userData != null) && (Array.IndexOf(userData, "liveidlogin") >= 0))
                            {
                                string navUrl = wll.GetLogoutUrl();

                                // If text is set use text/button link
                                if (!string.IsNullOrEmpty(SignOutText))
                                {
                                    // Button link
                                    if (ShowAsButton)
                                    {
                                        btnSignOut.CommandArgument = navUrl;
                                        btnSignOut.Text = SignOutText;
                                        btnSignOut.Visible = true;
                                    }
                                    // Text link
                                    else
                                    {
                                        btnSignOutLink.CommandArgument = navUrl;
                                        btnSignOutLink.Text = SignOutText;
                                        btnSignOutLink.Visible = true;
                                    }
                                }
                                // Image link
                                else
                                {
                                    btnSignOutImage.CommandArgument = navUrl;
                                    btnSignOutImage.ImageUrl = ResolveUrl(SignOutImageURL);
                                    btnSignOutImage.Visible = true;
                                    btnSignOut.Text = GetString("webparts_membership_signoutbutton.signout");
                                }
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

                        // Create return URL
                        string returnUrl = QueryHelper.GetText("returnurl", "");
                        returnUrl = (returnUrl == String.Empty) ? URLHelper.CurrentURL : returnUrl;

                        // Create parameters for LiveID request URL
                        String[] parameters = new String[3];
                        parameters[0] = returnUrl;
                        parameters[1] = TrackConversionName;
                        parameters[2] = ConversionValue.ToString();
                        SessionHelper.SetValue("LiveIDInformtion", parameters);

                        returnUrl = wll.GetLoginUrl();

                        // Get App ID
                        appId = SettingsKeyProvider.GetStringValue(siteName + ".CMSApplicationID");

                        // Create full LiveID request URL                        
                        string navUrl = "https://oauth.live.com/authorize?&client_id=" + appId + "&redirect=true&scope=wl.signin&response_type=code&redirect_uri=" + HttpUtility.UrlEncode(returnUrl);

                        // If text is set use text/button link
                        if (!string.IsNullOrEmpty(SignInText))
                        {
                            // Button link
                            if (ShowAsButton)
                            {
                                AssignButtonControl(navUrl, returnUrl, appId);
                                btnSignIn.Text = SignInText;
                            }
                            // Text link
                            else
                            {
                                AssignHyperlinkControl(navUrl, returnUrl, appId);
                                lnkSignIn.Text = SignInText;
                            }
                        }
                        // Image link
                        else
                        {
                            AssignHyperlinkControl(navUrl, returnUrl, appId);
                            lnkSignIn.ImageUrl = ResolveUrl(SignInImageURL);
                            lnkSignIn.Text = GetString("webparts_membership_signoutbutton.signin");
                        }
                    }
                }
            }
            else
            {
                // Error label is displayed in Design mode when Windows Live ID is disabled
                if (CMSContext.ViewMode == ViewModeEnum.Design)
                {
                    StringBuilder parameter = new StringBuilder();
                    parameter.Append(GetString("header.sitemanager") + " -> ");
                    parameter.Append(GetString("settingscategory.cmssettings") + " -> ");
                    parameter.Append(GetString("settingscategory.cmsmembership") + " -> ");
                    parameter.Append(GetString("settingscategory.cmsmembershipauthentication") + " -> ");
                    parameter.Append(GetString("settingscategory.cmswindowsliveid"));
                    if (CMSContext.CurrentUser.UserSiteManagerAdmin)
                    {
                        // Make it link for SiteManager Admin
                        parameter.Insert(0, "<a href=\"" + URLHelper.GetAbsoluteUrl("~/CMSSiteManager/default.aspx?section=settings") + "\" target=\"_top\">");
                        parameter.Append("</a>");
                    }

                    lblError.Text = String.Format(GetString("mem.liveid.disabled"), parameter.ToString());
                    lblError.Visible = true;
                }
                else
                {
                    Visible = false;
                }
            }
        }
    }


    /// <summary>
    /// Assign attributes (url) to hyperlink sign in control
    /// </summary>
    /// <param name="navUrl">Navigation url (login.liveid....)</param>
    /// <param name="retUrl">Return url (usually CMSPages/LiveIDLogin.aspx)</param>
    /// <param name="appID">Application ID</param>
    private void AssignHyperlinkControl(String navUrl, String retUrl, String appID)
    {
        if (!WindowsLiveLogin.UseServerSideAuthorization)
        {
            AddOnClickAttribute(lnkSignIn, appID, retUrl);
        }
        else
        {
            lnkSignIn.NavigateUrl = navUrl;
        }
        lnkSignIn.Visible = true;
    }


    /// <summary>
    /// Assign attributes (url) to button sign in control
    /// </summary>
    /// <param name="navUrl">Navigation url (login.liveid....)</param>
    /// <param name="retUrl">Return url (usually CMSPages/LiveIDLogin.aspx)</param>
    /// <param name="appID">Application ID</param>
    private void AssignButtonControl(String navUrl, String retUrl, String appID)
    {
        if (!WindowsLiveLogin.UseServerSideAuthorization)
        {
            AddOnClickAttribute(btnSignIn, appID, retUrl);
        }
        else
        {
            btnSignIn.CommandArgument = navUrl;
        }
        btnSignIn.Visible = true;
    }


    /// <summary>
    /// Adds on click attribute to control (used for client login)
    /// </summary>
    /// <param name="ctrl">Web control</param>
    /// <param name="appID">Application ID</param>
    /// <param name="retUrl">Return url (usually CMSPages/LiveIDLogin.aspx)</param>
    private void AddOnClickAttribute(WebControl ctrl, String appID, string retUrl)
    {
        ctrl.Attributes["onClick"] = @"
                                            WL.init({
                                                client_id: '" + appID + @"',
                                                redirect_uri: '" + retUrl + @"',
                                                response_type: ""token""
                                            });
                                            signUserIn();";
    }


    /// <summary>
    /// SignOut handler.
    /// </summary>
    protected void btnSignOut_Click(object sender, CommandEventArgs e)
    {
        if (StopProcessing)
        {
            // Do not process
        }
        else
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                // Sign out from CMS
                FormsAuthentication.SignOut();
                CMSContext.CurrentUser = null;
                CMSContext.ClearShoppingCart();

                Response.Cache.SetNoStore();

                if (Session != null)
                {
                    // Store info about logout request, for validation logout request
                    SessionHelper.SetValue("liveidlogout", DateTime.Now);
                }
            }

            // Clear session parameter
            Session.Remove("liveidlogout");

            // Redirect to LiveID logout
            URLHelper.Redirect(e.CommandArgument.ToString());
        }
    }


    /// <summary>
    /// SignIn handler.
    /// </summary>
    protected void btnSignIn_Click(object sender, CommandEventArgs e)
    {
        if (StopProcessing)
        {
            // Do not process
        }
        else
        {
            // Redirect to sign in to Windows Live
            URLHelper.Redirect(e.CommandArgument.ToString());
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }

    #endregion

    /// <summary>
    /// Manages some actions, after user authorized and page postbacked
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Args</param>
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        string arg = Request["__EVENTARGUMENT"];
        switch (arg.ToLower())
        {
            case "redirecttoadditionalpage":

                // Get additional page
                string additionalInfoPage = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSLiveIDRequiredUserDataPage");

                if (!String.IsNullOrEmpty(additionalInfoPage))
                {
                    // Redirect to additional info page
                    URLHelper.Redirect(URLHelper.ResolveUrl(additionalInfoPage));
                }

                break;

            case "clearcookieandredirect":
                WindowsLiveLogin.ClearCookieAndRedirect(loginPage);
                break;
        }
    }
}