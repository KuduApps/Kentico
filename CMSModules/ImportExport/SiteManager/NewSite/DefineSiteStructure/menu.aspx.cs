using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_menu : SiteManagerPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {        
        ScriptHelper.RegisterScriptFile(this.Page, @"~/CMSModules/ImportExport/SiteManager/NewSite/DefineSiteStructure/menu.js");
        ScriptHelper.RegisterDialogScript(this);

        // Initialize content management menu
        menuLeft.Groups = new string[,] { { GetString("ContentMenu.ContentManagement"), "~/CMSAdminControls/UI/UniMenu/NewSite/NewSiteMenu.ascx", null } };
    }

    #endregion
}