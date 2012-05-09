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

public partial class CMSWebParts_Layouts_CollapsiblePanel : CMSAbstractLayoutWebPart
{
    #region "Properties"

    /// <summary>
    /// Collapsed text.
    /// </summary>
    public string CollapsedText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CollapsedText"), "");
        }
        set
        {
            this.SetValue("CollapsedText", value);
        }
    }


    /// <summary>
    /// Collapsed image.
    /// </summary>
    public string CollapsedImage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CollapsedImage"), "");
        }
        set
        {
            this.SetValue("CollapsedImage", value);
        }
    }


    /// <summary>
    /// Expanded text.
    /// </summary>
    public string ExpandedText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ExpandedText"), "");
        }
        set
        {
            this.SetValue("ExpandedText", value);
        }
    }


    /// <summary>
    /// Expanded image.
    /// </summary>
    public string ExpandedImage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ExpandedImage"), "");
        }
        set
        {
            this.SetValue("ExpandedImage", value);
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
    /// Title CSS class.
    /// </summary>
    public string TitleCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TitleCSSClass"), "");
        }
        set
        {
            this.SetValue("TitleCSSClass", value);
        }
    }


    /// <summary>
    /// Image CSS class.
    /// </summary>
    public string ImageCSSClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ImageCSSClass"), "");
        }
        set
        {
            this.SetValue("ImageCSSClass", value);
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
    /// Collapsed size (px).
    /// </summary>
    public int CollapsedSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("CollapsedSize"), -1);
        }
        set
        {
            this.SetValue("CollapsedSize", value);
        }
    }


    /// <summary>
    /// Expanded size (px).
    /// </summary>
    public int ExpandedSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("ExpandedSize"), -1);
        }
        set
        {
            this.SetValue("ExpandedSize", value);
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
    /// Collapsed.
    /// </summary>
    public bool Collapsed
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("Collapsed"), false);
        }
        set
        {
            this.SetValue("Collapsed", value);
        }
    }


    /// <summary>
    /// Auto collapse.
    /// </summary>
    public bool AutoCollapse
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AutoCollapse"), false);
        }
        set
        {
            this.SetValue("AutoCollapse", value);
        }
    }


    /// <summary>
    /// Auto expand.
    /// </summary>
    public bool AutoExpand
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AutoExpand"), false);
        }
        set
        {
            this.SetValue("AutoExpand", value);
        }
    }


    /// <summary>
    /// Expand direction.
    /// </summary>
    public string ExpandDirection
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ExpandDirection"), "");
        }
        set
        {
            this.SetValue("ExpandDirection", value);
        }
    }


    /// <summary>
    /// Scroll content.
    /// </summary>
    public bool ScrollContent
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ScrollContent"), false);
        }
        set
        {
            this.SetValue("ScrollContent", value);
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

        // Header panel
        Panel pnlHeader = new Panel();
        pnlHeader.ID = "pnlH";
        pnlHeader.CssClass = this.HeaderCSSClass;
        pnlHeader.EnableViewState = false;

        // Header label
        Label lblHeader = new Label();
        lblHeader.CssClass = this.HeaderCSSClass;
        lblHeader.ID = "lblH";

        pnlHeader.Controls.Add(lblHeader);

        // Header image
        Image imgHeader = new Image();
        imgHeader.CssClass = this.ImageCSSClass;
        imgHeader.ID = "imgH";

        pnlHeader.Controls.Add(imgHeader);


        AddControl(pnlHeader);

        // Content panel
        Panel pnlContent = new Panel();
        pnlContent.CssClass = this.ContentCSSClass;
        pnlContent.ID = "pnlC";

        AddControl(pnlContent);

        // Add the zone
        CMSWebPartZone zone = AddZone(this.ID + "_zone", this.ID, pnlContent);

        // Add the extender
        CollapsiblePanelExtender cp = new CollapsiblePanelExtender();
        cp.ID = "extCP";
        cp.TargetControlID = pnlContent.ID;

        cp.ExpandControlID = pnlHeader.ID;
        cp.CollapseControlID = pnlHeader.ID;

        cp.TextLabelID = lblHeader.ID;
        cp.ImageControlID = imgHeader.ID;

        cp.ExpandDirection = (this.ExpandDirection.Equals("horz", StringComparison.InvariantCultureIgnoreCase) ? CollapsiblePanelExpandDirection.Horizontal : CollapsiblePanelExpandDirection.Vertical);

        // Texts
        string expText = ResHelper.LocalizeString(ExpandedText);
        string colText = ResHelper.LocalizeString(CollapsedText);

        cp.ExpandedText = expText;
        cp.CollapsedText = colText;

        if (String.IsNullOrEmpty(expText) && String.IsNullOrEmpty(colText))
        {
            lblHeader.Visible = false;
        }

        // Images
        string expImage = this.ExpandedImage;
        string colImage = this.CollapsedImage;

        if (!String.IsNullOrEmpty(expImage) && !String.IsNullOrEmpty(colImage))
        {
            cp.ExpandedImage = expImage;
            cp.CollapsedImage = colImage;
        }
        else
        {
            imgHeader.Visible = false;
        }

        // Sizes
        int expSize = this.ExpandedSize;
        if (expSize > 0)
        {
            cp.ExpandedSize = expSize;
        }

        int collapsed = this.CollapsedSize;
        if (collapsed >= 0)
        {
            cp.CollapsedSize = this.CollapsedSize;
        }

        cp.Collapsed = this.Collapsed;

        if (!IsDesign)
        {
            cp.AutoCollapse = this.AutoCollapse;
            if (this.AutoExpand)
            {
                cp.AutoExpand = true;

                // Ensure some collapsed size
                if (collapsed < 0)
                {
                    cp.CollapsedSize = 10;
                }
            }
        }

        cp.ScrollContents = this.ScrollContent;

        // Add the extender
        this.Controls.Add(cp);

        if (IsDesign)
        {
            Append("</td>");

            // Width resizer
            if (AllowDesignMode)
            {
                Append("<td class=\"HorizontalResizer\" onmousedown=\"", GetHorizontalResizerScript("env", "Width"), " return false;\">&nbsp;</td>");
            }

            Append("</tr></table>");
        }

        Append("</div>");

        FinishLayout();
    }

    #endregion
}
