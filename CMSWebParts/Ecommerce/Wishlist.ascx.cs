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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.URLRewritingEngine;
using CMS.ExtendedControls;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSWebParts_Ecommerce_Wishlist : CMSAbstractWebPart
{
    #region "Variables"

    protected int mSKUId = 0;
    protected CurrentUserInfo currentUser = null;
    protected SiteInfo currentSite = null;
    protected bool mRemove = false;

    protected Button btnRemoveProduct = null;
    protected HiddenField hidProductID = null;
    protected HiddenField hidQuantity = null;
    protected string mTransformationName = "ecommerce.transformations.product_wishlist";

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets or sets the page url which is related to 'continue shopping' action.
    /// </summary>
    private string PreviousPageUrl
    {
        get
        {
            object obj = ViewState["PreviousPageUrl"];
            return (obj != null) ? (string)obj : "~/";
        }
        set
        {
            ViewState["PreviousPageUrl"] = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), mTransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets the separator (tetx, html code) which is displayed between displayed items.
    /// </summary>
    public string ItemSeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemSeparator"), repeater.ItemSeparator);
        }
        set
        {
            this.SetValue("ItemSeparator", value);
            repeater.ItemSeparator = value;
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
            currentUser = CMSContext.CurrentUser;

            if (currentUser.IsAuthenticated())
            {
                // Control initialiazation
                lblTitle.Text = GetString("Ecommerce.Wishlist.Title");
                btnContinue.Text = GetString("Ecommerce.Wishlist.btnContinue");

                mSKUId = QueryHelper.GetInteger("productID", 0);                
                currentSite = CMSContext.CurrentSite;

                // Set repeater transformation
                repeater.TransformationName = this.TransformationName;
                repeater.ItemSeparator = this.ItemSeparator;

                if ((currentUser != null) && (currentSite != null))
                {
                    if ((!RequestHelper.IsPostBack()) && (mSKUId > 0))
                    {
                        int addSKUId = mSKUId;

                        // Get added SKU info object from database
                        SKUInfo skuObj = SKUInfoProvider.GetSKUInfo(addSKUId);
                        if (skuObj != null)
                        {
                            // Can not add option as a product
                            if (skuObj.SKUOptionCategoryID > 0)
                            {
                                addSKUId = 0;
                            }
                            else if (!skuObj.IsGlobal)
                            {
                                // Site specific product must belong to the current site
                                if (skuObj.SKUSiteID != currentSite.SiteID)
                                {
                                    addSKUId = 0;
                                }
                            }
                            else
                            {
                                // Global products must be allowed when adding global product
                                if (!ECommerceSettings.AllowGlobalProducts(currentSite.SiteName))
                                {
                                    addSKUId = 0;
                                }
                            }
                        }

                        if (addSKUId > 0)
                        {
                            // Add specified product to the user's wishlist
                            WishlistItemInfoProvider.AddSKUToWishlist(currentUser.UserID, addSKUId, currentSite.SiteID);
                            LogProductAddedToWLActivity(addSKUId, ResHelper.LocalizeString(skuObj.SKUName), currentSite.SiteID);
                        }
                    }

                    if (mSKUId > 0)
                    {
                        // Remove product parameter from URL to avoid adding it next time
                        string newUrl = URLHelper.RemoveParameterFromUrl(URLRewriter.CurrentURL, "productID");
                        URLHelper.Redirect(newUrl);
                    }
                }
            }
            else
            {
                // Hide control if current user is not authenticated
                this.Visible = false;
            }
        }
    }


    /// <summary>
    /// Child control creation.
    /// </summary>
    protected override void CreateChildControls()
    {
        // Add product button
        this.btnRemoveProduct = new CMSButton();
        this.btnRemoveProduct.Attributes["style"] = "display: none";
        this.Controls.Add(this.btnRemoveProduct);
        this.btnRemoveProduct.Click += new EventHandler(btnRemoveProduct_Click);

        // Add the hidden fields for productId 
        this.hidProductID = new HiddenField();
        this.hidProductID.ID = "hidProductID";
        this.Controls.Add(this.hidProductID);

        base.CreateChildControls();
    }


    /// <summary>
    /// Load event handler.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        LoadData();
    }


    /// <summary>
    /// OnPreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if (!this.StopProcessing)
        {
            // Register the dialog scripts
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RemoveProductFromWishlist",
                ScriptHelper.GetScript(
                    "function setProduct(val) { document.getElementById('" + this.hidProductID.ClientID + "').value = val; } \n" +
                    "function RemoveFromWishlist(productId) { \n" +
                        "setProduct(productId); \n" +
                        ControlsHelper.GetPostBackEventReference(this.btnRemoveProduct, null) +
                    ";} \n"
                ));
        }

        // Set previous page url
        if ((!RequestHelper.IsPostBack()) && (Request.UrlReferrer != null))
        {
            string path = URLHelper.GetAppRelativePath(Request.UrlReferrer);
            if (!URLHelper.IsExcludedSystem(path))
            {
                this.PreviousPageUrl = Request.UrlReferrer.AbsoluteUri;
            }
        }
        else
        {
            // Try to find the Previeous page in session
            string prevPage = ValidationHelper.GetString(SessionHelper.GetValue("ShoppingCartUrlReferrer"), "");
            if (!String.IsNullOrEmpty(prevPage))
            {
                this.PreviousPageUrl = prevPage;
            }
        }

        base.OnPreRender(e);
    }
    

    /// <summary>
    /// Removes product from wishlist.
    /// </summary>
    void btnRemoveProduct_Click(object sender, EventArgs e)
    {
        if ((currentUser != null) && (currentSite != null))
        {
            // Remove specified product from the user's wishlist
            WishlistItemInfoProvider.RemoveSKUFromWishlist(currentUser.UserID, ValidationHelper.GetInteger(this.hidProductID.Value, 0), currentSite.SiteID);

            LoadData();
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Reloads data for wishlist.
    /// </summary>
    private void LoadData()
    {
        this.SetContext();

        if ((currentUser != null) && (currentSite != null))
        {
            repeater.DataSource = SKUInfoProvider.GetWishlistProducts(currentUser.UserID, currentSite.SiteID);
            repeater.DataBind();
        }

        // Show "Empty wishlist" message
        if (DataHelper.DataSourceIsEmpty(repeater.DataSource))
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("Ecommerce.Wishlist.EmptyMessage");
        }

        this.ReleaseContext();
    }


    /// <summary>
    /// Continue shopping.
    /// </summary>
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(this.PreviousPageUrl);
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.repeater.ClearCache();
    }


    /// <summary>
    /// Logs activity
    /// </summary>
    /// <param name="skuId">SKU ID</param>
    /// <param name="skuName">Product name</param>
    /// <param name="siteId">Site ID</param>
    private void LogProductAddedToWLActivity(int skuId, string skuName, int siteId)
    {
        if ((currentUser == null) || (CMSContext.ViewMode != ViewModeEnum.LiveSite) || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteId) ||
            !ActivitySettingsHelper.ActivitiesEnabledForThisUser(currentUser) || !ActivitySettingsHelper.AddingProductToWLEnabled(siteId))
        {
            return;
        }

        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = siteId,
            Type = PredefinedActivityType.PRODUCT_ADDED_TO_WISHLIST,
            TitleData = skuName,
            ItemID = skuId,
            URL = URLHelper.CurrentRelativePath,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }
}
