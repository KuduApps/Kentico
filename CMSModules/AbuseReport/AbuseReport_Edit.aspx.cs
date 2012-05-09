using System;
using System.Web.UI.WebControls;

using CMS.CMSImportExport;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.TreeEngine;

public partial class CMSModules_AbuseReport_AbuseReport_Edit : CMSAbuseReportPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the page title
        CurrentMaster.Title.TitleText = GetString("abuse.properties");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_AbuseReport/object.png");
        CurrentMaster.Title.HelpTopicName = "abusereport";

        int reportID = QueryHelper.GetInteger("reportid", 0);
        ucAbuseEdit.ReportID = reportID;

        // Initializes page breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("abuse.reports");
        breadcrumbs[0, 1] = "~/CMSModules/AbuseReport/AbuseReport_List.aspx";

        AbuseReportInfo ar = AbuseReportInfoProvider.GetAbuseReportInfo(reportID);

        // Set edited object
        EditedObject = ar;

        if (ar != null)
        {
            // Set breadcrumbs
            breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(ar.ReportTitle);
        }

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        ucAbuseEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(ucAbuseEdit_OnCheckPermissions);
    }


    /// <summary>
    /// Check permission.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="sender">Sender</param>
    void ucAbuseEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.AbuseReport", permissionType))
        {
            sender.StopProcessing = true;
            RedirectToAccessDenied("CMS.AbuseReport", permissionType);
        }
    }

    #endregion
}
