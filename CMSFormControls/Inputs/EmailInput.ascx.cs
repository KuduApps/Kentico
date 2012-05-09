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

public partial class CMSFormControls_Inputs_EmailInput : CMS.FormControls.FormEngineUserControl
{
    #region "Properties"

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
            this.txtEmailInput.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtEmailInput.Text.Trim();
        }
        set
        {
            txtEmailInput.Text = (string)value;
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with emailinput.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return txtEmailInput.ClientID;
        }
    }


    private bool mAllowMultipleAddresses = false;

    /// <summary>
    /// Gets or sets if multiple e-mail addresses can be entered.
    /// </summary>
    public bool AllowMultipleAddresses
    {
        get
        {
            return mAllowMultipleAddresses;
        }
        set
        {
            mAllowMultipleAddresses = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set control style and css class
        if (!string.IsNullOrEmpty(this.ControlStyle))
        {
            txtEmailInput.Attributes.Add("style", this.ControlStyle);
        }
        if (!string.IsNullOrEmpty(this.CssClass))
        {
            txtEmailInput.CssClass = this.CssClass;
        }

        // Set additional properties
        AllowMultipleAddresses = ValidationHelper.GetBoolean(GetValue("allowmultipleaddresses"), AllowMultipleAddresses);
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        if (string.IsNullOrEmpty(txtEmailInput.Text.Trim()))
        {
            return true;
        }

        // Check if valid e-mail addresses were entered
        bool validEmails = (AllowMultipleAddresses ? ValidationHelper.AreEmails(txtEmailInput.Text.Trim()) : ValidationHelper.IsEmail(txtEmailInput.Text.Trim()));
        if (validEmails)
        {
            if (this.FieldInfo != null)
            {
                // Check regular expresion
                if (!string.IsNullOrEmpty(this.FieldInfo.RegularExpression))
                {
                    if (new Validator().IsRegularExp(txtEmailInput.Text.Trim(), this.FieldInfo.RegularExpression, "error").Result == "error")
                    {
                        this.ValidationError = this.FieldInfo.ValidationErrorMessage;
                        return false;
                    }
                }

                // Check min lenght
                if ((this.FieldInfo.MinStringLength > 0) && (txtEmailInput.Text.Trim().Length < this.FieldInfo.MinStringLength))
                {
                    this.ValidationError = this.FieldInfo.ValidationErrorMessage;
                    return false;
                }

                // Check max lenght
                if ((this.FieldInfo.MaxStringLength > 0) && (txtEmailInput.Text.Length > this.FieldInfo.MaxStringLength))
                {
                    this.ValidationError = this.FieldInfo.ValidationErrorMessage;
                    return false;
                }
            }

            return true;
        }
        else
        {
            this.ValidationError = GetString("EmailInput.ValidationError");
            return false;
        }
    }

    #endregion
}
