using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ResourceManager;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;

[ValidationPropertyAttribute("Value")]
public partial class CMSFormControls_System_LocalizableTextBox : FormEngineUserControl, ICallbackEventHandler
{
    #region "Macros and variables"

    /// <summary>
    /// Localization macro starts with '{$' characters.
    /// </summary>
    public const string MACRO_START = "{$";

    /// <summary>
    /// Localization macro starts with '$}' characters.
    /// </summary>
    public const string MACRO_END = "$}";

    /// <summary>
    /// In-place localization macro starts with '{$=' characters and should not be localized in localizable textbox!
    /// </summary>
    protected const string INPLACE_MACRO_START = "{$=";

    /// <summary>
    /// URL of field localization modal dialog.
    /// </summary>
    public const string LOCALIZE_FIELD = "~/CMSFormControls/Selectors/LocalizableTextBox/LocalizeField.aspx";

    /// <summary>
    /// URL of string localization modal dialog.
    /// </summary>
    public const string LOCALIZE_STRING = "~/CMSFormControls/Selectors/LocalizableTextBox/LocalizeString.aspx";

    /// <summary>
    /// Default prefix for keys created in development mode.
    /// </summary>
    private const string PREFIX = "test.";

    /// <summary>
    /// Maximum resource string key length.
    /// </summary>
    private const int MAX_KEY_LENGTH = 200;

    /// <summary>
    /// Default button style.
    /// </summary>
    private const string BUTTON_STYLE = "background-image: url('{0}'); background-color: transparent; background-repeat: no-repeat; width: 16px; height: 16px; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; ";

    // Indicates if changes to resource string should be performed immediately after each PostBack.
    private bool mAutoSave = true;

    // Value returned by callback
    private string callbackReturnVal = null;

    // Resource key prefix
    private string mResourceKeyPrefix = String.Empty;
    private bool prefixIsSet = false;
    private bool? mLocalizationExists = null;
    private bool mIsLiveSite = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets whether the control is read only
    /// </summary>
    public bool ReadOnly
    {
        get
        {
            return textbox.ReadOnly;
        }
        set
        {
            textbox.ReadOnly = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            if (textbox != null)
            {
                textbox.Enabled = value;
                btnLocalize.Enabled = value;
                btnOtherLanguages.Enabled = value;
                btnRemoveLocalization.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Return macro contained in hidden field if text is macro
            if (IsMacro)
            {
                if (LocalizationExists)
                {
                    return hdnValue.Value;
                }
                else
                {
                    return MACRO_START + textbox.Text + MACRO_END;
                }
            }
            // Return plain text contained in textbox
            else
            {
                return textbox.Text;
            }
        }
        set
        {
            string valueStr = hdnValue.Value = ValidationHelper.GetString(value, null);

            // Check if value is localization macro
            if (!String.IsNullOrEmpty(valueStr) && !valueStr.StartsWith(INPLACE_MACRO_START) && valueStr.StartsWith(MACRO_START) && valueStr.EndsWith(MACRO_END))
            {
                this.IsMacro = true;
                if (!RequestHelper.IsPostBack())
                {
                    textbox.Text = ResHelper.LocalizeString(valueStr);
                }
            }
            // Value is plain text
            else
            {
                this.IsMacro = false;
                textbox.Text = valueStr;

                if (!string.IsNullOrEmpty(valueStr) && pnlButtons.Visible)
                {
                    // Hide localization buttons if in-place macro is edited
                    pnlButtons.Visible = !valueStr.StartsWith(INPLACE_MACRO_START);
                }
            }
        }
    }


    /// <summary>
    /// Gets value indicating if localization of key exists.
    /// </summary>
    private bool LocalizationExists
    {
        get
        {
            // Determine if translation exists
            if (mLocalizationExists == null)
            {
                bool translationFound = false;
                ResHelper.LocalizeString(hdnValue.Value, out translationFound);
                mLocalizationExists = translationFound;
            }
            if (mLocalizationExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    /// <summary>
    /// Publicly visible textbox which contains translated string or plain text.
    /// </summary>
    public TextBox TextBox
    {
        get
        {
            return textbox;
        }
    }


    /// <summary>
    /// TextMode of textbox.
    /// </summary>
    public TextBoxMode TextMode
    {
        get
        {
            return textbox.TextMode;
        }
        set
        {
            textbox.TextMode = value;
        }
    }


    /// <summary>
    /// Number of columns of textbox in multiline mode.
    /// </summary>
    public int Columns
    {
        get
        {
            return textbox.Columns;
        }
        set
        {
            textbox.Columns = value;
        }
    }


    /// <summary>
    /// Number of rows of textbox in multiline mode.
    /// </summary>
    public int Rows
    {
        get
        {
            return textbox.Rows;
        }
        set
        {
            textbox.Rows = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if control should save changes to resource string immediately after each PostBack. Default true.
    /// </summary>
    public bool AutoSave
    {
        get
        {
            return mAutoSave;
        }
        set
        {
            mAutoSave = value;
        }
    }


    /// <summary>
    /// Indicates if text contained in textbox is resolved resource string or if it is just plain text.
    /// </summary>
    private bool IsMacro
    {
        get
        {
            return ValidationHelper.GetBoolean(hdnIsMacro.Value, false);
        }
        set
        {
            hdnIsMacro.Value = value.ToString();
        }
    }


    /// <summary>
    /// Modal dialog identificator.
    /// </summary>
    private string Identificator
    {
        get
        {
            // Try to load data from control viewstate
            string identificator = ValidationHelper.GetString(Request.Params[hdnIdentificator.UniqueID], String.Empty);

            // Create new Guid
            if (string.IsNullOrEmpty(identificator))
            {
                identificator = Guid.NewGuid().ToString();
            }

            // Store Guid to hidden control
            if (string.IsNullOrEmpty(hdnIdentificator.Value))
            {
                hdnIdentificator.Value = identificator;                
            }

            return identificator;
        }
    }


    /// <summary>
    /// Maximum length of plain text or resource string key. Validates in IsValid() method.
    /// </summary>
    public int MaxLength
    {
        get
        {
            return textbox.MaxLength;
        }
        set
        {
            textbox.MaxLength = value;
        }
    }


    /// <summary>
    /// Resource key prefix. Default value is empty for DevelopmentMode, 'custom.' value otherwise.
    /// </summary>
    public string ResourceKeyPrefix
    {
        get
        {
            // If user set prefix
            if (prefixIsSet)
            {
                return mResourceKeyPrefix;
            }
            // If in DevelopmentMode
            else if (SettingsKeyProvider.DevelopmentMode)
            {
                return String.Empty;
            }
            // Otherwise return "custom."
            else
            {
                return "custom.";
            }
        }
        set
        {
            mResourceKeyPrefix = value;
            prefixIsSet = true;
        }
    }


    /// <summary>
    /// If TRUE then resource string key selection is skipped. Instead resource string key is automaticaly created from entered text.
    /// Also when removing localization it also deletes resource string key assigned.
    /// </summary>
    public bool AutomaticMode
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if control is used on live site. Default value is FALSE for localizable text box.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return mIsLiveSite;
        }
        set
        {
            mIsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set controls
        btnOtherLanguages.ToolTip = GetString("localizable.otherlanguages");
        btnOtherLanguages.OnClientClick = "LocalizeString('" + hdnValue.ClientID + "', '" + textbox.ClientID + "'); return false;";
        btnLocalize.ToolTip = GetString("localizable.localize");
        btnRemoveLocalization.Click += new EventHandler(btnRemoveLocalization_Click);
        btnRemoveLocalization.ToolTip = GetString("localizable.remove");
        string buttonScript =
@"var confirmValue = confirm('" + GetString("localizable.removelocalization") + @"');
if (!confirmValue) return false;";
        btnRemoveLocalization.OnClientClick = buttonScript;

        // In automatic mode resource string key is generated from plain text
        if (this.AutomaticMode)
        {
            btnLocalize.Click += new EventHandler(btnLocalize_Click);
        }
        // Otherwise user has option to select resource string key
        else
        {
            btnLocalize.OnClientClick = "LocalizationDialog" + ClientID + "('$|' + document.getElementById('" + textbox.ClientID + "').value); return false;";
        }

        if (textbox.TextMode == TextBoxMode.MultiLine)
        {
            pnlButtons.CssClass = "LocalizablePanel LocalizableTop";
        }

        // Show/hide localization controls
        pnlButtons.Visible =
            CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Localization", "LocalizeStrings") &&
            !this.IsLiveSite &&
            (SettingsKeyProvider.DevelopmentMode || (UICultureInfoProvider.NumberOfUICultures > 1)) &&
            !textbox.Text.StartsWith(INPLACE_MACRO_START);

        // Apply CSS style
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            textbox.CssClass = this.CssClass;
            this.CssClass = null;
        }

        // Register event handler for saving data in BasicForm
        if (this.Form != null)
        {
            this.Form.OnAfterSave += Form_OnAfterSave;
        }
        // Save changes after each PostBack if set so
        else if (RequestHelper.IsPostBack() && this.AutoSave)
        {
            Save();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Ensure the text in textbox
        if (this.IsMacro && RequestHelper.IsPostBack())
        {
            if (this.LocalizationExists)
            {
                textbox.Text = ResHelper.LocalizeString(hdnValue.Value);
            }
            else
            {
                textbox.Text = hdnValue.Value.Substring(MACRO_START.Length, hdnValue.Value.Length - (MACRO_END.Length + MACRO_START.Length));
            }
        }

        Reload();

        // Register the scripts
        if (pnlButtons.Visible)
        {
            RegisterScripts();
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Validates control.
    /// </summary>
    public override bool IsValid()
    {
        // Check for maximum length
        if (MaxLength > 0)
        {
            return (hdnValue.Value.Length <= MaxLength) && (textbox.Text.Length <= MaxLength);
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    public void Reload()
    {
        if (this.pnlButtons.Visible)
        {
            string iconLocalize = GetImageUrl("/Objects/CMS_UICulture/add.png");
            string iconOther = GetImageUrl("Objects/CMS_UICulture/list.png");
            string iconRemove = GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png");

            // Textbox contains translated macro
            if (IsMacro && LocalizationExists)
            {
                btnLocalize.Attributes.Add("style", string.Format(BUTTON_STYLE, iconLocalize) + "display: none;");
                btnOtherLanguages.Attributes.Add("style", string.Format(BUTTON_STYLE, iconOther) + "display: inline;");
                btnRemoveLocalization.Attributes.Add("style", string.Format(BUTTON_STYLE, iconRemove) + "display: inline;");
            }
            // Textbox contains only plain text
            else
            {
                btnOtherLanguages.Attributes.Add("style", string.Format(BUTTON_STYLE, iconOther) + "display: none;");
                btnRemoveLocalization.Attributes.Add("style", string.Format(BUTTON_STYLE, iconRemove) + "display: none;");
                btnLocalize.Attributes.Add("style", string.Format(BUTTON_STYLE, iconLocalize) + "display:inline;");
            }
        }
    }


    /// <summary>
    /// Registers JS.
    /// </summary>
    private void RegisterScripts()
    {
        // Register function to set translation string key from dialog window
        ScriptHelper.RegisterDialogScript(this.Page);
        StringBuilder script = new StringBuilder();
        script.Append(
            @"
function Get(id) {
    return document.getElementById(id);
}

function SetResource(hdnValue, value, textbox, textboxvalue, hdnIsMacro, btnLocalizeField, btnLocalizeString, btnRemoveLocalization, windowObj) {
    SetProperties(hdnValue, value, textbox, textboxvalue, hdnIsMacro, btnLocalizeField, btnLocalizeString, btnRemoveLocalization);
    if (windowObj != null) { windowObj.close() }
    return false;
}

function SetResourceAndOpen(hdnValue, value, textbox, textboxvalue, hdnIsMacro, btnLocalizeField, btnLocalizeString, btnRemoveLocalization, windowObj) { 
    SetProperties(hdnValue, value, textbox, textboxvalue, hdnIsMacro, btnLocalizeField, btnLocalizeString, btnRemoveLocalization);
    if (windowObj != null) { windowObj.close() }
    OpenLocalize(hdnValue, textbox);
    return false;
}

function SetProperties(hdnValue, value, textbox, textboxvalue, hdnIsMacro, btnLocalizeField, btnLocalizeString, btnRemoveLocalization) {
    Get(hdnValue).value = '", MACRO_START, @"' + value + '", MACRO_END, @"';
    Get(textbox).value = textboxvalue;
    Get(hdnIsMacro).value = 'true';
    Get(btnLocalizeField).style.display='none';
    Get(btnLocalizeString).style.display='inline';
    Get(btnRemoveLocalization).style.display='inline';
    return false;
}

function OpenLocalize(hdnValue, textbox) {
    window.setTimeout(function() { LocalizeString(hdnValue, textbox); }, 1);
    return false
}

function SetTranslation(textbox, textboxvalue, hdnValue, value) {
    Get(textbox).value = textboxvalue;
    Get(hdnValue).value = '", MACRO_START, @"' + value + '", MACRO_END, @"';
}

function LocalizeFieldReady(rvalue, context) {
    modalDialog(context, 'localizableField', 500, 350, null, null, true); 
    return false;
}

function LocalizeString(hiddenValueControl, textboxControl) {
    var stringKey = Get(hiddenValueControl).value;
    stringKey = stringKey.substring(", MACRO_START.Length, @",stringKey.length-", MACRO_END.Length, @");
    modalDialog('", ResolveUrl(LOCALIZE_STRING), @"?hiddenValueControl=' + hiddenValueControl + '&stringKey=' + escape(stringKey) + '&parentTextbox=' + textboxControl, 'localizableString', 600, 635, null, null, true);
    return false;
}
");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "LocalizationDialogFunction", ScriptHelper.GetScript(script.ToString()));

        // Register callback to send current plain text to modal window for localization
        string url = LOCALIZE_FIELD + "?params=" + Identificator;
        url += "&hash=" + QueryHelper.GetHash(url, false);

        script = new StringBuilder();
        script.Append(
@"
function LocalizationDialog", ClientID, @"(values) {
    ", Page.ClientScript.GetCallbackEventReference(this, "values", "LocalizeFieldReady", "'" + ResolveUrl(url) + "'"), @"
}
"
    );

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "LocalizationDialog" + ClientID, ScriptHelper.GetScript(script.ToString()));
    }


    /// <summary>
    /// Saves translation for given resource string.
    /// </summary>
    /// <returns>Returns TRUE if resource string was succesfully modified</returns>
    public bool Save()
    {
        // Save changes only when macro is edited
        if (this.IsMacro)
        {
            if (!textbox.Text.Trim().StartsWith(INPLACE_MACRO_START))
            {
                // Update key
                var key = hdnValue.Value.Substring(MACRO_START.Length, hdnValue.Value.Length - (MACRO_END.Length + MACRO_START.Length));
                var ri = SqlResourceManager.GetResourceStringInfo(key) ?? new ResourceStringInfo {StringKey = key};

                ri.TranslationText = textbox.Text.Trim();
                if (SqlResourceManager.GetUICultureID(CultureHelper.PreferredUICulture) != 0)
                {
                    ri.UICultureCode = CultureHelper.PreferredUICulture;
                }
                else
                {
                    ri.UICultureCode = CultureHelper.DefaultUICulture;
                }
                SqlResourceManager.SetResourceStringInfo(ri);
                return true;
            }
            else
            {
                // Remove localization if in-place macro was inserted
                RemoveLocalization();
            }
        }
        return false;
    }


    /// <summary>
    /// Removes localization from the textbox.
    /// </summary>
    private void RemoveLocalization()
    {
        // In automatic mode remove resource string key 
        if (AutomaticMode)
        {
            string key = hdnValue.Value.Substring(MACRO_START.Length, hdnValue.Value.Length - (MACRO_END.Length + MACRO_START.Length));
            SqlResourceManager.DeleteResourceStringInfo(key, CultureHelper.DefaultUICulture);
        }

        hdnValue.Value = textbox.Text;
        this.IsMacro = false;
    }


    /// <summary>
    /// Sets the dialog parameters to the context.
    /// </summary>
    protected void SetFieldDialog(string textboxValue)
    {
        Hashtable parameters = new Hashtable();
        parameters["TextBoxValue"] = textboxValue;
        parameters["HiddenValue"] = hdnValue.ClientID;
        parameters["TextBoxID"] = textbox.ClientID;
        parameters["HiddenIsMacro"] = hdnIsMacro.ClientID;
        parameters["ButtonLocalizeField"] = btnLocalize.ClientID;
        parameters["ButtonLocalizeString"] = btnOtherLanguages.ClientID;
        parameters["ButtonRemoveLocalization"] = btnRemoveLocalization.ClientID;
        parameters["ResourceKeyPrefix"] = this.ResourceKeyPrefix;

        WindowHelper.Add(Identificator, parameters);
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Button localize click. In AutomaticMode available only.
    /// </summary>
    void btnLocalize_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(textbox.Text.Trim()))
        {
            // Get maximum length of resource key
            int maxKeyLength = MAX_KEY_LENGTH;
            string prefix = ResourceKeyPrefix;
            if (SettingsKeyProvider.DevelopmentMode && String.IsNullOrEmpty(ResourceKeyPrefix))
            {
                prefix = PREFIX;
            }

            if (!String.IsNullOrEmpty(prefix))
            {
                maxKeyLength -= prefix.Length;
            }

            // Initialize resource string
            string newResource = TextHelper.LimitLength(ValidationHelper.GetCodeName(textbox.Text.Trim()), maxKeyLength, String.Empty, true);

            int i = 0;
            if (!newResource.StartsWith(prefix))
            {
                hdnValue.Value = prefix + newResource;
            }
            else
            {
                hdnValue.Value = newResource;
            }
            // If key exists then create new one with number as a suffix
            while (SqlResourceManager.GetResourceStringInfo(hdnValue.Value) != null)
            {
                // If newly created resource key is longer then allowed length then trim end by one character
                if ((prefix.Length + newResource.Length + ++i) > MAX_KEY_LENGTH)
                {
                    newResource = newResource.Substring(0, newResource.Length - 1);
                }

                if (!newResource.StartsWith(prefix))
                {
                    hdnValue.Value = prefix + newResource + i;
                }
                else
                {
                    hdnValue.Value = newResource + i;
                }
            }

            // Check if current user's culture exists
            UICultureInfo uiCulture = null;
            string cultureCode = CultureHelper.PreferredUICulture;
            try
            {
                uiCulture = UICultureInfoProvider.GetUICultureInfo(CultureHelper.PreferredUICulture);
            }
            // Use default UI culture
            catch
            {
                cultureCode = CultureHelper.DefaultUICulture;
            }
            // Use default UI culture
            if (uiCulture == null)
            {
                cultureCode = CultureHelper.DefaultUICulture;
            }

            // Save ResourceString
            ResourceStringInfo ri = new ResourceStringInfo();
            ri.StringKey = hdnValue.Value;
            ri.UICultureCode = cultureCode;
            ri.TranslationText = textbox.Text;
            ri.StringIsCustom = !SettingsKeyProvider.DevelopmentMode;
            SqlResourceManager.SetResourceStringInfo(ri);

            // Open 'localization to other languages' window
            ScriptHelper.RegisterStartupScript(this, typeof(string), "OpenLocalization", ScriptHelper.GetScript("modalDialog('" + ResolveUrl(LOCALIZE_STRING) + "?hiddenValueControl=" + hdnValue.ClientID + "&stringKey=" + ri.StringKey + "&parentTextbox=" + textbox.ClientID + "', 'localizableString', 600, 635, null, null, true);"));

            // Set macro settings
            this.Value = MACRO_START + hdnValue.Value + MACRO_END;
            Reload();
        }
        else
        {
            lblError.Visible = true;
            lblError.ResourceString = "localize.entertext";
        }
    }


    /// <summary>
    /// Remove localization button click.
    /// </summary>
    void btnRemoveLocalization_Click(object sender, EventArgs e)
    {
        RemoveLocalization();
    }


    /// <summary>
    /// BasicForm saved event handler.
    /// </summary>
    void Form_OnAfterSave(object sender, EventArgs e)
    {
        Save();
    }


    /// <summary>
    /// Gets callback result.
    /// </summary>
    string ICallbackEventHandler.GetCallbackResult()
    {
        // Prepare the parameters for dialog
        SetFieldDialog(callbackReturnVal);
        return "";
    }


    /// <summary>
    /// Raise callback event.
    /// </summary>
    void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
    {
        // Get value from callback
        if ((eventArgument != null) && eventArgument.StartsWith("$|"))
        {
            callbackReturnVal = eventArgument.Substring(2);
        }
    }

    #endregion
}