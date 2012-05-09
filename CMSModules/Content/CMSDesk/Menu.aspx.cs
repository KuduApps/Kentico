using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Content_CMSDesk_Menu : CMSContentPage
{
    #region "Protected variables"

    protected int selectedNodeId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ManagersContainer = this.scriptManager;

        // Register the dialog script
        ScriptHelper.RegisterScriptFile(this, @"~/CMSModules/Content/CMSDesk/Menu.js");
        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ContentMenuScripts", ScriptHelper.GetScript("var contentDir = '" + ResolveUrl("~/CMSModules/Content/CMSDesk/") + "';"));

        ltlData.Text += string.Format("<input type=\"hidden\" id=\"imagesUrl\" value=\"{0}\" />", 
            GetImageUrl("CMSModules/CMS_Content/Menu/", false, true));

        // Check (and ensure) the proper content culture
        CheckPreferredCulture(true);

        // Setup selected node ID
        selectedNodeId = QueryHelper.GetInteger("nodeid", 0);

        // Initialize menu
        DataSet siteCulturesDS = CultureInfoProvider.GetSiteCultures(CMSContext.CurrentSiteName);
        // Do not display language menu
        if (DataHelper.DataSourceIsEmpty(siteCulturesDS) || (siteCulturesDS.Tables[0].Rows.Count <= 1))
        {
            contentMenu.Groups = new string[,] { { GetString("ContentMenu.ContentManagement"), "~/CMSAdminControls/UI/UniMenu/Content/ContentMenu.ascx", "ContentMenuGroup" }, { GetString("ContentMenu.ViewMode"), "~/CMSAdminControls/UI/UniMenu/Content/ModeMenu.ascx", "ContentMenuGroup" }, { GetString("contentmenu.other"), "~/CMSAdminControls/UI/UniMenu/Content/OtherMenu.ascx", "ContentMenuGroup" } };
        }
        else
        {
            contentMenu.Groups = new string[,] { { GetString("ContentMenu.ContentManagement"), "~/CMSAdminControls/UI/UniMenu/Content/ContentMenu.ascx", "ContentMenuGroup" }, { GetString("ContentMenu.ViewMode"), "~/CMSAdminControls/UI/UniMenu/Content/ModeMenu.ascx", "ContentMenuGroup" }, { GetString("contentmenu.language"), "~/CMSModules/Content/Controls/LanguageMenu.ascx", "ContentMenuGroup" }, { GetString("contentmenu.other"), "~/CMSAdminControls/UI/UniMenu/Content/OtherMenu.ascx", "ContentMenuGroup" } };
        }

        ScriptHelper.RegisterTitleScript(this, GetString("cmsdesk.ui.content"));
    }

    #endregion


    #region "Overidden methods"

    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }

    #endregion
}