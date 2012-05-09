using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.ExtendedControls;
using CMS.PortalEngine;

public partial class CMSModules_Ecommerce_Controls_ProductOptions_ShoppingCartItemSelector : CMSUserControl
{
    #region "Variables"

    private bool mSKUEnabled = true;
    private bool mDataLoaded = false;

    private string mAddToCartLinkText = "";
    private string mAddToCartText = "shoppingcart.addtoshoppingcart";
    private string mAddToCartTooltip = "";
    private string mAddToWishlistImageButton = "";
    private string mAddToWishlistLinkText = "";

    private string mImageFolder = "";
    private string mAddToCartImageButton = "";
    private string mShoppingCartUrl = "";
    private string mWishlistUrl = "";

    private bool mShowUnitsTextBox = false;
    private bool mShowProductOptions = false;
    private bool mShowDonationProperties = false;
    private bool mShowWishlistLink = false;
    private bool mShowPriceIncludingTax = false;
    private bool mShowTotalPrice = false;
    private bool mDialogMode = false;
    private bool? mRedirectToShoppingCart = null;
    private bool mRedirectToDetailsEnabled = true;

    private int mDefaultQuantity = 1;

    private ShoppingCartInfo mShoppingCart = null;
    private SKUInfo mSKU = null;

    private Hashtable mProductOptions = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Product ID (SKU ID).
    /// </summary>
    public int SKUID
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


    /// <summary>
    /// Product SKU data
    /// </summary>
    public SKUInfo SKU
    {
        get
        {
            if (mSKU == null)
            {
                this.mSKU = SKUInfoProvider.GetSKUInfo(this.SKUID);
            }

            return mSKU;
        }
    }


    /// <summary>
    /// Indicates whether current product (SKU) is enabled, TRUE - button/link for adding product to the shopping cart is rendered, otherwise it is not rendered.
    /// </summary>
    public bool SKUEnabled
    {
        get
        {
            return mSKUEnabled;
        }
        set
        {
            mSKUEnabled = value;
            InitializeControls();
        }
    }


    /// <summary>
    /// File name of the image which is used as a source for image button to add product to the shopping cart, default image folder is '~/App_Themes/(Skin_Folder)/Images/ShoppingCart/'.
    /// </summary>
    public string AddToCartImageButton
    {
        get
        {
            return mAddToCartImageButton;
        }
        set
        {
            mAddToCartImageButton = value;
        }
    }


    /// <summary>
    /// Simple string or localizable string of the link to add product to the shopping cart.
    /// </summary>
    public string AddToCartLinkText
    {
        get
        {
            return mAddToCartLinkText;
        }
        set
        {
            mAddToCartLinkText = value;
        }
    }


    /// <summary>
    /// Simple string or localizable string of add to shopping cart link or button.
    /// </summary>
    public string AddToCartText
    {
        get
        {
            return mAddToCartText;
        }
        set
        {
            this.mAddToCartText = value;
        }
    }


    /// <summary>
    /// Simple string or localizable string of add to shopping cart link or button tooltip.
    /// </summary>
    public string AddToCartTooltip
    {
        get
        {
            return this.mAddToCartTooltip;
        }
        set
        {
            this.mAddToCartTooltip = value;
        }
    }


    /// <summary>
    /// Indicates if textbox for entering number of units to add to the shopping cart should be displayed, if it is hidden number of units is equal to 1.
    /// </summary>
    public bool ShowUnitsTextBox
    {
        get
        {
            return mShowUnitsTextBox;
        }
        set
        {
            mShowUnitsTextBox = value;
        }
    }


    /// <summary>
    /// Indicates if product options of the current product should be displayed.
    /// </summary>
    public bool ShowProductOptions
    {
        get
        {
            return mShowProductOptions;
        }
        set
        {
            mShowProductOptions = value;
        }
    }


    /// <summary>
    /// Indicates if donation properties control is displayed.
    /// </summary>
    public bool ShowDonationProperties
    {
        get
        {
            return this.mShowDonationProperties;
        }
        set
        {
            this.mShowDonationProperties = value;
        }
    }


    /// <summary>
    /// Indicates if "Add to Wishlist" link should be displayed.
    /// </summary>
    public bool ShowWishlistLink
    {
        get
        {
            return mShowWishlistLink;
        }
        set
        {
            mShowWishlistLink = value;
        }
    }


    /// <summary>
    /// Default quantity when adding product to the shopping cart.
    /// </summary>
    public int DefaultQuantity
    {
        get
        {
            return mDefaultQuantity;
        }
        set
        {
            mDefaultQuantity = value;
        }
    }


    /// <summary>
    /// Show total price.
    /// </summary>
    public bool ShowTotalPrice
    {
        get
        {
            return mShowTotalPrice;
        }
        set
        {
            mShowTotalPrice = value;
        }
    }


    /// <summary>
    /// Quantity of the specified product to add to the shopping cart.
    /// </summary>
    public int Quantity
    {
        get
        {
            if (this.ShowUnitsTextBox)
            {
                return ValidationHelper.GetInteger(txtUnits.Text.Trim(), this.DefaultQuantity);
            }
            else
            {
                return this.DefaultQuantity;
            }
        }
    }


    /// <summary>
    /// Price of the product set by user.
    /// </summary>
    public double Price
    {
        get
        {
            return this.donationProperties.DonationAmount;
        }
    }


    /// <summary>
    /// Indicates if product is added to shopping cart as private.
    /// </summary>
    public bool IsPrivate
    {
        get
        {
            return this.donationProperties.DonationIsPrivate;
        }
    }


    /// <summary>
    /// Product options selected by inner selectors (string of SKUIDs separated byt the comma).
    /// </summary>
    public string ProductOptions
    {
        get
        {
            string options = "";

            // Get product options from selectors
            foreach (Control selector in pnlSelectors.Controls)
            {
                if (selector is ProductOptionSelector)
                {
                    options += ((ProductOptionSelector)selector).GetSelectedSKUOptions() + ",";
                }
            }

            return options.TrimEnd(',');
        }
    }


    /// <summary>
    /// List containing product options selected by inner selectors.
    /// </summary>
    public List<ShoppingCartItemParameters> ProductOptionsParameters
    {
        get
        {
            List<ShoppingCartItemParameters> options = new List<ShoppingCartItemParameters>();

            // Get product options from selectors
            foreach (Control selector in pnlSelectors.Controls)
            {
                if (selector is ProductOptionSelector)
                {
                    options.AddRange(((ProductOptionSelector)selector).GetSelectedOptionsParameters());
                }
            }

            return options;
        }
    }


    /// <summary>
    /// Image folder, default image folder is '~/App_Themes/(Skin_Folder)/Images/ShoppingCart/'.
    /// </summary>
    public string ImageFolder
    {
        get
        {
            return mImageFolder;
        }
        set
        {
            mImageFolder = value;
        }
    }


    /// <summary>
    /// Shopping cart url. By default Shopping cart url from SiteManager settings is returned.
    /// </summary>
    public string ShoppingCartUrl
    {
        get
        {
            if (mShoppingCartUrl == "")
            {
                mShoppingCartUrl = ECommerceSettings.ShoppingCartURL(CMSContext.CurrentSiteName);
            }
            return mShoppingCartUrl;
        }
        set
        {
            mShoppingCartUrl = value;
        }
    }


    /// <summary>
    /// Inidicates if user has to be redirected to shopping cart after adding an item to cart. Default value is taken from
    /// Ecommerce settings (keyname "CMSStoreRedirectToShoppingCart").
    /// </summary>
    public bool RedirectToShoppingCart
    {
        get
        {
            if (!mRedirectToShoppingCart.HasValue)
            {
                mRedirectToShoppingCart = ECommerceSettings.RedirectToShoppingCart(CMSContext.CurrentSiteName);
            }

            return mRedirectToShoppingCart.Value;
        }
        set
        {
            mRedirectToShoppingCart = value;
        }
    }


    /// <summary>
    /// Wishlist url. By default WIshlist url from SiteManager settings is returned.
    /// </summary>
    public string WishlistUrl
    {
        get
        {
            if (mWishlistUrl == "")
            {
                mWishlistUrl = ECommerceSettings.WishListURL(CMSContext.CurrentSiteName);
            }
            return mWishlistUrl;
        }
        set
        {
            mWishlistUrl = value;
        }
    }


    /// <summary>
    /// File name of the image which is used as a source for image button to add product to the wishlist, default image folder is '~/App_Themes/(Skin_Folder)/Images/ShoppingCart/'.
    public string AddToWishlistImageButton
    {
        get
        {
            return mAddToWishlistImageButton;
        }
        set
        {
            mAddToWishlistImageButton = value;
        }
    }


    /// <summary>
    /// Simple string or localizable string of the link to add product to the wishlist.
    /// </summary>
    public string AddToWishlistLinkText
    {
        get
        {
            return mAddToWishlistLinkText;
        }
        set
        {
            mAddToWishlistLinkText = value;
        }
    }


    /// <summary>
    /// Shopping cart object required for formatting of the displayed prices.
    /// If it is not set, current shopping cart from E-commerce context is used.
    /// </summary>
    public ShoppingCartInfo ShoppingCart
    {
        get
        {
            if (mShoppingCart == null)
            {
                mShoppingCart = ECommerceContext.CurrentShoppingCart;
            }

            return mShoppingCart;
        }
        set
        {
            mShoppingCart = value;
        }
    }


    /// <summary>
    /// TRUE - product option price is displayed including tax, FALSE - product option price is displayed without tax.
    /// </summary>
    public bool ShowPriceIncludingTax
    {
        get
        {
            return mShowPriceIncludingTax;
        }
        set
        {
            mShowPriceIncludingTax = value;
        }
    }


    /// <summary>
    /// Indicates if shopping car item selector is modal dialog.
    /// </summary>
    public bool DialogMode
    {
        get
        {
            return this.mDialogMode;
        }
        set
        {
            this.mDialogMode = value;
        }
    }


    /// <summary>
    /// Control that is used to add the product to shopping cart.
    /// </summary>
    public Control AddToCartControl
    {
        get;
        private set;
    }


    /// <summary>
    /// Indicates if redirect to product details is enabled if other conditions are met.
    /// Set to true by default.
    /// </summary>
    public bool RedirectToDetailsEnabled
    {
        get
        {
            return this.mRedirectToDetailsEnabled;
        }
        set
        {
            this.mRedirectToDetailsEnabled = value;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Fires when "Add to shopping cart" button is clicked, overrides original action.
    /// </summary>
    public event CancelEventHandler OnAddToShoppingCart;

    #endregion


    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        lnkAdd.Click += new EventHandler(lnkAdd_Click);
        btnAdd.Click += new EventHandler(btnAdd_Click);
        lnkWishlist.Click += new EventHandler(lnkWishlist_Click);
        btnWishlist.Click += new ImageClickEventHandler(btnWishlist_Click);

        this.ltlScript.Text = ScriptHelper.GetScript("function ShoppingCartItemAddedHandler(message) { alert(message); }");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.SKUID > 0)
        {
            InitializeControls();

            if (!mDataLoaded)
            {
                ReloadData();
            }

            // Get count of the product options            
            if ((this.ShowTotalPrice) && SKUInfoProvider.HasSKUEnabledOptions(this.SKUID))
            {
                if (!RequestHelper.IsPostBack())
                {
                    // Count and show total price with options
                    CalculateTotalPrice();
                }

                // Show total price container
                if (this.ShowPriceIncludingTax)
                {
                    lblPrice.Text = GetString("ShoppingCartItemSelector.TotalPriceIncludeTax");
                }
                else
                {
                    lblPrice.Text = GetString("ShoppingCartItemSelector.TotalPriceWithoutTax");
                }
                pnlPrice.Visible = true;
            }
            else
            {
                // Hide total price container
                pnlPrice.Visible = false;
            }

            if (DialogMode)
            {
                pnlButton.CssClass += " PageFooterLine";
            }
        }

        lblUnits.Style.Add("display", "none");

        // Show panel only when some selectors loaded
        this.pnlSelectors.Visible = (this.pnlSelectors.Controls.Count > 1) || this.donationProperties.Visible;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads shopping cart item selector data.
    /// </summary>
    public void ReloadData()
    {
        if (this.SKUID > 0)
        {
            DebugHelper.SetContext("ShoppingCartItemSelector");

            InitializeControls();

            if (this.ShowProductOptions)
            {
                LoadProductOptions();
            }

            // If donation properties should be shown and SKU product type is donation
            if (this.ShowDonationProperties && (this.SKU != null) && (this.SKU.SKUProductType == SKUProductTypeEnum.Donation))
            {
                this.donationProperties.Visible = true;
                this.donationProperties.StopProcessing = false;
                this.donationProperties.SKU = this.SKU;
                this.donationProperties.ShoppingCart = this.ShoppingCart;
                this.donationProperties.ReloadData();
            }

            // Get count of the product options            
            if (this.ShowTotalPrice && SKUInfoProvider.HasSKUEnabledOptions(this.SKUID))
            {
                // Count and show total price with options
                CalculateTotalPrice();
            }

            // Fill units textbox with default quantity
            if (this.ShowUnitsTextBox)
            {
                if (txtUnits.Text.Trim() == "")
                {
                    txtUnits.Text = this.DefaultQuantity.ToString();
                }
            }

            mDataLoaded = true;
            DebugHelper.ReleaseContext();
        }
    }


    /// <summary>
    /// Initializes controls.
    /// </summary>
    private void InitializeControls()
    {
        if (this.lnkAdd == null)
        {
            this.upnlAjax.LoadContainer();
        }

        if (this.SKUEnabled)
        {
            // Display/hide units textbox with default quantity
            if (this.ShowUnitsTextBox)
            {
                txtUnits.Visible = true;
                lblUnits.Visible = true;
                lblUnits.AssociatedControlID = "txtUnits";
            }

            // Dispaly link button
            if (!String.IsNullOrEmpty(this.AddToCartLinkText))
            {
                this.lnkAdd.Visible = true;
                this.lnkAdd.Text = this.GetString(this.AddToCartLinkText);
                this.lnkAdd.ToolTip = this.GetString(this.AddToCartTooltip);

                this.AddToCartControl = this.lnkAdd;
            }
            // Display image button
            else if (!String.IsNullOrEmpty(this.AddToCartImageButton))
            {
                if (this.AddToCartImageButton.StartsWith("~"))
                {
                    this.btnAddImage.ImageUrl = URLHelper.ResolveUrl(this.AddToCartImageButton);
                }
                else
                {
                    // Set default image folder
                    if (this.ImageFolder == "")
                    {
                        this.ImageFolder = GetImageUrl("ShoppingCart/");
                    }

                    this.btnAddImage.ImageUrl = this.ImageFolder.TrimEnd('/') + "/" + this.AddToCartImageButton;
                }

                this.btnAddImage.Visible = true;
                this.btnAddImage.AlternateText = this.GetString(this.AddToCartTooltip);
                this.btnAddImage.ToolTip = this.GetString(this.AddToCartTooltip);

                this.AddToCartControl = btnAddImage;
            }
            // Display classic button
            else
            {
                this.btnAdd.Visible = true;
                this.btnAdd.Text = this.GetString(this.AddToCartText);
                this.btnAdd.ToolTip = this.GetString(this.AddToCartTooltip);

                this.AddToCartControl = this.btnAdd;
            }
        }

        // Display "Add to Wishlist" link        
        if (this.AddToWishlistLinkText != "")
        {
            lnkWishlist.Visible = true;
            lnkWishlist.Text = ResHelper.LocalizeString(this.AddToWishlistLinkText);
            lnkWishlist.ToolTip = ResHelper.LocalizeString(this.AddToWishlistLinkText);
        }
        // Display "Add to Wishlist" image button 
        else if (this.AddToWishlistImageButton != "")
        {
            if (this.ImageFolder == "")
            {
                // Set default image folder
                this.ImageFolder = GetImageUrl("ShoppingCart/");
            }

            // Image button
            btnWishlist.Visible = true;
            btnWishlist.ImageUrl = this.ImageFolder.TrimEnd('/') + "/" + this.AddToWishlistImageButton;
            btnWishlist.AlternateText = GetString("ShoppingCart.AddToWishlistToolTip");
            btnWishlist.ToolTip = GetString("ShoppingCart.AddToWishlistToolTip");
        }
    }


    /// <summary>
    /// Sets donation properties.
    /// </summary>
    /// <param name="amount">Donation amount in shopping cart currency.</param>
    /// <param name="isPrivate">Donation is private</param>
    /// <param name="units">Donation units</param>
    public void SetDonationProperties(double amount, bool isPrivate, int units)
    {
        this.donationProperties.DonationAmount = (amount > 0.0) ? amount : this.donationProperties.DonationAmount;
        this.donationProperties.DonationIsPrivate = isPrivate;
        this.DefaultQuantity = units;
    }


    /// <summary>
    /// Returns selected shopping cart item parameters containig product option parameters.
    /// </summary>
    public ShoppingCartItemParameters GetShoppingCartItemParameters()
    {
        // Get product options
        List<ShoppingCartItemParameters> options = this.ProductOptionsParameters;

        // Create params
        ShoppingCartItemParameters cartItemParams = new ShoppingCartItemParameters(this.SKUID, this.Quantity, options);

        if (this.donationProperties.Visible || !this.RedirectToDetailsEnabled)
        {
            // Get exchange rate from cart currency to site main currency         
            double rate = (this.SKU.IsGlobal) ? this.ShoppingCart.ExchangeRateForGlobalItems : this.ShoppingCart.ExchangeRate;

            // Set donation specific shopping cart item parameters
            cartItemParams.IsPrivate = this.donationProperties.DonationIsPrivate;

            // Get donation amount in site main currency
            cartItemParams.Price = ExchangeRateInfoProvider.ApplyExchangeRate(this.donationProperties.DonationAmount, 1 / rate);
        }

        return cartItemParams;
    }


    void lnkWishlist_Click(object sender, EventArgs e)
    {
        AddProductToWishlist();
    }


    void btnWishlist_Click(object sender, ImageClickEventArgs e)
    {
        AddProductToWishlist();
    }


    void lnkAdd_Click(object sender, EventArgs e)
    {
        AddProductToShoppingCart();
    }


    protected void btnAddImage_Click(object sender, ImageClickEventArgs e)
    {
        AddProductToShoppingCart();
    }


    void btnAdd_Click(object sender, EventArgs e)
    {
        AddProductToShoppingCart();
    }


    private void AddProductToWishlist()
    {
        SessionHelper.SetValue("ShoppingCartUrlReferrer", URLHelper.CurrentURL);
        URLHelper.Redirect(this.WishlistUrl + "?productid=" + this.SKUID);
    }


    /// <summary>
    /// Validates shopping cart item selector input data.
    /// </summary>    
    private bool IsValid()
    {
        // Validates all product options
        if (this.ShowProductOptions)
        {
            foreach (Control selector in pnlSelectors.Controls)
            {
                if (selector is ProductOptionSelector)
                {
                    // End if invalid option found
                    if (!((ProductOptionSelector)selector).IsValid())
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }


    /// <summary>
    /// Adds product to the shopping cart.
    /// </summary>
    private void AddProductToShoppingCart()
    {
        // Validate input data
        if (!IsValid() || (this.SKU == null))
        {
            // Do not proces
            return;
        }

        if (this.RedirectToDetailsEnabled)
        {
            if (!this.ShowProductOptions && !this.ShowDonationProperties)
            {
                // Does product have some enabled product option categories?
                bool hasOptions = !DataHelper.DataSourceIsEmpty(OptionCategoryInfoProvider.GetSKUOptionCategories(this.SKUID, true));

                // Is product a customizable donation?
                bool isCustomizableDonation = ((this.SKU != null)
                    && (this.SKU.SKUProductType == SKUProductTypeEnum.Donation)
                    && (!((this.SKU.SKUPrice == this.SKU.SKUMinPrice) && (this.SKU.SKUPrice == this.SKU.SKUMaxPrice)) || this.SKU.SKUPrivateDonation));

                if (hasOptions || isCustomizableDonation)
                {
                    // Redirect to product details
                    URLHelper.Redirect("~/CMSPages/GetProduct.aspx?productid=" + this.SKUID);
                }
            }
        }

        // Get cart item parameters
        ShoppingCartItemParameters cartItemParams = this.GetShoppingCartItemParameters();

        // Check if it is possible to add this item to shopping cart
        if (!ShoppingCartInfoProvider.CheckNewShoppingCartItems(this.ShoppingCart, cartItemParams))
        {
            // Show error message and cancel adding the product to shopping cart
            string error = String.Format(this.GetString("ecommerce.cartcontent.productdisabled"), this.SKU.SKUName);
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ShoppingCartAddItemErrorAlert", ScriptHelper.GetAlertScript(error));
            return;
        }

        // If donation properties are used and donation properties form is not valid
        if (this.donationProperties.Visible && !String.IsNullOrEmpty(this.donationProperties.Validate()))
        {
            return;
        }

        // Fire on add to shopping cart event
        CancelEventArgs eventArgs = new CancelEventArgs();
        if (this.OnAddToShoppingCart != null)
        {
            this.OnAddToShoppingCart(this, eventArgs);
        }

        // If adding to shopping cart was cancelled
        if (eventArgs.Cancel)
        {
            return;
        }

        // Get cart item parameters in case something changed
        cartItemParams = this.GetShoppingCartItemParameters();

        // Log activity
        LogProductAddedToSCActivity(this.SKUID, this.SKU.SKUName, this.Quantity);

        if (this.ShoppingCart != null)
        {
            bool updateCart = false;

            // Assign current shopping cart to current user
            CurrentUserInfo ui = CMSContext.CurrentUser;
            if (!ui.IsPublic())
            {
                this.ShoppingCart.UserInfoObj = ui;
                updateCart = true;
            }

            // Shopping cart is not saved yet
            if (this.ShoppingCart.ShoppingCartID == 0)
            {
                updateCart = true;
            }

            // Update shopping cart when required
            if (updateCart)
            {
                ShoppingCartInfoProvider.SetShoppingCartInfo(this.ShoppingCart);
            }

            // Add item to shopping cart
            ShoppingCartItemInfo addedItem = this.ShoppingCart.SetShoppingCartItem(cartItemParams);

            if (addedItem != null)
            {
                // Update shopping cart item in database
                ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(addedItem);

                // Update product options in database
                foreach (ShoppingCartItemInfo option in addedItem.ProductOptions)
                {
                    ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(option);
                }

                // Update bundle items in database
                foreach (ShoppingCartItemInfo bundleItem in addedItem.BundleItems)
                {
                    ShoppingCartItemInfoProvider.SetShoppingCartItemInfo(bundleItem);
                }

                // Track 'Add to shopping cart' conversion
                ECommerceHelper.TrackAddToShoppingCartConversion(addedItem);

                // If user has to be redirected to shopping cart
                if (this.RedirectToShoppingCart)
                {
                    // Set shopping cart referrer
                    SessionHelper.SetValue("ShoppingCartUrlReferrer", URLHelper.CurrentURL);

                    // Ensure shopping cart update
                    SessionHelper.SetValue("checkinventory", true);

                    // Redirect to shopping cart
                    URLHelper.Redirect(this.ShoppingCartUrl);
                }
                else
                {
                    // Localize SKU name
                    string skuName = (addedItem.SKUObj != null) ? ResHelper.LocalizeString(addedItem.SKUObj.SKUName) : "";

                    // Check inventory
                    string checkInventoryMessage = ShoppingCartInfoProvider.CheckShoppingCart(this.ShoppingCart).Replace(";", "\n");

                    // Get prodcut added message
                    string message = String.Format(this.GetString("com.productadded"), skuName);

                    // Add inventory check message
                    if (!String.IsNullOrEmpty(checkInventoryMessage))
                    {
                        message += "\n\n" + checkInventoryMessage;
                    }

                    // Count and show total price with options
                    CalculateTotalPrice();

                    // Register the call of JS handler informing about added product
                    ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ShoppingCartItemAddedHandler", "if (typeof ShoppingCartItemAddedHandler == 'function') { ShoppingCartItemAddedHandler(" + ScriptHelper.GetString(message) + "); }", true);
                }
            }
        }
    }


    /// <summary>
    /// Loads product options.
    /// </summary>
    private void LoadProductOptions()
    {
        DataSet dsCategories = null;

        if (this.IsLiveSite)
        {
            // Get cache minutes
            int cacheMinutes = SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSCacheMinutes");

            // Try to get data from cache
            using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref dsCategories, cacheMinutes, true, null, "skuproductoptions", this.SKUID))
            {
                if (cs.LoadData)
                {
                    // Get the data
                    dsCategories = OptionCategoryInfoProvider.GetSKUOptionCategories(this.SKUID, true);

                    // Save to the cache
                    if (cs.Cached)
                    {
                        cs.CacheDependency = CacheHelper.GetCacheDependency("ecommerce.sku|byid|" + this.SKUID);
                        cs.Data = dsCategories;
                    }
                }
            }
        }
        else
        {
            // Get the data
            dsCategories = OptionCategoryInfoProvider.GetSKUOptionCategories(this.SKUID, true);
        }

        // Initialize product option selectors
        if (!DataHelper.DataSourceIsEmpty(dsCategories))
        {
            this.mProductOptions = new Hashtable();

            foreach (DataRow dr in dsCategories.Tables[0].Rows)
            {
                try
                {
                    // Load control for selection product options
                    ProductOptionSelector selector = (ProductOptionSelector)LoadControl("~/CMSModules/Ecommerce/Controls/ProductOptions/ProductOptionSelector.ascx");

                    selector.ID = "opt" + ValidationHelper.GetInteger(dr["CategoryID"], 0);
                    selector.LocalShoppingCartObj = this.ShoppingCart;
                    selector.ShowPriceIncludingTax = this.ShowPriceIncludingTax;

                    // Set option category data to the selector
                    selector.OptionCategory = new OptionCategoryInfo(dr);

                    // Load all product options
                    foreach (DictionaryEntry entry in selector.ProductOptions)
                    {
                        this.mProductOptions[entry.Key] = entry.Value;
                    }

                    if (this.ShowTotalPrice)
                    {
                        ListControl lc = selector.SelectionControl as ListControl;
                        if (lc != null)
                        {
                            // Add Index change handler
                            lc.AutoPostBack = true;
                            lc.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);
                        }
                    }

                    // Add control to the collection
                    this.pnlSelectors.Controls.Add(selector);
                }
                catch
                {
                }
            }
        }
    }


    /// <summary>
    /// Selector index change handler.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    private void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateTotalPrice();
    }


    /// <summary>
    /// Calculate total price with product options prices.
    /// </summary>
    private void CalculateTotalPrice()
    {
        // Add SKU price        
        double price = SKUInfoProvider.GetSKUPrice(this.SKU, this.ShoppingCart, true, this.ShowPriceIncludingTax);

        // Add prices of all product options
        List<ShoppingCartItemParameters> optionParams = ProductOptionsParameters;

        foreach (ShoppingCartItemParameters optionParam in optionParams)
        {
            SKUInfo sku = null;

            if (this.mProductOptions.Contains(optionParam.SKUID))
            {
                // Get option SKU data
                sku = (SKUInfo)this.mProductOptions[optionParam.SKUID];

                // Add option price                
                price += SKUInfoProvider.GetSKUPrice(sku, this.ShoppingCart, true, this.ShowPriceIncludingTax);
            }
        }

        // Convert to shopping cart currency
        price = this.ShoppingCart.ApplyExchangeRate(price);

        lblPriceValue.Text = CurrencyInfoProvider.GetFormattedPrice(price, this.ShoppingCart.CurrencyInfoObj);
    }


    /// <summary>
    /// Logs activity
    /// </summary>
    /// <param name="skuId">Product ID</param>
    /// <param name="skuName">Product name</param>
    /// <param name="Quantity">Quantity</param>
    private void LogProductAddedToSCActivity(int skuId, string skuName, int Quantity)
    {
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        string siteName = CMSContext.CurrentSiteName;
        if (!ActivitySettingsHelper.AddingProductToSCEnabled(siteName) || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName))
        {
            return;
        }

        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.PRODUCT_ADDED_TO_SHOPPINGCART,
            TitleData = skuName,
            Value = Quantity.ToString(),
            ItemID = skuId,
            URL = URLHelper.CurrentRelativePath,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }

    #endregion
}
