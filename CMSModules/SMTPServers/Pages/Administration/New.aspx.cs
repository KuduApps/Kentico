using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.UIControls;

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "SMTPServer_Edit.ItemListLink", "List.aspx", null)]
[Breadcrumb(1, "SMTPServer_Edit.NewItemCaption")]

// Title
[Title("Objects/CMS_SMTPServer/new.png", "SMTPServer_New.HeaderCaption", "new_smtpserver")]

public partial class CMSModules_SMTPServers_Pages_Administration_New : CMSSMTPServersPage
{
    #region "Variables"

    private CurrentSiteInfo currentSite;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentSite = CMSContext.CurrentSite;
        if (currentSite != null)
        {
            chkAssign.Text = string.Format("{0} {1}", GetString("General.AssignWithWebSite"), currentSite.DisplayName);
            chkAssign.Visible = true;
        }

        rfvServerName.ErrorMessage = GetString("SMTPServer_New.NoServerName");

        ddlPriorities.Items.Add(new ListItem("SMTPServerPriorityEnum.Low", SMTPServerPriorityEnum.Low.ToString()));
        ddlPriorities.Items.Add(new ListItem("SMTPServerPriorityEnum.Normal", SMTPServerPriorityEnum.Normal.ToString()));
        ddlPriorities.Items.Add(new ListItem("SMTPServerPriorityEnum.High", SMTPServerPriorityEnum.High.ToString()));

        if (!RequestHelper.IsPostBack())
        {
            ddlPriorities.SelectedIndex = 1;
        }
    }


    protected void btnOk_Click(object semder, EventArgs e)
    {
        Save();
    }


    private void Save()
    {
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
            SMTPServerPriorityEnum priority = (SMTPServerPriorityEnum)Enum.Parse(typeof(SMTPServerPriorityEnum), ddlPriorities.SelectedValue); 

            SMTPServerInfo smtpServer =
                SMTPServerInfoProvider.CreateSMTPServer(txtServerName.Text, txtUserName.Text, txtPassword.Text, chkUseSSL.Checked, priority);

            if (chkAssign.Checked && currentSite != null)
            {
                SMTPServerSiteInfoProvider.AddSMTPServerToSite(smtpServer.ServerID, currentSite.SiteID);
            }

            URLHelper.Redirect(string.Format("Frameset.aspx?smtpserverid={0}&saved=1", smtpServer.ServerID));
        }
        catch (Exception e)
        {
            ShowError(e.Message);
            return;
        }
    }

    #endregion
}