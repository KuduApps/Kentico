using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_VolumeDiscount_List : CMSProductsPage
{
    #region "Variables"

    private int productId = 0;
    private SKUInfo product = null;
    private CurrencyInfo productCurrency = null;
    private bool disableEditing = false;

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Products.VolumeDiscounts"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Products.VolumeDiscounts");
        }

        productId = QueryHelper.GetInteger("productId", 0);
        product = SKUInfoProvider.GetSKUInfo(productId);

        EditedObject = product;

        if (product != null)
        {
            // Check site id
            CheckEditedObjectSiteID(product.SKUSiteID);

            // Load product currency
            productCurrency = CurrencyInfoProvider.GetMainCurrency(product.SKUSiteID);
        }

        // Set unigrid properties
        SetUnigridProperties();

        // Initialize the master page elements
        InitializeMasterPage();
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set URL of new volume discount editing page
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Product_Edit_VolumeDiscount_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Products/Product_Edit_VolumeDiscount_Edit.aspx?ProductID=" + productId + "&siteId=" + SiteID;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_VolumeDiscount/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName.ToLower() == "edit")
        {
            URLHelper.Redirect("Product_Edit_VolumeDiscount_Edit.aspx?productID=" + productId + "&volumeDiscountID=" + Convert.ToString(actionArgument) + "&siteId=" + SiteID);
        }
        else if (actionName.ToLower() == "delete")
        {
            SKUInfo sku = SKUInfoProvider.GetSKUInfo(productId);
            if (sku == null)
            {
                return;
            }

            if (CheckProductPermissions(sku))
            {
                // Delete VolumeDiscountInfo object from database
                VolumeDiscountInfoProvider.DeleteVolumeDiscountInfo(Convert.ToInt32(actionArgument));
            }
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnExternalDataBound event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Parameter</param>
    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "discountvalue":
                DataRowView row = (DataRowView)parameter;
                double value = ValidationHelper.GetDouble(row["VolumeDiscountValue"], 0);
                bool isFlat = ValidationHelper.GetBoolean(row["VolumeDiscountIsFlatValue"], false);
                // If value is relative, add "%" next to the value.
                if (isFlat)
                {
                    return CurrencyInfoProvider.GetFormattedPrice(value, productCurrency);
                }
                else
                {
                    return value.ToString() + "%";
                }
            case "edit":
                if (disableEditing)
                {
                    ImageButton editButton = ((ImageButton)sender);
                    editButton.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Editdisabled.png");
                    editButton.OnClientClick = "return false;";
                }
                break;

            case "delete":
                if (disableEditing)
                {
                    ImageButton deleteButton = ((ImageButton)sender);
                    deleteButton.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Deletedisabled.png");
                    deleteButton.OnClientClick = "return false;";
                }
                break;
        }
        return parameter;
    }


    protected void SetUnigridProperties()
    {
        UniGrid.OnAction += new OnActionEventHandler(UniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.WhereCondition = "VolumeDiscountSKUID = " + productId;
        UniGrid.OrderBy = "VolumeDiscountMinCount ASC";
    }

    #endregion
}
