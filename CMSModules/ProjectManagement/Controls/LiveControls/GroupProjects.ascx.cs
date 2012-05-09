using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;
using CMS.CMSHelper;

public partial class CMSModules_ProjectManagement_Controls_LiveControls_GroupProjects : CMSAdminControl
{
    #region "Variables"

    private bool displayControlPerformed = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the community group id.
    /// </summary>
    public int CommunityGroupID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("CommunityGroupID"), 0);
        }
        set
        {
            this.SetValue("CommunityGroupID", value);
        }
    }


    /// <summary>
    /// Gets or sets the display mode of the control.
    /// </summary>
    public override ControlDisplayModeEnum DisplayMode
    {
        get
        {
            return base.DisplayMode;
        }
        set
        {
            base.DisplayMode = value;
            ucProjectNew.DisplayMode = value;
            ucProjectEdit.DisplayMode = value;
            ucProjectList.DisplayMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("IsLiveSite"), base.IsLiveSite);
        }
        set
        {
            base.IsLiveSite = value;
            this.SetValue("IsLiveSite", value);
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        #region "New project link"

        // New item link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("pm.project.new");
        actions[0, 2] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/PM_Project/add.png");
        actions[0, 6] = "new_project";

        actionsElem.Actions = actions;
        this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

        #endregion

        // Breadcrumbs back handlers
        this.lnkEditBack.Click += new EventHandler(lnkEditBack_Click);
        this.lnkNewBack.Click += new EventHandler(lnkEditBack_Click);

        // Breadcrumb strings
        lnkEditBack.Text = GetString("pm.project.list");
        lnkNewBack.Text = GetString("pm.project.list");

        // List settings
        ucProjectList.UsePostbackOnEdit = true;
        ucProjectList.OnAction += new CommandEventHandler(ucProjectList_OnAction);
        ucProjectList.CommunityGroupID = this.CommunityGroupID;

        // New item settings
        ucProjectNew.CommunityGroupID = this.CommunityGroupID;
        ucProjectNew.OnSaved += new EventHandler(ucProjectNew_OnSaved);

        ucProjectNew.OnCheckPermissions += new CheckPermissionsEventHandler(controls_OnCheckPermissions);
        ucProjectList.OnCheckPermissions += new CheckPermissionsEventHandler(controls_OnCheckPermissions);
        ucProjectList.OnDelete += new EventHandler(ucProjectList_OnDelete);
    }


    /// <summary>
    /// OnPreRender - ensures defautl displaying.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Display list by default
        if ((!displayControlPerformed) && (!RequestHelper.IsPostBack()))
        {
            DisplayControl("list");
        }

        base.OnPreRender(e);
    }

    #endregion


    #region "Event handler methods"

    /// <summary>
    /// New task created event handler => Display edit page
    /// </summary>
    protected void ucProjectNew_OnSaved(object sender, EventArgs e)
    {
        // Set breadcrumb to currently created project
        SetBreadcrumbs(ucProjectNew.ItemID);
        // Set current project id
        this.ucProjectEdit.ProjectID = ucProjectNew.ItemID;
        // Display edit controls
        DisplayControl("edit");
    }


    /// <summary>
    /// List action event handler.
    /// </summary>
    protected void ucProjectList_OnAction(object sender, CommandEventArgs e)
    {
        // Switch by command name
        switch (e.CommandName.ToString())
        {
            case "edit":
                // Get project id from command argument
                int projectID = ValidationHelper.GetInteger(e.CommandArgument, 0);
                // Set breadcrumbs to edited project
                SetBreadcrumbs(projectID);
                // Set project id
                this.ucProjectEdit.ProjectID = projectID;
                // Display edit controls
                DisplayControl("edit");
                break;
        }
    }


    /// <summary>
    /// Breadcrumbs back link click handler.
    /// </summary>
    protected void lnkEditBack_Click(object sender, EventArgs e)
    {
        // Clear edit form
        ucProjectEdit.ClearForm();
        // Display project list
        DisplayControl("list");
    }


    /// <summary>
    /// New project link click handler.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Switch by command name
        switch (e.CommandName.ToLower())
        {
            // New project
            case "new_project":
                // Set breadcrumbs to new project
                SetBreadcrumbs(0);
                // Display new project controls
                DisplayControl("new");
                break;
        }
    }


    /// <summary>
    /// Check permission handler.
    /// </summary>
    protected void controls_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // If user is group admin => allow all actions
        if (!CMSContext.CurrentUser.IsGroupAdministrator(this.CommunityGroupID))
        {
            sender.StopProcessing = true;
        }
    }


    /// <summary>
    /// Handles the OnDelete event of the ucProjectList control.
    /// </summary>
    protected void ucProjectList_OnDelete(object sender, EventArgs e)
    {
        // Clear the ProjectID of the Edit control otherwise it could redirect you to the "Edited object no longer exists" page
        ucProjectEdit.ProjectID = 0;
    }

    #endregion


    #region "General Methods"

    /// <summary>
    /// Display control.
    /// </summary>
    /// <param name="control">Type of displayed control</param>
    public void DisplayControl(string control)
    {
        // Set displaying flag
        displayControlPerformed = true;

        // Hide all controls
        plcEdit.Visible = false;
        plcNew.Visible = false;
        pnlBody.Visible = false;

        // Switch by control type
        switch (control.ToLower())
        {
            // List
            case "list":
                pnlBody.Visible = true;
                ucProjectList.ReloadData();
                break;

            // Edit
            case "edit":
                plcEdit.Visible = true;
                ucProjectEdit.ReloadData();
                break;

            // New
            case "new":
                plcNew.Visible = true;
                ucProjectNew.ReloadData();
                break;
        }
    }


    /// <summary>
    /// Sets breadcrumbs for project.
    /// </summary>
    /// <param name="projectID">ID of project</param>
    private void SetBreadcrumbs(int projectID)
    {
        // If project id is defined display actual project name
        if (projectID != 0)
        {
            // Load project info
            ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(projectID);
            // Check whether project info is defined
            if (pi != null)
            {
                lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span>" + HTMLHelper.HTMLEncode(pi.ProjectDisplayName);
                lblNewBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span>" + HTMLHelper.HTMLEncode(pi.ProjectDisplayName);
            }
        }
        // Display breadcrumbs for ne project
        else
        {
            lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span>" + GetString("pm.project.new");
            lblNewBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span>" + GetString("pm.project.new");
        }
    }


    /// <summary>
    /// Sets the property value of the control, setting the value affects only local property value.
    /// </summary>
    /// <param name="propertyName">Property name to set</param>
    /// <param name="value">New property value</param>
    public override void SetValue(string propertyName, object value)
    {
        // Community group id
        if (String.Compare(propertyName, "CommunityGroupID", true) == 0)
        {
            int groupId = ValidationHelper.GetInteger(value, 0);
            ucProjectList.CommunityGroupID = groupId;
            ucProjectNew.CommunityGroupID = groupId;
            ucProjectEdit.CommunityGroupID = groupId;
        }
        // Is livesite
        else if (String.Compare(propertyName, "IsLiveSite", true) == 0)
        {
            bool isLiveSite = ValidationHelper.GetBoolean(value, base.IsLiveSite);
            ucProjectEdit.IsLiveSite = isLiveSite;
            ucProjectList.IsLiveSite = isLiveSite;
            ucProjectNew.IsLiveSite = isLiveSite;
        }

        // Call base method
        base.SetValue(propertyName, value);
    }

    #endregion

}
