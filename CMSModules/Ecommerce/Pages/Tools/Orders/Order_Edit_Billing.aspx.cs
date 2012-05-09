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
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Billing : CMSOrdersPage
{
    #region "Variables"

    protected int orderId = 0;
    protected int customerId = 0;

    private bool orderIsPaidChanged = false;
    private bool enabled = true;

    private bool showMembershipWarning = false;
    private bool showEproductWarning = false;

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        customerId = QueryHelper.GetInteger("customerid", 0);
        this.CustomerID = customerId;
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.Billing"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.Billing");
        }

        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AddressChange", ScriptHelper.GetScript("   function AddressChange(AddressId) { if (AddressId > 0) { document.getElementById('" + hdnAddress.ClientID + "').value = AddressId; " + ClientScript.GetPostBackEventReference(btnChange, "") + " } } "));

        this.orderId = QueryHelper.GetInteger("orderId", 0);

        lblCurrency.Text = GetString("OrderBilling.Currency");
        lblPayment.Text = GetString("OrderBilling.Payment");
        lblBillingAddress.Text = GetString("OrderBilling.Address");
        lblPaymentResult.Text = GetString("OrderBilling.lblPaymentResult");

        btnEdit.Text = GetString("general.edit");
        btnNew.Text = GetString("general.new");
        btnOk.Text = GetString("general.ok");

        addressElem.DropDownSingleSelect.AutoPostBack = true;
        addressElem.DropDownSingleSelect.SelectedIndexChanged += new EventHandler(DropDownSingleSelect_SelectedIndexChanged);

        LoadData();

        btnNew.OnClientClick = "AddAddress('" + customerId + "'); return false;";
        btnEdit.OnClientClick = "EditAddress('" + customerId + "','" + addressElem.AddressID + "'); return false;";

        // If one of the rollback warnings should be shown
        if (this.showEproductWarning || this.showMembershipWarning)
        {
            // Set standard warning message
            string paidUncheckWarning = this.GetString("order_edit_billing.orderispaiduncheckwarning");

            // Add memberships rollback warning message if required
            if (this.showMembershipWarning)
            {
                paidUncheckWarning += "\\n\\n- " + this.GetString("order_edit_billing.orderispaiduncheckwarningmemberships");
            }

            // Add e-products rollback warning message if required
            if (this.showEproductWarning)
            {
                paidUncheckWarning += "\\n\\n- " + this.GetString("order_edit_billing.orderispaiduncheckwarningeproducts");
            }

            // Register submit script
            string submitScript = String.Format("return CheckOrderIsPaid('{0}', '{1}');", this.chkOrderIsPaid.ClientID, paidUncheckWarning);
            ScriptHelper.RegisterOnSubmitStatement(this.Page, typeof(string), "SubmitScript", submitScript);
        }
    }


    protected void DropDownSingleSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnAddress.Value = addressElem.AddressID.ToString();
        // Enable edit address only if address selected
        btnEdit.Enabled = addressElem.AddressID != 0;
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Get order information
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(this.orderId);

        if (oi != null)
        {
            // Check order site ID
            CheckOrderSiteID(oi.OrderSiteID);

            // Register client script
            string script = String.Format("var originalOrderIsPaid = {0};", oi.OrderIsPaid.ToString().ToLower());
            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "StoreOriginalOrderIsPaidValue", ScriptHelper.GetScript(script));

            // If order is paid
            if (oi.OrderIsPaid)
            {
                this.enabled = false;
            }
            // If order is paid changed
            else if (this.orderIsPaidChanged)
            {
                this.enabled = true;
            }
        }

        // Set enabled to specific controls
        this.drpPayment.Enabled = this.enabled;
        this.drpCurrency.Enabled = this.enabled;
        this.addressElem.Enabled = this.enabled;
        this.btnNew.Enabled = this.enabled;

        // Enable edit address only if address selected
        this.btnEdit.Enabled = (this.enabled) ? (this.addressElem.AddressID != 0) : false;

        base.OnPreRender(e);
    }


    protected void LoadData()
    {
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(this.orderId);
        EditedObject = oi;

        if (oi != null)
        {
            customerId = oi.OrderCustomerID;

            string paymentResultValue = oi.OrderPaymentResult.GetFormattedPaymentResultString();

            if (paymentResultValue.Trim() == String.Empty)
            {
                paymentResultValue = GetString("general.na");
            }

            lblPaymentResultValue.Text = paymentResultValue;

            this.drpPayment.ShippingOptionID = oi.OrderShippingOptionID;
            this.drpPayment.AddNoneRecord = true;

            // If no shipping option selected
            if (this.drpPayment.ShippingOptionID == 0)
            {
                this.drpPayment.DisplayOnlyAllowedIfNoShipping = true;
            }

            addressElem.CustomerID = customerId;

            if (!URLHelper.IsPostback())
            {
                drpPayment.PaymentID = oi.OrderPaymentOptionID;
                drpCurrency.CurrencyID = oi.OrderCurrencyID;
                addressElem.AddressID = oi.OrderBillingAddressID;
                chkOrderIsPaid.Checked = oi.OrderIsPaid;
            }

            // Get order items for this order
            DataSet orderItems = OrderItemInfoProvider.GetOrderItems(this.orderId);

            foreach (DataRow orderItem in orderItems.Tables[0].Rows)
            {
                // Get order item
                OrderItemInfo oii = new OrderItemInfo(orderItem);

                if ((oii != null) && (oii.OrderItemSKU != null))
                {
                    switch (oii.OrderItemSKU.SKUProductType)
                    {
                        // If order item represents membership
                        case SKUProductTypeEnum.Membership:
                            this.showMembershipWarning = true;
                            break;

                        // If order item represents e-product
                        case SKUProductTypeEnum.EProduct:
                            this.showEproductWarning = true;
                            break;
                    }
                }
            }
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }

        // Check whether some address is selected
        int addressId = addressElem.AddressID;
        if (addressId <= 0)
        {
            lblError.Visible = true;
            lblError.Text = GetString("Order_Edit_Billing.NoAddress");
            return;
        }

        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);
        EditedObject = oi;

        if (oi != null)
        {
            // Check if paid status was changed
            this.orderIsPaidChanged = (oi.OrderIsPaid != this.chkOrderIsPaid.Checked);

            // Load the shopping cart to process the data
            ShoppingCartInfo sci = ShoppingCartInfoProvider.GetShoppingCartInfoFromOrder(orderId);
            if (sci != null)
            {
                // Set order new properties
                sci.ShoppingCartBillingAddressID = addressId;
                sci.ShoppingCartPaymentOptionID = drpPayment.PaymentID;
                sci.ShoppingCartCurrencyID = drpCurrency.CurrencyID;
                
                // Evaluate order data
                ShoppingCartInfoProvider.EvaluateShoppingCart(sci);

                // Update order data
                ShoppingCartInfoProvider.SetOrder(sci, false);
            }

            // Mark order as paid
            oi.OrderIsPaid = chkOrderIsPaid.Checked;
            OrderInfoProvider.SetOrderInfo(oi);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }
}
