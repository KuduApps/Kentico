using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using CMS.FormControls;

public partial class CMSFormControls_Inputs_MacroEditor : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the value of this form control.
    /// </summary>
    /// <value>Text content of this editor</value>
    [Browsable(false)]
    public override object Value
    {
        get
        {
            return this.ucEditor.Text;
        }
        set
        {
            this.ucEditor.Text = (string)value;
        }
    }


    /// <summary>
    /// Gets or sets whether this form control is enabled.
    /// </summary>
    /// <value>True, if form control is enabled, otherwise false</value>
    [Browsable(true)]
    [Description("Determines whether this form control is enabled")]
    [Category("Form Control")]
    [DefaultValue(true)]
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = this.ucEditor.Editor.Enabled = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
