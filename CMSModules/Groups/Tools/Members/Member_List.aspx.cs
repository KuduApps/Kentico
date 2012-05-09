using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Members_Member_List : CMSGroupPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int groupId = QueryHelper.GetInteger("groupId", 0);

        // Register scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EditGroupMember", ScriptHelper.GetScript(
            "function editMember(memberId) {" +
            "    location.replace('Member_Edit.aspx?groupId=" + groupId + "&memberId=' + memberId);" +
            "}"));

      

        // Initialize the new customer element
        string[,] actions = new string[2, 8];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("group.member.addmember");
        actions[0, 3] = "~/CMSModules/Groups/Tools/Members/Member_New.aspx?groupId=" + groupId;
        actions[0, 5] = GetImageUrl("Objects/Community_GroupMember/add.png");
        actions[1, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[1, 1] = GetString("group.member.invitemember");
        actions[1, 3] = "~/CMSModules/Groups/Tools/Members/Member_Invite.aspx?groupId=" + groupId;
        actions[1, 5] = GetImageUrl("CMSModules/CMS_Groups/invitemember.png");

        CurrentMaster.HeaderActions.Actions = actions;

        memberListElem.GroupID = groupId;
        memberListElem.OnAction += memberListElem_OnAction;
    }


    protected void memberListElem_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("~/CMSModules/Groups/Tools/Members/Member_Edit.aspx?groupId=" + memberListElem.GroupID + "&memberId=" + e.CommandArgument);
                break;

            case "approve":
                lblInfo.Text = GetString("group.member.userhasbeenapproved");
                lblInfo.Visible = true;
                break;

            case "reject":
                lblInfo.Text = GetString("group.member.userhasbeenrejected");
                lblInfo.Visible = true;
                break;
        }
    }
}
