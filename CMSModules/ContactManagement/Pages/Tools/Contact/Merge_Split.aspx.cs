using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.OnlineMarketing;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Merge_Split : CMSContactManagementContactsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check read permission
        this.CheckReadPermission(ContactHelper.ObjectSiteID(EditedObject));
        mergeSplit.ShowChildrenOption = true;
    }
}