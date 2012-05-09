using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSFormControls_Basic_RadioButtonsControl : FormEngineUserControl
{
    #region "Variables"

    private string selectedValue = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return list.Enabled;
        }
        set
        {
            list.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return list.SelectedValue;
        }
        set
        {
            object convertedValue = value;
            if ((value != null) || ((this.FieldInfo != null) && this.FieldInfo.AllowEmpty))
            {
                if (this.FieldInfo != null)
                {
                    // Convert default boolean value to proper format
                    if (this.FieldInfo.DataType == FormFieldDataTypeEnum.Boolean)
                    {
                        convertedValue = ValidationHelper.GetBoolean(value, false);
                    }
                    // Ensure rendering of decimal in current culture format
                    else if (this.FieldInfo.DataType == FormFieldDataTypeEnum.Decimal)
                    {
                        convertedValue = FormHelper.GetDoubleValueInCurrentCulture(value);
                    }
                    // Ensure rendering of datetime in current culture format
                    else if (this.FieldInfo.DataType == FormFieldDataTypeEnum.DateTime)
                    {
                        convertedValue = FormHelper.GetDateTimeValueInCurrentCulture(value);
                    }
                }

                selectedValue = ValidationHelper.GetString(convertedValue, null);
                list.ClearSelection();
                if (list.Items.FindByValue(selectedValue) != null)
                {
                    list.SelectedValue = selectedValue;
                }
            }
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAndSelectList();

        // Apply styles
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            list.CssClass = this.CssClass;
            this.CssClass = null;
        }
        else if (String.IsNullOrEmpty(list.CssClass))
        {
            list.CssClass = "RadioButtonList";
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            list.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckRegularExpression = true;
        this.CheckFieldEmptiness = true;
    }


    /// <summary>
    /// Loads and selects control.
    /// </summary>
    private void LoadAndSelectList()
    {
        if (list.Items.Count == 0)
        {
            // Set control direction
            string direction = ValidationHelper.GetString(this.GetValue("repeatdirection"), "");
            if (direction.ToLower() == "horizontal")
            {
                list.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                list.RepeatDirection = RepeatDirection.Vertical;
            }

            string options = ValidationHelper.GetString(this.GetValue("options"), null);
            string query = ValidationHelper.GetString(this.GetValue("query"), null);

            try
            {
                FormHelper.LoadItemsIntoList(options, query, list.Items, this.FieldInfo);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }

            FormHelper.SelectSingleValue(selectedValue, list);
        }
    }


    /// <summary>
    /// Displays exception control with current error.
    /// </summary>
    /// <param name="ex">Thrown exception</param>
    private void DisplayException(Exception ex)
    {
        FormControlError ctrlError = new FormControlError();
        ctrlError.InnerException = ex;
        this.Controls.Add(ctrlError);
        list.Visible = false;
    }

    #endregion
}
