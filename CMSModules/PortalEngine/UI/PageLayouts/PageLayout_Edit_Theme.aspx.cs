using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.IO;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_UI_PageLayouts_PageLayout_Edit_Theme : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        int layoutId = QueryHelper.GetInteger("layoutid", 0);
        if (layoutId > 0)
        {
            LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(layoutId);
            EditedObject = li;

            if (li != null)
            {
                // Ensure the theme folder
                themeElem.Path = "~/App_Themes/Components/Layouts/" + ValidationHelper.GetSafeFileName(li.LayoutCodeName);
            }
        }
        else
        {
            EditedObject = null;
        }
    }
}
