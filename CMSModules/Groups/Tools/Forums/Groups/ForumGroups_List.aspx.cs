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
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.Community;
using CMS.SiteProvider;

public partial class CMSModules_Groups_Tools_Forums_Groups_ForumGroups_List : CMSGroupForumPage
{
    int groupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {        
        // Get info on the current community group ID
        groupId = QueryHelper.GetInteger("groupid", 0);
        if (groupId > 0)
        {
            this.forumGroupsList.CommunityGroupId = groupId;
            this.forumGroupsList.OnAction += new CommandEventHandler(forumGroupsList_OnAction);
        }

        this.forumGroupsList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(forumGroupsList_OnCheckPermissions);
        this.forumGroupsList.IsLiveSite = false;

        InitializeMasterPage();
    }


    void forumGroupsList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (permissionType == CMSAdminControl.PERMISSION_READ)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_READ))
            {
                sender.StopProcessing = true;
            }
        }
        else
        {
            // Check permissions
            CheckPermissions(this.forumGroupsList.CommunityGroupId, CMSAdminControl.PERMISSION_MANAGE);
        }
    }


    void forumGroupsList_OnAction(object sender, CommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edit")
        {
            // Check permissions
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_READ))
            {
                RedirectToCMSDeskAccessDenied("CMS.Groups", CMSAdminControl.PERMISSION_READ);
            }

            URLHelper.Redirect("Group_Frameset.aspx?groupId=" + e.CommandArgument.ToString());
        }
        else
        {
            forumGroupsList.ReloadData();
        }
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        // Set title and help
        this.Title = "Group list";

        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Group_List.NewItemCaption");
        actions[0, 3] = ResolveUrl("~/CMSModules/Groups/Tools/Forums/Groups/ForumGroup_New.aspx") + "?groupid=" + groupId.ToString();
        actions[0, 5] = GetImageUrl("Objects/Forums_ForumGroup/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }
}
