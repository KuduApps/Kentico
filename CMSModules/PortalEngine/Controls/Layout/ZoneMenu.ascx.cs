using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_PortalEngine_Controls_Layout_ZoneMenu : CMSAbstractPortalUserControl
{
    #region "Variables"

    // Column names
    string columnVariantID = string.Empty;
    string columnVariantDisplayName = string.Empty;
    string columnVariantZoneID = string.Empty;
    string columnVariantPageTemplateID = string.Empty;
    string columnVariantInstanceGUID = string.Empty;
    string updateCombinationPanelScript = string.Empty;
    private CurrentUserInfo currentUser = null;

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;

        // Use UI culture for strings
        string culture = currentUser.PreferredUICultureCode;

        // Hide the add MVT/CP variant when Manage permission is not allowed
        if (!currentUser.IsAuthorizedPerResource("cms.contentpersonalization", "Manage"))
        {
            plcAddCPVariant.Visible = false;            
        }

        if (!currentUser.IsAuthorizedPerResource("cms.mvtest", "Manage"))
        {
            plcAddMVTVariant.Visible = false;
        }        

        // Main menu
        imgNewWebPart.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Add.png");
        lblNewWebPart.Text = ResHelper.GetString("ZoneMenu.IconNewWebPart", culture);
        imgNewWebPart.AlternateText = lblNewWebPart.Text;
        pnlNewWebPart.Attributes.Add("onclick", "ContextNewWebPart(GetContextMenuParameter('webPartZoneMenu'));");

        // Configure
        imgConfigureZone.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Properties.png");
        lblConfigureZone.Text = ResHelper.GetString("ZoneMenu.IconConfigureWebpartZone", culture);
        imgConfigureZone.AlternateText = lblConfigureZone.Text;
        pnlConfigureZone.Attributes.Add("onclick", "ContextConfigureWebPartZone(GetContextMenuParameter('webPartZoneMenu'));");

        // Move to
        this.imgMoveTo.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/MoveTo.png");
        this.lblMoveTo.Text = ResHelper.GetString("ZoneMenu.IconMoveTo", culture);

        // Delete all web parts
        this.imgDelete.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Delete.png");
        this.lblDelete.Text = ResHelper.GetString("ZoneMenu.RemoveAll", culture);
        this.imgDelete.AlternateText = this.lblDelete.Text;
        this.pnlDelete.Attributes.Add("onclick", "ContextRemoveAllWebParts(GetContextMenuParameter('webPartZoneMenu'));");

        // Add new MVT variants
        this.lblAddMVTVariant.Text = ResHelper.GetString("ZoneMenu.AddZoneVariant", culture);
        this.imgAddMVTVariant.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/addZone.png");
        this.imgAddMVTVariant.AlternateText = this.lblAddMVTVariant.Text;

        // Add new Content personalization variant
        this.lblAddCPVariant.Text = ResHelper.GetString("ZoneMenu.AddZoneVariant", culture);
        this.imgAddCPVariant.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/addZone.png");
        this.imgAddCPVariant.AlternateText = this.lblAddCPVariant.Text;

        // Add new variant
        this.pnlAddMVTVariant.Attributes.Add("onclick", "ContextAddWebPartZoneMVTVariant(GetContextMenuParameter('webPartZoneMenu'));");
        this.pnlAddCPVariant.Attributes.Add("onclick", "ContextAddWebPartZoneCPVariant(GetContextMenuParameter('webPartZoneMenu'));");

        // List all variants
        this.lblMVTVariants.Text = ResHelper.GetString("ZoneMenu.ZoneMVTVariants", culture);
        this.lblCPVariants.Text = ResHelper.GetString("ZoneMenu.ZonePersonalizationVariants", culture);

        // No MVT variants
        lblNoZoneMVTVariants.Text = ResHelper.GetString("ZoneMenu.NoVariants");
        imgNoZoneMVTVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/novariant.png");

        // No CP variants
        lblNoZoneCPVariants.Text = ResHelper.GetString("ZoneMenu.NoVariants");
        imgNoZoneCPVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/novariant.png");

        // List all variants
        this.imgMVTVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/zoneList.png");
        this.imgMVTVariants.AlternateText = this.lblMVTVariants.Text;
        this.imgCPVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/zoneList.png");
        this.imgCPVariants.AlternateText = this.lblCPVariants.Text;

        if (this.PortalManager.CurrentPlaceholder != null)
        {
            // Build the list of web part zones
            ArrayList webPartZones = new ArrayList();

            if (this.PortalManager.CurrentPlaceholder.WebPartZones != null)
            {
                foreach (CMSWebPartZone zone in this.PortalManager.CurrentPlaceholder.WebPartZones)
                {
                    // Add only standard zones to the list
                    if (zone.ZoneInstance.WidgetZoneType == WidgetZoneTypeEnum.None)
                    {
                        webPartZones.Add(zone);
                    }
                }
            }

            this.repZones.DataSource = webPartZones;
            this.repZones.DataBind();
        }

        if (PortalContext.MVTVariantsEnabled || PortalContext.ContentPersonalizationEnabled)
        {
            menuMoveToZoneVariants.LoadingContent = "<div class=\"PortalContextMenu ZoneContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading", CultureHelper.GetPreferredUICulture()) + "</div></div>";
            menuMoveToZoneVariants.OnReloadData += menuMoveToZoneVariants_OnReloadData;
            repMoveToZoneVariants.ItemDataBound += repZoneVariants_ItemDataBound;

            // Display the MVT menu part in the CMSDesk->Design only. Hide the context menu in the SM->PageTemplates->Design
            if (PortalContext.MVTVariantsEnabled && (CMSContext.CurrentPageInfo != null) && (CMSContext.CurrentPageInfo.DocumentId > 0) && currentUser.IsAuthorizedPerResource("cms.mvtest", "read"))
            {
                // Set Display='none' for the MVT panel. Show dynamically only if required.
                pnlContextMenuMVTVariants.Visible = true;
                pnlContextMenuMVTVariants.Style.Add("display", "none");
                menuZoneMVTVariants.LoadingContent = "<div class=\"PortalContextMenu ZoneContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading", CultureHelper.GetPreferredUICulture()) + "</div></div>";
                menuZoneMVTVariants.OnReloadData += menuZoneMVTVariants_OnReloadData;
                repZoneMVTVariants.ItemDataBound += repVariants_ItemDataBound;

                string script = "zoneMVTVariantContextMenuId = '" + pnlContextMenuMVTVariants.ClientID + "';";
                ScriptHelper.RegisterStartupScript(this, typeof(string), "zoneMVTVariantContextMenuId", ScriptHelper.GetScript(script));
            }
            else
            {
                // Hide the MVT variant context menu items when MVT is not enabled for the current document
                pnlUIMVTVariants.Visible = false;
            }

            // Display the Content personalization menu part in the CMSDesk->Design only. Hide the context menu in the SM->PageTemplates->Design
            if ((PortalContext.ContentPersonalizationEnabled) && (CMSContext.CurrentPageInfo != null) && (CMSContext.CurrentPageInfo.DocumentId > 0) && currentUser.IsAuthorizedPerResource("cms.contentpersonalization", "read"))
            {
                // Set Display='none' for the MVT panel. Show dynamically only if required.
                pnlContextMenuCPVariants.Visible = true;
                pnlContextMenuCPVariants.Style.Add("display", "none");
                menuZoneCPVariants.LoadingContent = "<div class=\"PortalContextMenu ZoneContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading", CultureHelper.GetPreferredUICulture()) + "</div></div>";
                menuZoneCPVariants.OnReloadData += menuZoneCPVariants_OnReloadData;
                repZoneCPVariants.ItemDataBound += repVariants_ItemDataBound;

                string script = "zoneCPVariantContextMenuId = '" + pnlContextMenuCPVariants.ClientID + "';";
                ScriptHelper.RegisterStartupScript(this, typeof(string), "zoneCPVariantContextMenuId", ScriptHelper.GetScript(script));
            }
            else
            {
                // Hide the Content personalization variant context menu items when the Content Personalization is not enabled.
                pnlUICPVariants.Visible = false;
            }
        }
    }


    /// <summary>
    /// Handles the ItemDataBound event of the repVariants control.
    /// </summary>
    protected void repVariants_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Panel pnlVariantItem = (Panel)e.Item.FindControl("pnlVariantItem");
        if (pnlVariantItem != null)
        {
            Label lblVariantName = pnlVariantItem.FindControl("lblVariantItem") as Label;
            if (lblVariantName != null)
            {
                string variantName = (((DataRowView)e.Item.DataItem)[columnVariantDisplayName]).ToString();
                lblVariantName.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(variantName, currentUser.PreferredUICultureCode));
            }

            // Get unique zone code
            string zoneId = ValidationHelper.GetString(((DataRowView)e.Item.DataItem)[columnVariantZoneID], string.Empty);
            int variantId = ValidationHelper.GetInteger(((DataRowView)e.Item.DataItem)[columnVariantID], 0);
            string itemCode = "Variant_Zone_" + zoneId;

            // Display the zone variant when clicked
            pnlVariantItem.Attributes.Add("onclick", "SetVariant('" + itemCode + "', " + variantId + "); " + updateCombinationPanelScript);
        }

        // Display zone icon for each of the zone variants
        Image imgZoneVariant = (Image)e.Item.FindControl("imgVariantItem");
        if (imgZoneVariant != null)
        {
            imgZoneVariant.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/zone.png");
        }
    }


    /// <summary>
    /// Handles the ItemDataBound event of the repZoneVariants control.
    /// </summary>
    protected void repZoneVariants_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Panel pnlZoneVariantItem = (Panel)e.Item.FindControl("pnlZoneVariantItem");
        if (pnlZoneVariantItem != null)
        {
            // Set the zone label
            Label lblZoneVariantItem = pnlZoneVariantItem.FindControl("lblZoneVariantItem") as Label;
            if (lblZoneVariantItem != null)
            {
                string variantName = (((DataRowView)e.Item.DataItem)[columnVariantDisplayName]).ToString();
                lblZoneVariantItem.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(variantName, currentUser.PreferredUICultureCode));
            }

            // Set the zone icon
            Image imgZoneVariantItem = pnlZoneVariantItem.FindControl("imgZoneVariantItem") as Image;
            if (imgZoneVariantItem != null)
            {
                imgZoneVariantItem.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/zone.png");
            }

            // Get unique web part code
            string zoneId = ValidationHelper.GetString(((DataRowView)e.Item.DataItem)[columnVariantZoneID], string.Empty);
            int variantId = ValidationHelper.GetInteger(((DataRowView)e.Item.DataItem)[columnVariantID], 0);
            string itemCode = "Variant_Zone_" + zoneId;

            // Select the web part variant on mouse over
            pnlZoneVariantItem.Attributes.Add("onmouseover", "CM_Disable(this); CM_Cancel('" + menuMoveTo.MenuID + "'); SetVariant('" + itemCode + "', " + variantId + "); " + updateCombinationPanelScript);
            // Hide the context menus when clicked
            pnlZoneVariantItem.Attributes.Add("onclick", "ContextMoveWebPartsToZone(GetContextMenuParameter('webPartZoneMenu'), '" + zoneId + "', " + variantId + ");");
        }
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuWebPartVariants control.
    /// </summary>
    protected void menuZoneMVTVariants_OnReloadData(object sender, EventArgs e)
    {
        // Check permissions
        if ((currentUser == null)
            || (!currentUser.IsAuthorizedPerResource("CMS.MVTest", "Read")))
        {
            return;
        }

        SetColumnNames(VariantModeEnum.MVT);

        string parameters = ValidationHelper.GetString(menuZoneMVTVariants.Parameter, string.Empty);
        string[] items = parameters.Split(new char[] { ',' }, 3);
        if ((items == null) || (items.Length != 3))
        {
            return;
        }

        string zoneId = ValidationHelper.GetString(items[0], string.Empty);
        string aliasPath = ValidationHelper.GetString(items[1], string.Empty);

        if ((CMSContext.CurrentPageInfo != null)
            && (CMSContext.CurrentPageInfo.TemplateInstance != null))
        {
            int templateId = CMSContext.CurrentPageInfo.PageTemplateInfo.PageTemplateId;

            // Get all MVT zone variants of the current web part
            DataSet ds = ModuleCommands.OnlineMarketingGetMVTVariants(templateId, zoneId, Guid.Empty, 0);
            DataTable resultTable = null;

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataTable table = ds.Tables[0].Copy();
                table.DefaultView.Sort = columnVariantID;

                // Add the original web part as the first item in the variant list
                DataRow originalVariant = table.NewRow();
                originalVariant[columnVariantID] = 0;
                originalVariant[columnVariantDisplayName] = ResHelper.GetString("WebPartMenu.OriginalWebPart");
                originalVariant[columnVariantZoneID] = zoneId;
                originalVariant[columnVariantPageTemplateID] = templateId;
                originalVariant[columnVariantInstanceGUID] = Guid.Empty;
                table.Rows.InsertAt(originalVariant, 0);

                resultTable = table.DefaultView.ToTable();

                if (DataHelper.DataSourceIsEmpty(resultTable))
                {
                    pnlNoZoneMVTVariants.Visible = true;
                    lblNoZoneMVTVariants.Text = ResHelper.GetString("Content.NoPermissions");
                }
            }
            else
            {
                pnlNoZoneMVTVariants.Visible = true;
            }

            repZoneMVTVariants.DataSource = resultTable;
            repZoneMVTVariants.DataBind();
        }
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuWebPartVariants control.
    /// </summary>
    protected void menuZoneCPVariants_OnReloadData(object sender, EventArgs e)
    {
        // Check permissions
        if ((currentUser == null)
            || (!currentUser.IsAuthorizedPerResource("CMS.ContentPersonalization", "Read")))
        {
            return;
        }

        SetColumnNames(VariantModeEnum.ContentPersonalization);

        string parameters = ValidationHelper.GetString(menuZoneCPVariants.Parameter, string.Empty);
        string[] items = parameters.Split(new char[] { ',' }, 3);
        if ((items == null) || (items.Length != 3))
        {
            return;
        }

        string zoneId = ValidationHelper.GetString(items[0], string.Empty);
        string aliasPath = ValidationHelper.GetString(items[1], string.Empty);

        if ((CMSContext.CurrentPageInfo != null)
            && (CMSContext.CurrentPageInfo.TemplateInstance != null))
        {
            int templateId = CMSContext.CurrentPageInfo.PageTemplateInfo.PageTemplateId;

            DataSet ds = ModuleCommands.OnlineMarketingGetContentPersonalizationVariants(templateId, zoneId, Guid.Empty, 0);
            DataTable resultTable = null;

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataTable table = ds.Tables[0].Copy();
                table.DefaultView.Sort = columnVariantID;

                // Add the original web part as the first item in the variant list
                DataRow originalVariant = table.NewRow();
                originalVariant[columnVariantID] = 0;
                originalVariant[columnVariantDisplayName] = ResHelper.GetString("WebPartMenu.OriginalWebPart");
                originalVariant[columnVariantZoneID] = zoneId;
                originalVariant[columnVariantPageTemplateID] = templateId;
                originalVariant[columnVariantInstanceGUID] = Guid.Empty;
                table.Rows.InsertAt(originalVariant, 0);

                resultTable = table.DefaultView.ToTable();

                if (DataHelper.DataSourceIsEmpty(resultTable))
                {
                    pnlNoZoneCPVariants.Visible = true;
                    lblNoZoneCPVariants.Text = ResHelper.GetString("Content.NoPermissions");
                }
            }
            else
            {
                pnlNoZoneCPVariants.Visible = true;
            }

            repZoneCPVariants.DataSource = resultTable;
            repZoneCPVariants.DataBind();
        }
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuZoneVariants control.
    /// </summary>
    protected void menuMoveToZoneVariants_OnReloadData(object sender, EventArgs e)
    {
        // Check permissions
        if (currentUser == null)            
        {
            return;
        }

        if ((CMSContext.CurrentPageInfo != null)
            && (CMSContext.CurrentPageInfo.TemplateInstance != null))
        {
            VariantModeEnum currentVariantMode = VariantModeEnum.None;
            string targetZoneId = ValidationHelper.GetString(menuMoveToZoneVariants.Parameter, string.Empty);
            int pageTemplateId = CMSContext.CurrentPageInfo.PageTemplateInfo.PageTemplateId;

            // Get selected zone variant mode
            if ((CMSContext.CurrentPageInfo != null)
                && (CMSContext.CurrentPageInfo.TemplateInstance != null))
            {
                WebPartZoneInstance targetZone = CMSContext.CurrentPageInfo.TemplateInstance.GetZone(targetZoneId);
                if (targetZone != null)
                {
                    currentVariantMode = targetZone.VariantMode;
                }
            }

            SetColumnNames(currentVariantMode);

            // Get all zone variants of the current web part
            DataSet ds = null;
            DataTable resultTable = null;

            if (currentVariantMode == VariantModeEnum.MVT)
            {
                if (currentUser.IsAuthorizedPerResource("CMS.mvtest", "Read"))
                {
                    // MVT variants
                    ds = ModuleCommands.OnlineMarketingGetMVTVariants(pageTemplateId, targetZoneId, Guid.Empty, 0);
                }
            }
            else if (currentVariantMode == VariantModeEnum.ContentPersonalization)
            {
                if (currentUser.IsAuthorizedPerResource("CMS.ContentPersonalization", "Read"))
                {
                    // Content personalization variants
                    ds = ModuleCommands.OnlineMarketingGetContentPersonalizationVariants(pageTemplateId, targetZoneId, Guid.Empty, 0);
                }
            }

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataTable table = ds.Tables[0].Copy();
                table.DefaultView.Sort = columnVariantID;

                // Add the original web part as the first item in the variant list
                DataRow originalVariant = table.NewRow();
                originalVariant[columnVariantID] = 0;
                originalVariant[columnVariantDisplayName] = ResHelper.GetString("ZoneMenu.OriginalZone");
                originalVariant[columnVariantZoneID] = targetZoneId;
                originalVariant[columnVariantPageTemplateID] = pageTemplateId;
                originalVariant[columnVariantInstanceGUID] = Guid.Empty;
                table.Rows.InsertAt(originalVariant, 0);

                resultTable = table.DefaultView.ToTable();

                if (DataHelper.DataSourceIsEmpty(resultTable))
                {
                    pnlNoZoneVariants.Visible = true;
                    ltlNoZoneVariants.Text = ResHelper.GetString("Content.NoPermissions");
                }
            }

            this.repMoveToZoneVariants.DataSource = resultTable;
            this.repMoveToZoneVariants.DataBind();
        }
    }


    /// <summary>
    /// Sets the column names according to the variant mode.
    /// </summary>
    private void SetColumnNames(VariantModeEnum variantMode)
    {
        if (variantMode == VariantModeEnum.MVT)
        {
            // MVT column names
            columnVariantID = "MVTVariantID";
            columnVariantDisplayName = "MVTVariantDisplayName";
            columnVariantZoneID = "MVTVariantZoneID";
            columnVariantPageTemplateID = "MVTVariantPageTemplateID";
            columnVariantInstanceGUID = "MVTVariantInstanceGUID";
            updateCombinationPanelScript = "UpdateCombinationPanel();";
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            // Content personalization column names
            columnVariantID = "VariantID";
            columnVariantDisplayName = "VariantDisplayName";
            columnVariantZoneID = "VariantZoneID";
            columnVariantPageTemplateID = "VariantPageTemplateID";
            columnVariantInstanceGUID = "VariantInstanceGUID";
            updateCombinationPanelScript = string.Empty;
        }
    }

    #endregion
}
