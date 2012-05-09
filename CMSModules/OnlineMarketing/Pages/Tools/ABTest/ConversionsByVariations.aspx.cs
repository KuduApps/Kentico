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
using CMS.OnlineMarketing;

public partial class CMSModules_OnlineMarketing_Pages_Tools_ABTest_ConversionsByVariations : CMSABTestPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private bool isSaved = false;
    private bool reportDisplayed = false;
    IDisplayReport ucDisplayReport;

    #endregion

    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckWebAnalyticsUI("ABTest.ConversionsByVariations");
        CurrentMaster.PanelContent.CssClass = "";
        UIHelper.AllowUpdateProgress = false;

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlDisplayReport.Controls.Add((Control)ucDisplayReport);

        String siteName = CMSContext.CurrentSiteName;

        // Text for menu
        mSave = GetString("general.save");
        mPrint = GetString("Analytics_Report.Print");
        mDeleteData = GetString("Analytics_Report.ManageData");

        // Images for menu buttons
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save_small.png");
        imgPrint.ImageUrl = GetImageUrl("General/printSmall.png");
        imgManageData.ImageUrl = GetImageUrl("CMSModules/CMS_Reporting/managedataSmall.png");

        // Check 'ManageData' permission
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageData"))
        {
            this.lnkDeleteData.OnClientClick = "modalDialog('" +
            ResolveUrl("~/CMSModules/Reporting/WebAnalytics/Analytics_ManageData.aspx") +
            "?statcodename=abtest', 'AnalyticsManageData'," + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
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

        ucABTests.ReturnColumnName = "ABTestName";
        ucABTests.AllowEmpty = false;
        ucABTests.ReloadData(false);
        ucABTests.PostbackOnChange = true;
        ucABTests.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
        ucSelectVariation.WhereCondition = GetWhereCondition();
        ucSelectVariation.ShowAllVariations = true;
    }


    /// <summary>
    /// VerifyRenderingInServerForm.
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
    /// Returns where condition for variation selector.
    /// </summary>
    private string GetWhereCondition()
    {
        string testName = ValidationHelper.GetString(ucABTests.Value, String.Empty);
        return "ABVariantTestID  IN ( SELECT ABTestID FROM OM_ABTest WHERE ABTestName = N'" + SqlHelperClass.GetSafeQueryString(testName, false) + "')";
    }


    void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        ucSelectVariation.WhereCondition = GetWhereCondition();
        ucSelectVariation.ReloadData(true);
    }


    /// <summary>
    /// Handles lnkSave click event.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        Save();
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
        dtp.Columns.Add("VariationName", typeof(string));

        object[] parameters = new object[5];

        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = "abtests";
        parameters[3] = ValidationHelper.GetString(ucABTests.Value, String.Empty);
        parameters[4] = ValidationHelper.GetString(ucSelectVariation.Value, String.Empty);

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();

        String reportName = ucGraphType.GetReportName("abtestconversionsbyvariations.yearreport;abtestconversionsbyvariations.monthreport;abtestconversionsbyvariations.weekreport;abtestconversionsbyvariations.dayreport;abtestconversionsbyvariations.hourreport");
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
        lnkPrint.OnClientClick = "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + ucDisplayReport.ReportName + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + ucDisplayReport.ReportName + "', 800, 700); return false;";
        base.OnPreRender(e);
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

        DisplayReport(false);

        // Saves the report        
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

