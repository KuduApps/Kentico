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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Membership_FormControls_Passwords_ChangePassword : CMSUserControl
{
    protected bool mAllowEmptyPassword = false;


    /// <summary>
    /// Indicates whether to allow to save empty password.
    /// </summary>
    public bool AllowEmptyPassword
    {
        get
        {
        	 return mAllowEmptyPassword; 
        }
        set
        {
        	 mAllowEmptyPassword = value; 
        }
    }


    /// <summary>
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["StopProcessing"], false);
        }
        set
        {
            ViewState["StopProcessing"] = value;
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            // Show only to authenticated users, if intended to be displayed
            if (this.Visible)
            {
                this.Visible = CMSContext.CurrentUser.IsAuthenticated();
            }

            lblExistingPassword.Text = GetString("MyAccount.Password.ExistingPassword");
            lblPassword1.Text = GetString("MyAccount.Password.NewPassword");
            lblPassword2.Text = GetString("MyAccount.Password.ConfirmPassword");
            btnOk.Text = GetString("MyAccount.Password.SetPassword");
        }
    }


    /// <summary>
    /// On btnOK click, save changed password.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        UserInfo ui = CMSContext.CurrentUser;
        SiteInfo si = CMSContext.CurrentSite;

        if ((ui != null) && (si != null))
        {
            // Authenticate current user
            ui = UserInfoProvider.AuthenticateUser(ui.UserName, txtExistingPassword.Text, si.SiteName, false);

            if (ui != null)
            {
                if ((!mAllowEmptyPassword) && (DataHelper.IsEmpty(passStrength.Text.Trim())))
                {
                    lblError.Text = GetString("myaccount.password.emptypassword");
                }
                else
                {
                    if (passStrength.Text == txtPassword2.Text)
                    {
                        // Check policy
                        if (!passStrength.IsValid())
                        {
                            lblError.Text = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                        }
                        else
                        {
                            UserInfoProvider.SetPassword(ui.UserName, passStrength.Text);
                            lblInfo.Text = GetString("General.ChangesSaved");
                        }
                    }
                    else
                    {
                        // New and confirmed password are not equal
                        lblError.Text = GetString("Administration-User_Edit_Password.PasswordsDoNotMatch");
                    }
                }
            }
            else
            {
                // Incorrect existing password
                lblError.Text = GetString("myaccount.password.incorrectexistingpassword");
            }
        }
    }
}