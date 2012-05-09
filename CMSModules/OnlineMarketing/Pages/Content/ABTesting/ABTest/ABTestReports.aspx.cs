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
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SettingsProvider;


public partial class CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABTest_ABTestReports : CMSABTestPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private bool isSaved = false;
    private bool reportLoaded = false;
    string testName = String.Empty;

    private IDisplayReport ucDisplayReport = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlContent.Controls.Add((Control)ucDisplayReport);

        CurrentMaster.PanelContent.CssClass = "";
        UIHelper.AllowUpdateProgress = false;

        // ABTest Info
        int abTestID = QueryHelper.GetInteger("abtestid", 0);
        ABTestInfo abInfo = ABTestInfoProvider.GetABTestInfo(abTestID);
        if (abInfo == null)
        {
            return;
        }

        String siteName = CMSContext.CurrentSiteName;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlWarning.Visible = true;
            this.lblWAWarning.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }

        if (!ABTestInfoProvider.ABTestingEnabled(siteName))
        {
            this.pnlWarning.Visible = true;
            this.lblABWarning.Text = GetString("abtesting.disabled");
        }

        testName = abInfo.ABTestName;

        // Variants condition
        ucSelectVariation.WhereCondition = "ABVariantTestID  IN ( SELECT ABTestID FROM OM_ABTest WHERE ABTestName = N'" + testName + "')";

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
            "?statcodename=abconversion;" + testName + ";%', 'AnalyticsManageData', " + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
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

        ucGraphType.ProcessChartSelectors(false);

        // Enables/disables radio buttons (based on UI elements)
        CurrentUserInfo ui = CMSContext.CurrentUser;

        if (!RequestHelper.IsPostBack())
        {
            if (!ui.IsGlobalAdministrator)
            {
                rbCount.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "ABTest.ConversionsCount");
                rbRate.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "ABTest.ConversionsRate");
                rbValue.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "ABTest.ConversionsValue");
                rbSourcePages.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "ABTest.ConversionsSourcePages");
                rbVariants.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "ABTest.ConversionsByVariations");

                bool checkedButton = false;

                // Check first enabled button 
                foreach (Control ctrl in pnlRadios.Controls)
                {
                    RadioButton rb = ctrl as RadioButton;
                    if (rb != null)
                    {
                        if (rb.Enabled)
                        {
                            rb.Checked = true;
                            checkedButton = true;
                            break;
                        }
                    }
                }

                // No report avaible -> redirect to access denied
                if (!checkedButton)
                {
                    RedirectToAccessDenied(GetString("abtest.noreportavaible"));
                }
            }
            else
            {
                // Admin check first radio button
                rbCount.Checked = true;
            }
        }
    }


    /// <summary>
    /// Display report
    /// </summary>
    /// <param name="reload">If true, display report control is reloaded</param>
    private void DisplayReport(bool reload)
    {
        if (reportLoaded)
        {
            return;
        }

        String siteName = CMSContext.CurrentSiteName;

        // Set repors name
        String conversionCount = "abtestconversioncount.yearreport;abtestconversioncount.monthreport;abtestconversioncount.weekreport;abtestconversioncount.dayreport;abtestconversioncount.hourreport";
        String conversionRate = "abtestdetailconversionrate.yearreport;abtestdetailconversionrate.monthreport;abtestdetailconversionrate.weekreport;abtestdetailconversionrate.dayreport;abtestdetailconversionrate.hourreport";
        String conversionValue = "abtestconversionvalue.yearreport;abtestconversionvalue.monthreport;abtestconversionvalue.weekreport;abtestconversionvalue.dayreport;abtestconversionvalue.hourreport";
        String conversionSourcePages = "abtestconversionsource.yearreport;abtestconversionsource.monthreport;abtestconversionsource.weekreport;abtestconversionsource.dayreport;abtestconversionsource.hourreport";
        String conversionVariant = "abtestconversionsbyvariations.yearreport;abtestconversionsbyvariations.monthreport;abtestconversionsbyvariations.weekreport;abtestconversionsbyvariations.dayreport;abtestconversionsbyvariations.hourreport";

        pnlVariant.Visible = false;

        if (rbCount.Checked)
        {
            CheckWebAnalyticsUI("ABTest.ConversionsCount");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionCount);
        }

        if (rbRate.Checked)
        {
            CheckWebAnalyticsUI("ABTest.ConversionsRate");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionRate);
        }

        if (rbValue.Checked)
        {
            CheckWebAnalyticsUI("ABTest.ConversionsValue");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionValue);
        }

        if (rbSourcePages.Checked)
        {
            CheckWebAnalyticsUI("ABTest.ConversionsSourcePages");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionSourcePages);
        }

        if (rbVariants.Checked)
        {
            CheckWebAnalyticsUI("ABTest.ConversionsByVariations");
            pnlVariant.Visible = true;
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionVariant);
        }

        // Conversion
        ucConversions.PostbackOnDropDownChange = true;
        ucConversions.ABTestName = testName;
        ucConversions.ReloadData(true);

        String conversion = ValidationHelper.GetString(ucConversions.Value, String.Empty);
        if (conversion == ucConversions.AllRecordValue)
        {
            conversion = String.Empty;
        }

        // General report data
        ucDisplayReport.LoadFormParameters = false;
        ucDisplayReport.DisplayFilter = false;
        ucDisplayReport.GraphImageWidth = 100;
        ucDisplayReport.IgnoreWasInit = true;
        ucDisplayReport.TableFirstColumnWidth = Unit.Percentage(30);
        ucDisplayReport.UseExternalReload = true;
        ucDisplayReport.UseProgressIndicator = true;

        // Resolve report macros 
        DataTable dtp = new DataTable();
        dtp.Columns.Add("FromDate", typeof(DateTime));
        dtp.Columns.Add("ToDate", typeof(DateTime));
        dtp.Columns.Add("CodeName", typeof(string));
        dtp.Columns.Add("TestName", typeof(string));
        dtp.Columns.Add("ConversionName", typeof(string));
        dtp.Columns.Add("VariationName", typeof(string));

        object[] parameters = new object[6];
        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = "pageviews";
        parameters[3] = testName;
        parameters[4] = conversion;
        parameters[5] = ValidationHelper.GetString(ucSelectVariation.Value, String.Empty);

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();
        ucDisplayReport.ReportParameters = dtp.Rows[0];

        if (reload)
        {
            ucDisplayReport.ReloadData(true);
        }

        reportLoaded = true;
    }


    /// <summary>
    /// Handles lnkSave click event.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        Save();
    }


    protected override void OnPreRender(EventArgs e)
    {
        DisplayReport(true);
        lnkPrint.OnClientClick = "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + ucDisplayReport.ReportName + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + ucDisplayReport.ReportName + "', 800, 700); return false;";
        base.OnPreRender(e);
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

