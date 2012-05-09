using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Forums_Content_Properties_Header : CMSForumsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("forum.header.forum");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Forums_Forum/object.png");
        this.CurrentMaster.FrameResizer.Visible = false;
    }
}
