using System;
using System.Web.UI;
using System.Web;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.MembershipProvider;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.EventLog;
using CMS.ExtendedControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSDesk_Header : CMSDeskPage
{
    #region "Variables"

    private string section = null;
    private bool exploreTreePermissionMissing = false;

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this["TabControl"] = BasicTabControlHeader;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CheckUserImpersonate();

        // Facebook Connect sign out
        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            if (QueryHelper.GetInteger("logout", 0) > 0)
            {
                btnSignOut_Click(this, EventArgs.Empty);
                return;
            }
        }

        InitializeVersion();

        // Make 'Site manager' link visible for global administrators
        CurrentUserInfo ui = CMSContext.CurrentUser;
        if ((ui != null) && (ui.UserSettings != null))
        {
            lnkSiteManager.Visible = ui.UserSiteManagerAdmin;
        }

        // Site selector settings
        siteSelector.DropDownSingleSelect.CssClass = "HeaderSiteDrop";
        siteSelector.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline;
        siteSelector.AllowAll = false;
        siteSelector.UniSelector.OnSelectionChanged += SiteSelector_OnSelectionChanged;
        siteSelector.UniSelector.OnBeforeClientChanged = "if (!CheckChanges()) { this.value = this.originalValue; return false; }";
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.OnlyRunningSites = true;

        if (!RequestHelper.IsPostBack())
        {
            siteSelector.Value = CMSContext.CurrentSiteID;
        }

        // Show only assigned sites for not global admins
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            siteSelector.UserId = CMSContext.CurrentUser.UserID;
        }

        section = QueryHelper.GetString("section", string.Empty).ToLower();

        lblUser.Text = GetString("Header.User");
        lblUserInfo.Text = HTMLHelper.HTMLEncode(CMSContext.CurrentUser.FullName);

        lnkLiveSite.Text = ResHelper.GetString("Header.LiveSite");

        lnkTestingMode.Text = GetString("cmstesting.headerlink");
        lnkTestingMode.Visible = SettingsKeyProvider.TestingMode;
        lnkTestingMode.NavigateUrl = "~/CMSPages/GetTestingModeReport.aspx";

        // Initialize variables from query string 
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        string culture = QueryHelper.GetText("culture", null);
        string url = "~";

        // Set url to node from which CMSDesk was opened
        if ((nodeId > 0) && !String.IsNullOrEmpty(culture))
        {
            TreeProvider treeProvider = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = treeProvider.SelectSingleNode(nodeId, culture, false, false);
            if (node != null)
            {
                url = CMSContext.GetUrl(node.NodeAliasPath, node.DocumentUrlPath);
            }
        }
        // Resolve Url and add live site view mode
        url = ResolveUrl(url);
        url = URLHelper.AddParameterToUrl(url, "viewmode", "livesite");

        lnkLiveSite.NavigateUrl = url;
        lnkLiveSite.Target = "_parent";

        lnkSiteManager.Text = GetString("Header.SiteManager");
        lnkSiteManager.NavigateUrl = "~/CMSSiteManager/default.aspx";
        lnkSiteManager.Target = "_parent";

        lnkSiteManagerLogo.NavigateUrl = "~/CMSDesk/default.aspx";
        lnkSiteManagerLogo.Target = "_parent";

        elemLinks.RedirectURL = URLHelper.CurrentURL;

        BasicTabControlHeader.OnTabCreated += tabElem_OnTabCreated;
        BasicTabControlHeader.UrlTarget = "cmsdesktop";

        BasicTabControlHeader.QueryParameterName = "section";

        if (RequestHelper.IsWindowsAuthentication())
        {
            pnlSignOut.Visible = false;
            pnlRight.CssClass += " HeaderWithoutSignOut";
        }
        else
        {
            pnlSignOut.BackImageUrl = GetImageUrl("Design/Buttons/SignOutButton.png");
            lblSignOut.Text = GetString("signoutbutton.signout");

            // Init Facebook Connect and join logout script to sign out button
            string logoutScript = FacebookConnectHelper.FacebookConnectInitForSignOut(CMSContext.CurrentSiteName, ltlFBConnectScript);
            if (!String.IsNullOrEmpty(logoutScript))
            {
                // If Facebook Connect initialized include 'CheckChanges()' to logout script
                logoutScript = "if (CheckChanges()) { " + logoutScript + " } return false; ";
            }
            else
            {
                // If Facebook Connect not initialized just return 'CheckChanges()' script
                logoutScript = "return CheckChanges();";
            }
            lnkSignOut.OnClientClick = logoutScript;
        }

        // Displays windows azure and EMS icons
        if (AzureHelper.IsRunningOnAzure && SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSShowAzureLogo"))
        {
            imgWindowsAzure.Visible = true;
            imgWindowsAzure.ImageUrl = GetImageUrl("General/IconWindowsAzure.png");
            pnlExtraIcons.Visible = true;
        }
        if (LicenseHelper.CurrentEdition == ProductEditionEnum.EnterpriseMarketingSolution)
        {
            imgEnterpriseSolution.Visible = true;
            imgEnterpriseSolution.ImageUrl = GetImageUrl("General/IconEnterpriseSolution.png");
            pnlExtraIcons.Visible = true;
        }
    }


    private void InitializeVersion()
    {
        string version = "v";

        if (SettingsKeyProvider.DevelopmentMode)
        {
            version += CMSContext.FullSystemSuffixVersion;
        }
        else
        {
            int hotFixVersion = ValidationHelper.GetInteger(CMSContext.HotfixVersion, 0);
            if (hotFixVersion > 0)
            {
                version += CMSContext.HotfixedSystemSuffixVersion;
                if (hotFixVersion > 22)
                {
                    // Add SP1 suffix with left-to-right mark (for RTL)
                    version += " (SP1)&lrm;";
                }
            }
            else
            {
                version += CMSContext.GeneralVersionSuffix;
            }
            lblVersion.ToolTip = CMSContext.FullSystemSuffixVersion.Trim();
        }
        lblVersion.Text = version.Trim();
    }


    /// <summary>
    /// Check for user impersonate.
    /// </summary>
    private void CheckUserImpersonate()
    {
        CurrentUserInfo user = CMSContext.CurrentUser;

        // Show impersonate button for global admin only
        if ((user.IsGlobalAdministrator) && !RequestHelper.IsWindowsAuthentication())
        {
            // Show users from current site only
            ucUsers.WhereCondition = "UserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID = " + CMSContext.CurrentSiteID + " AND (UserIsGlobalAdministrator = 0 OR UserSiteManagerDisabled != 0))";

            pnlUsers.Visible = true;

            // Set context menu for impersonate
            imgImpersonate.Attributes.Add("src", GetImageUrl("Design/Backgrounds/ArrowWhite.png"));

            menuCont.MenuControlPath = "~/CMSAdminControls/ContextMenus/UserImpersonateMenu.ascx";
            menuCont.MenuID = ClientID + "m_impersonate_context_menu";
            menuCont.ParentElementClientID = ClientID;
            menuCont.Parameter = "''";
            menuCont.RenderAsTag = HtmlTextWriterTag.A;
            menuCont.MouseButton = MouseButtonEnum.Left;
            menuCont.VerticalPosition = VerticalPositionEnum.Bottom;
            menuCont.HorizontalPosition = HorizontalPositionEnum.Left;

            // Script for open uniselector modal dialog
            string impersonateScript = "function userImpersonateShowDialog () {US_SelectionDialog_" + ucUsers.ClientID + "()}";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ImpersonateContextMenu", ScriptHelper.GetScript(impersonateScript));

            string userName = ValidationHelper.GetString(ucUsers.Value, String.Empty);
            if (userName != String.Empty)
            {
                // Get selected user info
                UserInfo iui = UserInfoProvider.GetUserInfo(userName);
                if (!iui.IsGlobalAdministrator)
                {
                    CMSContext.CurrentUser.UserImpersonate(iui);
                }
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        BasicTabControlHeader.DoTabSelection(exploreTreePermissionMissing, "CMS.Content", "ExploreTree");
    }


    protected string[] tabElem_OnTabCreated(UIElementInfo element, string[] parameters, int tabIndex)
    {
        // Ensure additional permissions to 'Content' tab
        if (element.ElementName.ToLower() == "content")
        {
            if (!IsUserAuthorizedPerContent())
            {
                exploreTreePermissionMissing = true;
                return null;
            }
        }
        else if (element.ElementName.ToLower() == "ecommerce")
        {
            if (!LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Ecommerce, ModuleEntry.ECOMMERCE))
            {
                return null;
            }
        }
        else if (element.ElementName.ToLower() == "onlinemarketing")
        {
            if (!ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING))
            {
                return null;
            }
        }

        return parameters;
    }


    protected void SiteSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Create url
        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            string domain = si.DomainName.TrimEnd('/');
            string url = "~" + VirtualContext.CurrentURLPrefix + "/CMSDesk/default.aspx";

            if (domain.Contains("/"))
            {
                // Resolve application path
                url = url.Substring(1);
            }

            url = URLHelper.GetAbsoluteUrl(url, domain, null, null);

            // Check if single sign-on is turned on
            if (SettingsKeyProvider.GetBoolValue("CMSAutomaticallySignInUser"))
            {
                url = UserInfoProvider.GetUserAuthenticationUrl(CMSContext.CurrentUser, url);
            }
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "selectSite", ScriptHelper.GetScript("SiteRedirect('" + url + "');"));
        }
    }


    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        // Usual sign out
        string signOutUrl = URLHelper.ApplicationPath.TrimEnd('/') + "/default.aspx";

        // LiveID sign out URL is set if this LiveID session
        SignOut(ref signOutUrl);

        ltlScript.Text += ScriptHelper.GetScript("parent.location.replace('" + signOutUrl + "');");
    }
}
