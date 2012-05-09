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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.Community;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Groups_Controls_Security_GroupSecurity : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    private bool mNoRolesAvailable = false;
    private string[] allowedPermissions = new string[3] { "createpages", "deletepages", "editpages" };
    protected GroupInfo group = null;
    protected ResourceInfo resGroups = null;

    // HashTable holding information on all permissions that 'OnlyAuthorizedRoles' access is selected for
    private Hashtable onlyAuth = new Hashtable();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Community group id.
    /// </summary>
    public int GroupID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("GroupID"), 0);
        }
        set
        {
            SetValue("GroupID", value);
        }
    }


    /// <summary>
    /// Indicates if control is enabled.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return gridMatrix.Enabled;
        }
        set
        {
            gridMatrix.Enabled = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnPreRender(EventArgs e)
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (this.GroupID > 0)
            {
                // Render permission matrix
                CreateMatrix();
            }
        }

        base.OnPreRender(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        // Get group resource info
        resGroups = ResourceInfoProvider.GetResourceInfo("CMS.Groups");
        if (resGroups != null)
        {
            // Retrive permission matrix data
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@ID", resGroups.ResourceId);
            parameters.Add("@GroupID", this.GroupID);
            parameters.Add("@SiteID", CMSContext.CurrentSiteID);

            // Setup WHERE condition
            string where = "RoleGroupID=" + this.GroupID.ToString() + "AND PermissionDisplayInMatrix = 0";

            // Setup grid control
            gridMatrix.QueryParameters = parameters;
            gridMatrix.WhereCondition = where;
            gridMatrix.ContentBefore = "<table class=\"PermissionMatrix\" cellspacing=\"0\" cellpadding=\"0\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\">";
            gridMatrix.ContentAfter = "</table>";

            gridMatrix.OnItemChanged += new UniMatrix.ItemChangedEventHandler(gridMatrix_OnItemChanged);

            // Disable permission matrix if user has no MANAGE rights
            if (!CMSContext.CurrentUser.IsGroupAdministrator(this.GroupID))
            {
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_MANAGE))
                {
                    this.Enabled = false;
                    gridMatrix.Enabled = false;
                    lblError.Text = String.Format(ResHelper.GetString("CMSSiteManager.AccessDeniedOnPermissionName"), "Manage");
                    lblError.Visible = true;
                }
            }
        }

    }


    void gridMatrix_OnItemChanged(object sender, int roleID, int permissionID, bool allow)
    {
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return;
        }

        if (allow)
        {
            GroupRolePermissionInfoProvider.AddRoleToGroup(roleID, this.GroupID, permissionID);
        }
        else
        {
            GroupRolePermissionInfoProvider.RemoveRoleFromGroup(roleID, this.GroupID, permissionID);
        }
    }

    #endregion



    /// <summary>
    /// Generates the permission matrix for the cutrrent group.
    /// </summary>
    private void CreateMatrix()
    {
        // Get group resource info 
        if (resGroups == null)
        {
            resGroups = ResourceInfoProvider.GetResourceInfo("CMS.Groups");
        }

        if (resGroups != null)
        {
            group = GroupInfoProvider.GetGroupInfo(this.GroupID);

            // Get permissions for the current group resource                       
            DataSet permissions = PermissionNameInfoProvider.GetResourcePermissions(resGroups.ResourceId);
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
                newHeaderCell.CssClass = "MatrixHeader";
                newHeaderCell.Attributes["style"] = "width:30%;";
                headerRow.Cells.Add(newHeaderCell);

                foreach (string permission in allowedPermissions)
                {
                    DataRow[] drArray = permissions.Tables[0].DefaultView.Table.Select("PermissionName = '" + permission + "'");
                    if ((drArray != null) && (drArray.Length > 0))
                    {
                        DataRow dr = drArray[0];
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.CssClass = "MatrixHeader";
                        newHeaderCell.Attributes["style"] = "width:18%;text-align:center;white-space:nowrap;";
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
                // Insert the empty cell at the end
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "&nbsp;";
                headerRow.Cells.Add(newHeaderCell);
                tblMatrix.Rows.Add(headerRow);

                // Render group access permissions
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

                    // Generate cell holding access item name
                    newRow = new TableRow();
                    newRow.CssClass = ((rowIndex % 2 == 0) ? "EvenRow" : "OddRow");
                    newCell = new TableCell();
                    newCell.CssClass = "MatrixHeader";
                    newCell.Text = accessNames[access, 0].ToString();
                    newCell.Wrap = false;
                    newRow.Cells.Add(newCell);
                    rowIndex++;

                    // Render the permissions access items
                    bool isAllowed = false;
                    int permissionIndex = 0;
                    for (int permission = 0; permission < (tblMatrix.Rows[0].Cells.Count - 2); permission++)
                    {
                        newCell = new TableCell();
                        newCell.CssClass = "MatrixCell";
                        newCell.HorizontalAlign = HorizontalAlign.Center;

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
                        string disabled = null;
                        if (!this.Enabled)
                        {
                            disabled = "disabled=\"disabled\"";
                        }
                        newCell.Text = "<label style=\"display:none;\" for=\"" + elemId + "\">" + permissionText + "</label><input type=\"radio\" id=\"" + elemId + "\" name=\"" + permissionText + "\" " + disabled + " onclick=\"" +
                         ControlsHelper.GetPostBackEventReference(this, permission.ToString() + ";" + Convert.ToInt32(currentAccess).ToString()) + "\" " +
                            ((isAllowed) ? "checked = \"checked\"" : "") + "/>";

                        newCell.Wrap = false;
                        newRow.Cells.Add(newCell);
                        permissionIndex++;
                    }

                    newCell = new TableCell();
                    newCell.Text = "&nbsp;";
                    newRow.Cells.Add(newCell);
                    // Add the access row to the table
                    tblMatrix.Rows.Add(newRow);
                }

                // Get permission matrix for current group resource
                bool rowIsSeparator = false;

                // Get permission matrix for the current group resource
                this.mNoRolesAvailable = !gridMatrix.HasData;

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
                    newCell.Attributes.Add("colspan", Convert.ToString(tblMatrix.Rows[0].Cells.Count - 1));
                    newRow.Controls.Add(newCell);
                    tblMatrix.Rows.Add(newRow);
                }

                // Add the latest row if present
                if (newRow != null)
                {
                    // The row is only role row and at the same time is divider between accesses section and roles section - make border higher
                    if (rowIsSeparator)
                    {
                        rowIsSeparator = false;
                    }
                    if (!mNoRolesAvailable)
                    {
                        newRow.Cells.Add(new TableCell());
                        tblMatrix.Rows.Add(newRow);
                    }
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

        if (group != null)
        {
            switch (currentPermission)
            {
                case 0:
                    // Process 'AllowCreate' permission and check by current access
                    result = (group.AllowCreate == currentAccess);
                    break;

                case 1:
                    // Process 'AllowDelete' permission and check by current access
                    result = (group.AllowDelete == currentAccess);
                    break;

                case 2:
                    // Process 'AllowModify' permission and check by current access
                    result = (group.AllowModify == currentAccess);
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


    #region "PostBack event handler"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return;
        }

        string[] args = eventArgument.Split(';');

        if (args.Length == 2)
        {
            // Get info on currently selected item
            int permission = Convert.ToInt32(args[0]);
            int access = Convert.ToInt32(args[1]);

            GroupInfo group = GroupInfoProvider.GetGroupInfo(this.GroupID);
            if (group != null)
            {
                // Update forum permission access information
                switch (permission)
                {
                    case 0:
                        // Set 'AllowCreate' permission to specified access
                        group.AllowCreate = (SecurityAccessEnum)access;
                        break;

                    case 1:
                        // Set 'AllowDelete' permission to specified access
                        group.AllowDelete = ((SecurityAccessEnum)access);
                        break;

                    case 2:
                        // Set 'AllowModify' permission to specified access
                        group.AllowModify = (SecurityAccessEnum)access;
                        break;

                    default:
                        break;
                }

                // Save changes to the forum
                GroupInfoProvider.SetGroupInfo(group);
            }
        }
    }

    #endregion
}
