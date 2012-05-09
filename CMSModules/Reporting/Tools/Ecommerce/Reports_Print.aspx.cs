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
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.Reporting;

public partial class CMSModules_Reporting_Tools_Ecommerce_Reports_Print : CMSModalPage
{
    private string reportName = "";

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        CMSEcommerceReportingPage.CheckSecurity();

        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Ecommerce", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Ecommerce");
        }
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Reporting", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Reporting");
        }

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Get report name from querystring
        reportName = QueryHelper.GetString("reportname", "");

        // Check module permissions
        if (ReportInfoProvider.IsEcommerceReport(reportName))
        {
            if (!user.IsAuthorizedPerResource("CMS.Ecommerce", "EcommerceRead") && !user.IsAuthorizedPerResource("CMS.Ecommerce", "ReadReports"))
            {
                RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceRead OR ReadReports");
            }
        }
        else
        {
            if (!user.IsAuthorizedPerResource("CMS.Reporting", "Read"))
            {
                RedirectToAccessDenied("CMS.Reporting", "Read");
            }
        }

        // Get parameters from querystring
        string[] httpParameters = QueryHelper.GetString("parameters", "").Split(";".ToCharArray());

        if (httpParameters.Length > 1)
        {
            string[] parameters = new string[httpParameters.Length / 2];

            DataTable dtp = new DataTable();

            // Create correct columns and put values in it
            for (int i = 0; i < httpParameters.Length; i = i + 2)
            {
                dtp.Columns.Add(httpParameters[i]);
                parameters[i / 2] = httpParameters[i + 1];
            }


            dtp.Rows.Add(parameters);
            dtp.AcceptChanges();

            DisplayReport1.LoadFormParameters = false;
            DisplayReport1.ReportName = reportName;
            DisplayReport1.DisplayFilter = false;
            DisplayReport1.ReportParameters = dtp.Rows[0];
        }
        else
        {
            DisplayReport1.ReportName = reportName;
            DisplayReport1.DisplayFilter = false;
        }
        this.Page.Title = GetString("Report_Print.lblPrintReport") + " " + reportName;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ManagersContainer = pnlManager;
    }
}