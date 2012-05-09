using System;
using System.Web.UI.WebControls;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_Layout : CMSUserControl
{
    #region "Events"

    public event EventHandler OnBeforeSave;


    public event EventHandler OnAfterSave;

    #endregion


    #region "Private variables"

    protected string mSave = String.Empty;


    protected int mFormType = 0;


    private int mOldObjectId = 0;


    private int mDataClassId = 0;


    private bool mIsAlternative = false;


    private int mCssStyleSheetId = 0;

    #endregion


    #region "Public consts"

    /// <summary>
    /// Undefined /unknown type of layout.
    /// </summary>
    public const int FORMTYPE_UNKNOWN = 0;

    /// <summary>
    /// Layout for document types.
    /// </summary>
    public const int FORMTYPE_DOCUMENT = 1;


    /// <summary>
    /// Layout for bizforms.
    /// </summary>
    public const int FORMTYPE_BIZFORM = 2;


    /// <summary>
    /// Layout for system tables.
    /// </summary>
    public const int FORMTYPE_SYSTEMTABLE = 3;


    /// <summary>
    /// Layout for custom tables.
    /// </summary>
    public const int FORMTYPE_CUSTOMTABLE = 4;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets object id (document type id, bizform id, alternative form id).
    /// </summary>
    public int ObjectID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["ObjectID"], 0);
        }
        set
        {
            ViewState["ObjectID"] = value;
        }
    }


    /// <summary>
    /// Gets or sets layout.
    /// </summary>
    public string FormLayout
    {
        get
        {
            if (htmlEditor.Value != null)
            {
                return htmlEditor.ResolvedValue;
            }
            return null;
        }
        set
        {
            if (value != null)
            {
                htmlEditor.ResolvedValue = value;
            }
            else
            {
                htmlEditor.Value = null;
            }
        }
    }


    /// <summary>
    /// Determines whether layout was set.
    /// </summary>
    public bool LayoutIsSet
    {
        get
        {
            return !string.IsNullOrEmpty(FormLayout);
        }
    }


    /// <summary>
    /// Gets or sets state of custom layout checkbox.
    /// </summary>
    public bool CustomLayoutEnabled
    {
        get
        {
            return chkCustomLayout.Checked;
        }
        set
        {
            chkCustomLayout.Checked = value;
        }
    }


    /// <summary>
    /// Gets or sets BizForm flag for BizForms.
    /// </summary>
    public int FormType
    {
        get
        {
            return mFormType;
        }
        set
        {
            mFormType = value;
        }
    }


    /// <summary>
    /// Gets or sets CSS style sheet ID that is used for editor area.
    /// </summary>
    public int CssStyleSheetID
    {
        get
        {
            return mCssStyleSheetId;
        }
        set
        {
            mCssStyleSheetId = value;
        }
    }


    /// <summary>
    /// Gets or sets alternative form behavior.
    /// </summary>
    public bool IsAlternative
    {
        get
        {
            return mIsAlternative;
        }
        set
        {
            mIsAlternative = value;
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets appropriate id (document, bizform, alternative form)according to layout.
    /// </summary>
    private int DataClassID
    {
        get
        {
            if (mOldObjectId != ObjectID)
            {
                mOldObjectId = ObjectID;

                if (!IsAlternative)
                {
                    switch (FormType)
                    {
                        case FORMTYPE_DOCUMENT:
                        case FORMTYPE_SYSTEMTABLE:
                        case FORMTYPE_CUSTOMTABLE:
                            mDataClassId = ObjectID;
                            break;

                        case FORMTYPE_BIZFORM:
                            BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(ObjectID);
                            if (bfi != null)
                            {
                                mDataClassId = bfi.FormClassID;
                            }
                            else
                            {
                                mDataClassId = 0;
                            }
                            break;
                    }
                }
                else
                {
                    AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(ObjectID);
                    if (afi != null)
                    {
                        mDataClassId = afi.FormClassID;
                    }
                }
            }
            return mDataClassId;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register a script file
        ScriptHelper.RegisterScriptFile(this.Page, "~/CMSModules/AdminControls/Controls/Class/layout.js");

        mSave = GetString("general.save");

        lblAvailableFields.Text = GetString("DocumentType_Edit_Form.AvailableFields");
        btnGenerateLayout.Text = GetString("DocumentType_Edit_Form.btnGenerateLayout");
        btnInsertLabel.Text = GetString("DocumentType_Edit_Form.btnInsertLabel");
        btnInsertInput.Text = GetString("DocumentType_Edit_Form.btnInsertInput");
        btnInsertValLabel.Text = GetString("DocumentType_Edit_Form.InsertValidationLabel");
        btnInsertSubmitButton.Text = GetString("DocumentType_Edit_Form.InsertSubmitButton");
        btnInsertVisibility.Text = GetString("DocumentType_Edit_Form.InsertVisibility");
        chkCustomLayout.Text = GetString("DocumentType_Edit_Form.chkCustomLayout");

        // Alert messages for JavaScript
        ltlAlertExist.Text = "<input type=\"hidden\" id=\"alertexist\" value=\"" + GetString("DocumentType_Edit_Form.AlertExist") + "\">";
        ltlAlertExistFinal.Text = "<input type=\"hidden\" id=\"alertexistfinal\" value=\"" + GetString("DocumentType_Edit_Form.AlertExistFinal") + "\">";
        ltlConfirmDelete.Text = "<input type=\"hidden\" id=\"confirmdelete\" value=\"" + GetString("DocumentType_Edit_Form.ConfirmDelete") + "\">";

        // Element IDs
        ltlAvailFieldsElement.Text = ScriptHelper.GetScript("var lstAvailFieldsElem = document.getElementById('" + lstAvailableFields.ClientID + "'); ");
        ltlHtmlEditorID.Text = ScriptHelper.GetScript("var ckEditorID = '" + htmlEditor.ClientID + "'; ");

        if (!RequestHelper.IsPostBack())
        {
            InitHTMLeditor();
            FillFieldsList();
            LoadData();
            chkCustomLayout.Checked = LayoutIsSet;
        }

        // Load CSS style for editor area if any
        if (CssStyleSheetID > 0)
        {
            CssStylesheetInfo cssi = CssStylesheetInfoProvider.GetCssStylesheetInfo(CssStyleSheetID);
            if (cssi != null)
            {
                htmlEditor.EditorAreaCSS = CSSHelper.GetStylesheetUrl(cssi.StylesheetName);
            }
        }

        // Saving when layout editor is hidden
        if (!CustomLayoutEnabled)
        {
            lnkSave.OnClientClick = " SaveDocument(); return false; ";
        }
        else
        {
            lnkSave.OnClientClick = " return CheckContent(); ";
        }

        pnlCustomLayout.Visible = chkCustomLayout.Checked;

        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");

        lnkSave.Click += new EventHandler(lnkSave_Click);

        // Display button for inserting visibility macros only if enabled and the class is 'cms.user'
        if (IsAlternative)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(DataClassID);
            if ((dci != null) && (dci.ClassName.ToLower() == "cms.user"))
            {
                plcVisibility.Visible = true;
            }
        }
    }


    /// <summary>
    /// Initializes HTML editor's settings.
    /// </summary>
    protected void InitHTMLeditor()
    {
        htmlEditor.AutoDetectLanguage = false;
        htmlEditor.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }


    /// <summary>
    /// Fills list of available fields.
    /// </summary>
    protected void FillFieldsList()
    {
        FormInfo fi = null;
        FormFieldInfo[] visibleFields = null;
        DataClassInfo dci = DataClassInfoProvider.GetDataClass(DataClassID);

        if (dci != null)
        {
            // Load form definition
            string formDefinition = dci.ClassFormDefinition;
            if (IsAlternative)
            {
                // Get alternative form definition and merge if with the original one
                AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(ObjectID);

                if (afi.FormCoupledClassID > 0)
                {
                    // If coupled class is defined combine form definitions
                    DataClassInfo coupledDci = DataClassInfoProvider.GetDataClass(afi.FormCoupledClassID);
                    if (coupledDci != null)
                    {
                        formDefinition = FormHelper.MergeFormDefinitions(formDefinition, coupledDci.ClassFormDefinition, true);
                    }
                }

                // Merge class and alternative form definitions
                formDefinition = FormHelper.MergeFormDefinitions(formDefinition, afi.FormDefinition);
            }
            fi = new FormInfo(formDefinition);
            // Get visible fields
            visibleFields = fi.GetFields(true, false);

            lstAvailableFields.Items.Clear();

            if (FormType == FORMTYPE_DOCUMENT)
            {
                if (dci.ClassNodeNameSource == "") //if node name source is not set
                {
                    lstAvailableFields.Items.Add(new ListItem(GetString("DocumentType_Edit_Form.DocumentName"), "DocumentName"));
                }
            }

            if (visibleFields != null)
            {
                // Add public visible fields to the list
                foreach (FormFieldInfo ffi in visibleFields)
                {
                    lstAvailableFields.Items.Add(new ListItem(ffi.Name, ffi.Name));
                }
            }

            if (FormType == FORMTYPE_DOCUMENT)
            {
                if (dci.ClassUsePublishFromTo)
                {
                    lstAvailableFields.Items.Add(new ListItem(GetString("DocumentType_Edit_Form.DocumentPublishFrom"), "DocumentPublishFrom"));
                    lstAvailableFields.Items.Add(new ListItem(GetString("DocumentType_Edit_Form.DocumentPublishTo"), "DocumentPublishTo"));
                }
            }

            lstAvailableFields.SelectedIndex = 0;
        }
    }


    protected void lnkSave_Click(object sender, EventArgs e)
    {
        // Perform OnBeforeSave if defined
        if (OnBeforeSave != null)
        {
            OnBeforeSave(this, EventArgs.Empty);
        }

        SaveData();

        // Perform OnAfterSave if defined
        if (OnAfterSave != null)
        {
            OnAfterSave(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Loads form layout of document, bizform, systemtable or alternative form.
    /// </summary>
    protected void LoadData()
    {
        if (DataClassID > 0)
        {
            if (!IsAlternative)
            {
                DataClassInfo dci = DataClassInfoProvider.GetDataClass(DataClassID);
                CMSPage.EditedObject = dci;
                if (dci != null)
                {
                    // Load layout of document, bizform or systemtable
                    FormLayout = dci.ClassFormLayout;
                }
            }
            else
            {
                AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(ObjectID);
                CMSPage.EditedObject = afi;
                if (afi != null)
                {
                    // Load layout of alternative form
                    FormLayout = afi.FormLayout;
                }
            }
        }
        else
        {
            CMSPage.EditedObject = null;
        }
    }


    /// <summary>
    /// Saves form layout.
    /// </summary>
    protected void SaveData()
    {
        bool saved = false;
        bool deleted = false;

        // Get form layout
        string layout = FormLayout;

        // Delete layout if editor is hidden
        if (!CustomLayoutEnabled)
        {
            deleted = LayoutIsSet;
            layout = null;
        }

        if (DataClassID > 0)
        {
            if (!IsAlternative)
            {
                DataClassInfo dci = DataClassInfoProvider.GetDataClass(DataClassID);
                CMSPage.EditedObject = dci;
                if (dci != null)
                {
                    // Update dataclass form layout and save object
                    dci.ClassFormLayout = layout;
                    DataClassInfoProvider.SetDataClass(dci);
                    saved = true;
                }
            }
            else
            {
                AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(ObjectID);
                CMSPage.EditedObject = afi;
                if (afi != null)
                {
                    // Update alternative form layout and save object
                    afi.FormLayout = layout;
                    AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);
                    saved = true;
                }
            }

            // Display info if successfully saved
            if (saved)
            {
                lblInfo.Visible = true;
                if (!deleted)
                {
                    lblInfo.Text = GetString("general.changessaved");
                }
                else
                {
                    lblInfo.Text = GetString("DocumentType_Edit_Form.LayoutDeleted");
                }
            }
        }
        else
        {
            CMSPage.EditedObject = null;
        }
    }


    protected void Page_PreRender(Object sender, EventArgs e)
    {
        if (!IsClientScriptRegistered())
        {
            if (!pnlCustomLayout.Visible)
            {
                if (LayoutIsSet)
                {
                    RegisterSaveDocumentWithDeleteConfirmation();
                }
                else
                {
                    RegisterSaveDocumentOnly();
                }
            }
            else
            {
                RegisterSaveDocumentWithContentCheck();
            }
        }
    }


    /// <summary>
    /// Register client script block for document saving via 'Ctrl+S' with content checking.
    /// </summary>
    protected void RegisterSaveDocumentOnly()
    {
        ScriptHelper.RegisterSaveShortcut(lnkSave, null, false);
    }


    /// <summary>
    /// Register client script block for document saving via 'Ctrl+S' with content checking.
    /// </summary>
    protected void RegisterSaveDocumentWithContentCheck()
    {        
        string saveScript = string.Format(@"function SaveDocument() {{ if (CheckContent()) {{ {0} }} }}", 
            this.Page.ClientScript.GetPostBackClientHyperlink(lnkSave, null));

        ScriptHelper.RegisterSaveShortcut(this.Page, saveScript);
    }


    /// <summary>
    /// Register client script block for document saving via 'Ctrl+S' with layout delete confirmation.
    /// </summary>
    protected void RegisterSaveDocumentWithDeleteConfirmation()
    {
        string saveScript = string.Format(@"function SaveDocument() {{ if (ConfirmDelete()) {{ {0} }} }}",
            this.Page.ClientScript.GetPostBackClientHyperlink(lnkSave, null));

        ScriptHelper.RegisterSaveShortcut(this.Page, saveScript);        
    }


    /// <summary>
    /// Returns true if Save Document client script block is registered.
    /// </summary>
    protected bool IsClientScriptRegistered()
    {
        return ScriptHelper.IsClientScriptBlockRegistered(ScriptHelper.SAVE_DOCUMENT_SCRIPT_KEY);
    }


    protected void chkCustomLayout_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCustomLayout.Checked)
        {
            LoadData();
        }
    }

    #endregion
}
