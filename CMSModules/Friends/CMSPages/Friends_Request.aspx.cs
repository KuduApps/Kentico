using System;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Friends_CMSPages_Friends_Request : CMSLiveModalPage
{
    #region "Variables"

    protected int userId = 0;
    protected CurrentUserInfo currentUser = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Friends);
        }

        userId = QueryHelper.GetInteger("userid", 0);
        currentUser = CMSContext.CurrentUser;

        int requestedUserId = QueryHelper.GetInteger("requestid", 0);
        CurrentMaster.Title.TitleText = GetString("friends.addnewfriend");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Friends/request.png");

        FriendsRequest.UserID = userId;
        FriendsRequest.RequestedUserID = requestedUserId;
        FriendsRequest.OnCheckPermissions += FriendsRequest_OnCheckPermissions;
        FriendsRequest.IsLiveSite = true;

        if (requestedUserId != 0)
        {
            string fullUserName = String.Empty;

            UserInfo requestedUser = UserInfoProvider.GetFullUserInfo(requestedUserId);
            if (requestedUser != null)
            {
                fullUserName = Functions.GetFormattedUserName(requestedUser.UserName, requestedUser.FullName, requestedUser.UserNickName, true);
            }

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
