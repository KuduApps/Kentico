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

public partial class CMSModules_ImportExport_SiteManager_ExportSite : CMSImportExportPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set hyperlink on export history properties
        HyperlinkExportHistory.Text = GetString("SiteManager.ExportSettings.ExportHistoryLinkTitle");
        HyperlinkExportHistory.NavigateUrl = "~/CMSModules/ImportExport/SiteManager/ExportHistory/ExportHistory_Edit.aspx?siteid=" + ValidationHelper.GetInteger(Request.QueryString["siteid"], 0);
        ImageExportHistory.ImageUrl = GetImageUrl("Objects/Export_History/list.png");

        int siteId = 0;

        // Get site id from query string
        if ((Request.QueryString["siteid"] != null) && (Request.QueryString["siteid"] != ""))
        {
            siteId = Convert.ToInt32(Request.QueryString["siteid"]);
        }
        else
        {
            if (CMSContext.CurrentSite == null)
            {
                throw new Exception("[ExportSite]: There is currently no site to be exported!");
            }

            siteId = CMSContext.CurrentSite.SiteID;
        }

        // Init wizard
        this.wzdExport.SiteId = siteId;

        string sites = GetString("general.sites");
        string title = GetString("ExportSite.Title");

        //initializes PageTitle
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = sites;
        pageTitleTabs[0, 1] = "~/CMSSiteManager/Sites/site_list.aspx";
        pageTitleTabs[1, 0] = GetString("ExportSite.ExportSite");
        pageTitleTabs[1, 1] = "";
        
        ptExport.Breadcrumbs = pageTitleTabs;
        ptExport.TitleText = title;
        ptExport.TitleImage = GetImageUrl("CMSModules/CMS_Sites/export.png");
    }
}
