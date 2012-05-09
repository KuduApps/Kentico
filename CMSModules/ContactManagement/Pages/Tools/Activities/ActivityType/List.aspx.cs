using System;

using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.CMSHelper;
using CMS.OnlineMarketing;
using CMS.SiteProvider;
using CMS.GlobalHelper;

[Actions(1)]
[Action(0, "Objects/OM_ActivityType/add.png", "om.activitytype.new", "New.aspx")]

[Security(Resource = "CMS.ContactManagement", Permission = "ReadActivities")]
public partial class CMSModules_ContactManagement_Pages_Tools_Activities_ActivityType_List : CMSContactManagementActivitiesPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string currSiteName = null;
        int currSiteId = 0;

        // Get current site ID/name
        if (ContactHelper.IsSiteManager)
        {
            currSiteId = this.SiteID;
            currSiteName = SiteInfoProvider.GetSiteName(currSiteId);
        }
        else
        {
            currSiteName = CMSContext.CurrentSiteName;
            currSiteId = CMSContext.CurrentSiteID;
        }

        bool globalObjectsSelected = (currSiteId == UniSelector.US_GLOBAL_RECORD);
        bool allSitesSelected = (currSiteId == UniSelector.US_ALL_RECORDS);

        // Show warning if activity logging is disabled (do not show anything if global objects or all sites is selected)
        if (!ActivitySettingsHelper.OnlineMarketingEnabled(currSiteName))
        {
            lblDis.ResourceString = "om.onlinemarketing.disabled";
        }
        pnlDis.Visible = !globalObjectsSelected && !allSitesSelected && !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(currSiteName);

        CurrentMaster.HeaderActions.Actions[0, 3] = AddSiteQuery(CurrentMaster.HeaderActions.Actions[0, 3], QueryHelper.GetInteger("siteid", 0));
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        CurrentMaster.HeaderActions.Enabled = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ContactManagement", "ManageActivities");
    }

    #endregion
}
