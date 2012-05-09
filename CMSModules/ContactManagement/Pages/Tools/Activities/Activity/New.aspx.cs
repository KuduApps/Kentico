using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.OnlineMarketing;

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.activity.list", "~/CMSModules/ContactManagement/Pages/Tools/Activities/Activity/List.aspx", null)]
[Breadcrumb(1, "om.activity.newcustom")]

// Help
[Help("activity_new", "helptopic")]

[Security(Resource = "CMS.ContactManagement", Permission = "ReadActivities")]
public partial class CMSModules_ContactManagement_Pages_Tools_Activities_Activity_New : CMSContactManagementActivitiesPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        int siteId = QueryHelper.GetInteger("siteid", 0);
        editElem.SiteID = siteId;
        editElem.ShowSiteSelector = false;
        CurrentMaster.Title.Breadcrumbs[0, 1] = AddSiteQuery(CurrentMaster.Title.Breadcrumbs[0, 1], siteId);
    }

    #endregion
}
