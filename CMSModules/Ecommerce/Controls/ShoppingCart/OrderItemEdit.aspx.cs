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
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_OrderItemEdit : CMSOrdersPage
{
    #region "Variables"

    private ShoppingCartInfo mShoppingCart = null;
    private ShoppingCartItemInfo mShoppingCartItem = null;
    private OptionCategoryInfo mOptionCategory = null;
    private SKUInfo mSKU = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Shopping cart object with order data.
    /// </summary>
    private ShoppingCartInfo ShoppingCart
    {
        get
        {
            if (mShoppingCart == null)
            {
                string cartSessionName = QueryHelper.GetString("cart", String.Empty);
                if (cartSessionName != String.Empty)
                {
                    mShoppingCart = SessionHelper.GetValue(cartSessionName) as ShoppingCartInfo;
                }
            }

            return mShoppingCart;
        }
    }


    /// <summary>
    /// Shopping cart item data.
    /// </summary>
    private ShoppingCartItemInfo ShoppingCartItem
    {
        get
        {
            if (mShoppingCartItem == null)
            {
                if (this.ShoppingCart != null)
                {
                    Guid cartItemGuid = QueryHelper.GetGuid("itemguid", Guid.Empty);
                    if (cartItemGuid != Guid.Empty)
                    {
                        mShoppingCartItem = this.ShoppingCart.GetShoppingCartItem(cartItemGuid);
                    }
                }
            }

            return mShoppingCartItem;
        }
    }

    /// <summary>
    /// SKU option category data
    /// </summary>
    private OptionCategoryInfo OptionCategory
    {
        get
        {
            if ((mOptionCategory == null) && (this.SKU != null))
            {
                mOptionCategory = OptionCategoryInfoProvider.GetOptionCategoryInfo(this.SKU.SKUOptionCategoryID);
            }

            return mOptionCategory;
        }
    }

    /// <summary>
    /// SKU data
    /// </summary>
    private SKUInfo SKU
    {
        get
        {
            if ((mSKU == null) && (this.ShoppingCartItem != null))
            {
                mSKU = this.ShoppingCartItem.SKUObj;
            }

            return mSKU;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls
        lblSKUName.Text = GetString("OrderItemEdit.SKUName");
        lblSKUPrice.Text = GetString("OrderItemEdit.SKUPrice");
        lblSKUUnits.Text = GetString("OrderItemEdit.SKUUnits");
        btnOk.Text = GetString("general.ok");
        this.btnCancel.Text = this.GetString("general.cancel");

        // Initialize validators
        rfvSKUName.ErrorMessage = GetString("OrderItemEdit.ErrorSKUName");
        rfvSKUUnits.ErrorMessage = GetString("OrderItemEdit.ErrorSKUUnits");
        txtSKUPrice.EmptyErrorMessage = GetString("OrderItemEdit.ErrorSKUNPrice");
        txtSKUPrice.ValidationErrorMessage = GetString("com.newproduct.skupricenotdouble");

        // Title
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_OrderItem/object.png");
        this.CurrentMaster.Title.TitleText = GetString("OrderItemEdit.Title");

        // Load data
        if (!RequestHelper.IsPostBack())
        {
            LoadData();
        }

        RegisterEscScript();
        RegisterModalPageScripts();
    }


    /// <summary>
    /// Loads product data to the form fields.
    /// </summary>
    private void LoadData()
    {
        SKUInfo sku = this.SKU;
        OrderInfo order = this.ShoppingCart.Order;

        if ((ShoppingCartItem == null) || (sku == null) || (order == null))
        {
            // Do not process
            return;
        }

        // Is form editing enabled?
        bool editingEnabled = !order.OrderIsPaid && ECommerceSettings.EnableOrderItemEditing && !ECommerceSettings.UseCurrentSKUData;

        this.txtSKUName.Text = sku.SKUName;
        this.txtSKUName.Enabled = editingEnabled;

        this.txtSKUPrice.Value = this.ShoppingCartItem.UnitPrice;
        this.txtSKUPrice.Currency = this.ShoppingCart.CurrencyInfoObj;
        this.txtSKUPrice.Enabled = editingEnabled;

        this.txtSKUUnits.Text = this.ShoppingCartItem.CartItemUnits.ToString();
        this.txtSKUUnits.Enabled = editingEnabled && !(this.ShoppingCartItem.IsProductOption || this.ShoppingCartItem.IsBundleItem);

        this.chkIsPrivate.Enabled = editingEnabled;

        this.txtItemText.Enabled = editingEnabled;
        this.txtItemMultiText.Enabled = editingEnabled;

        this.btnOk.Enabled = editingEnabled;

        // If cart item represents donation
        if (sku.SKUProductType == SKUProductTypeEnum.Donation)
        {
            // Apply additional price editing conditions
            this.txtSKUPrice.AllowZero = false;
            this.txtSKUPrice.Enabled &= !((sku.SKUMinPrice == sku.SKUPrice) && (sku.SKUMaxPrice == sku.SKUPrice));

            // Display is private checkbox
            if (sku.SKUPrivateDonation)
            {
                this.chkIsPrivate.Checked = this.ShoppingCartItem.CartItemIsPrivate;
                this.plcIsPrivate.Visible = true;
            }
        }

        // If cart item represents text option
        if (sku.SKUProductType == SKUProductTypeEnum.Text)
        {
            // Get option category
            if (this.OptionCategory != null)
            {
                // Display cart item text textbox
                if (this.OptionCategory.CategorySelectionType == OptionCategorySelectionTypeEnum.TextBox)
                {
                    this.txtItemText.Text = this.ShoppingCartItem.CartItemText;
                    this.txtItemText.Visible = true;
                }
                else
                {
                    this.txtItemMultiText.Text = this.ShoppingCartItem.CartItemText;
                    this.txtItemMultiText.Visible = true;
                }

                this.plcItemText.Visible = true;
            }
        }

        // If cart item represents e-product
        if (sku.SKUProductType == SKUProductTypeEnum.EProduct)
        {
            // Display valid to information
            if (order.OrderIsPaid)
            {
                if (this.ShoppingCartItem.CartItemValidTo.CompareTo(DateTimeHelper.ZERO_TIME) == 0)
                {
                    this.lblValidTo.Text = this.GetString("general.unlimited");
                }
                else
                {
                    this.lblValidTo.Text = this.ShoppingCartItem.CartItemValidTo.ToString();
                }
            }
            else
            {
                this.lblValidTo.Text = this.GetString("com.orderitemedit.validtonotset");
            }

            this.plcValidTo.Visible = true;
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check 'ModifyOrders' permission
        if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
        }

        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            this.ShoppingCartItem.SKUObj.SKUName = txtSKUName.Text;

            // Get new price
            double rate = (this.ShoppingCartItem.SKUObj.IsGlobal) ? this.ShoppingCart.ExchangeRateForGlobalItems : this.ShoppingCart.ExchangeRate;
            double newPrice = ExchangeRateInfoProvider.ApplyExchangeRate(txtSKUPrice.Value, 1 / rate);

            // Update price
            if (this.ShoppingCartItem.SKUObj.SKUProductType == SKUProductTypeEnum.Donation
                || this.ShoppingCartItem.SKUObj.SKUProductType == SKUProductTypeEnum.Text)
            {
                this.ShoppingCartItem.CartItemPrice = newPrice;
            }
            else
            {
                this.ShoppingCartItem.SKUObj.SKUPrice = newPrice;
            }

            // Update units
            this.ShoppingCartItem.CartItemUnits = ValidationHelper.GetInteger(txtSKUUnits.Text, 0);

            // Update is private information
            if (this.plcIsPrivate.Visible)
            {
                this.ShoppingCartItem.CartItemIsPrivate = this.chkIsPrivate.Checked;
            }

            // Update product text when visible
            if (plcItemText.Visible == true)
            {
                this.ShoppingCartItem.CartItemText = txtItemText.Visible ? txtItemText.Text : txtItemMultiText.Text;
            }

            // Update units of the product options
            foreach (ShoppingCartItemInfo option in ShoppingCartItem.ProductOptions)
            {
                option.CartItemUnits = ShoppingCartItem.CartItemUnits;
            }

            // Evaluate shopping cart content
            ShoppingCartInfoProvider.EvaluateShoppingCart(this.ShoppingCart);

            // Close dialog window and refresh parent window
            string url = "~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_Edit_OrderItems.aspx?orderid=" + this.ShoppingCart.OrderId + "&cartexist=1";
            ltlScript.Text = ScriptHelper.GetScript("CloseAndRefresh('" + ResolveUrl(url) + "')");
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Validates form data.
    /// </summary>
    private string ValidateForm()
    {
        string error = new Validator().NotEmpty(txtSKUName.Text.Trim(), rfvSKUName.ErrorMessage).
                        NotEmpty(txtSKUUnits.Text.Trim(), rfvSKUUnits.ErrorMessage).Result;

        // Validate product price
        if (error == "")
        {
            error = txtSKUPrice.ValidatePrice(this.ShoppingCartItem.IsProductOption);
        }

        // Validate price for donation
        if ((error == "") && (this.SKU.SKUProductType == SKUProductTypeEnum.Donation))
        {
            // Get min and max price in cart currency
            double minPrice = this.ShoppingCart.ApplyExchangeRate(this.SKU.SKUMinPrice, this.SKU.IsGlobal);
            double maxPrice = this.ShoppingCart.ApplyExchangeRate(this.SKU.SKUMaxPrice, this.SKU.IsGlobal);

            if ((minPrice > 0) && (this.txtSKUPrice.Value < minPrice) || ((maxPrice > 0) && (this.txtSKUPrice.Value > maxPrice)))
            {
                // Get formatted min and max price
                string fMinPrice = this.ShoppingCart.GetFormattedPrice(minPrice);
                string fMaxPrice = this.ShoppingCart.GetFormattedPrice(maxPrice);

                // Set correct error message
                if ((minPrice > 0.0) && (maxPrice > 0.0))
                {
                    error = String.Format(this.GetString("com.orderitemedit.pricerange"), fMinPrice, fMaxPrice);
                }
                else if (minPrice > 0.0)
                {
                    error = String.Format(this.GetString("com.orderitemedit.pricerangemin"), fMinPrice);
                }
                else if (maxPrice > 0.0)
                {
                    error = String.Format(this.GetString("com.orderitemedit.pricerangemax"), fMaxPrice);
                }
            }
        }

        // Validate product units
        if (error == "")
        {
            if (ValidationHelper.GetInteger(txtSKUUnits.Text.Trim(), -1) < 0)
            {
                error = GetString("OrderItemEdit.SKUUnitsNotPositiveInteger");
            }
        }

        // Validata text product option        
        if ((error == "") && (this.SKU.SKUProductType == SKUProductTypeEnum.Text))
        {
            int maxLength = this.OptionCategory.CategoryTextMaxLength;
            if (maxLength > 0)
            {
                // Check text length
                if ((txtItemText.Visible && (txtItemText.Text.Length > maxLength)) ||
                    (txtItemMultiText.Visible && (txtItemMultiText.Text.Length > maxLength)))
                {
                    error = string.Format(GetString("com.optioncategory.maxtextlengthexceeded"), maxLength);
                }
            }
        }

        return error;
    }
}
