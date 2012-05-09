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
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Header : CMSGroupMessageBoardsPage
{
    private int mGroupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Intialize the control
        SetupControl();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);

        // initializes breadcrumbs 		
        string[,] pageTitleTabs = new string[1, 3];
        pageTitleTabs[0, 0] = GetString("board.header.messageboards");
        pageTitleTabs[0, 1] = "";
        pageTitleTabs[0, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        CurrentMaster.Title.HelpTopicName = "messages_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        InitalizeMenu();
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeMenu()
    {
        // Collect tabs data
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("board.header.messages");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'messages_list');";
        tabs[0, 2] = "Messages/Message_List.aspx" + ((this.mGroupId > 0) ? "?groupid=" + this.mGroupId : "");

        tabs[1, 0] = GetString("board.header.boards");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'board_list');";
        tabs[1, 2] = "Boards/Board_List.aspx" + ((this.mGroupId > 0) ? "?groupid=" + this.mGroupId : "");

        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "boardsContent";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
