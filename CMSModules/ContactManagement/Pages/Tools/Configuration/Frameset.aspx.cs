using System;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Configuration_Frameset : CMSContactManagementConfigurationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        if (this.IsSiteManager)
        {
            configurationContent.Attributes["src"] = AddSiteQuery("AccountStatus/List.aspx", null);
            configurationMenu.Attributes["src"] = AddSiteQuery("Header.aspx", null);
        }
    }
}