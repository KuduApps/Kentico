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
using System.Text;

using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartPreview : ShoppingCartStep
{
    #region "ViewState Constants"

    private const string ORDER_NOTE = "OrderNote";

    #endregion


    SiteInfo currentSite = null;
    private int mAddressCount = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentSite = CMSContext.CurrentSite;

        this.lblTitle.Text = GetString("ShoppingCartPreview.Title");

        if ((ShoppingCartInfoObj != null) && (ShoppingCartInfoObj.CountryID == 0) && (currentSite != null))
        {
            string countryName = ECommerceSettings.DefaultCountryName(currentSite.SiteName);
            CountryInfo ci = CountryInfoProvider.GetCountryInfo(countryName);
            ShoppingCartInfoObj.CountryID = (ci != null) ? ci.CountryID : 0;
        }

        this.ShoppingCartControl.ButtonNext.Text = GetString("Ecommerce.OrderPreview.NextButtonText");

        // addresses initialization
        pnlBillingAddress.GroupingText = GetString("Ecommerce.CartPreview.BillingAddressPanel");
        pnlShippingAddress.GroupingText = GetString("Ecommerce.CartPreview.ShippingAddressPanel");
        pnlCompanyAddress.GroupingText = GetString("Ecommerce.CartPreview.CompanyAddressPanel");

        FillBillingAddressForm(ShoppingCartInfoObj.ShoppingCartBillingAddressID);
        FillShippingAddressForm(ShoppingCartInfoObj.ShoppingCartShippingAddressID);

        // Load company address
        if (ShoppingCartInfoObj.ShoppingCartCompanyAddressID > 0)
        {
            lblCompany.Text = OrderInfoProvider.GetAddress(ShoppingCartInfoObj.ShoppingCartCompanyAddressID);
            mAddressCount++;
            tdCompanyAddress.Visible = true;
        }
        else
        {
            tdCompanyAddress.Visible = false;
        }

        // Enable sending order notifications when creating order from CMSDesk
        if ((this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrder) ||
            this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskCustomer)
        {
            chkSendEmail.Visible = true;
            chkSendEmail.Checked = ECommerceSettings.SendOrderNotification(currentSite.SiteName);
            chkSendEmail.Text = GetString("ShoppingCartPreview.SendNotification");
        }

        // Show tax registration ID and organization ID
        InitializeIDs();

        // shopping cart content table initialization
        gridData.Columns[4].HeaderText = GetString("Ecommerce.ShoppingCartContent.SKUName");
        gridData.Columns[5].HeaderText = GetString("Ecommerce.ShoppingCartContent.SKUUnits");
        gridData.Columns[6].HeaderText = GetString("Ecommerce.ShoppingCartContent.UnitPrice");
        gridData.Columns[7].HeaderText = GetString("Ecommerce.ShoppingCartContent.UnitDiscount");
        gridData.Columns[8].HeaderText = GetString("Ecommerce.ShoppingCartContent.Tax");
        gridData.Columns[9].HeaderText = GetString("Ecommerce.ShoppingCartContent.Subtotal");

        // Product tax summary table initialiazation
        gridTaxSummary.Columns[0].HeaderText = GetString("Ecommerce.CartContent.TaxDisplayName");
        gridTaxSummary.Columns[1].HeaderText = GetString("Ecommerce.CartContent.TaxSummary");

        // Shipping tax summary table initialiazation
        gridShippingTaxSummary.Columns[0].HeaderText = GetString("com.CartContent.ShippingTaxDisplayName");
        gridShippingTaxSummary.Columns[1].HeaderText = GetString("Ecommerce.CartContent.TaxSummary");

        ReloadData();

        // order note initialization
        lblNote.Text = GetString("ecommerce.cartcontent.notelabel");
        if (!this.ShoppingCartControl.IsCurrentStepPostBack)
        {
            // Try to select payment from ViewState first
            object viewStateValue = this.ShoppingCartControl.GetTempValue(ORDER_NOTE);
            if (viewStateValue != null)
            {
                this.txtNote.Text = Convert.ToString(viewStateValue);
            }
            else
            {
                this.txtNote.Text = ShoppingCartInfoObj.ShoppingCartNote;
            }
        }
        // Display/Hide column with applied discount
        gridData.Columns[7].Visible = this.ShoppingCartInfoObj.IsDiscountApplied;


        if (mAddressCount == 2)
        {
            tblAddressPreview.Attributes["class"] = "AddressPreviewWithTwoColumns";
        }
        else if (mAddressCount == 3)
        {
            tblAddressPreview.Attributes["class"] = "AddressPreviewWithThreeColumns";
        }
    }


    protected void Page_Prerender(object sender, EventArgs e)
    {
        // Hide columns with identifiers
        gridData.Columns[0].Visible = false;
        gridData.Columns[1].Visible = false;
        gridData.Columns[2].Visible = false;
        gridData.Columns[3].Visible = false;

        // Disable default button in the order preview to 
        // force approvement of the order by mouse click
        if (this.ShoppingCartControl.ShoppingCartContainer != null)
        {
            this.ShoppingCartControl.ShoppingCartContainer.DefaultButton = "";
        }

        // Display/hide error message
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text.Trim());
    }


    protected void ReloadData()
    {
        gridData.DataSource = ShoppingCartInfoObj.ContentTable;
        gridData.DataBind();

        gridTaxSummary.DataSource = ShoppingCartInfoObj.ContentTaxesTable;
        gridTaxSummary.DataBind();

        gridShippingTaxSummary.DataSource = ShoppingCartInfoObj.ShippingTaxesTable;
        gridShippingTaxSummary.DataBind();

        // shipping option, payment method initialization
        InitPaymentShipping();
    }


    /// <summary>
    /// Fills billing address form.
    /// </summary>
    /// <param name="addressId">Billing address id</param>
    protected void FillBillingAddressForm(int addressId)
    {
        this.lblBill.Text = OrderInfoProvider.GetAddress(addressId);
    }


    /// <summary>
    /// Fills shipping address form.
    /// </summary>
    /// <param name="addressId">Shipping address id</param>
    protected void FillShippingAddressForm(int addressId)
    {
        this.lblShip.Text = OrderInfoProvider.GetAddress(addressId);
    }


    /// <summary>
    /// Back button actions.
    /// </summary>
    public override void ButtonBackClickAction()
    {
        // Save the values to ShoppingCart ViewState
        this.ShoppingCartControl.SetTempValue(ORDER_NOTE, this.txtNote.Text);

        base.ButtonBackClickAction();
    }


    /// <summary>
    /// Validates shopping cart content.
    /// </summary>    
    public override bool IsValid()
    {
        // Check inventory
        string error = ShoppingCartInfoProvider.CheckShoppingCart(this.ShoppingCartInfoObj);
        if (!string.IsNullOrEmpty(error))
        {
            // Display error message
            lblError.Text = error.Replace(";", "<br />");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Saves order information from ShoppingCartInfo object to database as new order.
    /// </summary>
    public override bool ProcessStep()
    {
        // Load first step if there is no currency or no address
        if ((this.ShoppingCartInfoObj.ShoppingCartBillingAddressID <= 0) || (this.ShoppingCartInfoObj.ShoppingCartCurrencyID <= 0))
        {
            this.ShoppingCartControl.LoadStep(0);
            return false;
        }

        // Deal with order note
        this.ShoppingCartControl.SetTempValue(ORDER_NOTE, null);
        this.ShoppingCartInfoObj.ShoppingCartNote = this.txtNote.Text.Trim();

        try
        {
            // Set order culture
            ShoppingCartInfoObj.ShoppingCartCulture = CMSContext.PreferredCultureCode;

            // Update customer preferences
            CustomerInfoProvider.SetCustomerPreferredSettings(ShoppingCartInfoObj);

            // Create order
            ShoppingCartInfoProvider.SetOrder(this.ShoppingCartInfoObj);
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("Ecommerce.OrderPreview.ErrorOrderSave") + " (" + ex.Message + ")";
            return false;
        }

        // Track order items conversions
        ECommerceHelper.TrackOrderItemsConversions(ShoppingCartInfoObj);

        // Track order conversion        
        string name = this.ShoppingCartControl.OrderTrackConversionName;
        ECommerceHelper.TrackOrderConversion(ShoppingCartInfoObj, name);

        // Track order activity
        string siteName = CMSContext.CurrentSiteName;

        if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && this.LogActivityForCustomer && (this.ContactID > 0))
        {
            // Track individual items
            if (ActivitySettingsHelper.PurchasedProductEnabled(siteName))
            {
                this.ShoppingCartControl.TrackActivityPurchasedProducts(ShoppingCartInfoObj, siteName, this.ContactID);
            }
            // Tack entire purchase
            if (ActivitySettingsHelper.PurchaseEnabled(siteName))
            {
                this.ShoppingCartControl.TrackActivityPurchase(ShoppingCartInfoObj.OrderId, this.ContactID,
                    CMSContext.CurrentSiteName, URLHelper.CurrentRelativePath,
                    ShoppingCartInfoObj.TotalPriceInMainCurrency, CurrencyInfoProvider.GetFormattedPrice(ShoppingCartInfoObj.TotalPriceInMainCurrency,
                    CurrencyInfoProvider.GetMainCurrency(CMSContext.CurrentSiteID)));
            }
        }

        // Raise finish order event
        this.ShoppingCartControl.RaiseOrderCompletedEvent();

        // When in CMSDesk
        if (this.ShoppingCartControl.IsInternalOrder)
        {
            if (chkSendEmail.Checked)
            {
                // Send order notification emails
                OrderInfoProvider.SendOrderNotificationToAdministrator(this.ShoppingCartInfoObj);
                OrderInfoProvider.SendOrderNotificationToCustomer(this.ShoppingCartInfoObj);
            }
        }
        // When on the live site
        else if (ECommerceSettings.SendOrderNotification(CMSContext.CurrentSite.SiteName))
        {
            // Send order notification emails
            OrderInfoProvider.SendOrderNotificationToAdministrator(this.ShoppingCartInfoObj);
            OrderInfoProvider.SendOrderNotificationToCustomer(this.ShoppingCartInfoObj);
        }

        return true;

    }


    protected void InitPaymentShipping()
    {
        // shipping option and payment method
        lblShippingOption.Text = GetString("Ecommerce.CartContent.ShippingOption");
        lblPaymentMethod.Text = GetString("Ecommerce.CartContent.PaymentMethod");
        lblShipping.Text = GetString("Ecommerce.CartContent.Shipping");

        if (currentSite != null)
        {
            // get shipping option name
            ShippingOptionInfo shippingObj = ShoppingCartInfoObj.ShippingOptionInfoObj;
            if (shippingObj != null)
            {
                mAddressCount++;
                //plcShippingAddress.Visible = true;
                tdShippingAddress.Visible = true;
                plcShipping.Visible = true;
                plcShippingOption.Visible = true;
                lblShippingOptionValue.Text = HTMLHelper.HTMLEncode(shippingObj.ShippingOptionDisplayName);
                lblShippingValue.Text = CurrencyInfoProvider.GetFormattedPrice(ShoppingCartInfoObj.TotalShipping, ShoppingCartInfoObj.CurrencyInfoObj);
            }
            else
            {
                //plcShippingAddress.Visible = false;
                tdShippingAddress.Visible = false;
                plcShippingOption.Visible = false;
                plcShipping.Visible = false;
            }
        }

        // get payment method name
        PaymentOptionInfo paymentObj = PaymentOptionInfoProvider.GetPaymentOptionInfo(ShoppingCartInfoObj.ShoppingCartPaymentOptionID);
        if (paymentObj != null)
        {
            lblPaymentMethodValue.Text = HTMLHelper.HTMLEncode(paymentObj.PaymentOptionDisplayName);
        }


        // total price initialization
        lblTotalPrice.Text = GetString("ecommerce.cartcontent.totalprice");
        lblTotalPriceValue.Text = CurrencyInfoProvider.GetFormattedPrice(ShoppingCartInfoObj.RoundedTotalPrice, ShoppingCartInfoObj.CurrencyInfoObj);
    }


    /// <summary>
    /// Displays product error message in shopping cart content table.
    /// </summary>
    /// <param name="skuErrorMessage">Error message to be displayed</param>
    protected string DisplaySKUErrorMessage(object skuErrorMessage)
    {
        string err = ValidationHelper.GetString(skuErrorMessage, "");
        if (err != "")
        {
            return "<br /><span class=\"ItemsNotAvailable\">" + err + "</span>";
        }
        return "";
    }


    /// <summary>
    /// Initializes tax registration ID and orgranization ID.
    /// </summary>
    protected void InitializeIDs()
    {
        SiteInfo si = CMSContext.CurrentSite;
        if (si != null)
        {
            if ((ECommerceSettings.ShowOrganizationID(si.SiteName)) && (this.ShoppingCartInfoObj.CustomerInfoObj != null))
            {
                // Initialize organization ID
                plcIDs.Visible = true;
                lblOrganizationID.Text = GetString("OrderPreview.OrganizationID");
                lblOrganizationIDVal.Text = HTMLHelper.HTMLEncode(this.ShoppingCartInfoObj.CustomerInfoObj.CustomerOrganizationID);
            }
            else
            {
                lblOrganizationID.Visible = false;
                lblOrganizationIDVal.Visible = false;
            }
            if ((ECommerceSettings.ShowTaxRegistrationID(si.SiteName)) && (this.ShoppingCartInfoObj.CustomerInfoObj != null))
            {
                // Initialize tax registration ID
                plcIDs.Visible = true;
                lblTaxRegistrationID.Text = GetString("OrderPreview.TaxRegistrationID");
                lblTaxRegistrationIDVal.Text = HTMLHelper.HTMLEncode(this.ShoppingCartInfoObj.CustomerInfoObj.CustomerTaxRegistrationID);
            }
            else
            {
                lblTaxRegistrationID.Visible = false;
                lblTaxRegistrationIDVal.Visible = false;
            }
        }
    }


    /// <summary>
    /// Returns formated value string.
    /// </summary>
    /// <param name="value">Value to format</param>
    protected string GetFormattedValue(object value)
    {
        double price = ValidationHelper.GetDouble(value, 0);
        return CurrencyInfoProvider.GetFormattedValue(price, this.ShoppingCartInfoObj.CurrencyInfoObj);
    }


    /// <summary>
    /// Returns formatted and localized SKU name.
    /// </summary>
    /// <param name="value">SKU name</param>
    /// <param name="isProductOption">Indicates if cart item is product option</param>
    /// <param name="isBundleItem">Indicates if cart item is bundle item</param>
    protected string GetSKUName(object value, object isProductOption, object isBundleItem, object itemText)
    {
        string name = ResHelper.LocalizeString((string)value);
        string text = itemText as string;

        // If it is a product option or bundle item
        if (ValidationHelper.GetBoolean(isProductOption, false) || ValidationHelper.GetBoolean(isBundleItem, false))
        {
            StringBuilder skuName = new StringBuilder("<span style=\"font-size:90%\"> - ");
            skuName.Append(HTMLHelper.HTMLEncode(name));

            if (!string.IsNullOrEmpty(text))
            {
                skuName.Append(" '" + HTMLHelper.HTMLEncode(text) + "'");
            }

            skuName.Append("</span>");
            return skuName.ToString();
        }
        // If it is a parent product
        else
        {
            return HTMLHelper.HTMLEncode(name);
        }
    }
}
