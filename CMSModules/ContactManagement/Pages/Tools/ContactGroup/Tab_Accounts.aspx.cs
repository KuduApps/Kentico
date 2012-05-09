using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.OnlineMarketing;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACTGROUP, "groupId")]

public partial class CMSModules_ContactManagement_Pages_Tools_ContactGroup_Tab_Accounts : CMSContactManagementContactGroupsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.PanelContent.CssClass = string.Empty;
    }
}