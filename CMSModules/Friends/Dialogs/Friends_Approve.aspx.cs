using System;
using System.Collections;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Friends_Dialogs_Friends_Approve : CMSModalPage
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

        FriendsApprove.SelectedFriends = null;
        FriendsApprove.OnCheckPermissions += FriendsApprove_OnCheckPermissions;

        int requestedId = QueryHelper.GetInteger("requestid", 0);
        int friendshipId = 0;
        Page.Title = GetString("friends.approvefriendship");
        CurrentMaster.Title.TitleText = Page.Title;
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Friends/Waitingforapproval.png");

        // Multiple selection
        if (Request["ids"] != null)
        {
            string[] items = Request["ids"].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length > 0)
            {
                ArrayList friends = new ArrayList();
                foreach (string item in items)
                {
                    friends.Add(ValidationHelper.GetInteger(item, 0));
                }
                FriendsApprove.SelectedFriends = friends;
                if (friends.Count == 1)
                {
                    friendshipId = Convert.ToInt32(friends[0]);
                }
            }
        }
        // For one user
        else
        {
            FriendsApprove.RequestedUserID = requestedId;
        }

        FriendInfo fi = null;
        if (friendshipId != 0)
        {
            fi = FriendInfoProvider.GetFriendInfo(friendshipId);
            // Set edited object
            EditedObject = fi;
        }
        else if (requestedId != 0)
        {
            fi = FriendInfoProvider.GetFriendInfo(userId, requestedId);
            // Set edited object
            EditedObject = fi;
        }

        if (fi != null)
        {
            UserInfo requestedUser = (userId == fi.FriendRequestedUserID) ? UserInfoProvider.GetFullUserInfo(fi.FriendUserID) : UserInfoProvider.GetFullUserInfo(fi.FriendRequestedUserID);
            string fullUserName = Functions.GetFormattedUserName(requestedUser.UserName, requestedUser.FullName, requestedUser.UserNickName, false);
            Page.Title = GetString("friends.approvefriendshipwith") + " " + HTMLHelper.HTMLEncode(fullUserName);
            CurrentMaster.Title.TitleText = Page.Title;
        }

        // Set current user
        FriendsApprove.UserID = userId;
    }

    void FriendsApprove_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check if approve is for current user or another user with permission to manage it
        if ((currentUser.UserID != userId) && !currentUser.IsAuthorizedPerResource("CMS.Friends", permissionType))
        {
            RedirectToAccessDenied("CMS.Friends", permissionType);
        }
    }
}
