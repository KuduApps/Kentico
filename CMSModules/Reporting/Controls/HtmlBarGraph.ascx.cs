using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.EventLog;


public partial class CMSModules_Reporting_Controls_HtmlBarGraph : AbstractReportControl
{
    #region "Variables"

    private ReportGraphInfo mReportGraphInfo;
    private string mParameter = String.Empty;
    private ContextResolver itemResolver = null;
    private ReportCustomData reportSettings = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the graph datasource.
    /// </summary>
    public DataSet DataSource
    {
        get;
        set;
    }


    /// <summary>
    /// Returns graph info from DB or from memory.
    /// </summary>
    public ReportGraphInfo ReportGraphInfo
    {
        get
        {
            // If graph info is not set already
            if (mReportGraphInfo == null)
            {
                mReportGraphInfo = ReportGraphInfoProvider.GetReportGraphInfo(Parameter);
            }
            return mReportGraphInfo;
        }
        set
        {
            mReportGraphInfo = value;
        }
    }


    /// <summary>
    /// Graph name - prevent using viewstate  (problems with displayreportcontrol and postback).
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
    /// Gets the graph settings for current report, if report is not defined returns an empty object.
    /// </summary>
    private ReportCustomData ReportSettings
    {
        get
        {
            // If graph info is defined, return graph settings
            if (ReportGraphInfo != null)
            {
                return ReportGraphInfo.GraphSettings;
            }

            // Create empty object
            if (reportSettings == null)
            {
                reportSettings = new ReportCustomData();
            }
            return reportSettings;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Ensures graph HTML code.
    /// </summary>
    protected void EnsureGraph()
    {
        // Get html graph content
        string content = Generate();

        // If graph is not defined display info message
        if (String.IsNullOrEmpty(content))
        {
            // Check whether no data text is defiend
            if (!String.IsNullOrEmpty(this.ReportSettings["QueryNoRecordText"]))
            {
                // Display no data text
                lblInfo.Visible = true;
                lblInfo.Text = HTMLHelper.HTMLEncode(this.ReportSettings["QueryNoRecordText"]);
            }
        }
        // Display graph
        else
        {
            // Set generated HTML to the literal control
            ltlGraph.Text = content;
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    /// <param name="forceLoad">Indicates whether data should be loaded forcibly</param>
    public override void ReloadData(bool forceLoad)
    {
        base.ReloadData(forceLoad);

        if ((GraphImageWidth != 0) && (ComputedWidth == 0))
        {
            // Graph width is computed no need to create graph
            return;
        }

        GetReportGraph(ReportGraphInfo);
        EnsureGraph();
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Register context menu if allowed export
        if ((ReportGraphInfo != null) && ValidationHelper.GetBoolean(ReportGraphInfo.GraphSettings["ExportEnabled"], false))
        {
            RegisterContextMenu(menuCont);
        }
    }


    /// <summary>
    /// Returns report graph.
    /// </summary>
    private void GetReportGraph(ReportGraphInfo reportGraph)
    {
        // Check whether report graph is defined
        if (reportGraph == null)
        {
            return;
        }

        ReportInfo report = ReportInfoProvider.GetReportInfo(reportGraph.GraphReportID);
        //check graph security settings
        if (report.ReportAccess != ReportAccessEnum.All)
        {
            if (!CMSContext.CurrentUser.IsAuthenticated())
            {
                this.Visible = false;
                return;
            }
        }

        //Set default parametrs directly if not set
        if (this.ReportParameters == null)
        {
            //Load ReportInfo            
            if (report != null)
            {
                FormInfo fi = new FormInfo(report.ReportParameters);
                // Get datarow with required columns
                ReportParameters = fi.GetDataRow();
                fi.LoadDefaultValues(ReportParameters, true);
            }
        }

        // Only use base parameters in case of stored procedure
        if (this.QueryIsStoredProcedure)
        {
            this.AllParameters = SpecialFunctions.ConvertDataRowToParams(this.ReportParameters, null);
        }

        // Indicaates whether exception was throw during data loading
        bool errorOccurred = false;

        // Create graph data                    
        try
        {
            ContextResolver resolver = CMSContext.CurrentResolver.CreateContextChild();
            //Resolve parameters in query
            resolver.SourceParameters = this.AllParameters.ToArray();

            // Resolve dynamic data macros
            if (DynamicMacros != null)
            {
                for (int i = 0; i <= this.DynamicMacros.GetUpperBound(0); i++)
                {
                    resolver.AddDynamicParameter(DynamicMacros[i, 0], DynamicMacros[i, 1]);
                }
            }

            //Prepare query attributes
            QueryText = resolver.ResolveMacros(reportGraph.GraphQuery);
            QueryIsStoredProcedure = reportGraph.GraphQueryIsStoredProcedure;

            // Load data
            this.DataSource = LoadData();

        }
        catch (Exception ex)
        {
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent("Get report graph", "E", ex);
            lblError.Text = ex.Message;
            lblError.Visible = true;
            errorOccurred = true;
        }

        // Export data
        if ((report != null) && (!errorOccurred))
        {
            ProcessExport(ValidationHelper.GetCodeName(report.ReportDisplayName));
        }

    }


    /// <summary>
    /// Returns true if graph belongs to report.
    /// </summary>
    /// <param name="report">Report to validate</param>
    public override bool IsValid(ReportInfo report)
    {
        ReportGraphInfo rgi = ReportGraphInfo;
        // Test validity
        if ((report != null) && (rgi != null) && (report.ReportID == rgi.GraphReportID))
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Generates graph html code.
    /// </summary>
    public string Generate()
    {
        // Check whether dataset contains at least one row
        if (DataHelper.DataSourceIsEmpty(this.DataSource))
        {
            return String.Empty;
        }

        #region "max & sum computing"

        // Find max value and sum from current dataset
        double max = 0.0;
        double sum = 0.0;
        // Loop thru all data rows
        foreach (DataRow dr in this.DataSource.Tables[0].Rows)
        {
            // Loop thru all columns
            foreach (DataColumn dc in this.DataSource.Tables[0].Columns)
            {
                // Skip first column with data name
                if (dc.Ordinal > 0)
                {
                    // Get column value
                    double value = ValidationHelper.GetDouble(dr[dc.ColumnName], 0.0);
                    sum += value;
                    // Set max value from current value if is higher than current max value
                    if (max < value)
                    {
                        max = value;
                    }
                }
            }
        }

        #endregion

        // Initialize string builder
        StringBuilder sb = new StringBuilder(1024);
        sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\"ReportBarGraphTable\">");

        bool displayLegend = ValidationHelper.GetBoolean(ReportSettings["DisplayLegend"], false);
        bool headerDisplayed = !String.IsNullOrEmpty(ReportGraphInfo.GraphTitle) || displayLegend;

        #region "Legend/Title row"

        if (headerDisplayed)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine("<td class=\"ReportBarGraphLegend\" colspan=\"2\">");
            // Graph title
            sb.AppendLine("<div class=\"ReportBarGraphTitle FloatLeft\">" + HTMLHelper.HTMLEncode(CMSContext.CurrentResolver.ResolveMacros(ReportGraphInfo.GraphTitle)) + "</div>");

            // Check whether legend should be displayed
            if (displayLegend)
            {
                // Loop thru all columns in reverse order due to similarity with graph representation
                for (int i = this.DataSource.Tables[0].Columns.Count - 1; i > 0; i--)
                {
                    DataColumn dc = this.DataSource.Tables[0].Columns[i];
                    string backColorClass = GetColorClass(i);

                    sb.AppendLine("<div class=\"FloatRight ReportBarGraphLegendItemEnvelope\"> ");
                    sb.AppendLine("<div class=\"FloatLeft ReportBarGraphLegendItem " + backColorClass + " \">&nbsp;</div> ");
                    sb.AppendLine(HTMLHelper.HTMLEncode(dc.ColumnName));
                    sb.AppendLine("</div>");
                }

                string legendText = HTMLHelper.HTMLEncode(ReportSettings["LegendTitle"]);
                if (!String.IsNullOrEmpty(legendText))
                {
                    legendText += ":";
                }

                sb.AppendLine("<div class=\"FloatRight ReportBarGraphLegendTitle\">" + legendText + "</div>");
            }

            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
        }

        #endregion

        string rowClass = "ReportBarGraphNameCellFirst";
        string itemValueFormat = ReportSettings["ItemValueFormat"];

        // Loop thru all data rows
        for (int i = this.DataSource.Tables[0].Rows.Count - 1; i >= 0; i--)
        {
            // Get current datarow
            DataRow dr = this.DataSource.Tables[0].Rows[i];
            sb.AppendLine("<tr>");

            // Loop thru all columns in current dataset
            foreach (DataColumn dc in this.DataSource.Tables[0].Columns)
            {
                // Generate data name cell
                if (dc.Ordinal == 0)
                {
                    sb.AppendLine("<td class=\"ReportBarGraphNameCell " + rowClass + "\">");

                    // Get column name
                    string name = dr[dc.ColumnName].ToString();
                    // Get 
                    string format = ReportSettings["SeriesItemNameFormat"];

                    #region "Item name formating"

                    // Check whether format is defined
                    if (!String.IsNullOrEmpty(format))
                    {
                        // Convert to specific type
                        if (ValidationHelper.IsDateTime(name))
                        {
                            DateTime dt = ValidationHelper.GetDateTime(name, DateTimeHelper.ZERO_TIME);
                            name = dt.ToString(format);
                        }
                        else
                        {
                            name = String.Format(name, format); ;
                        }
                    }

                    #endregion

                    // Name value
                    sb.AppendLine(HTMLHelper.HTMLEncode(name));
                    sb.AppendLine("</td>");
                }
                else
                {
                    // Generate open cell tag
                    if (dc.Ordinal == 1)
                    {
                        sb.AppendLine("<td class=\"ReportBarGraphDataCell " + rowClass + "\">");
                    }
                    // Generate cell data divider
                    else
                    {
                        sb.AppendLine("<div style=\"clear:both\"></div>");
                    }

                    // Default width type
                    string widthType = "%";
                    // Default width value
                    double widthValue = 0.0;
                    // Current value
                    double currentValue = ValidationHelper.GetDouble(dr[dc.ColumnName], 0.0);

                    // If current value is defined compute reltive width
                    if (currentValue > 0.0)
                    {
                        // Relative width
                        widthValue = (currentValue / max) * 80;

                        // If value is defined but relative width is lower than 1, generate simple line
                        if (widthValue < 1)
                        {
                            widthValue = 1;
                            widthType = "px";
                        }


                        // Get background color for current column
                        string backColorClass = GetColorClass(dc.Ordinal);
                        // Item link
                        string itemLink = ReportSettings["SeriesItemLink"];
                        // Item tooltip
                        string itemToolTip = ReportSettings["SeriesItemToolTip"];


                        if (!String.IsNullOrEmpty(itemLink))
                        {
                            sb.AppendLine("<a href=\"" + ResolveUrl(ResolveCustomMacros(itemLink, dr, dc.ColumnName, sum)) + "\">");
                        }

                        if (!String.IsNullOrEmpty(itemToolTip))
                        {
                            itemToolTip = " title=\"" + ResolveCustomMacros(itemToolTip.Replace("\"", String.Empty), dr, dc.ColumnName, sum) + "\"";
                        }

                        // <DIV> - bar
                        sb.AppendLine(@"<div " + itemToolTip + @" class=""FloatLeft ReportBarGraphDataItem " + backColorClass + @""" style="" width:" + (ValidationHelper.GetInteger(Math.Truncate(widthValue), 0)).ToString() + widthType + @""">&nbsp;</div>");

                        // Check whether item value should be displayed
                        if (!String.IsNullOrEmpty(itemValueFormat))
                        {
                            string itemValue = ResolveCustomMacros(itemValueFormat, dr, dc.ColumnName, sum);
                            // <DIV> - item value
                            sb.AppendLine(@"<div " + itemToolTip + @" class=""FloatLeft ReportBarGraphDataItemValue"">" + itemValue + "</div>");
                        }

                        if (!String.IsNullOrEmpty(itemLink))
                        {
                            sb.AppendLine("</a>");
                        }
                    }
                    else
                    {
                        sb.AppendLine(@"<div class=""FloatLeft ReportBarGraphDataItem"" style=""width:1px"">&nbsp;</div>");
                    }
                }
            }

            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");

            // Clear first row class
            rowClass = String.Empty;
        }

        sb.AppendLine("</table>");

        return sb.ToString();
    }


    /// <summary>
    /// Resolve custom macros for specified input value.
    /// </summary>
    /// <param name="value">Value to resolve</param>
    /// <param name="dr">Current data row</param>
    /// <param name="columnName">Column name</param>
    /// <param name="sum">Summary of all items</param>
    private string ResolveCustomMacros(string value, DataRow dr, string columnName, double sum)
    {
        // Ensure resolver
        if (itemResolver == null)
        {
            itemResolver = CMSContext.CurrentResolver.CreateContextChild();
        }

        // Get current item value
        double itemvalue = ValidationHelper.GetDouble(dr[columnName], 0.0);

        // Custom macros definition
        string[,] macros = new string[4, 2];
        macros[0, 0] = "xval";
        macros[0, 1] = Convert.ToString(dr[0]);
        macros[1, 0] = "yval";
        macros[1, 1] = Convert.ToString(itemvalue);
        macros[2, 0] = "ser";
        macros[2, 1] = columnName;
        macros[3, 0] = "pval";
        macros[3, 1] = Convert.ToString(itemvalue / sum * 100);

        // Set custom macros
        itemResolver.SourceParameters = macros;
        // Resolve macros
        return itemResolver.ResolveMacros(value);
    }



    /// <summary>
    /// Returns html color value for specific position.
    /// </summary>
    /// <param name="position">Column position</param>
    private string GetColorClass(int position)
    {
        return "ReportBarGraphItem ReportBarGraphItem" + position;
    }

    #endregion
}
