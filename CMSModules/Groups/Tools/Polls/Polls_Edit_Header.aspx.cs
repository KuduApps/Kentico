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

using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Polls_Polls_Edit_Header : CMSGroupPollsPage
{
    protected int pollId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string pollDisplayName = "";

        // Get GroupID
        int groupId = QueryHelper.GetInteger("groupid", 0);

        // Get the pollID        
        pollId = QueryHelper.GetInteger("pollid", 0);
        PollInfo pi = PollInfoProvider.GetPollInfo(pollId);
        if (pi != null)
        {
            pollDisplayName = pi.PollDisplayName;
        }

        // Pagetitle breadcrumbs		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("group.polls.title");
        pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Polls/Polls_List.aspx?groupId=" + groupId;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = pollDisplayName;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        // Tabs
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("General.General");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tab4');";
        tabs[0, 2] = "Polls_Edit_General.aspx?pollID=" + pollId + "&groupId=" + groupId;
        tabs[1, 0] = GetString("group.polls.answers");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'answer_list');";
        tabs[1, 2] = "Polls_Edit_Answer_List.aspx?pollID=" + pollId + "&groupId=" + groupId;
        tabs[2, 0] = GetString("general.security");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'security3');";
        tabs[2, 2] = "Polls_Edit_Security.aspx?pollID=" + pollId + "&groupId=" + groupId;
        tabs[3, 0] = GetString("general.view");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'view');";
        tabs[3, 2] = "Polls_Edit_View.aspx?pollid=" + pollId + "&groupID=" + groupId;

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "content";

        this.CurrentMaster.Title.HelpTopicName = "general_tab4";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }
}
