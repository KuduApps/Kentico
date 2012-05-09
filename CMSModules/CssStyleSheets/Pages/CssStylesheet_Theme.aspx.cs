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

public partial class CMSModules_CssStylesheets_Pages_CssStylesheet_Theme : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        int cssStylesheetId = QueryHelper.GetInteger("cssstylesheetid", 0);
        if (cssStylesheetId > 0)
        {
            CssStylesheetInfo csi = CssStylesheetInfoProvider.GetCssStylesheetInfo(cssStylesheetId);
            EditedObject = csi;

            if (csi != null)
            {
                // Ensure the theme folder
                themeElem.Path = "~/App_Themes/" + ValidationHelper.GetSafeFileName(csi.StylesheetName);
            }
        }
        else
        {
            EditedObject = null;
        }
    }
}
