using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_Membership_Pages_Users_Users_Header : CMSUsersPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Users administration";

        // Initialize the master page elements
        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set the master page title
        CurrentMaster.Title.TitleText = GetString("general.users");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_User/object.png");

        this.CurrentMaster.Title.HelpTopicName = "users_list";
        this.CurrentMaster.Title.HelpName = "title";

        string query = GetQueryParameters();

        bool registrationAdministratorApproval = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSRegistrationAdministratorApproval");
        bool onlineUsers = LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.OnlineUsers);
        int count = 2;
        if (registrationAdministratorApproval) { count++; }
        if (onlineUsers) { count++; }

        // Set the tabs
        string[,] tabs = new string[count, 8];
        int i = 0;
        tabs[i, 0] = GetString("general.users");
        tabs[i, 1] = "SetHelpTopic('title', 'users_list');";
        tabs[i, 2] = "User_List.aspx" + query;

        if (registrationAdministratorApproval)
        {
            i++;
            tabs[i, 0] = GetString("administration.users_header.myapproval");
            tabs[i, 1] = "SetHelpTopic('title', 'User_WaitingForApproval');";
            tabs[i, 2] = "General/User_WaitingForApproval.aspx" + query;
        }

        i++;
        tabs[i, 0] = GetString("administration.users_header.massemails");
        tabs[i, 1] = "SetHelpTopic('title', 'User_MassEmail');";
        tabs[i, 2] = "General/User_MassEmail.aspx" + query;

        if (onlineUsers)
        {
            i++;
            tabs[i, 0] = GetString("administration.users_header.onlineusers");
            tabs[i, 1] = "SetHelpTopic('title', 'User_OnlineUsers');";
            tabs[i, 2] = "General/User_Online.aspx" + query;
        }

        CurrentMaster.Tabs.UrlTarget = "usersContent";
        CurrentMaster.Tabs.Tabs = tabs;

        // Load action page directly, if set by URL
        switch (QueryHelper.GetString("action", null))
        {
            case "newuser":
                this.CurrentMaster.Tabs.StartPageURL = URLHelper.ResolveUrl("user_new.aspx" + query);
                break;
        }
    }


    private string GetQueryParameters()
    {
        if (SiteID != 0)
        {
            return "?siteid=" + SiteID;
        }

        return "";
    }
}
