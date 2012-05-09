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
using CMS.WebAnalytics;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_WebAnalytics_Tools_Disabled : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName))
        {
            URLHelper.Redirect("default.aspx");
        }
        else
        {
            this.lblInfo.Text = GetString("WebAnalytics.Disabled");
        }
    }
}
