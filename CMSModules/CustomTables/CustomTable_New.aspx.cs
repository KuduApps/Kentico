using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_CustomTable_New : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the inner control properties
        newTableWizard.Theme = Theme;

        // Set new custom table wizzard
        newTableWizard.Mode = NewClassWizardModeEnum.CustomTable;
        newTableWizard.SystemDevelopmentMode = false;
        newTableWizard.Step6Description = "customtable_new.description";

        // Initialize master page
        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set master page title
        CurrentMaster.Title.TitleText = GetString("customtable.list.NewCustomTable");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CustomTable/new.png");

        // Set master page HELP control
        CurrentMaster.Title.HelpTopicName = "new_custom_table";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Initialize breadcrumbs element
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("customtable.list.Title");
        pageTitleTabs[0, 1] = "~/CMSModules/CustomTables/CustomTable_List.aspx";
        pageTitleTabs[0, 2] = string.Empty;
        pageTitleTabs[1, 0] = GetString("customtable.list.NewCustomTable");
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }
}
