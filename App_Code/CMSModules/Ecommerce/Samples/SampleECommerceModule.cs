using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;

/// <summary>
/// Sample e-commerce module class. Partial class ensures correct registration.
/// </summary>
[SampleECommerceModuleLoader]
public partial class CMSModuleLoader
{
    #region "Macro methods loader attribute"

    /// <summary>
    /// Module registration
    /// </summary>
    private class SampleECommerceModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SampleECommerceModuleLoaderAttribute()
        {
            // Require E-commerce module to load properly
            RequiredModules = new string[] { ModuleEntry.ECOMMERCE };
        }


        /// <summary>
        /// Initializes the module
        /// </summary>
        public override void Init()
        {
            // -- Uncomment this line to register the CustomShippingOptionInfoProvider programatically
            //ShippingOptionInfoProvider.ProviderObject = new CustomShippingOptionInfoProvider();

            // -- Uncomment this line to register the CustomShoppingCartInfoProvider programatically
            //ShoppingCartInfoProvider.ProviderObject = new CustomShoppingCartInfoProvider();

            // -- Uncomment this line to register the CustomShoppingCartItemInfoProvider programatically
            //ShoppingCartItemInfoProvider.ProviderObject = new CustomShoppingCartItemInfoProvider();

            // -- Uncomment this line to register the CustomSKUInfoProvider programatically
            //SKUInfoProvider.ProviderObject = new CustomSKUInfoProvider();

            // This line provides the ability to register the classes via web.config cms.extensibility section from App_Code
            ClassHelper.OnGetCustomClass += GetCustomClass;
        }


        /// <summary>
        /// Gets the custom class object based on the given class name. This handler is called when the assembly name is App_Code.
        /// </summary>
        private static void GetCustomClass(object sender, ClassEventArgs e)
        {
            if (e.Object == null)
            {
                // Provide your custom classes
                switch (e.ClassName)
                {
                    // Define the class CustomShippingOptionInfoProvider inheriting the ShippingOptionInfoProvider and you can customize the provider
                    case "CustomShippingOptionInfoProvider":
                        e.Object = new CustomShippingOptionInfoProvider();
                        break;

                    // Define the class CustomShoppingCartInfoProvider inheriting the ShoppingCartInfoProvider and you can customize the provider
                    case "CustomShoppingCartInfoProvider":
                        e.Object = new CustomShoppingCartInfoProvider();
                        break;

                    // Define the class CustomShoppingCartItemInfoProvider inheriting the ShoppingCartItemInfoProvider and you can customize the provider
                    case "CustomShoppingCartItemInfoProvider":
                        e.Object = new CustomShoppingCartItemInfoProvider();
                        break;

                    // Define the class CustomSKUInfoProvider inheriting the SKUInfoProvider and you can customize the provider
                    case "CustomSKUInfoProvider":
                        e.Object = new CustomSKUInfoProvider();
                        break;
                }
            }
        }
    }

    #endregion
}
