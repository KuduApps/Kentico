using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_MyDesk_MyProfile_MyProfile_Header : CMSMyProfilePage
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this["TabControl"] = tabElem;

        pnlLeft.CssClass = "TabsLeft";
        pnlRight.CssClass = "TabsRight";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("MyDesk.MyProfileTitle");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_MyDesk/MyProfile/module.png");
        titleElem.HelpName = "helpTopic";
        titleElem.HelpTopicName = "my_profile_details";

        // Initialize tabs
        tabElem.OnTabCreated += tabElem_OnTabCreated;
        tabElem.SelectedTab = 0;
        tabElem.UrlTarget = "myProfileContent";
    }


    protected override void OnPreRender(EventArgs e)
    {
        tabElem.SelectedTab = QueryHelper.GetInteger("selectedtab", 0);

        base.OnPreRender(e);
    }


    private static string[] tabElem_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        switch (element.ElementName.ToLower())
        {
            case "myprofile.notifications":
                if (!LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Notifications, ModuleEntry.NOTIFICATIONS) ||
                    !ResourceSiteInfoProvider.IsResourceOnSite(ModuleEntry.NOTIFICATIONS, CMSContext.CurrentSiteName))
                {
                    return null;
                }
                break;

            case "myprofile.categories":
                if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Categories", CMSContext.CurrentSiteName))
                {
                    return null;
                }
                break;
        }

        return parameters;
    }
}
