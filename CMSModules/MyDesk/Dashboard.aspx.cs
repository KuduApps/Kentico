using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_MyDesk_Dashboard : DashboardPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        ucDashboard.ResourceName = "CMS.MyDesk";
        ucDashboard.ElementName = "MyDeskDashBoardItem";
        ucDashboard.PortalPageInstance = this as PortalPage;
        ucDashboard.TagsLiteral = this.ltlTags;
        ucDashboard.DashboardSiteName = CMSContext.CurrentSiteName;

        ucDashboard.SetupDashboard();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyDeskDashBoard")))
        {
            CMSMyDeskPage.RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyDeskDashBoard");
        }

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyDeskDashBoardItem"))
        {
            CMSMyDeskPage.RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyDeskDashBoardItem");
        }
    }
}

