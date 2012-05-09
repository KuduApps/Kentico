using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;

using CMS.MembershipProvider;
using CMS.PortalControls;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.PortalEngine;

public partial class CMSWebParts_Membership_Logon_SignOutButton : CMSAbstractWebPart
{

    #region "Constants"

    protected const string SESSION_NAME_USERDATA = "facebookid";

    #endregion


    #region "Variables"

    private string mSignOutText = ResHelper.LocalizeString("{$Webparts_Membership_SignOutButton.SignOut$}");
    private string mSignInText = ResHelper.LocalizeString("{$Webparts_Membership_SignOutButton.SignIn$}");

    private bool mShowOnlyWhenAuthenticated = true;
    private bool mShowAsLink = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the sign out text.
    /// </summary>
    public string SignOutText
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SignOutText"), mSignOutText);
        }
        set
        {
            SetValue("SignOutText", value);
            mSignOutText = value;
        }
    }


    /// <summary>
    /// Gets or sets the sign in text.
    /// </summary>
    public string SignInText
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SignInText"), mSignInText);
        }
        set
        {
            SetValue("SignInText", value);
            mSignInText = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the webpart is shown as button or as text link.
    /// </summary>
    public bool ShowAsLink
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAsLink"), mShowAsLink);
        }
        set
        {
            SetValue("ShowAsLink", value);
            mShowAsLink = value;
        }
    }


    /// <summary>
    /// Gets or sets the URL where user is redirected after sign out.
    /// </summary>
    public string RedirectToUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("RedirectToURL"), URLRewriter.CurrentURL);
        }
        set
        {
            SetValue("RedirectToURL", value);
        }
    }


    /// <summary>
    /// Gets or sets the path where user is redirected before sign in.
    /// </summary>
    public string SignInUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SignInPageUrl"), "");
        }
        set
        {
            SetValue("SignInPageUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets the path where user is redirected after sign in.
    /// </summary>
    public string ReturnPath
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ReturnPath"), "");
        }
        set
        {
            SetValue("ReturnPath", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether webpart is shown only when the user is authenticated.
    /// </summary>
    public bool ShowOnlyWhenAuthenticated
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowOnlyWhenAuthenticated"), mShowOnlyWhenAuthenticated);
        }
        set
        {
            SetValue("ShowOnlyWhenAuthenticated", value);
            mShowOnlyWhenAuthenticated = value;
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
            btnSignOut.SkinID = value;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.Visible = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do not process
        }
        else
        {
            // Facebook Connect sign out
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                if (QueryHelper.GetInteger("logout", 0) > 0)
                {
                    // Sign out from CMS

                    // Remove Facebook user object from session
                    SessionHelper.Remove(SESSION_NAME_USERDATA);

                    FormsAuthentication.SignOut();
                    CMSContext.ClearShoppingCart();
                    CMSContext.CurrentUser = null;
                    Response.Cache.SetNoStore();
                    URLHelper.Redirect(URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, "logout"));
                    return;
                }
            }

            // Show only desired button
            btnSignOut.Visible = !ShowAsLink;
            btnSignOutLink.Visible = ShowAsLink;

            if (!CMSContext.CurrentUser.IsPublic())
            {
                // Hide for windows authentication
                if (RequestHelper.IsWindowsAuthentication())
                {
                    Visible = false;
                }
                else
                {
                    // Set signout text
                    btnSignOutLink.Text = SignOutText;
                    btnSignOut.Text = SignOutText;
                }
            }
            else
            {
                // Set signin text
                btnSignOutLink.Text = SignInText;
                btnSignOut.Text = SignInText;
            }

            // Facebook Connect initialization
            btnSignOut.OnClientClick = FacebookConnectHelper.FacebookConnectInitForSignOut(CMSContext.CurrentSiteName, ltlScript);
        }

        if (!StandAlone && (PageCycle < PageCycleEnum.Initialized) && (ValidationHelper.GetString(Page.StyleSheetTheme, "") == ""))
        {
            btnSignOut.SkinID = SkinID;
        }


        // if user is not authenticated and ShowOnlyWhenAuthenticated is set
        if (CMSContext.CurrentUser.IsPublic() && ShowOnlyWhenAuthenticated)
        {
            Visible = false;
        }
    }


    /// <summary>
    /// SignOut handler.
    /// </summary>
    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do not process
        }
        else
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                FormsAuthentication.SignOut();
                CMSContext.ClearShoppingCart();

                string redirectUrl = RedirectToUrl;

                // If the user has registered Windows Live ID
                if (!String.IsNullOrEmpty(CMSContext.CurrentUser.UserSettings.WindowsLiveID))
                {
                    // Get data from auth cookie
                    string[] userData = UserInfoProvider.GetUserDataFromAuthCookie();

                    // If user has logged in using Windows Live ID, then sign him out from Live too
                    if ((userData != null) && (Array.IndexOf(userData, "liveidlogin") >= 0))
                    {
                        string siteName = CMSContext.CurrentSiteName;

                        // Get LiveID settings
                        string appId = SettingsKeyProvider.GetStringValue(siteName + ".CMSApplicationID");
                        string secret = SettingsKeyProvider.GetStringValue(siteName + ".CMSApplicationSecret");

                        // Check valid Windows LiveID parameters
                        if ((appId != string.Empty) && (secret != string.Empty))
                        {
                            WindowsLiveLogin wll = new WindowsLiveLogin(appId, secret);

                            // Store info about logout request, for validation logout request
                            SessionHelper.SetValue("liveidlogout", DateTime.Now);

                            // Redirect to Windows Live 
                            redirectUrl = wll.GetLogoutUrl();
                        }
                    }
                }

                CMSContext.ViewMode = ViewModeEnum.LiveSite;
                CMSContext.CurrentUser = null;

                Response.Cache.SetNoStore();
                URLHelper.Redirect(redirectUrl);
            }
            else
            {
                string returnUrl = null;
                string signInUrl = null;

                if (SignInUrl != "")
                {
                    signInUrl = ResolveUrl(CMSContext.GetUrl(CMSContext.ResolveCurrentPath(SignInUrl)));
                }
                else
                {
                    signInUrl = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSecuredAreasLogonPage");
                }

                if (ReturnPath != "")
                {
                    returnUrl = ResolveUrl(CMSContext.GetUrl(CMSContext.ResolveCurrentPath(ReturnPath)));
                }
                else
                {
                    returnUrl = URLHelper.CurrentURL;
                }

                if (signInUrl != "")
                {
                    // Prevent multiple returnUrl parameter
                    returnUrl = URLHelper.RemoveParameterFromUrl(returnUrl, "returnUrl");
                    URLHelper.Redirect(URLHelper.AddParameterToUrl(signInUrl, "returnurl", returnUrl));
                }
            }
        }
    }


    /// <summary>
    /// Applies given stylesheet skin.
    /// </summary>
    /// <param name="page">Page</param>
    public override void ApplyStyleSheetSkin(Page page)
    {
        btnSignOut.SkinID = SkinID;

        base.ApplyStyleSheetSkin(page);
    }


    /// <summary>
    /// PreInit handler.
    /// </summary>
    protected void CMSWebParts_Search_cmssearchboxl_PreInit(object sender, EventArgs e)
    {
        // Set SkinID property
        if (!StandAlone && (PageCycle < PageCycleEnum.Initialized))
        {
            btnSignOut.SkinID = SkinID;
        }
    }

    #endregion
}
