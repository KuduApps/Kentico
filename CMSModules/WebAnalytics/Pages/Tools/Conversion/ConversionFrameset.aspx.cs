using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_WebAnalytics_Pages_Tools_Conversion_ConversionFrameset : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frmContent.Attributes["src"] = ResolveUrl("~/CMSModules/WebAnalytics/Tools/Analytics_Report.aspx" + URLHelper.Url.Query + "&displayTitle=0");
        frmHeader.Attributes["src"] = "ConversionHeader.aspx" + URLHelper.Url.Query;
    }
}

