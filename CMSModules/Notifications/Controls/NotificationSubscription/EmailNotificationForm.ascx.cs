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

using CMS.Notifications;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.EventLog;

public partial class CMSModules_Notifications_Controls_NotificationSubscription_EmailNotificationForm : CMSNotificationGatewayForm
{
    #region "Variables"

    private bool mEnableMultipleEmails = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the e-mail(s) from/to textbox.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.txtEmail.Text.Trim();
        }
        set
        {
            this.txtEmail.Text = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to allow multiple e-mails separated with semicolon.
    /// </summary>
    public bool EnableMultipleEmails
    {
        get
        {
            return this.mEnableMultipleEmails;
        }
        set
        {
            this.mEnableMultipleEmails = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblEmail.Text = (this.EnableMultipleEmails ? GetString("general.emails") : GetString("general.email")) + ResHelper.Colon;

        // Fill in the default e-mail
        if ((!RequestHelper.IsPostBack()) && (CMSContext.CurrentUser != null) && (!String.IsNullOrEmpty(CMSContext.CurrentUser.Email)))
        {
            this.txtEmail.Text = CMSContext.CurrentUser.Email;
        }
    }


    #region "Public methods"

    /// <summary>
    /// Checks whether the input is correct e-mail address (or multiple e-mail addresses).
    /// </summary>
    public override string Validate()
    {
        if (this.EnableMultipleEmails)
        {
            if (!ValidationHelper.AreEmails(this.txtEmail.Text.Trim()))
            {
                return GetString("notifications.emailgateway.formats");
            }
        }
        else
        {
            if (!ValidationHelper.IsEmail(this.txtEmail.Text.Trim()))
            {
                return GetString("notifications.emailgateway.format");
            }
        }

        return String.Empty;
    }


    /// <summary>
    /// Clears the e-mail textbox field.
    /// </summary>
    public override void ClearForm()
    {
        this.txtEmail.Text = "";
    }

    #endregion
}
