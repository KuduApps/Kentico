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


using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ImportExport_SiteManager_ExportObjects : CMSImportExportPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HyperlinkExportHistory.Text = GetString("SiteManager.ExportSettings.ExportHistoryLinkTitle");
        HyperlinkExportHistory.NavigateUrl = "~/CMSModules/ImportExport/SiteManager/ExportHistory/ExportHistory_Edit.aspx?siteid=" + ValidationHelper.GetInteger(Request.QueryString["siteid"], 0);
        ImageExportHistory.ImageUrl = GetImageUrl("Objects/Export_History/list.png");

        //initialize PageTitle
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("general.sites");
        pageTitleTabs[0, 1] = "~/CMSSiteManager/Sites/site_list.aspx";
        pageTitleTabs[0, 2] = "cmsdesktop";
        pageTitleTabs[1, 0] = GetString("ExportSettings.ExportSiteSetings");
        pageTitleTabs[1, 1] = "";
        
        ptExportSiteSettings.Breadcrumbs = pageTitleTabs;
        ptExportSiteSettings.TitleText = GetString("ExportSettings.ExportSiteSetings");
        ptExportSiteSettings.TitleImage = GetImageUrl("CMSModules/CMS_Sites/exportobjects24.png");
    }
}
