using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.OnlineMarketing;
using CMS.UIControls;

[EditedObject(OnlineMarketingObjectType.CONTACT, "contactId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Tab_Accounts : CMSContactManagementContactsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.PanelContent.CssClass = String.Empty;

        int siteID = ContactHelper.ObjectSiteID(EditedObject);

        // Check read permission 
        if (!AccountHelper.AuthorizedReadAccount(siteID, false) && !ContactHelper.AuthorizedReadContact(siteID, false))
        {
            RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadContacts");
        }
    }
}
