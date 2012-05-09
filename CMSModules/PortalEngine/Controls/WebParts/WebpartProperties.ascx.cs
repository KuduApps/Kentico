using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.EventLog;
using CMS.UIControls;
using CMS.PortalControls;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebpartProperties : CMSUserControl
{
    #region "Variables"

    protected string mAliasPath = null;
    protected int mPageTemplateId = 0;
    protected string mZoneId = null;
    protected string mWebpartId = null;
    protected Guid mInstanceGUID = Guid.Empty;
    protected bool mWebPartIdChanged = false;
    protected string mBeforeFormDefinition = null;
    protected string mAfterFormDefinition = null;
    protected bool mIsNewWebPart = false;
    protected bool mIsNewVariant = false;
    protected int mVariantId = 0;
    protected int mZoneVariantId = 0;
    protected VariantModeEnum mVariantMode = VariantModeEnum.None;


    /// <summary>
    /// Current page info.
    /// </summary>
    PageInfo pi = null;

    /// <summary>
    /// Page template info.
    /// </summary>
    PageTemplateInfo pti = null;

    /// <summary>
    /// Current web part.
    /// </summary>
    WebPartInstance webPartInstance = null;

    /// <summary>
    /// Current page template.
    /// </summary>
    PageTemplateInstance templateInstance = null;

    /// <summary>
    /// Zone instance.
    /// </summary>
    //WebPartZoneInstance zone = null;

    /// <summary>
    /// Tree provider.
    /// </summary>
    TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

    /// <summary>
    /// Gets web part from instance.
    /// </summary>
    WebPartInfo wpi = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Page alias path.
    /// </summary>
    public string AliasPath
    {
        get
        {
            return mAliasPath;
        }
        set
        {
            mAliasPath = value;
        }
    }


    /// <summary>
    /// Page template ID.
    /// </summary>
    public int PageTemplateId
    {
        get
        {
            return mPageTemplateId;
        }
        set
        {
            mPageTemplateId = value;
        }
    }


    /// <summary>
    /// Zone ID.
    /// </summary>
    public string ZoneId
    {
        get
        {
            return mZoneId;
        }
        set
        {
            mZoneId = value;
        }
    }


    /// <summary>
    /// Web part ID.
    /// </summary>
    public string WebpartId
    {
        get
        {
            return mWebpartId;
        }
        set
        {
            mWebpartId = value;
        }
    }


    /// <summary>
    /// Instance GUID.
    /// </summary>
    public Guid InstanceGUID
    {
        get
        {
            return mInstanceGUID;
        }
        set
        {
            mInstanceGUID = value;
        }
    }


    /// <summary>
    /// True if the web part ID has changed.
    /// </summary>
    public bool WebPartIdChanged
    {
        get
        {
            return mWebPartIdChanged;
        }
    }


    /// <summary>
    /// Before form definition.
    /// </summary>
    public string BeforeFormDefinition
    {
        get
        {
            return mBeforeFormDefinition;
        }
        set
        {
            mBeforeFormDefinition = value;
        }
    }


    /// <summary>
    /// After form definition.
    /// </summary>
    public string AfterFormDefinition
    {
        get
        {
            return mAfterFormDefinition;
        }
        set
        {
            mAfterFormDefinition = value;
        }
    }


    /// <summary>
    /// Whether is webpart new or not.
    /// </summary>
    public bool IsNewWebPart
    {
        get
        {
            return mIsNewWebPart;
        }
        set
        {
            mIsNewWebPart = value;
        }
    }


    /// <summary>
    /// Indicates whether is a new variant.
    /// </summary>
    public bool IsNewVariant
    {
        get
        {
            return mIsNewVariant;
        }
        set
        {
            mIsNewVariant = value;
        }
    }


    /// <summary>
    /// Gets or sets the actual web part variant ID.
    /// </summary>
    public int VariantID
    {
        get
        {
            return mVariantId;
        }
        set
        {
            mVariantId = value;
        }
    }


    /// <summary>
    /// Gets or sets the web part zone variant ID.
    /// This property is set when adding a new webpart into the zone variant, in all other cases is set to 0.
    /// </summary>
    public int ZoneVariantID
    {
        get
        {
            return mZoneVariantId;
        }
        set
        {
            mZoneVariantId = value;
        }
    }


    /// <summary>
    /// Gets the variant mode. Indicates whether there are MVT/ContentPersonalization/None variants active.
    /// </summary>
    public VariantModeEnum VariantMode
    {
        get
        {
            return mVariantMode;
        }
        set
        {
            mVariantMode = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Init event handler.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Load settings
        if (!string.IsNullOrEmpty(Request.Form[hdnIsNewWebPart.UniqueID]))
        {
            IsNewWebPart = ValidationHelper.GetBoolean(Request.Form[hdnIsNewWebPart.UniqueID], false);
        }
        if (!string.IsNullOrEmpty(Request.Form[hdnInstanceGUID.UniqueID]))
        {
            InstanceGUID = ValidationHelper.GetGuid(Request.Form[hdnInstanceGUID.UniqueID], Guid.Empty);
        }

        // Try to find the web part variant in the database and set its VariantID
        if (IsNewVariant)
        {
            Hashtable properties = WindowHelper.GetItem("variantProperties") as Hashtable;
            if (properties != null)
            {
                // Get the variant code name from the WindowHelper
                string variantName = ValidationHelper.GetString(properties["codename"], string.Empty);

                // Check if the variant exists in the database
                int variantIdFromDB = 0;
                if (VariantMode == VariantModeEnum.MVT)
                {
                    variantIdFromDB = ModuleCommands.OnlineMarketingGetMVTVariantId(PageTemplateId, variantName);
                }
                else if (VariantMode == VariantModeEnum.ContentPersonalization)
                {
                    variantIdFromDB = ModuleCommands.OnlineMarketingGetContentPersonalizationVariantId(PageTemplateId, variantName);
                }

                // Set the variant id from the database
                if (variantIdFromDB > 0)
                {
                    VariantID = variantIdFromDB;
                    IsNewVariant = false;
                }
            }
        }

        if (!String.IsNullOrEmpty(WebpartId))
        {
            // Get the page info
            pi = CMSWebPartPropertiesPage.GetPageInfo(this.AliasPath, this.PageTemplateId);
            if (pi != null)
            {
                // Get template
                pti = pi.PageTemplateInfo;

                // Get template instance
                templateInstance = pti.TemplateInstance;

                // Parent webpart
                WebPartInfo parentWpi = null;

                //Before FormInfo
                FormInfo beforeFI = null;

                //After FormInfo
                FormInfo afterFI = null;

                // Webpart form info
                FormInfo fi = null;

                if (!IsNewWebPart)
                {
                    // Standard zone
                    webPartInstance = pti.GetWebPart(InstanceGUID, WebpartId);

                    // If the web part not found, try to find it among the MVT/CP variants
                    if (webPartInstance == null)
                    {
                        // MVT/CP variant
                        templateInstance.LoadVariants(false, VariantModeEnum.None);
                        webPartInstance = templateInstance.GetWebPart(InstanceGUID, true);

                        // Set the VariantMode according to the selected web part/zone variant
                        if ((webPartInstance != null) && (webPartInstance.ParentZone != null))
                        {
                            VariantMode = (webPartInstance.VariantMode != VariantModeEnum.None) ? webPartInstance.VariantMode : webPartInstance.ParentZone.VariantMode;
                        }
                        else
                        {
                            VariantMode = VariantModeEnum.None;
                        }
                    }
                    else
                    {
                        // Ensure that the ZoneVarianID is not set when the web part was found in a regural zone.
                        ZoneVariantID = 0;
                    }

                    if ((VariantID > 0) && (webPartInstance != null) && (webPartInstance.PartInstanceVariants != null))
                    {
                        // Check OnlineMarketing permissions.
                        if (CheckPermissions("Read"))
                        {
                            webPartInstance = webPartInstance.FindVariant(VariantID);
                        }
                        else
                        {
                            // Not authorised for OnlineMarketing - Manage.
                            RedirectToInformation(String.Format(GetString("general.permissionresource"), "Read", (VariantMode == VariantModeEnum.ContentPersonalization) ? "CMS.ContentPersonalization" : "CMS.MVTest"));
                        }
                    }

                    if (webPartInstance == null)
                    {
                        lblInfo.Text = GetString("WebPartProperties.WebPartNotFound");
                        pnlFormArea.Visible = false;
                        return;
                    }

                    wpi = WebPartInfoProvider.GetWebPartInfo(webPartInstance.WebPartType);
                    form.Mode = FormModeEnum.Update;
                }
                // Webpart instance hasn't created yet
                else
                {
                    wpi = WebPartInfoProvider.GetWebPartInfo(ValidationHelper.GetInteger(WebpartId, 0));
                    form.Mode = FormModeEnum.Insert;
                }

                // Load parent
                if (wpi != null)
                {
                    if (wpi.WebPartParentID > 0)
                    {
                        parentWpi = WebPartInfoProvider.GetWebPartInfo(wpi.WebPartParentID);
                    }
                }

                // Get the form definition
                string wpProperties = "<form></form>";
                if (wpi != null)
                {
                    wpProperties = wpi.WebPartProperties;

                    // Use parent webpart if is defined
                    if (parentWpi != null)
                    {
                        wpProperties = parentWpi.WebPartProperties;
                    }

                    // Get before FormInfo
                    if (BeforeFormDefinition == null)
                    {
                        beforeFI = PortalHelper.GetPositionFormInfo((WebPartTypeEnum)wpi.WebPartType, PropertiesPosition.Before);
                    }
                    else
                    {
                        beforeFI = new FormInfo(BeforeFormDefinition);
                    }

                    // Get after FormInfo
                    if (AfterFormDefinition == null)
                    {
                        afterFI = PortalHelper.GetPositionFormInfo((WebPartTypeEnum)wpi.WebPartType, PropertiesPosition.After);
                    }
                    else
                    {
                        afterFI = new FormInfo(AfterFormDefinition);
                    }
                }

                // Add 'General' category at the beginning if no one is specified
                if (!string.IsNullOrEmpty(wpProperties) && (!wpProperties.StartsWith("<form><category", StringComparison.InvariantCultureIgnoreCase)))
                {
                    wpProperties = wpProperties.Insert(6, "<category name=\"" + GetString("general.general") + "\" />");
                }

                // Get merged web part FormInfo 
                fi = FormHelper.GetWebPartFormInfo(wpi.WebPartName, wpProperties, beforeFI, afterFI, true);

                // Get datarow with required columns
                DataRow dr = fi.GetDataRow();

                if (IsNewWebPart)
                {
                    // Load default properties values                    
                    fi.LoadDefaultValues(dr);

                    // Load overriden system values
                    fi.LoadDefaultValues(dr, wpi.WebPartDefaultValues);

                    // Set control ID
                    FormFieldInfo ffi = fi.GetFormField("WebPartControlID");
                    if (ffi != null)
                    {
                        ffi.DefaultValue = WebPartZoneInstance.GetUniqueWebPartId(wpi.WebPartName, templateInstance);
                        fi.UpdateFormField("WebPartControlID", ffi);
                    }
                }

                // Load values from existing webpart
                LoadDataRowFromWebPart(dr, webPartInstance);

                // Set a unique WebPartControlID for athe new variant
                if (IsNewVariant)
                {
                    // Set control ID
                    dr["WebPartControlID"] = WebPartZoneInstance.GetUniqueWebPartId(wpi.WebPartName, templateInstance);
                }

                // Init the form
                InitForm(form, dr, fi);

                AddExportLink();
            }
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOnOK.Click += btnOnOK_Click;
        btnOnApply.Click += btnOnApply_Click;

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript(
            "function SetRefresh(refreshpage) { document.getElementById('" + hidRefresh.ClientID + "').value = refreshpage; } \n" +
            "function GetRefresh() { return document.getElementById('" + hidRefresh.ClientID + "').value == 'true'; } \n" +
            "function OnApplyButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnApply, "") + "} \n" +
            "function OnOKButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnOK, "") + "} \n"
        ));

        // Reload parent page after closing, if needed
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UnloadRefresh", ScriptHelper.GetScript("window.isPostBack = false; window.onunload = function() { if (!window.isPostBack && GetRefresh()) { RefreshPage(); }};"));
        ScriptHelper.RegisterScriptFile(Page, "cmsedit.js");
        ScriptHelper.RegisterJQuery(Page);
    }


    /// <summary>
    /// Control ID validation.
    /// </summary>
    void formElem_OnItemValidation(object sender, ref string errorMessage)
    {
        Control ctrl = (Control)sender;
        if (ctrl.ID.ToLowerInvariant() == "webpartcontrolid")
        {
            FormEngineUserControl ctrlTextbox = (FormEngineUserControl)ctrl;
            string newId = ValidationHelper.GetString(ctrlTextbox.Value, null);

            // Validate unique ID
            WebPartInstance existingPart = pti.GetWebPart(newId);
            if ((existingPart != null) && ((webPartInstance == null) || (existingPart.InstanceGUID != webPartInstance.InstanceGUID)))
            {
                // Error - duplicit IDs
                errorMessage = GetString("WebPartProperties.ErrorUniqueID");
            }
            else
            {
                string uniqueId = WebPartZoneInstance.GetUniqueWebPartId(newId, pi.TemplateInstance);
                if (!uniqueId.Equals(newId))
                {
                    // Check if there is already a widget with the same id in the page
                    existingPart = pi.TemplateInstance.GetWebPart(newId);
                    if ((existingPart != null) && existingPart.IsWidget)
                    {
                        // Error - the ID collide with another widget which is already in the page
                        errorMessage = ResHelper.GetString("WidgetProperties.ErrorUniqueID");
                    }
                }
            }

        }
    }


    /// <summary>
    /// Saves the given form.
    /// </summary>
    /// <param name="form">Form to save</param>
    private static bool SaveForm(BasicForm form)
    {
        if ((form != null) && form.Visible)
        {
            return form.SaveData("");
        }

        return true;
    }


    /// <summary>
    /// Checks permissions (depends on variant mode) 
    /// </summary>
    /// <param name="permissionName">Name of permission to test</param>
    private bool CheckPermissions(string permissionName)
    {
        CurrentUserInfo cui = CMSContext.CurrentUser;
        switch (VariantMode)
        {
            case VariantModeEnum.MVT:
                return cui.IsAuthorizedPerResource("cms.mvtest", permissionName);

            case VariantModeEnum.ContentPersonalization:
                return cui.IsAuthorizedPerResource("cms.contentpersonalization", permissionName);

            case VariantModeEnum.Conflicted:
            case VariantModeEnum.None:
                return cui.IsAuthorizedPerResource("cms.mvtest", permissionName) || cui.IsAuthorizedPerResource("cms.contentpersonalization", permissionName);
        }

        return true;
    }


    /// <summary>
    /// Saves webpart properties.
    /// </summary>
    public bool Save()
    {
        // Check MVT/CP security
        if (VariantID > 0)
        {
            // Check OnlineMarketing permissions.
            if (!CheckPermissions("Manage"))
            {
                lblError.Visible = true;
                lblError.Text = GetString("general.modifynotallowed");
                return false;
            }
        }

        // Save the data
        if ((pi != null) && (pti != null) && (templateInstance != null) && SaveForm(form))
        {
            // Add web part if new
            if (IsNewWebPart)
            {
                AddWebPart();
            }

            WebPartInstance originalWebPartInstance = webPartInstance;
            if (IsNewVariant)
            {
                webPartInstance = webPartInstance.Clone();
                webPartInstance.VariantMode = VariantModeFunctions.GetVariantModeEnum(QueryHelper.GetString("variantmode", String.Empty).ToLower());
            }

            // Get basicform's datarow and update webpart
            SaveFormToWebPart(form);

            bool isWebPartVariant = (VariantID > 0) || (ZoneVariantID > 0) || IsNewVariant;
            if (!isWebPartVariant)
            {
                // Save the changes  
                CMSPortalManager.SaveTemplateChanges(pi, pti, templateInstance, WidgetZoneTypeEnum.None, ViewModeEnum.Design, tree);
            }
            else
            {
                // Save the variant properties
                if ((webPartInstance != null)
                    && (webPartInstance.ParentZone != null)
                    && (!webPartInstance.ParentZone.HasVariants) // Save only if the parent zone does not have any variants
                    && (webPartInstance.ParentZone.ParentTemplateInstance != null)
                    && (webPartInstance.ParentZone.ParentTemplateInstance.ParentPageTemplate != null))
                {
                    string variantName = string.Empty;
                    string variantDisplayName = string.Empty;
                    string variantDisplayCondition = string.Empty;
                    string variantDescription = string.Empty;
                    bool variantEnabled = true;
                    string zoneId = webPartInstance.ParentZone.ZoneID;
                    int templateId = webPartInstance.ParentZone.ParentTemplateInstance.ParentPageTemplate.PageTemplateId;
                    Guid instanceGuid = Guid.Empty;
                    XmlDocument doc = new XmlDocument();
                    XmlNode xmlWebParts = null;

                    if (ZoneVariantID > 0)
                    {
                        // This webpart is in a zone variant therefore save the whole variant webparts
                        xmlWebParts = webPartInstance.ParentZone.GetXmlNode(doc);
                        if (VariantMode == VariantModeEnum.MVT)
                        {
                            // MVT variant
                            ModuleCommands.OnlineMarketingSaveMVTVariantWebParts(ZoneVariantID, xmlWebParts);
                        }
                        else if (VariantMode == VariantModeEnum.ContentPersonalization)
                        {
                            // Content personalization variant
                            ModuleCommands.OnlineMarketingSaveContentPersonalizationVariantWebParts(ZoneVariantID, xmlWebParts);
                        }
                    }
                    else
                    {
                        // web part/widget variant
                        xmlWebParts = webPartInstance.GetXmlNode(doc);
                        instanceGuid = InstanceGUID;

                        Hashtable properties = WindowHelper.GetItem("variantProperties") as Hashtable;
                        if (properties != null)
                        {
                            variantName = ValidationHelper.GetString(properties["codename"], string.Empty);
                            variantDisplayName = ValidationHelper.GetString(properties["displayname"], string.Empty);
                            variantDescription = ValidationHelper.GetString(properties["description"], string.Empty);
                            variantEnabled = ValidationHelper.GetBoolean(properties["enabled"], true);
                            if (VariantMode == VariantModeEnum.ContentPersonalization)
                            {
                                variantDisplayCondition = ValidationHelper.GetString(properties["condition"], string.Empty);
                            }
                        }

                        // Save the web part variant properties
                        if (VariantMode == VariantModeEnum.MVT)
                        {
                            // MVT variant
                            ModuleCommands.OnlineMarketingSaveMVTVariant(VariantID, variantName, variantDisplayName, variantDescription, variantEnabled, zoneId, webPartInstance.InstanceGUID, templateId, 0, xmlWebParts);
                        }
                        else if (VariantMode == VariantModeEnum.ContentPersonalization)
                        {
                            // Content personalization variant
                            ModuleCommands.OnlineMarketingSaveContentPersonalizationVariant(VariantID, variantName, variantDisplayName, variantDescription, variantEnabled, variantDisplayCondition, zoneId, webPartInstance.InstanceGUID, templateId, 0, xmlWebParts);
                        }

                        // The variants are cached -> Reload
                        if (originalWebPartInstance != null)
                        {
                            originalWebPartInstance.LoadVariants(true, VariantMode);
                        }
                    }
                }
            }

            // Reload the form (because of macro values set only by JS)
            form.ReloadData();

            // Clear the cached web part
            if (InstanceGUID != null)
            {
                CacheHelper.TouchKey("webpartinstance|" + InstanceGUID.ToString().ToLower());
            }

            return true;
        }
        else if ((webPartInstance != null) && (webPartInstance.ParentZone != null))
        {
            // Reload the zone/web part variants when saving of the form fails
            webPartInstance.ParentZone.LoadVariants(true, VariantModeEnum.None);
        }

        return false;
    }


    /// <summary>
    /// Saves the given DataRow data to the web part properties.
    /// </summary>
    /// <param name="form">Form to save</param>
    private void SaveFormToWebPart(BasicForm form)
    {
        if (form.Visible && (webPartInstance != null))
        {
            // Keep the old ID to check the change of the ID
            string oldId = webPartInstance.ControlID.ToLowerInvariant();

            DataRow dr = form.DataRow;
            foreach (DataColumn column in dr.Table.Columns)
            {
                webPartInstance.MacroTable[column.ColumnName.ToLower()] = form.MacroTable[column.ColumnName.ToLower()];
                webPartInstance.SetValue(column.ColumnName, dr[column]);

                // If name changed, move the content
                if (column.ColumnName.ToLowerInvariant() == "webpartcontrolid")
                {
                    try
                    {
                        string newId = null;
                        if (!IsNewVariant)
                        {
                            newId = ValidationHelper.GetString(dr[column], "").ToLowerInvariant();
                        }

                        // Name changed
                        if ((!string.IsNullOrEmpty(newId)) && (newId != oldId))
                        {
                            if (!IsNewWebPart && !IsNewVariant)
                            {
                                mWebPartIdChanged = true;
                            }
                            WebpartId = newId;

                            // Move the document content if present
                            string currentContent = (string)(pi.EditableWebParts[oldId]);
                            if (currentContent != null)
                            {
                                TreeNode node = DocumentHelper.GetDocument(pi.DocumentId, tree);

                                // Move the content in the page info
                                pi.EditableWebParts[oldId] = null;
                                pi.EditableWebParts[newId] = currentContent;

                                // Update the document
                                node.SetValue("DocumentContent", pi.GetContentXml());
                                DocumentHelper.UpdateDocument(node, tree);
                            }

                            // Change the underlying zone names if layout web part
                            if ((wpi != null) && ((WebPartTypeEnum)wpi.WebPartType == WebPartTypeEnum.Layout))
                            {
                                string prefix = oldId + "_";

                                foreach (WebPartZoneInstance zone in pti.WebPartZones)
                                {
                                    if (zone.ZoneID.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        // Change the zone prefix to the new one
                                        zone.ZoneID = newId + "_" + zone.ZoneID.Substring(prefix.Length);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider ev = new EventLogProvider();
                        ev.LogEvent("Content", "CHANGEWEBPART", ex);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Loads the data row data from given web part instance.
    /// </summary>
    /// <param name="dr">DataRow to fill</param>
    /// <param name="webPart">Source web part</param>
    private static void LoadDataRowFromWebPart(DataRow dr, WebPartInstance webPart)
    {
        foreach (DataColumn column in dr.Table.Columns)
        {
            try
            {
                object value = webPart.GetValue(column.ColumnName);

                // Convert value into default format
                if (value != null)
                {
                    if (column.DataType == typeof(decimal))
                    {
                        value = ValidationHelper.GetDouble(value, 0, "en-us");
                    }

                    if (column.DataType == typeof(DateTime))
                    {
                        value = ValidationHelper.GetDateTime(value, DateTime.MinValue, "en-us");
                    }
                }

                DataHelper.SetDataRowValue(dr, column.ColumnName, value);
            }
            catch
            {
            }
        }
    }


    /// <summary>
    /// Initializes the form.
    /// </summary>
    /// <param name="form">Form</param>
    /// <param name="dr">Datarow with the data</param>
    /// <param name="fi">Form info</param>
    private void InitForm(BasicForm form, DataRow dr, FormInfo fi)
    {
        if (form != null)
        {
            form.DataRow = dr;
            if (webPartInstance != null)
            {
                form.MacroTable = webPartInstance.MacroTable;
            }
            else
            {
                form.MacroTable = new Hashtable();
            }

            form.SubmitButton.Visible = false;
            form.SiteName = CMSContext.CurrentSiteName;
            form.FormInformation = fi;
            form.ShowPrivateFields = true;
            form.OnItemValidation += formElem_OnItemValidation;
        }
    }


    /// <summary>
    /// Saves the webpart properties and closes the window.
    /// </summary>
    protected void btnOnOK_Click(object sender, EventArgs e)
    {
        // Save webpart properties
        if (Save())
        {
            bool refresh = ValidationHelper.GetBoolean(hidRefresh.Value, false);

            string script = "";

            if (refresh || WebPartIdChanged)
            {
                if (IsNewVariant && (webPartInstance != null))
                {
                    // Display the new variant by default
                    script = "UpdateVariantPosition('" + "Variant_WP_" + webPartInstance.InstanceGUID.ToString("N") + "', -1); ";
                }

                script += "RefreshPage(); \n";
            }

            // Close the window
            ltlScript.Text += ScriptHelper.GetScript(script + "top.window.close();");
        }
    }


    /// <summary>
    /// Saves the webpart properties.
    /// </summary>
    protected void btnOnApply_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            hdnIsNewWebPart.Value = "false";
            hdnInstanceGUID.Value = webPartInstance.InstanceGUID.ToString();
            if (WebPartIdChanged)
            {
                ltlScript.Text += ScriptHelper.GetScript("ChangeWebPart('" + ZoneId + "', '" + WebpartId + "', '" + AliasPath + "'); RefreshPage();");
            }

            AddExportLink();
        }
    }


    /// <summary>
    /// Render event handler.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        lblInfo.Visible = (lblInfo.Text != "");

        base.Render(writer);
    }


    /// <summary>
    /// Adds web part.
    /// </summary>
    private void AddWebPart()
    {
        int webpartID = ValidationHelper.GetInteger(WebpartId, 0);
        // Add web part to the currently selected zone under currently selected page
        if ((webpartID > 0) && !string.IsNullOrEmpty(ZoneId))
        {
            // Get the web part by code name
            WebPartInfo wi = WebPartInfoProvider.GetWebPartInfo(webpartID);
            if (wi != null)
            {
                // Ensure layout zone flag
                if (QueryHelper.GetBoolean("layoutzone", false))
                {
                    WebPartZoneInstance zone = pti.EnsureZone(ZoneId);
                    zone.LayoutZone = true;
                }

                // Add the web part
                WebPartInstance newPart = null;
                if (ZoneVariantID == 0)
                {
                    newPart = pti.AddWebPart(ZoneId, webpartID);
                }
                else
                {
                    WebPartZoneInstance wpzi = templateInstance.EnsureZone(ZoneId);

                    // Load the zone variants if not loaded yet
                    if (wpzi.ZoneInstanceVariants == null)
                    {
                        wpzi.LoadVariants();
                    }

                    // Find the correct zone variant
                    wpzi = wpzi.ZoneInstanceVariants.Find(z => z.VariantID.Equals(ZoneVariantID));
                    if (wpzi != null)
                    {
                        newPart = wpzi.AddWebPart(webpartID);
                    }
                }

                if (newPart != null)
                {
                    // Prepare the form info to get the default properties
                    FormInfo fi = null;
                    if (wi.WebPartParentID > 0)
                    {
                        // Get from parent
                        WebPartInfo parentWi = WebPartInfoProvider.GetWebPartInfo(wi.WebPartParentID);
                        if (parentWi != null)
                        {
                            fi = new FormInfo(parentWi.WebPartProperties);
                        }
                    }
                    if (fi == null)
                    {
                        fi = new FormInfo(wi.WebPartProperties);
                    }

                    // Load the default values to the properties
                    if (fi != null)
                    {
                        DataRow dr = fi.GetDataRow();
                        fi.LoadDefaultValues(dr);

                        newPart.LoadProperties(dr);
                    }

                    // Add webpart to user's last recently used
                    CMSContext.CurrentUser.UserSettings.UpdateRecentlyUsedWebPart(wi.WebPartName);

                    // Add last selection date to webpart
                    wi.WebPartLastSelection = DateTime.Now;
                    WebPartInfoProvider.SetWebPartInfo(wi);

                    webPartInstance = newPart;
                }
            }
        }
    }


    /// <summary>
    /// Adds the export link.
    /// </summary>
    private void AddExportLink()
    {
        if (webPartInstance != null)
        {
            ltlExport.Text = "&nbsp;<a target=\"_parent\" href=\"GetWebPartProperties.aspx?webpartid=" + webPartInstance.ControlID + "&webpartguid=" + webPartInstance.InstanceGUID + "&aliaspath=" + AliasPath + "&zoneid=" + ZoneId + "\">" + GetString("WebpartProperties.Export") + "</a>";
        }
    }

    #endregion
}
