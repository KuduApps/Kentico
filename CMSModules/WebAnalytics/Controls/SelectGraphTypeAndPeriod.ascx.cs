using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.UIControls;

public partial class CMSModules_WebAnalytics_Controls_SelectGraphTypeAndPeriod : CMSAdminControl
{
    #region "Variables"

    const int YEAR_REP = 0;
    const int MONTH_REP = 1;
    const int WEEK_REP = 2;
    const int DAY_REP = 3;
    const int HOUR_REP = 4;

    private bool mAllowEmtpyDate = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Period start time.
    /// </summary>
    public DateTime From
    {
        get
        {
            DateTime fromDate = ucGraphPeriod.From;
            if ((ucGraphType.SelectedValue == HitsIntervalEnum.Week) && (fromDate != DateTimeHelper.ZERO_TIME))
            {
                fromDate = DateTimeHelper.GetWeekStart(fromDate, AnalyticsHelper.AnalyticsCulture);
            }

            return fromDate;
        }
        set
        {
            ucGraphPeriod.From = value;
        }
    }


    /// <summary>
    /// If true, empty dates are allowed in date time selectors
    /// </summary>
    public bool AllowEmptyDate
    {
        get
        {
            return mAllowEmtpyDate;
        }
        set
        {
            mAllowEmtpyDate = value;
        }
    }


    /// <summary>
    /// Shows/hides graph period selector
    /// </summary>
    public bool ShowIntervalSelector
    {
        get
        {
            return pnlGraphType.Visible;
        }
        set
        {
            pnlGraphType.Visible = false;
        }
    }


    /// <summary>
    /// Period end time.
    /// </summary>
    public DateTime To
    {
        get
        {
            DateTime toDate = ucGraphPeriod.To;
            if ((ucGraphType.SelectedValue == HitsIntervalEnum.Week) && (toDate != DateTimeHelper.ZERO_TIME))
            {
                toDate = DateTimeHelper.GetWeekStart(toDate, AnalyticsHelper.AnalyticsCulture).AddDays(7).AddSeconds(-1);
            }

            return toDate;
        }
        set
        {
            ucGraphPeriod.To = value;
        }
    }


    /// <summary>
    /// Get selected interval of control (year,month,..)
    /// </summary>
    public HitsIntervalEnum SelectedInterval
    {
        get
        {
            return ucGraphType.SelectedValue;
        }
    }


    /// <summary>
    /// Enables/disables range date time picker
    /// </summary>
    public bool EnableDateTimePicker
    {
        get
        {
            return ucGraphPeriod.Enabled;
        }
        set
        {
            ucGraphPeriod.Enabled = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Defualt settings
        if (!URLHelper.IsPostback())
        {
            ucGraphType.SelectedValue = HitsIntervalEnum.Day;
        }

        ucGraphType.OnGraphTypeChanged += new EventHandler(ucGraphType_OnGraphTypeChanged);
    }

    /// <summary>
    /// Raised when graph type changed (week,hour,month ...).
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="ea">Event args</param>
    void ucGraphType_OnGraphTypeChanged(object sender, EventArgs ea)
    {
        ProcessChartSelectors(true);
    }


    /// <summary>
    /// Display report with given criteria.
    /// </summary>
    public void ProcessChartSelectors(bool intervalChanged)
    {
        // Get values from range selector
        DateTime fromDate = intervalChanged ? ucGraphPeriod.FullFrom : ucGraphPeriod.From;
        DateTime toDate = intervalChanged ? ucGraphPeriod.FullTo : ucGraphPeriod.To;
        DateTime now = DateTime.Now;

        // Set current range type to selector
        ucGraphPeriod.Interval = ucGraphType.SelectedValue;
        bool noTimeSet = false;

        // If no values are set in first load 
        if (((toDate == DateTimeHelper.ZERO_TIME) || (fromDate == DateTimeHelper.ZERO_TIME)) && !URLHelper.IsPostback())
        {
            noTimeSet = true;

            toDate = now.Date.AddDays(1).AddSeconds(-1);
            fromDate = toDate.Date.AddDays(-31);
        }

        // If no time is set or interval changed 
        // Select new values to calendar
        if (noTimeSet || intervalChanged)
        {
            if ((fromDate != DateTimeHelper.ZERO_TIME) && !AllowEmptyDate)
            {
                ucGraphPeriod.From = fromDate;
            }
            if ((toDate != DateTimeHelper.ZERO_TIME) && !AllowEmptyDate)
            {
                ucGraphPeriod.To = toDate;
            }
        }
    }


    /// <summary>
    /// Returns full report name depending on selected period.
    /// </summary>
    /// <param name="ReportsCodeName">All repord names</param>
    public string GetReportName(string ReportsCodeName)
    {
        string reportName = String.Empty;

        // Select reports depending on graph type chosen
        switch (ucGraphType.SelectedValue)
        {
            case HitsIntervalEnum.Year:
                reportName = GetReportCodeName(YEAR_REP, ReportsCodeName);
                break;

            case HitsIntervalEnum.Month:
                reportName = GetReportCodeName(MONTH_REP, ReportsCodeName);
                break;

            case HitsIntervalEnum.Week:
                reportName = GetReportCodeName(WEEK_REP, ReportsCodeName);
                break;

            case HitsIntervalEnum.Day:
                reportName = GetReportCodeName(DAY_REP, ReportsCodeName);
                break;

            case HitsIntervalEnum.Hour:
                reportName = GetReportCodeName(HOUR_REP, ReportsCodeName);
                break;
        }
        return reportName;
    }


    /// <summary>
    /// Extracts report code name from report code names list.
    /// </summary>
    /// <param name="rep">Requested report (0 - year, 1 - month, 2 - week, 3 - day, 4 - hour)</param>
    /// <param name="reportCodeName">Report code name list (delimited by semicolons)</param>
    private string GetReportCodeName(int rep, string reportCodeName)
    {
        string[] reports = reportCodeName.Split(';');
        int length = reports.GetLength(0);

        // Return solo report name (no interval)
        if (length == 1)
        {
            return reportCodeName;
        }

        if ((length < 5) || (rep < 0) || (rep > 4))
        {
            return "";
        }

        return reports[rep];
    }

    #endregion
}
