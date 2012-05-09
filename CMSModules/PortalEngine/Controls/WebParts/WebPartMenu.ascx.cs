using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.ExtendedControls;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebPartMenu : CMSAbstractPortalUserControl
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
        if (!currentUser.IsAuthorizedPerResource("cms.mvtest", "Manage"))
        {
            plcAddMVTVariant.Visible = false;
        }

        if (!currentUser.IsAuthorizedPerResource("cms.contentpersonalization", "Manage"))
        {
            plcAddCPVariant.Visible = false;
        }

        // Main menu
        this.iProperties.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Properties.png");
        this.iProperties.Text = ResHelper.GetString("WebPartMenu.IconProperties", culture);
        this.iProperties.Attributes.Add("onclick", "ContextConfigureWebPart(GetContextMenuParameter('webPartMenu'));");

        // Up
        this.iUp.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Up.png");
        this.iUp.Text = ResHelper.GetString("WebPartMenu.IconUp", culture);
        this.iUp.Attributes.Add("onclick", "ContextMoveWebPartUp(GetContextMenuParameter('webPartMenu'));");

        // Down
        this.iDown.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Down.png");
        this.iDown.Text = ResHelper.GetString("WebPartMenu.IconDown", culture);
        this.iDown.Attributes.Add("onclick", "ContextMoveWebPartDown(GetContextMenuParameter('webPartMenu'));");

        // Move to
        this.iMoveTo.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/MoveTo.png");
        this.iMoveTo.Text = ResHelper.GetString("WebPartMenu.IconMoveTo", culture);

        // Clone
        this.iClone.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Clonewebpart.png");
        this.iClone.Text = ResHelper.GetString("WebPartMenu.IconClone", culture);
        this.iClone.Attributes.Add("onclick", "ContextCloneWebPart(GetContextMenuParameter('webPartMenu'));");

        // Delete
        this.iDelete.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Delete.png");
        this.iDelete.Text = ResHelper.GetString("general.remove", culture);
        this.iDelete.Attributes.Add("onclick", "ContextRemoveWebPart(GetContextMenuParameter('webPartMenu'));");

        // Up menu - Bottom
        this.iTop.Text = ResHelper.GetString("UpMenu.IconTop", culture);
        this.iTop.Attributes.Add("onclick", "ContextMoveWebPartTop(GetContextMenuParameter('webPartMenu'));");
        this.iTop.Attributes.Add("onmouseover", "CM_Disable(this); CM_Cancel('" + menuMoveTo.MenuID + "');");

        // Down menu - Bottom
        this.iBottom.Text = ResHelper.GetString("DownMenu.IconBottom", culture);
        this.iBottom.Attributes.Add("onclick", "ContextMoveWebPartBottom(GetContextMenuParameter('webPartMenu'));");
        this.iBottom.Attributes.Add("onmouseover", "CM_Disable(this); CM_Cancel('" + menuMoveTo.MenuID + "');");

        // Add new MVT variant
        this.lblAddMVTVariant.Text = ResHelper.GetString("WebPartMenu.AddWebPartVariant", culture);
        this.imgAddMVTVariant.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/addWebPart.png");
        this.imgAddMVTVariant.AlternateText = this.lblAddMVTVariant.Text;
        this.pnlAddMVTVariant.Attributes.Add("onclick", "ContextAddWebPartMVTVariant(GetContextMenuParameter('webPartMenu'));");

        // Add new Content personalization variant
        this.lblAddCPVariant.Text = ResHelper.GetString("WebPartMenu.AddWebPartVariant", culture);
        this.imgAddCPVariant.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/addWebPart.png");
        this.imgAddCPVariant.AlternateText = this.lblAddCPVariant.Text;
        this.pnlAddCPVariant.Attributes.Add("onclick", "ContextAddWebPartCPVariant(GetContextMenuParameter('webPartMenu'));");

        // List all variants
        this.lblMVTVariants.Text = ResHelper.GetString("WebPartMenu.WebPartMVTVariants", culture);
        this.lblCPVariants.Text = ResHelper.GetString("WebPartMenu.WebPartPersonalizationVariants", culture);

        // No MVT variants
        lblNoWebPartMVTVariants.Text = ResHelper.GetString("ZoneMenu.NoVariants");
        imgNoWebPartMVTVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/novariant.png");

        // No CP variants
        lblNoWebPartCPVariants.Text = ResHelper.GetString("ZoneMenu.NoVariants");
        imgNoWebPartCPVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/novariant.png");

        this.imgMVTVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/webPartList.png");
        this.imgMVTVariants.AlternateText = this.lblMVTVariants.Text;

        this.imgCPVariants.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/webPartList.png");
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
            menuMoveToZoneVariants.LoadingContent = "<div class=\"PortalContextMenu WebPartContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading") + "</div></div>";
            menuMoveToZoneVariants.OnReloadData += menuMoveToZoneVariants_OnReloadData;
            repMoveToZoneVariants.ItemDataBound += repMoveToZoneVariants_ItemDataBound;

            // Display the MVT menu part in the CMSDesk->Design only. Hide the context menu in the SM->PageTemplates->Design
            if (PortalContext.MVTVariantsEnabled && (CMSContext.CurrentPageInfo != null) && (CMSContext.CurrentPageInfo.DocumentId > 0))
            {
                // Set Display='none' for the MVT panel. Show dynamically only if required.
                pnlContextMenuMVTVariants.Visible = true;
                pnlContextMenuMVTVariants.Style.Add("display", "none");
                menuWebPartMVTVariants.LoadingContent = "<div class=\"PortalContextMenu WebPartContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading", CultureHelper.GetPreferredUICulture()) + "</div></div>";
                menuWebPartMVTVariants.OnReloadData += menuWebPartMVTVariants_OnReloadData;
                repWebPartMVTVariants.ItemDataBound += repWebPartVariants_ItemDataBound;

                string script = "webPartMVTVariantContextMenuId = '" + pnlContextMenuMVTVariants.ClientID + "';";
                ScriptHelper.RegisterStartupScript(this, typeof(string), "webPartMVTVariantContextMenuId", ScriptHelper.GetScript(script));
            }
            else
            {
                // Hide the MVT variant context menu items when MVT is not enabled for the current document
                pnlUIMVTVariants.Visible = false;
            }

            // Display the Content personalization menu part in the CMSDesk->Design only. Hide the context menu in the SM->PageTemplates->Design
            if ((PortalContext.ContentPersonalizationEnabled) && (CMSContext.CurrentPageInfo != null) && (CMSContext.CurrentPageInfo.DocumentId > 0))
            {
                // Set Display='none' for the MVT panel. Show dynamically only if required.
                pnlContextMenuCPVariants.Visible = true;
                pnlContextMenuCPVariants.Style.Add("display", "none");
                menuWebPartCPVariants.LoadingContent = "<div class=\"PortalContextMenu WebPartContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading", CultureHelper.GetPreferredUICulture()) + "</div></div>";
                menuWebPartCPVariants.OnReloadData += menuWebPartCPVariants_OnReloadData;
                repWebPartCPVariants.ItemDataBound += repWebPartVariants_ItemDataBound;

                string script = "webPartCPVariantContextMenuId = '" + pnlContextMenuCPVariants.ClientID + "';";
                ScriptHelper.RegisterStartupScript(this, typeof(string), "webPartCPVariantContextMenuId", ScriptHelper.GetScript(script));
            }
            else
            {
                // Hide the Content personalization variant context menu items when the Content Personalization is not enabled.
                pnlUICPVariants.Visible = false;
            }
        }
    }


    /// <summary>
    /// Handles the ItemDataBound event of the repWebPartVariants control.
    /// </summary>
    protected void repWebPartVariants_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            // Get unique web part code
            Guid instanceGuid = ValidationHelper.GetGuid(((DataRowView)e.Item.DataItem)[columnVariantInstanceGUID], Guid.Empty);
            int variantId = ValidationHelper.GetInteger(((DataRowView)e.Item.DataItem)[columnVariantID], 0);
            string itemCode = "Variant_WP_" + instanceGuid.ToString("N");

            // Display the web part variant when clicked
            pnlVariantItem.Attributes.Add("onclick", "SetVariant('" + itemCode + "', " + variantId + "); " + updateCombinationPanelScript);
        }

        // Display web part icon for each of the web part variants
        Image imgZoneVariant = (Image)e.Item.FindControl("imgVariantItem");
        if (imgZoneVariant != null)
        {
            imgZoneVariant.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/webPart.png");
        }
    }

    /// <summary>
    /// Handles the ItemDataBound event of the repZoneVariants control.
    /// </summary>
    protected void repMoveToZoneVariants_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            pnlZoneVariantItem.Attributes.Add("onclick", "ContextMoveWebPartToZone(GetContextMenuParameter('webPartMenu'), '" + zoneId + "', " + variantId + ");");
        }
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuWebPartVariants control.
    /// </summary>
    protected void menuWebPartMVTVariants_OnReloadData(object sender, EventArgs e)
    {
        // Check permissions
        if ((currentUser == null)
            || (!currentUser.IsAuthorizedPerResource("CMS.MVTest", "Read")))
        {
            return;
        }

        SetColumnNames(VariantModeEnum.MVT);

        string parameters = ValidationHelper.GetString(menuWebPartMVTVariants.Parameter, string.Empty);
        string[] items = parameters.Split(new char[] { ',' }, 5);
        if ((items == null) || (items.Length != 5))
        {
            return;
        }

        string zoneId = ValidationHelper.GetString(items[0], string.Empty);
        string webpartName = ValidationHelper.GetString(items[1], string.Empty);
        string aliasPath = ValidationHelper.GetString(items[2], string.Empty);
        Guid instanceGuid = ValidationHelper.GetGuid(items[3], Guid.Empty);

        if ((CMSContext.CurrentPageInfo != null)
            && (CMSContext.CurrentPageInfo.TemplateInstance != null))
        {
            int templateId = CMSContext.CurrentPageInfo.PageTemplateInfo.PageTemplateId;

            // Get all MVT zone variants of the current web part
            DataSet ds = ModuleCommands.OnlineMarketingGetMVTVariants(templateId, zoneId, instanceGuid, 0);
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
                originalVariant[columnVariantInstanceGUID] = instanceGuid;
                table.Rows.InsertAt(originalVariant, 0);

                resultTable = table.DefaultView.ToTable();

                if (DataHelper.DataSourceIsEmpty(resultTable))
                {
                    pnlNoWebPartMVTVariants.Visible = true;
                    lblNoWebPartMVTVariants.Text = ResHelper.GetString("Content.NoPermissions");
                }
            }
            else
            {
                pnlNoWebPartMVTVariants.Visible = true;
            }

            repWebPartMVTVariants.DataSource = resultTable;
            repWebPartMVTVariants.DataBind();
        }
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuWebPartVariants control.
    /// </summary>
    protected void menuWebPartCPVariants_OnReloadData(object sender, EventArgs e)
    {
        // Check permissions
        if ((currentUser == null)
            || (!currentUser.IsAuthorizedPerResource("CMS.ContentPersonalization", "Read")))
        {
            return;
        }

        SetColumnNames(VariantModeEnum.ContentPersonalization);

        string parameters = ValidationHelper.GetString(menuWebPartCPVariants.Parameter, string.Empty);
        string[] items = parameters.Split(new char[] { ',' }, 5);
        if ((items == null) || (items.Length != 5))
        {
            return;
        }

        string zoneId = ValidationHelper.GetString(items[0], string.Empty);
        string webpartName = ValidationHelper.GetString(items[1], string.Empty);
        string aliasPath = ValidationHelper.GetString(items[2], string.Empty);
        Guid instanceGuid = ValidationHelper.GetGuid(items[3], Guid.Empty);

        if ((CMSContext.CurrentPageInfo != null)
            && (CMSContext.CurrentPageInfo.TemplateInstance != null))
        {
            int templateId = CMSContext.CurrentPageInfo.PageTemplateInfo.PageTemplateId;

            DataSet ds = ModuleCommands.OnlineMarketingGetContentPersonalizationVariants(templateId, zoneId, instanceGuid, 0);
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
                originalVariant[columnVariantInstanceGUID] = instanceGuid;
                table.Rows.InsertAt(originalVariant, 0);

                resultTable = table.DefaultView.ToTable();

                if (DataHelper.DataSourceIsEmpty(resultTable))
                {
                    pnlNoWebPartCPVariants.Visible = true;
                    lblNoWebPartCPVariants.Text = ResHelper.GetString("Content.NoPermissions");
                }
            }
            else
            {
                pnlNoWebPartCPVariants.Visible = true;
            }

            repWebPartCPVariants.DataSource = resultTable;
            repWebPartCPVariants.DataBind();
        }
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuZoneVariants control.
    /// </summary>
    protected void menuMoveToZoneVariants_OnReloadData(object sender, EventArgs e)
    {
        if ((CMSContext.CurrentPageInfo != null)
            && (CMSContext.CurrentPageInfo.TemplateInstance != null))
        {
            string targetZoneId = ValidationHelper.GetString(menuMoveToZoneVariants.Parameter, string.Empty);
            int pageTemplateId = CMSContext.CurrentPageInfo.PageTemplateInfo.PageTemplateId;
            VariantModeEnum currentVariantMode = VariantModeEnum.None;

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
                if (currentUser.IsAuthorizedPerResource("CMS.MVTest", "Read"))
                {
                    // Get all MVT zone variants of the current web part
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
