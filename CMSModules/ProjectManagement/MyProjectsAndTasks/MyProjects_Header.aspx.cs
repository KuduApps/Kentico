using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ProjectManagement_MyProjectsAndTasks_MyProjects_Header : CMSMyProjectsAndTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("mydesk.ui.myprojects");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_ProjectManagement/module.png");
        CurrentMaster.Title.HelpTopicName = "myprojects_mytasks";
        CurrentMaster.Title.HelpName = "helpTopic";

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }

    #region "Private methods"

    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("myprojects.mytasks");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'myprojects_mytasks');";
        tabs[0, 2] = "MyProjects_TasksAssignedToMe.aspx" + URLHelper.Url.Query;

        tabs[1, 0] = GetString("myprojects.myprojects");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'myprojects_myprojects');";
        tabs[1, 2] = "MyProjects_MyProjects.aspx" + URLHelper.Url.Query;

        tabs[2, 0] = GetString("myprojects.ownedtasks");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'myprojects_ownedtasks');";
        tabs[2, 2] = "MyProjects_TasksOwnedByMe.aspx" + URLHelper.Url.Query;

        CurrentMaster.Tabs.UrlTarget = "projectsContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
