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
using System.ComponentModel;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_AddItems : CMSOrdersModalPage
{
    #region "Variables"

    protected int orderId = 0;
    protected ShoppingCartInfo mShoppingCartObj = null;
    protected bool allowGlobalProducts = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Shopping cart object with order data.
    /// </summary>
    protected ShoppingCartInfo ShoppingCartObj
    {
        get
        {
            if (mShoppingCartObj == null)
            {
                string cartSessionName = QueryHelper.GetString("cart", "");
                if (cartSessionName != "")
                {
                    mShoppingCartObj = SessionHelper.GetValue(cartSessionName) as ShoppingCartInfo;
                }
            }
            return mShoppingCartObj;
        }
    }


    /// <summary>
    /// Shopping cart items selector SKU ID.
    /// </summary>
    private int SKUID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["SKUID"], 0);
        }
        set
        {
            ViewState["SKUID"] = value;
        }
    }

    #endregion


    #region "Page methods"

    protected override void OnPreInit(EventArgs e)
    {
        // Get customer ID
        CustomerID = QueryHelper.GetInteger("customerid", 0);

        // Check whether global products are allowed
        allowGlobalProducts = ECommerceSettings.AllowGlobalProducts(CMSContext.CurrentSiteName);

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.Items"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.Items");
        }

        lblProductName.Text = GetString("Order_Edit_AddItems.ProductName");
        lblProductCode.Text = GetString("Order_Edit_AddItems.ProductCode");
        lblDepartment.Text = GetString("Order_Edit_AddItems.Department");
        btnAdd.Text = GetString("Order_Edit_AddItems.Add");
        btnShow.Text = GetString("general.show");

        if (CultureHelper.IsUICultureRTL())
        {
            CSSHelper.RegisterCSSBlock(this, ".AddToCartContainer { text-align: left; }");
            GridViewProducts.RowStyle.HorizontalAlign = HorizontalAlign.Right;
        }
        else
        {
            CSSHelper.RegisterCSSBlock(this, ".AddToCartContainer { text-align: right; }");
            GridViewProducts.RowStyle.HorizontalAlign = HorizontalAlign.Left;
        }

        if (!RequestHelper.IsPostBack())
        {
            // Display products
            plcProducts.Visible = true;

            // Hide shopping cart item selector
            plcSelector.Visible = false;
        }

        // Display global departments when using global products
        if (allowGlobalProducts)
        {
            departmentElem.DisplayGlobalItems = true;
        }

        bool isAdmin = (CMSContext.CurrentUser != null) && (CMSContext.CurrentUser.IsGlobalAdministrator);

        GridViewProducts.Columns[0].HeaderText = "<span style=\"padding-left:10px;\">" + GetString("Order_Edit_AddItems.GridProductName") + "</span>";
        GridViewProducts.Columns[1].HeaderText = GetString("Order_Edit_AddItems.GridProductCode");
        GridViewProducts.Columns[2].HeaderText = GetString("Order_Edit_AddItems.GridUnitPrice");
        GridViewProducts.Columns[3].HeaderText = GetString("Order_Edit_AddItems.GridQuantity");
        GridViewProducts.Columns[4].Visible = false;
        InitializeGridView();

        PageTitleAddItems.TitleText = GetString("Order_Edit_AddItems.Title");
        PageTitleAddItems.TitleImage = GetImageUrl("Objects/Ecommerce_OrderItem/new.png");

        // Initialize shopping cart item selector
        cartItemSelector.SKUID = this.SKUID;
        cartItemSelector.ShoppingCart = this.ShoppingCartObj;
        cartItemSelector.OnAddToShoppingCart += new CancelEventHandler(cartItemSelector_OnAddToShoppingCart);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (plcSelector.Visible)
        {
            // Get product name and price
            SKUInfo skuObj = SKUInfoProvider.GetSKUInfo(this.SKUID);
            string skuName = "";

            if (skuObj != null)
            {
                lblPriceValue.Text = SKUInfoProvider.GetSKUFormattedPrice(skuObj, this.ShoppingCartObj, true, false);                    
                skuName = ResHelper.LocalizeString(skuObj.SKUName);
            }

            // Set SKU name label
            this.lblSKUName.Text = skuName;

            // Show info text
            this.lblPrice.Text = GetString("Order_Edit_AddItems.UnitPrice");

            // Initializes page title control		
            string[,] tabs = new string[2, 3];
            tabs[0, 0] = GetString("Order_Edit_AddItems.Products");
            tabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_Edit_AddItems.aspx?cart=" + HTMLHelper.HTMLEncode(QueryHelper.GetString("cart", ""));
            tabs[0, 2] = "";
            tabs[1, 0] = skuName;
            tabs[1, 1] = "";
            tabs[1, 2] = "";
            PageTitleAddItems.Breadcrumbs = tabs;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// On GridViewProduct databound event.
    /// </summary>
    protected void GridViewProducts_DataBound(object sender, EventArgs e)
    {
        Label id;
        TextBox txtUnits;

        if (GridViewProducts.Rows.Count > 0)
        {
            btnAdd.Enabled = true;

            for (int i = 0; i < GridViewProducts.Rows.Count; i++)
            {
                // Copy ID from 5th column to invisible label in last column
                id = new Label();
                id.Text = GridViewProducts.Rows[i].Cells[4].Text;
                id.ID = id.Text;
                id.Attributes.Add("style", "display: none;");
                GridViewProducts.Rows[i].Cells[5].Controls.Add(id);

                txtUnits = (TextBox)GridViewProducts.Rows[i].Cells[3].Controls[1];
                txtUnits.ID = "txtTaxValue" + id.Text;
                txtUnits.TextChanged += new EventHandler(txtUnits_Changed);
                txtUnits.MaxLength = 9;
            }
        }
    }


    protected void txtUnits_Changed(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// On BtnShow click event.
    /// </summary>
    protected void BtnShow_Click(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// GridView initialization.
    /// </summary>
    protected void InitializeGridView()
    {
        string productNameFilter = txtProductName.Text.Trim().Replace("'", "''");
        string productCodeFilter = txtProductCode.Text.Trim().Replace("'", "''");

        // to display ONLY product - not product options
        string where = "(SKUEnabled = 1) AND (SKUOptionCategoryID IS NULL)";

        if (productNameFilter != "")
        {
            where += " AND (SKUName LIKE '%" + productNameFilter + "%')";
        }
        if (productCodeFilter != "")
        {
            where += " AND (SKUNumber LIKE '%" + productCodeFilter + "%')";
        }
        if (departmentElem.DepartmentID > 0)
        {
            where += " AND (SKUDepartmentID = " + departmentElem.DepartmentID + ")";
        }

        // Products for current site
        where += " AND (SKUSiteID = " + CMSContext.CurrentSiteID;

        // Global products when allowed
        if (allowGlobalProducts)
        {
            where += " OR SKUSiteID IS NULL";
        }

        where += ")";

        DataSet dsSKU = SKUInfoProvider.GetSKUs(where, "SKUName", 0, "SKUID, SKUName, SKUNumber, SKUPrice, SKUProductType, SKUDepartmentID, SKUSiteID");

        GridViewProducts.Columns[4].Visible = true;
        GridViewProducts.DataSource = dsSKU.Tables[0];
        GridViewProducts.DataBind();
        GridViewProducts.Columns[4].Visible = false;
        GridViewProducts.GridLines = GridLines.Horizontal;
    }


    protected void btnAddOneUnit_Click(object sender, EventArgs e)
    {
        // Get SKU ID
        int skuId = ValidationHelper.GetInteger(((LinkButton)sender).CommandArgument, 0);

        // Get product
        SKUInfo skui = SKUInfoProvider.GetSKUInfo(skuId);

        bool hasProductOptions = !DataHelper.DataSourceIsEmpty(OptionCategoryInfoProvider.GetSKUOptionCategories(skuId, true));
        bool isCustomizableDonation = ((skui != null) && (skui.SKUProductType == SKUProductTypeEnum.Donation))
            && (skui.SKUPrivateDonation || !((skui.SKUMinPrice == skui.SKUPrice) && (skui.SKUMaxPrice == skui.SKUPrice)));

        // If product has product options or product is a customizable donation
        // -> abort inserting products to the shopping cart
        if (hasProductOptions || isCustomizableDonation)
        {
            // Hide products
            this.plcProducts.Visible = false;

            // Set title message per specific case
            if (hasProductOptions && isCustomizableDonation)
            {
                this.lblTitle.ResourceString = "order_edit_additems.donationpropertiesproductoptions";
            }
            else if (hasProductOptions)
            {
                this.lblTitle.ResourceString = "order_edit_additems.productoptions";
            }
            else if (isCustomizableDonation)
            {
                this.lblTitle.ResourceString = "order_edit_additems.donationproperties";
            }

            // Save SKU ID to the viewstate
            this.SKUID = skuId;

            // Initialize shopping cart item selector
            this.cartItemSelector.SKUID = this.SKUID;
            this.cartItemSelector.ShowProductOptions = hasProductOptions;
            this.cartItemSelector.ShowDonationProperties = isCustomizableDonation;
            this.cartItemSelector.ReloadData();

            // Show shopping cart item selector
            this.plcSelector.Visible = true;
        }
        else
        {
            // Add product to shopping cart and close dialog window
            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "addproduct", ScriptHelper.GetScript("AddProducts(" + skuId + ", 1);"));
        }
    }


    void cartItemSelector_OnAddToShoppingCart(object sender, CancelEventArgs e)
    {
        // Get items parameters
        ShoppingCartItemParameters cartItemParams = this.cartItemSelector.GetShoppingCartItemParameters();

        if (this.ShoppingCartObj != null)
        {
            // Add item to shopping cart
            ShoppingCartItemInfo addedItem = this.ShoppingCartObj.SetShoppingCartItem(cartItemParams);
        }

        // Close dialog window and refresh content
        string script = "RefreshCart();";
        ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "addScript", ScriptHelper.GetScript(script));

        // Cancel further processing by shopping cart item selector control
        e.Cancel = true;
    }


    /// <summary>
    /// On BtnAdd click event.
    /// </summary>
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        // Check 'EcommerceModify' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }

        Label lblSkuId;
        TextBox txtUnits;
        int units = 0;
        int skuId = 0;
        string allUnits = null;
        string allSkuId = null;

        for (int i = 0; i < GridViewProducts.Rows.Count; i++)
        {
            lblSkuId = (Label)GridViewProducts.Rows[i].Cells[5].Controls[0];
            skuId = ValidationHelper.GetInteger(lblSkuId.Text, 0);
            if (skuId > 0)
            {
                txtUnits = (TextBox)GridViewProducts.Rows[i].Cells[3].Controls[1];
                txtUnits.ID = "txtTaxValue" + skuId.ToString();
                units = ValidationHelper.GetInteger(txtUnits.Text, 0);
                if (units > 0)
                {
                    // If product has some product options
                    // -> abort inserting products to the shopping cart
                    if (!DataHelper.DataSourceIsEmpty(OptionCategoryInfoProvider.GetSKUOptionCategories(skuId, true)))
                    {
                        string skuName = ((LinkButton)GridViewProducts.Rows[i].Cells[0].Controls[1]).Text;
                        lblError.Visible = true;
                        lblError.Text = string.Format(GetString("Order_Edit_AddItems.ProductOptionsRequired"), skuName);

                        return;
                    }

                    // Get product
                    SKUInfo skui = SKUInfoProvider.GetSKUInfo(skuId);

                    // If selected product is a donation
                    if ((skui != null) && (skui.SKUProductType == SKUProductTypeEnum.Donation))
                    {
                        // If donation is customizable
                        if (skui.SKUPrivateDonation || !((skui.SKUMinPrice == skui.SKUPrice) && (skui.SKUMaxPrice == skui.SKUPrice)))
                        {
                            string skuName = ((LinkButton)this.GridViewProducts.Rows[i].Cells[0].Controls[1]).Text;
                            this.lblError.Text = string.Format(this.GetString("order_edit_additems.donationpropertiesrequired"), skuName);
                            this.lblError.Visible = true;

                            return;
                        }
                    }

                    // Create strings with SKU IDs and units separated by ';'
                    allSkuId += skuId.ToString() + ";";
                    allUnits += units.ToString() + ";";
                }
            }
        }

        // Close this modal window and refresh parent values in window
        CloseWindow(allSkuId, allUnits);
    }


    /// <summary>
    /// Generates script that closes the window and refreshes the parent page.
    /// </summary>
    /// <param name="skuIds">String with SKU IDs separated by ';'</param>
    /// <param name="units">String with SKU units separated by ';'</param>
    private void CloseWindow(string skuIds, string units)
    {
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "addproductClose", ScriptHelper.GetScript("AddProducts('" + skuIds + "','" + units + "');"));
    }

    #endregion
}
