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

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Forums_Forum_Header : CMSGroupForumPage
{
    protected int forumId = 0;    
    private bool changeMaster = false;


    protected override void OnPreInit(EventArgs e)
    {
        // External call
        changeMaster = QueryHelper.GetBoolean("changemaster", false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentForum = "";
        int groupId = 0;

        forumId = ValidationHelper.GetInteger(Request.QueryString["forumid"], 0);
        ForumInfo forum = ForumInfoProvider.GetForumInfo(forumId);
        if (forum == null)
        {
            return;
        }
        currentForum = forum.ForumDisplayName;
        groupId = forum.ForumGroupID;

        this.InitalizeMasterPage(currentForum, groupId);

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }


    /// <summary>
    /// Initializes Master page properties.
    /// </summary>
    protected void InitalizeMasterPage(string currentForum, int groupId)
    {
        // Initialize title and help 
        this.Title = "Forums - Header";
        this.CurrentMaster.Title.HelpTopicName = "posts_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        if (!changeMaster)
        {
            // Initialize breadcrumbs
            string[,] tabs = new string[2, 3];
            tabs[0, 0] = GetString("forum_list.headercaption");
            tabs[0, 1] = "~/CMSModules/Groups/Tools/Forums/Forums/Forums_List.aspx?groupid=" + groupId;
            tabs[0, 2] = "groupsContent";
            tabs[1, 0] = HTMLHelper.HTMLEncode(currentForum);
            tabs[1, 1] = "";
            tabs[1, 2] = "";

            this.CurrentMaster.Title.Breadcrumbs = tabs;
        }
    }


    /// <summary>
    /// Initializes forum edit menu tabs.
    /// </summary>
    protected void InitalizeMenu()
    {
        string changeMasterStr = changeMaster ? "1" : "0";

        string[,] tabs = new string[6, 4];
        tabs[0, 0] = GetString("Forum_Edit.Posts");
        tabs[0, 2] = "../Posts/ForumPost_Frameset.aspx?changemaster=" + changeMasterStr + "&forumid=" + forumId.ToString();
        tabs[1, 0] = GetString("general.general");
        tabs[1, 2] = "Forum_General.aspx?changemaster=" + changeMasterStr + "&forumid=" + forumId.ToString();
        tabs[2, 0] = GetString("Forum_Edit.Subscriptions");
        tabs[2, 2] = "../Subscriptions/ForumSubscription_List.aspx?changemaster=" + changeMasterStr + "&forumid=" + forumId.ToString();
        tabs[3, 0] = GetString("Forum_Edit.Moderating");
        tabs[3, 2] = "Forum_Moderators.aspx?changemaster=" + changeMasterStr + "&forumid=" + forumId.ToString();
        tabs[4, 0] = GetString("general.Security");
        tabs[4, 2] = "Forum_Security.aspx?changemaster=" + changeMasterStr + "&forumid=" + forumId.ToString();
        tabs[5, 0] = GetString("general.view");
        tabs[5, 2] = "Forum_View.aspx?changemaster=" + changeMasterStr + "&forumid=" + forumId.ToString();

        if (!changeMaster)
        {
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'posts_tab');";
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'general_tab3');";
            tabs[2, 1] = "SetHelpTopic('helpTopic', 'subscriptions_tab2');";
            tabs[3, 1] = "SetHelpTopic('helpTopic', 'moderators_tab');";
            tabs[4, 1] = "SetHelpTopic('helpTopic', 'security_tab');";
            tabs[5, 1] = "SetHelpTopic('helpTopic', 'view_tab');";
        }

        this.CurrentMaster.Tabs.UrlTarget = "forumsContent";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }
}
