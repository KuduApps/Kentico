using System;
using System.Web.UI.WebControls;

using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_DatabaseConfiguration : CMSUserControl
{
    #region "Events"

    /// <summary>
    /// Event raised when drop-down list is changed.
    /// </summary>
    public event EventHandler DropChanged;


    /// <summary>
    /// Event raised when attribute is changed.
    /// </summary>
    public event EventHandler AttributeChanged;

    #endregion


    #region "Variables"

    private FieldEditorModeEnum mMode;
    private FormFieldInfo ffi = null;
    private string mFieldDataType = null;
    private bool mIsFieldPrimary = false;
    private bool mIsDocumentType = false;
    private bool mEnableSystemFields = false;
    private bool mShowSystemFields = false;
    private bool mDevelopmentMode = false;
    private bool mIsAlternativeForm = false;

    #endregion


    #region "Properties"

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
        }
    }


    /// <summary>
    /// FormFieldInfo of given field.
    /// </summary>
    public FormFieldInfo FieldInfo
    {
        get
        {
            return ffi;
        }
        set
        {
            ffi = value;
        }
    }


    /// <summary>
    /// Field data type.
    /// </summary>
    private string FieldDataType
    {
        get
        {
            return mFieldDataType;
        }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                mFieldDataType = value;
            }
            else
            {
                mFieldDataType = value.ToLower();
            }
        }
    }


    /// <summary>
    /// Gets or sets value indicating if current field is primary.
    /// </summary>
    public bool IsFieldPrimary
    {
        get
        {
            return mIsFieldPrimary;
        }
        set
        {
            mIsFieldPrimary = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if System fields should be enabled.
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
        }
    }


    /// <summary>
    /// Gets or sets value indicating if System fields should be displayed.
    /// </summary>
    public bool ShowSystemFields
    {
        get
        {
            return mShowSystemFields;
        }
        set
        {
            mShowSystemFields = value;
        }
    }



    /// <summary>
    /// Gets or sets attribute name.
    /// </summary>
    public string AttributeName
    {
        get
        {
            return txtAttributeName.Text.Trim();
        }
        set
        {
            txtAttributeName.Text = value;
        }
    }


    /// <summary>
    /// Gets value indicating new item is being edited.
    /// </summary>
    public bool IsNewItemEdited
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsNewItemEdited"], false);
        }
        set
        {
            ViewState["IsNewItemEdited"] = value;
        }
    }


    /// <summary>
    /// Indicates if document type is edited.
    /// </summary>
    public bool IsDocumentType
    {
        get
        {
            return mIsDocumentType;
        }
        set
        {
            mIsDocumentType = value;
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
    /// Gets or sets value which should be selected in group selector.
    /// </summary>
    public string GroupValue
    {
        get
        {
            return drpGroup.SelectedValue;
        }
    }


    /// <summary>
    /// Gets or sets value which should be selected in system fields selector.
    /// </summary>
    public string SystemValue
    {
        get
        {
            return drpSystemFields.SelectedValue;
        }
    }


    /// <summary>
    /// Returns True if system fields are enabled and one of them is selected.
    /// </summary>
    public bool IsSystemFieldSelected
    {
        get
        {
            return (EnableSystemFields && plcGroup.Visible);
        }
    }


    /// <summary>
    /// Returns text value from Attribute size textbox.
    /// </summary>
    public string AttributeSize
    {
        get
        {
            return txtAttributeSize.Text.Trim();
        }
    }


    /// <summary>
    /// Returns text value from Default value textbox.
    /// </summary>
    public string DefaultValueText
    {
        get
        {
            return txtDefaultValue.Text.Trim();
        }
    }


    /// <summary>
    /// Returns text value from Large Default value text area.
    /// </summary>
    public string LargeDefaultValueText
    {
        get
        {
            return txtLargeDefaultValue.Value.ToString().Trim();
        }
    }


    /// <summary>
    /// Gets value which is contained in default Date time picker.
    /// </summary>
    public string DefaultDateTime
    {
        get
        {
            return datetimeDefaultValue.DateTimeTextBox.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value which indicates if checkbox Default value is checked.
    /// </summary>
    public bool DefaultBoolValue
    {
        get
        {
            return chkDefaultValue.Checked;
        }
    }

    /// <summary>
    /// Gets or sets selected attribute type.
    /// </summary>
    public string AttributeType
    {
        get
        {
            return drpAttributeType.SelectedValue.ToLower();
        }
        set
        {
            if (!String.IsNullOrEmpty(value))
            {
                drpAttributeType.SelectedValue = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets value indicating if field allows empty values.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return chkAllowEmpty.Checked;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if field is system field.
    /// </summary>
    public bool IsSystem
    {
        get
        {
            return chkIsSystem.Checked;
        }
    }


    /// <summary>
    /// Indicates if Field Editor is used as alternative form.
    /// </summary>
    public bool IsAlternativeForm
    {
        get
        {
            return mIsAlternativeForm;
        }
        set
        {
            mIsAlternativeForm = value;
        }
    }


    /// <summary>
    /// Class name.
    /// </summary>
    public string ClassName
    {
        get;
        set;
    }


    /// <summary>
    /// Coupled class name.
    /// </summary>
    public string CoupledClassName
    {
        get;
        set;
    }

    #endregion


    #region "Public methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlDatabase.GroupingText = GetString("templatedesigner.section.database");
    }


    /// <summary>
    /// Loads group drop-down list with data.
    /// </summary>
    public void LoadGroupField()
    {
        // Set field types
        if (this.EnableSystemFields && (drpGroup.Items.Count == 0))
        {
            drpGroup.Items.Add(new ListItem(GetString("TemplateDesigner.DocumentAttributes"), "cms_document"));
            drpGroup.Items.Add(new ListItem(GetString("TemplateDesigner.NodeAttributes"), "cms_tree"));
        }
    }


    /// <summary>
    /// Loads field with values from FormFieldInfo object.
    /// </summary>
    public void Reload(string attributeName, bool enableSystemDDL)
    {
        if (this.EnableSystemFields && this.ShowSystemFields)
        {
            plcGroup.Visible = true;
            drpSystemFields.Visible = true;
            txtAttributeName.Visible = false;
            drpGroup.Enabled = enableSystemDDL;
            drpSystemFields.Enabled = enableSystemDDL;

            // Get system table name
            string tableName = null;
            if ((string.IsNullOrEmpty(attributeName)) || (attributeName.ToLower().StartsWith("document")))
            {
                // Fields from table CMS_Document
                tableName = "cms_document";
            }
            else
            {
                // Fields from table CMS_Node
                tableName = "cms_tree";
            }
            drpGroup.SelectedValue = tableName;

            // Select system attribute name in dropdownlist
            SetSystemAttributeName(tableName, attributeName);
        }
        else
        {
            // Show textbox only     
            plcGroup.Visible = false;
            drpSystemFields.Visible = false;
            txtAttributeName.Visible = true;
            txtAttributeName.Text = this.AttributeName;
        }

        LoadAttributeTypes();

        if (this.FieldInfo != null)
        {
            if (DevelopmentMode)
            {
                lblGuidValue.Text = ffi.Guid.ToString();
            }

            txtAttributeName.Text = ffi.Name;
            txtAttributeSize.Text = (ffi.Size == 0) ? "" : Convert.ToString(ffi.Size);

            // Select attribute type
            string selectedItem = FormHelper.GetFormFieldDataTypeString(ffi.DataType);
            if (((Mode == FieldEditorModeEnum.BizFormDefinition) || (Mode == FieldEditorModeEnum.SystemTable))
                        && FormHelper.IsFieldOfType(ffi, FormFieldControlTypeEnum.UploadControl))
            {
                selectedItem = FormFieldDataTypeCode.FILE;
            }
            if (drpAttributeType.Items.FindByValue(selectedItem) != null)
            {
                drpAttributeType.SelectedValue = selectedItem;
            }
            chkAllowEmpty.Checked = ffi.AllowEmpty;
            chkIsSystem.Checked = (ffi.System || (ffi.PrimaryKey && !IsDocumentType));
            plcIsSystem.Visible = DevelopmentMode;

            SetDefaultValue();
            ShowDefaultControl();
        }
        // Clear form in FormFieldInfo not specified
        else
        {
            lblGuidValue.Text = null;
            drpAttributeType.SelectedValue = null;
            txtAttributeName.Text = null;
            txtDefaultValue.Text = "";
            txtLargeDefaultValue.Value = "";
            txtAttributeSize.Text = null;
            chkDefaultValue.Checked = false;
            chkAllowEmpty.Checked = (Mode == FieldEditorModeEnum.SystemTable);
            chkIsSystem.Checked = DevelopmentMode && !IsDocumentType;
            datetimeDefaultValue.SelectedDateTime = DateTimePicker.NOT_SELECTED;
            plcIsSystem.Visible = DevelopmentMode;
        }

        // If key is primary, disable "Allow null" checkbox
        if (IsFieldPrimary)
        {
            chkAllowEmpty.Enabled = false;
            chkAllowEmpty.Checked = false;
        }

        // Primary key not allowed to change
        // System field not allowed to change unless development mode
        if ((ffi != null) && (ffi.PrimaryKey || (ffi.System && !EnableSystemFields)))
        {
            bool enableDefault = ((Mode != FieldEditorModeEnum.SystemTable) || (this.IsAlternativeForm)) && !ffi.PrimaryKey;
            DisableFieldEditing(ffi.External, enableDefault);
        }
        else
        {
            EnableFieldEditing();
            EnableDisableSections();
        }
        EnableOrDisableAttributeSize();
    }


    /// <summary>
    /// Sets default value according to attribute type.
    /// </summary>
    public void SetDefaultValue()
    {
        txtDefaultValue.Text = "";
        txtLargeDefaultValue.Value = "";
        chkDefaultValue.Checked = false;
        datetimeDefaultValue.SelectedDateTime = DateTimePicker.NOT_SELECTED;

        if (this.FieldInfo != null)
        {
            switch (this.AttributeType)
            {
                case FormFieldDataTypeCode.DATETIME:
                    if (string.IsNullOrEmpty(this.FieldInfo.DefaultValue))
                    {
                        datetimeDefaultValue.DateTimeTextBox.Text = "";
                    }
                    else if (this.FieldInfo.DefaultValue.ToLower() == DateTimePicker.DATE_TODAY.ToLower())
                    {
                        datetimeDefaultValue.DateTimeTextBox.Text = DateTimePicker.DATE_TODAY;
                    }
                    else if (this.FieldInfo.DefaultValue.ToLower() == DateTimePicker.TIME_NOW.ToLower())
                    {
                        datetimeDefaultValue.DateTimeTextBox.Text = DateTimePicker.TIME_NOW;
                    }
                    else if (ValidationHelper.IsMacro(ffi.DefaultValue))
                    {
                        datetimeDefaultValue.DateTimeTextBox.Text = ffi.DefaultValue;
                    }
                    else
                    {
                        datetimeDefaultValue.SelectedDateTime = FormHelper.GetDateTimeValueInCurrentCulture(this.FieldInfo.DefaultValue);
                    }
                    break;

                case FormFieldDataTypeCode.BOOLEAN:
                    chkDefaultValue.Checked = ValidationHelper.GetBoolean(this.FieldInfo.DefaultValue, false);
                    break;

                case FormFieldDataTypeCode.LONGTEXT:
                    txtLargeDefaultValue.Value = this.FieldInfo.DefaultValue;
                    break;

                case FormFieldDataTypeCode.DOUBLE:
                    if (ValidationHelper.IsMacro(ffi.DefaultValue))
                    {
                        txtDefaultValue.Text = ffi.DefaultValue;
                    }
                    else
                    {
                        txtDefaultValue.Text = FormHelper.GetDoubleValueInCurrentCulture(ffi.DefaultValue);
                    }
                    break;

                default:
                    txtDefaultValue.Text = ffi.DefaultValue;
                    break;
            }
        }
    }


    /// <summary>
    /// Returns attribute name.
    /// </summary>    
    public string GetAttributeName()
    {
        if (drpSystemFields.Visible)
        {
            return drpSystemFields.SelectedValue;
        }
        else
        {
            return txtAttributeName.Text.Trim();
        }
    }


    /// <summary>
    /// Enable or disable size attribute.
    /// </summary>
    public void EnableOrDisableAttributeSize()
    {
        if (this.AttributeType == "text")
        {
            if (IsAlternativeForm || IsSystemFieldSelected)
            {
                txtAttributeSize.Enabled = false;
            }
            else
            {
                txtAttributeSize.Enabled = true;
            }
            txtAttributeSize.MaxLength = 9;
        }
        else
        {
            txtAttributeSize.Enabled = false;
            txtAttributeSize.Text = "";
        }

        if (this.AttributeType == "docattachments")
        {
            chkAllowEmpty.Checked = true;
        }
        chkAllowEmpty.Enabled = ((this.AttributeType != "docattachments") && (((Mode == FieldEditorModeEnum.SystemTable) && this.IsAlternativeForm) || (Mode != FieldEditorModeEnum.SystemTable)));

        if (this.AttributeType == "binary")
        {
            chkAllowEmpty.Checked = true;
            chkAllowEmpty.Enabled = false;
            txtDefaultValue.Enabled = false;
        }

        if (this.IsAlternativeForm && !this.GetFormAllowEmpty())
        {
            chkAllowEmpty.Enabled = false;
        }
    }


    /// <summary>
    /// Disables field editing controls.
    /// </summary>
    /// <param name="enableName">Indicates if field name should remain enabled</param>
    /// <param name="enableDefault">Indicates if control for default value should remain enabled</param>
    public void DisableFieldEditing(bool enableName, bool enableDefault)
    {
        txtDefaultValue.Enabled = enableDefault;
        txtLargeDefaultValue.Enabled = enableDefault;
        chkDefaultValue.Enabled = enableDefault;
        datetimeDefaultValue.Enabled = enableDefault;
        drpAttributeType.Enabled = false;
        chkAllowEmpty.Enabled = false;
        EnableDisableAttributeName(enableName);
        chkIsSystem.Enabled = false;
        txtAttributeSize.Enabled = false;
    }


    /// <summary>
    /// Enables field editing controls, except field name.
    /// </summary>
    public void EnableFieldEditing()
    {
        txtDefaultValue.Enabled = true;
        txtLargeDefaultValue.Enabled = true;
        chkDefaultValue.Enabled = true;
        datetimeDefaultValue.Enabled = true;
        drpAttributeType.Enabled = true;
        chkAllowEmpty.Enabled = (this.Mode != FieldEditorModeEnum.SystemTable) && !IsAlternativeForm;
        chkIsSystem.Enabled = true;
        EnableDisableAttributeName(true);
    }


    /// <summary>
    /// Show default value control according to attribute type.
    /// </summary>
    public void ShowDefaultControl()
    {
        plcDefaultValue.Visible = true;

        switch (this.AttributeType)
        {
            case FormFieldDataTypeCode.DATETIME:
                datetimeDefaultValue.Visible = true;
                chkDefaultValue.Visible = false;
                txtLargeDefaultValue.Visible = false;
                txtDefaultValue.Visible = false;
                break;

            case FormFieldDataTypeCode.BOOLEAN:
                chkDefaultValue.Visible = true;
                txtLargeDefaultValue.Visible = false;
                txtDefaultValue.Visible = false;
                datetimeDefaultValue.Visible = false;
                break;

            case FormFieldDataTypeCode.LONGTEXT:
                txtLargeDefaultValue.Visible = true;
                chkDefaultValue.Visible = false;
                txtDefaultValue.Visible = false;
                datetimeDefaultValue.Visible = false;
                break;

            case FormFieldDataTypeCode.FILE:
                // Hide default value for File-type fields within Document types
                if (this.Mode == FieldEditorModeEnum.ClassFormDefinition)
                {
                    plcDefaultValue.Visible = false;
                }
                // Display textbox otherwise
                else
                {
                    txtDefaultValue.Visible = true;
                    chkDefaultValue.Visible = false;
                    txtLargeDefaultValue.Visible = false;
                    datetimeDefaultValue.Visible = false;
                }
                break;

            default:
                txtDefaultValue.Visible = true;
                chkDefaultValue.Visible = false;
                txtLargeDefaultValue.Visible = false;
                datetimeDefaultValue.Visible = false;
                break;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Fill attribute types list.
    /// </summary>
    private void LoadAttributeTypes()
    {
        if (drpAttributeType.Items.Count > 0)
        {
            return;
        }

        drpAttributeType.DataSource = FormHelper.GetFieldTypes(IsDocumentType, DevelopmentMode);
        drpAttributeType.DataBind();

        // Remove file type when editing webpart properties 
        if ((Mode == FieldEditorModeEnum.WebPartProperties) || (Mode == FieldEditorModeEnum.CustomTable) || (Mode == FieldEditorModeEnum.SystemTable) || (Mode == FieldEditorModeEnum.FormControls))
        {
            ListItem item = drpAttributeType.Items.FindByValue(FormFieldDataTypeCode.FILE);
            if (item != null)
            {
                drpAttributeType.Items.Remove(item);
            }
        }

        if (drpAttributeType.Items.Count > 0)
        {
            drpAttributeType.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// Sets system attribute name.
    /// </summary>
    private void SetSystemAttributeName(string tableName, string attributeName)
    {
        // Load system fields from specified table
        drpSystemFields.DataSource = TableManager.GetColumnInformation(tableName);
        drpSystemFields.DataBind();
        try
        {
            drpSystemFields.SelectedValue = attributeName;
        }
        catch
        {
            if (drpSystemFields.Items.Count > 0)
            {
                drpSystemFields.SelectedIndex = 0;
            }
        }
    }


    /// <summary>
    /// Enables or disables control related to attribute name.
    /// </summary>
    private void EnableDisableAttributeName(bool enable)
    {
        drpGroup.Enabled = enable && IsNewItemEdited;
        drpSystemFields.Enabled = enable && IsNewItemEdited;
        txtAttributeName.Enabled = enable;
    }


    /// <summary>
    /// Enables or disables sections of the unigrid according to the selected mode.
    /// </summary>
    private void EnableDisableSections()
    {
        EnableDisableAttributeName(!IsAlternativeForm);
        drpAttributeType.Enabled = !IsAlternativeForm && (!IsSystem || !IsSystemFieldSelected && DevelopmentMode);
        txtAttributeSize.Enabled = !IsAlternativeForm;
    }


    /// <summary>
    /// Used only for alternative forms. If current field in class allows empty then it returns TRUE.
    /// </summary>
    private bool GetFormAllowEmpty()
    {
        // Check if field exists in class
        FormInfo fi = FormHelper.GetFormInfo(this.ClassName, false);
        FormFieldInfo ffi = null;
        if (fi != null)
        {
            ffi = fi.GetFormField(this.ffi.Name);
            if (ffi != null)
            {
                return ffi.AllowEmpty;
            }
        }

        // Check if field exists in coupled class
        if (!String.IsNullOrEmpty(this.CoupledClassName))
        {
            fi = FormHelper.GetFormInfo(this.CoupledClassName, false);
            if (fi != null)
            {
                ffi = fi.GetFormField(this.ffi.Name);
                if (ffi != null)
                {
                    return ffi.AllowEmpty;
                }
            }
        }

        return false;
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Drop-down list event handler.
    /// </summary>
    protected void drpGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSystemAttributeName(drpGroup.SelectedValue, "");
        if (DropChanged != null)
        {
            DropChanged(this, EventArgs.Empty);
        }
    }


    /// <summary>
    ///  Drop-down system fields event handler.
    /// </summary>
    protected void drpSystemFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropChanged != null)
        {
            DropChanged(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Listbox attribute type event handler.
    /// </summary>
    protected void drpAttributeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AttributeChanged != null)
        {
            AttributeChanged(this, EventArgs.Empty);
        }
    }

    #endregion
}