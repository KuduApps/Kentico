using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_Groups_Tools_ProjectManagement_Project_Header : CMSGroupProjectManagementPage
{
    #region "Methods" 

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        int projectId = QueryHelper.GetInteger("projectid", 0);
        int groupId = QueryHelper.GetInteger("groupid", 0);
        int selectedTab = QueryHelper.GetInteger("tab", 0);

        // Prepare the tabs
        string[,] tabs = new string[3, 4];

        tabs[0, 0] = GetString("pm.projecttask");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'pm_project_tasks');";
        tabs[0, 2] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/ProjectTask/List.aspx?projectid=" + projectId + "&groupid=" + groupId);

        tabs[1, 0] = GetString("general.general");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'pm_project_edit');";
        tabs[1, 2] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/Project/Edit.aspx?projectid=" + projectId + "&groupid=" + groupId);

        tabs[2, 0] = GetString("general.security");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'pm_project_security');";
        tabs[2, 2] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/Project/Security.aspx?projectid=" + projectId + "&groupid=" + groupId);

        // Set the tabs
        ICMSMasterPage master = this.CurrentMaster;
        master.Tabs.Tabs = tabs;
        master.Tabs.SelectedTab = selectedTab;
        master.Tabs.UrlTarget = "content";

        ProjectInfo projectObj = ProjectInfoProvider.GetProjectInfo(projectId);
        string projectName = "";
        if (projectObj != null)
        {
            projectName = projectObj.ProjectDisplayName;
        }

        // Set the title
        PageTitle title = this.CurrentMaster.Title;
        title.HelpName = "helpTopic";
        title.HelpTopicName = "pm_project_tasks";

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("pm.project.list");
        breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/Project/List.aspx?groupid=" + groupId);
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(projectName);

        // Set the title
        title.Breadcrumbs = breadcrumbs;

    }

    #endregion
}