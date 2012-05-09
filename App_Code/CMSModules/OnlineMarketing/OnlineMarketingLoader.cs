using System;

using CMS.SettingsProvider;

/// <summary>
/// Online marketing functions loader (registers online marketing functions to macro resolver).
/// </summary>
[OnlineMarketingModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Attribute class ensuring correct initialization of methods in macro resolver.
    /// </summary>
    private class OnlineMarketingModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OnlineMarketingModuleLoaderAttribute()
        {
            // Require Online marketing module to load properly
            RequiredModules = new string[] { ModuleEntry.ONLINEMARKETING };
        }


        /// <summary>
        /// Registers online marketing methods.
        /// </summary>
        public override void Init()
        {
            OnlineMarketingMethods.RegisterMethods();
        }
    }
}