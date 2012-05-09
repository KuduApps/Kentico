using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Edit : CMSNewsletterNewslettersPage
{
    #region "Variables"

    protected string mSave = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        chkShowInArchive.Text = GetString("NewsletterTemplate_Edit.ShowInArchive");

        mSave = GetString("general.save");

        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Newsletter", "AuthorIssues"))
        {
            // Disable Save button if user is not authorized
            lnkSave.Enabled = false;
            imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
        }
        else
        {
            imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
            lnkSave.Attributes.Add("onclick", "SaveDocument(); return false;");

            lblInfo.Text = GetString("general.changessaved");
            lblInfo.Style.Add("display", "none");
            lblError.Text = GetString("NewsletterContentEditor.SubjectRequired");
            lblError.Style.Add("display", "none");

            // Registr scripts
            ScriptHelper.RegisterShortcuts(this);

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

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "GetSubject",
                ScriptHelper.GetScript(
                "function SaveDocument() \n" +
                "{ \n" +
                "    ClearToolbar(); \n" +
                "    window.frames['iframeContent'].SaveContent(false, document.getElementById('" + txtSubject.ClientID + "').value, " +
                "                                                      document.getElementById('" + chkShowInArchive.ClientID + "').checked ); \n" +
                "    document.getElementById('" + lblInfo.ClientID + "').style.display = \"block\"; \n" +
                "    setTimeout('RefreshPage()',200); \n" +
                "} \n"));
        }

        // Get issue ID from query string
        int issueId = QueryHelper.GetInteger("issueid", 0);
        // Get edited issue object and check its existence
        Issue issue = IssueProvider.GetIssue(issueId);
        EditedObject = issue;

        // Check whether issue has been sent
        bool isSent = (issue.IssueMailoutTime == DateTimeHelper.ZERO_TIME ? false : (issue.IssueMailoutTime < DateTime.Now));
        if (isSent)
        {
            lblSent.Text = GetString("Newsletter_Issue_Header.AlreadySent");
        }
        else
        {
            lblSent.Text = GetString("Newsletter_Issue_Header.NotSentYet");
        }

        // Get newsletter and check its existence
        Newsletter news = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);
        EditedObject = news;

        if (news.NewsletterType == NewsletterType.Dynamic)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("Newsletter_Issue_Edit.CannotBeEdited");
            contentBody.Visible = false;
            contentFooter.Visible = false;
            pnlMenu.Visible = false;
            return;
        }
        else
        {
            contentBody.IssueID = issueId;
            txtSubject.Text = issue.IssueSubject;
            chkShowInArchive.Checked = issue.IssueShowInNewsletterArchive;
        }

        // Initialize attachment list
        AttachmentTitle.TitleText = GetString("general.attachments");
        AttachmentTitle.SetWindowTitle = false;
        AttachmentList.ObjectID = issueId;
        AttachmentList.SiteID = CMSContext.CurrentSiteID;
        AttachmentList.AllowPasteAttachments = true;
        AttachmentList.ObjectType = NewsletterObjectType.NEWSLETTERISSUE;
        AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_ISSUE;
        AttachmentList.AllowEdit = lnkSave.Enabled;

        // Add after upload event
        AttachmentList.OnAfterUpload += new EventHandler(AttachmentList_OnAfterUpload);
        AttachmentList.OnAfterDelete += new EventHandler(AttachmentList_OnAfterUpload);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblInfo.Visible = (lblInfo.Text != String.Empty);
        lblError.Visible = (lblError.Text != String.Empty);
    }


    void AttachmentList_OnAfterUpload(object sender, EventArgs e)
    {
        // Show info message after upload/delete
        lblInfo.Style.Remove("display");
        lblInfo.Style.Add("display", "block");      
    }

    #endregion
}