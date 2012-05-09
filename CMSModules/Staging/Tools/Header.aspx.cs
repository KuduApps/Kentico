using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

[Tabs("CMS.Staging", "", "stagingContent")]
public partial class CMSModules_Staging_Tools_Header : CMSStagingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("Staging.HeaderCaption");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Staging/object.png");
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "CMS_Blog_Comments";
    }
}
