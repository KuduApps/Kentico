using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SiteProvider;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;


public partial class CMSModules_Widgets_Controls_WidgetProperties : CMSUserControl
{
    #region "Variables"

    protected string mAliasPath = null;
    protected int mPageTemplateId = 0;
    protected string mZoneId = null;
    protected string mWidgetId = null;
    protected Guid mInstanceGUID = Guid.Empty;
    protected bool mWidgetIdChanged = false;
    protected string mBeforeFormDefinition = null;
    protected string mAfterFormDefinition = null;
    protected bool mIsNewWidget = false;
    protected bool mIsNewVariant = false;
    protected int mVariantId = 0;
    protected VariantModeEnum mVariantMode = VariantModeEnum.None;
    protected WidgetZoneTypeEnum mZoneType = WidgetZoneTypeEnum.None;
    private bool mIsInline = false;
    private string mName = String.Empty;
    private bool mIsValidWidget = true;


    // Zone type used for editing
    WidgetZoneTypeEnum zoneType = WidgetZoneTypeEnum.None;


    /// <summary>
    /// Current page info.
    /// </summary>
    PageInfo pi = null;


    /// <summary>
    /// Page template info.
    /// </summary>
    PageTemplateInfo pti = null;


    /// <summary>
    /// Web part info.
    /// </summary>
    WebPartInfo wpi = null;


    /// <summary>
    /// Current widget (alias web part instance).
    /// </summary>
    WebPartInstance widgetInstance = null;


    /// <summary>
    /// Current page template.
    /// </summary>
    PageTemplateInstance templateInstance = null;


    /// <summary>
    /// Zone instance.
    /// </summary>
    WebPartZoneInstance zone = null;


    /// <summary>
    /// Tree provider.
    /// </summary>
    readonly TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);


    /// <summary>
    /// Result of transformation (loaded in init).
    /// </summary>
    FormFieldInfo[] mFields;


    // Widget info object
    WidgetInfo wi = null;

    #endregion


    #region "Events"

    /// <summary>
    /// On not allowed - security check.
    /// </summary>
    public event EventHandler OnNotAllowed;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Zone type.
    /// </summary>
    public WidgetZoneTypeEnum ZoneType
    {
        get
        {
            return mZoneType;
        }
        set
        {
            mZoneType = value;
        }
    }


    /// <summary>
    /// Indicated whether the widget is inline.
    /// </summary>
    public bool IsInline
    {
        get
        {
            return mIsInline;
        }
        set
        {
            mIsInline = value;
        }
    }


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
    public string WidgetId
    {
        get
        {
            return mWidgetId;
        }
        set
        {
            mWidgetId = value;
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


    /// <summary>
    /// True if the web part ID has changed.
    /// </summary>
    public bool WidgetIdChanged
    {
        get
        {
            return mWidgetIdChanged;
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
    /// Whether is widget new or not.
    /// </summary>
    public bool IsNewWidget
    {
        get
        {
            return mIsNewWidget;
        }
        set
        {
            mIsNewWidget = value;
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
    /// Gets or sets the actual widget variant ID.
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
    /// Ensure portal view mode for dasboards.
    /// </summary>
    public void EnsureDashboard()
    {
        if (PageTemplateId > 0)
        {
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(PageTemplateId);
            if ((pti != null) && (pti.PageTemplateType == PageTemplateTypeEnum.Dashboard))
            {
                PortalContext.SetRequestViewMode(ViewModeEnum.DashboardWidgets);
                PortalContext.DashboardName = QueryHelper.GetString("dashboard", String.Empty);
                PortalContext.DashboardSiteName = QueryHelper.GetString("sitename", String.Empty);
                formCustom.DisplayContext = FormInfo.DISPLAY_CONTEXT_DASHBOARD;
            }
        }
    }

    #endregion


    /// <summary>
    /// Init event handler.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Setup basic form on live site
        formCustom.AllowMacroEditing = false;
        formCustom.IsLiveSite = IsLiveSite;

        // Load settings
        if (!String.IsNullOrEmpty(Request.Form[hdnIsNewWebPart.UniqueID]))
        {
            IsNewWidget = ValidationHelper.GetBoolean(Request.Form[hdnIsNewWebPart.UniqueID], false);
        }
        if (!String.IsNullOrEmpty(Request.Form[hdnInstanceGUID.UniqueID]))
        {
            InstanceGUID = ValidationHelper.GetGuid(Request.Form[hdnInstanceGUID.UniqueID], Guid.Empty);
        }

        // Try to find the widget variant in the database and set its VariantID
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

        EnsureDashboard();

        if (!String.IsNullOrEmpty(WidgetId) && !IsInline)
        {
            // Get pageinfo
            try
            {
                pi = CMSWebPartPropertiesPage.GetPageInfo(AliasPath, PageTemplateId);
            }
            catch (PageNotFoundException)
            {
                // Do not throw exception if page info not found (e.g. bad alias path)
            }

            if (pi == null)
            {
                lblInfo.Text = GetString("Widgets.Properties.aliasnotfound");
                lblInfo.Visible = true;
                pnlFormArea.Visible = false;
                return;
            }

            // Get template
            pti = pi.PageTemplateInfo;

            // Get template instance
            templateInstance = CMSPortalManager.GetTemplateInstanceForEditing(pi);

            if (!IsNewWidget)
            {
                // Get the instance of widget
                widgetInstance = templateInstance.GetWebPart(InstanceGUID, WidgetId);
                if (widgetInstance == null)
                {
                    lblInfo.Text = GetString("Widgets.Properties.WidgetNotFound");
                    lblInfo.Visible = true;
                    pnlFormArea.Visible = false;
                    return;
                }

                if ((VariantID > 0) && (widgetInstance != null) && (widgetInstance.PartInstanceVariants != null))
                {
                    // Check OnlineMarketing permissions.
                    if (CheckPermissions("Read"))
                    {
                        widgetInstance = pi.DocumentTemplateInstance.GetWebPart(InstanceGUID, WidgetId);
                        widgetInstance = widgetInstance.PartInstanceVariants.Find(v => v.VariantID.Equals(VariantID));
                        // Set the widget variant mode
                        if (widgetInstance != null)
                        {
                            VariantMode = widgetInstance.VariantMode;
                        }
                    }
                    else
                    {
                        // Not authorised for OnlineMarketing - Manage.
                        RedirectToInformation(String.Format(GetString("general.permissionresource"), "Read", (VariantMode == VariantModeEnum.ContentPersonalization) ? "CMS.ContentPersonalization" : "CMS.MVTest"));
                    }
                }

                // Get widget info by widget name(widget type)
                wi = WidgetInfoProvider.GetWidgetInfo(widgetInstance.WebPartType);
            }
            // Widget instance hasn't created yet
            else
            {
                wi = WidgetInfoProvider.GetWidgetInfo(ValidationHelper.GetInteger(WidgetId, 0));
            }

            CMSPage.EditedObject = wi;
            zoneType = ZoneType;

            // Get the zone to which it inserts
            WebPartZoneInstance zone = templateInstance.GetZone(ZoneId);
            if ((zoneType == WidgetZoneTypeEnum.None) && (zone != null))
            {
                zoneType = zone.WidgetZoneType;
            }

            // Check security
            CurrentUserInfo currentUser = CMSContext.CurrentUser;

            switch (zoneType)
            {
                // Group zone => Only group widgets and group admin
                case WidgetZoneTypeEnum.Group:
                    // Should always be, only group widget are allowed in group zone
                    if (!wi.WidgetForGroup || (!currentUser.IsGroupAdministrator(pi.NodeGroupId) && ((CMSContext.ViewMode != ViewModeEnum.Design) || ((CMSContext.ViewMode == ViewModeEnum.Design) && (!currentUser.IsAuthorizedPerResource("CMS.Design", "Design"))))))
                    {
                        if (OnNotAllowed != null)
                        {
                            OnNotAllowed(this, null);
                        }
                    }
                    break;

                // Widget must be allowed for editor zones
                case WidgetZoneTypeEnum.Editor:
                    if (!wi.WidgetForEditor)
                    {
                        if (OnNotAllowed != null)
                        {
                            OnNotAllowed(this, null);
                        }
                    }
                    break;

                // Widget must be allowed for user zones
                case WidgetZoneTypeEnum.User:
                    if (!wi.WidgetForUser)
                    {
                        if (OnNotAllowed != null)
                        {
                            OnNotAllowed(this, null);
                        }
                    }
                    break;

                // Widget must be allowed for dasboard zones
                case WidgetZoneTypeEnum.Dashboard:
                    if (!wi.WidgetForDashboard)
                    {
                        if (OnNotAllowed != null)
                        {
                            OnNotAllowed(this, null);
                        }
                    }
                    break;
            }

            // Check security
            if ((zoneType != WidgetZoneTypeEnum.Group) && !WidgetRoleInfoProvider.IsWidgetAllowed(wi, currentUser.UserID, currentUser.IsAuthenticated()))
            {
                if (OnNotAllowed != null)
                {
                    OnNotAllowed(this, null);
                }
            }

            // Get form schemas
            wpi = WebPartInfoProvider.GetWebPartInfo(wi.WidgetWebPartID);
            FormInfo zoneTypeDefinition = PortalHelper.GetPositionFormInfo(zoneType);
            string widgetProperties = FormHelper.MergeFormDefinitions(wpi.WebPartProperties, wi.WidgetProperties);
            FormInfo fi = FormHelper.GetWidgetFormInfo(wi.WidgetName, Enum.GetName(typeof(WidgetZoneTypeEnum), zoneType), widgetProperties, zoneTypeDefinition, true);

            if (fi != null)
            {
                // Check if there are some editable properties
                FormFieldInfo[] ffi = fi.GetFields(true, false);
                if ((ffi == null) || (ffi.Length == 0))
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("widgets.emptyproperties");
                }

                // Get datarows with required columns
                DataRow dr = CombineWithDefaultValues(fi, wi);

                // Load default values for new widget
                if (IsNewWidget)
                {
                    fi.LoadDefaultValues(dr, FormResolveTypeEnum.Visible);

                    // Overide default value and set title as widget display name
                    DataHelper.SetDataRowValue(dr, "WidgetTitle", ResHelper.LocalizeString(wi.WidgetDisplayName));
                }

                // Load values from existing widget
                LoadDataRowFromWidget(dr);

                // Init HTML toolbar if exists                
                InitHTMLToobar(fi);

                // Init the form
                InitForm(formCustom, dr, fi);

                // Set the context name
                formCustom.ControlContext.ContextName = CMS.SiteProvider.ControlContext.WIDGET_PROPERTIES;
            }
        }

        if (IsInline)
        {
            //Load text definition from session
            string definition = ValidationHelper.GetString(SessionHelper.GetValue("WidgetDefinition"), string.Empty);
            if (String.IsNullOrEmpty(definition))
            {
                definition = Request.Form[hdnWidgetDefinition.UniqueID];
            }
            else
            {
                hdnWidgetDefinition.Value = definition;
            }

            Hashtable parameters = null;

            if (IsNewWidget)
            {
                // new wdiget - load widget info by id
                if (!String.IsNullOrEmpty(WidgetId))
                {
                    wi = WidgetInfoProvider.GetWidgetInfo(ValidationHelper.GetInteger(WidgetId, 0));
                }
                else
                {
                    // Try to get widget from codename
                    mName = QueryHelper.GetString("WidgetName", String.Empty);
                    wi = WidgetInfoProvider.GetWidgetInfo(mName);
                }
            }
            else
            {
                if (definition == null)
                {
                    ShowError("widget.failedtoload");
                    return;
                }

                //parse defininiton 
                parameters = CMSDialogHelper.GetHashTableFromString(definition);

                //trim control name
                if (parameters["name"] != null)
                {
                    mName = parameters["name"].ToString();

                }

                wi = WidgetInfoProvider.GetWidgetInfo(mName);
            }
            if (wi == null)
            {
                ShowError("widget.failedtoload");
                return;
            }

            //If widget cant be used asi inline
            if (!wi.WidgetForInline)
            {
                ShowError("widget.cantbeusedasinline");
                return;
            }


            //Test permission for user
            CurrentUserInfo currentUser = CMSContext.CurrentUser;
            if (!WidgetRoleInfoProvider.IsWidgetAllowed(wi, currentUser.UserID, currentUser.IsAuthenticated()))
            {
                mIsValidWidget = false;
                OnNotAllowed(this, null);
            }

            //If user is editor, more properties are shown
            WidgetZoneTypeEnum zoneType = WidgetZoneTypeEnum.User;
            if (currentUser.IsEditor)
            {
                zoneType = WidgetZoneTypeEnum.Editor;
            }

            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(wi.WidgetWebPartID);
            string widgetProperties = FormHelper.MergeFormDefinitions(wpi.WebPartProperties, wi.WidgetProperties);
            FormInfo zoneTypeDefinition = PortalHelper.GetPositionFormInfo(zoneType);
            FormInfo fi = FormHelper.GetWidgetFormInfo(wi.WidgetName, Enum.GetName(typeof(WidgetZoneTypeEnum), zoneType), widgetProperties, zoneTypeDefinition, true);

            if (fi != null)
            {
                // Check if there are some editable properties
                mFields = fi.GetFields(true, true);
                if ((mFields == null) || (mFields.Length == 0))
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("widgets.emptyproperties");
                }

                // Get datarows with required columns
                DataRow dr = CombineWithDefaultValues(fi, wi);

                if (IsNewWidget)
                {
                    // Load default values for new widget
                    fi.LoadDefaultValues(dr, FormResolveTypeEnum.Visible);
                }
                else
                {
                    foreach (string key in parameters.Keys)
                    {
                        string value = parameters[key].ToString();
                        // Test if given property exists
                        if (dr.Table.Columns.Contains(key) && !String.IsNullOrEmpty(value))
                        {
                            try
                            {
                                dr[key] = value;
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                // Overide default value and set title as widget display name
                DataHelper.SetDataRowValue(dr, "WidgetTitle", wi.WidgetDisplayName);

                // Init HTML toolbar if exists
                InitHTMLToobar(fi);
                // Init the form
                InitForm(formCustom, dr, fi);

                // Set the context name
                formCustom.ControlContext.ContextName = CMS.SiteProvider.ControlContext.WIDGET_PROPERTIES;
            }
        }
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
    /// Show error label.
    /// </summary>
    /// <param name="error">Error message to show</param>
    public void ShowError(string error)
    {
        mIsValidWidget = false;
        lblError.Visible = true;
        lblError.Text = GetString(error);
    }


    /// <summary>
    /// Combines widget info with default XML system propertiers.
    /// </summary>
    /// <param name="fi">Widget form info</param>
    /// <param name="wi">Widget info object</param>
    public DataRow CombineWithDefaultValues(FormInfo fi, WidgetInfo wi)
    {
        if ((!String.IsNullOrEmpty(wi.WidgetDefaultValues)) && (String.Compare(wi.WidgetDefaultValues, "<form></form>", false) != 0))
        {
            // Apply changed values
            DataRow dr = fi.GetDataRow();
            fi.LoadDefaultValues(dr, wi.WidgetDefaultValues);

            return dr;
        }

        return fi.GetDataRow();
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOnOK.Click += btnOnOK_Click;
        btnOnApply.Click += btnOnApply_Click;

        string jSFunctions = String.Format(@"
function SetRefresh(refreshpage) {{ document.getElementById('{0}').value = refreshpage; }}
function GetRefresh() {{ return document.getElementById('{0}').value == 'true'; }}
function OnApplyButton(refreshpage) {{ SetRefresh(refreshpage); {1}}}
function OnOKButton(refreshpage) {{ SetRefresh(refreshpage); {2}}}",
           hidRefresh.ClientID,
           Page.ClientScript.GetPostBackEventReference(btnOnApply, ""),
           Page.ClientScript.GetPostBackEventReference(btnOnOK, "")
        );

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript(jSFunctions));
        ScriptHelper.RegisterScriptFile(Page, "cmsedit.js");

        // Reload parent page after closing, if needed
        if (!IsInline)
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UnloadRefresh", ScriptHelper.GetScript("window.isPostBack = false; window.onunload = function() { if (!window.isPostBack && GetRefresh()) { RefreshPage(); }};"));
        }

        // Register css file with editor classes
        CSSHelper.RegisterCSSLink(Page, "~/CMSModules/Widgets/CSS/editor.css");

        string definition = ValidationHelper.GetString(SessionHelper.GetValue("WidgetDefinition"), string.Empty);
        if (String.IsNullOrEmpty(definition))
        {
            hdnWidgetDefinition.Value = definition;
            SessionHelper.Remove("WidgetDefinition");
        }
    }


    /// <summary>
    /// Control ID validation.
    /// </summary>
    protected void formElem_OnItemValidation(object sender, ref string errorMessage)
    {
        Control ctrl = (Control)sender;
        if (String.Compare(ctrl.ID, "widgetcontrolid", StringComparison.InvariantCultureIgnoreCase) == 0)
        {
            TextBox ctrlTextbox = (TextBox)ctrl;
            string newId = ctrlTextbox.Text;

            // Validate unique ID
            WebPartInstance existingPart = pti.GetWebPart(newId);
            if ((existingPart != null) && (existingPart != widgetInstance) && (existingPart.InstanceGUID != widgetInstance.InstanceGUID))
            {
                // Error - duplicit IDs
                errorMessage = GetString("Widgets.Properties.ErrorUniqueID");
            }
        }
    }


    /// <summary>
    /// Saves the given form.
    /// </summary>
    /// <param name="form">Form to save</param>
    private static bool SaveForm(BasicForm form)
    {
        if (form.Visible)
        {
            return form.SaveData("");
        }

        return true;
    }


    /// <summary>
    /// Saves the widget data and create string for inline widget.
    /// </summary>
    private string SaveInline()
    {
        string script = "var widgetObj = new Object(); \n";
        if (IsInline)
        {
            // Validate data
            if (!SaveForm(formCustom))
            {
                return String.Empty;
            }

            DataRow dr = formCustom.DataRow;

            if (wi == null)
            {
                return String.Empty;
            }

            // Name of the widget is first argument
            script += String.Format("widgetObj['name']='{0}';", HttpUtility.UrlEncode(wi.WidgetName));

            if (mFields == null)
            {
                return String.Empty;
            }
            foreach (FormFieldInfo ffi in mFields)
            {
                if (dr.Table.Columns.Contains(ffi.Name))
                {
                    script += String.Format("widgetObj['{0}'] = {1}; \n", ffi.Name, ScriptHelper.GetString(HttpUtility.UrlEncode(dr[ffi.Name].ToString().Replace("%", "%25"))));
                }
            }
            // Add image GUID
            DataSet ds = MetaFileInfoProvider.GetMetaFiles("MetaFileObjectID = " + wi.WidgetID + "  AND MetaFileObjectType = 'cms.widget'", String.Empty, "MetafileGuid", 0);
            if (!SqlHelperClass.DataSourceIsEmpty(ds))
            {
                Guid guid = ValidationHelper.GetGuid(ds.Tables[0].Rows[0]["MetafileGuid"], Guid.Empty);
                script += "widgetObj['image_guid'] = '" + guid.ToString() + "'; \n";
            }

            // Add display name
            script += "widgetObj['widget_displayname'] = " + ScriptHelper.GetString(HttpUtility.UrlEncode(wi.WidgetDisplayName.Replace("%", "%25"))) + "; \n";

            // Create javascript for save
            script += "widgetObj['cms_type'] = 'widget';\n InsertSelectedItem(widgetObj);\n";

            // Add to recently used widgtets collection
            CMSContext.CurrentUser.UserSettings.UpdateRecentlyUsedWidget(wi.WidgetName);
            return script;

        }
        return string.Empty;

    }



    /// <summary>
    /// Saves widget properties.
    /// </summary>
    public bool Save()
    {
        if (VariantID > 0)
        {
            // Check MVT/CP security
            if (!CheckPermissions("Manage"))
            {
                ShowError("general.modifynotallowed");
                return false;
            }
        }

        // Save the data
        if ((pi != null) && (pti != null) && (templateInstance != null) && SaveForm(formCustom))
        {
            // Check manage permission for non-livesite version
            if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) && (CMSContext.ViewMode != ViewModeEnum.DashboardWidgets))
            {
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(pi.NodeId, pi.ClassName, NodePermissionsEnum.Modify) != AuthorizationResultEnum.Allowed)
                {
                    return false;
                }
            }

            // Get the zone
            zone = templateInstance.EnsureZone(ZoneId);

            if (zone != null)
            {
                zone.WidgetZoneType = zoneType;

                // Add new widget
                if (IsNewWidget)
                {
                    AddWidget();
                }

                if (IsNewVariant)
                {
                    widgetInstance = widgetInstance.Clone();

                    if (pi.DocumentTemplateInstance.WebPartZones.Count == 0)
                    {
                        // Save to the document as editor admin changes
                        TreeNode node = DocumentHelper.GetDocument(pi.DocumentId, tree);

                        // Extract and set the document web parts
                        node.SetValue("DocumentWebParts", templateInstance.GetZonesXML(WidgetZoneTypeEnum.Editor));

                        // Save the document
                        DocumentHelper.UpdateDocument(node, tree);
                    }
                }

                // Get basicform's datarow and update widget            
                SaveFormToWidget(formCustom);

                if (IsNewVariant)
                {
                    // Ensures unique id for new widget variant
                    widgetInstance.ControlID = WebPartZoneInstance.GetUniqueWebPartId(wi.WidgetName, zone.ParentTemplateInstance);
                }

                // Allow set dashboard in design mode
                if ((zoneType == WidgetZoneTypeEnum.Dashboard) && String.IsNullOrEmpty(PortalContext.DashboardName))
                {
                    PortalContext.SetViewMode(ViewModeEnum.Design);
                }

                bool isWidgetVariant = (VariantID > 0) || IsNewVariant;
                if (!isWidgetVariant)
                {
                    // Save the changes  
                    CMSPortalManager.SaveTemplateChanges(pi, pti, templateInstance, zoneType, CMSContext.ViewMode, tree);
                }
                else if ((CMSContext.ViewMode == ViewModeEnum.Edit) && (zoneType == WidgetZoneTypeEnum.Editor))
                {
                    // Save the variant properties
                    if ((widgetInstance != null)
                        && (widgetInstance.ParentZone != null)
                        && (widgetInstance.ParentZone.ParentTemplateInstance != null)
                        && (widgetInstance.ParentZone.ParentTemplateInstance.ParentPageTemplate != null))
                    {
                        string variantName = string.Empty;
                        string variantDisplayName = string.Empty;
                        string variantDisplayCondition = string.Empty;
                        string variantDescription = string.Empty;
                        bool variantEnabled = true;
                        string zoneId = widgetInstance.ParentZone.ZoneID;
                        int templateId = widgetInstance.ParentZone.ParentTemplateInstance.ParentPageTemplate.PageTemplateId;
                        Guid instanceGuid = Guid.Empty;
                        XmlDocument doc = new XmlDocument();
                        XmlNode xmlWebParts = null;

                        xmlWebParts = widgetInstance.GetXmlNode(doc);
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
                            ModuleCommands.OnlineMarketingSaveMVTVariant(VariantID, variantName, variantDisplayName, variantDescription, variantEnabled, zoneId, widgetInstance.InstanceGUID, templateId, pi.DocumentId, xmlWebParts);
                        }
                        else if (VariantMode == VariantModeEnum.ContentPersonalization)
                        {
                            ModuleCommands.OnlineMarketingSaveContentPersonalizationVariant(VariantID, variantName, variantDisplayName, variantDescription, variantEnabled, variantDisplayCondition, zoneId, widgetInstance.InstanceGUID, templateId, pi.DocumentId, xmlWebParts);
                        }

                        // Clear the document template
                        templateInstance.ParentPageTemplate.ParentPageInfo.DocumentTemplateInstance = null;

                        // Log widget variant synchronization
                        TreeNode node = DocumentHelper.GetDocument(pi.DocumentId, tree);
                        DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
                    }
                }

            }

            // Reload the form (because of macro values set only by JS)            
            formCustom.ReloadData();

            // Clear the cached web part
            if (InstanceGUID != null)
            {
                CacheHelper.TouchKey("webpartinstance|" + InstanceGUID.ToString().ToLower());
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Adds widget.
    /// </summary>
    private void AddWidget()
    {
        int widgetID = ValidationHelper.GetInteger(WidgetId, 0);

        // Add web part to the currently selected zone under currently selected page
        if ((widgetID > 0) && !String.IsNullOrEmpty(ZoneId))
        {
            if (wi != null)
            {
                // Ensure layout zone flag
                if (QueryHelper.GetBoolean("layoutzone", false))
                {
                    WebPartZoneInstance zone = pti.EnsureZone(ZoneId);
                    zone.LayoutZone = true;
                    zone.WidgetZoneType = zoneType;

                    // Ensure the layout zone flag in the original page template instance
                    WebPartZoneInstance zoneInstance = templateInstance.GetZone(ZoneId);
                    if (zoneInstance != null)
                    {
                        zoneInstance.LayoutZone = true;
                        zone.WidgetZoneType = zoneType;
                    }
                }

                // Add the widget
                WebPartInstance newWidget = templateInstance.AddWidget(ZoneId, widgetID);
                if (newWidget != null)
                {
                    // Prepare the form info to get the default properties
                    FormInfo fi = new FormInfo(wi.WidgetProperties);

                    DataRow dr = fi.GetDataRow();
                    fi.LoadDefaultValues(dr);

                    newWidget.LoadProperties(dr);

                    // Add webpart to user's last recently used
                    CMSContext.CurrentUser.UserSettings.UpdateRecentlyUsedWidget(wi.WidgetName);

                    widgetInstance = newWidget;
                }
            }
        }
    }


    /// <summary>
    /// Saves the given DataRow data to the web part properties.
    /// </summary>
    /// <param name="form">Form to save</param>
    private void SaveFormToWidget(BasicForm form)
    {
        if (form.Visible && (widgetInstance != null))
        {
            // Keep the old ID to check the change of the ID
            string oldId = widgetInstance.ControlID.ToLowerInvariant();

            DataRow dr = form.DataRow;

            // Load default values for new widget
            if (IsNewWidget)
            {
                form.FormInformation.LoadDefaultValues(dr, wi.WidgetDefaultValues);
            }

            foreach (DataColumn column in dr.Table.Columns)
            {
                widgetInstance.MacroTable[column.ColumnName.ToLower()] = form.MacroTable[column.ColumnName.ToLower()];
                widgetInstance.SetValue(column.ColumnName, dr[column]);

                // If name changed, move the content
                if (String.Compare(column.ColumnName, "widgetcontrolid", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    try
                    {
                        string newId = ValidationHelper.GetString(dr[column], "").ToLowerInvariant();

                        // Name changed
                        if (!String.IsNullOrEmpty(newId) && (String.Compare(newId, oldId, false) != 0))
                        {
                            mWidgetIdChanged = true;
                            WidgetId = newId;

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
                                        zone.ZoneID = String.Format("{0}_{1}", newId, zone.ZoneID.Substring(prefix.Length));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider ev = new EventLogProvider();
                        ev.LogEvent("Content", "CHANGEWIDGET", ex);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Loads the data row data from given web part instance.
    /// </summary>
    /// <param name="dr">DataRow to fill</param>
    private void LoadDataRowFromWidget(DataRow dr)
    {
        foreach (DataColumn column in dr.Table.Columns)
        {
            try
            {
                object value = widgetInstance.GetValue(column.ColumnName);

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
        form.DataRow = dr;
        form.MacroTable = widgetInstance != null ? widgetInstance.MacroTable : new Hashtable();

        form.SubmitButton.Visible = false;
        form.SiteName = CMSContext.CurrentSiteName;
        form.FormInformation = fi;
        form.ShowPrivateFields = true;
        form.OnItemValidation += formElem_OnItemValidation;
    }


    /// <summary>
    /// Initializes the HTML toolbar.
    /// </summary>
    /// <param name="form">Form information</param>
    private void InitHTMLToobar(FormInfo form)
    {
        // Display / hide the HTML editor toolbar area
        if (form.UsesHtmlArea())
        {
            plcToolbar.Visible = true;
        }
    }


    /// <summary>
    /// Saves the widget properties and closes the window.
    /// </summary>
    protected void btnOnOK_Click(object sender, EventArgs e)
    {
        // Save widget properties
        if (IsInline)
        {
            //Is valid widget
            if (!mIsValidWidget)
            {
                ltlScript.Text += ScriptHelper.GetScript("top.window.close();");
                return;
            }

            // Register HTMLEditor.js script file
            ScriptHelper.RegisterScriptFile(Page, "~/CMSScripts/Dialogs/HTMLEditor.js");

            string widgetString = SaveInline();

            //Validate the data input
            if (String.IsNullOrEmpty(widgetString))
            {
                return;
            }
            SessionHelper.Remove("WidgetDefinition");
            ltlScript.Text += ScriptHelper.GetScript(widgetString + "top.window.close()");

        }
        else
        {
            // Save the widget
            if (Save())
            {
                bool refresh = ValidationHelper.GetBoolean(hidRefresh.Value, false);

                string script = "";
                if (WidgetIdChanged || refresh)
                {
                    if (IsNewVariant && (widgetInstance != null))
                    {
                        // Display the new variant by default
                        script = "UpdateVariantPosition('" + "Variant_WP_" + widgetInstance.InstanceGUID.ToString("N") + "', -1); \n";
                    }

                    script += "RefreshPage(); \n";
                }

                // Close the window
                ltlScript.Text += ScriptHelper.GetScript(script + " top.window.close();");
            }
        }
    }


    /// <summary>
    /// Saves the widget properties.
    /// </summary>
    protected void btnOnApply_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            hdnIsNewWebPart.Value = "false";
            hdnInstanceGUID.Value = widgetInstance.InstanceGUID.ToString();

            if (WidgetIdChanged)
            {
                ltlScript.Text += ScriptHelper.GetScript(String.Format("ChangeWidget('{0}', '{1}', '{2}'); RefreshPage();", ZoneId, WidgetId, AliasPath));
            }
        }
    }


    /// <summary>
    /// Render event handler.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);
    }
}
