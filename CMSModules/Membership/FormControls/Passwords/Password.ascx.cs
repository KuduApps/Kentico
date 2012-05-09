using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.FormControls;

public partial class CMSModules_Membership_FormControls_Passwords_Password : FormEngineUserControl
{
    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            txtPassword.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtPassword.Text;
        }
        set
        {
            string pass = ValidationHelper.GetString(value, "");
            txtPassword.Attributes.Add("value", pass);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Apply CSS styles
        if (!String.IsNullOrEmpty(CssClass))
        {
            txtPassword.CssClass = CssClass;
            CssClass = null;
        }
        else if (String.IsNullOrEmpty(txtPassword.CssClass))
        {
            // Set automatic class to the textbox
            txtPassword.CssClass = "TextBoxField";
        }

        if (!String.IsNullOrEmpty(ControlStyle))
        {
            txtPassword.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Check regular expresion
        if ((!String.IsNullOrEmpty(this.FieldInfo.RegularExpression)) && (new Validator().IsRegularExp(txtPassword.Text, this.FieldInfo.RegularExpression, "error").Result == "error"))
        {
            this.ValidationError = GetString("PassConfirmator.InvalidPassword");
            return false;
        }

        // Check min length
        if ((this.FieldInfo.MinStringLength > 0) && (txtPassword.Text.Length < this.FieldInfo.MinStringLength))
        {
            this.ValidationError = String.Format(GetString("PassConfirmator.PasswordLength"), this.FieldInfo.MinStringLength);
            return false;
        }

        // Check max length
        if ((this.FieldInfo.MaxStringLength > 0) && (txtPassword.Text.Length > this.FieldInfo.MaxStringLength))
        {
            this.ValidationError = String.Format(GetString("basicform.errortexttoolong"));
            return false;
        }

        return true;
    }
}
