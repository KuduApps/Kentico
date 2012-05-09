using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_UICultures_Pages_Development_UICulture_Header : SiteManagerPage
{
    #region "Variables"

    protected int uiCultureID;


    protected string currentUICultureName = string.Empty;


    protected UICultureInfo uici;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        uiCultureID = QueryHelper.GetInteger("UIcultureID", 0);

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        EditedObject = uici = UICultureInfoProvider.GetSafeUICulture(uiCultureID);

        if (uici != null)
        {
            currentUICultureName = uici.UICultureName;
        }

        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[2, 4];

        tabs[0, 0] = GetString("UICulture.Strings");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'strings_tab');";
        tabs[0, 2] = "../ResourceString/List.aspx?uicultureid=" + uiCultureID;

        tabs[1, 0] = GetString("General.General");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'new_culturegeneral_tab');";
        tabs[1, 2] = "Tab_General.aspx?uicultureid=" + uiCultureID;

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "content";
    }


    /// <summary>
    /// Initializes MasterPage.
    /// </summary>
    protected void InitializeMasterPage()
    {
        // Initializes breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("UICulture_Edit.UICultures");
        breadcrumbs[0, 1] = ResolveUrl("List.aspx");
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 0] = currentUICultureName;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Initializes help icon
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "strings_tab";
    }

    #endregion
}