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
using CMS.MessageBoard;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Board_Header : CMSGroupMessageBoardsPage
{
    #region "Private fields"

    private int mBoardId = 0;
    private int mGroupId = 0;

    /// <summary>
    /// Flag when used from /CMSModules/PortalEngine/UI/Content/Properties/Advanced
    /// </summary>
    private bool changeMaster = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the current board.
    /// </summary>
    public int BoardID
    {
        get
        {
            if (this.mBoardId == 0)
            {
                this.mBoardId = QueryHelper.GetInteger("boardid", 0);
            }

            return this.mBoardId;
        }
        set
        {
            this.mBoardId = value;
        }
    }

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        // External call
        changeMaster = QueryHelper.GetBoolean("changemaster", false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);

        // Intialize the control
        SetupControl();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        InitalizeMenu();

        if (!changeMaster)
        {
            InitializeBreadcrumb();
        }
    }


    /// <summary>
    /// Initializes the breadcrumb header element of the master page.
    /// </summary>
    private void InitializeBreadcrumb()
    {
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("board.header.messageboards");
        breadcrumbs[0, 1] = "~/CMSModules/Groups/Tools/MessageBoards/Boards/Board_List.aspx" + ((this.mGroupId > 0) ? "?groupid=" + this.mGroupId : "");
        breadcrumbs[0, 2] = "_parent";
        if (this.BoardID > 0)
        {
            breadcrumbs[1, 0] = BoardInfoProvider.GetBoardInfo(this.BoardID).BoardDisplayName;
        }
        else
        {
            breadcrumbs[1, 0] = GetString("board.header.newboard");
        }

        breadcrumbs[1, 1] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "board_edit_messages";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeMenu()
    {
        string changeMasterStr = changeMaster ? "1" : "0";

        // Collect tabs data
        string[,] tabs = new string[5, 4];
        tabs[0, 0] = GetString("board.header.messages");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'board_edit_messages');";
        tabs[0, 2] = "../Messages/Message_List.aspx?changemaster=" + changeMasterStr + "&boardid=" + this.BoardID.ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : "");

        tabs[1, 0] = GetString("general.general");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'board_edit_general');";
        tabs[1, 2] = "Board_Edit_General.aspx?changemaster=" + changeMasterStr + "&boardid=" + this.BoardID.ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : "");

        tabs[2, 0] = GetString("board.header.subscriptions");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'board_edit_subscriptions');";
        tabs[2, 2] = "Board_Edit_Subscriptions.aspx?changemaster=" + changeMasterStr + "&boardid=" + this.BoardID.ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : "");

        tabs[3, 0] = GetString("board.header.moderators");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'board_edit_moderators');";
        tabs[3, 2] = "Board_Edit_Moderators.aspx?changemaster=" + changeMasterStr + "&boardid=" + this.BoardID.ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : "");

        tabs[4, 0] = GetString("board.header.security");
        tabs[4, 1] = "SetHelpTopic('helpTopic', 'board_edit_security');";
        tabs[4, 2] = "Board_Edit_Security.aspx?changemaster=" + changeMasterStr + "&boardid=" + this.BoardID.ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : "");


        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "boardEditContent";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
