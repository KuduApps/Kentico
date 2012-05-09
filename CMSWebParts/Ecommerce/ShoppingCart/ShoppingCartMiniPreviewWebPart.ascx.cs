using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.SettingsProvider;

public partial class CMSWebParts_Ecommerce_ShoppingCart_ShoppingCartMiniPreviewWebPart : CMSAbstractWebPart
{
    protected string mSiteName = "";
    protected string rtlFix = "";


    #region "Public properties"

    /// <summary>
    /// Indicates whether shopping cart total price should be displayed to the user.
    /// </summary>
    public bool ShowTotalPrice
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowTotalPrice"), true);
        }
        set
        {
            this.SetValue("ShowTotalPrice", value);
            this.plcTotalPrice.Visible = value;
        }
    }


    /// <summary>
    /// Icon image Url.
    /// </summary>
    public string IconImageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("IconImageUrl"), GetImageUrl("CMSModules/CMS_Ecommerce/cart.png"));
        }
        set
        {
            this.SetValue("IconImageUrl", value);
            this.imgCartIcon.ImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the shopping cart link url.
    /// </summary>
    public string ShoppingCartLinkUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ShoppingCartLinkUrl"), SettingsKeyProvider.GetStringValue(SiteName + ".CMSShoppingCartURL"));
        }
        set
        {
            this.SetValue("ShoppingCartLinkUrl", value);
            this.lnkShoppingCart.NavigateUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the shopping cart link text.
    /// </summary>
    public string ShoppingCartLinkText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ShoppingCartLinkText"), ResHelper.LocalizeString("{$ShopingCartMiniPreview.ShoppingCartLinkText$}"));
        }
        set
        {
            this.SetValue("ShoppingCartLinkText", value);
            lnkShoppingCart.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the my account link url.
    /// </summary>
    public string MyAccountLinkUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("MyAccountLinkUrl"), SettingsKeyProvider.GetStringValue(SiteName + ".CMSMyAccountURL"));
        }
        set
        {
            this.SetValue("MyAccountLinkUrl", value);
            lnkMyAccount.NavigateUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the my account link text.
    /// </summary>
    public string MyAccountLinkText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("MyAccountLinkText"), ResHelper.LocalizeString("{$ShopingCartMiniPreview.MyAccountLinkText$}"));
        }
        set
        {
            this.SetValue("MyAccountLinkText", value);
            lnkMyAccount.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the my wishlist link url.
    /// </summary>
    public string MyWishlistLinkUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WishlistLinkUrl"), SettingsKeyProvider.GetStringValue(SiteName + ".CMSWishlistURL"));
        }
        set
        {
            this.SetValue("WishlistLinkUrl", value);
            this.lnkMyWishlist.NavigateUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the my wishlist link text.
    /// </summary>
    public string MyWishlistLinkText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WishlistLinkText"), ResHelper.LocalizeString("{$ShopingCartMiniPreview.MyWishlistLinkText$}"));
        }
        set
        {
            this.SetValue("WishlistLinkText", value);
            this.lnkMyWishlist.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the total price title text.
    /// </summary>
    public string TotalPriceTitleText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("TotalPriceTitleText"), ResHelper.LocalizeString("{$ShopingCartMiniPreview.TotalPriceTitleText$}"));
        }
        set
        {
            this.SetValue("TotalPriceTitleText", value);
            lblTotalPriceTitle.Text = value;
        }
    }


    /// <summary>
    /// Gets the total price value text.
    /// </summary>
    public string TotalPriceValueText
    {
        get
        {
            ShoppingCartInfo sc = ECommerceContext.CurrentShoppingCart;
            if ((sc != null) && (sc.CartItems.Count > 0))
            {
                return CurrencyInfoProvider.GetFormattedPrice(sc.RoundedTotalPrice, sc.CurrencyInfoObj);
            }
            else
            {
                return "";
            }
        }
    }


    /// <summary>
    /// Link separator
    /// </summary>
    public string Separator 
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("Separator"), "|&nbsp;");
        }
        set
        {
            this.SetValue("Separator", value);
        }
    }


    /// <summary>
    /// Gets the site name.
    /// </summary>
    protected string SiteName
    {
        get
        {
            if ((mSiteName == "") && (CMSContext.CurrentSite != null))
            {
                mSiteName = CMSContext.CurrentSiteName;
            }

            return mSiteName;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            imgCartIcon.ImageUrl = ResolveUrl(this.IconImageUrl);
            imgCartIcon.AlternateText = GetString("ShoppingcartPreview.Icon");

            // Set link to the shopping cart if is defined
            if (this.ShoppingCartLinkUrl != "")
            {
                lnkShoppingCart.NavigateUrl = URLHelper.ResolveUrl(URLHelper.AddPrefixToUrl(this.ShoppingCartLinkUrl));
                lnkShoppingCart.Text = this.ShoppingCartLinkText;
            }
            else
            {
                plcShoppingCart.Visible = false;
            }

            // Set link to the wishlist if is defined
            if (this.MyWishlistLinkUrl != "")
            {
                lnkMyWishlist.NavigateUrl = URLHelper.ResolveUrl(URLHelper.AddPrefixToUrl(this.MyWishlistLinkUrl));
                lnkMyWishlist.Text = this.MyWishlistLinkText;
            }
            else
            {
                plcMyWishlist.Visible = false;
            }

            // Set link to the My Account if is defined
            if (this.MyAccountLinkUrl != "")
            {
                lnkMyAccount.NavigateUrl = URLHelper.ResolveUrl(URLHelper.AddPrefixToUrl(this.MyAccountLinkUrl));
                lnkMyAccount.Text = this.MyAccountLinkText;
            }
            else
            {
                plcMyAccount.Visible = false;
            }            
            if (this.ShowTotalPrice)
            {
                plcTotalPrice.Visible = true;
                imgCartIcon.Visible = true;

                // Check if RTL fix must be applied
                if (CultureHelper.IsPreferredCultureRTL())
                {
                    rtlFix = "<span style=\"visibility:hidden;\">.</span>";
                }
            }
            else
            {
                plcTotalPrice.Visible = false;
                imgCartIcon.Visible = false;
            }
        }
    }


    /// OnPreRender override
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Set total price value
        SetShoppingCartPreviewText();
    }


    private void SetShoppingCartPreviewText()
    {
        ShoppingCartInfo sc = ECommerceContext.CurrentShoppingCart;
        if ((sc != null) && (sc.CartItems.Count > 0))
        {            
            lblTotalPriceTitle.Text = this.TotalPriceTitleText.TrimEnd() + "&nbsp;";

            // Try to get shopping cart currency
            CurrencyInfo currency = sc.CurrencyInfoObj;

            // If shopping cart currency not defined, use default currency
            if (sc.CurrencyInfoObj == null)
            {
                currency = CurrencyInfoProvider.GetMainCurrency(CMSContext.CurrentSiteID);
            }

            // Display formatted total price
            lblTotalPriceValue.Text = CurrencyInfoProvider.GetFormattedPrice(sc.RoundedTotalPrice, currency);
        }
        else
        {
            // Display shopping cart empty string
            lblTotalPriceTitle.Text = GetString("ShoppingCart.Empty");
            lblTotalPriceValue.Visible = false;
        }
    }
}
