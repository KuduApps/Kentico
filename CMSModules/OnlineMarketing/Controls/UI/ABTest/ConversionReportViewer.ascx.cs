using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_OnlineMarketing_Controls_UI_ABTest_ConversionReportViewer : CMSAdminControl
{
    #region "Variables"

    private String mReportsName;
    private bool reportDisplayed = false;
    private IDisplayReport ucDisplayReport;

    #endregion


    #region "Properties"

    /// <summary>
    /// Name of all time reports  (year report,monthreport ,...).
    /// </summary>
    public String ReportsName
    {
        get
        {
            return mReportsName;
        }
        set
        {
            mReportsName = value;
        }
    }


    /// <summary>
    /// If true, (all) is added to conversion selector.
    /// </summary>
    public bool ShowAllConversions
    {
        get
        {
            return ucConversions.AllowAll;
        }
        set
        {
            ucConversions.AllowAll = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlDisplayReport.Controls.Add((Control)ucDisplayReport);

        ucABTests.ReturnColumnName = "ABTestName";
        ucABTests.AllowEmpty = false;
        ucABTests.ReloadData(false);
        ucABTests.PostbackOnChange = true;
        ucABTests.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
        ucConversions.ABTestName = ValidationHelper.GetString(ucABTests.Value, String.Empty);
        ucConversions.PostbackOnDropDownChange = true;
    }


    void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        ucConversions.ABTestName = ValidationHelper.GetString(ucABTests.Value, String.Empty);
        ucConversions.ReloadData(true);
    }


    /// <summary>
    /// Displays the report
    /// </summary>
    /// <param name="reload">If true dirplay reload control is reloaded</param>
    private void DisplayReport(bool reload)
    {
        if (reportDisplayed)
        {
            return;
        }

        ucGraphType.ProcessChartSelectors(false);

        // Prepare report parameters
        DataTable dtp = new DataTable();

        dtp.Columns.Add("FromDate", typeof(DateTime));
        dtp.Columns.Add("ToDate", typeof(DateTime));
        dtp.Columns.Add("CodeName", typeof(string));
        dtp.Columns.Add("TestName", typeof(string));
        dtp.Columns.Add("ConversionName", typeof(string));

        object[] parameters = new object[5];

        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = "abtests";
        parameters[3] = ValidationHelper.GetString(ucABTests.Value, String.Empty);

        // Conversion
        String conversion = ValidationHelper.GetString(ucConversions.Value, String.Empty);
        if (conversion == ucConversions.AllRecordValue)
        {
            conversion = String.Empty;
        }

        parameters[4] = conversion;

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();

        String reportName = ucGraphType.GetReportName(ReportsName);
        ucDisplayReport.ReportName = reportName;

        // Conversion count
        if (!ucDisplayReport.IsReportLoaded())
        {
            lblErrorConversions.Visible = true;
            lblErrorConversions.Text = String.Format(GetString("Analytics_Report.ReportDoesnotExist"), reportName);
        }
        else
        {
            ucDisplayReport.LoadFormParameters = false;
            ucDisplayReport.DisplayFilter = false;
            ucDisplayReport.ReportParameters = dtp.Rows[0];
            ucDisplayReport.GraphImageWidth = 100;
            ucDisplayReport.IgnoreWasInit = true;
            ucDisplayReport.UseExternalReload = true;
            ucDisplayReport.UseProgressIndicator = true;

            if (reload)
            {
                ucDisplayReport.ReloadData(true);
            }
        }

        reportDisplayed = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        DisplayReport(true);
        base.OnPreRender(e);
    }


    /// <summary>
    /// Create button print onclick event
    /// </summary>
    public string CreatePrintLink()
    {
        DisplayReport(true);
        return "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + ucDisplayReport.ReportName + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + ucDisplayReport.ReportName + "', 800, 700); return false;";
    }


    /// <summary>
    /// Saves the report - Returns the saved report ID or 0 if some error was occurred or don't have permissions to this report.
    /// </summary>
    public int SaveReport()
    {
        // Don't reload display report - save does it
        DisplayReport(false);

        int ret = ucDisplayReport.SaveReport();
        if (ret > 0)
        {
            lblInfo.Visible = true;
            lblInfo.Text = String.Format(GetString("Analytics_Report.ReportSavedTo"), ucDisplayReport.ReportDisplayName + " - " + DateTime.Now.ToString());
        }
        return ret;
    }

    #endregion
}
