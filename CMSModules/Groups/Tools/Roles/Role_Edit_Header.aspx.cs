using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Roles_Role_Edit_Header : CMSGroupRolesPage
{
    protected int roleId;
    protected int groupId;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        roleId = QueryHelper.GetInteger("roleid", 0);
        groupId = QueryHelper.GetInteger("groupid", 0);

        string currentRole = "";
        RoleInfo role = RoleInfoProvider.GetRoleInfo(roleId);
        if (role != null)
        {
            currentRole = role.DisplayName;
        }

        // Initialize PageTitle breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("general.roles");
        pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Roles/Role_List.aspx?groupid=" + groupId;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentRole;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.HelpTopicName = "group_role_general";
        this.CurrentMaster.Title.HelpName = "title";

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
        string generalString = GetString("general.general");
        string usersString = GetString("general.users");

        string[,] tabs = new string[2, 4];
        tabs[0, 0] = generalString;
        tabs[0, 1] = "SetHelpTopic('title', 'group_role_general');";
        tabs[0, 2] = "Role_Edit_General.aspx?roleid=" + roleId + "&groupid=" + groupId;
        tabs[1, 0] = usersString;
        tabs[1, 1] = "SetHelpTopic('title', 'group_role_users');";
        tabs[1, 2] = "Role_Edit_Users.aspx?roleid=" + roleId + "&groupid=" + groupId;
        this.CurrentMaster.Tabs.UrlTarget = "content";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }
}