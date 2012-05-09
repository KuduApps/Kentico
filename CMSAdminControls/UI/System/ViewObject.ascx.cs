using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;
using System.Xml;

using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.UIControls;
using CMS.Controls;

public partial class CMSAdminControls_UI_System_ViewObject : CMSAdminEditControl
{
    #region "Variables"

    protected object mObject = null;

    // List of tables [DataTable, UniGridPager]
    protected ArrayList tables = new ArrayList();

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the object to display.
    /// </summary>
    public object Object
    {
        get
        {
            return mObject;
        }
        set
        {
            mObject = value;
            ReloadData();
        }
    }

    #endregion"


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        ReloadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        WriteTablesContent();
    }


    public override void ReloadData()
    {
        tables.Clear();
        pnlContent.Controls.Clear();

        WriteObject(this.Object);
    }


    protected void WriteObject(object obj)
    {
        // Write the objects
        if (obj != null)
        {
            if (obj == DBNull.Value)
            {
                WriteObjectType("DBNull.Value");
            }
            else if (obj is DataView)
            {
                WriteObjectType(obj.ToString());
                WriteDataTable(((DataView)obj).Table);
            }
            else if (obj is DataTable)
            {
                WriteObjectType(obj.ToString());
                WriteDataTable((DataTable)obj);
            }
            else if (obj is DataSet)
            {
                WriteObjectType(obj.ToString());
                WriteDataSet((DataSet)obj);
            }
            else if (obj is IDataContainer)
            {
                WriteObjectType(obj.ToString());
                WriteDataContainer((IDataContainer)obj);
            }
            else if (obj is string)
            {
                WriteObjectType("string");

                string content = "<div><textarea style=\"width: 100%; height: 530px;\">" + HttpUtility.HtmlEncode((string)obj) + "</textarea></div>";
                pnlContent.Controls.Add(new LiteralControl(content));
            }
            else if (obj is object[])
            {
                pnlContent.Controls.Add(new LiteralControl("<h3><strong>Object type: </strong>" + obj.ToString() + "</h3>"));

                // Write all objects in array
                foreach (object child in (object[])obj)
                {
                    WriteObject(child);
                }
            }
            else
            {
                WriteObjectType(obj.ToString());

                string objString = DataHelper.GetObjectString(obj);
                if (objString != obj.ToString())
                {
                    this.pnlContent.Controls.Add(new LiteralControl("<div>" + objString + "</div>"));
                }
            }
        }
        pnlContent.Controls.Add(new LiteralControl("<br /><br />"));
    }


    protected void WriteTablesContent()
    {
        foreach (object[] table in tables)
        {
            // Prepare the components
            DataTable dt = (DataTable)table[0];
            LiteralControl ltlContent = (LiteralControl)table[1];
            UniGridPager pagerElem = (UniGridPager)table[2];
            UniPagerConnector connectorElem = (UniPagerConnector)table[3];

            // Handle the different types of direct page selector
            int currentPageSize = pagerElem.CurrentPageSize;
            if (currentPageSize > 0)
            {
                if ((float)connectorElem.PagerForceNumberOfResults / (float)currentPageSize > 20.0f)
                {
                    pagerElem.DirectPageControlID = "txtPage";
                }
                else
                {
                    pagerElem.DirectPageControlID = "drpPage";
                }
            }

            // Bind the pager first
            connectorElem.RaiseOnPageBinding(null, null);

            // Prepare the string builder
            StringBuilder sb = new StringBuilder();

            // Prepare the indexes for paging
            int pageSize = pagerElem.CurrentPageSize;

            int startIndex = (pagerElem.CurrentPage - 1) * pageSize + 1;
            int endIndex = startIndex + pageSize;

            // Process all items
            int index = 0;
            bool all = (endIndex <= startIndex);

            if (dt.Columns.Count > 6)
            {
                // Write all rows
                foreach (DataRow dr in dt.Rows)
                {
                    index++;
                    if (all || (index >= startIndex) && (index < endIndex))
                    {
                        sb.Append("<table class=\"UniGridGrid\" cellspacing=\"0\" cellpadding=\"3\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\" width=\"100%\">");

                        // Add header
                        sb.Append("<tr class=\"UniGridHead\"><th>" + GetString("General.FieldName") + "</th><th style=\"width: 100%;\">" + GetString("General.Value") + "</th></tr>");

                        // Add values
                        int i = 0;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            object value = dr[dc.ColumnName];

                            // Binary columns
                            string content = null;
                            if ((dc.DataType == typeof(byte[])) && (value != DBNull.Value))
                            {
                                byte[] data = (byte[])value;
                                content = "<" + GetString("General.BinaryData") + ", " + DataHelper.GetSizeString(data.Length) + ">";
                            }
                            else
                            {
                                content = ValidationHelper.GetString(value, "");
                            }

                            if (!String.IsNullOrEmpty(content))
                            {
                                ++i;
                                string className = ((i % 2) == 0) ? "OddRow" : "EvenRow";
                                sb.Append("<tr class=\"" + className + "\"><td>");
                                sb.Append("<strong>" + dc.ColumnName + "</strong>");
                                sb.Append("</td><td style=\"width: 100%;\">");

                                // Possible DataTime columns
                                if ((dc.DataType == typeof(DateTime)) && (value != DBNull.Value))
                                {
                                    DateTime dateTime = Convert.ToDateTime(content);
                                    System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
                                    content = dateTime.ToString(cultureInfo);
                                }

                                // Process content
                                ProcessContent(sb, dr, dc.ColumnName, ref content);

                                sb.Append("</td></tr>");
                            }
                        }

                        sb.Append("</table>\n");
                    }
                }
            }
            else
            {
                sb.Append("<table class=\"UniGridGrid\" cellspacing=\"0\" cellpadding=\"3\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\" width=\"100%\">");

                // Add header
                sb.Append("<tr class=\"UniGridHead\">");
                int h = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    string style = null;
                    if (h == dt.Columns.Count)
                    {
                        style = "style=\"width:100%;\"";
                    }
                    sb.Append("<th " + style + " >" + column.ColumnName + "</th>");
                    h++;
                }
                sb.Append("</tr>");

                // Write all rows
                foreach (DataRow dr in dt.Rows)
                {
                    index++;
                    if (all || (index >= startIndex) && (index < endIndex))
                    {
                        string className = ((index % 2) == 0) ? "OddRow" : "EvenRow";

                        sb.Append("<tr class=\"");
                        sb.Append(className);
                        sb.Append("\">");

                        // Add values
                        foreach (DataColumn dc in dt.Columns)
                        {
                            object value = dr[dc.ColumnName];
                            // Possible DataTime columns
                            if ((dc.DataType == typeof(DateTime)) && (value != DBNull.Value))
                            {
                                DateTime dateTime = Convert.ToDateTime(value);
                                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
                                value = dateTime.ToString(cultureInfo);
                            }

                            string content = ValidationHelper.GetString(value, "");
                            content = HTMLHelper.HTMLEncode(content);

                            string cellStyle = null;
                            if (content.Length < 100)
                            {
                                cellStyle = " style=\"white-space:nowrap;\"";
                            }
                            sb.Append("<td" + cellStyle + ">" + content + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                }
                sb.Append("</table>\n");
            }

            ltlContent.Text = sb.ToString();
        }
    }


    /// <summary>
    /// Writes the object type to the output.
    /// </summary>
    /// <param name="dt">Object type</param>
    protected void WriteObjectType(string objectType)
    {
        pnlContent.Controls.Add(new LiteralControl("<div style=\"margin-bottom:5px\"><strong>Object type: </strong>" + objectType + "</div>"));
    }

    /// <summary>
    /// Writes the table content to the output.
    /// </summary>
    /// <param name="dt">Table to write</param>
    protected void WriteDataTable(DataTable dt)
    {
        pnlContent.Controls.Add(new LiteralControl("<h3>" + dt.TableName + "</h3>"));

        if (!DataHelper.DataSourceIsEmpty(dt))
        {
            // Add literal for content
            LiteralControl ltlContent = new LiteralControl();
            pnlContent.Controls.Add(ltlContent);

            // Add control for pager
            UniGridPager pagerElem = (UniGridPager)LoadControl("~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx");
            pagerElem.ID = dt.TableName + "_pager";
            pagerElem.PageSizeOptions = "1,2,5,10,25,50,100,##ALL##";
            if (dt.Columns.Count > 10)
            {
                pagerElem.DefaultPageSize = 1;
            }
            else if (dt.Columns.Count > 6)
            {
                pagerElem.DefaultPageSize = 2;
            }
            pnlContent.Controls.Add(pagerElem);

            // Add pager connector
            UniPagerConnector connectorElem = new UniPagerConnector();
            connectorElem.PagerForceNumberOfResults = dt.Rows.Count;
            pagerElem.PagedControl = connectorElem;
            pnlContent.Controls.Add(connectorElem);

            tables.Add(new object[] { dt, ltlContent, pagerElem, connectorElem });

        }
    }


    /// <summary>
    /// Writes the table content to the output.
    /// </summary>
    /// <param name="ds">DataSet to write</param>
    protected void WriteDataSet(DataSet ds)
    {
        // Write all tables
        foreach (DataTable dt in ds.Tables)
        {
            WriteDataTable(dt);
        }
    }


    /// <summary>
    /// Writes the data container to the output.
    /// </summary>
    /// <param name="dc">Container to write</param>
    protected void WriteDataContainer(IDataContainer dc)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table class=\"UniGridGrid\" cellspacing=\"0\" cellpadding=\"3\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\" width=\"100%\">");

        // Add header
        sb.Append("<tr class=\"UniGridHead\"><th>" + GetString("General.FieldName") + "</th><th style=\"width: 100%;\">" + GetString("General.Value") + "</th></tr>");

        // Add values
        int i = 0;
        foreach (string column in dc.ColumnNames)
        {
            try
            {
                object value = dc.GetValue(column);

                // Binary columns
                string content = null;
                if (value is byte[])
                {
                    byte[] data = (byte[])value;
                    content = "<" + GetString("General.BinaryData") + ", " + DataHelper.GetSizeString(data.Length) + ">";
                }
                else
                {
                    content = ValidationHelper.GetString(value, "");
                }

                if (!String.IsNullOrEmpty(content))
                {
                    ++i;
                    string className = ((i % 2) == 0) ? "OddRow" : "EvenRow";
                    sb.Append("<tr class=\"" + className + "\"><td>");
                    sb.Append("<strong>" + column + "</strong>");
                    sb.Append("</td><td style=\"width: 100%;\">");

                    // Possible DataTime columns
                    if (value is DateTime)
                    {
                        DateTime dateTime = Convert.ToDateTime(content);
                        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
                        content = dateTime.ToString(cultureInfo);
                    }

                    // Process content
                    ProcessContent(sb, dc, column, ref content);

                    sb.Append("</td></tr>");
                }
            }
            catch
            {
            }
        }

        sb.Append("</table><br />\n");

        pnlContent.Controls.Add(new LiteralControl(sb.ToString()));
    }


    /// <summary>
    /// Processes the content.
    /// </summary>
    /// <param name="sb">StringBuilder to write</param>
    /// <param name="source">Source object</param>
    /// <param name="column">Column</param>
    /// <param name="content">Content</param>
    protected void ProcessContent(StringBuilder sb, object source, string column, ref string content)
    {
        bool standard = true;
        switch (column.ToLower())
        {
            // Document content
            case "documentcontent":
                EditableItems items = new EditableItems();
                items.LoadContentXml(content);

                // Add regions
                foreach (DictionaryEntry region in items.EditableRegions)
                {
                    sb.Append("<span class=\"VersionEditableRegionTitle\">" + (string)region.Key + "</span>");

                    string regionContent = HTMLHelper.ResolveUrls((string)region.Value, URLHelper.ApplicationPath);

                    sb.Append("<span class=\"VersionEditableRegionText\">" + regionContent + "</span>");
                }

                // Add web parts
                foreach (DictionaryEntry part in items.EditableWebParts)
                {
                    sb.Append("<span class=\"VersionEditableWebPartTitle\">" + (string)part.Key + "</span>");

                    string regionContent = HTMLHelper.ResolveUrls((string)part.Value, URLHelper.ApplicationPath);
                    sb.Append("<span class=\"VersionEditableWebPartText\">" + regionContent + "</span>");
                }

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
                content = HTMLHelper.ReformatHTML(content);
                break;

            // File columns
            case "metafilename":
                {
                    Guid metaFileGuid = ValidationHelper.GetGuid(GetValueFromSource(source, "MetaFileGuid"), Guid.Empty);
                    if (metaFileGuid != Guid.Empty)
                    {
                        string metaFileName = ValidationHelper.GetString(GetValueFromSource(source, "MetaFileName"), "");

                        content = "<a href=\"" + ResolveUrl(MetaFileInfoProvider.GetMetaFileUrl(metaFileGuid, metaFileName)) + "\" target=\"_blank\" >" + HTMLHelper.HTMLEncode(metaFileName) + "</a>";
                        sb.Append(content);

                        standard = false;
                    }
                }
                break;
        }

        // Standard rendering
        if (standard)
        {
            if (content.Length > 500)
            {
                content = TextHelper.EnsureMaximumLineLength(content, 50, "&#x200B;", true);
            }
            else
            {
                content = HTMLHelper.HTMLEncode(content);
            }

            content = TextHelper.EnsureLineEndings(content, "<br />");
            content = content.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.Append("<div style=\"max-height: 300px; overflow: auto;\">" + content + "</div>");
        }
    }


    /// <summary>
    /// Gets the value from given source.
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="column">Column name</param>
    protected object GetValueFromSource(object source, string column)
    {
        if (source is DataRow)
        {
            DataRow dr = (DataRow)source;
            return dr[column];

        }
        else if (source is IDataContainer)
        {
            IDataContainer dc = (IDataContainer)source;
            return dc.GetValue(column);
        }

        return null;
    }
}
