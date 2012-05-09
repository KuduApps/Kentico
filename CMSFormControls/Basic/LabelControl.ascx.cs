using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Basic_LabelControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return label.Enabled;
        }
        set
        {
            label.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return label.Text;
        }
        set
        {
            label.Text = ValidationHelper.GetString(value, null);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Apply styles to control
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            label.CssClass = this.CssClass;
            this.CssClass = null;
        }
        else if (String.IsNullOrEmpty(label.CssClass))
        {
            label.CssClass = "LabelField";
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            label.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckFieldEmptiness = false;
    }

    #endregion
}