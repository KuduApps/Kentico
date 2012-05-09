using System;

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.GlobalHelper;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_CustomFields : CMSOrdersPage
{
    protected int orderId;


    protected override void OnPreInit(EventArgs e)
    {        
        CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.CustomFields"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.CustomFields");
        }

        // set edit mode
        orderId = QueryHelper.GetInteger("orderId", 0);
        if (orderId > 0)
        {
            OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);

            if (oi != null)
            {
                // Check order site ID
                CheckOrderSiteID(oi.OrderSiteID);
            }

            formOrderCustomFields.Info = oi;
            EditedObject = formOrderCustomFields.Info;
            formOrderCustomFields.OnBeforeSave += formOrderCustomFields_OnBeforeSave;
            formOrderCustomFields.OnAfterSave += formOrderCustomFields_OnAfterSave;
        }
        else
        {
            formOrderCustomFields.Enabled = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (formOrderCustomFields.BasicForm != null)
        {
            if (formOrderCustomFields.BasicForm.FieldControls.Count <= 0)
            {
                // Hide submit button if no field is present
                formOrderCustomFields.BasicForm.SubmitButton.Visible = false;
            }
            else
            {
                // Set submit button's css class
                formOrderCustomFields.BasicForm.SubmitButton.CssClass = "ContentButton";
            }
        }
    }


    void formOrderCustomFields_OnBeforeSave(object sender, EventArgs e)
    {
        // Check 'EcommerceModify' permission
        if ((CMSContext.CurrentSite != null) && (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders")))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }
    }


    void formOrderCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        // Display 'changes saved' information
        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}
