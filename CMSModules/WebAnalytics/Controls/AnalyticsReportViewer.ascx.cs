using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_WebAnalytics_Controls_AnalyticsReportViewer : CMSAdminControl
{
    #region "Variables"

    private string mReportsCodeName;
    private string mDataName;
    private bool mLoadFormParameters = false;

    /// <summary>
    /// Display control should be loaded only once.
    /// </summary>
    private bool reportLoaded = false;

    /// <summary>
    /// Control for display report (separation) problem
    /// </summary>
    private IDisplayReport ucDisplayReport = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Report name to show.
    /// </summary>
    public string ReportsCodeName
    {
        get
        {
            return mReportsCodeName;
        }
        set
        {
            mReportsCodeName = value;
        }
    }


    /// <summary>
    /// Data report name.
    /// </summary>
    public string DataName
    {
        get
        {
            return mDataName;
        }
        set
        {
            mDataName = value;
        }
    }


    /// <summary>
    /// If true, report form parameters are loaded,form filter is displayed,period and graph type selector is hidden.
    /// </summary>
    public bool LoadFormParameters
    {
        get
        {
            return mLoadFormParameters;
        }
        set
        {
            EnsureDisplayReport();
            mLoadFormParameters = value;
            ucDisplayReport.LoadFormParameters = value;
            ucDisplayReport.DisplayFilter = value;
            pnlPeriodSelectors.Visible = !value;
        }
    }


    /// <summary>
    /// Css class for display report
    /// </summary>
    public string DisplayReportBodyClass
    {
        get
        {
            EnsureDisplayReport();
            return ucDisplayReport.BodyCssClass;
        }
        set
        {
            EnsureDisplayReport();
            ucDisplayReport.BodyCssClass = value;
        }
    }


    /// <summary>
    /// Start of shown period.
    /// </summary>
    public DateTime From
    {
        get
        {
            return ucGraphTypePeriod.From;
        }
    }


    /// <summary>
    /// End of show period.
    /// </summary>
    public DateTime To
    {
        get
        {
            return ucGraphTypePeriod.To;
        }
    }


    /// <summary>
    /// Display name of shown report.
    /// </summary>
    public string ReportDisplayName
    {
        get
        {
            EnsureDisplayReport();
            return ucDisplayReport.ReportDisplayName;
        }
    }


    /// <summary>
    /// Name of single report (f.e. pageviews.monthreport).
    /// </summary>
    public string ReportName
    {
        get
        {
            EnsureDisplayReport();
            if (!String.IsNullOrEmpty(ucDisplayReport.ReportName))
            {
                return ucDisplayReport.ReportName;
            }

            return ucGraphTypePeriod.GetReportName(ReportsCodeName);
        }
        set
        {
            EnsureDisplayReport();
            ucDisplayReport.ReportName = value;
        }
    }

    #endregion


    #region "Methods"

    private void EnsureDisplayReport()
    {
        if (ucDisplayReport == null)
        {
            ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
            divGraphArea.Controls.Add((Control)ucDisplayReport);
        }
    }    


    protected override void OnLoad(EventArgs e)
    {
        EnsureDisplayReport();
        if (ucDisplayReport != null)
        {
            ucDisplayReport.RenderCssClasses = true;
        }

        base.OnLoad(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!reportLoaded)
        {
            DisplayReport(false);
        }
    }


    /// <summary>
    /// Extracts report code name from report code names list, based on interval selection. Returns single codename for no interval reports.
    /// </summary>
    /// <param name="reportCodeName">Report code name list (delimited by semicolons)</param>
    public string GetReportCodeName(string reportCodeName)
    {
        return ucGraphTypePeriod.GetReportName(reportCodeName);
    }


    /// <summary>
    /// Display report with given criteria.
    /// </summary>
    public void DisplayReport(bool intervalChanged)
    {
        // If load form parameters are loaded display report calls reload on onInit (time charts not allowed)
        if (LoadFormParameters)
        {
            return;
        }

        if (ucDisplayReport == null)
        {
            return;
        }

        ucGraphTypePeriod.ProcessChartSelectors(intervalChanged);
        ucDisplayReport.ReportName = ucGraphTypePeriod.GetReportName(ReportsCodeName);
        ucDisplayReport.GraphImageWidth = 100;
        ucDisplayReport.IgnoreWasInit = true;        

        if (pnlPeriodSelectors.Visible)
        {
            // Prepare report parameters
            DataTable dtp = new DataTable();

            dtp.Columns.Add("FromDate", typeof(DateTime));
            dtp.Columns.Add("ToDate", typeof(DateTime));
            dtp.Columns.Add("CodeName", typeof(string));
            dtp.Columns.Add("FirstCategory", typeof(string));
            dtp.Columns.Add("SecondCategory", typeof(string));
            dtp.Columns.Add("Direct", typeof(string));
            dtp.Columns.Add("Search", typeof(string));
            dtp.Columns.Add("Reffering", typeof(string));

            object[] parameters = new object[8];

            parameters[0] = ucGraphTypePeriod.From;
            parameters[1] = ucGraphTypePeriod.To;
            parameters[2] = DataName;
            parameters[3] = HitLogProvider.VISITORS_FIRST;
            parameters[4] = HitLogProvider.VISITORS_RETURNING;
            parameters[5] = HitLogProvider.REFERRINGSITE + "_direct";
            parameters[6] = HitLogProvider.REFERRINGSITE + "_search";
            parameters[7] = HitLogProvider.REFERRINGSITE + "_referring";

            dtp.Rows.Add(parameters);
            dtp.AcceptChanges();

            if (!ucDisplayReport.IsReportLoaded())
            {
                lblError.Visible = true;
                lblError.Text = String.Format(GetString("Analytics_Report.ReportDoesnotExist"), ucDisplayReport.ReportName);
            }
            else
            {
                ucDisplayReport.LoadFormParameters = false;
                ucDisplayReport.DisplayFilter = false;
                ucDisplayReport.ReportParameters = dtp.Rows[0];
                ucDisplayReport.UseExternalReload = true;
                ucDisplayReport.UseProgressIndicator = true;
            }
        }

        ucDisplayReport.ReloadData(true);
        reportLoaded = true;
    }


    /// <summary>
    /// Saves current report.
    /// </summary>
    public int SaveReport()
    {
        if (ucDisplayReport != null)
        {
            DisplayReport(false);
            int result = ucDisplayReport.SaveReport();

            // Display info label
            if (result > 0)
            {
                lblInfo.Visible = true;
                lblInfo.Text = String.Format(GetString("Ecommerce_Report.ReportSavedTo"), ReportDisplayName + " - " + DateTime.Now.ToString());
            }

            return result;
        }

        return 0;
    }


    /// <summary>
    /// Generates query string parameters.
    /// </summary>
    public string GetQueryStringParameters()
    {
        if (ucDisplayReport != null)
        {
            return AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters);
        }

        return String.Empty;
    }

    #endregion
}
