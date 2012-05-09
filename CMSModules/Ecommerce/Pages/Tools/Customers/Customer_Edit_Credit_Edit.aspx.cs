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

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Credit_Edit : CMSCustomersPage
{
    private int eventid = 0;
    private int customerId = 0;
    private int creditSiteId = -1;
    private CustomerInfo customer = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Customers.Credit"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Customers.Credit");
        }

        // Get site id of credits main currency
        creditSiteId = ECommerceHelper.GetSiteID(CMSContext.CurrentSiteID, ECommerceSettings.USE_GLOBAL_CREDIT);

        string currentCreditEvent = "";
        string eventImage = "";
        string title = "";

        // Required field validator error messages initialization
        rfvEventName.ErrorMessage = GetString("Ecommerce.Customer.CreditEventNameEmpty");
        txtEventCreditChange.EmptyErrorMessage = GetString("Ecommerce.Customer.CreditChangeFormat");
        txtEventCreditChange.ValidationErrorMessage = txtEventCreditChange.EmptyErrorMessage;
        txtEventCreditChange.CurrencySiteID = creditSiteId;
        txtEventCreditChange.AllowZero = false;

        // Control initializations				
        lblEventName.Text = GetString("CreditEvent_Edit.EventNameLabel");
        lblEventDescription.Text = GetString("CreditEvent_Edit.EventDescriptionLabel");
        lblEventDate.Text = GetString("CreditEvent_Edit.EventDateLabel");
        lblEventCreditChange.Text = GetString("CreditEvent_Edit.EventCreditChangeLabel");
        btnOk.Text = GetString("General.OK");
        dtPickerEventDate.SupportFolder = "~/CMSAdminControls/Calendar";

        // Get event ID
        eventid = QueryHelper.GetInteger("eventid", 0);

        CreditEventInfo creditEventObj = null; 

        // Edit existing credit event
        if (eventid > 0)
        {
            creditEventObj = CreditEventInfoProvider.GetCreditEventInfo(eventid);
            EditedObject = creditEventObj;

            if (creditEventObj != null)
            {
                // Credit from another site
                if (creditEventObj.EventSiteID != creditSiteId)
                {
                    EditedObject = null;
                }

                currentCreditEvent = creditEventObj.EventName;
                customerId = creditEventObj.EventCustomerID;
            }

            eventImage = "creditevent.png";
            title = GetString("CreditEvent_Edit.HeaderCaption");
        }
        // Create new credit event
        else
        {
            currentCreditEvent = GetString("CreditEvent_Edit.NewItemCaption");
            if (!RequestHelper.IsPostBack())
            {
                dtPickerEventDate.SelectedDateTime = DateTime.Now;
            }
            eventImage = "newcreditevent.png";
            title = GetString("CreditEvent_Edit.NewItemCaption");

            // Get customer id from querystring
            customerId = QueryHelper.GetInteger("customerid", 0);
        }

        customer = CustomerInfoProvider.GetCustomerInfo(customerId);
        // Check if customer belongs to current site
        if (!CheckCustomerSiteID(customer))
        {
            customer = null;
        }

        // Fill editing form
        if ((customer != null) && (creditEventObj != null) && !RequestHelper.IsPostBack())
        {
            LoadData(creditEventObj);

            // Show that the creditEvent was created or updated successfully
            if (QueryHelper.GetString("saved", "") == "1")
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
        }

        // Initializes page title control		
        InitializeMasterPage(title, eventImage, currentCreditEvent);

        // Check presence of main currency
        string currencyErr = CheckMainCurrency(creditSiteId);

        if (!string.IsNullOrEmpty(currencyErr))
        {
            // Show message
            lblError.Text = currencyErr;
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// Initializes master page.
    /// </summary>
    private void InitializeMasterPage(string title, string eventImage, string currentCreditEvent)
    {
        // Set the master page title element
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "newedit_credit_event";

        // Set the tabs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("CreditEvent_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_Edit_Credit_List.aspx?customerid=" + customerId + "&siteId=" + SiteID;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = currentCreditEvent;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Load data of editing creditEvent.
    /// </summary>
    /// <param name="creditEventObj">CreditEvent object</param>
    protected void LoadData(CreditEventInfo creditEventObj)
    {
        txtEventName.Text = creditEventObj.EventName;
        txtEventDescription.Text = creditEventObj.EventDescription;
        txtEventCreditChange.Value = creditEventObj.EventCreditChange;
        dtPickerEventDate.SelectedDateTime = creditEventObj.EventDate;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (customer == null)
        {
            return;
        }

        if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
        }

        // Check if using global credit
        if (creditSiteId <= 0)
        {
            // Check Ecommerce global modify permission
            if (!ECommerceContext.IsUserAuthorizedForPermission("EcommerceGlobalModify"))
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
        }

        // Validate values
        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            CreditEventInfo creditEventObj = CreditEventInfoProvider.GetCreditEventInfo(eventid);

            // if creditEvent doesnt already exist, create new one
            if (creditEventObj == null)
            {
                creditEventObj = new CreditEventInfo();
                creditEventObj.EventCustomerID = customerId;
                creditEventObj.EventSiteID = creditSiteId;
            }

            creditEventObj.EventName = txtEventName.Text.Trim();
            creditEventObj.EventDescription = txtEventDescription.Text.Trim();
            creditEventObj.EventCreditChange = txtEventCreditChange.Value;
            creditEventObj.EventDate = dtPickerEventDate.SelectedDateTime;

            CreditEventInfoProvider.SetCreditEventInfo(creditEventObj);

            URLHelper.Redirect("Customer_Edit_Credit_Edit.aspx?customerid=" + customerId + "&eventid=" + Convert.ToString(creditEventObj.EventID) + "&saved=1&siteId=" + SiteID);
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Validates form input data and returns error message if occures.
    /// </summary>
    private string ValidateForm()
    {
        string errorMessage = "";

        // Event name must be not empty
        if (txtEventName.Text.Trim() == "")
        {
            errorMessage = rfvEventName.ErrorMessage;
        }

        if (errorMessage == "")
        {
            errorMessage = txtEventCreditChange.ValidatePrice(true);
        }

        if (errorMessage == "")
        {
            if (!dtPickerEventDate.IsValidRange())
            {
                errorMessage = GetString("general.errorinvaliddatetimerange");
            }

            // Credit event date must be selected
            if (dtPickerEventDate.SelectedDateTime == DataHelper.DATETIME_NOT_SELECTED)
            {
                errorMessage = GetString("Ecommerce.Customer.CreditDateEmpty");
            }
            // Credit event date has wrong format
            else if (ValidationHelper.GetDateTime(dtPickerEventDate.SelectedDateTime, DataHelper.DATETIME_NOT_SELECTED) == DataHelper.DATETIME_NOT_SELECTED)
            {
                errorMessage = GetString("Ecommerce.Customer.CreditDateFormat");
            }
        }

        return errorMessage;
    }
}
