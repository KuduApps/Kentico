using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_layout_frameset_frameset : CMSWebPartPropertiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Layout"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Layout");
        }
    }
}
