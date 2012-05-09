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
using CMS.FormControls;
using CMS.Ecommerce;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_ManufacturerSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAddAllItemsRecord = true;
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
            return this.ManufacturerID;
        }
        set
        {
            this.ManufacturerID = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Gets or sets the Manufacturer ID.
    /// </summary>
    public int ManufacturerID
    {
        get
        {
            return ValidationHelper.GetInteger(uniSelector.Value, 0);
        }
        set
        {
            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the Manufacturer code name.
    /// </summary>
    public string ManufacturerName
    {
        get
        {
            int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);
            ManufacturerInfo mi = ManufacturerInfoProvider.GetManufacturerInfo(id);
            if (mi != null)
            {
                return mi.ManufacturerDisplayName;
            }
            return "";
        }
        set
        {
            ManufacturerInfo mi = ManufacturerInfoProvider.GetManufacturerInfo(value, SiteInfoProvider.GetSiteName(this.SiteID));
            if (mi != null)
            {
                this.uniSelector.Value = mi.ManufacturerID;
            }
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
    /// Inner control.
    /// </summary>
    public DropDownList InnerControl
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect;
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
    /// Allows to display manufacturers only for specified site id. Use 0 for global manufacturers. Default value is current site id.
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
    /// Returns true if site given by SiteID uses global manufacturers beside site-specific ones.
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
                    mAllowGlobalObjects = ECommerceSettings.AllowGlobalManufacturers(si.SiteName);
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
            this.uniSelector.EnabledColumnName = "ManufacturerEnabled";
            this.uniSelector.IsLiveSite = this.IsLiveSite;
            this.uniSelector.AllowAll = this.AddAllItemsRecord;
            this.uniSelector.AllowEmpty = this.AddNoneRecord;

            SetupWhereCondition();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        SetupWhereCondition();
    }


    /// <summary>
    /// Inits the selector.
    /// </summary>
    protected void SetupWhereCondition()
    {


        string where = "";
        // Add global items
        if (DisplayGlobalItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ManufacturerSiteID IS NULL", "OR");
        }
        // Add site specific items
        if (DisplaySiteItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ManufacturerSiteID = " + SiteID, "OR");
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ManufacturerEnabled = 1");
        }

        // Add items which have to be on the list
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if (!string.IsNullOrEmpty(additionalList))
        {
            where = SqlHelperClass.AddWhereCondition(where, "ManufacturerID IN (" + additionalList + ")", "OR");
        }

        // Selected value must be on the list
        if (ManufacturerID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(ManufacturerID = " + ManufacturerID + ")", "OR");
        }

        this.uniSelector.WhereCondition = where;
    }
}
