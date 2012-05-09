using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_Edit_Options : CMSProductOptionsPage
{
    #region "Variables"

    protected int categoryId = 0;
    protected OptionCategoryInfo categoryObj = null;
    protected int editedSiteId = 0;
    protected bool allowActions = true;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "ProductOptions.Options"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "ProductOptions.Options");
        }

        // Get category ID and department ID from url
        categoryId = QueryHelper.GetInteger("categoryid", 0);
        categoryObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryId);

        EditedObject = categoryObj;

        if (categoryObj != null)
        {
            editedSiteId = categoryObj.CategorySiteID;

            // Check edited objects site id
            CheckEditedObjectSiteID(editedSiteId);

            // Allow actions only for non-text categories
            allowActions = (categoryObj.CategorySelectionType != OptionCategorySelectionTypeEnum.TextBox) && (categoryObj.CategorySelectionType != OptionCategorySelectionTypeEnum.TextArea);

            if (allowActions)
            {
                string[,] actions = new string[2, 7];

                // New item link
                actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
                actions[0, 1] = GetString("ProductOptions.NewItemCaption");
                actions[0, 2] = null;
                actions[0, 3] = ResolveUrl("~/CMSModules/Ecommerce/Pages/Tools/Products/Product_New.aspx?categoryid=" + categoryId + "&siteId=" + SiteID);
                actions[0, 4] = null;
                actions[0, 5] = GetImageUrl("Objects/Ecommerce_SKU/add.png");

                // Sort link & img
                actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
                actions[1, 1] = GetString("ProductOptions.SortAlphabetically");
                actions[1, 2] = null;
                actions[1, 3] = null;
                actions[1, 4] = null;
                actions[1, 5] = GetImageUrl("CMSModules/CMS_Ecommerce/optionssort.png");
                actions[1, 6] = "lnkSort_Click";

                this.CurrentMaster.HeaderActions.Actions = actions;
                this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);
            }

            // Unigrid
            grid.OnAction += new OnActionEventHandler(grid_OnAction);
            grid.OnExternalDataBound += new OnExternalDataBoundEventHandler(grid_OnExternalDataBound);
            grid.WhereCondition = "SKUOptionCategoryID = " + categoryId;
        }
    }

    #endregion


    #region "Event handlers"

    object grid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "skuenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "skuprice":
                DataRowView row = (DataRowView)parameter;
                double value = ValidationHelper.GetDouble(row["SKUPrice"], 0);
                int siteId = ValidationHelper.GetInteger(row["SKUSiteID"], 0);

                // Format price
                return CurrencyInfoProvider.GetFormattedPrice(value, siteId);

            case "delete":
            case "moveup":
            case "movedown":
                {
                    ImageButton button = sender as ImageButton;
                    if (button != null)
                    {
                        // Hide actions when not allowed
                        button.Visible = allowActions;
                    }
                }
                break;
        }

        return parameter;
    }


    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "lnksort_click":
                if (allowActions)
                {
                    // Check permissions
                    CheckModifyPermission();

                    SKUInfoProvider.SortProductOptionsAlphabetically(categoryId);
                    grid.ReloadData();
                }
                break;
        }
    }


    void grid_OnAction(string actionName, object actionArgument)
    {
        int skuId = ValidationHelper.GetInteger(actionArgument, 0);

        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("~/CMSModules/Ecommerce/Pages/Tools/Products/Product_Edit_Frameset.aspx?categoryid=" + categoryId + "&productid=" + skuId + "&siteId=" + categoryObj.CategorySiteID);
                break;

            case "delete":
                // Check permissions
                CheckModifyPermission();

                // Check dependencies
                if (SKUInfoProvider.CheckDependencies(skuId))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Ecommerce.DeleteDisabled");
                    return;
                }

                SKUInfoProvider.DeleteSKUInfo(skuId);
                grid.ReloadData();

                break;

            case "moveup":
                // Check permissions
                CheckModifyPermission();

                SKUInfoProvider.MoveSKUOptionUp(skuId);
                break;

            case "movedown":
                // Check permissions
                CheckModifyPermission();

                SKUInfoProvider.MoveSKUOptionDown(skuId);
                break;
        }
    }


    private void CheckModifyPermission()
    {
        // Check permissions
        bool global = (editedSiteId <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifyOptionCategory(global))
        {
            if (global)
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
            }
        }
    }

    #endregion
}
