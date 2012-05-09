using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Address_List : CMSCustomersPage
{
    #region "Variables"

    protected int customerId = 0;
    protected CustomerInfo customerObj = null;

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Customers.Addresses"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Customers.Addresses");
        }

        customerId = QueryHelper.GetInteger("customerId", 0);
        customerObj = CustomerInfoProvider.GetCustomerInfo(customerId);

        // Check if customer belongs to current site
        if (!CheckCustomerSiteID(customerObj))
        {
            customerObj = null;
        }

        EditedObject = customerObj;

        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.WhereCondition = "AddressCustomerID = " + customerId;

        // Intialize the master page elements
        InitializeMasterPage();
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Intializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Customer_Edit_Address_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_Edit_Address_Edit.aspx?customerId=" + customerId;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_Address/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }

    #endregion


    #region "Event Handlers"

    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "addressenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
            case "addressisbilling":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
            case "addressisshipping":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
            case "addressiscompany":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
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
            URLHelper.Redirect("Customer_Edit_Address_Edit.aspx?customerId=" + customerId + "&addressId=" + Convert.ToString(actionArgument));
        }
        else if (actionName == "delete")
        {
            if (customerObj == null)
            {
                return;
            }

            if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
            }

            int addressId = ValidationHelper.GetInteger(actionArgument, 0);
            // Check for the address dependences            
            if (AddressInfoProvider.CheckDependencies(addressId))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            // Delete AddressInfo object from database
            AddressInfoProvider.DeleteAddressInfo(addressId);
        }
    }

    #endregion
}
