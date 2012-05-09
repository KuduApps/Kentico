using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_AsyncBackground : CMSUserControl
{
    #region "Page events"

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Register dialog script
        ScriptHelper.RegisterJQueryDialog(Page);
        string resizeScript = "ShowModalBackground('" + pnlAsyncBackground.ClientID + "');";
        ScriptHelper.RegisterStartupScript(this, typeof(string), "asyncBackground" + ClientID, ScriptHelper.GetScript(resizeScript));
    }

    #endregion
}