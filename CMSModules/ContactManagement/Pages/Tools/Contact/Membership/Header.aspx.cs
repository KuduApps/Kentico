using System;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactid")]

// Title
[Title("", "", "onlinemarketing_contact_membershipusers")]

// Breadcrumbs
[Breadcrumbs(1)]
[Breadcrumb(0, "om.membership.list")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Header : CMSContactManagementContactsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get contact info object
        ContactInfo ci = (ContactInfo)EditedObject;
        if (ci == null)
        {
            return;
        }

        // Check permisssions
        this.CheckReadPermission(ci.ContactSiteID);

        // Check if running under site manager
        string siteManagerParam = "";
        if (this.IsSiteManager)
        {
            siteManagerParam = "&issitemanager=1";
        }

        int counter = 0;
        int contactId = ci.ContactID;

        // Initialize tabs
        this.InitTabs(3, "membershipContent");
        this.SetTab(counter++, GetString("om.membership.userslist"), "Users.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_membershipusers');");
        if (ModuleEntry.IsModuleLoaded(ModuleEntry.ECOMMERCE))
        {
            this.SetTab(counter++, GetString("om.membership.customerslist"), "Customers.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_membershipcustomers');");
        }
        if (ModuleEntry.IsModuleLoaded(ModuleEntry.NEWSLETTER))
        {
            this.SetTab(counter++, GetString("om.membership.subscriberslist"), "Subscribers.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_membershipsubscribers');");
        }
    }
}
