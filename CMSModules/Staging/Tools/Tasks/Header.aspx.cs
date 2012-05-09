using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Tasks_Header : CMSStagingTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Manage servers' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageDocumentsTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDocumentsTasks");
        }

        imgComplete.ImageUrl = GetImageUrl("CMSModules/CMS_Staging/completesync.png");

        selectorElem.DropDownList.AutoPostBack = true;
        selectorElem.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        int serverId = ValidationHelper.GetInteger(selectorElem.Value, 0);
        // All servers
        if (serverId == -1)
        {
            serverId = 0;
        }
        ScriptHelper.RegisterStartupScript(this, typeof(string), "changeServer", ScriptHelper.GetScript("ChangeServer(" + serverId + ");"));
    }
}
