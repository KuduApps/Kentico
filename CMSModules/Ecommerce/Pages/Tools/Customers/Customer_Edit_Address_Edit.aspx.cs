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

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Address_Edit : CMSCustomersPage
{
    protected int addressId = 0;
    protected int customerId = 0;
    protected CustomerInfo customerObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Customers.Addresses"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Customers.Addresses");
        }

        //rqvAddressName.ErrorMessage = GetString("Customer_Edit_Address_Edit.rqvAddressName");
        rqvCity.ErrorMessage = GetString("Customer_Edit_Address_Edit.rqvCity");
        rqvLine.ErrorMessage = GetString("Customer_Edit_Address_Edit.rqvLine");
        rqvZipCode.ErrorMessage = GetString("Customer_Edit_Address_Edit.rqvZipCode");
        rqvPersonalName.ErrorMessage = GetString("Customer_Edit_Address_Edit.rqvPersonalName");

        // control initializations				
        lblAddressZip.Text = GetString("Customer_Edit_Address_Edit.AddressZipLabel");
        //lblAddressState.Text = GetString("Customer_Edit_Address_Edit.AddressStateIDLabel");
        lblAddressDeliveryPhone.Text = GetString("Customer_Edit_Address_Edit.AddressDeliveryPhoneLabel");
        lblAddressCountry.Text = GetString("Customer_Edit_Address_Edit.AddressCountryIDLabel");
        //lblAddressName.Text = GetString("Customer_Edit_Address_Edit.AddressNameLabel");
        //lblAddressLine3.Text = GetString("Customer_Edit_Address_Edit.AddressLine3Label");
        lblAddressLine1.Text = GetString("Customer_Edit_Address_Edit.AddressLine1Label");
        //lblAddressLine2.Text = GetString("Customer_Edit_Address_Edit.AddressLine2Label");
        lblAddressCity.Text = GetString("Customer_Edit_Address_Edit.AddressCityLabel");
        //lblAddressType.Text = GetString("Customer_Edit_Address_Edit.AddressTypeLabel");
        lblPersonalName.Text = GetString("Customer_Edit_Address_Edit.lblPersonalName");
        lblAddressIsBilling.Text = GetString("Customer_Edit_Address_Edit.lblAddressIsBilling");
        lblAddressIsShipping.Text = GetString("Customer_Edit_Address_Edit.lblAddressIsShipping");
        lblAddressIsCompany.Text = GetString("Customer_Edit_Address_Edit.lblAddressIsCompany");

        btnOk.Text = GetString("General.OK");

        string currentAddress = GetString("Customer_Edit_Address_Edit.NewItemCaption");

        // Get address id from querystring		
        customerId = QueryHelper.GetInteger("customerId", 0);
        customerObj = CustomerInfoProvider.GetCustomerInfo(customerId);
        addressId = QueryHelper.GetInteger("addressId", 0);

        if (addressId > 0)
        {
            AddressInfo addressObj = AddressInfoProvider.GetAddressInfo(addressId);
            EditedObject = addressObj;

            if (addressObj != null)
            {
                currentAddress = addressObj.AddressName;

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(addressObj);

                    // Show that the address was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }

            string titleImage = GetImageUrl("Objects/Ecommerce_Address/object.png");
            string titleText = GetString("Customer_Edit_Address_Edit.HeaderCaption");

            InitializeMasterPage(titleImage, titleText, currentAddress);
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                // Init data due to customer settings
                InitData();
            }

            string titleImage = GetImageUrl("Objects/Ecommerce_Address/new.png");
            string titleText = GetString("Customer_Edit_Address_New.HeaderCaption");

            InitializeMasterPage(titleImage, titleText, currentAddress);
        }
    }


    private void InitializeMasterPage(string titleImage, string titleText, string currentAddress)
    {
        // Initialize the title master page element
        //this.CurrentMaster.Title.TitleImage = titleImage;
        //this.CurrentMaster.Title.TitleText = titleText;
        this.CurrentMaster.Title.HelpTopicName = "newedit_address";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Customer_Edit_Address_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_Edit_Address_List.aspx?customerId=" + customerId;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = currentAddress;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }

    void drpAddressType_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Load data of editing address.
    /// </summary>
    /// <param name="addressObj">Address object</param>
    protected void LoadData(AddressInfo addressObj)
    {
        txtAddressZip.Text = addressObj.AddressZip;
        txtAddressDeliveryPhone.Text = addressObj.AddressPhone;
        txtPersonalName.Text = addressObj.AddressPersonalName;
        //txtAddressName.Text = addressObj.AddressName;
        txtAddressLine1.Text = addressObj.AddressLine1;
        chkAddressEnabled.Checked = addressObj.AddressEnabled;
        txtAddressLine2.Text = addressObj.AddressLine2;
        txtAddressCity.Text = addressObj.AddressCity;

        //drpAddressType.SelectedValue = AddressInfoProvider.GetAddressString(addressObj.AddressType);
        chkAddressIsBilling.Checked = addressObj.AddressIsBilling;
        chkAddressIsShipping.Checked = addressObj.AddressIsShipping;
        chkAddressIsCompany.Checked = addressObj.AddressIsCompany;
        ucCountrySelector.CountryID = addressObj.AddressCountryID;
        ucCountrySelector.StateID = addressObj.AddressStateID;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (customerObj == null)
        {
            return;
        }

        if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
        }

        if (customerId != 0)
        {
            string errorMessage = new Validator().NotEmpty(txtAddressLine1.Text, "Customer_Edit_Address_Edit.rqvLine")
                                                 .NotEmpty(txtAddressCity.Text, "Customer_Edit_Address_Edit.rqvCity")
                                                 .NotEmpty(txtAddressZip.Text, "Customer_Edit_Address_Edit.rqvZipCode")
                                                 .NotEmpty(txtPersonalName.Text, "Customer_Edit_Address_Edit.rqvPersonalName").Result;

            // Check country presence
            if (errorMessage == "" && (ucCountrySelector.CountryID <= 0))
            {
                errorMessage = GetString("countryselector.selectedcountryerr");
            }

            if (errorMessage == "")
            {
                // Get object
                AddressInfo addressObj = AddressInfoProvider.GetAddressInfo(addressId);
                if (addressObj == null)
                {
                    addressObj = new AddressInfo();
                }

                addressObj.AddressIsBilling = chkAddressIsBilling.Checked;
                addressObj.AddressIsShipping = chkAddressIsShipping.Checked;
                addressObj.AddressZip = txtAddressZip.Text.Trim();
                addressObj.AddressPhone = txtAddressDeliveryPhone.Text.Trim();
                addressObj.AddressPersonalName = txtPersonalName.Text.Trim();
                addressObj.AddressLine1 = txtAddressLine1.Text.Trim();
                addressObj.AddressEnabled = chkAddressEnabled.Checked;
                addressObj.AddressLine2 = txtAddressLine2.Text.Trim();
                addressObj.AddressCity = txtAddressCity.Text.Trim();
                addressObj.AddressCountryID = ucCountrySelector.CountryID;
                addressObj.AddressStateID = ucCountrySelector.StateID;
                addressObj.AddressIsCompany = chkAddressIsCompany.Checked;
                addressObj.AddressName = AddressInfoProvider.GetAddressName(addressObj);
                addressObj.AddressCustomerID = customerId;

                AddressInfoProvider.SetAddressInfo(addressObj);

                URLHelper.Redirect("Customer_Edit_Address_Edit.aspx?customerId=" + customerId + "&addressId=" + Convert.ToString(addressObj.AddressID) + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = errorMessage;
            }
        }
    }


    private void InitData()
    {
        if (customerObj != null)
        {
            ucCountrySelector.CountryID = customerObj.CustomerCountryID;
            ucCountrySelector.StateID = customerObj.CustomerStateID;
            txtAddressDeliveryPhone.Text = customerObj.CustomerPhone;
            txtPersonalName.Text = customerObj.CustomerFirstName + " " + customerObj.CustomerLastName;
            txtPersonalName.Text = txtPersonalName.Text.Trim();
            if ((txtPersonalName.Text == "") && (customerObj.CustomerCompany != ""))
            {
                txtPersonalName.Text += customerObj.CustomerCompany;
            }
        }
    }
}
