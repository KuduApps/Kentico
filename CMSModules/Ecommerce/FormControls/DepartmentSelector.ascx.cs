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

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_DepartmentSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private bool mUseDepartmentNameForSelection = true;
    private bool mAddNoneRecord = false;
    private bool mAddAllItemsRecord = true;
    private bool mAddAllMyRecord = false;
    private int mUserId = 0;
    private bool? mAllowGlobalObjects = null;
    private bool? mDisplaySiteItems = null;
    private bool? mDisplayGlobalItems = null;
    private int mSiteId = -1;
    private string mAdditionalItems = "";
    private bool mDropDownListMode = true;
    private bool mShowAllSites = false;
    private bool mAddWithoutDepartmentRecord = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the user the departments of which should be displayed. 0 means all departments are displayed.
    /// </summary>
    public int UserID
    {
        get
        {
            return mUserId;
        }
        set
        {
            this.mUserId = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            if (this.mUseDepartmentNameForSelection)
            {
                return this.DepartmentName;
            }
            else
            {
                return this.DepartmentID;
            }
        }
        set
        {
            EnsureChildControls();
            if (this.mUseDepartmentNameForSelection)
            {
                this.DepartmentName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.DepartmentID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the Department ID.
    /// </summary>
    public int DepartmentID
    {
        get
        {
            EnsureChildControls();
            if (this.mUseDepartmentNameForSelection)
            {
                string name = ValidationHelper.GetString(uniSelector.Value, "");
                DepartmentInfo tgi = DepartmentInfoProvider.GetDepartmentInfo(name, SiteInfoProvider.GetSiteName(this.SiteID));
                if (tgi != null)
                {
                    return tgi.DepartmentID;
                }
                return 0;
            }
            else
            {
                return ValidationHelper.GetInteger(uniSelector.Value, 0);
            }
        }
        set
        {
            EnsureChildControls();
            if (this.mUseDepartmentNameForSelection)
            {
                DepartmentInfo tgi = DepartmentInfoProvider.GetDepartmentInfo(value);
                if (tgi != null)
                {
                    this.uniSelector.Value = tgi.DepartmentName;
                }
            }
            else
            {
                this.uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the Department code name.
    /// </summary>
    public string DepartmentName
    {
        get
        {
            EnsureChildControls();
            if (this.mUseDepartmentNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelector.Value, "");
            }
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);
                DepartmentInfo di = DepartmentInfoProvider.GetDepartmentInfo(id);
                if (di != null)
                {
                    return di.DepartmentName;
                }
                return "";
            }
        }
        set
        {
            EnsureChildControls();
            if (this.mUseDepartmentNameForSelection)
            {
                this.uniSelector.Value = value;
            }
            else
            {
                DepartmentInfo di = null;
                if (ShowAllSites)
                {
                    DataSet ds = DepartmentInfoProvider.GetDepartments("DepartmentName = '" + value.Replace("'", "''") + "'", "DepartmentSiteID", 1, null);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        di = new DepartmentInfo(ds.Tables[0].Rows[0]);
                    }
                }
                else
                {
                    di = DepartmentInfoProvider.GetDepartmentInfo(value, SiteInfoProvider.GetSiteName(this.SiteID));
                }

                if (di != null)
                {
                    this.uniSelector.Value = di.DepartmentID;
                }
            }
        }
    }


    /// <summary>
    ///  If true, selected value is DepartmentName, if false, selected value is DepartmentID.
    /// </summary>
    public bool UseDepartmentNameForSelection
    {
        get
        {
            return mUseDepartmentNameForSelection;
        }
        set
        {
            mUseDepartmentNameForSelection = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return mAddNoneRecord;
        }
        set
        {
            mAddNoneRecord = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add all item record to the dropdownlist.
    /// </summary>
    public bool AddAllItemsRecord
    {
        get
        {
            return mAddAllItemsRecord;
        }
        set
        {
            mAddAllItemsRecord = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add all my departments item record to the dropdownlist.
    /// </summary>
    public bool AddAllMyRecord
    {
        get
        {
            return mAddAllMyRecord;
        }
        set
        {
            mAddAllMyRecord = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add 'without deparmtnet' item record to the dropdownlist.
    /// </summary>
    public bool AddWithoutDepartmentRecord
    {
        get
        {
            return mAddWithoutDepartmentRecord;
        }
        set
        {
            mAddWithoutDepartmentRecord = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            EnsureChildControls();
            base.Enabled = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Returns ClientID of the dropdownlist.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            EnsureChildControls();
            if (DropDownListMode)
            {
                return this.uniSelector.DropDownSingleSelect.ClientID;
            }
            else
            {
                return this.uniSelector.TextBoxSelect.ClientID;
            }
        }
    }

    /// <summary>
    /// Indicates if site items will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplaySiteItems
    {
        get
        {
            // Unknown yet
            if (!mDisplaySiteItems.HasValue)
            {
                // Display site item when working with site records
                mDisplaySiteItems = (SiteID != 0);
            }

            return mDisplaySiteItems.Value;
        }
        set
        {
            mDisplaySiteItems = value;
        }
    }


    /// <summary>
    /// Indicates if global items will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplayGlobalItems
    {
        get
        {
            // Unknown yet
            if (!mDisplayGlobalItems.HasValue)
            {
                mDisplayGlobalItems = false;
                if ((SiteID == 0) || AllowGlobalObjects)
                {
                    mDisplayGlobalItems = true;
                }
            }

            return mDisplayGlobalItems.Value;
        }
        set
        {
            mDisplayGlobalItems = value;
        }
    }


    /// <summary>
    /// Allows to display payment methods only for specified site id. Use 0 for global methods. Default value is current site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            // No site id given
            if (mSiteId == -1)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;

            mDisplayGlobalItems = null;
            mDisplaySiteItems = null;
            mAllowGlobalObjects = null;
        }
    }


    /// <summary>
    /// Id of items which have to be displayed. Use ',' or ';' as separator if more ids required.
    /// </summary>
    public string AdditionalItems
    {
        get
        {
            return mAdditionalItems;
        }
        set
        {
            // Prevent from setting null value
            if (value != null)
            {
                mAdditionalItems = value.Replace(';', ',');
            }
            else
            {
                mAdditionalItems = "";
            }
        }
    }


    /// <summary>
    /// Indicates if drop down list mode is used. Default value is true.
    /// </summary>
    public bool DropDownListMode
    {
        get
        {
            return mDropDownListMode;
        }
        set
        {
            mDropDownListMode = value;
        }
    }


    /// <summary>
    /// Indicates if departments for all sites will be listed.
    /// </summary>
    public bool ShowAllSites
    {
        get
        {
            return mShowAllSites;
        }
        set
        {
            mShowAllSites = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Returns true if site given by SiteID uses global payment methods beside site-specific ones.
    /// </summary>
    protected bool AllowGlobalObjects
    {
        get
        {
            if (ShowAllSites)
            {
                return true;
            }

            // Unknown yet
            if (!mAllowGlobalObjects.HasValue)
            {
                mAllowGlobalObjects = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mAllowGlobalObjects = ECommerceSettings.AllowGlobalDepartments(si.SiteName);
                }
            }

            return mAllowGlobalObjects.Value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        uniSelector.SelectionMode = (DropDownListMode ? CMS.UIControls.SelectionModeEnum.SingleDropDownList : CMS.UIControls.SelectionModeEnum.SingleTextBox);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;
        this.uniSelector.AllowAll = this.AddAllItemsRecord;
        this.uniSelector.ReturnColumnName = (this.UseDepartmentNameForSelection ? "DepartmentName" : "DepartmentID");

        if (ShowAllSites)
        {
            uniSelector.FilterControl = "~/CMSFormControls/Filters/SiteFilter.ascx";
            uniSelector.SetValue("FilterMode", "department");
        }

        // Add special records
        if (this.AddAllMyRecord && this.AddWithoutDepartmentRecord)
        {
            this.uniSelector.SpecialFields = new string[,] { { GetString("product_list.allmydepartments"), "" }, { GetString("com.productswithoutdepartment"), "-5" } };
        }
        else
        {
            if (this.AddAllMyRecord)
            {
                this.uniSelector.SpecialFields = new string[,] { { GetString("product_list.allmydepartments"), "" } };
            }

            if (this.AddWithoutDepartmentRecord)
            {
                this.uniSelector.SpecialFields = new string[,] { { GetString("general.empty"), "-5" } };
            }
        }

        SetupWhereCondition();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (URLHelper.IsPostback()
            && this.DependsOnAnotherField)
        {
            SetupWhereCondition();
            uniSelector.Reload(true);
            pnlUpdate.Update();
        }
        else
        {
            uniSelector.Reload(true);
        }
    }


    /// <summary>
    /// Creates child controls and loads update panel container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load update panel container
        if (uniSelector == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Inits the selector.
    /// </summary>
    protected void SetupWhereCondition()
    {
        SetFormSiteID();

        // Select all
        string where = "(1=1)";

        if (!ShowAllSites)
        {
            // Get only departments of the given user if he is not authorized for all departments
            if ((this.UserID > 0) && !UserInfoProvider.IsAuthorizedPerResource("CMS.Ecommerce", "AccessAllDepartments", CMSContext.CurrentSiteName, UserInfoProvider.GetUserInfo(this.UserID)))
            {
                where = "DepartmentID IN (SELECT DepartmentID FROM COM_UserDepartment WHERE UserID = " + this.UserID + ")";
            }

            where += " AND ((1=0)";
            // Add global items
            if (DisplayGlobalItems)
            {
                where += " OR (DepartmentSiteID IS NULL) ";
            }
            // Add site specific items
            if (DisplaySiteItems)
            {
                where += " OR (DepartmentSiteID = " + SiteID + ")";
            }
            where += ")";

            where = "(" + where + ")";
            // Add items which have to be on the list (if any)
            string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
            if ((!string.IsNullOrEmpty(where)) && (!string.IsNullOrEmpty(additionalList)))
            {
                where += " OR (DepartmentID IN (" + additionalList + "))";
            }

            // Selected value (if any) must be on the list
            if ((!string.IsNullOrEmpty(where)) && (DepartmentID > 0))
            {
                where += " OR (DepartmentID = " + DepartmentID + ")";
            }
        }

        this.uniSelector.WhereCondition = where;

        if (this.UseDepartmentNameForSelection)
        {
            this.uniSelector.AllRecordValue = "";
            this.uniSelector.NoneRecordValue = "";
        }
    }


    /// <summary>
    /// Sets the SiteID if the SiteName field is available in the form.
    /// </summary>
    private void SetFormSiteID()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), null);
            if (!String.IsNullOrEmpty(siteName))
            {
                SiteInfo siteObj = SiteInfoProvider.GetSiteInfo(siteName);
                if (siteObj != null)
                {
                    SiteID = siteObj.SiteID;
                }
            }
            else
            {
                SiteID = -1;
            }
        }
    }
}
