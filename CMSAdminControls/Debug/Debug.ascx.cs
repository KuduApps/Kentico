using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSAdminControls_Debug_Debug : CMSUserControl
{
    protected override void Render(HtmlTextWriter writer)
    {
        // Do not render if nothing is displayed
        if (this.logCache.Visible ||
            this.logSQL.Visible ||
            this.logFiles.Visible ||
            this.logSec.Visible ||
            this.logState.Visible || 
            this.logMac.Visible ||
            this.logAn.Visible ||
            this.logReq.Visible)
        {
            base.Render(writer);

            RequestStockHelper.Add("DebugPresent", true, true);
        }
    }
}
