using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;

public partial class CMSAdminControls_ObjectDataViewer : CMSAdminEditControl
{
    #region "Variables"

    private string mObjectType = null;
    private int mObjectID = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets object type.
    /// </summary>
    public string ObjectType
    {
        get
        {
            return mObjectType;
        }
        set
        {
            mObjectType = value;
        }
    }


    /// <summary>
    /// Gets or sets object ID.
    /// </summary>
    public int ObjectID
    {
        get
        {
            return mObjectID;
        }
        set
        {
            mObjectID = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do not load the data
        }
        else
        {
            // Get the base object
            GeneralizedInfo info = CMSObjectHelper.GetReadOnlyObject(ObjectType);
            if (info != null)
            {
                DataSet ds = info.GetData(null, info.IDColumn + " = " + this.ObjectID, null, 0, null, false);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    ds.Tables[0].TableName = ValidationHelper.GetIdentifier(info.ObjectClassName);
                    DisplayData(ds);
                }
            }
        }
    }

    #endregion


    #region "Other methods

    /// <summary>
    /// Displays data in table.
    /// </summary>
    /// <param name="ds">Dataset with data</param>
    protected void DisplayData(DataSet ds)
    {
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            // Prepare list of tables
            SortedDictionary<string, DataTable> tables = new SortedDictionary<string, DataTable>();
            foreach (DataTable dt in ds.Tables)
            {
                if (!DataHelper.DataSourceIsEmpty(dt))
                {
                    tables.Add(GetString("ObjectType." + dt.TableName), dt);
                }
            }

            // Generate the tables
            foreach (DataTable dt in tables.Values)
            {
                pnlContent.Controls.Add(new LiteralControl("<h3>" + GetString("ObjectType." + dt.TableName) + "</h3>"));

                if (dt.Columns.Count >= 6)
                {
                    // Write all rows
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("<table cellspacing=\"0\" cellpadding=\"3\" rules=\"rows\" border=\"1\" style=\"border-collapse:collapse;\" width=\"100%\" class=\"UniGridGrid\">");

                        // Add header
                        sb.Append("<tr class=\"UniGridHead\"><th>" + GetString("General.FieldName") + "</th><th style=\"width: 100%;\">" + GetString("General.Value") + "</th></tr>");

                        int i = 0;
                        // Add values
                        foreach (DataColumn dc in dt.Columns)
                        {
                            ++i;
                            string className = ((i % 2) == 0) ? "OddRow" : "EvenRow";
                            sb.Append("<tr class=\"" + className + "\"><td>");
                            sb.Append("<strong>" + dc.ColumnName + "</strong>");
                            sb.Append("</td><td style=\"width: 100%;\">");

                            string content = null;

                            // Binary columns
                            if ((dc.DataType == typeof(byte[])) && (dr[dc.ColumnName] != DBNull.Value))
                            {
                                content = "<binary data>";
                            }
                            else
                            {
                                content = ValidationHelper.GetString(dr[dc.ColumnName], "");
                            }

                            bool standard = true;
                            switch (dc.ColumnName.ToLower())
                            {
                                // Document content
                                case "documentcontent":
                                    EditableItems items = new EditableItems();
                                    items.LoadContentXml(ValidationHelper.GetString(dr[dc.ColumnName], ""));

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
                                    content = HTMLHelper.ReformatHTML(content);
                                    break;
                            }

                            // Standard rendering
                            if (standard)
                            {
                                content = HttpUtility.HtmlEncode(content);
                                content = TextHelper.EnsureLineEndings(content, "<br />");
                                content = content.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
                                sb.Append("<div style=\"max-height: 300px; overflow: auto;\">" + content + "</div>");
                            }

                            sb.Append("</td></tr>");
                        }

                        sb.Append("</table><br />\n");

                        pnlContent.Controls.Add(new LiteralControl(sb.ToString()));
                    }
                }
                else
                {
                    GridView newGrid = new GridView();
                    newGrid.ID = "grid" + dt.TableName;
                    newGrid.EnableViewState = false;
                    newGrid.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    newGrid.CellPadding = 3;
                    newGrid.GridLines = GridLines.Horizontal;

                    pnlContent.Controls.Add(newGrid);

                    newGrid.DataSource = ds;
                    newGrid.DataMember = dt.TableName;

                    newGrid.DataBind();
                }
            }
        }
    }

    #endregion
}
