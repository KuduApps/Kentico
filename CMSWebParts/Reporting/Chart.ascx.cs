using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.Reporting;
using CMS.IO;
using CMS.WebAnalytics;

public partial class CMSWebParts_Reporting_Chart : CMSAbstractWebPart
{

    #region "Properties"

    /// <summary>
    /// Gets or sets chart name in format reportname;itemname.
    /// </summary>
    public string ChartName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ReportChart"), String.Empty);
        }
        set
        {
            this.SetValue("ReportChart", value);
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
    /// Gets or sets width of graph.
    /// </summary>
    public String Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Width"), String.Empty);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Interval of time range.
    /// </summary>
    public HitsIntervalEnum RangeInterval
    {
        get
        {
            return HitsIntervalEnumFunctions.StringToHitsConversion(ValidationHelper.GetString(this.GetValue("Range"), "year"));
        }
        set
        {
            this.SetValue("Range", HitsIntervalEnumFunctions.HitsConversionToString(value));
        }
    }


    /// <summary>
    /// Value of range interval.
    /// </summary>
    public int RangeValue
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RangeValue"), 0);
        }
        set
        {
            this.SetValue("RangeValue", value);
        }
    }


    /// <summary>
    /// Gets or sets height of graph.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Height"), 0);
        }
        set
        {
            this.SetValue("Height", value);
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



    /// <summary>
    /// Graph possible width of control.
    /// </summary>
    public int AreaMaxWidth
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["AreaMaxWidth"], 0);
        }
        set
        {
            ViewState["AreaMaxWidth"] = value;
        }
    }

    #endregion


    #region "Methods"

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
            string[] items = ChartName.Split(';');
            if ((items != null) && (items.Length == 2))
            {

                ucGraph.Parameter = items[0] + "." + items[1];
                ucGraph.ReportItemName = items[0] + ";" + items[1];
                ucGraph.CacheItemName = CacheItemName;
                ucGraph.CacheMinutes = CacheMinutes;
                ucGraph.CacheDependencies = CacheDependencies;
                ucGraph.Width = Width;
                ucGraph.Height = Height;
                ucGraph.ItemType = ReportItemType.Graph;
                ucGraph.LoadDefaultParameters(this.ParametersXmlData, this.ParametersXmlSchema);
                ucGraph.RangeInterval = RangeInterval;
                ucGraph.RangeValue = RangeValue;
                ucGraph.EnableExport = EnableExport;
            }
        }
    }



    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }


    /// <summary>
    /// OnPreRender override - set visibility.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Set visibility of current webpart with dependence on graph visibility
        this.Visible = ucGraph.Visible;

        if (AreaMaxWidth != 0)
        {
            ucGraph.ComputedWidth = AreaMaxWidth;
        }

        ucGraph.ReloadData(true);

        if (ucGraph.ComputedWidth != 0)
        {
            AreaMaxWidth = ucGraph.ComputedWidth;
        }

        base.OnPreRender(e);
    }

    #endregion
}
