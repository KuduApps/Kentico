using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_Groups_Tools_ProjectManagement_Project_List : CMSGroupProjectManagementPage
{
    #region "Variables"

    int groupId = 0;

    #endregion 


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        groupId = QueryHelper.GetInteger("groupid", 0);

        listElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(listElem_OnCheckPermissions);

        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("pm.project.new");
        actions[0, 3] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/Project/Edit.aspx?groupid=" + groupId);
        actions[0, 5] = GetImageUrl("Objects/PM_Project/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;

        // Set the title
        PageTitle title = master.Title;

        // Set the group id
        listElem.CommunityGroupID = groupId;
    }


    /// <summary>
    /// Check permissions handler.
    /// </summary>
    /// <param name="permissionType">Type of a permission to check</param>
    /// <param name="sender">Sender</param>
    protected void listElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(groupId, permissionType);
    }

    #endregion
}
