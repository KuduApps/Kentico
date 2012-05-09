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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_New : CMSOrdersPage
{

    private int customerId = 0;


    /// <summary>
    /// Shopping cart to use.
    /// </summary>
    private ShoppingCartInfo ShoppingCart
    {
        get
        {
            ShoppingCartInfo sci = SessionHelper.GetValue(this.SessionKey) as ShoppingCartInfo;
            if (sci == null)
            {
                sci = GetNewCart();
                SessionHelper.SetValue(this.SessionKey, sci);
            }

            return sci;
        }
        set
        {
            SessionHelper.SetValue(this.SessionKey, value);
        }
    }


    /// <summary>
    /// Shopping cart session key.
    /// </summary>
    private string SessionKey
    {
        get
        {
            if (customerId > 0)
            {
                return "CMSDeskNewCustomerOrderShoppingCart";
            }
            else
            {
                return "CMSDeskNewOrderShoppingCart";
            }
        }
    }


    protected ShoppingCartInfo GetNewCart()
    {
        ShoppingCartInfo newCart = ShoppingCartInfoProvider.CreateShoppingCartInfo(CMSContext.CurrentSite.SiteID);
        if (customerId > 0)
        {
            CustomerInfo ci = CustomerInfoProvider.GetCustomerInfo(customerId);
            if (ci != null)
            {
                UserInfo ui = null;
                if (ci.CustomerUserID > 0)
                {
                    ui = UserInfoProvider.GetUserInfo(ci.CustomerUserID);
                    newCart.UserInfoObj = ui;
                }
                //if (ui == null)
                //{
                //    ui = CMSContext.GlobalPublicUser;
                //}
                //newCart.UserInfoObj = ui;
                newCart.ShoppingCartCustomerID = customerId;
            }
        }

        return newCart;
    }


    protected override void OnPreInit(EventArgs e)
    {
        customerId = QueryHelper.GetInteger("customerid", 0);
        this.CustomerID = customerId;
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI element
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "NewOrder"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "NewOrder");
        }

        if (!RequestHelper.IsPostBack())
        {
            this.ShoppingCart = GetNewCart();
        }

        this.Cart.LocalShoppingCart = this.ShoppingCart;
        this.Cart.EnableProductPriceDetail = true;
        this.Cart.OnPaymentCompleted += new EventHandler(Cart_OnPaymentCompleted);
        this.Cart.OnPaymentSkipped += new EventHandler(Cart_OnPaymentSkipped);
        this.Cart.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(Cart_OnCheckPermissions);
        this.Cart.RequiredFieldsMark = CMS.EcommerceProvider.ShoppingCart.DEFAULT_REQUIRED_FIELDS_MARK;

        if (customerId > 0)
        {
            this.Cart.CheckoutProcessType = CheckoutProcessEnum.CMSDeskCustomer;
            AddMenuButtonSelectScript("Customers", "");
        }
        else
        {
            this.Cart.CheckoutProcessType = CheckoutProcessEnum.CMSDeskOrder;
            AddMenuButtonSelectScript("NewOrder", "");
        }
    }


    void Cart_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check ecommerce permission
        if (!ECommerceContext.IsUserAuthorizedForPermission(permissionType))
        {
            string message = permissionType;
            if (permissionType.ToLower().StartsWith("modify"))
            {
                message = "EcommerceModify OR " + message;
            }

            RedirectToAccessDenied("CMS.Ecommerce", message);
        }
    }


    void Cart_OnPaymentSkipped(object sender, EventArgs e)
    {
        URLHelper.Redirect("~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_Edit.aspx?orderID=" + this.ShoppingCart.OrderId.ToString() + "&customerid=" + customerId);
    }


    void Cart_OnPaymentCompleted(object sender, EventArgs e)
    {
        URLHelper.Redirect("~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_Edit.aspx?orderID=" + this.ShoppingCart.OrderId.ToString() + "&customerid=" + customerId);
    }


    protected void Page_Prerender()
    {
        int customerId = QueryHelper.GetInteger("customerId", 0);

        // For all steps
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Order_New.Orders");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_List.aspx";
        if (customerId != 0)
        {
            pageTitleTabs[0, 1] += "?customerId=" + customerId.ToString();
        }
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("Order_New.HeaderCaption");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "order_step_1";
        if (this.customerId <= 0)
        {
            this.CurrentMaster.Title.TitleText = GetString("Order_New.HeaderCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Order/new.png");
        }
    }
}
