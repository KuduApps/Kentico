using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.FormControls;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_SimpleMode : CMSUserControl
{
    #region "Events"

    /// <summary>
    /// Fired when DDL with form controls has been changed.
    /// </summary>
    public event EventHandler OnFieldSelected;


    /// <summary>
    /// Occurs when FormFieldInfo is requested from control.
    /// </summary>
    public event EventHandler OnGetFieldInfo;

    #endregion


    #region "Variables"

    private FieldEditorSelectedItemEnum mSelectedItemType = FieldEditorSelectedItemEnum.Field;


    private FieldEditorControlsEnum mDisplayedControls = FieldEditorControlsEnum.ModeSelected;


    private string mClassName = string.Empty;


    private FieldEditorModeEnum mMode;


    private FormInfo fi = null;


    private FormFieldInfo mFieldInfo = null;


    private const string controlPrefix = "#uc#";


    private static Hashtable mSettings = null;


    private bool mDevelopmentMode = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Selected item type.
    /// </summary>
    public FieldEditorSelectedItemEnum SelectedItemType
    {
        get
        {
            return mSelectedItemType;
        }
        set
        {
            mSelectedItemType = value;
        }
    }


    /// <summary>
    /// Shows in what control is this form used.
    /// </summary>
    public FormTypeEnum FormType
    {
        get
        {
            return form.FormType;
        }
        set
        {
            form.FormType = value;
        }
    }


    /// <summary>
    /// Indicates whether new item is edited.
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
        }
    }


    /// <summary>
    /// Form field info of current field.
    /// </summary>
    public FormFieldInfo FieldInfo
    {
        get
        {
            return mFieldInfo;
        }
        set
        {
            mFieldInfo = value;
        }
    }


    /// <summary>
    /// Field settings hashtable.
    /// </summary>
    public Hashtable Settings
    {
        get
        {
            return mSettings;
        }
        set
        {
            mSettings = new Hashtable(value, StringComparer.InvariantCultureIgnoreCase);
        }
    }


    /// <summary>
    /// Gets or sets selected control type.
    /// </summary>
    public string FieldType
    {
        get
        {
            return drpFieldType.SelectedValue.ToLower();
        }
        set
        {
            if (!String.IsNullOrEmpty(value))
            {
                drpFieldType.SelectedValue = value.ToLower();
            }
        }
    }


    /// <summary>
    /// Gets attribute name.
    /// </summary>
    public string AttributeName
    {
        get
        {
            return txtColumnName.Text.Trim();
        }
    }


    /// <summary>
    /// Gets field caption.
    /// </summary>
    public string FieldCaption
    {
        get
        {
            return txtFieldCaption.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value indicating if field allows empty values.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return chkAllowEmpty.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if field is system field.
    /// </summary>
    public bool IsSystem
    {
        get
        {
            return chkIsSystem.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if field is marked as public.
    /// </summary>
    public bool PublicField
    {
        get
        {
            return chkPublicField.Checked;
        }
    }


    /// <summary>
    /// BasicFrom data.
    /// </summary>
    public DataRow FormData
    {
        get
        {
            return form.DataRow;
        }
    }


    /// <summary>
    /// Attribute size.
    /// </summary>
    public int AttributeSize
    {
        get
        {
            return ValidationHelper.GetInteger(txtSimpleTextBoxMaxLength.Text.Trim(), 0);
        }
    }


    /// <summary>
    /// Indicates if field editor is in development mode.
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
    /// Gets field data type.
    /// </summary>
    public string AttributeType
    {
        get
        {
            string selectedType = null;

            // Try to load FormFieldInfo from parent
            if (OnGetFieldInfo != null)
            {
                OnGetFieldInfo(this, EventArgs.Empty);
            }

            // Get data type from FormFieldInfo if not NULL
            if (mFieldInfo != null)
            {
                selectedType = FormHelper.GetFormFieldDataTypeString(mFieldInfo.DataType);
            }
            // Get default data type for selected control
            else
            {
                selectedType = FormUserControlInfoProvider.GetUserControlDefaultDataType(this.FieldType)[0];
            }

            // For special cases change data type
            if (((Mode == FieldEditorModeEnum.BizFormDefinition) || (Mode == FieldEditorModeEnum.SystemTable))
                        && (FormHelper.IsFieldOfType(mFieldInfo, FormFieldControlTypeEnum.UploadControl) || (FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.UploadControl).ToLower())))
            {
                selectedType = FormFieldDataTypeCode.FILE;
            }

            return selectedType;
        }
    }

    #endregion


    #region "Methods"

    public void Page_Load(object sender, EventArgs e)
    {
        drpControlType.Changed += drpControlType_Changed;
        drpFieldType.SelectedIndexChanged += drpFieldType_SelectedIndexChanged;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        pnlSimple.GroupingText = GetString("templatedesigner.simplemode");
        pnlSettings.GroupingText = GetString("fieldeditor.fieldsettings");

        // Display or hide control settings
        pnlSettings.Visible = !((form.DataRow == null) || (form.DataRow.ItemArray.Length == 0) || (this.fi == null));

        // Display maximum length textbox according to current field type
        plcSimpleTextBox.Visible = (this.AttributeType == FormFieldDataTypeCode.TEXT);

        // Public field option initialization    
        pnlPublicField.Visible = (Mode == FieldEditorModeEnum.BizFormDefinition);

        // Display or hide 'is system' option
        plcIsSystem.Visible = this.DevelopmentMode;

        // Enable or disable some controls in simplified mode while editing existing field
        if (SelectedItemType == FieldEditorSelectedItemEnum.Field)
        {
            // Editing new or existing item
            txtColumnName.Enabled = IsNewItemEdited;
            txtColumnName.ReadOnly = !IsNewItemEdited;
            drpControlType.Enabled = IsNewItemEdited;
            drpFieldType.Enabled = IsNewItemEdited;
        }
    }


    /// <summary>
    /// Loads control with new FormInfo data.
    /// </summary>
    /// <param name="type">Selected control type</param>
    /// <param name="defaultValues">Indicates if default values should be loaded</param>
    public void LoadControlSettings(string type, bool defaultValues)
    {
        string selectedFieldType = type;
        if (String.IsNullOrEmpty(type))
        {
            selectedFieldType = drpFieldType.SelectedValue.ToLower();
        }
        if (selectedFieldType.StartsWith(controlPrefix))
        {
            selectedFieldType = selectedFieldType.Substring(controlPrefix.Length);
        }

        FormUserControlInfo control = FormUserControlInfoProvider.GetFormUserControlInfo(selectedFieldType);
        if (control != null)
        {
            // Get form controls' settings
            fi = FormHelper.GetFormControlParameters(selectedFieldType, control.UserControlParameters, true);

            // Simplify the settings - remove advanced ones
            SimplifyControlSettings(control.UserControlCodeName);

            Reload(defaultValues);
        }
    }


    /// <summary>
    /// Reload control.
    /// </summary>
    /// <param name="defaultValues">Indicates if default values should be loaded</param>
    public void Reload(bool defaultValues)
    {
        // Prepare basic form
        if (fi != null)
        {
            form.SubmitButton.Visible = false;
            form.SiteName = CMSContext.CurrentSiteName;
            form.FormInformation = fi;
            form.Data = GetData();
            form.ShowPrivateFields = true;
        }
        else
        {
            form.DataRow = null;
        }
        form.ReloadData();
        form.FormType = CMS.FormControls.FormTypeEnum.BizForm;

        // Display or hide controls for default values
        DisplayDefaultControls();

        // Load controls with values
        if (this.FieldInfo != null)
        {
            txtColumnName.Text = this.FieldInfo.Name;
            chkPublicField.Checked = this.FieldInfo.PublicField;
            txtFieldCaption.Text = this.FieldInfo.Caption;
            chkAllowEmpty.Checked = this.FieldInfo.AllowEmpty;
            chkIsSystem.Checked = this.FieldInfo.System;
            txtSimpleTextBoxMaxLength.Text = ValidationHelper.GetString(this.FieldInfo.Size, null);
        }
        // Get default maximum length for text fields
        else if (defaultValues)
        {
            txtSimpleTextBoxMaxLength.Text = FormUserControlInfoProvider.GetUserControlDefaultDataType(this.FieldType)[1];
        }

        // Primary key not allowed to change
        // System field not allowed to change unless development mode
        if ((this.FieldInfo != null) && ((this.FieldInfo.PrimaryKey) || (this.FieldInfo.System && !DevelopmentMode)))
        {
            DisableFieldEditing();
        }
        // Enable to change settings for new items and non-primary items
        else
        {
            EnableFieldEditing();
        }
    }


    /// <summary>
    /// Clears simple form.
    /// </summary>
    public void ClearForm()
    {
        txtColumnName.Text = null;
        chkPublicField.Checked = true;
        txtFieldCaption.Text = null;
        chkAllowEmpty.Checked = false;
        chkIsSystem.Checked = false;
        txtSimpleTextBoxMaxLength.Text = null;
        txtDefaultValue.Text = null;
        chkDefaultValue.Checked = false;
        txtLargeDefaultValue.Value = null;
        datetimeDefaultValue.SelectedDateTime = DateTime.MinValue;
        drpControlType.ClearSelection();
        drpFieldType.Items.Clear();
        LoadFieldTypes(FormUserControlTypeEnum.Unspecified);
    }


    /// <summary>
    /// Save data from Basic form.
    /// </summary>
    public bool SaveData()
    {
        if (form.Visible)
        {
            return form.SaveData(null);
        }
        return true;
    }


    /// <summary>
    /// Loads both field types and control types from inner settings.
    /// </summary>
    public void LoadTypes()
    {
        if (FieldInfo != null)
        {
            drpControlType.DataType = FormFieldDataTypeCode.ALL;
            drpControlType.FieldEditorControls = GetDisplayedControls();
            drpControlType.ReloadControl();

            // Get control name..
            string item = null;
            // ..from settings..
            if (!string.IsNullOrEmpty(ValidationHelper.GetString(FieldInfo.Settings["controlname"], null)))
            {
                item = Convert.ToString(FieldInfo.Settings["controlname"]).ToLower();
            }
            // ..or from field type.
            else
            {
                item = FormHelper.GetFormFieldControlTypeString(FieldInfo.FieldType).ToLower();
            }

            // Load and select options
            FormUserControlInfo fi = FormUserControlInfoProvider.GetFormUserControlInfo(item);
            if (fi != null)
            {
                drpControlType.ControlType = fi.UserControlType;
                LoadFieldTypes(drpControlType.ControlType);

                // And set field value
                if (drpFieldType.Items.FindByValue(item) != null)
                {
                    drpFieldType.SelectedValue = item;
                }
            }
        }
    }


    /// <summary>
    /// Loads field types.
    /// </summary>
    /// <param name="type">Control type specifying which field types to load</param>
    public void LoadFieldTypes(FormUserControlTypeEnum type)
    {
        string selectedValue = drpFieldType.SelectedValue;
        FieldEditorControlsEnum controls = GetDisplayedControls();

        // Clear list
        bool isPrimary = false;
        if (this.FieldInfo != null)
        {
            isPrimary = this.FieldInfo.PrimaryKey;
        }
        drpFieldType.Items.Clear();
        drpFieldType.DataTextField = "UserControlDisplayName";
        drpFieldType.DataValueField = "UserControlCodeName";
        drpFieldType.DataSource = FormHelper.GetFieldControlTypesWithUserControls(null, controls, true, isPrimary, type);
        drpFieldType.DataBind();

        // Add trackback controls when editing CMS.BlogPost document type
        if ((controls == FieldEditorControlsEnum.DocumentTypes) && (ClassName.ToLower() == "cms.blogpost"))
        {
            drpFieldType.Items.Add(new ListItem("Trackbacks - pinged URLs", "#uc#trackbackspingedurls"));
            drpFieldType.Items.Add(new ListItem("Trackbacks - not pinged URLs", "#uc#trackbacksnotpingedurls"));
        }

        // And set field value (if set before)
        if (drpFieldType.Items.FindByValue(selectedValue) != null)
        {
            drpFieldType.SelectedValue = selectedValue;
        }
    }


    /// <summary>
    /// Returns control default value based on control data type.
    /// </summary>
    /// <param name="ffi">FormFieldInfo for existing field. Can be NULL for newly created fields</param>
    /// <returns>Returns default value.</returns>
    public string GetDefaultValue(FormFieldInfo ffi)
    {
        // Return default value based on FFI settings
        if (ffi != null)
        {
            string fieldDataType = FormHelper.GetFormFieldDataTypeString(ffi.DataType);

            switch (fieldDataType)
            {
                case FormFieldDataTypeCode.BOOLEAN:
                    return Convert.ToString(chkDefaultValue.Checked).ToLower();

                case FormFieldDataTypeCode.DATETIME:
                    return GetDateTimeDefaultValue();

                case FormFieldDataTypeCode.LONGTEXT:
                    return txtLargeDefaultValue.Value.ToString().Trim();

                default:
                    return txtDefaultValue.Text.Trim();

            }
        }
        // Return default value based on currently selected field type
        else
        {
            string defaultValue = null;

            // DateTime based control
            if (datetimeDefaultValue.Visible)
            {
                string datetimevalue = datetimeDefaultValue.DateTimeTextBox.Text.Trim();
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
                else if (datetimeDefaultValue.SelectedDateTime == DateTimePicker.NOT_SELECTED)
                {
                    defaultValue = "";
                }
                else
                {
                    defaultValue = datetimeDefaultValue.SelectedDateTime.ToString();
                }
            }
            // Bool based control
            else if (chkDefaultValue.Visible)
            {
                defaultValue = Convert.ToString(chkDefaultValue.Checked).ToLower();
            }
            // Long text based control
            else if (txtLargeDefaultValue.Visible)
            {
                defaultValue = txtLargeDefaultValue.Value.ToString().Trim();
            }
            // Other types of controls
            else
            {
                defaultValue = txtDefaultValue.Text.Trim();
            }

            return defaultValue;
        }
    }


    /// <summary>
    /// Disables field editing controls.
    /// </summary>
    public void DisableFieldEditing()
    {
        txtDefaultValue.Enabled = false;
        txtLargeDefaultValue.Enabled = false;
        chkDefaultValue.Enabled = false;
        datetimeDefaultValue.Enabled = false;
        chkAllowEmpty.Enabled = false;
        chkIsSystem.Enabled = false;
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
        chkAllowEmpty.Enabled = (this.Mode != FieldEditorModeEnum.SystemTable);
        chkIsSystem.Enabled = true;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Hides or displays controls which are needed for default values.
    /// </summary>
    private void DisplayDefaultControls()
    {
        string fieldDataType = null;

        // Load control with values from FormFieldInfo
        if (this.FieldInfo != null)
        {
            fieldDataType = FormHelper.GetFormFieldDataTypeString(this.FieldInfo.DataType);
        }
        else
        {
            fieldDataType = FormUserControlInfoProvider.GetUserControlDefaultDataType(drpFieldType.SelectedValue)[0];
        }

        // UserControl with boolean datatype
        if (fieldDataType == FormFieldDataTypeCode.BOOLEAN)
        {
            if (this.FieldInfo != null)
            {
                chkDefaultValue.Checked = ValidationHelper.GetBoolean(this.FieldInfo.DefaultValue, false);
            }
            chkDefaultValue.Visible = true;
            txtDefaultValue.Visible = false;
            txtLargeDefaultValue.Visible = false;
            datetimeDefaultValue.Visible = false;
        }
        // UserControl with datetime datatype
        else if (fieldDataType == FormFieldDataTypeCode.DATETIME)
        {
            if (FieldInfo != null)
            {
                if (string.IsNullOrEmpty(this.FieldInfo.DefaultValue))
                {
                    datetimeDefaultValue.DateTimeTextBox.Text = String.Empty;
                }
                else if (FieldInfo.DefaultValue.ToLower() == DateTimePicker.DATE_TODAY.ToLower())
                {
                    datetimeDefaultValue.DateTimeTextBox.Text = DateTimePicker.DATE_TODAY;
                }
                else if (FieldInfo.DefaultValue.ToLower() == DateTimePicker.TIME_NOW.ToLower())
                {
                    datetimeDefaultValue.DateTimeTextBox.Text = DateTimePicker.TIME_NOW;
                }
                else if (ValidationHelper.IsMacro(FieldInfo.DefaultValue))
                {
                    datetimeDefaultValue.DateTimeTextBox.Text = FieldInfo.DefaultValue;
                }
                else
                {
                    datetimeDefaultValue.SelectedDateTime = FormHelper.GetDateTimeValueInCurrentCulture(FieldInfo.DefaultValue);
                }
            }
            chkDefaultValue.Visible = false;
            txtDefaultValue.Visible = false;
            txtLargeDefaultValue.Visible = false;
            datetimeDefaultValue.Visible = true;
        }
        // UserControl with longtext datatype
        else if (fieldDataType == FormFieldDataTypeCode.LONGTEXT)
        {
            if (FieldInfo != null)
            {
                txtLargeDefaultValue.Value = FieldInfo.DefaultValue;
            }
            chkDefaultValue.Visible = false;
            txtDefaultValue.Visible = false;
            txtLargeDefaultValue.Visible = true;
            datetimeDefaultValue.Visible = false;
        }
        // UserControl with text datatype
        else
        {
            if (FieldInfo != null)
            {
                txtDefaultValue.Text = FieldInfo.DefaultValue;
            }
            chkDefaultValue.Visible = false;
            txtDefaultValue.Visible = true;
            txtLargeDefaultValue.Visible = false;
            datetimeDefaultValue.Visible = false;
        }
    }


    /// <summary>
    /// Loads DataRow for BasicForm with data from FormFieldInfo settings.
    /// </summary>
    private DataRowContainer GetData()
    {
        DataRowContainer result = new DataRowContainer(fi.GetDataRow());

        if (this.Settings != null)
        {
            foreach (string columnName in result.ColumnNames)
            {
                if (Settings.ContainsKey(columnName) && !String.IsNullOrEmpty(Convert.ToString(Settings[columnName])))
                {
                    result[columnName] = Settings[columnName];
                }
            }
        }
        return result;
    }


    /// <summary>
    /// Returns default date time value.
    /// </summary>
    private string GetDateTimeDefaultValue()
    {
        string datetimevalue = datetimeDefaultValue.DateTimeTextBox.Text.Trim();

        // Get today's date
        if (datetimevalue.ToLower() == DateTimePicker.DATE_TODAY.ToLower())
        {
            datetimevalue = DateTimePicker.DATE_TODAY;
        }
        // Get time 
        else if (datetimevalue.ToLower() == DateTimePicker.TIME_NOW.ToLower())
        {
            datetimevalue = DateTimePicker.TIME_NOW;
        }
        // Macro
        else if (ValidationHelper.IsMacro(datetimevalue))
        {
            // Use current value
        }
        // No date time
        else if (datetimeDefaultValue.SelectedDateTime == DateTimePicker.NOT_SELECTED)
        {
            datetimevalue = "";
        }
        // Selected value
        else
        {
            datetimevalue = datetimeDefaultValue.SelectedDateTime.ToString();
        }
        return datetimevalue;
    }


    /// <summary>
    /// Gets displayed controls.
    /// </summary>
    private FieldEditorControlsEnum GetDisplayedControls()
    {
        FieldEditorControlsEnum controls;
        if (this.DisplayedControls == FieldEditorControlsEnum.ModeSelected)
        {
            switch (this.Mode)
            {
                case FieldEditorModeEnum.BizFormDefinition:
                    controls = FieldEditorControlsEnum.Bizforms;
                    break;

                case FieldEditorModeEnum.ClassFormDefinition:
                    controls = FieldEditorControlsEnum.DocumentTypes;
                    break;

                case FieldEditorModeEnum.SystemTable:
                    controls = FieldEditorControlsEnum.SystemTables;
                    break;

                case FieldEditorModeEnum.CustomTable:
                    controls = FieldEditorControlsEnum.CustomTables;
                    break;

                case FieldEditorModeEnum.WebPartProperties:
                    controls = FieldEditorControlsEnum.Controls;
                    break;

                case FieldEditorModeEnum.General:
                    controls = FieldEditorControlsEnum.All;
                    break;

                default:
                    controls = FieldEditorControlsEnum.None;
                    break;
            }
        }
        else
        {
            controls = this.DisplayedControls;
        }

        return controls;
    }


    /// <summary>
    /// Field type changed event handler.
    /// </summary>
    protected void drpFieldType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnFieldSelected != null)
        {
            OnFieldSelected(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Field type changed event handler.
    /// </summary>
    protected void drpControlType_Changed(object sender, EventArgs e)
    {
        LoadFieldTypes(drpControlType.ControlType);
        if (OnFieldSelected != null)
        {
            OnFieldSelected(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Simplify form control settings in simple mode.
    /// </summary>
    /// <param name="controlName">Form control name</param>
    private void SimplifyControlSettings(string controlName)
    {
        if ((fi != null) && (fi.ItemsList.Count > 0))
        {
            switch (controlName.ToLower())
            {
                // Leave all settings for the following controls
                case "htmlareacontrol":
                case "dropdownlistcontrol":
                case "listboxcontrol":
                case "multiplechoicecontrol":
                case "radiobuttonscontrol":
                case "uploadcontrol":
                    break;

                // BBEditor, Text area - leave 'Cols', 'Rows' and 'Size'
                case "bbeditorcontrol":
                case "textareacontrol":
                    PreserveSettings("cols;rows;size");
                    break;

                // Calendar - leave 'EditTime' and 'DisplayNow'
                case "calendarcontrol":
                    PreserveSettings("edittime;displaynow");
                    break;

                // Image selection - leave 'Width', 'Height' and 'MaxSideSize'
                case "imageselectioncontrol":
                    PreserveSettings("width;height;maxsidesize");
                    break;

                // Numeric Up/Down - leave DataSource
                case "numericupdown":
                    PreserveSettings("options;query;width");
                    break;

                // Slider - leave 'Minimum' and 'Maximum'
                case "slider":
                    PreserveSettings("minimum;maximum;steps;length;showlabel");
                    break;

                default:
                    fi.ItemsList.Clear();
                    break;
            }
        }
    }


    /// <summary>
    /// Preserves settings according to given list, other settings are removed.
    /// </summary>
    /// <param name="settings">Settings that should be preserved, separated by semicolon</param>
    private void PreserveSettings(string settings)
    {
        if (string.IsNullOrEmpty(settings))
        {
            return;
        }
        else
        {
            // Add border semicolons
            settings = string.Format(";{0};", settings.ToLower());
        }

        FormFieldInfo ffi = null;
        List<FormItem> items2Remove = new List<FormItem>();

        foreach (FormItem item in fi.ItemsList)
        {
            if (item is FormFieldInfo)
            {
                ffi = (FormFieldInfo)item;

                if (!settings.Contains(string.Format(";{0};", ffi.Name.ToLower())))
                {
                    // Remove fields that should not be preserved
                    items2Remove.Add(item);
                }
            }
            else
            {
                // Remove categories
                items2Remove.Add(item);
            }
        }

        foreach (FormItem item in items2Remove)
        {
            fi.ItemsList.Remove(item);
        }
    }

    #endregion
}