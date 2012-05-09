using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.IO;
using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.TreeEngine;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebPartZoneProperties : CMSUserControl
{
    #region "Variables"

    /// <summary>
    /// Current page info.
    /// </summary>
    private PageInfo pi = null;


    /// <summary>
    /// Page template info.
    /// </summary>
    private PageTemplateInfo pti = null;


    /// <summary>
    /// Current web part zone.
    /// </summary>
    private WebPartZoneInstance webPartZone = null;

    private bool mIsNewVariant = false;
    private int mZoneVariantID = 0;
    private VariantModeEnum variantMode = VariantModeEnum.None;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether this instance is a new variant.
    /// </summary>
    public bool IsNewVariant
    {
        get
        {
            return mIsNewVariant;
        }
    }


    /// <summary>
    /// Gets the web part zone instance.
    /// </summary>
    public WebPartZoneInstance WebPartZoneInstance
    {
        get
        {
            return webPartZone;
        }
    }


    /// <summary>
    /// Gets the zone variant ID.
    /// </summary>
    public int ZoneVariantID
    {
        get
        {
            return mZoneVariantID;
        }
    }

    #endregion


    #region "Page methods"

    /// <summary>
    /// OnInit event (BasicForm initialization).
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        string zoneId = QueryHelper.GetString("zoneid", "");
        string aliasPath = QueryHelper.GetString("aliaspath", "");
        int templateId = QueryHelper.GetInteger("templateid", 0);
        mZoneVariantID = QueryHelper.GetInteger("variantid", 0);
        mIsNewVariant = QueryHelper.GetBoolean("isnewvariant", false);
        variantMode = VariantModeFunctions.GetVariantModeEnum(QueryHelper.GetString("variantmode", string.Empty));

        // When displaying an existing variant of a web part, get the variant mode for its original web part
        if (ZoneVariantID > 0)
        {
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
            if ((pti != null) && ((pti.TemplateInstance != null)))
            {
                // Get the original webpart and retrieve its variant mode
                WebPartZoneInstance zoneInstance = pti.TemplateInstance.GetZone(zoneId);
                if ((zoneInstance != null) && (zoneInstance.VariantMode != VariantModeEnum.None))
                {
                    variantMode = zoneInstance.VariantMode;
                }
            }
        }

        // Try to find the zone variant in the database and set its VariantID
        if (IsNewVariant)
        {
            Hashtable properties = WindowHelper.GetItem("variantProperties") as Hashtable;
            if (properties != null)
            {
                // Get the variant code name from the WindowHelper
                string variantName = ValidationHelper.GetString(properties["codename"], string.Empty);

                // Check if the variant exists in the database
                int variantIdFromDB = 0;
                if (variantMode == VariantModeEnum.MVT)
                {
                    variantIdFromDB = ModuleCommands.OnlineMarketingGetMVTVariantId(templateId, variantName);
                }
                else if (variantMode == VariantModeEnum.ContentPersonalization)
                {
                    variantIdFromDB = ModuleCommands.OnlineMarketingGetContentPersonalizationVariantId(templateId, variantName);
                }

                // Set the variant id from the database
                if (variantIdFromDB > 0)
                {
                    mZoneVariantID = variantIdFromDB;
                    mIsNewVariant = false;
                }
            }
        }

        if (!String.IsNullOrEmpty(zoneId))
        {
            // Get pageinfo
            pi = CMSWebPartPropertiesPage.GetPageInfo(aliasPath, templateId);
            if (pi == null)
            {
                this.lblInfo.Text = GetString("webpartzone.notfound");
                this.pnlFormArea.Visible = false;
                return;
            }

            // Get template
            pti = pi.PageTemplateInfo;
            if (pti != null)
            {
                // Get web part zone
                pti.EnsureZone(zoneId);
                webPartZone = pti.GetZone(zoneId);

                if ((ZoneVariantID > 0) && (webPartZone != null) && (webPartZone.ZoneInstanceVariants != null))
                {
                    // Check OnlineMarketing permissions
                    if (CheckPermissions("Read"))
                    {
                        webPartZone = webPartZone.ZoneInstanceVariants.Find(v => v.VariantID.Equals(ZoneVariantID));
                    }
                    else
                    {
                        // Not authorised for OnlineMarketing - Manage.
                        RedirectToInformation(String.Format(GetString("general.permissionresource"), "Read", (variantMode == VariantModeEnum.ContentPersonalization) ? "CMS.ContentPersonalization" : "CMS.MVTest"));
                    }
                }
                if (webPartZone == null)
                {
                    this.lblInfo.Text = GetString("webpartzone.notfound");
                    this.pnlFormArea.Visible = false;
                    return;
                }

                FormInfo fi = BuildFormInfo(webPartZone);

                // Get the datarow and fill the data row with values
                DataRow dr = fi.GetDataRow();
                foreach (DataColumn column in dr.Table.Columns)
                {
                    try
                    {
                        DataHelper.SetDataRowValue(dr, column.ColumnName, webPartZone.GetValue(column.ColumnName));
                    }
                    catch { }
                }

                // Initialize Form
                formElem.DataRow = dr;
                formElem.MacroTable = webPartZone.MacroTable;
                formElem.SubmitButton.Visible = false;
                formElem.SiteName = CMSContext.CurrentSiteName;
                formElem.FormInformation = fi;
                formElem.ShowPrivateFields = true;
                formElem.OnAfterDataLoad += formElem_OnAfterDataLoad;

                // HTML editor toolbar
                if (fi.UsesHtmlArea())
                {
                    plcToolbarPadding.Visible = true;
                    plcToolbar.Visible = true;
                    pnlFormArea.Height = 285;
                }
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
        switch (variantMode)
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
    /// Handles the OnAfterDataLoad event of the formElem control.
    /// </summary>
    void formElem_OnAfterDataLoad(object sender, EventArgs e)
    {
        if ((pti != null) && (pti.PageTemplateType == PageTemplateTypeEnum.Dashboard))
        {
            FormEngineUserControl typeCtrl = formElem.FieldControls["WidgetZoneType"] as FormEngineUserControl;
            if (typeCtrl != null)
            {
                typeCtrl.SetValue("IsDashboard", true);
            }
        }
        else
        {
            // Disable WidgetZoneType if the zone is a zone variant or has variants
            if ((ZoneVariantID > 0)
                || IsNewVariant
                || ((webPartZone != null) && (webPartZone.HasVariants)))
            {
                EditingFormControl editingCtrl = formElem.FieldEditingControls["WidgetZoneType"] as EditingFormControl;
                if (editingCtrl != null)
                {
                    editingCtrl.Enabled = false;
                }
            }
        }
    }


    /// <summary>
    /// Render event (set info label visibility).
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        this.lblInfo.Visible = (this.lblInfo.Text != "");
        base.Render(writer);
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Saves web part zone properties.
    /// </summary>
    public bool Save()
    {
        if (ZoneVariantID > 0)
        {
            // Check OnlineMarketing permissions
            if (!CheckPermissions("Manage"))
            {
                lblError.Visible = true;
                lblError.Text = GetString("general.modifynotallowed");
                return false;
            }
        }

        // Save the data
        if (formElem.SaveData(""))
        {
            DataRow dr = formElem.DataRow;

            // Get basicform's datarow and update the fields
            if ((webPartZone != null) && (dr != null) && (pti != null))
            {
                // New variant
                // Clone original zone
                WebPartZoneInstance originalWebPartZone = webPartZone;
                if (IsNewVariant)
                {
                    webPartZone = pti.EnsureZone(webPartZone.ZoneID);

                    // Ensure that all the zones which are not saved in the template already will be saved now
                    // This is a case for new layout zones
                    if (!webPartZone.HasVariants)
                    {
                        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                        CMSPortalManager.SaveTemplateChanges(pi, pti, pti.TemplateInstance, WidgetZoneTypeEnum.None, ViewModeEnum.Design, tree);
                    }

                    webPartZone = webPartZone.Clone();

                    bool webPartIdExists = false;
                    int offset = 0;
                    string webPartControlId = string.Empty;

                    // Re-generate web part unique IDs
                    foreach (WebPartInstance wpi in webPartZone.WebParts)
                    {
                        webPartIdExists = false;
                        offset = 0;

                        // Set new web part unique ID
                        string baseId = Regex.Replace(wpi.ControlID, "\\d+$", "");
                        do
                        {
                            webPartControlId = WebPartZoneInstance.GetUniqueWebPartId(baseId, pti.TemplateInstance, offset);
                            // Check if the returned web part control id is already used in the new zone variant
                            webPartIdExists = (webPartZone.GetWebPart(webPartControlId) != null);
                            offset++;
                        } while (webPartIdExists);

                        wpi.ControlID = webPartControlId;
                        wpi.InstanceGUID = new Guid();
                    }
                }

                // If zone type changed, delete all webparts in the zone
                if (ValidationHelper.GetString(webPartZone.GetValue("WidgetZoneType"), "") != ValidationHelper.GetString(dr["WidgetZoneType"], ""))
                {
                    // Remove all variants
                    if (variantMode == VariantModeEnum.MVT)
                    {
                        ModuleCommands.OnlineMarketingRemoveMVTWebPartVariants(webPartZone.WebParts);
                    }
                    else if (variantMode == VariantModeEnum.ContentPersonalization)
                    {
                        ModuleCommands.OnlineMarketingRemoveContentPersonalizationWebPartVariants(webPartZone.WebParts);
                    }



                    webPartZone.WebParts.Clear();
                }

                foreach (DataColumn column in dr.Table.Columns)
                {
                    webPartZone.MacroTable[column.ColumnName.ToLower()] = formElem.MacroTable[column.ColumnName.ToLower()];
                    webPartZone.SetValue(column.ColumnName, dr[column]);
                }

                // Ensure the layout zone flag
                webPartZone.LayoutZone = QueryHelper.GetBoolean("layoutzone", false);

                // Save standard zone
                if ((ZoneVariantID == 0) && (!IsNewVariant))
                {
                    // Update page template
                    PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                }
                else
                {
                    // Save zone variant
                    if ((webPartZone != null)
                        && (webPartZone.ParentTemplateInstance != null)
                        && (webPartZone.ParentTemplateInstance.ParentPageTemplate != null)
                        && (!webPartZone.WebPartsContainVariants)) // Save only if any of the child web parts does not have variants
                    {
                        // Save the variant properties
                        string variantName = string.Empty;
                        string variantDisplayName = string.Empty;
                        string variantDisplayCondition = string.Empty;
                        string variantDescription = string.Empty;
                        bool variantEnabled = true;


                        string zoneId = webPartZone.ZoneID;
                        int templateId = webPartZone.ParentTemplateInstance.ParentPageTemplate.PageTemplateId;
                        XmlDocument doc = new XmlDocument();
                        XmlNode xmlWebParts = webPartZone.GetXmlNode(doc);

                        // Get variant description properties
                        Hashtable properties = WindowHelper.GetItem("variantProperties") as Hashtable;
                        if (properties != null)
                        {
                            variantName = ValidationHelper.GetString(properties["codename"], string.Empty);
                            variantDisplayName = ValidationHelper.GetString(properties["displayname"], string.Empty);
                            variantDescription = ValidationHelper.GetString(properties["description"], string.Empty);
                            variantEnabled = ValidationHelper.GetBoolean(properties["enabled"], true);

                            if (PortalContext.ContentPersonalizationEnabled)
                            {
                                variantDisplayCondition = ValidationHelper.GetString(properties["condition"], string.Empty);
                            }
                        }

                        if (variantMode == VariantModeEnum.MVT)
                        {
                            // Save MVT variant properties
                            mZoneVariantID = ModuleCommands.OnlineMarketingSaveMVTVariant(ZoneVariantID, variantName, variantDisplayName, variantDescription, variantEnabled, zoneId, Guid.Empty, templateId, 0, xmlWebParts);
                        }
                        else if (variantMode == VariantModeEnum.ContentPersonalization)
                        {
                            // Save Content personalization variant properties
                            mZoneVariantID = ModuleCommands.OnlineMarketingSaveContentPersonalizationVariant(ZoneVariantID, variantName, variantDisplayName, variantDescription, variantEnabled, variantDisplayCondition, zoneId, Guid.Empty, templateId, 0, xmlWebParts);
                        }

                        // The variants are cached -> Reload
                        if (originalWebPartZone != null)
                        {
                            originalWebPartZone.LoadVariants(true, variantMode);
                        }
                    }
                }

                // Reload the form (because of macro values set only by JS)
                this.formElem.LoadData(dr);

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns the form definition for the webpart zone properties.
    /// </summary>
    private FormInfo BuildFormInfo(WebPartZoneInstance webPartZone)
    {
        FormInfo fi = null;

        string formDefinition = String.Empty;

        // Dashboard zone properties
        if ((pti != null) && (pti.PageTemplateType == PageTemplateTypeEnum.Dashboard))
        {
            formDefinition = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/Controls/WebParts/WebPartZone_Dashboard_Properties.xml"));
        }
        // Classic webpart/widget properties
        else
        {
            formDefinition = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/Controls/WebParts/WebPartZone_Properties.xml"));
        }

        if (!String.IsNullOrEmpty(formDefinition))
        {
            // Load properties
            fi = new FormInfo(formDefinition);
            fi.UpdateExistingFields(fi);

            DataRow dr = fi.GetDataRow();
            LoadDataRowFromWebPartZone(dr, webPartZone);
        }

        return fi;
    }


    /// <summary>
    /// Loads the data row data from given web part zone instance.
    /// </summary>
    /// <param name="dr">DataRow to fill</param>
    /// <param name="webPart">Source web part zone</param>
    private void LoadDataRowFromWebPartZone(DataRow dr, WebPartZoneInstance webPartZone)
    {
        foreach (DataColumn column in dr.Table.Columns)
        {
            try
            {
                object value = webPartZone.GetValue(column.ColumnName);
                if (column.DataType == typeof(decimal))
                {
                    value = ValidationHelper.GetDouble(value, 0, "en-us");
                }
                DataHelper.SetDataRowValue(dr, column.ColumnName, value);
            }
            catch
            {
            }
        }
    }

    #endregion
}
