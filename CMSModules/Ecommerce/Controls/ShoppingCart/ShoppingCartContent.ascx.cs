using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartContent : ShoppingCartStep
{
    #region "Variables"

    protected Button btnReload = null;
    protected Button btnAddProduct = null;
    protected HiddenField hidProductID = null;
    protected HiddenField hidQuantity = null;
    protected HiddenField hidOptions = null;

    protected Nullable<bool> mEnableEditMode = null;
    protected bool checkInventory = false;

    #endregion


    /// <summary>
    /// Child control creation.
    /// </summary>
    protected override void CreateChildControls()
    {
        // Add product button
        this.btnAddProduct = new CMSButton();
        this.btnAddProduct.Attributes["style"] = "display: none";
        this.Controls.Add(this.btnAddProduct);
        this.btnAddProduct.Click += new EventHandler(btnAddProduct_Click);
        this.selectCurrency.UniSelector.OnSelectionChanged += new EventHandler(drpCurrency_SelectedIndexChanged);
        this.selectCurrency.DropDownSingleSelect.AutoPostBack = true;

        // Add the hidden fields for productId, quantity and product options
        this.hidProductID = new HiddenField();
        this.hidProductID.ID = "hidProductID";
        this.Controls.Add(this.hidProductID);

        this.hidQuantity = new HiddenField();
        this.hidQuantity.ID = "hidQuantity";
        this.Controls.Add(this.hidQuantity);

        this.hidOptions = new HiddenField();
        this.hidOptions.ID = "hidOptions";
        this.Controls.Add(this.hidOptions);
    }


    /// <summary>
    /// Page pre-render.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Register the dialog scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AddProductScript",
            ScriptHelper.GetScript(
                "function setProduct(val) { document.getElementById('" + this.hidProductID.ClientID + "').value = val; } \n" +
                "function setQuantity(val) { document.getElementById('" + this.hidQuantity.ClientID + "').value = val; } \n" +
                "function setOptions(val) { document.getElementById('" + this.hidOptions.ClientID + "').value = val; } \n" +
                "function setPrice(val) { document.getElementById('" + this.hdnPrice.ClientID + "').value = val; } \n" +
                "function setIsPrivate(val) { document.getElementById('" + this.hdnIsPrivate.ClientID + "').value = val; } \n" +
                "function AddProduct(productIDs, quantities, options, price, isPrivate) { \n" +
                    "setProduct(productIDs); \n" +
                    "setQuantity(quantities); \n" +
                    "setOptions(options); \n" +
                    "setPrice(price); \n" +
                    "setIsPrivate(isPrivate); \n" +
                    this.Page.ClientScript.GetPostBackEventReference(this.btnAddProduct, null) +
                ";} \n" +
                "function RefreshCart() {" +
                    this.Page.ClientScript.GetPostBackEventReference(this.btnAddProduct, null) +
                ";} \n"
            ));
        ScriptHelper.RegisterDialogScript(this.Page);

        // Hide columns with identifiers
        gridData.Columns[0].Visible = false;
        gridData.Columns[1].Visible = false;
        gridData.Columns[2].Visible = false;
        gridData.Columns[3].Visible = false;

        // Disable specific controls
        if (!this.Enabled)
        {
            this.lnkNewItem.Enabled = false;
            this.lnkNewItem.OnClientClick = "";
            this.selectCurrency.Enabled = false;
            this.btnEmpty.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.chkSendEmail.Enabled = false;
        }

        // Show/Hide dropdownlist with currencies
        pnlCurrency.Visible &= (selectCurrency.UniSelector.HasData && selectCurrency.DropDownSingleSelect.Items.Count > 1);

        // Check session parameters for inventory check
        if (ValidationHelper.GetBoolean(SessionHelper.GetValue("checkinventory"), false))
        {
            this.checkInventory = true;
            SessionHelper.Remove("checkinventory");
        }

        // Check inventory
        if (this.checkInventory)
        {
            string error = ShoppingCartInfoProvider.CheckShoppingCart(this.ShoppingCartInfoObj);
            if (!string.IsNullOrEmpty(error))
            {
                lblError.Text = error.Replace(";", "<br />");
            }
        }

        // Display/hide error message
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text.Trim());

        // Display/hide info message
        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text.Trim());
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblTitle.Text = GetString("ShoppingCart.CartContent");
        //bool currencySelected = false;

        if ((ShoppingCartInfoObj != null) && (ShoppingCartInfoObj.CountryID == 0) && (CMSContext.CurrentSite != null))
        {
            string countryName = ECommerceSettings.DefaultCountryName(CMSContext.CurrentSite.SiteName);
            CountryInfo ci = CountryInfoProvider.GetCountryInfo(countryName);
            ShoppingCartInfoObj.CountryID = (ci != null) ? ci.CountryID : 0;

            // Set currency selectors site ID
            selectCurrency.SiteID = this.ShoppingCartInfoObj.ShoppingCartSiteID;
        }

        // Initialization
        imgNewItem.ImageUrl = GetImageUrl("Objects/Ecommerce_OrderItem/add.png");
        lblCurrency.Text = GetString("Ecommerce.ShoppingCartContent.Currency");
        lblCoupon.Text = GetString("Ecommerce.ShoppingCartContent.Coupon");
        lnkNewItem.Text = GetString("Ecommerce.ShoppingCartContent.AddItem");
        imgNewItem.AlternateText = lnkNewItem.Text;
        btnUpdate.Text = GetString("Ecommerce.ShoppingCartContent.BtnUpdate");
        btnEmpty.Text = GetString("Ecommerce.ShoppingCartContent.BtnEmpty");
        btnEmpty.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Ecommerce.ShoppingCartContent.EmptyConfirm")) + ");";
        //this.TitleText = GetString("order_new.cartcontent.title");
        lnkNewItem.OnClientClick = "OpenAddProductDialog('" + GetCMSDeskShoppingCartSessionName() + "');return false;";
        gridData.Columns[4].HeaderText = GetString("general.remove");
        gridData.Columns[5].HeaderText = GetString("Ecommerce.ShoppingCartContent.SKUName");
        gridData.Columns[6].HeaderText = GetString("Ecommerce.ShoppingCartContent.SKUUnits");
        gridData.Columns[7].HeaderText = GetString("Ecommerce.ShoppingCartContent.UnitPrice");
        gridData.Columns[8].HeaderText = GetString("Ecommerce.ShoppingCartContent.UnitDiscount");
        gridData.Columns[9].HeaderText = GetString("Ecommerce.ShoppingCartContent.Tax");
        gridData.Columns[10].HeaderText = GetString("Ecommerce.ShoppingCartContent.Subtotal");
        this.gridData.RowDataBound += new GridViewRowEventHandler(gridData_RowDataBound);

        // Registre product price detail javascript
        StringBuilder script = new StringBuilder();
        script.Append("function ShowProductPriceDetail(cartItemGuid, sessionName){");
        script.Append("if (sessionName != \"\"){sessionName =  \"&cart=\" + sessionName;}");
        string detailUrl = "~/CMSModules/Ecommerce/Controls/ShoppingCart/ShoppingCartSKUPriceDetail.aspx";
        if (this.IsLiveSite)
        {
            detailUrl = "~/CMSModules/Ecommerce/CMSPages/ShoppingCartSKUPriceDetail.aspx";
        }
        script.Append("modalDialog('" + ResolveUrl(detailUrl) + "?itemguid=' + cartItemGuid + sessionName, 'ProductPriceDetail', 750, 500);}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ProductPriceDetail", ScriptHelper.GetScript(script.ToString()));

        script = new StringBuilder();
        script.Append("function OpenOrderItemDialog(cartItemGuid, sessionName){");
        script.Append("if (sessionName != \"\"){sessionName =  \"&cart=\" + sessionName;}");
        script.Append("modalDialog('" + ResolveUrl("~/CMSModules/Ecommerce/Controls/ShoppingCart/OrderItemEdit.aspx") + "?itemguid=' + cartItemGuid + sessionName, 'OrderItemEdit', 500, 350);}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "OrderItemEdit", ScriptHelper.GetScript(script.ToString()));

        // Hide "add product" link if it is live site order
        if (!this.ShoppingCartControl.IsInternalOrder)
        {
            pnlNewItem.Visible = false;

            this.ShoppingCartControl.ButtonBack.Text = GetString("Ecommerce.CartContent.ButtonBackText");
            this.ShoppingCartControl.ButtonBack.CssClass = "LongButton";
            this.ShoppingCartControl.ButtonNext.Text = GetString("Ecommerce.CartContent.ButtonNextText");

            if (!this.ShoppingCartControl.IsCurrentStepPostBack)
            {
                // Get shopping cart item parameters from URL
                ShoppingCartItemParameters itemParams = ShoppingCartItemParameters.GetShoppingCartItemParameters();

                // Set item in the shopping cart
                AddProducts(itemParams);
            }
        }

        // Set sending order notification when editing existing order according to the site settings
        if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
        {
            if (!this.ShoppingCartControl.IsCurrentStepPostBack)
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(this.ShoppingCartInfoObj.ShoppingCartSiteID);
                if (si != null)
                {
                    chkSendEmail.Checked = ECommerceSettings.SendOrderNotification(si.SiteName);
                }
            }
            chkSendEmail.Visible = true;
            chkSendEmail.Text = GetString("ShoppingCartContent.SendEmail");
        }

        if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
        {
            this.ShoppingCartControl.ButtonBack.Visible = false;
            this.ShoppingCartInfoObj.CheckAvailableItems = false;

            if ((!ExistAnotherStepsExceptPayment) && (this.ShoppingCartControl.PaymentGatewayProvider == null))
            {
                this.ShoppingCartControl.ButtonNext.Text = GetString("General.OK");
            }
            else
            {
                this.ShoppingCartControl.ButtonNext.Text = GetString("general.next");
            }
        }

        // Fill dropdownlist
        if (!this.ShoppingCartControl.IsCurrentStepPostBack)
        {
            if ((this.ShoppingCartInfoObj.CartItems.Count > 0) || this.ShoppingCartControl.IsInternalOrder)
            {
                if (this.ShoppingCartInfoObj != null)
                {
                    if (ShoppingCartInfoObj.ShoppingCartCurrencyID == 0)
                    {
                        // Select customer preferred currency                    
                        if (ShoppingCartInfoObj.CustomerInfoObj != null)
                        {
                            CustomerInfo customer = ShoppingCartInfoObj.CustomerInfoObj;
                            ShoppingCartInfoObj.ShoppingCartCurrencyID = (customer.CustomerUser != null) ? customer.CustomerUser.GetUserPreferredCurrencyID(CMSContext.CurrentSiteName) : 0;
                        }
                    }

                    if (ShoppingCartInfoObj.ShoppingCartCurrencyID == 0)
                    {
                        if (CMSContext.CurrentSite != null)
                        {
                            ShoppingCartInfoObj.ShoppingCartCurrencyID = CMSContext.CurrentSite.SiteDefaultCurrencyID;
                        }
                    }

                    selectCurrency.CurrencyID = ShoppingCartInfoObj.ShoppingCartCurrencyID;

                    if (ShoppingCartInfoObj.ShoppingCartDiscountCouponID > 0)
                    {
                        // fill textbox with discount coupon code
                        DiscountCouponInfo dci = DiscountCouponInfoProvider.GetDiscountCouponInfo(ShoppingCartInfoObj.ShoppingCartDiscountCouponID);
                        if (dci != null)
                        {
                            if (this.ShoppingCartInfoObj.IsCreatedFromOrder || dci.IsValid)
                            {
                                txtCoupon.Text = dci.DiscountCouponCode;
                            }
                            else
                            {
                                this.ShoppingCartInfoObj.ShoppingCartDiscountCouponID = 0;
                            }
                        }
                    }
                }
            }

            ReloadData();
        }

        // Turn on available items checking after content is loaded
        ShoppingCartInfoObj.CheckAvailableItems = true;
    }


    void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Set enabled for order item units editing
            e.Row.Cells[6].Enabled = this.Enabled;
        }
    }


    /// <summary>
    /// On btnUpdate click event.
    /// </summary>
    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        if (this.ShoppingCartInfoObj != null)
        {
            this.ShoppingCartInfoObj.ShoppingCartCurrencyID = ValidationHelper.GetInteger(this.selectCurrency.CurrencyID, 0);

            // Skip if method was called by btnAddProduct
            if (sender != this.btnAddProduct)
            {
                foreach (GridViewRow row in gridData.Rows)
                {
                    // Get shopping cart item Guid
                    Guid cartItemGuid = ValidationHelper.GetGuid(((Label)row.Cells[1].Controls[1]).Text, Guid.Empty);

                    // Try to find shopping cart item in the list
                    ShoppingCartItemInfo cartItem = this.ShoppingCartInfoObj.GetShoppingCartItem(cartItemGuid);
                    if (cartItem != null)
                    {
                        // If product and its product options should be removed
                        if (((CheckBox)row.Cells[4].Controls[1]).Checked && (sender != null))
                        {
                            // Remove product and its product option from list
                            this.ShoppingCartInfoObj.RemoveShoppingCartItem(cartItemGuid);

                            if (!this.ShoppingCartControl.IsInternalOrder)
                            {
                                if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
                                {
                                    // Log activity
                                    if (!cartItem.IsProductOption && !cartItem.IsBundleItem)
                                    {
                                        this.ShoppingCartControl.TrackActivityProductRemovedFromShoppingCart(cartItem.SKUID, ResHelper.LocalizeString(cartItem.SKUObj.SKUName), this.ContactID,
                                            CMSContext.CurrentSiteName, URLHelper.CurrentRelativePath, cartItem.CartItemUnits);
                                    }
                                }

                                // Delete product from database
                                ShoppingCartItemInfoProvider.DeleteShoppingCartItemInfo(cartItemGuid);
                            }
                        }
                        // If product units has changed
                        else if (!cartItem.IsProductOption)
                        {
                            // Get number of units
                            int itemUnits = ValidationHelper.GetInteger(((TextBox)(row.Cells[6].Controls[1])).Text.Trim(), 0);
                            if ((itemUnits > 0) && (cartItem.CartItemUnits != itemUnits))
                            {
                                // Update units of the parent product
                                cartItem.CartItemUnits = itemUnits;

                                // Update units of the child product options
                                foreach (ShoppingCartItemInfo option in cartItem.ProductOptions)
                                {
                                    option.CartItemUnits = itemUnits;
                                }

                                // Update units of child bundle items
                                foreach (ShoppingCartItemInfo bundleItem in cartItem.BundleItems)
                                {
                                    bundleItem.CartItemUnits = itemUnits;
                                }

                                if (!this.ShoppingCartControl.IsInternalOrder)
                                {
                                    ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(cartItem);

                                    // Update product options in database
                                    foreach (ShoppingCartItemInfo option in cartItem.ProductOptions)
                                    {
                                        ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(option);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            CheckDiscountCoupon();

            ReloadData();

            if ((ShoppingCartInfoObj.ShoppingCartDiscountCouponID > 0) && (!this.ShoppingCartInfoObj.IsDiscountCouponApplied))
            {
                // Discount coupon code is valid but not applied to any product of the shopping cart
                lblError.Text = GetString("ShoppingCart.DiscountCouponNotApplied");
            }

            // Inventory shloud be checked
            this.checkInventory = true;
        }
    }


    /// <summary>
    /// On btnEmpty click event.
    /// </summary>
    protected void btnEmpty_Click1(object sender, EventArgs e)
    {
        if (this.ShoppingCartInfoObj != null)
        {
            // Log activity "product removed" for all items in shopping cart
            string siteName = CMSContext.CurrentSiteName;
            if (!this.ShoppingCartControl.IsInternalOrder && (CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
                && ActivitySettingsHelper.RemovingProductFromSCEnabled(siteName))
            {
                this.ShoppingCartControl.TrackActivityAllProductsRemovedFromShoppingCart(this.ShoppingCartInfoObj, siteName, this.ContactID);
            }

            ShoppingCartInfoProvider.EmptyShoppingCart(this.ShoppingCartInfoObj);
            ReloadData();
        }
    }


    /// <summary>
    /// Validates this step.
    /// </summary>
    public override bool IsValid()
    {
        // Check modify permissions
        if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
        {
            // Check 'ModifyOrders' permission
            if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
            {
                CMSEcommercePage.RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
            }
        }

        // Allow to go to the next step only if shopping cart contains some products
        bool IsValid = (ShoppingCartInfoObj.CartItems.Count > 0);

        if (!IsValid)
        {
            HideCartContentWhenEmpty();
        }

        if (this.ShoppingCartInfoObj.IsCreatedFromOrder)
        {
            IsValid = true;
        }

        if (!IsValid)
        {
            lblError.Text = GetString("Ecommerce.Error.InsertSomeProducts");
        }

        return IsValid;
    }


    /// <summary>
    /// Process this step.
    /// </summary>
    public override bool ProcessStep()
    {
        // Shopping cart units are already saved in database (on "Update" or on "btnAddProduct_Click" actions) 
        bool isOK = false;

        if (ShoppingCartInfoObj != null)
        {
            // Reload data
            ReloadData();

            // Check available items before "Check out"
            string error = ShoppingCartInfoProvider.CheckShoppingCart(this.ShoppingCartInfoObj);
            if (!string.IsNullOrEmpty(error))
            {
                lblError.Text = error.Replace(";", "<br />");
            }
            else if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
            {
                // Indicates wheter order saving process is successful
                isOK = true;

                try
                {
                    ShoppingCartInfoProvider.SetOrder(this.ShoppingCartInfoObj);
                }
                catch
                {
                    isOK = false;
                }

                if (isOK)
                {
                    lblInfo.Text = GetString("General.ChangesSaved");

                    // Send order notification when editing existing order
                    if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
                    {
                        if (chkSendEmail.Checked)
                        {
                            OrderInfoProvider.SendOrderNotificationToAdministrator(this.ShoppingCartInfoObj);
                            OrderInfoProvider.SendOrderNotificationToCustomer(this.ShoppingCartInfoObj);
                        }
                    }
                    // Send order notification emails when on the live site
                    else if (ECommerceSettings.SendOrderNotification(CMSContext.CurrentSite.SiteName))
                    {
                        OrderInfoProvider.SendOrderNotificationToAdministrator(this.ShoppingCartInfoObj);
                        OrderInfoProvider.SendOrderNotificationToCustomer(this.ShoppingCartInfoObj);
                    }
                }
                else
                {
                    lblError.Text = GetString("Ecommerce.OrderPreview.ErrorOrderSave");
                }
            }
            // Go to the next step
            else
            {
                // Save other options
                if (!this.ShoppingCartControl.IsInternalOrder)
                {
                    ShoppingCartInfoProvider.SetShoppingCartInfo(ShoppingCartInfoObj);
                }
                isOK = true;
            }
        }

        return isOK;
    }


    /// <summary>
    /// Add product event handler.
    /// </summary>
    void btnAddProduct_Click(object sender, EventArgs e)
    {
        // Get strings with productIDs and quantities separated by ';'
        string productIDs = ValidationHelper.GetString(this.hidProductID.Value, "");
        string quantities = ValidationHelper.GetString(this.hidQuantity.Value, "");
        string options = ValidationHelper.GetString(this.hidOptions.Value, "");
        double price = ValidationHelper.GetDouble(this.hdnPrice.Value, -1);
        bool isPrivate = ValidationHelper.GetBoolean(this.hdnIsPrivate.Value, false);

        // Add new products to shopping cart
        if ((productIDs != "") && (quantities != ""))
        {
            string[] arrID = productIDs.TrimEnd(';').Split(';');
            string[] arrQuant = quantities.TrimEnd(';').Split(';');
            int[] intOptions = ValidationHelper.GetIntegers(options.Split(','), 0);

            lblError.Text = "";

            for (int i = 0; i < arrID.Length; i++)
            {
                int skuId = ValidationHelper.GetInteger(arrID[i], 0);

                SKUInfo skuInfo = SKUInfoProvider.GetSKUInfo(skuId);
                if (skuInfo != null)
                {
                    int quant = ValidationHelper.GetInteger(arrQuant[i], 0);

                    ShoppingCartItemParameters cartItemParams = new ShoppingCartItemParameters(skuId, quant, intOptions);

                    // If product is donation
                    if (skuInfo.SKUProductType == SKUProductTypeEnum.Donation)
                    {
                        // Get donation properties
                        if (price < 0)
                        {
                            cartItemParams.Price = SKUInfoProvider.GetSKUPrice(skuInfo, ShoppingCartInfoObj, false, false);
                        }
                        else
                        {
                            cartItemParams.Price = price;
                        }

                        cartItemParams.IsPrivate = isPrivate;
                    }

                    // Add product to the shopping cart
                    this.ShoppingCartInfoObj.SetShoppingCartItem(cartItemParams);

                    // Log activity
                    string siteName = CMSContext.CurrentSiteName;
                    if (!this.ShoppingCartControl.IsInternalOrder && (CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
                        && ActivitySettingsHelper.AddingProductToSCEnabled(siteName))
                    {
                        this.ShoppingCartControl.TrackActivityProductAddedToShoppingCart(skuId, ResHelper.LocalizeString(skuInfo.SKUName), this.ContactID, siteName, URLHelper.CurrentRelativePath, quant);
                    }

                    // Show empty button
                    btnEmpty.Visible = true;
                }
            }
        }

        // Invalidate values
        this.hidProductID.Value = "";
        this.hidOptions.Value = "";
        this.hidQuantity.Value = "";
        this.hdnPrice.Value = "";

        // Update values in table
        btnUpdate_Click1(this.btnAddProduct, e);

        // Hide cart content when empty
        if (DataHelper.DataSourceIsEmpty(ShoppingCartInfoObj.ContentTable))
        {
            HideCartContentWhenEmpty();
        }
        else
        {
            // Inventory shloud be checked
            this.checkInventory = true;
        }
    }


    /// <summary>
    /// Checks whether entered dsicount coupon code is usable for this cart. Returns true if so.
    /// </summary>
    private bool CheckDiscountCoupon()
    {
        bool success = true;

        if (txtCoupon.Text.Trim() != "")
        {
            // Get discount info
            DiscountCouponInfo dci = DiscountCouponInfoProvider.GetDiscountCouponInfo(txtCoupon.Text.Trim(), this.ShoppingCartInfoObj.SiteName);
            // Do not validate coupon when editing existing order and coupon code was not changed, otherwise process validation
            if ((dci != null) && (((this.ShoppingCartInfoObj.IsCreatedFromOrder) && (ShoppingCartInfoObj.ShoppingCartDiscountCouponID == dci.DiscountCouponID)) || dci.IsValid))
            {
                ShoppingCartInfoObj.ShoppingCartDiscountCouponID = dci.DiscountCouponID;
            }
            else
            {
                ShoppingCartInfoObj.ShoppingCartDiscountCouponID = 0;

                // Discount coupon is not valid                
                lblError.Text = GetString("ecommerce.error.couponcodeisnotvalid");

                success = false;
            }
        }
        else
        {
            ShoppingCartInfoObj.ShoppingCartDiscountCouponID = 0;
        }

        return success;
    }


    // Displays total price
    protected void DisplayTotalPrice()
    {
        pnlPrice.Visible = true;
        lblTotalPriceValue.Text = CurrencyInfoProvider.GetFormattedPrice(ShoppingCartInfoObj.RoundedTotalPrice, ShoppingCartInfoObj.CurrencyInfoObj);
        lblTotalPrice.Text = GetString("ecommerce.cartcontent.totalpricelabel");

        lblShippingPrice.Text = GetString("ecommerce.cartcontent.shippingpricelabel");
        lblShippingPriceValue.Text = CurrencyInfoProvider.GetFormattedPrice(ShoppingCartInfoObj.TotalShipping, ShoppingCartInfoObj.CurrencyInfoObj);
    }


    /// <summary>
    /// Sets product in the shopping cart.
    /// </summary>
    /// <param name="itemParams">Shoppping cart item parameters</param>
    protected void AddProducts(ShoppingCartItemParameters itemParams)
    {
        // Get main product info
        int productId = itemParams.SKUID;
        int quantity = itemParams.Quantity;

        if ((productId > 0) && (quantity > 0))
        {
            // Check product/options combination
            if (ShoppingCartInfoProvider.CheckNewShoppingCartItems(ShoppingCartInfoObj, itemParams))
            {
                // Get requested SKU info object from database
                SKUInfo skuObj = SKUInfoProvider.GetSKUInfo(productId);
                if (skuObj != null)
                {
                    // On the live site
                    if (!ShoppingCartControl.IsInternalOrder)
                    {
                        bool updateCart = false;

                        // Assign current shopping cart to current user
                        CurrentUserInfo ui = CMSContext.CurrentUser;
                        if (!ui.IsPublic())
                        {
                            this.ShoppingCartInfoObj.UserInfoObj = ui;
                            updateCart = true;
                        }

                        // Shopping cart is not saved yet
                        if (ShoppingCartInfoObj.ShoppingCartID == 0)
                        {
                            updateCart = true;
                        }

                        // Update shopping cart when required
                        if (updateCart)
                        {
                            ShoppingCartInfoProvider.SetShoppingCartInfo(ShoppingCartInfoObj);
                        }

                        // Set item in the shopping cart
                        ShoppingCartItemInfo product = this.ShoppingCartInfoObj.SetShoppingCartItem(itemParams);

                        // Update shopping cart item in database
                        ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(product);

                        // Update product options in database
                        foreach (ShoppingCartItemInfo option in product.ProductOptions)
                        {
                            ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(option);
                        }

                        // Update bundle items in database
                        foreach (ShoppingCartItemInfo bundleItem in product.BundleItems)
                        {
                            ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(bundleItem);
                        }

                        // Track add to shopping cart conversion
                        ECommerceHelper.TrackAddToShoppingCartConversion(product);
                    }
                    // In CMSDesk
                    else
                    {
                        // Set item in the shopping cart
                        this.ShoppingCartInfoObj.SetShoppingCartItem(itemParams);
                    }
                }
            }

            // Avoid adding the same product after page refresh
            if (lblError.Text == "")
            {
                string url = URLRewriter.CurrentURL;

                if (!string.IsNullOrEmpty(URLHelper.GetUrlParameter(url, "productid")) ||
                    !string.IsNullOrEmpty(URLHelper.GetUrlParameter(url, "quantity")) ||
                    !string.IsNullOrEmpty(URLHelper.GetUrlParameter(url, "options")))
                {
                    // Remove parameters from URL
                    url = URLHelper.RemoveParameterFromUrl(url, "productid");
                    url = URLHelper.RemoveParameterFromUrl(url, "quantity");
                    url = URLHelper.RemoveParameterFromUrl(url, "options");
                    URLHelper.Redirect(url);
                }
            }
        }
    }


    /// <summary>
    /// Hides cart content controls when no items are in shopping cart.
    /// </summary>
    protected void HideCartContentWhenEmpty()
    {
        pnlNewItem.Visible = this.ShoppingCartControl.IsInternalOrder;
        pnlPrice.Visible = false;
        btnEmpty.Visible = false;
        plcCoupon.Visible = false;

        if (!this.ShoppingCartControl.IsInternalOrder)
        {
            pnlCurrency.Visible = false;
            gridData.Visible = false;
            this.ShoppingCartControl.ButtonNext.Visible = false;
            lblInfo.Text = GetString("Ecommerce.ShoppingCartEmpty") + "<br />";
        }
    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    protected void ReloadData()
    {
        //gridData.DataSource = ShoppingCartInfoObj.ShoppingCartContentTable;
        gridData.DataSource = ShoppingCartInfoObj.ContentTable;

        // Hide coupon placeholder when no coupons are defined
        plcCoupon.Visible = AreDiscountCouponsAvailableOnSite();

        // Bind data
        gridData.DataBind();

        if (!DataHelper.DataSourceIsEmpty(ShoppingCartInfoObj.ContentTable))
        {
            // Display total price
            DisplayTotalPrice();

            // Display/hide discount column
            gridData.Columns[8].Visible = this.ShoppingCartInfoObj.IsDiscountApplied;
        }
        else
        {
            // Hide some items
            HideCartContentWhenEmpty();
        }

        if (!ShippingOptionInfoProvider.IsShippingNeeded(this.ShoppingCartInfoObj))
        {
            this.plcShippingPrice.Visible = false;
        }
    }


    /// <summary>
    /// Determines if the discount coupons are available for the current site.
    /// </summary>
    private bool AreDiscountCouponsAvailableOnSite()
    {
        string siteName = this.ShoppingCartInfoObj.SiteName;

        // Check site and global discount coupons
        string where = "DiscountCouponSiteID = " + SiteInfoProvider.GetSiteID(siteName);
        if (ECommerceSettings.AllowGlobalDiscountCoupons(siteName))
        {
            where += " OR DiscountCouponSiteID IS NULL";
        }

        // Coupons are available if found any
        DataSet ds = DiscountCouponInfoProvider.GetDiscountCoupons(where, null, -1, "count(DiscountCouponID)");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return (ValidationHelper.GetInteger(ds.Tables[0].Rows[0][0], 0) > 0);
        }

        return false;
    }


    /// <summary>
    /// Returns price detail link.
    /// </summary>
    protected string GetPriceDetailLink(object value)
    {
        if ((this.ShoppingCartControl.EnableProductPriceDetail) && (this.ShoppingCartInfoObj.ContentTable != null))
        {
            Guid cartItemGuid = ValidationHelper.GetGuid(value, Guid.Empty);
            if (cartItemGuid != Guid.Empty)
            {
                // Add product price detail
                return "<img src=\"" + GetImageUrl("Design/Controls/UniGrid/Actions/detail.png") + "\" onclick=\"javascript: ShowProductPriceDetail('" + cartItemGuid.ToString() + "','" + GetCMSDeskShoppingCartSessionName() + "')\" alt=\"" + GetString("ShoppingCart.ProductPriceDetail") + "\" class=\"ProductPriceDetailImage\" style=\"cursor:pointer;\" />";
            }
        }

        return "";
    }


    /// <summary>
    /// Returns shopping cart session name.
    /// </summary>
    private string GetCMSDeskShoppingCartSessionName()
    {
        switch (this.ShoppingCartControl.CheckoutProcessType)
        {
            case CheckoutProcessEnum.CMSDeskOrder:
                return "CMSDeskNewOrderShoppingCart";

            case CheckoutProcessEnum.CMSDeskCustomer:
                return "CMSDeskNewCustomerOrderShoppingCart";

            case CheckoutProcessEnum.CMSDeskOrderItems:
                return "CMSDeskOrderItemsShoppingCart";

            case CheckoutProcessEnum.LiveSite:
            case CheckoutProcessEnum.Custom:
            default:
                return "";
        }
    }


    protected void drpCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate_Click1(null, null);
    }


    public override void ButtonBackClickAction()
    {
        if ((!this.ShoppingCartControl.IsInternalOrder) && (this.ShoppingCartControl.CurrentStepIndex == 0))
        {
            // Continue shopping
            URLHelper.Redirect(this.ShoppingCartControl.PreviousPageUrl);
        }
        else
        {
            // Standard action
            base.ButtonBackClickAction();
        }
    }


    /// <summary>
    /// Indicates whether there are another checkout process steps after the current step, except payment.
    /// </summary>
    private bool ExistAnotherStepsExceptPayment
    {
        get
        {
            return (this.ShoppingCartControl.CurrentStepIndex + 2 <= this.ShoppingCartControl.CheckoutProcessSteps.Count - 1);
        }
    }


    /// <summary>
    /// Returns formated value string.
    /// </summary>
    /// <param name="value">Value to format</param>
    protected string GetFormattedValue(object value)
    {
        double price = ValidationHelper.GetDouble(value, 0);
        return CurrencyInfoProvider.GetFormattedValue(price, this.ShoppingCartInfoObj.CurrencyInfoObj);
    }


    /// <summary>
    /// Returns formatted and localized SKU name.
    /// </summary>
    /// <param name="skuId">SKU ID</param>
    /// <param name="skuSiteId">SKU site ID</param>
    /// <param name="value">SKU name</param>
    /// <param name="isProductOption">Indicates if cart item is product option</param>
    /// <param name="isBundleItem">Indicates if cart item is bundle item</param>
    protected string GetSKUName(object skuId, object skuSiteId, object value, object isProductOption, object isBundleItem, object cartItemIsPrivate, object itemText, object productType)
    {
        string name = ResHelper.LocalizeString((string)value);
        bool isPrivate = ValidationHelper.GetBoolean(cartItemIsPrivate, false);
        string text = itemText as string;
        StringBuilder skuName = new StringBuilder();
        SKUProductTypeEnum type = SKUInfoProvider.GetSKUProductTypeEnum(productType as string);

        // If it is a product option or bundle item
        if (ValidationHelper.GetBoolean(isProductOption, false) || ValidationHelper.GetBoolean(isBundleItem, false))
        {
            skuName.Append("<span style=\"font-size:90%\"> - ");
            skuName.Append(HTMLHelper.HTMLEncode(name));

            if (!string.IsNullOrEmpty(text))
            {
                skuName.Append(" '" + HTMLHelper.HTMLEncode(text) + "'");
            }

            skuName.Append("</span>");
        }
        // If it is a parent product
        else
        {
            // Add private donation suffix
            if ((type == SKUProductTypeEnum.Donation) && (isPrivate))
            {
                name += String.Format(" ({0})", this.GetString("com.shoppingcartcontent.privatedonation"));
            }

            // In CMS Desk
            if (this.ShoppingCartControl.IsInternalOrder)
            {
                // Display link to SKU edit in dialog
                string url = this.ResolveUrl("~/CMSModules/Ecommerce/Pages/Tools/Products/Product_Edit_Frameset.aspx");
                url = URLHelper.AddParameterToUrl(url, "productid", skuId.ToString());
                url = URLHelper.AddParameterToUrl(url, "siteid", skuSiteId.ToString());
                url = URLHelper.AddParameterToUrl(url, "dialogmode", "1");
                skuName.Append(String.Format("<a href=\"#\" onclick=\"modalDialog('{0}', 'SKUEdit', 950, 700); return false;\">{1}</a>", url, HTMLHelper.HTMLEncode(name)));
            }
            // On live site
            else
            {
                if (type == SKUProductTypeEnum.Donation)
                {
                    // Display donation name without link
                    skuName.Append(HTMLHelper.HTMLEncode(name));
                }
                else
                {
                    // Display link to product page
                    skuName.Append(String.Format("<a href=\"{0}?productid={1}\" class=\"CartProductDetailLink\">{2}</a>", ResolveUrl("~/CMSPages/GetProduct.aspx"), skuId.ToString(), HTMLHelper.HTMLEncode(name)));
                }
            }
        }

        return skuName.ToString();
    }


    protected static bool IsProductOption(object isProductOption)
    {
        return ValidationHelper.GetBoolean(isProductOption, false);
    }


    protected static bool IsBundleItem(object isBundleItem)
    {
        return ValidationHelper.GetBoolean(isBundleItem, false);
    }


    protected string GetEditOrderItemLink(object value)
    {
        // When editing existing order -> show edit button
        if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
        {
            Guid cartItemGuid = ValidationHelper.GetGuid(value, Guid.Empty);
            if (cartItemGuid != Guid.Empty)
            {
                // Return product edit link
                return "<img src=\"" + GetImageUrl("Design/Controls/UniGrid/Actions/edit.png") + "\" onclick=\"javascript: OpenOrderItemDialog('" + cartItemGuid.ToString() + "','" + GetCMSDeskShoppingCartSessionName() + "')\" alt=\"" + GetString("ShoppingCart.EditOrderItem") + "\" class=\"OrderItemEditLink\" style=\"cursor:pointer;\" />";
            }
        }

        return "";
    }


    /// <summary>
    /// Returns formatted child cart item units. Returns empty string if it is not product option or bundle item.
    /// </summary>
    /// <param name="skuUnits">SKU units</param>
    /// <param name="isProductOption">Indicates if cart item is product option</param>
    /// <param name="isBundleItem">Indicates if cart item is bundle item</param>
    protected static string GetChildCartItemUnits(object skuUnits, object isProductOption, object isBundleItem)
    {
        if (ValidationHelper.GetBoolean(isProductOption, false) || ValidationHelper.GetBoolean(isBundleItem, false))
        {
            return "<span style=\"padding-left:2px;\">" + Convert.ToString(skuUnits) + "</span>";
        }

        return "";
    }
}
