using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSSiteManager_Dashboard : DashboardPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Get current user info
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Check whether user is global admin
        if (currentUser.UserSiteManagerAdmin)
        {
            ucDashboard.PortalPageInstance = this as PortalPage;
            ucDashboard.TagsLiteral = this.ltlTags;

            ucDashboard.SetupDashboard();
        }
        // For non-global admin redirect to access denied
        else
        {
            URLHelper.Redirect(URLHelper.ResolveUrl("~/CMSSiteManager/accessdenied.aspx") + "?message=" + ResHelper.GetString("accessdeniedtopage.sitemanagerdenied"));
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

