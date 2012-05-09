using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Tab_General : CMSContactManagementContactsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int siteID = ContactHelper.ObjectSiteID(EditedObject);
        this.CheckReadPermission(siteID);

        editElem.OnSaved += new EventHandler(editElem_OnSaved);
    }


    void editElem_OnSaved(object sender, EventArgs e)
    {
        // Refresh breadcrumbs
        ScriptHelper.RefreshTabHeader(Page, null); 
    }


}