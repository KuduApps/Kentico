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
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_OnlineMarketing_Pages_Tools_AbTest_OverView : CMSABTestPage
{
    private IDisplayReport ucTestValuesReport;
    private IDisplayReport ucConversionRateReport;
    private IDisplayReport ucDisplayReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckWebAnalyticsUI("ABTest.Overview");

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlOverview.Controls.Add((Control)ucDisplayReport);

        ucConversionRateReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlConversionRate.Controls.Add((Control)ucConversionRateReport);

        ucTestValuesReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlConversionValue.Controls.Add((Control)ucTestValuesReport);

        listElem.ShowObjectMenu = false;
        CurrentMaster.PanelContent.CssClass = "";
        String siteName = CMSContext.CurrentSiteName;
        UIHelper.AllowUpdateProgress = false;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }

        if (!ABTestInfoProvider.ABTestingEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblABTestingDisabled.Text = GetString("abtesting.disabled");
        }

        listElem.ShowFilter = false;
        listElem.DelayedReload = true;
        listElem.Grid.GridName = "~/CMSModules/OnlineMarketing/Controls/UI/ABTest/ListWithValues.xml";

        lnkTestRate.NavigateUrl = "#";
        lnkTestValue.NavigateUrl = "#";
        lnkTestValue.Attributes["OnClick"] = "if (parent.parent.selectTreeNode != null) parent.parent.selectTreeNode('ABTest.ConversionsValue');parent.location=\"abtest_frameset.aspx?page=conversionsvalue&displayTab=abtesting.conversionsvalue\"";
        lnkTestRate.Attributes["OnClick"] = "if (parent.parent.selectTreeNode != null) parent.parent.selectTreeNode('ABTest.ConversionsRate');parent.location=\"abtest_frameset.aspx?page=conversionsrate&displayTab=abtesting.conversionrate\"";

        ucABTests.AllowAll = true;
        ucABTests.AllowEmpty = false;
        ucABTests.IsLiveSite = false;
        ucABTests.ReturnColumnName = "ABTestName";
        ucABTests.PostbackOnChange = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
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
        parameters[3] = String.Empty;
        parameters[4] = String.Empty;

        String reportName = ucGraphType.GetReportName("abtests.yearreport;abtests.monthreport;abtests.weekreport;abtests.dayreport;abtests.hourreport");
        String conversionRateReportName = ucGraphType.GetReportName("abtestconversionrate.yearreport;abtestconversionrate.monthreport;abtestconversionrate.weekreport;abtestconversionrate.dayreport;abtestconversionrate.hourreport");
        String valuesReportName = ucGraphType.GetReportName("abtestsvalue.yearreport;abtestsvalue.monthreport;abtestsvalue.weekreport;abtestsvalue.dayreport;abtestsvalue.hourreport");

        string ABTestName = ValidationHelper.GetString(ucABTests.Value, String.Empty);        
        if ((ABTestName != ucABTests.AllRecordValue) && (!String.IsNullOrEmpty(ABTestName)))
        {
            parameters[3] = ABTestName;

            listElem.Grid.WhereCondition = SqlHelperClass.AddWhereCondition(listElem.Grid.WhereCondition, "ABTestName='" + SqlHelperClass.GetSafeQueryString(ABTestName, false) + "'");
            listElem.Grid.ReloadData();
        }

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();

        ucDisplayReport.ReportName = reportName;
        ucTestValuesReport.ReportName = valuesReportName;
        ucConversionRateReport.ReportName = conversionRateReportName;

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
            ucDisplayReport.ReloadData(true);
            ucDisplayReport.UseExternalReload = true;
            ucDisplayReport.UseProgressIndicator = true;
        }

        // Conversion value
        if (!ucTestValuesReport.IsReportLoaded())
        {
            lblErrorValues.Visible = true;
            lblErrorValues.Text = String.Format(GetString("Analytics_Report.ReportDoesnotExist"), valuesReportName);
        }
        else
        {
            ucTestValuesReport.LoadFormParameters = false;
            ucTestValuesReport.DisplayFilter = false;
            ucTestValuesReport.ReportParameters = dtp.Rows[0];
            ucTestValuesReport.GraphImageWidth = 50;
            ucTestValuesReport.AreaMaxWidth = ucDisplayReport.AreaMaxWidth;
            ucTestValuesReport.IgnoreWasInit = true;
            ucTestValuesReport.ReloadData(true);
            ucTestValuesReport.UseExternalReload = true;
            ucTestValuesReport.UseProgressIndicator = true;
        }

        // Conversion rate
        if (!ucConversionRateReport.IsReportLoaded())
        {
            lblErrorRate.Visible = true;
            lblErrorRate.Text = String.Format(GetString("Analytics_Report.ReportDoesnotExist"), conversionRateReportName);
        }
        else
        {
            ucConversionRateReport.LoadFormParameters = false;
            ucConversionRateReport.DisplayFilter = false;
            ucConversionRateReport.ReportParameters = dtp.Rows[0];
            ucConversionRateReport.GraphImageWidth = 50;
            ucConversionRateReport.AreaMaxWidth = ucDisplayReport.AreaMaxWidth;
            ucConversionRateReport.UseExternalReload = true;
            ucConversionRateReport.UseProgressIndicator = true;
            ucConversionRateReport.IgnoreWasInit = true;
            ucConversionRateReport.ReloadData(true);
        }

        base.OnPreRender(e);


    }
}

