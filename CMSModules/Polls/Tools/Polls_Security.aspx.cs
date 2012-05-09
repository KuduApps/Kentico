using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;

public partial class CMSModules_Polls_Tools_Polls_Security : CMSPollsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int pollid = QueryHelper.GetInteger("pollid", 0);
        PollInfo pi = PollInfoProvider.GetPollInfo(pollid);
        EditedObject = pi;

        // Check global and site read permmision
        this.CheckPollsReadPermission(pi.PollSiteID);

        PollSecurity.ItemID = pollid;
        PollSecurity.IsLiveSite = false;
        PollSecurity.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(PollSecurity_OnCheckPermissions);
    }


    /// <summary>
    /// Check permissions event handler.
    /// </summary>
    void PollSecurity_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Polls", permissionType))
        {
            sender.StopProcessing = true;
        }
    }
}
