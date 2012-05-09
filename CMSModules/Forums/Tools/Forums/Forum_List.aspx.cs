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

public partial class CMSModules_Forums_Tools_Forums_Forum_List : CMSForumsPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
        int groupId = QueryHelper.GetInteger("groupid", 0);

        this.forumList.GroupID = groupId;
        this.forumList.OnAction += new CommandEventHandler(forumList_OnAction);
        this.forumList.IsLiveSite = false;

        this.InitializeMasterPage(groupId);
	}

    
    protected void forumList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToString())
        {
            case "edit":
                URLHelper.Redirect("Forum_Frameset.aspx?forumid=" + e.CommandArgument);
                break;
            default:
                forumList.ReloadData();
                break;
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
