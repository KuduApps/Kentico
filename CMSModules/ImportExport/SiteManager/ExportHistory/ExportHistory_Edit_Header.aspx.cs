using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ImportExport_SiteManager_ExportHistory_ExportHistory_Edit_Header : CMSImportExportPage
{
    private bool hideBreadcrumbs = false;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        hideBreadcrumbs = ValidationHelper.GetBoolean(Request.QueryString["hidebreadcrumbs"], false);

        CurrentMaster.PanelTitle.CssClass = hideBreadcrumbs ? "PageTitle" : "TabsPageTitle";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int siteId = ValidationHelper.GetInteger(Request.QueryString["siteid"], 0);

        if (!hideBreadcrumbs)
        {
            // initializes page title control		
            string[,] pageTitleTabs = new string[3, 3];

            pageTitleTabs[0, 0] = GetString("general.sites");
            pageTitleTabs[0, 1] = "~/CMSSiteManager/Sites/site_list.aspx";
            pageTitleTabs[0, 2] = "_parent";

            // Look from which url user comes
            if (siteId > 0)
            // Comes from site export
            {
                pageTitleTabs[1, 0] = GetString("ExportHistory.ExportSiteLink");
                pageTitleTabs[1, 1] = "~/CMSModules/Importexport/SiteManager/ExportSite.aspx?siteid=" + siteId;
                pageTitleTabs[1, 2] = "_parent";
            }
            // Comes from object export
            else
            {
                pageTitleTabs[1, 0] = GetString("ExportHistory.ExportObjectsLink");
                pageTitleTabs[1, 1] = "~/CMSModules/Importexport/SiteManager/ExportObjects.aspx";
                pageTitleTabs[1, 2] = "_parent";
            }

            pageTitleTabs[2, 0] = GetString("ExportHistory.ExportHistoryBreadcrumbs");
            pageTitleTabs[2, 1] = "";
            pageTitleTabs[2, 2] = "";
            CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        }
        CurrentMaster.Title.TitleText = GetString("ExportHistory.HeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Sites/exportobjects24.png");

        string[,] tabs = new string[3, 4];
        tabs[0, 0] = GetString("ExportHistory.History");
        tabs[0, 2] = "ExportHistory_Edit_History.aspx?siteid=" + siteId;
        tabs[1, 0] = GetString("ExportHistory.Tasks");
        tabs[1, 2] = "ExportHistory_Edit_Tasks.aspx?siteid=" + siteId;

        CurrentMaster.Tabs.UrlTarget = "ExportHistoryContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }
}
