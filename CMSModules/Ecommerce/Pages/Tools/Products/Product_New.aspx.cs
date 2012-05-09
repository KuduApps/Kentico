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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_New : CMSProductsPage, IPostBackEventHandler
{
    #region "Variables"

    protected int optionCategoryId = 0;
    protected int newObjSiteId = -1;
    protected bool createAnotherAfterSave = false;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get option category ID from url
        optionCategoryId = QueryHelper.GetInteger("categoryid", 0);

        newObjSiteId = ConfiguredSiteID;

        // Creating new product option
        if (optionCategoryId > 0)
        {
            GlobalObjectsKeyName = ECommerceSettings.ALLOW_GLOBAL_PRODUCT_OPTIONS;
            OptionCategoryInfo oci = OptionCategoryInfoProvider.GetOptionCategoryInfo(optionCategoryId);

            // Check edited object
            EditedObject = oci;

            // New product option wil be bound to same site as option category
            if (oci != null)
            {
                // Check edited site id
                CheckEditedObjectSiteID(oci.CategorySiteID);

                newObjSiteId = oci.CategorySiteID;
            }
        }
        // Creating new product
        else
        {
            GlobalObjectsKeyName = ECommerceSettings.ALLOW_GLOBAL_PRODUCTS;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI element
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "NewProduct"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "NewProduct");
        }

        string currentSKU = "";
        string productList = "";
        string productListText = "";

        this.productEditElem.ProductSiteID = newObjSiteId;

        // Creating new product option
        if (optionCategoryId > 0)
        {
            this.productEditElem.OptionCategoryID = optionCategoryId;

            currentSKU = GetString("Product_New.NewProductOption");
            productList = "~/CMSModules/Ecommerce/Pages/Tools/ProductOptions/OptionCategory_Edit_Options.aspx?categoryid=" + optionCategoryId + "&siteId=" + SiteID;
            productListText = GetString("Product_New.ProductOptionsLink");

            string titleText = "";
            string titleImage = "";

            // Initialize the master page elements
            InitializeMasterPage(titleText, titleImage, productListText, productList, currentSKU);

            AddMenuButtonSelectScript("ProductOptions", "");
        }
        // Creating new product
        else
        {
            currentSKU = GetString("com_SKU_edit_general.NewItemCaption");
            productList = "~/CMSModules/Ecommerce/Pages/Tools/Products/Product_List.aspx?siteId=" + SiteID;
            productListText = GetString("Product_Edit_Header.ItemListLink");

            string titleText = GetString("com_SKU_edit_general.NewItemCaption");
            string titleImage = GetImageUrl("Objects/Ecommerce_SKU/new.png");

            // Initialize the master page elements
            InitializeMasterPage(titleText, titleImage, productListText, productList, currentSKU);

            AddMenuButtonSelectScript("NewProduct", "");
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Initialize the master page elements.
    /// </summary>
    /// <param name="titleText">Text of the title</param>
    /// <param name="titleImage">Image URL for the title</param>
    private void InitializeMasterPage(string titleText, string titleImage, string productListText, string productList, string currentSKU)
    {
        // Set the master page title
        this.CurrentMaster.Title.HelpTopicName = "general_tab15";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = productListText;
        breadcrumbs[0, 1] = productList;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentSKU, newObjSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set master title
        this.CurrentMaster.Title.TitleText = titleText;
        this.CurrentMaster.Title.TitleImage = titleImage;

        // Set header actions
        string[,] actions = new string[2, 10];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = this.GetString("Header.Settings.SaveChanged");
        actions[0, 2] = String.Empty;
        actions[0, 3] = String.Empty;
        actions[0, 4] = this.GetString("general.savectrls");
        actions[0, 5] = this.productEditElem.FormEnabled ? this.GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png") : this.GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
        actions[0, 6] = "save";
        actions[0, 7] = String.Empty;
        actions[0, 8] = this.productEditElem.FormEnabled.ToString();
        actions[0, 9] = this.productEditElem.FormEnabled.ToString();

        actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[1, 1] = this.GetString("editmenu.iconsaveandanother");
        actions[1, 2] = String.Empty;
        actions[1, 3] = String.Empty;
        actions[1, 4] = actions[1, 1];
        actions[1, 5] = this.productEditElem.FormEnabled ? this.GetImageUrl("CMSModules/CMS_Content/EditMenu/saveandanother.png") : this.GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
        actions[1, 6] = "saveAndNew";
        actions[1, 7] = String.Empty;
        actions[1, 9] = this.productEditElem.FormEnabled.ToString();

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += this.HeaderActions_ActionPerformed;
    }


    protected void productEditElem_ProductSaved(object sender, EventArgs e)
    {
        if (createAnotherAfterSave)
        {
            URLHelper.Redirect("Product_New.aspx?siteId=" + SiteID + "&saved=1&categoryId=" + optionCategoryId);
        }
        else
        {
            int productId = ValidationHelper.GetInteger(productEditElem.ProductID, 0);
            if (productId > 0)
            {
                URLHelper.Redirect("Product_Edit_Frameset.aspx?categoryId=" + optionCategoryId + "&productID=" + productId + "&saved=1&siteId=" + SiteID);
            }
        }
    }


    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                createAnotherAfterSave = false;
                this.productEditElem.Save();
                break;

            case "saveandnew":
                createAnotherAfterSave = true;
                this.productEditElem.Save();
                break;
        }
    }

    #endregion


    #region IPostBackEventHandler Members

    /// <summary>
    /// Handles postback events.
    /// </summary>
    /// <param name="eventArgument">Postback argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        switch (eventArgument.ToLower())
        {
            case "save":
                createAnotherAfterSave = false;
                this.productEditElem.Save();
                break;
        }
    }

    #endregion
}
