using System;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_ProductAddedToShoppingCart : ActivityDetail
{

    #region "Methods"

    public override bool LoadData(ActivityInfo ai)
    {
        if ((ai == null) || !ModuleEntry.IsModuleLoaded(ModuleEntry.ECOMMERCE)) 
        {
            return false; ;
        }

        switch (ai.ActivityType)
        {
            case PredefinedActivityType.PRODUCT_ADDED_TO_WISHLIST:
            case PredefinedActivityType.PRODUCT_ADDED_TO_SHOPPINGCART:
            case PredefinedActivityType.PRODUCT_REMOVED_FROM_SHOPPINGCART:
                break;
            default:
                return false;
        }

        GeneralizedInfo iinfo = ModuleCommands.ECommerceGetSKUInfo(ai.ActivityItemID);
        if (iinfo != null)
        {
            string productName = ValidationHelper.GetString(iinfo.GetValue("SKUName"), null);
            ucDetails.AddRow("om.activitydetails.product", productName);

            if (ai.ActivityType != PredefinedActivityType.PRODUCT_ADDED_TO_WISHLIST)
            {
                ucDetails.AddRow("om.activitydetails.productunits", ai.ActivityValue);
            }
        }

        return ucDetails.IsDataLoaded;
    }

    #endregion
}

