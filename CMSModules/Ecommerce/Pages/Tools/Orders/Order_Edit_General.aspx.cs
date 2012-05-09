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
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_General : CMSOrdersPage
{
    protected int orderId = 0;
    protected int originalStatusId = 0;
    protected int originalCompanyAddressId = 0;
    protected int customerId = 0;


    protected override void OnPreInit(EventArgs e)
    {
        customerId = QueryHelper.GetInteger("customerid", 0);
        this.CustomerID = customerId;
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.General");
        }

        // Register the dialog script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshPageScript", ScriptHelper.GetScript("function RefreshPage() { window.location.replace(window.location.href); }"));
        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AddressChange", ScriptHelper.GetScript("   function AddressChange(AddressId) { if (AddressId > 0) { document.getElementById('" + hdnAddress.ClientID + "').value = AddressId; " + ClientScript.GetPostBackEventReference(btnChange, "") + " } } "));

        this.addressElem.DropDownSingleSelect.AutoPostBack = true;
        this.addressElem.DropDownSingleSelect.SelectedIndexChanged += new EventHandler(DropDownSingleSelect_SelectedIndexChanged);

        // controls initialization
        lblOrderId.Text = GetString("order_edit.orderidlabel");
        lblOrderDate.Text = GetString("order_edit.orderdatelabel");
        lblInvoiceNumber.Text = GetString("order_edit.invoicenumberlabel");
        lblStatus.Text = GetString("order_edit.orderstatuslabel");
        lblCustomer.Text = GetString("order_edit.customerlabel");
        lblNotes.Text = GetString("order_edit.ordernotelabel");
        btnEditCustomer.Text = GetString("general.edit");
        btnOk.Text = GetString("general.ok");
        lblCompanyAddress.Text = GetString("order_edit.lblCompanyAddress");
        btnNewAddress.Text = GetString("general.new");
        btnEditAddress.Text = GetString("general.edit");

        this.btnEditCustomer.Visible = ECommerceContext.IsUserAuthorizedForPermission("ReadCustomer");

        // get order ID from url
        orderId = QueryHelper.GetInteger("orderid", 0);
        // get order info from database and fill the form
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);

        if (oi != null)
        {
            // Check order site ID
            CheckOrderSiteID(oi.OrderSiteID);

            originalCompanyAddressId = oi.OrderCompanyAddressID;
            originalStatusId = oi.OrderStatusID;
            customerId = oi.OrderCustomerID;

            this.statusElem.SiteID = oi.OrderSiteID;
            this.addressElem.CustomerID = customerId;

            // Initialize javascript to button clicks
            btnEditCustomer.OnClientClick = "EditCustomer(" + customerId + "); return false;";
            btnNewAddress.OnClientClick = "AddAddress('" + customerId + "'); return false;";
            if (this.addressElem.AddressID > 0)
            {
                btnEditAddress.OnClientClick = "EditAddress('" + customerId + "','" + this.addressElem.AddressID + "'); return false;";
            }

            if (!RequestHelper.IsPostBack())
            {
                // initialize form
                InitializeForm(oi);

                // show that the Order was updated successfully
                if (QueryHelper.GetString("saved", "") == "1")
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
            }
        }

        // Enable edit address only if address selected
        btnEditAddress.Enabled = this.addressElem.AddressID != 0;

        // If order is paid
        if ((oi != null) && (oi.OrderIsPaid))
        {
            // Disable specific controls
            this.orderDate.Enabled = false;
            this.btnEditCustomer.Enabled = false;
            this.addressElem.Enabled = false;
            this.btnEditAddress.Enabled = false;
            this.btnNewAddress.Enabled = false;
        }
    }


    protected void DropDownSingleSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnAddress.Value = this.addressElem.AddressID.ToString();
        // Enable edit address only if address selected
        btnEditAddress.Enabled = this.addressElem.AddressID != 0;
    }


    /// <summary>
    /// Initialize the form with order values.
    /// </summary>
    protected void InitializeForm(OrderInfo orderInfo)
    {
        lblOrderIdValue.Text = Convert.ToString(orderInfo.OrderID);
        orderDate.SelectedDateTime = orderInfo.OrderDate;
        lblInvoiceNumberValue.Text = HTMLHelper.HTMLEncode(Convert.ToString(orderInfo.OrderInvoiceNumber));
        txtNotes.Text = orderInfo.OrderNote;

        CustomerInfo ci = CustomerInfoProvider.GetCustomerInfo(customerId);
        if (ci != null)
        {
            lblCustomerName.Text = HTMLHelper.HTMLEncode(ci.CustomerFirstName + " " + ci.CustomerLastName);
        }

        this.statusElem.OrderStatusID = originalStatusId;
        this.addressElem.AddressID = originalCompanyAddressId;
    }


    /// <summary>
    /// On btnOK button click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event argument</param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }

        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);
            if (oi != null)
            {
                oi.OrderDate = orderDate.SelectedDateTime;
                oi.OrderNote = txtNotes.Text;
                oi.OrderStatusID = this.statusElem.OrderStatusID;
                oi.OrderCompanyAddressID = this.addressElem.AddressID;

                // update orderinfo
                OrderInfoProvider.SetOrderInfo(oi);

                URLHelper.Redirect("Order_Edit_General.aspx?orderid=" + Convert.ToString(oi.OrderID) + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                errorMessage = GetString("order_edit.ordernotexist");
            }
        }

        // Show error message
        if (errorMessage != "")
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Validates form input fields.
    /// </summary>
    protected string ValidateForm()
    {
        // Validate order date for emptiness
        string errorMessage = (orderDate.SelectedDateTime.CompareTo(DataHelper.DATETIME_NOT_SELECTED) == 0) ? this.GetString("order_edit.dateerr") : "";

        if (errorMessage == "")
        {
            if (!orderDate.IsValidRange())
            {
                errorMessage = GetString("general.errorinvaliddatetimerange");
            }

            // Validate order date for wrong format
            if (ValidationHelper.GetDateTime(orderDate.SelectedDateTime, DataHelper.DATETIME_NOT_SELECTED) == DataHelper.DATETIME_NOT_SELECTED)
            {
                errorMessage = GetString("order_edit.datewrongformat");
            }
            if ((originalCompanyAddressId > 0) && (errorMessage == ""))
            {
                if (this.addressElem.AddressID == 0)
                {
                    errorMessage = GetString("order_edit.emptycompanyaddress");
                }
            }
        }

        return errorMessage;
    }
}
