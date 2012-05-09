using System;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Groups_CMSPages_InviteToGroup : CMSLiveModalPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        groupInviteElem.InvitationButton = btnInvite;
        groupInviteElem.CancelButton = btnCancel;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page
        CurrentMaster.Title.TitleText = GetString("groupinvitation.invite");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Groups/invitemember24.png");
        Title = GetString("groupinvitation.invite");

        btnInvite.Text = GetString("general.invite");
        
        int userId = QueryHelper.GetInteger("invitedid", 0);
        int groupId = QueryHelper.GetInteger("groupid", 0);

        if (userId > 0)
        {
            groupInviteElem.InvitedUserID = userId;
            groupInviteElem.DisplayGroupSelector = true;
        }
        else if (groupId > 0)
        {
            groupInviteElem.AllowInviteNewUser = true;
            groupInviteElem.DisplayUserSelector = true;
            groupInviteElem.UseMultipleUserSelector = false;
            groupInviteElem.DisplayGroupSelector = false;
            groupInviteElem.GroupID = groupId;
        }
        else
        {
            groupInviteElem.Visible = false;
        }
    }
}
