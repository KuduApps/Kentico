using System;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Friends_Dialogs_Friends_Request : CMSModalPage
{
    #region "Variables"

    protected int userId = 0;
    protected CurrentUserInfo currentUser = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        userId = QueryHelper.GetInteger("userid", 0);
        currentUser = CMSContext.CurrentUser;

        // Check license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Friends);
        }

        int requestedUserId = QueryHelper.GetInteger("requestid", 0);
        Page.Title = GetString("friends.addnewfriend");
        CurrentMaster.Title.TitleText = Page.Title;
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Friend/new.png");

        FriendsRequest.UserID = userId;
        FriendsRequest.RequestedUserID = requestedUserId;
        FriendsRequest.OnCheckPermissions += FriendsRequest_OnCheckPermissions;

        if (requestedUserId != 0)
        {
            UserInfo requestedUser = UserInfoProvider.GetFullUserInfo(requestedUserId);
            string fullUserName = Functions.GetFormattedUserName(requestedUser.UserName, requestedUser.FullName, requestedUser.UserNickName, false);
            Page.Title = string.Format(GetString("friends.requestfriendshipwith"), HTMLHelper.HTMLEncode(fullUserName));
            CurrentMaster.Title.TitleText = Page.Title;
        }
    }


    void FriendsRequest_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check if request is for current user or another user with permission to manage it
        if ((currentUser.UserID != userId) && !currentUser.IsAuthorizedPerResource("CMS.Friends", permissionType))
        {
            RedirectToAccessDenied("CMS.Friends", permissionType);
        }
    }
}
