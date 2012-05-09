using System;

using CMS.SettingsProvider;

/// <summary>
/// MediaLibrary functions loader (registers media library functions to macro resolver).
/// </summary>
[MediaLibraryModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Attribute class ensuring correct initialization of methods in macro resolver.
    /// </summary>
    public class MediaLibraryModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Registers media library methods.
        /// </summary>
        public override void Init()
        {
            MediaLibraryMethods.RegisterMethods();
        }
    }
}
