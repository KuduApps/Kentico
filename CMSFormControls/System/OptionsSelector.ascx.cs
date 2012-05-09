using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;

using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using System.Text.RegularExpressions;

/// <summary>
/// This form control must be used with name 'options' only. Another blank form control must be registered to hold second value. Second name must be 'query'.
/// </summary>
public partial class CMSFormControls_System_OptionsSelector : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return txtValue.Enabled;
        }
        set
        {
            txtValue.Enabled = value;
            radOptions.Enabled = value;
            radSQL.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value. Form control must be called options. Second blank form control must be created with name 'query'.
    /// </summary>
    public override object Value
    {
        get
        {
            if (radOptions.Checked)
            {
                return txtValue.Text.Trim();
            }
            return null;
        }
        set
        {
            txtValue.Text = LoadTextFromData(ValidationHelper.GetString(value, null));
        }
    }


    /// <summary>
    /// Returns values of other related fields.
    /// </summary>
    public override object[,] GetOtherValues()
    {
        object[,] values = new object[1, 2];
        values[0, 0] = "query";

        if (radOptions.Checked)
        {
            values[0, 1] = null;
        }
        else
        {
            values[0, 1] = txtValue.Text.Trim();
        }

        return values;
    }

    #endregion


    #region "Methods

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            this.CheckFieldEmptiness = false;
            // Clear control
            if ((this.Form == null) || (this.Form.Data == null))
            {
                radOptions.Checked = true;
                txtValue.Text = null;
            }
        }
        else
        {
            this.Visible = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (this.Form.FormType == FormTypeEnum.BizForm)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Form", "EditSQLQueries"))
            {
                radSQL.Enabled = false;
                if (radSQL.Checked)
                {
                    txtValue.Enabled = false;
                    radOptions.Attributes["onclick"] = "document.getElementById('" + txtValue.ClientID + "').disabled = false;";
                }
            }
        }
    }

    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        bool valid = true;

        // Check 'options' validity
        if (radOptions.Checked)
        {
            // Some option must be included
            if (string.IsNullOrEmpty(txtValue.Text.Trim()))
            {
                ValidationError += GetString("TemplateDesigner.ErrorDropDownListOptionsEmpty") + " ";
                valid = false;
            }
            else
            {
                // Parse lines
                int lineIndex = 0;
                string[] lines = txtValue.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string[] items = null;

                for (lineIndex = 0; lineIndex <= lines.GetUpperBound(0); lineIndex++)
                {
                    // Loop through only not-empty lines
                    if ((lines[lineIndex] != null) && (lines[lineIndex].Trim() != ""))
                    {
                        // Get line items
                        items = lines[lineIndex].Replace(FormHelper.SEMICOLON_TO_REPLACE, FormHelper.REPLACED_SEMICOLON).Split(';');

                        // Every line must have value and item element
                        if (items.Length != 2)
                        {
                            ValidationError += GetString("TemplateDesigner.ErrorDropDownListOptionsInvalidFormat") + " ";
                            valid = false;
                        }
                        else
                        {
                            // Check valid Double format
                            if (this.FieldInfo.DataType == FormFieldDataTypeEnum.Decimal)
                            {
                                if (!ValidationHelper.IsDouble(items[0]) && !(this.FieldInfo.AllowEmpty && string.IsNullOrEmpty(items[0])))
                                {
                                    ValidationError += string.Format(GetString("TemplateDesigner.ErrorDropDownListOptionsInvalidDoubleFormat"), lineIndex + 1) + " ";
                                    valid = false;
                                }
                            }
                            // Check valid Integer format
                            else if (this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer)
                            {
                                if (!ValidationHelper.IsInteger(items[0]) && !(this.FieldInfo.AllowEmpty && string.IsNullOrEmpty(items[0])))
                                {
                                    ValidationError += string.Format(GetString("TemplateDesigner.ErrorDropDownListOptionsInvalidIntFormat"), lineIndex + 1) + " ";
                                    valid = false;
                                }
                            }
                            // Check valid Long integer format
                            else if (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)
                            {
                                if (!ValidationHelper.IsLong(items[0]) && !(this.FieldInfo.AllowEmpty && string.IsNullOrEmpty(items[0])))
                                {
                                    ValidationError += string.Format(GetString("TemplateDesigner.ErrorDropDownListOptionsInvalidLongIntFormat"), lineIndex + 1) + " ";
                                    valid = false;
                                }
                            }
                            // Check valid Date time format
                            else if (this.FieldInfo.DataType == FormFieldDataTypeEnum.DateTime)
                            {
                                if ((ValidationHelper.GetDateTime(items[0], DateTimeHelper.ZERO_TIME) == DateTimeHelper.ZERO_TIME) && !this.FieldInfo.AllowEmpty)
                                {
                                    ValidationError += string.Format(GetString("TemplateDesigner.ErrorDropDownListOptionsInvalidDateTimeFormat"), lineIndex + 1) + " ";
                                    valid = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        // Check sql query validity
        else if (radSQL.Checked && (string.IsNullOrEmpty(txtValue.Text.Trim())))
        {
            ValidationError += GetString("TemplateDesigner.ErrorDropDownListQueryEmpty") + " ";
            valid = false;
        }

        if (!ContainsColumn("options"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "options", GetString("templatedesigner.fieldtypes.longtext"));
            valid = false;
        }

        if (!ContainsColumn("query"))
        {
            this.ValidationError += String.Format(GetString("formcontrol.missingcolumn"), "query", GetString("templatedesigner.fieldtypes.longtext"));
            valid = false;
        }

        return valid;
    }


    #endregion


    #region "Data loading methods"

    /// <summary>
    /// Loads text into textbox from value or from 'query' column.
    /// </summary>
    /// <param name="txtValue">Value parameter</param>
    /// <returns>Returns text of options or query</returns>
    private string LoadTextFromData(string txtValue)
    {
        // Options data
        if (!String.IsNullOrEmpty(txtValue))
        {
            return LoadTextFromOptions(txtValue);
        }
        // Query selected
        else if (ContainsColumn("query"))
        {
            return LoadTextFromQuery();
        }

        return null;
    }


    /// <summary>
    /// Load text from 'options' value
    /// </summary>
    /// <param name="txtValue">Raw text data</param>
    /// <returns>Returns parsed data</returns>
    private string LoadTextFromOptions(string txtValue)
    {
        // Initialize variables
        XmlDocument xmlDoc = new XmlDocument();
        MatchCollection foundLines = null;

        // Preselect radio buttons
        SetRadioButtons(true);

        // Try to load xml with option items
        if (TryLoadXmlOptions(txtValue, xmlDoc))
        {
            // Options selected
            return ParseXMLWithOptions(xmlDoc);
        }
        // Try to load XML as regular expression
        else if (TryLoadRegexOptions(txtValue, ref foundLines))
        {
            // Complete text from found groups 
            return GetFromRegex(foundLines);
        }

        // If loading XML failed then return text without parsing
        return txtValue;
    }


    /// <summary>
    /// Get lines from regular expresion.
    /// </summary>
    /// <param name="text">Text to be parsed</param>
    /// <param name="foundLines">Matched collection</param>
    /// <returns>Returns TRUE if regular expression found any matches</returns>
    private bool TryLoadRegexOptions(string text, ref MatchCollection foundLines)
    {
        Regex regex = RegexHelper.GetRegex(FormHelper.OPTIONS_REGEX);
        foundLines = regex.Matches(text);
        return foundLines.Count > 0;
    }


    /// <summary>
    /// Completes text from found regex groups.
    /// </summary>
    /// <param name="foundLines">Matched regex groups representing lines.</param>
    /// <returns>Returns completed text</returns>
    private string GetFromRegex(MatchCollection foundLines)
    {
        StringBuilder returnString = new StringBuilder();
        string dataField = null;
        string textField = null;

        foreach (Match match in foundLines)
        {
            dataField = ValidationHelper.GetString(match.Groups["value"], null).Replace(";", FormHelper.SEMICOLON_TO_REPLACE);
            textField = ValidationHelper.GetString(match.Groups["text"], null).Replace(";", FormHelper.SEMICOLON_TO_REPLACE);
            dataField = ConvertSpecialFormats(dataField);
            returnString.Append(dataField + ";" + textField + "\n");
        }

        return returnString.ToString();
    }

    /// <summary>
    /// Sets options/query radiobuttons
    /// </summary>
    /// <returns>Returns query text</returns>
    private string LoadTextFromQuery()
    {
        string query = ValidationHelper.GetString(this.Form.Data.GetValue("query"), "").Trim();
        if (!String.IsNullOrEmpty(query))
        {
            SetRadioButtons(false);
            return query;
        }
        return null;
    }


    /// <summary>
    /// Tries to load XML document with option items.
    /// </summary>
    /// <param name="txtValue">Text with option items</param>
    /// <param name="xmlDoc">XML document which will be loaded with items</param>
    /// <returns>Returns true if loading was successful</returns>
    private bool TryLoadXmlOptions(string txtValue, XmlDocument xmlDoc)
    {
        if (txtValue.StartsWith("<"))
        {
            try
            {
                xmlDoc.LoadXml("<options>" + txtValue + "</options>");
                if ((xmlDoc != null) && (xmlDoc.SelectNodes("options/item").Count > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Parse XML with options data.
    /// </summary>
    /// <param name="xmlDoc">XML document</param>
    /// <returns>Returns parsed text</returns>
    private string ParseXMLWithOptions(XmlDocument xmlDoc)
    {
        StringBuilder returnString = new StringBuilder();
        string dataField = null;
        string textField = null;

        // Loop through all items in XML
        foreach (XmlNode optionItem in xmlDoc.SelectNodes("options/item"))
        {
            dataField = optionItem.Attributes["value"].Value.Replace(";", FormHelper.SEMICOLON_TO_REPLACE);
            textField = optionItem.Attributes["text"].Value.Replace(";", FormHelper.SEMICOLON_TO_REPLACE);
            dataField = ConvertSpecialFormats(dataField);
            returnString.Append(dataField + ";" + textField + "\n");
        }

        return returnString.ToString();
    }


    /// <summary>
    /// Displays value in correct format for current culture
    /// </summary>
    /// <param name="value">Current value</param>
    /// <returns>Returns converted data</returns>
    private string ConvertSpecialFormats(string value)
    {
        // Convert decimal value
        if (this.FieldInfo.DataType == FormFieldDataTypeEnum.Decimal)
        {
            return FormHelper.GetDoubleValueInCurrentCulture(value);
        }
        // Covert date time value
        else if (this.FieldInfo.DataType == FormFieldDataTypeEnum.DateTime)
        {
            return FormHelper.GetDateTimeValueInCurrentCulture(value).ToString();
        }

        return value;
    }


    /// <summary>
    /// Sets radio buttons according to selected value options/query.
    /// </summary>
    /// <param name="options">Indicates if options radiobutton should be selected</param>
    private void SetRadioButtons(bool options)
    {
        radOptions.Checked = options;
        radSQL.Checked = !options;
    }

    #endregion
}