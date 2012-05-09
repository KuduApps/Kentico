using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_MyProjectsAndTasks_MyProjects_ProjectEdit : CMSMyProjectsAndTasksPage
{
    #region "Variables"
    
    int projectId = 0;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get project id from query string
        projectId = QueryHelper.GetInteger("projectid", 0);

        // Check whether user can see required project
        if (!ProjectInfoProvider.IsAuthorizedPerProject(projectId, ProjectManagementPermissionType.READ, CMSContext.CurrentUser))
        {
            RedirectToAccessDenied("CMS.ProjectManagement", "Manage");
        }

        editElem.ProjectID = QueryHelper.GetInteger("projectid", 0);

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Keep title object
        PageTitle title = this.CurrentMaster.Title;

        // Title
        string name = String.Empty;
        title.TitleImage = GetImageUrl("Objects/PM_Project/new.png");
        title.HelpTopicName = "pm_project_edit";

        // Set breadcrumb to exisiting task
        if (editElem.ProjectObj != null)
        {
            name = editElem.ProjectObj.ProjectDisplayName;
        }

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("pm.project.list");
        breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_MyProjects.aspx");
        breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(name);

        // Set the title
        title.Breadcrumbs = breadcrumbs;

        // Handle check permissions
        editElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(editElem_OnCheckPermissions);
    }


    void editElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Keep current user info
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Check whether user has manage permission or is project owner to edit project
        if (!currentUser.IsAuthorizedPerResource("CMS.ProjectManagement", "Manage") && (editElem.ProjectObj != null) && (editElem.ProjectObj.ProjectOwner != currentUser.UserID))
        {
            sender.StopProcessing = true;
            RedirectToAccessDenied("CMS.ProjectManagement", "Manage");
        }
    }

    #endregion
}
