using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Community;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

[Title(Text = "Friends", ImageUrl = "CMSModules/CMS_Friends/module.png")]
public partial class CMSAPIExamples_Code_Community_Friends_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Friends);

        // Friend
        this.apiRequestFriendship.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RequestFriendship);
        this.apiApproveFriendship.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(ApproveFriendship);
        this.apiRejectFriendship.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RejectFriendship);
        this.apiGetAndBulkUpdateFriends.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateFriends);
        this.apiDeleteFriends.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteFriends);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Friend
        this.apiRequestFriendship.Run();
        this.apiApproveFriendship.Run();
        this.apiRejectFriendship.Run();
        this.apiGetAndBulkUpdateFriends.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Friend
        this.apiDeleteFriends.Run();
    }

    #endregion


    #region "API examples - Friend"

    /// <summary>
    /// Creates friend. Called when the "Create friend" button is pressed.
    /// </summary>
    private bool RequestFriendship()
    {
        // First create a new user which the friendship request will be sent to
        UserInfo newFriend = new UserInfo();
        newFriend.UserName = "MyNewFriend";
        newFriend.FullName = "My new friend";
        newFriend.UserGUID = Guid.NewGuid();

        UserInfoProvider.SetUserInfo(newFriend);

        // Create new friend object
        FriendInfo newFriendship = new FriendInfo();

        // Set the properties
        newFriendship.FriendUserID = CMSContext.CurrentUser.UserID;
        newFriendship.FriendRequestedUserID = newFriend.UserID;
        newFriendship.FriendRequestedWhen = DateTime.Now;
        newFriendship.FriendComment = "Sample friend request comment.";
        newFriendship.FriendStatus = FriendshipStatusEnum.Waiting;
        newFriendship.FriendGUID = Guid.NewGuid();

        // Save the friend
        FriendInfoProvider.SetFriendInfo(newFriendship);

        return true;
    }


    /// <summary>
    /// Approves the friendship. Called when the "Approve friendship" button is pressed.
    /// Expects the RequestFriendship method to be run first.
    /// </summary>
    private bool ApproveFriendship()
    {
        // Get the users involved in the friendship
        UserInfo user = CMSContext.CurrentUser;
        UserInfo friend = UserInfoProvider.GetUserInfo("MyNewFriend");

        if (friend != null)
        {
            // Get the friendship with current user
            FriendInfo updateFriendship = FriendInfoProvider.GetFriendInfo(user.UserID, friend.UserID);
            if (updateFriendship != null)
            {
                // Set its properties
                updateFriendship.FriendStatus = FriendshipStatusEnum.Approved;
                updateFriendship.FriendRejectedBy = 0;
                updateFriendship.FriendApprovedBy = user.UserID;
                updateFriendship.FriendApprovedWhen = DateTime.Now;

                // Save the changes to database
                FriendInfoProvider.SetFriendInfo(updateFriendship);

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Rejects the friendship. Called when the "Reject friendship" button is pressed.
    /// Expects the RequestFriendship method to be run first.
    /// </summary>
    private bool RejectFriendship()
    {
        // Get the users involved in the friendship
        UserInfo user = CMSContext.CurrentUser;
        UserInfo friend = UserInfoProvider.GetUserInfo("MyNewFriend");

        if (friend != null)
        {
            // Get the friendship with current user
            FriendInfo updateFriendship = FriendInfoProvider.GetFriendInfo(user.UserID, friend.UserID);

            // Set its properties
            updateFriendship.FriendStatus = FriendshipStatusEnum.Rejected;
            updateFriendship.FriendApprovedBy = 0;
            updateFriendship.FriendRejectedBy = user.UserID;
            updateFriendship.FriendRejectedWhen = DateTime.Now;

            // Save the changes to database
            FriendInfoProvider.SetFriendInfo(updateFriendship);

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets and bulk updates friends. Called when the "Get and bulk update friends" button is pressed.
    /// Expects the RequestFriendship method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateFriends()
    {
        // Get the user
        UserInfo friend = UserInfoProvider.GetUserInfo("MyNewFriend");

        if (friend != null)
        {
            // Prepare the parameters
            string where = "FriendRequestedUserID = " + friend.UserID;

            // Get the data
            DataSet friends = FriendInfoProvider.GetFriends(where, null);
            if (!DataHelper.DataSourceIsEmpty(friends))
            {
                // Loop through the individual items
                foreach (DataRow friendDr in friends.Tables[0].Rows)
                {
                    // Create object from DataRow
                    FriendInfo modifyFriend = new FriendInfo(friendDr);

                    // Update the properties
                    modifyFriend.FriendStatus = FriendshipStatusEnum.Approved;
                    modifyFriend.FriendRejectedBy = 0;
                    modifyFriend.FriendApprovedBy = CMSContext.CurrentUser.UserID;
                    modifyFriend.FriendApprovedWhen = DateTime.Now;

                    // Save the changes
                    FriendInfoProvider.SetFriendInfo(modifyFriend);
                }

                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Deletes all friends of "My new user". Called when the "Delete friends" button is pressed.
    /// Expects the CreateFriend method to be run first.
    /// </summary>
    private bool DeleteFriends()
    {
        // Get the user
        UserInfo friend = UserInfoProvider.GetUserInfo("MyNewFriend");

        if (friend != null)
        {
            // Prepare the parameters
            string where = "FriendRequestedUserID = " + friend.UserID;

            // Get all user's friendships
            DataSet friends = FriendInfoProvider.GetFriends(where, null);
            if (!DataHelper.DataSourceIsEmpty(friends))
            {
                // Delete all the friendships
                foreach (DataRow friendDr in friends.Tables[0].Rows)
                {
                    FriendInfo deleteFriend = new FriendInfo(friendDr);

                    FriendInfoProvider.DeleteFriendInfo(deleteFriend);
                }
            }
            else
            {
                // Change the info message
                apiDeleteFriends.InfoMessage = "The user 'My new friend' doesn't have any friends. The user has been deleted.";
            }

            // Finally delete the user "My new friend"
            UserInfoProvider.DeleteUser(friend);

            return true;
        }

        return false;

    }

    #endregion
}
