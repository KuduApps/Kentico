using System;
using System.Data.SqlTypes;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_New_Send : CMSNewsletterNewslettersPage
{
    #region "Variables"

    protected int issueId = 0;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get issue ID from querystring
        issueId = QueryHelper.GetInteger("issueid", 0);
        Issue issue = IssueProvider.GetIssue(issueId);
        if (issue == null)
        {
            return;
        }

        btnFinish.Enabled = true;

        // Initializes page title control
        this.CurrentMaster.Title.TitleText = GetString("newsletter_issue_list.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Newsletter_Issue/new.png");

        // Initialize controls' labels
        ucHeader.Title = GetString("Newsletter_Issue_New_Send.Step3");
        ucHeader.Header = GetString("Newsletter_Issue_New_Send.header");
        ucHeader.DescriptionVisible = false;

        if (!RequestHelper.IsPostBack())
        {
            // Fill draft emails box
            Newsletter newsletter = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);
            txtSendDraft.Text = newsletter.NewsletterDraftEmails;
        }

        calendarControl.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";

        RegisterModalPageScripts();
        ScriptHelper.RegisterClientScriptBlock(this, this.GetType(), "RefreshPage", "function RefreshPage() { wopener.RefreshPage(); }", true);
    }


    protected void btnFinish_Click(object sender, EventArgs e)
    {
        // Check 'Author issues' permission
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
        if (IsValidDate(calendarControl.SelectedDateTime))
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


    private static bool IsValidDate(DateTime date)
    {
        return (date > SqlDateTime.MinValue.Value) && (date < SqlDateTime.MaxValue.Value);
    }

    #endregion
}