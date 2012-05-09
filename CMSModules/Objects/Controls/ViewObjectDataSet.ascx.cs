using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Objects_Controls_ViewObjectDataSet : CMSUserControl
{
    #region "Variables"

    private bool mShowAllFields = false;
    private bool mShowLinksForMetafiles = false;
    private bool mForceRowDisplayFormat = false;
    private bool mEncodeDisplayedData = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// DataSet containing data to display.
    /// </summary>
    public DataSet DataSet
    {
        get;
        set;
    }


    /// <summary>
    /// DataSet containgind data to be compared.
    /// </summary>
    public DataSet CompareDataSet
    {
        get;
        set;
    }


    /// <summary>
    /// Additional HTML content to display.
    /// </summary>
    public string AdditionalContent
    {
        get
        {
            return ltlAdditionalContent.Text;
        }
        set
        {
            ltlAdditionalContent.Text = value;
        }
    }


    /// <summary>
    /// Gets table control displaying DataSets.
    /// </summary>
    public Table Table
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if all fields should be displayed(even if value is empty).
    /// </summary>
    public bool ShowAllFields
    {
        get
        {
            return mShowAllFields;
        }
        set
        {
            mShowAllFields = value;
        }
    }


    /// <summary>
    /// List of excluded table names separated by semicolon (;)
    /// </summary>
    public string ExcludedTableNames
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if link to Metafile preview should be displayed instead of simple metafile name
    /// </summary>
    public bool ShowLinksForMetafiles
    {
        get
        {
            return mShowLinksForMetafiles;
        }
        set
        {
            mShowLinksForMetafiles = value;
        }
    }


    /// <summary>
    /// Force display tables content in rows instead of separated tables
    /// </summary>
    public bool ForceRowDisplayFormat
    {
        get
        {
            return mForceRowDisplayFormat;
        }
        set
        {
            mForceRowDisplayFormat = value;
        }
    }


    /// <summary>
    /// Indicates if displayed data should be encoded
    /// </summary>
    public bool EncodeDisplayedData
    {
        get
        {
            return mEncodeDisplayedData;
        }
        set
        {
            mEncodeDisplayedData = value;
        }
    }


    /// <summary>
    /// Object type of given data (optional)
    /// </summary>
    public string ObjectType
    {
        get;
        set;
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Display DataSet content.
    /// </summary>
    /// <param name="ds">DataSet to display</param>
    private void DisplayDataSet(DataSet ds)
    {
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            // Prepare list of tables
            string excludedTableNames = (ExcludedTableNames != null) ? ";" + ExcludedTableNames.Trim(';').ToLower() + ";" : "";
            SortedDictionary<string, string> tables = new SortedDictionary<string, string>();
            foreach (DataTable dt in ds.Tables)
            {
                if (!DataHelper.DataSourceIsEmpty(dt))
                {
                    string tableName = dt.TableName;
                    if (!excludedTableNames.Contains(";" + tableName.ToLower() + ";"))
                    {
                        tables.Add(GetString("ObjectType." + tableName), tableName);
                    }
                }
            }

            // Generate the tables
            foreach (string tableName in tables.Values)
            {
                DataTable dt = ds.Tables[tableName];
                if (!DataHelper.DataSourceIsEmpty(dt))
                {
                    if (ds.Tables.Count > 1)
                    {
                        plcContent.Controls.Add(new LiteralControl(GetTableHeaderText(dt)));
                    }

                    Table contentTable = null;

                    if (!ForceRowDisplayFormat && (dt.Columns.Count >= 6) && !dt.TableName.Equals("ObjectTranslation", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Write all rows
                        foreach (DataRow dr in dt.Rows)
                        {
                            contentTable = new Table();
                            SetTable(contentTable);

                            // Set first table as table property
                            if (Table == null)
                            {
                                Table = contentTable;
                            }

                            // Create table header
                            TableCell labelCell = new TableHeaderCell();
                            labelCell.Text = GetString("General.FieldName");

                            TableCell valueCell = new TableHeaderCell();
                            valueCell.Text = GetString("General.Value");

                            AddRow(contentTable, labelCell, valueCell, null, "UniGridHead", false);

                            // Add values
                            bool even = false;
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string content = GetRowColumnContent(dr, dc, false);

                                if (ShowAllFields || !String.IsNullOrEmpty(content))
                                {
                                    labelCell = new TableCell();
                                    labelCell.Text = "<strong>" + dc.ColumnName + "</strong>";

                                    valueCell = new TableCell();
                                    valueCell.Text = content;
                                    AddRow(contentTable, labelCell, valueCell, null, null, even);
                                    even = !even;
                                }
                            }

                            plcContent.Controls.Add(contentTable);
                            plcContent.Controls.Add(new LiteralControl("<br />"));
                        }
                    }
                    else
                    {
                        contentTable = new Table();
                        SetTable(contentTable);

                        // Add header
                        TableRow tr = new TableRow();
                        tr.CssClass = "UniGridHead";

                        int h = 1;
                        foreach (DataColumn column in dt.Columns)
                        {
                            TableHeaderCell th = new TableHeaderCell();
                            th.Text = ValidationHelper.GetString(column.Caption, column.ColumnName);
                            th.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                            if (!ForceRowDisplayFormat && (h == dt.Columns.Count))
                            {
                                th.Style.Add(HtmlTextWriterStyle.Width, "100%");
                            }
                            tr.Cells.Add(th);
                            h++;
                        }
                        contentTable.Rows.Add(tr);

                        // Write all rows
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            ++i;
                            tr = new TableRow();
                            tr.CssClass = ((i % 2) == 0) ? "OddRow" : "EvenRow";

                            // Add values
                            foreach (DataColumn dc in dt.Columns)
                            {
                                TableCell tc = new TableCell();
                                object value = dr[dc.ColumnName];

                                // Possible DataTime columns
                                if ((dc.DataType == typeof(DateTime)) && (value != DBNull.Value))
                                {
                                    DateTime dateTime = Convert.ToDateTime(value);
                                    System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
                                    value = dateTime.ToString(cultureInfo);
                                }

                                string content = ValidationHelper.GetString(value, "");
                                tc.Text = EncodeDisplayedData ? HTMLHelper.HTMLEncode(content) : content;

                                if (!ForceRowDisplayFormat)
                                {
                                    tc.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                                }
                                tr.Cells.Add(tc);
                            }
                            contentTable.Rows.Add(tr);
                        }
                        plcContent.Controls.Add(contentTable);
                        plcContent.Controls.Add(new LiteralControl("<br />"));
                    }
                }
            }
        }
        else
        {
            Label lblError = new Label();
            lblError.CssClass = "InfoLabel";
            lblError.Text = GetString("general.nodatafound");
            plcContent.Controls.Add(lblError);
        }
    }


    /// <summary>
    /// Gets header text for given table
    /// </summary>
    /// <param name="table">Table to get the header for</param>
    /// <returns>HTML representing header text</returns>
    private string GetTableHeaderText(DataTable table)
    {
        string tableName = table.TableName;

        string defaultString = null;
        // ### Special cases
        if (!string.IsNullOrEmpty(ObjectType))
        {
            if (ObjectType.StartsWith(PredefinedObjectType.CUSTOM_TABLE_ITEM_PREFIX))
            {
                defaultString = "CustomTableData";
            }
            else if (ObjectType == PredefinedObjectType.DOCUMENT)
            {
                defaultString = "DocumentData";
            }
            else
            {
                defaultString = tableName;
            }
        }
        return "<h3>" + ResHelper.GetString("ObjectType." + tableName, Thread.CurrentThread.CurrentUICulture.ToString(), GetString("ObjectType." + defaultString)) + "</h3>";
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public void ReloadData()
    {
        if (!DataHelper.DataSourceIsEmpty(DataSet) && !DataHelper.DataSourceIsEmpty(CompareDataSet))
        {
            CompareDataSets(DataSet, CompareDataSet);
        }
        else
        {
            DisplayDataSet(DataSet);
        }
    }

    /// <summary>
    /// Compare DataSets.
    /// </summary>
    /// <param name="ds">Original DataSet</param>
    /// <param name="compareDs">DataSet to compare</param>
    private void CompareDataSets(DataSet ds, DataSet compareDs)
    {
        Table = new Table();
        SetTable(Table);
        Table.CssClass += " NoSideBorders";

        // Ensure same tables in DataSets
        EnsureSameTables(ds, compareDs);
        EnsureSameTables(compareDs, ds);

        // Prepare list of tables
        SortedDictionary<string, string> tables = new SortedDictionary<string, string>();
        foreach (DataTable dt in ds.Tables)
        {
            string excludedTableNames = (ExcludedTableNames != null) ? ";" + ExcludedTableNames.Trim(';').ToLower() + ";" : "";
            string tableName = dt.TableName;
            if (!DataHelper.DataSourceIsEmpty(ds.Tables[tableName]) || !DataHelper.DataSourceIsEmpty(CompareDataSet.Tables[tableName]))
            {
                if (!excludedTableNames.Contains(";" + tableName.ToLower() + ";"))
                {
                    tables.Add(GetString("ObjectType." + tableName), tableName);
                }
            }
        }

        // Generate the tables
        foreach (string tableName in tables.Values)
        {
            DataTable dt = ds.Tables[tableName].Copy();
            DataTable dtCompare = CompareDataSet.Tables[tableName].Copy();

            if (dt.PrimaryKey.Length <= 0)
            {
                continue;
            }

            // Add table heading
            if ((tables.Count > 1) || (ds.Tables.Count > 1))
            {
                AddTableHeaderRow(Table, GetTableHeaderText(dt), ((Table.Rows.Count > 0) ? "TableSeparator " : "") + "NoSideBorders");
            }

            while (dt.Rows.Count > 0 || dtCompare.Rows.Count > 0)
            {
                // Add table header row
                TableCell labelCell = new TableHeaderCell();
                labelCell.Text = GetString("General.FieldName");
                TableCell valueCell = new TableHeaderCell();
                valueCell.Text = GetString("General.Value");
                TableCell valueCompare = new TableHeaderCell();
                valueCompare.Text = GetString("General.Value");

                AddRow(Table, labelCell, valueCell, valueCompare, "UniGridHead", false);

                DataRow srcDr = null;
                DataRow dstDr = null;

                if ((tables.Count == 1) && (dt.Rows.Count == 1) && (dtCompare.Rows.Count == 1))
                {
                    srcDr = dt.Rows[0];
                    dstDr = dtCompare.Rows[0];
                }
                else
                {
                    if (!DataHelper.DataSourceIsEmpty(dt))
                    {
                        srcDr = dt.Rows[0];
                        dstDr = dtCompare.Rows.Find(GetPrimaryColumnsValue(dt, srcDr));
                    }
                    else
                    {
                        dstDr = dtCompare.Rows[0];
                        srcDr = dt.Rows.Find(GetPrimaryColumnsValue(dtCompare, dstDr));
                    }

                    // If match not find, try to find in guid column
                    if ((srcDr == null) || (dstDr == null))
                    {
                        DataTable dtToSearch = null;
                        DataRow drTocheck = null;

                        if (srcDr == null)
                        {
                            dtToSearch = dt;
                            drTocheck = dstDr;
                        }
                        else
                        {
                            dtToSearch = dtCompare;
                            drTocheck = srcDr;
                        }


                        GeneralizedInfo infoObj = CMSObjectHelper.GetObject(drTocheck, dt.TableName.Replace("_", "."));
                        if ((infoObj != null) && ((infoObj.CodeNameColumn != TypeInfo.COLUMN_NAME_UNKNOWN) || (infoObj.GUIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN)))
                        {
                            DataRow[] rows = dtToSearch.Select(infoObj.CodeNameColumn + "='" + drTocheck[infoObj.CodeNameColumn] + "'");
                            if (rows.Length > 0)
                            {
                                if (srcDr == null)
                                {
                                    srcDr = rows[0];
                                }
                                else
                                {
                                    dstDr = rows[0];
                                }
                            }
                            else
                            {
                                rows = dtToSearch.Select(infoObj.GUIDColumn + "='" + drTocheck[infoObj.GUIDColumn] + "'");
                                if (rows.Length > 0)
                                {
                                    if (srcDr == null)
                                    {
                                        srcDr = rows[0];
                                    }
                                    else
                                    {
                                        dstDr = rows[0];
                                    }
                                }
                            }
                        }
                    }
                }

                // Add values
                bool even = false;
                foreach (DataColumn dc in dt.Columns)
                {
                    // Get content values
                    string fieldContent = GetRowColumnContent(srcDr, dc, true);
                    string fieldCompareContent = GetRowColumnContent(dstDr, dc, true);

                    if (ShowAllFields || !String.IsNullOrEmpty(fieldContent) || !String.IsNullOrEmpty(fieldCompareContent))
                    {
                        // Initialize comparators
                        TextComparison comparefirst = new TextComparison();
                        comparefirst.SynchronizedScrolling = false;
                        comparefirst.ComparisonMode = TextComparisonModeEnum.PlainTextWithoutFormating;
                        comparefirst.EnsureHTMLLineEndings = true;

                        TextComparison comparesecond = new TextComparison();
                        comparesecond.SynchronizedScrolling = false;
                        comparesecond.RenderingMode = TextComparisonTypeEnum.DestinationText;
                        comparesecond.EnsureHTMLLineEndings = true;

                        comparefirst.PairedControl = comparesecond;

                        // Set comparator content 
                        comparefirst.SourceText = fieldContent;
                        comparefirst.DestinationText = fieldCompareContent;

                        // Create set of cells
                        labelCell = new TableCell();
                        labelCell.Text = "<strong>" + dc.ColumnName + "</strong>";
                        valueCell = new TableCell();
                        valueCell.Controls.Add(comparefirst);
                        valueCompare = new TableCell();
                        valueCompare.Controls.Add(comparesecond);

                        // Add comparison row
                        AddRow(Table, labelCell, valueCell, valueCompare, null, even);
                        even = !even;
                    }
                }

                // Remove rows from tables
                if (srcDr != null)
                {
                    dt.Rows.Remove(srcDr);
                }
                if (dstDr != null)
                {
                    dtCompare.Rows.Remove(dstDr);
                }

                if (dt.Rows.Count > 0 || dtCompare.Rows.Count > 0)
                {
                    TableCell emptyCell = new TableCell();
                    emptyCell.Text = "&nbsp;";
                    AddRow(Table, emptyCell, null, null, "TableSeparator", false);
                    even = false;
                }
            }
        }
        plcContent.Controls.Add(Table);
    }


    /// <summary>
    /// Creates 3 column table row.
    /// </summary>
    /// <param name="table">Table element</param>
    /// <param name="labelCell">Cell with label</param>
    /// <param name="valueCell">Cell with content</param>
    /// <param name="compareCell">Cell with compare content</param>
    /// <param name="cssClass">CSS class</param>
    /// <param name="even">Determines whether row is odd or even</param>
    /// <returns>Returns TableRow object</returns>
    private TableRow AddRow(Table table, TableCell labelCell, TableCell valueCell, TableCell compareCell, string cssClass, bool even)
    {
        TableRow newRow = new TableRow();

        if (!String.IsNullOrEmpty(cssClass))
        {
            newRow.CssClass = cssClass;
        }
        else if (even)
        {
            newRow.CssClass = "EvenRow";
        }
        else
        {
            newRow.CssClass = "OddRow";
        }

        labelCell.Wrap = false;
        labelCell.Width = new Unit(20, UnitType.Percentage);
        newRow.Cells.Add(labelCell);

        int cellWidth = (compareCell != null) ? 40 : 80;

        if (valueCell != null)
        {
            valueCell.Width = new Unit(cellWidth, UnitType.Percentage);
            newRow.Cells.Add(valueCell);
        }

        if (compareCell != null)
        {
            compareCell.Width = new Unit(cellWidth, UnitType.Percentage);
            newRow.Cells.Add(compareCell);
        }

        table.Rows.Add(newRow);
        return newRow;
    }


    /// <summary>
    /// Sets table properties to ensure same design.
    /// </summary>
    /// <param name="table"></param>
    private void SetTable(Table table)
    {
        table.CellPadding = 3;
        table.CellSpacing = 0;
        table.CssClass = "UniGridGrid";
        table.Width = new Unit(100, UnitType.Percentage);
        table.GridLines = GridLines.Horizontal;
    }


    /// <summary>
    /// Ensure that compared DataSet will contain same tables as source DataSet.
    /// </summary>
    /// <param name="ds">Source DataSet</param>
    /// <param name="dsCompare">Compared DataSet</param>
    private void EnsureSameTables(DataSet ds, DataSet dsCompare)
    {
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            DataTable procTable = ds.Tables[i];

            if (!dsCompare.Tables.Contains(procTable.TableName))
            {
                dsCompare.Tables.Add(procTable.Clone());
            }
        }
    }


    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="table">Table to which header will be added</param>
    /// <param name="text">Header text</param>
    /// <param name="cssClass">Css class of header</param>
    /// <returns>TableRow with header text</returns>
    private TableRow AddTableHeaderRow(Table table, string text, string cssClass)
    {
        TableRow newRow = new TableRow();

        if (!String.IsNullOrEmpty(cssClass))
        {
            newRow.CssClass = cssClass;
        }

        TableCell cell = new TableCell();
        cell.ColumnSpan = 3;
        cell.Text = text;

        newRow.Cells.Add(cell);
        table.Rows.Add(newRow);
        return newRow;
    }


    /// <summary>
    /// Gets row column content.
    /// </summary>
    /// <param name="dr">DataRow</param>
    /// <param name="dc">DataColumn</param>
    /// <param name="toCompare">Indicates if comparison will be used for content</param>
    /// <returns>String with column content</returns>
    private string GetRowColumnContent(DataRow dr, DataColumn dc, bool toCompare)
    {
        if (dr != null)
        {
            object value = dr[dc.ColumnName];
            if (!DataHelper.IsEmpty(value))
            {
                string content = null;

                // Binary columns
                if ((dc.DataType == typeof(byte[])) && (value != DBNull.Value))
                {
                    byte[] data = (byte[])dr[dc.ColumnName];
                    content = "<" + GetString("General.BinaryData") + ": " + DataHelper.GetSizeString(data.Length) + ">";
                }
                else
                {
                    content = ValidationHelper.GetString(value, "");
                }

                // Possible DataTime columns
                if ((dc.DataType == typeof(DateTime)) && (value != DBNull.Value))
                {
                    DateTime dateTime = Convert.ToDateTime(content);
                    System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
                    content = dateTime.ToString(cultureInfo);
                }

                bool standard = true;
                switch (dc.ColumnName.ToLower())
                {
                    // Document content
                    case "documentcontent":
                        EditableItems items = new EditableItems();
                        items.LoadContentXml(ValidationHelper.GetString(value, ""));
                        StringBuilder sb = new StringBuilder();
                        // Add regions
                        foreach (DictionaryEntry region in items.EditableRegions)
                        {
                            if (toCompare)
                            {
                                sb.AppendLine((string)region.Key);
                            }
                            else
                            {
                                sb.Append("<span class=\"VersionEditableRegionTitle\">" + (string)region.Key + "</span>");
                            }
                            string regionContent = HTMLHelper.ResolveUrls((string)region.Value, URLHelper.ApplicationPath);

                            if (toCompare)
                            {
                                sb.AppendLine(regionContent);
                            }
                            else
                            {
                                sb.Append("<span class=\"VersionEditableRegionText\">" + HTMLHelper.HTMLEncode(regionContent) + "</span>");
                            }
                        }

                        // Add web parts
                        foreach (DictionaryEntry part in items.EditableWebParts)
                        {
                            if (toCompare)
                            {
                                sb.AppendLine((string)part.Key);
                            }
                            else
                            {
                                sb.Append("<span class=\"VersionEditableWebPartTitle\">" + (string)part.Key + "</span>");
                            }

                            string regionContent = HTMLHelper.ResolveUrls((string)part.Value, URLHelper.ApplicationPath);
                            if (toCompare)
                            {
                                sb.AppendLine(regionContent);
                            }
                            else
                            {
                                sb.Append("<span class=\"VersionEditableWebPartText\">" + HTMLHelper.HTMLEncode(regionContent) + "</span>");
                            }
                        }

                        content = sb.ToString();
                        standard = false;
                        break;

                    // XML columns
                    case "pagetemplatewebparts":
                    case "webpartproperties":
                    case "reportparameters":
                    case "classformdefinition":
                    case "classxmlschema":
                    case "classformlayout":
                    case "userdialogsconfiguration":
                    case "siteinvoicetemplate":
                    case "userlastlogoninfo":
                    case "formdefinition":
                    case "formlayout":
                    case "uservisibility":
                    case "classsearchsettings":
                    case "graphsettings":
                    case "tablesettings":
                    case "transformationhierarchicalxml":
                    case "issuetext":
                    case "savedreportparameters":

                    // HTML columns
                    case "emailtemplatetext":
                    case "templatebody":
                    case "templateheader":
                    case "templatefooter":
                    case "containertextbefore":
                    case "containertextafter":
                    case "savedreporthtml":
                    case "layoutcode":
                    case "webpartlayoutcode":
                    case "transformationcode":
                    case "reportlayout":
                        if (BrowserHelper.IsIE())
                        {
                            content = HTMLHelper.ReformatHTML(content, " ");
                        }
                        else
                        {
                            content = HTMLHelper.ReformatHTML(content);
                        }
                        break;

                    // File columns
                    case "metafilename":
                        {
                            string metaFileName = HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["MetaFileName"], ""));

                            if (ShowLinksForMetafiles)
                            {
                                Guid metaFileGuid = ValidationHelper.GetGuid(dr["MetaFileGuid"], Guid.Empty);
                                if (metaFileGuid != Guid.Empty)
                                {
                                    content = "<a href=\"" + ResolveUrl(MetaFileInfoProvider.GetMetaFileUrl(metaFileGuid, metaFileName)) + "\" target=\"_blank\" >" + metaFileName + "</a>";
                                }
                            }
                            else
                            {
                                content = metaFileName;
                            }
                            standard = false;
                        }
                        break;
                }

                // Standard rendering
                if (!toCompare)
                {
                    if (standard)
                    {
                        if (EncodeDisplayedData)
                        {
                            content = HTMLHelper.HTMLEncode(content);
                        }
                        content = content.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
                        content = "<div style=\"max-height: 300px; overflow: auto;\">" + content + "</div>";
                    }

                    // Ensure line ending
                    content = TextHelper.EnsureLineEndings(content, "<br />");
                }

                return content;
            }
        }
        return String.Empty;
    }


    private object[] GetPrimaryColumnsValue(DataTable dt, DataRow dr)
    {
        if (dt.PrimaryKey.Length > 0)
        {
            object[] columns = new object[dt.PrimaryKey.Length];
            for (int i = 0; i < dt.PrimaryKey.Length; i++)
            {
                columns[i] = dr[dt.PrimaryKey[i].ColumnName];
            }
            return columns;
        }
        return null;
    }

    #endregion

}