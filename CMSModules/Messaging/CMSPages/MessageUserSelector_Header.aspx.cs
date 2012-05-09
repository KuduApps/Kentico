using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Messaging_CMSPages_MessageUserSelector_Header : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Add styles
        RegisterDialogCSSLink();

        CurrentMaster.Title.TitleText = GetString("Messaging.MessageUserSelector.HeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_User/object.png");
        CurrentMaster.FrameResizer.Visible = false;

        if (!CMSContext.CurrentUser.IsPublic())
        {
            string[,] tabs = new string[3, 4];
            int selectedTab = 2;
            CurrentMaster.Tabs.Visible = true;

            if (QueryHelper.GetString("showtab", String.Empty).ToLower() != "search")
            {
                selectedTab = 0;
                // ContactList tab
                tabs[0, 0] = GetString("Messaging.MessageUserSelector.ContactList");
                tabs[0, 2] = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/CMSPages/MessageUserSelector_ContactList.aspx") + "?hidid=" +
                    QueryHelper.GetText("hidid", String.Empty) +
                    "&mid=" +
                    QueryHelper.GetText("mid", String.Empty);

                // Show only if community module is present
                if (ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY) && UIHelper.IsFriendsModuleEnabled(CMSContext.CurrentSiteName))
                {
                    // Friends tab
                    tabs[1, 0] = GetString("friends.friends");
                    tabs[1, 2] = CMSContext.ResolveDialogUrl("~/CMSModules/Friends/CMSPages/MessageUserSelector_FriendsList.aspx") + "?hidid=" +
                            QueryHelper.GetText("hidid", String.Empty) +
                            "&mid=" +
                            QueryHelper.GetText("mid", String.Empty);
                }
            }

            // Search tab
            tabs[2, 0] = GetString("general.search");
            tabs[2, 2] = CMSContext.ResolveDialogUrl("~/CMSModules/Messaging/CMSPages/MessageUserSelector_Search.aspx") + "?refresh=" +
                QueryHelper.GetText("refresh", String.Empty) +
                "&hidid=" +
                QueryHelper.GetText("hidid", String.Empty) +
                "&mid=" +
                QueryHelper.GetText("mid", String.Empty);

            CurrentMaster.Tabs.Tabs = tabs;
            CurrentMaster.Tabs.SelectedTab = selectedTab;
            CurrentMaster.Tabs.UrlTarget = "MessageUserSelectorContent";
        }
    }
}
