using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Data_Frameset : CMSStagingDataPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        // Check 'Manage object tasks' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageDataTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDataTasks");
        }
    }
}
