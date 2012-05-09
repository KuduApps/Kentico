using System;
using System.Collections;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

public partial class CMSDesk_Administration_Administration : CMSAdministrationPage
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Header.Administration");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Administration/module.png");
        this.guide.OnGuideItemCreated += new CMSAdminControls_UI_UIProfiles_UIGuide.GuideItemCreatedEventHandler(guide_OnGuideItemCreated);        
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
        if (!IsAdministrationUIElementAvailable(uiElement))
        {
            return null;
        }
        return defaultItem;
    }
}
