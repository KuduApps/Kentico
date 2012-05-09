using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

/// <summary>
/// This form control must be used with name 'showimage'. Another blank form control with name 'showadvancedimage' must be used as well.
/// </summary>
public partial class CMSFormControls_System_ImageDialogSelector : FormEngineUserControl
{
    #region "Public properties"

    /// <summary>
    /// Indicates if control is enabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return radImageSimple.Enabled;
        }
        set
        {
            radImageNo.Enabled = value;
            radImageSimple.Enabled = value;
            radImageAdvanced.Enabled = value;
        }
    }


    /// <summary>
    /// Radiobutton 'simple' selected value.
    /// </summary>
    public override object Value
    {
        get
        {
            return radImageSimple.Checked;
        }
        set
        {
            radImageSimple.Checked = ValidationHelper.GetBoolean(value, false);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ContainsColumn("ShowAdvancedImage"))
        {
            radImageAdvanced.Checked = ValidationHelper.GetBoolean(this.Form.Data.GetValue("ShowAdvancedImage"), false);
        }
        radImageNo.Checked = !(radImageAdvanced.Checked || radImageSimple.Checked);
    }


    /// <summary>
    /// Returns other values related to this form control.
    /// </summary>
    /// <returns>Returns an array where first dimension is attribute name and the second dimension is its value.</returns>
    public override object[,] GetOtherValues()
    {
        // Set properties names
        object[,] values = new object[1, 2];
        values[0, 0] = "showadvancedimage";
        values[0, 1] = radImageAdvanced.Checked;
        return values;
    }


    /// <summary>
    /// Validates control.
    /// </summary>
    public override bool IsValid()
    {
        bool isValid = true;

        if (!ContainsColumn("showimage"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "showimage", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }

        if (!ContainsColumn("showadvancedimage"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "showadvancedimage", GetString("templatedesigner.fieldtypes.boolean"));
            isValid = false;
        }

        return isValid;
    }

    #endregion
}