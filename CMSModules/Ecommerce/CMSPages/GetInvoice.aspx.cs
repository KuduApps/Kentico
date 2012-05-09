using System;

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_CMSPages_GetInvoice : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int orderId = QueryHelper.GetInteger("orderid", 0);
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);

        if (oi != null)
        {
            CustomerInfo customer = CustomerInfoProvider.GetCustomerInfoByUserID(CMSContext.CurrentUser.UserID);

            if (((customer != null) && (oi.OrderCustomerID == customer.CustomerID)) || CMSContext.CurrentUser.IsGlobalAdministrator || (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "EcommerceRead")))
            {
                ltlInvoice.Text = URLHelper.MakeLinksAbsolute(oi.OrderInvoice);
                
            }
        }
    }
}
