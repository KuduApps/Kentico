using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSDesk_Tools_Header : CMSToolsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uiToolbarElem.OnButtonFiltered += new CMSAdminControls_UI_UIProfiles_UIToolbar.ButtonFilterEventHandler(uiToolbarElem_OnButtonFiltered);
    }


    private bool uiToolbarElem_OnButtonFiltered(UIElementInfo uiElement)
    {
        // Check site availabitility
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("cms.tools", uiElement.ElementName, true))
        {
            return false;
        }

        // Check whether separable modules are loaded
        return IsToolsUIElementAvailable(uiElement);
    }
}
