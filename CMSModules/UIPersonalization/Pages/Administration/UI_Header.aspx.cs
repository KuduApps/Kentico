using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_UIPersonalization_Pages_Administration_UI_Header : CMSUIPersonalizationPage
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
        // Set the master page title
        CurrentMaster.Title.TitleText = GetString("uiprofile.personalization");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_UIElement/object.png");
        CurrentMaster.Title.HelpTopicName = "UIPersonalization_Dialogs";
        CurrentMaster.Title.HelpName = "helpTopic";
        
        // Set the tabs
        string[,] tabs = new string[2, 8];
        tabs[0, 0] = GetString("uiprofile.dialogs");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'UIPersonalization_Dialogs');";
        tabs[0, 2] = "UI_Dialogs.aspx?siteId=" + SiteID;
        tabs[1, 0] = GetString("uiprofile.editor");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'UIPersonalization_Editor');";
        tabs[1, 2] = "UI_Editor.aspx?siteId=" + SiteID;

        CurrentMaster.Tabs.UrlTarget = "uiContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }
}
