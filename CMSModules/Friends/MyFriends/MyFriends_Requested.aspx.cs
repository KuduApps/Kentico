using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Friends_MyFriends_MyFriends_Requested : CMSMyFriendsPage
{
    #region "Variables"

    protected CurrentUserInfo currentUser = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;
        string imagePath = URLHelper.ResolveUrl(GetImageUrl("Objects/CMS_Friend/"));
        ScriptHelper.RegisterDialogScript(this);
        FriendsListRequested.UserID = currentUser.UserID;
        FriendsListRequested.OnCheckPermissions += CheckPermissions;
        FriendsListRequested.ZeroRowsText = GetString("friends.norequestedfriends");

        // Request friend link
        string script =
            "function displayRequest(){ \n" +
                "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Friends/Dialogs/Friends_Request.aspx") + "?userid=" + currentUser.UserID + "', 'rejectDialog', 480, 350);}";

        ScriptHelper.RegisterStartupScript(this, GetType(), "displayModalRequest", ScriptHelper.GetScript(script));

        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Friends_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = "javascript:displayRequest();";
        actions[0, 4] = null;
        actions[0, 5] = imagePath + "add.png";
        CurrentMaster.HeaderActions.Actions = actions;
    }

    #endregion


    #region "Other events"

    protected void CheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Do not check permissions since user can always manage her friends
    }

    #endregion
}
