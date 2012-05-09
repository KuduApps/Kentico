using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.FormControls;

public partial class CMSFormControls_System_Password : FormEngineUserControl
{
    #region "Variables"

    /// <summary>
    /// Default hidden password value.
    /// </summary>
    private string hiddenPassword = "********";

    /// <summary>
    /// Current password.
    /// </summary>
    private string mPassword = String.Empty;

    /// <summary>
    /// Indicates whether textbox should be filled.
    /// </summary>
    private bool fillPassword = false;

    /// <summary>
    /// Indicates whether load phase finished.
    /// </summary>
    private bool loadFinished = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.Password;
        }
        set
        {
            this.Password = Convert.ToString(value);
        }
    }


    /// <summary>
    /// Gets or sets the enabled.
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
    /// Gets or sets the current password.
    /// </summary>
    public string Password
    {
        get
        {
            // Get password from textbox if is set
            if (loadFinished && RequestHelper.IsPostBack() && (String.Compare(txtPassword.Text, hiddenPassword) != 0))
            {
                this.Password = txtPassword.Text;
            }
            else
            {
                mPassword = GetPassword();
            }
            return mPassword;
        }
        set
        {
            mPassword = value;
            SetPassword(mPassword);
        }
    }


    /// <summary>
    /// Returns ClientID of the textbox with password.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.txtPassword.ClientID;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// OnLoad - Set loading flag.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckMinMaxLength = true;
        CheckRegularExpression = true;
        loadFinished = true;
    }


    /// <summary>
    /// Encrypt and save password. Encryption method is not implemented yet!
    /// </summary>
    /// <param name="password">Password value</param>
    protected void SetPassword(string password)
    {
        fillPassword = false;
        if (!String.IsNullOrEmpty(password))
        {
            fillPassword = true;
        }

        mPassword = password;
    }


    /// <summary>
    /// Decrypt and returns password. Decryption method is not implemented yet!
    /// </summary>
    protected string GetPassword()
    {
        return ValidationHelper.GetString(mPassword, String.Empty);
    }


    /// <summary>
    /// OnPreRender override - set password text.
    /// </summary>
    /// <param name="e">EventArgs</param>
    protected override void OnPreRender(EventArgs e)
    {
        if (fillPassword)
        {
            txtPassword.Attributes.Add("value", hiddenPassword);
        }
        else
        {
            txtPassword.Attributes.Remove("value");
        }

        base.OnPreRender(e);
    }

    #endregion
}
