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

[Security(Resource = "CMS.MessageBoards", UIElements = "Messages")]
public partial class CMSModules_MessageBoards_Tools_Messages_Message_List : CMSMessageBoardPage
{
    private int mBoardId = 0;
    private int mGroupId = 0;


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this.mBoardId = QueryHelper.GetInteger("boardId", 0);
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.messageList.IsLiveSite = false;
        this.messageList.BoardID = mBoardId;
        this.messageList.GroupID = mGroupId;
        this.messageList.OnAction += new CommandEventHandler(messageList_OnAction);

        if (mBoardId > 0)
        {
            // New message link
            string[,] actions = new string[1, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("Board.MessageList.NewMessage");
            actions[0, 2] = "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/MessageBoards/Tools/Messages/Message_Edit.aspx") + "?boardId=" + this.mBoardId + "&changemaster=" + QueryHelper.GetBoolean("changemaster", false) + "', 'MessageEdit', 500, 400); return false;";
            actions[0, 3] = "#";
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("CMSModules/CMS_MessageBoards/addmessage.png");
            this.CurrentMaster.HeaderActions.Actions = actions;
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
}
