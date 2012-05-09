using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_UI_Header : CMSRolesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the master page elements
        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        int roleId = QueryHelper.GetInteger("roleid", 0);
        string urlQuery = String.Empty;
        int siteID = 0;

        if (SelectedSiteID != 0)
        {
            urlQuery = "&selectedsiteid=" + SelectedSiteID;
            siteID = SelectedSiteID;
        }
        else if (SiteID != 0)
        {
            urlQuery = "&siteid=" + SiteID;
            siteID = SiteID;
        }

        // Set the tabs
        string[,] tabs = new string[2, 8];
        tabs[0, 0] = GetString("uiprofile.dialogs");
        tabs[0, 1] = "";
        tabs[0, 2] = "Role_Edit_UI_Dialogs.aspx?siteId=" + siteID + "&roleid=" + roleId;
        tabs[1, 0] = GetString("uiprofile.editor");
        tabs[1, 1] = "";
        tabs[1, 2] = "Role_Edit_UI_Editor.aspx?siteId=" + siteID + "&roleid=" + roleId;

        CurrentMaster.Tabs.UrlTarget = "uiContent";
        CurrentMaster.Tabs.Tabs = tabs;

        // Initialize PageTitle breadcrumbs
        RoleInfo ri = RoleInfoProvider.GetRoleInfo(roleId);
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = ri.DisplayName;
        breadcrumbs[0, 1] = ResolveUrl("Role_Edit_Frameset.aspx") + "?roleid=" + roleId + urlQuery;
        breadcrumbs[0, 2] = SiteID > 0 ? "cmsdeskadmincontent" : "frameMain";
        breadcrumbs[1, 0] = GetString("administration.roles.uipersonalization"); ;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }
}
