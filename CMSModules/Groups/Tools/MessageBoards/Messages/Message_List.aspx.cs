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

public partial class CMSModules_Groups_Tools_MessageBoards_Messages_Message_List : CMSGroupMessageBoardsPage
{
    #region "Variables"

    private int mBoardId = 0;
    private int mGroupId = 0;
    private BoardInfo boardObj = null;

    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this.mBoardId = QueryHelper.GetInteger("boardId", 0);

        boardObj = BoardInfoProvider.GetBoardInfo(this.mBoardId);
        if (boardObj != null)
        {
            this.mGroupId = boardObj.BoardGroupID;

            // Check whether edited board belongs to any group
            if (this.mGroupId == 0)
            {
                EditedObject = null;
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.messageList.BoardID = mBoardId;
        this.messageList.GroupID = mGroupId;
        this.messageList.EditPageUrl = "~/CMSModules/Groups/Tools/MessageBoards/Messages/Message_Edit.aspx";
        this.messageList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(messageList_OnCheckPermissions);
        this.messageList.OnAction += new CommandEventHandler(messageList_OnAction);

        if (mBoardId > 0)
        {
            // New message link
            string[,] actions = new string[1, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("Board.MessageList.NewMessage");
            actions[0, 2] = "modalDialog('" + ResolveUrl("~/CMSModules/Groups/Tools/MessageBoards/Messages/Message_Edit.aspx") + "?boardId=" + this.mBoardId + "', 'MessageEdit', 500, 400); return false;";
            actions[0, 3] = "#";
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("CMSModules/CMS_MessageBoards/addmessage.png");
            this.CurrentMaster.HeaderActions.Actions = actions;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!URLHelper.IsPostback()) 
        {
            this.messageList.ReloadData();
        }
    }


    void messageList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit":

                string[] arguments = e.CommandArgument as string[];
                URLHelper.Redirect("Message_Edit.aspx?boardId=" + this.mBoardId + "&messageId=" + arguments[1].ToString() + arguments[0].ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : ""));
                break;

            default:
                break;
        }
    }


    void messageList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check 'Manage' permission
        CheckPermissions(messageList.GroupID, CMSAdminControl.PERMISSION_MANAGE);
    }
}
