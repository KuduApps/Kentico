using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_ValidationSettings : CMSUserControl
{
    #region "Variables"

    private string mAttributeType = null;
    private string mFieldType = null;
    private FieldEditorModeEnum mMode = FieldEditorModeEnum.General;
    private FormFieldInfo ffi = null;
    private bool mIsPrimary = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Type of current attribute.
    /// </summary>
    public string AttributeType
    {
        get
        {
            if (!String.IsNullOrEmpty(mAttributeType))
            {
                return mAttributeType.ToLower();
            }
            return "";
        }
        set
        {
            mAttributeType = value;
        }
    }


    /// <summary>
    /// Type of current field.
    /// </summary>
    public string FieldType
    {
        get
        {
            if (!String.IsNullOrEmpty(mFieldType))
            {
                return mFieldType.ToLower();
            }
            return "";
        }
        set
        {
            mFieldType = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if current item is primary.
    /// </summary>
    public bool IsPrimary
    {
        get
        {
            return mIsPrimary;
        }
        set
        {
            mIsPrimary = value;
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
    /// Gets value set in Minimum length textbox.
    /// </summary>
    public string MinLengthText
    {
        get
        {
            return txtMinLength.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value set in Maximum length textbox.
    /// </summary>
    public string MaxLengthText
    {
        get
        {
            return txtMaxLength.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value set in Minimum value textbox.
    /// </summary>
    public string MinValueText
    {
        get
        {
            return txtMinValue.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value set in Maximum value textbox.
    /// </summary>
    public string MaxValueText
    {
        get
        {
            return txtMaxValue.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value indicating if Specll-check is checked.
    /// </summary>
    public bool SpellCheck
    {
        get
        {
            return chkSpellCheck.Checked;
        }
    }


    /// <summary>
    /// Gets value which is contained in Regular expression textbox.
    /// </summary>
    public string RegularExpression
    {
        get
        {
            return txtRegExpr.Text.Trim();
        }
    }


    /// <summary>
    /// Gets value which indicates which value is selected in 'Date from' control.
    /// </summary>
    public DateTime DateFrom
    {
        get
        {
            return dateFrom.SelectedDateTime;
        }
    }


    /// <summary>
    /// Gets value which indicates which value is selected in 'Date to' control .
    /// </summary>
    public DateTime DateTo
    {
        get
        {
            return dateTo.SelectedDateTime;
        }
    }


    /// <summary>
    /// Gets value which is contained in Error message textbox.
    /// </summary>
    public string ErrorMessage
    {
        get
        {
            return txtErrorMessage.Text.Trim();
        }
    }

    #endregion


    #region "Methods"

    public void Page_Load(object sender, EventArgs e)
    {
        pnlSectionValidation.GroupingText = GetString("TemplateDesigner.Section.Validation");
    }


    /// <summary>
    /// Show validation options according to selected attribute type.
    /// </summary>
    public void Reload()
    {
        if (ffi != null)
        {
            chkSpellCheck.Checked = ffi.SpellCheck;
            if (pnlSectionValidation.Visible)
            {
                // Validation section
                txtRegExpr.Text = ffi.RegularExpression;
                switch (ffi.DataType)
                {
                    case FormFieldDataTypeEnum.Integer:
                        txtMinValue.Text = ValidationHelper.IsInteger(ffi.MinValue) ? ffi.MinValue : "";
                        txtMaxValue.Text = ValidationHelper.IsInteger(ffi.MaxValue) ? ffi.MaxValue : "";
                        break;

                    case FormFieldDataTypeEnum.LongInteger:
                        txtMinValue.Text = ValidationHelper.IsLong(ffi.MinValue) ? ffi.MinValue : "";
                        txtMaxValue.Text = ValidationHelper.IsLong(ffi.MaxValue) ? ffi.MaxValue : "";
                        break;

                    case FormFieldDataTypeEnum.Decimal:
                        txtMinValue.Text = ValidationHelper.IsDouble(ffi.MinValue) ? ffi.MinValue : "";
                        txtMaxValue.Text = ValidationHelper.IsDouble(ffi.MaxValue) ? ffi.MaxValue : "";
                        break;

                }

                txtMinLength.Text = (ffi.MinStringLength > -1) ? Convert.ToString(ffi.MinStringLength) : "";
                txtMaxLength.Text = (ffi.MaxStringLength > -1) ? Convert.ToString(ffi.MaxStringLength) : "";

                dateFrom.SelectedDateTime = ffi.MinDateTimeValue;
                dateTo.SelectedDateTime = ffi.MaxDateTimeValue;
                txtErrorMessage.Text = ffi.ValidationErrorMessage;
            }
        }
        else
        {
            chkSpellCheck.Checked = true;
            txtRegExpr.Text = null;
            txtMinLength.Text = null;
            txtMaxLength.Text = null;
            txtMinValue.Text = null;
            txtMaxValue.Text = null;
            dateFrom.SelectedDateTime = DateTimePicker.NOT_SELECTED;
            dateTo.SelectedDateTime = DateTimePicker.NOT_SELECTED;
            txtErrorMessage.Text = null;
        }
    }


    /// <summary>
    /// Displays controls according to current field settings.
    /// </summary>
    public void DisplayControls()
    {
        this.Visible = true;
        this.plcMinMaxLengthValidation.Visible = true;

        if (!this.IsPrimary)
        {
            switch (this.AttributeType)
            {
                case "text":
                case "longtext":
                    pnlSectionValidation.Visible = true;
                    plcTextValidation.Visible = true;
                    plcErrorMessageValidation.Visible = true;
                    if (this.Mode == FieldEditorModeEnum.ClassFormDefinition)
                    {
                        plcSpellCheck.Visible = true;
                    }
                    plcDateTimeValidation.Visible = false;
                    plcNumberValidation.Visible = false;
                    this.Visible = true;
                    break;

                case "datetime":
                    pnlSectionValidation.Visible = true;
                    plcDateTimeValidation.Visible = true;
                    plcErrorMessageValidation.Visible = true;
                    plcTextValidation.Visible = false;
                    plcNumberValidation.Visible = false;
                    plcSpellCheck.Visible = false;
                    this.Visible = true;
                    break;

                case "integer":
                case "longinteger":
                case "double":
                    pnlSectionValidation.Visible = true;
                    plcNumberValidation.Visible = true;
                    plcErrorMessageValidation.Visible = true;
                    plcTextValidation.Visible = false;
                    plcDateTimeValidation.Visible = false;
                    plcSpellCheck.Visible = false;
                    this.Visible = true;
                    break;

                default:
                    this.Visible = false;
                    break;
            }
        }
        else
        {
            this.Visible = false;
        }

        // Hide Min max length for selection controls
        if ((this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.DropDownListControl).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.RadioButtonsControl).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.MultipleChoiceControl).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.ListBoxControl).ToLower()))
        {
            plcMinMaxLengthValidation.Visible = false;
        }
        // Hide the whole validation section 
        else if ((this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.ImageSelectionControl).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.FileSelectionControl).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.MediaSelectionControl).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.SelectColumns).ToLower()) ||
        (this.FieldType.ToLower() == Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.EncryptedPassword).ToLower()))
        {
            pnlSectionValidation.Visible = false;
        }

        if (!plcTextValidation.Visible && !plcErrorMessageValidation.Visible && !plcDateTimeValidation.Visible && !plcNumberValidation.Visible && !plcSpellCheck.Visible)
        {
            this.Visible = false;
        }
    }

    #endregion
}