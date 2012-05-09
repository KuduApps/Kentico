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

public partial class CMSModules_MessageBoards_Tools_Boards_Board_List : CMSMessageBoardBoardsPage
{
    private int mGroupId = 0;

    protected override void OnPreInit(EventArgs e)
    {
        if (this.mGroupId > 0)
        {
            this.Page.MasterPageFile = "~/CMSMasterPages/UI/SimplePage.master";
        }

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);

        this.boardList.IsLiveSite = false;
        this.boardList.GroupID = mGroupId;
        this.boardList.OnAction += new CommandEventHandler(boardList_OnAction);
    }


    void boardList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "edit":
                int boardId = ValidationHelper.GetInteger(e.CommandArgument, 0);

                // Create a target site URL and pass the category ID as a parameter
                string editUrl = "Board_Edit.aspx?boardid=" + boardId.ToString() + ((this.mGroupId > 0) ? "&groupid=" + this.mGroupId : "");
                URLHelper.Redirect(editUrl);
                break;

            default:
                break;
        }
    }
}
