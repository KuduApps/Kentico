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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Membership_Controls_PersonalSettings : CMSUserControl
{
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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            // Strings
            lblAvatar.Text = GetString("MyAccount.ForumAvatar");
            lblNickName.Text = GetString("MyAccount.ForumNickName");
            lblSignature.Text = GetString("MyAccount.ForumSignature");
            lblEmail.Text = GetString("MyAccount.MessagingEmail");
            btnOk.Text = GetString("General.Ok");

            UserInfo cui = UserInfoProvider.GetUserInfo(CMSContext.CurrentUser.UserID);

            if (cui != null)
            {
                // Load values
                if (!RequestHelper.IsPostBack())
                {
                    txtNickName.Text = cui.UserNickName;
                    txtSignature.Text = cui.UserSignature;
                    txtEmail.Text = cui.UserMessagingNotificationEmail;
                }

                // Set user picture form control
                UserPictureFormControl.UserInfo = cui;
            }

            // IE6 design fix
            if (CMSContext.GetBrowserClass() == "IE6")
            {
                this.txtSignature.Width = 280;
            }
        }
    }


    /// <summary>
    /// OK click handler.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        CMSContext.CurrentUser.UserNickName = txtNickName.Text;
        CMSContext.CurrentUser.UserSignature = txtSignature.Text;

        UserInfo ui = UserInfoProvider.GetUserInfo(CMSContext.CurrentUser.UserID);
        if (ui != null)
        {
            string email = txtEmail.Text.Trim();
            if ((email != "") && (!ValidationHelper.IsEmail(email)))
            {
                lblError.Visible = true;
                lblError.Text = GetString("MyAccount.ErrorEmail");
            }
            else
            {
                // Check whether email is unique if it is required
                if (!UserInfoProvider.IsEmailUnique(email, ui))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("UserInfo.EmailAlreadyExist");
                    return;
                }

                UserPictureFormControl.UpdateUserPicture(ui);

                ui.UserNickName = txtNickName.Text;
                ui.UserSignature = txtSignature.Text;
                ui.UserMessagingNotificationEmail = (email == "") ? null : email;

                UserInfoProvider.SetUserInfo(ui);

                UserPictureFormControl.UserInfo = ui;

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
        }
    }
}
