using System;

using CMS.SettingsProvider;

/// <summary>
/// Ecommerce functions loader (registers ecommerce functions to macro resolver).
/// </summary>
[EcommerceModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Attribute class ensuring correct initialization of methods in macro resolver.
    /// </summary>
    private class EcommerceModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EcommerceModuleLoaderAttribute()
        {
            // Require E-commerce module to load properly
            RequiredModules = new string[] { ModuleEntry.ECOMMERCE };
        }


        /// <summary>
        /// Registers ecommerce methods.
        /// </summary>
        public override void Init()
        {
            EcommerceMethods.RegisterMethods();
        }
    }
}
