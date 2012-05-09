using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.OnlineMarketing;

// Edited object
[EditedObject(OnlineMarketingObjectType.ACCOUNT, "accountID")]

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Merge_Split : CMSContactManagementAccountsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        mergeSplit.ShowChildrenOption = true;

        // Check read permission for accounts
        int siteID = AccountHelper.ObjectSiteID(EditedObject);
        this.CheckReadPermission(siteID);
    }
}