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

public partial class CMSModules_Membership_FormControls_Users_UserName : FormEngineUserControl
{
    private bool mUseDefaultValidationGroup = true;

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

            this.txtUserName.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtUserName.Text;
        }
        set
        {
            txtUserName.Text = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// If true validator has default validation group set.
    /// </summary>
    public bool UseDefaultValidationGroup
    {
        get
        {
            return mUseDefaultValidationGroup;
        }
        set
        {
            mUseDefaultValidationGroup = value;
        }
    }


    /// <summary>
    /// Gets inner textbox.
    /// </summary>
    public TextBox TextBox
    {
        get
        {
            return txtUserName;
        }
    }


    /// <summary>
    /// Clears current value.
    /// </summary>
    public void Clear()
    {
        txtUserName.Text = "";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Apply CSS styles
        if (!String.IsNullOrEmpty(CssClass))
        {
            txtUserName.CssClass = CssClass;
            CssClass = null;
        }
        else if (String.IsNullOrEmpty(txtUserName.CssClass))
        {
            txtUserName.CssClass = "TextBoxField";
        }

        if (!String.IsNullOrEmpty(ControlStyle))
        {
            txtUserName.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }

        CheckRegularExpression = true;
        CheckMinMaxLength = true;
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Get appropriate validation message
        string userValidationString = ValidationHelper.UseSafeUserName ? "general.safeusernamevalidation" : "general.usernamevalidation";

        // For custom regular expression use general validation message
        if (ValidationHelper.CustomUsernameRegExpString != null)
        {
            userValidationString = "general.customusernamevalidation";
        }

        if ((this.FieldInfo == null) || !this.FieldInfo.AllowEmpty || !(txtUserName.Text.Length == 0))
        {
            string result = new Validator().NotEmpty(txtUserName.Text, GetString("Administration-User_New.RequiresUserName")).IsUserName(txtUserName.Text, GetString(userValidationString)).Result;

            if (!String.IsNullOrEmpty(result))
            {
                this.ValidationError = result;
                return false;
            }
        }
        return true;
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        RequiredFieldValidatorUserName.ErrorMessage = GetString("Administration-User_New.RequiresUserName");

        if (UseDefaultValidationGroup)
        {
            RequiredFieldValidatorUserName.ValidationGroup = "ConfirmRegForm";
        }
    }
}
