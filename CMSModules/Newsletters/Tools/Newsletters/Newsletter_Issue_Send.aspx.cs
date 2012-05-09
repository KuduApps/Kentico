using System;
using System.Data.SqlTypes;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Send : CMSNewsletterNewslettersPage
{
    #region "Variables"

    protected int issueId = 0;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get issue ID from querystring
        issueId = QueryHelper.GetInteger("issueid", 0);

        // Get newsletter issue and check its existence
        Issue issue = IssueProvider.GetIssue(issueId);
        EditedObject = issue;

        btnSend.Enabled = true;

        // Display information wheather the issue was sent
        bool isSent = (issue.IssueMailoutTime != DateTimeHelper.ZERO_TIME) && (issue.IssueMailoutTime < DateTime.Now);
        lblSent.Text = isSent ?
            GetString("Newsletter_Issue_Header.AlreadySent") :
            GetString("Newsletter_Issue_Header.NotSentYet");

        if (!RequestHelper.IsPostBack())
        {
            // Fill draft emails box
            Newsletter newsletter = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);
            EditedObject = newsletter;
            txtSendDraft.Text = newsletter.NewsletterDraftEmails;
        }

        calendarControl.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";

        string scriptBlock = "var wopener = parent.wopener; function RefreshPage() { wopener.RefreshPage(); }";
        ScriptHelper.RegisterClientScriptBlock(this, this.GetType(), "RefreshPage", scriptBlock, true);
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        // Check permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "authorissues"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "authorissues");
        }

        // Depending on action chosen, send the issue
        if (radSendNow.Checked)
        {
            SendNow();
        }
        else if (radSchedule.Checked)
        {
            SendScheduled();
        }
        else if (radSendDraft.Checked)
        {
            SendDraft();
        }

        // Close window on success
        if (!lblError.Visible)
        {
            ScriptHelper.RegisterStartupScript(this, this.GetType(), "ClosePage", "RefreshPage(); setTimeout('top.window.close()',200);", true);
        }
    }


    protected void radGroupSend_CheckedChanged(object sender, EventArgs e)
    {
        calendarControl.Enabled = radSchedule.Checked;
        txtSendDraft.Enabled = radSendDraft.Checked;
    }


    private void SendNow()
    {
        IssueProvider.SendIssue(issueId);
    }


    private void SendScheduled()
    {
        if (calendarControl.IsValidRange() && (calendarControl.SelectedDateTime != DateTimeHelper.ZERO_TIME))
        {
            IssueProvider.SendIssue(issueId, calendarControl.SelectedDateTime);
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = GetString("newsletter.incorrectdate");
        }
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