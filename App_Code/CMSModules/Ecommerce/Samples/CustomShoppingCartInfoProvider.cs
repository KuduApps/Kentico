using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.Ecommerce;

/// <summary>
/// Sample shopping cart info provider. 
/// Can be registered either by replacing the ShoppingCartInfoProvider.ProviderObject (uncomment the line in SampleECommerceModule.cs) or through cms.extensibility section of the web.config
/// </summary>
public class CustomShoppingCartInfoProvider : ShoppingCartInfoProvider
{
    /// <summary>
    /// Calculates discount which should be applied to the total items price.
    /// </summary>
    /// <param name="cart">Shopping cart</param>        
    protected override double CalculateOrderDiscountInternal(ShoppingCartInfo cart)
    {
        double result = base.CalculateOrderDiscountInternal(cart);

        // Example of order discount based on the time of shopping - Happy hours (4 PM - 7 PM)
        if ((DateTime.Now.Hour >= 16) && (DateTime.Now.Hour <= 19))
        {
            // 20% discount 
            result = result + cart.TotalItemsPriceInMainCurrency * 0.2;
        }
        // Example of order discount based on the total price of all cart items
        else if (cart.TotalItemsPriceInMainCurrency > 500)
        {
            // 10% discount
            result = result + cart.TotalItemsPriceInMainCurrency * 0.1;
        }
        
        return result;
    }
}
