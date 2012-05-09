using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ProjectManagement;
using CMS.Synchronization;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_ProjectManagement_Controls_LiveControls_GroupTaskEdit : CMSAdminControl
{
    #region "Variables"

    bool displayControlPerformed = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the ProjectID value.
    /// </summary>
    public int ProjectID
    {
        get
        {
            return ucTaskList.ProjectID;
        }
        set
        {
            this.EnsureChildControls();
            ucTaskList.ProjectID = value;
            ucTaskEdit.ProjectID = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of selected task.
    /// </summary>
    public int SelectedTaskID
    {
        get
        {
            return ucTaskList.SelectedItemID;
        }
        set
        {
            this.EnsureChildControls();
            ucTaskList.SelectedItemID = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether is live site.
    /// </summary>f
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            this.EnsureChildControls();
            base.IsLiveSite = value;
            ucTaskList.IsLiveSite = value;
            ucTaskEdit.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"


    protected void Page_Load(object sender, EventArgs e)
    {
        // Handle control events
        ucTaskList.OnAction += new CommandEventHandler(ucTaskList_OnAction);
        ucTaskEdit.OnSaved += new EventHandler(ucTaskEdit_OnSaved);

        // Breadcrumbs for edit
        lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";
        lnkEditBack.Text = GetString("pm.tasks");
        lnkEditBack.Click += new EventHandler(lnkEditBack_Click);

        #region "New task link"

        // New item link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("pm.projecttask.new");
        actions[0, 2] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/PM_Project/add.png");
        actions[0, 6] = "new_task";

        actionsElem.Actions = actions;
        actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

        #endregion

        // Set control properties
        ucTaskList.UsePostbackOnEdit = true;
        ucTaskList.OrderByType = ProjectTaskOrderByEnum.ProjectOrder;
        ucTaskEdit.OnSaved += new EventHandler(ucTaskEdit_OnSaved);

        ucTaskEdit.OnCheckPermissions += new CheckPermissionsEventHandler(controls_OnCheckPermissions);
        ucTaskList.OnCheckPermissions += new CheckPermissionsEventHandler(controls_OnCheckPermissions);
    }


    /// <summary>
    /// Check permission handler.
    /// </summary>
    void controls_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(this.ProjectID);
        if (pi != null)
        {
            // If user is group admin => allow all actions
            if (!CMSContext.CurrentUser.IsGroupAdministrator(pi.ProjectGroupID))
            {
                sender.StopProcessing = true;
            }
        }
    }


    /// <summary>
    /// Task saved event handler.
    /// </summary>
    void ucTaskEdit_OnSaved(object sender, EventArgs e)
    {
        // Set task id
        this.SelectedTaskID = ucTaskEdit.ItemID;
        // Display edit control
        DisplayControl("edit");
    }


    /// <summary>
    /// Edit clicked on list.
    /// </summary>
    void ucTaskList_OnAction(object sender, CommandEventArgs e)
    {
        // Switch by command name
        switch (e.CommandName.ToString())
        {
            // Edit
            case "edit":
                // Set task id from command argument
                int taskID = ValidationHelper.GetInteger(e.CommandArgument, 0);
                this.ucTaskEdit.ItemID = taskID;
                this.SelectedTaskID = taskID;
                // Display edit control
                DisplayControl("edit");
                break;
        }
    }


    /// <summary>
    /// New task click handler.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Swrich by command name
        switch (e.CommandName.ToLower())
        {
            case "new_task":
                // Display new control
                DisplayControl("new");
                break;
        }
    }


    /// <summary>
    /// Display given control.
    /// </summary>
    /// <param name="control">Type of control to display</param>
    private void DisplayControl(string control)
    {
        // Set display performed falg
        displayControlPerformed = true;

        // Hide all controls 
        pnlEdit.Visible = false;
        plcList.Visible = false;

        // Switch by display control type
        switch (control.ToLower())
        {
            // List
            case "list":
                plcList.Visible = true;
                ucTaskList.ProjectID = ProjectID;
                ucTaskList.ReloadData();
                break;

            // New
            case "new":
                pnlEdit.Visible = true;
                ucTaskEdit.ProjectID = ProjectID;
                SelectedTaskID = -1;
                SetBreadcrumbs(0);
                ucTaskEdit.ItemID = 0;
                ucTaskEdit.ClearForm();
                ucTaskEdit.ReloadData(true);
                break;

            // Edit
            case "edit":
                pnlEdit.Visible = true;
                ucTaskEdit.ProjectID = ProjectID;
                SetBreadcrumbs(SelectedTaskID);
                ucTaskEdit.ClearForm();
                ucTaskEdit.ReloadData(true);
                break;
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // If display method wasn't called ensure default displaying with dependence on selected item on tab control
        if (!displayControlPerformed)
        {
            switch (this.SelectedTaskID)
            {
                // Default list
                case 0:
                    DisplayControl("list");
                    break;

                // New item 
                case -1:
                    SetBreadcrumbs(0);
                    break;

                // Edit item
                default:
                    SetBreadcrumbs(this.SelectedTaskID);
                    break;
            }
        }
    }


    /// <summary>
    /// Clear the selection.
    /// </summary>
    public void ClearControl()
    {
        // Un-select current task if exist
        this.SelectedTaskID = 0;
        // Display list control by default
        DisplayControl("list");
    }


    /// <summary>
    /// Sets breacrumbs for edit.
    /// </summary>
    private void SetBreadcrumbs(int projectTaskID)
    {
        // If project task is defined display task specific breadcrumbs
        if (projectTaskID != 0)
        {
            // Load project name
            ProjectTaskInfo pi = ProjectTaskInfoProvider.GetProjectTaskInfo(projectTaskID);
            if (pi != null)
            {
                lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span>" + HTMLHelper.HTMLEncode(pi.ProjectTaskDisplayName);
            }
        }
        // Dsiplay new task breadcrumb
        else
        {
            lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span>" + GetString("pm.projecttask.new");
        }
    }


    /// <summary>
    /// Breadcrumbs back link event handler.
    /// </summary>
    void lnkEditBack_Click(object sender, EventArgs e)
    {
        // Display list
        DisplayControl("list");
    }

    #endregion
}
