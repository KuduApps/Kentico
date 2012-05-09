using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_TaxClasses : CMSShippingOptionsPage
{
    #region "Variables"

    protected int mShippingOptionId = 0;
    protected string mCurrentValues = string.Empty;
    protected ShippingOptionInfo mShippingOptionInfoObj = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for Tax clases
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.ShippingOptions.TaxClasses"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.ShippingOptions.TaxClasses");
        }

        lblAvialable.Text = GetString("com.shippingoption.taxes");
        mShippingOptionId = QueryHelper.GetInteger("shippingoptionid", 0);
        if (mShippingOptionId > 0)
        {
            mShippingOptionInfoObj = ShippingOptionInfoProvider.GetShippingOptionInfo(mShippingOptionId);
            EditedObject = mShippingOptionInfoObj;

            if (mShippingOptionInfoObj != null)
            {
                // Check object's site id
                CheckEditedObjectSiteID(mShippingOptionInfoObj.ShippingOptionSiteID);

                DataSet ds = ShippingOptionTaxClassInfoProvider.GetShippingOptionTaxClasses(mShippingOptionId);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    mCurrentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "TaxClassID"));
                }

                if (!RequestHelper.IsPostBack())
                {
                    uniSelector.Value = mCurrentValues;
                }
            }
        }

        uniSelector.IconPath = GetObjectIconUrl("ecommerce.taxclass", "object.png");
        uniSelector.OnSelectionChanged += uniSelector_OnSelectionChanged;
        uniSelector.OrderBy = "TaxClassDisplayName";
        uniSelector.WhereCondition = GetSelectorWhereCondition();
    }

    #endregion


    #region "Protected methods"

    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveItems();
    }


    /// <summary>
    /// Saves selection (removes unchecked, adds checked).
    /// </summary>
    protected void SaveItems()
    {
        if (mShippingOptionInfoObj == null)
        {
            return;
        }

        // Check permissions
        CheckConfigurationModification(mShippingOptionInfoObj.ShippingOptionSiteID);

        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, mCurrentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int taxClassId = ValidationHelper.GetInteger(item, 0);
                    ShippingOptionTaxClassInfoProvider.RemoveTaxClassFromShippingOption(mShippingOptionId, taxClassId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(mCurrentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int taxClassId = ValidationHelper.GetInteger(item, 0);
                    ShippingOptionTaxClassInfoProvider.AddTaxClassToShippingOption(mShippingOptionId, taxClassId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }


    /// <summary>
    /// Returns where condition for uniselector. This condition filters records contained in currently selected values,
    /// global records according to offerGlobalObjects paramter and site-specific records according to edited objects 
    /// site Id.
    /// </summary>
    protected string GetSelectorWhereCondition()
    {
        // Select nothing
        string where = "(1=0) ";

        // Add records which are used by parent object
        if (!string.IsNullOrEmpty(mCurrentValues))
        {
            where += "OR (TaxClassID IN (" + mCurrentValues.Replace(';', ',') + ")) ";
        }

        int taxSiteId = 0;
        // Add site specific records when editing site shipping option and not using global tax classes
        if ((mShippingOptionInfoObj != null) && (mShippingOptionInfoObj.ShippingOptionSiteID > 0))
        {
            if (!ECommerceSettings.UseGlobalTaxClasses(SiteInfoProvider.GetSiteName(mShippingOptionInfoObj.ShippingOptionSiteID)))
            {
                taxSiteId = mShippingOptionInfoObj.ShippingOptionSiteID;
            }
        }

        where += "OR (ISNULL(TaxClassSiteID, 0) = " + taxSiteId + ") ";

        return where;
    }

    #endregion
}
