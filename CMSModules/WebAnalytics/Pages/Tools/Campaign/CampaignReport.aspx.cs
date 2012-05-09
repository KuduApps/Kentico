using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_CampaignReport : CMSWebAnalyticsPage
{
    #region "Variables"

    private bool isSaved = false;
    private bool reportDisplayed = false;
    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private String dataCodeName = String.Empty;
    private String reportCodeNames = String.Empty;

    private string allDetailReport = "campaigns.alldetails";
    private string singleDetailReport = "campaigns.singledetails";
    private IDisplayReport ucDisplayReport;

    private bool allowNoTimeSelection = false;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.PanelContent.CssClass = "";
        UIHelper.AllowUpdateProgress = false;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = ResHelper.GetString("WebAnalytics.Disabled");
        }

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlDisplayReport.Controls.Add((Control)ucDisplayReport);

        // Text for menu buttons
        mSave = GetString("general.save");
        mPrint = GetString("Analytics_Report.Print");
        mDeleteData = GetString("Analytics_Report.ManageData");

        // Images for menu buttons
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save_small.png");
        imgPrint.ImageUrl = GetImageUrl("General/printSmall.png");
        imgManageData.ImageUrl = GetImageUrl("CMSModules/CMS_Reporting/managedataSmall.png");

        dataCodeName = QueryHelper.GetString("dataCodeName", String.Empty);
        reportCodeNames = QueryHelper.GetString("reportCodeName", String.Empty);

        // Control initialization (based on displayed report)
        switch (dataCodeName)
        {
            // Overview
            case "campaign":
                CheckWebAnalyticsUI("campaign.overview");
                ucReportHeader.CampaignAllowAll = true;
                ucReportHeader.ShowConversionSelector = false;
                break;

            // Conversion count 
            case "campconversioncount":
                dataCodeName = "campconversion";
                CheckWebAnalyticsUI("CampaignConversionCount");
                ucReportHeader.CampaignAllowAll = false;
                break;

            // Conversion value 
            case "campconversionvalue":
                dataCodeName = "campconversion";
                CheckWebAnalyticsUI("campaignsConversionValue");
                ucReportHeader.CampaignAllowAll = false;
                break;

            // Campaign compare
            case "campaigncompare":
                CheckWebAnalyticsUI("CampaignCompare");
                ucReportHeader.ShowCampaignSelector = false;
                dataCodeName = ucReportHeader.CodeNameByGoal;
                ucReportHeader.ShowGoalSelector = true;
                ucReportHeader.ShowSiteSelector = true;

                // Get table column name
                string name = "analytics.hits";
                switch (ucReportHeader.SelectedGoal.ToLower())
                {
                    case "view": name = "analytics.view"; break;
                    case "count": name = "conversion.count"; break;
                    case "value": name = "om.trackconversionvalue"; break;
                }

                string[,] dynamicMacros = new string[1, 2];
                dynamicMacros[0, 0] = "ColumnName";
                dynamicMacros[0, 1] = GetString(name);

                ucDisplayReport.DynamicMacros = dynamicMacros;
                break;

            // Campaign detail
            case "campaigndetails":
                CheckWebAnalyticsUI("CampaignDetails");
                ucReportHeader.ShowConversionSelector = false;
                String selectedCampaign = ValidationHelper.GetString(ucReportHeader.SelectedCampaign, String.Empty);
                reportCodeNames = (selectedCampaign == ucReportHeader.AllRecordValue || selectedCampaign == String.Empty) ? allDetailReport : singleDetailReport;
                ucGraphType.ShowIntervalSelector = false;
                allowNoTimeSelection = true;
                ucGraphType.AllowEmptyDate = true;
                break;
        }

        // Set table same first column width for all time
        ucDisplayReport.TableFirstColumnWidth = Unit.Percentage(20);

        // Check 'ManageData' permission
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageData"))
        {
            this.lnkDeleteData.OnClientClick = "modalDialog('" +
            ResolveUrl("~/CMSModules/Reporting/WebAnalytics/Analytics_ManageData.aspx") +
            "?statcodename=campaigns', 'AnalyticsManageData'," + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
            this.lnkDeleteData.Visible = true;
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "myModalDialog",
                ScriptHelper.GetScript("function myModalDialog(url, name, width, height) { " +
                    "win = window; " +
                    "var dHeight = height; var dWidth = width; " +
                    "if (( document.all )&&(navigator.appName != 'Opera')) { " +
                    "try { win = wopener.window; } catch (e) {} " +
                    "if ( parseInt(navigator.appVersion.substr(22, 1)) < 7 ) { dWidth += 4; dHeight += 58; }; " +
                    "dialog = win.showModalDialog(url, this, 'dialogWidth:' + dWidth + 'px;dialogHeight:' + dHeight + 'px;resizable:yes;scroll:yes'); " +
                    "} else { " +
                    "oWindow = win.open(url, name, 'height=' + dHeight + ',width=' + dWidth + ',toolbar=no,directories=no,menubar=no,modal=yes,dependent=yes,resizable=yes,scroll=yes,scrollbars=yes'); oWindow.opener = this; oWindow.focus(); } } "));
    }


    /// <summary>
    /// Displays the given report
    /// </summary>
    private void DisplayReport()
    {
        // If report was already displayed .. return
        if (reportDisplayed)
        {
            return;
        }

        ucGraphType.ProcessChartSelectors(false);

        // Prepare report parameters
        DataTable dtp = new DataTable();

        // In case of hidden datetime -> for save purpose pass from (to) as now to query parameter
        DateTime from = ((ucGraphType.From == DateTimeHelper.ZERO_TIME) && !pnlHeader.Visible) ? DateTime.Now : ucGraphType.From;
        DateTime to = ((ucGraphType.To == DateTimeHelper.ZERO_TIME) && !pnlHeader.Visible) ? DateTime.Now : ucGraphType.To;

        dtp.Columns.Add("FromDate", typeof(DateTime));
        dtp.Columns.Add("ToDate", typeof(DateTime));
        dtp.Columns.Add("CodeName", typeof(string));
        dtp.Columns.Add("CampaignName", typeof(string));
        dtp.Columns.Add("ConversionName", typeof(string));
        dtp.Columns.Add("Goal", typeof(string));
        dtp.Columns.Add("SiteID", typeof(int));

        object[] parameters = new object[7];

        parameters[0] = (allowNoTimeSelection && from == DateTimeHelper.ZERO_TIME) ? (DateTime?)null : from;
        parameters[1] = (allowNoTimeSelection && to == DateTimeHelper.ZERO_TIME) ? (DateTime?)null : to;
        parameters[2] = dataCodeName;
        parameters[3] = "";
        parameters[4] = "";
        parameters[5] = ucReportHeader.SelectedGoal;
        parameters[6] = ucReportHeader.SelectedSiteID;

        // Get report name from query
        String reportName = ucGraphType.GetReportName(reportCodeNames);

        // Filter campaign if any campaign selected
        string campaignName = ValidationHelper.GetString(ucReportHeader.SelectedCampaign, String.Empty);
        if ((campaignName != ucReportHeader.AllRecordValue) && (!String.IsNullOrEmpty(campaignName)))
        {
            parameters[3] = campaignName;
        }

        // Filter conversion
        String conversionName = ValidationHelper.GetString(ucReportHeader.SelectedConversion, String.Empty);
        if ((conversionName != ucReportHeader.AllRecordValue) && (!String.IsNullOrEmpty(conversionName)))
        {
            parameters[4] = conversionName;
        }

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();

        ucDisplayReport.ReportName = reportName;

        // Set display report
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

        // Create onclick event for print button
        this.lnkPrint.OnClientClick = "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + reportName + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + reportName + "', 800, 700); return false;";

        // Mark as report displayed
        reportDisplayed = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        DisplayReport();
        base.OnPreRender(e);
    }


    /// <summary>
    /// Handles lnkSave click event.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        DisplayReport();
        Save();
    }


    /// <summary>
    /// Used in rendering to control outside render stage (save method) 
    /// </summary>
    /// <param name="control">Control</param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!isSaved)
        {
            base.VerifyRenderingInServerForm(control);
        }
    }


    /// <summary>
    /// Saves the graph report.
    /// </summary>
    private void Save()
    {
        // Check web analytics save permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "SaveReports"))
        {
            RedirectToCMSDeskAccessDenied("CMS.WebAnalytics", "SaveReports");
        }

        isSaved = true;

        if (ucDisplayReport.SaveReport() > 0)
        {
            lblInfo.Visible = true;
            lblInfo.Text = String.Format(GetString("Analytics_Report.ReportSavedTo"), ucDisplayReport.ReportDisplayName + " - " + DateTime.Now.ToString());
        }

        isSaved = false;
    }

    #endregion
}

