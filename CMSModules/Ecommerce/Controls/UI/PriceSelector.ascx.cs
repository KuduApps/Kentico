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

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Ecommerce_Controls_UI_PriceSelector : CMSUserControl
{
    #region "Variables"

    protected bool mValidatorOnNewLine = false;
    protected int mCurrencySiteId = -1;
    protected CurrencyInfo mCurrency = null;
    protected bool mAllowZero = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether the validators messages should be under the textbox. Default is false.
    /// </summary>
    public bool ValidatorOnNewLine
    {
        get
        {
            return this.mValidatorOnNewLine;
        }
        set
        {
            this.mValidatorOnNewLine = value;

            if (value)
            {
                this.pnlNewLineWrapper.Controls.Add(this.plcValidators);
            }
            else
            {
                this.Controls.Add(this.plcValidators);
            }
        }
    }


    /// <summary>
    /// Gets or sets the message which is displayed in required field validator of the price textbox.
    /// </summary>
    public string EmptyErrorMessage
    {
        get
        {
            return this.rfvPrice.ErrorMessage;
        }
        set
        {
            this.rfvPrice.ErrorMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets the message which is displayed in range field validator of the price textbox.
    /// </summary>
    public string ValidationErrorMessage
    {
        get
        {
            return this.rvPrice.ErrorMessage;
        }
        set
        {
            this.rvPrice.ErrorMessage = value;
        }
    }


    /// <summary>
    /// Enables or disables the price textbox.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return this.txtPrice.Enabled;
        }
        set
        {
            this.txtPrice.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the value with the number of digits according to main currency significant digits settings.
    /// </summary>
    public double Value
    {
        get
        {
            return ValidationHelper.GetDouble(FormatPrice(this.txtPrice.Text), 0);
        }
        set
        {
            this.txtPrice.Text = FormatPrice(value);
        }
    }


    /// <summary>
    /// ID of the site specifying which main currency has to be used for price formatting. Default value is current site id.
    /// </summary>
    public int CurrencySiteID
    {
        get
        {
            // User current site id, when site id not entered
            if (mCurrencySiteId < 0)
            {
                return CMSContext.CurrentSiteID;
            }

            return mCurrencySiteId;
        }
        set
        {
            mCurrencySiteId = value;
            mCurrency = null;
        }
    }


    /// <summary>
    /// Currency info used for formating price.
    /// </summary>
    public CurrencyInfo Currency
    {
        get
        {
            if (mCurrency == null)
            {
                mCurrency = CurrencyInfoProvider.GetMainCurrency(CurrencySiteID);
            }

            return mCurrency;
        }
        set
        {
            mCurrency = value;
            mCurrencySiteId = -1;

            if (value != null)
            {
                mCurrencySiteId = mCurrency.CurrencySiteID;
            }
        }
    }


    /// <summary>
    /// Indicates if currency code is displayed next to price input field.
    /// </summary>
    public bool ShowCurrencyCode
    {
        get
        {
            return this.lblCurrencyCode.Visible;
        }
        set
        {
            this.lblCurrencyCode.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if zero is considered to be a valid value. Default value is true.
    /// </summary>
    public bool AllowZero
    {
        get
        {
            return ValidationHelper.GetBoolean(this.ViewState["AllowZero"], mAllowZero);
        }
        set
        {
            this.ViewState["AllowZero"] = value;
        }
    }


    /// <summary>
    /// Indicates if empty price is considered to be a valid value. Default value is false.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return !this.rfvPrice.Enabled;
        }
        set
        {
            this.rfvPrice.Enabled = !value;
        }
    }


    /// <summary>
    /// Indicates if value is formatted as integer in case it is a whole number.
    /// </summary>
    public bool FormatValueAsInteger
    {
        get;
        set;
    }

    #endregion


    #region "Methods"

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (CurrencySiteID >= 0)
        {
            lblCurrencyCode.Text = (Currency != null) ? ("&nbsp;(" + HTMLHelper.HTMLEncode(Currency.CurrencyCode) + ")") : "";
        }

        this.pnlNewLineWrapper.Visible = (this.pnlNewLineWrapper.Controls.Count > 0);
    }


    /// <summary>
    /// Validates the price (to be the double value).
    /// </summary>
    public string ValidatePrice(bool isProductOption)
    {
        // If price is empty and not required
        if (String.IsNullOrEmpty(this.txtPrice.Text.Trim()) && this.AllowEmpty)
        {
            return String.Empty;
        }

        if (isProductOption)
        {
            // Product option can be negative
            if (!ValidationHelper.IsDouble(txtPrice.Text.Trim()))
            {
                return this.ValidationErrorMessage;
            }
        }
        else
        {
            // Basic product can't be negative
            if (ValidationHelper.GetDouble(txtPrice.Text.Trim(), -1) < 0)
            {
                return this.ValidationErrorMessage;
            }
        }

        if (!AllowZero && ValidationHelper.GetDouble(txtPrice.Text.Trim(), 0) == 0)
        {
            return this.ValidationErrorMessage;
        }

        return String.Empty;
    }


    /// <summary>
    /// Returns value with the number of digits according to currency significant digits settings.
    /// Formats value as integer if enabled and value is a whole number.
    /// </summary>
    /// <param name="value"></param>
    private string FormatPrice(string value)
    {
        value = (value != null ? value.Trim() : "");
        return FormatPrice(ValidationHelper.GetDouble(value, 0));
    }


    /// <summary>
    /// Returns value with the number of digits according to currency significant digits settings.
    /// Formats value as integer if enabled and value is a whole number.
    /// </summary>
    /// <param name="value">Value to be formated</param>
    private string FormatPrice(double value)
    {
        if (this.FormatValueAsInteger)
        {
            // Format value as integer if value is a whole number
            double truncatedValue = Math.Truncate(value);
            if (value == truncatedValue)
            {
                return truncatedValue.ToString("0");
            }
        }

        // Format value according to currency
        if (Currency != null)
        {
            int digits = this.Currency.CurrencyRoundTo;
            string format = "0." + new string('0', digits);
            return value.ToString(format);
        }

        return value.ToString();
    }

    #endregion
}
