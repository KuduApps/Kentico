using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;


public partial class CMSModules_WebAnalytics_Tools_Dashboard : DashboardPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        ucDashboard.ResourceName = "CMS.WebAnalytics";
        ucDashboard.ElementName = "Dashboard";
        ucDashboard.PortalPageInstance = this as PortalPage;
        ucDashboard.TagsLiteral = this.ltlTags;
        ucDashboard.DashboardSiteName = CMSContext.CurrentSiteName;

        ucDashboard.SetupDashboard();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Keep current user
        CurrentUserInfo cu = CMSContext.CurrentUser;

        // Check permissions
        if ((CMSContext.CurrentUser == null) || !cu.IsAuthorizedPerResource("CMS.WebAnalytics", "Read"))
        {
            CMSMyDeskPage.RedirectToCMSDeskAccessDenied("CMS.WebAnalytics", "Read");
        }

        // Check ui elements
        if (!cu.IsAuthorizedPerUIElement("CMS.WebAnalytics", "Dashboard"))
        {
            CMSMyDeskPage.RedirectToCMSDeskUIElementAccessDenied("CMS.WebAnalytics", "Dashboard");
        }
    }
}

