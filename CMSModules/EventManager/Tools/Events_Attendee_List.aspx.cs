using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.EventManager;
using CMS.LicenseProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_EventManager_Tools_Events_Attendee_List : CMSEventManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventNodeId = QueryHelper.GetInteger("eventId", 0);
        attendeesList.EventID = eventNodeId;

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Events_Attendee_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Events_Attendee_Edit.aspx?eventid=" + eventNodeId.ToString());
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_EventManager/addattendee.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
        attendeesList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(attendeesList_OnCheckPermissions);
    }


    /// <summary>
    /// 'Check permission' event handler.
    /// </summary>
    void attendeesList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check READ permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EventManager", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.EventManager", permissionType);
        }
    }
}
