using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_ProjectManagement_MyProjectsAndTasks_MyProjects_TaskEdit : CMSMyProjectsAndTasksPage
{
    #region "Variables"

    private bool taskAssignedToMe = false;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        taskAssignedToMe = QueryHelper.GetBoolean("mytasks", false);
        editElem.ItemID = QueryHelper.GetInteger("projecttaskid", 0);
        
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Check whether task can be displayed (depends on display mode)
        if (editElem.ProjectTaskObj != null)
        {
            if (taskAssignedToMe)
            {
                if (editElem.ProjectTaskObj.ProjectTaskAssignedToUserID != currentUser.UserID)
                {
                    editElem.ItemID = 0;
                    editElem.ProjectTaskObj = null;
                }
            }
            else
            {
                if (editElem.ProjectTaskObj.ProjectTaskOwnerID != currentUser.UserID)
                {
                    editElem.ItemID = 0;
                    editElem.ProjectTaskObj = null;
                }
            }
        }
        editElem.OnSaved += new EventHandler(editElem_OnSaved);
        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Keep title object
        PageTitle title = this.CurrentMaster.Title;

        // Title
        string name = GetString("pm.projecttask.newpersonal");
        title.TitleImage = GetImageUrl("Objects/PM_ProjectTask/new.png");
        title.HelpTopicName = "pm_task_edit";

        // Set breadcrumb to exisiting task
        if (editElem.ProjectTaskObj != null)
        {
            name = editElem.ProjectTaskObj.ProjectTaskDisplayName;
        }

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("pm.projecttask");
        if (taskAssignedToMe)
        {
            breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_TasksAssignedToMe.aspx");
        }
        else
        {
            breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_TasksOwnedByMe.aspx");
        }

        breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(name);

        // Set the title
        title.Breadcrumbs = breadcrumbs;
    }


    /// <summary>
    /// OnSaved event handler.
    /// </summary>
    void editElem_OnSaved(object sender, EventArgs e)
    {
        if (editElem.ItemID > 0)
        {
            URLHelper.Redirect("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_TaskEdit.aspx?projecttaskid=" + editElem.ItemID + "&saved=1&mytasks=" + taskAssignedToMe.ToString());
        }
    }

    #endregion
}
