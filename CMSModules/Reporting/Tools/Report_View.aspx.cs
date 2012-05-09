using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;

public partial class CMSModules_Reporting_Tools_Report_View : CMSReportingPage
{
    private int reportId = 0;

    private bool isSaved = false;

    public string mSave = "";
    public string mSendToEmail = "";
    public string mPrint = "";


    /// <summary>
    /// OnInit.
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {

        base.OnPreInit(e);

        // Ensure the script manager
        EnsureScriptManager();

        reportId = ValidationHelper.GetInteger(Request.QueryString["ReportId"], 0);
        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportId);
        if (ri != null)
        {
            DisplayReport1.ReportName = ri.ReportName;
        }
    }


    /// <summary>
    /// VerifyRenderingInServerForm.
    /// </summary>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!isSaved)
        {
            base.VerifyRenderingInServerForm(control);
        }
    }


    /// <summary>
    /// Save click handler.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        // Check 'SaveReports' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "SaveReports"))
        {
            RedirectToAccessDenied("cms.reporting", "SaveReports");
        }

        if (!DisplayReport1.ParametersForm.ValidateData())
        {
            return;
        }

        isSaved = true;
        int savedReportId = DisplayReport1.SaveReport();

        if (savedReportId != 0)
        {
            URLHelper.Redirect("SavedReports/SavedReport_View.aspx?reportId=" + savedReportId.ToString() + "&view=1");
        }
    }


    /// <summary>
    /// Send to email click handler.
    /// </summary>
    protected void lnkSend_Click(object sender, EventArgs e)
    {
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterDialogScript(Page);

        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        imgSendTo.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/send.png");
        imgPrint.ImageUrl = GetImageUrl("General/print.png");

        mSave = GetString("general.save");
        mSendToEmail = GetString("Report_General.SendToEmail");
        mPrint = GetString("Report_View.Print");

        imgPrint.AlternateText = mPrint;
        imgSendTo.AlternateText = mSendToEmail;
        imgSave.AlternateText = mSave;

        ltlModal.Text = ScriptHelper.GetScript("function myModalDialog(url, name, width, height) { " +
            "win = window; " +
            "var dHeight = height; var dWidth = width; " +
            "if (( document.all )&&(navigator.appName != 'Opera')) { " +
            "try { win = wopener.window; } catch (e) {} " +
            "if ( parseInt(navigator.appVersion.substr(22, 1)) < 7 ) { dWidth += 4; dHeight += 58; }; " +
            "dialog = win.showModalDialog(url, this, 'dialogWidth:' + dWidth + 'px;dialogHeight:' + dHeight + 'px;resizable:yes;scroll:yes'); " +
            "} else { " +
            "oWindow = win.open(url, name, 'height=' + dHeight + ',width=' + dWidth + ',toolbar=no,directories=no,menubar=no,modal=yes,dependent=yes,resizable=yes,scroll=yes,scrollbars=yes'); oWindow.opener = this; oWindow.focus(); } } ");
    }


    /// <summary>
    /// On PreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.btnPrint.OnClientClick = "myModalDialog('Report_Print.aspx?reportid=" + reportId.ToString() + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(DisplayReport1.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + DisplayReport1.ReportName + "', 650, 700); return false;";
    }
}
