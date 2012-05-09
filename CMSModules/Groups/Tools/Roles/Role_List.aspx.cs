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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Roles_Role_List : CMSGroupRolesPage
{
    int groupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        groupId = QueryHelper.GetInteger("groupid", 0);

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Administration-Role_List.NewRole");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Role_New.aspx?groupid=" + groupId);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Role/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        // Show only roles from current site
        if (CMSContext.CurrentSite != null)
        {
            this.roleListElem.SiteID = CMSContext.CurrentSite.SiteID;
        }
        this.roleListElem.GroupID = groupId;
        this.roleListElem.IsGroupList = true;
        this.roleListElem.OnAction += new CommandEventHandler(roleListElem_OnAction);
        this.roleListElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(roleListElem_OnCheckPermissions);
    }


    void roleListElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (permissionType == CMSAdminControl.PERMISSION_READ)
        {
            // Check permissions
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_READ))
            {
                RedirectToCMSDeskAccessDenied("CMS.Groups", CMSAdminControl.PERMISSION_READ);
            }
        }
        else
        {
            // Check permissions
            CheckPermissions(this.roleListElem.GroupID, CMSAdminControl.PERMISSION_MANAGE);
        }
    }


    protected void roleListElem_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_READ))
                {
                    RedirectToCMSDeskAccessDenied("CMS.Groups", CMSAdminControl.PERMISSION_READ);
                }
                URLHelper.Redirect("~/CMSModules/Groups/Tools/Roles/Role_Edit.aspx?groupid=" + groupId + "&roleid=" + ValidationHelper.GetInteger(e.CommandArgument, 0));
                break;
        }
    }
}
