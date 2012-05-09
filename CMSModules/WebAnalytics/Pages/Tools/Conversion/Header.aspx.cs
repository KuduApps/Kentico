using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.WebAnalytics;

// Edited object
[EditedObject(AnalyticsObjectType.CONVERSION, "conversionID")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "abtesting.conversions", "~/CMSModules/WebAnalytics/Pages/Tools/Conversion/List.aspx", "_parent")]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}")]

// Tabs
[Tabs(3, "content")]
[Tab(0, "general.general", "edit.aspx?conversionid={%EditedObject.ID%}", "SetHelpTopic('helpTopic', 'conversion_general');")]
[Tab(1, "analytics_codename.campaign", "Tab_Campaigns.aspx?campaignid={%EditedObject.ID%}", "SetHelpTopic('helpTopic', 'conversions_campaign');")]

// Empty title for creating context help
[Title("", "", "conversion_general")]

public partial class CMSModules_WebAnalytics_Pages_Tools_Conversion_Header : CMSWebAnalyticsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

