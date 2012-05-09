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
using CMS.Forums;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Controls_Forums_ForumSecurity : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    private int mForumId;
    private bool mIsGroupForum = false;
    private bool mNoRolesAvailable = false;
    private bool process = true;
    private bool createMatrix = false;
    private bool mEnable = true;

    private string[] allowedPermissions = new string[6] { "accesstoforum", "attachfiles", "markasanswer", "post", "reply", "subscribe" };

    protected ForumInfo forum = null;
    protected ResourceInfo resForums = null;

    // HashTable holding information on all permissions that 'OnlyAuthorizedRoles' access is selected for
    Hashtable onlyAuth = new Hashtable();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the forum to edit.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }


    /// <summary>
    /// Indicates whether the forum security is displayed as a part of group module.
    /// </summary>
    public bool IsGroupForum
    {
        get
        {
            return this.mIsGroupForum;
        }
        set
        {
            this.mIsGroupForum = value;
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


    #region "Page events"

    protected override void OnPreRender(EventArgs e)
    {
        if (this.ForumID > 0)
        {
            this.chkChangeName.Checked = forum.ForumAllowChangeName;
            
            this.gridMatrix.StopProcessing = true;

            if (!this.IsLiveSite && process)
            {
                this.gridMatrix.StopProcessing = false;
                // Render permission matrix
                CreateMatrix();
            }
            else if (createMatrix)
            {
                this.gridMatrix.StopProcessing = false;
                CreateMatrix();
                createMatrix = false;
            }
            else if (this.IsLiveSite && process && RequestHelper.IsPostBack())
            {
                this.gridMatrix.StopProcessing = false;
                CreateMatrix();
                createMatrix = false;
            }
        }

        base.OnPreRender(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        process = true;
        if (!this.Visible || StopProcessing)
        {
            this.EnableViewState = false;
            process = false;
        }

        this.chkChangeName.CheckedChanged += new EventHandler(chkChangeName_CheckedChanged);

        if (this.ForumID > 0)
        {
            // Get information on current forum
            forum = ForumInfoProvider.GetForumInfo(this.ForumID);

            // Check whether the forum still exists
            EditedObject = forum;
        }

        // Get forum resource
        resForums = ResourceInfoProvider.GetResourceInfo("CMS.Forums");

        if ((resForums != null) && (forum != null))
        {
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@ID", resForums.ResourceId);
            parameters.Add("@ForumID", forum.ForumID);
            parameters.Add("@SiteID", CMSContext.CurrentSiteID);

            string where = "";
            int groupId = 0;
            if (this.IsGroupForum)
            {
                ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(forum.ForumGroupID);
                groupId = fgi.GroupGroupID;
            }

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
            gridMatrix.OnItemChanged += new UniMatrix.ItemChangedEventHandler(gridMatrix_OnItemChanged);

            // Disable permission matrix if user has no MANAGE rights
            if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
            {
                this.Enable = false;
                gridMatrix.Enabled = false;
                lblError.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnPermissionName"), "Manage");
                lblError.Visible = true;
            }
        }
    }

    #endregion


    /// <summary>
    /// Change name checkbox handler.
    /// </summary>
    protected void chkChangeName_CheckedChanged(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        if (forum != null)
        {
            forum.ForumAllowChangeName = this.chkChangeName.Checked;
            ForumInfoProvider.SetForumInfo(forum);
        }
    }


    /// <summary>
    /// Generates the permission matrix for the cutrrent forum.
    /// </summary>
    private void CreateMatrix()
    {
        // Get forum resource info     
        if (resForums == null)
        {
            resForums = ResourceInfoProvider.GetResourceInfo("CMS.Forums");
        }

        // Get forum object
        if ((forum == null) && (ForumID > 0))
        {
            forum = ForumInfoProvider.GetForumInfo(this.ForumID);
        }

        if ((resForums != null) && (forum != null))
        {
            // Get permission matrix for roles of the current site/group
            int groupId = 0;
            if (this.IsGroupForum)
            {
                ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(forum.ForumGroupID);
                groupId = fgi.GroupGroupID;
            }

            // Get permissions for the current forum resource                       
            DataSet permissions = PermissionNameInfoProvider.GetResourcePermissions(resForums.ResourceId);
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

                // Render forum access permissions
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
                    if ((currentAccess == SecurityAccessEnum.GroupMembers) && (!this.IsGroupForum))
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
                        bool isDisabled = true;
                        int permissionIndex = 0;
                        for (int permission = 0; permission < (tblMatrix.Rows[0].Cells.Count - 2); permission++)
                        {
                            newCell = new TableCell();

                            // Check if the currently processed access is applied for permission
                            isAllowed = CheckPermissionAccess(currentAccess, permission, tblMatrix.Rows[0].Cells[permission + 1].Text);
                            isDisabled = ((currentAccess == SecurityAccessEnum.AllUsers) && (permission == 1)) || (!this.Enable);

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

                // Check if forum has some roles assigned           
                this.mNoRolesAvailable = !gridMatrix.HasData;

                // Get permission matrix for current forum resource
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
    /// On item changed event.
    /// </summary>    
    void gridMatrix_OnItemChanged(object sender, int roleId, int permissionId, bool allow)
    {
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        if (allow)
        {
            ForumRoleInfoProvider.AddRoleToForum(roleId, this.ForumID, permissionId);
        }
        else
        {
            ForumRoleInfoProvider.RemoveRoleFromForum(roleId, this.ForumID, permissionId);
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

        if (forum != null)
        {
            switch (currentPermission)
            {
                case 0:
                    // Process 'AccessToForum' permission and check by current access
                    result = (forum.AllowAccess == currentAccess);
                    break;

                case 1:
                    // Process 'AttachFiles' permission and check by current access
                    result = (forum.AllowAttachFiles == currentAccess);
                    break;

                case 3:
                    // Process 'Post' permission and check by current access
                    result = (forum.AllowPost == currentAccess);
                    break;

                case 2:
                    // Process 'MarkAsAnswer' permission and check by current access
                    result = (forum.AllowMarkAsAnswer == currentAccess);
                    break;

                case 4:
                    // Process 'Reply' permission and check by current access
                    result = (forum.AllowReply == currentAccess);
                    break;

                case 5:
                    // Process 'Subscribe' permission and check by current access
                    result = (forum.AllowSubscribe == currentAccess);
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
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        string[] args = eventArgument.Split(';');
        if (args.Length == 2)
        {

            // Get info on currently selected item
            int permission = Convert.ToInt32(args[0]);
            int access = Convert.ToInt32(args[1]);

            if (forum != null)
            {
                // Update forum permission access information
                switch (permission)
                {
                    case 0:
                        // Set 'AllowAccess' permission to specified access
                        forum.AllowAccess = (SecurityAccessEnum)access;
                        break;

                    case 1:
                        // Set 'AttachFiles' permission to specified access
                        forum.AllowAttachFiles = ((SecurityAccessEnum)access);
                        break;

                    case 2:
                        // Set 'MarkAsAnswer' permission to specified access
                        forum.AllowMarkAsAnswer = (SecurityAccessEnum)access;
                        break;

                    case 3:
                        // Set 'Post' permission to specified access
                        forum.AllowPost = ((SecurityAccessEnum)access);
                        break;

                    case 4:
                        // Set 'Reply' permission to specified access
                        forum.AllowReply = (SecurityAccessEnum)access;
                        break;

                    case 5:
                        // Set 'Subscribe' permission to specified access
                        forum.AllowSubscribe = (SecurityAccessEnum)access;
                        break;

                    default:
                        break;
                }

                // Save changes to the forum
                ForumInfoProvider.SetForumInfo(forum);

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
