using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_Membership_FormControls_Passwords_EncryptedPassword : FormEngineUserControl
{
    #region "Constants"

    const string hiddenPassword = "********";
    
    #endregion

    
    #region "Private properties"

    /// <summary>
    /// Crypted password.
    /// </summary>
    private string CryptedPassword
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CryptedPassword"], string.Empty);
        }
        set
        {
            ViewState["CryptedPassword"] = value;
        }
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns encrypted password.
    /// </summary>
    public override object Value
    {
        get
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Attributes.Add("value", string.Empty);
                return string.Empty;
            }                        
            
            if (txtPassword.Text == hiddenPassword)
            {
                return CryptedPassword;
            }

            txtPassword.Attributes.Add("value", hiddenPassword);
            return EncryptionHelper.EncryptData(txtPassword.Text);
        }
        set
        {
            CryptedPassword = ValidationHelper.GetString(value, string.Empty);
             
            if (!string.IsNullOrEmpty(CryptedPassword))
            {
                txtPassword.Attributes.Add("value", hiddenPassword);
            }
            else
            {
                txtPassword.Attributes.Add("value", string.Empty);
            }
        }
    }


    /// <summary>
    /// Indicates whether control is enabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return txtPassword.Enabled;
        }
        set
        {
            txtPassword.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets max length.
    /// </summary>
    public int MaxLength
    {
        get
        {
            return txtPassword.MaxLength;
        }
        set
        {
            txtPassword.MaxLength = value;
        }
    }

    #endregion
}
