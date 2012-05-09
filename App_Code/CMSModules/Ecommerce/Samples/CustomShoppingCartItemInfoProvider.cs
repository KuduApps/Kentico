using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SiteProvider;

/// <summary>
/// Sample shopping cart item info provider. 
/// Can be registered either by replacing the ShoppingCartItemInfoProvider.ProviderObject (uncomment the line in SampleECommerceModule.cs) or through cms.extensibility section of the web.config
/// </summary>
public class CustomShoppingCartItemInfoProvider : ShoppingCartItemInfoProvider
{
    /// <summary>
    /// Returns list of all discounts which should be applied to the specified shopping cart item.
    /// </summary>
    /// <param name="item">Shopping cart item</param>       
    protected override List<IItemDiscount> GetDiscountsInternal(ShoppingCartItemInfo item)
    {
        // SKU number of product A
        string skuA = "A";

        // Get default discounts
        List<IItemDiscount> discounts =  base.GetDiscountsInternal(item);

        // Add extra discounts to product A 
        if (item.SKUObj.SKUNumber.ToLower() == skuA.ToLower())
        {
            // Add extra discount if product B is also in the cart
            AddDiscountForBundledPurchase(item, discounts, "B");

            // Add another extra discount if user is in VIP role
            AddDiscountForVIPRole(item, discounts, "VIP");
        }
        
        return discounts;
    }


    /// <summary>
    /// Adds custom discount to the given shopping cart item if another product is also in the cart.
    /// </summary>
    /// <param name="item">Shoppping cart item</param>
    /// <param name="discounts">Discounts of the shopping cart item</param>
    /// <param name="anotherItemName">SKU number of another product which must be in the cart to apply the discount</param>
    private void AddDiscountForBundledPurchase(ShoppingCartItemInfo item, List<IItemDiscount> discounts, string anotherItemName)
    {
        // Add discount to the product if product B is also in the cart
        ShoppingCartItemInfo itemB = GetShoppingCartItem(item.ShoppingCartObj, anotherItemName);
        if (itemB != null)
        {
            // Create custom 20% discount
            ItemDiscount discount = new ItemDiscount()
            {
                ItemDiscountID = "DISCOUNT_AB",
                ItemDiscountDisplayName = string.Format("Discount for bundled purchase of product '{0}' and '{1}'", item.SKUObj.SKUName, itemB.SKUObj.SKUName),
                ItemDiscountValue = 20,
                ItemDiscountedUnits = itemB.CartItemUnits
            };

            // Add custom discount to discounts to be applied to the product
            discounts.Add(discount);
        }
    }


    /// <summary>
    /// Adds custom discount to the given shopping cart item if user is member of specified role.
    /// </summary>
    /// <param name="item">Shoppping cart item</param>
    /// <param name="discounts">Discounts of the shopping cart item</param>
    /// <param name="roleName">Name of the role the user must be member of to apply the discount</param>
    private void AddDiscountForVIPRole(ShoppingCartItemInfo item, List<IItemDiscount> discounts, string roleName)
    {                
        UserInfo user = item.ShoppingCartObj.UserInfoObj;

        // Add discount to the product if user is member of the role
        if ((user != null) && user.IsInRole(roleName, item.ShoppingCartObj.SiteName))
        {
            // Create custom 10% discount
            ItemDiscount discount = new ItemDiscount()
            {
                ItemDiscountID = "DISCOUNT_VIP",
                ItemDiscountDisplayName = string.Format("Discount for VIP customers", item.SKUObj.SKUName),
                ItemDiscountValue = 10,
                ItemDiscountedUnits = item.CartItemUnits
            };

            // Add custom discount to discounts to be applied to the product
            discounts.Add(discount);
        }
    }


    /// <summary>
    /// Returns shopping cart item with specified SKU number.
    /// </summary>
    /// <param name="cart">Shopping cart</param>
    /// <param name="skuNumber">Shopping cart item SKU number</param>    
    private ShoppingCartItemInfo GetShoppingCartItem(ShoppingCartInfo cart, string skuNumber)
    {
        // Cart not found
        if (cart == null)
        {
            return null;
        }

        skuNumber = skuNumber.ToLower();

        // Try to find item with specifid SKU number
        foreach (ShoppingCartItemInfo item in cart.CartItems)
        {
            if (item.SKUObj.SKUNumber.ToLower() == skuNumber)
            {
                // Item found
                return item;
            }
        }

        // Item not found
        return null;
    }
}
