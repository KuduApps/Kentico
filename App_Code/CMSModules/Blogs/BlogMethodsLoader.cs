using System;

using CMS.SettingsProvider;

/// <summary>
/// Blog functions loader (registers blog functions to macro resolver).
/// </summary>
[BlogModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Attribute class ensuring correct initialization of methods in macro resolver.
    /// </summary>
    public class BlogModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Registers blog methods.
        /// </summary>
        public override void Init()
        {
            BlogMethods.RegisterMethods();
        }   
    }
}


