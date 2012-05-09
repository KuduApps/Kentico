using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Xml;

using CMS.CMSHelper;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Iframe_Edit : CMSNewsletterNewslettersPage
{
    #region "Variables"

    private int mIssueID = 0;


    private int mNewsletterID = 0;


    private int templateId = 0;


    private ArrayList regionList = new ArrayList();


    private Hashtable regionsContents = new Hashtable();


    Issue issue = null;

    // True indicates that new issue is created otherwise existing one is edited
    private bool mIsNewIssue = false;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterScriptFile(Page, "cmsedit.js");
        ScriptHelper.RegisterStartupScript(Page, typeof(string), "Initialize", ScriptHelper.GetScript("InitializePage();"));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Reload", ScriptHelper.GetScript("" +
           "function SaveContent(callNext, newsSubject, newsShowInArchive) { \n" +
           " document.getElementById('" + hdnNewsletterSubject.ClientID + "').value = newsSubject; \n" +
           " document.getElementById('" + hdnNewsletterShowInArchive.ClientID + "').value = newsShowInArchive; \n" +
           " if (callNext) { document.getElementById('" + this.hdnNext.ClientID + "').value = 1; } \n" +
           this.Page.ClientScript.GetPostBackEventReference(btnHidden, null) +
           "} \n" +
           "function SaveDocument() { window.parent.ClearToolbar(); window.parent.SaveDocument(); } \n"));

        if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "authorissues"))
        {
            ScriptHelper.RegisterShortcuts(this);
        }

        // Get issue ID
        mIssueID = QueryHelper.GetInteger("issueid", 0);
        if (mIssueID == 0)
        {
            mIssueID = QueryHelper.GetInteger(hdnIssueId.Value, 0);
        }

        // Get newsletter ID
        mNewsletterID = QueryHelper.GetInteger("newsletterid", 0);
        // Get info if new issue is created
        mIsNewIssue = QueryHelper.GetBoolean("new", false);

        if (mIssueID > 0)
        {
            issue = IssueProvider.GetIssue(mIssueID);
            if (issue != null)
            {
                templateId = issue.IssueTemplateID;
            }
        }
        else if (mNewsletterID > 0)
        {
            Newsletter newsletter = NewsletterProvider.GetNewsletter(mNewsletterID);
            if (newsletter != null)
            {
                templateId = newsletter.NewsletterTemplateID;
            }
        }

        if (templateId > 0)
        {
            // Load content from the template
            LoadContent();
        }

        if (!RequestHelper.IsPostBack())
        {
            if ((issue != null))
            {
                bool saved = QueryHelper.GetBoolean("saved", false);
                if (saved)
                {
                    ltlScript2.Text = ScriptHelper.GetScript("if (parent.MsgInfo) { parent.MsgInfo(0); } ");
                }
            }
        }

        LoadRegionList();
    }


    /// <summary>
    /// Loads content from specific newsletter template.
    /// </summary>
    private void LoadContent()
    {
        EmailTemplate emailTemplate = EmailTemplateProvider.GetEmailTemplate(templateId);
        if ((emailTemplate == null) || string.IsNullOrEmpty(emailTemplate.TemplateBody))
        {
            return;
        }

        // Remove security parameters from macros
        string templateText = MacroResolver.RemoveSecurityParameters(emailTemplate.TemplateBody, true, null);

        if (!RequestHelper.IsPostBack() && (issue != null))
        {
            // Load content of editable regions
            IssueHelper.LoadRegionsContents(ref regionsContents, issue.IssueText);
        }

        CMSEditableRegion editableRegion = null;
        LiteralControl before = null;
        int count = 0;
        int textStart = 0;
        int editRegStart = templateText.IndexOf("$$", textStart);

        // Apply CSS e-mail template style        
        HTMLHelper.AddToHeader(this.Page, CSSHelper.GetCSSFileLink(EmailTemplateProvider.GetStylesheetUrl(emailTemplate.TemplateName)));

        while (editRegStart >= 0)
        {
            count++;

            before = new LiteralControl();
            // Get template text surrounding editable regions - make links absolute
            before.Text = URLHelper.MakeLinksAbsolute(templateText.Substring(textStart, (editRegStart - textStart)));
            plcContent.Controls.Add(before);

            // End of region
            editRegStart += 2;
            textStart = editRegStart;
            if (editRegStart < templateText.Length - 1)
            {
                int editRegEnd = templateText.IndexOf("$$", editRegStart);
                if (editRegEnd >= 0)
                {
                    string region = templateText.Substring(editRegStart, editRegEnd - editRegStart);
                    string[] parts = (region + ":" + ":").Split(':');

                    try
                    {
                        string name = parts[0];
                        if (!string.IsNullOrEmpty(name.Trim()))
                        {
                            int width = ValidationHelper.GetInteger(parts[1], 0);
                            int height = ValidationHelper.GetInteger(parts[2], 0);

                            editableRegion = new CMSEditableRegion();
                            editableRegion.ID = name;
                            editableRegion.RegionType = CMSEditableRegionTypeEnum.HtmlEditor;
                            editableRegion.ViewMode = ViewModeEnum.Edit;

                            editableRegion.DialogHeight = height;
                            editableRegion.DialogWidth = width;

                            editableRegion.WordWrap = false;
                            editableRegion.HtmlAreaToolbarLocation = "Out:CKEditorToolbar";
                            editableRegion.RegionTitle = name;
                            editableRegion.UseStylesheet = false;
                            editableRegion.HTMLEditorCssStylesheet = EmailTemplateProvider.GetStylesheetUrl(emailTemplate.TemplateName);
                            editableRegion.HtmlAreaToolbar = "Newsletter";
                            editableRegion.HtmlEditor.MediaDialogConfig.UseFullURL = true;
                            editableRegion.HtmlEditor.LinkDialogConfig.UseFullURL = true;
                            editableRegion.HtmlEditor.QuickInsertConfig.UseFullURL = true;

                            editableRegion.LoadContent(ValidationHelper.GetString(regionsContents[name.ToLower()], ""));

                            plcContent.Controls.Add(editableRegion);

                            textStart = editRegEnd + 2;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            editRegStart = templateText.IndexOf("$$", textStart);
        }

        before = new LiteralControl();
        before.Text = URLHelper.MakeLinksAbsolute(templateText.Substring(textStart));

        plcContent.Controls.Add(before);
    }


    /// <summary>
    /// Saves content of editable region(s) of the issue.
    /// </summary>
    protected string SaveContent()
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Newsletter", "AuthorIssues"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Newsletter", "AuthorIssues");
        }

        CMSEditableRegion editReg;
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineOnAttributes = false;

        StringBuilder sb = new StringBuilder();
        using (XmlWriter writer = XmlWriter.Create(sb, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("content");

            for (int i = 0; i < regionList.Count; i++)
            {
                editReg = (CMSEditableRegion)regionList[i];
                writer.WriteStartElement("region");
                writer.WriteStartAttribute("id");
                writer.WriteValue(editReg.ID);
                writer.WriteEndAttribute();
                writer.WriteString(editReg.GetContent());
                writer.WriteEndElement(); //region
            }

            writer.WriteEndElement(); //content
            writer.WriteEndDocument();
        }

        return sb.ToString();
    }


    /// <summary>
    /// Prepares script with array of editable regions.
    /// </summary>
    protected void LoadRegionList()
    {
        regionList = CMSPageManager.CollectEditableControls(plcContent);

        // Create array of regions IDs in javascript. We will use it to find out the focused region
        string script = "var focusedRegionID = ''; \n var regions = new Array(" + regionList.Count.ToString() + "); \n ";
        for (int i = 0; i < regionList.Count; i++)
        {
            script += "regions[" + i.ToString() + "] = '" + ((CMSEditableRegion)regionList[i]).ClientID + "_HtmlEditor'; \n ";
        }
        ltlScript.Text = ScriptHelper.GetScript(script);
    }


    /// <summary>
    /// Save click handler.
    /// </summary>
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        string result = new Validator().NotEmpty(hdnNewsletterSubject.Value.Trim(), GetString("NewsletterContentEditor.SubjectRequired")).Result;
        if (result != String.Empty)
        {
            ltlScript2.Text = ScriptHelper.GetScript("if (parent.MsgInfo) { parent.MsgInfo(1); }");
            return;
        }

        if (issue == null)
        {
            // Initialize new issue
            issue = new Issue();
            issue.IssueUnsubscribed = 0;
            issue.IssueSentEmails = 0;
            issue.IssueTemplateID = templateId;
            issue.IssueNewsletterID = mNewsletterID;
            issue.IssueSiteID = CMSContext.CurrentSiteID;
        }

        // Saves content of editable region(s)
        issue.IssueText = SaveContent();

        // Remove '#' from macros if included
        hdnNewsletterSubject.Value = hdnNewsletterSubject.Value.Trim().Replace("#%}", "%}");

        // Sign macros if included in the subject
        issue.IssueSubject = MacroResolver.AddSecurityParameters(hdnNewsletterSubject.Value, CMSContext.CurrentUser.UserName, null);
        issue.IssueShowInNewsletterArchive = ValidationHelper.GetBoolean(hdnNewsletterShowInArchive.Value, false);

        // Save issue
        IssueProvider.SetIssue(issue);

        hdnIssueId.Value = issue.IssueID.ToString();

        if (mIsNewIssue)
        {
            // Hide content if redirecting
            plcContent.Visible = false;
        }

        if (hdnNext.Value == "1")
        {
            ltlScript2.Text = ScriptHelper.GetScript("parent.NextClick('" + issue.IssueID.ToString() + "');");
        }
        else
        {
            ltlScript2.Text = ScriptHelper.GetScript("if (parent.SaveClick) {parent.SaveClick('" + issue.IssueID.ToString() + "');} ");
        }
    }

    #endregion
}