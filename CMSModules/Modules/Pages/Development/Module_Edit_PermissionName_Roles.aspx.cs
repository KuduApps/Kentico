using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_PermissionName_Roles : SiteManagerPage
{
    #region "Constants"

    /// <summary>
    /// CSS class for highlighted UI permission matrix rows.
    /// </summary>
    private const string HIGHLIGHTED_ROW_CSS = "Highlighted";

    #endregion


    #region "Variables"

    private int mPermissionId = 0;
    private UserInfo mSelectedUser = null;
    private PermissionNameInfo mPermission = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets selected user ID.
    /// </summary>
    private int SelectedUserID
    {
        get
        {
            return ValidationHelper.GetInteger(userSelector.Value, 0);
        }
    }


    /// <summary>
    /// Gets UserInfo object for selected user.
    /// </summary>
    private UserInfo SelectedUser
    {
        get
        {
            if (mSelectedUser == null && SelectedUserID > 0)
            {
                mSelectedUser = UserInfoProvider.GetUserInfo(SelectedUserID);
            }
            return mSelectedUser;
        }
    }


    /// <summary>
    /// Gets PermissionNameInfo object for selected permission element.
    /// </summary>
    private PermissionNameInfo Permission
    {
        get
        {
            if (mPermission == null)
            {
                mPermissionId = QueryHelper.GetInteger("permissionId", 0);
                mPermission = PermissionNameInfoProvider.GetPermissionNameInfo(mPermissionId);
            }
            return mPermission;
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        // Register js script
        string script = "function NA() {alert(" + ScriptHelper.GetString(GetString("Administration-Permissions_Matrix.NotAdjustable")) + ");}";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "PermissionMatrix_NA", ScriptHelper.GetScript(script));

        // Initialize matrix control
        gridMatrix.ContentBefore = "<table class=\"PermissionMatrix\" cellspacing=\"0\" cellpadding=\"0\" rules=\"rows\" border=\"1\" style=\"width:100%;border-collapse:collapse;\">";
        gridMatrix.ContentAfter = "</table>";
        gridMatrix.ColumnsCount = 1;
        gridMatrix.OnItemChanged += gridMatrix_OnItemChanged;
        gridMatrix.CornerText = GetString("administration-module_edit_permissionnames.role");
        gridMatrix.ContentBeforeRowsCssClass = "ContentBefore";
        gridMatrix.OnGetRowItemCssClass += gridMatrix_OnGetRowItemCssClass;
        this.gridMatrix.NoRecordsMessage = GetString("general.emptymatrix");

        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_SelectedIndexChanged;
        siteSelector.IsLiveSite = false;
        siteSelector.AllowGlobal = true;

        // Initialize user selector
        userSelector.SiteID = (siteSelector.SiteID > 0) ? siteSelector.SiteID : 0;
        userSelector.ShowSiteFilter = false;
        userSelector.DropDownSingleSelect.AutoPostBack = true;

        // Display all users if global
        if (userSelector.SiteID <= 0)
        {
            userSelector.DisplayUsersFromAllSites = true;
        }
        else
        {
            userSelector.DisplayUsersFromAllSites = false;
        }

        chkUserOnly.Text = GetString("Administration-Permissions_Header.UserRoles");
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Load the matrix
        if (Permission != null)
        {
            // Disable check box if no user selected
            if (SelectedUserID > 0)
            {
                chkUserOnly.Enabled = true;
            }
            else
            {
                chkUserOnly.Checked = false;
                chkUserOnly.Enabled = false;
            }

            lblInfo.Text = String.Format(GetString("administration-module_edit_permissionnames.rolesinfo"), HTMLHelper.HTMLEncode(Permission.PermissionDisplayName));

            gridMatrix.QueryParameters = GetQueryParameters(siteSelector.SiteID, Permission.PermissionId, Permission.PermissionDisplayName);
            gridMatrix.WhereCondition = GetWhereCondition();
            gridMatrix.ShowContentBeforeRows = (SelectedUser != null);
            gridMatrix.ContentBeforeRows = GetBeforeRowsContent();

            if (!this.gridMatrix.HasData)
            {
                this.plcUpdate.Visible = false;
                this.lblInfo.Text = (chkUserOnly.Checked) ? GetString("general.norolemember") : GetString("general.emptymatrix");
            }
            else
            {
                // Inform user that global admin was selected
                if ((SelectedUserID > 0) && SelectedUser.IsGlobalAdministrator)
                {
                    this.lblGlobalAdmin.Text = GetString("Administration-Permissions_Matrix.GlobalAdministrator");
                }

                this.plcUpdate.Visible = true;
            }

            this.lblGlobalAdmin.Visible = !string.IsNullOrEmpty(this.lblGlobalAdmin.Text);
        }
        base.OnPreRender(e);
    }

    #endregion


    #region "Event Handlers

    protected void UniSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Reload user selector
        userSelector.ReloadData();
        mSelectedUser = null;

        // Set matrix current page to first     
        gridMatrix.Pager.CurrentPage = 1;
    }


    protected void gridMatrix_OnItemChanged(object sender, int rowItemId, int colItemId, bool newState)
    {
        if (newState)
        {
            RolePermissionInfoProvider.SetRolePermissionInfo(rowItemId, colItemId);
        }
        else
        {
            RolePermissionInfoProvider.DeleteRolePermissionInfo(rowItemId, colItemId);
        }
        // Invalidate all users
        UserInfo.TYPEINFO.InvalidateAllObjects();

        // Update content before rows
        gridMatrix.ContentBeforeRows = GetBeforeRowsContent();
    }


    protected string gridMatrix_OnGetRowItemCssClass(object sender, DataRow dr)
    {
        string roleName = ValidationHelper.GetString(dr["RoleName"], String.Empty);

        // Check if all necessary data are available
        if (!String.IsNullOrEmpty(roleName) && (SelectedUser != null))
        {
            if (SelectedUser.IsInRole(roleName, siteSelector.SiteName))
            {
                return HIGHLIGHTED_ROW_CSS;
            }
        }

        return String.Empty;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns query parameters for permission matrix.
    /// </summary>
    /// <param name="siteId">Site ID</param>
    /// <param name="permissionId">Permission ID</param>
    /// <param name="permissionName">Permission display name</param>
    /// <returns>Two dimensional object array.</returns>
    private QueryDataParameters GetQueryParameters(int siteId, int permissionId, string permissionName)
    {
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@SiteID", (ValidationHelper.GetString(siteSelector.Value, String.Empty) == siteSelector.GlobalRecordValue) ? 0 : siteId);
        parameters.Add("@PermissionID", permissionId);
        parameters.Add("@PermissionDisplayName", ResHelper.LocalizeString(permissionName));

        return parameters;
    }


    /// <summary>
    /// Gets user effective UI permission HTML content.
    /// </summary>
    private string GetBeforeRowsContent()
    {
        // Initialize string builder
        StringBuilder sb = new StringBuilder("");
        ResourceInfo resource = ResourceInfoProvider.GetResourceInfo(Permission.ResourceId);

        // Check if every neccessary property is set
        if ((SelectedUser != null) && (Permission != null) && (resource != null))
        {
            // Initialize variables used during rendering
            string firstColumnsWidth = (gridMatrix.FirstColumnsWidth > 0) ? "width:" + gridMatrix.FirstColumnsWidth + (gridMatrix.UsePercentage ? "%;" : "px;") : "";
            string imagesUrl = GetImageUrl("Design/Controls/UniMatrix/", false, true);
            string userName = HTMLHelper.HTMLEncode(TextHelper.LimitLength(Functions.GetFormattedUserName(SelectedUser.UserName, SelectedUser.FullName), 50));

            // Get user name column
            sb.Append("<td class=\"MatrixHeader\" style=\"");
            sb.Append(firstColumnsWidth);
            sb.Append("white-space:nowrap;\" title=\"");
            sb.Append(userName);
            sb.Append("\">");
            sb.Append(userName);
            sb.Append("</td>\n");

            // Render UI permission cell
            sb.Append("<td style=\"white-space:nowrap; text-align: center;\"><img src=\"");
            sb.Append(imagesUrl);

            // Render checkboxes
            if (SelectedUser.IsGlobalAdministrator || UserInfoProvider.IsAuthorizedPerResource(resource.ResourceName, Permission.PermissionName, siteSelector.SiteName, SelectedUser))
            {
                sb.Append("allowed.png");
            }
            else
            {
                sb.Append("denied.png");
            }

            // Append tootlip and alternative text
            sb.Append("\" onclick=\"NA()\" title=\"\" alt=\"\" /></td>\n");

            // Add finish row
            sb.Append("<td></td>");

        }
        return sb.ToString();
    }


    /// <summary>
    /// Gets where condition for the matrix.
    /// </summary>
    /// <returns>String representing where condition for the matrix</returns>
    private string GetWhereCondition()
    {
        string where = "RoleGroupId IS NULL";

        if (chkUserOnly.Checked && (SelectedUserID > 0))
        {
            // Get selected site name
            string siteName = UserInfo.GLOBAL_ROLES_KEY;
            if (siteSelector.SiteID > 0)
            {
                siteName = siteSelector.SiteName.ToLower();
            }

            // Build roles by comma string
            StringBuilder sbRolesWhere = new StringBuilder();
            foreach (int roleId in ((Hashtable)SelectedUser.SitesRoles[siteName]).Values)
            {
                sbRolesWhere.Append(",");
                sbRolesWhere.Append(roleId);
            }
            string rolesWhere = sbRolesWhere.ToString();

            // Add roles where condition
            if (!String.IsNullOrEmpty(rolesWhere))
            {
                rolesWhere = rolesWhere.Remove(0, 1);
                where = SqlHelperClass.AddWhereCondition(where, "RoleID IN(" + rolesWhere + ")");
            }
            else
            {
                where = SqlHelperClass.NO_DATA_WHERE;
                this.gridMatrix.StopProcessing = true;
            }
        }

        return where;
    }

    #endregion
}
