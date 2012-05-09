using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

[Title(Text = "Abuse report", ImageUrl = "Objects/CMS_AbuseReport/object.png")]
public partial class CMSAPIExamples_Code_Tools_AbuseReport_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Abuse report
        this.apiCreateAbuseReport.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAbuseReport);
        this.apiGetAndUpdateAbuseReport.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateAbuseReport);
        this.apiGetAndBulkUpdateAbuseReports.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateAbuseReports);
        this.apiDeleteAbuseReport.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteAbuseReport);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Abuse report
        this.apiCreateAbuseReport.Run();
        this.apiGetAndUpdateAbuseReport.Run();
        this.apiGetAndBulkUpdateAbuseReports.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Abuse report
        this.apiDeleteAbuseReport.Run();
    }

    #endregion


    #region "API examples - Abuse report"

    /// <summary>
    /// Creates abuse report. Called when the "Create report" button is pressed.
    /// </summary>
    private bool CreateAbuseReport()
    {
        // Create new abuse report object
        AbuseReportInfo newReport = new AbuseReportInfo();

        // Set the properties
        newReport.ReportTitle = "MyNewReport";
        newReport.ReportComment = "This is an example abuse report.";

        newReport.ReportURL = URLHelper.GetAbsoluteUrl(URLHelper.CurrentURL);
        newReport.ReportCulture = CMSContext.PreferredCultureCode;
        newReport.ReportSiteID = CMSContext.CurrentSiteID;
        newReport.ReportUserID = CMSContext.CurrentUser.UserID;
        newReport.ReportWhen = DateTime.Now;
        newReport.ReportStatus = AbuseReportStatusEnum.New;

        // Save the abuse report
        AbuseReportInfoProvider.SetAbuseReportInfo(newReport);

        return true;
    }


    /// <summary>
    /// Gets and updates abuse report. Called when the "Get and update report" button is pressed.
    /// Expects the CreateAbuseReport method to be run first.
    /// </summary>
    private bool GetAndUpdateAbuseReport()
    {
        string where = "ReportTitle LIKE N'MyNewReport%'";

        // Get the report
        DataSet reports = AbuseReportInfoProvider.GetAbuseReports(where, null);

        if (!DataHelper.DataSourceIsEmpty(reports))
        {
            // Create the object from DataRow
            AbuseReportInfo updateReport = new AbuseReportInfo(reports.Tables[0].Rows[0]);

            // Update the properties
            updateReport.ReportStatus = AbuseReportStatusEnum.Solved;

            // Save the changes
            AbuseReportInfoProvider.SetAbuseReportInfo(updateReport);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates abuse reports. Called when the "Get and bulk update reports" button is pressed.
    /// Expects the CreateAbuseReport method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateAbuseReports()
    {
        // Prepare the parameters
        string where = "ReportTitle LIKE N'MyNewReport%'";

        // Get the data
        DataSet reports = AbuseReportInfoProvider.GetAbuseReports(where, null);
        if (!DataHelper.DataSourceIsEmpty(reports))
        {
            // Loop through the individual items
            foreach (DataRow reportDr in reports.Tables[0].Rows)
            {
                // Create object from DataRow
                AbuseReportInfo modifyReport = new AbuseReportInfo(reportDr);

                // Update the properties
                modifyReport.ReportStatus = AbuseReportStatusEnum.Rejected;

                // Save the changes
                AbuseReportInfoProvider.SetAbuseReportInfo(modifyReport);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes abuse report. Called when the "Delete report" button is pressed.
    /// Expects the CreateAbuseReport method to be run first.
    /// </summary>
    private bool DeleteAbuseReport()
    {
        string where = "ReportTitle LIKE N'MyNewReport%'";

        // Get the report
        DataSet reports = AbuseReportInfoProvider.GetAbuseReports(where, null);

        if (!DataHelper.DataSourceIsEmpty(reports))
        {
            // Create the object from DataRow
            AbuseReportInfo deleteReport = new AbuseReportInfo(reports.Tables[0].Rows[0]);

            // Delete the abuse report
            AbuseReportInfoProvider.DeleteAbuseReportInfo(deleteReport);

            return true;
        }

        return false;

    }

    #endregion
}
