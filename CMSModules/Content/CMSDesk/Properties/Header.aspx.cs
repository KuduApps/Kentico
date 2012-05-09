using System;
using System.Web;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

public partial class CMSModules_Content_CMSDesk_Properties_Header : CMSPropertiesPage
{
    #region "Variables"

    private string selected = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        selected = DataHelper.GetNotEmpty(QueryHelper.GetString("tab", String.Empty).ToLower(), PropertyTabCode.ToString(UIContext.PropertyTab).ToLower());

        CurrentMaster.Tabs.OnTabCreated += tabElem_OnTabCreated;
        CurrentMaster.Tabs.ModuleName = "CMS.Content";
        CurrentMaster.Tabs.ElementName = "Properties";
        CurrentMaster.Tabs.UrlTarget = "propedit";
        CurrentMaster.SetRTL();
    }


    protected string[] tabElem_OnTabCreated(UIElementInfo element, string[] parameters, int tabIndex)
    {
        bool splitViewSupported = true;
        string elementName = element.ElementName.ToLower();
        switch (elementName)
        {
            case "properties.languages":
                splitViewSupported = false;
                if (!CultureInfoProvider.IsSiteMultilignual(CMSContext.CurrentSiteName) || !CultureInfoProvider.LicenseVersionCheck())
                {
                    return null;
                }
                break;

            case "properties.security":
            case "properties.relateddocs":
            case "properties.linkeddocs":
                splitViewSupported = false;
                break;

            case "properties.variants":
                // Check license
                if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
                {
                    if (!LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.ContentPersonalization, ModuleEntry.ONLINEMARKETING)
                         || !ResourceSiteInfoProvider.IsResourceOnSite("CMS.ContentPersonalization", CMSContext.CurrentSiteName))
                    {
                        return null;
                    }
                }
                break;
        }

        // Ensure tab preselection
        if (elementName.StartsWith("properties.") && (elementName.Substring("properties.".Length) == selected))
        {
            CurrentMaster.Tabs.SelectedTab = tabIndex;
        }

        // Ensure split view mode
        if (splitViewSupported && CMSContext.DisplaySplitMode)
        {
            parameters[2] = GetSplitViewUrl(parameters[2]);
        }

        return parameters;
    }
}
