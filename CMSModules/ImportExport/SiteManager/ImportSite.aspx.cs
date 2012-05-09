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
using System.Threading;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_ImportExport_SiteManager_ImportSite : CMSImportExportPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Init wizard
        string sites = GetString("general.sites");
        string title = GetString("ImportSite.Title");

        //initializes PageTitle
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = sites;
        pageTitleTabs[0, 1] = "~/CMSSiteManager/Sites/site_list.aspx";
        pageTitleTabs[1, 0] = GetString("ImportSite.ImportSite");
        pageTitleTabs[1, 1] = "";

        titleElem.Breadcrumbs = pageTitleTabs;
        titleElem.TitleText = title;
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Sites/import.png");
    }
}
