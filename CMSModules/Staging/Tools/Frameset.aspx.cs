using System;

using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Frameset : CMSStagingPage
{
    protected string contentPage = "Servers/List.aspx";


    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Check the permissions
        if (currentUser.IsAuthorizedPerResource("cms.staging", "ManageAllTasks"))
        {
            // All tasks allowed
            contentPage = "AllTasks/TaskSeparator.aspx";
        }
        else if (currentUser.IsAuthorizedPerResource("cms.staging", "ManageDocumentsTasks"))
        {
            // All tasks allowed
            contentPage = "Tasks/TaskSeparator.aspx";
        }
        else if (currentUser.IsAuthorizedPerResource("cms.staging", "ManageDataTasks"))
        {
            // Data tasks allowed
            contentPage = "Data/TaskSeparator.aspx";
        }
        else if (currentUser.IsAuthorizedPerResource("cms.staging", "ManageObjectsTasks"))
        {
            // Object tasks allowed
            contentPage = "Objects/TaskSeparator.aspx";
        }
        else if (currentUser.IsAuthorizedPerResource("cms.staging", "ManageServers"))
        {
            // Object tasks allowed
            contentPage = "Servers/List.aspx";
        }
        else
        {
            // Redirect to access denied
            RedirectToAccessDenied("cms.staging", "ManageAllTasks|ManageTasks|ManageDataTasks|ManageObjectTasks|ManageServers");
        }
    }
}
