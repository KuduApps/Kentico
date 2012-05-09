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
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.UIControls;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_CustomFields : CMSCustomersPage
{
    protected int customerId;
    protected CustomerInfo customerObj;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Customers.CustomFields"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Customers.CustomFields");
        }

        // Set edit mode
        customerId = QueryHelper.GetInteger("customerId", 0);
        if (customerId > 0)
        {
            customerObj = CustomerInfoProvider.GetCustomerInfo(customerId);
            // Check if customer belongs to current site
            if (!CheckCustomerSiteID(customerObj))
            {
                customerObj = null;
            }

            EditedObject = customerObj;
            formCustomerCustomFields.Info = customerObj;
            formCustomerCustomFields.OnBeforeSave += formCustomerCustomFields_OnBeforeSave;
            formCustomerCustomFields.OnAfterSave += formCustomerCustomFields_OnAfterSave;
        }
        else
        {
            formCustomerCustomFields.Enabled = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (formCustomerCustomFields.BasicForm != null)
        {
            if (formCustomerCustomFields.BasicForm.FieldControls.Count <= 0)
            {
                // Hide submit button if no field are present
                formCustomerCustomFields.BasicForm.SubmitButton.Visible = false;
            }
            else
            {
                // Set submit button's css class
                formCustomerCustomFields.BasicForm.SubmitButton.CssClass = "ContentButton";
            }
        }
    }


    void formCustomerCustomFields_OnBeforeSave(object sender, EventArgs e)
    {
        if (customerObj == null)
        {
            return;
        }

        // Check module permissions
        if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
        }
    }


    void formCustomerCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        // Display 'changes saved' information
        this.lblInfo.Visible = true;
        this.lblInfo.Text = GetString("General.ChangesSaved");
    }
}
