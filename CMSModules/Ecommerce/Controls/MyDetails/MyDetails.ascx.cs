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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_MyDetails_MyDetails : CMSAdminControl
{
    /// <summary>
    /// Inform on new customer creation.
    /// </summary>
    /// <param name="url">Url of the page where control currently sits</param>
    public delegate void CustomerCreated();

    /// <summary>
    /// Fired when new customer is created.
    /// </summary>
    public event CustomerCreated OnCustomerCrated;

    private CustomerInfo mCustomer = null;

    /// <summary>
    /// Customer info object.
    /// </summary>
    public CustomerInfo Customer
    {
        get
        {
            return mCustomer;
        }
        set
        {
            mCustomer = value;
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
            this.drpCountry.StopProcessing = value;
            this.drpPayment.StopProcessing = value;
            this.drpShipping.StopProcessing = value;
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                btnOk.Text = GetString("General.OK");
                lblCustomerLastName.Text = GetString("Customers_Edit.CustomerLastNameLabel");
                lblCustomerFirstName.Text = GetString("Customers_Edit.CustomerFirstNameLabel");
                lblCustomerCompany.Text = GetString("Customers_Edit.CustomerCompanyLabel");
                lblCustomerFax.Text = GetString("customers_edit.CustomerFax");
                lblCustomerPhone.Text = GetString("customers_edit.CustomerPhone");
                lblCustomerPreferredCurrency.Text = GetString("customers_edit.CustomerCurrency");
                lblCustomerPreferredShippingOption.Text = GetString("customers_edit.CustomerShipping");
                lblCustomerPrefferedPaymentOption.Text = GetString("customers_edit.CustomerPayment");
                lblCustomerEmail.Text = GetString("general.email");
                lblCustomerCountry.Text = GetString("Customers_Edit.CustomerCountry");
                lblCompanyAccount.Text = GetString("Customers_Edit.lblCompanyAccount");

                // WAI validation
                lblCustomerPreferredCurrency.AssociatedControlClientID = selectCurrency.InputClientID;
                lblCustomerPreferredShippingOption.AssociatedControlClientID = drpShipping.InputClientID;
                lblCustomerPrefferedPaymentOption.AssociatedControlClientID = drpPayment.InputClientID;
                lblCustomerCountry.AssociatedControlClientID = drpCountry.InputClientID;

                if (ECommerceSettings.ShowTaxRegistrationID(CMSContext.CurrentSite.SiteName))
                {
                    lblTaxRegistrationID.Text = GetString("Customers_Edit.lblTaxRegistrationID");
                }
                else
                {
                    plhTaxRegistrationID.Visible = false;
                }
                if (ECommerceSettings.ShowOrganizationID(CMSContext.CurrentSite.SiteName))
                {
                    lblOrganizationID.Text = GetString("Customers_Edit.lblOrganizationID");
                }
                else
                {
                    plhOrganizationID.Visible = false;
                }

                int siteId = CMSContext.CurrentSiteID;
                
                // Set site IDs
                drpPayment.SiteID = siteId;
                drpShipping.SiteID = siteId;
                selectCurrency.SiteID = siteId;
                
                if (mCustomer != null)
                {
                    // Fill editing form
                    if (!RequestHelper.IsPostBack())
                    {
                        LoadData();

                        // Show that the customer was created or updated successfully
                        if (QueryHelper.GetString("saved", String.Empty) == "1")
                        {
                            lblInfo.Visible = true;
                            lblInfo.Text = GetString("General.ChangesSaved");
                        }
                    }
                }
                else
                {
                    if (!RequestHelper.IsPostBack())
                    {
                        txtCustomerEmail.Text = CMSContext.CurrentUser.Email;
                    }

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("MyAccount.MyDetails.CreateNewCustomer");
                }
            }
            else
            {
                // Hide if user is not authenticated
                this.Visible = false;
            }
        }
    }


    /// <summary>
    /// Load form data.
    /// </summary>
    public void LoadData()
    {
        txtCustomerCompany.Text = mCustomer.CustomerCompany;
        txtCustomerEmail.Text = mCustomer.CustomerEmail;
        txtCustomerFax.Text = mCustomer.CustomerFax;
        txtCustomerFirstName.Text = mCustomer.CustomerFirstName;
        txtCustomerLastName.Text = mCustomer.CustomerLastName;
        txtCustomerPhone.Text = mCustomer.CustomerPhone;
        txtOraganizationID.Text = mCustomer.CustomerOrganizationID;
        txtTaxRegistrationID.Text = mCustomer.CustomerTaxRegistrationID;

        if (mCustomer.CustomerCountryID > 0)
        {
            drpCountry.CountryID = mCustomer.CustomerCountryID;
        }
        if (mCustomer.CustomerStateID > 0)
        {
            drpCountry.StateID = mCustomer.CustomerStateID;
        }

        string currentSiteName = CMSContext.CurrentSiteName;
        int currencyId = (Customer.CustomerUser != null) ? Customer.CustomerUser.GetUserPreferredCurrencyID(currentSiteName) : 0;
        currencyId = (currencyId > 0) ? currencyId : Customer.CustomerPreferredCurrencyID;
        if (currencyId > 0)
        {
            selectCurrency.CurrencyID = currencyId;
        }

        int paymentId = (Customer.CustomerUser != null) ? Customer.CustomerUser.GetUserPreferredPaymentOptionID(currentSiteName) : 0;
        paymentId = (paymentId > 0) ? paymentId : Customer.CustomerPreferredPaymentOptionID;
        if (paymentId > 0)
        {
            drpPayment.PaymentID = paymentId;
        }

        int shippingId = (Customer.CustomerUser != null) ? Customer.CustomerUser.GetUserPreferredShippingOptionID(currentSiteName) : 0;
        shippingId = (shippingId > 0) ? shippingId : Customer.CustomerPreferredShippingOptionID;
        if (shippingId > 0)
        {
            drpShipping.ShippingID = shippingId;
        }

        if (!DataHelper.IsEmpty(txtCustomerCompany.Text) || !DataHelper.IsEmpty(txtOraganizationID.Text) || !DataHelper.IsEmpty(txtTaxRegistrationID.Text))
        {
            chkCompanyAccount.Checked = true;
            pnlCompanyInfo.Visible = true;
        }
    }


    /// <summary>
    /// On chkCompanyAccount check box check changed event handler.
    /// </summary>
    protected void chkCompanyAccount_CheckChanged(object sender, EventArgs e)
    {
        // Displays/hides company info region
        if (chkCompanyAccount.Checked)
        {
            pnlCompanyInfo.Visible = true;
        }
        else
        {
            pnlCompanyInfo.Visible = false;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = "";
        string siteName = CMSContext.CurrentSiteName;

        if ((txtCustomerCompany.Text.Trim() == "" || !chkCompanyAccount.Checked) &&
            ((txtCustomerFirstName.Text.Trim() == "") || (txtCustomerLastName.Text.Trim() == "")))
        {
            errorMessage = GetString("Customers_Edit.errorInsert");
        }
        // Check the following items if complete company info is required for company account
        if (errorMessage == "" && ECommerceSettings.RequireCompanyInfo(siteName) && chkCompanyAccount.Checked)
        {
            errorMessage = new Validator().NotEmpty(txtCustomerCompany.Text, GetString("customers_edit.errorCompany"))
                .NotEmpty(txtOraganizationID.Text, GetString("customers_edit.errorOrganizationID"))
                .NotEmpty(txtTaxRegistrationID.Text, GetString("customers_edit.errorTaxRegID")).Result;
        }

        if (errorMessage == "")
        {
            errorMessage = new Validator().IsEmail(txtCustomerEmail.Text.Trim(), GetString("customers_edit.erroremailformat")).Result;
        }

        pnlCompanyInfo.Visible = chkCompanyAccount.Checked;

        if (errorMessage == "")
        {
            // If customer doesn't already exist, create new one
            if (mCustomer == null)
            {
                mCustomer = new CustomerInfo();
                mCustomer.CustomerEnabled = true;
                mCustomer.CustomerUserID = CMSContext.CurrentUser.UserID;
            }

            int currencyId = selectCurrency.CurrencyID;

            if (ECommerceContext.CurrentShoppingCart != null)
            {
                ECommerceContext.CurrentShoppingCart.ShoppingCartCurrencyID = currencyId;
            }

            mCustomer.CustomerEmail = txtCustomerEmail.Text.Trim();
            mCustomer.CustomerFax = txtCustomerFax.Text.Trim();
            mCustomer.CustomerLastName = txtCustomerLastName.Text.Trim();
            mCustomer.CustomerPhone = txtCustomerPhone.Text.Trim();
            mCustomer.CustomerFirstName = txtCustomerFirstName.Text.Trim();
            mCustomer.CustomerCountryID = drpCountry.CountryID;
            mCustomer.CustomerStateID = drpCountry.StateID;
            mCustomer.CustomerCreated = DateTime.Now;
            
            // Set customers's preferences
            mCustomer.CustomerPreferredCurrencyID = (currencyId > 0) ? currencyId : 0;
            mCustomer.CustomerPreferredPaymentOptionID = drpPayment.PaymentID;
            mCustomer.CustomerPreferredShippingOptionID = drpShipping.ShippingID;

            // Check if customer is registered
            if (mCustomer.CustomerIsRegistered)
            {
                // Find user-site binding
                UserSiteInfo userSite = UserSiteInfoProvider.GetUserSiteInfo(Customer.CustomerUserID, CMSContext.CurrentSiteID);
                if (userSite != null)
                {
                    // Set user's preferences
                    userSite.UserPreferredCurrencyID = mCustomer.CustomerPreferredCurrencyID;
                    userSite.UserPreferredPaymentOptionID = mCustomer.CustomerPreferredPaymentOptionID;
                    userSite.UserPreferredShippingOptionID = mCustomer.CustomerPreferredShippingOptionID;

                    UserSiteInfoProvider.SetUserSiteInfo(userSite);
                }
            }

            if (chkCompanyAccount.Checked)
            {
                mCustomer.CustomerCompany = txtCustomerCompany.Text.Trim();
                if (ECommerceSettings.ShowOrganizationID(siteName))
                {
                    mCustomer.CustomerOrganizationID = txtOraganizationID.Text.Trim();
                }
                if (ECommerceSettings.ShowTaxRegistrationID(siteName))
                {
                    mCustomer.CustomerTaxRegistrationID = txtTaxRegistrationID.Text.Trim();
                }
            }
            else
            {
                mCustomer.CustomerCompany = "";
                mCustomer.CustomerOrganizationID = "";
                mCustomer.CustomerTaxRegistrationID = "";
            }

            // Update customer data
            CustomerInfoProvider.SetCustomerInfo(mCustomer);

            // Update corresponding user email
            UserInfo user = mCustomer.CustomerUser;
            if (user != null)
            {
                user.Email = mCustomer.CustomerEmail;
                UserInfoProvider.SetUserInfo(user);
            }

            // Let others now that customer was created
            if (OnCustomerCrated != null)
            {
                OnCustomerCrated();

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
            else
            {
                URLHelper.Redirect(URLHelper.AddParameterToUrl(URLRewriter.CurrentURL, "saved", "1"));
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Raises OnShippingChange event.
    /// </summary>
    protected void drpShipping_ShippingChange(object sender, EventArgs e)
    {
        this.drpPayment.ShippingOptionID = this.drpShipping.ShippingID;
        this.drpPayment.PaymentID = 0;
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
            case "customer":
                GeneralizedInfo gi = value as GeneralizedInfo;
                if (gi != null)
                {
                    this.Customer = gi.MainObject as CustomerInfo;
                }
                break;
        }
    }
}
