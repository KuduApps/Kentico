using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSModules_Ecommerce_Controls_UI_ProductTypes_Donation : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Private donation.
    /// </summary>
    public bool DonationIsPrivate
    {
        get
        {
            return this.chkPrivateDonation.Checked;
        }
        set
        {
            this.chkPrivateDonation.Checked = value;
        }
    }


    /// <summary>
    /// Minimum donation amount.
    /// </summary>
    public double MinimumDonationAmount
    {
        get
        {
            return this.minDonationAmount.Value;
        }
        set
        {
            this.minDonationAmount.Value = value;
        }
    }


    /// <summary>
    /// Maximum donation amount.
    /// </summary>
    public double MaximumDonationAmount
    {
        get
        {
            return this.maxDonationAmount.Value;
        }
        set
        {
            this.maxDonationAmount.Value = value;
        }
    }


    /// <summary>
    /// Product site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.minDonationAmount.CurrencySiteID;
        }
        set
        {
            this.minDonationAmount.CurrencySiteID = value;
            this.maxDonationAmount.CurrencySiteID = value;
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


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set validation messages
        this.minDonationAmount.ValidationErrorMessage = this.GetString("com.donation.mindonationamountinvalid");
        this.maxDonationAmount.ValidationErrorMessage = this.GetString("com.donation.maxdonationamountinvalid");
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Validates form and returns string with error messages.
    /// </summary>
    public string Validate()
    {
        this.ErrorMessage = String.Empty;

        // Validate minimum donation amount
        if (String.IsNullOrEmpty(this.ErrorMessage))
        {
            this.ErrorMessage = this.minDonationAmount.ValidatePrice(false);
        }

        // Validate maximum donation amount
        if (String.IsNullOrEmpty(this.ErrorMessage))
        {
            this.ErrorMessage = this.maxDonationAmount.ValidatePrice(false);
        }

        // Validate donation amount range
        if (String.IsNullOrEmpty(this.ErrorMessage) && (this.minDonationAmount.Value != 0) && (this.maxDonationAmount.Value != 0) && (this.minDonationAmount.Value > this.maxDonationAmount.Value))
        {
            this.ErrorMessage = this.GetString("com.donation.donationamountrangeinvalid");
        }

        return this.ErrorMessage;
    }

    #endregion
}
