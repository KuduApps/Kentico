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
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Membership_FormControls_Passwords_PasswordConfirmator : FormEngineUserControl
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
            txtConfirmPassword.Enabled = value;
            passStrength.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return passStrength.Value;            
        }
        set
        {
        }
    }   

    #endregion


    #region "Page events"

    /// <summary>
    /// Page load event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        passStrength.ShowStrengthIndicator = ValidationHelper.GetBoolean(GetValue("showstrength"), true);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {        
        if (passStrength.Text != txtConfirmPassword.Text)
        {
            this.ValidationError = GetString("PassConfirmator.PasswordDoNotMatch");
            return false;
        }

        // Check regular expresion
        if ((!String.IsNullOrEmpty(this.FieldInfo.RegularExpression)) && (new Validator().IsRegularExp(passStrength.Text, this.FieldInfo.RegularExpression, "error").Result == "error"))
        {
            this.ValidationError = GetString("PassConfirmator.InvalidPassword");
            return false;
        }

        // Check min lenght
        if ((this.FieldInfo.MinStringLength > 0)&&(passStrength.Text.Length < this.FieldInfo.MinStringLength))
        {
            this.ValidationError = string.Format(GetString("PassConfirmator.PasswordLength"), this.FieldInfo.MinStringLength);
            return false;
        }       

        // Check password policy
        if (!passStrength.IsValid())
        {
            this.ValidationError = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
            return false;
        }

        return true;
    }

    #endregion
}
