using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_MessageBoards_Content_Properties_Header : CMSContentMessageBoardsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("board.header.messageboards");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_MessageBoards/module.png");
    }
}
