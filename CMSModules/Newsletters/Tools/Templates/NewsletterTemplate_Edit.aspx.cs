using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;

// Set edited object
[EditedObject("newsletter.emailtemplate", "templateid", "NewsletterTemplate_Frameset.aspx")]

public partial class CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_Edit : CMSNewsletterTemplatesPage
{
    #region "Variables"

    protected int templateid = 0;
    protected int siteId = 0;
    protected string mSave = null;
    protected string mSpellCheck = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlHeader.Visible = !TabMode;

        // Get site ID from context
        siteId = CMSContext.CurrentSiteID;

        rfvTemplateDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvTemplateName.ErrorMessage = GetString("NewsletterTemplate_Edit.ErrorEmptyName");

        ScriptHelper.RegisterSaveShortcut(lnkSave, null, false);
        ScriptHelper.RegisterSpellChecker(this);

        // Control initializations
        string varsScript = string.Format("var emptyNameMsg = '{0}'; \nvar emptyWidthMsg = '{1}'; \nvar emptyHeightMsg = '{2}'; \nvar spellURL = '{3}'; \n",
            GetString("NewsletterTemplate_Edit.EmptyNameMsg"),
            GetString("NewsletterTemplate_Edit.EmptyWidthMsg"),
            GetString("NewsletterTemplate_Edit.EmptyHeightMsg"),
            CMSContext.ResolveDialogUrl("~/CMSModules/Content/CMSDesk/Edit/SpellCheck.aspx"));
        ltlScript.Text = ScriptHelper.GetScript(varsScript);

        // Set fields to be checked by Spell Checker
        string spellCheckScript = string.Format("if (typeof(spellCheckFields)==='undefined') {{var spellCheckFields = new Array();}} spellCheckFields.push('{0}');",
            htmlTemplateBody.ClientID);
        ScriptHelper.RegisterStartupScript(this, typeof(string), this.ClientID, ScriptHelper.GetScript(spellCheckScript));

        // Set button properties
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        imgSpellCheck.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/spellcheck.png");
        mSave = GetString("general.save");
        mSpellCheck = GetString("spellcheck.title");
        lnkSpellCheck.ToolTip = GetString("EditMenu.SpellCheck");
        lnkSpellCheck.OnClientClick = "checkSpelling(spellURL); return false;";

        AttachmentTitle.TitleText = GetString("general.attachments");

        string currentEmailTemplate = GetString("NewsletterTemplate_Edit.NewItemCaption");

        EmailTemplate emailTemplateObj = null;
        // Get emailTemplate ID from querystring
        templateid = QueryHelper.GetInteger("templateid", 0);
        if (templateid > 0)
        {
            emailTemplateObj = EmailTemplateProvider.GetEmailTemplate(templateid);

            currentEmailTemplate = emailTemplateObj.TemplateDisplayName;

            macroSelectorElm.Resolver = EmailTemplateMacros.NewsletterResolver;
            macroSelectorElm.JavaScripFunction = "InsertHTML";

            AttachmentList.ObjectID = emailTemplateObj.TemplateID;
            AttachmentList.SiteID = siteId;
            AttachmentList.ObjectType = NewsletterObjectType.NEWSLETTERTEMPLATE;
            AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE;
            AttachmentList.AllowPasteAttachments = true;

            // Display editable region section only for e-mail templates of type "Issue template"
            if (emailTemplateObj.TemplateType == EmailTemplateType.Issue)
            {
                pnlEditableRegion.Visible = true;
                btnInsertEditableRegion.Visible = true;
                lblInsertEditableRegion.Visible = true;
            }
            else
            {
                plcSubject.Visible = true;
            }

            // Init CSS styles every time during page load
            htmlTemplateBody.EditorAreaCSS = EmailTemplateProvider.GetStylesheetUrl(emailTemplateObj.TemplateName) + "&timestamp=" + DateTime.Now.Millisecond;
        }

        // Initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("NewsletterTemplate_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Newsletters/Tools/Templates/NewsletterTemplate_List.aspx";
        pageTitleTabs[0, 2] = "newslettersContent";
        pageTitleTabs[1, 0] = currentEmailTemplate;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;
        PageTitle.Breadcrumbs = pageTitleTabs;

        if (!RequestHelper.IsPostBack())
        {
            // Initialize dialog
            LoadData(emailTemplateObj);
            FillFieldsList(emailTemplateObj);

            // Initialize HTML editor
            InitHTMLEditor();

            // Show that the emailTemplate was created or updated successfully
            if (QueryHelper.GetBoolean("saved", false))
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");

                // Reload header if changes were saved
                if (TabMode)
                {
                    ScriptHelper.RefreshTabHeader(Page, null);
                }
            }
        }
    }


    /// <summary>
    /// Load data of editing emailTemplate.
    /// </summary>
    /// <param name="emailTemplateObj">EmailTemplate object</param>
    protected void LoadData(EmailTemplate emailTemplateObj)
    {
        if (emailTemplateObj != null)
        {
            htmlTemplateBody.ResolvedValue = emailTemplateObj.TemplateBody;
            txtTemplateName.Text = emailTemplateObj.TemplateName;
            txtTemplateHeader.Value = emailTemplateObj.TemplateHeader;
            txtTemplateFooter.Value = emailTemplateObj.TemplateFooter;
            txtTemplateDisplayName.Text = emailTemplateObj.TemplateDisplayName;
            txtTemplateStyleSheetText.Text = emailTemplateObj.TemplateStylesheetText;

            // Display temaplate subject only for 'subscription' and 'unsubscription' template types
            if (emailTemplateObj.TemplateType != EmailTemplateType.Issue)
            {
                txtTemplateSubject.Text = emailTemplateObj.TemplateSubject;
            }
        }
    }


    /// <summary>
    /// Fills list of available fields.
    /// </summary>
    /// <param name="emailTemplateObj">EmailTemplate object</param>
    protected void FillFieldsList(EmailTemplate emailTemplateObj)
    {
        ListItem newItem = null;

        // Insert double opt-in activation link
        if ((emailTemplateObj != null) && (emailTemplateObj.TemplateType == EmailTemplateType.DoubleOptIn))
        {
            newItem = new ListItem(GetString("NewsletterTemplate.MacroActivationLink"), IssueHelper.MacroActivationLink);
            lstInsertField.Items.Add(newItem);
        }
        newItem = new ListItem(GetString("general.email"), IssueHelper.MacroEmail);
        lstInsertField.Items.Add(newItem);
        newItem = new ListItem(GetString("NewsletterTemplate.MacroFirstName"), IssueHelper.MacroFirstName);
        lstInsertField.Items.Add(newItem);
        newItem = new ListItem(GetString("NewsletterTemplate.MacroLastName"), IssueHelper.MacroLastName);
        lstInsertField.Items.Add(newItem);
        newItem = new ListItem(GetString("NewsletterTemplate.MacroUnsubscribeLink"), IssueHelper.MacroUnsubscribeLink);
        lstInsertField.Items.Add(newItem);
    }


    /// <summary>
    /// Initializes HTML editor's settings.
    /// </summary>
    protected void InitHTMLEditor()
    {
        htmlTemplateBody.AutoDetectLanguage = false;
        htmlTemplateBody.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlTemplateBody.ToolbarSet = "Newsletter";
        htmlTemplateBody.MediaDialogConfig.UseFullURL = true;
        htmlTemplateBody.LinkDialogConfig.UseFullURL = true;
        htmlTemplateBody.QuickInsertConfig.UseFullURL = true;
    }


    /// <summary>
    /// Saves data to database.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        // Check 'Manage templates' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managetemplates"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managetemplates");
        }

        string errorMessage = null;
        // Check template code name
        if (!ValidationHelper.IsCodeName(txtTemplateName.Text))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }
        else
        {
            // Check code and display name for emptiness
            errorMessage = new Validator().NotEmpty(txtTemplateDisplayName.Text, GetString("general.requiresdisplayname")).NotEmpty(txtTemplateName.Text, GetString("NewsletterTemplate_Edit.ErrorEmptyName")).Result;
        }

        if (string.IsNullOrEmpty(errorMessage))
        {
            // TemplateName must to be unique
            EmailTemplate emailTemplateObj = EmailTemplateProvider.GetEmailTemplate(txtTemplateName.Text.Trim(), siteId);

            // If templateName value is unique														
            if ((emailTemplateObj == null) || (emailTemplateObj.TemplateID == templateid))
            {
                // If templateName value is unique -> determine whether it is update or insert 
                if ((emailTemplateObj == null))
                {
                    // Get EmailTemplate object by primary key
                    emailTemplateObj = EmailTemplateProvider.GetEmailTemplate(templateid);
                    if (emailTemplateObj == null)
                    {
                        // Create new item -> insert
                        emailTemplateObj = new EmailTemplate();
                    }
                }

                // Check if template doesn't contains more editable regions with same name
                Hashtable eRegions = new Hashtable();
                bool isValid = true;
                bool isValidRegionName = true;

                int textStart = 0;
                int editRegStart = htmlTemplateBody.ResolvedValue.Trim().IndexOf("$$", textStart);
                int editRegEnd = 0;
                string region = null;
                string[] parts = null;
                string name = null;

                while (editRegStart >= 0)
                {
                    // End of region
                    editRegStart += 2;
                    textStart = editRegStart;
                    if (editRegStart < htmlTemplateBody.ResolvedValue.Trim().Length - 1)
                    {
                        editRegEnd = htmlTemplateBody.ResolvedValue.Trim().IndexOf("$$", editRegStart);
                        if (editRegEnd >= 0)
                        {
                            region = htmlTemplateBody.ResolvedValue.Trim().Substring(editRegStart, editRegEnd - editRegStart);
                            parts = (region + ":" + ":").Split(':');

                            textStart = editRegEnd + 2;
                            try
                            {
                                name = parts[0];
                                if ((!string.IsNullOrEmpty(name)) && (name.Trim() != ""))
                                {
                                    if (eRegions[name.ToLower()] != null)
                                    {
                                        isValid = false;
                                        break;
                                    }

                                    if (!ValidationHelper.IsCodeName(name))
                                    {
                                        isValidRegionName = false;
                                        break;
                                    }
                                    eRegions[name.ToLower()] = 1;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    editRegStart = htmlTemplateBody.ResolvedValue.Trim().IndexOf("$$", textStart);
                }

                if (isValid)
                {
                    if (isValidRegionName)
                    {
                        // Set template object
                        emailTemplateObj.TemplateBody = htmlTemplateBody.ResolvedValue.Trim();
                        emailTemplateObj.TemplateName = txtTemplateName.Text.Trim();
                        emailTemplateObj.TemplateHeader = ValidationHelper.GetString(txtTemplateHeader.Value, "").Trim();
                        emailTemplateObj.TemplateFooter = ValidationHelper.GetString(txtTemplateFooter.Value, "").Trim();
                        emailTemplateObj.TemplateDisplayName = txtTemplateDisplayName.Text.Trim();
                        emailTemplateObj.TemplateStylesheetText = txtTemplateStyleSheetText.Text.Trim();

                        // Set temaplte subject only for 'subscription' and 'unsubscription' template types
                        if (plcSubject.Visible)
                        {
                            emailTemplateObj.TemplateSubject = txtTemplateSubject.Text.Trim();
                        }

                        EmailTemplateProvider.SetEmailTemplate(emailTemplateObj);

                        URLHelper.Redirect("NewsletterTemplate_Edit.aspx?templateid=" + Convert.ToString(emailTemplateObj.TemplateID) + "&saved=1&tabmode=" + QueryHelper.GetInteger("tabmode", 0));
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("NewsletterTemplate_Edit.EditableRegionNameError");
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("NewsletterTemplate_Edit.EditableRegionError");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("NewsletterTemplate_Edit.TemplateNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion
}