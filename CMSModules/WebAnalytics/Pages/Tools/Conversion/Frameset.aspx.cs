using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;


public partial class CMSModules_WebAnalytics_Pages_Tools_Conversion_Frameset : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frm.ContentUrl = "edit.aspx";
        frm.FrameHeight = 65;
    }
}

