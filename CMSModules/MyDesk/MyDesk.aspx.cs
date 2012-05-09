using System;
using System.Collections;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SiteProvider;

public partial class CMSModules_MyDesk_MyDesk : CMSMyDeskPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize page
        this.CurrentMaster.Title.TitleText = GetString("Header.MyDesk");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_MyDesk/module.png");

        guide.OnGuideItemCreated += new CMSAdminControls_UI_UIProfiles_UIGuide.GuideItemCreatedEventHandler(guide_OnGuideItemCreated);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // No modules allowed
        if (this.guide.GuideEmpty)
        {
            RedirectToUINotAvailable();
        }
    }


    object[] guide_OnGuideItemCreated(UIElementInfo uiElement, object[] defaultItem)
    {
        if (!IsMyDeskUIElementAvailable(uiElement.ElementName))
        {
            return null;
        }
        return defaultItem;
    }
}
