using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Caching;

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

/// <summary>
/// Summary description for Functions.
/// </summary>
public static class EcommerceFunctions
{
    #region "Price - obsolete methods"

    /// <summary>
    /// Returns formatted price using current shopping cart data from CMS context. Customer discount level is not applied to the product price.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    [Obsolete ("Use SKUInfoProvider.GetSKUFormattedPrice(SKUInfo, null, false, false) in your web project or GetSKUFormattedPrice(false, false) in transformation.")]
    public static string GetFormatedPrice(object SKUPrice)
    {
        return GetFormatedPrice(SKUPrice, 0, null, 0, true);
    }


    /// <summary>
    /// Returns formatted price using current shopping cart data from CMS context. Customer discount level is applied to the product price when this discount level is assigned to the specified product department.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>    
    /// <param name="SKUDepartmentId">SKU department ID</param>
    [Obsolete("Use SKUInfoProvider.GetSKUFormattedPrice(SKUInfo, null, true, false) in your web project or GetSKUFormattedPrice(true, false) in transformation.")]
    public static string GetFormatedPrice(object SKUPrice, object SKUDepartmentId)
    {
        return GetFormatedPrice(SKUPrice, SKUDepartmentId, null, 0, true);
    }


    /// <summary>
    /// Returns formatted price using specified shopping cart data.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    /// <param name="SKUDepartmentId">SKU department ID</param>
    /// <param name="cart">Shopping cart object with required data for price formatting, if it is NULL shopping cart data from CMS context are used</param>
    [Obsolete("Use SKUInfoProvider.GetSKUFormattedPrice(SKUInfo, ShoppingCartInfo, true, false) in your web project or GetSKUFormattedPrice(true, false) in transformation.")]
    public static string GetFormatedPrice(object SKUPrice, object SKUDepartmentId, ShoppingCartInfo cart)
    {
        return GetFormatedPrice(SKUPrice, SKUDepartmentId, cart, 0, true);
    }


    /// <summary>
    /// Returns formatted price using specified shopping cart data.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    /// <param name="SKUDepartmentId">SKU department ID</param>
    /// <param name="SKUId">Product ID</param>
    [Obsolete("Use SKUInfoProvider.GetSKUFormattedPrice(SKUInfo, null, true, true) in your web project or GetSKUFormattedPrice(true, true) in transformation.")]
    public static string GetFormatedPrice(object SKUPrice, object SKUDepartmentId, object SKUId)
    {
        return GetFormatedPrice(SKUPrice, SKUDepartmentId, null, SKUId, true);
    }


    /// <summary>
    /// Returns formatted price using specified shopping cart data.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    /// <param name="SKUDepartmentId">SKU department ID</param>
    /// <param name="cart">Shopping cart object with required data for price formatting, if it is NULL shopping cart data from CMS context are used</param>
    /// <param name="SKUId">Product ID</param>
    [Obsolete("Use SKUInfoProvider.GetSKUFormattedPrice(SKUInfo, ShoppingCartInfo, true, true) in your web project or GetSKUFormattedPrice(true, true) in transformation.")]
    public static string GetFormatedPrice(object SKUPrice, object SKUDepartmentId, ShoppingCartInfo cart, object SKUId)
    {
        return GetFormatedPrice(SKUPrice, SKUDepartmentId, cart, SKUId, true);
    }


    /// <summary>
    /// Returns product price including all its taxes.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    /// <param name="SKUDepartmentId">SKU department ID</param>
    /// <param name="cart">Shopping cart with shopping data</param>
    /// <param name="SKUId">SKU ID</param>
    [Obsolete("Use SKUInfoProvider.GetSKUPrice(SKUInfo, ShoppingCartInfo, true, true) instead.")]
    public static double GetPrice(object SKUPrice, object SKUDepartmentId, ShoppingCartInfo cart, object SKUId)
    {
        string price = GetFormatedPrice(SKUPrice, SKUDepartmentId, cart, SKUId, false);
        return ValidationHelper.GetDouble(price, 0);
    }


    /// <summary>
    /// Returns product price without its taxes.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    /// <param name="SKUDepartmentId">SKU department ID</param>
    /// <param name="cart">Shopping cart with shopping data</param>
    [Obsolete("Use SKUInfoProvider.GetSKUPrice(SKUInfo, ShoppingCartInfo, true, false) instead.")]
    public static double GetPrice(object SKUPrice, object SKUDepartmentId, ShoppingCartInfo cart)
    {
        return GetPrice(SKUPrice, SKUDepartmentId, cart, 0);
    }


    /// <summary>
    /// Returns formatted price using specified shopping cart data.
    /// </summary>
    /// <param name="SKUPrice">SKU price</param>
    /// <param name="SKUDepartmentId">SKU department ID</param>
    /// <param name="cart">Shopping cart object with required data for price formatting, if it is NULL shopping cart data from CMS context are used</param>
    /// <param name="SKUId">Product ID</param>
    /// <param name="FormatPrice">Format output price</param>
    /// <param name="globalSKU">True if price belongs to global product.</param>
    [Obsolete("Use SKUInfoProvider.GetSKUFormattedPrice(SKUInfo, ShoppingCartInfo, true, true) in your web project or GetSKUFormattedPrice(true, true) in transformation.")]
    public static string GetFormatedPrice(object SKUPrice, object SKUDepartmentId, ShoppingCartInfo cart, object SKUId, bool FormatPrice)
    {
        double price = ValidationHelper.GetDouble(SKUPrice, 0);
        int departmentId = ValidationHelper.GetInteger(SKUDepartmentId, 0);
        int skuId = ValidationHelper.GetInteger(SKUId, 0);

        // When on the live site
        if (cart == null)
        {
            cart = ECommerceContext.CurrentShoppingCart;           
        }

        if (departmentId > 0)
        {
            // Try site discount level
            DiscountLevelInfo discountLevel = cart.SiteDiscountLevel;
            bool valid = (discountLevel != null) && discountLevel.IsInDepartment(departmentId) && (cart.IsCreatedFromOrder || discountLevel.IsValid);

            if (!valid)
            {
                // Try global discount level
                discountLevel = cart.DiscountLevelInfoObj;
                valid = (discountLevel != null) && discountLevel.IsInDepartment(departmentId) && (cart.IsCreatedFromOrder || discountLevel.IsValid);
            }

            // Apply discount to product price 
            if (valid)
            {
                // Remember price before discount
                double oldPrice = price;

                // Get new price after discount
                price = price * (1 - discountLevel.DiscountLevelValue / 100);

                if ((oldPrice > 0) && (price < 0))
                {
                    price = 0;
                }
            }
        }

        int stateId = cart.StateID;
        int countryId = cart.CountryID;
        bool isTaxIDSupplied = ((cart.CustomerInfoObj != null) && (cart.CustomerInfoObj.CustomerTaxRegistrationID != ""));
        
        if ((skuId > 0) && ((stateId > 0) || (countryId > 0)))
        {
            // Apply taxes
            price += GetSKUTotalTax(price, skuId, stateId, countryId, isTaxIDSupplied);
        }
        
        // Apply exchange rate
        price = cart.ApplyExchangeRate(price);

        if (FormatPrice)
        {
            // Get formatted price
            return cart.GetFormattedPrice(price, true);
        }
        else
        {
            // Get not-formated price
            return price.ToString();
        }
    }


    /// <summary>
    /// Returns product total tax in site main currency.
    /// </summary>
    /// <param name="skuPrice">SKU price</param>
    /// <param name="skuId">SKU ID</param>
    /// <param name="stateId">Customer billing address state ID</param>
    /// <param name="countryId">Customer billing addres country ID</param>
    /// <param name="isTaxIDSupplied">Indicates if customer tax registration ID is supplied</param>    
    private static double GetSKUTotalTax(double skuPrice, int skuId, int stateId, int countryId, bool isTaxIDSupplied)
    {
        double totalTax = 0;
        int cacheMinutes = 0;

        // Try to get data from cache
        using (CachedSection<double> cs = new CachedSection<double>(ref totalTax, cacheMinutes, true, null, "skutotaltax|", skuId, skuPrice, stateId, countryId, isTaxIDSupplied))
        {
            if (cs.LoadData)
            {
                // Get all the taxes and their values which are applied to the specified product
                DataSet ds = TaxClassInfoProvider.GetTaxes(skuId, countryId, stateId, null);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        bool zeroTax = ValidationHelper.GetBoolean(dr["TaxClassZeroIfIDSupplied"], false);
                        if (!(isTaxIDSupplied && zeroTax))
                        {
                            double taxValue = ValidationHelper.GetDouble(dr["TaxValue"], 0);
                            bool isFlat = ValidationHelper.GetBoolean(dr["TaxIsFlat"], false);

                            // Add tax value                        
                            totalTax += TaxClassInfoProvider.GetTaxValue(skuPrice, taxValue, isFlat);
                        }
                    }
                }

                // Cache the data
                cs.Data = totalTax;
            }
        }

        return totalTax;
    }

    #endregion


    #region "Links"

    /// <summary>
    /// Returns link to "add to shoppingcart".
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="enabled">Indicates whether product is enabled or not</param>
    public static string GetAddToShoppingCartLink(object productId, object enabled)
    {
        return GetAddToShoppingCartLink(productId, enabled, null);
    }


    /// <summary>
    /// Returns link to "add to shoppingcart".
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="enabled">Indicates whether product is enabled or not</param>
    /// <param name="imageUrl">Image URL</param>
    public static string GetAddToShoppingCartLink(object productId, object enabled, string imageUrl)
    {
        if (ValidationHelper.GetBoolean(enabled, false) && (ValidationHelper.GetInteger(productId, 0) != 0))
        {
            // Get default image URL
            imageUrl = imageUrl ?? "CMSModules/CMS_Ecommerce/addorder.png";
            return "<img src=\"" + UIHelper.GetImageUrl(null, imageUrl) + "\" alt=\"Add to cart\" /><a href=\"" + ShoppingCartURL(CMSContext.CurrentSiteName) + "?productId=" + Convert.ToString(productId) + "&amp;quantity=1\">" + ResHelper.GetString("EcommerceFunctions.AddToShoppingCart") + "</a>";
        }
        else
        {
            return "";
        }
    }


    /// <summary>
    /// Returns link to "add to shoppingcart".
    /// </summary>
    /// <param name="productId">Product ID</param>
    public static string GetAddToShoppingCartLink(object productId)
    {
        return GetAddToShoppingCartLink(productId, true);
    }


    /// <summary>
    /// Returns link to add specified product to the user's wishlist.
    /// </summary>
    /// <param name="productId">Product ID</param>
    public static string GetAddToWishListLink(object productId)
    {
        return GetAddToWishListLink(productId, null);
    }


    /// <summary>
    /// Returns link to add specified product to the user's wishlist.
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="imageUrl">Image URL</param>
    public static string GetAddToWishListLink(object productId, string imageUrl)
    {
        if (ValidationHelper.GetInteger(productId, 0) != 0)
        {
            // Get default image URL
            imageUrl = imageUrl ?? "CMSModules/CMS_Ecommerce/addtowishlist.png";
            return "<img src=\"" + UIHelper.GetImageUrl(null, imageUrl) + "\" alt=\"Add to wishlist\" /><a href=\"" + WishlistURL(CMSContext.CurrentSiteName) + "?productId=" + Convert.ToString(productId) + "\">" + ResHelper.GetString("EcommerceFunctions.AddToWishlist") + "</a>";
        }
        else
        {
            return "";
        }
    }


    /// <summary>
    /// Returns link to remove specified product from the user's wishlist.
    /// </summary>
    /// <param name="productId">Product ID</param>
    public static string GetRemoveFromWishListLink(object productId)
    {
        if ((productId != DBNull.Value) && (!CMSContext.CurrentUser.IsPublic()))
        {
            return "<a href=\"javascript:onclick=RemoveFromWishlist(" + Convert.ToString(productId) + ")\" class=\"RemoveFromWishlist\">" + ResHelper.GetString("Wishlist.RemoveFromWishlist") + "</a>";
        }
        else
        {
            return "";
        }
    }

    #endregion


    #region "URLs"

    /// <summary>
    /// Returns URL to the shopping cart.
    /// </summary>
    /// <param name="siteName">Site name</param>
    public static string ShoppingCartURL(string siteName)
    {
        string settingsKey = "CMSShoppingCartURL";

        // Create settingkey
        if (ValidationHelper.GetString(siteName, "") != "")
        {
            settingsKey = siteName + "." + settingsKey;
        }

        return URLHelper.ResolveUrl(SettingsKeyProvider.GetStringValue(settingsKey));
    }


    /// <summary>
    /// Returns URL to the wish list.
    /// </summary>
    /// <param name="siteName">Site name</param>
    public static string WishlistURL(string siteName)
    {
        string settingsKey = "CMSWishlistURL";

        // Create settingkey
        if (ValidationHelper.GetString(siteName, "") != "")
        {
            settingsKey = siteName + "." + settingsKey;
        }

        return URLHelper.ResolveUrl(SettingsKeyProvider.GetStringValue(settingsKey));
    }


    /// <summary>
    /// Returns product URL.
    /// </summary>
    /// <param name="SKUID">SKU ID</param>
    public static string GetProductUrl(object SKUID)
    {
        return URLHelper.ResolveUrl("~/CMSPages/GetProduct.aspx?productId=" + Convert.ToString(SKUID));
    }


    /// <summary>
    /// Returns user friendly URL of the specified SKU and site name.
    /// </summary>
    /// <param name="skuGuid">SKU Guid</param>
    /// <param name="skuName">SKU Name</param>
    /// <param name="siteNameObj">Site Name</param>
    public static string GetProductUrl(object skuGuid, object skuName, object siteNameObj)
    {
        Guid guid = ValidationHelper.GetGuid(skuGuid, Guid.Empty);
        string name = Convert.ToString(skuName);
        string siteName = ValidationHelper.GetString(siteNameObj, null);
        return URLHelper.ResolveUrl(SKUInfoProvider.GetSKUUrl(guid, name, siteName));
    }


    /// <summary>
    /// Returns user friendly URL of the specified SKU.
    /// </summary>
    /// <param name="skuGuid">SKU Guid</param>
    /// <param name="skuName">SKU Name</param>
    public static string GetProductUrl(object skuGuid, object skuName)
    {
        Guid guid = ValidationHelper.GetGuid(skuGuid, Guid.Empty);
        string name = Convert.ToString(skuName);
        return URLHelper.ResolveUrl(SKUInfoProvider.GetSKUUrl(guid, name));
    }

    #endregion


    #region "Objects properties"

    /// <summary>
    /// Returns the public SKU status display name.
    /// </summary>
    /// <param name="statusId">Status ID</param>
    public static string GetPublicStatusName(object statusId)
    {
        int sid = ValidationHelper.GetInteger(statusId, 0);
        if (sid > 0)
        {
            PublicStatusInfo si = PublicStatusInfoProvider.GetPublicStatusInfo(sid);
            if (si != null)
            {
                return si.PublicStatusDisplayName;
            }
        }
        return "";
    }


    /// <summary>
    /// Gets object from the specified column of the manufacturer with specific ID.
    /// </summary>
    /// <param name="Id">Manufacturer ID</param>
    /// <param name="column">Column name</param>
    public static object GetManufacturer(object Id, string column)
    {
        int id = ValidationHelper.GetInteger(Id, 0);
        if ((id > 0) && !DataHelper.IsEmpty(column))
        {
            // Get manufacturer
            ManufacturerInfo mi = ManufacturerInfoProvider.GetManufacturerInfo(id);
            if (mi != null)
            {
                // Return datarow value if specified column exists
                if (mi.ContainsColumn(column))
                {
                    return mi.GetValue(column);
                }
                else
                {
                    return "";
                }
            }
        }

        return "";
    }


    /// <summary>
    /// Gets object from the specified column of the department with specific ID.
    /// </summary>
    /// <param name="Id">Department ID</param>
    /// <param name="column">Column name</param>
    public static object GetDepartment(object Id, string column)
    {
        int id = ValidationHelper.GetInteger(Id, 0);

        if (id > 0 && !DataHelper.IsEmpty(column))
        {
            // Get department
            DepartmentInfo di = DepartmentInfoProvider.GetDepartmentInfo(id);

            if (di != null)
            {
                // Return datarow value if specified column exists
                if (di.ContainsColumn(column))
                {
                    return di.GetValue(column);
                }
                else
                {
                    return "";
                }
            }
        }

        return "";
    }


    /// <summary>
    /// Gets object from the specified column of the supplier with specific ID.
    /// </summary>
    /// <param name="Id">Supplier ID</param>
    /// <param name="column">Column name</param>
    public static object GetSupplier(object Id, string column)
    {
        int id = ValidationHelper.GetInteger(Id, 0);
        if ((id > 0) && !DataHelper.IsEmpty(column))
        {
            // Get supplier
            SupplierInfo si = SupplierInfoProvider.GetSupplierInfo(id);

            if (si != null)
            {
                // Return datarow value if specified column exists
                if (si.ContainsColumn(column))
                {
                    return si.GetValue(column);
                }
                else
                {
                    return "";
                }
            }
        }

        return "";
    }


    /// <summary>
    /// Gets object from the specified column of the internal status with specific ID.
    /// </summary>
    /// <param name="Id">Internal status ID</param>
    /// <param name="column">Column name</param>
    public static object GetInternalStatus(object Id, string column)
    {
        int id = ValidationHelper.GetInteger(Id, 0);
        if ((id > 0) && !DataHelper.IsEmpty(column))
        {
            // Get internal status
            InternalStatusInfo isi = InternalStatusInfoProvider.GetInternalStatusInfo(id);

            if (isi != null)
            {
                // Return datarow value if specified column exists
                if (isi.ContainsColumn(column))
                {
                    return isi.GetValue(column);
                }
                else
                {
                    return "";
                }
            }
        }

        return "";
    }


    /// <summary>
    /// Gets object from the specified column of the public status with specific ID.
    /// </summary>
    /// <param name="Id">Public status ID</param>
    /// <param name="column">Column name</param>
    public static object GetPublicStatus(object Id, string column)
    {
        int id = ValidationHelper.GetInteger(Id, 0);
        if ((id > 0) && !DataHelper.IsEmpty(column))
        {
            // Get public status
            PublicStatusInfo psi = PublicStatusInfoProvider.GetPublicStatusInfo(id);

            if (psi != null)
            {
                // Return datarow value if specified column exists
                if (psi.ContainsColumn(column))
                {
                    return psi.GetValue(column);
                }
                else
                {
                    return "";
                }
            }
        }

        return "";
    }


    /// <summary>
    /// Gets document name of specified nodeid.
    /// </summary>
    public static string GetDocumentName(object nodeIdent)
    {
        int nodeId = ValidationHelper.GetInteger(nodeIdent, 0);
        if (nodeId != 0)
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode);
            if (node != null)
            {
                return node.DocumentName;
            }
        }
        return String.Empty;
    }

    #endregion
    

    #region "Images"

    /// <summary>
    /// Returns complete HTML code of the specified product image, if not such image exists, default image is returned.
    /// </summary>
    /// <param name="imageUrl">Product image url</param>    
    /// <param name="alt">Image alternate text</param>
    public static string GetProductImage(object imageUrl, object alt)
    {
        return GetImage(imageUrl, 0, 0, 0, alt);
    }


    /// <summary>
    /// Returns complete HTML code of the specified resized product image, if not such image exists, default image is returned.
    /// </summary>
    /// <param name="imageUrl">Product image url</param>
    /// <param name="height">Height of image</param>
    /// <param name="alt">Image alternate text</param>
    /// <param name="width">Width of image</param>
    public static string GetProductImage(object imageUrl, object width, object height, object alt)
    {
        return GetImage(imageUrl, width, height, 0, alt);
    }


    /// <summary>
    /// Returns complete HTML code of the specified resized product image, if not such image exists, default image is returned.
    /// </summary>
    /// <param name="imageUrl">Product image url</param>
    /// <param name="maxSideSize">Max side size</param>   
    /// <param name="alt">Image alternate text</param>
    public static string GetProductImage(object imageUrl, object maxSideSize, object alt)
    {
        return GetImage(imageUrl, 0, 0, maxSideSize, alt);
    }


    private static string GetImage(object imageUrl, object width, object height, object maxSideSize, object alt)
    {
        // Get image alternate text
        string sAlt = ValidationHelper.GetString(alt, "");
        if (sAlt != "")
        {
            sAlt = ResHelper.LocalizeString(sAlt);
        }

        // Get product image url        
        string url = ValidationHelper.GetString(imageUrl, "");

        // If product image not found
        if (url == "")
        {
            // Get default product image url                      
            url = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultProductImageURL");
        }

        // Get url with application path
        if (!String.IsNullOrEmpty(url))
        {
            url = URLHelper.ResolveUrl(url);
            int slashIndex = url.LastIndexOf('/');
            if (slashIndex >= 0)
            {
                string urlStartPart = url.Substring(0, slashIndex);
                string urlEndPart = url.Substring(slashIndex);

                url = urlStartPart + HttpUtility.UrlPathEncode(urlEndPart);
            
                int iMaxSideSize = ValidationHelper.GetInteger(maxSideSize, 0);
                int iWidth = ValidationHelper.GetInteger(width, 0);
                int iHeight = ValidationHelper.GetInteger(height, 0);

                // Add max side size
                if (iMaxSideSize > 0)
                {
                    url = URLHelper.AddParameterToUrl(url, "maxSideSize", iMaxSideSize.ToString());
                }
                else
                {
                    // Add width
                    if (iWidth > 0)
                    {
                        url = URLHelper.AddParameterToUrl(url, "width", iWidth.ToString());
                    }

                    // Add height
                    if (iHeight > 0)
                    {
                        url = URLHelper.AddParameterToUrl(url, "height", iHeight.ToString());
                    }
                }
            }

            return "<img alt=\"" + HTMLHelper.HTMLEncode(sAlt) + "\" title=\"" + HTMLHelper.HTMLEncode(sAlt) + "\" src=\"" + HTMLHelper.HTMLEncode(url) + "\" border=\"0\" />";
        }

        return "";
    }

    #endregion


    #region "UI methods"

    /// <summary>
    /// Sets different css styles to enabled and disabled dropdownlist items.
    /// </summary>
    /// <param name="drpTemp">Dropdownlist control</param>
    /// <param name="valueFieldName">Field name with ID value</param>
    /// <param name="statusFieldName">Field name with status value</param>
    /// <param name="itemEnabledStyle">Enabled item style</param>
    /// <param name="itemDisabledStyle">Disabled item style</param>
    public static void MarkEnabledAndDisabledItems(DropDownList drpTemp, string valueFieldName, string statusFieldName, string itemEnabledStyle, string itemDisabledStyle)
    {
        itemEnabledStyle = (itemEnabledStyle == "") ? "DropDownItemEnabled" : itemEnabledStyle;
        itemDisabledStyle = (itemDisabledStyle == "") ? "DropDownItemDisabled" : itemDisabledStyle;

        if (!DataHelper.DataSourceIsEmpty(drpTemp.DataSource))
        {
            if (drpTemp.DataSource is DataSet)
            {
                ListItem li = null;

                foreach (DataRow row in ((DataSet)drpTemp.DataSource).Tables[0].Rows)
                {
                    li = drpTemp.Items.FindByValue(Convert.ToString(row[valueFieldName]));
                    if ((li != null) && (li.Value != "0"))
                    {
                        // Item is enabled
                        if (ValidationHelper.GetBoolean(row[statusFieldName], false))
                        {
                            li.Attributes.Add("class", itemEnabledStyle);
                        }
                        // Item is disabled
                        else
                        {
                            li.Attributes.Add("class", itemDisabledStyle);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Sets different css styles to enabled and disabled dropdownlist items.
    /// </summary>
    /// <param name="drpTemp">Dropdownlist control</param>
    /// <param name="valueFieldName">Field name with ID value</param>
    /// <param name="statusFieldName">Field name with status value</param>
    public static void MarkEnabledAndDisabledItems(DropDownList drpTemp, string valueFieldName, string statusFieldName)
    {
        MarkEnabledAndDisabledItems(drpTemp, valueFieldName, statusFieldName, "", "");
    }

    #endregion
}
