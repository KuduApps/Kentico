using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;

// Tabs
[Tabs("CMS.OnlineMarketing", "Configuration", "configurationContent")]

public partial class CMSModules_ContactManagement_Pages_Tools_Configuration_Header : CMSContactManagementConfigurationPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ((CMSMasterPage)CurrentMaster).PanelBody.CssClass += " Separator";

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Configuration", null, "menu");
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        UITabs tabs = ((CMSMasterPage)CurrentMaster).Tabs;

        for (int i = 0; i < tabs.Tabs.GetLength(0); i++)
        {
            if (!String.IsNullOrEmpty(tabs.Tabs[i, 2]))
            {
                tabs.Tabs[i, 2] += URLHelper.Url.Query;
            }
        }
    }

    #endregion
}