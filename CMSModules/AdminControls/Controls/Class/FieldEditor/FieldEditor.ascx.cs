using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.EventLog;
using CMS.FormControls;

[GuidAttribute("B1E7505C-1699-4872-9A02-6D17FD661ABA")]
public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_FieldEditor : CMSUserControl
{
    #region "Events"

    /// <summary>
    /// Event raised when OK button is clicked and before xml definition is changed.
    /// </summary>
    public event EventHandler OnBeforeDefinitionUpdate;


    /// <summary>
    /// Event raised when OK button is clicked and after xml definition is changed.
    /// </summary>
    public event EventHandler OnAfterDefinitionUpdate;


    /// <summary>
    /// Event raised when new field is created and form defifition is saved.
    /// </summary>
    public event OnFieldCreatedEventHandler OnFieldCreated;


    /// <summary>
    /// Event raised when field name was changed.
    /// </summary>
    public event OnFieldNameChangedEventHandler OnFieldNameChanged;

    #endregion


    #region "Variables"

    private FormInfo fi = null;
    private FormFieldInfo ffi = null;
    private FormCategoryInfo fci = null;
    private string[] columnNames = null;
    private bool mShowFieldVisibility = false;
    private bool mDevelopmentMode = false;
    private string mClassName = String.Empty;
    private string mImageDirectoryPath = null;
    private bool mDisplaySourceFieldSelection = true;
    private int mWebPartId = 0;
    private FieldEditorModeEnum mMode;
    private FieldEditorControlsEnum mDisplayedControls = FieldEditorControlsEnum.ModeSelected;
    private bool mEnableSystemFields = false;
    private bool mEnableMacrosForDefaultValue = true;
    private string mDisplayIn = String.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// Adjust the context in which the attribute can be displayed.
    /// </summary>
    public string DisplayIn
    {
        get
        {
            return mDisplayIn;
        }
        set
        {
            mDisplayIn = value;
        }
    }


    /// <summary>
    /// Indicates if system fields from tables CMS_Tree and CMS_Document are offered to the user.
    /// </summary>
    public bool EnableSystemFields
    {
        get
        {
            return mEnableSystemFields;
        }
        set
        {
            mEnableSystemFields = value;
            databaseConfiguration.EnableSystemFields = value;
        }
    }


    /// <summary>
    /// Indicates if field visibility selector should be displayed.
    /// </summary>
    public bool ShowFieldVisibility
    {
        get
        {
            return mShowFieldVisibility;
        }
        set
        {
            mShowFieldVisibility = value;
            fieldAppearance.ShowFieldVisibility = value;
        }
    }


    /// <summary>
    /// Indicates if field editor works in development mode.
    /// </summary>
    public bool DevelopmentMode
    {
        get
        {
            return mDevelopmentMode;
        }
        set
        {
            mDevelopmentMode = value;
        }
    }


    /// <summary>
    /// Class name.
    /// </summary>
    public string ClassName
    {
        get
        {
            return mClassName;
        }
        set
        {
            mClassName = value;
            fieldAppearance.ClassName = value;
        }
    }


    /// <summary>
    /// Coupled class name.
    /// </summary>
    public string CoupledClassName
    {
        get;
        set;
    }


    /// <summary>
    /// Directory path for images.
    /// </summary>
    public string ImageDirectoryPath
    {
        get
        {
            if (String.IsNullOrEmpty(mImageDirectoryPath))
            {
                mImageDirectoryPath = "CMSModules/CMS_Class/";
            }
            return mImageDirectoryPath;
        }
        set
        {
            if (!value.EndsWith("/"))
            {
                mImageDirectoryPath = value + "/";
            }
            else
            {
                mImageDirectoryPath = value;
            }
        }
    }


    /// <summary>
    /// Indicates if display source field selection.
    /// </summary>
    public bool DisplaySourceFieldSelection
    {
        get
        {
            return mDisplaySourceFieldSelection;
        }
        set
        {
            mDisplaySourceFieldSelection = value;
        }
    }


    /// <summary>
    /// Webpart ID.
    /// </summary>
    public int WebPartId
    {
        get
        {
            return mWebPartId;
        }
        set
        {
            mWebPartId = value;
        }
    }


    /// <summary>
    /// Field editor mode.
    /// </summary>
    public FieldEditorModeEnum Mode
    {
        get
        {
            return mMode;
        }
        set
        {
            mMode = value;
            fieldAppearance.Mode = value;
        }
    }


    /// <summary>
    /// Type of custom controls that can be selected from the control list in FieldEditor.
    /// </summary>
    public FieldEditorControlsEnum DisplayedControls
    {
        get
        {
            return mDisplayedControls;
        }
        set
        {
            mDisplayedControls = value;
            fieldAppearance.DisplayedControls = value;
        }
    }


    /// <summary>
    /// Indicates if simplified mode is enabled.
    /// </summary>
    public bool EnableSimplifiedMode
    {
        get;
        set;
    }


    /// <summary>
    /// Form XML definition.
    /// </summary>
    public string FormDefinition
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FormDefinition"], "");
        }
        set
        {
            ViewState["FormDefinition"] = value;
        }
    }


    /// <summary>
    /// Enable or disable the option to use macros as default value.
    /// </summary>
    public bool EnableMacrosForDefaultValue
    {
        get
        {
            return mEnableMacrosForDefaultValue;
        }
        set
        {
            mEnableMacrosForDefaultValue = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if control is placed in wizard.
    /// </summary>
    public bool IsWizard
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if Field Editor is used as alternative form.
    /// </summary>
    public bool IsAlternativeForm
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets alternative form full name.
    /// </summary>
    public string AlternativeFormFullName
    {
        get;
        set;
    }


    /// <summary>
    /// Shows in what control is this basic form used.
    /// </summary>
    public FormTypeEnum FormType
    {
        get
        {
            return simpleMode.FormType;
        }
        set
        {
            simpleMode.FormType = value;
            controlSettings.FormType = value;
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns True if system fields are enabled and one of them is selected.
    /// </summary>
    private bool IsSystemFieldSelected
    {
        get
        {
            return databaseConfiguration.IsSystemFieldSelected;
        }
    }


    /// <summary>
    /// Selected mode.
    /// </summary>
    private FieldEditorSelectedModeEnum SelectedMode
    {
        get
        {
            FieldEditorSelectedModeEnum mode;

            if (ViewState["SelectedMode"] == null)
            {
                mode = GetDefaultSelectedMode();
            }
            else
            {
                switch (ValidationHelper.GetInteger(ViewState["SelectedMode"], 0))
                {
                    case 0:
                        mode = FieldEditorSelectedModeEnum.Simplified;
                        break;

                    case 1:
                        mode = FieldEditorSelectedModeEnum.Advanced;
                        break;

                    default:
                        mode = GetDefaultSelectedMode();
                        break;
                }
            }

            return mode;
        }
        set
        {
            switch (value)
            {
                case FieldEditorSelectedModeEnum.Simplified:
                    ViewState["SelectedMode"] = 0;
                    break;

                case FieldEditorSelectedModeEnum.Advanced:
                    ViewState["SelectedMode"] = 1;
                    break;
            }
        }
    }


    /// <summary>
    /// Indicates whether new item is edited.
    /// </summary>
    private bool IsNewItemEdited
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsNewItemEdited"], false);
        }
        set
        {
            ViewState["IsNewItemEdited"] = value;
            databaseConfiguration.IsNewItemEdited = value;
            simpleMode.IsNewItemEdited = value;
        }
    }


    /// <summary>
    /// Selected item name.
    /// </summary>
    private string SelectedItemName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SelectedItemName"], "");
        }
        set
        {
            ViewState["SelectedItemName"] = value;
        }
    }


    /// <summary>
    /// Selected item type.
    /// </summary>
    private FieldEditorSelectedItemEnum SelectedItemType
    {
        get
        {
            object obj = ViewState["SelectedItemType"];
            return (obj == null) ? 0 : (FieldEditorSelectedItemEnum)obj;
        }
        set
        {
            ViewState["SelectedItemType"] = value;
        }
    }


    /// <summary>
    /// Is field primary.
    /// </summary>
    private bool IsPrimaryField
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsPrimaryField"], false);
        }
        set
        {
            ViewState["IsPrimaryField"] = value;
        }
    }


    /// <summary>
    /// Indicates if document type is edited.
    /// </summary>
    private bool IsDocumentType
    {
        get
        {
            object obj = ViewState["IsDocumentType"];
            if ((obj == null) && (!string.IsNullOrEmpty(ClassName)))
            {
                DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
                ViewState["IsDocumentType"] = ((dci != null) && dci.ClassIsDocumentType);
            }
            return ValidationHelper.GetBoolean(ViewState["IsDocumentType"], false);
        }
    }


    /// <summary>
    /// Gets or sets value indicating what item was selected in field type drop-down list.
    /// </summary>
    private string PreviousField
    {
        get
        {
            return ValidationHelper.GetString(ViewState["PreviousValue"], "");
        }
        set
        {
            ViewState["PreviousValue"] = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if detailed controls are visible.
    /// </summary>
    private bool FieldDetailsVisible
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["FieldDetailsVisible"], false);
        }
        set
        {
            ViewState["FieldDetailsVisible"] = value;
        }
    }

    #endregion


    #region "Global definitions"

    // Constants
    private const string newCategPreffix = "#categ##new#";
    private const string newFieldPreffix = "#field##new#";
    private const string categPreffix = "#categ#";
    private const string fieldPreffix = "#field#";
    private const int preffixLength = 7; // Length of categPreffix = length of fieldPreffix = 7
    private const string controlPrefix = "#uc#";

    #endregion


    #region Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        var page = Page as CMSPage;
        if (page != null)
        {
            page.EnsureScriptManager();
            if (page.ScriptManagerControl != null)
            {
                var script = new ScriptReference("~/CMSScripts/RestoreLostFocus.js");
                page.ScriptManagerControl.Scripts.Add(script);
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (EnableSystemFields)
        {
            btnNewSysAttribute.Visible = true;
            btnNewSysAttribute.ImageUrl = GetImageUrl(ImageDirectoryPath + "NewSystem.png");
            btnNewSysAttribute.ToolTip = GetString("TemplateDesigner.NewSysAttribute");
        }
        else
        {
            btnNewSysAttribute.Visible = false;
        }

        // Hide New primary attribute if we are not in system development mode
        btnNewPrimaryAttribute.Visible = this.DevelopmentMode;
        simpleMode.DevelopmentMode = this.DevelopmentMode;
        simpleMode.Mode = this.Mode;

        // Set images url
        btnNewCategory.ImageUrl = GetImageUrl(ImageDirectoryPath + "NewCategory.png");
        btnNewAttribute.ImageUrl = GetImageUrl(ImageDirectoryPath + "New.png");
        btnNewPrimaryAttribute.ImageUrl = GetImageUrl(ImageDirectoryPath + "NewPK.png");
        btnDeleteItem.ImageUrl = GetImageUrl(ImageDirectoryPath + "Delete.png");
        btnUpAttribute.ImageUrl = GetImageUrl(ImageDirectoryPath + "Up.png");
        btnDownAttribute.ImageUrl = GetImageUrl(ImageDirectoryPath + "Down.png");

        btnDeleteItem.ToolTip = GetString("TemplateDesigner.DeleteItem");
        btnNewCategory.ToolTip = GetString("TemplateDesigner.NewCategory");
        btnNewAttribute.ToolTip = GetString("TemplateDesigner.NewAttribute");
        btnNewPrimaryAttribute.ToolTip = GetString("TemplateDesigner.NewPrimaryAttribute");
        btnDownAttribute.ToolTip = GetString("TemplateDesigner.DownAttribute");
        btnUpAttribute.ToolTip = GetString("TemplateDesigner.UpAttribute");

        if (String.Compare(this.DisplayIn, FormInfo.DISPLAY_CONTEXT_DASHBOARD, true) == 0)
        {
            pnlDisplayInDashBoard.Visible = true;
        }
        ltlConfirmText.Text = "<input type=\"hidden\" id=\"confirmdelete\" value=\"" + GetString("TemplateDesigner.ConfirmDelete") + "\"/>";
        btnDeleteItem.Attributes.Add("onclick", "javascript:return confirmDelete();");
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        lnkSave.Enabled = true;
        lnkSave.CssClass = "ContentSaveLinkButton";
        btnUpAttribute.Enabled = true;
        btnDownAttribute.Enabled = true;
        btnDeleteItem.Enabled = true;
        btnNewAttribute.Enabled = true;
        btnNewCategory.Enabled = true;

        databaseConfiguration.LoadGroupField();

        if (!URLHelper.IsPostback())
        {
            Reload(null);
        }
        else
        {
            LoadControlSettings(this.PreviousField, false);
        }

        // Mark categories and primary keys in the listbox
        MarkCategories();
        if (this.DevelopmentMode)
        {
            MarkPrimaryKeys();
        }

        // Register event handlers
        databaseConfiguration.DropChanged += databaseConfiguration_DropChanged;
        databaseConfiguration.AttributeChanged += databaseConfiguration_AttributeChanged;
        fieldAppearance.OnFieldSelected += control_FieldSelected;
        simpleMode.OnFieldSelected += control_FieldSelected;
        simpleMode.OnGetFieldInfo += simpleMode_OnGetFieldInfo;
        documentSource.OnSourceFieldChanged += documentSource_OnSourceFieldChanged;

        plcValidation.Visible = true;
        plcQuickValidation.Visible = true;
        plcSettings.Visible = true;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Display or hide tabs
        if (EnableSimplifiedMode)
        {
            if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
            {
                btnAdvanced.Visible = (SelectedMode == FieldEditorSelectedModeEnum.Simplified);
                btnSimplified.Visible = (SelectedMode != FieldEditorSelectedModeEnum.Simplified);
                pnlModeButtons.Visible = true;
            }
            else if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
            {
                pnlModeButtons.Visible = false;
            }
        }

        // Display controls and quick links according to current mode
        bool displayDetails = FieldDetailsVisible && chkDisplayInForm.Checked;
        fieldAppearance.Visible = displayDetails;
        cssSettings.Visible = displayDetails;
        validationSettings.DisplayControls();
        validationSettings.Visible = displayDetails && validationSettings.Visible;
        controlSettings.CheckVisibility();
        controlSettings.Visible = displayDetails && controlSettings.Visible;
        plcQuickValidation.Visible = displayDetails && validationSettings.Visible;
        plcQuickSettings.Visible = displayDetails && controlSettings.Visible;
        plcQuickStyles.Visible = displayDetails && cssSettings.Visible;
        plcQuickAppearance.Visible = displayDetails && fieldAppearance.Visible;
        chkDisplayInDashBoard.Enabled = chkDisplayInForm.Checked;

        // Display and store last value for simple mode
        if (this.SelectedMode == FieldEditorSelectedModeEnum.Simplified)
        {
            this.PreviousField = simpleMode.FieldType;
            plcQuickSelect.Visible = false;
        }
        // Display and store last value for advanced mode
        else
        {
            this.PreviousField = fieldAppearance.FieldType;
            plcQuickSelect.Visible = true;
        }

        // Hide quick links if only 'database quick link' should be displayed
        if ((SelectedItemType == 0) || (!plcQuickValidation.Visible && !plcQuickSettings.Visible && !plcQuickStyles.Visible && !plcQuickAppearance.Visible))
        {
            plcQuickSelect.Visible = false;
        }

        // Display error or info labels
        if (lblError.Visible || lblInfo.Visible)
        {
            lblError.ToolTip = lblError.Text;
            if (lblError.Text.Length > 100)
            {
                lblError.Text = lblError.Text.Substring(0, 97) + "...";
            }
        }
        else if (!lblError.Visible && !lblInfo.Visible)
        {
            pnlFieldEditorWrapper.CssClass = "";
        }

        // Register script for scrolling content using quick links
        ScriptHelper.RegisterJQuery(this.Page);
        ScriptHelper.RegisterStartupScript(this, typeof(string), "ContentSlider", ScriptHelper.GetScript(@"
          jQuery(document).ready(function(){
            jQuery('a[href*=#]').click(function() {
              if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'')
              && location.hostname == this.hostname) {
                var $target = jQuery(this.hash);
                $target = $target.length && $target
                || jQuery('[name=' + this.hash.slice(1) +']');
                if ($target.length) {
                  var targetOffset = $target." + (IsWizard ? "position" : "offset") + @"().top;
                  jQuery('" + (IsWizard ? ".FieldPanelRightWizard" : "html, body") + @"')
                  .animate({scrollTop: targetOffset}, 300);
                 return false;
                }
              }
            });
          });
        "));

        ScriptHelper.RegisterSaveShortcut(lnkSave, String.Empty, false);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reload field editor.
    /// </summary>
    /// <param name="selectedValue">Selected field in field list</param>
    public void Reload(string selectedValue)
    {
        Reload(selectedValue, false);
    }


    /// <summary>
    /// Reload field editor.
    /// </summary>
    /// <param name="selectedValue">Selected field in field list</param>
    /// <param name="partialReload">Indicates if not all controls need to be reloaded</param>
    private void Reload(string selectedValue, bool partialReload)
    {
        bool isModeSelected = false;
        bool isItemSelected = false;

        // Check for alternative form mode
        if (IsAlternativeForm)
        {
            if (!string.IsNullOrEmpty(FormDefinition))
            {
                isModeSelected = true;
            }
            else
            {
                // Clear item list
                lstAttributes.Items.Clear();
            }
        }
        else
        {
            switch (mMode)
            {
                case FieldEditorModeEnum.General:
                case FieldEditorModeEnum.FormControls:
                    if (!string.IsNullOrEmpty(FormDefinition))
                    {
                        isModeSelected = true;
                    }
                    else
                    {
                        // Clear item list
                        lstAttributes.Items.Clear();
                    }
                    break;

                case FieldEditorModeEnum.ClassFormDefinition:
                case FieldEditorModeEnum.BizFormDefinition:
                case FieldEditorModeEnum.SystemTable:
                case FieldEditorModeEnum.CustomTable:
                    if (!string.IsNullOrEmpty(mClassName))
                    {
                        isModeSelected = true;
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "fieldeditor.noclassname";
                    }
                    break;

                case FieldEditorModeEnum.WebPartProperties:
                    if ((mWebPartId > 0))
                    {
                        isModeSelected = true;
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "fieldeditor.nowebpartid";
                    }
                    break;

                default:
                    lblError.Visible = true;
                    lblError.ResourceString = "fieldeditor.nomode";
                    break;
            }
        }

        if (!partialReload)
        {
            // Display controls if mode is determined
            ShowOrHideFieldDetails(true);
        }

        if (isModeSelected)
        {
            isItemSelected = LoadInnerControls(selectedValue, partialReload);
        }

        // Hide controls when item isn't selected
        if ((!partialReload) && (!isItemSelected))
        {
            HideAllPanels();
            lnkSave.Enabled = false;
            lnkSave.CssClass = "MenuItemEditDisabled";
            imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
            btnUpAttribute.Enabled = false;
            btnDownAttribute.Enabled = false;
            btnDeleteItem.Enabled = false;
            btnNewAttribute.Enabled = true; // Only new items can be added
            btnNewCategory.Enabled = true; // Only new items can be added
            lblInfo.ResourceString = "fieldeditor.nofieldsdefined";
            lblInfo.Visible = true;
        }

        DisplayOrHideActions();

        if (documentSource.VisibleContent)
        {
            documentSource.Reload();
        }

        // Show or hide field visibility selector
        fieldAppearance.ShowFieldVisibility = ShowFieldVisibility;
    }


    /// <summary>
    /// Loads inner FieldEditor controls.
    /// </summary>
    /// <returns>Returns TRUE if any item is selected</returns>
    private bool LoadInnerControls(string selectedValue, bool partialReload)
    {
        bool isItemSelected = false;
        LoadFormDefinition();
        if (!partialReload)
        {
            LoadAttributesList(selectedValue);
        }

        documentSource.FormInfo = this.fi;
        documentSource.Mode = this.Mode;
        documentSource.IsAlternativeForm = this.IsAlternativeForm;
        documentSource.ClassName = this.ClassName;

        fieldAppearance.IsAlternativeForm = this.IsAlternativeForm;
        fieldAppearance.AlternativeFormFullName = this.AlternativeFormFullName;

        if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
        {
            isItemSelected = true;
            DisplaySelectedTabContent();
            ffi = fi.GetFormField(SelectedItemName);
            LoadSelectedField(partialReload);
        }
        else if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
        {
            isItemSelected = true;
            LoadSelectedCategory();
        }

        return isItemSelected;
    }


    /// <summary>
    /// Load xml definition of the form.
    /// </summary>
    private void LoadFormDefinition()
    {
        bool isError = false;

        switch (mMode)
        {
            case FieldEditorModeEnum.General:
                // Definition is loaded from external xml
                break;

            case FieldEditorModeEnum.WebPartProperties:
                if (!IsAlternativeForm)
                {
                    // Load xml definition from webpartinfo
                    WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(mWebPartId);
                    if (wpi != null)
                    {
                        FormDefinition = wpi.WebPartProperties;
                    }
                    else
                    {
                        isError = true;
                    }
                }
                break;

            case FieldEditorModeEnum.ClassFormDefinition:
            case FieldEditorModeEnum.BizFormDefinition:
            case FieldEditorModeEnum.SystemTable:
            case FieldEditorModeEnum.CustomTable:
                if (!IsAlternativeForm)
                {
                    // Load xml definition from Classinfo
                    DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
                    if (dci != null)
                    {
                        FormDefinition = dci.ClassFormDefinition;
                    }
                    else
                    {
                        isError = true;
                    }
                }
                break;
        }

        if (isError)
        {
            lblError.Visible = true;
            lblError.Text = "[ FieldEditor.LoadFormDefinition() ]: " + GetString("FieldEditor.XmlDefinitionNotLoaded");
        }

        fi = new FormInfo(FormDefinition);
    }


    /// <summary>
    /// Fill attribute list.
    /// </summary>
    /// <param name="selectedValue">Selected value in attribute list, null if first item is selected</param>
    private void LoadAttributesList(string selectedValue)
    {
        FormFieldInfo formField = null;
        FormCategoryInfo formCategory = null;
        ListItem li = null;

        // Reload list only if new item is not edited
        if (!IsNewItemEdited)
        {
            // Clear item list
            lstAttributes.Items.Clear();

            // Get all list items (fields and categories)        
            ArrayList itemList = fi.GetFormElements(true, true);

            if (itemList != null)
            {
                string itemDisplayName = null;
                string itemCodeName = null;
                foreach (object item in itemList)
                {
                    if (item is FormFieldInfo)
                    {
                        formField = ((FormFieldInfo)(item));

                        itemDisplayName = formField.Name;
                        if (!formField.AllowEmpty)
                        {
                            itemDisplayName += "*";
                        }

                        itemCodeName = fieldPreffix + formField.Name;

                        li = new ListItem(itemDisplayName, itemCodeName);

                        if (this.DevelopmentMode && formField.PrimaryKey)
                        {
                            // Highlight primary keys in the list
                            li.Attributes.Add("class", "FieldEditorPrimaryAttribute");
                        }
                    }
                    else if (item is FormCategoryInfo)
                    {
                        formCategory = ((FormCategoryInfo)(item));
                        itemDisplayName = ResHelper.LocalizeString(formCategory.CategoryCaption);
                        itemCodeName = categPreffix + formCategory.CategoryName;
                        li = new ListItem(itemDisplayName, itemCodeName);
                        // Highlight categories in the list
                        li.Attributes.Add("class", "FieldEditorCategoryItem");
                    }

                    // Load listbox
                    if (li != null)
                    {
                        lstAttributes.Items.Add(li);
                    }
                }
            }

            // Set selected item
            if (lstAttributes.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(selectedValue) && lstAttributes.Items.FindByValue(selectedValue) != null)
                {
                    lstAttributes.SelectedValue = selectedValue;
                }
                else
                {
                    // Select first item of the list       
                    lstAttributes.SelectedIndex = 0;
                }
            }

            // Default values - list is empty
            SelectedItemName = null;
            SelectedItemType = 0;

            // Save selected item info
            if (lstAttributes.SelectedValue != null)
            {
                if (lstAttributes.SelectedValue.StartsWith(fieldPreffix))
                {
                    SelectedItemName = lstAttributes.SelectedValue.Substring(preffixLength);
                    SelectedItemType = FieldEditorSelectedItemEnum.Field;
                }
                else if (lstAttributes.SelectedValue.StartsWith(categPreffix))
                {
                    SelectedItemName = lstAttributes.SelectedValue.Substring(preffixLength);
                    SelectedItemType = FieldEditorSelectedItemEnum.Category;
                }
            }
        }
    }


    /// <summary>
    /// Marks catories in list with different color.
    /// </summary>
    private void MarkCategories()
    {
        foreach (ListItem item in lstAttributes.Items)
        {
            // Mark category item with different color
            if (item.Value.StartsWith(categPreffix))
            {
                item.Attributes.Add("class", "FieldEditorCategoryItem");
            }
        }
    }


    /// <summary>
    /// Marks catories in list with different color.
    /// </summary>
    private void MarkPrimaryKeys()
    {
        if (fi == null)
        {
            LoadFormDefinition();
        }

        foreach (ListItem item in lstAttributes.Items)
        {
            // Mark primary key item with different color
            string name = item.Text;
            if (name.EndsWith("*"))
            {
                name = name.Substring(0, name.Length - 1);
            }
            FormFieldInfo ffiPk = fi.GetFormField(name);
            if (ffiPk != null)
            {
                if (ffiPk.PrimaryKey)
                {
                    item.Attributes.Add("class", "FieldEditorPrimaryAttribute");
                }
            }
        }
    }


    /// <summary>
    /// Sets all values of the category edit form to defaults.
    /// </summary>
    private void LoadDefaultCategoryEditForm()
    {
        plcCategory.Visible = true;
        plcSimple.Visible = false;
        plcAdvanced.Visible = false;
        categoryEdit.Value = string.Empty;
    }


    /// <summary>
    /// Sets all values of form to defaults.
    /// </summary>
    /// <param name="system">True - empty form for node or document attribute should be loaded, False - standard form should be loaded</param>
    /// <param name="partialReload">True - indicates that only some controls should be loaded, False - reload all controls</param>
    private void LoadDefaultAttributeEditForm(bool system, bool partialReload)
    {
        ffi = null;
        plcCategory.Visible = false;
        chkDisplayInForm.Checked = true;
        chkDisplayInDashBoard.Checked = true;

        if ((this.SelectedMode == FieldEditorSelectedModeEnum.Advanced) && !partialReload)
        {
            databaseConfiguration.DevelopmentMode = this.DevelopmentMode;
            databaseConfiguration.ShowSystemFields = system;
            databaseConfiguration.IsDocumentType = this.IsDocumentType;
            databaseConfiguration.Mode = this.Mode;
            databaseConfiguration.ClassName = this.ClassName;
            databaseConfiguration.CoupledClassName = this.CoupledClassName;
            databaseConfiguration.IsAlternativeForm = this.IsAlternativeForm;
            databaseConfiguration.Reload("", this.IsNewItemEdited);
            databaseConfiguration.ShowDefaultControl();
        }

        if (system)
        {
            LoadSystemField();
        }

        if (this.SelectedMode == FieldEditorSelectedModeEnum.Advanced)
        {
            fieldAppearance.ClassName = this.ClassName;
            fieldAppearance.AttributeType = databaseConfiguration.AttributeType;
            fieldAppearance.Reload();
            cssSettings.Reload();
            validationSettings.AttributeType = databaseConfiguration.AttributeType;
            validationSettings.Mode = this.Mode;
            validationSettings.DisplayControls();
            validationSettings.Reload();
            chkDisplayInForm.Checked = true;
            chkDisplayInDashBoard.Checked = true;
        }
        else
        {
            simpleMode.FieldInfo = null;
            simpleMode.DisplayedControls = this.DisplayedControls;
            simpleMode.Mode = this.Mode;
            simpleMode.ClearForm();
            simpleMode.LoadTypes();
            simpleMode.LoadControlSettings(null, true);
        }
    }


    /// <summary>
    /// Fill form with selected category data.
    /// </summary>    
    private void LoadSelectedCategory()
    {
        plcAdvanced.Visible = false;
        plcSimple.Visible = false;
        plcCategory.Visible = true;

        fci = fi.GetFormCategory(SelectedItemName);
        if (fci != null)
        {
            HandleInherited(fci.IsInherited);

            categoryEdit.Value = fci.CategoryCaption;
        }
        else
        {
            LoadDefaultCategoryEditForm();
        }
    }


    /// <summary>
    /// Displays controls for advanced editing.
    /// </summary>
    private void ShowAdvancedOptions()
    {
        if (this.SelectedMode != FieldEditorSelectedModeEnum.Simplified)
        {
            plcSimple.Visible = false;
            plcCategory.Visible = false;
            databaseConfiguration.Visible = true;
            controlSettings.Visible = true;
            fieldAppearance.Visible = true;
            validationSettings.Visible = true;
            cssSettings.Visible = true;
            this.FieldDetailsVisible = true;
        }
    }


    /// <summary>
    /// Handles the inheritance of the field.
    /// </summary>
    private void HandleInherited(bool inherited)
    {
        this.pnlField.Enabled = true;
        this.lblInfo.Visible = false;
        this.lnkSave.Visible = true;

        if (inherited)
        {
            // Get information on inherited class
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
            if (dci != null)
            {
                DataClassInfo parentCi = DataClassInfoProvider.GetDataClass(dci.ClassInheritsFromClassID);
                if (parentCi != null)
                {
                    this.pnlField.Enabled = false;
                    this.lnkSave.Visible = false;

                    this.lblInfo.Text = String.Format(GetString("DocumentType.FieldIsInherited"), parentCi.ClassDisplayName);
                    this.lblInfo.Visible = true;
                }
            }
        }
    }


    /// <summary>
    /// Fill form with selected attribute data.
    /// </summary>    
    /// <param name="partialReload">Indicates if only some controls should be reloaded</param>
    private void LoadSelectedField(bool partialReload)
    {
        // Fill form
        if (ffi != null)
        {
            HandleInherited(ffi.IsInherited);

            IsPrimaryField = ffi.PrimaryKey;

            if (!partialReload)
            {
                chkDisplayInForm.Checked = ffi.Visible;
                chkDisplayInDashBoard.Checked = (String.Compare(ffi.DisplayIn, this.DisplayIn, true) == 0);
            }

            ShowAdvancedOptions();

            // Load controls for advanced mode
            if (this.SelectedMode == FieldEditorSelectedModeEnum.Advanced)
            {
                if (!partialReload)
                {
                    databaseConfiguration.DevelopmentMode = this.DevelopmentMode;
                    databaseConfiguration.ShowSystemFields = ffi.External;
                    databaseConfiguration.FieldInfo = this.ffi;
                    databaseConfiguration.IsDocumentType = this.IsDocumentType;
                    databaseConfiguration.Mode = this.Mode;
                    databaseConfiguration.IsAlternativeForm = this.IsAlternativeForm;
                    databaseConfiguration.ClassName = this.ClassName;
                    databaseConfiguration.CoupledClassName = this.CoupledClassName;
                    databaseConfiguration.Reload(ffi.Name, this.IsNewItemEdited);
                }

                if (chkDisplayInForm.Checked && fieldAppearance.Visible)
                {
                    fieldAppearance.Mode = this.Mode;
                    fieldAppearance.ClassName = this.ClassName;
                    fieldAppearance.FieldInfo = this.ffi;
                    fieldAppearance.AttributeType = databaseConfiguration.AttributeType;
                    fieldAppearance.Reload();
                }

                if (chkDisplayInForm.Checked && validationSettings.Visible)
                {
                    validationSettings.IsPrimary = this.IsPrimaryField;
                    validationSettings.FieldInfo = this.ffi;
                    validationSettings.Mode = this.Mode;
                    validationSettings.AttributeType = databaseConfiguration.AttributeType;
                    validationSettings.FieldType = fieldAppearance.FieldType;
                    validationSettings.DisplayControls();
                    validationSettings.Reload();
                }

                if (chkDisplayInForm.Checked && cssSettings.Visible)
                {
                    cssSettings.FieldInfo = this.ffi;
                    cssSettings.Reload();
                }
            }
            // Load controls for simple mode
            else
            {
                simpleMode.FieldInfo = this.ffi;
                simpleMode.Mode = this.Mode;
                simpleMode.LoadTypes();
            }

            LoadControlSettings(null, false);
        }
        else
        {
            LoadDefaultAttributeEditForm(false, partialReload);
        }
    }


    /// <summary>
    /// Displays or hides actions according to the selected mode.
    /// </summary>
    protected void DisplayOrHideActions()
    {
        // Hide actions only when alternative form definition is edited
        if (IsAlternativeForm)
        {
            plcActions.Visible = false;
        }
    }


    /// <summary>
    /// Generates the class code.
    /// </summary>
    protected void GenerateCode()
    {
        // Temporarily disabled prototype
        return;

        //switch (mMode)
        //{
        //    case FieldEditorModeEnum.WebPartProperties:
        //    case FieldEditorModeEnum.SystemTable:
        //    case FieldEditorModeEnum.FormControls:
        //    case FieldEditorModeEnum.BizFormDefinition:
        //        return;
        //}

        //try
        //{
        //    // Prepare the folders
        //    string templateFile = Server.MapPath("~/App_Data/CodeTemplates/");

        //    string codeFile = Server.MapPath("~/App_Code");
        //    if (!Directory.Exists(codeFile))
        //    {
        //        codeFile = Server.MapPath("~/Old_App_Code");
        //    }

        //    codeFile += "/Global/AutoGenerated/";

        //    // Ensure the directory
        //    Directory.CreateDirectory(codeFile);

        //    // Save xml string to CMS_Class table
        //    DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
        //    if (dci != null)
        //    {
        //        // Prepare the class name
        //        string className = ValidationHelper.GetIdentifier(dci.ClassName, "");
        //        string originalClassName = null;

        //        if (dci.ClassIsDocumentType)
        //        {
        //            // Document type
        //            className += "Document";
        //            codeFile += "DocumentTypes/";
        //            templateFile += "DocType.template";
        //            originalClassName = "DocType";
        //        }
        //        else if (dci.ClassIsCustomTable)
        //        {
        //            // Custom table
        //            className += "Item";
        //            codeFile += "CustomTables/";
        //            templateFile += "CustomTableType.template";
        //            originalClassName = "CustomTableType";
        //        }
        //        else
        //        {
        //            // BizForm
        //            className += "Item";
        //            codeFile += "BizForms/";
        //            templateFile += "BizFormType.template";
        //            originalClassName = "BizFormType";
        //        }

        //        // Generate the code
        //        string code = File.ReadAllText(templateFile);

        //        StringBuilder sbInit = new StringBuilder();

        //        string propertiesCode = CodeGenerator.GetPropertiesCode(fi, false, null, null);

        //        // Replace in code
        //        code = code.Replace(originalClassName, className);
        //        code = code.Replace("// ##PROPERTIES##", propertiesCode);
        //        code = code.Replace("##SUMMARY##", dci.ClassDisplayName);

        //        codeFile += className + ".cs";

        //        File.WriteAllText(codeFile, code);
        //    }
        //    else
        //    {
        //        lblError.Visible = true;
        //        lblError.Text = "[FieldEditor.UpdateFormDefinition]: " + GetString("FieldEditor.ClassNotFound");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    // Log the error silently
        //    EventLogProvider.LogException("FieldEditor", "CODEGEN", ex);
        //}
    }


    /// <summary>
    /// Saves the form definition and refreshes the form.
    /// </summary>
    protected void SaveFormDefinition()
    {
        // Update form definition
        FormDefinition = fi.GetXmlDefinition();

        switch (mMode)
        {
            case FieldEditorModeEnum.WebPartProperties:
                // Save xml string to CMS_WebPart table
                WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(mWebPartId);
                if (wpi != null)
                {
                    wpi.WebPartProperties = FormDefinition;
                    WebPartInfoProvider.SetWebPartInfo(wpi);
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "[FieldEditor.UpdateFormDefinition]: " + GetString("FieldEditor.WebpartNotFound");
                }
                break;

            case FieldEditorModeEnum.ClassFormDefinition:
            case FieldEditorModeEnum.BizFormDefinition:
            case FieldEditorModeEnum.SystemTable:
            case FieldEditorModeEnum.CustomTable:
                // Save xml string to CMS_Class table
                DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
                if (dci != null)
                {
                    dci.ClassFormDefinition = FormDefinition;

                    using (CMSActionContext context = new CMSActionContext())
                    {
                        // Do not log synchronization for BizForm
                        if (mMode == FieldEditorModeEnum.BizFormDefinition)
                        {
                            context.DisableLogging();
                        }

                        // Save the class data
                        DataClassInfoProvider.SetDataClass(dci);

                        // Update inherited classes with new fields
                        FormHelper.UpdateInheritedClasses(dci);
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "[FieldEditor.UpdateFormDefinition]: " + GetString("FieldEditor.ClassNotFound");
                }
                break;
        }

        // Reload attibute list
        LoadAttributesList(lstAttributes.SelectedValue);
    }


    /// <summary>
    /// When attribute up button is clicked.
    /// </summary>
    protected void btnUpAttribute_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        // Raise on before definition update event
        if (OnBeforeDefinitionUpdate != null)
        {
            OnBeforeDefinitionUpdate(this, EventArgs.Empty);
        }

        if (Mode == FieldEditorModeEnum.BizFormDefinition)
        {
            // Check 'EditForm' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
            {
                RedirectToAccessDenied("cms.form", "EditForm");
            }
        }

        LoadFormDefinition();

        // First item of the attribute list cannot be moved higher
        if (string.IsNullOrEmpty(lstAttributes.SelectedValue) || (lstAttributes.SelectedIndex == 0))
        {
            return;
        }
        // 'new (not saved)' attribute cannot be moved
        else if ((SelectedItemName == newCategPreffix) || (SelectedItemName == newFieldPreffix))
        {
            ShowMessage(GetString("TemplateDesigner.AlertNewAttributeCannotBeMoved"));
            return;
        }

        if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
        {
            // Move attribute up in attribute list                        
            fi.MoveFormFieldUp(SelectedItemName);
        }
        else if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
        {
            // Move category up in attribute list                        
            fi.MoveFormCategoryUp(SelectedItemName);
        }

        // Update the form definition
        SaveFormDefinition();

        if (OnAfterDefinitionUpdate != null)
        {
            OnAfterDefinitionUpdate(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// When attribute down button is clicked.
    /// </summary>
    protected void btnDownAttribute_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        // Raise on before definition update event
        if (OnBeforeDefinitionUpdate != null)
        {
            OnBeforeDefinitionUpdate(this, EventArgs.Empty);
        }

        if (Mode == FieldEditorModeEnum.BizFormDefinition)
        {
            // Check 'EditForm' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
            {
                RedirectToAccessDenied("cms.form", "EditForm");
            }
        }

        LoadFormDefinition();

        // Last item of the attribute list cannot be moved lower
        if (string.IsNullOrEmpty(lstAttributes.SelectedValue) || lstAttributes.SelectedIndex >= lstAttributes.Items.Count - 1)
        {
            return;
        }
        // 'new and not saved' attribute cannot be moved
        else if ((SelectedItemName == newCategPreffix) || (SelectedItemName == newFieldPreffix))
        {
            ShowMessage(GetString("TemplateDesigner.AlertNewAttributeCannotBeMoved"));
            return;
        }

        if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
        {
            // Move attribute down in attribute list                        
            fi.MoveFormFieldDown(SelectedItemName);
        }
        else if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
        {
            // Move category down in attribute list                        
            fi.MoveFormCategoryDown(SelectedItemName);
        }

        // Update the form definition
        SaveFormDefinition();

        // Raise on after definition update event
        if (OnAfterDefinitionUpdate != null)
        {
            OnAfterDefinitionUpdate(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// When chkDisplayInForm checkbox checked changed.
    /// </summary>
    protected void chkDisplayInForm_CheckedChanged(Object sender, EventArgs e)
    {
        ShowOrHideFieldDetails(false);
    }


    /// <summary>
    /// Selected attribute changed event handler.
    /// </summary>
    protected void lstAttributes_SelectedIndexChanged(Object sender, EventArgs e)
    {
        bool isNewCreated = false;

        // Check if new attribute is edited -> select it and avoid selecting another attribute
        foreach (ListItem item in lstAttributes.Items)
        {
            switch (item.Value)
            {
                case newCategPreffix:
                    isNewCreated = true;
                    lstAttributes.SelectedValue = newCategPreffix;
                    break;

                case newFieldPreffix:
                    isNewCreated = true;
                    lstAttributes.SelectedValue = newFieldPreffix;
                    break;
            }

            if (isNewCreated)
            {
                ShowMessage(GetString("TemplateDesigner.AlertSaveNewItemOrDeleteItFirst"));
                if (IsSystemFieldSelected)
                {
                    databaseConfiguration.DisableFieldEditing(true, false);
                    simpleMode.DisableFieldEditing();
                }
                else
                {
                    databaseConfiguration.EnableFieldEditing();
                    simpleMode.EnableFieldEditing();
                }
                return;
            }
        }

        // Reload data
        Reload(lstAttributes.SelectedValue);
    }


    /// <summary>
    /// Show or hide details according to chkDisplayInForm checkbox is checked or not.
    /// </summary>   
    /// <param name="changeMode">Indicates if SelectedMode was changed by link</param>
    private void ShowOrHideFieldDetails(bool changeMode)
    {
        // Hide or display controls because mode was changed from Simple to Advanced or otherwise
        if (changeMode)
        {
            this.FieldDetailsVisible = (SelectedMode != FieldEditorSelectedModeEnum.Simplified);
        }
        // Hide or display controls because checkbox 'display in form' was checked
        else
        {
            this.FieldDetailsVisible = chkDisplayInForm.Checked;
        }

        if (FieldDetailsVisible)
        {
            Reload(lstAttributes.SelectedValue, true);
        }
    }


    /// <summary>
    /// Save/update button clicked.
    /// </summary>
    protected void lnkSave_Click(Object sender, EventArgs e)
    {
        // Raise on after definition update event
        if (OnBeforeDefinitionUpdate != null)
        {
            OnBeforeDefinitionUpdate(this, EventArgs.Empty);
        }

        if (Mode == FieldEditorModeEnum.BizFormDefinition)
        {
            // Check 'EditForm' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
            {
                RedirectToAccessDenied("cms.form", "EditForm");
            }
        }

        string errorMessage = String.Empty;

        if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
        {
            errorMessage += ValidateForm();
        }

        // Check occurred errors
        if (!string.IsNullOrEmpty(errorMessage))
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
        else
        {
            if (ValidateControlForms())
            {
                lblError.Text = string.Empty;
                // Save selected field
                SaveSelectedField();

                ClearHashtables();
            }
            else
            {
                lblError.Visible = true;
                lblError.ResourceString = "fieldeditor.invalidcontrolform";
            }
        }

        // Raise on after definition update event
        if (OnAfterDefinitionUpdate != null)
        {
            OnAfterDefinitionUpdate(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Save selected field.
    /// </summary>
    private void SaveSelectedField()
    {
        // FormFieldInfo structure with data from updated form
        FormFieldInfo ffiUpdated = null;
        // FormCategoryInfo structure with data from updated form
        FormCategoryInfo fciUpdated = null;
        // Determines whether it is a new attribute (or attribute to update)
        bool isNewItem = false;
        string errorMessage = null;
        DataClassInfo dci = null;
        WebPartInfo wpi = null;

        // Variables for changes in DB tables
        string tableName = null;
        string oldColumnName = null;
        string newColumnName = null;
        string newColumnSize = null;
        string newColumnType = null;
        string newColumnDefaultValue = null;  // No default value
        bool newColumnAllowNull = true;

        if (!IsAlternativeForm)
        {
            switch (mMode)
            {
                case FieldEditorModeEnum.WebPartProperties:
                    // Fill WebPartInfo structure with data from database
                    wpi = WebPartInfoProvider.GetWebPartInfo(mWebPartId);
                    break;

                case FieldEditorModeEnum.ClassFormDefinition:
                case FieldEditorModeEnum.BizFormDefinition:
                case FieldEditorModeEnum.SystemTable:
                case FieldEditorModeEnum.CustomTable:
                    // Fill ClassInfo structure with data from database
                    dci = DataClassInfoProvider.GetDataClass(mClassName);
                    if (dci != null)
                    {
                        // Set table name 
                        tableName = dci.ClassTableName;
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "fieldeditor.notablename";
                        return;
                    }
                    break;
            }
        }

        // Load current xml form definition
        LoadFormDefinition();

        if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
        {
            // Fill FormFieldInfo structure with original data
            ffi = fi.GetFormField(SelectedItemName);

            // Fill FormFieldInfo structure with updated form data
            ffiUpdated = FillFormFieldInfoStructure(ffi);

            // Determine whether it is a new attribute or not
            isNewItem = (ffi == null);

            // Check if the attribute name already exists
            if (isNewItem || (ffi.Name.ToLower() != ffiUpdated.Name.ToLower()))
            {
                columnNames = fi.GetColumnNames();

                if (columnNames != null)
                {
                    foreach (string colName in columnNames)
                    {
                        // If name already exists
                        if (ffiUpdated.Name.ToLower() == colName.ToLower())
                        {
                            lblError.Visible = true;
                            lblError.ResourceString = "TemplateDesigner.ErrorExistingColumnName";
                            return;
                        }
                    }
                }

                // Check column name duplicity in JOINed tables
                if (!IsSystemFieldSelected)
                {
                    // Check whether current column already exists in 'View_CMS_Tree_Joined'
                    if (IsDocumentType && DocumentHelper.ColumnExistsInSystemTable(ffiUpdated.Name))
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "TemplateDesigner.ErrorExistingColumnInJoinedTable";
                        return;
                    }

                    // Check whether current column is uniquie in tables used to create views - applied only for system tables
                    if ((Mode == FieldEditorModeEnum.SystemTable) && FormHelper.ColumnExistsInView(mClassName, ffiUpdated.Name))
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "TemplateDesigner.ErrorExistingColumnInJoinedTable";
                        return;
                    }
                }
            }

            // New node                
            if (isNewItem)
            {
                ffiUpdated.PrimaryKey = this.IsPrimaryField;
                newColumnName = ffiUpdated.Name;
                newColumnAllowNull = ffiUpdated.AllowEmpty;

                // Set implicit default value
                if (!(newColumnAllowNull) && (string.IsNullOrEmpty(ffiUpdated.DefaultValue)))
                {
                    if (!this.DevelopmentMode)
                    {
                        switch (ffiUpdated.DataType)
                        {
                            case FormFieldDataTypeEnum.Integer:
                            case FormFieldDataTypeEnum.LongInteger:
                            case FormFieldDataTypeEnum.Decimal:
                            case FormFieldDataTypeEnum.Boolean:
                                newColumnDefaultValue = "0";
                                break;

                            case FormFieldDataTypeEnum.Text:
                            case FormFieldDataTypeEnum.LongText:
                            case FormFieldDataTypeEnum.DocumentAttachments:
                                newColumnDefaultValue = "";
                                break;

                            case FormFieldDataTypeEnum.DateTime:
                                newColumnDefaultValue = new DateTime(1970, 1, 1, 0, 0, 0).ToString();
                                break;

                            case FormFieldDataTypeEnum.File:
                            case FormFieldDataTypeEnum.GUID:
                                // 32 digits, empty Guid
                                newColumnDefaultValue = Guid.Empty.ToString();
                                break;

                            case FormFieldDataTypeEnum.Binary:
                                newColumnDefaultValue = null;
                                break;
                        }
                    }
                }
                // Check if default value is in required format
                else if (!string.IsNullOrEmpty(ffiUpdated.DefaultValue))
                {
                    // If default value is macro, don't try to ensure the type
                    if (!ffiUpdated.IsMacro)
                    {
                        switch (ffiUpdated.DataType)
                        {
                            case FormFieldDataTypeEnum.Integer:
                                try
                                {
                                    int i = Int32.Parse(ffiUpdated.DefaultValue);
                                    newColumnDefaultValue = i.ToString();
                                }
                                catch
                                {
                                    newColumnDefaultValue = "0";
                                    errorMessage = GetString("TemplateDesigner.ErrorDefaultValueInteger");
                                }
                                break;

                            case FormFieldDataTypeEnum.LongInteger:
                                try
                                {
                                    long longInt = long.Parse(ffiUpdated.DefaultValue);
                                    newColumnDefaultValue = longInt.ToString();
                                }
                                catch
                                {
                                    newColumnDefaultValue = "0";
                                    errorMessage = GetString("TemplateDesigner.ErrorDefaultValueLongInteger");
                                }
                                break;

                            case FormFieldDataTypeEnum.Decimal:
                                if (ValidationHelper.IsDouble(ffiUpdated.DefaultValue))
                                {
                                    newColumnDefaultValue = FormHelper.GetDoubleValueInDBCulture(ffiUpdated.DefaultValue);
                                }
                                else
                                {
                                    newColumnDefaultValue = "0";
                                    errorMessage = GetString("TemplateDesigner.ErrorDefaultValueDouble");
                                }
                                break;

                            case FormFieldDataTypeEnum.DateTime:
                                if ((ffiUpdated.DefaultValue.ToLower() == DateTimePicker.DATE_TODAY.ToLower()) || (ffiUpdated.DefaultValue.ToLower() == DateTimePicker.TIME_NOW.ToLower()))
                                {
                                    newColumnDefaultValue = ffiUpdated.DefaultValue;
                                }
                                else
                                {
                                    try
                                    {
                                        DateTime dat = DateTime.Parse(ffiUpdated.DefaultValue);
                                        newColumnDefaultValue = dat.ToString();
                                    }
                                    catch
                                    {
                                        newColumnDefaultValue = DateTime.Now.ToString();
                                        errorMessage = GetString("TemplateDesigner.ErrorDefaultValueDateTime");
                                    }
                                }
                                break;

                            case FormFieldDataTypeEnum.File:
                            case FormFieldDataTypeEnum.GUID:
                                try
                                {
                                    Guid g = new Guid(ffiUpdated.DefaultValue);
                                    newColumnDefaultValue = g.ToString();
                                }
                                catch
                                {
                                    newColumnDefaultValue = Guid.Empty.ToString();
                                    errorMessage = GetString("TemplateDesigner.ErrorDefaultValueGuid");
                                }
                                break;

                            case FormFieldDataTypeEnum.LongText:
                            case FormFieldDataTypeEnum.Text:
                            case FormFieldDataTypeEnum.Boolean:

                                newColumnDefaultValue = ffiUpdated.DefaultValue;
                                break;
                        }
                    }
                }

                // Set column type and size
                LoadColumnTypeAndSize(ffiUpdated.DataType, ffiUpdated.Size, ref newColumnType, ref newColumnSize);

                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (!IsAlternativeForm)
                    {
                        switch (mMode)
                        {
                            case FieldEditorModeEnum.ClassFormDefinition:
                            case FieldEditorModeEnum.BizFormDefinition:
                            case FieldEditorModeEnum.SystemTable:
                            case FieldEditorModeEnum.CustomTable:

                                // Add new column to specified table  
                                try
                                {
                                    string newDBDefaultValue = null;

                                    // Check if it is not a macro
                                    if (ffiUpdated.IsMacro)
                                    {
                                        newDBDefaultValue = newColumnDefaultValue;
                                    }
                                    else
                                    {
                                        switch (ffiUpdated.DataType)
                                        {
                                            case FormFieldDataTypeEnum.Decimal:
                                                newDBDefaultValue = FormHelper.GetDoubleValueInDBCulture(newColumnDefaultValue);
                                                break;

                                            case FormFieldDataTypeEnum.DateTime:
                                                newDBDefaultValue = FormHelper.GetDateTimeValueInDBCulture(newColumnDefaultValue);
                                                break;
                                            default:
                                                newDBDefaultValue = newColumnDefaultValue;
                                                break;
                                        }
                                    }

                                    if (!ffiUpdated.External)
                                    {
                                        if (this.DevelopmentMode)
                                        {
                                            TableManager.AddTableColumn(tableName, newColumnName, newColumnType, newColumnAllowNull, newDBDefaultValue, false);
                                        }
                                        else
                                        {
                                            TableManager.AddTableColumn(tableName, newColumnName, newColumnType, newColumnAllowNull, newDBDefaultValue);
                                        }

                                        // Recreate the table PK constraint
                                        if (IsPrimaryField)
                                        {
                                            int pos = 0;
                                            FormFieldInfo[] pkFields = fi.GetFields(true, true, false, true);
                                            string[] primaryKeys = new string[pkFields.Length + 1];
                                            foreach (FormFieldInfo pk in pkFields)
                                            {
                                                if (pk != null)
                                                {
                                                    primaryKeys[pos++] = "[" + pk.Name + "]";
                                                }
                                            }
                                            primaryKeys[pos] = "[" + newColumnName + "]";
                                            TableManager.RecreatePKConstraint(tableName, primaryKeys, null);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblError.Visible = true;
                                    lblError.Text = ex.Message;
                                    return;
                                }

                                break;
                        }
                    }
                }
                // Some error has occurred
                else
                {
                    lblError.Visible = true;
                    lblError.Text = errorMessage;
                    return;
                }
            }
            // Existing node
            else
            {
                // Get info whether it is a primary key or system fild
                ffiUpdated.PrimaryKey = ffi.PrimaryKey;

                // If attribute is a primary key
                if (ffi.PrimaryKey)
                {
                    // Check if the attribute type is integer number
                    if (ffiUpdated.DataType != FormFieldDataTypeEnum.Integer)
                    {
                        errorMessage += GetString("TemplateDesigner.ErrorPKNotInteger") + " ";
                    }

                    // Check if allow empty is disabled
                    if (ffiUpdated.AllowEmpty)
                    {
                        errorMessage += GetString("TemplateDesigner.ErrorPKAllowsNulls") + " ";
                    }

                    // Check that the field type is label
                    string labelControlName = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLower();
                    if ((ffiUpdated.FieldType != FormFieldControlTypeEnum.LabelControl) && ((ffiUpdated.FieldType != FormFieldControlTypeEnum.CustomUserControl) && (ffiUpdated.Settings["controlname"].ToString().ToLower() != labelControlName)))
                    {
                        errorMessage += GetString("TemplateDesigner.ErrorPKisNotLabel") + " ";
                    }

                    // Some error has occurred
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("TemplateDesigner.ErrorPKThisIsPK") + " " + errorMessage;
                        return;
                    }
                }

                // If table column update is needed
                if (((ffi.PrimaryKey) && (ffi.Name != ffiUpdated.Name)) ||
                     ((!ffi.PrimaryKey) &&
                        ((ffi.Name != ffiUpdated.Name) ||
                         (ffi.DataType != ffiUpdated.DataType) ||
                         (ffi.AllowEmpty != ffiUpdated.AllowEmpty) ||
                         (ffi.Size != ffiUpdated.Size) ||
                         ((ffi.DefaultValue != ffiUpdated.DefaultValue) || (ffiUpdated.DataType == FormFieldDataTypeEnum.Decimal)))
                     )
                   )
                {
                    // Set variables needed for changes in DB                
                    oldColumnName = ffi.Name;
                    newColumnName = ffiUpdated.Name;
                    newColumnAllowNull = ffiUpdated.AllowEmpty;

                    // Set implicit default value
                    if (!(newColumnAllowNull) && (string.IsNullOrEmpty(ffiUpdated.DefaultValue)))
                    {
                        switch (ffiUpdated.DataType)
                        {
                            case FormFieldDataTypeEnum.Integer:
                            case FormFieldDataTypeEnum.LongInteger:
                            case FormFieldDataTypeEnum.Decimal:
                            case FormFieldDataTypeEnum.Boolean:
                                newColumnDefaultValue = "0";
                                break;

                            case FormFieldDataTypeEnum.Text:
                            case FormFieldDataTypeEnum.LongText:
                                newColumnDefaultValue = "";
                                break;

                            case FormFieldDataTypeEnum.DateTime:
                                newColumnDefaultValue = DateTime.Now.ToString();
                                break;

                            case FormFieldDataTypeEnum.File:
                            case FormFieldDataTypeEnum.GUID:
                                // 32 digits, empty Guid
                                newColumnDefaultValue = Guid.Empty.ToString();
                                break;

                            case FormFieldDataTypeEnum.Binary:
                                newColumnDefaultValue = null;
                                break;
                        }
                    }

                    // Check if default value is in required format
                    else if (!string.IsNullOrEmpty(ffiUpdated.DefaultValue))
                    {
                        // If default value is macro, don't try to ensure the type
                        if (!ffiUpdated.IsMacro)
                        {
                            switch (ffiUpdated.DataType)
                            {
                                case FormFieldDataTypeEnum.Integer:
                                    try
                                    {
                                        int i = Int32.Parse(ffiUpdated.DefaultValue);
                                        newColumnDefaultValue = i.ToString();
                                    }
                                    catch
                                    {
                                        newColumnDefaultValue = "0";
                                        errorMessage = GetString("TemplateDesigner.ErrorDefaultValueInteger");
                                    }
                                    break;


                                case FormFieldDataTypeEnum.LongInteger:
                                    try
                                    {
                                        long longInt = long.Parse(ffiUpdated.DefaultValue);
                                        newColumnDefaultValue = longInt.ToString();
                                    }
                                    catch
                                    {
                                        newColumnDefaultValue = "0";
                                        errorMessage = GetString("TemplateDesigner.ErrorDefaultValueLongInteger");
                                    }
                                    break;

                                case FormFieldDataTypeEnum.Decimal:
                                    if (ValidationHelper.IsDouble(ffiUpdated.DefaultValue))
                                    {
                                        newColumnDefaultValue = FormHelper.GetDoubleValueInDBCulture(ffiUpdated.DefaultValue);
                                    }
                                    else
                                    {
                                        newColumnDefaultValue = "0";
                                        errorMessage = GetString("TemplateDesigner.ErrorDefaultValueDouble");
                                    }
                                    break;

                                case FormFieldDataTypeEnum.DateTime:
                                    if ((ffiUpdated.DefaultValue.ToLower() == DateTimePicker.DATE_TODAY.ToLower()) || (ffiUpdated.DefaultValue.ToLower() == DateTimePicker.TIME_NOW.ToLower()))
                                    {
                                        newColumnDefaultValue = ffiUpdated.DefaultValue;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            DateTime dat = DateTime.Parse(ffiUpdated.DefaultValue);
                                            newColumnDefaultValue = dat.ToString();
                                        }
                                        catch
                                        {
                                            newColumnDefaultValue = DateTime.Now.ToString();
                                            errorMessage = GetString("TemplateDesigner.ErrorDefaultValueDateTime");
                                        }
                                    }
                                    break;

                                case FormFieldDataTypeEnum.File:
                                case FormFieldDataTypeEnum.GUID:
                                    try
                                    {
                                        Guid g = new Guid(ffiUpdated.DefaultValue);
                                        newColumnDefaultValue = g.ToString();
                                    }
                                    catch
                                    {
                                        newColumnDefaultValue = Guid.Empty.ToString();
                                        errorMessage = GetString("TemplateDesigner.ErrorDefaultValueGuid");
                                    }
                                    break;

                                case FormFieldDataTypeEnum.LongText:
                                case FormFieldDataTypeEnum.Text:
                                case FormFieldDataTypeEnum.Boolean:

                                    newColumnDefaultValue = ffiUpdated.DefaultValue;
                                    break;
                            }
                        }
                    }

                    // Set column type and size
                    LoadColumnTypeAndSize(ffiUpdated.DataType, ffiUpdated.Size, ref newColumnType, ref newColumnSize);

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        if (!IsAlternativeForm)
                        {
                            switch (mMode)
                            {
                                case FieldEditorModeEnum.ClassFormDefinition:
                                case FieldEditorModeEnum.BizFormDefinition:
                                case FieldEditorModeEnum.SystemTable:
                                case FieldEditorModeEnum.CustomTable:

                                    try
                                    {
                                        string newDBDefaultValue = null;

                                        // Check if it is not a macro
                                        if (ffiUpdated.IsMacro)
                                        {
                                            newDBDefaultValue = newColumnDefaultValue;
                                        }
                                        else
                                        {
                                            switch (ffiUpdated.DataType)
                                            {
                                                case FormFieldDataTypeEnum.Decimal:
                                                    newDBDefaultValue = FormHelper.GetDoubleValueInDBCulture(newColumnDefaultValue);
                                                    break;

                                                case FormFieldDataTypeEnum.DateTime:
                                                    newDBDefaultValue = FormHelper.GetDateTimeValueInDBCulture(newColumnDefaultValue);
                                                    break;
                                                default:
                                                    newDBDefaultValue = newColumnDefaultValue;
                                                    break;
                                            }
                                        }

                                        if (ffiUpdated.External)
                                        {
                                            if (!ffi.External)
                                            {
                                                // Drop old column from table
                                                TableManager.DropTableColumn(tableName, ffi.Name);
                                            }
                                        }
                                        else
                                        {
                                            if (ffi.External)
                                            {
                                                // Add table column
                                                TableManager.AddTableColumn(tableName, newColumnName, newColumnType, newColumnAllowNull, newDBDefaultValue);
                                            }
                                            else
                                            {
                                                // Change table column
                                                TableManager.AlterTableColumn(tableName, oldColumnName, newColumnName, newColumnType, newColumnAllowNull, newDBDefaultValue);
                                                if (OnFieldNameChanged != null)
                                                {
                                                    OnFieldNameChanged(this, oldColumnName, newColumnName);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // User friendly message for not null setting of column
                                        if (ffi.AllowEmpty && !newColumnAllowNull)
                                        {
                                            lblError.Visible = true;
                                            lblError.ResourceString = "FieldEditor.ColumnNotAcceptNull";
                                            lblError.ToolTip = ex.Message;
                                        }
                                        else
                                        {
                                            lblError.Visible = true;
                                            lblError.Text = ex.Message;
                                        }
                                        return;
                                    }

                                    break;
                            }
                        }
                    }
                    // Some error has occurred
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = errorMessage;
                        return;
                    }
                } // End update needed
            } // End existing node

            // Insert new field
            if (isNewItem)
            {
                InsertFormItem(ffiUpdated);
            }
            // Update current field 
            else
            {
                fi.UpdateFormField(ffi.Name, ffiUpdated);
            }
        }
        else if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
        {
            // Fill FormCategoryInfo structure with original data
            fci = fi.GetFormCategory(SelectedItemName);
            // Determine whether it is a new attribute or not
            isNewItem = (fci == null);

            // Fill FormCategoryInfo structure with updated form data
            fciUpdated = new FormCategoryInfo();
            fciUpdated.CategoryCaption = categoryEdit.Value.Replace("'", "");

            // Check if the category caption is empty
            if (string.IsNullOrEmpty(fciUpdated.CategoryCaption))
            {
                lblError.Visible = true;
                lblError.ResourceString = "TemplateDesigner.ErrorCategoryNameEmpty";
                return;
            }

            if (isNewItem)
            {
                // Use category caption for name attribut
                fciUpdated.CategoryName = fciUpdated.CategoryCaption;
            }
            else
            {
                fciUpdated.CategoryName = SelectedItemName;
            }

            if (isNewItem)
            {
                // Get form category names
                string[] categoryNames = fi.GetCategoryNames();

                if (categoryNames != null)
                {
                    // Check if the category name is unique
                    foreach (string name in categoryNames)
                    {
                        // If name already exists return error
                        if (fciUpdated.CategoryName == name)
                        {
                            lblError.Visible = true;
                            lblError.ResourceString = "TemplateDesigner.ErrorExistingCategoryName";
                            return;
                        }
                    }
                }

                // Insert new category
                InsertFormItem(fciUpdated);
            }
            else
            {
                // Update current 
                fi.UpdateFormCategory(fci.CategoryName, fciUpdated);
            }
        }

        // Make changes in database
        if (SelectedItemType != 0)
        {
            // Get updated definition
            FormDefinition = fi.GetXmlDefinition();

            string error = null;

            if (!IsAlternativeForm)
            {
                switch (mMode)
                {
                    case FieldEditorModeEnum.WebPartProperties:
                        if (wpi != null)
                        {
                            // Update xml definition
                            wpi.WebPartProperties = FormDefinition;

                            try
                            {
                                WebPartInfoProvider.SetWebPartInfo(wpi);
                            }
                            catch (Exception ex)
                            {
                                error = ex.Message;
                            }
                        }
                        else
                        {
                            error = GetString("FieldEditor.WebpartNotFound");
                        }
                        break;

                    case FieldEditorModeEnum.ClassFormDefinition:
                    case FieldEditorModeEnum.BizFormDefinition:
                    case FieldEditorModeEnum.SystemTable:
                    case FieldEditorModeEnum.CustomTable:
                        if (dci != null)
                        {
                            // Update xml definition
                            dci.ClassFormDefinition = FormDefinition;

                            // Update xml schema
                            dci.ClassXmlSchema = TableManager.GetXmlSchema(dci.ClassTableName);

                            // When updating existing field
                            if (ffi != null)
                            {
                                // Update ClassNodeNameSource field
                                if (dci.ClassNodeNameSource == ffi.Name)
                                {
                                    dci.ClassNodeNameSource = ffiUpdated.Name;
                                }
                            }

                            bool fieldType = (SelectedItemType == FieldEditorSelectedItemEnum.Field);

                            // Update changes in DB
                            try
                            {
                                // Save the data class
                                DataClassInfoProvider.SetDataClass(dci);

                                // Generate the class code
                                GenerateCode();

                                // Update inherited classes with new fields
                                FormHelper.UpdateInheritedClasses(dci);
                            }
                            catch (Exception ex)
                            {
                                error = ex.Message;
                            }

                            if (fieldType)
                            {
                                // Generate default view
                                if (mMode == FieldEditorModeEnum.BizFormDefinition)
                                {
                                    SqlGenerator.GenerateDefaultView(dci, CMSContext.CurrentSiteName);
                                }
                                else
                                {
                                    SqlGenerator.GenerateDefaultView(dci, null);
                                }

                                // Regenerate queries                            
                                SqlGenerator.GenerateDefaultQueries(dci, true, true);
                            }

                            // Updates custom views
                            if ((mMode == FieldEditorModeEnum.SystemTable) || (mMode == FieldEditorModeEnum.ClassFormDefinition))
                            {
                                try
                                {
                                    TableManager.RefreshCustomViews(dci.ClassTableName);

                                    string lowClassName = dci.ClassName.ToLower();
                                    if (lowClassName == "cms.document" || lowClassName == "cms.tree")
                                    {
                                        TableManager.RefreshDocumentViews();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error = ResHelper.GetString("fieldeditor.refreshingviewsfailed");
                                    EventLogProvider ev = new EventLogProvider();
                                    ev.LogEvent("Field Editor", "EXCEPTION", ex);
                                }
                            }
                        }
                        else
                        {
                            error = GetString("FieldEditor.ClassNotFound");
                        }
                        break;
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                lblError.Visible = true;
                lblError.Text = "[FieldEditor.SaveSelectedField()]: " + error;
            }
            else
            {
                IsNewItemEdited = false;

                if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
                {
                    Reload(categPreffix + fciUpdated.CategoryName);
                }
                else if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
                {
                    Reload(fieldPreffix + ffiUpdated.Name);
                }

                lblError.Visible = false;
                lblInfo.Visible = true;
                lblInfo.ResourceString = "general.changessaved";
            }
        }

        // All done and new item, fire OnFieldCreated  event
        if (isNewItem && (ffiUpdated != null))
        {
            RaiseOnFieldCreated(ffiUpdated);
        }
    }


    /// <summary>
    /// New category button clicked.
    /// </summary>
    protected void btnNewCategory_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (Mode == FieldEditorModeEnum.BizFormDefinition)
        {
            // Check 'EditForm' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
            {
                RedirectToAccessDenied("cms.form", "EditForm");
            }
        }

        if (IsNewItemEdited)
        {
            // Display error - Only one new item can be edited
            ShowMessage(GetString("TemplateDesigner.ErrorCannotCreateAnotherNewItem"));
        }
        else
        {
            HandleInherited(false);

            // Create #categ##new# item in list
            ListItem newListItem = new ListItem(GetString("TemplateDesigner.NewCategory"), newCategPreffix);

            if ((lstAttributes.Items.Count > 0) && (lstAttributes.SelectedIndex >= 0))
            {
                // Add behind the selected item 
                lstAttributes.Items.Insert(lstAttributes.SelectedIndex + 1, newListItem);
            }
            else
            {
                // Add at the end of the item collection
                lstAttributes.Items.Add(newListItem);
            }

            // Select new item 
            lstAttributes.SelectedIndex = lstAttributes.Items.IndexOf(newListItem);

            SelectedItemType = FieldEditorSelectedItemEnum.Category;
            SelectedItemName = newCategPreffix;

            LoadDefaultCategoryEditForm();

            IsNewItemEdited = true;
        }
    }


    /// <summary>
    /// New system attribute button clicked.
    /// </summary>
    protected void btnNewSysAttribute_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        NewAttribute(true);
    }


    /// <summary>
    /// New attribute button clicked.
    /// </summary>
    protected void btnNewAttribute_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        NewAttribute(false);
    }


    /// <summary>
    /// New attribute button clicked.
    /// </summary>
    protected void btnNewPrimaryAttribute_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        NewAttribute(false, true);
    }


    /// <summary>
    /// Creates new attribute with non-primary key.
    /// </summary>
    private void NewAttribute(bool system)
    {
        NewAttribute(system, false);
    }


    /// <summary>
    /// Creates new attribute.
    /// </summary>
    /// <param name="system">Indicates if attribute is system</param>
    /// <param name="isPrimary">Indicates if attribute is primary</param>
    private void NewAttribute(bool system, bool isPrimary)
    {
        HandleInherited(false);

        FieldDetailsVisible = true;

        // Clear settings
        simpleMode.Settings = new Hashtable();
        simpleMode.SelectedItemType = FieldEditorSelectedItemEnum.Field;
        controlSettings.Settings = new Hashtable();

        if (Mode == FieldEditorModeEnum.BizFormDefinition)
        {
            // Check 'EditForm' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
            {
                RedirectToAccessDenied("cms.form", "EditForm");
            }
        }

        IsPrimaryField = isPrimary;

        if (IsNewItemEdited)
        {
            // Only one new item can be edited
            ShowMessage(GetString("TemplateDesigner.ErrorCannotCreateAnotherNewItem"));
        }
        else
        {
            // Create #new# attribute in attribute list
            ListItem newListItem = new ListItem(GetString("TemplateDesigner.NewAttribute"), newFieldPreffix);

            if ((lstAttributes.Items.Count > 0) && (lstAttributes.SelectedIndex >= 0))
            {
                // Add behind the selected item 
                lstAttributes.Items.Insert(lstAttributes.SelectedIndex + 1, newListItem);
            }
            else
            {
                // Add at the end of the item collection
                lstAttributes.Items.Add(newListItem);
            }

            // Select new item 
            lstAttributes.SelectedIndex = lstAttributes.Items.IndexOf(newListItem);

            // Get type of previously selected item
            FieldEditorSelectedItemEnum oldItemType = SelectedItemType;

            // Initialize currently selected item type and name
            SelectedItemType = FieldEditorSelectedItemEnum.Field;
            SelectedItemName = newFieldPreffix;

            IsNewItemEdited = true;
            databaseConfiguration.AttributeName = "";

            bool newItemBlank = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSClearFieldEditor"], true);
            if (newItemBlank || (oldItemType != FieldEditorSelectedItemEnum.Field))
            {
                LoadDefaultAttributeEditForm(system, false);
            }
            DisplaySelectedTabContent();
        }
    }


    /// <summary>
    /// Delete attribute button clicked.
    /// </summary>
    protected void btnDeleteItem_Click(Object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (Mode == FieldEditorModeEnum.BizFormDefinition)
        {
            // Check 'EditForm' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
            {
                RedirectToAccessDenied("cms.form", "EditForm");
            }
        }

        // Raise on before definition update event
        if (OnBeforeDefinitionUpdate != null)
        {
            OnBeforeDefinitionUpdate(this, EventArgs.Empty);
        }

        FormFieldInfo ffiSelected = null;
        DataClassInfo dci = null;
        WebPartInfo wpi = null;
        string errorMessage = null;
        string newSelectedValue = null;
        string deletedItemPreffix = null;

        // Clear settings
        simpleMode.Settings = new Hashtable();
        controlSettings.Settings = new Hashtable();

        // Load current xml form definition
        LoadFormDefinition();

        if ((!string.IsNullOrEmpty(SelectedItemName)) && (!IsNewItemEdited))
        {
            if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
            {
                ffiSelected = fi.GetFormField(SelectedItemName);
                deletedItemPreffix = fieldPreffix;

                if (ffiSelected != null)
                {
                    // Do not allow deleting of the primary key except for external fields
                    if (ffiSelected.PrimaryKey && !ffiSelected.External)
                    {
                        if (!this.DevelopmentMode)
                        {
                            lblError.Visible = true;
                            lblError.ResourceString = "TemplateDesigner.ErrorCannotDeletePK";
                            return;
                        }
                        else
                        {
                            // Check if at least one primary key stays
                            if (fi.GetFields(true, true, false, true).Length < 2)
                            {
                                lblError.Visible = true;
                                lblError.ResourceString = "TemplateDesigner.ErrorCannotDeletePK";
                                return;
                            }
                        }
                    }

                    // Check if at least two fields stay in document type definition
                    if ((this.Mode == FieldEditorModeEnum.ClassFormDefinition) && (fi.GetFields(true, true, true).Length < 3))
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "TemplateDesigner.ErrorCannotDeleteAllCustomFields";
                        return;
                    }

                    // Do not allow deleting of the system field
                    if (ffiSelected.System && !ffiSelected.External && !DevelopmentMode)
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "TemplateDesigner.ErrorCannotDeleteSystemField";
                        return;
                    }

                    // Remove specifield field from xml form definition
                    fi.RemoveFormField(SelectedItemName);

                    // Get updated definition
                    FormDefinition = fi.GetXmlDefinition();

                    switch (mMode)
                    {
                        case FieldEditorModeEnum.WebPartProperties:
                            // Web part properties
                            {
                                wpi = WebPartInfoProvider.GetWebPartInfo(mWebPartId);
                                if (wpi != null)
                                {
                                    wpi.WebPartProperties = FormDefinition;
                                    try
                                    {
                                        WebPartInfoProvider.SetWebPartInfo(wpi);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorMessage = ex.Message;
                                    }
                                }
                                else
                                {
                                    errorMessage = GetString("FieldEditor.WebpartNotFound");
                                }
                            }
                            break;

                        case FieldEditorModeEnum.ClassFormDefinition:
                        case FieldEditorModeEnum.BizFormDefinition:
                        case FieldEditorModeEnum.SystemTable:
                        case FieldEditorModeEnum.CustomTable:
                            {
                                // Standard classes
                                dci = DataClassInfoProvider.GetDataClass(mClassName);
                                if (dci != null)
                                {
                                    // If document type is edited AND field that should be removed is FILE
                                    if ((mMode == FieldEditorModeEnum.ClassFormDefinition) && (!string.IsNullOrEmpty(ClassName)) &&
                                        (ffiSelected.DataType == FormFieldDataTypeEnum.File))
                                    {
                                        DocumentHelper.DeleteDocumentAttachments(ClassName, ffiSelected.Name, null);
                                    }

                                    // If bizform is edited AND field that should be removed is FILE
                                    if ((mMode == FieldEditorModeEnum.BizFormDefinition) && (!string.IsNullOrEmpty(ClassName)) &&
                                        (ffiSelected.FieldType == FormFieldControlTypeEnum.UploadControl))
                                    {
                                        BizFormInfoProvider.DeleteBizFormFiles(ClassName, ffiSelected.Name, CMSContext.CurrentSiteID);
                                    }

                                    // Update xml definition
                                    dci.ClassFormDefinition = FormDefinition;

                                    try
                                    {
                                        if (!ffiSelected.External)
                                        {
                                            // Remove corresponding column from table
                                            TableManager.DropTableColumn(dci.ClassTableName, SelectedItemName);

                                            // Update xml schema
                                            dci.ClassXmlSchema = TableManager.GetXmlSchema(dci.ClassTableName);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errorMessage = ex.Message;
                                    }

                                    // Deleted field is used as ClassNodeNameSource -> remove node name source
                                    if (dci.ClassNodeNameSource == SelectedItemName)
                                    {
                                        dci.ClassNodeNameSource = "";
                                    }

                                    // Update changes in database
                                    try
                                    {
                                        using (CMSActionContext context = new CMSActionContext())
                                        {
                                            // Do not log synchronization for BizForm
                                            if (mMode == FieldEditorModeEnum.BizFormDefinition)
                                            {
                                                context.DisableLogging();
                                            }

                                            // Save the data class
                                            DataClassInfoProvider.SetDataClass(dci);

                                            // Update inherited classes with new fields
                                            FormHelper.UpdateInheritedClasses(dci);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errorMessage = ex.Message;
                                    }

                                    // Refresh views and quries only if changes to DB were made
                                    if (!ffiSelected.External)
                                    {
                                        // Generate default view
                                        if (mMode == FieldEditorModeEnum.BizFormDefinition)
                                        {
                                            SqlGenerator.GenerateDefaultView(dci, CMSContext.CurrentSiteName);
                                        }
                                        else
                                        {
                                            SqlGenerator.GenerateDefaultView(dci, null);
                                        }

                                        // Regenerate queries                            
                                        SqlGenerator.GenerateDefaultQueries(dci, true, true);

                                        // Updates custom views
                                        if ((mMode == FieldEditorModeEnum.SystemTable) || (mMode == FieldEditorModeEnum.ClassFormDefinition))
                                        {
                                            try
                                            {
                                                TableManager.RefreshCustomViews(dci.ClassTableName);

                                                string lowClassName = dci.ClassName.ToLower();
                                                if (lowClassName == "cms.document" || lowClassName == "cms.tree")
                                                {
                                                    TableManager.RefreshDocumentViews();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                errorMessage = ResHelper.GetString("fieldeditor.refreshingviewsfailed");
                                                EventLogProvider ev = new EventLogProvider();
                                                ev.LogEvent("Field Editor", "EXCEPTION", ex);
                                            }
                                        }
                                    }

                                    // Clear hashtables
                                    ClearHashtables();
                                }
                                else
                                {
                                    errorMessage = GetString("FieldEditor.ClassNotFound");
                                }
                            }
                            break;
                    }
                }
            }
            else if (SelectedItemType == FieldEditorSelectedItemEnum.Category)
            {
                deletedItemPreffix = categPreffix;

                // Remove specifield category from xml form definition
                fi.RemoveFormCategory(SelectedItemName);

                // Get updated form definition
                FormDefinition = fi.GetXmlDefinition();

                switch (mMode)
                {
                    case FieldEditorModeEnum.WebPartProperties:
                        wpi = WebPartInfoProvider.GetWebPartInfo(mWebPartId);
                        if (wpi != null)
                        {
                            wpi.WebPartProperties = FormDefinition;
                            try
                            {
                                WebPartInfoProvider.SetWebPartInfo(wpi);
                            }
                            catch (Exception ex)
                            {
                                errorMessage = ex.Message;
                            }
                        }
                        else
                        {
                            errorMessage = GetString("FieldEditor.WebpartNotFound");
                        }
                        break;

                    case FieldEditorModeEnum.ClassFormDefinition:
                    case FieldEditorModeEnum.BizFormDefinition:
                    case FieldEditorModeEnum.SystemTable:
                    case FieldEditorModeEnum.CustomTable:
                        dci = DataClassInfoProvider.GetDataClass(mClassName);

                        if (dci != null)
                        {
                            // Update xml definition
                            dci.ClassFormDefinition = FormDefinition;

                            // Update changes in database
                            try
                            {
                                using (CMSActionContext context = new CMSActionContext())
                                {
                                    // Do not log synchronization for BizForm
                                    if (mMode == FieldEditorModeEnum.BizFormDefinition)
                                    {
                                        context.DisableLogging();
                                    }

                                    // Save the data class
                                    DataClassInfoProvider.SetDataClass(dci);

                                    // Update inherited classes with new fields
                                    FormHelper.UpdateInheritedClasses(dci);
                                }
                            }
                            catch (Exception ex)
                            {
                                errorMessage = ex.Message;
                            }
                        }
                        else
                        {
                            errorMessage = GetString("FieldEditor.ClassNotFound");
                        }
                        break;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                lblError.Visible = true;
                lblError.Text = "[ FieldEditor.btnDeleteItem_Click() ]: " + errorMessage;
            }
        }
        else
        {
            // "delete" new item from the list
            IsNewItemEdited = false;
        }

        // Set new selected value
        ListItem deletedItem = lstAttributes.Items.FindByValue(deletedItemPreffix + SelectedItemName);
        int deletedItemIndex = lstAttributes.Items.IndexOf(deletedItem);

        if ((deletedItemIndex > 0) && (lstAttributes.Items[deletedItemIndex - 1] != null))
        {
            newSelectedValue = lstAttributes.Items[deletedItemIndex - 1].Value;
        }

        // Reload data
        Reload(newSelectedValue);

        // Raise on after definition update event
        if (OnAfterDefinitionUpdate != null)
        {
            OnAfterDefinitionUpdate(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Show javasript's alert message.
    /// </summary>
    /// <param name="message">Message to show</param>
    private void ShowMessage(string message)
    {
        ltlScript.Text = ScriptHelper.GetScript("alert(" + ScriptHelper.GetString(message) + ");");
    }


    /// <summary>
    /// Called when source field selected index changed.
    /// </summary>
    protected void documentSource_OnSourceFieldChanged(object sender, EventArgs e)
    {
        if (mMode == FieldEditorModeEnum.ClassFormDefinition)
        {
            string errorMessage = null;

            DataClassInfo dci = DataClassInfoProvider.GetDataClass(ClassName);
            if (dci != null)
            {
                // Set document name source field
                if (string.IsNullOrEmpty(documentSource.SourceFieldValue))
                {
                    // Use extra field
                    dci.ClassNodeNameSource = "";
                }
                else
                {
                    // Use specified name
                    dci.ClassNodeNameSource = documentSource.SourceFieldValue;
                }
                // Set document alias source field
                if (string.IsNullOrEmpty(documentSource.SourceAliasFieldValue))
                {
                    // Use extra field
                    dci.ClassNodeAliasSource = "";
                }
                else
                {
                    // Use specified name
                    dci.ClassNodeAliasSource = documentSource.SourceAliasFieldValue;
                }

                try
                {
                    using (CMSActionContext context = new CMSActionContext())
                    {
                        // Do not log synchronization for BizForm
                        if (mMode == FieldEditorModeEnum.BizFormDefinition)
                        {
                            context.DisableLogging();
                        }

                        DataClassInfoProvider.SetDataClass(dci);
                    }

                    lblError.Visible = false;
                    lblInfo.Visible = true;
                    lblInfo.ResourceString = "TemplateDesigner.SourceFieldSaved";
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                errorMessage = GetString("FieldEditor.ClassNotFound");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                lblError.Visible = true;
                lblError.Text = "[ FieldEditor.drpSourceField_SelectedIndexChanged() ]: " + errorMessage;
            }
        }
    }


    /// <summary>
    /// Validates form and returns validation errorr message.
    /// </summary>
    private string ValidateForm()
    {
        const string INVALIDCHARACTERS = @".,;'`:/\*|?""&%$!-+=()[]{} ";
        string attributeName = null;
        string fieldCaption = null;
        string control = null;
        bool MinAndMaxLengthInCorrectFormat = true;
        bool MinAndMaxValueInCorrectFormat = true;

        if (this.SelectedMode == FieldEditorSelectedModeEnum.Simplified)
        {
            attributeName = simpleMode.AttributeName;
            fieldCaption = simpleMode.FieldCaption;
            control = simpleMode.FieldType;
        }
        else
        {
            attributeName = databaseConfiguration.GetAttributeName();
            fieldCaption = fieldAppearance.FieldCaption;
            control = fieldAppearance.FieldType;
        }

        // Check if attribute name isn't empty
        if (string.IsNullOrEmpty(attributeName))
        {
            return GetString("TemplateDesigner.ErrorEmptyAttributeName") + " ";
        }

        // Check if attribute name starts with a letter or '_' (if it is an identificator)
        if (!ValidationHelper.IsIdentifier(attributeName))
        {
            return GetString("TemplateDesigner.ErrorAttributeNameDoesNotStartWithLetter") + " ";
        }

        // Check attribute name for invalid characters
        for (int i = 0; i <= INVALIDCHARACTERS.Length - 1; i++)
        {
            if (attributeName.Contains(Convert.ToString(INVALIDCHARACTERS[i])))
            {
                return GetString("TemplateDesigner.ErrorInvalidCharacter") + INVALIDCHARACTERS + ". ";
            }
        }

        if (chkDisplayInForm.Checked)
        {
            // Check if field caption is entered
            if (string.IsNullOrEmpty(fieldCaption))
            {
                return GetString("TemplateDesigner.ErrorEmptyFieldCaption") + " ";
            }
            // Check if control is selected
            if (String.IsNullOrEmpty(control))
            {
                return GetString("fieldeditor.selectformcontrol");
            }
        }

        // Validation of the advanced mode controls
        if (SelectedMode == FieldEditorSelectedModeEnum.Advanced)
        {
            // Check attribute size value
            if (databaseConfiguration.AttributeType == FormFieldDataTypeCode.TEXT)
            {
                string attSize = databaseConfiguration.AttributeSize;
                // Attribute size is empty -> error
                if (string.IsNullOrEmpty(attSize))
                {
                    return GetString("TemplateDesigner.ErrorEmptyAttributeSize") + " ";
                }

                int attributeSize = ValidationHelper.GetInteger(attSize, 0);

                // Attribute size is invalid -> error
                if ((attributeSize <= 0) || (attributeSize > ValidationHelper.MAX_NVARCHAR_LENGTH))
                {
                    return GetString("TemplateDesigner.ErrorInvalidAttributeSize") + " ";
                }

                if ((databaseConfiguration.DefaultValueText.Length > attributeSize) ||
                    (databaseConfiguration.LargeDefaultValueText.Length > attributeSize))
                {
                    return GetString("TemplateDesigner.ErrorDefaultValueSize") + " ";
                }
            }

            // Check min length value
            string minLength = validationSettings.MinLengthText;
            if (!string.IsNullOrEmpty(minLength))
            {
                if ((!ValidationHelper.IsInteger(minLength)) ||
                     ((ValidationHelper.IsInteger(minLength)) && (Convert.ToInt32(minLength) < 0)))
                {
                    MinAndMaxLengthInCorrectFormat = false;
                    return GetString("TemplateDesigner.ErrorMinLengthNotInteger") + " ";
                }
            }

            // Check max length value
            string maxLength = validationSettings.MaxLengthText;
            if (!string.IsNullOrEmpty(maxLength))
            {
                if ((!ValidationHelper.IsInteger(maxLength)) ||
                     ((ValidationHelper.IsInteger(maxLength)) && (Convert.ToInt32(maxLength) < 0)))
                {
                    MinAndMaxLengthInCorrectFormat = false;
                    return GetString("TemplateDesigner.ErrorMaxLengthNotInteger") + " ";
                }
            }

            // Min and max length are specified and in correct format -> check if min length is less than max length
            if ((!string.IsNullOrEmpty(minLength)) && (!string.IsNullOrEmpty(maxLength)) && (MinAndMaxLengthInCorrectFormat))
            {
                if ((Convert.ToInt32(minLength)) > (Convert.ToInt32(maxLength)))
                {
                    return GetString("TemplateDesigner.ErrorMinLengthGreater") + " ";
                }
            }

            if (databaseConfiguration.AttributeType == FormFieldDataTypeCode.DOUBLE)
            {
                string strMinValue = validationSettings.MinValueText;
                string strMaxValue = validationSettings.MaxValueText;

                double minValue = 0.0;
                double maxValue = 0.0;

                // Check min value
                if (!string.IsNullOrEmpty(strMinValue))
                {
                    minValue = ValidationHelper.GetDouble(strMinValue, Double.NaN);

                    if (Double.IsNaN(minValue))
                    {
                        MinAndMaxValueInCorrectFormat = false;
                        return GetString("TemplateDesigner.ErrorMinValueNotDouble") + " ";
                    }
                }

                // Check max value
                if (!string.IsNullOrEmpty(strMaxValue))
                {
                    maxValue = ValidationHelper.GetDouble(strMaxValue, Double.NaN);

                    if (Double.IsNaN(maxValue))
                    {
                        MinAndMaxValueInCorrectFormat = false;
                        return GetString("TemplateDesigner.ErrorMaxValueNotDouble") + " ";
                    }
                }

                // Min and max value are specified and in correct format -> check if min value is less than max value
                if ((!string.IsNullOrEmpty(strMinValue)) && (!string.IsNullOrEmpty(strMaxValue)) && (MinAndMaxValueInCorrectFormat))
                {
                    if (minValue > maxValue)
                    {
                        return GetString("TemplateDesigner.ErrorMinValueGreater") + " ";
                    }
                }
            }
            else if (databaseConfiguration.AttributeType == FormFieldDataTypeCode.INTEGER)
            {
                // Check min value
                if (!string.IsNullOrEmpty(validationSettings.MinValueText))
                {
                    if (!ValidationHelper.IsInteger(validationSettings.MinValueText))
                    {
                        MinAndMaxValueInCorrectFormat = false;
                        return GetString("TemplateDesigner.ErrorMinValueNotInteger") + " ";
                    }
                }

                // Check max value
                if (!string.IsNullOrEmpty(validationSettings.MaxValueText))
                {
                    if (!ValidationHelper.IsInteger(validationSettings.MaxValueText))
                    {
                        MinAndMaxValueInCorrectFormat = false;
                        return GetString("TemplateDesigner.ErrorMaxValueNotInteger") + " ";
                    }
                }

                // Min and max value are specified and in correct format -> check if min value is less than max value
                if ((!string.IsNullOrEmpty(validationSettings.MinValueText)) && (!string.IsNullOrEmpty(validationSettings.MaxValueText)) && (MinAndMaxValueInCorrectFormat))
                {
                    if ((Convert.ToInt32(validationSettings.MinValueText)) > (Convert.ToInt32(validationSettings.MaxValueText)))
                    {
                        return GetString("TemplateDesigner.ErrorMinValueGreater") + " ";
                    }
                }
            }
            else if (databaseConfiguration.AttributeType == FormFieldDataTypeCode.LONGINTEGER)
            {
                string minValue = validationSettings.MinValueText;
                string maxValue = validationSettings.MaxValueText;
                // Check min value
                if (!string.IsNullOrEmpty(minValue))
                {
                    if (!ValidationHelper.IsLong(minValue))
                    {
                        MinAndMaxValueInCorrectFormat = false;
                        return GetString("TemplateDesigner.ErrorMinValueNotLongInteger") + " ";
                    }
                }

                // Check max value
                if (!string.IsNullOrEmpty(maxValue))
                {
                    if (!ValidationHelper.IsLong(maxValue))
                    {
                        MinAndMaxValueInCorrectFormat = false;
                        return GetString("TemplateDesigner.ErrorMaxValueNotLongInteger") + " ";
                    }
                }

                // Min and max value are specified and in correct format -> check if min value is less than max value
                if ((!string.IsNullOrEmpty(minValue)) && (!string.IsNullOrEmpty(maxValue)) && (MinAndMaxValueInCorrectFormat))
                {
                    if ((Convert.ToInt64(minValue)) > (Convert.ToInt64(maxValue)))
                    {
                        return GetString("TemplateDesigner.ErrorMinValueGreater") + " ";
                    }
                }
            }
        }
        // Validate simple mode
        else
        {
            // Check that textbox field has some minimum length set
            if (simpleMode.AttributeType == FormFieldDataTypeCode.TEXT)
            {
                if (simpleMode.AttributeSize <= 0 || simpleMode.AttributeSize > ValidationHelper.MAX_NVARCHAR_LENGTH)
                {
                    return GetString("TemplateDesigner.ErrorInvalidTextBoxMaxLength");
                }

                ffi = fi.GetFormField(SelectedItemName);
                if (simpleMode.GetDefaultValue(ffi).Length > simpleMode.AttributeSize)
                {
                    return GetString("TemplateDesigner.ErrorDefaultValueSize");
                }
            }
        }

        return null;
    }


    /// <summary>
    /// Validates basic forms for simple or advanced mode for generated properties.
    /// </summary>
    /// <returns>TRUE if form is valid. FALSE is form is invalid</returns>
    private bool ValidateControlForms()
    {
        if (this.SelectedMode == FieldEditorSelectedModeEnum.Simplified)
        {
            return simpleMode.SaveData();
        }
        else if (chkDisplayInForm.Checked)
        {
            return controlSettings.SaveData();
        }

        return true;
    }


    /// <summary>
    /// Returns FormFieldInfo structure with form data.
    /// </summary>   
    /// <param name="ffiOriginal">Original field info</param>
    private FormFieldInfo FillFormFieldInfoStructure(FormFieldInfo ffiOriginal)
    {
        // Field info with updated information
        FormFieldInfo ffi = new FormFieldInfo();
        // Ensure field GUID
        if (ffiOriginal != null)
        {
            ffi.Guid = ffiOriginal.Guid;
        }
        if (ffi.Guid == Guid.Empty)
        {
            ffi.Guid = Guid.NewGuid();
        }

        // Load FormFieldInfo with data from simple control
        if (this.SelectedMode == FieldEditorSelectedModeEnum.Simplified)
        {
            string selectedType = simpleMode.FieldType;
            // Part of database section
            ffi.Name = simpleMode.AttributeName;
            ffi.Caption = simpleMode.FieldCaption;
            ffi.AllowEmpty = simpleMode.AllowEmpty;
            ffi.System = simpleMode.IsSystem;
            ffi.DefaultValue = simpleMode.GetDefaultValue(ffiOriginal);
            ffi.IsMacro = EnableMacrosForDefaultValue && ValidationHelper.IsMacro(ffi.DefaultValue);

            // Get control data type
            string[] controlDefaultDataType = FormUserControlInfoProvider.GetUserControlDefaultDataType(selectedType);
            ffi.DataType = FormHelper.GetFormFieldDataType(controlDefaultDataType[0]);

            if (simpleMode.AttributeType == "text")
            {
                ffi.Size = simpleMode.AttributeSize;
            }
            else
            {
                ffi.Size = ValidationHelper.GetInteger(controlDefaultDataType[1], 0);
            }

            // Remove control prefix if exists
            if (selectedType.Contains(controlPrefix))
            {
                selectedType = selectedType.Substring(controlPrefix.Length);
            }

            // Store field type
            ffi.Settings.Add("controlname", selectedType);
            if ((simpleMode.FormData != null) && (simpleMode.FormData.ItemArray.Length > 0))
            {
                foreach (System.Data.DataColumn column in simpleMode.FormData.Table.Columns)
                {
                    ffi.Settings[column.ColumnName] = simpleMode.FormData.Table.Rows[0][column.Caption];
                }
            }

            if ((Mode == FieldEditorModeEnum.BizFormDefinition) || (DisplayedControls == FieldEditorControlsEnum.Bizforms))
            {
                ffi.PublicField = simpleMode.PublicField;
            }
            else
            {
                ffi.PublicField = false;
            }

            ffi.Visible = true;

            // Existing field -> set advanced settings to original settings
            if (ffiOriginal != null)
            {
                ffi.DataType = ffiOriginal.DataType;
                ffi.Visible = ffiOriginal.Visible;

                // Part of database section                    
                ffi.Description = ffiOriginal.Description;

                // Validation section
                ffi.RegularExpression = ffiOriginal.RegularExpression;
                ffi.MinStringLength = ffiOriginal.MinStringLength;
                ffi.MaxStringLength = ffiOriginal.MaxStringLength;
                ffi.MinValue = ffiOriginal.MinValue;
                ffi.MaxValue = ffiOriginal.MaxValue;
                ffi.MinDateTimeValue = ffiOriginal.MinDateTimeValue;
                ffi.MaxDateTimeValue = ffiOriginal.MaxDateTimeValue;
                ffi.ValidationErrorMessage = ffiOriginal.ValidationErrorMessage;

                // Design section
                ffi.CaptionStyle = ffiOriginal.CaptionStyle;
                ffi.InputControlStyle = ffiOriginal.InputControlStyle;
                ffi.ControlCssClass = ffiOriginal.ControlCssClass;
            }
        }
        // Load FormFieldInfo with data from advanced control
        else
        {
            string selectedType = fieldAppearance.FieldType;
            string attributeType = databaseConfiguration.AttributeType;
            // Part of database section
            ffi.Name = databaseConfiguration.GetAttributeName();
            ffi.Caption = fieldAppearance.FieldCaption;
            ffi.AllowEmpty = databaseConfiguration.AllowEmpty;
            ffi.System = databaseConfiguration.IsSystem;
            ffi.DefaultValue = GetDefaultValue();
            ffi.IsMacro = EnableMacrosForDefaultValue && ValidationHelper.IsMacro(ffi.DefaultValue);

            // Save user visibility
            ffi.AllowUserToChangeVisibility = fieldAppearance.ChangeVisibility;
            ffi.Visibility = fieldAppearance.VisibilityValue;
            ffi.VisibilityControl = fieldAppearance.VisibilityDDL;

            if ((Mode == FieldEditorModeEnum.BizFormDefinition) ||
                DisplayedControls == FieldEditorControlsEnum.Bizforms)
            {
                ffi.PublicField = fieldAppearance.PublicField;
            }
            else
            {
                ffi.PublicField = false;
            }

            // Remove control prefix if exists
            if (selectedType.Contains(controlPrefix))
            {
                selectedType = selectedType.Substring(controlPrefix.Length);
            }

            // Store field type
            ffi.Settings.Add("controlname", selectedType);

            // Store settings
            if ((controlSettings.FormData != null) && (controlSettings.FormData.ItemArray.Length > 0))
            {
                foreach (System.Data.DataColumn column in controlSettings.FormData.Table.Columns)
                {
                    ffi.Settings[column.ColumnName] = controlSettings.FormData.Table.Rows[0][column.Caption];
                }
            }
            // Determine if it external column
            ffi.External = IsSystemFieldSelected;

            if (((Mode == FieldEditorModeEnum.BizFormDefinition) || (Mode == FieldEditorModeEnum.SystemTable))
                && (databaseConfiguration.AttributeType == FormFieldDataTypeCode.FILE))
            {
                // Allow to save <guid>.<extension>
                ffi.DataType = FormFieldDataTypeEnum.Text;
                ffi.Size = 500;
            }
            else if (databaseConfiguration.AttributeType == FormFieldDataTypeCode.DOCUMENT_ATTACHMENTS)
            {
                ffi.DataType = FormFieldDataTypeEnum.DocumentAttachments;
                ffi.Size = 200;
            }
            else
            {
                ffi.DataType = FormHelper.GetFormFieldDataType(databaseConfiguration.AttributeType);
                ffi.Size = ValidationHelper.GetInteger(databaseConfiguration.AttributeSize, 0);
            }

            // Part of database section
            ffi.Visible = chkDisplayInForm.Checked;
            ffi.Description = fieldAppearance.Description;
            ffi.SpellCheck = validationSettings.SpellCheck;

            // Control dependencies
            ffi.HasDependingFields = fieldAppearance.HasDependingFields;
            ffi.DependsOnAnotherField = fieldAppearance.DependsOnAnotherField;

            ffi.DisplayIn = String.Empty;
            if (chkDisplayInDashBoard.Checked)
            {
                ffi.DisplayIn = this.DisplayIn;
            }

            // Validation section
            ffi.RegularExpression = validationSettings.RegularExpression;
            ffi.MinStringLength = (string.IsNullOrEmpty(validationSettings.MinLengthText)) ? -1 : Convert.ToInt32(validationSettings.MinLengthText);
            ffi.MaxStringLength = (string.IsNullOrEmpty(validationSettings.MaxLengthText)) ? -1 : Convert.ToInt32(validationSettings.MaxLengthText);

            ffi.MinValue = validationSettings.MinValueText;
            ffi.MaxValue = validationSettings.MaxValueText;

            ffi.MinDateTimeValue = validationSettings.DateFrom;
            ffi.MaxDateTimeValue = validationSettings.DateTo;
            ffi.ValidationErrorMessage = validationSettings.ErrorMessage;

            // Design section
            ffi.CaptionStyle = cssSettings.CaptionStyle;
            ffi.InputControlStyle = cssSettings.InputStyle;
            ffi.ControlCssClass = cssSettings.ControlCssClass;
        }

        ffi.FieldType = FormFieldControlTypeEnum.CustomUserControl;

        return ffi;
    }


    /// <summary>
    /// Returns default value according to attribute type.
    /// </summary>
    protected string GetDefaultValue()
    {
        string defaultValue = null;

        switch (SelectedMode)
        {
            // Advanced mode
            case FieldEditorSelectedModeEnum.Advanced:

                switch (databaseConfiguration.AttributeType)
                {
                    case FormFieldDataTypeCode.DATETIME:
                        string datetimevalue = databaseConfiguration.DefaultDateTime;
                        if (datetimevalue.ToLower() == DateTimePicker.DATE_TODAY.ToLower())
                        {
                            defaultValue = DateTimePicker.DATE_TODAY;
                        }
                        else if (datetimevalue.ToLower() == DateTimePicker.TIME_NOW.ToLower())
                        {
                            defaultValue = DateTimePicker.TIME_NOW;
                        }
                        else if (ValidationHelper.IsMacro(datetimevalue))
                        {
                            defaultValue = datetimevalue;
                        }
                        else if (ValidationHelper.GetDateTime(datetimevalue, DateTime.MinValue) == DateTimePicker.NOT_SELECTED)
                        {
                            defaultValue = "";
                        }
                        else
                        {
                            defaultValue = datetimevalue;
                        }
                        break;

                    case FormFieldDataTypeCode.BOOLEAN:
                        defaultValue = Convert.ToString(databaseConfiguration.DefaultBoolValue).ToLower();
                        break;

                    case FormFieldDataTypeCode.LONGTEXT:
                        defaultValue = databaseConfiguration.LargeDefaultValueText;
                        break;

                    case FormFieldControlTypeCode.DOCUMENT_ATTACHMENTS:
                        string defValue = databaseConfiguration.DefaultValueText;
                        defaultValue = (string.IsNullOrEmpty(defValue)) ? null : defValue;
                        break;

                    default:
                        defaultValue = databaseConfiguration.DefaultValueText;
                        break;
                }
                break;
        }

        return defaultValue;
    }


    /// <summary>
    /// Displays selected tab content.
    /// </summary>
    protected void DisplaySelectedTabContent()
    {
        switch (SelectedMode)
        {
            case FieldEditorSelectedModeEnum.Simplified:
                plcAdvanced.Visible = false;
                plcSimple.Visible = true;
                plcCategory.Visible = false;
                break;

            case FieldEditorSelectedModeEnum.Advanced:
                plcAdvanced.Visible = true;
                plcSimple.Visible = false;
                plcCategory.Visible = false;
                break;
        }
    }


    /// <summary>
    /// Called by javascript postback when tab is clicked.
    /// </summary>
    protected void btnSimplified_Command(object sender, CommandEventArgs e)
    {
        SelectedMode = FieldEditorSelectedModeEnum.Simplified;
        btnAdvanced.Visible = (SelectedMode == FieldEditorSelectedModeEnum.Simplified);
        btnSimplified.Visible = (SelectedMode != FieldEditorSelectedModeEnum.Simplified);
        Reload(lstAttributes.SelectedValue);
    }


    /// <summary>
    /// Called by javascript postback when tab is clicked.
    /// </summary>
    protected void btnAdvanced_Command(object sender, CommandEventArgs e)
    {
        SelectedMode = FieldEditorSelectedModeEnum.Advanced;
        btnAdvanced.Visible = (SelectedMode == FieldEditorSelectedModeEnum.Simplified);
        btnSimplified.Visible = (SelectedMode != FieldEditorSelectedModeEnum.Simplified);
        Reload(lstAttributes.SelectedValue);
    }


    /// <summary>
    /// Returns default selected mode.
    /// </summary>
    private FieldEditorSelectedModeEnum GetDefaultSelectedMode()
    {
        if (EnableSimplifiedMode)
        {
            return FieldEditorSelectedModeEnum.Simplified;
        }
        else
        {
            return FieldEditorSelectedModeEnum.Advanced;
        }
    }


    /// <summary>
    /// Hides all editing panels.
    /// </summary>
    protected void HideAllPanels()
    {
        plcCategory.Visible = false;
        plcSimple.Visible = false;
        plcAdvanced.Visible = false;
    }


    /// <summary>
    /// Adds new form item (field or category) to the form definition.
    /// </summary>
    /// <param name="formItem">Form item to add</param>
    protected void InsertFormItem(FormItem formItem)
    {
        // Set new item preffix
        string newItemPreffix = (formItem is FormFieldInfo) ? newFieldPreffix : newCategPreffix;

        ListItem newItem = lstAttributes.Items.FindByValue(newItemPreffix);
        int newItemIndex = lstAttributes.Items.IndexOf(newItem);

        if ((newItemIndex > 0) && (lstAttributes.Items[newItemIndex - 1] != null))
        {
            // Add new item at the specified position
            fi.AddFormItem(formItem, newItemIndex);
        }
        else
        {
            if (formItem is FormFieldInfo)
            {
                // Add new field at the end of the collection
                fi.AddFormField((FormFieldInfo)formItem);
            }
            else if (formItem is FormCategoryInfo)
            {
                // Add new category at the end of the collection
                fi.AddFormCategory((FormCategoryInfo)formItem);
            }
        }
    }


    /// <summary>
    /// Returns FormInfo for given form user control.
    /// </summary>
    /// <param name="controlName">Code name of form control</param>
    /// <returns>Form info</returns>
    private FormInfo GetUserControlSettings(string controlName)
    {
        FormUserControlInfo control = FormUserControlInfoProvider.GetFormUserControlInfo(controlName);
        if (control != null)
        {
            FormInfo fi = FormHelper.GetFormControlParameters(controlName, control.UserControlParameters, true);
            return fi;
        }
        return null;
    }


    /// <summary>
    /// Clears hashtables.
    /// </summary>
    private void ClearHashtables()
    {
        // Clear the object type hashtable
        ProviderStringDictionary.ReloadDictionaries(ClassName, true);

        // Clear the classes hashtable
        ProviderStringDictionary.ReloadDictionaries("cms.class", true);

        // Clear class strucures
        ClassStructureInfo.Remove(ClassName, true);

        // Clear data items type infos
        switch (Mode)
        {
            case FieldEditorModeEnum.CustomTable:
                CMSObjectHelper.RemoveReadOnlyObjects(CustomTableItemProvider.GetObjectType(ClassName), true);
                CustomTableItemProvider.Remove(ClassName, true);
                break;

            case FieldEditorModeEnum.BizFormDefinition:
                CMSObjectHelper.RemoveReadOnlyObjects(BizFormItemProvider.GetObjectType(ClassName), true);
                BizFormItemProvider.Remove(ClassName, true);
                break;
        }

        // Get lower case class name
        string lowerClassName = ValidationHelper.GetString(ClassName, String.Empty).ToLower();

        // Invalidate all user objects if current class in cms.user or cms.usersettings
        if ((lowerClassName == UserInfo.TYPEINFO.ObjectClassName.ToLower()) ||
            (lowerClassName == UserSettingsInfo.TYPEINFO.ObjectClassName.ToLower()))
        {
            UserInfo.TYPEINFO.InvalidateAllObjects();
        }
    }



    /// <summary>
    /// Raises OnFieldCreated event.
    /// </summary>
    /// <param name="newField">Newly created field</param>
    protected void RaiseOnFieldCreated(FormFieldInfo newField)
    {
        if (OnFieldCreated != null)
        {
            OnFieldCreated(this, newField);
        }
    }

    #endregion


    #region "Support for system fields"

    /// <summary>
    /// Group changed event handler.
    /// </summary>
    void databaseConfiguration_DropChanged(object sender, EventArgs e)
    {
        LoadSystemField();
    }


    /// <summary>
    /// Database attribute has changed.
    /// </summary>
    void databaseConfiguration_AttributeChanged(object sender, EventArgs e)
    {
        databaseConfiguration.Mode = this.Mode;
        databaseConfiguration.IsAlternativeForm = this.IsAlternativeForm;
        databaseConfiguration.EnableOrDisableAttributeSize();
        fieldAppearance.Mode = this.Mode;
        fieldAppearance.ClassName = this.ClassName;
        fieldAppearance.AttributeType = databaseConfiguration.AttributeType;
        fieldAppearance.LoadControlTypes(false);
        fieldAppearance.LoadFieldTypes(IsPrimaryField, false);
        ShowAdvancedOptions();
        validationSettings.Mode = this.Mode;
        validationSettings.AttributeType = databaseConfiguration.AttributeType;
        validationSettings.IsPrimary = IsPrimaryField;
        validationSettings.FieldInfo = this.ffi;
        databaseConfiguration.ShowDefaultControl();
        LoadControlSettings(null, false);
    }


    /// <summary>
    /// Field control changed event handler.
    /// </summary>
    void control_FieldSelected(object sender, EventArgs e)
    {
        LoadControlSettings(null, true);
    }


    /// <summary>
    /// SimpleMode control request FormFieldInfo.
    /// </summary>
    void simpleMode_OnGetFieldInfo(object sender, EventArgs e)
    {
        if (ffi != null)
        {
            simpleMode.FieldInfo = ffi;
        }
        else
        {
            LoadFormDefinition();
            simpleMode.FieldInfo = fi.GetFormField(SelectedItemName);
        }
    }


    /// <summary>
    /// Loads control with new FormInfo data.
    /// </summary>
    /// <param name="selectedFieldType">Selected field</param>
    /// <param name="defaultValues">Indicates if simple field editor should load default values for selected field</param>
    private void LoadControlSettings(string selectedFieldType, bool defaultValues)
    {
        // Reload control for simplified mode
        if (this.SelectedMode == FieldEditorSelectedModeEnum.Simplified)
        {
            simpleMode.FieldInfo = this.ffi;
            if (this.ffi != null)
            {
                simpleMode.Settings = this.ffi.Settings;
            }

            simpleMode.LoadControlSettings(selectedFieldType, defaultValues);
            simpleMode.Mode = this.Mode;
            simpleMode.ClassName = this.ClassName;
            simpleMode.DisplayedControls = this.DisplayedControls;
            simpleMode.SelectedItemType = this.SelectedItemType;
        }
        // Reload control for advanced mode
        else
        {
            if (String.IsNullOrEmpty(selectedFieldType))
            {
                selectedFieldType = fieldAppearance.FieldType;
            }
            if (selectedFieldType.StartsWith(controlPrefix))
            {
                selectedFieldType = selectedFieldType.Substring(controlPrefix.Length);
            }

            controlSettings.FormInfo = GetUserControlSettings(selectedFieldType);
            if (this.ffi != null)
            {
                controlSettings.Settings = this.ffi.Settings;
            }

            validationSettings.FieldType = selectedFieldType;
            validationSettings.AttributeType = databaseConfiguration.AttributeType;
            controlSettings.Reload();
        }
    }


    /// <summary>
    /// Loads system field either from database colum data or from field XML definition.
    /// </summary>
    private void LoadSystemField()
    {
        string tableName = databaseConfiguration.GroupValue;
        string columnName = databaseConfiguration.SystemValue;
        if (SelectedItemName.ToLower() != columnName.ToLower())
        {
            // Get field info from database column
            ffi = FormHelper.GetFormFieldInfo(tableName, columnName);
        }
        else
        {
            // Get field info from XML definition
            LoadFormDefinition();
            ffi = fi.GetFormField(SelectedItemName);
        }
        LoadSelectedField(false);
    }


    /// <summary>
    /// Loads the new column type and size.
    /// </summary>
    /// <param name="dataType">Data type</param>
    /// <param name="newSize">New size</param>
    /// <param name="newColumnType">New column type</param>
    /// <param name="newColumnSize">New column size</param>
    protected void LoadColumnTypeAndSize(FormFieldDataTypeEnum dataType, int newSize, ref string newColumnType, ref string newColumnSize)
    {
        switch (dataType)
        {
            case FormFieldDataTypeEnum.Integer:
                newColumnType = SqlHelperClass.DATATYPE_INTEGER;
                break;

            case FormFieldDataTypeEnum.LongInteger:
                newColumnType = SqlHelperClass.DATATYPE_LONG;
                break;

            case FormFieldDataTypeEnum.Decimal:
                newColumnType = SqlHelperClass.DATATYPE_FLOAT;
                break;

            case FormFieldDataTypeEnum.Boolean:
                newColumnType = SqlHelperClass.DATATYPE_BOOLEAN;
                break;

            case FormFieldDataTypeEnum.DateTime:
                newColumnType = SqlHelperClass.DATATYPE_DATETIME;
                break;

            case FormFieldDataTypeEnum.LongText:
                newColumnType = SqlHelperClass.DATATYPE_LONGTEXT;
                break;

            case FormFieldDataTypeEnum.File:
            case FormFieldDataTypeEnum.GUID:
                newColumnType = SqlHelperClass.DATATYPE_GUID;
                break;

            case FormFieldDataTypeEnum.Binary:
                newColumnType = SqlHelperClass.DATATYPE_BINARY;
                break;

            default:
                newColumnSize = Convert.ToString(newSize);
                newColumnType = SqlHelperClass.DATATYPE_TEXT + "(" + newColumnSize + ")";
                break;
        }
    }

    #endregion
}