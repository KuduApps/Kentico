using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_Modules_Pages_Development_Module_UI_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query parameters
        int moduleId = QueryHelper.GetInteger("moduleid", 0);
        int elementId = QueryHelper.GetInteger("elementid", 0);
        int parentId = QueryHelper.GetInteger("parentid", 0);

        string currentElement = "";

        // Get current element display name
        UIElementInfo elemInfo = UIElementInfoProvider.GetUIElementInfo(elementId);
        if (elemInfo != null)
        {
            currentElement = elemInfo.ElementDisplayName;
        }

        // Setup breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("resource.ui.element");
        pageTitleTabs[0, 1] = "~/CMSModules/Modules/Pages/Development/Module_UI_Frameset.aspx?moduleid=" + moduleId;
        pageTitleTabs[0, 2] = "content";
        pageTitleTabs[1, 0] = ResHelper.LocalizeString(currentElement);
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        // Setup Tabs if no creating and no parent element
        if (parentId > 0)
        {
            string[,] tabs = new string[2, 4];
            tabs[0, 0] = GetString("general.general");
            tabs[0, 1] = "SaveTab(0);SetHelpTopic('helpTopic', 'resource_ui_general');";
            tabs[0, 2] = "Module_UI_General.aspx?moduleID=" + moduleId + "&elementId=" + elementId + "&parentId=" + parentId;
            tabs[1, 0] = GetString("general.roles");
            tabs[1, 1] = "SaveTab(1);SetHelpTopic('helpTopic', 'resource_ui_roles');";
            tabs[1, 2] = "Module_UI_Roles.aspx?moduleID=" + moduleId + "&elementId=" + elementId + "&parentId=" + parentId;

            this.CurrentMaster.Tabs.Tabs = tabs;
            this.CurrentMaster.Tabs.UrlTarget = "editcontent";

            int selectedTab = QueryHelper.GetInteger("tabindex", 0);
            this.CurrentMaster.Tabs.SelectedTab = selectedTab;

            if (selectedTab == 0)
            {
                this.CurrentMaster.Title.HelpTopicName = "resource_ui_general";
            }
            else
            {
                this.CurrentMaster.Title.HelpTopicName = "resource_ui_roles";
            }
            this.CurrentMaster.Title.HelpName = "helpTopic";

            this.CurrentMaster.HeadElements.Visible = true;
            this.CurrentMaster.HeadElements.Text = ScriptHelper.GetScript("function SaveTab(tabIndex){\n if ((window.parent != null) && (window.parent.parent != null) && (window.parent.parent.frames['tree'])){\n window.parent.parent.frames['tree'].setTab(tabIndex);\n}}");
        }
    }
}
