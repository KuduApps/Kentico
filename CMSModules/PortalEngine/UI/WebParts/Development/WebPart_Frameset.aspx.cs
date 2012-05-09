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

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Frameset : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFrameset);
        }

        webparttree.Attributes["src"] += URLHelper.Url.Query;
    }
}
