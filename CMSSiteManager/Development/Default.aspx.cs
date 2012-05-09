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

public partial class CMSSiteManager_Development_Default : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFrameset);
        }

        ScriptHelper.RegisterTitleScript(this, GetString("Header.Development"));
    }
}
