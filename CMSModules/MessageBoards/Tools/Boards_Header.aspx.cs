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
using CMS.UIControls;

[Tabs("CMS.MessageBoards", "", "boardsContent")]
public partial class CMSModules_MessageBoards_Tools_Boards_Header : CMSMessageBoardPage
{
    #region "Variables"

    private int mGroupId = 0;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Intialize the control
        SetupControl();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        // Set the page title when existing category is being edited
        this.CurrentMaster.Title.TitleText = GetString("board.header.messageboards");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Board_Board/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "CMS_MessageBoards_Messages";
    }

    #endregion
}
