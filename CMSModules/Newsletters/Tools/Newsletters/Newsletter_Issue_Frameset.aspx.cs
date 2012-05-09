using System;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Frameset : CMSNewsletterNewslettersPage
{
    protected string issueContentUrl = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get issue object
        int issueId = QueryHelper.GetInteger("issueid", 0);
        Issue issue = IssueProvider.GetIssue(issueId);

        // Get information about author issue permission
        bool authorized = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "authorissues");

        if (issue != null)
        {
            // Get newsletter object
            Newsletter news = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);
            if (news != null)
            {
                if (news.NewsletterType == NewsletterType.Dynamic)
                {
                    // Only send page is allowed for dynamic newsletters
                    issueContentUrl = "Newsletter_Issue_Send.aspx?issueid=" + issueId.ToString();
                }
                else
                {
                    if (authorized)
                    {
                        issueContentUrl = "Newsletter_Issue_Edit.aspx?issueid=" + issueId.ToString();
                    }
                    else
                    {
                        issueContentUrl = "Newsletter_Issue_Preview.aspx?issueid=" + issueId.ToString();
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(issueContentUrl))
        {
            issueContentUrl = "Newsletter_Issue_Edit.aspx";
        }

        RegisterModalPageScripts();
    }
}
