using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_Project_List : CMSProjectManagementProjectsPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int projectID = QueryHelper.GetInteger("projectid", 0);
        ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(projectID);
        if (pi != null)
        {
            if (pi.ProjectSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToInformation(GetString("general.notassigned"));
            }
        }

        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("pm.project.new");        
        actions[0, 3] = ResolveUrl("Edit.aspx");       
        actions[0, 5] = GetImageUrl("Objects/PM_Project/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;

        // Set the title
        PageTitle title = master.Title;

        ScriptHelper.RegisterTitleScript(this, GetString("projectmanagement.projects"));
    }

    #endregion
}
