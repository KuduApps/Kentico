using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.ProjectManagement;
using CMS.LicenseProvider;
using CMS.SettingsProvider;


[Title(Text = "Projects", ImageUrl = "Objects/PM_Project/object.png")]
public partial class CMSAPIExamples_Code_Tools_ProjectManagement_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ProjectManagement);

        // Project
        this.apiCreateProject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateProject);
        this.apiGetAndUpdateProject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateProject);
        this.apiGetAndBulkUpdateProjects.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateProjects);
        this.apiDeleteProject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteProject);

        // Project task
        this.apiCreateProjectTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateProjectTask);
        this.apiGetAndUpdateProjectTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateProjectTask);
        this.apiGetAndBulkUpdateProjectTasks.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateProjectTasks);
        this.apiDeleteProjectTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteProjectTask);

        // Project security
        this.apiAddAuthorizedRole.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddAuthorizedRole);
        this.apiSetSecurity.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SetSecurity);
        this.apiRemoveAuthorizedRole.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveAuthorizedRole);
    }



    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Project
        this.apiCreateProject.Run();
        this.apiGetAndUpdateProject.Run();
        this.apiGetAndBulkUpdateProjects.Run();

        // Project task
        this.apiCreateProjectTask.Run();
        this.apiGetAndUpdateProjectTask.Run();
        this.apiGetAndBulkUpdateProjectTasks.Run();

        // Project security
        this.apiAddAuthorizedRole.Run();
        this.apiSetSecurity.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Project's security
        this.apiRemoveAuthorizedRole.Run();

        // Project task
        this.apiDeleteProjectTask.Run();

        // Project
        this.apiDeleteProject.Run();
    }

    #endregion


    #region "API examples - Project"

    /// <summary>
    /// Creates project. Called when the "Create project" button is pressed.
    /// </summary>
    private bool CreateProject()
    {
        ProjectStatusInfo status = ProjectStatusInfoProvider.GetProjectStatusInfo("NotStarted");

        if (status != null)
        {
            int currentUserID = CMSContext.CurrentUser.UserID;

            // Create new project object
            ProjectInfo newProject = new ProjectInfo();

            // Set the properties
            newProject.ProjectDisplayName = "My new project";
            newProject.ProjectName = "MyNewProject";
            newProject.ProjectStatusID = status.StatusID;
            newProject.ProjectSiteID = CMSContext.CurrentSiteID;
            newProject.ProjectOwner = currentUserID;
            newProject.ProjectCreatedByID = currentUserID;

            // Save the project
            ProjectInfoProvider.SetProjectInfo(newProject);

        }
        return true;
    }


    /// <summary>
    /// Gets and updates project. Called when the "Get and update project" button is pressed.
    /// Expects the CreateProject method to be run first.
    /// </summary>
    private bool GetAndUpdateProject()
    {
        // Get the project
        ProjectInfo updateProject = ProjectInfoProvider.GetProjectInfo("MyNewProject", CMSContext.CurrentSiteID, 0);
        if (updateProject != null)
        {
            // Update the properties
            updateProject.ProjectDisplayName = updateProject.ProjectDisplayName.ToLower();

            // Save the changes
            ProjectInfoProvider.SetProjectInfo(updateProject);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates projects. Called when the "Get and bulk update projects" button is pressed.
    /// Expects the CreateProject method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateProjects()
    {
        // Prepare the parameters
        string where = "ProjectName LIKE N'MyNewProject%'";
        string orderBy = "";
        int topN = 0;
        string columns = "";

        // Get the data
        DataSet projects = ProjectInfoProvider.GetProjects(where, orderBy, topN, columns);
        if (!DataHelper.DataSourceIsEmpty(projects))
        {
            // Loop through the individual items
            foreach (DataRow projectDr in projects.Tables[0].Rows)
            {
                // Create object from DataRow
                ProjectInfo modifyProject = new ProjectInfo(projectDr);

                // Update the properties
                modifyProject.ProjectDisplayName = modifyProject.ProjectDisplayName.ToUpper();

                // Save the changes
                ProjectInfoProvider.SetProjectInfo(modifyProject);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes project. Called when the "Delete project" button is pressed.
    /// Expects the CreateProject method to be run first.
    /// </summary>
    private bool DeleteProject()
    {
        // Get the project
        ProjectInfo deleteProject = ProjectInfoProvider.GetProjectInfo("MyNewProject", CMSContext.CurrentSiteID, 0);

        // Delete the project
        ProjectInfoProvider.DeleteProjectInfo(deleteProject);

        return (deleteProject != null);
    }




    #endregion


    #region "API examples - Project task"

    /// <summary>
    /// Creates project task. Called when the "Create task" button is pressed.
    /// </summary>
    private bool CreateProjectTask()
    {
        ProjectInfo project = ProjectInfoProvider.GetProjectInfo("MyNewProject", CMSContext.CurrentSiteID, 0);
        ProjectTaskStatusInfo status = ProjectTaskStatusInfoProvider.GetProjectTaskStatusInfo("NotStarted");
        ProjectTaskPriorityInfo priority = ProjectTaskPriorityInfoProvider.GetProjectTaskPriorityInfo("Normal");

        if ((project != null) && (status != null) && (priority != null))
        {
            // Create new project task object
            ProjectTaskInfo newTask = new ProjectTaskInfo();

            int currentUserID = CMSContext.CurrentUser.UserID;

            // Set the properties
            newTask.ProjectTaskDisplayName = "My new task";
            newTask.ProjectTaskCreatedByID = currentUserID;
            newTask.ProjectTaskOwnerID = currentUserID;
            newTask.ProjectTaskAssignedToUserID = currentUserID;
            newTask.ProjectTaskStatusID = status.TaskStatusID;
            newTask.ProjectTaskPriorityID = priority.TaskPriorityID;
            newTask.ProjectTaskProjectID = project.ProjectID;

            // Save the project task
            ProjectTaskInfoProvider.SetProjectTaskInfo(newTask);

            return true;
        }
        return false;
    }


    /// <summary>
    /// Gets and updates project task. Called when the "Get and update task" button is pressed.
    /// Expects the CreateProjectTask method to be run first.
    /// </summary>
    private bool GetAndUpdateProjectTask()
    {
        // Prepare the parameters
        string where = "ProjectTaskDisplayName LIKE N'My new task%'";
        string orderBy = "";
        int topN = 0;
        string columns = "";

        // Get the data
        DataSet tasks = ProjectTaskInfoProvider.GetProjectTasks(where, orderBy, topN, columns);
        if (!DataHelper.DataSourceIsEmpty(tasks))
        {
            // Get the project task
            ProjectTaskInfo updateTask = new ProjectTaskInfo(tasks.Tables[0].Rows[0]);
            if (updateTask != null)
            {
                // Update the properties
                updateTask.ProjectTaskDisplayName = updateTask.ProjectTaskDisplayName.ToLower();

                // Save the changes
                ProjectTaskInfoProvider.SetProjectTaskInfo(updateTask);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates project tasks. Called when the "Get and bulk update tasks" button is pressed.
    /// Expects the CreateProjectTask method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateProjectTasks()
    {
        // Prepare the parameters
        string where = "ProjectTaskDisplayName LIKE N'My new task%'";
        string orderBy = "";
        int topN = 0;
        string columns = "";

        // Get the data
        DataSet tasks = ProjectTaskInfoProvider.GetProjectTasks(where, orderBy, topN, columns);
        if (!DataHelper.DataSourceIsEmpty(tasks))
        {
            // Loop through the individual items
            foreach (DataRow taskDr in tasks.Tables[0].Rows)
            {
                // Create object from DataRow
                ProjectTaskInfo modifyTask = new ProjectTaskInfo(taskDr);

                // Update the properties
                modifyTask.ProjectTaskDisplayName = modifyTask.ProjectTaskDisplayName.ToUpper();

                // Save the changes
                ProjectTaskInfoProvider.SetProjectTaskInfo(modifyTask);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes project task. Called when the "Delete task" button is pressed.
    /// Expects the CreateProjectTask method to be run first.
    /// </summary>
    private bool DeleteProjectTask()
    {
        // Prepare the parameters
        string where = "ProjectTaskDisplayName LIKE N'My new task%'";
        string orderBy = "";
        int topN = 0;
        string columns = "";

        // Get the data
        DataSet tasks = ProjectTaskInfoProvider.GetProjectTasks(where, orderBy, topN, columns);
        if (!DataHelper.DataSourceIsEmpty(tasks))
        {

            // Get the project task
            ProjectTaskInfo deleteTask = new ProjectTaskInfo(tasks.Tables[0].Rows[0]);

            // Delete the project task
            ProjectTaskInfoProvider.DeleteProjectTaskInfo(deleteTask);

            return (deleteTask != null);
        }
        return false;
    }

    #endregion


    #region "API examples - Project security"


    /// <summary>
    /// Sets project's security. Called when the "Set project's security" button is pressed.
    /// Expects the CreateProject method to be run first.
    /// </summary>
    private bool SetSecurity()
    {
        // Get the project
        ProjectInfo project = ProjectInfoProvider.GetProjectInfo("MyNewProject", CMSContext.CurrentSiteID, 0);

        if (project != null)
        {
            // Set properties
            project.AllowCreate = SecurityAccessEnum.AllUsers;
            project.AllowDelete = SecurityAccessEnum.Owner;
            project.AllowRead = SecurityAccessEnum.AuthorizedRoles;

            ProjectInfoProvider.SetProjectInfo(project);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Adds authorized role to project. Called when the "Add authorized role" button is pressed.
    /// Expects the CreateProject method to be run first.
    /// </summary>
    private bool AddAuthorizedRole()
    {
        // Get the project
        ProjectInfo project = ProjectInfoProvider.GetProjectInfo("MyNewProject", CMSContext.CurrentSiteID, 0);
        RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSDeskAdmin", CMSContext.CurrentSiteID);
        PermissionNameInfo permission = PermissionNameInfoProvider.GetPermissionNameInfo("AccessToProject", "ProjectManagement", null);

        if ((project != null) && (role != null) && (permission != null))
        {
            // Add relationship
            ProjectRolePermissionInfoProvider.AddRelationship(project.ProjectID, role.RoleID, permission.PermissionId);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes authorized role to project. Called when the "Remove authorized role" button is pressed.
    /// Expects the CreateProject and AddAuthorizedRole methods to be run first.
    /// </summary>
    private bool RemoveAuthorizedRole()
    {
        // Get the project
        ProjectInfo project = ProjectInfoProvider.GetProjectInfo("MyNewProject", CMSContext.CurrentSiteID, 0);
        RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSDeskAdmin", CMSContext.CurrentSiteID);
        PermissionNameInfo permission = PermissionNameInfoProvider.GetPermissionNameInfo("AccessToProject", "ProjectManagement", null);

        if ((project != null) && (role != null) && (permission != null))
        {
            // Remove relationship
            ProjectRolePermissionInfoProvider.RemoveRelationship(project.ProjectID, role.RoleID, permission.PermissionId);

            return true;
        }

        return false;
    }

    #endregion
}
