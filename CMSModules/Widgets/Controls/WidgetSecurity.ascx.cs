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
using CMS.UIControls;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Widgets_Controls_WidgetSecurity : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    private bool mNoRolesAvailable = false;
    private bool mEnable = true;

    private WidgetInfo mWidgetInfo = null;
    private ResourceInfo mResWidget = null;

    // HashTable holding information on all permissions that 'OnlyAuthorizedRoles' access is selected for
    private Hashtable onlyAuth = new Hashtable();

    private ArrayList permissionArray = new ArrayList();

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current widget info.
    /// </summary>
    private WidgetInfo WidgetInfo
    {
        get
        {
            if ((this.mWidgetInfo == null) && (this.WidgetID > 0))
            {
                this.mWidgetInfo = WidgetInfoProvider.GetWidgetInfo(this.WidgetID);
            }
            return this.mWidgetInfo;
        }
    }


    /// <summary>
    /// Current widget resource info.
    /// </summary>
    private ResourceInfo ResWidget
    {
        get
        {
            if (this.mResWidget == null)
            {
                this.mResWidget = ResourceInfoProvider.GetResourceInfo("CMS.Widgets");
            }
            return this.mResWidget;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the widget to edit.
    /// </summary>
    public int WidgetID
    {
        get
        {
            return this.ItemID;
        }
        set
        {
            this.ItemID = value;
            this.mWidgetInfo = null;
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


    protected override void OnPreRender(EventArgs e)
    {
        if (this.WidgetInfo != null)
        {
            this.chkUsedInGroupZones.Checked = this.WidgetInfo.WidgetForGroup;
            this.chkUsedInUserZones.Checked = this.WidgetInfo.WidgetForUser;
            this.chkUsedInEditorZones.Checked = this.WidgetInfo.WidgetForEditor;
            this.chkUsedAsInlineWidget.Checked = this.WidgetInfo.WidgetForInline;
            this.chkUsedInDashboard.Checked = this.WidgetInfo.WidgetForDashboard;

            // Render permission matrix
            CreateMatrix();
        }

        // Disable control if needed
        if (!Enable)
        {
            ltlScript.Text = "";
            tblMatrix.Enabled = false;
        }

        base.OnPreRender(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        if (!Visible)
        {
            EnableViewState = false;
        }

        this.chkUsedInGroupZones.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, "group"));
        this.chkUsedInUserZones.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, "user"));
        this.chkUsedInEditorZones.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, "editor"));
        this.chkUsedInDashboard.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, "dashboard"));
        this.chkUsedAsInlineWidget.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this, "inline"));


        if (this.WidgetInfo != null)
        {
            gridMatrix.NoRecordsMessage = GetString("general.norolesinsite");

            siteSelector.AllowGlobal = true;
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
            siteSelector.AllowEmpty = false;

            int siteId = 0;
            if (!RequestHelper.IsPostBack())
            {
                siteId = CMSContext.CurrentSiteID;

                // Site may be stopped, get truly selected value
                if (siteId == 0)
                {
                    siteSelector.Reload(false);
                    siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
                }
                else
                {
                    siteSelector.Value = siteId;
                }
            }
            else
            {
                siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }

            // If global role is selected - set SiteID to 0
            if (siteId.ToString() == siteSelector.GlobalRecordValue)
            {
                siteId = 0;
            }


            // Set editable permissions
            permissionArray.Add("allowedfor");

            if (this.ResWidget != null)
            {
                // Retrive permission matrix data
                QueryDataParameters parameters = new QueryDataParameters();
                parameters.Add("@ID", this.ResWidget.ResourceId);
                parameters.Add("@WidgetID", this.WidgetID);
                parameters.Add("@SiteID", siteId);

                // Do not show community roles
                string where = "RoleGroupID IS NULL";

                if (permissionArray != null)
                {
                    where += " AND PermissionName IN (";
                    foreach (string permission in permissionArray)
                    {
                        where += "'" + permission + "',";
                    }
                    where = where.TrimEnd(',');
                    where += ") ";
                }

                // Setup matrix control            
                gridMatrix.QueryParameters = parameters;
                gridMatrix.WhereCondition = where;
                gridMatrix.ContentBefore = "<table class=\"PermissionMatrix\" cellspacing=\"0\" cellpadding=\"0\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\">";
                gridMatrix.ContentAfter = "</table>";
                gridMatrix.OnItemChanged += new UniMatrix.ItemChangedEventHandler(gridMatrix_OnItemChanged);

            }
        }
        else
        {
            this.Visible = false;
            gridMatrix.StopProcessing = true;
        }
    }


    /// <summary>
    /// Site change.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Sets pager to first page
        gridMatrix.ResetPager();

        pnlUpdate.Update();
    }


    /// <summary>
    /// Generates the permission matrix for the cutrrent widget.
    /// </summary>
    private void CreateMatrix()
    {
        // Get widget resource info             
        if ((this.ResWidget != null) && (this.WidgetInfo != null))
        {
            // Get permissions for the current widget resource                       
            DataSet permissions = PermissionNameInfoProvider.GetResourcePermissions(this.ResWidget.ResourceId);
            if (DataHelper.DataSourceIsEmpty(permissions))
            {
                lblInfo.Text = GetString("general.emptymatrix");
            }
            else
            {
                TableRow headerRow = new TableRow();
                headerRow.CssClass = "UniGridHead";
                headerRow.HorizontalAlign = HorizontalAlign.Left;
                TableCell newCell = null;
                TableHeaderCell newHeaderCell = new TableHeaderCell();

                newHeaderCell.Attributes.Add("style", "width:300px; white-space: nowrap;");
                headerRow.Cells.Add(newHeaderCell);

                DataView dv = permissions.Tables[0].DefaultView;
                dv.Sort = "PermissionName ASC";

                // Generate header cells                
                foreach (DataRowView drv in dv)
                {
                    string permissionName = drv.Row["PermissionName"].ToString();
                    if (permissionArray.Contains(permissionName.ToLower()))
                    {
                        newHeaderCell = new TableHeaderCell();
                        newHeaderCell.CssClass = "MatrixHeader";
                        newHeaderCell.Text = HTMLHelper.HTMLEncode(drv.Row["PermissionDisplayName"].ToString());
                        newHeaderCell.ToolTip = Convert.ToString(drv.Row["PermissionDescription"]);
                        newHeaderCell.Attributes.Add("style", "text-align: center; white-space: nowrap;");

                        headerRow.Cells.Add(newHeaderCell);
                    }
                }

                // Insert the empty cell at the end
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "&#160;";
                headerRow.Cells.Add(newHeaderCell);
                tblMatrix.Rows.AddAt(0, headerRow);

                // Render widget access permissions
                object[,] accessNames = new object[3, 2];
                //accessNames[0, 0] = GetString("security.allusers");
                //accessNames[0, 1] = SecurityAccessEnum.AllUsers;
                accessNames[0, 0] = GetString("security.authenticated");
                accessNames[0, 1] = SecurityAccessEnum.AuthenticatedUsers;
                accessNames[1, 0] = GetString("security.globaladmin");
                accessNames[1, 1] = SecurityAccessEnum.GlobalAdmin;
                accessNames[2, 0] = GetString("security.authorizedroles");
                accessNames[2, 1] = SecurityAccessEnum.AuthorizedRoles;

                TableRow newRow = null;

                for (int access = 0; access <= accessNames.GetUpperBound(0); access++)
                {
                    SecurityAccessEnum currentAccess = ((SecurityAccessEnum)accessNames[access, 1]);

                    // Generate cell holding access item name
                    newRow = new TableRow();
                    newCell = new TableCell();
                    newCell.CssClass = "MatrixHeader";
                    newCell.Text = accessNames[access, 0].ToString();
                    newCell.Wrap = false;
                    newCell.Width = new Unit(150, UnitType.Pixel);
                    newRow.Cells.Add(newCell);

                    // Render the permissions access items
                    int permissionIndex = 0;
                    for (int permission = 0; permission < (tblMatrix.Rows[0].Cells.Count - 2); permission++)
                    {
                        newCell = new TableCell();
                        newCell.CssClass = "MatrixCell";
                        newCell.Attributes.Add("style", "text-align: center; white-space: nowrap;");

                        int accessEnum = Convert.ToInt32(accessNames[access, 1]);
                        // Check if the currently processed access is applied for permission
                        bool isAllowed = CheckPermissionAccess(accessEnum, permission, tblMatrix.Rows[0].Cells[permission + 1].Text);

                        // Disable column in roles grid if needed
                        if ((currentAccess == SecurityAccessEnum.AuthorizedRoles) && !isAllowed)
                        {
                            gridMatrix.DisableColumn(permissionIndex);
                        }

                        // Insert the radio button for the current permission
                        string permissionText = tblMatrix.Rows[0].Cells[permission + 1].Text;
                        string elemId = ClientID + "_" + permission + "_" + access;
                        newCell.Text = "<label style=\"display:none;\" for=\"" + elemId + "\">" + permissionText + "</label><input type=\"radio\" id=\"" + elemId + "\" name=\"" + permissionText + "\" onclick=\"" + Page.ClientScript.GetPostBackEventReference(this, permission + ";" + accessEnum) + "\" " + ((isAllowed) ? "checked = \"checked\"" : "") + "/>";

                        newRow.Cells.Add(newCell);
                        permissionIndex++;
                    }

                    // Add the access row to the table
                    newCell = new TableCell();
                    newRow.Cells.Add(newCell);
                    tblMatrix.Rows.Add(newRow);
                }

                // Get permission matrix for roles of the current site/group            
                mNoRolesAvailable = !gridMatrix.HasData;
                if (!this.mNoRolesAvailable)
                {
                    this.lblRolesInfo.Visible = true;
                }
            }
        }
    }


    /// <summary>
    /// Indicates the permission acess.
    /// </summary>
    /// <param name="currentAccess">Currently processed integer representation of item from SecurityAccessEnum</param>    
    /// <param name="currentPermission">Currently processed integer representation of permission to check</param>    
    private bool CheckPermissionAccess(int currentAccess, int currentPermission, string currentPermissionName)
    {
        bool result = false;

        if (this.WidgetInfo != null)
        {
            switch (currentPermission)
            {
                case 0:
                    result = ((int)this.WidgetInfo.AllowedFor == currentAccess);
                    break;

                default:
                    break;
            }
        }

        // Make note about type of permission with access set to 'OnlyAuthorizedRoles'
        if (result && (currentAccess == 2))
        {
            this.onlyAuth[currentPermissionName] = true;
        }
        return result;
    }


    /// <summary>
    /// On item changed event.
    /// </summary>    
    void gridMatrix_OnItemChanged(object sender, int roleId, int permissionId, bool allow)
    {
        if (!CheckPermissions("cms.widget", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        if (allow)
        {
            WidgetRoleInfoProvider.AddRoleToWidget(roleId, this.WidgetID, permissionId);
        }
        else
        {
            WidgetRoleInfoProvider.RemoveRoleFromWidget(roleId, this.WidgetID, permissionId);
        }
    }


    #region "PostBack event handler"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (!CheckPermissions("cms.widget", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        string[] args = eventArgument.Split(';');

        if (args.Length == 2)
        {
            // Get info on currently selected item
            int permission = Convert.ToInt32(args[0]);
            int access = Convert.ToInt32(args[1]);

            if (this.WidgetInfo != null)
            {
                // Update widget permission access information
                switch (permission)
                {
                    case 0:
                        this.WidgetInfo.AllowedFor = ((SecurityAccessEnum)access);
                        break;
                }

                // Save changes to the widget
                WidgetInfoProvider.SetWidgetInfo(this.WidgetInfo);
            }
        }
        else if ((args.Length == 1))
        {
            switch (args[0].ToLower())
            {
                // Used in group zones
                case "group":
                    if (this.WidgetInfo != null)
                    {
                        this.WidgetInfo.WidgetForGroup = chkUsedInGroupZones.Checked;

                    }
                    break;

                // Used in user zones
                case "user":
                    if (this.WidgetInfo != null)
                    {
                        this.WidgetInfo.WidgetForUser = chkUsedInUserZones.Checked;
                    }
                    break;

                // Used in editor zones
                case "editor":
                    if (this.WidgetInfo != null)
                    {
                        this.WidgetInfo.WidgetForEditor = chkUsedInEditorZones.Checked;
                    }
                    break;

                //Used as inline widget
                case "inline":
                    if (this.WidgetInfo != null)
                    {
                        WidgetInfo.WidgetForInline = chkUsedAsInlineWidget.Checked;
                    }
                    break;

                // Used in dashboard zones
                case "dashboard":
                    if (this.WidgetInfo != null)
                    {
                        WidgetInfo.WidgetForDashboard = chkUsedInDashboard.Checked;
                    }
                    break;


                default:
                    break;
            }

            // Update database
            WidgetInfoProvider.SetWidgetInfo(this.WidgetInfo);
        }
    }

    #endregion
}
