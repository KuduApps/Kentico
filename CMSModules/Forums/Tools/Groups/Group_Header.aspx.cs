using System;

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Groups_Group_Header : CMSForumsPage
{
    protected int groupId;
    protected int siteId;


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentForumGroup = GetString("");

        groupId = QueryHelper.GetInteger("groupid", 0);
        if (groupId > 0)
        {
            ForumGroupInfo group = ForumGroupInfoProvider.GetForumGroupInfo(groupId);
            if (group != null)
            {
                currentForumGroup = HTMLHelper.HTMLEncode(group.GroupDisplayName);

                this.InitalizeMasterPage(currentForumGroup);

                InitalizeMenu();
            }
        }
    }


    /// <summary>
    /// Initializes Master page properties.
    /// </summary>
    protected void InitalizeMasterPage(string currentForumGroup)
    {
        // Initialize title and help
        this.Title = "Group header";
        this.CurrentMaster.Title.HelpTopicName = "forum_list2";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initialize title label
        this.CurrentMaster.Title.TitleText = GetString("Group_General.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Forums_ForumGroup/object.png");

        // Initialize breadcrumbs

        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("Forums.ForumGroups");
        tabs[0, 1] = "~/CMSModules/Forums/Tools/Groups/Group_List.aspx";
        tabs[0, 2] = "_parent";
        tabs[1, 0] = currentForumGroup;
        tabs[1, 1] = "";
        tabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = tabs;
    }


    /// <summary>
    /// Initializes user edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string generalString = GetString("general.general");
        string forumsString = GetString("Group_General.Forums");
        string viewString = GetString("general.view");

        string[,] tabs = new string[3, 4];
        tabs[0, 0] = forumsString;
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'forum_list2');";
        tabs[0, 2] = "../Forums/Forum_List.aspx?groupid=" + groupId;
        tabs[1, 0] = generalString;
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'forum_group_general_tab');";
        tabs[1, 2] = "Group_General.aspx?groupid=" + groupId;
        tabs[2, 0] = viewString;
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'view_tab');";
        tabs[2, 2] = "Group_View.aspx?groupid=" + groupId;

        this.CurrentMaster.Tabs.UrlTarget = "groupsContent";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }
}
