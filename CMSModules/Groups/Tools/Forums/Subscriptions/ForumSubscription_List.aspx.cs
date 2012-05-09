using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Subscriptions_ForumSubscription_List : CMSGroupForumPage
{
    protected int forumId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        forumId = QueryHelper.GetInteger("forumid", 0);
        this.subscriptionList.ForumID = forumId;
        this.subscriptionList.OnAction += new CommandEventHandler(subscriptionList_OnAction);
        this.subscriptionList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(subscriptionList_OnCheckPermissions);
        this.subscriptionList.IsLiveSite = false;

        InitializeMasterPage(forumId);
    }


    void subscriptionList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        ForumInfo fi = ForumInfoProvider.GetForumInfo(this.subscriptionList.ForumID);
        if (fi != null)
        {
            ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
            if (fgi != null)
            {
                groupId = fgi.GroupGroupID;
            }
        }
        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    void subscriptionList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("ForumSubscription_Edit.aspx?subscriptionId=" + Convert.ToString(e.CommandArgument) + "&forumid=" + forumId);

                break;

            case "delete":
            default:
                subscriptionList.ReloadData();
                break;
        }
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage(int forumId)
    {
        this.Title = "Forums - Subscriptions List";

        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("ForumSubscription_List.NewItemCaption");
        if (forumId > 0)
        {
            actions[0, 3] = ResolveUrl("ForumSubscription_Edit.aspx?forumid=" + forumId.ToString());
        }
        actions[0, 5] = GetImageUrl("Objects/Forums_ForumSubscription/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;
    }
}
