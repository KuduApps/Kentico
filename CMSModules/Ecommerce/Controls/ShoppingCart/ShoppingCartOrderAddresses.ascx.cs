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
using CMS.EcommerceProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartOrderAddresses : ShoppingCartStep
{
    #region "ViewState Constants"

    private const string BILLING_ADDRESS_ID = "BillingAddressID";
    private const string BILLING_ADDRESS_NAME = "BillingAddressName";
    private const string BILLING_ADDRESS_LINE1 = "BillingAddressLine1";
    private const string BILLING_ADDRESS_LINE2 = "BillingAddressLine2";
    private const string BILLING_ADDRESS_CITY = "BillingAddressCity";
    private const string BILLING_ADDRESS_ZIP = "BillingAddressZIP";
    private const string BILLING_ADDRESS_COUNTRY_ID = "BillingAddressCountryID";
    private const string BILLING_ADDRESS_STATE_ID = "BillingAddressStateID";
    private const string BILLING_ADDRESS_PHONE = "BillingAddressPhone";

    private const string SHIPPING_ADDRESS_CHECKED = "ShippingAddressChecked";
    private const string SHIPPING_ADDRESS_ID = "ShippingAddressID";
    private const string SHIPPING_ADDRESS_NAME = "ShippingAddressName";
    private const string SHIPPING_ADDRESS_LINE1 = "ShippingAddressLine1";
    private const string SHIPPING_ADDRESS_LINE2 = "ShippingAddressLine2";
    private const string SHIPPING_ADDRESS_CITY = "ShippingAddressCity";
    private const string SHIPPING_ADDRESS_ZIP = "ShippingAddressZIP";
    private const string SHIPPING_ADDRESS_COUNTRY_ID = "ShippingAddressCountryID";
    private const string SHIPPING_ADDRESS_STATE_ID = "ShippingAddressStateID";
    private const string SHIPPING_ADDRESS_PHONE = "ShippingAddressPhone";

    private const string COMPANY_ADDRESS_CHECKED = "CompanyAddressChecked";
    private const string COMPANY_ADDRESS_ID = "CompanyAddressID";
    private const string COMPANY_ADDRESS_NAME = "CompanyAddressName";
    private const string COMPANY_ADDRESS_LINE1 = "CompanyAddressLine1";
    private const string COMPANY_ADDRESS_LINE2 = "CompanyAddressLine2";
    private const string COMPANY_ADDRESS_CITY = "CompanyAddressCity";
    private const string COMPANY_ADDRESS_ZIP = "CompanyAddressZIP";
    private const string COMPANY_ADDRESS_COUNTRY_ID = "CompanyAddressCountryID";
    private const string COMPANY_ADDRESS_STATE_ID = "CompanyAddressStateID";
    private const string COMPANY_ADDRESS_PHONE = "CompanyAddressPhone";

    #endregion


    #region "Temporary values operations"

    /// <summary>
    /// Removes billing address values from ShoppingCart ViewState.
    /// </summary>
    private void RemoveBillingTempValues()
    {
        // Billing address values
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_ID, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_CITY, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_COUNTRY_ID, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_LINE1, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_LINE2, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_NAME, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_PHONE, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_STATE_ID, null);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_ZIP, null);
    }


    /// <summary>
    /// Removes shipping address values from ShoppingCart ViewState.
    /// </summary>
    private void RemoveShippingTempValues()
    {
        // Shipping address values
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_CHECKED, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_ID, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_CITY, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_COUNTRY_ID, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_LINE1, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_LINE2, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_NAME, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_PHONE, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_STATE_ID, null);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_ZIP, null);
    }


    /// <summary>
    /// Removes company address values from ShoppingCart ViewState.
    /// </summary>
    private void RemoveCompanyTempValues()
    {
        // Company address values
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_CHECKED, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_ID, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_CITY, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_COUNTRY_ID, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_LINE1, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_LINE2, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_NAME, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_PHONE, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_STATE_ID, null);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_ZIP, null);
    }


    /// <summary>
    /// Loads billing address temp values.
    /// </summary>
    private void LoadBillingFromViewState()
    {
        this.txtBillingName.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_NAME));
        this.txtBillingAddr1.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_LINE1));
        this.txtBillingAddr2.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_LINE2));
        this.txtBillingCity.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_CITY));
        this.txtBillingZip.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_ZIP));
        this.txtBillingPhone.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_PHONE));
        this.CountrySelector1.CountryID = ValidationHelper.GetInteger(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_COUNTRY_ID), 0);
        this.CountrySelector1.StateID = ValidationHelper.GetInteger(this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_STATE_ID), 0);
    }


    /// <summary>
    /// Loads shipping address temp values.
    /// </summary>
    private void LoadShippingFromViewState()
    {
        this.txtShippingName.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_NAME));
        this.txtShippingAddr1.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_LINE1));
        this.txtShippingAddr2.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_LINE2));
        this.txtShippingCity.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_CITY));
        this.txtShippingZip.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_ZIP));
        this.txtShippingPhone.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_PHONE));
        this.CountrySelector2.CountryID = ValidationHelper.GetInteger(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_COUNTRY_ID), 0);
        this.CountrySelector2.StateID = ValidationHelper.GetInteger(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_STATE_ID), 0);
    }


    /// <summary>
    /// Loads company address temp values.
    /// </summary>
    private void LoadCompanyFromViewState()
    {
        this.txtCompanyName.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_NAME));
        this.txtCompanyLine1.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_LINE1));
        this.txtCompanyLine2.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_LINE2));
        this.txtCompanyCity.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_CITY));
        this.txtCompanyZip.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_ZIP));
        this.txtCompanyPhone.Text = Convert.ToString(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_PHONE));
        this.CountrySelector3.CountryID = ValidationHelper.GetInteger(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_COUNTRY_ID), 0);
        this.CountrySelector3.StateID = ValidationHelper.GetInteger(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_STATE_ID), 0);
    }


    /// <summary>
    /// Back button actions.
    /// </summary>
    public override void ButtonBackClickAction()
    {
        // Billing address values
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_ID, this.drpBillingAddr.SelectedValue);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_CITY, this.txtBillingCity.Text);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_COUNTRY_ID, this.CountrySelector1.CountryID);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_LINE1, this.txtBillingAddr1.Text);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_LINE2, this.txtBillingAddr2.Text);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_NAME, this.txtBillingName.Text);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_PHONE, this.txtBillingPhone.Text);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_STATE_ID, this.CountrySelector1.StateID);
        this.ShoppingCartControl.SetTempValue(BILLING_ADDRESS_ZIP, this.txtBillingZip.Text);

        // Shipping address values
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_CHECKED, this.chkShippingAddr.Checked);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_ID, this.drpShippingAddr.SelectedValue);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_CITY, this.txtShippingCity.Text);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_COUNTRY_ID, this.CountrySelector2.CountryID);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_LINE1, this.txtShippingAddr1.Text);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_LINE2, this.txtShippingAddr2.Text);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_NAME, this.txtShippingName.Text);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_PHONE, this.txtShippingPhone.Text);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_STATE_ID, this.CountrySelector2.StateID);
        this.ShoppingCartControl.SetTempValue(SHIPPING_ADDRESS_ZIP, this.txtShippingZip.Text);

        // Company address values
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_CHECKED, this.chkCompanyAddress.Checked);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_ID, this.drpCompanyAddress.SelectedValue);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_CITY, this.txtCompanyCity.Text);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_COUNTRY_ID, this.CountrySelector3.CountryID);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_LINE1, this.txtCompanyLine1.Text);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_LINE2, this.txtCompanyLine2.Text);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_NAME, this.txtCompanyName.Text);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_PHONE, this.txtCompanyPhone.Text);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_STATE_ID, this.CountrySelector3.StateID);
        this.ShoppingCartControl.SetTempValue(COMPANY_ADDRESS_ZIP, this.txtCompanyZip.Text);

        base.ButtonBackClickAction();
    }

    #endregion


    /// <summary>
    /// Private properties.
    /// </summary>
    private int mCustomerId = 0;
    private SiteInfo mCurrentSite = null;

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        mCurrentSite = CMSContext.CurrentSite;

        this.CountrySelector1.IsLiveSite = this.IsLiveSite;
        this.CountrySelector2.IsLiveSite = this.IsLiveSite;
        this.CountrySelector3.IsLiveSite = this.IsLiveSite;

        this.lblBillingTitle.Text = GetString("ShoppingCart.BillingAddress");
        this.lblShippingTitle.Text = GetString("ShoppingCart.ShippingAddress");
        this.lblCompanyAddressTitle.Text = GetString("ShoppingCart.CompanyAddress");

        drpBillingAddr.SelectedIndexChanged += new EventHandler(drpBillingAddr_SelectedIndexChanged);
        drpShippingAddr.SelectedIndexChanged += new EventHandler(drpShippingAddr_SelectedIndexChanged);
        drpCompanyAddress.SelectedIndexChanged += new EventHandler(drpCompanyAddress_SelectedIndexChanged);

        // Initialize labels.
        LabelInitialize();
        //this.TitleText = GetString("Order_new.ShoppingCartOrderAddresses.Title");

        // Get customer ID from ShoppingCartInfoObj
        mCustomerId = this.ShoppingCartInfoObj.ShoppingCartCustomerID;

        // Display/ Hide company address panel with checkbox to display company address detail        
        plcCompanyAll.Visible = ((mCurrentSite != null) && (ECommerceSettings.UseExtraCompanyAddress(mCurrentSite.SiteName)));

        // Get customer info.
        CustomerInfo ci = CustomerInfoProvider.GetCustomerInfo(mCustomerId);

        if (ci != null)
        {
            // Display customer addresses if customer is not anonymous
            if (ci.CustomerID > 0)
            {
                plhBillAddr.Visible = true;
                plhShippAddr.Visible = true;
                plcCompanyAddress.Visible = true;
                if (!this.ShoppingCartControl.IsCurrentStepPostBack)
                {
                    // Initialize customer billing and shipping addresses
                    InitializeAddresses();
                }
            }
        }

        // If shopping cart does not need shipping
        if (!ShippingOptionInfoProvider.IsShippingNeeded(this.ShoppingCartInfoObj))
        {
            // Hide title
            this.lblBillingTitle.Visible = false;

            // Hide shipping address section
            this.plcShippingAddress.Visible = false;

            // Change current checkout process step caption
            this.ShoppingCartControl.CheckoutProcessSteps[this.ShoppingCartControl.CurrentStepIndex].Caption = this.GetString("order_new.shoppingcartorderaddresses.titlenoshipping");
        }
    }


    /// <summary>
    /// Initialize page labels.
    /// </summary>
    protected void LabelInitialize()
    {
        lblBillingAddr.Text = GetString("ShoppingCartOrderAddresses.BillingAddr");
        lblBillingName.Text = GetString("ShoppingCartOrderAddresses.BillingName");
        lblBillingAddrLine.Text = GetString("ShoppingCartOrderAddresses.BillingAddrLine");
        lblBillingAddrLine2.Text = lblBillingAddrLine.Text;
        lblBillingAddrLine2.Style["display"] = "none";
        lblBillingCity.Text = GetString("ShoppingCartOrderAddresses.BillingCity");
        lblBillingZip.Text = GetString("ShoppingCartOrderAddresses.BillingZIP");
        lblBillingCountry.Text = GetString("ShoppingCartOrderAddresses.BillingCountry");
        lblBillingPhone.Text = GetString("ShoppingCartOrderAddresses.BillingPhone");
        chkShippingAddr.Text = GetString("ShoppingCartOrderAddresses.DifferentShippingAddr");
        lblShippingAddr.Text = GetString("ShoppingCartOrderAddresses.ShippingAddr");

        lblShippingName.Text = lblBillingName.Text;
        lblShippingAddrLine.Text = lblBillingAddrLine.Text;
        lblShippingAddrLine2.Text = lblShippingAddrLine.Text;
        lblShippingAddrLine2.Style["display"] = "none";
        lblShippingCity.Text = lblBillingCity.Text;
        lblShippingZip.Text = lblBillingZip.Text;
        lblShippingCountry.Text = lblBillingCountry.Text;
        lblShippingPhone.Text = lblBillingPhone.Text;

        lblCompanyName.Text = lblBillingName.Text;
        lblCompanyLines.Text = lblBillingAddrLine.Text;
        lblCompanyLines2.Text = lblBillingAddrLine.Text;
        lblCompanyLines2.Style["display"] = "none";
        lblCompanyCity.Text = lblBillingCity.Text;
        lblCompanyZip.Text = lblBillingZip.Text;
        lblCompanyCountry.Text = lblBillingCountry.Text;
        lblCompanyPhone.Text = lblBillingPhone.Text;

        lblCompanyAddress.Text = GetString("ShoppingCartOrderAddresses.lblCompanyAddress");
        chkCompanyAddress.Text = GetString("ShoppingCartOrderAddresses.chkCompanyAddress");

        lblError.Text = "";
        lblError.Visible = false;

        // Mark required field
        if (this.ShoppingCartControl.RequiredFieldsMark != "")
        {
            string mark = this.ShoppingCartControl.RequiredFieldsMark;
            lblMark1.Text = mark;
            lblMark2.Text = mark;
            lblMark3.Text = mark;
            lblMark4.Text = mark;
            lblMark5.Text = mark;
            lblMark6.Text = mark;
            lblMark7.Text = mark;
            lblMark8.Text = mark;
            lblMark9.Text = mark;
            lblMark10.Text = mark;
            lblMark11.Text = mark;
            lblMark12.Text = mark;
        }
    }


    /// <summary>
    /// Initialize customer's addresses in billing and shipping dropdown lists.
    /// </summary>
    protected void InitializeAddresses()
    {
        // add new item <(new), 0>
        ListItem li = new ListItem(GetString("ShoppingCartOrderAddresses.NewAddress"), "0");


        // get all billing addresses of the customer
        if (this.drpBillingAddr.Items.Count == 0)
        {
            DataSet ds = AddressInfoProvider.GetAddresses(mCustomerId, true, false, false, true);
            drpBillingAddr.DataSource = ds;
            drpBillingAddr.DataBind();
            drpBillingAddr.Items.Insert(0, li);
        }

        li = new ListItem(GetString("ShoppingCartOrderAddresses.NewAddress"), "0");

        // get all shipping addresses of the customer
        if (this.drpShippingAddr.Items.Count == 0)
        {
            DataSet ds = AddressInfoProvider.GetAddresses(mCustomerId, false, true, false, true);
            drpShippingAddr.DataSource = ds;
            drpShippingAddr.DataBind();
            drpShippingAddr.Items.Insert(0, li);
        }

        li = new ListItem(GetString("ShoppingCartOrderAddresses.NewAddress"), "0");

        // get all shipping addresses of the customer
        if (this.drpCompanyAddress.Items.Count == 0)
        {
            DataSet ds = AddressInfoProvider.GetAddresses(mCustomerId, false, false, true, true);
            drpCompanyAddress.DataSource = ds;
            drpCompanyAddress.DataBind();
            drpCompanyAddress.Items.Insert(0, li);
        }

        LoadBillingSelectedValue();

        // Try remove same shipping address as selected billing address
        // (if the selected value is not "0")
        if (drpBillingAddr.SelectedValue != "0")
        {
            try
            {
                drpShippingAddr.Items.Remove(drpBillingAddr.SelectedItem);
            }
            catch
            {
            }
        }
        LoadShippingSelectedValue();


        // Try remove same company address as selected billing address
        // (if the selected value is not "0")
        if (drpBillingAddr.SelectedValue != "0")
        {
            try
            {
                drpCompanyAddress.Items.Remove(drpBillingAddr.SelectedItem);
            }
            catch
            {
            }
        }
        LoadCompanySelectedValue();

        LoadBillingAddressInfo();
        LoadShippingAddressInfo();
        LoadCompanyAddressInfo();
    }


    protected void LoadBillingSelectedValue()
    {
        try
        {
            drpBillingAddr.ClearSelection();
            int lastBillingAddressId = 0;

            // Get last used shipping and billing addresses in the order
            DataSet ds = OrderInfoProvider.GetOrders("OrderCustomerID=" + mCustomerId, "OrderDate DESC");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                OrderInfo oi = new OrderInfo(ds.Tables[0].Rows[0]);
                lastBillingAddressId = oi.OrderBillingAddressID;
            }

            // Set billing address
            // Try to select billing address from ViewState first
            object viewStateValue = this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_ID);
            if (viewStateValue != null)
            {
                try
                {
                    drpBillingAddr.SelectedValue = Convert.ToString(viewStateValue);
                }
                catch
                {
                }
            }
            // If there is already selected billing address, set it
            else if (ShoppingCartInfoObj.ShoppingCartBillingAddressID > 0)
            {
                try
                {
                    drpBillingAddr.SelectedValue = ShoppingCartInfoObj.ShoppingCartBillingAddressID.ToString();
                }
                catch
                {
                }
            }
            // If there is last used billing address, set it
            else if (lastBillingAddressId != 0)
            {
                try
                {
                    drpBillingAddr.SelectedValue = lastBillingAddressId.ToString();
                }
                catch
                {
                }
            }
            else if (drpBillingAddr.Items.Count > 1)
            {
                drpBillingAddr.SelectedIndex = 1;
            }
        }
        catch
        {
        }
    }


    protected void LoadShippingSelectedValue()
    {
        try
        {
            drpShippingAddr.ClearSelection();
            int lastShippingAddressId = 0;

            // Get last used shipping and billing addresses in the order
            DataSet ds = OrderInfoProvider.GetOrders("OrderCustomerID=" + mCustomerId, "OrderDate DESC");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                OrderInfo oi = new OrderInfo(ds.Tables[0].Rows[0]);
                lastShippingAddressId = oi.OrderShippingAddressID;
            }

            // Try to select shipping address from ViewState first
            object viewStateValue = this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_ID);
            bool viewStateChecked = ValidationHelper.GetBoolean(this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_CHECKED), false);
            if (viewStateValue != null)
            {
                try
                {
                    drpShippingAddr.SelectedValue = Convert.ToString(viewStateValue);
                    plhShipping.Visible = viewStateChecked;
                    chkShippingAddr.Checked = viewStateChecked;
                    return;
                }
                catch
                {
                }
            }
            else if ((ShoppingCartInfoObj.ShoppingCartShippingAddressID > 0) && (ShoppingCartInfoObj.ShoppingCartShippingAddressID != ShoppingCartInfoObj.ShoppingCartBillingAddressID))
            {
                try
                {
                    drpShippingAddr.SelectedValue = ShoppingCartInfoObj.ShoppingCartShippingAddressID.ToString();
                    // Display Shipping part of the form and check checkbox
                    plhShipping.Visible = true;
                    chkShippingAddr.Checked = true;
                    return;
                }
                catch
                {
                }
            }
            // Select some address only if ShippingAddressID is diferent from BillingAddressID
            else if (lastShippingAddressId != 0)
            {
                try
                {
                    drpShippingAddr.SelectedValue = lastShippingAddressId.ToString();
                    if (drpShippingAddr.SelectedIndex != 0)
                    {
                        return;
                    }
                }
                catch
                {
                }
            }

            if (drpShippingAddr.Items.Count > 1)
            {
                drpShippingAddr.SelectedIndex = 1;
            }
        }
        catch
        {
        }
    }


    protected void LoadCompanySelectedValue()
    {
        try
        {
            drpCompanyAddress.ClearSelection();
            int lastCompanyAddressId = 0;

            // Get last used shipping and billing addresses in the order
            DataSet ds = OrderInfoProvider.GetOrders("OrderCustomerID=" + mCustomerId, "OrderDate DESC");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                OrderInfo oi = new OrderInfo(ds.Tables[0].Rows[0]);
                lastCompanyAddressId = oi.OrderCompanyAddressID;
            }

            // Try to select company address from ViewState first
            object viewStateValue = this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_ID);
            bool viewStateChecked = ValidationHelper.GetBoolean(this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_CHECKED), false);
            if ((viewStateValue != null))
            {
                try
                {
                    drpCompanyAddress.SelectedValue = Convert.ToString(viewStateValue);
                    plcCompanyDetail.Visible = viewStateChecked;
                    chkCompanyAddress.Checked = viewStateChecked;
                    return;
                }
                catch
                {
                }
            }
            else if ((ShoppingCartInfoObj.ShoppingCartCompanyAddressID > 0) && (ShoppingCartInfoObj.ShoppingCartCompanyAddressID != ShoppingCartInfoObj.ShoppingCartBillingAddressID))
            {
                try
                {
                    drpCompanyAddress.SelectedValue = ShoppingCartInfoObj.ShoppingCartCompanyAddressID.ToString();
                    // Display company address part of the form and check checkbox
                    plcCompanyDetail.Visible = true;
                    chkCompanyAddress.Checked = true;
                    return;
                }
                catch
                {
                }
            }
            // Select some address only if CompanyAddressID is diferent from BillingAddressID
            else if (lastCompanyAddressId != 0)
            {
                try
                {
                    drpCompanyAddress.SelectedValue = lastCompanyAddressId.ToString();
                    if (drpCompanyAddress.SelectedIndex != 0)
                    {
                        return;
                    }
                }
                catch
                {
                }
            }

            if (drpCompanyAddress.Items.Count > 1)
            {
                drpCompanyAddress.SelectedIndex = 1;
            }
        }
        catch
        {
        }
    }


    /// <summary>
    /// Change visibility of shipping address.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Parameter</param>
    protected void chkShippingAddr_CheckedChanged(object sender, EventArgs e)
    {
        if (plhShipping.Visible)
        {
            plhShipping.Visible = false;
        }
        else
        {
            plhShipping.Visible = true;
            LoadShippingAddressInfo();
        }
    }


    /// <summary>
    /// Change visibility of company address.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Parameter</param>
    protected void chkCompanyAddress_CheckedChanged(object sender, EventArgs e)
    {
        if (plcCompanyDetail.Visible)
        {
            plcCompanyDetail.Visible = false;
        }
        else
        {
            plcCompanyDetail.Visible = true;
            LoadCompanyAddressInfo();
        }
    }


    /// <summary>
    /// On drpBillingAddr selected index changed.
    /// </summary>
    private void drpBillingAddr_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateShippingAddresses();
        UpdateCompanyAddresses();

        LoadBillingAddressInfo();
    }


    /// <summary>
    /// On drpShippingAddr selected index changed.
    /// </summary>
    private void drpShippingAddr_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadShippingAddressInfo();
    }


    /// <summary>
    /// On drpCompanyAddress selected index changed.
    /// </summary>
    void drpCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCompanyAddressInfo();
    }


    /// <summary>
    /// Clean specified part of the form.
    /// </summary>    
    private void CleanForm(bool billing, bool shipping, bool company)
    {
        int defaultCountryId = 0;
        int defaultStateId = 0;

        // Prefill country from customer if any
        if ((ShoppingCartInfoObj != null) && (ShoppingCartInfoObj.CustomerInfoObj != null))
        {
            defaultCountryId = ShoppingCartInfoObj.CustomerInfoObj.CustomerCountryID;
            defaultStateId = ShoppingCartInfoObj.CustomerInfoObj.CustomerStateID;
        }

        // Prefill default store country if customers country not found
        if ((defaultCountryId <= 0) && (CMSContext.CurrentSite != null))
        {
            string countryName = ECommerceSettings.DefaultCountryName(CMSContext.CurrentSite.SiteName);
            CountryInfo ci = CountryInfoProvider.GetCountryInfo(countryName);
            defaultCountryId = (ci != null) ? ci.CountryID : 0;
            defaultStateId = 0;
        }

        if (shipping)
        {
            txtShippingName.Text = "";
            txtShippingAddr1.Text = "";
            txtShippingAddr2.Text = "";
            txtShippingCity.Text = "";
            txtShippingZip.Text = "";
            txtShippingPhone.Text = "";

            // Pre-load default country
            CountrySelector2.CountryID = defaultCountryId;
            CountrySelector2.StateID = defaultStateId;
            CountrySelector2.ReloadData(true);
        }
        if (billing)
        {
            txtBillingName.Text = "";
            txtBillingAddr1.Text = "";
            txtBillingAddr2.Text = "";
            txtBillingCity.Text = "";
            txtBillingZip.Text = "";
            txtBillingPhone.Text = "";

            // Pre-load default country
            CountrySelector1.CountryID = defaultCountryId;
            CountrySelector1.StateID = defaultStateId;
            CountrySelector1.ReloadData(true);
        }
        if (company)
        {
            txtCompanyName.Text = "";
            txtCompanyLine1.Text = "";
            txtCompanyLine2.Text = "";
            txtCompanyCity.Text = "";
            txtCompanyZip.Text = "";
            txtCompanyPhone.Text = "";

            // Pre-load default country
            CountrySelector3.CountryID = defaultCountryId;
            CountrySelector3.StateID = defaultStateId;
            CountrySelector3.ReloadData(true);
        }
    }


    /// <summary>
    /// Loads selected billing  address info.
    /// </summary>
    protected void LoadBillingAddressInfo()
    {
        // Try to select company address from ViewState first
        if (!this.ShoppingCartControl.IsCurrentStepPostBack && this.ShoppingCartControl.GetTempValue(BILLING_ADDRESS_ID) != null)
        {
            LoadBillingFromViewState();
        }
        else
        {
            int addressId = 0;

            if (drpBillingAddr.SelectedValue != "0")
            {
                addressId = Convert.ToInt32(drpBillingAddr.SelectedValue);

                AddressInfo ai = AddressInfoProvider.GetAddressInfo(addressId);
                if (ai != null)
                {
                    txtBillingName.Text = ai.AddressPersonalName;
                    txtBillingAddr1.Text = ai.AddressLine1;
                    txtBillingAddr2.Text = ai.AddressLine2;
                    txtBillingCity.Text = ai.AddressCity;
                    txtBillingZip.Text = ai.AddressZip;
                    txtBillingPhone.Text = ai.AddressPhone;
                    CountrySelector1.CountryID = ai.AddressCountryID;
                    CountrySelector1.StateID = ai.AddressStateID;
                    CountrySelector1.ReloadData(true);
                }
            }
            else
            {
                // Clean billing part of the form
                CleanForm(true, false, false);

                // Prefill customer company name or full name
                if ((this.ShoppingCartInfoObj.CustomerInfoObj != null) &&
                    (this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany != ""))
                {
                    txtBillingName.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany;
                }
                else
                {
                    txtBillingName.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerFirstName + " " + this.ShoppingCartInfoObj.CustomerInfoObj.CustomerLastName;
                }
            }
        }
    }


    /// <summary>
    /// Loads selected shipping  address info.
    /// </summary>
    protected void LoadShippingAddressInfo()
    {
        int addressId = 0;

        // Load shipping info only if shipping part is visible
        if (plhShipping.Visible)
        {
            // Try to select company address from ViewState first
            if (!this.ShoppingCartControl.IsCurrentStepPostBack && this.ShoppingCartControl.GetTempValue(SHIPPING_ADDRESS_ID) != null)
            {
                LoadShippingFromViewState();
            }
            else
            {
                if (drpShippingAddr.SelectedValue != "0")
                {
                    addressId = Convert.ToInt32(drpShippingAddr.SelectedValue);

                    AddressInfo ai = AddressInfoProvider.GetAddressInfo(addressId);
                    if (ai != null)
                    {
                        txtShippingName.Text = ai.AddressPersonalName;
                        txtShippingAddr1.Text = ai.AddressLine1;
                        txtShippingAddr2.Text = ai.AddressLine2;
                        txtShippingCity.Text = ai.AddressCity;
                        txtShippingZip.Text = ai.AddressZip;
                        txtShippingPhone.Text = ai.AddressPhone;
                        CountrySelector2.CountryID = ai.AddressCountryID;
                        CountrySelector2.StateID = ai.AddressStateID;
                        CountrySelector2.ReloadData(true);
                    }
                }
                else
                {
                    // clean shipping part of the form
                    CleanForm(false, true, false);

                    // Prefill customer company name or full name
                    if ((this.ShoppingCartInfoObj.CustomerInfoObj != null) &&
                        (this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany != ""))
                    {
                        txtShippingName.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany;
                    }
                    else
                    {
                        txtShippingName.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerFirstName + " " + this.ShoppingCartInfoObj.CustomerInfoObj.CustomerLastName;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Loads selected company address info.
    /// </summary>
    protected void LoadCompanyAddressInfo()
    {
        int addressId = 0;

        // Load company address info only if company part is visible
        if (plcCompanyDetail.Visible)
        {
            // Try to select company address from ViewState first
            if (!this.ShoppingCartControl.IsCurrentStepPostBack && this.ShoppingCartControl.GetTempValue(COMPANY_ADDRESS_ID) != null)
            {
                LoadCompanyFromViewState();
            }
            else
            {
                if (drpCompanyAddress.SelectedValue != "0")
                {
                    addressId = Convert.ToInt32(drpCompanyAddress.SelectedValue);

                    AddressInfo ai = AddressInfoProvider.GetAddressInfo(addressId);
                    if (ai != null)
                    {
                        txtCompanyName.Text = ai.AddressPersonalName;
                        txtCompanyLine1.Text = ai.AddressLine1;
                        txtCompanyLine2.Text = ai.AddressLine2;
                        txtCompanyCity.Text = ai.AddressCity;
                        txtCompanyZip.Text = ai.AddressZip;
                        txtCompanyPhone.Text = ai.AddressPhone;
                        CountrySelector3.CountryID = ai.AddressCountryID;
                        CountrySelector3.StateID = ai.AddressStateID;
                        CountrySelector3.ReloadData(true);
                    }
                }
                else
                {
                    // clean shipping part of the form
                    CleanForm(false, false, true);

                    // Prefill customer company name or full name
                    if ((this.ShoppingCartInfoObj.CustomerInfoObj != null) &&
                        (this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany != ""))
                    {
                        txtCompanyName.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerCompany;
                    }
                    else
                    {
                        txtCompanyName.Text = this.ShoppingCartInfoObj.CustomerInfoObj.CustomerFirstName + " " + this.ShoppingCartInfoObj.CustomerInfoObj.CustomerLastName;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Check if the form is well filled.
    /// </summary>
    /// <returns>True or false.</returns>
    public override bool IsValid()
    {
        Validator val = new Validator();
        // check billing part of the form
        string result = val.NotEmpty(txtBillingName.Text.Trim(), GetString("ShoppingCartOrderAddresses.BillingNameErr"))
            .NotEmpty(txtBillingAddr1.Text.Trim(), GetString("ShoppingCartOrderAddresses.BillingAddressLineErr"))
            .NotEmpty(txtBillingCity.Text.Trim(), GetString("ShoppingCartOrderAddresses.BillingCityErr"))
            .NotEmpty(txtBillingZip.Text.Trim(), GetString("ShoppingCartOrderAddresses.BillingZIPErr")).Result;

        if (result == "")
        {
            // Check shipping address
            if (chkShippingAddr.Checked)
            {
                // check shipping part of the form if checkbox is checked
                result = val.NotEmpty(txtShippingName.Text.Trim(), GetString("ShoppingCartOrderAddresses.ShippingNameErr"))
                    .NotEmpty(txtShippingAddr1.Text.Trim(), GetString("ShoppingCartOrderAddresses.ShippingAddressLineErr"))
                    .NotEmpty(txtShippingCity.Text.Trim(), GetString("ShoppingCartOrderAddresses.ShippingCityErr"))
                    .NotEmpty(txtShippingZip.Text.Trim(), GetString("ShoppingCartOrderAddresses.ShippingZIPErr")).Result;
            }
            // Check company address
            if ((result == "") && (chkCompanyAddress.Checked))
            {
                // check company part of the form if checkbox is checked
                result = val.NotEmpty(txtCompanyName.Text.Trim(), GetString("ShoppingCartOrderAddresses.CompanyNameErr"))
                    .NotEmpty(txtCompanyLine1.Text.Trim(), GetString("ShoppingCartOrderAddresses.CompanyAddressLineErr"))
                    .NotEmpty(txtCompanyCity.Text.Trim(), GetString("ShoppingCartOrderAddresses.CompanyCityErr"))
                    .NotEmpty(txtCompanyZip.Text.Trim(), GetString("ShoppingCartOrderAddresses.CompanyZIPErr")).Result;
            }
        }

        if (result != "")
        {
            // display error
            lblError.Visible = true;
            lblError.Text = result;

            return false;
        }

        return true;
    }


    /// <summary>
    /// Process valid values of this step.
    /// </summary>
    public override bool ProcessStep()
    {
        AddressInfo ai = null;
        bool newAddress = false;
        if (mCustomerId > 0)
        {
            // Clean the viewstate
            RemoveBillingTempValues();
            RemoveShippingTempValues();
            RemoveCompanyTempValues();

            // Check country presence
            if (CountrySelector1.CountryID <= 0)
            {
                lblError.Visible = true;
                lblError.Text = GetString("countryselector.selectedcountryerr");
                return false;
            }

            // Process billing address
            //------------------------
            ai = AddressInfoProvider.GetAddressInfo(ValidationHelper.GetInteger(drpBillingAddr.SelectedValue, 0));
            if (ai == null)
            {
                ai = new AddressInfo();
                newAddress = true;
            }

            ai.AddressPersonalName = txtBillingName.Text.Trim();
            ai.AddressLine1 = txtBillingAddr1.Text.Trim();
            ai.AddressLine2 = txtBillingAddr2.Text.Trim();
            ai.AddressCity = txtBillingCity.Text.Trim();
            ai.AddressZip = txtBillingZip.Text.Trim();
            ai.AddressCountryID = CountrySelector1.CountryID;
            ai.AddressStateID = CountrySelector1.StateID;
            ai.AddressPhone = txtBillingPhone.Text.Trim();
            if (newAddress)
            {
                ai.AddressIsBilling = true;
                ai.AddressIsShipping = !chkShippingAddr.Checked;
                ai.AddressIsCompany = !chkCompanyAddress.Checked;
                ai.AddressEnabled = true;
            }
            ai.AddressCustomerID = mCustomerId;
            ai.AddressName = AddressInfoProvider.GetAddressName(ai);

            // Save address and set it's ID to ShoppingCartInfoObj
            AddressInfoProvider.SetAddressInfo(ai);

            // Update current contact's address
            ModuleCommands.OnlineMarketingMapAddress(ai, this.ContactID);

            this.ShoppingCartInfoObj.ShoppingCartBillingAddressID = ai.AddressID;

            // If shopping cart does not need shipping
            if (!ShippingOptionInfoProvider.IsShippingNeeded(this.ShoppingCartInfoObj))
            {
                this.ShoppingCartInfoObj.ShoppingCartShippingAddressID = 0;
            }
            // If shipping address is different from billing address
            else if (chkShippingAddr.Checked)
            {
                // Check country presence
                if (CountrySelector2.CountryID <= 0)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("countryselector.selectedcountryerr");
                    return false;
                }

                newAddress = false;
                // Process shipping address
                //-------------------------
                ai = AddressInfoProvider.GetAddressInfo(Convert.ToInt32(drpShippingAddr.SelectedValue));
                if (ai == null)
                {
                    ai = new AddressInfo();
                    newAddress = true;
                }

                ai.AddressPersonalName = txtShippingName.Text.Trim();
                ai.AddressLine1 = txtShippingAddr1.Text.Trim();
                ai.AddressLine2 = txtShippingAddr2.Text.Trim();
                ai.AddressCity = txtShippingCity.Text.Trim();
                ai.AddressZip = txtShippingZip.Text.Trim();
                ai.AddressCountryID = CountrySelector2.CountryID;
                ai.AddressStateID = CountrySelector2.StateID;
                ai.AddressPhone = txtShippingPhone.Text.Trim();
                if (newAddress)
                {
                    ai.AddressIsShipping = true;
                    ai.AddressEnabled = true;
                    ai.AddressIsBilling = false;
                    ai.AddressIsCompany = false;
                    ai.AddressEnabled = true;
                }
                ai.AddressCustomerID = mCustomerId;
                ai.AddressName = AddressInfoProvider.GetAddressName(ai);

                // Save address and set it's ID to ShoppingCartInfoObj
                AddressInfoProvider.SetAddressInfo(ai);
                this.ShoppingCartInfoObj.ShoppingCartShippingAddressID = ai.AddressID;
            }
            // Shipping address is the same as billing address
            else
            {
                ShoppingCartInfoObj.ShoppingCartShippingAddressID = ShoppingCartInfoObj.ShoppingCartBillingAddressID;
            }

            if (chkCompanyAddress.Checked)
            {
                // Check country presence
                if (CountrySelector3.CountryID <= 0)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("countryselector.selectedcountryerr");
                    return false;
                }

                newAddress = false;
                // Process company address
                //-------------------------
                ai = AddressInfoProvider.GetAddressInfo(Convert.ToInt32(drpCompanyAddress.SelectedValue));
                if (ai == null)
                {
                    ai = new AddressInfo();
                    newAddress = true;
                }

                ai.AddressPersonalName = txtCompanyName.Text.Trim();
                ai.AddressLine1 = txtCompanyLine1.Text.Trim();
                ai.AddressLine2 = txtCompanyLine2.Text.Trim();
                ai.AddressCity = txtCompanyCity.Text.Trim();
                ai.AddressZip = txtCompanyZip.Text.Trim();
                ai.AddressCountryID = CountrySelector3.CountryID;
                ai.AddressStateID = CountrySelector3.StateID;
                ai.AddressPhone = txtCompanyPhone.Text.Trim();
                if (newAddress)
                {
                    ai.AddressIsCompany = true;
                    ai.AddressIsBilling = false;
                    ai.AddressIsShipping = false;
                    ai.AddressEnabled = true;
                }
                ai.AddressCustomerID = mCustomerId;
                ai.AddressName = AddressInfoProvider.GetAddressName(ai);

                // Save address and set it's ID to ShoppingCartInfoObj
                AddressInfoProvider.SetAddressInfo(ai);
                this.ShoppingCartInfoObj.ShoppingCartCompanyAddressID = ai.AddressID;
            }
            // Company address is the same as billing address
            else
            {
                // Save information about company address or not (according to the site settings)
                if (ECommerceSettings.UseExtraCompanyAddress(mCurrentSite.SiteName))
                {
                    ShoppingCartInfoObj.ShoppingCartCompanyAddressID = ShoppingCartInfoObj.ShoppingCartBillingAddressID;
                }
                else
                {
                    ShoppingCartInfoObj.ShoppingCartCompanyAddressID = 0;
                }
            }

            try
            {
                // Update changes in database only when on the live site
                if (!this.ShoppingCartControl.IsInternalOrder)
                {
                    ShoppingCartInfoProvider.SetShoppingCartInfo(this.ShoppingCartInfoObj);
                }
                return true;
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
                return false;
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = GetString("Ecommerce.NoCustomerSelected");
            return false;
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        if (!this.ShoppingCartControl.IsCurrentStepPostBack)
        {
            LoadShippingSelectedValue();
            LoadBillingSelectedValue();
            LoadCompanySelectedValue();
        }
        base.Render(writer);
    }


    private void UpdateShippingAddresses()
    {
        string oldSelectedValue = drpShippingAddr.SelectedValue;

        drpShippingAddr.ClearSelection();
        drpShippingAddr.SelectedIndex = -1;

        // add new item <(new), 0>
        ListItem li = new ListItem(GetString("ShoppingCartOrderAddresses.NewAddress"), "0");

        DataSet ds = AddressInfoProvider.GetAddresses(mCustomerId, false, true, false, true);
        drpShippingAddr.DataSource = ds;
        drpShippingAddr.DataBind();
        drpShippingAddr.Items.Insert(0, li);
        // Try to remove selected biling address
        try
        {
            // Do not remove (new) item
            if (drpBillingAddr.SelectedIndex != 0)
            {
                drpShippingAddr.Items.Remove(drpBillingAddr.SelectedItem);
            }

            drpShippingAddr.SelectedValue = oldSelectedValue;
        }
        catch
        {
            LoadShippingSelectedValue();
            LoadShippingAddressInfo();
        }

        if (drpShippingAddr.SelectedIndex != 0)
        {
            LoadShippingSelectedValue();
            LoadShippingAddressInfo();
        }
    }


    private void UpdateCompanyAddresses()
    {
        string oldSelectedValue = drpCompanyAddress.SelectedValue;

        drpCompanyAddress.ClearSelection();
        drpCompanyAddress.SelectedIndex = -1;

        // add new item <(new), 0>
        ListItem li = new ListItem(GetString("ShoppingCartOrderAddresses.NewAddress"), "0");

        DataSet ds = AddressInfoProvider.GetAddresses(mCustomerId, false, false, true, true);
        drpCompanyAddress.DataSource = ds;
        drpCompanyAddress.DataBind();
        drpCompanyAddress.Items.Insert(0, li);
        // Try to remove selected biling address
        try
        {
            // Do not remove (new) item
            if (drpBillingAddr.SelectedIndex != 0)
            {
                drpCompanyAddress.Items.Remove(drpBillingAddr.SelectedItem);
            }

            drpCompanyAddress.SelectedValue = oldSelectedValue;
        }
        catch
        {
            LoadCompanySelectedValue();
            LoadCompanyAddressInfo();
        }

        if (drpCompanyAddress.SelectedIndex != 0)
        {
            LoadCompanySelectedValue();
            LoadCompanyAddressInfo();
        }
    }
}
