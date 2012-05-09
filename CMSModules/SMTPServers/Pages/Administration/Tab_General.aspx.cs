using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

// Edited object
[EditedObject(PredefinedObjectType.SMTPSERVER, "smtpserverid", ExistingObject = true)]

public partial class CMSModules_SMTPServers_Pages_Administration_Tab_General : CMSSMTPServersPage
{
    #region "Variables"

    private SMTPServerInfo smtpServer;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        smtpServer = (SMTPServerInfo)EditedObject;

        rfvServerName.ErrorMessage = GetString("SMTPServer_New.NoServerName");

        ddlPriorities.Items.Add(new ListItem("SMTPServerPriorityEnum.Low", SMTPServerPriorityEnum.Low.ToString()));
        ddlPriorities.Items.Add(new ListItem("SMTPServerPriorityEnum.Normal", SMTPServerPriorityEnum.Normal.ToString()));
        ddlPriorities.Items.Add(new ListItem("SMTPServerPriorityEnum.High", SMTPServerPriorityEnum.High.ToString()));

        if (!RequestHelper.IsPostBack())
        {
            LoadValues();
        }
    }


    protected void btnOk_Click(object semder, EventArgs e)
    {
        Save();
    }


    /// <summary>
    /// Saves current form
    /// </summary>
    private void Save()
    {
        if (smtpServer == null)
        {
            return;
        }

        // Validate input
        string validationResult = new Validator()
            .NotEmpty(txtServerName.Text, rfvServerName.ErrorMessage)
            .Result;

        if (!string.IsNullOrEmpty(validationResult))
        {
            ShowError(validationResult);
            return;
        }

        try
        {
            smtpServer.ServerName = txtServerName.Text;
            smtpServer.ServerUserName = txtUserName.Text;
            smtpServer.ServerPassword = encryptedPassword.Value.ToString();
            smtpServer.ServerUseSSL = chkUseSSL.Checked;
            smtpServer.ServerPriority = (SMTPServerPriorityEnum)Enum.Parse(typeof(SMTPServerPriorityEnum), ddlPriorities.SelectedValue);
            smtpServer.ServerEnabled = chkEnabled.Checked;
            
            // Save changes
            SMTPServerInfoProvider.SetSMTPServerInfo(smtpServer);

            ShowInformation(GetString("General.ChangesSaved"));

            // Refresh tab header
            ScriptHelper.RefreshTabHeader(this, null);
        }
        catch (Exception e)
        {
            ShowError(e.Message);
            return;
        }
    }


    // Load editing form
    private void LoadValues()
    {
        txtServerName.Text = smtpServer.ServerName;
        txtUserName.Text = smtpServer.ServerUserName;
        encryptedPassword.Value = smtpServer.ServerPassword;
        chkUseSSL.Checked = smtpServer.ServerUseSSL;
        ddlPriorities.SelectedIndex = ((int)smtpServer.ServerPriority) + 1;
        chkEnabled.Checked = smtpServer.ServerEnabled;
    }

    #endregion
}