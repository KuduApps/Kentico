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

using AjaxControlToolkit;

public partial class CMSWebParts_Layouts_Accordion : CMSAbstractLayoutWebPart
{
    #region "Public properties"

    /// <summary>
    /// Number of panes.
    /// </summary>
    public int Panes
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Panes"), 2);
        }
        set
        {
            this.SetValue("Panes", value);
        }
    }


    /// <summary>
    /// Pane headers.
    /// </summary>
    public string PaneHeaders
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PaneHeaders"), "");
        }
        set
        {
            this.SetValue("PaneHeaders", value);
        }
    }


    /// <summary>
    /// Active pane index.
    /// </summary>
    public int ActivePaneIndex
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("ActivePaneIndex"), -1);
        }
        set
        {
            this.SetValue("ActivePaneIndex", value);
        }
    }


    /// <summary>
    /// Require opened pane.
    /// </summary>
    public bool RequireOpenedPane
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RequireOpenedPane"), false);
        }
        set
        {
            this.SetValue("RequireOpenedPane", value);
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
    /// Header CSS class.
    /// </summary>
    public string HeaderCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("HeaderCSSClass"), "");
        }
        set
        {
            this.SetValue("HeaderCSSClass", value);
        }
    }


    /// <summary>
    /// Selected header CSS class.
    /// </summary>
    public string SelectedHeaderCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedHeaderCSSClass"), "");
        }
        set
        {
            this.SetValue("SelectedHeaderCSSClass", value);
        }
    }


    /// <summary>
    /// Content CSS class.
    /// </summary>
    public string ContentCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ContentCSSClass"), "");
        }
        set
        {
            this.SetValue("ContentCSSClass", value);
        }
    }


    /// <summary>
    /// Fade transitions.
    /// </summary>
    public bool FadeTransitions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("FadeTransitions"), false);
        }
        set
        {
            this.SetValue("FadeTransitions", value);
        }
    }


    /// <summary>
    /// Transition duration (ms).
    /// </summary>
    public int TransitionDuration
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("TransitionDuration"), 500);
        }
        set
        {
            this.SetValue("TransitionDuration", value);
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

            Append("<tr><td id=\"", ShortClientID, "_info\" style=\"width: 100%;\">");
        }
        else
        {
            Append(">");
        }

        // Add the tabs
        Accordion acc = new Accordion();
        acc.ID = "acc";
        AddControl(acc);

        if (IsDesign)
        {
            Append("</td>");

            if (AllowDesignMode)
            {
                // Width resizer
                Append("<td class=\"HorizontalResizer\" onmousedown=\"" + GetHorizontalResizerScript("env", "Width", false, "info") + " return false;\">&nbsp;</td>");
            }

            Append("</tr>");
        }

        // Pane headers
        string[] headers = PaneHeaders.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i <= Panes; i++)
        {
            // Create new pane
            AccordionPane pane = new AccordionPane();
            pane.ID = "pane" + i;

            // Prepare the header
            string header = null;
            if (headers.Length >= i)
            {
                header = ResHelper.LocalizeString(headers[i - 1]);
            }
            if (String.IsNullOrEmpty(header))
            {
                header = "Pane " + i;
            }

            pane.Header = new TextTransformationTemplate(header);
            acc.Panes.Add(pane);

            AddZone(this.ID + "_" + i, header, pane.ContentContainer);            
        }

        // Setup the accordion
        if ((this.ActivePaneIndex >= 1) && (this.ActivePaneIndex <= acc.Panes.Count))
        {
            acc.SelectedIndex = this.ActivePaneIndex - 1;
        }

        acc.ContentCssClass = this.ContentCSSClass;
        acc.HeaderCssClass = this.HeaderCSSClass;
        acc.HeaderSelectedCssClass = this.SelectedHeaderCSSClass;

        acc.FadeTransitions = this.FadeTransitions;
        acc.TransitionDuration = this.TransitionDuration;
        acc.RequireOpenedPane = this.RequireOpenedPane;

        // If no active pane is selected and doesn't require opened one, do not preselect any
        if (!acc.RequireOpenedPane && (this.ActivePaneIndex < 0))
        {
            acc.SelectedIndex = -1;
        }

        if (IsDesign)
        {
            if (AllowDesignMode)
            {
                Append("<tr><td class=\"LayoutFooter\" colspan=\"2\"><div class=\"LayoutFooterContent\">");

                // Pane actions
                Append("<div class=\"LayoutLeftActions\">");

                if (this.Panes > 1)
                {
                    AppendRemoveAction(ResHelper.GetString("Layout.RemoveLastPane"), "Panes");
                    Append(" ");
                }
                AppendAddAction(ResHelper.GetString("Layout.AddPane"), "Panes");

                Append("</div></div></td></tr>");
            }

            Append("</table>");
        }

        Append("</div>");

        FinishLayout();
    }

    #endregion
}
