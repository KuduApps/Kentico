using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.ComponentModel;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSWebParts_Ecommerce_Donate : CMSAbstractWebPart
{
    #region "Variables"

    private Guid mDonationGUID = Guid.Empty;
    private SKUInfo mDonationSKU = null;
    private bool mShowInDialog = false;
    private double mDonationAmount = 0.0;
    private bool mShowAmountTextbox = false;
    private bool mShowCurrencyCode = false;
    private bool mShowUnitsTextbox = false;
    private bool mAllowPrivateDonation = false;

    private string mDonationsPagePath = null;

    private string mControlType = "BUTTON";
    private string mControlText = null;
    private string mControlImage = null;
    private string mControlTooltip = null;
    private string mDescription = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// GUID of the selected donation product.
    /// </summary>
    public Guid DonationGUID
    {
        get
        {
            return ValidationHelper.GetGuid(this.GetValue("DonationGUID"), this.mDonationGUID);
        }
        set
        {
            this.SetValue("DonationGUID", value);
            this.mDonationGUID = value;

            // Invalidate product data
            this.mDonationSKU = null;
        }
    }


    /// <summary>
    /// Data of the selected donation product.
    /// </summary>
    private SKUInfo DonationSKU
    {
        get
        {
            if (mDonationSKU == null)
            {
                mDonationSKU = SKUInfoProvider.GetSKUInfo(this.DonationGUID);
            }

            return mDonationSKU;
        }
    }


    /// <summary>
    /// Indicates if donate action opens donate form in dialog window.
    /// </summary>
    public bool ShowInDialog
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowInDialog"), this.mShowInDialog);
        }
        set
        {
            this.SetValue("ShowInDialog", value);
            this.mShowInDialog = value;
        }
    }


    /// <summary>
    /// Overrides donation amount value of selected donation product. It is in site main currency.
    /// </summary>
    public double DonationAmount
    {
        get
        {
            return ValidationHelper.GetDouble(this.GetValue("DonationAmount"), this.mDonationAmount);
        }
        set
        {
            this.SetValue("DonationAmount", value);
            this.mDonationAmount = value;
        }
    }


    /// <summary>
    /// Show donation amount textbox
    /// </summary>
    public bool ShowAmountTextbox
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowAmountTextbox"), this.mShowAmountTextbox);
        }
        set
        {
            this.SetValue("ShowAmountTextbox", value);
            this.mShowAmountTextbox = value;
        }
    }


    /// <summary>
    /// Show currency code
    /// </summary>
    public bool ShowCurrencyCode
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowCurrencyCode"), this.mShowCurrencyCode);
        }
        set
        {
            this.SetValue("ShowCurrencyCode", value);
            this.mShowCurrencyCode = value;
        }
    }


    /// <summary>
    /// Indicates if units textbox will be displayed in donate dialog and therefore if it will be possible to change number of units added to the shopping cart.
    /// </summary>
    public bool ShowUnitsTextbox
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowUnitsTextbox"), this.mShowUnitsTextbox);
        }
        set
        {
            this.SetValue("ShowUnitsTextbox", value);
            this.mShowUnitsTextbox = value;
        }
    }


    /// <summary>
    /// Allow private donation
    /// </summary>
    public bool AllowPrivateDonation
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowPrivateDonation"), this.mAllowPrivateDonation);
        }
        set
        {
            this.SetValue("AllowPrivateDonation", value);
            this.mAllowPrivateDonation = value;
        }
    }


    /// <summary>
    /// Path to the page with list of available donations.
    /// </summary>
    public string DonationsPagePath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DonationsPagePath"), this.mDonationsPagePath);
        }
        set
        {
            this.SetValue("DonationsPagePath", value);
            this.mDonationsPagePath = value;
        }
    }


    /// <summary>
    /// Type of the donate control.
    /// Possible values: 'BUTTON' - button control, 'LINK' - text link control.
    /// </summary>
    public string ControlType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ControlType"), this.mControlType);
        }
        set
        {
            this.SetValue("ControlType", value);
            this.mControlType = value;
        }
    }


    /// <summary>
    /// Text of the donate control.
    /// </summary>
    public string ControlText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ControlText"), this.mControlText);
        }
        set
        {
            this.SetValue("ControlText", value);
            this.mControlText = value;
        }
    }


    /// <summary>
    /// Image of the donate control.
    /// </summary>
    public string ControlImage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ControlImage"), this.mControlImage);
        }
        set
        {
            this.SetValue("ControlImage", value);
            this.mControlImage = value;
        }
    }


    /// <summary>
    /// Tooltip text of the donate control.
    /// </summary>
    public string ControlTooltip
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ControlTooltip"), this.mControlTooltip);
        }
        set
        {
            this.SetValue("ControlTooltip", value);
            this.mControlTooltip = value;
        }
    }


    /// <summary>
    /// Text that is be displayed along with donate control.
    /// </summary>
    public string Description
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Description"), this.mDescription);
        }
        set
        {
            this.SetValue("Description", value);
            this.mDescription = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Gets dialog identificator.
    /// </summary>
    protected string DialogIdentificator
    {
        get
        {
            if (String.IsNullOrEmpty(this.hdnDialogIdentificator.Value))
            {
                if (String.IsNullOrEmpty(this.Request.Form[this.hdnDialogIdentificator.UniqueID]))
                {
                    this.hdnDialogIdentificator.Value = Guid.NewGuid().ToString();
                }
            }

            return this.hdnDialogIdentificator.Value;
        }
    }


    /// <summary>
    /// Indicates if selected donation has fixed parameters.
    /// </summary>
    protected bool DonationIsFixed
    {
        get
        {
            if (this.DonationSKU != null)
            {
                return (!this.DonationSKU.SKUPrivateDonation && (this.DonationSKU.SKUMinPrice == this.DonationSKU.SKUPrice) && (this.DonationSKU.SKUMaxPrice == this.DonationSKU.SKUPrice));
            }

            return false;
        }
    }

    #endregion


    #region "Page methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        this.SetupControl();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblError.Visible = !String.IsNullOrEmpty(this.lblError.Text);
        this.lblDescription.Visible = !String.IsNullOrEmpty(this.lblDescription.Text);

        this.plcDonationProperties.Visible = this.donationProperties.Visible;
        this.plcFieldLabel.Visible = this.donationProperties.HasEditableFieldsVisible;

        // Register dialog script
        ScriptHelper.RegisterDialogScript(this.Page);

        // Register script to open donate dialog
        StringBuilder openDonateDialogScript = new StringBuilder();

        openDonateDialogScript.AppendLine("function openDonateDialog(url) {");
        openDonateDialogScript.AppendLine("    modalDialog(url, 'Donate', 500, 360);");
        openDonateDialogScript.AppendLine("};");

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "OpenDonateDialog", ScriptHelper.GetScript(openDonateDialogScript.ToString()));

        // Register script to set donation parameter
        StringBuilder setDonationParameterScript = new StringBuilder();

        setDonationParameterScript.AppendLine("function setDonationParameter(elementId, value) {");
        setDonationParameterScript.AppendLine("    var element = document.getElementById(elementId);");
        setDonationParameterScript.AppendLine("    if (element != null) {");
        setDonationParameterScript.AppendLine("        element.value = value;");
        setDonationParameterScript.AppendLine("    };");
        setDonationParameterScript.AppendLine("};");

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "SetDonationParameter", ScriptHelper.GetScript(setDonationParameterScript.ToString()));
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.shoppingCartItemSelector.StopProcessing = true;
            return;
        }

        // Set description
        this.lblDescription.Text = HTMLHelper.HTMLEncode(this.Description);

        // Initialize donation properties
        this.donationProperties.SKU = this.DonationSKU;
        this.donationProperties.Visible = (!this.ShowInDialog && (this.DonationSKU != null));
        this.donationProperties.ShowDonationAmount = this.ShowAmountTextbox;
        this.donationProperties.ShowCurrencyCode = this.ShowCurrencyCode;
        this.donationProperties.ShowDonationUnits = this.ShowUnitsTextbox;
        this.donationProperties.ShowDonationIsPrivate = this.AllowPrivateDonation;

        if ((this.DonationAmount > 0) && !this.donationProperties.DonationAmountInitialized)
        {
            // Get amount in cart currency
            double amount = ECommerceContext.CurrentShoppingCart.ApplyExchangeRate(this.DonationAmount);

            this.donationProperties.DonationAmount = amount;
        }

        // Initialize shopping cart item selector control
        if (this.DonationSKU != null)
        {
            this.shoppingCartItemSelector.SKUID = this.DonationSKU.SKUID;
        }

        if (!String.IsNullOrEmpty(this.ControlImage))
        {
            this.shoppingCartItemSelector.AddToCartImageButton = this.ControlImage;
        }
        else
        {
            if (this.ControlType.ToUpper() == "BUTTON")
            {
                this.shoppingCartItemSelector.AddToCartText = this.ControlText;
            }
            else
            {
                this.shoppingCartItemSelector.AddToCartLinkText = HTMLHelper.HTMLEncode(this.ControlText);
            }
        }

        this.shoppingCartItemSelector.AddToCartTooltip = this.ControlTooltip;
        this.shoppingCartItemSelector.SKUEnabled = true;
        this.shoppingCartItemSelector.OnAddToShoppingCart += new CancelEventHandler(shoppingCartItemSelector_OnAddToShoppingCart);
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        this.SetupControl();
    }


    protected void shoppingCartItemSelector_OnAddToShoppingCart(object sender, CancelEventArgs e)
    {
        // If donations page path specified
        if (!String.IsNullOrEmpty(this.DonationsPagePath))
        {
            // Redirect to donations page
            URLHelper.Redirect(CMSContext.GetUrl(this.DonationsPagePath));

            // Cancel further processing
            e.Cancel = true;
            return;
        }

        // If donation not selected
        if (this.DonationGUID == Guid.Empty)
        {
            // Show alert
            ScriptHelper.Alert(this.Page, this.GetString("com.donate.donationnotspecified"));

            // Cancel further processing
            e.Cancel = true;
            return;
        }

        // If donate form should be opened in dialog and donation parameters are not fixed
        if (this.ShowInDialog && !this.DonationIsFixed)
        {
            // Get donation parameters from hidden fields
            double donationAmount = ValidationHelper.GetDouble(this.hdnDonationAmount.Value, 0.0);
            bool donationIsPrivate = ValidationHelper.GetBoolean(this.hdnDonationIsPrivate.Value, false);
            int donationUnits = ValidationHelper.GetInteger(this.hdnDonationUnits.Value, 1);

            // If donation parameters set
            if (donationAmount > 0.0)
            {
                // Set donation properties for item to be added
                this.shoppingCartItemSelector.SetDonationProperties(donationAmount, donationIsPrivate, donationUnits);

                // Clear hidden fields
                this.hdnDonationAmount.Value = "";
                this.hdnDonationIsPrivate.Value = "";
                this.hdnDonationUnits.Value = "";
            }
            else
            {
                // Set dialog parameters
                Hashtable dialogParameters = new Hashtable();

                dialogParameters["DonationGUID"] = this.DonationGUID.ToString();
                dialogParameters["DonationAmount"] = this.DonationAmount;

                dialogParameters["DonationAmountElementID"] = this.hdnDonationAmount.ClientID;
                dialogParameters["DonationIsPrivateElementID"] = this.hdnDonationIsPrivate.ClientID;
                dialogParameters["DonationUnitsElementID"] = this.hdnDonationUnits.ClientID;

                dialogParameters["ShowDonationAmount"] = this.ShowAmountTextbox.ToString();
                dialogParameters["ShowCurrencyCode"] = this.ShowCurrencyCode.ToString();
                dialogParameters["ShowDonationUnits"] = this.ShowUnitsTextbox.ToString();
                dialogParameters["ShowDonationIsPrivate"] = this.AllowPrivateDonation.ToString();

                dialogParameters["PostBackEventReference"] = ControlsHelper.GetPostBackEventReference(this.shoppingCartItemSelector.AddToCartControl, null);

                WindowHelper.Add(this.DialogIdentificator, dialogParameters);

                // Register startup script that opens donate dialog
                string url = URLHelper.ResolveUrl("~/CMSModules/Ecommerce/CMSPages/Donate.aspx");
                url = URLHelper.AddParameterToUrl(url, "params", this.DialogIdentificator);

                string startupScript = String.Format("openDonateDialog('{0}')", url);

                ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "StartupDialogOpen", ScriptHelper.GetScript(startupScript));

                // Cancel further processing
                e.Cancel = true;
            }

            return;
        }

        // If donation properties form is valid
        if (String.IsNullOrEmpty(this.donationProperties.Validate()))
        {
            // Set donation properties for item to be added
            this.shoppingCartItemSelector.SetDonationProperties(this.donationProperties.DonationAmount, this.donationProperties.DonationIsPrivate, this.donationProperties.DonationUnits);
        }
        else
        {
            if (this.donationProperties.HasEditableFieldsVisible)
            {
                if (!this.donationProperties.ShowDonationAmount)
                {
                    // Display error messega on page
                    this.lblError.Text = this.donationProperties.ErrorMessage;
                }
            }
            else
            {
                // Display error message as alert
                ScriptHelper.Alert(this.Page, this.donationProperties.ErrorMessage);
            }

            // Cancel further processing
            e.Cancel = true;
        }
    }

    #endregion
}
