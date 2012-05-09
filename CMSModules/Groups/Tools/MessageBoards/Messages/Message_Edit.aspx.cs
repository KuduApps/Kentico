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

public partial class CMSModules_Groups_Tools_MessageBoards_Messages_Message_Edit : CMSModalPage
{
    private int mBoardId = 0;
    private int mMessageId = 0;
    private int mGroupId = 0;
    private CurrentUserInfo cu = null;


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Check permissions for CMS Desk -> Tools -> Groups
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Tools", "Groups"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Tools", "Groups");
        }  

        this.mBoardId = QueryHelper.GetInteger("boardId", 0);
        this.mMessageId = QueryHelper.GetInteger("messageId", 0);
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Read' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_READ))
        {
            RedirectToCMSDeskAccessDenied("cms.groups", CMSAdminControl.PERMISSION_READ);
        }

        cu = CMSContext.CurrentUser;

        this.messageEditElem.IsLiveSite = false;
        this.messageEditElem.AdvancedMode = true;
        this.messageEditElem.MessageID = mMessageId;
        this.messageEditElem.MessageBoardID = mBoardId;

        this.messageEditElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(messageEditElem_OnCheckPermissions);

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


    void messageEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CheckLocalPermissions();
    }

    protected void CheckLocalPermissions()
    {
        int groupId = 0;
        int boardId = this.mBoardId;

        BoardMessageInfo bmi = BoardMessageInfoProvider.GetBoardMessageInfo(mMessageId);
        if (bmi != null)
        {
            boardId = bmi.MessageBoardID;
        }

        BoardInfo bi = BoardInfoProvider.GetBoardInfo(boardId);
        if (bi != null)
        {
            groupId = bi.BoardGroupID;
        }


        // Check 'Manage' permission
        if (!CMSContext.CurrentUser.IsGroupAdministrator(groupId))
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_MANAGE))
            {
                RedirectToCMSDeskAccessDenied("cms.groups", CMSAdminControl.PERMISSION_MANAGE);
            }
        }
    }


    void messageEditElem_OnAfterMessageSaved(BoardMessageInfo message)
    {
        int queryMarkIndex = this.Request.RawUrl.IndexOf('?');
        string filterParams = this.Request.RawUrl.Substring(queryMarkIndex);

        this.ltlScript.Text = ScriptHelper.GetScript("wopener.RefreshBoardList('" + filterParams + "');window.close();");
    }


    void messageEditElem_OnBeforeMessageSaved()
    {
        CheckLocalPermissions();
    }
}
