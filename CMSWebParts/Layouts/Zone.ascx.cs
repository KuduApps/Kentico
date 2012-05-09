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

using AjaxControlToolkit;

public partial class CMSWebParts_Layouts_Zone : CMSAbstractLayoutWebPart
{
    #region "Properties"

    /// <summary>
    /// Width.
    /// </summary>
    public string Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Width"), "");
        }
        set
        {
            this.SetValue("Width", value);
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
    /// Zone CSS class.
    /// </summary>
    public string ZoneCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZoneCSSClass"), "");
        }
        set
        {
            this.SetValue("ZoneCSSClass", value);
        }
    }


    /// <summary>
    /// Location.
    /// </summary>
    public string Location
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Location"), "");
        }
        set
        {
            this.SetValue("Location", value);
        }
    }


    /// <summary>
    /// Vertical offset.
    /// </summary>
    public int VerticalOffset
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("VerticalOffset"), 0);
        }
        set
        {
            this.SetValue("VerticalOffset", value);
        }
    }


    /// <summary>
    /// Horizontal offset.
    /// </summary>
    public int HorizontalOffset
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("HorizontalOffset"), 0);
        }
        set
        {
            this.SetValue("HorizontalOffset", value);
        }
    }


    /// <summary>
    /// Scroll effect duration (ms).
    /// </summary>
    public int ScrollEffectDuration
    {
        get
        {
            int result = ValidationHelper.GetInteger(this.GetValue("ScrollEffectDuration"), 100);
            if (result <= 0)
            {
                result = 100;
            }

            return result;
        }
        set
        {
            this.SetValue("ScrollEffectDuration", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Prepares the layout of the web part.
    /// </summary>
    protected override void PrepareLayout()
    {
        string location = this.Location;

        bool alwaysVisible = !String.IsNullOrEmpty(location);

        StartLayout();

        if (IsDesign)
        {
            Append("<table class=\"LayoutTable\" cellspacing=\"0\">");

            if (this.ViewMode == ViewModeEnum.Design)
            {
                Append("<tr><td class=\"LayoutHeader\" colspan=\"2\">");

                // Add header container
                AddHeaderContainer();

                Append("</td></tr>");
            }

            Append("<tr><td>");
        }

        string style = null;

        // Width
        string width = this.Width;
        if (!String.IsNullOrEmpty(width))
        {
            style += " width: " + width + ";";
        }

        // Height
        string height = this.Height;
        if (!String.IsNullOrEmpty(height))
        {
            style += " height: " + height + ";";
        }

        string cssclass = this.ZoneCSSClass;

        // Render the envelope if needed
        bool renderEnvelope = IsDesign || !String.IsNullOrEmpty(style) || !String.IsNullOrEmpty(cssclass);
        if (renderEnvelope)
        {
            Append("<div");

            if (IsDesign)
            {
                Append(" id=\"", ShortClientID, "_env\"");
            }

            if (!String.IsNullOrEmpty(style))
            {
                Append(" style=\"", style, "\"");
            }

            if (!String.IsNullOrEmpty(cssclass))
            {
                Append(" class=\"", cssclass, "\"");
            }

            Append(">");
        }

        if (alwaysVisible)
        {
            // Add the extender
            AlwaysVisibleControlExtender av = new AlwaysVisibleControlExtender();
            av.TargetControlID = "pnlEx";
            av.ID = "avExt";

            // Horizontal location
            if (location.EndsWith("left", StringComparison.InvariantCultureIgnoreCase))
            {
                av.HorizontalSide = HorizontalSide.Left;
            }
            else if (location.EndsWith("center", StringComparison.InvariantCultureIgnoreCase))
            {
                av.HorizontalSide = HorizontalSide.Center;
            }
            else if (location.EndsWith("right", StringComparison.InvariantCultureIgnoreCase))
            {
                av.HorizontalSide = HorizontalSide.Right;
            }

            // Horizontal location
            if (location.StartsWith("top", StringComparison.InvariantCultureIgnoreCase))
            {
                av.VerticalSide = VerticalSide.Top;
            }
            else if (location.StartsWith("middle", StringComparison.InvariantCultureIgnoreCase))
            {
                av.VerticalSide = VerticalSide.Middle;
            }
            else if (location.StartsWith("bottom", StringComparison.InvariantCultureIgnoreCase))
            {
                av.VerticalSide = VerticalSide.Bottom;
            }

            // Offsets
            av.HorizontalOffset = this.HorizontalOffset;
            av.VerticalOffset = this.VerticalOffset;

            av.ScrollEffectDuration = this.ScrollEffectDuration / 1000f;

            // Add the extender
            this.Controls.Add(av);
        }

        // Add the zone
        CMSWebPartZone zone = AddZone(this.ID + "_zone", this.ID);

        if (renderEnvelope)
        {
            Append("</div>");
        }

        if (IsDesign)
        {
            Append("</td>");

            // Resizers
            if (AllowDesignMode)
            {
                // Vertical resizer
                Append("<td class=\"HorizontalResizer\" onmousedown=\"", GetHorizontalResizerScript("env", "Width", false, null), " return false;\">&nbsp;</td></tr><tr>");

                // Horizontal resizer
                Append("<td class=\"VerticalResizer\" onmousedown=\"", GetVerticalResizerScript("env", "Height"), " return false;\">&nbsp;</td>");
                Append("<td class=\"BothResizer\" onmousedown=\"", GetHorizontalResizerScript("env", "Width", false, null), " ", GetVerticalResizerScript("env", "Height"), " return false;\">&nbsp;</td>");
            }

            Append("</tr></table>");
        }

        // Panel for extender
        PlaceHolder pnlEx = new PlaceHolder();
        pnlEx.ID = "pnlEx";
        //pnlEx.Visible = false;

        AddControl(pnlEx);


        FinishLayout();
    }


    protected override void Render(HtmlTextWriter writer)
    {
        if (!String.IsNullOrEmpty(this.Location))
        {
            // Ensure the envelope for placing elsewhere
            writer.Write("<div id=\"" + this.ClientID + "_pnlEx\" style=\"z-index: 9901;\">");

            base.Render(writer);

            writer.Write("</div>");
        }
        else
        {
            // Standard rendering of single zone
            base.Render(writer);
        }
    }

    #endregion
}
