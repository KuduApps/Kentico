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

using CMS.Reporting;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.FormEngine;

public partial class CMSModules_Reporting_Tools_Report_Print : CMSReportingModalPage
{
    private int reportId = 0;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        SetCulture();

        IFormatProvider culture = DateTimeHelper.DefaultIFormatProvider;
        IFormatProvider currentCulture = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);

        reportId = ValidationHelper.GetInteger(Request.QueryString["reportid"], 0);
        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportId);
        if (ri != null)
        {
            string[] httpParameters = ValidationHelper.GetString(Request.QueryString["parameters"], "").Split(";".ToCharArray());

            FormInfo fi = new FormInfo(ri.ReportParameters);            
            // Get datarow with required columns
            DataRow dr = fi.GetDataRow();

            if (httpParameters.Length > 1)
            {
                string[] parameters = new string[httpParameters.Length / 2];

                // Put values to given data row
                for (int i = 0; i < httpParameters.Length; i = i + 2)
                {
                    if (dr.Table.Columns.Contains(httpParameters[i]))
                    {
                        if (dr.Table.Columns[httpParameters[i]].DataType != typeof(DateTime))
                        {
                            dr[httpParameters[i]] = httpParameters[i + 1];
                        }
                        else
                        {
                            if (httpParameters[i + 1] != String.Empty)
                            {
                                dr[httpParameters[i]] = Convert.ToDateTime(httpParameters[i + 1], culture).ToString(currentCulture);
                            }
                        }
                    }
                }

                dr.AcceptChanges();

                DisplayReport1.LoadFormParameters = false;
                DisplayReport1.ReportName = ri.ReportName;
                DisplayReport1.DisplayFilter = false;
                DisplayReport1.ReportParameters = dr;
            }
            else
            {
                DisplayReport1.ReportName = ri.ReportName;
                DisplayReport1.DisplayFilter = false;
            }
            this.Page.Title = GetString("Report_Print.lblPrintReport") + " " + ri.ReportDisplayName;
        }
    }


    protected override void OnLoad(EventArgs e)
    {
        ManagersContainer = pnlManager;
    }
}