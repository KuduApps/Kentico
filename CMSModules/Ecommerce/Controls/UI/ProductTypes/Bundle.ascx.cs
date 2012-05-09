using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Ecommerce_Controls_UI_ProductTypes_Bundle : CMSUserControl
{
    #region "Variables"

    private string selectedProducts = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Bundle ID.
    /// </summary>
    public int BundleID
    {
        get;
        set;
    }


    /// <summary>
    /// Site ID.
    /// </summary>
    public int SiteID
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates how bundle product is removed from inventory.
    /// </summary>
    public BundleInventoryTypeEnum RemoveFromInventory
    {
        get
        {
            if (this.radRemoveBundle.Checked)
            {
                return BundleInventoryTypeEnum.RemoveBundle;
            }
            else if (this.radRemoveProducts.Checked)
            {
                return BundleInventoryTypeEnum.RemoveProducts;
            }
            else if (this.radRemoveBundleAndProducts.Checked)
            {
                return BundleInventoryTypeEnum.RemoveBundleAndProducts;
            }
            else
            {
                return BundleInventoryTypeEnum.RemoveBundle;
            }
        }
        set
        {
            this.radRemoveBundle.Checked = (value == BundleInventoryTypeEnum.RemoveBundle);
            this.radRemoveProducts.Checked = (value == BundleInventoryTypeEnum.RemoveProducts);
            this.radRemoveBundleAndProducts.Checked = (value == BundleInventoryTypeEnum.RemoveBundleAndProducts);
        }
    }


    /// <summary>
    /// Error message.
    /// </summary>
    public string ErrorMessage
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if control reload is required.
    /// </summary>
    public bool ReloadRequired
    {
        get;
        set;
    }

    #endregion


    #region "Events"

    public event EventHandler OnRemoveFromInventoryChanged;

    public event EventHandler OnProductsSelectionChangesSaved;

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // If bundle ID set
        if (this.BundleID != 0)
        {
            // Get selected products from database
            DataSet selectedProductsDataSet = BundleInfoProvider.GetBundles("BundleID = " + this.BundleID, "SKUID");

            if (!DataHelper.DataSourceIsEmpty(selectedProductsDataSet))
            {
                this.selectedProducts = TextHelper.Join(";", SqlHelperClass.GetStringValues(selectedProductsDataSet.Tables[0], "SKUID"));
            }
        }

        // If reload required
        if (this.ReloadRequired)
        {
            // Set selected products to selector
            this.productsUniSelector.Value = this.selectedProducts;
        }

        string where = null;

        // Exclude product options
        where = SqlHelperClass.AddWhereCondition(where, "SKUOptionCategoryID IS NULL");

        // Exclude bundle products
        where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Bundle)));

        // Exclude donation products
        where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUProductType <> '{0}'", SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Donation)));

        // Exclude edited product itself
        where = SqlHelperClass.AddWhereCondition(where, "SKUID <> " + this.BundleID);

        // If bundle is global
        if (this.SiteID == 0)
        {
            // Inlcude global products
            where = SqlHelperClass.AddWhereCondition(where, "SKUSiteID IS NULL");
        }
        else
        {
            // If global products are allowed on this site
            if (ECommerceSettings.AllowGlobalProducts(SiteInfoProvider.GetSiteName(this.SiteID)))
            {
                // Include global and site products
                where = SqlHelperClass.AddWhereCondition(where, String.Format("(SKUSiteID IS NULL) OR (SKUSiteID = {0})", this.SiteID));
            }
            else
            {
                // Include site products
                where = SqlHelperClass.AddWhereCondition(where, "SKUSiteID = " + this.SiteID);
            }
        }

        // Include only enabled products
        where = SqlHelperClass.AddWhereCondition(where, "SKUEnabled = 1");

        // Include only products from user's departments
        if (!ECommerceContext.IsUserAuthorizedForPermission("AccessAllDepartments"))
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("(SKUDepartmentID IN (SELECT DepartmentID FROM COM_UserDepartment WHERE UserID = {0}) OR (SKUDepartmentID IS NULL))", CMSContext.CurrentUser.UserID));
        }

        // Include currently selected products
        if (!string.IsNullOrEmpty(this.selectedProducts))
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUID IN ({0})", this.selectedProducts.Replace(';', ',')), "OR");
        }

        this.productsUniSelector.WhereCondition = where;
        this.productsUniSelector.SelectionMode = SelectionModeEnum.Multiple;
        this.productsUniSelector.OnSelectionChanged += new EventHandler(productsUniSelector_OnSelectionChanged);
        this.productsUniSelector.StopProcessing = this.StopProcessing;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Validates control.
    /// </summary>
    public string Validate()
    {
        return this.ErrorMessage;
    }


    /// <summary>
    /// Saves currently selected products to bundle.
    /// </summary>
    public void SaveProductsSelectionChanges()
    {
        string newSelection = ValidationHelper.GetString(this.productsUniSelector.Value, null);
        string selectionDifference = null;

        // Get deselected products
        selectionDifference = DataHelper.GetNewItemsInList(newSelection, this.selectedProducts);

        // Remove deselected products
        if (!String.IsNullOrEmpty(selectionDifference))
        {
            string[] items = selectionDifference.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (items != null)
            {
                // Remove products from bundle
                foreach (string item in items)
                {
                    int skuId = ValidationHelper.GetInteger(item, 0);
                    BundleInfoProvider.RemoveSKUFromBundle(this.BundleID, skuId);
                }
            }
        }

        // Get newly selected products
        selectionDifference = DataHelper.GetNewItemsInList(this.selectedProducts, newSelection);

        // Add newly selected products
        if (!String.IsNullOrEmpty(selectionDifference))
        {
            string[] items = selectionDifference.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (items != null)
            {
                // Add products to bundle
                foreach (string item in items)
                {
                    int skuId = ValidationHelper.GetInteger(item, 0);
                    BundleInfoProvider.AddSKUToBundle(this.BundleID, skuId);
                }
            }
        }

        if (this.OnProductsSelectionChangesSaved != null)
        {
            this.OnProductsSelectionChangesSaved(this, EventArgs.Empty);
        }
    }


    protected void RemoveFromInventoryRadioGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (this.OnRemoveFromInventoryChanged != null)
        {
            this.OnRemoveFromInventoryChanged(this, EventArgs.Empty);
        }
    }


    void productsUniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // If bundle exists
        if (this.BundleID != 0)
        {
            // Save product selection changes immediately
            this.SaveProductsSelectionChanges();
        }
        else
        {
            // Don't save product selection changes yet
            this.selectedProducts = ValidationHelper.GetString(this.productsUniSelector.Value, null);
        }
    }

    #endregion
}
