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

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Tab_Membership : CMSContactManagementContactsPage
{
    protected int contactId = 0;
    protected string siteManagerParam = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        contactId = QueryHelper.GetInteger("contactId", 0);
        if (this.IsSiteManager)
        {
            siteManagerParam = "&issitemanager=1";
        }
    }
}
