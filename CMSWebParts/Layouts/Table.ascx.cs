using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_Layouts_Table : CMSAbstractLayoutWebPart
{
    #region "Public properties"

    /// <summary>
    /// Total width.
    /// </summary>
    public string TableWidth
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TableWidth"), "");
        }
        set
        {
            this.SetValue("TableWidth", value);
        }
    }


    /// <summary>
    /// Number of columns.
    /// </summary>
    public int Columns
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Columns"), 2);
        }
        set
        {
            this.SetValue("Columns", value);
        }
    }


    /// <summary>
    /// Number of rows.
    /// </summary>
    public int Rows
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Rows"), 1);
        }
        set
        {
            this.SetValue("Rows", value);
        }
    }


    /// <summary>
    /// Table CSS class.
    /// </summary>
    public string TableCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TableCSSClass"), "");
        }
        set
        {
            this.SetValue("TableCSSClass", value);
        }
    }


    /// <summary>
    /// Vertical align.
    /// </summary>
    public string VerticalAlign
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("VerticalAlign"), "");
        }
        set
        {
            this.SetValue("VerticalAlign", value);
        }
    }


    /// <summary>
    /// First column width.
    /// </summary>
    public string Column1Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column1Width"), "");
        }
        set
        {
            this.SetValue("Column1Width", value);
        }
    }


    /// <summary>
    /// First column CSS class.
    /// </summary>
    public string Column1CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column1CSSClass"), "");
        }
        set
        {
            this.SetValue("Column1CSSClass", value);
        }
    }


    /// <summary>
    /// Second column width.
    /// </summary>
    public string Column2Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column2Width"), "");
        }
        set
        {
            this.SetValue("Column2Width", value);
        }
    }


    /// <summary>
    /// Second column CSS class.
    /// </summary>
    public string Column2CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column2CSSClass"), "");
        }
        set
        {
            this.SetValue("Column2CSSClass", value);
        }
    }


    /// <summary>
    /// Third column width.
    /// </summary>
    public string Column3Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column3Width"), "");
        }
        set
        {
            this.SetValue("Column3Width", value);
        }
    }


    /// <summary>
    /// Third column CSS class.
    /// </summary>
    public string Column3CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column3CSSClass"), "");
        }
        set
        {
            this.SetValue("Column3CSSClass", value);
        }
    }


    /// <summary>
    /// Fourth column width.
    /// </summary>
    public string Column4Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column4Width"), "");
        }
        set
        {
            this.SetValue("Column4Width", value);
        }
    }


    /// <summary>
    /// Fourth column CSS class.
    /// </summary>
    public string Column4CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column4CSSClass"), "");
        }
        set
        {
            this.SetValue("Column4CSSClass", value);
        }
    }


    /// <summary>
    /// Fifth column width.
    /// </summary>
    public string Column5Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column5Width"), "");
        }
        set
        {
            this.SetValue("Column5Width", value);
        }
    }


    /// <summary>
    /// Fifth column CSS class.
    /// </summary>
    public string Column5CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Column5CSSClass"), "");
        }
        set
        {
            this.SetValue("Column5CSSClass", value);
        }
    }


    /// <summary>
    /// First row height.
    /// </summary>
    public string Row1Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row1Height"), "");
        }
        set
        {
            this.SetValue("Row1Height", value);
        }
    }


    /// <summary>
    /// First row CSS class.
    /// </summary>
    public string Row1CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row1CSSClass"), "");
        }
        set
        {
            this.SetValue("Row1CSSClass", value);
        }
    }


    /// <summary>
    /// Second row height.
    /// </summary>
    public string Row2Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row2Height"), "");
        }
        set
        {
            this.SetValue("Row2Height", value);
        }
    }


    /// <summary>
    /// Second row CSS class.
    /// </summary>
    public string Row2CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row2CSSClass"), "");
        }
        set
        {
            this.SetValue("Row2CSSClass", value);
        }
    }


    /// <summary>
    /// Third row height.
    /// </summary>
    public string Row3Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row3Height"), "");
        }
        set
        {
            this.SetValue("Row3Height", value);
        }
    }


    /// <summary>
    /// Third row CSS class.
    /// </summary>
    public string Row3CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row3CSSClass"), "");
        }
        set
        {
            this.SetValue("Row3CSSClass", value);
        }
    }


    /// <summary>
    /// Fourth row height.
    /// </summary>
    public string Row4Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row4Height"), "");
        }
        set
        {
            this.SetValue("Row4Height", value);
        }
    }


    /// <summary>
    /// Fourth row CSS class.
    /// </summary>
    public string Row4CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row4CSSClass"), "");
        }
        set
        {
            this.SetValue("Row4CSSClass", value);
        }
    }


    /// <summary>
    /// Fifth row height.
    /// </summary>
    public string Row5Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row5Height"), "");
        }
        set
        {
            this.SetValue("Row5Height", value);
        }
    }


    /// <summary>
    /// Fifth row CSS class.
    /// </summary>
    public string Row5CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row5CSSClass"), "");
        }
        set
        {
            this.SetValue("Row5CSSClass", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Prepares the layout of the web part.
    /// </summary>
    protected override void PrepareLayout()
    {
        // Prepare the main markup
        StartLayout();

        string style = null;

        // Table width
        string width = this.TableWidth;
        bool hasTableWidth = false;

        if (!String.IsNullOrEmpty(width))
        {
            style += "width: " + width;
            hasTableWidth = true;
        }

        if (IsDesign)
        {
            Append("<table class=\"LayoutTable\" cellspacing=\"0\"");

            // Append style
            if (!String.IsNullOrEmpty(style))
            {
                Append(" style=\"");
                Append(style);
                Append("\"");
            }

            Append(">");

            if (this.ViewMode == ViewModeEnum.Design)
            {
                Append("<tr><td class=\"LayoutHeader\">");

                // Add header container
                AddHeaderContainer();

                Append("</td></tr>");
            }

            Append("<tr><td>");
        }

        Append("<table cellspacing=\"0\" ");

        // Add table class
        string tableClass = this.TableCSSClass;
        if (!String.IsNullOrEmpty(tableClass))
        {
            Append(" class=\"");
            Append(tableClass);
            Append("\"");
        }

        // Append style
        if (!String.IsNullOrEmpty(style))
        {
            Append(" style=\"");
            Append(style);
            Append("\"");
        }

        Append(">");

        string heightPropertyName = null;
        string widthPropertyName = null;

        // Prepare vertical alignment
        string valign = null;
        switch (this.VerticalAlign.ToLower())
        {
            case "top":
            case "middle":
            case "bottom":
                valign = this.VerticalAlign.ToLower();
                break;
        }

        bool hasEmptyColumn = false;

        // Add the rows
        for (int j = 1; j <= this.Rows; j++)
        {
            // Set the height property
            heightPropertyName = "Row" + j + "Height";

            Append("<tr");

            // Prepare the class for the row
            string thisRowClass = ValidationHelper.GetString(this.GetValue("Row" + j + "CSSClass"), "");
            if (!String.IsNullOrEmpty(thisRowClass))
            {
                Append(" class=\"");
                Append(thisRowClass);
                Append("\"");
            }

            Append(">");

            // Add the columns
            int cols = this.Columns;
            for (int i = 1; i <= cols; i++)
            {
                // Set the width property
                widthPropertyName = "Column" + i + "Width";

                Append("<td");

                // Cell class
                string thisColumnClass = ValidationHelper.GetString(this.GetValue("Column" + i + "CSSClass"), "");
                if (!String.IsNullOrEmpty(thisColumnClass))
                {
                    Append(" class=\"");
                    Append(thisColumnClass);
                    Append("\"");
                }

                style = null;

                // Add vertical alignment
                if (!String.IsNullOrEmpty(valign))
                {
                    style += "vertical-align: " + valign + ";";
                }

                // Column width
                if (j == 1)
                {
                    width = ValidationHelper.GetString(this.GetValue(widthPropertyName), "");
                    if (!String.IsNullOrEmpty(width))
                    {
                        if (!IsDesign || (i < cols) || !hasTableWidth || hasEmptyColumn)
                        {
                            style += " width: " + width + ";";
                        }
                    }
                    else
                    {
                        hasEmptyColumn = true;
                    }
                }

                // Row height
                if (i == 1)
                {
                    string height = ValidationHelper.GetString(this.GetValue(heightPropertyName), "");
                    if (!String.IsNullOrEmpty(height))
                    {
                        style += " height: " + height + ";";
                    }
                }

                // Append style
                if (!String.IsNullOrEmpty(style))
                {
                    Append(" style=\"");
                    Append(style);
                    Append("\"");
                }

                if (IsDesign)
                {
                    string cellId = "cell_" + j + "_" + i;
                    Append(" id=\"" + ShortClientID + "_" + cellId + "\"");
                }

                Append(">");

                // Add the zone
                AddZone(this.ID + "_" + j + "_" + i, "[" + j + "," + i + "]");

                Append("</td>");

                if (IsDesign && AllowDesignMode)
                {
                    Append("<td class=\"HorizontalResizer\" onmousedown=\"" + GetHorizontalResizerScript("cell_1_" + i, widthPropertyName, false, "cell_" + j + "_" + i) + " return false;\">&nbsp;</td>");
                }
            }

            Append("</tr>");

            if (IsDesign && AllowDesignMode)
            {
                Append("<tr>");

                // Add the columns
                for (int i = 1; i <= this.Columns; i++)
                {
                    // Set the width property
                    widthPropertyName = "Column" + i + "Width";

                    Append("<td class=\"VerticalResizer\" onmousedown=\"" + GetVerticalResizerScript("cell_" + j + "_1", heightPropertyName, "cell_" + j + "_" + i) + " return false;\">&nbsp;</td>");
                    Append("<td class=\"BothResizer\" onmousedown=\"" + GetHorizontalResizerScript("cell_1_" + i, widthPropertyName, false, "cell_" + j + "_" + i) + " " + GetVerticalResizerScript("cell_" + j + "_1", heightPropertyName, "cell_" + j + "_" + i) + " return false;\">&nbsp;</td>");
                }

                Append("</tr>");
            }
        }

        Append("</table>");

        if (IsDesign)
        {
            Append("</td></tr>");

            // Footer
            if (AllowDesignMode)
            {
                Append("<tr><td class=\"LayoutFooter\"><div class=\"LayoutFooterContent\">");

                // Row actions
                Append("<div class=\"LayoutLeftActions\">");
                if (Rows > 1)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveRow"), "Rows");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddRow"), "Rows");
                Append("</div>");

                // Column actions
                Append("<div class=\"LayoutRightActions\">");
                if (Columns > 1)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveColumn"), "Columns");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddColumn"), "Columns");
                Append("</div>");
                Append("<div class=\"ClearBoth\"></div>");

                Append("</div></td></tr>");
            }

            Append("</table>");
        }

        // Finalize
        FinishLayout();
    }

    #endregion
}
