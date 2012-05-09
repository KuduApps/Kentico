using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

// Set page title
[Title("CMSModules/CMS_MyDesk/RecycleBin/module.png", "MyDesk.RecycleBinTitle", "CMS_MyDesk_MyRecycleBin_Documents")]

// Set page tabs
[Tabs("CMS.MyDesk", "MyRecycleBin", "recbin_content")]

public partial class CMSModules_MyDesk_RecycleBin_RecycleBin_Header : CMSDeskPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        CurrentMaster.Tabs.OnTabCreated += Tabs_OnTabCreated;
    }


    protected override void OnPreRender(EventArgs e)
    {
        CurrentMaster.Tabs.SelectedTab = QueryHelper.GetInteger("selectedtab", 0);

        base.OnPreRender(e);
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        // Skip objects tab element if not have proper license
        if ((element.ElementName.ToLower() == "myrecyclebin.objects") && !LicenseKeyInfoProvider.IsFeatureAvailable(FeatureEnum.ObjectVersioning))
        {
            return null;
        }
        return parameters;
    }
}

