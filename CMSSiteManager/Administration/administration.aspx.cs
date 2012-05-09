using System;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Administration_administration : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize page
        this.CurrentMaster.Title.TitleText = GetString("Header.Administration");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Administration/module.png");

        this.guide.OnGuideItemCreated += new CMSAdminControls_UI_UIProfiles_UIGuide.GuideItemCreatedEventHandler(guide_OnGuideItemCreated);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hardcoded modules
        object[] row;

        row = new object[5];
        row[0] = GetImageUrl("Objects/CMS_Avatar/object.png");
        row[1] = GetString("Administration-LeftMenu.Avatars");
        row[2] = ResolveUrl("~/CMSModules/Avatars/Avatar_List.aspx");
        row[3] = GetString("cms.avatar.description");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("Objects/CMS_Badge/object.png");
        row[1] = GetString("Administration-LeftMenu.Badges");
        row[2] = ResolveUrl("~/CMSModules/Badges/Badges_List.aspx");
        row[3] = GetString("badges.description");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("Objects/Badwords_Word/object.png");
        row[1] = GetString("Administration-LeftMenu.BadWords");
        row[2] = ResolveUrl("~/CMSModules/BadWords/BadWords_List.aspx");
        row[3] = GetString("cms.badwords.Description");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("CMSModules/CMS_EmailQueue/module.png");
        row[1] = GetString("Administration-LeftMenu.EmailQueue");
        row[2] = ResolveUrl("~/CMSModules/EmailQueue/EmailQueue_Frameset.aspx");
        row[3] = GetString("emailqueue.description");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("CMSModules/CMS_RecycleBin/module.png");
        row[1] = GetString("Administration-LeftMenu.RecycleBin");
        row[2] = ResolveUrl("~/CMSModules/RecycleBin/Pages/default.aspx");
        row[3] = GetString("Administration-LeftMenu.RecycleBinDescription");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        row = new object[5];
        row[0] = GetImageUrl("Objects/CMS_SearchIndex/object.png");
        row[1] = GetString("srch.index.title");
        row[2] = ResolveUrl("~/CMSModules/SmartSearch/SearchIndex_List.aspx");
        row[3] = GetString("Administration-LeftMenu.SmartSearchDescription");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        guide.GuideParameters.Add(new object[] {
            GetImageUrl("Objects/CMS_SMTPServer/object.png"),
            GetString("Administration-LeftMenu.SMTPServers"),
            ResolveUrl("~/CMSModules/SMTPServers/Pages/Administration/List.aspx"),
            GetString("Administration-LeftMenu.SMTPServersDescription"),
            ValidationHelper.GetCodeName(GetString("Administration-LeftMenu.SMTPServers"))
        });

        row = new object[5];
        row[0] = GetImageUrl("CMSModules/CMS_System/module.png");
        row[1] = GetString("Administration-LeftMenu.System");
        row[2] = ResolveUrl("~/CMSModules/System/System_Frameset.aspx");
        row[3] = GetString("Administration-LeftMenu.SystemDescription");
        row[4] = ValidationHelper.GetCodeName(row[1]);
        this.guide.GuideParameters.Add(row);

        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Webfarm))
        {
            row = new object[5];
            row[0] = GetImageUrl("Objects/CMS_WebFarmServer/object.png");
            row[1] = GetString("Administration-LeftMenu.WebFarm");
            row[2] = ResolveUrl("~/CMSModules/WebFarm/Pages/WebFarm_Frameset.aspx");
            row[3] = GetString("Administration-LeftMenu.WebFarmDescription");
            row[4] = ValidationHelper.GetCodeName(row[1]);
            this.guide.GuideParameters.Add(row);
        }

        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.IntegrationBus))
        {
            row = new object[5];
            row[0] = GetImageUrl("CMSModules/CMS_Integration/module.png");
            row[1] = GetString("integration.integration");
            row[2] = ResolveUrl("~/CMSModules/Integration/Pages/Administration/Frameset.aspx");
            row[3] = GetString("integration.description");
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
