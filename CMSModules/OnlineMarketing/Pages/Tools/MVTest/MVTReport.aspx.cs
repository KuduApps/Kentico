using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.WebAnalytics;


public partial class CMSModules_OnlineMarketing_Pages_Tools_MVTest_MVTReport : CMSMVTestPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private bool isSaved = false;

    private IDisplayReport ucDisplayReport;
    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckWebAnalyticsUI(QueryHelper.GetString("datacodename", String.Empty));

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlDisplayReport.Controls.Add((Control)ucDisplayReport);

        CurrentMaster.PanelContent.CssClass = "";
        UIHelper.AllowUpdateProgress = false;

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
            "?statcodename=mvtest', 'AnalyticsManageData', " + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
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

        if (!MVTestInfoProvider.MVTestingEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblMVTestingDisabled.Text = GetString("mvt.disabled");
        }

        ucMVTests.ReturnColumnName = "MVTestName";
        ucMVTests.AllowEmpty = false;
        ucMVTests.PostbackOnChange = true;
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


    /// <summary>
    /// Displays the report
    /// </summary>
    /// <param name="reload">If true dirplay reload control is reloaded</param>
    private void DisplayReport(bool reload)
    {
        ucGraphType.ProcessChartSelectors(false);

        // Prepare report parameters
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
        parameters[2] = "";
        parameters[3] = ValidationHelper.GetString(ucMVTests.Value, String.Empty);

        // Conversion
        String conversion = ValidationHelper.GetString(ucConversions.Value, String.Empty);
        if (conversion == ucConversions.AllRecordValue)
        {
            conversion = String.Empty;
        }

        parameters[4] = ValidationHelper.GetString(conversion, String.Empty);

        // Combination
        String combination = ValidationHelper.GetString(usCombination.Value, String.Empty);
        if (combination == usCombination.AllRecordValue)
        {
            combination = String.Empty;
        }

        parameters[5] = ValidationHelper.GetString(combination, String.Empty);

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();

        string reportName = ucGraphType.GetReportName(QueryHelper.GetString("reportCodeName", String.Empty));

        // Hide show selectors by report name
        if (reportName.Contains("mvtestconversionsbycombinations"))
        {
            pnlConversion.Visible = false;
        }
        else
        {
            pnlCultures.Visible = false;
            pnlCombination.Visible = false;
        }

        // Load conversion by mvt code name
        if (pnlConversion.Visible)
        {
            ucConversions.MVTestName = ValidationHelper.GetString(ucMVTests.Value, String.Empty);
            ucConversions.PostbackOnDropDownChange = true;
            ucConversions.ReloadData(true);
        }

        // Load combination by mvt name
        if (pnlCombination.Visible)
        {
            string name = ValidationHelper.GetString(ucMVTests.Value, String.Empty);
            if ((name != String.Empty) && (name != ucMVTests.AllRecordValue))
            {
                string cultureWhere = String.Empty;
                MVTestInfo mvt = MVTestInfoProvider.GetMVTestInfo(name, CMSContext.CurrentSiteName);
                if (mvt != null)
                {
                    string culture = ValidationHelper.GetString(usCulture.Value, String.Empty);
                    if ((culture != String.Empty) && (culture != usCulture.AllRecordValue))
                    {
                        cultureWhere = " AND DocumentCulture = '" + culture.Replace("'", "''") + "'";
                    }

                    string where = "MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = " + CMSContext.CurrentSiteID + @"
                            AND NodeAliasPath ='" + mvt.MVTestPage + "'" + cultureWhere + ")";

                    usCombination.WhereCondition = SqlHelperClass.AddWhereCondition(usCombination.WhereCondition, where);
                    usCombination.ReloadData(true);
                }
            }
        }

        // Set report
        ucDisplayReport.ReportName = reportName;

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

            // Reload data only if parameter is set
            if (reload)
            {
                ucDisplayReport.ReloadData(true);
            }
        }
    }

    #endregion
}

