using System;

using CMS.OnlineMarketing;
using CMS.UIControls;
using CMS.WebAnalytics;

// Edited object
[EditedObject(OnlineMarketingObjectType.ACTIVITYTYPE, "typeId")]

// Help
[Help("activitytype_edit", "helptopic")]

[Security(Resource = "CMS.ContactManagement", Permission = "ReadActivities")]
public partial class CMSModules_ContactManagement_Pages_Tools_Activities_ActivityType_Tab_General : CMSContactManagementActivitiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentPage.InitBreadcrumbs(2);
        CurrentPage.SetBreadcrumb(0, GetString("om.activitytype.list"), "~/CMSModules/ContactManagement/Pages/Tools/Activities/ActivityType/List.aspx", null, null);

        RefreshBreadCrumb();

        editElem.OnSaved += new EventHandler(editElem_OnSaved);
    }


    void editElem_OnSaved(object sender, EventArgs e)
    {
        this.RefreshBreadCrumb();
    }


    /// <summary>
    /// Sets the second part of breadcrumbs (name of activity type).
    /// </summary>
    private void RefreshBreadCrumb()
    {
        ActivityTypeInfo ati = (ActivityTypeInfo)EditedObject;
        if (ati != null)
        {
            CurrentPage.SetBreadcrumb(1, ati.ActivityTypeDisplayName, null, null, null);
        }
    }
}