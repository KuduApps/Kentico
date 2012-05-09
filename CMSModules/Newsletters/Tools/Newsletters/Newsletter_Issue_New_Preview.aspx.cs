using System;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_New_Preview : CMSNewsletterNewslettersPage
{
    protected int newsletterIssueId = 0;
    protected string backUrl = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnBack.Text = "< " + GetString("general.back");
        btnNext.Text = GetString("general.next") + " >";
        btnClose.Text = GetString("general.close");

        ucHeader.Title = GetString("Newsletter_Issue_New_Preview.Step2");
        ucHeader.Header = GetString("Newsletter_Issue_New_Preview.header");
        ucHeader.DescriptionVisible = false;

        this.CurrentMaster.Title.TitleText = GetString("newsletter_issue_list.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Newsletter_Issue/new.png");

        newsletterIssueId = QueryHelper.GetInteger("issueid", 0);
        if (newsletterIssueId == 0)
        {
            btnNext.Enabled = false;
            int newsletterId = QueryHelper.GetInteger("newsletterid", 0);
            backUrl = "Newsletter_Issue_New_Edit.aspx?newsletterid=" + newsletterId.ToString();
            return;
        }

        backUrl = "Newsletter_Issue_New_Edit.aspx?issueid=" + newsletterIssueId.ToString();
        Issue issue = IssueProvider.GetIssue(newsletterIssueId);

        RegisterModalPageScripts();
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(backUrl);
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect("Newsletter_Issue_New_Send.aspx?issueid=" + newsletterIssueId.ToString());
    }
}
