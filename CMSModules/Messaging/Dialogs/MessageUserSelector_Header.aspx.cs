using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

public partial class CMSModules_Messaging_Dialogs_MessageUserSelector_Header : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Messaging);

        CurrentMaster.Title.TitleText = GetString("Messaging.MessageUserSelector.HeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("/Objects/CMS_User/object.png");
        CurrentMaster.FrameResizer.Visible = false;

        if (!CMSContext.CurrentUser.IsPublic())
        {
            CurrentMaster.Tabs.Visible = true;
            bool communityModuleLoaded = ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY);
            bool showOnlySearch = (QueryHelper.GetString("showtab", String.Empty).ToLower() != "search");

            int tabCount = 1;
            int tabIndex = 0;
            if (showOnlySearch)
            {
                tabCount++;
                if (communityModuleLoaded)
                {
                    tabCount++;
                }
            }
            string[,] tabs = new string[tabCount, 4];


            if (showOnlySearch)
            {
                // ContactList tab
                tabs[tabIndex, 0] = GetString("Messaging.MessageUserSelector.ContactList");
                tabs[tabIndex, 2] = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/Dialogs/MessageUserSelector_ContactList.aspx") + "?hidid=" +
                    QueryHelper.GetText("hidid", String.Empty) +
                    "&mid=" +
                    QueryHelper.GetText("mid", String.Empty);

                tabIndex++;

                // Show only if community module is present
                if (communityModuleLoaded && UIHelper.IsFriendsModuleEnabled(CMSContext.CurrentSiteName))
                {
                    // Friends tab
                    tabs[tabIndex, 0] = GetString("friends.friends");
                    tabs[tabIndex, 2] = CMSContext.ResolveDialogUrl("~/CMSModules/Friends/Dialogs/MessageUserSelector_FriendsList.aspx") + "?hidid=" +
                            QueryHelper.GetText("hidid", String.Empty) +
                            "&mid=" +
                            QueryHelper.GetText("mid", String.Empty);
                    tabIndex++;
                }

            }

            // Search tab
            tabs[tabIndex, 0] = GetString("general.search");
            tabs[tabIndex, 2] = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/Dialogs/MessageUserSelector_Search.aspx") + "?refresh=" +
                QueryHelper.GetText("refresh", String.Empty) +
                "&hidid=" +
                QueryHelper.GetText("hidid", String.Empty) +
                "&mid=" +
                QueryHelper.GetText("mid", String.Empty);

            CurrentMaster.Tabs.Tabs = tabs;
            CurrentMaster.Tabs.SelectedTab = 0;
            CurrentMaster.Tabs.UrlTarget = "MessageUserSelectorContent";
        }
    }
}
