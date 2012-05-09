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
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Address : CMSOrdersModalPage
{
    protected int addressId = 0;
    protected int customerId = 0;
    protected int typeId = -1;


    protected override void OnPreInit(EventArgs e)
    {
        this.CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.General");
        }

        this.CurrentMaster.Title.TitleText = GetString("Order_Edit_Address.Title");

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
        lblPersonalName.Text = GetString("Customer_Edit_Address_Edit.lblPersonalName");

        btnOk.Text = GetString("General.OK");
        btnCancel.Text = GetString("General.Cancel");

        // Get ids from querystring		
        customerId = QueryHelper.GetInteger("customerId", 0);
        addressId = QueryHelper.GetInteger("addressId", 0);
        typeId = QueryHelper.GetInteger("typeId", -1);

        if (addressId > 0)
        {
            AddressInfo addressObj = AddressInfoProvider.GetAddressInfo(addressId);
            EditedObject = addressObj;

            if (addressObj != null)
            {
                // fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(addressObj);
                }
            }
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Address/object.png");
        }

        else
        {
            if (!RequestHelper.IsPostBack())
            {
                // Init data due to customer settings
                InitData();
            }
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Address/new.png");
        }
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

        ucCountrySelector.CountryID = addressObj.AddressCountryID;
        ucCountrySelector.StateID = addressObj.AddressStateID;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyCustomers"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
        }

        if (customerId != 0)
        {
            string errorMessage = new Validator().NotEmpty(txtAddressLine1.Text, "Customer_Edit_Address_Edit.rqvLine").NotEmpty(txtAddressCity.Text, "Customer_Edit_Address_Edit.rqvCity").NotEmpty(txtAddressZip.Text, "Customer_Edit_Address_Edit.rqvZipCode").NotEmpty(txtPersonalName.Text, "Customer_Edit_Address_Edit.rqvPersonalName").Result;

            // Check country presence
            if (errorMessage == "" && (ucCountrySelector.CountryID <= 0))
            {
                errorMessage = GetString("countryselector.selectedcountryerr");
            }

            if (errorMessage == "")
            {
                AddressInfo addressObj = AddressInfoProvider.GetAddressInfo(addressId);

                // if address doesn't already exist, create new one
                if (addressObj == null)
                {
                    addressObj = new AddressInfo();
                    addressObj.AddressIsShipping = false;
                    addressObj.AddressIsBilling = false;
                    addressObj.AddressIsCompany = false;
                }

                switch (typeId)
                {
                    // Shipping addres selection
                    case 0:
                        addressObj.AddressIsShipping = true;
                        break;
                    // Billing addres selection
                    case 1:
                        addressObj.AddressIsBilling = true;
                        break;
                    // Company addres selection
                    case 2:
                        addressObj.AddressIsCompany = true;
                        break;
                    default:
                        break;
                }
                addressObj.AddressZip = txtAddressZip.Text.Trim();
                addressObj.AddressPhone = txtAddressDeliveryPhone.Text.Trim();
                addressObj.AddressPersonalName = txtPersonalName.Text.Trim();
                addressObj.AddressLine1 = txtAddressLine1.Text.Trim();
                addressObj.AddressEnabled = chkAddressEnabled.Checked;
                addressObj.AddressLine2 = txtAddressLine2.Text.Trim();
                addressObj.AddressCity = txtAddressCity.Text.Trim();                
                addressObj.AddressCountryID = ucCountrySelector.CountryID;
                addressObj.AddressStateID = ucCountrySelector.StateID;
                addressObj.AddressCustomerID = customerId;
                addressObj.AddressName = AddressInfoProvider.GetAddressName(addressObj);

                AddressInfoProvider.SetAddressInfo(addressObj);

                ltlScript.Text = ScriptHelper.GetScript("if(wopener.AddressChange!=null){wopener.AddressChange('" + addressObj.AddressID + "');}window.close();");
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
        CustomerInfo ci = CustomerInfoProvider.GetCustomerInfo(customerId);
        if (ci != null)
        {
            ucCountrySelector.CountryID = ci.CustomerCountryID;
            ucCountrySelector.StateID = ci.CustomerStateID;
            txtAddressDeliveryPhone.Text = ci.CustomerPhone;
            txtPersonalName.Text = ci.CustomerFirstName + " " + ci.CustomerLastName;
        }
    }
}
