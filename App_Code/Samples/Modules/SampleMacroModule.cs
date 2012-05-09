using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSOutputFilter;

/// <summary>
/// Sample custom module class. Partial class ensures correct registration. For adding new methods, modify SampleModule inner class.
/// </summary>
[SampleMacroLoader]
public partial class CMSModuleLoader
{
    #region "Macro methods loader attribute"

    /// <summary>
    /// Attribute class ensuring correct initialization of methods in macro resolver. You do not need to modify this class.
    /// </summary>
    private class SampleMacroLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Registers module methods.
        /// </summary>
        public override void Init()
        {
            // -- Custom macro methods
            //CustomMacroMethods.RegisterMethods();

            // -- Custom macro resolving
            //MacroResolver.OnResolveCustomMacro += MacroResolver_OnResolveCustomMacro;

            // -- Custom output substitution resolving
            //OutputFilter.OnResolveSubstitution += OutputFilter_OnResolveSubstitution;
        }


        /// <summary>
        /// Resolves the output substitution
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OutputFilter_OnResolveSubstitution(object sender, CMS.OutputFilter.SubstitutionEventArgs e)
        {
            if (!e.Match)
            {
                // Add your custom macro evaluation
                switch (e.Expression.ToLower())
                {
                    case "somesubstitution":
                        e.Match = true;
                        e.Result = "Resolved substitution";
                        break;
                }
            }
        }


        /// <summary>
        /// Resolves the custom macro
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void MacroResolver_OnResolveCustomMacro(object sender, MacroEventArgs e)
        {
            if (!e.Match)
            {
                // Add your custom macro evaluation
                switch (e.Expression.ToLower())
                {
                    case "someexpression":
                        e.Match = true;
                        e.Result = "Resolved expression";
                        break;
                }
            }
        }
    }

    #endregion
}
