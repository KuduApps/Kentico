using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_MyProjectsAndTasks_MyProjects_MyProjects : CMSMyProjectsAndTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucProjectList.Grid.PageSize = "25";
        ucProjectList.Grid.FilterLimit = 25;

        ucProjectList.Grid.GridName = "~/CMSModules/ProjectManagement/Controls/UI/Project/MyDeskProjects.xml";
        ucProjectList.EditPageURL = ResolveUrl("~/CMSModules/ProjectManagement/MyProjectsAndTasks/MyProjects_ProjectEdit.aspx");
        ucProjectList.BuildCondition +=new CMSModules_ProjectManagement_Controls_UI_Project_List.BuildConditionEvent(ucProjectList_BuildCondition);
    }


    /// <summary>
    /// Builds where condition.
    /// </summary>
    string ucProjectList_BuildCondition(object sender, string whereCondition)
    {
        // Security condition
        return ProjectInfoProvider.CombineSecurityWhereCondition(whereCondition, CMSContext.CurrentUser, CMSContext.CurrentSiteName);
    }
}
