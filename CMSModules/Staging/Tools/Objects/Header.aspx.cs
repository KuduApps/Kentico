using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Objects_Header : CMSStagingObjectsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Manage object tasks' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageObjectsTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageObjectsTasks");
        }

        selectorElem.DropDownList.AutoPostBack = true;
        selectorElem.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        int serverId = ValidationHelper.GetInteger(selectorElem.Value, 0);
        // All servers
        if (serverId == UniSelector.US_ALL_RECORDS)
        {
            serverId = 0;
        }
        ScriptHelper.RegisterStartupScript(this, typeof(string), "changeServer", ScriptHelper.GetScript("ChangeServer(" + serverId + ");"));
    }
}
