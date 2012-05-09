using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_NotificationEmail : CMSBizFormPage
{
    #region "Variables"

    protected string mSave = null;


    private int formId = 0;


    private DataClassInfo formClassObj = null;

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

        // Get form id from url
        formId = QueryHelper.GetInteger("formid", 0);

        // Control initialization
        ltlConfirmDelete.Text = "<input type=\"hidden\" id=\"confirmdelete\" value=\"" + GetString("Bizform_Edit_Notificationemail.ConfirmDelete") + "\">";

        chkSendToEmail.Text = GetString("BizFormGeneral.chkSendToEmail");
        chkAttachDocs.Text = GetString("BizForm_Edit_NotificationEmail.AttachUploadedDocs");

        chkCustomLayout.Text = GetString("BizForm_Edit_NotificationEmail.CustomLayout");

        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        mSave = GetString("general.save");

        // Initialize HTML editor
        InitHTMLEditor();

        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (!RequestHelper.IsPostBack())
        {
            if (bfi != null)
            {
                // Get bizform class object
                formClassObj = DataClassInfoProvider.GetDataClass(bfi.FormClassID);

                // Fill list of available fields                    
                FillFieldsList();

                // Load email from/to address and email subject
                txtFromEmail.Text = ValidationHelper.GetString(bfi.FormSendFromEmail, "");
                txtToEmail.Text = ValidationHelper.GetString(bfi.FormSendToEmail, "");
                txtSubject.Text = ValidationHelper.GetString(bfi.FormEmailSubject, "");
                chkAttachDocs.Checked = bfi.FormEmailAttachUploadedDocs;
                chkSendToEmail.Checked = ((txtFromEmail.Text + txtToEmail.Text) != "");
                if (!chkSendToEmail.Checked)
                {
                    txtFromEmail.Enabled = false;
                    txtToEmail.Enabled = false;
                    txtSubject.Enabled = false;
                    chkAttachDocs.Enabled = false;
                    chkCustomLayout.Visible = false;
                    pnlCustomLayout.Visible = false;
                }
                else
                {
                    // Enable or disable form
                    EnableDisableForm(bfi.FormEmailTemplate);
                }
            }
            else
            {
                // Disable form by default
                EnableDisableForm(null);
            }
        }
    }


    protected void Page_PreRender(Object sender, EventArgs e)
    {
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
    /// Returns true if "EditShortcuts" client script block is registered.
    /// </summary>
    protected bool IsClientScriptRegistered()
    {
        return ScriptHelper.IsClientScriptBlockRegistered(ScriptHelper.SAVE_DOCUMENT_SCRIPT_KEY);
    }


    /// <summary>
    /// On chkSendToEmail checked event handler.
    /// </summary>
    protected void chkSendToEmail_CheckedChanged(object sender, EventArgs e)
    {
        txtFromEmail.Enabled = chkSendToEmail.Checked;
        txtToEmail.Enabled = chkSendToEmail.Checked;
        txtSubject.Enabled = chkSendToEmail.Checked;
        chkAttachDocs.Enabled = chkSendToEmail.Checked;
        if (chkSendToEmail.Checked)
        {
            chkCustomLayout.Visible = true;
            if (chkCustomLayout.Checked)
            {
                pnlCustomLayout.Visible = true;
                lnkSave.OnClientClick = "";

                // Reload HTML editor content
                BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
                if (bfi != null && bfi.FormEmailTemplate != null)
                {
                    htmlEditor.ResolvedValue = bfi.FormEmailTemplate;
                }
            }
        }
        else
        {
            chkCustomLayout.Visible = false;
            pnlCustomLayout.Visible = false;

            // Add delete confirmation
            if (IsLayoutSet)
            {
                lnkSave.OnClientClick = "return ConfirmDelete();";
            }
        }
    }


    /// <summary>
    /// Custom layout checkbox checked changed.
    /// </summary>
    protected void chkCustomLayout_CheckedChanged(object sender, EventArgs e)
    {
        pnlCustomLayout.Visible = !pnlCustomLayout.Visible;

        // Add delete confirmation
        if (!chkCustomLayout.Checked && IsLayoutSet)
        {
            lnkSave.OnClientClick = "return ConfirmDelete();";
        }
        // Remove delete confirmation and reload HTML editor content
        else if (chkCustomLayout.Checked)
        {
            lnkSave.OnClientClick = "";

            BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
            if (bfi != null && bfi.FormEmailTemplate != null)
            {
                htmlEditor.ResolvedValue = bfi.FormEmailTemplate;
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
            // Load form definition and get visible fields
            fi = FormHelper.GetFormInfo(formClassObj.ClassName, false);
            fields = fi.GetFields(true, true);

            lstAvailableFields.Items.Clear();

            if (fields != null)
            {
                // Add visible fields to the list
                foreach (FormFieldInfo ffi in fields)
                {
                    lstAvailableFields.Items.Add(new ListItem(ffi.Name, ffi.Name));
                }
            }
            lstAvailableFields.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// Enables or disables form according to form layout is defined or not.
    /// </summary>
    protected void EnableDisableForm(string formLayout)
    {
        // if form layout is set
        if (formLayout != null)
        {
            //enable form editing                    
            chkCustomLayout.Checked = true;
            pnlCustomLayout.Visible = true;

            // set text (form layout) to the editable window of the HTML editor
            htmlEditor.ResolvedValue = formLayout;

            // save info to viewstate 
            IsLayoutSet = true;

            lnkSave.OnClientClick = "";
        }
        else
        {
            // form is not enabled by default        
            chkCustomLayout.Checked = false;
            pnlCustomLayout.Visible = false;

            htmlEditor.Value = "";

            // save info to viewstate
            IsLayoutSet = false;

            lnkSave.OnClientClick = "";
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
    /// Save button is clicked.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }

        string errorMessage = "";

        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        if (bfi != null)
        {
            if (chkSendToEmail.Checked)
            {
                // Validate form
                errorMessage = new Validator().NotEmpty(txtFromEmail.Text, GetString("BizFormGeneral.EmptyFromEmail"))
                    .NotEmpty(txtToEmail.Text, GetString("BizFormGeneral.EmptyToEmail"))
                    .NotEmpty(txtSubject.Text, GetString("BizFormGeneral.EmptyEmailSubject")).Result;

                // Check if to e-mail contains macro expression or e-mails separated by semicolon
                if (string.IsNullOrEmpty(errorMessage) && !MacroResolver.ContainsMacro(txtToEmail.Text.Trim()) && !ValidationHelper.AreEmails(txtToEmail.Text.Trim()))
                {
                    errorMessage = GetString("BizFormGeneral.EmptyToEmail");
                }

                // Check if from e-mail contains macro expression or e-mails separated by semicolon
                if (string.IsNullOrEmpty(errorMessage) && !MacroResolver.ContainsMacro(txtFromEmail.Text.Trim()) && !ValidationHelper.IsEmail(txtFromEmail.Text.Trim()))
                {
                    errorMessage = GetString("BizFormGeneral.EmptyFromEmail");
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    bfi.FormSendFromEmail = txtFromEmail.Text.Trim();
                    bfi.FormSendToEmail = txtToEmail.Text.Trim();
                    bfi.FormEmailSubject = txtSubject.Text.Trim();
                    bfi.FormEmailAttachUploadedDocs = chkAttachDocs.Checked;
                    if (chkCustomLayout.Checked)
                    {
                        bfi.FormEmailTemplate = htmlEditor.ResolvedValue.Trim();
                    }
                    else
                    {
                        bfi.FormEmailTemplate = null;
                    }
                }
            }
            else
            {
                bfi.FormSendFromEmail = null;
                bfi.FormSendToEmail = null;
                bfi.FormEmailSubject = null;
                bfi.FormEmailTemplate = null;
                txtToEmail.Text = "";
                txtFromEmail.Text = "";
                txtSubject.Text = "";
                chkAttachDocs.Checked = true;
                htmlEditor.ResolvedValue = "";
            }

            if (errorMessage == "")
            {
                try
                {
                    BizFormInfoProvider.SetBizFormInfo(bfi);
                    DisplayInfoMessage(GetString("General.ChangesSaved"));
                    EnableDisableForm(bfi.FormEmailTemplate);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }

            if (errorMessage != "")
            {
                DisplayErrorMessage(errorMessage);
            }
        }
    }

    #endregion
}
