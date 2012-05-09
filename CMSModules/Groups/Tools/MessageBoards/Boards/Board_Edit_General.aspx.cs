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
using CMS.MessageBoard;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Board_Edit_General : CMSGroupMessageBoardsPage
{
    #region "Variables"

    private BoardInfo boardObj = null;
    private int groupId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        boardObj = BoardInfoProvider.GetBoardInfo(QueryHelper.GetInteger("boardid", 0));
        if (boardObj != null)
        {
            groupId = boardObj.BoardGroupID;

            // Check whether edited board belongs to any group
            if (groupId == 0)
            {
                EditedObject = null;
            }
        }        
        
        this.boardEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(boardEdit_OnCheckPermissions);
    }

    #endregion


    #region "Events"
    
    void boardEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }

    #endregion
}
