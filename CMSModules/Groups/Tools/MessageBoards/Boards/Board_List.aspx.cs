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
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.MessageBoard;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Board_List : CMSGroupMessageBoardsPage
{
    private int mGroupId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        mGroupId = QueryHelper.GetInteger("groupid", 0);

        // Prevent display global messageboards
        if (mGroupId == 0)
        {
            mGroupId = -1;
        }

        boardList.IsLiveSite = false;
        boardList.GroupID = mGroupId;
        boardList.OnAction += new CommandEventHandler(boardList_OnAction);
        boardList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(boardList_OnCheckPermissions);
        boardList.GridName = "~/CMSModules/Groups/Tools/MessageBoards/Boards/Board_List.xml";
    }


    protected void boardList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit":
                int boardId = ValidationHelper.GetInteger(e.CommandArgument, 0);
                // Create a target site URL and pass the category ID as a parameter
                string editUrl = "Board_Edit.aspx?boardid=" + boardId.ToString() + ((mGroupId > 0) ? "&groupid=" + mGroupId : string.Empty);
                URLHelper.Redirect(editUrl);
                break;

            default:
                break;
        }
    }


    protected void boardList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check 'Manage' permission
        CheckPermissions(this.boardList.GroupID, CMSAdminControl.PERMISSION_MANAGE);
    }
}
