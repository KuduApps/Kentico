using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;
using CMS.SettingsProvider;

public partial class CMSModules_ProjectManagement_MyProjectsAndTasks_MyProjects_TasksAssignedToMe : CMSMyProjectsAndTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("pm.projecttask.newpersonal");
        actions[0, 3] = ResolveUrl("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_TaskEdit.aspx?mytasks=1");
        actions[0, 5] = GetImageUrl("Objects/PM_ProjectTask/add.png");
        
        // Add empty action to set visibility
        CurrentMaster.HeaderActions.Actions = actions;

        // Set not specified order by default
        ucTaskList.OrderByType = ProjectTaskOrderByEnum.NotSpecified;
        
        // Use user order
        ucTaskList.OrderByType = ProjectTaskOrderByEnum.UserOrder;
        
        // Default order by
        ucTaskList.Grid.OrderBy = "TaskPriorityOrder ASC,ProjectTaskDeadline DESC";

        // Grid name
        ucTaskList.Grid.GridName = "~/CMSModules/ProjectManagement/Controls/UI/ProjectTask/ListMyTasks.xml";

        // Edit page
        ucTaskList.EditPageURL = ResolveUrl("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_TaskEdit.aspx");

        // Handle where condition
        ucTaskList.BuildCondition += new CMSModules_ProjectManagement_Controls_UI_ProjectTask_List.BuildConditionEvent(ucTaskList_BuildCondition);

        // Set my tasks
        ucTaskList.EditPageParameters = "&mytasks=1";
    }


    /// <summary>
    /// Adds  specific conditions to the list where condition.
    /// </summary>
    string ucTaskList_BuildCondition(object sender, string whereCondition)
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Display onlyt task assigned to me
        whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "ProjectTaskAssignedToUserID = " + currentUser.UserID);

        // Add security condition - display only tasks which are assigned or owned by the current user or which are a part of a project where the current user is authorised to Read from
        whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, ProjectTaskInfoProvider.CombineSecurityWhereCondition(whereCondition, currentUser, null));

        return whereCondition;
    }
}
