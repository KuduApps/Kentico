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

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Theme : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        int templateId = QueryHelper.GetInteger("templateid", 0);
        if (templateId > 0)
        {
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
            EditedObject = pti;

            if (pti != null)
            {
                // Ensure the theme folder
                themeElem.Path = "~/App_Themes/Components/PageTemplates/" + ValidationHelper.GetSafeFileName(pti.CodeName);
            }
        }
        else
        {
            EditedObject = null;
        }
    }
}
