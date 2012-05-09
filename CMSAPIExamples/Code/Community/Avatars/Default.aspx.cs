using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.IO;


[Title(Text = "Avatars", ImageUrl = "Objects/CMS_Avatar/object.png")]
public partial class CMSAPIExamples_Code_Community_Avatars_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Avatar
        this.apiCreateAvatar.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAvatar);
        this.apiGetAndUpdateAvatar.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateAvatar);
        this.apiGetAndBulkUpdateAvatars.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateAvatars);
        this.apiDeleteAvatar.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteAvatar);

        // Avatar on user
        this.apiAddAvatarToUser.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddAvatarToUser);
        this.apiRemoveAvatarFromUser.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveAvatarFromUser);
    }


    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Avatar
        this.apiCreateAvatar.Run();
        this.apiGetAndUpdateAvatar.Run();
        this.apiGetAndBulkUpdateAvatars.Run();

        // Avatar on user
        this.apiAddAvatarToUser.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Avatar on user
        this.apiRemoveAvatarFromUser.Run();

        // Avatar
        this.apiDeleteAvatar.Run();
    }

    #endregion


    #region "API examples - Avatar"

    /// <summary>
    /// Creates avatar. Called when the "Create avatar" button is pressed.
    /// </summary>
    private bool CreateAvatar()
    {
        // Create new avatar object
        AvatarInfo newAvatar = new AvatarInfo(Server.MapPath("~\\CMSAPIExamples\\Code\\Community\\Avatars\\Files\\avatar_man.jpg"));

        // Set the properties
        newAvatar.AvatarName = "MyNewAvatar";
        newAvatar.AvatarType = AvatarInfoProvider.GetAvatarTypeString(AvatarTypeEnum.All);
        newAvatar.AvatarIsCustom = false;

        // Save the avatar
        AvatarInfoProvider.SetAvatarInfo(newAvatar);

        return true;
    }


    /// <summary>
    /// Gets and updates avatar. Called when the "Get and update avatar" button is pressed.
    /// Expects the CreateAvatar method to be run first.
    /// </summary>
    private bool GetAndUpdateAvatar()
    {
        // Get the avatar
        AvatarInfo updateAvatar = AvatarInfoProvider.GetAvatarInfo("MyNewAvatar");
        if (updateAvatar != null)
        {
            // Update the properties
            updateAvatar.AvatarName = updateAvatar.AvatarName.ToLower();

            // Save the changes
            AvatarInfoProvider.SetAvatarInfo(updateAvatar);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates avatars. Called when the "Get and bulk update avatars" button is pressed.
    /// Expects the CreateAvatar method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateAvatars()
    {
        // Prepare the parameters
        string where = "AvatarName LIKE N'MyNewAvatar%'";

        // Get the data
        DataSet avatars = AvatarInfoProvider.GetAvatars(where, null);
        if (!DataHelper.DataSourceIsEmpty(avatars))
        {
            // Loop through the individual items
            foreach (DataRow avatarDr in avatars.Tables[0].Rows)
            {
                // Create object from DataRow
                AvatarInfo modifyAvatar = new AvatarInfo(avatarDr);

                // Update the properties
                modifyAvatar.AvatarName = modifyAvatar.AvatarName.ToUpper();

                // Save the changes
                AvatarInfoProvider.SetAvatarInfo(modifyAvatar);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes avatar. Called when the "Delete avatar" button is pressed.
    /// Expects the CreateAvatar method to be run first.
    /// </summary>
    private bool DeleteAvatar()
    {
        // Get the avatar
        AvatarInfo deleteAvatar = AvatarInfoProvider.GetAvatarInfo("MyNewAvatar");

        // Delete the avatar
        AvatarInfoProvider.DeleteAvatarInfo(deleteAvatar);

        return (deleteAvatar != null);
    }


    #endregion


    #region "Avatar on user"


    /// <summary>
    /// Adds avatar to user. Called when the "Add avatar to user" button is pressed.
    /// Expects the CreateAvatar method to be run first.
    /// </summary>
    private bool AddAvatarToUser()
    {
        // Get the avatar and user
        AvatarInfo avatar = AvatarInfoProvider.GetAvatarInfo("MyNewAvatar");
        UserInfo user = UserInfoProvider.GetUserInfo(CMSContext.CurrentUser.UserName);

        if ((avatar != null) && (user != null))
        {
            user.UserAvatarID = avatar.AvatarID;

            // Save edited object
            UserInfoProvider.SetUserInfo(user);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes avatar from user. Called when the "Remove avatar from user" button is pressed.
    /// </summary>
    private bool RemoveAvatarFromUser()
    {
        // Get the user
        UserInfo user = UserInfoProvider.GetUserInfo(CMSContext.CurrentUser.UserName);

        if (user != null)
        {
            user.UserAvatarID = 0;

            // Save edited object
            UserInfoProvider.SetUserInfo(user);

            return true;
        }

        return false;
    }

    #endregion
}
