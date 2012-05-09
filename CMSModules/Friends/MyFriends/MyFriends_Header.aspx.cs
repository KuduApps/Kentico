using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Friends_MyFriends_MyFriends_Header : CMSMyFriendsPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("friends.friends");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Friends/module.png");
        CurrentMaster.Title.HelpTopicName = "friends_myfriends";
        CurrentMaster.Title.HelpName = "helpTopic";

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("friends.myfriends");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'friends_myfriends');";
        tabs[0, 2] = "MyFriends_Approved.aspx" + URLHelper.Url.Query;

        tabs[1, 0] = GetString("friends.waitingforapproval");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'friends_waitingforapproval');";
        tabs[1, 2] = "MyFriends_ToApprove.aspx" + URLHelper.Url.Query;

        tabs[2, 0] = GetString("friends.rejectedfriendships");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'friends_rejectedfriendships');";
        tabs[2, 2] = "MyFriends_Rejected.aspx" + URLHelper.Url.Query;

        tabs[3, 0] = GetString("friends.requestedfriendships");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'friends_requestedfriendships');";
        tabs[3, 2] = "MyFriends_Requested.aspx" + URLHelper.Url.Query;
        CurrentMaster.Tabs.UrlTarget = "friendsContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
