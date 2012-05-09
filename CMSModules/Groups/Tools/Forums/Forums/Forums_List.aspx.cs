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
using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;
using CMS.Forums;

public partial class CMSModules_Groups_Tools_Forums_Forums_Forums_List : CMSGroupForumPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int groupId = QueryHelper.GetInteger("groupid", 0);
        this.forumList.GroupID = groupId;
        this.forumList.OnAction += new CommandEventHandler(forumList_OnAction);
        this.forumList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(forumList_OnCheckPermissions);
        this.forumList.IsLiveSite = false;

        this.InitializeMasterPage(groupId);
    }


    void forumList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int lGroupId = 0;

        ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(forumList.GroupID);
        if (fgi != null)
        {
            lGroupId = fgi.GroupGroupID;
        }

        CheckPermissions(lGroupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    void forumList_OnAction(object sender, CommandEventArgs e)
    {
        int lGroupId = 0;

        ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(forumList.GroupID);
        if (fgi != null)
        {
            lGroupId = fgi.GroupGroupID;
        }

        CheckPermissions(lGroupId, CMSAdminControl.PERMISSION_READ);

        if (e.CommandName.ToLower() == "edit")
        {
            URLHelper.Redirect("Forum_Frameset.aspx?forumid=" + Convert.ToString(e.CommandArgument));
        }
        else
        {
            forumList.ReloadData();
        }
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage(int groupId)
    {
        // Set title
        this.Title = "Forum List";
        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Forum_List.NewItemCaption");
        actions[0, 3] = ResolveUrl("Forum_New.aspx?groupId=" + groupId.ToString());
        actions[0, 5] = GetImageUrl("Objects/Forums_Forum/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }
}
