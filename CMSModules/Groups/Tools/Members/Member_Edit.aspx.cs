using System;

using CMS.GlobalHelper;
using CMS.Community;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Groups_Tools_Members_Member_Edit : CMSGroupPage
{
    #region "Private variables"

    protected int memberId = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get memberId
        memberId = QueryHelper.GetInteger("memberid", 0);

        // Initialize editing control
        memberEditElem.MemberID = memberId;

        this.CurrentMaster.Title.HelpTopicName = "group_members_edit";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        GroupMemberInfo gmi = GroupMemberInfoProvider.GetGroupMemberInfo(memberId);
        if (gmi != null)
        {
            memberEditElem.GroupID = gmi.MemberGroupID;
            UserInfo ui = UserInfoProvider.GetUserInfo(gmi.MemberUserID);
            if (ui != null)
            {
                // Initialize breadcrumbs
                string[,] pageTitleTabs = new string[2, 3];
                pageTitleTabs[0, 0] = GetString("group.members");
                pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Members/Member_List.aspx";
                pageTitleTabs[0, 1] += "?groupId=" + gmi.MemberGroupID;
                pageTitleTabs[0, 2] = "_self";
                pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(ui.FullName);
                pageTitleTabs[1, 1] = "";
                pageTitleTabs[1, 2] = "";
                CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
            }
        }
    }
}
