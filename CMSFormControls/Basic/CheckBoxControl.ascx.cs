using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;

public partial class CMSFormControls_Basic_CheckBoxControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return checkbox.Enabled;
        }
        set
        {
            checkbox.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return checkbox.Checked;
        }
        set
        {
            checkbox.Checked = ValidationHelper.GetBoolean(value, false);
        }
    }


    /// <summary>
    /// Gets or sets if control causes postback.
    /// </summary>
    public bool AutoPostBack
    {
        get
        {
            return checkbox.AutoPostBack;
        }
        set
        {
            checkbox.AutoPostBack = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Apply CSS styles
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            checkbox.CssClass = this.CssClass;
            this.CssClass = null;
        }
        else if (String.IsNullOrEmpty(checkbox.CssClass))
        {
            checkbox.CssClass = "CheckBoxField";
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            checkbox.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckFieldEmptiness = false;
    }

    #endregion
}
