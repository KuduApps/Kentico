using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Controls_SettingsKeyEdit : CMSAdminEditControl
{
    #region "Private Members"

    private SettingsKeyInfo mSettingsKeyObj = null;
    private int mSettingsKeyId = 0;
    private int mRootCategoryId = 0;
    private int mSelectedGroupId = -1;
    private bool mIsCustomSetting = true;
    private string mTreeRefreshUrl = string.Empty;
    private string mHeaderRefreshUrl = string.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// If set to true "Key is hidden checkbox" will be hidden in the form and current key will be marked as custom.
    /// </summary>
    public bool IsCustomSetting
    {
        get
        {
            return mIsCustomSetting;
        }
        set
        {
            mIsCustomSetting = value;
        }
    }


    /// <summary>
    /// Gets or sets RootCategoryID. Specifies SettingsCategory which should be set up as the root of the SettingsCategorySelector.
    /// </summary>
    public int RootCategoryID
    {
        get
        {
            return mRootCategoryId;
        }
        set
        {
            mRootCategoryId = value;
        }
    }


    /// <summary>
    /// Gets or sets SelectedGroupID. Specifies SettingsCategory for new record. If set, SettingsCategorySelector is not shown.
    /// </summary>
    public int SelectedGroupID
    {
        get
        {
            return mSelectedGroupId;
        }
        set
        {
            mSelectedGroupId = value;
        }
    }


    /// <summary>
    /// Gets or sets SettingsKeyID. Specifies ID of SettingsKey object.
    /// </summary>
    public int SettingsKeyID
    {
        get
        {
            return mSettingsKeyId;
        }
        set
        {
            mSettingsKeyId = value;
            mSettingsKeyObj = null;
        }
    }


    /// <summary>
    /// Gets or sets SettingsKey object. Specifies SettingsKey object which should be edited.
    /// </summary>
    public SettingsKeyInfo SettingsKeyObj
    {
        get
        {
            return mSettingsKeyObj ?? (mSettingsKeyObj = SettingsKeyProvider.GetSettingsKeyInfo(mSettingsKeyId));
        }
        set
        {
            mSettingsKeyObj = value;
            mSettingsKeyId = (value != null) ? value.KeyID : 0;
        }
    }

    /// <summary>
    /// Url for refreshing tree with settings.
    /// </summary>
    public string TreeRefreshUrl
    {
        get
        {
            return mTreeRefreshUrl;
        }
        set
        {
            mTreeRefreshUrl = value;
        }
    }


    /// <summary>
    /// Url for refreshing header.
    /// </summary>
    public string HeaderRefreshUrl
    {
        get
        {
            return mHeaderRefreshUrl;
        }
        set
        {
            mHeaderRefreshUrl = value;
        }
    }


    /// <summary>
    /// Default key value from/for appropriate control (text box or check box)
    /// </summary>
    protected string DefaultValue
    {
        get
        {
            if (drpKeyType.SelectedValue == "boolean")
            {
                return chkKeyValue.Checked ? "True" : "False";
            }
            else
            {
                return txtKeyValue.Text.Trim();
            }
        }
        set
        {
            if (drpKeyType.SelectedValue == "boolean")
            {
                chkKeyValue.Checked = ValidationHelper.GetBoolean(value, false);
            }
            else
            {
                txtKeyValue.Text = value;
            }
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }
        InitControls();

        // Load the form data
        if (!URLHelper.IsPostback())
        {
            LoadData();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show or hide messages
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text);
        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);

        // Set the root category
        if (RootCategoryID > 0)
        {
            drpCategory.RootCategoryId = RootCategoryID;
            drpCategory.ReloadData();
        }

        // Hide category selector if category specified programaticaly.
        if (mSelectedGroupId >= 0)
        {
            trCategory.Visible = false;
        }
        else
        {
            // Set parrent category
            if ((SettingsKeyObj != null) && (SettingsKeyObj.KeyCategoryID > 0))
            {
                if (drpCategory.Items.Count == 0)
                {
                    drpCategory.ReloadData();
                }
                drpCategory.SelectedCategory = SettingsKeyObj.KeyCategoryID;
            }
        }

        SelectDefaultValueControl();
        trKeyIsHidden.Visible = !mIsCustomSetting;
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Initialization of controls.
    /// </summary>
    private void InitControls()
    {
        // Init validators
        rfvKeyDisplayName.ErrorMessage = ResHelper.GetString("general.requiresdisplayname");
        rfvKeyName.ErrorMessage = ResHelper.GetString("general.requirescodename");

        // Display of LoadGeneration table row
        trLoadGeneration.Visible = SettingsKeyProvider.DevelopmentMode;

        if (!RequestHelper.IsPostBack())
        {
            LoadKeyTypes();
        }
    }


    /// <summary>
    /// Loads key types into the DropDownList control.
    /// </summary>
    private void LoadKeyTypes()
    {
        // Load KeyTypes
        drpKeyType.Items.Clear();
        drpKeyType.Items.Add(new ListItem(GetString("settings.keytype.boolean"), "boolean"));
        drpKeyType.Items.Add(new ListItem(GetString("settings.keytype.integer"), "int"));
        drpKeyType.Items.Add(new ListItem(GetString("settings.keytype.double"), "double"));
        drpKeyType.Items.Add(new ListItem(GetString("settings.keytype.string"), "string"));

        SelectDefaultValueControl();
    }


    /// <summary>
    /// Loads the data into the form.
    /// </summary>
    private void LoadData()
    {
        // Load the form from the Info object
        if (SettingsKeyObj != null)
        {
            txtKeyName.Text = SettingsKeyObj.KeyName;
            txtKeyDisplayName.Text = SettingsKeyObj.KeyDisplayName;
            txtKeyDescription.Text = SettingsKeyObj.KeyDescription;
            drpCategory.SelectedCategory = SettingsKeyObj.KeyCategoryID;
            drpKeyType.SelectedValue = SettingsKeyObj.KeyType;
            DefaultValue = SettingsKeyObj.KeyDefaultValue;
            txtKeyValidation.Text = SettingsKeyObj.KeyValidation;
            txtFormControl.Text = SettingsKeyObj.KeyEditingControlPath;
            drpGeneration.Value = -1;
            chkKeyIsGlobal.Checked = SettingsKeyObj.KeyIsGlobal;
            chkKeyIsHidden.Checked = SettingsKeyObj.KeyIsHidden;
        }
    }


    /// <summary>
    /// Updates settings key for all sites (or only global if the IsGlobal checkbox is checked).
    /// </summary>
    /// <param name="siteKeyName">CodeName of the SettingsKey for the site</param>
    /// <param name="keyObj">Instance of the SettingsKey object</param>
    /// <param name="putNullValues">If set to <c>true</c> null value will be set as KeyValue</param>
    /// <returns>CodeName of the SettingsKey objects.</returns>
    private string UpdateAllSitesKey(string siteKeyName, SettingsKeyInfo keyObj, bool putNullValues)
    {
        int oldKeyCategoryID = keyObj.KeyCategoryID;

        keyObj.KeyName = txtKeyName.Text.Trim();
        keyObj.KeyDisplayName = txtKeyDisplayName.Text.Trim();
        keyObj.KeyDescription = txtKeyDescription.Text.Trim();
        keyObj.KeyType = drpKeyType.SelectedValue;
        keyObj.KeyCategoryID = mSelectedGroupId >= 0 ? mSelectedGroupId : drpCategory.SelectedCategory;
        keyObj.KeyIsGlobal = chkKeyIsGlobal.Checked;
        keyObj.KeyIsHidden = chkKeyIsHidden.Checked;
        keyObj.KeyIsCustom = mIsCustomSetting;
        if (putNullValues)
        {
            keyObj.KeyValue = DefaultValue;
        }
        keyObj.KeyValidation = (string.IsNullOrEmpty(txtKeyValidation.Text.Trim()) ? null : txtKeyValidation.Text.Trim());
        keyObj.KeyDefaultValue = (string.IsNullOrEmpty(DefaultValue) ? null : DefaultValue);
        keyObj.KeyEditingControlPath = (string.IsNullOrEmpty(txtFormControl.Text.Trim()) ? null : txtFormControl.Text.Trim());
        if (drpGeneration.Value >= 0)
        {
            keyObj.KeyLoadGeneration = drpGeneration.Value;
        }
        // Update information on setting key concerning application level
        if (chkKeyIsGlobal.Checked)
        {
            keyObj.SiteID = 0;
        }

        // If category changed set new order or if new set on the end of key list
        if (keyObj.KeyCategoryID != oldKeyCategoryID)
        {
            DataSet ds = SettingsKeyProvider.GetSettingsKeys(0, keyObj.KeyCategoryID);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataTable keyTable = ds.Tables[0];
                keyTable.DefaultView.Sort = "KeyOrder DESC";
                keyTable = keyTable.DefaultView.ToTable();

                // Set new KeyOrder as last setings key
                keyObj.KeyOrder = ValidationHelper.GetInteger(keyTable.Rows[0]["KeyOrder"], 0) + 1;
            }
            else
            {
                // Move into empty category
                keyObj.KeyOrder = 1;
            }
        }
        SettingsKeyProvider.SetValue(keyObj);

        // Update all sites keys
        DataSet sites = SiteInfoProvider.GetSites(null, null, "SiteID");
        foreach (DataRow dr in sites.Tables[0].Rows)
        {
            int siteId = ValidationHelper.GetInteger(dr["SiteID"], 0);

            // Get site specific key information
            SettingsKeyInfo keySite = SettingsKeyProvider.GetSettingsKeyInfo(siteKeyName, siteId);
            if ((keySite == null) && (!keyObj.KeyIsGlobal))
            {
                keySite = new SettingsKeyInfo();
                // Default value for new key for site-specific setting is to inherit from global
                keySite.KeyDefaultValue = null;
            }

            if (!keyObj.KeyIsGlobal && (keySite != null))
            {
                keySite.KeyName = txtKeyName.Text;
                keySite.KeyDisplayName = txtKeyDisplayName.Text;
                keySite.KeyDescription = txtKeyDescription.Text;
                keySite.KeyValidation = txtKeyValidation.Text;
                keySite.KeyCategoryID = keyObj.KeyCategoryID;
                keySite.SiteID = siteId;
                keySite.KeyOrder = keyObj.KeyOrder;
                keySite.KeyType = drpKeyType.SelectedValue;
                keySite.KeyEditingControlPath = keyObj.KeyEditingControlPath;
                keySite.KeyIsHidden = chkKeyIsHidden.Checked;
                keySite.KeyIsCustom = mIsCustomSetting;

                if (drpGeneration.Value >= 0)
                {
                    keySite.KeyLoadGeneration = drpGeneration.Value;
                }
                if (putNullValues)
                {
                    keySite.KeyValue = null;
                }
                SettingsKeyProvider.SetValue(keySite);
            }
            else
            {
                // Remove the site specific key as setting key isn't local any more
                SettingsKeyProvider.DeleteKey(keySite);
            }
        }
        return keyObj.KeyName;
    }


    /// <summary>
    /// Validates the form. If validation succeeds returns true, otherwise returns false.
    /// </summary>
    private bool IsValid()
    {
        bool res = true;

        // Validate form fields
        string errMsg = new Validator().NotEmpty(txtKeyName.Text.Trim(), ResHelper.GetString("general.requirescodename"))
            .NotEmpty(txtKeyDisplayName.Text.Trim(), ResHelper.GetString("general.requiresdisplayname"))
            .IsIdentificator(txtKeyName.Text.Trim(), GetString("General.ErrorCodeNameInIdentificatorFormat"))
            .Result;

        // Validate default value format
        if (!string.IsNullOrEmpty(DefaultValue))
        {
            switch (drpKeyType.SelectedValue)
            {
                case "double":
                    if (!ValidationHelper.IsDouble(DefaultValue))
                    {
                        lblDefValueError.Text = ResHelper.GetString("settings.validationdoubleerror");
                        lblDefValueError.Visible = true;
                        res = false;
                    }
                    break;

                case "int":
                    if (!ValidationHelper.IsInteger(DefaultValue))
                    {
                        lblDefValueError.Text = ResHelper.GetString("settings.validationinterror");
                        lblDefValueError.Visible = true;
                        res = false;
                    }
                    break;
            }
        }

        // Set up error message
        if (!string.IsNullOrEmpty(errMsg))
        {
            lblError.Text = errMsg;
            res = false;
        }
        return res;
    }


    /// <summary>
    /// Shows suitable default value edit control accordint to key type from drpKeyType.
    /// </summary>
    private void SelectDefaultValueControl()
    {
        chkKeyValue.Visible = drpKeyType.SelectedValue == "boolean";
        txtKeyValue.Visible = !chkKeyValue.Visible;
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles OnClick event of btnOK.
    /// </summary>
    /// <param name="sender">Asp Button instance</param>
    /// <param name="e">EventArgs instance</param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            // Try to get SettingsKey object by name
            SettingsKeyInfo sk = SettingsKeyProvider.GetSettingsKeyInfo(txtKeyName.Text.Trim());
            if ((sk == null) || (sk.KeyID == mSettingsKeyId))
            {
                SettingsKeyInfo ski = (mSettingsKeyId > 0) ? SettingsKeyProvider.GetSettingsKeyInfo(mSettingsKeyId) : null;
                if (ski == null)
                {
                    ski = new SettingsKeyInfo();
                    UpdateAllSitesKey(txtKeyName.Text.Trim(), ski, true);
                }
                else
                {
                    UpdateAllSitesKey(ski.KeyName, ski, false);
                }
                mSettingsKeyId = ski.KeyID;
                RaiseOnSaved();

                // Set the info message
                lblInfo.Text = GetString("general.changessaved");

                // Select 'Keep current settings' option for load generation property
                drpGeneration.Value = -1;

                if ((TreeRefreshUrl != null) || (HeaderRefreshUrl != null))
                {
                    int parentCatForGroupId = mSelectedGroupId >= 0 ? mSelectedGroupId : drpCategory.SelectedCategory;
                    SettingsCategoryInfo parentCategoryForGroup = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(parentCatForGroupId);
                    if (parentCategoryForGroup != null)
                    {
                        int categoryIdToShow = parentCategoryForGroup.CategoryParentID;

                        StringBuilder sb = new StringBuilder();
                        sb.Append("if (window.parent != null) {");
                        if (HeaderRefreshUrl != null)
                        {
                            sb.Append("if (window.parent.parent.frames['customsettingscategorytabs'] != null) {");
                            sb.Append("window.parent.parent.frames['customsettingscategorytabs'].location = '" + ResolveUrl(HeaderRefreshUrl) + "categoryid=" + categoryIdToShow + "';");
                            sb.Append("}");
                            sb.Append("if (window.parent.frames['customsettingscategorytabs'] != null) {");
                            sb.Append("window.parent.frames['customsettingscategorytabs'].location = '" + ResolveUrl(HeaderRefreshUrl) + "categoryid=" + categoryIdToShow + "';");
                            sb.Append("}");
                        }
                        if (TreeRefreshUrl != null)
                        {
                            sb.Append("if (window.parent.parent.frames['customsettingstree'] != null) {");
                            sb.Append("window.parent.parent.frames['customsettingstree'].location = '" + ResolveUrl(TreeRefreshUrl) + "categoryid=" + categoryIdToShow + "';");
                            sb.Append("}");
                            sb.Append("if (window.parent.frames['customsettingstree'] != null) {");
                            sb.Append("window.parent.frames['customsettingstree'].location =  '" + ResolveUrl(TreeRefreshUrl) + "categoryid=" + categoryIdToShow + "';");
                            sb.Append("}");
                        }
                        sb.Append("}");
                        ltlScript.Text = ScriptHelper.GetScript(sb.ToString());
                    }
                }
            }
            else
            {
                lblError.Text = ResHelper.GetString("general.codenameexists");
            }
        }
    }


    protected void drpKeyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectDefaultValueControl();
    }

    #endregion
}