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

public partial class CMSModules_MessageBoards_CMSPages_Message_Edit : CMSLiveModalPage
{
    private int mBoardId = 0;
    private int mMessageId = 0;


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this.mBoardId = QueryHelper.GetInteger("messageboardid", 0);
        this.mMessageId = QueryHelper.GetInteger("messageId", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.messageEditElem.AdvancedMode = true;
        this.messageEditElem.CheckFloodProtection = true;
        this.messageEditElem.MessageID = mMessageId;
        this.messageEditElem.MessageBoardID = mBoardId;
        this.messageEditElem.OnBeforeMessageSaved += new OnBeforeMessageSavedEventHandler(messageEditElem_OnBeforeMessageSaved);
        this.messageEditElem.OnAfterMessageSaved += new OnAfterMessageSavedEventHandler(messageEditElem_OnAfterMessageSaved);

        // initializes page title control		
        if (this.mMessageId > 0)
        {
            this.CurrentMaster.Title.TitleText = GetString("Board.MessageEdit.title");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Board_Message/object.png");
        }
        else
        {
            this.CurrentMaster.Title.TitleText = GetString("Board.MessageNew.title");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Board_Message/new.png");
        }

        this.CurrentMaster.Title.HelpTopicName = "messages_edit";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        if (!URLHelper.IsPostback())
        {
            this.messageEditElem.ReloadData();
        }
    }


    void messageEditElem_OnAfterMessageSaved(BoardMessageInfo message)
    {
        int queryMarkIndex = this.Request.RawUrl.IndexOf('?');
        string filterParams = this.Request.RawUrl.Substring(queryMarkIndex);

        this.ltlScript.Text = ScriptHelper.GetScript("wopener.RefreshBoardList('" + filterParams + "'); window.close();");
    }


    void messageEditElem_OnBeforeMessageSaved()
    {
        bool isOwner = false;

        BoardInfo board = BoardInfoProvider.GetBoardInfo(messageEditElem.MessageBoardID);
        if (board != null)
        {
            // Check if the current user is allowed to modify the message
            isOwner = BoardInfoProvider.IsUserAuthorizedToManageMessages(board);
        }

        if (!isOwner && !CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.MessageBoards", "Modify"))
        {
            RedirectToAccessDenied(GetString("board.messageedit.notallowed"));
        }
    }
}
