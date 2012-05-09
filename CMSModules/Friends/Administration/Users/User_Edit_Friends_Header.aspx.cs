using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Friends_Administration_Users_User_Edit_Friends_Header : CMSUsersPage
{
    #region "Variables"

    protected int userId = 0;
    protected CurrentUserInfo currentUser = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        userId = QueryHelper.GetInteger("userId", 0);
        currentUser = CMSContext.CurrentUser;

        // Check 'read' permissions
        if (!currentUser.IsAuthorizedPerResource("CMS.Friends", "Read") && (currentUser.UserID != userId))
        {
            RedirectToAccessDenied("CMS.Friends", "Read");
        }

        // Check license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Friends);
        }

        // initializes breadcrumbs 		
        string[,] pageTitleTabs = new string[1, 3];
        pageTitleTabs[0, 0] = GetString("friends.friends");
        pageTitleTabs[0, 1] = "";
        pageTitleTabs[0, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.HelpTopicName = "friends_myfriends";
        CurrentMaster.Title.HelpName = "helpTopic";

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }


    #region "Private methods"

    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("friends.userfriends");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'friends_myfriends');";
        string url = "User_Edit_Friends_Approved.aspx" + URLHelper.Url.Query;
        tabs[0, 2] = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));

        tabs[1, 0] = GetString("friends.userwaitingforapproval");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'friends_waitingforapproval');";
        url = "User_Edit_Friends_ToApprove.aspx" + URLHelper.Url.Query;
        tabs[1, 2] = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));

        tabs[2, 0] = GetString("friends.userrejectedfriendships");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'friends_rejectedfriendships');";
        url = "User_Edit_Friends_Rejected.aspx" + URLHelper.Url.Query;
        tabs[2, 2] = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));

        tabs[3, 0] = GetString("friends.userrequestedfriendships");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'friends_requestedfriendships');";
        url = "User_Edit_Friends_Requested.aspx" + URLHelper.Url.Query;
        tabs[3, 2] = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));

        CurrentMaster.Tabs.UrlTarget = "friendsContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
