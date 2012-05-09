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
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_layout_frameset : CMSWebPartPropertiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Layout"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Layout");
        }

        this.frameMenu.Attributes.Add("src", "webpartproperties_layout_menu.aspx" + URLHelper.Url.Query);
        this.frameButtons.Attributes.Add("src", "webpartproperties_buttons.aspx" + URLHelper.Url.Query);

        string url = "webpartproperties_layout.aspx" + URLHelper.Url.Query;
        url = URLHelper.AddParameterToUrl(url, "noreload", "true");
        this.frameContent.Attributes.Add("src", url);
    }
}
