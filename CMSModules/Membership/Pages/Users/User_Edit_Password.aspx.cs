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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.EmailEngine;
using CMS.SettingsProvider;
using CMS.EventLog;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Users_User_Edit_Password : CMSUsersPage
{
    const string hiddenPassword = "********";

    #region "Private fields"

    private int mUserID = 0;
    private UserInfo ui = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current user ID.
    /// </summary>
    private int UserID 
    {
        get 
        {
            if (this.mUserID == 0) 
            {
                this.mUserID = QueryHelper.GetInteger("userid", 0);
            }

            return this.mUserID;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ButtonSetPassword.Text = GetString("Administration-User_Edit_Password.SetPassword");
        LabelPassword.Text = GetString("Administration-User_Edit_Password.NewPassword");
        LabelConfirmPassword.Text = GetString("Administration-User_Edit_Password.ConfirmPassword");
        this.chkSendEmail.Text = GetString("Administration-User_Edit_Password.SendEmail");
        this.btnGenerateNew.Text = GetString("Administration-User_Edit_Password.gennew");
        this.btnSendPswd.Text = GetString("Administration-User_Edit_Password.sendpswd");

        imgGenPassword.ImageUrl = GetImageUrl("Objects/CMS_User/passwordgenerate.png");
        imgSendPassword.ImageUrl = GetImageUrl("Objects/CMS_User/passwordsend.png");
        
        if (!RequestHelper.IsPostBack())
        {
            if (this.UserID > 0)            
            {
                // Check that only global administrator can edit global administrator's accouns
                ui = UserInfoProvider.GetUserInfo(UserID);
                EditedObject = ui;
                CheckUserAvaibleOnSite(ui); 
                if (!CheckGlobalAdminEdit(ui))
                {
                    plcTable.Visible = false;
                    lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                    lblError.Visible = true;
                    return;
                }
   
                if (ui != null)
                {
                    if (ui.GetValue("UserPassword") != null)
                    {
                        string password = ui.GetValue("UserPassword").ToString();
                        if (password.Length > 0)
                        {
                            passStrength.TextBoxAttributes.Add("value", hiddenPassword);
                            TextBoxConfirmPassword.Attributes.Add("value", hiddenPassword);
                        }
                    }
                }
            }
        }

        // Handle 'Send password' button
        DisplaySendPaswd();
        HandleGeneratePassword();       
    }


    /// <summary>
    /// Check whether current user is allowed to modify another user. Return "" or error message.
    /// </summary>
    /// <param name="userId">Modified user</param>
    protected string ValidateGlobalAndDeskAdmin()
    {
        string result = String.Empty;

        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return result;
        }

        UserInfo userInfo = UserInfoProvider.GetUserInfo(this.UserID);
        if (userInfo == null)
        {
            result = GetString("Administration-User.WrongUserId");
        }
        else
        {
            if (userInfo.IsGlobalAdministrator)
            {
                result = GetString("Administration-User.NotAllowedToModify");
            }
        }
        return result;
    }


    #region "Event handlers"

    /// <summary>
    /// Generates new password and sends it to the user.
    /// </summary>
    protected void btnGenerateNew_Click(object sender, EventArgs e) 
    {
        // Check modify permission
        CheckModifyPermissions();
                
        string result = ValidateGlobalAndDeskAdmin();

        if (result == String.Empty)
        {
            string pswd = UserInfoProvider.GenerateNewPassword(CMSContext.CurrentSiteName);
            string userName = UserInfoProvider.GetUserNameById(this.UserID);
            UserInfoProvider.SetPassword(userName, pswd);

            // Show actual information to the user
            if (passStrength.Text != String.Empty)
            {
                passStrength.TextBoxAttributes.Add("value", hiddenPassword);
                TextBoxConfirmPassword.Attributes.Add("value", hiddenPassword);
            }
            else
            {
                passStrength.TextBoxAttributes.Add("value", "");
                TextBoxConfirmPassword.Attributes.Add("value", "");
            }

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");

            // Process e-mail sending
            SendEmail(GetString("Administration-User_Edit_Password.NewGen"), pswd, this.UserID, "changed", true);

            ReloadPassword();
        }

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Sends the actual password of the current user.
    /// </summary>
    protected void btnSendPswd_Click(object sender, EventArgs e) 
    {
        // Check permissions
        CheckModifyPermissions();

        string result = ValidateGlobalAndDeskAdmin();

        if (result == String.Empty)
        {
            string pswd = UserInfoProvider.GetUserInfo(this.UserID).GetValue("UserPassword").ToString();

            // Process e-mail sending
            SendEmail(GetString("Administration-User_Edit_Password.Resend"), pswd, this.UserID, "RESEND", false);
        }

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Sets password of current user.
    /// </summary>
    protected void ButtonSetPassword_Click(object sender, EventArgs e)
    {
        // Check modify permission
        CheckModifyPermissions();
                
        string result = ValidateGlobalAndDeskAdmin();

        if ((result == String.Empty) && (ui != null))
        {
            if (TextBoxConfirmPassword.Text == passStrength.Text)
            {
                if (passStrength.IsValid())
                {
                    if (passStrength.Text != hiddenPassword) //password has been changed
                    {
                        string pswd = this.passStrength.Text;
                        UserInfoProvider.SetPassword(ui, passStrength.Text);

                        // Show actual information to the user
                        if (passStrength.Text != String.Empty)
                        {
                            passStrength.TextBoxAttributes.Add("value", hiddenPassword);
                            TextBoxConfirmPassword.Attributes.Add("value", hiddenPassword);
                        }
                        else
                        {
                            passStrength.TextBoxAttributes.Add("value", "");
                            TextBoxConfirmPassword.Attributes.Add("value", "");
                        }

                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");

                        if (this.chkSendEmail.Checked)
                        {
                            // Process e-mail sending
                            SendEmail(GetString("Administration-User_Edit_Password.Changed"), pswd, this.UserID, "CHANGED", false);
                        }
                    }
                }
                else
                {
                    result = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                }
            }
            else
            {
                result = GetString("Administration-User_Edit_Password.PasswordsDoNotMatch");
            }
        }

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = result;
        }      
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Loads the user password to the password fields.
    /// </summary>
    private void ReloadPassword()
    {
        UserInfo ui = UserInfoProvider.GetUserInfo(this.UserID);
        if (ui != null) 
        {
            string passwd = ui.GetValue("UserPassword").ToString();
            if (!string.IsNullOrEmpty(passwd))
            {
                this.passStrength.TextBoxAttributes.Add("value", hiddenPassword);
                this.TextBoxConfirmPassword.Attributes.Add("value", hiddenPassword);
            }
        }
    }


    /// <summary>
    /// Sends e-mail with password if required.
    /// </summary>
    /// <param name="pswd">Password to send</param>
    /// <param name="toEmail">E-mail address of the mail recepient</param>
    /// <param name="subject">Subject of the e-mail sent</param>
    /// <param name="emailType">Type of the e-mail specificating the template used (NEW, CHANGED, RESEND)</param>
    /// <param name="showPassword">Indicates whether password is shown in message.</param>
    private void SendEmail(string subject, string pswd, int userId, string emailType, bool showPassword)
    {
        // Check whether the 'From' elemtn was specified
        string emailFrom = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSendPasswordEmailsFrom");
        bool fromMissing = string.IsNullOrEmpty(emailFrom);
        
        if ((!string.IsNullOrEmpty(emailType)) && (ui != null) && (!fromMissing))
        {
            if (!string.IsNullOrEmpty(ui.Email))
            {
                EmailMessage em = new EmailMessage();

                em.From = emailFrom;
                em.Recipients = ui.Email;
                em.Subject = subject;
                em.EmailFormat = EmailFormatEnum.Default;

                string templateName = null;

                // Get e-mail template name
                switch (emailType.ToLower())
                {
                    case "new":
                        templateName = "Membership.NewPassword";
                        break;

                    case "changed":
                        templateName = "Membership.ChangedPassword";
                        break;

                    case "resend":
                        templateName = "Membership.ResendPassword";
                        break;

                    default:
                        break;
                }

                // Get template info object
                if (templateName != null)
                {
                    try
                    {
                        // Get e-mail template
                        EmailTemplateInfo template = EmailTemplateProvider.GetEmailTemplate(templateName, null);
                        if (template != null)
                        {
                            em.Body = template.TemplateText;

                            // Macros
                            string[,] macros = new string[2, 2];
                            macros[0, 0] = "UserName";
                            macros[0, 1] = ui.UserName;
                            macros[1, 0] = "Password";
                            macros[1, 1] = pswd;
                            // Create macro resolver
                            ContextResolver resolver = CMSContext.CurrentResolver;
                            resolver.SourceParameters = macros;

                            // Add template attachments
                            MetaFileInfoProvider.ResolveMetaFileImages(em, template.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                            // Send message immediately (+ resolve macros)
                            EmailSender.SendEmailWithTemplateText(CMSContext.CurrentSiteName, em, template, resolver, true);

                            // Inform on success
                            this.lblInfo.Text += " " + GetString("Administration-User_Edit_Password.PasswordsSent") + " " + HTMLHelper.HTMLEncode(ui.Email);
                            this.lblInfo.Visible = true;

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error to the event log
                        EventLogProvider eventLog = new EventLogProvider();
                        eventLog.LogEvent("Password retrieval", "USERPASSWORD", ex);
                        this.lblError.Text = "Failed to send the password: " + ex.Message;
                    }
                }
            }
            else 
            {
                // Inform on error
                this.lblInfo.Visible = true;
                if (showPassword)
                {
                    this.lblInfo.Text = string.Format(GetString("Administration-User_Edit_Password.passshow"), pswd);
                }
                else
                {
                    this.lblInfo.Text = GetString("Administration-User_Edit_Password.PassChangedNotSent");
                }

                return;
            }
        }

        // Inform on error
        this.lblError.Visible = true;
        this.lblError.Text = GetString("Administration-User_Edit_Password.PasswordsNotSent");

        if (fromMissing)
        {            
            this.lblError.Text += GetString("Administration-User_Edit_Password.FromMissing") + " ";
        }
    }


    /// <summary>
    /// Decides whether the 'Send password' button should be enabled or not.
    /// </summary>
    private void DisplaySendPaswd()
    {
        if (ui == null)
        {
            ui = UserInfoProvider.GetUserInfo(this.UserID);
        }

        if (ui != null)
        {
            // Password is stored in plain text, allow sending
            if (string.IsNullOrEmpty(ui.UserPasswordFormat) && !string.IsNullOrEmpty(ui.Email))
            {                
                return;
            }
        }

        this.btnSendPswd.Visible = false;
        imgSendPassword.Visible = false;
        
    }


    /// <summary>
    /// Decides whether enable genererate new password e-mail. 
    /// </summary>
    private void HandleGeneratePassword()
    {
        if (ui == null)
        {
            ui = UserInfoProvider.GetUserInfo(this.UserID);
        }

        if (ui != null)
        {
            if (string.IsNullOrEmpty(ui.Email))
            {
                btnGenerateNew.OnClientClick = "return confirm('" + GetString("user.showpasswarning") + "');";                
            }            
        }
    }


    /// <summary>
    /// Checks if the user is alloed to perform this action.
    /// </summary>
    private void CheckModifyPermissions()
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        } 
    }

    #endregion
}
