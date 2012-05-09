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
using CMS.EventLog;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.FormEngine;

public partial class CMSModules_Reporting_Controls_ReportValue : AbstractReportControl
{
    private ReportValueInfo mValueInfo = null;
    private string mParameter = string.Empty;


    /// <summary>
    /// Value info for direct input (no load from DB).
    /// </summary>
    public ReportValueInfo ValueInfo
    {
        get
        {
            if (mValueInfo == null)
            {
                mValueInfo = ReportValueInfoProvider.GetReportValueInfo(Parameter);
            }

            return mValueInfo;
        }
        set
        {
            mValueInfo = value;
        }
    }


    /// <summary>
    /// Value name - prevent using viewstate  (problems with displayreportcontrol and postback).
    /// </summary>
    public override string Parameter
    {
        get
        {
            return mParameter;
        }
        set
        {
            mParameter = value;
        }
    }


    /// <summary>
    /// Returns true if graph belongs to report.
    /// </summary>
    /// <param name="report">Report to validate</param>
    public override bool IsValid(ReportInfo report)
    {
        ReportValueInfo rvi = ValueInfo;

        if ((report != null) && (rvi != null) && (report.ReportID == rvi.ValueReportID))
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData(bool forceLoad)
    {
        try
        {
            // Load value info object
            ReportValueInfo rvi = ValueInfo;

            if (rvi != null)
            {
                //Test security
                ReportInfo ri = ReportInfoProvider.GetReportInfo(rvi.ValueReportID);
                if (ri.ReportAccess != ReportAccessEnum.All)
                {
                    if (!CMSContext.CurrentUser.IsAuthenticated())
                    {
                        this.Visible = false;
                        return;
                    }
                }

                ContextResolver resolver = CMSContext.CurrentResolver;
                // Resolve dynamic data macros
                if (DynamicMacros != null)
                {
                    for (int i = 0; i <= this.DynamicMacros.GetUpperBound(0); i++)
                    {
                        resolver.AddDynamicParameter(DynamicMacros[i, 0], DynamicMacros[i, 1]);
                    }
                }

                this.QueryIsStoredProcedure = rvi.ValueQueryIsStoredProcedure;
                this.QueryText = resolver.ResolveMacros(rvi.ValueQuery);

                //Set default parametrs directly if not set
                if (this.ReportParameters == null)
                {
                    if (ri != null)
                    {
                        FormInfo fi = new FormInfo(ri.ReportParameters);
                        // Get datarow with required columns
                        this.ReportParameters = fi.GetDataRow();
                        fi.LoadDefaultValues(this.ReportParameters, true);
                    }
                }
            }

            // Only use base parameters in case of stored procedure
            if (this.QueryIsStoredProcedure)
            {
                this.AllParameters = SpecialFunctions.ConvertDataRowToParams(this.ReportParameters, null);
            }

            // Load data
            DataSet ds = this.LoadData();

            // If datasource is emptry, create empty dataset
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // Set literal text
                string value = rvi.ValueFormatString;
                if ((value == null) || (value == ""))
                {
                    value = ValidationHelper.GetString(ds.Tables[0].Rows[0][0], "");
                }
                else
                {
                    value = string.Format(value, ds.Tables[0].Rows[0].ItemArray);
                }

                lblValue.Text = HTMLHelper.HTMLEncode(ResolveMacros(value));
            }
        }
        catch (Exception ex)
        {
            // Display error message, if data load fail
            lblError.Visible = true;
            lblError.Text = "[ReportValue.ascx] Error loading the data: " + ex.Message;
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent("Report value", "E", ex);
        }
    }
}
