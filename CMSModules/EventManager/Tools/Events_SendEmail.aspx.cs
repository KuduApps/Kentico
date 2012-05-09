using System;
using System.Data;
using System.Web;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_EventManager_Tools_Events_SendEmail : CMSEventManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventId = QueryHelper.GetInteger("eventId", 0);

        // Check if requested event exists
        TreeProvider mTree = new TreeProvider();
        TreeNode mEventNode = mTree.SelectSingleNode(eventId);
        if ((mEventNode == null) || (mEventNode.NodeClassName.ToLower() != "cms.bookingevent"))
        {
            CMSPage.EditedObject = null;
        }

        emailSender.EventID = eventId;
        if (!URLHelper.IsPostback())
        {
            emailSender.ReloadData(true);
        }
        emailSender.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(emailSender_OnCheckPermissions);
    }


    void emailSender_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check READ permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EventManager", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.EventManager", permissionType);
        }
    }
}