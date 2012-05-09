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
using CMS.UIControls;
using CMS.FormControls;
using CMS.FormEngine;

public partial class CMSModules_Ecommerce_FormControls_SKUSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mDisplayProductOptions = false;
    private bool mDisplayOnlyEnabled = true;
    private bool? mAllowGlobalProducts = null;
    private bool? mAllowGlobalOptions = null;
    private bool? mDisplaySiteProducts = null;
    private bool? mDisplayGlobalProducts = null;
    private bool? mDisplaySiteOptions = null;
    private bool? mDisplayGlobalOptions = null;
    private int mSiteId = -1;
    private int mUserId = 0;
    private string mAdditionalItems = "";

    private bool mAllowEmpty = false;
    private bool mUseGUIDForSelection = false;
    private bool mDisplayStandardProducts = true;
    private bool mDisplayMemberships = true;
    private bool mDisplayEproducts = true;
    private bool mDisplayDonations = true;
    private bool mDisplayBundles = true;
    private bool mDisplayOnlyProductsWithoutOptions = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether also product options should be displayed.
    /// </summary>
    public bool DisplayProductOptions
    {
        get
        {
            return mDisplayProductOptions;
        }
        set
        {
            mDisplayProductOptions = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                return this.SKUGUID;
            }
            // Use ID
            else
            {
                return this.SKUID;
            }
        }
        set
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                this.SKUGUID = ValidationHelper.GetGuid(value, Guid.Empty);
            }
            // Use ID
            else
            {
                this.SKUID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the SKU ID.
    /// </summary>
    public int SKUID
    {
        get
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                Guid guid = ValidationHelper.GetGuid(this.uniSelector.Value, Guid.Empty);

                SKUInfo skui = SKUInfoProvider.GetSKUInfo(guid);

                return (skui != null) ? skui.SKUID : 0;
            }
            // Use ID
            else
            {
                return ValidationHelper.GetInteger(uniSelector.Value, 0);
            }
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                SKUInfo skui = SKUInfoProvider.GetSKUInfo(value);

                if (skui != null)
                {
                    this.uniSelector.Value = skui.SKUGUID;
                }
            }
            // Use ID
            else
            {
                this.uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Selected SKU GUID.
    /// </summary>
    public Guid SKUGUID
    {
        get
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                return ValidationHelper.GetGuid(this.uniSelector.Value, Guid.Empty);
            }
            // Use ID
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);

                SKUInfo skui = SKUInfoProvider.GetSKUInfo(id);

                return (skui != null) ? skui.SKUGUID : Guid.Empty;
            }
        }
        set
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                this.uniSelector.Value = value;
            }
            // Use ID
            else
            {
                SKUInfo skui = SKUInfoProvider.GetSKUInfo(value);

                if (skui != null)
                {
                    this.uniSelector.Value = skui.SKUID;
                }
            }
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
            return this.uniSelector.TextBoxSelect.ClientID;
        }
    }

    /// <summary>
    /// Indicates if site products will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplaySiteProducts
    {
        get
        {
            // Unknown yet
            if (!mDisplaySiteProducts.HasValue)
            {
                // Display site item when working with site records
                mDisplaySiteProducts = (SiteID != 0);
            }

            return mDisplaySiteProducts.Value;
        }
        set
        {
            mDisplaySiteProducts = value;
        }
    }


    /// <summary>
    /// Indicates if global products will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplayGlobalProducts
    {
        get
        {
            // Unknown yet
            if (!mDisplayGlobalProducts.HasValue)
            {
                mDisplayGlobalProducts = false;
                if ((SiteID == 0) || AllowGlobalProducts)
                {
                    mDisplayGlobalProducts = true;
                }
            }

            return mDisplayGlobalProducts.Value;
        }
        set
        {
            mDisplayGlobalProducts = value;
        }
    }


    /// <summary>
    /// Indicates if site options will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplaySiteOptions
    {
        get
        {
            // Unknown yet
            if (!mDisplaySiteOptions.HasValue)
            {
                // Display site item when working with site records
                mDisplaySiteOptions = (SiteID != 0);
            }

            return mDisplaySiteOptions.Value;
        }
        set
        {
            mDisplaySiteOptions = value;
        }
    }


    /// <summary>
    /// Indicates if global options will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplayGlobalOptions
    {
        get
        {
            // Unknown yet
            if (!mDisplayGlobalOptions.HasValue)
            {
                mDisplayGlobalOptions = false;
                if ((SiteID == 0) || AllowGlobalOptions)
                {
                    mDisplayGlobalOptions = true;
                }
            }

            return mDisplayGlobalOptions.Value;
        }
        set
        {
            mDisplayGlobalOptions = value;
        }
    }


    /// <summary>
    /// Allows to display SKUs only for specified site id. Use 0 for global SKUs levels. Default value is current site id. 
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

            mDisplayGlobalProducts = null;
            mDisplaySiteProducts = null;
            mAllowGlobalOptions = null;
            mAllowGlobalProducts = null;
        }
    }


    /// <summary>
    /// ID of the user. If specified only products from users departments will be offered. 
    /// </summary>
    public int UserID
    {
        get
        {
            return mUserId;
        }
        set
        {
            mUserId = value;
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
    /// UniSelector object used for selection.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return uniSelector;
        }
    }


    /// <summary>
    /// Indicates if it should be possible to clear the selection.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowEmpty"), this.mAllowEmpty);
        }
        set
        {
            this.SetValue("AllowEmpty", value);
            this.mAllowEmpty = value;
        }
    }


    /// <summary>
    /// Indicates if SKU GUID is used for SKU selection.
    /// </summary>
    public bool UseGUIDForSelection
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseGUIDForSelection"), this.mUseGUIDForSelection);
        }
        set
        {
            this.SetValue("UseGUIDForSelection", value);
            this.mUseGUIDForSelection = value;
        }
    }


    /// <summary>
    /// Indicates if standard products will be displayed.
    /// </summary>
    public bool DisplayStandardProducts
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayStandardProducts"), this.mDisplayStandardProducts);
        }
        set
        {
            this.SetValue("DisplayStandardProducts", value);
            this.mDisplayStandardProducts = value;
        }
    }


    /// <summary>
    /// Indicates if memberships will be displayed.
    /// </summary>
    public bool DisplayMemberships
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayMemberships"), this.mDisplayMemberships);
        }
        set
        {
            this.SetValue("DisplayMemberships", value);
            this.mDisplayMemberships = value;
        }
    }


    /// <summary>
    /// Indicates if e-products will be displayed.
    /// </summary>
    public bool DisplayEproducts
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayEproducts"), this.mDisplayEproducts);
        }
        set
        {
            this.SetValue("DisplayEproducts", value);
            this.mDisplayEproducts = value;
        }
    }


    /// <summary>
    /// Indicates if donations will be displayed.
    /// </summary>
    public bool DisplayDonations
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayDonations"), this.mDisplayDonations);
        }
        set
        {
            this.SetValue("DisplayDonations", value);
            this.mDisplayDonations = value;
        }
    }


    /// <summary>
    /// Indicates if bundles will be displayed.
    /// </summary>
    public bool DisplayBundles
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayBundles"), this.mDisplayBundles);
        }
        set
        {
            this.SetValue("DisplayBundles", value);
            this.mDisplayBundles = value;
        }
    }


    /// <summary>
    /// Display only products without product options.
    /// </summary>
    public bool DisplayOnlyProductsWithoutOptions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayOnlyProductsWithoutOptions"), this.mDisplayOnlyProductsWithoutOptions);
        }
        set
        {
            this.SetValue("DisplayOnlyProductsWithoutOptions", value);
            this.mDisplayOnlyProductsWithoutOptions = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Returns true if site given by SiteID uses global products beside site-specific ones.
    /// </summary>
    protected bool AllowGlobalProducts
    {
        get
        {
            // Unknown yet
            if (!mAllowGlobalProducts.HasValue)
            {
                mAllowGlobalProducts = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mAllowGlobalProducts = ECommerceSettings.AllowGlobalProducts(si.SiteName);
                }
            }

            return mAllowGlobalProducts.Value;
        }
    }


    /// <summary>
    /// Returns true if site given by SiteID uses global options beside site-specific ones.
    /// </summary>
    protected bool AllowGlobalOptions
    {
        get
        {
            // Unknown yet
            if (!mAllowGlobalOptions.HasValue)
            {
                mAllowGlobalOptions = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mAllowGlobalOptions = ECommerceSettings.AllowGlobalProductOptions(si.SiteName);
                }
            }

            return mAllowGlobalOptions.Value;
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
            return;
        }

        ReloadData();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.ReturnColumnName = (this.UseGUIDForSelection) ? "SKUGUID" : "SKUID";
        this.uniSelector.AllowEmpty = this.AllowEmpty;

        string where = "";

        // Add global products
        if (DisplayGlobalProducts)
        {
            where = SqlHelperClass.AddWhereCondition(where, "SKUOptionCategoryID IS NULL AND SKUSiteID IS NULL", "OR");
        }

        // Add site specific products
        if (DisplaySiteProducts)
        {
            where = SqlHelperClass.AddWhereCondition(where, "SKUOptionCategoryID IS NULL AND SKUSiteID = " + SiteID, "OR");
        }

        // Exclude standard products if needed
        if (!this.DisplayStandardProducts)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Product)));
        }

        // Exclude memberships if needed
        if (!this.DisplayMemberships)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Membership)));
        }

        // Exclude e-products if needed
        if (!this.DisplayEproducts)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.EProduct)));
        }

        // Exclude donations if needed
        if (!this.DisplayDonations)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Donation)));
        }

        // Exclude bundles if needed
        if (!this.DisplayBundles)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Bundle)));
        }

        // Exclude products with product options if needed
        if (this.DisplayOnlyProductsWithoutOptions)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUID NOT IN (SELECT SKUID FROM COM_SKUOptionCategory)"));
        }

        if (this.DisplayProductOptions)
        {
            // Add global options
            if (DisplayGlobalOptions)
            {
                where = SqlHelperClass.AddWhereCondition(where, "SKUOptionCategoryID IS NOT NULL AND SKUSiteID IS NULL", "OR");
            }

            // Add site specific options
            if (DisplaySiteOptions)
            {
                where = SqlHelperClass.AddWhereCondition(where, "SKUOptionCategoryID IS NOT NULL AND SKUSiteID = " + SiteID, "OR");
            }
        }

        // Fitler out only product from users departments
        if (this.UserID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("(SKUDepartmentID IN (SELECT DepartmentID FROM COM_UserDepartment WHERE UserID = {0}) OR (SKUDepartmentID IS NULL))", this.UserID));
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "SKUEnabled = 1");
        }

        if (!this.DisplayProductOptions)
        {
            where = SqlHelperClass.AddWhereCondition(where, "SKUOptionCategoryID IS NULL");
        }

        // Add items which have to be on the list
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if (!string.IsNullOrEmpty(additionalList))
        {
            where = SqlHelperClass.AddWhereCondition(where, "SKUID IN (" + additionalList + ")", "OR");
        }

        // Selected value must be on the list
        if (SKUID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "SKUID = " + SKUID, "OR");
        }

        this.uniSelector.WhereCondition = where;
    }


    /// <summary>
    /// Returns WHERE condition for selected form.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Return correct WHERE condition for integer if none value is selected
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            int id = ValidationHelper.GetInteger(uniSelector.Value, 0);
            if (id > 0)
            {
                return base.GetWhereCondition();
            }
        }
        return null;
    }

    #endregion
}
