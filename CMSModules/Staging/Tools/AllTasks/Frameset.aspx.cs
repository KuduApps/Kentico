using System;

using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_AllTasks_Frameset : CMSStagingAllTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Manage object tasks' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageAllTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageAllTasks");
        }
    }
}
