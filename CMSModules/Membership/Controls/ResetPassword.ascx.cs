using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls ;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;


public partial class CMSModules_Membership_Controls_ResetPassword : CMSUserControl
{
    #region "Variables"

    string siteName = string.Empty;
    double interval = 0;
    string hash = string.Empty;
    string time = string.Empty;
    int userID = 0;

    #endregion


    #region "Properties"
    
    /// <summary>
    /// Text shown if request hash isn't found.
    /// </summary>
    public string InvalidRequestText
    {
        get;
        set;
    }


    public string ExceededIntervalText
    {
        get;
        set;
    }


    /// <summary>
    /// Url on which is user redirected after successful password reset.
    /// </summary>
    public string RedirectUrl
    {
        get;
        set;
    }

    /// <summary>
    /// E-mail address from which e-mail is sent.
    /// </summary>
    public string SendEmailFrom
    {
        get;
        set;
    }


    /// <summary>
    /// Text shown when password reset was succesful.
    /// </summary>
    public string SuccessText
    {
        get;
        set;
    }


    #endregion


    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {        
        userID = ValidationHelper.GetInteger(SessionHelper.GetValue("UserPasswordRequestID"), 0);

        hash = QueryHelper.GetString("hash", string.Empty) ;
        time = QueryHelper.GetString("datetime", string.Empty);

        btnReset.Text = GetString("general.reset");                
        rfvConfirmPassword.Text = GetString("general.requiresvalue");

        siteName = CMSContext.CurrentSiteName;

        // Get interval from settings
        interval = SettingsKeyProvider.GetDoubleValue(siteName + ".CMSResetPasswordInterval");

        // Prepare failed message
        string invalidRequestMessage = DataHelper.GetNotEmpty(InvalidRequestText, String.Format(ResHelper.GetString("membership.passwresetfailed"), ResolveUrl("~/cmspages/logon.aspx?forgottenpassword=1")));

        // Reset password cancelation
        if(QueryHelper.GetBoolean("cancel", false))
        {
            // Get user info
            UserInfo ui = UserInfoProvider.GetUserInfoWithSettings("UserPasswordRequestHash = '" + SqlHelperClass.GetSafeQueryString(hash, true) + "'");
            if (ui != null)
            {
                ui.UserPasswordRequestHash = null;
                UserInfoProvider.SetUserInfo(ui);

                SessionHelper.Remove("UserPasswordRequestID");

                lblInfo.Visible = true;
                lblInfo.Text = GetString("membership.passwresetcancelled");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = invalidRequestMessage;
            }

            pnlReset.Visible = false;
            return;
        }


        // Reset password request
        if (!URLHelper.IsPostback())
        {
            // Get user info
            UserInfo ui = UserInfoProvider.GetUserInfoWithSettings("UserPasswordRequestHash = '" + SqlHelperClass.GetSafeQueryString(hash, true) + "' OR UserID = " + userID);

            // Validate request
            ResetPasswordResultEnum result = UserInfoProvider.ValidateResetPassword(ui, hash, time, interval, "Reset password control");

            // Prepare messages
            string timeExceededMessage = DataHelper.GetNotEmpty(ExceededIntervalText, String.Format(ResHelper.GetString("membership.passwreqinterval"), ResolveUrl("~/cmspages/logon.aspx?forgottenpassword=1")));
            string resultMessage = string.Empty;

            // Check result
            switch (result)
            {
                case ResetPasswordResultEnum.Success:
                    // Save user is to session                    
                    SessionHelper.SetValue("UserPasswordRequestID", ui.UserID);

                    // Delete it from user info
                    ui.UserPasswordRequestHash = null ;
                    UserInfoProvider.SetUserInfo(ui);
                    
                    break;

                case ResetPasswordResultEnum.TimeExceeded:
                    resultMessage = timeExceededMessage;
                    break;

                default:
                    resultMessage = invalidRequestMessage;
                    break;
            }

            if (!string.IsNullOrEmpty(resultMessage))
            {
                // Show error message
                lblError.Visible = true;
                lblError.Text = resultMessage;

                pnlReset.Visible = false;

                return;
            }
        }
    }


    /// <summary>
    /// Click event of btnOk.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if ((passStrength.Text.Length > 0) && rfvConfirmPassword.IsValid)
        {
            if (passStrength.Text == txtConfirmPassword.Text)
            {
                // Check policy
                if (passStrength.IsValid())
                {
                    // Get e-mail address of sender
                    string emailFrom = DataHelper.GetNotEmpty(SendEmailFrom, SettingsKeyProvider.GetStringValue(siteName + ".CMSSendPasswordEmailsFrom"));                   

                    // Try to reset password and show result to user
                    bool success;
                    lblInfo.Text = UserInfoProvider.ResetPassword(hash, time, userID, interval, passStrength.Text, "Reset password control", emailFrom, siteName, null, out success, InvalidRequestText, ExceededIntervalText);
                    
                    // If password reset was successful
                    if (success)
                    {                       
                        SessionHelper.Remove("UserPasswordRequestID");

                        // Redirect to specified url 
                        if (!string.IsNullOrEmpty(RedirectUrl))
                        {
                            URLHelper.Redirect(RedirectUrl);
                        }

                        // Get proper text
                        lblInfo.Text = DataHelper.GetNotEmpty(SuccessText, lblInfo.Text);
                        lblInfo.Visible = true;
                    }
                    else
                    {
                        lblError.Text = lblInfo.Text;
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                }                
            }
            else
            {
                lblError.Visible = true;
                lblError.ResourceString = "passreset.notmatch";
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.ResourceString = "general.requiresvalue";
        }
    }

    #endregion
}

