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

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_binding_frameset : CMSWebPartPropertiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Bindings"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Bindings");
        }

        this.frameContent.Attributes.Add("src", "webpartproperties_binding.aspx" + URLHelper.Url.Query);
        this.frameButtons.Attributes.Add("src", "webpartproperties_buttons.aspx" + URLHelper.Url.Query);
    }
}
