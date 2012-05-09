using System;

using CMS.SettingsProvider;
using CMS.Controls;

/// <summary>
/// Module loader class (registres methods for macro resolver).
/// </summary>
public partial class CMSModuleLoader
{
    public CMSModuleLoader()
    {
        // Get the attributes
        Type type = typeof(CMSModuleLoader);
        object[] attributes = type.GetCustomAttributes(typeof(CMSLoaderAttribute), true);

        bool found = ((attributes != null) && (attributes.Length > 0));
        if (found)
        {
            // Process all attributes
            foreach (CMSLoaderAttribute attribute in attributes)
            {
                if (attribute.CheckModules())
                {
                    // Init the module if all required modules are installed
                    attribute.Init();
                }
            }
        }
    }


    /// <summary>
    /// Registers transformation methods.
    /// </summary>
    public void RegisterTransformationMethods()
    {
        TransformationMacroMethods.RegisterMethods();
    }
}

