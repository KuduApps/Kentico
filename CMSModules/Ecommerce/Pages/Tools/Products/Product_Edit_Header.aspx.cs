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
using CMS.FormEngine;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_Header : CMSProductsPage
{
    #region "Variables"

    protected int productId = 0;
    protected string productName = "";
    protected int optionCategoryId = 0;
    protected int siteId = 0;

    bool dialogMode = false;

    #endregion


    protected void Page_PreInit(object sender, EventArgs e)
    {
        optionCategoryId = QueryHelper.GetInteger("categoryid", 0);

        if (optionCategoryId > 0)
        {
            this.IsProductOption = true;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get product name ane option category ID
        productId = QueryHelper.GetInteger("productId", 0);
        if (productId > 0)
        {
            SKUInfo productInfoObj = SKUInfoProvider.GetSKUInfo(productId);
            if (productInfoObj != null)
            {
                productName = ResHelper.LocalizeString(productInfoObj.SKUName);
                optionCategoryId = productInfoObj.SKUOptionCategoryID;
                siteId = productInfoObj.SKUSiteID;

                // Check if edited object belongs to configured site
                CheckEditedObjectSiteID(siteId);
            }
        }

        this.dialogMode = QueryHelper.GetBoolean("dialogmode", false);

        AddMenuButtonSelectScript("Products", "");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string productList = "";
        string productListText = "";
        string targetFrame = "";
        string titleText = "";
        string titleImage = "";

        // When editing product option
        if (optionCategoryId > 0)
        {
            productListText = GetString("Prodect_Edit_Header.ProductOptionsLink");
            productList = "~/CMSModules/Ecommerce/Pages/Tools/ProductOptions/OptionCategory_Edit_Options.aspx?categoryid=" + optionCategoryId + "&siteId=" + SiteID;
            targetFrame = "OptionCategoryEdit";
        }
        // When editing product
        else
        {
            productListText = GetString("Product_Edit_Header.ItemListLink");
            productList = "~/CMSModules/Ecommerce/Pages/Tools/Products/Product_List.aspx?siteId=" + SiteID;
            targetFrame = "ecommerceContent";

            titleText = GetString("Product_Edit_Header.HeaderCaption");
            titleImage = GetImageUrl("Objects/Ecommerce_SKU/object.png");
        }

        int hideBreadcrumbs = QueryHelper.GetInteger("hidebreadcrumbs", 0);

        if (hideBreadcrumbs == 0)
        {
            // initializes page title control		
            IntializeBreadcrumbs(productListText, productList, targetFrame, productName);
        }

        // Ensure page with changes saved message is loaded initially if required
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            this.CurrentMaster.Tabs.StartPageURL = "Product_Edit_General.aspx" + URLHelper.Url.Query;
        }

        // Initialize the master page elements
        IntializeMasterPage(titleImage, titleText);
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    /// <param name="productListText">Text of the product list</param>
    /// <param name="productList">Product list</param>
    /// <param name="targetFrame">Name of the target frame</param>
    /// <param name="productName">Name of the current product</param>
    private void IntializeBreadcrumbs(string productListText, string productList, string targetFrame, string productName)
    {
        // Set the master page breadcrumb property
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = productListText;
        pageTitleTabs[0, 1] = (this.dialogMode) ? null : productList;
        pageTitleTabs[0, 2] = targetFrame;
        pageTitleTabs[1, 0] = FormatBreadcrumbObjectName(productName, siteId);
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    /// <param name="titleImage">Title of the page title element</param>
    /// <param name="titleText">URL of the image of the page title element</param>
    private void IntializeMasterPage(string titleImage, string titleText)
    {
        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        // Set the master page title
        master.Title.HelpTopicName = "general_tab15";
        master.Title.HelpName = "helpTopic";

        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
        master.Tabs.UrlTarget = "ProductContent";
        if (optionCategoryId > 0)
        {
            master.Tabs.ModuleName = "CMS.Ecommerce";
            master.Tabs.ElementName = "ProductOptions.Options";
        }
        else
        {
            master.Tabs.ModuleName = "CMS.Ecommerce";
            master.Tabs.ElementName = "Products";
        }

        // Set master title
        master.Title.TitleText = titleText;
        master.Title.TitleImage = titleImage;
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        if ((element.ElementName.ToLower() == "products.customfields") || (element.ElementName.ToLower() == "productoptions.options.customfields"))
        {
            // Check if SKU has any custom fields
            FormInfo formInfo = FormHelper.GetFormInfo("ecommerce.sku", false);
            if (formInfo.GetFormElements(true, false, true).Count <= 0)
            {
                return null;
            }
        }

        // Add SiteId parameter to each tab
        if (parameters.Length > 2)
        {
            parameters[2] = URLHelper.AddParameterToUrl(parameters[2], "siteId", SiteID.ToString());
        }

        return parameters;
    }
}
