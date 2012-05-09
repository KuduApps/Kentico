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

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.DataEngine;

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_General : CMSCustomersPage
{
    private int customerid = 0;
    private int currentSiteId = -1;
    private CustomerInfo customerObj = null;
    private bool allowGlobalDiscountLevels = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Customers.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Customers.General");
        }

        // Figure out if site allows using global discount levels
        allowGlobalDiscountLevels = ECommerceSettings.AllowGlobalDiscountLevels(CMSContext.CurrentSiteName);

        this.pnlGeneral.GroupingText = GetString("com.customeredit.general");
        this.pnlContacts.GroupingText = GetString("com.customeredit.contacts");
        this.pnlUserInfo.GroupingText = GetString("com.customeredit.userinfo");

        chkHasLogin.Text = GetString("Customer_Edit_Login_Edit.lblHasLogin");
        rqvUserName.ErrorMessage = GetString("Customer_Edit_Login_Edit.rqvUserName");
        rqvPassword2.ErrorMessage = GetString("Customer_Edit_Login_Edit.rqvPassword2");

        // Control initializations				
        lblPassword1.Text = GetString("Customer_Edit_Login_Edit.lblPassword1");
        lblPassword2.Text = GetString("Customer_Edit_Login_Edit.lblPassword2");

        btnEditUser.Text = GetString("general.edit");

        // Init controls
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
        lblTaxRegistrationID.Text = GetString("Customers_Edit.lblTaxRegistrationID");
        lblOrganizationID.Text = GetString("Customers_Edit.lblOrganizationID");
        lblCustomerGlobalDiscountLevel.Text = GetString("Customers_Edit.lblCustomerGlobalDiscountLevel");
        lblCustomerDiscountLevel.Text = GetString("Customers_Edit.lblCustomerDiscountLevel");

        string emptyRecordText = GetString(allowGlobalDiscountLevels ? "general.UseGlobal" : "general.empty");
        drpDiscountLevel.SpecialFields = new string[,] { { emptyRecordText, "0" } };

        // Init current site id
        currentSiteId = CMSContext.CurrentSiteID;

        customerid = QueryHelper.GetInteger("customerid", 0);
        if (customerid > 0)
        {
            customerObj = CustomerInfoProvider.GetCustomerInfo(customerid);
            // Check if customer belongs to current site
            if (!CheckCustomerSiteID(customerObj))
            {
                customerObj = null;
            }

            EditedObject = customerObj;

            if (customerObj != null)
            {
                // Display site discount level only for reistered customers
                plcSiteDiscount.Visible = customerObj.CustomerIsRegistered;
                if (customerObj.CustomerIsRegistered)
                {
                    UserInfo ui = UserInfoProvider.GetUserInfo(customerObj.CustomerUserID);
                    if (ui != null)
                    {
                        pnlEdit.Visible = false;
                        pnlStatic.Visible = true;
                        lblUserNameStaticValue.Text = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(ui.UserName));
                        btnEditUser.OnClientClick = "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Membership/Pages/Users/User_Edit_Frameset.aspx") + "?userid=" + customerObj.CustomerUserID + "&siteId=" + ConfiguredSiteID + "', 'UserEdit', 950, 700); return false;";
                    }

                    // Hide global discount level selector when global levels not allowed
                    plcGlobalDiscount.Visible = allowGlobalDiscountLevels;
                }
                else
                {
                    plcDiscounts.Visible = false;
                    plcPreferences.Visible = false;
                    pnlEdit.Visible = true;
                    pnlStatic.Visible = false;
                }

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    // Fill form
                    txtCustomerCompany.Text = customerObj.CustomerCompany;
                    txtCustomerEmail.Text = customerObj.CustomerEmail;
                    txtCustomerFax.Text = customerObj.CustomerFax;
                    txtCustomerFirstName.Text = customerObj.CustomerFirstName;
                    txtCustomerLastName.Text = customerObj.CustomerLastName;
                    txtCustomerPhone.Text = customerObj.CustomerPhone;
                    txtOraganizationID.Text = customerObj.CustomerOrganizationID;
                    txtTaxRegistrationID.Text = customerObj.CustomerTaxRegistrationID;
                    chkCustomerEnabled.Checked = customerObj.CustomerEnabled;

                    if (customerObj.CustomerCountryID > 0)
                    {
                        drpCountry.CountryID = customerObj.CustomerCountryID;
                    }
                    if (customerObj.CustomerStateID > 0)
                    {
                        drpCountry.StateID = customerObj.CustomerStateID;
                    }

                    // show that the customer was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");

                        int hideBreadcrumbs = QueryHelper.GetInteger("hidebreadcrumbs", 0);
                        if (hideBreadcrumbs > 0)
                        {
                            // Refresh the parent page when called from Orders section
                            string javaScript = @"if(window.parent.wopener != null)
                                                  {{
                                                        window.parent.wopener.location.replace(window.parent.wopener.location);
                                                  }}";
                            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "RefreshParentScript", ScriptHelper.GetScript(javaScript));
                        }
                    }
                }
            }

            LoadDataSelectors(customerObj);
        }

        chkHasLogin.Attributes["onclick"] = "ShowHideLoginControls()";

        // Register script to show / hide SKU controls
        string script = @"
function ShowHideLoginControls() {{ 
   checkbox = document.getElementById('" + this.chkHasLogin.ClientID + @"');
   line1 = document.getElementById('loginLine1');
   line2 = document.getElementById('loginLine2');
   line3 = document.getElementById('loginLine3');
   if ((checkbox != null) && (checkbox.checked)) {{
       line1.style.display = '';
       line2.style.display = '';
       line3.style.display = '';
   }} else {{
       line1.style.display = 'none';
       line2.style.display = 'none';
       line3.style.display = 'none';
   }}
}}";

        this.ltlScript.Text = ScriptHelper.GetScript(script);

        if (pnlEdit.Visible)
        {
            this.ltlScript.Text += ScriptHelper.GetScript("ShowHideLoginControls()");
        }

        if (QueryHelper.GetBoolean("refreshHeader", false))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("if (window.parent.frames['CustomerHeader'] != null) {");
            sb.Append("var loc = window.parent.frames['CustomerHeader'].location; if(!(/onlyRefresh=1/.test(loc))) {loc += '&onlyRefresh=1';} window.parent.frames['CustomerHeader'].location.replace(loc);");
            sb.Append("}");
            ScriptHelper.RegisterStartupScript(this, typeof(string), "RefreshHeaderScript", sb.ToString(), true);
        }
    }


    /// <summary>
    /// Load data.
    /// </summary>
    public void LoadDataSelectors(CustomerInfo customerObj)
    {
        // Set site id of the edited object to selectors
        drpCurrency.SiteID = currentSiteId;
        drpPayment.SiteID = currentSiteId;
        drpShipping.SiteID = currentSiteId;
        drpGlobalDiscountLevel.SiteID = 0;
        drpDiscountLevel.SiteID = currentSiteId;

        if (!URLHelper.IsPostback())
        {
            int currencyId = (customerObj.CustomerUser != null) ? customerObj.CustomerUser.GetUserPreferredCurrencyID(CurrentSiteName) : 0;
            currencyId = (currencyId > 0) ? currencyId : customerObj.CustomerPreferredCurrencyID;
            if (currencyId > 0)
            {
                drpCurrency.CurrencyID = currencyId;
            }

            int paymentId = (customerObj.CustomerUser != null) ? customerObj.CustomerUser.GetUserPreferredPaymentOptionID(CurrentSiteName) : 0;
            paymentId = (paymentId > 0) ? paymentId : customerObj.CustomerPreferredPaymentOptionID;
            if (paymentId > 0)
            {
                drpPayment.PaymentID = paymentId;
            }

            int shippingId = (customerObj.CustomerUser != null) ? customerObj.CustomerUser.GetUserPreferredShippingOptionID(CurrentSiteName) : 0;
            shippingId = (shippingId > 0) ? shippingId : customerObj.CustomerPreferredShippingOptionID;
            if (shippingId > 0)
            {
                drpShipping.ShippingID = shippingId;
            }

            if (customerObj.CustomerDiscountLevelID > 0)
            {
                drpGlobalDiscountLevel.DiscountLevel = customerObj.CustomerDiscountLevelID;
            }
            int siteDiscountId = (customerObj.CustomerUser != null) ? customerObj.CustomerUser.GetUserDiscountLevelID(CurrentSiteName) : 0;
            if (siteDiscountId > 0)
            {
                drpDiscountLevel.DiscountLevel = siteDiscountId;
            }
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
        }

        string errorMessage = "";

        if ((txtCustomerCompany.Text.Trim() == "") &&
            ((txtCustomerFirstName.Text.Trim() == "") || (txtCustomerLastName.Text.Trim() == "")))
        {
            errorMessage = GetString("Customers_Edit.errorInsert");
        }
        else if (ECommerceSettings.RequireCompanyInfo(CMSContext.CurrentSite.SiteName) && (txtCustomerCompany.Text.Trim() != "" || txtOraganizationID.Text.Trim() != "" || txtTaxRegistrationID.Text.Trim() != ""))
        {
            errorMessage = new Validator().NotEmpty(txtCustomerCompany.Text.Trim(), GetString("customers_edit.errorcompany"))
                .NotEmpty(txtOraganizationID.Text.Trim(), GetString("customers_edit.errororganizationid"))
                .NotEmpty(txtTaxRegistrationID.Text.Trim(), GetString("customers_edit.errortaxregid")).Result;
        }
        else if ((txtCustomerEmail.Text.Trim() != "") && !ValidationHelper.IsEmail(txtCustomerEmail.Text))
        {
            errorMessage = GetString("Customers_Edit.errorEmail");
        }

        if (chkHasLogin.Checked)
        {
            if (errorMessage == "")
            {
                errorMessage = new Validator().NotEmpty(txtUserName.Text.Trim(), GetString("Customer_Edit_Login_Edit.rqvUserName"))
                    .NotEmpty(passStrength.Text, GetString("Customer_Edit_Login_Edit.rqvPassword1"))
                    .NotEmpty(txtPassword2.Text, GetString("Customer_Edit_Login_Edit.rqvPassword2")).Result;
            }

            if ((errorMessage == "") && (passStrength.Text != txtPassword2.Text))
            {
                errorMessage = GetString("Customer_Edit_Login_Edit.DifferentPasswords");
            }

            // Check policy
            if ((errorMessage == "") && !passStrength.IsValid())
            {
                errorMessage = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
            }

            // Check if user name is unique
            if (errorMessage == "")
            {
                UserInfo existingUser = UserInfoProvider.GetUserInfo(txtUserName.Text.Trim());
                if (existingUser != null)
                {
                    errorMessage = GetString("Customer_Edit_Login_Edit.UserExist");
                }
            }
        }

        if (errorMessage == "")
        {
            CustomerInfo customerObj = CustomerInfoProvider.GetCustomerInfo(customerid);

            // If customer does not already exist, create new one
            if (customerObj == null)
            {
                customerObj = new CustomerInfo();
                customerObj.CustomerSiteID = currentSiteId;
                customerObj.CustomerEnabled = true;
            }

            customerObj.CustomerEmail = txtCustomerEmail.Text.Trim();
            customerObj.CustomerFax = txtCustomerFax.Text.Trim();
            customerObj.CustomerLastName = txtCustomerLastName.Text.Trim();
            customerObj.CustomerPhone = txtCustomerPhone.Text.Trim();
            customerObj.CustomerFirstName = txtCustomerFirstName.Text.Trim();
            customerObj.CustomerCompany = txtCustomerCompany.Text.Trim();
            customerObj.CustomerCountryID = drpCountry.CountryID;
            customerObj.CustomerStateID = drpCountry.StateID;
            customerObj.CustomerOrganizationID = txtOraganizationID.Text.Trim();
            customerObj.CustomerTaxRegistrationID = txtTaxRegistrationID.Text.Trim();

            // Set customer's preferences
            customerObj.CustomerPreferredCurrencyID = drpCurrency.CurrencyID;
            customerObj.CustomerPreferredPaymentOptionID = drpPayment.PaymentID;
            customerObj.CustomerPreferredShippingOptionID = drpShipping.ShippingID;

            if (plcDiscounts.Visible && plcGlobalDiscount.Visible)
            {
                customerObj.CustomerDiscountLevelID = drpGlobalDiscountLevel.DiscountLevel;
            }

            // Only registered customer can be enabled/diabled
            if (customerObj.CustomerIsRegistered)
            {
                customerObj.CustomerEnabled = chkCustomerEnabled.Checked;
            }

            bool refreshHeader = true;

            using (CMSTransactionScope tr = new CMSTransactionScope())
            {
                // Create user for customer
                if (chkHasLogin.Checked)
                {
                    UserInfo ui = new UserInfo();
                    ui.UserName = txtUserName.Text.Trim();
                    ui.FullName = customerObj.CustomerFirstName + " " + customerObj.CustomerLastName;
                    ui.IsGlobalAdministrator = false;
                    ui.UserEnabled = true;

                    UserInfoProvider.SetPassword(ui, passStrength.Text);
                    UserInfoProvider.AddUserToSite(ui.UserName, CMSContext.CurrentSiteName);

                    customerObj.CustomerEnabled = true;
                    customerObj.CustomerUserID = ui.UserID;

                    refreshHeader = true;
                }

                // Save customer
                CustomerInfoProvider.SetCustomerInfo(customerObj);

                // Enable/disable coresponding registered user
                if (customerObj.CustomerIsRegistered && !chkHasLogin.Checked)
                {
                    UserInfo ui = UserInfoProvider.GetUserInfo(customerObj.CustomerUserID);

                    // If the customer already has the record in the CMS_User table, update email
                    if (ui != null)
                    {
                        ui.Email = customerObj.CustomerEmail;
                        UserInfoProvider.SetUserInfo(ui);
                    }

                    // Save site specific values
                    UserSiteInfo userSite = UserSiteInfoProvider.GetUserSiteInfo(customerObj.CustomerUserID, CMSContext.CurrentSiteID);
                    if (userSite != null)
                    {
                        userSite.UserPreferredCurrencyID = drpCurrency.CurrencyID;
                        userSite.UserPreferredPaymentOptionID = drpPayment.PaymentID;
                        userSite.UserPreferredShippingOptionID = drpShipping.ShippingID;
                        userSite.UserDiscountLevelID = drpDiscountLevel.DiscountLevel;

                        UserSiteInfoProvider.SetUserSiteInfo(userSite);
                    }
                }

                // Commit transaction
                tr.Commit();
            }

            URLHelper.Redirect("Customer_Edit_General.aspx?customerid=" + Convert.ToString(customerObj.CustomerID) + "&saved=1&hidebreadcrumbs=" + QueryHelper.GetInteger("hidebreadcrumbs", 0) + "&siteId=" + SiteID + (refreshHeader ? "&refreshHeader=1" : ""));
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
