using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Shipping : CMSOrdersPage
{
    protected int orderId = 0;
    protected int customerId = 0;
    protected ShoppingCartInfo mShoppingCartInfoObj = null;


    private ShoppingCartInfo ShoppingCartInfoObj
    {
        get
        {
            if (mShoppingCartInfoObj == null)
            {
                mShoppingCartInfoObj = ShoppingCartInfoProvider.GetShoppingCartInfoFromOrder(orderId);
            }

            return mShoppingCartInfoObj;
        }
    }


    protected override void OnPreInit(EventArgs e)
    {
        this.CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.Shipping"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.Shipping");
        }

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AddressChange", ScriptHelper.GetScript("   function AddressChange(AddressId) { if (AddressId > 0) { document.getElementById('" + hdnAddress.ClientID + "').value = AddressId; " + ClientScript.GetPostBackEventReference(btnChange, "") + " } } "));

        lblAddress.Text = GetString("Order_Edit_Shipping.lblAddress");
        lblOption.Text = GetString("Order_Edit_Shipping.lblOption");
        lblTrackingNumber.Text = GetString("order_edit.lblTrackingNumber");

        btnEdit.Text = GetString("general.edit");
        btnNew.Text = GetString("general.new");
        btnOk.Text = GetString("General.OK");

        btnOk.Click += new EventHandler(btnOk_Click);
        addressElem.DropDownSingleSelect.SelectedIndexChanged += new EventHandler(DropDownSingleSelect_SelectedIndexChanged);
        addressElem.DropDownSingleSelect.AutoPostBack = true;

        orderId = QueryHelper.GetInteger("orderId", 0);
        LoadData();

        btnNew.OnClientClick = "AddAddress('" + customerId + "'); return false;";
        btnEdit.OnClientClick = "EditAddress('" + customerId + "','" + addressElem.AddressID + "'); return false;";
    }


    protected void DropDownSingleSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnAddress.Value = addressElem.AddressID.ToString();
        // Enable edit address only if address selected
        btnEdit.Enabled = addressElem.AddressID != 0;
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }

        // Get order
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);
        if (oi != null)
        {
            // Get order site
            SiteInfo si = SiteInfoProvider.GetSiteInfo(oi.OrderSiteID);

            // If shipping address is required
            if (((this.ShoppingCartInfoObj != null) && (ShippingOptionInfoProvider.IsShippingNeeded(this.ShoppingCartInfoObj)))
                || ((si != null) && (ECommerceSettings.ApplyTaxesBasedOn(si.SiteName) == ApplyTaxBasedOnEnum.ShippingAddress)))
            {
                // If shipping address is not set
                if (this.addressElem.AddressID <= 0)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Order_Edit_Shipping.NoAddress");

                    return;
                }
            }

            try
            {
                // Load the shopping cart to process the data
                ShoppingCartInfo sci = ShoppingCartInfoProvider.GetShoppingCartInfoFromOrder(orderId);
                if (sci != null)
                {
                    // Set order new properties
                    sci.ShoppingCartShippingOptionID = drpShippingOption.ShippingID;
                    sci.ShoppingCartShippingAddressID = this.addressElem.AddressID;
                    
                    // Evaulate order data
                    ShoppingCartInfoProvider.EvaluateShoppingCart(sci);

                    // Update order data
                    ShoppingCartInfoProvider.SetOrder(sci, false);
                }

                // Update tracking number
                oi.OrderTrackingNumber = txtTrackingNumber.Text.Trim();
                OrderInfoProvider.SetOrderInfo(oi);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
    }


    private void LoadData()
    {
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);
        EditedObject = oi;

        if (oi != null)
        {
            // Check order site ID
            CheckOrderSiteID(oi.OrderSiteID);

            customerId = oi.OrderCustomerID;
            addressElem.CustomerID = customerId;
            drpShippingOption.ShoppingCart = this.ShoppingCartInfoObj;

            if (!RequestHelper.IsPostBack())
            {
                txtTrackingNumber.Text = oi.OrderTrackingNumber;
                addressElem.CustomerID = customerId;
                addressElem.AddressID = oi.OrderShippingAddressID;
                drpShippingOption.ShippingID = oi.OrderShippingOptionID;
            }
        }

        // Enable edit address only if address selected
        btnEdit.Enabled = addressElem.AddressID != 0;

        // If order is paid
        if ((oi != null) && (oi.OrderIsPaid))
        {
            // Disable shipping option edit
            this.drpShippingOption.Enabled = false;

            // Get site information
            SiteInfo si = SiteInfoProvider.GetSiteInfo(oi.OrderSiteID);

            // If tax is based on shipping address
            if ((si != null) && (ECommerceSettings.ApplyTaxesBasedOn(si.SiteName) == ApplyTaxBasedOnEnum.ShippingAddress))
            {
                // Disable shipping address edit
                this.addressElem.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnNew.Enabled = false;
            }
        }
    }
}
