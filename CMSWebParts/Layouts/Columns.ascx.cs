using System;
using System.Collections;

using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;

public partial class CMSWebParts_Layouts_Columns : CMSAbstractLayoutWebPart
{
    #region "Variables"

    /// <summary>
    /// List of div IDs.
    /// </summary>
    ArrayList divIds = new ArrayList();

    #endregion


    #region "Properties"

    /// <summary>
    /// Number of left columns.
    /// </summary>
    public int LeftColumns
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("LeftColumns"), 1);
        }
        set
        {
            this.SetValue("LeftColumns", value);
        }
    }


    /// <summary>
    /// Use center column.
    /// </summary>
    public bool CenterColumn
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CenterColumn"), true);
        }
        set
        {
            this.SetValue("CenterColumn", value);
        }
    }


    /// <summary>
    /// Number of right columns.
    /// </summary>
    public int RightColumns
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RightColumns"), 1);
        }
        set
        {
            this.SetValue("RightColumns", value);
        }
    }


    /// <summary>
    /// Height.
    /// </summary>
    public string Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Height"), "");
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Equal columns height.
    /// </summary>
    public bool EqualHeight
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EqualHeight"), false);
        }
        set
        {
            this.SetValue("EqualHeight", value);
        }
    }


    /// <summary>
    /// First left column width.
    /// </summary>
    public string LColumn1Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn1Width"), "");
        }
        set
        {
            this.SetValue("LColumn1Width", value);
        }
    }


    /// <summary>
    /// First left column height.
    /// </summary>
    public string LColumn1Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn1Height"), "");
        }
        set
        {
            this.SetValue("LColumn1Height", value);
        }
    }


    /// <summary>
    /// First left column CSS class.
    /// </summary>
    public string LColumn1CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn1CSSClass"), "");
        }
        set
        {
            this.SetValue("LColumn1CSSClass", value);
        }
    }


    /// <summary>
    /// Second left column width.
    /// </summary>
    public string LColumn2Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn2Width"), "");
        }
        set
        {
            this.SetValue("LColumn2Width", value);
        }
    }


    /// <summary>
    /// Second left column height.
    /// </summary>
    public string LColumn2Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn2Height"), "");
        }
        set
        {
            this.SetValue("LColumn2Height", value);
        }
    }


    /// <summary>
    /// Second left column CSS class.
    /// </summary>
    public string LColumn2CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn2CSSClass"), "");
        }
        set
        {
            this.SetValue("LColumn2CSSClass", value);
        }
    }


    /// <summary>
    /// Third left column width.
    /// </summary>
    public string LColumn3Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn3Width"), "");
        }
        set
        {
            this.SetValue("LColumn3Width", value);
        }
    }


    /// <summary>
    /// Third left column height.
    /// </summary>
    public string LColumn3Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn3Height"), "");
        }
        set
        {
            this.SetValue("LColumn3Height", value);
        }
    }


    /// <summary>
    /// Third left column CSS class.
    /// </summary>
    public string LColumn3CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LColumn3CSSClass"), "");
        }
        set
        {
            this.SetValue("LColumn3CSSClass", value);
        }
    }


    /// <summary>
    /// Use center column.
    /// </summary>
    public string CenterColumnCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CenterColumnCSSClass"), "");
        }
        set
        {
            this.SetValue("CenterColumnCSSClass", value);
        }
    }


    /// <summary>
    /// Center column height.
    /// </summary>
    public string CenterColumnHeight
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CenterColumnHeight"), "");
        }
        set
        {
            this.SetValue("CenterColumnHeight", value);
        }
    }


    /// <summary>
    /// First right column width.
    /// </summary>
    public string RColumn1Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn1Width"), "");
        }
        set
        {
            this.SetValue("RColumn1Width", value);
        }
    }


    /// <summary>
    /// First right column height.
    /// </summary>
    public string RColumn1Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn1Height"), "");
        }
        set
        {
            this.SetValue("RColumn1Height", value);
        }
    }


    /// <summary>
    /// First right column CSS class.
    /// </summary>
    public string RColumn1CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn1CSSClass"), "");
        }
        set
        {
            this.SetValue("RColumn1CSSClass", value);
        }
    }


    /// <summary>
    /// Second right column width.
    /// </summary>
    public string RColumn2Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn2Width"), "");
        }
        set
        {
            this.SetValue("RColumn2Width", value);
        }
    }


    /// <summary>
    /// Second right column height.
    /// </summary>
    public string RColumn2Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn2Height"), "");
        }
        set
        {
            this.SetValue("RColumn2Height", value);
        }
    }


    /// <summary>
    /// Second right column CSS class.
    /// </summary>
    public string RColumn2CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn2CSSClass"), "");
        }
        set
        {
            this.SetValue("RColumn2CSSClass", value);
        }
    }


    /// <summary>
    /// Third right column width.
    /// </summary>
    public string RColumn3Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn3Width"), "");
        }
        set
        {
            this.SetValue("RColumn3Width", value);
        }
    }


    /// <summary>
    /// Third right column height.
    /// </summary>
    public string RColumn3Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn3Height"), "");
        }
        set
        {
            this.SetValue("RColumn3Height", value);
        }
    }


    /// <summary>
    /// Third right column CSS class.
    /// </summary>
    public string RColumn3CSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RColumn3CSSClass"), "");
        }
        set
        {
            this.SetValue("RColumn3CSSClass", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Prepares the layout of the web part.
    /// </summary>
    protected override void PrepareLayout()
    {
        StartLayout();

        string height = ValidationHelper.GetString(this.GetValue("Height"), "");

        // Prepare the data for equal heights script
        bool equal = (this.EqualHeight /*|| IsDesign*/) && String.IsNullOrEmpty(height);
        string groupClass = null;
        if (equal)
        {
            groupClass = "Cols_" + this.InstanceGUID.ToString().Replace("-", "");
        }

        if (IsDesign)
        {
            Append("<table class=\"LayoutTable\" cellspacing=\"0\" style=\"width: 100%;\">");

            if (this.ViewMode == ViewModeEnum.Design)
            {
                Append("<tr><td class=\"LayoutHeader\">");

                // Add header container
                AddHeaderContainer();

                Append("</td></tr>");
            }

            Append("<tr><td>");
        }

        // Prepare automatic width
        string autoWidth = null;
        int cols = this.LeftColumns + this.RightColumns;
        if (this.CenterColumn)
        {
            cols++;
        }
        if (cols > 0)
        {
            autoWidth = ((100 - cols) / cols) + "%";
        }

        // Encapsulating div
        Append("<div>");

        // Left columns
        CreateColumns(this.LeftColumns, height, equal, groupClass, autoWidth, false);

        // Right columns
        CreateColumns(this.RightColumns, height, equal, groupClass, autoWidth, true);

        // Center column
        if (this.CenterColumn)
        {
            if (IsDesign && AllowDesignMode)
            {
                Append("<div style=\"overflow: auto;\" class=\"LayoutCenterColumn\">");
            }

            Append("<div");

            // Cell class
            string thisColumnClass = ValidationHelper.GetString(this.GetValue("CenterColumnCSSClass"), "");
            if (equal)
            {
                thisColumnClass = CSSHelper.JoinClasses(thisColumnClass, groupClass);
            }

            if (!String.IsNullOrEmpty(thisColumnClass))
            {
                Append(" class=\"");
                Append(thisColumnClass);
                Append("\"");
            }

            string style = "overflow: auto;";

            // Height
            height = DataHelper.GetNotEmpty(this.GetValue("CenterColumnHeight"), height);
            if (!String.IsNullOrEmpty(height))
            {
                style += " height: " + height + ";";
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
                Append(" id=\"", ShortClientID, "_col_c\"");
            }

            Append(">");

            // Add the zone
            AddZone(this.ID + "_center", "Center");

            Append("</div>");

            if (IsDesign)
            {
                // Vertical resizer for center column
                if (AllowDesignMode)
                {
                    Append("<div class=\"VerticalResizer\" onmousedown=\"" + GetVerticalResizerScript("col_c", "CenterColumnHeight") + " return false;\">&nbsp;</div>");
                }

                Append("</div>");
            }
        }

        // End of encapsulating div
        Append("<div style=\"clear: both;\"></div></div>");

        if (IsDesign)
        {
            Append("</td></tr>");

            // Footer with actions
            if (AllowDesignMode)
            {
                Append("<tr><td class=\"LayoutFooter\"><div class=\"LayoutFooterContent\">");

                // Pane actions
                Append("<div class=\"LayoutLeftActions\">");
                if (LeftColumns > 0)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveLeftColumn"), "LeftColumns");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddLeftColumn"), "LeftColumns");
                Append("</div>");

                Append("<div class=\"LayoutRightActions\">");
                if (RightColumns > 0)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveRightColumn"), "RightColumns");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddRightColumn"), "RightColumns");
                Append("</div>");

                Append("</div></td></tr>");
            }

            Append("</table>");
        }

        FinishLayout();

        // Enforce equal height with a javascript
        if (equal)
        {
            ScriptHelper.RegisterJQuery(this.Page);
            ScriptHelper.RegisterScriptFile(this.Page, "jquery/jquery-equalheight.js");

            string script = "setInterval('$j(\"." + groupClass + "\").equalHeight()', 500);";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EqualHeight_" + groupClass, ScriptHelper.GetScript(script));

            mNeedsLayoutScript = true;
        }
    }


    /// <summary>
    /// Creates the columns in the layout.
    /// </summary>
    /// <param name="cols">Number of columns</param>
    /// <param name="height">Height</param>
    /// <param name="equal">If true, the column heights should equal</param>
    /// <param name="groupClass">Group class</param>
    /// <param name="autoWidth">Automatic width</param>
    /// <param name="right">Right columns</param>
    protected void CreateColumns(int cols, string height, bool equal, string groupClass, string autoWidth, bool right)
    {
        for (int i = 1; i <= cols; i++)
        {
            string colMark = (right ? "r" : "l");

            // Set the width property
            string widthPropertyName = colMark + "Column" + i + "Width";
            string heightPropertyName = colMark + "Column" + i + "Height";

            // Do not use automatic width in case of design mode
            if (IsDesign)
            {
                autoWidth = "";
            }

            string width = DataHelper.GetNotEmpty(this.GetValue(widthPropertyName), autoWidth);
            height = DataHelper.GetNotEmpty(this.GetValue(heightPropertyName), height);

            Append("<div");

            string colId = "col_" + colMark + i;

            // Add alignment
            string fl = null;
            if (right)
            {
                fl = "float: right;";
            }
            else
            {
                fl = "float: left;";
            }

            string style = fl;

            if (IsDesign)
            {
                // Append style
                if (!String.IsNullOrEmpty(style))
                {
                    Append(" style=\"", style, "\"");
                }

                // Design mode classes
                if (AllowDesignMode)
                {
                    Append(" class=\"", (right ? "LayoutRightColumn" : "LayoutLeftColumn"), "\"");
                }

                Append("><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr>");

                if (right)
                {
                    // Width resizer
                    if (AllowDesignMode)
                    {
                        Append("<td class=\"HorizontalResizer\" onmousedown=\"", GetHorizontalResizerScript(colId, widthPropertyName, true, null), " return false;\">&nbsp;</td>");
                    }
                }

                Append("<td style=\"vertical-align: top;\">");

                Append("<div");

                style = null;
            }

            // Column width
            if (!String.IsNullOrEmpty(width))
            {
                style += "width: " + width + ";";
            }

            // Height
            if (!String.IsNullOrEmpty(height))
            {
                style += "height: " + height + ";";
            }

            // Append style
            if (!String.IsNullOrEmpty(style))
            {
                Append(" style=\"", style, "\"");
            }

            // Cell class
            string thisColumnClass = ValidationHelper.GetString(this.GetValue(colMark + "Column" + i + "CSSClass"), "");
            if (equal)
            {
                thisColumnClass = CSSHelper.JoinClasses(thisColumnClass, groupClass);
            }

            if (!String.IsNullOrEmpty(thisColumnClass))
            {
                Append(" class=\"", thisColumnClass, "\"");
            }

            if (IsDesign)
            {
                Append(" id=\"", ShortClientID, "_", colId, "\"");
            }

            Append(">");

            // Add the zone
            AddZone(this.ID + "_" + colMark + i, "[" + colMark.ToUpper() + i + "]");

            Append("</div>");

            if (IsDesign)
            {
                // Right column
                Append("</td>");

                if (AllowDesignMode)
                {
                    if (right)
                    {
                        // Resizers
                        Append("</tr><tr>");

                        Append("<td class=\"BothResizer\" onmousedown=\"", GetHorizontalResizerScript(colId, widthPropertyName, true, null), " ", GetVerticalResizerScript(colId, heightPropertyName), " return false;\">&nbsp;</td>");
                        Append("<td class=\"VerticalResizer\" onmousedown=\"", GetVerticalResizerScript(colId, heightPropertyName), " return false;\">&nbsp;</td>");
                    }
                    else
                    {
                        // Resizers
                        Append("<td class=\"HorizontalResizer\" onmousedown=\"");
                        Append(GetHorizontalResizerScript(colId, widthPropertyName, false, null));
                        Append(" return false;\">&nbsp;</td></tr><tr>");

                        Append("<td class=\"VerticalResizer\" onmousedown=\"", GetVerticalResizerScript(colId, heightPropertyName), " return false;\">&nbsp;</td>");
                        Append("<td class=\"BothResizer\" onmousedown=\"", GetHorizontalResizerScript(colId, widthPropertyName, false, null), " ", GetVerticalResizerScript(colId, heightPropertyName), " return false;\">&nbsp;</td>");
                    }
                }

                Append("</tr></table>");

                Append("</div>");
            }
        }
    }

    #endregion
}
