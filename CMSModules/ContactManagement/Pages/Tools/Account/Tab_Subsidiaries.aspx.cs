using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.OnlineMarketing;

[EditedObject("om.account", "accountId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Tab_Subsidiaries : CMSContactManagementAccountsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.PanelContent.CssClass = "";
        // Check read permission for account
        int siteID = AccountHelper.ObjectSiteID(EditedObject);
        this.CheckReadPermission(siteID);
    }
}
