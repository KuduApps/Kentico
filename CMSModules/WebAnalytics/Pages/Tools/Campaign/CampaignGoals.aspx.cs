using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_CampaignGoals : CMSWebAnalyticsPage
{
    #region "Variables"

    private bool isSaved = false;
    private bool reportDisplayed = false;
    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private string statCodeName = String.Empty;
    private string dataCodeName = String.Empty;
    private IDisplayReport ucDisplayReport;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.PanelContent.CssClass = "";

        statCodeName = QueryHelper.GetString("statCodeName", String.Empty);
        dataCodeName = QueryHelper.GetString("dataCodeName", String.Empty);

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlDisplayReport.Controls.Add((Control)ucDisplayReport);

        CheckWebAnalyticsUI(dataCodeName);

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = ResHelper.GetString("WebAnalytics.Disabled");
        }

        // Text for menu buttons
        mSave = GetString("general.save");
        mPrint = GetString("Analytics_Report.Print");

        // Images for menu buttons
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save_small.png");
        imgPrint.ImageUrl = GetImageUrl("General/printSmall.png");

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

        CurrentMaster.Title.TitleText = GetString("analytics_codename.campaign") + " - " + GetString("analytics_codename." + statCodeName);

        // Icon
        string iconName = QueryHelper.GetString("icon", String.Empty);
        string imageUrl = GetImageUrl("CMSModules/CMS_WebAnalytics/Details/" + iconName + ".png");

        if (!FileHelper.FileExists(imageUrl))
        {
            imageUrl = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
        }

        CurrentMaster.Title.TitleImage = imageUrl;
        CurrentMaster.Title.HelpTopicName = Server.UrlEncode(statCodeName).Replace(".", "_");
    }


    /// <summary>
    /// Handles lnkSave click event.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        DisplayReport(false);
        Save();
    }


    /// <summary>
    /// OnPreRender - Display report
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        DisplayReport(true);
        base.OnPreRender(e);
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
    /// Displays the given report
    /// </summary>
    private void DisplayReport(bool reloadInnerReport)
    {
        // If report was already displayed .. return
        if (reportDisplayed)
        {
            return;
        }

        ucGraphType.ProcessChartSelectors(false);

        // Prepare report parameters
        DataTable dtp = new DataTable();

        dtp.Columns.Add("FromDate", typeof(DateTime));
        dtp.Columns.Add("ToDate", typeof(DateTime));
        dtp.Columns.Add("CampaignName", typeof(string));

        object[] parameters = new object[3];

        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = "";

        // Get report name from query
        string reportName = ucGraphType.GetReportName(QueryHelper.GetString("reportCodeName", String.Empty));

        if (String.Compare(Convert.ToString(ucSelectCampaign.Value), "-1", true) != 0)
        {
            parameters[2] = ucSelectCampaign.Value;
        }
        else
        {
            reportName = "all" + reportName;
        }

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();

        ucDisplayReport.ReportName = reportName;

        // Set display report
        if (!ucDisplayReport.IsReportLoaded())
        {
            lblError.Visible = true;
            lblError.Text = String.Format(GetString("Analytics_Report.ReportDoesnotExist"), reportName);
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
            if (reloadInnerReport)
            {
                ucDisplayReport.ReloadData(true);
            }
        }

        // Create onclick event for print button
        this.lnkPrint.OnClientClick = "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + reportName + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + reportName + "', 800, 700); return false;";

        if (reloadInnerReport)
        {
            // Mark as report displayed
            reportDisplayed = true;
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
}

