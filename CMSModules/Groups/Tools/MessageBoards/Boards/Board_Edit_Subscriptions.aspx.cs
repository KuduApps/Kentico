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
using CMS.SiteProvider;
using CMS.MessageBoard;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Board_Edit_Subscriptions : CMSGroupMessageBoardsPage
{
    #region "Variables"
    
    private int boardId = 0;
    private int groupId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get current board ID
        boardId = QueryHelper.GetInteger("boardid", 0);

        BoardInfo boardObj = BoardInfoProvider.GetBoardInfo(boardId);
        if (boardObj != null)
        {
            groupId = boardObj.BoardGroupID;

            // Check whether edited board belongs to group
            if (groupId == 0)
            {
                EditedObject = null;
            }
        }

        this.boardSubscriptions.BoardID = boardId;
        this.boardSubscriptions.GroupID = groupId;
        this.boardSubscriptions.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(boardSubscriptions_OnCheckPermissions);
        this.boardSubscriptions.OnAction += new CommandEventHandler(boardSubscriptions_OnAction);

        // Initialize the master page
        InitializeMasterPage();
    }

    #endregion


    #region "Events"
    
    void boardSubscriptions_OnAction(object sender, CommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edit")
        {
            // Redirect to edit page with subscription ID specified
            URLHelper.Redirect("Board_Edit_Subscription_Edit.aspx?subscriptionid=" + e.CommandArgument.ToString() + "&boardid=" + boardId + 
                ((this.groupId > 0) ? "&groupid=" + this.groupId : ""));
        }  
    }


    void boardSubscriptions_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        BoardInfo bi = BoardInfoProvider.GetBoardInfo(boardId);
        if (bi != null)
        {
            groupId = bi.BoardGroupID;

            // Check whether edited board belongs to any group
            if (groupId == 0)
            {
                EditedObject = null;
            }
        }

        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Setup master page action element
        string[,] actions = new string[1,6];
        actions[0,0] = HeaderActions.TYPE_HYPERLINK;
        actions[0,1] = GetString("board.subscriptions.newitem");
        actions[0,3] = "~/CMSModules/Groups/Tools/MessageBoards/Boards/Board_Edit_Subscription_Edit.aspx?boardid=" + boardId.ToString() + "&groupid=" + groupId;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_MessageBoards/newsubscription.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }

    #endregion
}

