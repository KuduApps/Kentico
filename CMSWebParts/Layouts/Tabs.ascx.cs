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
using CMS.Controls;
using CMS.ExtendedControls;

using AjaxControlToolkit;

public partial class CMSWebParts_Layouts_Tabs : CMSAbstractLayoutWebPart
{
    #region "Public properties"

    /// <summary>
    /// Number of tabs.
    /// </summary>
    public int Tabs
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Tabs"), 2);
        }
        set
        {
            this.SetValue("Tabs", value);
        }
    }


    /// <summary>
    /// Tab headers.
    /// </summary>
    public string TabHeaders
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TabHeaders"), "");
        }
        set
        {
            this.SetValue("TabHeaders", value);
        }
    }


    /// <summary>
    /// Active tab index.
    /// </summary>
    public int ActiveTabIndex
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("ActiveTabIndex"), 0);
        }
        set
        {
            this.SetValue("ActiveTabIndex", value);
        }
    }


    /// <summary>
    /// Tab strip placement.
    /// </summary>
    public string TabStripPlacement
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TabStripPlacement"), "top");
        }
        set
        {
            this.SetValue("TabStripPlacement", value);
        }
    }


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
    /// Tabs CSS class.
    /// </summary>
    public string TabsCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TabsCSSClass"), "");
        }
        set
        {
            this.SetValue("TabsCSSClass", value);
        }
    }


    /// <summary>
    /// Scrollbars.
    /// </summary>
    public string Scrollbars
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Scrollbars"), "");
        }
        set
        {
            this.SetValue("Scrollbars", value);
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

        Append("<div");

        // Width
        string width = this.Width;
        if (!string.IsNullOrEmpty(width))
        {
            Append(" style=\"width: ", width, "\"");
        }

        if (IsDesign)
        {
            Append(" id=\"", ShortClientID, "_env\">");

            Append("<table class=\"LayoutTable\" cellspacing=\"0\" style=\"width: 100%;\">");

            if (this.ViewMode == ViewModeEnum.Design)
            {
                Append("<tr><td class=\"LayoutHeader\" colspan=\"2\">");

                // Add header container
                AddHeaderContainer();

                Append("</td></tr>");
            }

            Append("<tr><td style=\"width: 100%;\">");
        }
        else
        {
            Append(">");
        }

        // Add the tabs
        TabContainer tabs = new TabContainer();
        tabs.ID = "tabs";
        AddControl(tabs);

        if (IsDesign)
        {
            Append("</td>");

            // Resizers
            if (AllowDesignMode)
            {
                // Width resizer
                Append("<td class=\"HorizontalResizer\" onmousedown=\"", GetHorizontalResizerScript("env", "Width", false, "tabs_body"), " return false;\">&nbsp;</td></tr>");

                // Height resizer
                Append("<tr><td class=\"VerticalResizer\" onmousedown=\"", GetVerticalResizerScript("tabs_body", "Height"), " return false;\">&nbsp;</td>");
                Append("<td class=\"BothResizer\" onmousedown=\"", GetHorizontalResizerScript("env", "Width", false, "tabs_body"), " ", GetVerticalResizerScript("tabs_body", "Height"), " return false;\">&nbsp;</td>");
            }

            Append("</tr>");
        }

        // Tab headers
        string[] headers = TabHeaders.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        if ((this.ActiveTabIndex >= 1) && (this.ActiveTabIndex <= Tabs))
        {
            tabs.ActiveTabIndex = this.ActiveTabIndex - 1;
        }

        for (int i = 1; i <= Tabs; i++)
        {
            // Create new tab
            TabPanel tab = new TabPanel();
            tab.ID = "tab" + i;

            // Prepare the header
            string header = null;
            if (headers.Length >= i)
            {
                header = ResHelper.LocalizeString(headers[i - 1]);
            }
            if (String.IsNullOrEmpty(header))
            {
                header = "Tab " + i;
            }

            tab.HeaderText = header;
            tabs.Tabs.Add(tab);

            AddZone(this.ID + "_" + i, header, tab);            
        }

        // Setup the tabs
        tabs.ScrollBars = ControlsHelper.GetScrollbarsEnum(this.Scrollbars);

        if (!String.IsNullOrEmpty(this.TabsCSSClass))
        {
            tabs.CssClass = this.TabsCSSClass;
        }

        tabs.TabStripPlacement = (this.TabStripPlacement.ToLower() == "bottom" ? AjaxControlToolkit.TabStripPlacement.Bottom : AjaxControlToolkit.TabStripPlacement.Top);
        
        if (!String.IsNullOrEmpty(this.Height))
        {
            tabs.Height = new Unit(this.Height);
        }
        
        if (IsDesign)
        {
            // Footer
            if (AllowDesignMode)
            {
                Append("<tr><td class=\"LayoutFooter\" colspan=\"2\"><div class=\"LayoutFooterContent\">");

                Append("<div class=\"LayoutLeftActions\">");

                // Pane actions
                if (this.Tabs > 1)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveTab"), "Tabs");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddTab"), "Tabs");

                Append("</div></div></td></tr>");
            }
                
            Append("</table>");
        }

        Append("</div>");

        FinishLayout();
    }

    #endregion
}
