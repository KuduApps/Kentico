using System;

using CMS.Controls;
using CMS.Newsletter;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

// Set edited object
[EditedObject("newsletter.issue", "issueid")]

// Set title
[Title("Objects/Newsletter_Issue/object.png", "Newsletter_Issue_List.HeaderCaption", "edit_tab")]

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Header : CMSNewsletterNewslettersPage
{
    protected Issue issue;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get newsletter issue ID from querystring
        Issue issue = EditedObject as Issue;
        if (issue != null)
        {
            InitalizeMenu(issue);
        }
    }


    /// <summary>
    /// Initializes header menu.
    /// </summary>
    /// <param name="issue">Issue object</param>
    protected void InitalizeMenu(Issue issue)
    {
        // Get newsletter
        Newsletter news = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);
        if (news == null)
        {
            return;
        }

        InitTabs(3, "newsletterIssueContent");

        // Show only 'Send' tab for dynamic newsletter
        if (news.NewsletterType == NewsletterType.Dynamic)
        {
            SetTab(0, GetString("Newsletter_Issue_Header.Send"), "Newsletter_Issue_Send.aspx?issueid=" + issue.IssueID, "SetHelpTopic('helpTopic', 'send_tab');");

            // Set proper context help page
            SetHelp("send_tab", "helpTopic");
        }
        else
        {
            // Show 'Edit' and 'Send' tabs only to authorized users
            if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "authorissues"))
            {
                SetTab(0, GetString("General.Edit"), "Newsletter_Issue_Edit.aspx?issueid=" + issue.IssueID, "SetHelpTopic('helpTopic', 'edit_tab');");
                SetTab(1, GetString("Newsletter_Issue_Header.Send"), "Newsletter_Issue_Send.aspx?issueid=" + issue.IssueID, "SetHelpTopic('helpTopic', 'send_tab');");
            }
            // Add 'Preview' tab
            SetTab(2, GetString("Newsletter_Issue_Header.Preview"), "Newsletter_Issue_Preview.aspx?issueid=" + issue.IssueID, "SetHelpTopic('helpTopic', 'preview_tab');");
        }
    }
}
