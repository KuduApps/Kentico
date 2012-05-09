using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_PublicStatusSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mUseStatusNameForSelection = true;
    private bool mAddAllItemsRecord = true;
    private bool mAddNoneRecord = false;
    private bool mDisplayOnlyEnabled = true;
    private bool? mUsingGlobalObjects = null;
    private bool mAppendGlobalItems = false;
    private int mSiteId = -1;
    private string mAdditionalItems = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            if (this.mUseStatusNameForSelection)
            {
                return this.PublicStatusName;
            }
            else
            {
                return this.PublicStatusID;
            }
        }
        set
        {
            EnsureChildControls();
            if (this.mUseStatusNameForSelection)
            {
                this.PublicStatusName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.PublicStatusID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the PublicStatus ID.
    /// </summary>
    public int PublicStatusID
    {
        get
        {
            EnsureChildControls();
            if (this.mUseStatusNameForSelection)
            {
                string name = ValidationHelper.GetString(uniSelector.Value, "");
                PublicStatusInfo psi = PublicStatusInfoProvider.GetPublicStatusInfo(name, SiteInfoProvider.GetSiteName(SiteID));
                if (psi != null)
                {
                    return psi.PublicStatusID;
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
            if (this.mUseStatusNameForSelection)
            {
                PublicStatusInfo psi = PublicStatusInfoProvider.GetPublicStatusInfo(value);
                if (psi != null)
                {
                    this.uniSelector.Value = psi.PublicStatusID;
                }
            }
            else
            {
                this.uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the PublicStatus code name.
    /// </summary>
    public string PublicStatusName
    {
        get
        {
            EnsureChildControls();
            if (this.mUseStatusNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelector.Value, "");
            }
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);
                PublicStatusInfo psi = PublicStatusInfoProvider.GetPublicStatusInfo(id);
                if (psi != null)
                {
                    return psi.PublicStatusName;
                }
                return "";
            }
        }
        set
        {
            EnsureChildControls();
            if (this.mUseStatusNameForSelection)
            {
                this.uniSelector.Value = value;
            }
            else
            {
                PublicStatusInfo psi = PublicStatusInfoProvider.GetPublicStatusInfo(value, SiteInfoProvider.GetSiteName(SiteID));
                if (psi != null)
                {
                    this.uniSelector.Value = psi.PublicStatusName;
                }
            }
        }
    }


    /// <summary>
    ///  If true, selected value is PublicStatusName, if false, selected value is PublicStatusID.
    /// </summary>
    public bool UseStatusNameForSelection
    {
        get
        {
            return mUseStatusNameForSelection;
        }
        set
        {
            mUseStatusNameForSelection = value;
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
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            uniSelector.StopProcessing = value;
        }
    }


    /// <summary>
    /// Inner control.
    /// </summary>
    public DropDownList InnerControl
    {
        get
        {
            EnsureChildControls();
            return this.uniSelector.DropDownSingleSelect;
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
            return this.uniSelector.DropDownSingleSelect.ClientID;
        }
    }


    /// <summary>
    /// Allows to display statuses only for specified site id. Use 0 for global statuses. Default value is current site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId == -1)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;
            mUsingGlobalObjects = null;
        }
    }


    /// <summary>
    /// Allows to display only enabled items. Default value is true.
    /// </summary>
    public bool DisplayOnlyEnabled
    {
        get
        {
            return mDisplayOnlyEnabled;
        }
        set
        {
            mDisplayOnlyEnabled = value;
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
    /// Indicates if global items have to be included in the list. Useful when using selector in filter.
    /// </summary>
    public bool AppendGlobalItems
    {
        get
        {
            return mAppendGlobalItems;
        }
        set
        {
            mAppendGlobalItems = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Returns true if site given by SiteID uses global order statuses.
    /// </summary>
    protected bool UsingGlobalObjects
    {
        get
        {
            // Unknown yet
            if (!mUsingGlobalObjects.HasValue)
            {
                mUsingGlobalObjects = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mUsingGlobalObjects = ECommerceSettings.UseGlobalPublicStatus(si.SiteName);
                }
            }

            return mUsingGlobalObjects.Value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        this.uniSelector.EnabledColumnName = "PublicStatusEnabled";
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowAll = this.AddAllItemsRecord;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;
        this.uniSelector.ReturnColumnName = (this.UseStatusNameForSelection ? "PublicStatusName" : "PublicStatusID");

        SetupWhereCondition();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        SetupWhereCondition();

        if (URLHelper.IsPostback() && this.DependsOnAnotherField)
        {
            pnlUpdate.Update();
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

        string where = "";
        // Select records by speciffied site
        if (SiteID >= 0)
        {
            // Show global records by default
            int filteredSiteId = 0;

            // Check configuration when site specified
            if (SiteID > 0)
            {
                // Show site specific records when not using global statuses
                filteredSiteId = UsingGlobalObjects ? 0 : SiteID;
            }

            where += "(ISNULL(PublicStatusSiteID, 0) = " + filteredSiteId + ") ";
        }

        // Append global items if requested
        if (AppendGlobalItems && (!string.IsNullOrEmpty(where)))
        {
            where = SqlHelperClass.AddWhereCondition(where, "PublicStatusSiteID IS NULL", "OR");
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PublicStatusEnabled = 1");
        }

        // Add items which have to be on the list
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if ((!string.IsNullOrEmpty(where)) && (!string.IsNullOrEmpty(additionalList)))
        {
            where = SqlHelperClass.AddWhereCondition(where, "PublicStatusID IN (" + additionalList + ")", "OR");
        }

        // Selected value must be on the list
        if ((!string.IsNullOrEmpty(where)) && (PublicStatusID > 0))
        {
            where = SqlHelperClass.AddWhereCondition(where, "PublicStatusID = " + PublicStatusID, "OR");
        }

        // Set where condition
        this.uniSelector.WhereCondition = where;

        if (this.UseStatusNameForSelection)
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
