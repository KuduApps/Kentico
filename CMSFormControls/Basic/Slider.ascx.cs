using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.PortalControls;
using CMS.CMSHelper;

using AjaxControlToolkit;

public partial class CMSFormControls_Basic_Slider : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return textbox.Enabled;
        }
        set
        {
            textbox.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return textbox.Text;
        }
        set
        {
            // Load default value on insert
            textbox.Text = FormHelper.GetDoubleValueInCurrentCulture(value);
        }
    }

    #endregion


    #region "Slider properties"

    /// <summary>
    /// Number of discrete values inside the slider's range.
    /// </summary>
    public int Steps
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Steps"), 0);
        }
        set
        {
            SetValue("Steps", value);
        }
    }


    /// <summary>
    /// Number of decimal digits for the value.
    /// </summary>
    public int Decimals
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Decimals"), 0);
        }
        set
        {
            SetValue("Decimals", value);
        }
    }


    /// <summary>
    /// Show/hide slider lable.
    /// </summary>
    public bool ShowLabel
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowLabel"), false);
        }
        set
        {
            SetValue("ShowLabel", value);
        }
    }


    /// <summary>
    /// CSS class for the label.
    /// </summary>
    public string LabelCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LabelCssClass"), null);
        }
        set
        {
            SetValue("LabelCssClass", value);
        }
    }


    /// <summary>
    /// CSS class for the slider's rail.
    /// </summary>
    public string RailCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("RailCssClass"), null);
        }
        set
        {
            SetValue("RailCssClass", value);
        }
    }


    /// <summary>
    /// CSS class for the slider's handle.
    /// </summary>
    public string HandleCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("HandleCssClass"), null);
        }
        set
        {
            SetValue("HandleCssClass", value);
        }
    }


    /// <summary>
    /// URL of the image to display as the slider's handle.
    /// </summary>
    public string HandleImageUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("HandleImageUrl"), null);
        }
        set
        {
            SetValue("HandleImageUrl", value);
        }
    }


    /// <summary>
    /// Width/height of a horizontal/vertical slider when the default layout is used.
    /// </summary>
    public int Length
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Length"), 0);
        }
        set
        {
            SetValue("Length", value);
        }
    }


    /// <summary>
    /// Text to display in a tooltip when the handle is hovered. The {0} placeholder in the text is replaced with the current value of the slider.
    /// </summary>
    public string TooltipText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TooltipText"), null);
        }
        set
        {
            SetValue("TooltipText", value);
        }
    }


    /// <summary>
    /// Orientation of the slider (horizontal/vertical)
    /// </summary>
    public SliderOrientation Orientation
    {
        get;
        set;
    }


    /// <summary>
    /// Minimal value of the slider.
    /// </summary>
    public double Minimum
    {
        get
        {
            return ValidationHelper.GetDouble(GetValue("Minimum"), 0);
        }
        set
        {
            SetValue("Minimum", value);
        }
    }


    /// <summary>
    /// Maximal value of the slider.
    /// </summary>
    public double Maximum
    {
        get
        {
            return ValidationHelper.GetDouble(GetValue("Maximum"), 0);
        }
        set
        {
            SetValue("Maximum", value);
        }
    }

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Initialize properties
        PortalHelper.EnsureScriptManager(Page);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        exSlider.Minimum = Minimum;
        exSlider.Maximum = Maximum;
        exSlider.Steps = Steps;
        exSlider.Decimals = Decimals;
        exSlider.HandleCssClass = HandleCssClass;
        exSlider.HandleImageUrl = HandleImageUrl;
        exSlider.Length = Length;
        exSlider.RailCssClass = RailCssClass;
        exSlider.TooltipText = CMSContext.CurrentResolver.ResolveMacros(TooltipText);

        // Set the orientation
        object orientObj = GetValue("Orientation");
        if (orientObj == null)
        {
            exSlider.Orientation = Orientation;
        }
        else
        {
            exSlider.Orientation = ValidationHelper.GetBoolean(orientObj, false) ? SliderOrientation.Vertical : SliderOrientation.Horizontal;
        }

        // Initialize label
        lblValue.CssClass = LabelCssClass;
        lblValue.Visible = ShowLabel;

        // Apply CSS styles
        if (!String.IsNullOrEmpty(CssClass))
        {
            pnlContainer.CssClass = CssClass;
            CssClass = null;
        }
        if (!String.IsNullOrEmpty(ControlStyle))
        {
            pnlContainer.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }

        CheckRegularExpression = true;
    }

    #endregion
}
