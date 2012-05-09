using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.ProjectManagement;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_ProjectManagement_Controls_UI_Project_Security : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    private int mProjectId;
    private bool mNoRolesAvailable = false;
    private bool process = true;
    private bool createMatrix = false;
    private bool stopProcessing = false;
    private bool mEnable = true;

    private string[] allowedPermissions = new string[4] {
            ProjectManagementPermissionType.READ,
            ProjectManagementPermissionType.CREATE,
            ProjectManagementPermissionType.MODIFY,
            ProjectManagementPermissionType.DELETE
    };

    protected ProjectInfo project = null;
    protected ResourceInfo resProjects = null;

    /// <summary>
    /// OnPage changed event.
    /// </summary>
    public event EventHandler OnPageChanged;


    // HashTable holding information on all permissions that 'OnlyAuthorizedRoles' access is selected for
    private Hashtable onlyAuth = new Hashtable();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the project to edit.
    /// </summary>
    public int ProjectID
    {
        get
        {
            return this.mProjectId;
        }
        set
        {
            this.mProjectId = value;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return mStopProcessing;
        }
        set
        {
            gridMatrix.StopProcessing = value;
            mStopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates whether permissions matrix is enabled.
    /// </summary>
    public bool Enable
    {
        get
        {
            return this.mEnable;
        }
        set
        {
            this.mEnable = value;
        }
    }

    #endregion


    #region "Page methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        gridMatrix.OnItemChanged += new UniMatrix.ItemChangedEventHandler(gridMatrix_OnItemChanged);
        gridMatrix.StopProcessing = true;
        gridMatrix.ColumnsPreferedOrder = string.Join(",", allowedPermissions);


        if (this.ProjectID > 0)
        {
            project = ProjectInfoProvider.GetProjectInfo(this.ProjectID);

            // Check whether the project still exists
            EditedObject = project;
        }

        // Handle page chnaged event
        gridMatrix.OnPageChanged += new EventHandler<EventArgs>(gridMatrix_OnPageChanged);

        // Disable permission matrix if user has no MANAGE rights
        if (!ProjectInfoProvider.IsAuthorizedPerProject(this.ProjectID, CMSAdminControl.PERMISSION_MANAGE, CMSContext.CurrentUser))
        {
            this.Enable = false;
            gridMatrix.Enabled = false;
            lblError.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnPermissionName"), "Manage");
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        LoadData();
        if (this.ProjectID > 0)
        {
            this.gridMatrix.StopProcessing = true; ;
            if (!this.IsLiveSite && process)
            {
                this.gridMatrix.StopProcessing = stopProcessing;
                // Render permission matrix
                CreateMatrix();
            }
            else if (createMatrix)
            {
                this.gridMatrix.StopProcessing = stopProcessing;
                CreateMatrix();
                createMatrix = false;
            }
            else if (this.IsLiveSite && process && RequestHelper.IsPostBack())
            {
                this.gridMatrix.StopProcessing = stopProcessing;
                CreateMatrix();
                createMatrix = false;
            }
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Load data.
    /// </summary>
    public void LoadData()
    {
        process = true;
        if (!this.Visible || StopProcessing)
        {
            this.EnableViewState = false;
            process = false;
        }

        this.IsLiveSite = false;

        if (this.ProjectID > 0)
        {
            // Get information on current project
            project = ProjectInfoProvider.GetProjectInfo(this.ProjectID);
        }

        // Get project resource
        resProjects = ResourceInfoProvider.GetResourceInfo("CMS.ProjectManagement");

        if ((resProjects != null) && (project != null))
        {
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@ID", resProjects.ResourceId);
            parameters.Add("@ProjectID", project.ProjectID);
            parameters.Add("@SiteID", CMSContext.CurrentSiteID);

            string where = "";
            int groupId = project.ProjectGroupID;

            // Build where condition
            if (groupId > 0)
            {
                where = "RoleGroupID=" + groupId.ToString() + " AND PermissionDisplayInMatrix = 0";
            }
            else
            {
                where = "RoleGroupID IS NULL AND PermissionDisplayInMatrix = 0";
            }

            // Setup matrix control    
            gridMatrix.IsLiveSite = this.IsLiveSite;
            gridMatrix.QueryParameters = parameters;
            gridMatrix.WhereCondition = where;
            gridMatrix.ContentBefore = "<table class=\"PermissionMatrix\" cellspacing=\"0\" cellpadding=\"0\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\">";
            gridMatrix.ContentAfter = "</table>";
        }

    }


    /// <summary>
    /// Page changed event habdler.
    /// </summary>
    void gridMatrix_OnPageChanged(object sender, EventArgs e)
    {
        // Raise on page changed event
        if (OnPageChanged != null)
        {
            OnPageChanged(this, null);
        }
    }


    /// <summary>
    /// Clears the security matrix.
    /// </summary>
    public void Clear()
    {
        gridMatrix.ResetMatrix();
    }


    #endregion


    #region "Page event handlers"

    /// <summary>
    /// On item changed event.
    /// </summary>    
    protected void gridMatrix_OnItemChanged(object sender, int roleId, int permissionId, bool allow)
    {
        if (!CheckPermissions("CMS.ProjectManagement", CMSAdminControl.PERMISSION_MANAGE))
        {
            return;
        }

        // Delete permission hash tables
        ProjectInfoProvider.ClearProjectPermissionTable(this.ProjectID, CMSContext.CurrentUser);

        if (allow)
        {
            ProjectRolePermissionInfoProvider.AddRelationship(this.ProjectID, roleId, permissionId);
        }
        else
        {
            ProjectRolePermissionInfoProvider.RemoveRelationship(this.ProjectID, roleId, permissionId);
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Generates the permission matrix for the cutrrent project.
    /// </summary>
    private void CreateMatrix()
    {
        // Get project resource info     
        if (resProjects == null)
        {
            resProjects = ResourceInfoProvider.GetResourceInfo("CMS.ProjectManagement");
        }

        // Get project object
        if ((project == null) && (ProjectID > 0))
        {
            project = ProjectInfoProvider.GetProjectInfo(this.ProjectID);
        }

        if ((resProjects != null) && (project != null))
        {
            // Get permissions for the current project resource                       
            DataSet permissions = PermissionNameInfoProvider.GetResourcePermissions(resProjects.ResourceId);
            if (DataHelper.DataSourceIsEmpty(permissions))
            {
                lblInfo.Text = GetString("general.emptymatrix");
            }
            else
            {
                TableRow headerRow = new TableRow();
                headerRow.CssClass = "UniGridHead";
                TableCell newCell = new TableCell();
                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "&nbsp;";
                newHeaderCell.Attributes["style"] = "width:200px;";
                headerRow.Cells.Add(newHeaderCell);

                foreach (string permission in allowedPermissions)
                {
                    DataRow[] drArray = permissions.Tables[0].DefaultView.Table.Select("PermissionName = '" + permission + "'");
                    if ((drArray != null) && (drArray.Length > 0))
                    {
                        DataRow dr = drArray[0];
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.Attributes["style"] = "text-align:center;white-space:nowrap;";
                        newHeaderCell.Text = dr["PermissionDisplayName"].ToString();
                        newHeaderCell.ToolTip = dr["PermissionDescription"].ToString();
                        newHeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        headerRow.Cells.Add(newHeaderCell);
                    }
                    else
                    {
                        throw new Exception("[Security matrix] Column '" + permission + "' cannot be found.");
                    }
                }
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "&nbsp;";
                headerRow.Cells.Add(newHeaderCell);

                tblMatrix.Rows.Add(headerRow);

                // Render project access permissions
                object[,] accessNames = new object[5, 2];
                accessNames[0, 0] = GetString("security.nobody");
                accessNames[0, 1] = SecurityAccessEnum.Nobody;
                accessNames[1, 0] = GetString("security.allusers");
                accessNames[1, 1] = SecurityAccessEnum.AllUsers;
                accessNames[2, 0] = GetString("security.authenticated");
                accessNames[2, 1] = SecurityAccessEnum.AuthenticatedUsers;
                accessNames[3, 0] = GetString("security.groupmembers");
                accessNames[3, 1] = SecurityAccessEnum.GroupMembers;
                accessNames[4, 0] = GetString("security.authorizedroles");
                accessNames[4, 1] = SecurityAccessEnum.AuthorizedRoles;

                TableRow newRow = null;
                int rowIndex = 0;
                for (int access = 0; access <= accessNames.GetUpperBound(0); access++)
                {
                    SecurityAccessEnum currentAccess = ((SecurityAccessEnum)accessNames[access, 1]);

                    // If the security isn't displayed as part of group section
                    if ((currentAccess == SecurityAccessEnum.GroupMembers) && (project.ProjectGroupID == 0))
                    {
                        // Do not render this access item
                    }
                    else
                    {
                        // Generate cell holding access item name
                        newRow = new TableRow();
                        newRow.CssClass = ((rowIndex % 2 == 0) ? "EvenRow" : "OddRow");
                        newCell = new TableCell();
                        newCell.Text = accessNames[access, 0].ToString();
                        newCell.Wrap = false;
                        newCell.CssClass = "MatrixHeader";
                        newCell.Width = new Unit(28, UnitType.Percentage);
                        newRow.Cells.Add(newCell);
                        rowIndex++;

                        // Render the permissions access items
                        bool isAllowed = false;
                        bool isDisabled = (!this.Enable);
                        int permissionIndex = 0;
                        for (int permission = 0; permission < (tblMatrix.Rows[0].Cells.Count - 2); permission++)
                        {
                            newCell = new TableCell();

                            // Check if the currently processed access is applied for permission
                            isAllowed = CheckPermissionAccess(currentAccess, permission, tblMatrix.Rows[0].Cells[permission + 1].Text);

                            // Disable column in roles grid if needed
                            if ((currentAccess == SecurityAccessEnum.AuthorizedRoles) && !isAllowed)
                            {
                                gridMatrix.DisableColumn(permissionIndex);
                            }

                            // Insert the radio button for the current permission
                            string permissionText = tblMatrix.Rows[0].Cells[permission + 1].Text;
                            string elemId = ClientID + "_" + permission + "_" + access;
                            newCell.Text = "<label style=\"display:none;\" for=\"" + elemId + "\">" + permissionText + "</label><input type=\"radio\" id=\"" + elemId + "\" name=\"" + permissionText + "\" onclick=\"" +
                                ControlsHelper.GetPostBackEventReference(this, permission.ToString() + ";" + Convert.ToInt32(currentAccess).ToString()) + "\" " +
                                ((isAllowed) ? "checked = \"checked\"" : "") + ((isDisabled) ? " disabled=\"disabled\"" : "") + "/>";

                            newCell.Wrap = false;
                            newCell.Width = new Unit(12, UnitType.Percentage);
                            newCell.HorizontalAlign = HorizontalAlign.Center;
                            newRow.Cells.Add(newCell);
                            permissionIndex++;
                        }

                        newCell = new TableCell();
                        newCell.Text = "&nbsp;";
                        newRow.Cells.Add(newCell);

                        // Add the access row to the table
                        tblMatrix.Rows.Add(newRow);
                    }
                }

                // Check if project has some roles assigned           
                this.mNoRolesAvailable = !gridMatrix.HasData;

                // Get permission matrix for current project resource
                if (!this.mNoRolesAvailable)
                {
                    // Security - Role separator
                    newRow = new TableRow();
                    newCell = new TableCell();
                    newCell.Text = "&nbsp;";
                    newCell.Attributes.Add("colspan", Convert.ToString(tblMatrix.Rows[0].Cells.Count));
                    newRow.Controls.Add(newCell);
                    tblMatrix.Rows.Add(newRow);

                    // Security - Role separator text
                    newRow = new TableRow();
                    newCell = new TableCell();
                    newCell.CssClass = "MatrixLabel";
                    newCell.Text = GetString("SecurityMatrix.RolesAvailability");
                    newCell.Attributes.Add("colspan", Convert.ToString(tblMatrix.Rows[0].Cells.Count));
                    newRow.Controls.Add(newCell);
                    tblMatrix.Rows.Add(newRow);
                }
            }
        }
    }


    /// <summary>
    /// Indicates the permission acess.
    /// </summary>
    /// <param name="currentAccess">Currently processed integer representation of item from SecurityAccessEnum</param>    
    /// <param name="currentPermission">Currently processed integer representation of permission to check</param>    
    private bool CheckPermissionAccess(SecurityAccessEnum currentAccess, int currentPermission, string currentPermissionName)
    {
        bool result = false;

        if (project != null)
        {
            switch (currentPermission)
            {
                case 0:
                    // Process 'AllowRead' permission and check by current access
                    result = (project.AllowRead == currentAccess);
                    break;

                case 1:
                    // Set 'AttachCreate' permission and check by current access
                    result = (project.AllowCreate == currentAccess);
                    break;

                case 2:
                    // Set 'AllowModify' permission and check by current access
                    result = (project.AllowModify == currentAccess);
                    break;

                case 3:
                    // Set 'AllowDelete' permission and check by current access
                    result = (project.AllowDelete == currentAccess);
                    break;

                default:
                    break;
            }
        }

        // Make note about type of permission with access set to 'OnlyAuthorizedRoles'
        if (result && (currentAccess == SecurityAccessEnum.AuthorizedRoles))
        {
            this.onlyAuth[currentPermissionName] = true;
        }
        return result;
    }

    #endregion


    #region "PostBack event handler"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (!CheckPermissions("CMS.ProjectManagement", CMSAdminControl.PERMISSION_MANAGE))
        {
            return;
        }

        string[] args = eventArgument.Split(';');
        if (args.Length == 2)
        {

            // Get info on currently selected item
            int permission = ValidationHelper.GetInteger(args[0], 0);
            int access = ValidationHelper.GetInteger(args[1], 0);

            if (project != null)
            {
                // Update project permission access information
                switch (permission)
                {
                    case 0:
                        // Set 'AllowRead' permission to specified access
                        project.AllowRead = (SecurityAccessEnum)access;
                        break;

                    case 1:
                        // Set 'AttachCreate' permission to specified access
                        project.AllowCreate = ((SecurityAccessEnum)access);
                        break;

                    case 2:
                        // Set 'AllowModify' permission to specified access
                        project.AllowModify = (SecurityAccessEnum)access;
                        break;

                    case 3:
                        // Set 'AllowDelete' permission to specified access
                        project.AllowDelete = ((SecurityAccessEnum)access);
                        break;

                    default:
                        break;
                }

                // Delete permission hash tables
                ProjectInfoProvider.ClearProjectPermissionTable(this.ProjectID, CMSContext.CurrentUser);

                // Use try/catch due to license check
                try
                {
                    // Save changes to the project
                    ProjectInfoProvider.SetProjectInfo(project);
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                }

                createMatrix = true;
            }
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        createMatrix = true;

        // Ensure viewstate
        this.EnableViewState = true;
    }

    #endregion
}
