using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartPaymentShipping : ShoppingCartStep
{
    #region "ViewState Constants"

    private const string SHIPPING_OPTION_ID = "OrderShippingOptionID";
    private const string PAYMENT_OPTION_ID = "OrderPaymenOptionID";

    #endregion


    #region "Variables"

    private bool? mIsShippingNeeded = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns true if shopping cart items need shipping.
    /// </summary>
    protected bool IsShippingNeeded
    {
        get
        {
            if (this.mIsShippingNeeded.HasValue)
            {
                return this.mIsShippingNeeded.Value;
            }
            else
            {
                if (this.ShoppingCartInfoObj != null)
                {
                    this.mIsShippingNeeded = ShippingOptionInfoProvider.IsShippingNeeded(this.ShoppingCartInfoObj);
                    return this.mIsShippingNeeded.Value;
                }
                else
                {
                    return true;
                }
            }
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblTitle.Text = this.GetString("shoppingcart.shippingpaymentoptions");
        this.lblPayment.Text = this.GetString("shoppingcartpaymentshipping.payment");
        this.lblShipping.Text = this.GetString("shoppingcartpaymentshipping.shipping");

        this.selectShipping.IsLiveSite = this.IsLiveSite;

        this.selectPayment.IsLiveSite = this.IsLiveSite;
        this.selectPayment.DisplayOnlyAllowedIfNoShipping = !this.IsShippingNeeded;

        if ((ShoppingCartInfoObj != null) && (CMSContext.CurrentSite != null))
        {
            if (ShoppingCartInfoObj.CountryID == 0)
            {
                string countryName = ECommerceSettings.DefaultCountryName(CMSContext.CurrentSite.SiteName);
                CountryInfo ci = CountryInfoProvider.GetCountryInfo(countryName);
                ShoppingCartInfoObj.CountryID = (ci != null) ? ci.CountryID : 0;
            }

            selectShipping.ShoppingCart = ShoppingCartInfoObj;
        }

        if (!this.ShoppingCartControl.IsCurrentStepPostBack)
        {
            if (this.IsShippingNeeded)
            {
                this.SelectShippingOption();
            }
            else
            {
                // Don't use shipping selection
                this.selectShipping.StopProcessing = true;

                // Hide title
                this.lblTitle.Visible = false;

                // Change current checkout process step caption
                this.ShoppingCartControl.CheckoutProcessSteps[this.ShoppingCartControl.CurrentStepIndex].Caption = this.GetString("order_new.paymentshipping.titlenoshipping");
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.selectShipping.HasData)
        {
            // Show shipping selection
            this.plcShipping.Visible = true;

            // Initialize payment options for selected shipping option
            this.selectPayment.ShippingOptionID = this.selectShipping.ShippingID;
            this.selectPayment.PaymentID = -1;
            this.selectPayment.ReloadData();
        }

        this.SelectPaymentOption();

        this.plcPayment.Visible = this.selectPayment.HasData;
    }

    #endregion


    /// <summary>
    /// Back button actions.
    /// </summary>
    public override void ButtonBackClickAction()
    {
        // Save the values to ShoppingCart ViewState
        this.ShoppingCartControl.SetTempValue(SHIPPING_OPTION_ID, this.selectShipping.ShippingID);
        this.ShoppingCartControl.SetTempValue(PAYMENT_OPTION_ID, this.selectPayment.PaymentID);

        base.ButtonBackClickAction();
    }


    public override bool ProcessStep()
    {
        try
        {
            // Cleanup the ShoppingCart ViewState
            this.ShoppingCartControl.SetTempValue(SHIPPING_OPTION_ID, null);
            this.ShoppingCartControl.SetTempValue(PAYMENT_OPTION_ID, null);

            ShoppingCartInfoObj.ShoppingCartShippingOptionID = this.selectShipping.ShippingID;
            ShoppingCartInfoObj.ShoppingCartPaymentOptionID = this.selectPayment.PaymentID;

            // Update changes in database only when on the live site
            if (!ShoppingCartControl.IsInternalOrder)
            {
                ShoppingCartInfoProvider.SetShoppingCartInfo(ShoppingCartInfoObj);
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


    /// <summary>
    /// Preselects payment option.
    /// </summary>
    protected void SelectPaymentOption()
    {
        try
        {
            // Try to select payment from ViewState first
            object viewStateValue = this.ShoppingCartControl.GetTempValue(PAYMENT_OPTION_ID);
            if (viewStateValue != null)
            {
                selectPayment.PaymentID = Convert.ToInt32(viewStateValue);
            }
            // Try to select payment option according to saved option in shopping cart object
            else if (ShoppingCartInfoObj.ShoppingCartPaymentOptionID > 0)
            {
                selectPayment.PaymentID = ShoppingCartInfoObj.ShoppingCartPaymentOptionID;
            }
            // Try to select payment option according to user preffered option
            else
            {
                CustomerInfo customer = ShoppingCartInfoObj.CustomerInfoObj;
                int paymentOptionId = (customer.CustomerUser != null) ? customer.CustomerUser.GetUserPreferredPaymentOptionID(CMSContext.CurrentSiteName) : 0;
                if (paymentOptionId > 0)
                {
                    selectPayment.PaymentID = paymentOptionId;
                }
            }
        }
        catch
        {
        }
    }


    /// <summary>
    /// Preselects shipping option.
    /// </summary>
    protected void SelectShippingOption()
    {
        try
        {
            // Try to select shipping from ViewState first
            object viewStateValue = this.ShoppingCartControl.GetTempValue(SHIPPING_OPTION_ID);
            if (viewStateValue != null)
            {
                selectShipping.ShippingID = Convert.ToInt32(viewStateValue);
            }
            // Try to select shipping option according to saved option in shopping cart object
            else if (ShoppingCartInfoObj.ShoppingCartShippingOptionID > 0)
            {
                selectShipping.ShippingID = ShoppingCartInfoObj.ShoppingCartShippingOptionID;
            }
            // Try to select shipping option according to user preffered option
            else
            {
                CustomerInfo customer = ShoppingCartInfoObj.CustomerInfoObj;
                int shippingOptionId = (customer.CustomerUser != null) ? customer.CustomerUser.GetUserPreferredShippingOptionID(CMSContext.CurrentSiteName) : 0;
                if (shippingOptionId > 0)
                {
                    selectShipping.ShippingID = shippingOptionId;
                }
            }
        }
        catch
        {
        }
    }


    public override bool IsValid()
    {
        string errorMessage = "";

        // If shipping is required
        if (plcShipping.Visible)
        {
            if (selectShipping.ShippingID <= 0)
            {
                errorMessage = GetString("Order_New.NoShippingOption");
            }
        }

        // If payment is required
        if (plcPayment.Visible)
        {
            if ((errorMessage == "") && (selectPayment.PaymentID <= 0))
            {
                errorMessage = GetString("Order_New.NoPaymentMethod");
            }
        }

        if (errorMessage == "")
        {
            // Form is valid
            return true;
        }

        // Form is not valid
        lblError.Visible = true;
        lblError.Text = errorMessage;
        return false;
    }
}
