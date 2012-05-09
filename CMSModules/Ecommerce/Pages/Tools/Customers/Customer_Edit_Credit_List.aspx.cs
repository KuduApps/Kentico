using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Credit_List : CMSCustomersPage
{
    #region "Variables"

    private int customerId = 0;
    private CustomerInfo customerObj = null;
    private int creditCurrencySiteId = -1;
    private CurrencyInfo currency = null;

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Customers.Credit"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Customers.Credit");
        }

        // Get site id of credits main currency
        creditCurrencySiteId = ECommerceHelper.GetSiteID(CMSContext.CurrentSiteID, ECommerceSettings.USE_GLOBAL_CREDIT);

        // Get currency in which credit is expressed in
        currency = CurrencyInfoProvider.GetMainCurrency(creditCurrencySiteId);

        // Get customerId from url
        customerId = QueryHelper.GetInteger("customerid", 0);

        // Load customer info
        customerObj = CustomerInfoProvider.GetCustomerInfo(customerId);

        // Check if customer belongs to current site
        if (!CheckCustomerSiteID(customerObj))
        {
            customerObj = null;
        }

        // Check, if edited customer exists
        EditedObject = customerObj;

        // Init unigrid
        UniGrid.HideControlForZeroRows = true;
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.OrderBy = "EventDate DESC, EventName ASC";
        UniGrid.WhereCondition = "EventCustomerID = " + customerId + " AND ISNULL(EventSiteID, 0) = " + creditCurrencySiteId;

        if (customerObj != null)
        {
            InitializeMasterPage();

            // Configuring global records
            if (creditCurrencySiteId == 0)
            {
                // Show "using global settings" info message only if showing global store settings
                lblGlobalInfo.Visible = true;
                lblGlobalInfo.Text = GetString("com.UsingGlobalSettings");
            }

            // Display customer total credit        
            lblCredit.Text = GetString("CreditEvent_List.TotalCredit");
            lblCreditValue.Text = GetFormatedTotalCredit();
        }
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set the action link
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("CreditEvent_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_Edit_Credit_Edit.aspx?customerid=" + customerId + "&siteId=" + SiteID;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Ecommerce/addcreditevent.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }

    /// <summary>
    /// Returns formated total credit string.
    /// </summary>
    private string GetFormatedTotalCredit()
    {
        // Get total credit
        double totalCredit = CreditEventInfoProvider.GetTotalCredit(customerId, CMSContext.CurrentSiteID);

        // Return formated total credit according to the credits main currency format string
        return CurrencyInfoProvider.GetFormattedPrice(totalCredit, currency);
    }

    #endregion


    #region "Event Handlers"

    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // Show only date part from date-time value
        switch (sourceName.ToLower())
        {
            case "eventdate":
                DateTime date = ValidationHelper.GetDateTime(parameter, DataHelper.DATETIME_NOT_SELECTED);
                if (date != DataHelper.DATETIME_NOT_SELECTED)
                {
                    return date.ToShortDateString();
                }
                else
                {
                    return "";
                }

            case "eventcreditchange":
                return CurrencyInfoProvider.GetFormattedPrice(ValidationHelper.GetDouble(parameter, 0), currency);
        }

        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("Customer_Edit_Credit_Edit.aspx?customerid=" + customerId + "&eventid=" + Convert.ToString(actionArgument) + "&siteId=" + SiteID);
        }
        else if (actionName == "delete")
        {
            // Check customer modification permission
            if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
            }

            // Check if using global credit
            if (ECommerceSettings.UseGlobalCredit(CMSContext.CurrentSiteName))
            {
                // Check Ecommerce global modify permission
                if (!ECommerceContext.IsUserAuthorizedForPermission("EcommerceGlobalModify"))
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
                }
            }
            
            // Get event info object
            int eventId = Convert.ToInt32(actionArgument);
            CreditEventInfo eventInfo = CreditEventInfoProvider.GetCreditEventInfo(eventId);

            // Check if deleted event exists and whether it belongs to edited customer
            if ((eventInfo != null) && (eventInfo.EventCustomerID == customerId))
            {
                // Delete CreditEventInfo object from database
                CreditEventInfoProvider.DeleteCreditEventInfo(eventInfo);
                UniGrid.ReloadData();
                lblCreditValue.Text = GetFormatedTotalCredit();
            }
        }
    }

    #endregion
}
