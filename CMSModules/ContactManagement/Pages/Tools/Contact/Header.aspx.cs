using System;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.UIControls;
using CMS.WebAnalytics;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactid")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Header : CMSContactManagementContactsPage
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Modify header master page in modal dialog
        if (QueryHelper.GetBoolean("dialogmode", false))
        {
            Page.MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }
        else
        {
            Page.MasterPageFile = "~/CMSMasterPages/UI/TabsHeader.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Contacts", null, "menu");

        // Get current user info
        CurrentUserInfo user = CMSContext.CurrentUser;

        // Get contact info object
        ContactInfo ci = (ContactInfo)EditedObject;
        if (ci == null)
        {
            return;
        }

        // Check permission read
        ContactHelper.AuthorizedReadContact(ci.ContactSiteID, true);

        // Check if running under site manager (and distribute "site manager" flag to other tabs)
        string siteManagerParam = string.Empty;
        if (this.IsSiteManager)
        {
            siteManagerParam = "&issitemanager=1";

            // Hide title
            CurrentMaster.Title.TitleText = CurrentMaster.Title.TitleImage = string.Empty;
        }
        else
        {
            // Set title
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/OM_Contact/object.png");
            CurrentMaster.Title.TitleText = GetString("om.contact.edit");
        }

        // Set default help topic
        SetHelp("onlinemarketing_contact_general", "helpTopic");

        string append = null;
        if (ci.ContactMergedWithContactID != 0)
        {
            // Append '(merged)' behind contact name in breadcrumbs
            append = " " + GetString("om.contact.mergedsuffix");
        }
        else if (ci.ContactSiteID == 0)
        {
            // Append '(global)' behind contact name in breadcrumbs
            append = " " + GetString("om.contact.globalsuffix");
        }

        // Modify header appearance in modal dialog (display title instead of breadcrumbs)
        if (QueryHelper.GetBoolean("dialogmode", false))
        {
            CurrentMaster.Title.TitleText = GetString("om.contact.edit") + " - " + HTMLHelper.HTMLEncode(ContactInfoProvider.GetContactFullName(ci)) + append;
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/OM_Contact/object.png");
        }
        else
        {
            // Get url for breadcrumbs
            string url = ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/Contact/List.aspx");
            url = URLHelper.AddParameterToUrl(url, "siteid", this.SiteID.ToString());
            if (this.IsSiteManager)
            {
                url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
            }

            CurrentPage.InitBreadcrumbs(2);
            CurrentPage.SetBreadcrumb(0, GetString("om.contact.list"), url, "_parent", null);
            CurrentPage.SetBreadcrumb(1, ContactInfoProvider.GetContactFullName(ci) + append, null, null, null);
        }

        // Check if contact has any custom fields
        int i = 0;

        FormInfo formInfo = FormHelper.GetFormInfo(ci.Generalized.DataClass.ClassName, false);
        if (formInfo.GetFormElements(true, false, true).Count > 0)
        {
            i = 1;
        }

        int contactId = ci.ContactID;
        bool ipTabAvailable = ActivitySettingsHelper.IPLoggingEnabled(CMSContext.CurrentSiteName) || ContactHelper.IsSiteManager;
        int counter = 0;

        // Initialize tabs
        this.InitTabs(7 + (ipTabAvailable ? 1 : 0) + i, "content");

        this.SetTab(counter++, GetString("general.general"), "Tab_General.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_general');");

        if (i > 0)
        {
            // Add tab for custom fields
            this.SetTab(counter++, GetString("general.customfields"), "Tab_CustomFields.aspx?contactid=" + ci.ContactID + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_customfields');");
        }

        if (AccountHelper.AuthorizedReadAccount(ci.ContactSiteID, false) || ContactHelper.AuthorizedReadContact(ci.ContactSiteID, false))
        {
            this.SetTab(counter++, GetString("om.account.list"), "Tab_Accounts.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_accounts');");
        }

        this.SetTab(counter++, GetString("om.membership.list"), "Membership/Tab_Membership.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_membership');");

        this.SetTab(counter++, GetString("om.activity.list"), "Tab_Activities.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_activities');");

        if (ipTabAvailable)
        {
            this.SetTab(counter++, GetString("om.activity.iplist"), "Tab_IPs.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_ips');");
        }

        // Show contact groups
        this.SetTab(counter++, GetString("om.contactgroup.list"), "Tab_ContactGroups.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_contactgroups');");

        // Show scoring tab for site contacts
        if (ci.ContactSiteID > 0)
        {
            this.SetTab(counter++, GetString("om.score.list"), "Tab_Scoring.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_scoring');");
        }

        // Hide last 3 tabs if the contact is merged
        if (ci.ContactMergedWithContactID == 0)
        {
            this.SetTab(counter++, GetString("om.contact.merge"), "Tab_Merge.aspx?contactid=" + contactId + siteManagerParam, "SetHelpTopic('helpTopic', 'onlinemarketing_contact_merge');");
        }
    }
}
