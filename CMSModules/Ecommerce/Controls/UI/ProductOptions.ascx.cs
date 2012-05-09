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
using CMS.FormControls;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_UI_ProductOptions : CMSAdminControl
{
    #region "Private & protected variables"

    protected string currentValues = string.Empty;
    private int mProductId = 0;
    private SKUInfo mProduct = null;
    private bool mDisplayOnlyEnabled = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Product ID.
    /// </summary>
    public int ProductID
    {
        get
        {
            return mProductId;
        }
        set
        {
            mProductId = value;
            mProduct = null;
        }
    }


    /// <summary>
    /// Product info object.
    /// </summary>
    public SKUInfo Product
    {
        get
        {
            if (mProduct == null)
            {
                mProduct = SKUInfoProvider.GetSKUInfo(mProductId);
            }

            return mProduct;
        }
        set
        {
            mProduct = value;

            mProductId = 0;
            if (value != null)
            {
                mProductId = value.SKUID;
            }
        }
    }


    /// <summary>
    /// Indicates whether form is enabled.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return this.uniSelector.Enabled;
        }
        set
        {
            this.uniSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates whether only enabled product options are to be listed. Default value is true.
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
    /// Uniselector for selecting options.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return this.uniSelector;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvailable.Text = GetString("com.sku.categoriesavailable");
        if (ProductID > 0)
        {
            // Get the active users
            DataSet ds = OptionCategoryInfoProvider.GetSKUOptionCategories(ProductID, false);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "CategoryID"));
            }

            this.uniSelector.WhereCondition = GetWhereCondition(currentValues);

            if (!RequestHelper.IsPostBack())
            {
                this.uniSelector.Value = currentValues;
            }
        }

        this.uniSelector.IconPath = GetObjectIconUrl("ecommerce.optioncategory", "object.png");
    }


    /// <summary>
    /// Saves selected item. No permission checks are performed.
    /// </summary>
    public void SaveItems()
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int categoryId = ValidationHelper.GetInteger(item, 0);
                    SKUOptionCategoryInfoProvider.RemoveOptionCategoryFromSKU(categoryId, ProductID);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int categoryId = ValidationHelper.GetInteger(item, 0);
                    SKUOptionCategoryInfoProvider.AddOptionCategoryToSKU(categoryId, ProductID);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }


    /// <summary>
    /// Returns Where condition according to global object settings.
    /// </summary>
    /// <param name="values"></param>
    protected string GetWhereCondition(string values)
    {
        string where = "";

        if (Product != null)
        {
            // Offer global product options classes for global products or when using global tax classes
            if (Product.IsGlobal)
            {
                where = "CategorySiteID IS NULL";
            }
            else
            {
                where = "CategorySiteID = " + Product.SKUSiteID;

                if (ECommerceSettings.AllowGlobalProductOptions(CMSContext.CurrentSiteName))
                {
                    where = SqlHelperClass.AddWhereCondition(where, "CategorySiteID IS NULL", "OR");
                }
            }
        }

        if (DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "CategoryEnabled = 1");
        }

        // Include selected values
        if (!string.IsNullOrEmpty(currentValues))
        {
            where = SqlHelperClass.AddWhereCondition(where, "CategoryID IN (" + currentValues.Replace(';', ',') + ")", "OR");
        }

        return where;
    }
}
