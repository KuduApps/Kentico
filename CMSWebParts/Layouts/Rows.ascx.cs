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

public partial class CMSWebParts_Layouts_Rows : CMSAbstractLayoutWebPart
{
    #region "Properties"

    /// <summary>
    /// Number of rows.
    /// </summary>
    public int Rows
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Rows"), 2);
        }
        set
        {
            this.SetValue("Rows", value);
        }
    }


    /// <summary>
    /// First row width.
    /// </summary>
    public string Row1Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row1Width"), "");
        }
        set
        {
            this.SetValue("Row1Width", value);
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
    /// Second row width.
    /// </summary>
    public string Row2Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row2Width"), "");
        }
        set
        {
            this.SetValue("Row2Width", value);
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
    /// Third row width.
    /// </summary>
    public string Row3Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row3Width"), "");
        }
        set
        {
            this.SetValue("Row3Width", value);
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
    /// Fourth row width.
    /// </summary>
    public string Row4Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row4Width"), "");
        }
        set
        {
            this.SetValue("Row4Width", value);
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
    /// Fifth row width.
    /// </summary>
    public string Row5Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Row5Width"), "");
        }
        set
        {
            this.SetValue("Row5Width", value);
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


        // Add the columns
        for (int i = 1; i <= this.Rows; i++)
        {
            // Set the width property
            string heightPropertyName = "Row" + i + "Height";
            string widthPropertyName = "Row" + i + "Width";

            string width = ValidationHelper.GetString(this.GetValue(widthPropertyName), "");
            string height = ValidationHelper.GetString(this.GetValue(heightPropertyName), "");
            
            string rowId = "row_" + i;

            if (IsDesign)
            {
                Append("<table cellspacing=\"0\" cellpadding=\"0\"><tr><td id=\"");
                Append(ShortClientID + "_" + rowId);
                Append("\"");
            }
            else
            {
                Append("<div");
            }

            string style = "vertical-align: top;";

            // Row height
            if (!String.IsNullOrEmpty(height))
            {
                style += "height: " + height + ";";
            }

            // Row width
            if (!String.IsNullOrEmpty(width))
            {
                style += "width: " + width + ";";
            }
            else if (IsDesign)
            {
                style += "width: 100%;";
            }

            // Append style
            if (!String.IsNullOrEmpty(style))
            {
                Append(" style=\"");
                Append(style);
                Append("\"");
            }

            // Cell class
            string thisRowClass = ValidationHelper.GetString(this.GetValue("Row" + i + "CSSClass"), "");
            if (!String.IsNullOrEmpty(thisRowClass))
            {
                Append(" class=\"");
                Append(thisRowClass);
                Append("\"");
            }

            Append(">");

            // Add the zone
            AddZone(this.ID + "_" + i, "[" + i + "]");

            if (IsDesign)
            {
                Append("</td>");

                // Resizers
                if (AllowDesignMode)
                {
                    Append("<td class=\"HorizontalResizer\" onmousedown=\"");
                    Append(GetHorizontalResizerScript(rowId, widthPropertyName, false, null));
                    Append(" return false;\">&nbsp;</td></tr><tr>");

                    Append("<td class=\"VerticalResizer\" onmousedown=\"" + GetVerticalResizerScript(rowId, heightPropertyName) + " return false;\">&nbsp;</td>");
                    Append("<td class=\"BothResizer\" onmousedown=\"" + GetHorizontalResizerScript(rowId, widthPropertyName, false, null) + " " + GetVerticalResizerScript(rowId, heightPropertyName) + " return false;\">&nbsp;</td>");
                }

                Append("</tr></table>");
            }
            else
            {
                Append("</div>");
            }
        }

        if (IsDesign)
        {
            Append("</td></tr>");

            // Footer
            if (AllowDesignMode)
            {
                Append("<tr><td class=\"LayoutFooter\"><div class=\"LayoutFooterContent\">");

                Append("<div class=\"LayoutLeftActions\">");
                
                // Row actions
                if (Rows > 1)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveRow"), "Rows");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddRow"), "Rows");

                Append("</div></div></td></tr>");
            }

            Append("</table>");
        }

        // Finalize
        FinishLayout();
    }

    #endregion
}
