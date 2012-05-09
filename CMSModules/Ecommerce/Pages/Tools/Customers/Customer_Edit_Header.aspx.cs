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
using CMS.FormEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Header : CMSCustomersPage
{
    private int customerId = 0;
    private bool hideBreadcrumbs = false;
    private CustomerInfo customerInfoObj = null;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Change master page for dialog
        hideBreadcrumbs = QueryHelper.GetInteger("hidebreadcrumbs", 0) > 0;
        if (hideBreadcrumbs)
        {
            this.MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentCustomer = "";
        customerId = QueryHelper.GetInteger("customerid", 0);
        if (customerId > 0)
        {
            customerInfoObj = CustomerInfoProvider.GetCustomerInfo(customerId);

            // Prepare customer name string
            if (customerInfoObj != null)
            {
                if (ValidationHelper.GetString(customerInfoObj.CustomerCompany, "") != "")
                {
                    currentCustomer = customerInfoObj.CustomerCompany;
                }
                else
                {
                    currentCustomer = customerInfoObj.CustomerLastName + " " + customerInfoObj.CustomerFirstName;
                }
            }
        }

        // Initialize breadcrumbs when visible
        if (!hideBreadcrumbs)
        {
            InitializeBreadcrumbs(currentCustomer);
        }

        // Ensure page with changes saved message is loaded initially if required
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            this.CurrentMaster.Tabs.StartPageURL = "Customer_Edit_General.aspx" + URLHelper.Url.Query;
        }

        InitializeMasterPage(customerId);

        AddMenuButtonSelectScript("Customers", "");
    }


    /// <summary>
    /// Initializes master page elements.
    /// </summary>
    private void InitializeMasterPage(int customerId)
    {
        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        // Set the HELP element
        master.Title.HelpTopicName = "general_tab14";
        master.Title.HelpName = "helpTopic";

        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
        master.Tabs.UrlTarget = "CustomerContent";
        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Customers";
        master.Tabs.OpenTabContentAfterLoad = !QueryHelper.GetBoolean("onlyRefresh", false);

        // Set master title
        master.Title.TitleText = GetString("com.customer.edit");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Customer/object.png");
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        switch (element.ElementName.ToLower())
        {
            case "customers.customfields":

                // Check if customer has any custom fields
                FormInfo formInfo = FormHelper.GetFormInfo("ecommerce.customer", false);
                if (formInfo.GetFormElements(true, false, true).Count <= 0)
                {
                    return null;
                }
                break;

            case "customers.newsletters":
                if (!ModuleEntry.IsModuleLoaded(ModuleEntry.NEWSLETTER))
                {
                    return null;
                }
                break;

            case "customers.general":
                // Add hidebreadcrumbs query parameter for the parent refreshing when customer section is opened from orders section
                parameters[2] += "&hidebreadcrumbs=1";
                break;

            case "customers.credit":
                // Hide Credit tab for anonymous customer
                if ((customerInfoObj == null) || !customerInfoObj.CustomerIsRegistered)
                {
                    return null;
                }
                break;
        }

        return parameters;
    }


    /// <summary>
    /// Initializes the breadcrumb mastre page element.
    /// </summary>
    private void InitializeBreadcrumbs(string currentCustomer)
    {
        // initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Customer_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_List.aspx";
        pageTitleTabs[0, 2] = "ecommerceContent";
        pageTitleTabs[1, 0] = currentCustomer;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }
}
