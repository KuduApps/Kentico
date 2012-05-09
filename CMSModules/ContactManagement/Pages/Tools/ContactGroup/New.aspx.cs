using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

// Title
[Title("Objects/OM_ContactGroup/new.png", "om.contactgroup.new", "onlinemarketing_contactgroup_new")]

public partial class CMSModules_ContactManagement_Pages_Tools_ContactGroup_New : CMSContactManagementContactGroupsPage
{
    private int siteId = 0;

    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);

        string url = ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/ContactGroup/List.aspx");
        url = URLHelper.AddParameterToUrl(url, "siteid", siteId.ToString());
        if (this.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
        }

        CurrentPage.InitBreadcrumbs(2);
        CurrentPage.SetBreadcrumb(0, GetString("om.contactgroup.list"), url, null, null);
        CurrentPage.SetBreadcrumb(1, GetString("om.contactgroup.new"), null, null, null);

        editElem.SiteID = siteId;
    }

    #endregion
}
