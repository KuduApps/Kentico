using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Community;

public partial class CMSModules_Groups_CMSPages_JoinTheGroup : CMSLiveModalPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        groupJoinElem.JoinButton = btnJoin;
        groupJoinElem.CancelButton = btnCancel;
    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("Groups.JoinTheGroup");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Groups/jointhegroup.png");
        Title = GetString("Groups.JoinTheGroup");

        if (CommunityContext.CurrentGroup != null)
        {
            groupJoinElem.Group = CommunityContext.CurrentGroup;
        }
    }
}
