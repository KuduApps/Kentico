using System;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Preview : CMSNewsletterNewslettersPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int newsletterIssueId = QueryHelper.GetInteger("issueid", 0);
        if (newsletterIssueId > 0)
        {
            // Get newsletter issue and check its existence
            Issue issue = IssueProvider.GetIssue(newsletterIssueId);
            EditedObject = issue;
            bool isSent = (issue.IssueMailoutTime == DateTimeHelper.ZERO_TIME ? false : (issue.IssueMailoutTime < DateTime.Now));

            lblSent.Text = isSent
                               ? GetString("Newsletter_Issue_Header.AlreadySent")
                               : GetString("Newsletter_Issue_Header.NotSentYet");
        }
        else
        {
            preview.Visible = false;
        }
    }
}
