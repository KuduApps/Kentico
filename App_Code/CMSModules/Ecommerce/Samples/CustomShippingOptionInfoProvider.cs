using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.SettingsProvider;

/// <summary>
/// Sample shipping option info provider. 
/// Can be registered either by replacing the ShippingOptionInfoProvider.ProviderObject (uncomment the line in SampleECommerceModule.cs) or through cms.extensibility section of the web.config
/// </summary>
public class CustomShippingOptionInfoProvider : ShippingOptionInfoProvider
{
    /// <summary>
    /// Calculates shipping charge for the given shopping cart.
    /// Shipping taxes are not included. Result is in site main currency.
    /// </summary>
    /// <param name="cartObj">Shopping cart data</param>
    protected override double CalculateShippingInternal(ShoppingCartInfo cart)
    {
        // Calculates shipping based on customer's billing address country
        if (cart != null)
        {
            // Get site name
            string siteName = cart.SiteName;

            if ((cart.UserInfoObj != null) && (cart.UserInfoObj.IsInRole("VIP", siteName)))
            {
                // Free shipping for VIP customers
                return 0;
            }
            else
            {
                // Get shipping address details
                AddressInfo address = AddressInfoProvider.GetAddressInfo(cart.ShoppingCartShippingAddressID);
                if (address != null)
                {
                    // Get shipping address country
                    CountryInfo country = CountryInfoProvider.GetCountryInfo(address.AddressCountryID);
                    if ((country != null) && (country.CountryName.ToLower() != "usa"))
                    {
                        // Get extra shipping for non-usa customers from 'ShippingExtraCharge' custom setting 
                        double extraCharge = SettingsKeyProvider.GetDoubleValue("ShippingExtraCharge");
                        
                        // Add an extra charge to standard shipping price for non-usa customers
                        return base.CalculateShippingInternal(cart) + extraCharge;
                    }
                }
            }
        }

        // Calculate shipping option without tax in default way
        return base.CalculateShippingInternal(cart);
    }
}
