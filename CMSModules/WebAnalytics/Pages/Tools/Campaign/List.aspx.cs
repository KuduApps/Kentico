using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WebAnalytics;


public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_List : CMSWebAnalyticsPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = ResHelper.GetString("campaign.campaign.new");
        actions[0, 3] = ResolveUrl("new.aspx" + (QueryHelper.GetBoolean("displayreport", false) ? "?displayreport=true" : ""));
        actions[0, 5] = GetImageUrl("Objects/Analytics_Campaign/add.png");

        // Set the actions
        CurrentMaster.HeaderActions.Actions = actions;
    }

    #endregion
}
