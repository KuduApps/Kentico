using System;
using System.Data;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.MembershipProvider;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSSiteManager_Header : SiteManagerPage
{
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

        lblUser.Text = GetString("Header.User");
        lblUserInfo.Text = HTMLHelper.HTMLEncode(CMSContext.CurrentUser.FullName);
        lnkCmsDesk.Text = GetString("Header.CMSDesk");
        lnkCmsDesk.NavigateUrl = "~/CMSDesk/default.aspx";
        lnkCmsDesk.Target = "_parent";

        lnkTestingMode.Text = GetString("cmstesting.headerlink");
        lnkTestingMode.Visible = SettingsKeyProvider.TestingMode;
        lnkTestingMode.NavigateUrl = "~/CMSPages/GetTestingModeReport.aspx";

        string url = "~/CMSSiteManager/default.aspx";
        lnkCmsDeskLogo.NavigateUrl = url;
        lnkCmsDeskLogo.Target = "_parent";

        elemLinks.RedirectURL = "~/CMSMessages/Redirect.aspx?frame=top&url=" + url;

        // Include 'Buy' tab if only trial and free licenses are present
        int buy = 1;
        if (!LicenseKeyInfoProvider.OnlyTrialLicenseKeys)
        {
            buy = 0;
        }

        string[,] tabs = new string[8 + buy, 4];
        tabs[0, 0] = GetString("general.sites");
        tabs[0, 2] = "sites/site_list.aspx";
        tabs[1, 0] = GetString("Header.Administration");
        tabs[1, 2] = "administration/default.aspx";
        tabs[2, 0] = GetString("Header.Settings");
        tabs[2, 2] = ResolveUrl("~/CMSModules/Settings/SiteManager/Default.aspx");
        tabs[3, 0] = GetString("Header.Development");
        tabs[3, 2] = "development/default.aspx";
        tabs[4, 0] = GetString("Header.Tools");
        tabs[4, 2] = "tools/default.aspx";
        tabs[5, 0] = GetString("Header.Dashboard");
        tabs[5, 2] = URLHelper.EnsureHashToQueryParameters("Dashboard.aspx?dashboardName=SiteManagerDashboard&templateName=Administratordashboard&{hash}");
        tabs[6, 0] = GetString("Header.Licenses");
        tabs[6, 2] = ResolveUrl("~/CMSModules/licenses/Pages/License_List.aspx");
        tabs[7, 0] = GetString("Header.Support");
        tabs[7, 2] = ResolveUrl("~/CMSModules/Support/Pages/default.aspx");

        // Add 'Buy' tab if needed
        if (buy > 0)
        {
            tabs[8, 0] = GetString("Header.Buy");
            tabs[8, 2] = BUY_PAGE;
        }

        BasicTabControlHeader.Tabs = tabs;

        string section = ValidationHelper.GetString(Request.QueryString["section"], "sites").ToLower();
        int selectedTab = 0;
        switch (section)
        {
            case "sites":
                selectedTab = 0;
                break;

            case "administration":
                selectedTab = 1;
                break;

            case "settings":
                selectedTab = 2;
                break;

            case "development":
                selectedTab = 3;
                break;

            case "tools":
                selectedTab = 4;
                break;

            case "dashboard":
                selectedTab = 5;
                break;

            case "licenses":
                selectedTab = 6;
                break;

            default:
                selectedTab = 0;
                break;
        }

        BasicTabControlHeader.SelectedTab = selectedTab;
        BasicTabControlHeader.UrlTarget = "cmsdesktop";

        if (RequestHelper.IsWindowsAuthentication())
        {
            pnlSignOut.Visible = false;
            PanelRight.CssClass += " HeaderWithoutSignOut";
        }
        else
        {
            pnlSignOut.BackImageUrl = GetImageUrl("Design/Buttons/SignOutButton.png");
            lblSignOut.Text = GetString("signoutbutton.signout");

            lnkSignOut.OnClientClick = FacebookConnectHelper.FacebookConnectInitForSignOut(CMSContext.CurrentSiteName, ltlFBConnectScript);
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
        if (RequestHelper.IsWindowsAuthentication())
        {
            pnlUsers.Visible = false;
            return;
        }

        ucUsers.WhereCondition = "(UserIsGlobalAdministrator = 0) AND (UserID != " + CMSContext.CurrentUser.UserID + ")";

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


    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        SignOut();
        ltlScript.Text += ScriptHelper.GetScript("parent.location.replace('" + URLHelper.ApplicationPath.TrimEnd('/') + "/default.aspx');");
    }
}
