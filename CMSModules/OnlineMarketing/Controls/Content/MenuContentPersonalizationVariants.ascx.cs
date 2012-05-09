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
using CMS.CMSHelper;
using CMS.PortalControls;
using CMS.SettingsProvider;

public partial class CMSModules_OnlineMarketing_Controls_Content_MenuContentPersonalizationVariants : CMSAbstractPortalUserControl
{
    #region "Variables"

    private string columnVariantID = "VariantID";
    private string columnVariantDisplayName = "VariantDisplayName";
    private string columnVariantZoneID = "VariantZoneID";
    private string columnVariantPageTemplateID = "VariantPageTemplateID";
    private string columnVariantInstanceGUID = "VariantInstanceGUID";
    private bool isZone = false;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlWebPartMenu.Attributes.Add("onmouseover", "ActivateParentBorder();");

        menuWebPartCPVariants.LoadingContent = "<div class=\"PortalContextMenu WebPartContextMenu\"><div class=\"ItemPadding\">" + ResHelper.GetString("ContextMenu.Loading") + "</div></div>";
        menuWebPartCPVariants.OnReloadData += menuWebPartCPVariants_OnReloadData;
        repWebPartCPVariants.ItemDataBound += repWebPartCPVariants_ItemDataBound;
    }


    /// <summary>
    /// Handles the OnReloadData event of the menuWebPartCPVariants control.
    /// </summary>
    protected void menuWebPartCPVariants_OnReloadData(object sender, EventArgs e)
    {
        // Check permissions
        if ((CMSContext.CurrentUser == null)
            || (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ContentPersonalization", "Read")))
        {
            return;
        }

        string parameters = ValidationHelper.GetString(menuWebPartCPVariants.Parameter, string.Empty);
        string[] items = parameters.Split(new char[] { ',' }, 5);

        isZone = (items.Length == 3);
        if ((items == null))
        {
            return;
        }

        string zoneId = string.Empty;
        string webpartName = string.Empty;
        string aliasPath = string.Empty;
        Guid instanceGuid = Guid.Empty;

        if (isZone)
        {
            zoneId = ValidationHelper.GetString(items[0], string.Empty);
            aliasPath = ValidationHelper.GetString(items[1], string.Empty);
        }
        else
        {
            zoneId = ValidationHelper.GetString(items[0], string.Empty);
            webpartName = ValidationHelper.GetString(items[1], string.Empty);
            aliasPath = ValidationHelper.GetString(items[2], string.Empty);
            instanceGuid = ValidationHelper.GetGuid(items[3], Guid.Empty);
        }

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
                originalVariant[columnVariantDisplayName] = ResHelper.GetString(isZone ? "ZoneMenu.OriginalZone" : "WebPartMenu.OriginalWebPart");
                originalVariant[columnVariantZoneID] = zoneId;
                originalVariant[columnVariantPageTemplateID] = templateId;
                originalVariant[columnVariantInstanceGUID] = instanceGuid;
                table.Rows.InsertAt(originalVariant, 0);

                resultTable = table.DefaultView.ToTable();
            }

            repWebPartCPVariants.DataSource = resultTable;
            repWebPartCPVariants.DataBind();
        }
    }


    /// <summary>
    /// Handles the ItemDataBound event of the repWebPartVariants control.
    /// </summary>
    protected void repWebPartCPVariants_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Panel pnlVariantItem = (Panel)e.Item.FindControl("pnlVariantItem");
        if (pnlVariantItem != null)
        {
            Label lblVariantName = pnlVariantItem.FindControl("lblVariantItem") as Label;
            if (lblVariantName != null)
            {
                lblVariantName.Text = HTMLHelper.HTMLEncode((((DataRowView)e.Item.DataItem)[columnVariantDisplayName]).ToString());
            }

            // Get unique web part code
            Guid instanceGuid = ValidationHelper.GetGuid(((DataRowView)e.Item.DataItem)[columnVariantInstanceGUID], Guid.Empty);
            int variantId = ValidationHelper.GetInteger(((DataRowView)e.Item.DataItem)[columnVariantID], 0);
            string itemCode = string.Empty;
            if (isZone)
            {
                string zoneId = ValidationHelper.GetString(((DataRowView)e.Item.DataItem)[columnVariantZoneID], string.Empty);
                itemCode = "Variant_Zone_" + zoneId;
            }
            else
            {
                itemCode = "Variant_WP_" + instanceGuid.ToString("N");
            }

            // Display the web part variant when clicked
            pnlVariantItem.Attributes.Add("onclick", "variantSliderChanged=true; UpdateVariantPosition('" + itemCode + "', " + variantId + "); RefreshPage();");
        }

        // Display web part icon for each of the web part variants
        Image imgVariantItem = (Image)e.Item.FindControl("imgVariantItem");
        if (imgVariantItem != null)
        {
            imgVariantItem.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Variants/" + (isZone ? "zone.png" : "webPart.png"));
        }
    }

    #endregion
}
