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

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Theme : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        int webPartId = QueryHelper.GetInteger("webpartid", 0);
        if (webPartId > 0)
        {
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPartId);
            EditedObject = wpi;

            if (wpi != null)
            {
                // Ensure the theme folder
                themeElem.Path = "~/App_Themes/Components/WebParts/" + ValidationHelper.GetSafeFileName(wpi.WebPartName);
            }
        }
        else
        {
            EditedObject = null;
        }
    }
}
