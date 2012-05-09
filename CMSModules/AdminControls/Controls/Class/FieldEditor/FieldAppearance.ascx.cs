using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_FieldAppearance : CMSUserControl
{
    #region "Events"

    /// <summary>
    /// Fired when DDL with form controls has been changed.
    /// </summary>
    public event EventHandler OnFieldSelected;

    #endregion


    #region "Variables"

    private FieldEditorControlsEnum mDisplayedControls = FieldEditorControlsEnum.ModeSelected;
    private FieldEditorModeEnum mMode;
    private string mClassName = null;
    private FormFieldInfo ffi = null;
    private bool mShowFieldVisibility = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Type of field.
    /// </summary>
    public string AttributeType
    {
        get
        {
            return ValidationHelper.GetString(ViewState["AttributeType"], null);
        }
        set
        {
            ViewState["AttributeType"] = value;
        }
    }


    /// <summary>
    /// Gets or sets selected control type.
    /// </summary>
    public string FieldType
    {
        get
        {
            return drpField.SelectedValue.ToLower();
        }
        set
        {
            if (!String.IsNullOrEmpty(value))
            {
                drpField.SelectedValue = value.ToLower();
            }
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
    /// Gets value from Field caption attribute.
    /// </summary>
    public string FieldCaption
    {
        get
        {
            return txtFieldCaption.Text.Trim();
        }
    }


    /// <summary>
    /// Gets or sets value indicating if user can change visibility of given field.
    /// </summary>
    public bool ChangeVisibility
    {
        get
        {
            return chkChangeVisibility.Checked;
        }
        set
        {
            chkChangeVisibility.Checked = value;
        }
    }


    /// <summary>
    /// Gets or sets value of visibility control.
    /// </summary>
    public string VisibilityValue
    {
        get
        {
            return ctrlVisibility.Value;
        }
        set
        {
            ctrlVisibility.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets value of DDL visibility section.
    /// </summary>
    public string VisibilityDDL
    {
        get
        {
            return drpVisibilityControl.SelectedValue;
        }
        set
        {
            drpVisibilityControl.SelectedValue = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if Public field checkbox is checked.
    /// </summary>
    public bool PublicField
    {
        get
        {
            return chkPublicField.Checked;
        }
        set
        {
            chkPublicField.Checked = value;
        }
    }


    /// <summary>
    /// Gets value which represents text field Description value.
    /// </summary>
    public string Description
    {
        get
        {
            return txtDescription.Text.Trim();
        }
    }


    /// <summary>
    /// Gets or sets field visibility.
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
    /// Gets value indicating if control has another depending controls.
    /// </summary>
    public bool HasDependingFields
    {
        get
        {
            return chkHasDepending.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if control is depending on another control.
    /// </summary>
    public bool DependsOnAnotherField
    {
        get
        {
            return chkDependsOn.Checked;
        }
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
    /// Indicates if Field Editor is used as alternative form.
    /// </summary>
    public bool IsAlternativeForm
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if the form is alternative filter form.
    /// </summary>
    public bool IsAlternativeFilterForm
    {
        get
        {
            if (IsAlternativeForm && !string.IsNullOrEmpty(AlternativeFormFullName))
            {
                return AlternativeFormFullName.ToLower().EndsWith(".filter");
            }

            return false;
        }
    }

    #endregion


    #region "Methods"

    public void Page_Load(object sender, EventArgs e)
    {
        pnlAppearance.GroupingText = GetString("templatedesigner.section.fieldappearance");

        // Public field option initialization    
        plcPublicField.Visible = (Mode == FieldEditorModeEnum.BizFormDefinition);
        drpTypeSelector.Changed += drpTypeSelector_Changed;
    }


    /// <summary>
    /// Loads field with values from FormFieldInfo.
    /// </summary>
    public void Reload()
    {
        if (ffi != null)
        {
            txtDescription.Text = ffi.Description;
            txtFieldCaption.Text = ffi.Caption;
            chkHasDepending.Checked = ffi.HasDependingFields;
            chkDependsOn.Checked = ffi.DependsOnAnotherField;

            // Field visibility
            if (ShowFieldVisibility)
            {
                chkChangeVisibility.Checked = ffi.AllowUserToChangeVisibility;
                ctrlVisibility.Value = ffi.Visibility;

                // Load controls for user visibility
                drpVisibilityControl.DataSource = FormUserControlInfoProvider.GetFormUserControls("UserControlCodeName, UserControlDisplayName", "UserControlForVisibility = 1", "UserControlDisplayName");
                drpVisibilityControl.DataBind();

                try
                {
                    ListItem item = drpVisibilityControl.Items.FindByValue(ffi.VisibilityControl);
                    drpVisibilityControl.SelectedValue = ffi.VisibilityControl;
                }
                catch
                {
                }
            }
            plcVisibility.Visible = ShowFieldVisibility;

            if (Mode == FieldEditorModeEnum.BizFormDefinition)
            {
                chkPublicField.Checked = ffi.PublicField;
            }

            string selectedItem = null;
            // Get control name from settings
            if (!String.IsNullOrEmpty(Convert.ToString(ffi.Settings["controlname"])))
            {
                selectedItem = Convert.ToString(ffi.Settings["controlname"]).ToLower();
            }
            // Or get control name from field type
            else
            {
                selectedItem = FormHelper.GetFormFieldControlTypeString(ffi.FieldType).ToLower();
            }

            FormUserControlInfo fi = FormUserControlInfoProvider.GetFormUserControlInfo(selectedItem);
            // Preselect options for specific control
            if (fi != null)
            {
                LoadControlTypes(false);
                LoadFieldTypes((ffi.PrimaryKey && !IsAlternativeFilterForm && !ffi.External), fi.UserControlType, false);
                SelectValue(selectedItem);
                SelectControlTypes(fi);
            }
            // Control not found, do not preselect
            else
            {
                LoadControlTypes(true);
                LoadFieldTypes(false, true);
                SelectValue(null);
            }
        }
        // If FormFieldInfo is not specified then clear form
        else
        {
            chkPublicField.Checked = true;
            ctrlVisibility.Value = null;
            drpVisibilityControl.ClearSelection();
            chkChangeVisibility.Checked = false;
            txtFieldCaption.Text = null;
            txtDescription.Text = null;
            chkHasDepending.Checked = false;
            chkDependsOn.Checked = false;

            // Set form control selectors
            LoadControlTypes(true);
            LoadFieldTypes(false, true);
            SelectValue(null);
        }
    }


    /// <summary>
    /// Fill field types list. Form control types will be restricted to actual selection in Form control types drop-down list.
    /// </summary>
    /// <param name="isPrimary">Determines whether the attribute is primary key</param>
    /// <param name="clearValue">Determines if selector should clear selected value</param>
    public void LoadFieldTypes(bool isPrimary, bool clearValue)
    {
        LoadFieldTypes(isPrimary, drpTypeSelector.ControlType, clearValue);
    }


    /// <summary>
    /// Gets available controls.
    /// </summary>
    /// <returns>Returns FieldEditorControlsEnum</returns>
    private FieldEditorControlsEnum GetControls()
    {
        FieldEditorControlsEnum controls = FieldEditorControlsEnum.None;

        // Get displayed controls
        if (this.DisplayedControls == FieldEditorControlsEnum.ModeSelected)
        {
            switch (mMode)
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
                case FieldEditorModeEnum.FormControls:
                    controls = FieldEditorControlsEnum.All;
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
    /// Fill field types list.
    /// </summary>
    /// <param name="isPrimary">Determines whether the attribute is primary key</param>
    /// <param name="type">Control type</param>
    public void LoadFieldTypes(bool isPrimary, FormUserControlTypeEnum type, bool clearValue)
    {
        FieldEditorControlsEnum controls = GetControls();
        string selectedValue = drpField.SelectedValue;

        // Clear list
        drpField.Items.Clear();
        drpField.ClearSelection();
        drpField.SelectedValue = null;
        drpField.DataTextField = "UserControlDisplayName";
        drpField.DataValueField = "UserControlCodeName";

        DataSet ds = FormHelper.GetFieldControlTypesWithUserControls(this.AttributeType, controls, false, isPrimary, type);

        if ((controls != FieldEditorControlsEnum.DocumentTypes) || (ClassName.ToLower() != "cms.blogpost"))
        {
            List<DataRow> rowList = new List<DataRow>();

            // Find trackback controls when not editing CMS.BlogPost document type
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string controlCodeName = row[drpField.DataValueField].ToString().ToLowerInvariant();
                if ((controlCodeName == "trackbackspingedurls") || (controlCodeName == "trackbacksnotpingedurls"))
                {
                    rowList.Add(row);
                }
            }

            // Remove trackback controls when not editing CMS.BlogPost document type
            foreach (DataRow row in rowList)
            {
                ds.Tables[0].Rows.Remove(row);
            }
        }

        drpField.DataSource = ds;
        drpField.DataBind();

        // Preselect value
        if ((drpField.Items.FindByValue(selectedValue) != null) && !clearValue)
        {
            drpField.SelectedValue = selectedValue;
        }
    }


    /// <summary>
    /// Selects field type from provided FormFieldInfo.
    /// </summary>
    private void SelectValue(string selectedItem)
    {
        if (drpField.Items.FindByValue(selectedItem) != null)
        {
            drpField.SelectedValue = selectedItem;
        }

        if (OnFieldSelected != null)
        {
            OnFieldSelected(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Reloads control types selector.
    /// </summary>
    public void LoadControlTypes(bool clearValue)
    {
        // Setup control for form control types
        string selectedValue = (string)drpTypeSelector.Value;
        if (ffi != null)
        {
            drpTypeSelector.IsPrimary = ffi.PrimaryKey;
            drpTypeSelector.External = ffi.External;
        }
        else
        {
            drpTypeSelector.IsPrimary = false;
        }

        drpTypeSelector.DataType = this.AttributeType;
        FieldEditorControlsEnum fieldEditorControls = GetControls();
        drpTypeSelector.FieldEditorControls = fieldEditorControls;

        if (IsAlternativeFilterForm && ((fieldEditorControls == FieldEditorControlsEnum.Bizforms) || (fieldEditorControls == FieldEditorControlsEnum.CustomTables)))
        {
            // Filter by ID for custom tables or bizforms filter form
            drpTypeSelector.IsPrimary = false;
        }

        drpTypeSelector.ReloadControl();

        // Preselect default value
        if (clearValue)
        {
            drpTypeSelector.Value = ((int)FormUserControlTypeEnum.Input).ToString();
        }
        // Keep selected value
        else
        {
            drpTypeSelector.Value = selectedValue;
        }
    }


    /// <summary>
    /// Selects control type according to current selected form control.
    /// </summary>
    private void SelectControlTypes(FormUserControlInfo fi)
    {
        if (fi != null)
        {
            drpTypeSelector.ControlType = fi.UserControlType;
        }
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
    /// Type selector changed.
    /// </summary>
    protected void drpTypeSelector_Changed(object sender, EventArgs e)
    {
        if (ffi != null)
        {
            LoadFieldTypes((ffi.PrimaryKey && !ffi.External), false);
        }
        else
        {
            LoadFieldTypes(false, false);
        }
        SelectValue(null);
    }


    #endregion
}