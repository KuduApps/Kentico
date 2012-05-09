using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_Header : CMSEcommerceStoreSettingsPage
{
    private UserInfo currentUser = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        currentUser = CMSContext.CurrentUser;

        // Initializes page title
        master.Title.TitleText = GetString("Store_Settings.HeaderCaption");
        master.Title.TitleImage = GetImageUrl("CMSModules/CMS_Ecommerce/storesettings.png");
        master.Title.HelpTopicName = "genral_tab";
        master.Title.HelpName = "helpTopic";

        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Configuration.Settings";
        master.Tabs.UrlTarget = "storesettingsContent";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        // Global objects tab will be displayed only to global admin
        if (!currentUser.IsGlobalAdministrator && (element.ElementName.ToLower() == "configuration.settings.globalobjects"))
        {
            return null;
        }

        // Add SiteId parameter to each tab
        if (parameters.Length > 2)
        {
            parameters[2] = URLHelper.AddParameterToUrl(parameters[2], "siteId", this.SiteID.ToString());
        }

        return parameters;
    }
}
