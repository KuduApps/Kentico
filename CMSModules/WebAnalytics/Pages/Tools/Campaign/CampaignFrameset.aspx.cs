using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_CampaignFrameset : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frmContent.Attributes["src"] = ResolveUrl("CampaignReport.aspx" + URLHelper.Url.Query + "&displayTitle=0");
        frmHeader.Attributes["src"] = "CampaignHeader.aspx" + URLHelper.Url.Query;
    }
}

