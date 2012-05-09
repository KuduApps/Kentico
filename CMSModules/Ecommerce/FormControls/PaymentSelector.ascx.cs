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
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_PaymentSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private bool mAddNoneRecord = false;
    private int mShippingOptionId = 0;
    private ShippingOptionInfo mShippingOption = null;
    private bool mDisplayOnlyEnabled = true;
    private bool? mAllowGlobalObjects = null;
    private bool? mDisplaySiteItems = null;
    private bool? mDisplayGlobalItems = null;
    private bool mIncludeSelected = true;

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
            return this.PaymentID;
        }
        set
        {
            this.PaymentID = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Indicates if selector has data.
    /// </summary>
    public bool HasData
    {
        get
        {
            return uniSelector.HasData;
        }
    }


    /// <summary>
    /// Gets or sets the Payment ID.
    /// </summary>
    public int PaymentID
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
    /// Add none record to the dropdownlist.
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
    /// Indicates if selected value will be included in the list despite of other conditions.
    /// </summary>
    public bool IncludeSelected
    {
        get
        {
            return mIncludeSelected;
        }
        set
        {
            mIncludeSelected = value;
        }
    }


    /// <summary>
    /// Shipping option ID.
    /// </summary>
    public int ShippingOptionID
    {
        get
        {
            if ((mShippingOptionId == 0) && (mShippingOption != null))
            {
                mShippingOptionId = ShippingOption.ShippingOptionID;
            }

            return mShippingOptionId;
        }
        set
        {
            mShippingOptionId = value;

            mShippingOption = null;
            mSiteId = -1;
            mDisplayGlobalItems = null;
            mDisplaySiteItems = null;
            mAllowGlobalObjects = null;
        }
    }


    /// <summary>
    /// Shipping option info object.
    /// </summary>
    public ShippingOptionInfo ShippingOption
    {
        get
        {
            if ((mShippingOption == null) && (mShippingOptionId != 0))
            {
                mShippingOption = ShippingOptionInfoProvider.GetShippingOptionInfo(ShippingOptionID);
            }

            return mShippingOption;
        }
        set
        {
            mShippingOption = value;

            mShippingOptionId = 0;
            mSiteId = -1;
            mDisplayGlobalItems = null;
            mDisplaySiteItems = null;
            mAllowGlobalObjects = null;
        }
    }


    /// <summary>
    /// Returns inner DropDownList control.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Returns inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return this.uniSelector;
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
                // If shipping option given use its site id
                if (ShippingOption != null)
                {
                    mSiteId = ShippingOption.ShippingOptionSiteID;
                }
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;

            mDisplayGlobalItems = null;
            mDisplaySiteItems = null;
            mShippingOption = null;
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
    /// Indicates if only payment options that are allowed to be used without shipping are displayed.
    /// </summary>
    public bool DisplayOnlyAllowedIfNoShipping
    {
        get;
        set;
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
    /// Returns true if site given by SiteID uses global payment methods beside site-specific ones.
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
                    mAllowGlobalObjects = ECommerceSettings.AllowGlobalPaymentMethods(si.SiteName);
                }
            }

            return mAllowGlobalObjects.Value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }

    #endregion


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        this.uniSelector.EnabledColumnName = "PaymentOptionEnabled";
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;

        string shippingWhere = "";

        if (this.ShippingOptionID > 0)
        {
            shippingWhere = "PaymentOptionID IN (SELECT PaymentOptionID FROM COM_PaymentShipping WHERE ShippingOptionID = " + this.ShippingOptionID + ")";
        }

        string where = "(1=0)";
        // Add global items
        if (DisplayGlobalItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionSiteID IS NULL", "OR");
        }
        // Add site specific items
        if (DisplaySiteItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionSiteID = " + SiteID, "OR");
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionEnabled = 1");
        }

        // Filter out only payment options that are allowed to be used without shipping
        if (this.DisplayOnlyAllowedIfNoShipping)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionAllowIfNoShipping = 1");
        }

        // Add items which have to be on the list
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if (!string.IsNullOrEmpty(additionalList))
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionID IN (" + additionalList + ")", "OR");
        }

        // Selected value must be on the list
        if ((PaymentID > 0) && IncludeSelected)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionID = " + PaymentID, "OR");
        }

        this.uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(shippingWhere, where);
    }
}
