using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Send : CMSNewsletterNewslettersPage
{
    #region "Variables"

    protected int newsletterId;


    private int issueId;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        if (newsletterId == 0)
        {
            return;
        }

        btnSend.Enabled = true;

        if (!RequestHelper.IsPostBack())
        {
            // Fill draft emails box
            Newsletter newsletter = NewsletterProvider.GetNewsletter(newsletterId);
            txtSendDraft.Text = newsletter.NewsletterDraftEmails;
        }
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        // Check permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "authorissues"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "authorissues");
        }

        // Generate new issue
        try
        {
            issueId = EmailQueueManager.GenerateDynamicIssue(newsletterId);
            if (issueId <= 0)
            {
                return;
            }

            // Depending on action chosen, send the issue
            if (radSendNow.Checked)
            {
                SendNow();
            }            
            else if (radSendDraft.Checked)
            {
                SendDraft();
            }

            lblInfo.Visible = true;
            lblInfo.Text = GetString("Newsletter_Send.SuccessfullySent");
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = GetString("Newsletter_Send.ErrorSent") + ex.Message;
        }
    }


    protected void radGroupSend_CheckedChanged(object sender, EventArgs e)
    {        
        txtSendDraft.Enabled = radSendDraft.Checked;
    }


    private void SendNow()
    {
        IssueProvider.SendIssue(issueId);
    }


    private void SendDraft()
    {
        if (string.IsNullOrEmpty(txtSendDraft.Text))
        {
            lblError.Visible = true;
            lblError.Text = GetString("newsletter.recipientsmissing");
        }
        else if (!ValidationHelper.AreEmails(txtSendDraft.Text))
        {
            lblError.Visible = true;
            lblError.Text = GetString("newsletter.wrongemailformat");
        }
        else
        {
            IssueProvider.SendIssue(issueId, txtSendDraft.Text);
        }
    }

    #endregion
}