using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_Header : CMSRolesPage
{
    private int roleId = 0;
    protected RoleInfo role = null;
    int siteID = 0;
    string urlQuery = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        roleId = QueryHelper.GetInteger("roleid", 0);

        string roleListUrl = "~/CMSModules/Membership/Pages/Roles/Role_List.aspx";

        if (SelectedSiteID != 0)
        {
            urlQuery = "selectedsiteid=" + SelectedSiteID;
            siteID = SelectedSiteID;
        }
        else if (SiteID != 0)
        {
            urlQuery = "siteid=" + SiteID;
            siteID = SiteID;
        }

        if (urlQuery != String.Empty)
        {
            roleListUrl += "?";
        }

        roleListUrl += urlQuery;

        string currentRole = "";
        role = RoleInfoProvider.GetRoleInfo(roleId);
        if (role != null)
        {
            currentRole = role.DisplayName;
        }

        // Initialize PageTitle breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("general.roles");
        pageTitleTabs[0, 1] = roleListUrl;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentRole;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.TitleText = GetString("Administration-Role_Edit.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Role/object.png");
        CurrentMaster.Title.HelpTopicName = "general_tab9";
        CurrentMaster.Title.HelpName = "title";

        // Register script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ShowContent", ScriptHelper.GetScript("function ShowContent(contentLocation) { parent.frames['content'].location.href= contentLocation; }"));

        // Tabs
        InitalizeTabs();
    }


    /// <summary>
    /// Initializes tabs.
    /// </summary>
    protected void InitalizeTabs()
    {
        if (urlQuery != String.Empty)
        {
            urlQuery = "&" + urlQuery;
        }
        string roleName = "";
        if (role != null)
        {
            roleName = role.RoleName;
        }

        bool displayUsers = ((roleName != RoleInfoProvider.EVERYONE) && (roleName != RoleInfoProvider.AUTHENTICATED) && (roleName != RoleInfoProvider.NOTAUTHENTICATED));

        bool showMembershipTab = LicenseKeyInfoProvider.IsFeatureAvailable(FeatureEnum.Membership);

        string generalString = GetString("general.general");
        string usersString = GetString("general.users");
        string permissionsString = GetString("administration.roles.permissions");
        string personalizationString = GetString("administration.roles.uipersonalization");
        string membershipString = GetString("membership.title.plular");

        string[,] tabs = new string[5, 4];
        tabs[0, 0] = generalString;
        tabs[0, 1] = "SetHelpTopic('title', 'general_tab9');";
        tabs[0, 2] = "Role_Edit_General.aspx?roleid=" + roleId + "&siteId=" + siteID;
        if (displayUsers)
        {
            tabs[1, 0] = usersString;
            tabs[1, 1] = "SetHelpTopic('title', 'users_tab');";
            tabs[1, 2] = "Role_Edit_Users.aspx?roleid=" + roleId + "&siteId=" + siteID;

            if (showMembershipTab)
            {
                tabs[2, 0] = membershipString;
                tabs[2, 1] = "SetHelpTopic('title', 'membership_tab');";
                tabs[2, 2] = "Role_Edit_Memberships.aspx?roleid=" + roleId + "&siteId=" + siteID;
            }
        }
        tabs[3, 0] = permissionsString;
        tabs[3, 1] = "SetHelpTopic('title', 'permissions_tab');";
        tabs[3, 2] = "Role_Edit_Permissions_Default.aspx?roleid=" + roleId + "&siteId=" + siteID;
        tabs[4, 0] = personalizationString;
        tabs[4, 1] = "SetHelpTopic('title', 'personalization_tab');";
        tabs[4, 2] = "Role_Edit_UI_Frameset.aspx?roleid=" + roleId + urlQuery;

        CurrentMaster.Tabs.UrlTarget = "content";
        CurrentMaster.Tabs.Tabs = tabs;
    }
}