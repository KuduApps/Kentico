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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_New : CMSCustomersPage
{
    protected int customerid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI element
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "NewCustomer"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "NewCustomer");
        }

        this.pnlGeneral.GroupingText = GetString("com.customeredit.general");
        this.pnlUserInfo.GroupingText = GetString("com.customeredit.userinfo");

        // control initializations				
        lblCustomerLastName.Text = GetString("Customers_Edit.CustomerLastNameLabel");
        lblCustomerFirstName.Text = GetString("Customers_Edit.CustomerFirstNameLabel");
        lblCustomerCompany.Text = GetString("Customers_Edit.CustomerCompanyLabel");
        lblCountry.Text = GetString("Customers_Edit.CustomerCountry");
        lblUserName.Text = GetString("Customers_Edit.UserName");
        lblPassword1.Text = GetString("Customer_Edit_Login_Edit.lblPassword1");
        lblPassword2.Text = GetString("Customer_Edit_Login_Edit.lblPassword2");
        rqvUserName.ErrorMessage = GetString("Customer_Edit_Login_Edit.rqvUserName");
        rqvPassword2.ErrorMessage = GetString("Customer_Edit_Login_Edit.rqvPassword2");
        btnOk.Text = GetString("General.OK");

        string currentCustomer = GetString("Customers_Edit.NewItemCaption");

        // Get customer id from querystring		
        customerid = QueryHelper.GetInteger("customerid", 0);
        if (customerid > 0)
        {
            CustomerInfo customerObj = CustomerInfoProvider.GetCustomerInfo(customerid);
            EditedObject = customerObj;

            if (customerObj != null)
            {
                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(customerObj);

                    // Show that the customer was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                this.drpCountry.CountryID = UniSelector.US_NONE_RECORD;
            }
        }

        InitializeMasterPage(currentCustomer);

        InitializeLoginControls();

        AddMenuButtonSelectScript("NewCustomer", "");
    }


    /// <summary>
    /// Initializes master page elements.
    /// </summary>
    private void InitializeMasterPage(string currentCustomer)
    {
        // initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Customers_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = currentCustomer;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("Customers_Edit.NewItemCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Customer/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_customer";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Load data of edited customer.
    /// </summary>
    /// <param name="customerObj">Customer object</param>
    protected void LoadData(CustomerInfo customerObj)
    {
        txtCustomerLastName.Text = customerObj.CustomerLastName;
        txtCustomerFirstName.Text = customerObj.CustomerFirstName;
        txtCustomerCompany.Text = customerObj.CustomerCompany;
        drpCountry.CountryID = customerObj.CustomerCountryID;
        drpCountry.StateID = customerObj.CustomerStateID;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
        }

        string errorMessage = "";

        if ((txtCustomerCompany.Text == null || txtCustomerCompany.Text.Trim() == "") &&
            ((txtCustomerFirstName.Text == null || txtCustomerFirstName.Text.Trim() == "") ||
             (txtCustomerLastName.Text == null || txtCustomerLastName.Text.Trim() == "")))
        {
            errorMessage = GetString("Customers_Edit.errorInsert");
        }

        if (errorMessage == "")
        {
            if (this.chkCreateLogin.Checked)
            {
                errorMessage = new Validator().NotEmpty(txtUserName.Text.Trim(), GetString("Customer_Edit_Login_Edit.rqvUserName"))
                                              .NotEmpty(passStrength.Text.Trim(), GetString("Customer_Edit_Login_Edit.rqvPassword1"))
                                              .NotEmpty(txtPassword2.Text.Trim(), GetString("Customer_Edit_Login_Edit.rqvPassword2")).Result;

                // Check policy
                if ((errorMessage == "") && !passStrength.IsValid())
                {
                    errorMessage = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                }

                // Compare passwords
                if ((errorMessage == "") && (passStrength.Text != txtPassword2.Text))
                {
                    errorMessage = GetString("Customer_Edit_Login_Edit.DifferentPasswords");
                }
            }
        }

        if (errorMessage == "")
        {
            if (this.chkCreateLogin.Checked)
            {
                // If user already has the record in the CMS_User table            
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

            // If customer doesn't already exist, create new one
            if (customerObj == null)
            {
                customerObj = new CustomerInfo();
                customerObj.CustomerSiteID = CMSContext.CurrentSiteID;
            }

            customerObj.CustomerLastName = txtCustomerLastName.Text.Trim();
            customerObj.CustomerFirstName = txtCustomerFirstName.Text.Trim();
            customerObj.CustomerCompany = txtCustomerCompany.Text.Trim();
            customerObj.CustomerCountryID = drpCountry.CountryID;
            customerObj.CustomerStateID = drpCountry.StateID;
            customerObj.CustomerEnabled = true;
            //customerObj.CustomerCreated = ucCustomerCreated.SelectedDateTime;

            using (CMSTransactionScope tr = new CMSTransactionScope())
            {
                CustomerInfoProvider.SetCustomerInfo(customerObj);

                // If create login checked
                if (this.chkCreateLogin.Checked)
                {
                    UserInfo ui = new UserInfo();
                    ui.UserName = txtUserName.Text.Trim();
                    ui.FullName = customerObj.CustomerFirstName + " " + customerObj.CustomerLastName;
                    ui.IsGlobalAdministrator = false;
                    ui.SetValue("UserEnabled", true);
                    UserInfoProvider.SetPassword(ui, passStrength.Text);

                    // Add user to current site
                    UserInfoProvider.AddUserToSite(ui.UserName, CMSContext.CurrentSiteName);
                    
                    customerObj.CustomerUserID = ui.UserID;

                    CustomerInfoProvider.SetCustomerInfo(customerObj);
                }

                // Commit transaction
                tr.Commit();
            }

            URLHelper.Redirect("Customer_Edit_Frameset.aspx?customerid=" + Convert.ToString(customerObj.CustomerID) + "&saved=1");
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }

    }


    private void InitializeLoginControls()
    {
        // Initialize login controls
        chkCreateLogin.Text = GetString("Customer_Edit_Login_Edit.lblHasLogin");
        chkCreateLogin.Attributes["onclick"] = "ShowHideLoginControls()";

        // Register script to show / hide SKU controls
        string script =
            "function ShowHideLoginControls() { \n" +
            "   checkbox = document.getElementById('" + this.chkCreateLogin.ClientID + "'); \n" +
            "   line1 = document.getElementById('loginLine1'); \n" +
            "   line2 = document.getElementById('loginLine2'); \n" +
            "   line3 = document.getElementById('loginLine3'); \n" +
            "   if ((checkbox != null) && (checkbox.checked)) { \n" +
            "       line1.style.display = '';" +
            "       line2.style.display = '';" +
            "       line3.style.display = '';" +
            "   } else { \n" +
            "       line1.style.display = 'none';" +
            "       line2.style.display = 'none';" +
            "       line3.style.display = 'none';" +
            "   } \n" +
            "} \n";

        this.ltlScript.Text += ScriptHelper.GetScript(script);

        this.ltlScript.Text += ScriptHelper.GetScript("ShowHideLoginControls()");
    }
}
