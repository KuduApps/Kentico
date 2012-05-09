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
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Polls_Polls_List : CMSGroupPollsPage
{
    protected int groupID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get GroupID from query string
        groupID = QueryHelper.GetInteger("groupID", 0);

        CheckGroupPermissions(groupID, CMSAdminControl.PERMISSION_READ);

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("group.polls.newpoll");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Polls_New.aspx") + "?groupid=" + groupID;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Polls_Poll/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        pollsList.OnEdit += new EventHandler(pollsList_OnEdit);
        pollsList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(pollsList_OnCheckPermissions);
        pollsList.GroupId = groupID;
        pollsList.IsLiveSite = false;
    }


    void pollsList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(groupID, CMSAdminControl.PERMISSION_MANAGE);
    }


    /// <summary>
    /// Edit poll click handler.
    /// </summary>
    void pollsList_OnEdit(object sender, EventArgs e)
    {
        URLHelper.Redirect(ResolveUrl("Polls_Edit.aspx?pollId=" + pollsList.SelectedItemID + "&GroupId=" + groupID));
    }
}
