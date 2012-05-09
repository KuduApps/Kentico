using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Tools_tools : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize page
        this.CurrentMaster.Title.TitleText = GetString("Header.Tools");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Tools/module.png");

        this.guide.OnGuideItemCreated += new CMSAdminControls_UI_UIProfiles_UIGuide.GuideItemCreatedEventHandler(guide_OnGuideItemCreated);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hardcoded modules
        object[] row;

        this.guide.InitEmptyParameters();

        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Ecommerce, ModuleEntry.ECOMMERCE))
        {
            row = new object[5];
            row[0] = GetImageUrl("CMSModules/CMS_Ecommerce/module.png");
            row[1] = GetString("Administration-LeftMenu.Ecommerce");
            row[2] = ResolveUrl("~/CMSModules/Ecommerce/Pages/SiteManager/Configuration_Frameset.aspx?siteId=0");
            row[3] = GetString("cms.ecommerce.description");
            row[4] = ValidationHelper.GetCodeName(row[1]);
            this.guide.GuideParameters.Add(row);
        }

        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.ContactManagement, ModuleEntry.ONLINEMARKETING))
        {
            row = new object[5];
            row[0] = GetImageUrl("Objects/OM_ContactManagement/object.png");
            row[1] = GetString("om.contactmanagement");
            row[2] = ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/Frameset.aspx?isSiteManager=1");
            row[3] = GetString("om.contactmanagement.description");
            row[4] = ValidationHelper.GetCodeName(row[1]);
            this.guide.GuideParameters.Add(row);
        }

        // Sort the collection
        ObjectArrayComparer comparer = new ObjectArrayComparer();
        this.guide.GuideParameters.Sort(comparer);
    }


    object[] guide_OnGuideItemCreated(UIElementInfo uiElement, object[] defaultItem)
    {
        if (!CMSAdministrationPage.IsAdministrationUIElementAvailable(uiElement))
        {
            return null;
        }

        // Ensure default icon
        string iconUrl = GetImageUrl(ValidationHelper.GetString(defaultItem[0], ""));
        if (!ValidationHelper.IsURL(iconUrl) && !FileHelper.FileExists(iconUrl))
        {
            iconUrl = UIHelper.GetImageUrl(this.Page, "/Images/CMSModules/module.png");
        }

        // Set correct JS selection parameter
        defaultItem[4] = ValidationHelper.GetCodeName(defaultItem[1]);

        // Remove siteid parameter from URL
        defaultItem[2] = URLHelper.RemoveParameterFromUrl(defaultItem[2].ToString(), "siteid");

        return defaultItem;
    }
}
