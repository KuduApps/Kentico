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

public partial class CMSModules_MessageBoards_Tools_Messages_Message_Edit : CMSModalPage
{
    private int mBoardId = 0;
    private int mMessageId = 0;


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Check permissions for CMS Desk -> Tools -> MessageBoards
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Tools", "MessageBoards"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Tools", "MessageBoards");
        }

        // Check permissions for MessageBoards -> Messages
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MessageBoards", "Messages"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MessageBoards", "Messages");
        }

        this.mBoardId = QueryHelper.GetInteger("boardId", 0);
        this.mMessageId = QueryHelper.GetInteger("messageId", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Read' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.messageboards", CMSAdminControl.PERMISSION_READ))
        {
            RedirectToAccessDenied("cms.messageboards", CMSAdminControl.PERMISSION_READ);
        }

        this.messageEditElem.IsLiveSite = false;
        this.messageEditElem.AdvancedMode = true;
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
    }


    void messageEditElem_OnAfterMessageSaved(BoardMessageInfo message)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("wopener.RefreshBoardList(); window.close();");
    }

    
    void messageEditElem_OnBeforeMessageSaved()
    {
        bool isOwner = false;

        BoardInfo board = BoardInfoProvider.GetBoardInfo(this.mBoardId);
        if (board != null)
        {
            // Check if the current user is allowed to modify the message
            isOwner = BoardInfoProvider.IsUserAuthorizedToManageMessages(board);
        }

        if (!isOwner && !CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.MessageBoards", CMSAdminControl.PERMISSION_MODIFY))
        {
            RedirectToAccessDenied("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY);
        }
    }
}
