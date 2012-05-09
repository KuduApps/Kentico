using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_Membership_Pages_Users_User_Edit : CMSUsersPage
{
    #region "Protected variables"

    protected int userId = 0;
    
    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = QueryHelper.GetInteger("userid", 0);
       
        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        string users = GetString("general.users");
        string currentUser = Functions.GetFormattedUserName(UserInfoProvider.GetUserNameById(userId));

        //initializes PageTitle
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = users;
        pageTitleTabs[0, 1] = "~/CMSModules/Membership/Pages/Users/User_List.aspx?siteid=" + SiteID;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentUser;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        CurrentMaster.Title.HelpTopicName = "general_tab8";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Register script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ShowContent", ScriptHelper.GetScript("function ShowContent(contentLocation) { parent.frames['content'].location.href= contentLocation; }"));
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Initializes user edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string generalString = GetString("general.general");
        string passwordString = GetString("Administration-User_Edit.Password");
        string rolesString = GetString("general.roles");
        string sitesString = GetString("general.sites");
        string customFieldsString = GetString("Administration-User_Edit.CustomFields");
        string departmentsString = GetString("Administration-User_Edit.Departments");
        string notificationsString = GetString("Administration-User_Edit.Notifications");
        string categoriesString = GetString("Administration-User_Edit.Categories");
        string friendsString = GetString("friends.friends");
        string subscriptionsString = GetString("Administration-User_Edit.Subscriptions");
        string languagesString = GetString("general.languages");
        string membershipString = GetString("membership.title");

        // Check custom fields of user
        int customFieldsTab = 0;
        UserInfo ui = UserInfoProvider.GetUserInfo(userId);
        CheckUserAvaibleOnSite(ui); 
        if (ui != null)
        {
            // Get user form information and check for visible non-system fields
            FormInfo formInfo = FormHelper.GetFormInfo(ui.ClassName, false);
            customFieldsTab = (formInfo.GetFormElements(true, false, true).Count > 0 ? 1 : 0);

            // Check custom fields of user settings if needed
            if ((customFieldsTab == 0) && (ui.UserSettings !=null))
            {
                // Get user settings form information and check for visible non-system fields
                formInfo = FormHelper.GetFormInfo(ui.UserSettings.ClassName, false);
                customFieldsTab = (formInfo.GetFormElements(true, false, true).Count > 0 ? 1 : 0);
            }
        }

        // Display notifications tab ?
        bool showNotificationsTab = LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Notifications, ModuleEntry.NOTIFICATIONS);
        int notificationsTab = Convert.ToInt32(showNotificationsTab);

        // Display languages tab ?
        bool showLanguagesTab = LicenseKeyInfoProvider.IsFeatureAvailable(FeatureEnum.Multilingual);
        int languagesTab = Convert.ToInt32(showLanguagesTab);

        // Display change password tab ?
        bool showChangePasswordTab = !RequestHelper.IsWindowsAuthentication() || (!ui.UserIsExternal && !ui.UserIsDomain);
        int changePasswordTab = Convert.ToInt32(showChangePasswordTab);

        bool showMembershipTab = LicenseKeyInfoProvider.IsFeatureAvailable(FeatureEnum.Membership);

        // Is E-commerce on site? => show department tab
        bool ecommerceOnSite = false;
        if (CMSContext.CurrentSiteName != null)
        {
            // Check if E-commerce module is installed
            ecommerceOnSite = ModuleEntry.IsModuleLoaded(ModuleEntry.ECOMMERCE) && ResourceSiteInfoProvider.IsResourceOnSite("CMS.Ecommerce", CMSContext.CurrentSiteName);
        }

        int departmentTab = Convert.ToInt32(ecommerceOnSite);
        int generalTabsCount = (CMSContext.CurrentUser.IsGlobalAdministrator) ? 8 : 7;

        string[,] tabs = new string[generalTabsCount + changePasswordTab + departmentTab + customFieldsTab + notificationsTab + languagesTab + 1, 4];

        int lastTabIndex = 0;
        tabs[lastTabIndex, 0] = generalString;
        tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'general_tab8');";
        tabs[lastTabIndex, 2] = "User_Edit_General.aspx?userid=" + userId + "&siteid=" + SiteID;
        lastTabIndex++;

        if (showChangePasswordTab)
        {
            tabs[lastTabIndex, 0] = passwordString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'password_tab');";
            tabs[lastTabIndex, 2] = "User_Edit_Password.aspx?userid=" + userId + "&siteid=" + SiteID;
            lastTabIndex++;
        }

        tabs[lastTabIndex, 0] = GetString("user.edit.settings");
        tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'usersettings_tab');";
        tabs[lastTabIndex, 2] = "User_Edit_Settings.aspx?userid=" + userId + "&siteid=" + SiteID;

        if (customFieldsTab > 0)
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = customFieldsString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'CustomFields_tab');";
            tabs[lastTabIndex, 2] = "User_Edit_CustomFields.aspx?userid=" + userId + "&siteid=" + SiteID;
        }

        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            int sitesCount = SiteInfoProvider.GetSitesCount();
            if (sitesCount > 0)
            {
                lastTabIndex++;
                tabs[lastTabIndex, 0] = sitesString;
                tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'sites_tab');";
                tabs[lastTabIndex, 2] = "User_Edit_Sites.aspx?userid=" + userId + "&siteid=" + SiteID;
            }

            lastTabIndex++;
            tabs[lastTabIndex, 0] = rolesString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'roles_tab');";
            tabs[lastTabIndex, 2] = "User_Edit_Roles.aspx?userid=" + userId + "&siteid=" + SiteID;
        }
        else
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = rolesString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'roles_tab');";
            tabs[lastTabIndex, 2] = "User_Edit_Roles.aspx?userid=" + userId + "&siteid=" + SiteID;
        }

        // Is e-commerce on site? => show department tab OR feature available
        if (ecommerceOnSite)
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = departmentsString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'departments_tab');";
            tabs[lastTabIndex, 2] = ResolveUrl("~/CMSModules/Ecommerce/Pages/Administration/Users/User_Edit_Departments.aspx") + "?userid=" + userId + "&siteid=" + SiteID;
        }

        if (showNotificationsTab)
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = notificationsString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'notifications_tab');";
            tabs[lastTabIndex, 2] = ResolveUrl("~/CMSModules/Notifications/Administration/Users/User_Edit_Notifications.aspx") + "?userid=" + userId + "&siteid=" + SiteID;
        }

        lastTabIndex++;
        tabs[lastTabIndex, 0] = categoriesString;
        tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'categories_tab');";
        tabs[lastTabIndex, 2] = "User_Edit_Categories.aspx?userid=" + userId + "&siteid=" + SiteID;

        bool showFriendsTab = LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Friends, ModuleEntry.COMMUNITY);
        if (showFriendsTab)
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = friendsString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'friends_tab');";
            tabs[lastTabIndex, 2] = ResolveUrl("~/CMSModules/Friends/Administration/Users/User_Edit_Friends_Frameset.aspx?userid=" + userId + "&siteid=" + SiteID);
        }

        lastTabIndex++;
        tabs[lastTabIndex, 0] = subscriptionsString;
        tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'user_subscriptions_tab');";
        tabs[lastTabIndex, 2] = "User_Edit_Subscriptions.aspx?userid=" + userId + "&siteid=" + SiteID;

        if (showLanguagesTab)
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = languagesString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'user_languages_tab');";
            tabs[lastTabIndex, 2] = "User_Edit_Languages.aspx?userid=" + userId + "&siteid=" + SiteID;
        }

        if (showMembershipTab)
        {
            lastTabIndex++;
            tabs[lastTabIndex, 0] = membershipString;
            tabs[lastTabIndex, 1] = "SetHelpTopic('helpTopic', 'user_membership_tab');";
            tabs[lastTabIndex, 2] = "User_Edit_Membership.aspx?userid=" + userId + "&siteid=" + SiteID;
        }

        // Object relationships
        //if (TypeInfo.AllowObjectRelationships)
        //{
        //    lastTabIndex++;
        //    tabs[lastTabIndex, 0] = GetString("General.Relationships");
        //    tabs[lastTabIndex, 2] = ResolveUrl("~/CMSModules/AdminControls/Pages/ObjectRelationships.aspx?objectid=" + userId + "&objecttype=cms.user&siteid=" + siteId);
        //}

        CurrentMaster.Tabs.UrlTarget = "content";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
