using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_SupplierSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAddAllItemsRecord = false;
    private bool mAddNoneRecord = false;
    private bool mDisplayOnlyEnabled = true;
    private bool? mAllowGlobalObjects = null;
    private bool? mDisplaySiteItems = null;
    private bool? mDisplayGlobalItems = null;
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
            return this.SupplierID;
        }
        set
        {
            this.SupplierID = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Gets or sets the Supplier ID.
    /// </summary>
    public int SupplierID
    {
        get
        {
            return ValidationHelper.GetInteger(uniSelector.Value, 0);
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            this.uniSelector.Value = value;
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
            return this.uniSelector.DropDownSingleSelect.ClientID;
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
    /// Allows to display suppliers only for specified site id. Use 0 for global suppliers. Default value is current site id. 
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
    /// Id of items which has to be displayed regardless other settings. Use ',' or ';' as separator if more ids required.
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

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Returns true if site given by SiteID uses global suppliers beside site-specific ones.
    /// </summary>
    protected bool AllowGlobalObjects
    {
        get
        {
            // Unknown yet
            if (!mAllowGlobalObjects.HasValue)
            {
                mAllowGlobalObjects = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mAllowGlobalObjects = ECommerceSettings.AllowGlobalSuppliers(si.SiteName);
                }
            }

            return mAllowGlobalObjects.Value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            InitSelector();
        }
    }


    /// <summary>
    /// Inits the selector.
    /// </summary>
    protected void InitSelector()
    {
        this.uniSelector.EnabledColumnName = "SupplierEnabled";
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowAll = this.AddAllItemsRecord;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;

        string where = "";        
        // Add global items
        if (DisplayGlobalItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(SupplierSiteID IS NULL)", "OR");
        }
        // Add site specific items
        if (DisplaySiteItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(SupplierSiteID = " + SiteID + ")", "OR");
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "SupplierEnabled = 1");
        }

        // Add items which have to be on the list
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if (!string.IsNullOrEmpty(additionalList))
        {
            where = SqlHelperClass.AddWhereCondition(where, "SupplierID IN (" + additionalList + ")", "OR");
        }

        // Selected value must be on the list
        if (SupplierID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(SupplierID = " + SupplierID + ")", "OR");
        }
  
        this.uniSelector.WhereCondition = where;
    }
}
