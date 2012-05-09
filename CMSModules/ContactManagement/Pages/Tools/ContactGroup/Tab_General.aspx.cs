using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACTGROUP, "groupId")]

public partial class CMSModules_ContactManagement_Pages_Tools_ContactGroup_Tab_General : CMSContactManagementContactGroupsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        editElem.SiteID = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);
    }
}