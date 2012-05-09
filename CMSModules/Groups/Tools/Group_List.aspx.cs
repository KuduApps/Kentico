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
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Group_List : CMSGroupPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("group.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Community_Group/object.png");
        this.CurrentMaster.Title.HelpTopicName = "group_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Group.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("~/CMSModules/Groups/Tools/Group_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Community_Group/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        // Only current site groups can be listed
        if (CMSContext.CurrentSite != null)
        {
            this.groupListElem.SiteID = CMSContext.CurrentSite.SiteID;
        }

        this.groupListElem.OnAction += new CommandEventHandler(groupListElem_OnAction);
    }


    void groupListElem_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.Trim().ToLower())
        {
            case "edit":
                URLHelper.Redirect("~/CMSModules/Groups/Tools/Group_Edit.aspx?groupid=" + e.CommandArgument.ToString());
                break;

            case "delete":
                CheckPermissions(ValidationHelper.GetInteger(e.CommandArgument, 0), CMSAdminControl.PERMISSION_MANAGE);
                URLHelper.Redirect("~/CMSModules/Groups/Tools/Group_Delete.aspx?groupid=" + e.CommandArgument.ToString());
                break;
        }
    }
}
