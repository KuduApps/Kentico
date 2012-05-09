using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_OnlineMarketing_Pages_Dashboard : DashboardPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        ucDashboard.ResourceName = "CMS.OnlineMarketing";
        ucDashboard.ElementName = "OMDashBoard";
        ucDashboard.PortalPageInstance = this as PortalPage;
        ucDashboard.TagsLiteral = this.ltlTags;
        ucDashboard.DashboardSiteName = CMSContext.CurrentSiteName;

        ucDashboard.SetupDashboard();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo cu = CMSContext.CurrentUser;

        // Check permissions
        if ((cu == null) || !cu.IsAuthorizedPerResource("CMS.OnlineMarketing", "Read"))
        {
            CMSMyDeskPage.RedirectToCMSDeskAccessDenied("CMS.OnlineMarketing", "Read");
        }

        // Check UIProfile
        if (!cu.IsAuthorizedPerUIElement("CMS.OnlineMarketing", "OMDashBoardGroup"))
        {
            CMSMyDeskPage.RedirectToCMSDeskUIElementAccessDenied("CMS.OnlineMarketing", "OMDashBoardGroup");
        }

        if (!cu.IsAuthorizedPerUIElement("CMS.OnlineMarketing", "OMDashBoard"))
        {
            CMSMyDeskPage.RedirectToCMSDeskUIElementAccessDenied("CMS.OnlineMarketing", "OMDashBoard");
        }

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "OMDashBoard", null, "menu");
    }
}