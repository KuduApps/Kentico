using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ProjectManagement;
using CMS.CMSHelper;
using CMS.SettingsProvider;


public partial class CMSModules_ProjectManagement_Controls_LiveControls_Tasks : CMSAdminItemsControl
{
    #region "Variables"

    private string mProjectNames = String.Empty;
    private TasksDisplayTypeEnum mTasksDisplayType = TasksDisplayTypeEnum.TasksAssignedToMe;
    private bool mShowOverdueTasks = true;
    private bool mShowOnTimeTasks = true;
    private bool mShowPrivateTasks = true;
    private bool mShowFinishedTasks = true;
    private HeaderActions mHeaderActions = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the header actions.
    /// </summary>
    public HeaderActions HeaderActions
    {
        get
        {
            if (mHeaderActions == null)
            {
                mHeaderActions = actionsElem;
            }
            return mHeaderActions;
        }
        set
        {
            mHeaderActions = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), String.Empty);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.EnsureChildControls();
            this.ucTaskList.SiteName = this.SiteName;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether paging should be used.
    /// </summary>
    public bool EnablePaging
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnablePaging"), false);
        }
        set
        {
            this.SetValue("EnablePaging", value);
        }
    }


    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), 10);
        }
        set
        {
            this.SetValue("PageSize", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether task actions should be enabled.
    /// </summary>
    public bool AllowTaskActions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowTaskActions"), false);
        }
        set
        {
            this.SetValue("AllowTaskActions", value);
        }
    }


    /// <summary>
    /// Gets or sets the project names which should be used for task filtering
    /// Project names are splitted by semicolon
    /// </summary>
    public string ProjectNames
    {
        get
        {
            return mProjectNames;
        }
        set
        {
            mProjectNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether current control is displayed on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return ucTaskList.IsLiveSite;
        }
        set
        {
            this.EnsureChildControls();
            ucTaskList.IsLiveSite = value;
            ucTaskEdit.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets the list where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ucTaskList.Grid.WhereCondition;
        }
        set
        {
            this.EnsureChildControls();
            ucTaskList.Grid.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the current display type.
    /// </summary>
    public TasksDisplayTypeEnum TasksDisplayType
    {
        get
        {
            return mTasksDisplayType;
        }
        set
        {
            this.EnsureChildControls();
            mTasksDisplayType = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowOnTimeTasks
    {
        get
        {
            return mShowOnTimeTasks;
        }
        set
        {
            mShowOnTimeTasks = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowPrivateTasks
    {
        get
        {
            return mShowPrivateTasks;
        }
        set
        {
            mShowPrivateTasks = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowFinishedTasks
    {
        get
        {
            return mShowFinishedTasks;
        }
        set
        {
            mShowFinishedTasks = value;
        }
    }


    /// <summary>
    /// Show overdue tasks.
    /// </summary>
    public bool ShowOverdueTasks
    {
        get
        {
            return mShowOverdueTasks;
        }
        set
        {
            mShowOverdueTasks = value;
        }
    }


    /// <summary>
    /// Display type of status.
    /// </summary>
    public StatusDisplayTypeEnum StatusDisplayType
    {
        get
        {
            return ucTaskList.StatusDisplayType;
        }
        set
        {
            this.EnsureChildControls();
            ucTaskList.StatusDisplayType = value;
        }
    }


    #endregion


    #region "Action methods"

    /// <summary>
    /// Edit task event handler.
    /// </summary>
    void ucTaskList_OnAction(object sender, CommandEventArgs e)
    {
        // Switch by command name
        switch (e.CommandName.ToString())
        {
            // Edit
            case "edit":
                // Clear edit form
                ucTaskEdit.ClearForm();
                // Set task id from command argument
                int taskID = ValidationHelper.GetInteger(e.CommandArgument, 0);
                // Set task id 
                this.ucTaskEdit.ItemID = taskID;
                // Reload task edit form data
                this.ucTaskEdit.ReloadData(true);
                // Render dialog
                this.ucPopupDialog.Visible = true;
                // Show modal dialog
                this.ucPopupDialog.Show();
                // Reload dialog update panel
                this.pnlUpdate.Update();
                break;
        }
    }


    /// <summary>
    /// New task event handler.
    /// </summary>
    void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Clear selected task ID
        ucTaskEdit.ItemID = 0;
        // Clear form data
        ucTaskEdit.ClearForm();
        // Reload task edit data
        this.ucTaskEdit.ReloadData(true);
        // Set popup title 
        titleElem.TitleText = GetString("pm.projecttask.new");
        // Render dialog
        this.ucPopupDialog.Visible = true;
        // Shoe modal dialog
        this.ucPopupDialog.Show();
        // Reload dialog update panel
        this.pnlUpdate.Update();
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Keep current user object
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        // Title element settings
        titleElem.TitleImage = GetImageUrl("Objects/PM_ProjectTask/object.png");
        titleElem.TitleText = GetString("pm.projecttask.edit");

        #region "Header actions"

        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            // New task link
            string[,] actions = new string[1, 7];
            actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[0, 1] = GetString("pm.projecttask.newpersonal");
            actions[0, 2] = null;
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("Objects/PM_Project/add.png");
            actions[0, 6] = "new_task";

            HeaderActions.Actions = actions;
            HeaderActions.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);
            HeaderActions.ReloadData();
        }
        #endregion

        // Switch by display type and set correct list grid name
        switch (this.TasksDisplayType)
        {
            // Project tasks
            case TasksDisplayTypeEnum.ProjectTasks:
                ucTaskList.OrderByType = ProjectTaskOrderByEnum.NotSpecified;
                ucTaskList.Grid.OrderBy = "TaskPriorityOrder ASC,ProjectTaskDeadline DESC";
                ucTaskList.Grid.GridName = "~/CMSModules/ProjectManagement/Controls/LiveControls/ProjectTasks.xml";
                pnlListActions.Visible = false;
                break;

            // Tasks owned by me
            case TasksDisplayTypeEnum.TasksOwnedByMe:
                ucTaskList.OrderByType = ProjectTaskOrderByEnum.NotSpecified;
                ucTaskList.Grid.OrderBy = "TaskPriorityOrder ASC,ProjectTaskDeadline DESC";
                ucTaskList.Grid.GridName = "~/CMSModules/ProjectManagement/Controls/LiveControls/TasksOwnedByMe.xml";
                break;

            // Tasks assigned to me
            case TasksDisplayTypeEnum.TasksAssignedToMe:
                // Set not specified order by default
                ucTaskList.OrderByType = ProjectTaskOrderByEnum.NotSpecified;
                // If sitename is not defined => display task from all sites => use user order
                if (String.IsNullOrEmpty(this.SiteName))
                {
                    ucTaskList.OrderByType = ProjectTaskOrderByEnum.UserOrder;
                }
                ucTaskList.Grid.OrderBy = "TaskPriorityOrder ASC,ProjectTaskDeadline DESC";
                ucTaskList.Grid.GridName = "~/CMSModules/ProjectManagement/Controls/LiveControls/TasksAssignedToMe.xml";
                break;
        }

        #region "Force edit by TaskID in querystring"

        // Check whether is not postback
        if (!RequestHelper.IsPostBack())
        {
            // Try get value from request stroage which indicates whether force dialog is displayed
            bool isDisplayed = ValidationHelper.GetBoolean(RequestStockHelper.GetItem("cmspmforceitemdisplayed", true), false);

            // Try get task id from querystring
            int forceTaskId = QueryHelper.GetInteger("taskid", 0);
            if ((forceTaskId > 0) && (!isDisplayed))
            {
                ProjectTaskInfo pti = ProjectTaskInfoProvider.GetProjectTaskInfo(forceTaskId);
                ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(pti.ProjectTaskProjectID);

                // Check whether task is defined 
                // and if is assigned to some project, this project is assigned to current site
                if ((pti != null) && ((pi == null) || (pi.ProjectSiteID == CMSContext.CurrentSiteID)))
                {
                    bool taskIdValid = false;

                    // Switch by display type
                    switch (this.TasksDisplayType)
                    {
                        // Tasks created by me
                        case TasksDisplayTypeEnum.TasksOwnedByMe:
                            if (pti.ProjectTaskOwnerID == currentUser.UserID)
                            {
                                taskIdValid = true;
                            }
                            break;

                        // Tasks assigned to me
                        case TasksDisplayTypeEnum.TasksAssignedToMe:
                            if (pti.ProjectTaskAssignedToUserID == currentUser.UserID)
                            {
                                taskIdValid = true;
                            }
                            break;

                        // Project task
                        case TasksDisplayTypeEnum.ProjectTasks:
                            if (!String.IsNullOrEmpty(ProjectNames) && (pi != null))
                            {
                                string projectNames = ";" + ProjectNames.ToLower() + ";";
                                if (projectNames.Contains(";" + pi.ProjectName.ToLower() + ";"))
                                {
                                    // Check whether user can see private task
                                    if (!pti.ProjectTaskIsPrivate
                                        || ((pti.ProjectTaskOwnerID == currentUser.UserID) || (pti.ProjectTaskAssignedToUserID == currentUser.UserID))
                                        || ((pi.ProjectGroupID > 0) && currentUser.IsGroupAdministrator(pi.ProjectGroupID))
                                        || ((pi.ProjectGroupID == 0) && (currentUser.IsAuthorizedPerResource("CMS.ProjectManagement", CMSAdminControl.PERMISSION_MANAGE))))
                                    {
                                        taskIdValid = true;
                                    }
                                }
                            }
                            break;
                    }

                    bool displayValid = true;

                    // Check whether do not display finished tasks is required
                    if (!this.ShowFinishedTasks)
                    {
                        ProjectTaskStatusInfo ptsi = ProjectTaskStatusInfoProvider.GetProjectTaskStatusInfo(pti.ProjectTaskStatusID);
                        if ((ptsi != null) && (ptsi.TaskStatusIsFinished))
                        {
                            displayValid = false;
                        }
                    }

                    // Check whether private task should be edited
                    if (!this.ShowPrivateTasks)
                    {
                        if (pti.ProjectTaskProjectID == 0)
                        {
                            displayValid = false;
                        }
                    }

                    // Check whether ontime task should be edited
                    if (!this.ShowOnTimeTasks)
                    {
                        if ((pti.ProjectTaskDeadline != DateTimeHelper.ZERO_TIME) && (pti.ProjectTaskDeadline < DateTime.Now))
                        {
                            displayValid = false;
                        }
                    }

                    // Check whether overdue task should be edited
                    if (!this.ShowOverdueTasks)
                    {
                        if ((pti.ProjectTaskDeadline != DateTimeHelper.ZERO_TIME) && (pti.ProjectTaskDeadline > DateTime.Now))
                        {
                            displayValid = false;
                        }
                    }

                    // Check whether user is allowed to see project
                    if ((pi != null) && (ProjectInfoProvider.IsAuthorizedPerProject(pi.ProjectID, ProjectManagementPermissionType.READ, CMSContext.CurrentUser)))
                    {
                        displayValid = false;
                    }

                    // If task is valid and user has permissions to see this task display edit task dialog
                    if (displayValid && taskIdValid && ProjectTaskInfoProvider.IsAuthorizedPerTask(forceTaskId, ProjectManagementPermissionType.READ, CMSContext.CurrentUser, CMSContext.CurrentSiteID))
                    {
                        this.ucTaskEdit.ItemID = forceTaskId;
                        this.ucTaskEdit.ReloadData();
                        // Render dialog
                        this.ucPopupDialog.Visible = true;
                        this.ucPopupDialog.Show();
                        // Set "force dialog displayed" flag
                        RequestStockHelper.Add("cmspmforceitemdisplayed", true, true);
                    }
                }
            }
        }

        #endregion


        #region "Event handlers registration"

        // Register list action handler
        ucTaskList.OnAction += new CommandEventHandler(ucTaskList_OnAction);

        #endregion


        #region "Pager settings"

        // Paging
        if (!EnablePaging)
        {
            ucTaskList.Grid.PageSize = "##ALL##";
            ucTaskList.Grid.Pager.DefaultPageSize = -1;
        }
        else
        {
            ucTaskList.Grid.Pager.DefaultPageSize = PageSize;
            ucTaskList.Grid.PageSize = this.PageSize.ToString();
            ucTaskList.Grid.FilterLimit = PageSize;
        }

        #endregion


        // Use postbacks on list actions
        ucTaskList.UsePostbackOnEdit = true;
        // Check delete permission
        ucTaskList.OnCheckPermissionsExtended += new CheckPermissionsExtendedEventHandler(ucTaskList_OnCheckPermissionsExtended);
        // Dont register JS edit script
        ucTaskList.RegisterEditScript = false;

        // Hide default ok button on edit 
        ucTaskEdit.ShowOKButton = false;
        // Disable on site validators
        ucTaskEdit.DisableOnSiteValidators = true;
        // Check modify permission
        ucTaskEdit.OnCheckPermissionsExtended += new CheckPermissionsExtendedEventHandler(ucTaskEdit_OnCheckPermissionsExtended);
        // Build condition event
        ucTaskList.BuildCondition += new CMSModules_ProjectManagement_Controls_UI_ProjectTask_List.BuildConditionEvent(ucTaskList_BuildCondition);
    }



    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData(bool forceReload)
    {
        // Reload list control
        ucTaskList.ReloadData();
        pnlUpdateList.Update();
        
        // Call base method
        base.ReloadData(forceReload);
    }


    /// <summary>
    /// Button OK click event handler.
    /// </summary>
    protected void btnOK_onClick(object sender, EventArgs ea)
    {
        // Save data
        if (ucTaskEdit.Save())
        {
            // Do not render popup dialog
            this.ucPopupDialog.Visible = false;
            // Hide dialog
            this.ucPopupDialog.Hide();
            // Clear edit item id
            this.ucTaskEdit.ItemID = 0;
            // Reload data
            ucTaskList.ReloadData();
            
            // Update dialog panel
            pnlUpdateList.Update();
        }
        // If save was unsucccessful keep dialog displayed
        else
        {
            // If new task dialog is displayed set appropriate title
            if (ucTaskEdit.ItemID == 0)
            {
                titleElem.TitleText = GetString("pm.projecttask.edit");
            }
            // Render dialog
            this.ucPopupDialog.Visible = true;
            // Show dialog
            ucPopupDialog.Show();
            // Update dialog panel
            pnlUpdateList.Update();
        }
    }


    /// <summary>
    /// Build list where condition.
    /// </summary>
    string ucTaskList_BuildCondition(object sender, string whereCondition)
    {
        // Keep current user
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Switch by display type
        switch (this.TasksDisplayType)
        {
            // Tasks owned by me
            case TasksDisplayTypeEnum.TasksOwnedByMe:
                whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "ProjectTaskOwnerID = " + currentUser.UserID);
                break;

            // Tasks assigned to me
            case TasksDisplayTypeEnum.TasksAssignedToMe:
                whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "ProjectTaskAssignedToUserID = " + currentUser.UserID);
                break;

            // Project tasks
            case TasksDisplayTypeEnum.ProjectTasks:
                // Check whether project names are defined
                if (!String.IsNullOrEmpty(ProjectNames))
                {
                    string condition = SqlHelperClass.GetSafeQueryString(ProjectNames, false);
                    condition = "N'" + condition.Replace(";", "',N'") + "'";
                    // Add condition for specified projects
                    condition = "ProjectTaskProjectID IN (SELECT ProjectID FROM PM_Project WHERE ProjectName IN (" + condition + "))";

                    // Add condition for private task, only if current user isn't project management admin
                    if (!currentUser.IsAuthorizedPerResource("CMS.ProjectManagement", CMSAdminControl.PERMISSION_MANAGE))
                    {
                        condition = SqlHelperClass.AddWhereCondition(condition, "(ProjectTaskIsPrivate = 0 OR ProjectTaskIsPrivate IS NULL) OR (ProjectTaskOwnerID = " + currentUser.UserID + " OR ProjectTaskAssignedToUserID = " + currentUser.UserID + " OR ProjectOwner = " + currentUser.UserID + ")");
                    }

                    // Complete where condition
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, condition);
                }
                // If project names aren't defined do nothing
                else
                {
                    whereCondition = "(1=2)";
                }
                break;
        }

        // Do not dsiplay finished tasks
        if (!ShowFinishedTasks)
        {
            whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "TaskStatusIsFinished = 0");
        }

        // Do not display on time tasks
        if (!ShowOnTimeTasks)
        {
            whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "((ProjectTaskDeadline < @Now) OR (ProjectTaskDeadline IS NULL))");
        }

        // Do not display overdue tasks
        if (!ShowOverdueTasks)
        {
            whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "((ProjectTaskDeadline > @Now) OR (ProjectTaskDeadline IS NULL))");
        }

        // Do not display private tasks
        if (!ShowPrivateTasks)
        {
            whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "ProjectTaskIsPrivate = 0");
        }

        // Task assigned to me, Task owned by me webparts
        object[,] projectParameters = null;
        if ((!ShowOnTimeTasks) || (!ShowOverdueTasks))
        {
            projectParameters = new object[1, 3];
            projectParameters[0, 0] = "@Now";
            projectParameters[0, 1] = DateTime.Now;

            this.ucTaskList.Grid.QueryParameters = QueryDataParameters.FromArray(projectParameters);
        }

        // Add security condition - display only tasks which are assigned or owned by the current user or which are a part of a project where the current user is authorised to Read from
        whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, ProjectTaskInfoProvider.CombineSecurityWhereCondition(whereCondition, currentUser, SiteName));

        return whereCondition;
    }


    /// <summary>
    /// Creates child controls and ensures updatepanel load container.
    /// </summary>
    protected override void CreateChildControls()
    {
        if ((ucTaskEdit == null) || (ucTaskList == null))
        {
            pnlUpdate.LoadContainer();
            pnlUpdateList.LoadContainer();
        }

        base.CreateChildControls();
    }


    /// <summary>
    /// OnPreRender - Reloads data.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        ReloadData();

        #region "Check hide actions"

        // Check whether actions should be hidden
        if (!this.AllowTaskActions)
        {
            // Hide new task link
            this.pnlListActions.Visible = false;

            // Get current gridview 
            GridView gv = this.ucTaskList.Grid.GridView;
            // Check whether grid contains data and if so hide action column
            if (gv.Rows.Count > 0)
            {
                gv.Columns[0].Visible = false;
            }
        }

        #endregion

        base.OnPreRender(e);
    }

    #endregion


    #region "Security methods"

    /// <summary>
    /// Checks whether current user can modify task.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="modulePermissionType">Module permission type</param>
    /// <param name="sender">Sender object</param>
    void ucTaskEdit_OnCheckPermissionsExtended(string permissionType, string modulePermissionType, CMSAdminControl sender)
    {
        // Get task info for currently deleted task
        ProjectTaskInfo pti = ProjectTaskInfoProvider.GetProjectTaskInfo(ucTaskEdit.ItemID);
        // Check permission only for existing tasks and tasks assigned to some project
        if ((pti != null) && (pti.ProjectTaskProjectID > 0))
        {
            // Keep current user
            CurrentUserInfo cui = CMSContext.CurrentUser;

            // Check access to project permission for modify action
            if ((String.Compare(permissionType, ProjectManagementPermissionType.MODIFY, true) == 0) && ProjectInfoProvider.IsAuthorizedPerProject(pti.ProjectTaskProjectID, ProjectManagementPermissionType.READ, cui))
            {
                // If user is owner or assignee => allow taks edit
                if ((pti.ProjectTaskOwnerID == cui.UserID) || (pti.ProjectTaskAssignedToUserID == cui.UserID))
                {
                    return;
                }
            }

            // Check whether user is allowed to modify task
            if (!ProjectInfoProvider.IsAuthorizedPerProject(pti.ProjectTaskProjectID, permissionType, cui))
            {
                // Set error message to the dialog
                ucTaskEdit.SetError(GetString("pm.project.permission"));
                // Stop edit control processing
                sender.StopProcessing = true;

                // Render dialog
                this.ucPopupDialog.Visible = true;
                // Show dialog
                ucPopupDialog.Show();
                // Update dialog panel
                pnlUpdateList.Update();
            }
        }
    }


    /// <summary>
    /// Checks whether current user can delete task.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="modulePermissionType">Module permission type</param>
    /// <param name="sender">Sender object</param>
    void ucTaskList_OnCheckPermissionsExtended(string permissionType, string modulePermissionType, CMSAdminControl sender)
    {
        int itemID = ucTaskEdit.ItemID;

        // If edit task ItemID is 0, try get from list
        if (itemID == 0)
        {
            itemID = ucTaskList.SelectedItemID;
        }

        // Get task info for currently deleted task
        ProjectTaskInfo pti = ProjectTaskInfoProvider.GetProjectTaskInfo(itemID);
        // Check permission only for existing tasks and tasks assigned to some project
        if ((pti != null) && (pti.ProjectTaskProjectID > 0))
        {
            // Check whether user is allowed to modify or delete task
            if (!ProjectInfoProvider.IsAuthorizedPerProject(pti.ProjectTaskProjectID, permissionType, CMSContext.CurrentUser))
            {
                lblError.Visible = true;
                lblError.Text = GetString("pm.project.permission");
                sender.StopProcessing = true;
            }
        }
    }

    #endregion
}
