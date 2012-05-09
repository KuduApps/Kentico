using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;

/// <summary>
/// Sample cache module class. Partial class ensures correct registration.
/// </summary>
[SampleHelperModuleLoader]
public partial class CMSModuleLoader
{
    #region "Macro methods loader attribute"

    /// <summary>
    /// Module registration
    /// </summary>
    private class SampleHelperModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Initializes the module
        /// </summary>
        public override void Init()
        {
            // -- Uncomment this line to register the helper programatically
            //CacheHelper.HelperObject = new CustomCacheHelper();
        }
    }

    #endregion
}
