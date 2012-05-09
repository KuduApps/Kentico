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

public partial class CMSModules_MessageBoards_Tools_Boards_Board_Edit_General : CMSMessageBoardBoardsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get board ID
        int boardId = QueryHelper.GetInteger("boardId", 0);

        // Get board info and chceck whether it belongs to current site
        BoardInfo board = BoardInfoProvider.GetBoardInfo(boardId);
        if (board != null)
        {
            CheckMessageBoardSiteID(board.BoardSiteID);
        }

        // Set board info to editing control
        boardEdit.Board = board;
    }
}
