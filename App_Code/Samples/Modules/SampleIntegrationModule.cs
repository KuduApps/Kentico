using CMS.SettingsProvider;

[SampleIntegrationConnectorLoader]
public partial class CMSModuleLoader
{
    public class SampleIntegrationConnectorLoaderAttribute : CMSLoaderAttribute
    {
        /// <summary>
        /// Initializes the module
        /// </summary>
        public override void Init()
        {
            ClassHelper.OnGetCustomClass += GetCustomClass;
        }


        /// <summary>
        /// Gets the custom class object based on the given class name. This handler is called when the assembly name is App_Code.
        /// </summary>
        private static void GetCustomClass(object sender, ClassEventArgs e)
        {
            if (e.Object == null)
            {
                switch (e.ClassName)
                {
                    // Load SampleIntegrationConnector
                    case "SampleIntegrationConnector":
                        e.Object = new SampleIntegrationConnector();
                        break;
                }
            }
        }
    }
}