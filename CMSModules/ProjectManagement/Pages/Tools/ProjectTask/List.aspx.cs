using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_ProjectTask_List : CMSProjectManagementTasksPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int projectId = QueryHelper.GetInteger("projectid", 0);
        
        ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(projectId);
        if (pi != null)
        {
            if (pi.ProjectSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToInformation(GetString("general.notassigned"));
            }
        }

        if (projectId > 0)
        {
            // Display project tasks
            listElem.ProjectID = projectId;
            listElem.OrderByType = ProjectTaskOrderByEnum.ProjectOrder;
            listElem.OrderBy = "TaskPriorityOrder ASC, ProjectTaskDeadline DESC";
        }
        else
        {
            // Display all task (project + ad-hoc tasks)
            listElem.Grid.GridName = "~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/ListAll.xml";
            listElem.OrderBy = "ProjectTaskDisplayName";
            listElem.SiteName = CMSContext.CurrentSiteName;
        }

        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = (projectId > 0) ? GetString("pm.projecttask.new") : GetString("pm.projecttask.newpersonal");
        actions[0, 3] = ResolveUrl("Edit.aspx" + ((projectId > 0) ? ("?projectid=" + projectId) : ""));
        actions[0, 5] = GetImageUrl("Objects/PM_ProjectTask/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;
        master.HeaderActions.Actions = actions;

        ScriptHelper.RegisterTitleScript(this, GetString("projectmanagement.tasks"));
    }

    #endregion
}
