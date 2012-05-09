using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;

/// <summary>
/// Sample Provider module class. Partial class ensures correct registration.
/// </summary>
[SampleProviderModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Module registration
    /// </summary>
    private class SampleProviderModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Initializes the module
        /// </summary>
        public override void Init()
        {
            // -- Uncomment this line to register the provider programatically
            //SiteInfoProvider.ProviderObject = new CustomSiteInfoProvider();
        }
    }
}
