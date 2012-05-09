using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.Ecommerce;
using CMS.GlobalHelper;

public partial class CMSModules_Ecommerce_Controls_ProductOptions_DonationProperties : CMSUserControl
{
    #region "Variables"

    private ShoppingCartInfo mShoppingCart = null;

    bool mShowDonationAmount = true;
    bool mShowCurrencyCode = true;
    bool mShowDonationUnits = true;
    bool mShowDonationIsPrivate = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Shopping cart data used for price calculation and formatting.
    /// </summary>
    public ShoppingCartInfo ShoppingCart
    {
        get
        {
            if (this.mShoppingCart == null)
            {
                this.mShoppingCart = ECommerceContext.CurrentShoppingCart;
            }

            return this.mShoppingCart;
        }
        set
        {
            this.mShoppingCart = value;
        }
    }


    /// <summary>
    /// Donation SKU data.
    /// </summary>
    public SKUInfo SKU
    {
        get;
        set;
    }


    /// <summary>
    /// Donation amount in shopping cart currency.
    /// </summary>
    public double DonationAmount
    {
        get
        {
            return this.amountPriceSelector.Value;
        }
        set
        {
            this.amountPriceSelector.Value = value;
            this.DonationAmountInitialized = true;
        }
    }


    /// <summary>
    /// Indicates if donation amount value was initialized already.
    /// </summary>
    public bool DonationAmountInitialized
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["DonationAmountInitialized"], false);
        }
        protected set
        {
            this.ViewState["DonationAmountInitialized"] = value;
        }
    }


    /// <summary>
    /// Indicates if donation is private.
    /// </summary>
    public bool DonationIsPrivate
    {
        get
        {
            return this.chkIsPrivate.Checked;
        }
        set
        {
            this.chkIsPrivate.Checked = value;
        }
    }


    /// <summary>
    /// Donation units.
    /// </summary>
    public int DonationUnits
    {
        get
        {
            return ValidationHelper.GetInteger(this.txtUnits.Text, 1);
        }
        set
        {
            this.txtUnits.Text = ValidationHelper.GetString(value, "1");
        }
    }


    /// <summary>
    /// Validation error message.
    /// </summary>
    public string ErrorMessage
    {
        get;
        set;
    }

    #endregion


    #region "Properties - Layout"

    /// <summary>
    /// Indicates if donation amount input field is displayed.
    /// </summary>
    public bool ShowDonationAmount
    {
        get
        {
            return this.mShowDonationAmount;
        }
        set
        {
            this.mShowDonationAmount = value;
            this.plcAmount.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if currency code is displayed next to donation amount input field.
    /// </summary>
    public bool ShowCurrencyCode
    {
        get
        {
            return this.mShowCurrencyCode;
        }
        set
        {
            this.mShowCurrencyCode = value;
            this.amountPriceSelector.ShowCurrencyCode = value;
        }
    }


    /// <summary>
    /// Indicates if donation units input field is displayed.
    /// </summary>
    public bool ShowDonationUnits
    {
        get
        {
            return this.mShowDonationUnits;
        }
        set
        {
            this.mShowDonationUnits = value;
            this.plcUnits.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if checkbox for private donation is displayed.
    /// </summary>
    public bool ShowDonationIsPrivate
    {
        get
        {
            return this.mShowDonationIsPrivate;
        }
        set
        {
            this.mShowDonationIsPrivate = value;
            this.plcIsPrivate.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if at least one of donation properties editable fields is visible.
    /// </summary>
    public bool HasEditableFieldsVisible
    {
        get
        {
            return (this.plcAmount.Visible || this.plcUnits.Visible || this.plcIsPrivate.Visible);
        }
    }

    #endregion


    #region "Page methods"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (this.StopProcessing)
        {
            return;
        }

        this.ReloadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show donation amount error message if required
        this.lblAmountError.Visible = !String.IsNullOrEmpty(this.lblAmountError.Text);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads donation properties data.
    /// </summary>
    public void ReloadData()
    {
        // If SKU is donation
        if ((this.SKU != null) && (this.SKU.SKUProductType == SKUProductTypeEnum.Donation))
        {
            this.amountPriceSelector.Currency = this.ShoppingCart.CurrencyInfoObj;
            this.amountPriceSelector.ValidationErrorMessage = this.GetString("com.donationproperties.amountinvalid");
            this.amountPriceSelector.EmptyErrorMessage = this.GetString("com.donationproperties.amountinvalid");

            this.rvUnits.ErrorMessage = this.GetString("com.donationproperties.unitsinvalid");

            if (!this.DonationAmountInitialized)
            {
                // Set donation default amount in cart currency                
                this.amountPriceSelector.Value = this.ShoppingCart.ApplyExchangeRate(this.SKU.SKUPrice, this.SKU.IsGlobal);

                this.DonationAmountInitialized = true;
            }

            this.ShowDonationAmount &= !((this.SKU.SKUMinPrice == this.SKU.SKUPrice) && (this.SKU.SKUMaxPrice == this.SKU.SKUPrice));
            this.ShowDonationIsPrivate &= this.SKU.SKUPrivateDonation;
        }
    }


    /// <summary>
    /// Validates form and returns error message in case form is not valid.
    /// </summary>
    public string Validate()
    {
        // Validate donation amount
        if (String.IsNullOrEmpty(this.ErrorMessage))
        {
            this.ErrorMessage = this.amountPriceSelector.ValidatePrice(false);
            this.lblAmountError.Text = this.ErrorMessage;
        }

        // Validate donation amount range
        if (String.IsNullOrEmpty(this.ErrorMessage) && (this.SKU != null))
        {
            // Get min/max prices in cart currency
            double minPrice = this.ShoppingCart.ApplyExchangeRate(this.SKU.SKUMinPrice, this.SKU.IsGlobal);
            double maxPrice = this.ShoppingCart.ApplyExchangeRate(this.SKU.SKUMaxPrice, this.SKU.IsGlobal);          

            if ((minPrice > 0) && (this.amountPriceSelector.Value < minPrice) || ((maxPrice > 0) && (this.amountPriceSelector.Value > maxPrice)))
            {
                // Get formatted min/max prices
                string fMinPrice = this.ShoppingCart.GetFormattedPrice(minPrice);
                string fMaxPrice = this.ShoppingCart.GetFormattedPrice(maxPrice);

                // Set appropriate error message
                if ((minPrice > 0.0) && (maxPrice > 0.0))
                {
                    this.ErrorMessage = String.Format(this.GetString("com.donationproperties.amountrange"), fMinPrice, fMaxPrice);
                }
                else if (minPrice > 0.0)
                {
                    this.ErrorMessage = String.Format(this.GetString("com.donationproperties.amountrangemin"), fMinPrice);
                }
                else if (maxPrice > 0.0)
                {
                    this.ErrorMessage = String.Format(this.GetString("com.donationproperties.amountrangemax"), fMaxPrice);
                }

                this.lblAmountError.Text = this.ErrorMessage;
            }
        }

        // Validate donation units
        if (String.IsNullOrEmpty(this.ErrorMessage))
        {
            this.rvUnits.Validate();
            if (!this.rvUnits.IsValid)
            {
                this.ErrorMessage = this.rvUnits.ErrorMessage;
            }
        }

        return this.ErrorMessage;
    }

    #endregion
}