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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.LicenseProvider;
using CMS.UIControls;

public partial class CMSModules_MessageBoards_Tools_Boards_Board_Edit_Security : CMSMessageBoardBoardsPage
{
    #region "Variables"

    protected int boardId = 0;
    protected int groupId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get parametr from query string
        boardId = QueryHelper.GetInteger("boardid", 0);
        groupId = QueryHelper.GetInteger("groupid", 0);

        // Get board info and chceck whether it belongs to current site
        BoardInfo board = BoardInfoProvider.GetBoardInfo(boardId);
        if (board != null)
        {
            CheckMessageBoardSiteID(board.BoardSiteID);
        }

        boardSecurity.GroupID = groupId;

        this.boardSecurity.GroupID = groupId;
        this.boardSecurity.Board = board;
        this.boardSecurity.IsLiveSite = false;
    }
}
