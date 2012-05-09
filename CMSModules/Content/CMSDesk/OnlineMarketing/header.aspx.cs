using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_Content_CMSDesk_OnlineMarketing_header : CMSAnalyticsContentPage
{
    private int selectedTabIndex = 0;
    string selected = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        selected = DataHelper.GetNotEmpty(QueryHelper.GetString("tab", String.Empty).ToLower(), AnalyticsTabCode.ToString(UIContext.AnalyticsTab).ToLower());

        CurrentMaster.Tabs.OnTabCreated += tabElem_OnTabCreated;
        CurrentMaster.Tabs.ModuleName = "CMS.Content";
        CurrentMaster.Tabs.ElementName = "Analytics";
        CurrentMaster.Tabs.UrlTarget = "edit";
        
        // initializes breadcrumbs 		
        string[,] pageTitleTabs = new string[1, 3];
        pageTitleTabs[0, 0] = GetString("content.ui.analytics");
        pageTitleTabs[0, 1] = "";
        pageTitleTabs[0, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.SetRTL();

        CurrentMaster.Title.HelpName = "helpTopic";
        // Must be set be to help icon created
        CurrentMaster.Title.HelpTopicName = "Analytics";

        CMSContext.ViewMode = ViewModeEnum.Analytics;
    }


    protected string[] tabElem_OnTabCreated(UIElementInfo element, string[] parameters, int tabIndex)
    {
        string elementName = element.ElementName.ToLower();
        string siteName = CMSContext.CurrentSiteName;
        if ((elementName == "onlinemarketing.languages") && (!CultureInfoProvider.IsSiteMultilignual(siteName) || !CultureInfoProvider.LicenseVersionCheck()))
        {
            return null;
        }

        if (elementName.StartsWith("onlinemarketing.") && (!ModuleEntry.IsModuleLoaded("cms.onlinemarketing")))
        {
            return null;
        }
        
        if (elementName.StartsWith("onlinemarketing.") && (elementName.Substring("onlinemarketing.".Length) == selected))
        {
            selectedTabIndex = tabIndex;
        }

        return parameters;
    }
}

