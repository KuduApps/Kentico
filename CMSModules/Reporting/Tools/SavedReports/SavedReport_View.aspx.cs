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
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.CMSHelper;
using CMS.UIControls;


public partial class CMSModules_Reporting_Tools_SavedReports_SavedReport_View : CMSReportingPage
{
    public string mPrint = "";
    public string mSendToEmail = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((!RequestHelper.IsPostBack()) && (QueryHelper.GetInteger("View", 0) == 1))
        {
            ltlScript.Text += ScriptHelper.GetScript("parent.frames['reportHeader'].SetSavedTab()");
        }

        // Initialize controls
        imgPrint.ImageUrl = GetImageUrl("General/print.png");
        imgSendTo.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/send.png");

        mSendToEmail = GetString("SavedReport_View.SendToEmail");
        mPrint = GetString("Report_View.Print");
        
        imgPrint.AlternateText = mPrint;
        imgSendTo.AlternateText = mSendToEmail;

        // Creates SavedReport obj
        int savedReportId = ValidationHelper.GetInteger(Request.QueryString["reportId"], 0);
        SavedReportInfo sri = SavedReportInfoProvider.GetSavedReportInfo(savedReportId);
        if (sri != null)
        {
            ReportInfo ri = ReportInfoProvider.GetReportInfo(sri.SavedReportReportID);
            if (ri != null)
            {
                // Initialize pagetitle breadcrumbs
                string[,] pageTitleTabs = new string[2, 3];
                pageTitleTabs[0, 0] = ri.ReportDisplayName;
                pageTitleTabs[0, 1] = "~/CMSModules/Reporting/Tools/SavedReports/SavedReports_List.aspx?reportid=" + ri.ReportID.ToString();
                pageTitleTabs[0, 2] = "";
                pageTitleTabs[1, 0] = sri.SavedReportTitle;
                pageTitleTabs[1, 1] = "";
                pageTitleTabs[1, 2] = "";
                PageTitle.Breadcrumbs = pageTitleTabs;
            }

            // Backward compatibility
            // Check whether url is relative and check whether url to get report graph page is correct
            string savedReportHtml = ReportInfoProvider.GetReportGraphUrlRegExp.Replace(sri.SavedReportHTML, RepairURL);
            ltlHtml.Text = HTMLHelper.ResolveUrls(savedReportHtml, ResolveUrl("~/"));
        }

        ltlModal.Text = ScriptHelper.GetScript("function myModalDialog(url, name, width, height) { " +
            "win = window; " +
            "var dHeight = height; var dWidth = width; " +
            "if (( document.all )&&(navigator.appName != 'Opera')) { " +
            "try { win = wopener.window; } catch (e) {} " +
            "if ( parseInt(navigator.appVersion.substr(22, 1)) < 7 ) { dWidth += 4; dHeight += 58; }; " +
            "dialog = win.showModalDialog(url, this, 'dialogWidth:' + dWidth + 'px;dialogHeight:' + dHeight + 'px;resizable:yes;scroll:yes'); " +
            "} else { " +
            "oWindow = win.open(url, name, 'height=' + dHeight + ',width=' + dWidth + ',toolbar=no,directories=no,menubar=no,modal=yes,dependent=yes,resizable=yes,scroll=yes,scrollbars=yes'); oWindow.opener = this; oWindow.focus(); } } ");

        this.btnPrint.OnClientClick = "myModalDialog('SavedReport_Print.aspx?reportid=" + savedReportId.ToString() + "', 'SavedReportPrint', 650, 700); return false;";
    }


    /// <summary>
    /// Repair URL if is with application path or if is not relative.
    /// </summary>
    /// <param name="m">Match</param>
    public string RepairURL(Match m)
    {
        return m.Groups[1].Value + "~/CMSModules/Reporting/CMSPages/GetReportGraph.aspx";
    }
}
