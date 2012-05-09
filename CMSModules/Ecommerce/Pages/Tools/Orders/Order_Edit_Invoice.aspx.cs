using System;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Invoice : CMSOrdersPage
{
    int orderId;
    OrderInfo order;


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

        // Register the dialog script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ScriptHelper.NEWWINDOW_SCRIPT_KEY, ScriptHelper.NewWindowScript);

        lblInvoiceNumber.Text = GetString("order_invoice.lblInvoiceNumber");
        btnGenerate.Text = GetString("order_invoice.btnGenerate");
        btnPrintPreview.Text = GetString("order_invoice.btnPrintPreview");

        if (QueryHelper.GetInteger("orderid", 0) != 0)
        {
            orderId = QueryHelper.GetInteger("orderid", 0);
        }
        order = OrderInfoProvider.GetOrderInfo(orderId);
        
        if (order == null)
        {
            btnGenerate.Enabled = false;
            btnPrintPreview.Enabled = false;
            return;
        }
        else
        {
            // Check order site ID
            CheckOrderSiteID(order.OrderSiteID);
        }

        ltlScript.Text = ScriptHelper.GetScript("function showPrintPreview() { NewWindow('Order_Edit_InvoicePrint.aspx?orderid=" + orderId + "', 'InvoicePrint', 650, 700);}");

        if (!RequestHelper.IsPostBack())
        {
            txtInvoiceNumber.Text = order.OrderInvoiceNumber;
            lblInvoice.Text = URLHelper.MakeLinksAbsolute(order.OrderInvoice);
        }
    }


    protected void btnGenerate_Click1(object sender, EventArgs e)
    {
        // check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }

        // Save updated order invoice number
        order.OrderInvoiceNumber = txtInvoiceNumber.Text;
        OrderInfoProvider.SetOrderInfo(order);

        // Generate and display new invoice
        string invoice = OrderInfoProvider.GetInvoice(orderId);
        lblInvoice.Text = URLHelper.MakeLinksAbsolute(invoice);
        
        // Save new invoice
        order.OrderInvoice = invoice;
        OrderInfoProvider.SetOrderInfo(order);

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");        
    }
}
