using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WebAnalytics;

public partial class CMSModules_WebAnalytics_Tools_Analytics_Report : CMSWebAnalyticsPage
{
    #region "Variables"

    private bool isSaved = false;
    private string statCodeName;

    public string mSave = null;
    public string mPrint = null;
    public string mDeleteData = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check analytics UI
        CheckWebAnalyticsUI();

        CurrentMaster.PanelContent.CssClass = "";
        ScriptHelper.RegisterDialogScript(this.Page);

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = ResHelper.GetString("WebAnalytics.Disabled");
        }

        UIHelper.AllowUpdateProgress = false;

        // Text for menu
        mSave = GetString("general.save");
        mPrint = GetString("Analytics_Report.Print");
        mDeleteData = GetString("Analytics_Report.ManageData");

        // Images for menu buttons
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save_small.png");
        imgPrint.ImageUrl = GetImageUrl("General/printSmall.png");
        imgManageData.ImageUrl = GetImageUrl("CMSModules/CMS_Reporting/managedataSmall.png");

        statCodeName = QueryHelper.GetString("statCodeName", String.Empty);
        string reportCodeName = QueryHelper.GetString("reportCodeName", String.Empty);
        string dataCodeName = QueryHelper.GetString("dataCodeName", String.Empty);

        ucReportViewer.DataName = dataCodeName;
        ucReportViewer.ReportsCodeName = reportCodeName;

        // Check 'ManageData' permission
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageData"))
        {
            this.lnkDeleteData.OnClientClick = "modalDialog('" +
            ResolveUrl("~/CMSModules/Reporting/WebAnalytics/Analytics_ManageData.aspx") +
            "?statcodename=" + dataCodeName + "', 'AnalyticsManageData', " + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
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

        bool displayTitle = QueryHelper.GetBoolean("DisplayTitle", true);
        if (displayTitle)
        {
            CurrentMaster.Title.TitleText = GetString("analytics_codename." + statCodeName);
            string imageUrl = GetImageUrl("CMSModules/CMS_WebAnalytics/Details/" + dataCodeName + ".png");

            if (!FileHelper.FileExists(imageUrl))
            {
                imageUrl = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
            }

            CurrentMaster.Title.TitleImage = imageUrl;


            if (!QueryHelper.GetBoolean("IsCustom", false))
            {
                CurrentMaster.Title.HelpTopicName = Server.UrlEncode(statCodeName).Replace(".", "_");
            }
        }
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


    protected override void OnPreRender(EventArgs e)
    {
        ucReportViewer.DisplayReport(false);
        this.lnkPrint.OnClientClick = "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + ucReportViewer.ReportName + "&parameters=" + HttpUtility.HtmlEncode(ucReportViewer.GetQueryStringParameters()) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + ucReportViewer.ReportName + "', 800, 700); return false;";
        base.OnPreRender(e);
    }


    /// <summary>
    /// Handles lnkSave click event.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        Save();
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

        // Saves the report 
        ucReportViewer.SaveReport();

        isSaved = false;

    }

    #endregion
}

