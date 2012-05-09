using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_CMSPages_Donate : CMSLiveModalPage
{
    #region "Variables"

    private SKUInfo mDonationSKU = null;

    private string dialogIdentificator = null;
    Hashtable dialogParameters = null;

    private Guid donationGuid = Guid.Empty;
    private double donationAmount = 0.0;

    private string amountElementId = null;
    private string isPrivateElementId = null;
    private string unitsElementId = null;

    private string postBackEventReference = null;

    #endregion


    #region "Properties - protected"

    /// <summary>
    /// Donation SKU data.
    /// </summary>
    protected SKUInfo DonationSKU
    {
        get
        {
            if (this.mDonationSKU == null)
            {
                this.mDonationSKU = SKUInfoProvider.GetSKUInfo(this.donationGuid);
            }

            return this.mDonationSKU;
        }
    }

    /// <summary>
    /// Indicates if donation has fixed donation amount.
    /// </summary>
    protected bool DonationHasFixedAmount
    {
        get
        {
            if (this.DonationSKU != null)
            {
                return ((this.DonationSKU.SKUMinPrice == this.DonationSKU.SKUPrice) && (this.DonationSKU.SKUMaxPrice == this.DonationSKU.SKUPrice));
            }

            return false;
        }
    }


    /// <summary>
    /// Error message.
    /// </summary>
    protected string ErrorMessage
    {
        get;
        set;
    }

    #endregion


    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get dialog identificator from URL
        this.dialogIdentificator = QueryHelper.GetString("params", null);

        // Get dialog parameters
        this.dialogParameters = (Hashtable)WindowHelper.GetItem(this.dialogIdentificator);

        if (dialogParameters != null)
        {
            this.donationGuid = ValidationHelper.GetGuid(dialogParameters["DonationGUID"], Guid.Empty);
            this.donationAmount = ValidationHelper.GetDouble(dialogParameters["DonationAmount"], 0.0);

            this.amountElementId = ValidationHelper.GetString(dialogParameters["DonationAmountElementID"], null);
            this.isPrivateElementId = ValidationHelper.GetString(dialogParameters["DonationIsPrivateElementID"], null);
            this.unitsElementId = ValidationHelper.GetString(dialogParameters["DonationUnitsElementID"], null);

            this.donationPropertiesElem.ShowDonationAmount = ValidationHelper.GetBoolean(dialogParameters["ShowDonationAmount"], false);
            this.donationPropertiesElem.ShowCurrencyCode = ValidationHelper.GetBoolean(dialogParameters["ShowCurrencyCode"], false);
            this.donationPropertiesElem.ShowDonationUnits = ValidationHelper.GetBoolean(dialogParameters["ShowDonationUnits"], false);
            this.donationPropertiesElem.ShowDonationIsPrivate = ValidationHelper.GetBoolean(dialogParameters["ShowDonationIsPrivate"], false);

            this.postBackEventReference = ValidationHelper.GetString(dialogParameters["PostBackEventReference"], null);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.DonationSKU != null)
        {
            // Set localized SKU name
            this.lblSKUName.Text = this.GetString(this.DonationSKU.SKUName);

            this.donationPropertiesElem.SKU = this.DonationSKU;

            if (!this.donationPropertiesElem.DonationAmountInitialized && (this.donationAmount > 0))
            {
                // Convert from main to cart currency               
                this.donationPropertiesElem.DonationAmount = ECommerceContext.CurrentShoppingCart.ApplyExchangeRate(this.donationAmount);
            }
        }

        // Initialize dialog title
        this.CurrentMaster.Title.TitleText = this.GetString("com.donatedialog.title");
        this.CurrentMaster.Title.TitleImage = this.GetImageUrl("Objects/Ecommerce_SKU/donation.png");

        // Initialize buttons
        this.btnDonate.Click += new EventHandler(btnDonate_Click);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Display description
        this.lblDescription.Visible = this.donationPropertiesElem.HasEditableFieldsVisible;

        // If donation amount field is not displayed
        if (!this.donationPropertiesElem.ShowDonationAmount)
        {            
            // Get default amount in site currency
            double amount = this.donationAmount;
            if (amount <= 0)
            {
                // Get amount in site or global currency
                amount = this.DonationSKU.SKUPrice;

                // Convert amount from global to site currency
                if (this.DonationSKU.IsGlobal)
                {
                    amount = ECommerceContext.CurrentShoppingCart.ApplyExchangeRate(amount, true);
                }
            }

            // Convert from site to cart currency
            amount = ECommerceContext.CurrentShoppingCart.ApplyExchangeRate(amount);
            string formattedAmount = ECommerceContext.CurrentShoppingCart.GetFormattedPrice(amount, true);           

            this.lblAmount.Text = String.Format(this.GetString("com.donatedialog.amount"), formattedAmount);
        }

        this.lblAmount.Visible = !String.IsNullOrEmpty(this.lblAmount.Text); ;

        // If donation has fixed donation amount and no donation properties fields are visible
        if (this.DonationHasFixedAmount || !this.donationPropertiesElem.HasEditableFieldsVisible)
        {
            this.plcMinMaxLabels.Visible = false;
        }
        else
        {
            // Initialize minimum and maximum donation amount labels
            if (this.DonationSKU.SKUMinPrice > 0.0)
            {
                double amount = ECommerceContext.CurrentShoppingCart.ApplyExchangeRate(this.DonationSKU.SKUMinPrice, this.DonationSKU.IsGlobal);
                string formattedAmount = ECommerceContext.CurrentShoppingCart.GetFormattedPrice(amount, true);
                this.lblMinimumAmount.Text = String.Format(this.GetString("com.donatedialog.minimumamount"), formattedAmount);
            }

            if (this.DonationSKU.SKUMaxPrice > 0.0)
            {
                double amount = ECommerceContext.CurrentShoppingCart.ApplyExchangeRate(this.DonationSKU.SKUMaxPrice, this.DonationSKU.IsGlobal);
                string formattedAmount = ECommerceContext.CurrentShoppingCart.GetFormattedPrice(amount, true);
                this.lblMaximumAmount.Text = String.Format(this.GetString("com.donatedialog.maximumamount"), formattedAmount);
            }
        }

        this.lblMinimumAmount.Visible = !String.IsNullOrEmpty(this.lblMinimumAmount.Text);
        this.lblMaximumAmount.Visible = !String.IsNullOrEmpty(this.lblMaximumAmount.Text);

        // Display error message
        this.lblError.Text = this.ErrorMessage;
        this.lblError.Visible = !String.IsNullOrEmpty(this.lblError.Text) && !this.donationPropertiesElem.ShowDonationAmount;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Validates form and returns true if valid.
    /// </summary>
    public bool ValidateForm()
    {
        this.ErrorMessage = this.donationPropertiesElem.Validate();

        return String.IsNullOrEmpty(this.ErrorMessage);
    }


    void btnDonate_Click(object sender, EventArgs e)
    {
        // If form is valid
        if (this.ValidateForm())
        {
            // Build script to add donation to shopping cart
            StringBuilder script = new StringBuilder();

            script.AppendLine(String.Format("wopener.setDonationParameter('{0}', '{1}');", this.amountElementId, this.donationPropertiesElem.DonationAmount));
            script.AppendLine(String.Format("wopener.setDonationParameter('{0}', '{1}');", this.unitsElementId, this.donationPropertiesElem.DonationUnits));
            script.AppendLine(String.Format("wopener.setDonationParameter('{0}', '{1}');", this.isPrivateElementId, this.donationPropertiesElem.DonationIsPrivate));
            script.AppendLine(String.Format("wopener.{0};", this.postBackEventReference));
            script.AppendLine("window.close();");

            // Register as startup script
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "AddToCart", ScriptHelper.GetScript(script.ToString()));
        }
    }

    #endregion
}

