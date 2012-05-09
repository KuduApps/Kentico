using System;
using System.Collections;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.SiteProvider;

public partial class CMSSiteManager_Development_development : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Header.Development");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Development/module.png");

        ArrayList parametersRow = new ArrayList();

        ArrayList al = FillCollectionWithModules(DevelopmentItems, "ShowInDevelopment = 1", true, true);

        // Display description overview for default development items
        foreach (object[] itemProperties in al)
        {
            FeatureEnum feature = (FeatureEnum)itemProperties[5];
            bool showItem = (feature == FeatureEnum.Unknown) ? true : LicenseHelper.IsFeatureAvailableInUI(feature);
            if (showItem)
            {
                object[] row = new object[5];

                // Start filling arraylist
                row[0] = itemProperties[4].ToString(); // Image
                row[1] = GetString(itemProperties[1].ToString()); // Name
                row[2] = itemProperties[0].ToString(); // Link
                row[3] = GetString(itemProperties[2].ToString()); // Description
                row[4] = ValidationHelper.GetCodeName(row[1]);
                parametersRow.Add(row);
            }
        }

        // Initialize guide
        Guide.Columns = 3;
        Guide.Parameters = parametersRow;
    }
}
