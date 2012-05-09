using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSModules_WebAnalytics_Tools_Header : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Web analytics header";

        this.CurrentMaster.Title.TitleText = GetString("tools.ui.webanalytics");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_WebAnalytics/module.png");

        // Register script for unimenu button selection
        AddMenuButtonSelectScript(this, "WebAnalytics", null, "menu");
    }
}
