using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Members_Member_Invite : CMSGroupPage
{
    #region "Private variables"

    protected int groupId = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get group ID
        groupId = QueryHelper.GetInteger("groupid", 0);

        // Initialize editing control
        groupInviteElem.GroupID = groupId;
        groupInviteElem.DisplayUserSelector = true;
        groupInviteElem.DisplayGroupSelector = false; 
        groupInviteElem.AllowInviteNewUser = true;
        groupInviteElem.DisplayAdvancedOptions = true;

        // Initialize breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("group.members");
        pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Members/Member_List.aspx";
        pageTitleTabs[0, 1] += "?groupId=" + groupId;
        pageTitleTabs[0, 2] = "_self";
        pageTitleTabs[1, 0] = GetString("group.member.invitemember");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        this.CurrentMaster.Title.HelpTopicName = "group_members_invite";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        groupInviteElem.IsLiveSite = false;
        
    }
}
