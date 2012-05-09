using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Tasks_Frameset : CMSStagingTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        // check 'Manage servers' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageDocumentsTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDocumentsTasks");
        }
    }
}
