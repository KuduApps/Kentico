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
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_InvoicePrint : CMSOrdersPage
{
    protected override void OnPreInit(EventArgs e)
    {
        this.CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.Invoice"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.Invoice");
        }

        int orderId = QueryHelper.GetInteger("orderid", 0);
        OrderInfo order = OrderInfoProvider.GetOrderInfo(orderId);
        if (order != null)
        {
            lblInvoice.Text = URLHelper.MakeLinksAbsolute(order.OrderInvoice);
        }
    }
}
