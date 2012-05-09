using System;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSDesk_Administration_Header : CMSAdministrationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uiToolbarElem.OnButtonFiltered += new CMSAdminControls_UI_UIProfiles_UIToolbar.ButtonFilterEventHandler(uiToolbarElem_OnButtonFiltered);
    }

    bool uiToolbarElem_OnButtonFiltered(CMS.SiteProvider.UIElementInfo uiElement)
    {
        // Check site availabitility
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("cms.administration", uiElement.ElementName, true))
        {
            return false;
        }

        // Check whether separable modules are loaded
        return IsAdministrationUIElementAvailable(uiElement);
    }
}
