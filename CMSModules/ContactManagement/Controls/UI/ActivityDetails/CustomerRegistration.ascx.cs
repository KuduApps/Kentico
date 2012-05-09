using System;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_CustomerRegistration : ActivityDetail
{

    #region "Methods"

    public override bool LoadData(ActivityInfo ai)
    {
        if ((ai == null) || (ai.ActivityType != PredefinedActivityType.CUSTOMER_REGISTRATION) || !ModuleEntry.IsModuleLoaded(ModuleEntry.ECOMMERCE))
        {
            return false;
        }

        int customerId = ai.ActivityItemID;
        GeneralizedInfo iinfo = ModuleCommands.ECommerceGetCustomerInfo(customerId);
        if (iinfo != null)
        {
            string name = GetUserName(iinfo.GetValue("CustomerFirstName"), null,
                                      iinfo.GetValue("CustomerLastName"),
                                      iinfo.GetValue("CustomerEmail"), null);

            ucDetails.AddRow("om.activitydetails.regcustomer", name);
        }

        return ucDetails.IsDataLoaded;
    }

    #endregion
}

