using System;
using System.Data;

using CMS.Reporting;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.WebAnalytics;

public partial class CMSModules_Reporting_Tools_Analytics_Print : CMSWebAnalyticsPage
{
    private string reportName = String.Empty;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Reporting", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Reporting");
        }

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check 'Read' permission
        if (!user.IsAuthorizedPerResource("CMS.Reporting", "Read"))
        {
            RedirectToAccessDenied("CMS.Reporting", "Read");
        }

        IFormatProvider culture = DateTimeHelper.DefaultIFormatProvider;
        IFormatProvider currentCulture = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

        // Get report name from querystring
        reportName = QueryHelper.GetString("reportname", String.Empty);
        // Get ReportInfo object
        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportName);
        if (ri != null)
        {
            // Get parameters from querystring
            string[] httpParameters = QueryHelper.GetString("parameters", String.Empty).Split(";".ToCharArray());

            if (httpParameters.Length > 1)
            {
                string[] parameters = new string[httpParameters.Length / 2];

                DataTable dtp = new DataTable();

                // Create correct columns and put values in it
                for (int i = 0; i < httpParameters.Length; i = i + 2)
                {
                    if (ValidationHelper.GetDateTime(httpParameters[i + 1], DataHelper.DATETIME_NOT_SELECTED, culture) == DataHelper.DATETIME_NOT_SELECTED)
                    {
                        dtp.Columns.Add(httpParameters[i]);
                        parameters[i / 2] = httpParameters[i + 1].Replace(AnalyticsHelper.PARAM_SEMICOLON, ";");
                    }
                    else
                    {
                        dtp.Columns.Add(httpParameters[i], typeof(DateTime));
                        parameters[i / 2] = Convert.ToDateTime(httpParameters[i + 1], culture).ToString(currentCulture);
                    }
                }


                dtp.Rows.Add(parameters);
                dtp.AcceptChanges();

                DisplayReport1.LoadFormParameters = false;
                DisplayReport1.ReportName = ri.ReportName;
                DisplayReport1.DisplayFilter = false;
                DisplayReport1.ReportParameters = dtp.Rows[0];
            }
            else
            {
                DisplayReport1.ReportName = ri.ReportName;
                DisplayReport1.DisplayFilter = false;
            }
            Page.Title = GetString("Report_Print.lblPrintReport") + " " + HTMLHelper.HTMLEncode(ri.ReportDisplayName);
        }
    }


    protected override void OnLoad(EventArgs e)
    {
        ManagersContainer = plcMenu;
    }
}