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
using CMS.PortalControls;

public partial class CMSPages_ScriptManager : CMSPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        // Ensure script manager
        PortalHelper.EnsureScriptManager(Page);
    }
}