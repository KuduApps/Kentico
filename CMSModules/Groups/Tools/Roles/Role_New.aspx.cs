using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.Community;
using CMS.UIControls;

public partial class CMSModules_Groups_Tools_Roles_Role_New : CMSGroupRolesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set group id
        int groupId = QueryHelper.GetInteger("groupid", 0);        

        // Pagetitle
        this.CurrentMaster.Title.HelpTopicName = "group_role_general";

        // Initializes page title breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("general.roles");
        pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Roles/Role_List.aspx?groupid=" + groupId;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("Administration-Role_New.NewRole");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        // Edit/Create only roles from current site
        if (CMSContext.CurrentSite != null)
        {
            this.roleEditElem.SiteID = CMSContext.CurrentSite.SiteID;
        }
        this.roleEditElem.GroupID = groupId;

        this.roleEditElem.OnSaved += new EventHandler(roleEditElem_OnSaved);
        this.roleEditElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(roleEditElem_OnCheckPermissions);
    }

    void roleEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CheckPermissions(this.roleEditElem.GroupID, CMSAdminControl.PERMISSION_MANAGE);
    }


    void roleEditElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Role_Edit.aspx?roleId=" + this.roleEditElem.RoleID  + "&groupid=" + this.roleEditElem.GroupID);
    }
}
