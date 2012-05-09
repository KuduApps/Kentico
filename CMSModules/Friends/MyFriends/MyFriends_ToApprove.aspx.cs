using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Friends_MyFriends_MyFriends_ToApprove : CMSMyFriendsPage
{
    #region "Variables"

    protected CurrentUserInfo currentUser = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;
        ScriptHelper.RegisterDialogScript(this);
        FriendsListToApprove.UserID = currentUser.UserID;
        FriendsListToApprove.OnCheckPermissions += CheckPermissions;
        FriendsListToApprove.ZeroRowsText = GetString("friends.nowaitingfriends");
    }

    #endregion


    #region "Other events"

    protected void CheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Do not check permissions since user can always manage her friends
    }

    #endregion
}
