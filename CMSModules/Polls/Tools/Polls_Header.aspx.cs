using System;

using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;

public partial class CMSModules_Polls_Tools_Polls_Header : CMSPollsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the poll ID
        int pollId = QueryHelper.GetInteger("pollId", 0);
        PollInfo pi = PollInfoProvider.GetPollInfo(pollId);

        // Check global and site read permmision
        this.CheckPollsReadPermission(pi.PollSiteID);

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu(pi);
        }

        // Initialize the page title
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Polls_Edit.itemlistlink");
        breadcrumbs[0, 1] = "~/CMSModules/Polls/Tools/Polls_List.aspx?siteid=" + QueryHelper.GetInteger("siteid", 0);
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 0] = (pi != null ? pi.PollDisplayName : String.Empty);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.TitleText = GetString("Polls_Edit.headercaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Polls_Poll/object.png");
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "general_tab4";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu(PollInfo pi)
    {
        int pollId = 0;
        if (pi != null)
        {
            pollId = pi.PollID;
        }

        string[,] tabs = new string[5, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tab4');";
        tabs[0, 2] = "Polls_General.aspx?pollId=" + pollId;
        tabs[1, 0] = GetString("Polls_Edit.Answers");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'answer_list');";
        tabs[1, 2] = "Polls_Answer_List.aspx?pollId=" + pollId;
        tabs[2, 0] = GetString("general.security");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'security3');";
        tabs[2, 2] = "Polls_Security.aspx?pollId=" + pollId;

        int i = 3;
        if ((pi != null) && (pi.PollSiteID == 0))
        {
            tabs[i, 0] = GetString("general.sites");
            tabs[i, 1] = "SetHelpTopic('helpTopic', 'sites');";
            tabs[i, 2] = "Polls_Sites.aspx?pollId=" + pollId;
            i++;
        }

        tabs[i, 0] = GetString("general.view");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'view');";
        tabs[i, 2] = "Polls_View.aspx?pollId=" + pollId;

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "pollContent";
    }
}
