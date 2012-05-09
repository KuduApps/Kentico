using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Ecommerce;
using CMS.CMSHelper;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Configuration_Header : CMSEcommerceSharedConfigurationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;
        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Configuration";
        master.Tabs.UrlTarget = "configEdit";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
        master.PanelBody.CssClass += " Separator";
        master.SetRTL();

        AddMenuButtonSelectScript("Configuration", "");

        ScriptHelper.RegisterTitleScript(this, GetString("ecommerce.configuration"));
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        int siteId = this.SiteID;

        // Skip some elements if editing global configuration
        if (siteId == 0)
        {
            switch (element.ElementName.ToLower())
            {
                case "configuration.departments":
                case "configuration.shippingoptions":
                case "configuration.paymentmethods":
                    return null;
            }
        }

        // Add SiteId parameter to each tab
        if ((parameters.Length > 2) && (siteId != CMSContext.CurrentSiteID))
        {
            parameters[2] = URLHelper.AddParameterToUrl(parameters[2], "SiteId", siteId.ToString());
        }

        return parameters;
    }
}
