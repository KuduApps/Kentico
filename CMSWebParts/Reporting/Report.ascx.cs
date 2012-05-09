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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.Reporting;

public partial class CMSWebParts_Reporting_Report : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the code name of the report, which should be displayed.
    /// </summary>
    public string ReportName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ReportName"), this.viewReport.ReportName);
        }
        set
        {
            this.SetValue("ReportName", value);
        }
    }


    /// <summary>
    /// Determines whether to show parameter filter or not.
    /// </summary>
    public bool DisplayFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayFilter"), false);
        }
        set
        {
            this.SetValue("DisplayFilter", value);
        }
    }


    /// <summary>
    /// Gets or sets the XML schema of parameters dataset.
    /// </summary>
    public string ParametersXmlSchema
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ParametersXmlSchema"), String.Empty);
        }
        set
        {
            this.SetValue("ParametersXmlSchema", value);
        }
    }


    /// <summary>
    /// Gets or sets chart name in format reportname;itemname.
    /// </summary>
    public string ParametersXmlData
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ParametersXmlData"), String.Empty);
        }
        set
        {
            this.SetValue("ParametersXmlData", value);
        }
    }


    /// <summary>
    /// Indicates whether enable export
    /// </summary>
    public bool EnableExport
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableExport"), false);
        }
        set
        {
            this.SetValue("EnableExport", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.SetContext();
            SetupBasicData();

            this.viewReport.ReloadData(true);
            this.ReleaseContext();
            this.Visible = this.viewReport.Visible;
        }
    }


    /// <summary>
    /// Setup basit settings for view report.
    /// </summary>
    private void SetupBasicData()
    {
        this.viewReport.DisplayFilter = this.DisplayFilter;
        this.viewReport.ReportName = this.ReportName;
        this.viewReport.EnableExport = EnableExport;

        if (!this.DisplayFilter && !String.IsNullOrEmpty(this.ParametersXmlData))
        {
            this.viewReport.LoadDefaultParameters(this.ParametersXmlData, this.ParametersXmlSchema);
            this.viewReport.LoadFormParameters = false;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupBasicData();

        this.viewReport.ForceLoadDefaultValues = true;
        this.viewReport.IgnoreWasInit = true;

        this.viewReport.ReloadData(true);
    }


    protected override void OnPreRender(EventArgs e)
    {
        //Security check
        this.Visible = this.viewReport.Visible;
        base.OnPreRender(e);
    }
}
