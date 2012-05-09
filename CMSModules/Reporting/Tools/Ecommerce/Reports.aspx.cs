using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.Reporting;

public partial class CMSModules_Reporting_Tools_Ecommerce_Reports : CMSEcommerceReportingPage
{
    private bool isSaved = false;
    public string mSave = "";
    public string mPrint = "";
    public string reportName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save_small.png");
        imgPrint.ImageUrl = GetImageUrl("General/printSmall.png");

        mSave = GetString("general.save");
        mPrint = GetString("Ecommerce_Report.Print");

        ltlModal.Text = ScriptHelper.GetScript("function myModalDialog(url, name, width, height) { " +
            "win = window; " +
            "var dHeight = height; var dWidth = width; " +
            "if (( document.all )&&(navigator.appName != 'Opera')) { " +
            "try { win = wopener.window; } catch (e) {} " +
            "if ( parseInt(navigator.appVersion.substr(22, 1)) < 7 ) { dWidth += 4; dHeight += 58; }; " +
            "dialog = win.showModalDialog(url, this, 'dialogWidth:' + dWidth + 'px;dialogHeight:' + dHeight + 'px;resizable:yes;scroll:yes'); " +
            "} else { " +
            "oWindow = win.open(url, name, 'height=' + dHeight + ',width=' + dWidth + ',toolbar=no,directories=no,menubar=no,modal=yes,dependent=yes,resizable=yes,scroll=yes,scrollbars=yes'); oWindow.opener = this; oWindow.focus(); } } ");

        string reportCodeName = QueryHelper.GetString("reportcodename", String.Empty);
        string dataCodeName = QueryHelper.GetString("datacodename", String.Empty);
        string statCodeName = QueryHelper.GetString("statcodename", String.Empty);

        // Check UI permissions
        String uiName = "ecreports." + statCodeName;
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("cms.ecommerce", uiName))
        {
            RedirectToCMSDeskAccessDenied("CMS.WebAnalytics", uiName);
        }

        ucReportViewer.DataName = dataCodeName;
        ucReportViewer.ReportsCodeName = reportCodeName;

        imgTitleImage.ImageUrl = GetImageUrl("Objects/Reporting_ReportCategory/object.png");
        imgTitleImage.AlternateText = GetString("analytics_codename." + statCodeName);
        lblTitle.ResourceString = GetString("analytics_codename." + statCodeName);
    }


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        ManagersContainer = plcContainerManager;

        // For purposes 'IsEcommerceReport' its sufficient to get report name of any period (year,month,week)
        string reportCodeName = QueryHelper.GetString("reportCodeName", String.Empty);
        string name = ucReportViewer.GetReportCodeName(reportCodeName);

        // To display filter form control (basic form) need to pass report name before control's init
        // To set fields before basic form control state fills them with actual data
        if (QueryHelper.GetBoolean("displayfilter", false))
        {
            ucReportViewer.LoadFormParameters = true;
            ucReportViewer.DisplayReportBodyClass = "DisplayReportBody";
        }

        // No interval report (only one report in url - pass it now
        if (!reportCodeName.Contains(";"))
        {
            ucReportViewer.ReportName = name;
        }

        this.IsEcommerceReport = ReportInfoProvider.IsEcommerceReport(name);
    }


    /// <summary>
    /// On PreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ucReportViewer.DisplayReport(false);
        reportName = ucReportViewer.ReportName;
        this.lnkPrint.OnClientClick = "myModalDialog('Reports_Print.aspx?reportname=" + ScriptHelper.GetString(reportName, false) + "&parameters=" + ScriptHelper.GetString(ucReportViewer.GetQueryStringParameters(), false) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + ScriptHelper.GetString(ucReportViewer.ReportName, false) + "', 800, 700); return false;";
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
        isSaved = true;

        // Save report - info label displays AnalyticsReportViewer control
        ucReportViewer.SaveReport();

        isSaved = false;
    }
}
