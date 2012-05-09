using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_Header : SiteManagerPage
{
    protected int moduleId;


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentModule = "";

        if (!string.IsNullOrEmpty(Request.QueryString["moduleId"]))
        {
            moduleId = Convert.ToInt32(Request.QueryString["moduleId"]);
        }

        // Initialize page title
        string modules = GetString("Administration-Module_Edit.Modules");
        ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(moduleId);
        if (ri != null)
        {
            currentModule = ri.ResourceDisplayName;
        }

        string title = GetString("Administration-Module_Edit.Title");
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = modules;
        pageTitleTabs[0, 1] = "~/CMSModules/Modules/Pages/Development/Module_List.aspx";
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentModule;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = title;
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Module/object.png");
        this.CurrentMaster.Title.HelpTopicName = "new_modulegenral_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.PanelSeparator.Visible = true;

        // Initialize menu
        string generalString = GetString("general.general");
        string permissionNamesString = GetString("Administration-Module_Edit.PermissionNames");
        string uiInterfaceString = GetString("Administration-Module_Edit.UserInterface");
        string sitesString = GetString("general.sites");

        string[,] tabs = new string[4, 4];
        tabs[0, 0] = generalString;
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'new_modulegenral_tab');";
        tabs[0, 2] = "Module_Edit_General.aspx?moduleID=" + moduleId;
        tabs[1, 0] = permissionNamesString;
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'permission_names_list');";
        tabs[1, 2] = "Module_Edit_PermissionNames.aspx?moduleID=" + moduleId;
        tabs[2, 0] = uiInterfaceString;
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'module_ui');";
        tabs[2, 2] = "Module_UI_Frameset.aspx?moduleID=" + moduleId;
        tabs[3, 0] = sitesString;
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'sites2');";
        tabs[3, 2] = "Module_Edit_Sites.aspx?moduleID=" + moduleId;

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "content";

        int tabIndex = QueryHelper.GetInteger("tabIndex", 0);
        this.CurrentMaster.Tabs.SelectedTab = tabIndex;
    }
}
