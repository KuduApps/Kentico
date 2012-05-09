using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.GlobalHelper;
using CMS.Ecommerce;

/// <summary>
/// Sample shopping cart info provider. 
/// Can be registered either by replacing the SKUInfoProvider.ProviderObject (uncomment the line in SampleECommerceModule.cs) or through cms.extensibility section of the web.config
/// </summary>
public class CustomSKUInfoProvider : SKUInfoProvider
{
    /// <summary>
    /// Returns catalogue price of the given product based on the SKU data and shopping cart data. 
    /// By default price is loaded from the SKUPrice field.
    /// </summary>
    /// <param name="sku">SKU dta</param>
    /// <param name="cart">Shopping cart data</param>
    protected override double GetSKUPriceInternal(SKUInfo sku, ShoppingCartInfo cart)
    {
        double price = 0;

        // 1) Get base SKU price according to the shopping cart data (current culture)
        if (cart != null)
        {
            switch (cart.ShoppingCartCulture.ToLower())
            {
                case "en-us":
                    // Get price form SKUEnUsPrice field
                    price = ValidationHelper.GetDouble(sku.GetValue("SKUEnUsPrice"), 0);
                    break;

                case "cs-cz":
                    // Get price form SKUCsCzPrice field
                    price = ValidationHelper.GetDouble(sku.GetValue("SKUCsCzPrice"), 0);
                    break;
            }
        }

        //// 2) Get base SKU price according to the product properties (product status)
        //PublicStatusInfo status = PublicStatusInfoProvider.GetPublicStatusInfo(sku.SKUPublicStatusID);
        //if ((status != null) && (status.PublicStatusName.ToLower() == "discounted"))
        //{
        //    // Get price form SKUDiscountedPrice field
        //    price = ValidationHelper.GetDouble(sku.GetValue("SKUDiscountedPrice"), 0);
        //}

        if (price == 0)
        {
            // Get price from the SKUPrice field by default
            return base.GetSKUPriceInternal(sku, cart);
        }
        else
        {
            // Return custom price
            return price;
        }
    }
}
