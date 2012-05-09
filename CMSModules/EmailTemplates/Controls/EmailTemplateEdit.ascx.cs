using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.EmailEngine;
using CMS.ExtendedControls;

public partial class CMSModules_EmailTemplates_Controls_EmailTemplateEdit : CMSAdminEditControl
{

    #region "Private Variables"

    private int mSiteId = 0;
    private int mEmailTemplateId = 0;
    private bool mGlobalTemplate = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether the e-mail template is global (independent on site).
    /// </summary>
    public bool GlobalTemplate
    {
        get
        {
            return mGlobalTemplate;
        }
        set
        {
            mGlobalTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the site ID for which the e-mail should be displayed.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// Gets the e-mail template ID of currently processed e-mail template.
    /// </summary>
    public int EmailTemplateID
    {
        get
        {
            return mEmailTemplateId;
        }
        set
        {
            mEmailTemplateId = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize HTML editor
        this.txtText.Editor.EnableViewState = false;
        this.txtText.Editor.EditorMode = EditorModeEnum.Advanced;
        this.txtText.Editor.Language = LanguageEnum.HTMLMixed;
        this.txtText.Editor.Height = new Unit("400px");
        this.txtText.Editor.Width = new Unit("100%");

        // Initialize Plain Text editor
        this.txtPlainText.Editor.EnableViewState = false;
        this.txtPlainText.Editor.EditorMode = EditorModeEnum.Advanced;
        this.txtPlainText.Editor.Language = LanguageEnum.Text;
        this.txtPlainText.Editor.Height = new Unit("400px");
        this.txtPlainText.Editor.Width = new Unit("100%");

        // Fill the e-mail type enumeration
        if (!RequestHelper.IsPostBack())
        {
            DataHelper.FillWithEnum<EmailTemplateMacros.EmailType>(drpEmailType, "emailtemplate.type.", EmailTemplateMacros.GetEmailTypeString, true);
        }

        // Initialize required field validators
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("EmailTemplate_Edit.FillDisplayNameField");
        RequiredFieldValidatorCodeName.ErrorMessage = GetString("EmailTemplate_Edit.FillCodeNameField");

        // Initialize other controls
        AttachmentTitle.TitleText = GetString("general.attachments");

        // Register save button handler
        CMSPage.CurrentPage.CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;

        if (EmailTemplateID > 0)
        {
            // Get email template info of specified 'templateid'
            EmailTemplateInfo templateInfo = EmailTemplateProvider.GetEmailTemplate(EmailTemplateID);
            EditedObject = templateInfo;
            // Get SiteID of the template
            SiteID = templateInfo.TemplateSiteID;

            // Check if user is assigned to web site
            if (!CMSContext.CurrentUser.IsGlobalAdministrator)
            {
                if (!CMSContext.CurrentUser.IsInSite(SiteInfoProvider.GetSiteName(SiteID)))
                {
                    // Disable object for user
                    EditedObject = null;
                }
            }

            // Fill the form
            if (!RequestHelper.IsPostBack())
            {
                // Load data to the form.
                LoadData(templateInfo);
                // Show message that the email template was created or updated successfully.
                if (QueryHelper.GetBoolean("saved", false))
                {
                    lblInfo.Visible = true;

                    // Reload header if changes were saved

                    if ((Page is CMSPage) && ((CMSPage)Page).TabMode)
                    {
                        ScriptHelper.RefreshTabHeader(Page, null);
                    }
                }

                // Show attachment list
                AttachmentList.Visible = true;
                AttachmentTitle.Visible = true;
            }
        }

        // Get correct MacroResolver
        MacroResolver resolver = EmailTemplateMacros.GetEmailTemplateResolver(EmailTemplateMacros.GetEmailTypeEnum(drpEmailType.SelectedValue));

        txtText.Resolver = resolver;
        txtPlainText.Resolver = resolver;

        macroSelectorElm.Resolver = resolver;
        macroSelectorElm.ShowMacroTreeAbove = true;
        macroSelectorElm.ExtendedTextAreaElem = this.txtText.Editor.EditorID;
        macroSelectorElm.TextAreaID = this.txtText.Editor.ClientID;

        macroSelectorPlain.Resolver = resolver;
        macroSelectorPlain.ShowMacroTreeAbove = true;
        macroSelectorPlain.ExtendedTextAreaElem = this.txtPlainText.Editor.EditorID;
        macroSelectorPlain.TextAreaID = this.txtPlainText.Editor.ClientID;
    }


    /// <summary>
    /// Load data of edited email template from DB into textboxes.
    /// </summary>
    /// <param name="templateInfo">EmailTemplateInfo object</param>
    protected void LoadData(EmailTemplateInfo templateInfo)
    {
        txtDisplayName.Text = templateInfo.TemplateDisplayName;
        txtCodeName.Text = templateInfo.TemplateName;
        txtSubject.Text = templateInfo.TemplateSubject;
        txtBcc.Text = templateInfo.TemplateBcc;
        txtCc.Text = templateInfo.TemplateCc;
        txtFrom.Text = templateInfo.TemplateFrom;
        txtText.Text = templateInfo.TemplateText;
        txtPlainText.Text = templateInfo.TemplatePlainText;
        drpEmailType.SelectedValue = templateInfo.TemplateType;

        // Init attachment storage
        AttachmentList.ObjectID = templateInfo.TemplateID;
        AttachmentList.ObjectType = EmailObjectType.EMAILTEMPLATE;
        AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE;

        if (this.SiteID != 0)
        {
            AttachmentList.SiteID = this.SiteID;
        }
    }


    /// <summary>
    /// Save button action.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "lnksave_click":
                txtDisplayName.Text = txtDisplayName.Text.Trim();
                txtCodeName.Text = txtCodeName.Text.Trim();
                txtSubject.Text = txtSubject.Text.Trim();

                // Find whether required fields are not empty
                string result = new Validator().NotEmpty(txtDisplayName.Text, GetString("EmailTemplate_Edit.FillDisplayNameField"))
                    .NotEmpty(txtCodeName.Text, GetString("EmailTemplate_Edit.FillCodeNameField"))
                    .IsCodeName(txtCodeName.Text, GetString("general.invalidcodename"))
                    .Result;

                // Check validity of entered e-mails
                if (!String.IsNullOrEmpty(txtFrom.Text) && !ValidationHelper.AreEmails(txtFrom.Text))
                {
                    result = GetString("EmailTemplate_Edit.InvalidFrom");
                }
                else if (!String.IsNullOrEmpty(txtBcc.Text) && !ValidationHelper.AreEmails(txtBcc.Text))
                {
                    result = GetString("EmailTemplate_Edit.InvalidBcc");
                }
                else if (!String.IsNullOrEmpty(txtCc.Text) && !ValidationHelper.AreEmails(txtCc.Text))
                {
                    result = GetString("EmailTemplate_Edit.InvalidCc");
                }

                if (String.IsNullOrEmpty(result))
                {
                    string siteName = null;
                    if (this.SiteID != 0)
                    {
                        // Get site name for non-global templates
                        SiteInfo site = SiteInfoProvider.GetSiteInfo(this.SiteID);
                        if (site != null)
                        {
                            siteName = site.SiteName;
                        }
                    }
                    // Try to get template by template name and site name
                    EmailTemplateInfo templateInfo = EmailTemplateProvider.GetEmailTemplate(txtCodeName.Text, siteName);

                    // Find if codename of the email template is unique for the site
                    if ((templateInfo == null) || (templateInfo.TemplateID == this.EmailTemplateID) || ((templateInfo.TemplateSiteID == 0) && (this.SiteID > 0)))
                    {
                        // Get object
                        if (templateInfo == null)
                        {
                            templateInfo = EmailTemplateProvider.GetEmailTemplate(this.EmailTemplateID);
                            if (templateInfo == null)
                            {
                                templateInfo = new EmailTemplateInfo();
                            }
                        }

                        templateInfo.TemplateID = EmailTemplateID;
                        templateInfo.TemplateDisplayName = txtDisplayName.Text;
                        templateInfo.TemplateName = txtCodeName.Text;
                        templateInfo.TemplateSubject = txtSubject.Text;
                        templateInfo.TemplateFrom = txtFrom.Text;
                        templateInfo.TemplateBcc = txtBcc.Text;
                        templateInfo.TemplateCc = txtCc.Text;
                        templateInfo.TemplateText = txtText.Text;
                        templateInfo.TemplatePlainText = txtPlainText.Text;
                        templateInfo.TemplateSiteID = SiteID;
                        templateInfo.TemplateType = drpEmailType.SelectedValue;

                        // Save (insert/update) EmailTemplateInfo object
                        EmailTemplateProvider.SetEmailTemplate(templateInfo);
                        // Handle redirection
                        string redirectToUrl = "~/CMSModules/EmailTemplates/Pages/Edit.aspx?templateid=" + templateInfo.TemplateID.ToString() + "&saved=1&tabmode=" + QueryHelper.GetInteger("tabmode", 0);
                        if (this.GlobalTemplate)
                        {
                            redirectToUrl += "&selectedsiteid=0";
                        }
                        else if (QueryHelper.GetInteger("selectedsiteid", 0) != 0)
                        {
                            redirectToUrl += "&selectedsiteid=" + QueryHelper.GetInteger("selectedsiteid", 0);
                        }
                        else if (this.SiteID > 0)
                        {
                            redirectToUrl += "&siteid=" + this.SiteID;
                        }
                        URLHelper.Redirect(redirectToUrl);
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "EmailTemplate_Edit.UniqueCodeName";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = result;
                }
                break;
        }
    }

    #endregion

}
