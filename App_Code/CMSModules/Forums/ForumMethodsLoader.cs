using System;

using CMS.SettingsProvider;

/// <summary>
/// Forum functions loader (registers forum functions to macro resolver).
/// </summary>
[ForumModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Attribute class ensuring correct initialization of methods in macro resolver.
    /// </summary>
    private class ForumModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Registers forum methods.
        /// </summary>
        public override void Init()
        {
            ForumMethods.RegisterMethods();
        }
    }
}
