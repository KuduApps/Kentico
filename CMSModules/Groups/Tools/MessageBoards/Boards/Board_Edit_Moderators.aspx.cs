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

using CMS.MessageBoard ;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Board_Edit_Moderators : CMSGroupMessageBoardsPage
{
    #region "Variables"

    int groupId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        int boardId = QueryHelper.GetInteger("boardid", 0);

        BoardInfo boardObj = BoardInfoProvider.GetBoardInfo(boardId);
        if (boardObj != null)
        {
            groupId = boardObj.BoardGroupID;

            // Check whether edited board belongs to any group
            if (groupId == 0)
            {
                EditedObject = null;
            }
        }   

        this.boardModerators.BoardID = boardId;

        this.boardModerators.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(boardModerators_OnCheckPermissions);
    }

    #endregion


    #region "Events"

    void boardModerators_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }

    #endregion
}
