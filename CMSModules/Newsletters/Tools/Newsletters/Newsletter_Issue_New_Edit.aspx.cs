using System;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

[Security(Resource = "CMS.Newsletter", UIElements = "Newsletters")]
public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_New_Edit : CMSToolsModalPage
{
    #region "Variables"

    protected int newsletterId = 0;
    protected Issue issue = null;

    #endregion

    
    #region "Properties"

    private int IssueId
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["IssueID"], 0);
        }
        set
        {
            ViewState["IssueID"] = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the license
        if (!string.IsNullOrEmpty(URLHelper.GetCurrentDomain()))
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Newsletters);
        }

        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Newsletter", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Newsletter");
        }

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check 'NewsletterRead' permission
        if (!user.IsAuthorizedPerResource("CMS.Newsletter", "Read"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Newsletter", "Read");
        }

        // Check 'Author issues' permission
        if (!user.IsAuthorizedPerResource("cms.newsletter", "authorissues"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "authorissues");
        }

        // Check permissions for CMS Desk -> Tools -> Newsletter
        if (!user.IsAuthorizedPerUIElement("CMS.Tools", "Newsletter"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Tools", "Newsletter");
        }

        chkShowInArchive.Text = GetString("NewsletterTemplate_Edit.ShowInArchive");

        string tmp = " document.getElementById('" + txtSubject.ClientID + "').value, " +
                     " document.getElementById('" + chkShowInArchive.ClientID + "').checked ";

        btnSave.Attributes.Add("onclick", "ClearToolbar();frames[0].SaveContent(false, " + tmp + ");return false;");
        btnNext.Attributes.Add("onclick", "ClearToolbar();frames[0].SaveContent(true, " + tmp + ");return false;");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "NextFunction", ScriptHelper.GetScript("" +
        "function NextClick(issueId){" +
        "document.getElementById('" + hdnNext.ClientID + "').value = issueId; " +
        this.Page.ClientScript.GetPostBackEventReference(btnNextHidden, null) +
        " }"));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SaveFunction", ScriptHelper.GetScript("" +
        "function SaveClick(issueId){" +
        "document.getElementById('" + hdnNext.ClientID + "').value = issueId; " +
        this.Page.ClientScript.GetPostBackEventReference(btnSaveHidden, null) +
        " }"));

        lblError.Text = GetString("NewsletterContentEditor.SubjectRequired");
        lblError.Style.Add("display", "none");
        lblInfo.Text = GetString("general.changessaved");
        lblInfo.Style.Add("display", "none");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MsgInfo", ScriptHelper.GetScript(
            "function MsgInfo(err) { \n" +
            "    if (err == 0) { \n" +
            "         document.getElementById('" + lblError.ClientID + "').style.display = \"none\"; \n" +
            "         document.getElementById('" + lblInfo.ClientID + "').style.display = \"block\"; \n" +
            "    } \n" +
            "    if (err == 1) { \n" +
            "         document.getElementById('" + lblInfo.ClientID + "').style.display = \"none\"; \n" +
            "         document.getElementById('" + lblError.ClientID + "').style.display = \"block\"; \n" +
            "    } \n" +
            "} \n"));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SaveDocument",
            ScriptHelper.GetScript(
"            function SaveDocument() \n" +
"            { \n" +
"                ClearToolbar(); \n" +
"                window.frames['iframeContent'].SaveContent(false, document.getElementById('" + txtSubject.ClientID + "').value, " +
                                                                 " document.getElementById('" + chkShowInArchive.ClientID + "').checked ); \n" +
"            } \n"));


        newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        IssueId = QueryHelper.GetInteger("issueid", IssueId);

        if ((newsletterId == 0) && (IssueId == 0))
        {
            return;
        }

        if (IssueId > 0) //user comes back from Newsletter_Issue_New_Preview.aspx page
        {
            contentBody.IssueID = IssueId;

            issue = IssueProvider.GetIssue(IssueId);
            EditedObject = issue;
            if (newsletterId == 0)
            {
                newsletterId = issue.IssueNewsletterID;
            }

            contentBody.NewsletterID = newsletterId;

            txtSubject.Text = issue.IssueSubject;
            chkShowInArchive.Checked = issue.IssueShowInNewsletterArchive;
        }
        else //user is creating new issue
        {
            contentBody.NewsletterID = newsletterId;
        }

        // Initializes page title control		
        Page.Title = GetString("newsletter_issue_list.title");
        CurrentMaster.Title.TitleText = Page.Title;
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Newsletter_Issue/new.png");

        ucHeader.Title = GetString("newsletter_issue_list.step1");
        ucHeader.Header = GetString("newsletter_issue_list.header");
        ucHeader.DescriptionVisible = false;
        btnNext.Text = GetString("general.next") + " >";
        btnSave.Text = GetString("general.save");
        btnClose.Text = GetString("general.close");
        AttachmentTitle.TitleText = GetString("general.attachments");

        if (IssueId > 0)
        {
            AttachmentList.Visible = true;
            AttachmentList.ObjectID = IssueId;
            AttachmentList.SiteID = CMSContext.CurrentSiteID;
            AttachmentList.AllowPasteAttachments = true;
            AttachmentList.ObjectType = NewsletterObjectType.NEWSLETTERISSUE;
            AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_ISSUE;
            AttachmentList.OnAfterUpload += new EventHandler(AttachmentList_OnAfterUpload);
            AttachmentList.UploadOnClickScript = "SaveDocument();";
            lblAttInfo.Visible = false;
        }
        else
        {
            lblAttInfo.Text = GetString("newsletter_issue_list.attachmentinfo");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        lblInfo.Visible = (!string.IsNullOrEmpty(lblInfo.Text));
        lblError.Visible = (!string.IsNullOrEmpty(lblError.Text));
        pnlInfo.Visible = (!string.IsNullOrEmpty(this.lblInfo.Text)) || (!string.IsNullOrEmpty(this.lblError.Text));

        base.OnPreRender(e);
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect("Newsletter_Issue_New_Preview.aspx?issueid=" + hdnNext.Value + "&newsletterid=" + newsletterId.ToString());
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect("Newsletter_Issue_New_Edit.aspx?issueid=" + hdnNext.Value + "&newsletterid=" + newsletterId.ToString() + "&saved=1");
    }


    protected void AttachmentList_OnAfterUpload(object sender, EventArgs e)
    {
        // Show info message after upload/delete
        lblInfo.Style.Remove("display");
        lblInfo.Style.Add("display", "block");
    }

    #endregion
}
