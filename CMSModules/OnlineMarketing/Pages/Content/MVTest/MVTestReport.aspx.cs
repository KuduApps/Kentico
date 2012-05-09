using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_Pages_Content_MVTest_MVTestReport : CMSOnlineMarketingPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private bool isSaved = false;
    private bool reportLoaded = false;
    private string testName = String.Empty;
    private IDisplayReport ucDisplayReport = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI Permissions
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "OnlineMarketing.MVTests")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "OnlineMarketing.MVTests");
        }

        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.MVTesting);
        }
        
        // Check features and display warning
        String siteName = CMSContext.CurrentSiteName;
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlWarning.Visible = true;
            this.lblWAWarning.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }

        if (!MVTestInfoProvider.MVTestingEnabled(siteName))
        {
            this.pnlWarning.Visible = true;
            this.lblMVWarning.Text = GetString("mvt.disabled");
        }

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlContent.Controls.Add((Control)ucDisplayReport);

        CurrentMaster.PanelContent.CssClass = "";
        UIHelper.AllowUpdateProgress = false;

        // MVTest Info
        int mvTestID = QueryHelper.GetInteger("mvTestID", 0);
        MVTestInfo mvInfo = MVTestInfoProvider.GetMVTestInfo(mvTestID);
        if (mvInfo == null)
        {
            return;
        }

        // Load combination by current template ID and culture
        if (pnlCombination.Visible)
        {
            int nodeID = QueryHelper.GetInteger("NodeID", 0);

            CurrentUserInfo cui = CMSContext.CurrentUser;

            // Create condition for current template combinations            
            TreeProvider tree = new TreeProvider(cui);
            TreeNode node = tree.SelectSingleNode(nodeID, cui.PreferredCultureCode);

            if (node != null)
            {
                usCombination.DocumentID = node.DocumentID;
                usCombination.PageTemplateID = node.DocumentPageTemplateID;
            }

            usCombination.ReloadData(true);
        }

        testName = mvInfo.MVTestName;

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
            "?statcodename=mvtconversion;" + testName + ";%', 'AnalyticsManageData', " + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
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
                rbCount.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "MVTestConversionCount");
                rbRate.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "MVTestConversionRate");
                rbValue.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "MVTestConversionValue");
                rbCombinations.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "ConversionsByCombination");

                bool checkedButton = false;

                // Check first enabled button 
                foreach (Control ctrl in pnlRadioButtons.Controls)
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
                    RedirectToAccessDenied(GetString("mvtest.noreportavaible"));
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
        String conversionCount = "mvtestconversioncount.yearreport;mvtestconversioncount.monthreport;mvtestconversioncount.weekreport;mvtestconversioncount.dayreport;mvtestconversioncount.hourreport";
        String conversionRate = "mvtestconversionrate.yearreport;mvtestconversionrate.monthreport;mvtestconversionrate.weekreport;mvtestconversionrate.dayreport;mvtestconversionrate.hourreport";
        String conversionValue = "mvtestconversionvalue.yearreport;mvtestconversionvalue.monthreport;mvtestconversionvalue.weekreport;mvtestconversionvalue.dayreport;mvtestconversionvalue.hourreport";
        String conversionCombinations = "mvtestconversionsbycombinations.yearreport;mvtestconversionsbycombinations.monthreport;mvtestconversionsbycombinations.weekreport;mvtestconversionsbycombinations.dayreport;mvtestconversionsbycombinations.hourreport";

        pnlCombination.Visible = false;
        // Set proper report name
        if (rbCount.Checked)
        {
            CheckWebAnalyticsUI("MVTestConversionCount");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionCount);
        }

        if (rbRate.Checked)
        {
            CheckWebAnalyticsUI("MVTestConversionRate");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionRate);
        }

        if (rbValue.Checked)
        {
            CheckWebAnalyticsUI("MVTestConversionValue");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionValue);
        }

        if (rbCombinations.Checked)
        {
            CheckWebAnalyticsUI("ConversionsByCombination");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionCombinations);
            pnlCombination.Visible = true;
        }

        // Conversion
        ucConversions.PostbackOnDropDownChange = true;
        ucConversions.MVTestName = testName;
        ucConversions.ReloadData(true);

        // Conversion
        String conversion = ValidationHelper.GetString(ucConversions.Value, String.Empty);
        if (conversion == ucConversions.AllRecordValue)
        {
            conversion = String.Empty;
        }

        // Combination
        String combination = ValidationHelper.GetString(usCombination.Value, String.Empty);
        if (combination == usCombination.AllRecordValue)
        {
            combination = String.Empty;
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
        dtp.Columns.Add("MVTestName", typeof(string));
        dtp.Columns.Add("ConversionName", typeof(string));
        dtp.Columns.Add("CombinationName", typeof(string));

        object[] parameters = new object[6];
        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = "pageviews";
        parameters[3] = testName;
        parameters[4] = conversion;
        parameters[5] = combination;

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

