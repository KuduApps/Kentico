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
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSSiteManager_Sites_Site_Edit : SiteManagerPage
{
    protected int siteId = 0;
    protected string currentWebSite = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.CurrentMaster.Tabs.SelectedTab = QueryHelper.GetInteger("selectedtab", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Site_Edit.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Site/object.png");
        this.CurrentMaster.Tabs.UseClientScript = true;
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "general_tab6";


        siteId = QueryHelper.GetInteger("siteid", 0);
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            currentWebSite = si.DisplayName;
        }

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        this.CurrentMaster.SetRTL();

        InitializeBreadCrumbs();
    }


    /// <summary>
    /// Initializes site edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        if (siteId > 0)
        {
            string[,] tabs = new string[4, 4];
            tabs[0, 0] = GetString("general.general");
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tab6');";
            tabs[0, 2] = "Site_Edit_General.aspx?siteid=" + siteId;
            tabs[1, 0] = GetString("Administration-Site_Edit.DomainAliases");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'domain_aliases_tab');";
            tabs[1, 2] = "Site_Edit_DomainAliases.aspx?siteid=" + siteId;

            int index = 2;
            bool showCulturesTab = LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Multilingual);
            if (showCulturesTab)
            {
                tabs[index, 0] = GetString("Administration-Site_Edit.Cultures");
                tabs[index, 1] = "SetHelpTopic('helpTopic', 'cultures_tab');";
                tabs[index, 2] = "Site_Edit_Cultures.aspx?siteid=" + siteId;
                index++;
            }

            tabs[index, 0] = GetString("Administration-Site_Edit.OfflineMode");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'offline_mode_tab');";
            tabs[index, 2] = "Site_Edit_OfflineMode.aspx?siteid=" + siteId;

            this.CurrentMaster.Tabs.Tabs = tabs;
            this.CurrentMaster.Tabs.UrlTarget = "content";
        }
    }


    /// <summary>
    /// Initializes BreadCrumbs.
    /// </summary>
    protected void InitializeBreadCrumbs()
    {
        // Initialize PageTitle
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("general.sites");
        pageTitleTabs[0, 1] = "~/CMSSiteManager/Sites/site_list.aspx";
        pageTitleTabs[0, 2] = "cmsdesktop";
        pageTitleTabs[1, 0] = currentWebSite;
        pageTitleTabs[1, 1] = string.Empty;
        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }
}
