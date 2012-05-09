using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSDesk_Administration_Menu : CMSAdministrationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.administrationUiPanelMenu.CategoryCreated += administrationUiPanelMenu_CategoryCreated;
        if (!ScriptHelper.IsJQueryRegistered())
        {
            ScriptHelper.RegisterJQuery(this.Page);
        }

        string categoryHoverScript = @"
$j('.PanelMenuCategory').mouseenter(function() {
    $j(this).addClass('PanelMenuCategoryHover');
}).mouseleave(function() {
    $j(this).removeClass('PanelMenuCategoryHover');
});";
        ScriptHelper.RegisterStartupScript(this, typeof(String), "CategoryHoverScript", ScriptHelper.GetScript(categoryHoverScript));
    }


    void administrationUiPanelMenu_CategoryCreated(object sender, CMSAdminControls_UI_UIProfiles_UIPanelMenu.CategoryCreatedEventArgs e)
    {
        // Cancel category if it is not available
        if (e.UIElement != null)
        {
            if (!IsAdministrationUIElementAvailable(e.UIElement))
            {
                e.Cancel = true;
            }

            // Check site availabitility
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("cms.administration", e.UIElement.ElementName, true))
            {
                e.Cancel = true;
            }
        }
    }
}