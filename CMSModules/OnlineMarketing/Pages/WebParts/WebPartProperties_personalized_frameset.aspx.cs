using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_OnlineMarketing_Pages_WebParts_WebPartProperties_personalized_frameset : CMSWebPartPropertiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Variant"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Variant");
        }

        this.frameContent.Attributes.Add("src", "webpartproperties_personalized.aspx" + URLHelper.Url.Query);
        this.frameButtons.Attributes.Add("src", ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/webpartproperties_buttons.aspx" + URLHelper.Url.Query));
    }
}

