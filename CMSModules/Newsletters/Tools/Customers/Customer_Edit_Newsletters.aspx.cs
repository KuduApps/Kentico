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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

[Security(Resource = "CMS.Desk", UIElements = "Ecommerce")]
[Security(Resource = "CMS.Ecommerce", UIElements = "CustomersGroup;Customers;Customers.Newsletters")]
public partial class CMSModules_Newsletters_Tools_Customers_Customer_Edit_Newsletters : CMSDeskPage
{
    private int siteId = 0;
    private int customerSiteId = -1;
    private string currentValues = string.Empty;
    private string email = null;
    private string firstName = null;
    private string lastName = null;
    private int customerUserId = -1;

    /// <summary>
    /// On page load.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Newsletters);
        }

        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Newsletter", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Newsletter");
        }

        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Ecommerce", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Ecommerce");
        }

        siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        if (!URLHelper.IsPostback())
        {
            siteSelector.SiteID = CMSContext.CurrentSiteID;
        }

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check 'NewsletterRead' permission
        if (!user.IsAuthorizedPerResource("CMS.Newsletter", "Read"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Newsletter", "Read");
        }

        lblTitle.Text = GetString("Customer_Edit_Newsletters.Title");

        // Load customer data
        GeneralizedInfo customerObj = ModuleCommands.ECommerceGetCustomerInfo(QueryHelper.GetInteger("customerId", 0));
        if (customerObj != null)
        {
            email = Convert.ToString(customerObj.GetValue("CustomerEmail"));
            firstName = Convert.ToString(customerObj.GetValue("CustomerFirstName"));
            lastName = Convert.ToString(customerObj.GetValue("CustomerLastName"));
            customerUserId = ValidationHelper.GetInteger(customerObj.GetValue("CustomerUserID"), -1);

            object customerSiteIdObj = customerObj.GetValue("CustomerSiteID");
            customerSiteId = ValidationHelper.GetInteger((customerSiteIdObj == null) ? 0 : customerSiteIdObj, CMSContext.CurrentSiteID);
        }

        if ((email == null) || (email.Trim() == string.Empty) || (!ValidationHelper.IsEmail(email)))
        {
            lblTitle.Visible = false;
            lblInfo.Visible = true;
            lblInfo.Text = GetString("ecommerce.customer.invalidemail");
            usNewsletters.Visible = false;
        }

        usNewsletters.ButtonRemoveSelected.CssClass = "XLongButton";
        usNewsletters.ButtonAddItems.CssClass = "XLongButton";
        usNewsletters.OnSelectionChanged += new EventHandler(usNewsletters_OnSelectionChanged);

        SetWhereCondition();

        LoadSelection(false);
    }


    void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SetWhereCondition();

        LoadSelection(true);
        usNewsletters.Reload(true);
    }


    private void usNewsletters_OnSelectionChanged(object sender, EventArgs e)
    {
        // Check 'EcommerceModify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "EcommerceModify"))
        {
            // Check 'ModifyCustomers' permission if don't have general ecommerce modify permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ModifyCustomers"))
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
            }
        }

        // Check if a customer has a valid email 
        if ((email != null) && (email.Trim() != string.Empty) && (ValidationHelper.IsEmail(email)))
        {
            // Check whether subcriber already exist
            Subscriber sb = SubscriberProvider.GetSubscriber(email, siteId);
            if (sb == null)
            {
                // Create new subscriber
                sb = new Subscriber();
                sb.SubscriberEmail = email;
                sb.SubscriberFirstName = firstName;
                sb.SubscriberLastName = lastName;
                sb.SubscriberFullName = (firstName + " " + lastName).Trim();
                sb.SubscriberSiteID = siteId;
                sb.SubscriberGUID = Guid.NewGuid();
                SubscriberProvider.SetSubscriber(sb);
            }

            // Remove old items
            string newValues = ValidationHelper.GetString(usNewsletters.Value, null);
            string items = DataHelper.GetNewItemsInList(newValues, currentValues);
            if (!String.IsNullOrEmpty(items))
            {
                string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (newItems != null)
                {
                    foreach (string item in newItems)
                    {
                        int newsletterId = ValidationHelper.GetInteger(item, 0);

                        // If subscriber is subscribed, unsubscribe him
                        if (SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId))
                        {
                            SubscriberProvider.Unsubscribe(sb.SubscriberID, newsletterId);
                        }
                    }
                }
            }

            // Add new items
            items = DataHelper.GetNewItemsInList(currentValues, newValues);
            if (!String.IsNullOrEmpty(items))
            {
                string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (newItems != null)
                {
                    foreach (string item in newItems)
                    {
                        int newsletterId = ValidationHelper.GetInteger(item, 0);

                        // If subscriber is not subscribed, subscribe him
                        if (!SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId))
                        {
                            SubscriberProvider.Subscribe(sb.SubscriberID, newsletterId, DateTime.Now);
                        }
                    }
                }
            }

            // Display information about successful (un)subscription
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }


    private void LoadSelection(bool force)
    {
        currentValues = string.Empty;

        Subscriber sb = SubscriberProvider.GetSubscriber(email, siteId);
        if (sb != null)
        {
            // Get selected newsletters
            DataSet ds = SubscriberNewsletterInfoProvider.GetSubscriberNewsletters(sb.SubscriberID, null, -1, "NewsletterID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "NewsletterID"));
            }

            if (!RequestHelper.IsPostBack() || force)
            {
                // Load selected newsletters
                usNewsletters.Value = currentValues;
            }
        }
    }


    private void SetWhereCondition()
    {
        // Working with registered customer
        if (customerUserId > 0)
        {
            // Show site selector
            CurrentMaster.DisplaySiteSelectorPanel = true;

            // Show site selector for registered customer
            pnlSiteSelector.Visible = true;
            siteSelector.UserId = customerUserId;
            siteId = siteSelector.SiteID;
            // Show only selected site newsletters for registered customer
            if (siteId > 0)
            {
                usNewsletters.WhereCondition = "NewsletterSiteID = " + siteId;
            }
            // When "all sites" selected
            else
            {
                usNewsletters.WhereCondition = "NewsletterSiteID IN (SELECT SiteID FROM CMS_UserSite WHERE UserID = " + customerUserId + ")";
            }
        }
        else
        {
            siteId = CMSContext.CurrentSiteID;
            usNewsletters.WhereCondition = "NewsletterSiteID = " + customerSiteId;
        }

        usNewsletters.Enabled = siteId > 0;
    }
}