using System;

using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.GlobalHelper;

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.activitytype.list", "~/CMSModules/ContactManagement/Pages/Tools/Activities/ActivityType/List.aspx", null)]
[Breadcrumb(1, "om.activitytype.new")]

// Help
[Help("activitytype_new", "helptopic")]

[Security(Resource = "CMS.ContactManagement", Permission = "ReadActivities")]
public partial class CMSModules_ContactManagement_Pages_Tools_Activities_ActivityType_New : CMSContactManagementActivitiesPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.Breadcrumbs[0, 1] = AddSiteQuery(CurrentMaster.Title.Breadcrumbs[0, 1], QueryHelper.GetInteger("siteid", 0));
    }

    #endregion
}
