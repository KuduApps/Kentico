using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_PageElements_guide : CMSUserControl
{
    private ArrayList mParameters;
    private int mColumns = 1;


    /// <summary>
    /// ArrayList of ArrayLists containig ImageURL, Title, PageURL, Description.
    /// </summary>
    public ArrayList Parameters
    {
        get
        {
            return mParameters;
        }
        set
        {
            mParameters = value;
        }
    }


    /// <summary>
    /// Number of columns (default 1).
    /// </summary>
    public int Columns
    {
        get
        {
            return mColumns;
        }
        set
        {
            mColumns = value;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (mParameters != null)
        {
            //Register javascript
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "detectTreeFrame", ScriptHelper.GetScript(InitScript()));

            Panel PanelModules = new Panel();
            Table tbl = new Table();
            tbl.CellSpacing = 10;
            TableRow tr = new TableRow();
            TableCell td;
            int actualRowIndex = 0;
            int relativeWidth = Convert.ToInt32(100 / mColumns);
            for (int i = 0; i < mParameters.Count; i++)
            {
                object[] row = (object[])mParameters[i];
                if (row != null)
                {
                    actualRowIndex++;
                    // Initialize Image
                    Image img = new Image();
                    img.ImageUrl = row[0].ToString();
                    img.CssClass = "PageTitleImage";

                    // Initialize Title
                    Label l = new Label();
                    l.Text = " " + HTMLHelper.HTMLEncode(row[1].ToString());

                    // Initialize Hyperlink
                    HyperLink h = new HyperLink();
                    h.Controls.Add(img);
                    h.Controls.Add(l);

                    if (row.Length > 4)
                    {
                        string fullContent = "false";
                        if (row.Length > 5)
                        {
                            fullContent = row[5].ToString().ToLower() == "true" ? "true" : "false";
                        }
                        
                        // Ensure not-null help key
                        row[4] = row[4] ?? "";

                        // For personalized guide use code name
                        h.Attributes.Add("onclick", "ShowDesktopContent(" + ScriptHelper.GetString(row[2].ToString()) + ", '" + row[1] + "', " + fullContent + "," + ScriptHelper.GetString(row[4].ToString()) + ");");
                    }
                    else
                    {
                        // Else use display name
                        h.Attributes.Add("onclick", "ShowDesktopContent(" + ScriptHelper.GetString(row[2].ToString()) + ", " + ScriptHelper.GetString(row[1].ToString()) + ");");
                    }
                    h.Attributes.Add("href", "#");

                    // Resolve description
                    string description = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(row[3].ToString()));

                    // Initialize Description
                    Label desc = new Label();
                    desc.Text = "<div>" + description + "</div>";

                    // Initialize wraping pannel
                    Panel p = new Panel();
                    p.Controls.Add(h);
                    p.Controls.Add(desc);

                    // Add style
                    p.CssClass = "Guide";

                    // Add to the table
                    td = new TableCell();
                    // Align all cells to top
                    td.VerticalAlign = VerticalAlign.Top;
                    // Add single description to tablecell
                    td.Controls.Add(p);

                    if (actualRowIndex == mColumns || (i == mParameters.Count - 1))
                    {
                        tr.Cells.Add(td);

                        // Ensure right column number for validity
                        if (i == mParameters.Count - 1)
                        {
                            for (int d = 0; d < (mColumns - (mParameters.Count % mColumns)); d++)
                            {
                                tr.Cells.Add(new TableCell());
                            }
                        }
                        // Add to table
                        tbl.Rows.Add(tr);
                        // Reset index counter
                        actualRowIndex = 0;
                        // Create new row
                        tr = new TableRow();
                    }
                    else
                    {
                        // Set relative width
                        td.Attributes.Add("style", "width:" + relativeWidth + "%;");
                        // Add to tablerow
                        tr.Cells.Add(td);
                    }
                }
            }

            // Add single module description to PanelModules
            PanelModules.Controls.Add(tbl);

            // Render whole description
            plcGuide.Controls.Add(PanelModules);
        }
    }


    /// <summary>
    /// Returns javascript to detect tree frame.
    /// </summary>
    private static string InitScript()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("var leftMenuFrame;\n");
        builder.Append("for(var f = 0;f < parent.frames.length;f++)\n");
        builder.Append("{\n");
        builder.Append("if(parent.frames[f].name.toLowerCase().indexOf('tree') != -1)\n");
        builder.Append("leftMenuFrame = parent.frames[f];\n");
        builder.Append("}\n");
        builder.Append("var allElems = leftMenuFrame.self.document.getElementsByTagName('*');");

        return builder.ToString();
    }
}
