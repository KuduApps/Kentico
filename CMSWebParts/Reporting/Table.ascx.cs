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
using CMS.FormEngine;
using CMS.WebAnalytics;

public partial class CMSWebParts_Reporting_Table : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets table name.
    /// </summary>
    public string TableName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ReportTable"), "");
        }
        set
        {
            this.SetValue("ReportTable", value);
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
    /// Enables/disables paging for tables
    /// </summary>
    public bool EnablePaging
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnablePaging"), false);
        }
        set
        {
            this.SetValue("EnablePaging", value);
        }
    }


    /// <summary>
    /// Page size for paged tables
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), 0);
        }
        set
        {
            this.SetValue("PageSize", value);
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
            string[] items = TableName.Split(';');
            if ((items != null) && (items.Length == 2))
            {
                ucTable.Parameter = items[0] + "." + items[1];
                ucTable.ReportItemName = items[0] + ";" + items[1];
                ucTable.CacheItemName = CacheItemName;
                ucTable.CacheMinutes = CacheMinutes;
                ucTable.CacheDependencies = CacheDependencies;
                ucTable.ItemType = ReportItemType.Graph;
                ucTable.LoadDefaultParameters(this.ParametersXmlData, this.ParametersXmlSchema);
                ucTable.RangeInterval = RangeInterval;
                ucTable.RangeValue = RangeValue;
                ucTable.EnableExport = EnableExport;
                ucTable.EnablePaging = EnablePaging;
                ucTable.PageSize = PageSize; 
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

        ucTable.ReloadData(true);
    }


    /// <summary>
    /// Prerenders the control.
    /// </summary>    
    protected override void OnPreRender(EventArgs e)
    {
        if (!StopProcessing)
        {
            ucTable.ReloadData(true);
        }

        // Set visibility of current webpart with dependence on graph visibility
        this.Visible = ucTable.Visible;

        base.OnPreRender(e);
    }

    #endregion
}
