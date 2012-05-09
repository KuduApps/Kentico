using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Controls_MyDetails_MyCredit : CMSAdminControl
{
    private CurrencyInfo mCurrency = null;
    private int mCustomerId = 0;
    private double rate = 1;

    /// <summary>
    /// Site default currency object.
    /// </summary>
    private CurrencyInfo Currency
    {
        get
        {
            if (mCurrency == null)
            {
                mCurrency = ECommerceContext.CurrentCurrency;
            }

            if (mCurrency == null)
            {
                mCurrency = CurrencyInfoProvider.GetCurrencyInfo(CMSContext.CurrentSite.SiteDefaultCurrencyID);
            }

            return mCurrency;
        }
    }


    /// <summary>
    /// Customer ID.
    /// </summary>
    public int CustomerId
    {
        get
        {
            return mCustomerId;
        }
        set
        {
            mCustomerId = value;
        }
    }


    /// <summary>
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["StopProcessing"], false);
        }
        set
        {
            ViewState["StopProcessing"] = value;
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                // Get site id of credits main currency
                int creditSiteId = ECommerceHelper.GetSiteID(CMSContext.CurrentSiteID, ECommerceSettings.USE_GLOBAL_CREDIT);

                gridCreditEvents.HideControlForZeroRows = true;
                gridCreditEvents.IsLiveSite = this.IsLiveSite;
                gridCreditEvents.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridCreditEvents_OnExternalDataBound);
                gridCreditEvents.OrderBy = "EventDate DESC, EventName ASC";
                gridCreditEvents.WhereCondition = "EventCustomerID = " + this.CustomerId + " AND ISNULL(EventSiteID, 0) = " + creditSiteId;

                // Get total credit value
                double credit = CreditEventInfoProvider.GetTotalCredit(this.CustomerId, CMSContext.CurrentSiteID);

                if (Currency != null)
                {
                    // Convert global credit to site main currency when using one
                    string siteName = CMSContext.CurrentSiteName;
                    rate = ExchangeRateInfoProvider.GetLastExchangeRate(Currency.CurrencyID, CMSContext.CurrentSiteID, ECommerceSettings.UseGlobalCredit(siteName));
                    credit = ExchangeRateInfoProvider.ApplyExchangeRate(credit, rate);
                }

                lblCreditValue.Text = CurrencyInfoProvider.GetFormattedPrice(credit, Currency);
                lblCredit.Text = GetString("Ecommerce.MyCredit.TotalCredit");
            }
            else
            {
                // Hide if user is not authenticated
                this.Visible = false;
            }
        }
    }

    protected object gridCreditEvents_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // Show only date part from date-time value
        switch (sourceName.ToLower())
        {
            case "eventdate":
                DateTime date = ValidationHelper.GetDateTime(parameter, DataHelper.DATETIME_NOT_SELECTED);
                if (date != DataHelper.DATETIME_NOT_SELECTED)
                {
                    return date.ToShortDateString();
                }
                else
                {
                    return "";
                }

            case "eventcreditchange":
                double credit = ExchangeRateInfoProvider.ApplyExchangeRate(ValidationHelper.GetDouble(parameter, 0), rate);
                return CurrencyInfoProvider.GetFormattedPrice(credit, Currency);
        }

        return parameter;
    }


    /// <summary>
    /// Overriden SetValue - because of MyAccount webpart.
    /// </summary>
    /// <param name="propertyName">Name of the property to set</param>
    /// <param name="value">Value to set</param>
    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        switch (propertyName.ToLower())
        {
            case "customerid":
                this.CustomerId = ValidationHelper.GetInteger(value, 0);
                break;
        }
    }
}
