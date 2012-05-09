using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.URLRewritingEngine;
using CMS.EventLog;
using CMS.Scheduler;


public partial class CMSModules_Settings_Controls_SettingsGroupViewer : CMSAdminEditControl, ICallbackEventHandler
{
    #region "Private Variables"

    private int mCategoryId = 0;
    private string mCategoryName = null;
    private int mSiteId = 0;
    private string mSiteName = string.Empty;
    private SiteInfo mSiteInfo = null;
    private string mWhere = null;
    private bool mShowExportLink = true;
    private bool mAllowGlobalInfoMessage = true;
    private SettingsCategoryInfo mSettingsCategoryInfo = null;
    private readonly List<SettingsKeyItem> mKeyItems = new List<SettingsKeyItem>();
    private string mCallBackResult = string.Empty;
    private const string UNDEFINED_VALUE_ELEMENT_CLIENT_ID = "##UNDEFINED##";

    private string mSearchText = "";
    private bool mSearchDescription = true;
    private int mSearchLimit = 2;


    #endregion


    #region "Events"

    // Occurs when info message is changed. If not assigned, control uses its own label (lblSaved).
    public delegate void InfoMessageChangedHandler(string message, bool visible, bool isError);

    public event InfoMessageChangedHandler OnInfoMessageChanged;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets SettingsCategoryInfo object for the specified CategoryID or CategoryName respectively.
    /// </summary>
    public SettingsCategoryInfo SettingsCategoryInfo
    {
        get
        {
            if (mSettingsCategoryInfo == null)
            {
                if (mCategoryId > 0)
                {
                    mSettingsCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(mCategoryId);
                }
                else
                {
                    if (mCategoryName != null)
                    {
                        mSettingsCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(mCategoryName);
                    }
                }
            }
            return mSettingsCategoryInfo;
        }
    }


    /// <summary>
    /// Gets the settings keys list for the current category.
    /// </summary>
    public List<SettingsKeyItem> KeyItems
    {
        get
        {
            return mKeyItems;
        }
    }


    /// <summary>
    /// Settings category ID.
    /// </summary>
    public int CategoryID
    {
        get
        {
            if ((mCategoryId == 0) && (SettingsCategoryInfo != null))
            {
                mCategoryId = SettingsCategoryInfo.CategoryID;
            }

            return mCategoryId;
        }
        set
        {
            mCategoryId = value;
            mCategoryName = null;
            mSettingsCategoryInfo = null;
        }
    }


    /// <summary>
    /// Settings category name.
    /// </summary>
    public string CategoryName
    {
        get
        {
            if ((mCategoryName == null) && (SettingsCategoryInfo != null))
            {
                mCategoryName = SettingsCategoryInfo.CategoryName;
            }

            return mCategoryName;
        }
        set
        {
            mCategoryName = value;
            mCategoryId = 0;
            mSettingsCategoryInfo = null;
        }
    }


    /// <summary>
    /// Site info object for configured site.
    /// </summary>
    public SiteInfo SiteInfo
    {
        get
        {
            if (mSiteInfo == null)
            {
                if (mSiteId != 0)
                {
                    mSiteInfo = SiteInfoProvider.GetSiteInfo(mSiteId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(mSiteName))
                    {
                        mSiteInfo = SiteInfoProvider.GetSiteInfo(mSiteName);
                    }
                }
            }

            return mSiteInfo;
        }
    }


    /// <summary>
    /// ID of the site.
    /// </summary>
    public int SiteID
    {
        get
        {
            if ((mSiteId == 0) && SiteInfo != null)
            {
                mSiteId = SiteInfo.SiteID;
            }
            return mSiteId;
        }
        set
        {
            mSiteId = value;
            mSiteName = string.Empty;
            mSiteInfo = null;
        }
    }


    /// <summary>
    /// Code name of the site.
    /// </summary>
    public string SiteName
    {
        get
        {
            if (string.IsNullOrEmpty(mSiteName) && SiteInfo != null)
            {
                mSiteName = SiteInfo.SiteName;
            }

            return mSiteName;
        }
        set
        {
            mSiteName = value;
            mSiteId = 0;
            mSiteInfo = null;
        }
    }


    /// <summary>
    /// Where condition used to filter settings groups. All groups will be selected if not set.
    /// </summary>
    public string Where
    {
        get
        {
            return mWhere;
        }
        set
        {
            mWhere = value;
        }
    }


    /// <summary>
    /// Indicates if export link should be displayed (default true);.
    /// </summary>
    public bool ShowExportLink
    {
        get
        {
            return mShowExportLink;
        }
        set
        {
            mShowExportLink = value;
        }
    }


    /// <summary>
    /// Indicates if "these settings are global..." message can shown. Default value is true.
    /// </summary>
    public bool AllowGlobalInfoMessage
    {
        get
        {
            return mAllowGlobalInfoMessage;
        }
        set
        {
            mAllowGlobalInfoMessage = value;
        }
    }

    #endregion


    #region "Structures"

    /// <summary>
    /// Settings key information for the form controls.
    /// </summary>
    public struct SettingsKeyItem
    {
        public string SettingsKey; // Settings key code name
        public string InputControlID; // Checkbox id, textbox id
        public string InheritID; // Inherit checkbox id
        public bool IsInherited;
        public string ErrorLabelID; // Error label id
        public string Value; // Value
        public string Type; // Type (int, boolean)
        public string Validation; // Regex for validation
        public bool Changed; // Changed flag
        public string ParentCategoryPanelID; // Id of the parent category panel
        public string ValueElementClientID; // Id of the value element on the client
        public string UpdatePanelClientID; // Client ID of the update panel in the formengineusercontrol
        public string ControlPath; // Path to the user control
        public string AdditionalValueElementID; // Other client ids of control that need to be set on callback
    }

    #endregion


    #region "Public Methods"

    /// <summary>
    /// Saves changes made to settings keys into the database.
    /// </summary>
    public void SaveChanges()
    {
        bool validationFailed = false;

        // Loop through all settings items
        for (int i = 0; i < mKeyItems.Count; i++)
        {
            SettingsKeyItem item = mKeyItems[i];
            // Find parent CategoryPanel
            CategoryPanel p = plcContent.FindControl(item.ParentCategoryPanelID) as CategoryPanel;
            if (p != null)
            {
                // Find input control (TextBox, CheckBox)
                Control ctl = p.FindControl(item.InputControlID);
                if ((ctl == null) || string.IsNullOrEmpty(ctl.ID))
                {
                    continue;
                }

                bool changed = false;
                if (ctl is CMSTextBox)
                {
                    CMSTextBox tb = (CMSTextBox)ctl;
                    // Trim text value
                    tb.Text = tb.Text.Trim();
                    changed = (tb.Text != item.Value);
                    item.Value = tb.Text;
                }
                else if (ctl is TextBox)
                {
                    TextBox tb = (TextBox)ctl;
                    // Trim text value
                    tb.Text = tb.Text.Trim();
                    changed = (tb.Text != item.Value);
                    item.Value = tb.Text;
                }
                else if (ctl is CheckBox)
                {
                    CheckBox cb = (CheckBox)ctl;
                    changed = (cb.Checked.ToString() != item.Value);
                    item.Value = cb.Checked.ToString();
                }
                else
                {
                    FormEngineUserControl control = ctl as FormEngineUserControl;
                    if (control != null)
                    {
                        if (control.IsValid())
                        {
                            changed = Convert.ToString(control.Value) != item.Value;
                            item.Value = Convert.ToString(control.Value);
                        }
                        else
                        {
                            Label lblError = (Label)p.FindControl(item.ErrorLabelID);
                            lblError.Text = GetString("Settings.ValidationError");
                            return;
                        }
                    }
                }

                // Check inherited CheckBox (if there is any)
                if (!string.IsNullOrEmpty(item.InheritID))
                {
                    Control checkBox = p.FindControl(item.InheritID);
                    if ((checkBox != null) && (checkBox is CheckBox))
                    {
                        bool inheritanceChanged = ((checkBox as CheckBox).Checked != item.IsInherited);
                        changed = inheritanceChanged || !item.IsInherited && changed;
                        item.IsInherited = (checkBox as CheckBox).Checked;
                    }
                }

                item.Changed = changed;

                if (item.IsInherited)
                {
                    FormEngineUserControl control = ctl as FormEngineUserControl;
                    if (control != null)
                    {
                        control.Value = SettingsKeyProvider.GetValue(item.SettingsKey);
                    }

                    if (ctl is CheckBox)
                    {
                        ((CheckBox)ctl).Checked = SettingsKeyProvider.GetBoolValue(item.SettingsKey);
                    }
                }

                if (changed)
                {
                    // Validation result
                    string result = string.Empty;

                    // Validation using regular expression if there is any
                    if (!string.IsNullOrEmpty(item.Validation) && (item.Validation.Trim() != string.Empty))
                    {
                        result = new Validator().IsRegularExp(item.Value, item.Validation, GetString("Settings.ValidationRegExError")).Result;
                    }

                    // Validation according to the value type (validate only nonempty values)
                    if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(item.Value))
                    {
                        switch (item.Type.ToLower())
                        {
                            case "int":
                                result = new Validator().IsInteger(item.Value, GetString("Settings.ValidationIntError")).Result;
                                break;

                            case "double":
                                result = new Validator().IsDouble(item.Value, GetString("Settings.ValidationDoubleError")).Result;
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        Label lblError = (Label)p.FindControl(item.ErrorLabelID);
                        lblError.Text = result;
                        validationFailed = true;
                    }
                    else
                    {
                        // Update changes
                        mKeyItems[i] = item;
                    }
                }
            }
        }

        if (validationFailed)
        {
            SetInfoMessage(GetString("general.saveerror"), true, true);
            return;
        }


        // Update changes in database/hashtables
        bool logSynchronization = (mSettingsCategoryInfo.CategoryName.ToLower() != "cms.staging");

        bool clearCache = false;
        bool clearOutputCache = false;
        bool clearCSSCache = false;
        bool clearPartialCache = false;

        string serviceBaseName = null;
        bool serviceEnabled = false;

        foreach (SettingsKeyItem tmpItem in mKeyItems)
        {
            if (tmpItem.Changed)
            {
                object keyValue = DBNull.Value;
                if (!tmpItem.IsInherited)
                {
                    keyValue = tmpItem.Value;
                }
                string keyName = tmpItem.SettingsKey;

                ObjectHelper.SetSettingsKeyValue(GetFullKeyName(keyName), keyValue, logSynchronization);

                // Clear the cached items
                switch (keyName.ToLower())
                {
                    case "cmsgooglesitemapurl":
                        // Google site map
                        URLRewriter.GoogleSiteMapURL = null;
                        break;

                    case "cmsemailsenabled":
                        if ((mSiteId <= 0) && (keyValue.ToString().Equals("false", StringComparison.OrdinalIgnoreCase)))
                        {
                            // Stop current sending of e-mails and newsletters if e-mails are disabled in global settings
                            ModuleCommands.CancelEmailSending();
                            ModuleCommands.CancelNewsletterSending();
                        }
                        break;

                    case "cmslogsize":
                        // Log size changed
                        EventLogProvider.Clear();
                        break;

                    case "cmscacheminutes":
                    case "cmscachepageinfo":
                    case "cmscacheimages":
                    case "cmsmaxcachefilesize":
                    case "cmsdefaultaliaspath":
                    case "cmsdefaultculturecode":
                    case "cmscombinewithdefaultculture":
                        // Clear cache upon change
                        clearCache = true;
                        break;

                    case "cmspagekeywordsprefix":
                    case "cmspagedescriptionprefix":
                    case "cmspagetitleformat":
                    case "cmspagetitleprefix":
                    case "cmscontrolelement":
                    case "cmsenableoutputcache":
                    case "cmsfilesystemoutputcacheminutes":
                        // Clear output cache upon change
                        clearOutputCache = true;
                        break;

                    case "cmsenablepartialcache":
                        // Clear output cache upon change
                        clearPartialCache = true;
                        break;

                    case "cmsresourcecompressionenabled":
                    case "cmsstylesheetminificationenabled":
                    case "cmsresolvemacrosincss":
                        // Clear the CSS styles
                        clearCSSCache = true;
                        break;

                    case "cmsuseexternalservice":
                    case "cmsservicehealthmonitoringinterval":
                    case "cmsenablehealthmonitoring":
                        // Restart Health Monitoring service
                        {
                            serviceBaseName = WinServiceHelper.HM_SERVICE_BASENAME;
                            serviceEnabled = HealthMonitoringHelper.UseExternalService;

                            // Clear status of health monitoring
                            HealthMonitoringHelper.Clear();
                        }
                        break;

                    case "cmsscheduleruseexternalservice":
                    case "cmsschedulerserviceinterval":
                        // Restart Scheduler service
                        serviceBaseName = WinServiceHelper.SCHEDULER_SERVICE_BASENAME;
                        serviceEnabled = SchedulingHelper.UseExternalService;
                        break;
                }
            }
        }

        // Clear the cache to apply the settings to the web site
        if (clearCache)
        {
            CacheHelper.ClearCache(null);
        }
        // Restart windows service
        else if (serviceEnabled && (serviceBaseName != null))
        {
            try
            {
                WinServiceItem def = WinServiceHelper.GetServiceDefinition(serviceBaseName);
                if (def != null)
                {
                    WinServiceHelper.RestartService(def.GetServiceName());
                }
            }
            catch (Exception ex)
            {
                EventLogProvider.LogException("Settings", "RestartService", ex);
            }
        }
        else
        {
            // Clear only cache portions
            if (clearOutputCache)
            {
                CacheHelper.ClearFullPageCache();
            }
            if (clearCSSCache)
            {
                CacheHelper.ClearCSSCache();
            }
            if (clearPartialCache)
            {
                CacheHelper.ClearPartialCache();
            }
        }

        SetInfoMessage(GetString("general.changessaved"), true, false);
    }


    /// <summary>
    /// Resets all SettingsKey values in the current category to the default ones.
    /// </summary>
    public void ResetToDefault()
    {
        if (SettingsCategoryInfo != null)
        {
            // Retrieve all groups for the current category
            foreach (SettingsCategoryInfo group in GetGroups())
            {

                // Get keys
                IEnumerable<SettingsKeyInfo> keys = GetKeys(group.CategoryID);

                // Filter
                if ((!string.IsNullOrEmpty(mSearchText)) && (mSearchText.Length >= mSearchLimit))
                {
                    keys = GetSearchedKeys(keys);
                }


                // Retrieve all keys for the current group
                foreach (SettingsKeyInfo key in keys)
                {
                    key.KeyValue = key.KeyDefaultValue;
                    SettingsKeyProvider.SetValue(key);
                }
            }
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterTooltip(Page);
        if (SettingsCategoryInfo == null)
        {
            plcContent.Controls.Add(new LiteralControl(GetString("settings.keys.nocategoryselected")));
            return;
        }

        if (!RequestHelper.IsCallback())
        {
            ClientScriptManager sm = Page.ClientScript;
            if (sm != null)
            {
                string cbRef = sm.GetCallbackEventReference(this, "arg", "OnCallbacked", "");
                string cbScript = string.Format(@"function InheritCheckChanged(arg, context) {{ {0}; }}", cbRef);
                ScriptHelper.RegisterClientScriptBlock(this, GetType(), "InheritCheckChanged", ScriptHelper.GetScript(cbScript));
                const string script = @"
                function GetElementType(element) 
                {
                    var type = null;
                    if (element != null) 
                    {
                        // If current element is table, check its children for the type
                        if (element.nodeName.toLowerCase() == 'table') 
                        {
                            var childs = element.getElementsByTagName('input');
                            if ((childs != null) && (childs.length > 0)) 
                            {
                                // Element is radiobuttonlist or checkboxlist
                                type = childs[0].type.toLowerCase() + 'list';
                            }
                        }
                        else 
                        {
                            if(element.type)
                            {
                                // Basic element
                                type = element.type.toLowerCase();
                            }
                            else
                            {
                                var childs = element.getElementsByTagName('input');
                                if ((childs != null) && (childs.length > 0)) 
                                {
                                    // Element is radiobuttonlist or checkboxlist
                                    type = childs[0].type.toLowerCase() + 'list';
                                }
                            }
                        }
                    }
                    return type;
                }
                function SetElementValue(elementId, value)
                {
                    var element = document.getElementById(elementId);
                    var elementType = GetElementType(element);
                    if((element != null) && (elementType != null))
                    {
                        // Take different actions on different value element type
                        switch(elementType)
                        {
                            case 'text':
                            case 'textarea':
                            case 'password':
                            case 'hidden':
                                element.value = value;
                                //alert(element + ' of type \'' + elementType + '\' set to \'' + value + '\'');
                                break;
                            case 'checkbox':
                            case 'radio':
                                element.checked = ((value.toLowerCase() == 'true') || (value.toLowerCase() == '1'));
                                break;
                            case 'select-one':
                            case 'select-multiple':
                                for (var i = 0; i < element.options.length; i++) 
                                {
                                    //alert('option value: ' + element.options[i].value);
                                    //alert('option text: ' + element.options[i].text);
                                    if (element.options[i].value == value) 
                                    {
                                        element.options[i].selected = true;
                                        break;
                                    }
                                    // Select the top first option if no option has been found
                                    element.options[0].selected = true;
                                }
                                break;
                            case 'radiolist':
                                var childs = element.getElementsByTagName('input');
                                // Get all input elements
                                if ((childs != null) && (childs.length > 0)) 
                                {
                                    var valueFound = false;
                                    for (var i = 0; i < childs.length; i++) 
                                    {
                                        if(childs[i].value == value)
                                        {
                                            // Element with value was found so check it
                                            childs[i].checked = true;
                                            valueFound = true;
                                            break;
                                        }
                                    }
                                    if(!valueFound)
                                    {
                                        // Element was not found, so check the first one
                                        childs[0].checked = true;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                function OnCallbacked(arg, context)
                {
                    var args = arg.split('|');
                    // Check if ClientID was defined for the control
                    if(args[3].toLowerCase() == 'true')
                    {
                        return;
                    }
                    // Get handle for the value element
                    var valElement = document.getElementById(args[0]);
                    var elementType = GetElementType(valElement);
                    if((valElement != null) && (elementType != null))
                    {
                        var value = decodeURI(args[1]);
                        SetElementValue(args[0], value);
                        // Form engine user control in update panel should be prerendered, not just enabled
                        // Get handler for the additional value element
                        var addElement = document.getElementById(args[4]);
                        var addElementType = GetElementType(addElement);
                        if((addElement != null) && (addElementType != null))
                        {
                            SetElementValue(args[4], args[5]);
                        }
                        if((args[2].length > 0) && (document.getElementById(args[2]) != null))
                        {
                            __doPostBack(args[2], '');
                        }
                        else
                        {
                            // Disable value element at the end
                            valElement.disabled = true;
                            var valElementType = GetElementType(valElement);
                            // different disabling for radiobuttonlist and checkboxlist
                            if(valElementType == 'radiolist')
                            {
                                EnableElementChildren(valElement, true);
                            }
                        }
                    }
                }

                function EnableElementChildren(element, disabled) 
                {
                    if(element != null) 
                    {
                        element.disabled = disabled;
                        var childs = element.getElementsByTagName('input');
                        if ((childs != null) && (childs.length > 0)) 
                        {
                            for (var i = 0; i < childs.length; i++) 
                            {
                                // Enable all children
                                childs[i].disabled = disabled;
                                try
                                {
                                   childs[i].parentNode.disabled = disabled;
                                }
                                catch(e) {}
                            }
                        }
                    }
                }


                function InheritCheckBox_Changed(id, valueElementId, updatePanelId, controlPath)
                {
                    // Get handle for inherit checkbox
                    var cb = document.getElementById(id);
                    if(cb != null)
                    {
                        if(cb.checked)
                        {
                            // Prepare arguments for callback and do callback
                            var args = valueElementId + '|' + controlPath;
                            InheritCheckChanged(args, '');
                        }
                        else
                        {
                            var valElement = document.getElementById(valueElementId);                            
                            // Form engine user control in update panel should be prerendered, not just enabled
                            if((updatePanelId.length > 0) && (document.getElementById(updatePanelId) != null))
                            {
                                __doPostBack(updatePanelId, '');
                            }
                            else if(valElement != null)
                            {
                                // Enable value element control
                                valElement.disabled = false;
                                var valElementType = GetElementType(valElement);
                                // Additional enabling for checkbox
                                if((valElementType == 'checkbox') && (valElement.parentNode != null))
                                {
                                    valElement.parentNode.disabled = false;
                                }
                                // different enabling for radiobuttonlist and checkboxlist
                                if(valElementType == 'radiolist')
                                {
                                    EnableElementChildren(valElement, false);
                                }
                            }
                        }
                    }
                }";
                ScriptHelper.RegisterClientScriptBlock(this, GetType(), "InheritCheckBox_Changed", ScriptHelper.GetScript(script));
            }
        }

        if (mSettingsCategoryInfo == null)
        {
            return;
        }

        if ((mSiteId <= 0) && (CategoryID > 0) && AllowGlobalInfoMessage)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("settings.keys.globalsettingsnote");
        }

        int groupNo = 0;
        bool hasOnlyGlobalSettings = true;

        mSearchText = QueryHelper.GetString("search", "").Trim();
        mSearchDescription = QueryHelper.GetBoolean("description", false);

        // Loop through all groups for current category
        foreach (SettingsCategoryInfo group in GetGroups())
        {
            // Get keys
            IEnumerable<SettingsKeyInfo> keys = GetKeys(group.CategoryID);

            if ((!string.IsNullOrEmpty(mSearchText)) && (mSearchText.Length >= mSearchLimit))
            {
                // Get searched keys
                keys = GetSearchedKeys(keys);
            }

            if (keys != null)
            {
                bool categoryPanelIsAdded = false;
                int keyNo = 0;
                CategoryPanel p = null;
                bool getCategoryPanel = true;

                // Loop through all setting keys in the current group
                foreach (SettingsKeyInfo key in keys)
                {
                    // Get category panel only if it has some keys
                    if (getCategoryPanel)
                    {
                        p = GetCategoryPanel(group, groupNo); // Create category panel for the group
                        getCategoryPanel = false;
                        groupNo++; // Increase group number for unique control identification
                    }


                    // Update flag when non-global-only key exists
                    if (!key.KeyIsGlobal)
                    {
                        hasOnlyGlobalSettings = false;
                    }

                    if (!categoryPanelIsAdded)
                    {
                        // Add category panel to the placeholder control collection
                        plcContent.Controls.Add(p);
                        categoryPanelIsAdded = true;
                    }
                    keyNo++; // Increase key number for unique control identification
                    // Add display name lable
                    AddControl(p, GetLabel(key, groupNo, keyNo), @"<tr class=""EditingFormRow""><td class=""EditingFormLeftBorder"">&nbsp;</td><td class=""EditingFormLabelCell"" style=""width:250px;"">", @"</td>");
                    // Add help image
                    AddControl(p, GetHelpImage(key, groupNo, keyNo), @"<td style=""width:25px"">", @"</td>");

                    SettingsKeyItem skr = new SettingsKeyItem();
                    skr.ParentCategoryPanelID = p.ID; // Assign category panel ID to be able to find all controls later
                    skr.SettingsKey = key.KeyName;

                    CheckBox chkInherit = GetInheritCheckBox(groupNo, keyNo, out skr.InheritID);
                    bool inheritChecked = false;
                    skr.ValueElementClientID = string.Empty;
                    FormEngineUserControl control = GetFormEngineUserControl(key, groupNo, keyNo);

                    // Add placeholder for the editing control
                    PlaceHolder plcControl = new PlaceHolder();
                    plcControl.ID = string.Format("plcControl_{0}{1}", groupNo, keyNo);
                    AddControl(p, plcControl, @"<td class=""EditingFormValueCell"" style=""width:400px"">", @"</td>");

                    // Add inherit CheckBox
                    AddControl(p, chkInherit, @"<td>", @"</td>");

                    if (control != null)
                    {
                        string defValue = SettingsKeyProvider.GetValue(key.KeyName.ToLower());
                        string value = null;
                        if ((key.KeyValue == null) && (mSiteId > 0))
                        {
                            inheritChecked = true;
                            value = defValue;
                        }
                        else
                        {
                            inheritChecked = false;
                            value = SettingsKeyProvider.GetValue(GetFullKeyName(key.KeyName).ToLower());
                        }
                        control.Value = value;
                        control.Enabled = (URLHelper.IsPostback() && (chkInherit != null)) ? (Request.Form[chkInherit.UniqueID] == null) : !inheritChecked;
                        plcControl.Controls.Add(control);
                        skr.InputControlID = control.ID;
                        skr.Value = value;
                        // Not all engine controls have ValueElementID, when it is not assigned in the structe it will fail during find
                        skr.ValueElementClientID = control.ValueElementID ?? (control.InputClientID ?? UNDEFINED_VALUE_ELEMENT_CLIENT_ID);
                        skr.UpdatePanelClientID = GetUpdatePanelClientId(control);
                        skr.ControlPath = key.KeyEditingControlPath;
                    }
                    else
                    {
                        switch (key.KeyType.ToLower())
                        {
                            case "boolean":
                                CheckBox cbVal = GetCheckBox(key, groupNo, keyNo, out inheritChecked, chkInherit, out skr.InputControlID, out skr.Value);
                                plcControl.Controls.Add(cbVal);
                                skr.ValueElementClientID = cbVal.ClientID;
                                skr.ControlPath = "";
                                break;

                            default:
                                TextBox tbVal = GetTextBox(key, groupNo, keyNo, out inheritChecked, chkInherit, out skr.InputControlID, out skr.Value);
                                plcControl.Controls.Add(tbVal);
                                skr.ValueElementClientID = tbVal.ClientID;
                                skr.ControlPath = "";
                                break;
                        }
                    }
                    // Set the inherit checkbox state
                    if (chkInherit != null)
                    {
                        chkInherit.Checked = !URLHelper.IsPostback() ? inheritChecked : chkInherit.Checked;
                        chkInherit.Attributes.Add("onclick", string.Format(@"InheritCheckBox_Changed(this.id, ""{0}"", ""{1}"", ""{2}"")", skr.ValueElementClientID, skr.UpdatePanelClientID, skr.ControlPath));
                    }

                    // Set the inherited status
                    skr.IsInherited = inheritChecked;

                    p.Controls.Add(GetLabelKeyName(key, groupNo, keyNo)); // Add key name Label

                    skr.Validation = ValidationHelper.GetString(key.KeyValidation, null);
                    if ((mSiteId > 0) && (skr.Validation == null))
                    {
                        SettingsKeyInfo ski = SettingsKeyProvider.GetSettingsKeyInfo(skr.SettingsKey, 0);
                        if (ski != null)
                        {
                            skr.Validation = ski.KeyValidation;
                        }
                    }

                    Label lblError = null;
                    // Add error label if KeyType is integer or validation expression defined or FormControl is used
                    if ((key.KeyType == "int") || (key.KeyType == "double") || (skr.Validation != null) || (control != null))
                    {
                        lblError = GetLabelError(groupNo, keyNo);
                        p.Controls.Add(lblError);
                        skr.ErrorLabelID = lblError.ID;
                    }
                    AddControl(p, lblError, @"<td>", @"</td>");
                    AddLiteral(p, @"<td class=""EditingFormRightBorder"">&nbsp;</td>");

                    skr.Type = key.KeyType;
                    mKeyItems.Add(skr);
                }
                //  AddLiteral(p, @"</table>");
            }
        }

        // Hide info message when only global-only keys are displayed
        if (hasOnlyGlobalSettings)
        {
            lblInfo.Visible = false;
        }

        // Display export and reset links only if some groups were found.
        if (groupNo > 0)
        {
            // Add Export settings link, but only if some category is specified
            if (SettingsCategoryInfo != null && ShowExportLink)
            {
                plcContent.Controls.Add(new LiteralControl(string.Format(@"<a href=""GetSettings.aspx?siteid={0}&categoryid={1}&search={2}&description={3}"">{4}</a>",
                                                         mSiteId, SettingsCategoryInfo.CategoryID, mSearchText, mSearchDescription, GetString("settings.keys.exportsettings"))));

            }

            if (QueryHelper.GetInteger("resettodefault", 0) == 1)
            {
                SetInfoMessage(GetString("Settings-Keys.ValuesWereResetToDefault"), true, false);
            }
        }
        else
        {
            SetInfoMessage("", false, false);
            // Hide "These settings are global..." message if no setting found in this group

            if (!string.IsNullOrEmpty(mSearchText))
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("settingskey.nodata");

                string script = @"
                function DisableHeaderActions(){
                        var element = document.getElementById('m_pnlActions');
                        element.style.display = 'none';
                    }
                DisableHeaderActions();
                ";

                Literal ltrScript = new Literal();
                ltrScript.Text = ScriptHelper.GetScript(script);
                plcContent.Controls.Add(ltrScript);

            }
            else
            {
                lblInfo.Visible = false;
            }

        }
    }


    /// <summary>
    /// Gets searched keys only
    /// </summary>
    /// <param name="keys"><c>IEnumerable</c> of <c>SettingsKeyInfo</c>.</param>
    /// <param name="searchDescription"></param>
    /// <returns><c>IEnumerable</c> of searched<c>SettingsKeyInfo</c>.</returns>
    private IEnumerable<SettingsKeyInfo> GetSearchedKeys(IEnumerable<SettingsKeyInfo> keys)
    {
        foreach (SettingsKeyInfo key in keys)
        {
            string keyDisplayName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(key.KeyDisplayName));
            string keyDescription = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(key.KeyDescription));
            if ((keyDisplayName.ToLower().Contains(mSearchText.ToLower())) || ((mSearchDescription) && (keyDescription.ToLower().Contains(mSearchText.ToLower()))))
            {
                yield return key;
            }
        }
    }



    #endregion


    #region "Private Methods"

    /// <summary>
    /// Gets ClientID of the firts control in the input ctl controls collection that is of type UpdatePanel.
    /// </summary>
    /// <param name="ctl">Control which should contain UpdatePanel</param>
    /// <returns>ClientID of the first UpdatePanel in the controls collection of the input control.</returns>
    private string GetUpdatePanelClientId(Control ctl)
    {
        foreach (Control c in ctl.Controls)
        {
            if (c is UpdatePanel)
            {
                return c.ClientID;
            }
            if (c.Controls.Count > 0)
            {
                return GetUpdatePanelClientId(c);
            }
        }
        return string.Empty;
    }


    /// <summary>
    /// Gets <c>SettingsKeyItem</c> object from the list for the input clientID.
    /// </summary>
    /// <param name="clientID">ClientID of the user control</param>
    /// <returns><c>SettingsKeyItem</c> object.</returns>
    private SettingsKeyItem GetKeyItemByClientID(string clientID)
    {
        return mKeyItems.Find(x => x.ValueElementClientID.Equals(clientID));
    }


    /// <summary>
    /// Returns settings key full name.
    /// </summary>
    /// <param name="keyName">Settings key</param>
    private string GetFullKeyName(string keyName)
    {
        if (mSiteId > 0)
        {
            return string.Format(@"{0}.{1}", SiteName, keyName);
        }
        return keyName;
    }


    /// <summary>
    /// Adds input text as <c>LiteralControl</c> to the controls colection of the input <c>CategoryPanel</c>.
    /// </summary>
    /// <param name="p"><c>CategoryPanel</c> instance</param>
    /// <param name="text">Text representing <c>LiteralControl</c></param>
    private void AddLiteral(CategoryPanel p, string text)
    {
        p.Controls.Add(new LiteralControl(text));
    }


    /// <summary>
    /// Adds input control into the controls collection of the input category panel.
    /// </summary>
    /// <param name="p"><c>CategoryPanel</c> whose control collection should be modified</param>
    /// <param name="ctl">Control that should be added to the <c>CategoryPanel</c> controls collection</param>
    /// <param name="literalBefore">If not <c>null</c> new <c>LiteralControl</c> will be added before the control</param>
    /// <param name="literalAfter">If not <c>null</c> new <c>LiteralControl</c> will be added after the control</param>
    private void AddControl(CategoryPanel p, Control ctl, string literalBefore, string literalAfter)
    {
        if (!string.IsNullOrEmpty(literalBefore))
        {
            p.Controls.Add(new LiteralControl(literalBefore));
        }
        if (ctl != null)
        {
            p.Controls.Add(ctl);
        }
        if (!string.IsNullOrEmpty(literalAfter))
        {
            p.Controls.Add(new LiteralControl(literalAfter));
        }
    }


    /// <summary>
    /// Gets <c>FormEngineUserControl</c> instance for the input <c>SettingsKeyInfo</c> object.
    /// </summary>
    /// <param name="settingsKey"><c>SettingsKeyInfo</c> instance</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <returns><c>CheckBox</c> object.</returns>
    private FormEngineUserControl GetFormEngineUserControl(SettingsKeyInfo settingsKey, int groupNo, int keyNo)
    {
        FormEngineUserControl control = null;
        if (!string.IsNullOrEmpty(settingsKey.KeyEditingControlPath))
        {
            try
            {
                control = Page.LoadControl(settingsKey.KeyEditingControlPath) as FormEngineUserControl;
            }
            catch
            {
            }
        }

        // Handling exceptions
        if (control != null)
        {
            control.ID = string.Format(@"key{0}{1}", groupNo, keyNo);
            control.IsLiveSite = false;

            // Store value of editing control path
            string editingCtrlPath = null;
            if (!String.IsNullOrEmpty(settingsKey.KeyEditingControlPath))
            {
                editingCtrlPath = settingsKey.KeyEditingControlPath.ToLower();
            }

            // Apply additional styles or set properties to the specific controls
            switch (editingCtrlPath)
            {
                case "~/cmsmodules/ecommerce/formcontrols/checkoutprocess.ascx":
                    SetStyle(control, "pnlContent", "panel", "padding:0px 0px 0px 0px");
                    SetStyle(control, "pnlHeaderLine", "panel", "padding:0px 0px 0px 0px;border-bottom:0px solid #CCCCCC;background-color:transparent");
                    SetStyle(control, "Panel2", "panel", "padding:0px 0px 0px 0px");
                    SetStyle(control, "plcEditDiv", "htmlcontrol", "padding:0px 0px 0px 0px;border-bottom:0px solid #CCCCCC;background-color:transparent");

                    HelpControl helpElem = control.FindControl("helpElem") as HelpControl;
                    if (helpElem != null)
                    {
                        HyperLink lnkHelp = helpElem.FindControl("lnkHelp") as HyperLink;
                        if (lnkHelp != null)
                        {
                            lnkHelp.Style["display"] = "none";
                        }
                    }
                    break;
                // Class names selectors
                case "~/cmsformcontrols/classes/selectclassnames.ascx":
                    control.SetValue("SiteID", 0);
                    break;
            }
        }
        return control;
    }


    /// <summary>
    /// Add style to the control defined by id.
    /// </summary>
    /// <param name="sourceControl">Source control containing the target control</param>
    /// <param name="id">Id of the target control</param>
    /// <param name="type">Type of the target control</param>
    /// <param name="styles">Style of the target control</param>
    private void SetStyle(FormEngineUserControl sourceControl, string id, string type, string styles)
    {
        if (type == "panel")
        {
            Panel p = sourceControl.FindControl(id) as Panel;
            if (p != null)
            {
                foreach (string style in styles.Split(';'))
                {
                    p.Style[style.Split(':')[0].Trim()] = style.Split(':')[1].Trim();
                }
            }
        }
        else if (type == "htmlcontrol")
        {
            HtmlControl p = sourceControl.FindControl(id) as HtmlControl;
            if (p != null)
            {
                foreach (string style in styles.Split(';'))
                {
                    p.Style[style.Split(':')[0].Trim()] = style.Split(':')[1].Trim();
                }
            }
        }
    }


    /// <summary>
    /// Gets <c>CategoryPanel</c> instance for the input settings group.
    /// </summary>
    /// <param name="group"><c>SettingsCategoryInfo</c> instance representing settings group</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <returns><c>CategoryPanel</c> object.</returns>
    private CategoryPanel GetCategoryPanel(SettingsCategoryInfo group, int groupNo)
    {
        CategoryPanel p = new CategoryPanel();
        p.ID = string.Format(@"CategoryPanel{0}", groupNo);
        p.DisplayActions = false;
        p.AllowCollapsing = false;

        if ((!string.IsNullOrEmpty(mSearchText)) && (mSearchText.Length >= mSearchLimit))
        {
            p.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(GetCategoryNames(group.CategoryIDPath)));
        }
        else
        {
            p.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(group.CategoryDisplayName));
        }
        return p;
    }


    /// <summary>
    /// Gets category names for given settings groupIDPath
    /// </summary>
    /// <param name="groupIDPath">Settings group IDPath</param>
    /// <returns>String with parent category names</returns>
    private string GetCategoryNames(string groupIDPath)
    {
        // Get parent category names
        DataSet parents = SettingsCategoryInfoProvider.GetSettingsCategories(SettingsCategoryInfoProvider.GetCategoriesOnPathWhereCondition(groupIDPath, true) + " AND (CategoryLevel > 0)", "CategoryLevel", -1, "CategoryDisplayName");

        if (!DataHelper.DataSourceIsEmpty(parents))
        {
            string result = "";
            foreach (DataRow parent in parents.Tables[0].Rows)
            {
                result += parent["CategoryDisplayName"] + " > ";
            }

            return result.Substring(0, result.LastIndexOf(">")).Trim();
        }
        return String.Empty;
    }



    /// <summary>
    /// Gets <c>CheckBox</c> control used for key value editing.
    /// </summary>
    /// <param name="settingsKey"><c>SettingsKeyInfo</c> instance representing the current processing key</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <param name="inheritChecked">Output parameter indicating whether the inherit <c>CheckBox</c> should be checked</param>
    /// <param name="chkInherit">Inherit <c>CheckBox</c> instance</param>
    /// <param name="inputControlID">Output parameter representing the ID of the edit control</param>
    /// <param name="value">Output parameter representing the value of the edit control</param>
    /// <returns><c>CheckBox</c> object.</returns>
    private CheckBox GetCheckBox(SettingsKeyInfo settingsKey, int groupNo, int keyNo, out bool inheritChecked, CheckBox chkInherit, out string inputControlID, out string value)
    {
        CheckBox chbox = new CheckBox();
        chbox.ID = string.Format("chkKey{0}{1}", groupNo, keyNo);
        chbox.EnableViewState = false;
        if (string.IsNullOrEmpty(settingsKey.KeyValue) && (mSiteId > 0))
        {
            inheritChecked = true;
            chbox.Checked = SettingsKeyProvider.GetBoolValue(settingsKey.KeyName.ToLower());
        }
        else
        {
            inheritChecked = false;
            chbox.Checked = SettingsKeyProvider.GetBoolValue(GetFullKeyName(settingsKey.KeyName).ToLower());
        }

        // Disable checkbox if inheriting from global settings
        if ((URLHelper.IsPostback() && (chkInherit != null)) ? (Request.Form[chkInherit.UniqueID] == null) : !inheritChecked)
        {
            chbox.InputAttributes.Remove("disabled");
        }
        else
        {
            chbox.InputAttributes.Add("disabled", "disabled");
        }

        inputControlID = chbox.ID;
        value = chbox.Checked.ToString();
        return chbox;
    }


    /// <summary>
    /// Gets <c>TextBox</c> control used for key value editing.
    /// </summary>
    /// <param name="settingsKey"><c>SettingsKeyInfo</c> instance representing the current processing key</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <param name="inheritChecked">Output parameter indicating whether the inherit <c>CheckBox</c> should be checked</param>
    /// <param name="chkInherit">Inherit <c>CheckBox</c> instance</param>
    /// <param name="inputControlID">Output parameter representing the ID of the edit control</param>
    /// <param name="value">Output parameter representing the value of the edit control</param>
    /// <returns><c>TextBox</c> object.</returns>
    private TextBox GetTextBox(SettingsKeyInfo settingsKey, int groupNo, int keyNo, out bool inheritChecked, CheckBox chkInherit, out string inputControlID, out string value)
    {
        CMSTextBox tbox = new CMSTextBox();
        tbox.ID = string.Format("txtKey{0}{1}", groupNo, keyNo);
        tbox.CssClass = "TextBoxField";
        tbox.EnableViewState = false;
        if ((settingsKey.KeyValue == null) && (mSiteId > 0))
        {
            inheritChecked = true;
            tbox.Text = SettingsKeyProvider.GetStringValue(settingsKey.KeyName);
        }
        else
        {
            inheritChecked = false;
            tbox.Text = ((chkInherit != null) && (Request.Form[chkInherit.UniqueID] == null)) ? settingsKey.KeyValue : SettingsKeyProvider.GetStringValue(settingsKey.KeyName);
        }
        tbox.Enabled = (URLHelper.IsPostback() && (chkInherit != null)) ? (Request.Form[chkInherit.UniqueID] == null) : !inheritChecked;
        inputControlID = tbox.ID;
        value = tbox.Text;
        return tbox;
    }


    /// <summary>
    /// Gets inherit <c>CheckBox</c> instance for the input <c>SettingsKeyInfo</c> object.
    /// </summary>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <param name="inheritID">ID of the inherit checkbox</param>
    /// <returns><c>CheckBox</c> object.</returns>
    private CheckBox GetInheritCheckBox(int groupNo, int keyNo, out string inheritID)
    {
        if (mSiteId > 0)
        {
            CheckBox cb = new CheckBox();
            cb.ID = string.Format(@"chkInherit{0}{1}", groupNo, keyNo);
            cb.AutoPostBack = false;
            cb.Text = GetString("settings.keys.checkboxinheritglobal");
            cb.EnableViewState = false;
            inheritID = cb.ID;
            return cb;
        }
        else
        {
            inheritID = string.Empty;
            // There is not needed any checkbox for global settings
            return null;
        }
    }


    /// <summary>
    /// Gets <c>Label</c> instance for the input <c>SettingsKeyInfo</c> object.
    /// </summary>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <returns><c>Label</c> object.</returns>
    private Label GetLabelError(int groupNo, int keyNo)
    {
        Label l = new Label();
        l.ID = string.Format(@"lblError{0}{1}", groupNo, keyNo);
        l.EnableViewState = false;
        l.CssClass = "ErrorLabel";
        l.Attributes.Add("style", "padding: 4px 0px 0px 0px");
        return l;
    }


    /// <summary>
    /// Gets <c>Label</c> instance for the input <c>SettingsKeyInfo</c> object.
    /// </summary>
    /// <param name="settingsKey"><c>SettingsKeyInfo</c> instance</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <returns><c>Label</c> object.</returns>
    private Label GetLabelKeyName(SettingsKeyInfo settingsKey, int groupNo, int keyNo)
    {
        Label l = new Label();
        l.EnableViewState = false;
        l.ID = string.Format(@"lblKeyName{0}{1}", groupNo, keyNo);
        l.Text = settingsKey.KeyName;
        l.Visible = false;
        return l;
    }


    /// <summary>
    /// Gets <c>Label</c> instance for the input <c>SettingsKeyInfo</c> object.
    /// </summary>
    /// <param name="settingsKey"><c>SettingsKeyInfo</c> instance</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <returns><c>Label</c> object.</returns>
    private Label GetLabel(SettingsKeyInfo settingsKey, int groupNo, int keyNo)
    {
        Label l = new Label();
        l.EnableViewState = false;
        l.ID = string.Format(@"lblDispName{0}{1}", groupNo, keyNo);
        l.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(settingsKey.KeyDisplayName));
        l.Attributes.Add("style", "cursor: help;");
        ScriptHelper.AppendTooltip(l, ResHelper.LocalizeString(settingsKey.KeyDescription), null);
        return l;
    }


    /// <summary>
    /// Gets <c>Image</c> instance for the input <c>SettingsKeyInfo</c> object.
    /// </summary>
    /// <param name="settingsKey"><c>SettingsKeyInfo</c> instance</param>
    /// <param name="groupNo">Number representing index of the proccesing settings group</param>
    /// <param name="keyNo">Number representing index of the proccesing <c>SettingsKeyInfo</c></param>
    /// <returns><c>Image</c> object.</returns>
    private Image GetHelpImage(SettingsKeyInfo settingsKey, int groupNo, int keyNo)
    {
        Image i = new Image();
        i.EnableViewState = false;
        i.ID = string.Format(@"imgHelp{0}{1}", groupNo, keyNo);
        i.ImageUrl = GetImageUrl("CMSModules/CMS_Settings/help.png");
        ScriptHelper.AppendTooltip(i, ResHelper.LocalizeString(settingsKey.KeyDescription), null);
        return i;
    }


    /// <summary>
    /// Gets all setting keys for the input groupID.
    /// </summary>
    /// <param name="groupID">ID of the SettingsCategory record in database. This instance should be marked as GROUP</param>
    /// <returns><c>IEnumerable</c> of <c>SettingsKeyInfo</c>.</returns>
    private IEnumerable<SettingsKeyInfo> GetKeys(int groupID)
    {
        DataSet keysSet = SettingsKeyProvider.GetSettingsKeysOrdered(mSiteId, groupID);
        if (!DataHelper.DataSourceIsEmpty(keysSet))
        {
            DataTable keyTable = keysSet.Tables[0];
            foreach (DataRow keyRow in keyTable.Rows)
            {
                yield return new SettingsKeyInfo(keyRow);
            }
        }
    }


    /// <summary>
    /// Gets all groups for the current SettingsCategoryInfo.
    /// </summary>
    /// <returns><c>IEnumerable</c> of <c>SettingsCategoryInfo</c>.</returns>
    private IEnumerable<SettingsCategoryInfo> GetGroups()
    {
        if (SettingsCategoryInfo != null)
        {
            DataSet groupsSet = null;

            if ((!string.IsNullOrEmpty(mSearchText)) && (mSearchText.Length >= mSearchLimit))
            {
                groupsSet = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryIsGroup = 1", "CategoryName");
            }
            else
            {
                groupsSet = SettingsCategoryInfoProvider.GetChildSettingsCategories(SettingsCategoryInfo.CategoryName, Where);
            }

            if (!DataHelper.DataSourceIsEmpty(groupsSet))
            {
                DataTable groupTable = groupsSet.Tables[0];
                foreach (DataRow groupRow in groupTable.Rows)
                {
                    SettingsCategoryInfo sci = new SettingsCategoryInfo(groupRow);
                    if (sci.CategoryIsGroup)
                    {
                        yield return sci;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Ensures displaying of info message (Changes were saved, reset to default or Save failed).
    /// </summary>
    /// <param name="message">Message to show</param>
    /// <param name="visible">Visibility of message</param>
    /// <param name="isError">Error indicator</param>
    private void SetInfoMessage(string message, bool visible, bool isError)
    {
        if (OnInfoMessageChanged != null)
        {
            OnInfoMessageChanged(message, visible, isError);
            lblSaved.Visible = false;
        }
        // Use custom info label if no handler set.
        else
        {
            lblSaved.Visible = visible;
            lblSaved.Text = message;
            if (isError)
            {
                lblSaved.CssClass = "ErrorLabel";
            }
        }
    }

    #endregion


    #region "ICallbackEventHandler Members"

    /// <summary>
    /// Returns the result of a callback.
    /// </summary>
    /// <returns>String representing the callback result.</returns>
    public string GetCallbackResult()
    {
        return mCallBackResult;
    }


    public void RaiseCallbackEvent(string eventArgument)
    {
        string[] args = eventArgument.Split('|');
        string clientId = args[0];
        string controlPath = args[1];

        SettingsKeyItem ski = GetKeyItemByClientID(clientId);
        string value = SettingsKeyProvider.GetStringValue(ski.SettingsKey);

        // Remove security params - this value is used only as info about global value
        value = MacroResolver.RemoveSecurityParameters(value, true, null);

        // Escape value
        value = Uri.EscapeUriString(value);

        // Result is in form [element client id|element value|updatepanel client id|is client id defined|additional element client id|value for the additional value element]
        mCallBackResult = string.Format(@"{0}|{1}|{2}|{3}|{4}|{5}",
                                        clientId, value, ski.UpdatePanelClientID, clientId.Equals(UNDEFINED_VALUE_ELEMENT_CLIENT_ID), "", "");
    }

    #endregion
}