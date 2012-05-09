using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

// Title
[Title("Objects/OM_Contact/new.png", "om.contact.new", "onlinemarketing_contact_new")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_New : CMSContactManagementContactsPage
{
    #region "Page events"

    private int siteId = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);

        string url = ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/Contact/List.aspx");
        url = URLHelper.AddParameterToUrl(url, "siteid", siteId.ToString());
        if (this.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
        }

        CurrentPage.InitBreadcrumbs(2);
        CurrentPage.SetBreadcrumb(0, GetString("om.contact.list"), url, null, null);
        CurrentPage.SetBreadcrumb(1, GetString("om.contact.new"), null, null, null);

        editElem.SiteID = siteId;

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "NewContact", null, "menu");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check read permission
        this.CheckReadPermission(siteId);
    }

    #endregion
}
