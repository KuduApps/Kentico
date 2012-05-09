using System;
using System.Web;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Autoresponder : CMSBizFormPage
{
    #region "Variables"

    private int formId = 0;


    private DataClassInfo formClassObj = null;


    private CurrentUserInfo currentUser = null;


    protected string mSave = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whether custom form layout is set or not.
    /// </summary>
    private bool IsLayoutSet
    {
        get
        {
            object obj = ViewState["IsLayoutSet"];
            return (obj == null) ? false : (bool)obj;
        }
        set
        {
            ViewState["IsLayoutSet"] = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' and 'EditData' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        currentUser = CMSContext.CurrentUser;

        // get form id from url
        formId = QueryHelper.GetInteger("formid", 0);
        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (bfi != null)
        {
            imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
            mSave = GetString("general.save");

            AttachmentTitle.TitleText = GetString("general.attachments");

            // Control initialization
            ltlConfirmDelete.Text = "<input type=\"hidden\" id=\"confirmdelete\" value=\"" + GetString("Bizform_Edit_Autoresponder.ConfirmDelete") + "\">";

            drpEmailField.SelectedIndexChanged += new EventHandler(drpEmailField_SelectedIndexChanged);

            // Init attachment storage
            AttachmentList.SiteID = CMSContext.CurrentSiteID;
            AttachmentList.AllowPasteAttachments = true;
            AttachmentList.ObjectID = bfi.FormID;
            AttachmentList.ObjectType = FormObjectType.BIZFORM;
            AttachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_FORM_LAYOUT;

            // Initialize HTML editor
            InitHTMLEditor();

            if (!RequestHelper.IsPostBack())
            {
                // Get bizform class object
                formClassObj = DataClassInfoProvider.GetDataClass(bfi.FormClassID);
                if (formClassObj != null)
                {
                    // Enable or disable form
                    EnableDisableForm(bfi.FormConfirmationTemplate);

                    // Fill list of available fields                    
                    FillFieldsList();

                    // Load dropdownlist with form text fields   
                    FormInfo fi = FormHelper.GetFormInfo(formClassObj.ClassName, false);
                    drpEmailField.DataSource = fi.GetFields(FormFieldDataTypeEnum.Text);
                    drpEmailField.DataBind();
                    drpEmailField.Items.Insert(0, new ListItem(GetString("bizform_edit_autoresponder.emptyemailfield"), ""));

                    // Try to select specified field
                    ListItem li = drpEmailField.Items.FindByValue(bfi.FormConfirmationEmailField);
                    if (li != null)
                    {
                        li.Selected = true;
                    }

                    // Load email subject and emailfrom address
                    txtEmailFrom.Text = bfi.FormConfirmationSendFromEmail;
                    txtEmailSubject.Text = bfi.FormConfirmationEmailSubject;
                }
                else
                {
                    // Disable form by default
                    EnableDisableForm(null);
                }
            }
        }
    }


    protected void Page_PreRender(Object sender, EventArgs e)
    {
        btnInsertInput.OnClientClick = "InsertAtCursorPosition('$$value:' + document.getElementById('" + lstAvailableFields.ClientID + "').value + '$$'); return false;";
        btnInsertLabel.OnClientClick = "InsertAtCursorPosition('$$label:' + document.getElementById('" + lstAvailableFields.ClientID + "').value + '$$'); return false;";

        SetCustomLayoutVisibility(!string.IsNullOrEmpty(drpEmailField.SelectedValue));

        if (!IsClientScriptRegistered())
        {
            if (!pnlCustomLayout.Visible && IsLayoutSet)
            {
                RegisterSaveDocumentWithDeleteConfirmation();
            }
            else
            {
                RegisterSaveDocument();
            }
        }
    }


    /// <summary>
    /// Register client script block for document saving via 'Ctrl+S'.
    /// </summary>
    protected void RegisterSaveDocument()
    {
        ScriptHelper.RegisterSaveShortcut(lnkSave, null, false);
    }


    /// <summary>
    /// Register client script block for document saving via 'Ctrl+S' with layout delete confirmation.
    /// </summary>
    protected void RegisterSaveDocumentWithDeleteConfirmation()
    {
        string saveScript = string.Format(@"function SaveDocument() {{ if (ConfirmDelete()) {{ {0} }} }}",
            this.Page.ClientScript.GetPostBackClientHyperlink(lnkSave, null));

        ScriptHelper.RegisterSaveShortcut(this, saveScript);
    }


    /// <summary>
    /// Returns true if Save Document client script block is registered.
    /// </summary>
    protected bool IsClientScriptRegistered()
    {
        return ScriptHelper.IsClientScriptBlockRegistered(ScriptHelper.SAVE_DOCUMENT_SCRIPT_KEY);
    }


    void drpEmailField_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCustomLayoutVisibility(!string.IsNullOrEmpty(drpEmailField.SelectedValue));
    }


    /// <summary>
    /// Sets visibility of custom layout form.
    /// </summary>
    private void SetCustomLayoutVisibility(bool visible)
    {
        pnlCustomLayout.Visible = visible;
        AttachmentTitle.Visible = visible;
        AttachmentList.Visible = visible;

        // Remove content checking and add delete confirmation
        if (!visible && IsLayoutSet)
        {
            lnkSave.OnClientClick = "return ConfirmDelete();";
        }
        // Remove delete confirmation and add content checking
        else if (visible)
        {
            lnkSave.OnClientClick = string.Empty;

            // Reload HTML editor content
            BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
            if (bfi != null && bfi.FormConfirmationTemplate != null)
            {
                htmlEditor.ResolvedValue = bfi.FormConfirmationTemplate;
            }
        }
    }


    /// <summary>
    /// Fills list of available fields.
    /// </summary>
    private void FillFieldsList()
    {
        FormInfo fi = null;
        FormFieldInfo[] fields = null;

        if (formClassObj != null)
        {
            // load form definition and get visible fields
            fi = FormHelper.GetFormInfo(formClassObj.ClassName, false);
            fields = fi.GetFields(true, true);

            lstAvailableFields.Items.Clear();

            if (fields != null)
            {
                // add visible fields to the list
                foreach (FormFieldInfo ffi in fields)
                {
                    lstAvailableFields.Items.Add(new ListItem(ffi.Name, ffi.Name));
                }
            }
            lstAvailableFields.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// Enables or disables form according to the confirmation email template text is defined or not.
    /// </summary>
    /// <param name="formLayout">Autoresponder layout</param>
    protected void EnableDisableForm(string formLayout)
    {
        if (!string.IsNullOrEmpty(formLayout))
        {
            //Enable layout editing                                
            pnlCustomLayout.Visible = true;
            AttachmentList.Visible = true;
            AttachmentTitle.Visible = true;

            // set confirmation email template text to the editable window of the HTML editor
            htmlEditor.ResolvedValue = formLayout;

            // Save info to viewstate 
            IsLayoutSet = true;
        }
        else
        {
            // Layout editing is not enabled by default        
            pnlCustomLayout.Visible = false;
            AttachmentList.Visible = false;
            AttachmentTitle.Visible = false;

            htmlEditor.ResolvedValue = string.Empty;

            // Save info to viewstate
            IsLayoutSet = false;

            lnkSave.OnClientClick = string.Empty;
        }
    }


    /// <summary>
    /// Displays specified info message and hide error message.
    /// </summary>
    /// <param name="infoMessage">Info message to display</param>
    protected void DisplayInfoMessage(string infoMessage)
    {
        lblError.Visible = false;
        lblInfo.Visible = true;
        lblInfo.Text = infoMessage;
    }


    /// <summary>
    /// Displays specified error message and hide info message.
    /// </summary>
    /// <param name="errorMessage">Error message to display</param>
    protected void DisplayErrorMessage(string errorMessage)
    {
        lblInfo.Visible = false;
        lblError.Visible = true;
        lblError.Text = errorMessage;
    }


    /// <summary>
    /// Initializes HTML editor's settings.
    /// </summary>
    protected void InitHTMLEditor()
    {
        htmlEditor.AutoDetectLanguage = false;
        htmlEditor.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlEditor.MediaDialogConfig.UseFullURL = true;
        htmlEditor.LinkDialogConfig.UseFullURL = true;
        htmlEditor.QuickInsertConfig.UseFullURL = true;
    }


    /// <summary>
    /// Gets uploaded file binary.
    /// </summary>
    /// <param name="postedFile">Posted file</param>
    protected byte[] GetAttachmentBinary(HttpPostedFile postedFile)
    {
        byte[] fileBinary = null;

        if (postedFile != null)
        {
            // Get file size and it's binary.
            int size = postedFile.ContentLength;
            fileBinary = new byte[size];
            postedFile.InputStream.Read(fileBinary, 0, size);
        }

        return fileBinary;
    }


    /// <summary>
    /// Gets name of uploaded file.
    /// </summary>
    /// <param name="postedFile">Posted file</param>
    protected string GetAttachmentName(HttpPostedFile postedFile)
    {
        string fileName = string.Empty;

        if (postedFile != null)
        {
            fileName = postedFile.FileName;
        }

        return fileName;
    }


    /// <summary>
    /// Gets MIME type of uploaded file.
    /// </summary>
    /// <param name="postedFile">Posted file</param>
    protected string GetAttachmentMIMEType(HttpPostedFile postedFile)
    {
        string mimeType = string.Empty;

        if (postedFile != null)
        {
            mimeType = postedFile.ContentType;
        }

        return mimeType;
    }


    /// <summary>
    /// Save button is clicked.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        string errorMessage = null;

        // check 'ReadForm' permission
        if (!currentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }

        // Validate form
        errorMessage = new Validator().NotEmpty(txtEmailFrom.Text.Trim(), GetString("bizform_edit_autoresponder.emptyemail")).NotEmpty(txtEmailSubject.Text.Trim(), GetString("bizform_edit_autoresponder.emptysubject")).Result;

        // Check if from e-mail contains macro expression or e-mails separated by semicolon
        if (string.IsNullOrEmpty(errorMessage) && !MacroResolver.ContainsMacro(txtEmailFrom.Text.Trim()) && !ValidationHelper.IsEmail(txtEmailFrom.Text.Trim()))
        {
            errorMessage = GetString("bizform_edit_autoresponder.emptyemail");
        }

        if ((string.IsNullOrEmpty(errorMessage)) || (!pnlCustomLayout.Visible))
        {
            errorMessage = String.Empty;
            BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
            if (bfi != null)
            {
                // Save custom layout
                if (!string.IsNullOrEmpty(drpEmailField.SelectedValue))
                {
                    bfi.FormConfirmationTemplate = htmlEditor.ResolvedValue.Trim();
                    bfi.FormConfirmationEmailField = drpEmailField.SelectedValue;
                    bfi.FormConfirmationEmailSubject = txtEmailSubject.Text.Trim();
                    bfi.FormConfirmationSendFromEmail = txtEmailFrom.Text.Trim();

                    try
                    {
                        BizFormInfoProvider.SetBizFormInfo(bfi);
                        DisplayInfoMessage(GetString("General.ChangesSaved"));
                        EnableDisableForm(bfi.FormConfirmationTemplate);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                    }
                }
                // Delete custom layout if exists
                else
                {
                    bfi.FormConfirmationTemplate = null;
                    bfi.FormConfirmationEmailField = drpEmailField.SelectedValue;
                    bfi.FormConfirmationEmailSubject = string.Empty;
                    bfi.FormConfirmationSendFromEmail = string.Empty;

                    // Delete all attachments
                    MetaFileInfoProvider.DeleteFiles(bfi.FormID, FormObjectType.BIZFORM, MetaFileInfoProvider.OBJECT_CATEGORY_FORM_LAYOUT);

                    try
                    {
                        BizFormInfoProvider.SetBizFormInfo(bfi);
                        DisplayInfoMessage(IsLayoutSet ? GetString("Bizform_Edit_Autoresponder.LayoutDeleted") : GetString("General.ChangesSaved"));
                        EnableDisableForm(bfi.FormConfirmationTemplate);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                    }
                }
            }
        }

        if (!string.IsNullOrEmpty(errorMessage))
        {
            DisplayErrorMessage(errorMessage);
        }
    }

    #endregion
}