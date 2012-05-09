using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Groups_Tools_Group_Edit_Header : CMSGroupPage
{
    protected int groupId = 0;
    protected string groupDisplayName = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the group ID and the group InfoObject
        groupId = QueryHelper.GetInteger("groupid", 0);
        GroupInfo gi = GroupInfoProvider.GetGroupInfo(groupId);
        if (gi != null)
        {
            groupDisplayName = HTMLHelper.HTMLEncode(gi.GroupDisplayName);
        }

        // Page title
        this.CurrentMaster.Title.TitleText = GetString("Group.EditHeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Community_Group/object.png");

        this.CurrentMaster.Title.HelpTopicName = "group_general";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Pagetitle breadcrumbs		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Group.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Group_List.aspx";
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = groupDisplayName;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        // Tabs
        string[,] tabs = new string[9, 4];
        tabs[0, 0] = GetString("General.General");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'group_general');";
        tabs[0, 2] = "Group_Edit_General.aspx?groupID=" + groupId;
        tabs[1, 0] = GetString("General.Security");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'group_security');";
        tabs[1, 2] = "Security/Security.aspx?groupID=" + groupId;
        tabs[2, 0] = GetString("Group.Members");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'group_members_list');";
        tabs[2, 2] = "Members/Member_List.aspx?groupID=" + groupId;

        if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.Roles", CMSContext.CurrentSiteName))
        {
            tabs[3, 0] = GetString("general.roles");
            tabs[3, 1] = "SetHelpTopic('helpTopic', 'group_roles_list');";
            tabs[3, 2] = "Roles/Role_List.aspx?groupID=" + groupId;
        }

        if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.Forums", CMSContext.CurrentSiteName))
        {
            tabs[4, 0] = GetString("group_general.forums");
            tabs[4, 1] = "SetHelpTopic('helpTopic', 'forum_list');";
            tabs[4, 2] = "Forums/Groups/ForumGroups_List.aspx?groupid=" + groupId;
        }

        if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.MediaLibrary", CMSContext.CurrentSiteName))
        {
            tabs[5, 0] = GetString("Group.MediaLibrary");
            tabs[5, 1] = "SetHelpTopic('helpTopic', 'library_list');";
            tabs[5, 2] = "MediaLibrary/Library_List.aspx?groupid=" + groupId;
        }

        if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.MessageBoards", CMSContext.CurrentSiteName))
        {
            tabs[6, 0] = GetString("Group.MessageBoards");
            tabs[6, 1] = "SetHelpTopic('helpTopic', 'group_messageboard');";
            tabs[6, 2] = "MessageBoards/Boards_Default.aspx?groupid=" + groupId;
        }

        if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.Polls", CMSContext.CurrentSiteName))
        {
            tabs[7, 0] = GetString("Group.Polls");
            tabs[7, 1] = "SetHelpTopic('helpTopic', 'polls_list');";
            tabs[7, 2] = "Polls/Polls_List.aspx?groupID=" + groupId;
        }

        // Check whether license for project management is avilable
        // if no hide project management tab
        if (LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ProjectManagement))
        {
            // Check site availability
            if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.ProjectManagement", CMSContext.CurrentSiteName))
            {
                tabs[8, 0] = ResHelper.GetString("pm.project.list");
                tabs[8, 1] = "SetHelpTopic('helpTopic', 'pm_projects');";
                tabs[8, 2] = "ProjectManagement/Project/List.aspx?groupid=" + groupId;
            }
        }

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "content";
    }
}
