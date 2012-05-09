using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Permissions_Controls_PermissionsFilter : CMSAdminControl
{
    #region "Variables"

    private int mSiteId = 0;
    private int mRoleId = 0;
    private bool mHideSiteSelector = false;
    private bool globalRecord = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if site selector contain sites.
    /// </summary>
    public bool HasSites
    {
        get
        {
            if (this.SiteID > 0)
            {
                return (true);
            }
            return siteSelector.DropDownSingleSelect.Items.Count > 0;
        }
    }


    /// <summary>
    /// Gets or sets Site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }


    /// <summary>
    /// If false site selector is hidden no matter what.
    /// </summary>
    public bool HideSiteSelector
    {
        get
        {
            return mHideSiteSelector;
        }
        set
        {
            this.mHideSiteSelector = value;
        }
    }


    /// <summary>
    /// Value for (global) item record.
    /// </summary>
    public string GlobalRecordValue
    {
        get
        {
            return this.siteSelector.GlobalRecordValue;
        }
        set
        {
            this.siteSelector.GlobalRecordValue = value;
        }
    }


    /// <summary>
    /// Gets or sets Role ID.
    /// </summary>
    public int RoleID
    {
        get
        {
            return this.mRoleId;
        }
        set
        {
            this.mRoleId = value;
        }
    }


    /// <summary>
    /// Gets ID selected in the moduleSelector or docTypeSelector or customTableSelector according to the selected PermissionType.
    /// </summary>
    public string SelectedID
    {
        get
        {
            if (this.drpPermissionType.SelectedIndex > -1)
            {
                if ((this.drpPermissionType.SelectedValue == PermissionTypes.Module.ToString()) && (this.moduleSelector.UniSelector.HasData))
                {
                    return ValidationHelper.GetString(this.moduleSelector.Value, "0");
                }
                else if ((this.drpPermissionType.SelectedValue == PermissionTypes.DocumentType.ToString()) && (this.docTypeSelector.UniSelector.HasData))
                {
                    return ValidationHelper.GetString(this.docTypeSelector.Value, "0");
                }
                else if (this.customTableSelector.UniSelector.HasData)
                {
                    return ValidationHelper.GetString(this.customTableSelector.Value, "0");
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
    }


    /// <summary>
    /// Gets type constant according to the selected value in the moduleSelector or docTypeSelector or customTableSelector and the selected PermissionType.
    /// </summary>
    public string SelectedType
    {
        get
        {
            if (this.drpPermissionType.SelectedIndex > -1)
            {
                if ((this.drpPermissionType.SelectedValue == PermissionTypes.Module.ToString()) && (this.moduleSelector.UniSelector.HasData))
                {
                    return "r";
                }
                else if (((this.drpPermissionType.SelectedValue == PermissionTypes.DocumentType.ToString()) && (this.docTypeSelector.UniSelector.HasData)) || (this.customTableSelector.UniSelector.HasData))
                {
                    return "c";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }


    /// <summary>
    /// Gets selected site ID.
    /// </summary>
    public int SelectedSiteID
    {
        get
        {
            return ValidationHelper.GetInteger(siteSelector.Value, 0);
        }
    }


    /// <summary>
    /// Indicates if user selector will be displayed.
    /// </summary>
    public bool ShowUserSelector
    {
        get
        {
            return plcUser.Visible;
        }
        set
        {
            plcUser.Visible = value;
        }
    }


    /// <summary>
    /// Gets selected user ID.
    /// </summary>
    public int SelectedUserID
    {
        get
        {
            return ValidationHelper.GetInteger(userSelector.Value, 0);
        }
    }


    /// <summary>
    /// Gets or sets live site mode.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return siteSelector.IsLiveSite;
        }
        set
        {
            customTableSelector.IsLiveSite = value;
            moduleSelector.IsLiveSite = value;
            docTypeSelector.IsLiveSite = value;
            siteSelector.IsLiveSite = value;
            userSelector.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates if only user roles should be displayed.
    /// </summary>
    public bool UserRolesOnly
    {
        get
        {
            return chkUserOnly.Checked;
        }
    }
    

    /// <summary>
    /// Indicates if filter was changed.
    /// </summary>
    public bool FilterChanged
    {
        get;
        set;
    }

    #endregion


    #region "Enums"

    private enum PermissionTypes
    {
        DocumentType = 1,
        Module,
        CustomTable
    };

    #endregion


    #region "Page Evenets"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize site selector, if no site supplied        
        if (this.SiteID <= 0)
        {
            // Set site selector
            siteSelector.AllowSetSpecialFields = true;
            siteSelector.OnlyRunningSites = false;
            siteSelector.AllowAll = false;
            siteSelector.AllowEmpty = false;
            siteSelector.AllowGlobal = true;

            if (!RequestHelper.IsPostBack())
            {
                this.SiteID = 0;
                siteSelector.Value = ValidationHelper.GetInteger(siteSelector.GlobalRecordValue, 0);

                // Force siteselector to reload
                siteSelector.Reload(false);
            }
        }
        else
        {
            this.plcSite.Visible = false;
            siteSelector.Value = this.SiteID;
        }

        // Hide site selector used from role edit
        if (HideSiteSelector)
        {
            this.plcSite.Visible = false;
        }

        if (ValidationHelper.GetString(siteSelector.Value, String.Empty) == siteSelector.GlobalRecordValue)
        {
            globalRecord = true;
        }

        if (!RequestHelper.IsPostBack())
        {
            InitializeDropDownListPermissionType();
        }

        // Get truly selected value
        this.SiteID = ValidationHelper.GetInteger(siteSelector.Value, 0);

        // Inicialize user selector
        userSelector.SiteID = (SiteID > 0) ? SiteID : 0;
        userSelector.ShowSiteFilter = false;
        userSelector.DisplayUsersFromAllSites = false;
        if (userSelector.SiteID <= 0)
        {
            userSelector.DisplayUsersFromAllSites = true;
        }
        userSelector.UniSelector.OnSelectionChanged += userSelector_OnSelectionChanged;

        moduleSelector.DisplayOnlyForGivenSite = !globalRecord;
        InitializeDropDownListPermissionMatrix();

        this.siteSelector.UniSelector.OnSelectionChanged += siteSelector_OnSelectionChanged;

        // Set auto postback for selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        moduleSelector.DropDownSingleSelect.AutoPostBack = true;
        docTypeSelector.DropDownSingleSelect.AutoPostBack = true;
        customTableSelector.DropDownSingleSelect.AutoPostBack = true;
        userSelector.DropDownSingleSelect.AutoPostBack = true;
        moduleSelector.DropDownSingleSelect.SelectedIndexChanged += moduleSelector_SelectedIndexChanged;
        chkUserOnly.Text = GetString("Administration-Permissions_Header.UserRoles");
        chkUserOnly.CheckedChanged += chkUserOnly_CheckedChanged;
    }

    #endregion


    #region "Private & protected methods"

    /// <summary>
    /// Initialize permission type drop down list.
    /// </summary>
    private void InitializeDropDownListPermissionType()
    {
        // Initialize drop down list with types
        this.drpPermissionType.Items.Clear();
        this.drpPermissionType.Items.Add(new ListItem(GetString("objecttype.cms_resource"), PermissionTypes.Module.ToString()));
        this.drpPermissionType.Items.Add(new ListItem(GetString("general.documenttype"), PermissionTypes.DocumentType.ToString()));

        // Check if any custom table available under site
        if ((!DataHelper.DataSourceIsEmpty(DataClassInfoProvider.GetCustomTableClasses(this.SiteID, null, null, 1, "ClassID")))
         || (ValidationHelper.GetString(siteSelector.Value, String.Empty) == siteSelector.GlobalRecordValue)
            || (globalRecord && CMSContext.CurrentUser.UserSiteManagerAdmin))
        {
            drpPermissionType.Items.Add(new ListItem(GetString("general.customtable"), PermissionTypes.CustomTable.ToString()));
        }
    }


    /// <summary>
    /// Initialize permission matrix drop down list.
    /// </summary>
    private void InitializeDropDownListPermissionMatrix()
    {
        string permissionType = null;
        if (this.drpPermissionType.SelectedIndex > -1)
        {
            permissionType = this.drpPermissionType.SelectedValue;
        }

        if (!string.IsNullOrEmpty(permissionType))
        {
            this.moduleSelector.Visible = (permissionType == PermissionTypes.Module.ToString());
            // Ensure module selection from query string
            if ((moduleSelector.Visible) && (!URLHelper.IsPostback()))
            {
                string selectedModule = QueryHelper.GetString("module", null);
                if (!String.IsNullOrEmpty(selectedModule))
                {
                    ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(selectedModule);
                    if (ri != null)
                    {
                        moduleSelector.Value = ri.ResourceId;
                    }
                }
            }
            this.docTypeSelector.Visible = (permissionType == PermissionTypes.DocumentType.ToString());
            this.customTableSelector.Visible = (permissionType == PermissionTypes.CustomTable.ToString());

            if (this.SiteID > 0)
            {
                string where = "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + this.SiteID + ")";
                this.moduleSelector.SiteID = this.SiteID;
                this.customTableSelector.WhereCondition = where;
                this.docTypeSelector.WhereCondition = where;
            }
        }
    }

    #endregion


    #region "Event Handlers"

    protected void siteSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        userSelector.ReloadData();
        userSelector_OnSelectionChanged(null, null);
        InitializeDropDownListPermissionType();
        InitializeDropDownListPermissionMatrix();
        ReloadSelectors();
        FilterChanged = true;
    }


    protected void userSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Disable check box if no user selected
        int selUser = ValidationHelper.GetInteger(userSelector.Value, 0);
        if (selUser > 0)
        {
            chkUserOnly.Enabled = true;
        }
        else
        {
            chkUserOnly.Checked = false;
            chkUserOnly.Enabled = false;
        }
        FilterChanged = true;
    }


    protected void drpPermissionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitializeDropDownListPermissionMatrix();
        ReloadSelectors();
        FilterChanged = true;
    }


    protected void moduleSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterChanged = true;
    }


    protected void chkUserOnly_CheckedChanged(object sender, EventArgs e)
    {
        FilterChanged = true;
    }


    private void ReloadSelectors()
    {
        globalRecord = (ValidationHelper.GetString(siteSelector.Value, String.Empty) == siteSelector.GlobalRecordValue);

        if (this.drpPermissionType.SelectedValue == PermissionTypes.Module.ToString())
        {
            this.moduleSelector.DisplayOnlyForGivenSite = !globalRecord;
            this.moduleSelector.ReloadData(true);
            this.moduleSelector.DropDownSingleSelect.SelectedIndex = 0;
        }
        else if (this.drpPermissionType.SelectedValue == PermissionTypes.DocumentType.ToString())
        {
            this.docTypeSelector.ReloadData(true);
            this.docTypeSelector.DropDownSingleSelect.SelectedIndex = 0;
        }
        else if (this.drpPermissionType.SelectedValue == PermissionTypes.CustomTable.ToString())
        {
            this.customTableSelector.ReloadData(true);
            this.customTableSelector.DropDownSingleSelect.SelectedIndex = 0;
        }
    }

    #endregion
}
