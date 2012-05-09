using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.OnlineMarketing;

[EditedObject("om.account", "accountId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Tab_Contacts : CMSContactManagementAccountsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.PanelContent.CssClass = "";

        int siteID = AccountHelper.ObjectSiteID(EditedObject);

        // Check read permission 
        if (!AccountHelper.AuthorizedReadAccount(siteID, false) && !ContactHelper.AuthorizedReadContact(siteID, false))
        {
            RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadAccounts");
        }
    }
}
