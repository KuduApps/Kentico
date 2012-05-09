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
using CMS.LicenseProvider;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_OrderItems : CMSOrdersPage
{
    protected int orderId = 0;
    private string mSessionKey = "CMSDeskOrderItemsShoppingCart";


    /// <summary>
    /// Shopping cart to use.
    /// </summary>
    private ShoppingCartInfo ShoppingCart
    {
        get
        {
            if (SessionHelper.GetValue(mSessionKey) == null)
            {
                SessionHelper.SetValue(mSessionKey, ShoppingCartInfoProvider.GetShoppingCartInfoFromOrder(orderId));
            }

            return (ShoppingCartInfo)SessionHelper.GetValue(mSessionKey);
        }
        set
        {
            SessionHelper.SetValue(mSessionKey, value);
        }
    }


    protected override void OnPreInit(EventArgs e)
    {
        this.CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.Items"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.Items");
        }

        // Get order id
        orderId = QueryHelper.GetInteger("orderid", 0);

        if (!RequestHelper.IsPostBack())
        {
            if (!QueryHelper.GetBoolean("cartexist", false))
            {
                this.ShoppingCart = ShoppingCartInfoProvider.GetShoppingCartInfoFromOrder(orderId);
            }
        }

        if (orderId > 0)
        {
            if ((ShoppingCart != null) && (ShoppingCart.Order != null))
            {
                // Check order site ID
                CheckOrderSiteID(ShoppingCart.Order.OrderSiteID);
            }

            this.Cart.LocalShoppingCart = this.ShoppingCart;
            this.Cart.EnableProductPriceDetail = true;
            this.Cart.ShoppingCartInfoObj.IsCreatedFromOrder = true;
            this.Cart.CheckoutProcessType = CheckoutProcessEnum.CMSDeskOrderItems;
            this.Cart.OnPaymentCompleted += new EventHandler(Cart_OnPaymentCompleted);
            this.Cart.OnPaymentSkipped += new EventHandler(Cart_OnPaymentSkipped);
            this.Cart.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(Cart_OnCheckPermissions);
            this.Cart.RequiredFieldsMark = CMS.EcommerceProvider.ShoppingCart.DEFAULT_REQUIRED_FIELDS_MARK;
        }


        if (!RequestHelper.IsPostBack())
        {
            // Display 'Changes saved' message        
            if (QueryHelper.GetBoolean("saved", false))
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
            // Display 'Payment completed' message        
            else if (QueryHelper.GetBoolean("paymentcompleted", false))
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("PaymentGateway.CMSDeskOrderItems.PaymentCompleted");
            }
        }
    }

    void Cart_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission(permissionType))
        {
            RedirectToAccessDenied("CMS.Ecommerce", permissionType);
        }
    }


    void Cart_OnPaymentCompleted(object sender, EventArgs e)
    {
        this.Cart.CleanUpShoppingCart();
        URLHelper.Redirect("~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_Edit_OrderItems.aspx?orderID=" + this.ShoppingCart.OrderId.ToString() + "&paymentcompleted=1");
    }


    void Cart_OnPaymentSkipped(object sender, EventArgs e)
    {
        string saved = "";

        // Payment skipped because of no payment gateway was specified
        if (this.Cart.PaymentGatewayProvider != null)
        {
            // Do nothing
        }
        // Payment skipped from shopping cart step with payment - Button 'Skip payment' was pressed
        else
        {
            // Display 'Changes saved' message after page redirect
            saved = "&saved=1";
        }

        this.Cart.CleanUpShoppingCart();
        URLHelper.Redirect("~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_Edit_OrderItems.aspx?orderID=" + this.ShoppingCart.OrderId.ToString() + saved);
    }


    /// <summary>
    /// On prerender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        this.ShoppingCart = this.Cart.LocalShoppingCart;
        base.OnPreRender(e);
    }
}
