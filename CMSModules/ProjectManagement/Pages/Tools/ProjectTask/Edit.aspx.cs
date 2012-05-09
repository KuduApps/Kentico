using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_ProjectTask_Edit : CMSProjectManagementTasksPage
{
    #region "Variables"

    int projectId = 0;
    int assigneeId = 0;
    int ownerId = 0;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get the IDs from query string
        projectId = QueryHelper.GetInteger("projectid", 0);

        ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(projectId);
        if (pi != null)
        {
            if (pi.ProjectSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToInformation(GetString("general.notassigned"));
            }
        }

        assigneeId = QueryHelper.GetInteger("assigneeid", 0);
        ownerId = QueryHelper.GetInteger("ownerid", 0);

        editElem.ProjectID = projectId;
        editElem.ItemID = QueryHelper.GetInteger("projecttaskid", 0);
        editElem.ProjectTaskAssigneeID = assigneeId;
        editElem.ProjectTaskOwnerID = ownerId;
        editElem.OnSaved += new EventHandler(editElem_OnSaved);

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle title = this.CurrentMaster.Title;
        title.HelpTopicName = "pm_task_edit";
        
        // Prepare the header
        string name = "";
        if (editElem.ProjectTaskObj != null)
        {
            name = editElem.ProjectTaskObj.ProjectTaskDisplayName;
            title.TitleImage = GetImageUrl("Objects/PM_ProjectTask/object.png");
        }
        else
        {
            name = (projectId > 0) ? GetString("pm.projecttask.new") : GetString("pm.projecttask.newpersonal");
            title.TitleImage = GetImageUrl("Objects/PM_ProjectTask/new.png");
        }

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = (projectId > 0) ? GetString("pm.projecttask.list") : GetString("pm.projecttask");
        breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/ProjectManagement/Pages/Tools/ProjectTask/List.aspx?projectid=" + projectId);
        breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(name);
        
        // Set the title
        title.Breadcrumbs = breadcrumbs;

        if ((projectId > 0)
            && (editElem.ProjectTaskObj != null))
        {
            // Set actions
            string[,] actions = new string[1, 8];
            actions[0, 0] = "HyperLink";
            actions[0, 1] = GetString("pm.projecttask.new");
            actions[0, 3] = "Edit.aspx";
            actions[0, 5] = GetImageUrl("Objects/PM_ProjectTask/add.png");

            CurrentMaster.HeaderActions.Actions = actions;
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if ((projectId > 0)
            && (CurrentMaster.HeaderActions.Actions != null))
        {
            string editUrl = CurrentMaster.HeaderActions.Actions[0, 3];
            string query = "?projectid=" + projectId;
            ProjectTaskInfo taskObj = editElem.ProjectTaskObj;
            
            if (taskObj != null)
            {
                query += "&assigneeid=" + taskObj.ProjectTaskAssignedToUserID
                        + "&ownerid=" + taskObj.ProjectTaskOwnerID;
            }

            CurrentMaster.HeaderActions.Actions[0, 3] = ResolveUrl(editUrl + query);
            CurrentMaster.HeaderActions.ReloadData();
        }
    }


    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        if (editElem.ItemID > 0)
        {
            URLHelper.Redirect("~/CMSModules/ProjectManagement/Pages/Tools/ProjectTask/Edit.aspx?projecttaskid=" + editElem.ItemID + "&projectid=" + projectId + "&saved=1");
        }
    }

    #endregion
}