using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.WorkflowEngine;

/// <summary>
/// Sample class loader module class. Partial class ensures correct registration.
/// </summary>
[SampleClassLoaderModuleLoader]
public partial class CMSModuleLoader
{
    /// <summary>
    /// Module registration
    /// </summary>
    private class SampleClassLoaderModuleLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Initializes the module
        /// </summary>
        public override void Init()
        {
            // -- This line provides the ability to register the classes via web.config cms.extensibility section from App_Code
            ClassHelper.OnGetCustomClass += ClassHelper_OnGetCustomClass;
        }


        /// <summary>
        /// Gets the custom class object based on the given parameters
        /// </summary>
        private void ClassHelper_OnGetCustomClass(object sender, ClassEventArgs e)
        {
            if (e.Object == null)
            {
                // Provide your custom classes
                switch (e.ClassName)
                {
                    // Define the class MyTask implementing ITask and you can provide your scheduled tasks out of App_Code
                    case "Custom.MyTask":
                        e.Object = new Custom.MyTask();
                        break;

                    // Define the class MyCustomIndex implementing ICustomSearchIndex and you can provide your custom search indexes from App_Code
                    case "Custom.MyIndex":
                        e.Object = new Custom.MyIndex();
                        break;

                    // Define the class MyCustomSiteInfoProvider inheriting the SiteInfoProvider and you can customize the provider
                    case "CustomSiteInfoProvider":
                        e.Object = new CustomSiteInfoProvider();
                        break;

                    // Define the class MyCustomCacheHelper inheriting the CacheHelper and you can customize the helper
                    case "CustomCacheHelper":
                        e.Object = new CustomCacheHelper();
                        break;

                    // Define the class MyCustomEmailProvider inheriting the EmailProvider and you can customize the provider
                    case "CustomEmailProvider":
                        e.Object = new CustomEmailProvider();
                        break;
                }
            }
        }
    }
}
