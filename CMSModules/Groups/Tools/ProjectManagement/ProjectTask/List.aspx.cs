using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_Groups_Tools_ProjectManagement_ProjectTask_List : CMSGroupProjectManagementPage
{
    #region "Variables"

    int groupId = 0; 

    #endregion

    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int projectId = QueryHelper.GetInteger("projectid", 0);
        groupId = QueryHelper.GetInteger("groupid", 0);

        if (projectId > 0)
        {
            // Display project tasks
            listElem.ProjectID = projectId;
            listElem.OrderByType = ProjectTaskOrderByEnum.ProjectOrder;
        }
        else
        {
            // Display all task (project + ad-hoc tasks)
            listElem.Grid.GridName = URLHelper.ResolveUrl("~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/ListAll.xml");
            listElem.OrderByType = ProjectTaskOrderByEnum.ProjectOrder;
            listElem.OrderBy = "ProjectTaskDisplayName";
        }

        // Set OnCheckPermissions event handler
        listElem.OnCheckPermissionsExtended += new CMSAdminControl.CheckPermissionsExtendedEventHandler(listElem_OnCheckPermissionsExtended);

        listElem.CommunityGroupID = groupId;

        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("pm.projecttask.new");
        actions[0, 3] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/ProjectTask/Edit.aspx?projectid=" + projectId + "&groupid=" + groupId);
        actions[0, 5] = GetImageUrl("Objects/PM_ProjectTask/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Check permissions handler.
    /// </summary>
    /// <param name="permissionType">Type of a permission to check</param>
    /// <param name="modulePermissionType">Name of the module permission</param>
    /// <param name="sender">Sender</param>
    protected void listElem_OnCheckPermissionsExtended(string permissionType, string modulePermissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(groupId, modulePermissionType);
    }

    #endregion
}
