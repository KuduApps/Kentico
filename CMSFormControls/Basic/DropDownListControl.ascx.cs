using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

using CMS.FormControls;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSFormControls_Basic_DropDownListControl : FormEngineUserControl
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
            return dropDownList.Enabled;
        }
        set
        {
            dropDownList.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (this.EditText)
            {
                return txtCombo.Text;
            }
            else
            {
                return dropDownList.SelectedValue;
            }
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

                if (this.EditText)
                {
                    txtCombo.Text = selectedValue;
                }
                else
                {
                    dropDownList.SelectedValue = selectedValue;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets selected value.
    /// </summary>
    public string SelectedValue
    {
        get
        {
            if (this.EditText)
            {
                return txtCombo.Text;
            }
            else
            {
                return dropDownList.SelectedValue;
            }
        }
        set
        {
            if (this.EditText)
            {
                txtCombo.Text = value;
            }
            else
            {
                dropDownList.SelectedValue = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets selected index. Returns -1 if no element is selected.
    /// </summary>
    public int SelectedIndex
    {
        get
        {
            if (this.EditText)
            {
                if (dropDownList.Items.FindByValue(txtCombo.Text) != null)
                {
                    return dropDownList.SelectedIndex;
                }
                return -1;
            }
            else
            {
                return dropDownList.SelectedIndex;
            }
        }
        set
        {
            dropDownList.SelectedIndex = value;
            if (this.EditText)
            {
                txtCombo.Text = dropDownList.SelectedValue;
            }
        }
    }


    /// <summary>
    /// Enables to edit text from textbox and select values from dropdownlist.
    /// </summary>
    public bool EditText
    {
        get;
        set;
    }


    /// <summary>
    /// Gets dropdown list control.
    /// </summary>
    public DropDownList DropDownList
    {
        get
        {
            return dropDownList;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAndSelectList();

        // Apply CSS class
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            dropDownList.CssClass = this.CssClass;
            CssClass = null;
        }
        else if (String.IsNullOrEmpty(dropDownList.CssClass))
        {
            dropDownList.CssClass = "DropDownField";
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            dropDownList.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckRegularExpression = true;
        this.CheckFieldEmptiness = true;

        if (this.EditText)
        {
            if (!RequestHelper.IsPostBack() && String.IsNullOrEmpty(txtCombo.Text))
            {
                txtCombo.Text = dropDownList.SelectedValue;
            }
            txtCombo.Visible = true;
            dropDownList.Attributes.Add("style", "display: none");
            ScriptHelper.RegisterJQueryUI(Page);
            ScriptHelper.RegisterScriptFile(Page, "jquery/jquery-combobox.js");
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "HideList_" + this.ClientID, ScriptHelper.GetScript(
@"jQuery(function() {
    jQuery(""#" + this.DropDownList.ClientID + @""").combobox();
	});"
));
        }
    }


    /// <summary>
    /// Loads and selects control.
    /// </summary>
    private void LoadAndSelectList()
    {
        if (dropDownList.Items.Count == 0)
        {
            string options = ValidationHelper.GetString(this.GetValue("options"), null);
            string query = ValidationHelper.GetString(this.GetValue("query"), null);

            try
            {
                FormHelper.LoadItemsIntoList(options, query, dropDownList.Items, this.FieldInfo);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }

            FormHelper.SelectSingleValue(selectedValue, dropDownList);
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
        dropDownList.Visible = false;
    }

    #endregion
}
