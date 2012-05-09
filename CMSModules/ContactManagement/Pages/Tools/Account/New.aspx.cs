using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

// Title
[Title("Objects/OM_Account/new.png", "om.account.new", "onlinemarketing_account_new")]

public partial class CMSModules_ContactManagement_Pages_Tools_Account_New : CMSContactManagementAccountsPage
{
    private int siteId = 0;

    #region "Page events"

    protected void Page_PreInit(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);

        string url = ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/Account/List.aspx");
        url = URLHelper.AddParameterToUrl(url, "siteid", siteId.ToString());
        if (this.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
        }

        CurrentPage.InitBreadcrumbs(2);
        CurrentPage.SetBreadcrumb(0, GetString("om.account.list"), url, null, null);
        CurrentPage.SetBreadcrumb(1, GetString("om.account.new"), null, null, null);

        // Set siteID for newly created object
        editElem.SiteID = siteId;

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "NewAccount", null, "menu");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check read permission
        this.CheckReadPermission(siteId);
    }

    #endregion
}
