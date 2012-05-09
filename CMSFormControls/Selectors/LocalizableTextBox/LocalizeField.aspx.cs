using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ResourceManager;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSFormControls_Selectors_LocalizableTextBox_LocalizeField : CMSModalPage
{
    #region "Variables"

    private string hdnValue = null;
    private string textbox = null;
    private string hdnIsMacro = null;
    private string btnLocalizeField = null;
    private string btnLocalizeString = null;
    private string btnRemoveLocalization = null;
    private string plainText = null;
    private string identificator = null;
    private string resourceKeyPrefix = null;

    /// <summary>
    /// Default prefix for keys created in development mode.
    /// </summary>
    private const string PREFIX = "test.";

    /// <summary>
    /// Maximum resource string key length.
    /// </summary>
    private const int MAX_KEY_LENGTH = 200;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Localization", "LocalizeStrings"))
        {
            // Set title
            CurrentMaster.Title.TitleText = GetString("localizable.localizefield");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_UICulture/new.png");
            CurrentMaster.Title.HelpTopicName = "localize_field";
            CurrentMaster.Title.HelpName = "helpTopic";

            // Validate hash
            Regex re = RegexHelper.GetRegex(@"[\w\d_$$]*");
            identificator = QueryHelper.GetString("params", "");

            if (!QueryHelper.ValidateHash("hash") || !re.IsMatch(identificator))
            {
                pnlContent.Visible = false;
                return;
            }

            // Load dialog parameters
            Hashtable parameters = (Hashtable)WindowHelper.GetItem(identificator.ToString());
            if (parameters != null)
            {
                hdnValue = ValidationHelper.GetString(parameters["HiddenValue"], String.Empty);
                textbox = ValidationHelper.GetString(parameters["TextBoxID"], String.Empty);
                hdnIsMacro = ValidationHelper.GetString(parameters["HiddenIsMacro"], String.Empty);
                plainText = ValidationHelper.GetString(parameters["TextBoxValue"], String.Empty);
                btnLocalizeField = ValidationHelper.GetString(parameters["ButtonLocalizeField"], String.Empty);
                btnLocalizeString = ValidationHelper.GetString(parameters["ButtonLocalizeString"], String.Empty);
                btnRemoveLocalization = ValidationHelper.GetString(parameters["ButtonRemoveLocalization"], String.Empty);
                resourceKeyPrefix = ValidationHelper.GetString(parameters["ResourceKeyPrefix"], String.Empty);
            }
            btnOk.Click += new EventHandler(btnOk_Click);

            lstExistingOrNew.Items[0].Text = GetString("localizable.createnew");
            lstExistingOrNew.Items[1].Text = GetString("localizable.useexisting");

            // Disable option to use existing resource string for user who is not admin
            if (!CurrentUser.UserSiteManagerAdmin)
            {
                lstExistingOrNew.Items[1].Enabled = false;
            }

            // If "create new" is selected
            if (lstExistingOrNew.SelectedIndex == 0)
            {
                if (!String.IsNullOrEmpty(resourceKeyPrefix))
                {
                    lblPrefix.Text = resourceKeyPrefix;
                    lblPrefix.Visible = true;
                }
                lblSelectKey.ResourceString = GetString("localizable.newkey");
                resourceSelector.Visible = false;
                txtNewResource.Visible = true;

                if (!RequestHelper.IsPostBack())
                {
                    // Get maximum length of resource key
                    int maxKeyLength = MAX_KEY_LENGTH;
                    string keyPrefix = resourceKeyPrefix;
                    if (SettingsKeyProvider.DevelopmentMode && String.IsNullOrEmpty(keyPrefix))
                    {
                        keyPrefix = PREFIX;
                    }
                    if (!String.IsNullOrEmpty(keyPrefix))
                    {
                        maxKeyLength -= keyPrefix.Length;
                    }

                    // Initialize resource string
                    string newResource = TextHelper.LimitLength(ValidationHelper.GetCodeName(plainText), maxKeyLength, String.Empty, true);

                    // Check if key already exists
                    string key = keyPrefix + newResource;
                    int i = 0;
                    // If key exists then create new one with number as suffix
                    while (SqlResourceManager.GetResourceStringInfo(key) != null)
                    {
                        // If newly created resource key is longer then allowed length then trim end by one character
                        if ((keyPrefix.Length + newResource.Length + ++i) > MAX_KEY_LENGTH)
                        {
                            newResource = newResource.Substring(0, newResource.Length - 1);
                        }
                        key = keyPrefix + newResource + i;
                    }

                    // Set newly created resource string
                    if (String.IsNullOrEmpty(resourceKeyPrefix))
                    {
                        if (!newResource.StartsWith(PREFIX))
                        {
                            txtNewResource.Text = PREFIX + newResource;
                        }
                        else
                        {
                            txtNewResource.Text = newResource;
                        }
                    }
                    else
                    {
                        txtNewResource.Text = newResource;
                    }

                    if (i > 0)
                    {
                        txtNewResource.Text += i;
                    }
                }
            }
            // If "use existing" is selected
            else
            {
                lblSelectKey.ResourceString = GetString("localizable.existingkey");
                resourceSelector.Visible = true;
                txtNewResource.Visible = false;
            }
        }
        // Dialog is not available for unauthorized user
        else
        {
            lblError.ResourceString = "security.accesspage.onlyglobaladmin";
            lblError.Visible = true;
            pnlControls.Visible = false;
        }
    }


    /// <summary>
    /// Button OK clicked.
    /// </summary>
    void btnOk_Click(object sender, EventArgs e)
    {
        // Check permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Localization", "LocalizeStrings"))
        {
            string key = null;
            ResourceStringInfo ri = null;

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

            // Creating new resource string
            if (lstExistingOrNew.SelectedIndex == 0)
            {
                if (SettingsKeyProvider.DevelopmentMode && String.IsNullOrEmpty(resourceKeyPrefix))
                {
                    key = txtNewResource.Text.Trim();
                }
                else
                {
                    key = resourceKeyPrefix + txtNewResource.Text.Trim();
                }
                ri = SqlResourceManager.GetResourceStringInfo(key);

                // Resource string doesn't exists yet
                if (ri == null)
                {
                    lblError.Text = new Validator().NotEmpty(key, GetString("Administration-UICulture_String_New.EmptyKey")).IsCodeName(key, GetString("Administration-UICulture_String_New.InvalidCodeName")).Result;
                    if (!String.IsNullOrEmpty(lblError.Text))
                    {
                        lblError.Visible = true;
                    }
                    else
                    {
                        // Save ResourceString
                        ri = new ResourceStringInfo();
                        ri.StringKey = key;
                        ri.UICultureCode = cultureCode;
                        ri.TranslationText = plainText;
                        ri.StringIsCustom = !SettingsKeyProvider.DevelopmentMode;
                        SqlResourceManager.SetResourceStringInfo(ri);

                        ScriptHelper.RegisterStartupScript(this, typeof(string), "localizeField", ScriptHelper.GetScript("wopener.SetResourceAndOpen('" + hdnValue + "', '" + key + "', '" + textbox + "', " + ScriptHelper.GetString(plainText) + ", '" + hdnIsMacro + "', '" + btnLocalizeField + "', '" + btnLocalizeString + "', '" + btnRemoveLocalization + "', window);"));
                    }
                }
                // If resource string already exists with different translation
                else
                {
                    lblError.Visible = true;
                    lblError.ResourceString = "localize.alreadyexists";
                }
            }
            // Using existing resource string
            else
            {
                key = ValidationHelper.GetString(resourceSelector.Value, String.Empty).Trim();
                ri = SqlResourceManager.GetResourceStringInfo(key);

                // Key not found in DB
                if (ri == null)
                {
                    // Try to find it in .resx file and save it in DB
                    FileResourceManager resourceManager = ResHelper.GetFileManager(cultureCode);
                    if (resourceManager != null)
                    {
                        string translation = resourceManager.GetString(key);
                        if (!String.IsNullOrEmpty(translation))
                        {
                            ri = new ResourceStringInfo();
                            ri.StringKey = key;
                            ri.StringIsCustom = !SettingsKeyProvider.DevelopmentMode;
                            ri.UICultureCode = cultureCode;
                            ri.TranslationText = translation;
                            SqlResourceManager.SetResourceStringInfo(ri);

                            ScriptHelper.RegisterStartupScript(this, typeof(string), "localizeField", ScriptHelper.GetScript("wopener.SetResource('" + hdnValue + "', '" + key + "', '" + textbox + "', " + ScriptHelper.GetString(translation) + ", '" + hdnIsMacro + "', '" + btnLocalizeField + "', '" + btnLocalizeString + "', '" + btnRemoveLocalization + "', window);"));
                        }
                        else
                        {
                            lblError.Visible = true;
                            lblError.ResourceString = "localize.doesntexist";
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ResourceString = "localize.doesntexist";
                    }
                }
                // Send to parent window selected resource key
                else
                {
                    string existingTranslation = GetString(key);
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "localizeField", ScriptHelper.GetScript("wopener.SetResource('" + hdnValue + "', '" + key + "', '" + textbox + "', " + ScriptHelper.GetString(existingTranslation) + ", '" + hdnIsMacro + "', '" + btnLocalizeField + "', '" + btnLocalizeString + "', '" + btnRemoveLocalization + "', window);"));
                }
            }
        }
        else
        {
            lblError.ResourceString = "general.actiondenied";
            lblError.Visible = true;
            pnlControls.Visible = false;
        }
    }

    #endregion
}
