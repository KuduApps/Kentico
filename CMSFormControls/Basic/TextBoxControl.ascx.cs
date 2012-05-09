using System;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.PortalControls;
using CMS.CMSHelper;

using AjaxControlToolkit;

public partial class CMSFormControls_Basic_TextBoxControl : FormEngineUserControl
{
    #region "Constants"

    protected const int FILTER_NUMBERS = 0;
    protected const int FILTER_LOWERCASE = 1;
    protected const int FILTER_UPPERCASE = 2;
    protected const int FILTER_CUSTOM = 3;

    #endregion


    #region "Variables"

    private FilterModes mFilterMode = FilterModes.ValidChars;

    #endregion


    #region "Properties"

    /// <summary>
    /// Maximum text length
    /// </summary>
    public int MaxLength
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("size"), 0);
        }
        set
        {
            SetValue("size", value);
            textbox.MaxLength = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return textbox.Enabled;
        }
        set
        {
            textbox.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (Trim)
            {
                return textbox.Text.Trim();
            }
            else
            {
                return textbox.Text;
            }
        }
        set
        {
            // Load default value on insert
            if ((FieldInfo != null) && (FieldInfo.DataType == FormFieldDataTypeEnum.Decimal) && !IsTextArea)
            {
                textbox.Text = FormHelper.GetDoubleValueInCurrentCulture(value);
            }
            else
            {
                textbox.Text = ValidationHelper.GetString(value, null);
            }
        }
    }


    /// <summary>
    /// Gets (or sets) the value indicating if form control is displayed as TextArea control.
    /// If FALSE then form control is displayed as TextBox control. Setting this value
    /// is performed only if FieldInfo is null.
    /// </summary>
    public bool IsTextArea
    {
        get
        {
            if (FormHelper.IsFieldOfType(FieldInfo, FormFieldControlTypeEnum.TextAreaControl))
            {
                return true;
            }

            return ValidationHelper.GetBoolean(GetValue("IsTextArea"), false);
        }
        set
        {
            SetValue("IsTextArea", value);
        }
    }

    #endregion


    #region "Watermark properties"

    /// <summary>
    /// The text to show when the control has no value.
    /// </summary>
    public string WatermarkText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WatermarkText"), null);
        }
        set
        {
            SetValue("WatermarkText", value);
        }
    }


    /// <summary>
    /// The CSS class to apply to the TextBox when it has no value (e.g. the watermark text is shown).
    /// </summary>
    public string WatermarkCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WatermarkCssClass"), "WatermarkText");
        }
        set
        {
            SetValue("WatermarkCssClass", value);
        }
    }

    #endregion


    #region "Filter properties"

    /// <summary>
    /// Indicates if filter is enabled.
    /// </summary>
    public bool FilterEnabled
    {
        get
        {
            // For custom filter type the valid characters or invalid charactes have to set
            if ((FilterTypeValue == FILTER_CUSTOM.ToString()))
            {
                if (FilterMode == FilterModes.ValidChars)
                {
                    return !String.IsNullOrEmpty(ValidChars);
                }
                else
                {
                    return !String.IsNullOrEmpty(InvalidChars);
                }
            }

            // Get the filter type from field settings and from control settings
            return ((FilterTypeValue != null) || (FilterType != 0));
        }
    }


    /// <summary>
    /// A string consisting of all characters considered valid for the text field, if "Custom" is specified as the filter type. Otherwise this parameter is ignored.
    /// </summary>
    public string ValidChars
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ValidChars"), null);
        }
        set
        {
            SetValue("ValidChars", value);
        }
    }


    /// <summary>
    /// A string consisting of all characters considered invalid for the text field, if "Custom" is specified as the filter type and "InvalidChars" as the filter mode. Otherwise this parameter is ignored.
    /// </summary>
    public string InvalidChars
    {
        get
        {
            return ValidationHelper.GetString(GetValue("InvalidChars"), null);
        }
        set
        {
            SetValue("InvalidChars", value);
        }
    }


    /// <summary>
    /// An integer containing the interval (in milliseconds) in which the field's contents are filtered, defaults to 250.
    /// </summary>
    public int FilterInterval
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("FilterInterval"), 250);
        }
        set
        {
            SetValue("FilterInterval", value);
        }
    }


    /// <summary>
    /// The type of filter to apply, as a comma-separated combination of Numbers, LowercaseLetters, UppercaseLetters, and Custom. If Custom is specified, the ValidChars field will be used in addition to other settings such as Numbers.
    /// </summary>
    public FilterTypes FilterType
    {
        get;
        set;
    }


    /// <summary>
    /// This property gets the form control settings of the FilterType.
    /// </summary>
    public string FilterTypeValue
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FilterType"), null);
        }
    }


    /// <summary>
    /// The filter mode to apply, either ValidChars (default) or InvalidChars. If set to InvalidChars, FilterType must be set to Custom; if set to ValidChars, FilterType must contain Custom.
    /// </summary>
    public FilterModes FilterMode
    {
        get
        {
            object filterModeObj = GetValue("FilterMode");
            if (filterModeObj == null)
            {
                return mFilterMode;
            }
            else
            {
                return ValidationHelper.GetBoolean(filterModeObj, false) ? FilterModes.InvalidChars : FilterModes.ValidChars;
            }
        }
        set
        {
            mFilterMode = value;
        }
    }

    #endregion


    #region "Autocomplete properties"

    /// <summary>
    /// The web service method to be called. The signature of this method must match the following: 
    /// [System.Web.Services.WebMethod]
    /// [System.Web.Script.Services.ScriptMethod]
    /// public string[] GetCompletionList(string prefixText, int count) { ... }
    /// Note that you can replace "GetCompletionList" with a name of your choice, but the return type and parameter name and type must exactly match, including case.
    /// </summary>
    public string AutoCompleteServiceMethod
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteServiceMethod"), null);
        }
        set
        {
            SetValue("AutoCompleteServiceMethod", value);
        }
    }


    /// <summary>
    /// The path to the web service that the extender will pull the word\sentence completions from. If this is not provided, the service method should be a page method.
    /// </summary>
    public string AutoCompleteServicePath
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteServicePath"), null);
        }
        set
        {
            SetValue("AutoCompleteServicePath", value);
        }
    }


    /// <summary>
    /// User/page specific context provided to an optional overload of the web method described by ServiceMethod/ServicePath. If the context key is used, it should have the same signature with an additional parameter named contextKey of type string: 
    /// [System.Web.Services.WebMethod]
    /// [System.Web.Script.Services.ScriptMethod]
    /// public string[] GetCompletionList(
    /// string prefixText, int count, string contextKey) { ... }
    /// Note that you can replace "GetCompletionList" with a name of your choice, but the return type and parameter name and type must exactly match, including case.
    /// </summary>
    public string AutoCompleteContextKey
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteContextKey"), null);
        }
        set
        {
            SetValue("AutoCompleteContextKey", value);
        }
    }


    /// <summary>
    /// Minimum number of characters that must be entered before getting suggestions from the web service.
    /// </summary>
    public int AutoCompleteMinimumPrefixLength
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("AutoCompleteMinimumPrefixLength"), 2);
        }
        set
        {
            SetValue("AutoCompleteMinimumPrefixLength", value);
        }
    }


    /// <summary>
    /// Time in milliseconds when the timer will kick in to get suggestions using the web service.
    /// </summary>
    public int AutoCompleteCompletionInterval
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("AutoCompleteCompletionInterval"), 2);
        }
        set
        {
            SetValue("AutoCompleteCompletionInterval", value);
        }
    }


    /// <summary>
    /// Whether client side caching is enabled.
    /// </summary>
    public bool AutoCompleteEnableCaching
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AutoCompleteEnableCaching"), false);
        }
        set
        {
            SetValue("AutoCompleteEnableCaching", value);
        }
    }


    /// <summary>
    /// Number of suggestions to be retrieved from the web service.
    /// </summary>
    public int AutoCompleteCompletionSetCount
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("AutoCompleteCompletionSetCount"), 2);
        }
        set
        {
            SetValue("AutoCompleteCompletionSetCount", value);
        }
    }


    /// <summary>
    /// CSS class that will be used to style the completion list flyout.
    /// </summary>
    public string AutoCompleteCompletionListCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteCompletionListCssClass"), null);
        }
        set
        {
            SetValue("AutoCompleteCompletionListCssClass", value);
        }
    }


    /// <summary>
    /// CSS class that will be used to style an item in the AutoComplete list flyout.
    /// </summary>
    public string AutoCompleteCompletionListItemCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteCompletionListItemCssClass"), null);
        }
        set
        {
            SetValue("AutoCompleteCompletionListItemCssClass", value);
        }
    }


    /// <summary>
    /// CSS class that will be used to style a highlighted item in the AutoComplete list flyout.
    /// </summary>
    public string AutoCompleteCompletionListHighlightedItemCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteCompletionListHighlightedItemCssClass"), null);
        }
        set
        {
            SetValue("AutoCompleteCompletionListHighlightedItemCssClass", value);
        }
    }


    /// <summary>
    /// Specifies one or more character(s) used to separate words. The text in the AutoComplete textbox is tokenized using these characters and the webservice completes the last token.
    /// </summary>
    public string AutoCompleteDelimiterCharacters
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AutoCompleteDelimiterCharacters"), null);
        }
        set
        {
            SetValue("AutoCompleteDelimiterCharacters", value);
        }
    }


    /// <summary>
    /// Determines if the first option in the AutoComplete list will be selected by default.
    /// </summary>
    public bool AutoCompleteFirstRowSelected
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AutoCompleteFirstRowSelected"), false);
        }
        set
        {
            SetValue("AutoCompleteFirstRowSelected", value);
        }
    }


    /// <summary>
    ///  If true and DelimiterCharacters are specified, then the AutoComplete list items display suggestions for the current word to be completed and do not display the rest of the tokens.
    /// </summary>
    public bool AutoCompleteShowOnlyCurrentWordInCompletionListItem
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AutoCompleteShowOnlyCurrentWordInCompletionListItem"), false);
        }
        set
        {
            SetValue("AutoCompleteShowOnlyCurrentWordInCompletionListItem", value);
        }
    }

    #endregion


    #region "Constructors"

    public CMSFormControls_Basic_TextBoxControl()
    {
        FilterMode = FilterModes.ValidChars;
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        SetClientSideMaxLength();

        // Setup TextArea control
        if (IsTextArea)
        {
            // Set cols
            int cols = ValidationHelper.GetInteger(GetValue("cols"), 0);
            if (cols > 0)
            {
                textbox.Columns = cols;
            }

            // Set rows
            int rows = ValidationHelper.GetInteger(GetValue("rows"), 0);
            if (rows > 0)
            {
                textbox.Rows = rows;
            }

            // Set size
            textbox.MaxLength = MaxLength;

            textbox.TextMode = TextBoxMode.MultiLine;
        }
        else
        {
            CheckMinMaxLength = true;
        }

        // Apply CSS styles
        if (!String.IsNullOrEmpty(CssClass))
        {
            textbox.CssClass = CssClass;
            CssClass = null;
        }
        else if (String.IsNullOrEmpty(textbox.CssClass))
        {
            // Set automatic class to the textbox
            if (IsTextArea)
            {
                textbox.CssClass = "TextAreaField";
            }
            else
            {
                textbox.CssClass = "TextBoxField";
            }
        }

        if (!String.IsNullOrEmpty(ControlStyle))
        {
            textbox.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }

        CheckRegularExpression = true;
        CheckFieldEmptiness = true;

        // Set trimming ability from form controls parameters
        Trim = ValidationHelper.GetBoolean(GetValue("trim"), Trim);
    }


    /// <summary>
    /// Sets client-side max length of the textbox control.
    /// </summary>
    private void SetClientSideMaxLength()
    {
        if (FieldInfo != null)
        {
            switch (FieldInfo.DataType)
            {
                case (FormFieldDataTypeEnum.Text):
                    textbox.MaxLength = FieldInfo.Size;
                    break;

                case (FormFieldDataTypeEnum.Integer):
                case (FormFieldDataTypeEnum.LongInteger):
                    if (string.IsNullOrEmpty(FieldInfo.MaxValue) || string.IsNullOrEmpty(FieldInfo.MinValue))
                    {
                        // One of the limit value is not set => set maxint/maxlong length
                        textbox.MaxLength = (FieldInfo.DataType == FormFieldDataTypeEnum.Integer) ?
                            ValidationHelper.MAX_INT_LENGTH :
                            ValidationHelper.MAX_LONGINT_LENGTH;
                    }
                    else
                    {
                        // Set maxlength to the bigger one
                        textbox.MaxLength = Math.Max(FieldInfo.MaxValue.Length, FieldInfo.MinValue.Length);
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// Returns the arraylist of the field IDs (Client IDs of the inner controls) that should be spell checked.
    /// </summary>
    public override ArrayList GetSpellCheckFields()
    {
        ArrayList result = new ArrayList();
        result.Add(textbox.ClientID);
        return result;
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Initialize properties
        if (!string.IsNullOrEmpty(WatermarkText) || FilterEnabled || (!string.IsNullOrEmpty(AutoCompleteServiceMethod) && !string.IsNullOrEmpty(AutoCompleteServicePath)))
        {
            PortalHelper.EnsureScriptManager(Page);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);


        #region "Watermark extender"

        // Watermark extender
        // Disable watermark exteder for nonempty fields (issue with value which is same as the watermark text)
        string resolvedWatermarkText = CMSContext.CurrentResolver.ResolveMacros(WatermarkText);
        if (!string.IsNullOrEmpty(WatermarkText) && !String.IsNullOrEmpty(resolvedWatermarkText) && !string.Equals(textbox.Text, WatermarkText, StringComparison.InvariantCulture))
        {
            // Create extender
            TextBoxWatermarkExtender exWatermark = new TextBoxWatermarkExtender();
            exWatermark.ID = "exWatermark";
            exWatermark.TargetControlID = textbox.ID;
            exWatermark.EnableViewState = false;
            Controls.Add(exWatermark);

            // Initialize extender
            exWatermark.WatermarkText = resolvedWatermarkText;
            exWatermark.WatermarkCssClass = textbox.CssClass + " " + ValidationHelper.GetString(GetValue("WatermarkCssClass"), WatermarkCssClass);
        }

        #endregion


        #region "Filter extender"

        if (FilterEnabled)
        {
            // Create extender
            FilteredTextBoxExtender exFilter = new FilteredTextBoxExtender();
            exFilter.ID = "exFilter";
            exFilter.TargetControlID = textbox.ID;
            exFilter.EnableViewState = false;
            Controls.Add(exFilter);

            // Filter extender
            exFilter.FilterInterval = FilterInterval;

            // Set the filter type
            if (FilterTypeValue == null)
            {
                exFilter.FilterType = FilterType;
            }
            else
            {
                if (!string.IsNullOrEmpty(FilterTypeValue))
                {
                    FilterTypes filterType = 0;
                    string[] types = FilterTypeValue.Split(new char[] { ';', '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (types.Length > 0)
                    {
                        foreach (string typeStr in types)
                        {
                            int type = ValidationHelper.GetInteger(typeStr, 0);
                            switch (type)
                            {
                                case FILTER_NUMBERS:
                                    filterType |= FilterTypes.Numbers;
                                    break;

                                case FILTER_LOWERCASE:
                                    filterType |= FilterTypes.LowercaseLetters;
                                    break;

                                case FILTER_UPPERCASE:
                                    filterType |= FilterTypes.UppercaseLetters;
                                    break;

                                case FILTER_CUSTOM:
                                    filterType |= FilterTypes.Custom;
                                    break;
                            }
                        }
                        exFilter.FilterType = filterType;
                    }
                }
            }

            FilterModes filterMode = FilterMode;

            // Set valid and invalid characters
            if (exFilter.FilterType == FilterTypes.Custom)
            {
                // When filter type is Custom only, filter mode can be anything
                exFilter.FilterMode = filterMode;

                if (filterMode == FilterModes.InvalidChars)
                {
                    exFilter.InvalidChars = InvalidChars;
                }
                else
                {
                    exFilter.ValidChars = ValidChars;
                }
            }
            else
            {
                // Otherwise filter type must be valid chars
                exFilter.FilterMode = FilterModes.ValidChars;

                // Set valid chars only if original filter mode was valid chars and filter type contains Custom
                if ((filterMode == FilterModes.ValidChars) && ((exFilter.FilterType & FilterTypes.Custom) != 0))
                {
                    exFilter.ValidChars = ValidChars;
                }
            }
        }

        #endregion


        #region "Autocomplete extender"

        // Autocomplete extender
        if (!string.IsNullOrEmpty(AutoCompleteServiceMethod) && !string.IsNullOrEmpty(AutoCompleteServicePath))
        {
            // Create extender
            AutoCompleteExtender exAuto = new AutoCompleteExtender();
            exAuto.ID = "exAuto";
            exAuto.TargetControlID = textbox.ID;
            exAuto.EnableViewState = false;
            Controls.Add(exAuto);

            exAuto.ServiceMethod = AutoCompleteServiceMethod;
            exAuto.ServicePath = URLHelper.ResolveUrl(AutoCompleteServicePath);
            exAuto.MinimumPrefixLength = AutoCompleteMinimumPrefixLength;
            exAuto.ContextKey = CMSContext.CurrentResolver.ResolveMacros(AutoCompleteContextKey);
            exAuto.CompletionInterval = AutoCompleteCompletionInterval;
            exAuto.EnableCaching = AutoCompleteEnableCaching;
            exAuto.CompletionSetCount = AutoCompleteCompletionSetCount;
            exAuto.CompletionListCssClass = AutoCompleteCompletionListCssClass;
            exAuto.CompletionListItemCssClass = AutoCompleteCompletionListItemCssClass;
            exAuto.CompletionListHighlightedItemCssClass = AutoCompleteCompletionListHighlightedItemCssClass;
            exAuto.DelimiterCharacters = AutoCompleteDelimiterCharacters;
            exAuto.FirstRowSelected = AutoCompleteFirstRowSelected;
            exAuto.ShowOnlyCurrentWordInCompletionListItem = AutoCompleteShowOnlyCurrentWordInCompletionListItem;
        }

        #endregion
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        if (IsTextArea)
        {
            int maxControlSize = MaxLength;
            int minControlSize = 0;
            string error = null;

            // Check min/max text length
            if (FieldInfo != null)
            {
                if ((FieldInfo.MaxStringLength > -1) && ((maxControlSize > FieldInfo.MaxStringLength) || (maxControlSize == 0)))
                {
                    maxControlSize = FieldInfo.MaxStringLength;
                }
                if ((FieldInfo.Size > 0) && ((maxControlSize > FieldInfo.Size) || (maxControlSize == 0)))
                {
                    maxControlSize = FieldInfo.Size;
                }
                minControlSize = FieldInfo.MinStringLength;
            }

            // Get text length
            int textLength = textbox.Text.Length;
            if (Trim)
            {
                // Get trimmed text length
                textLength = textbox.Text.Trim().Length;
            }

            bool valid = CheckLength(minControlSize, maxControlSize, textLength, ref error, ErrorMessage);
            ValidationError = error;

            return valid;
        }
        else
        {
            return true;
        }
    }

    #endregion
}