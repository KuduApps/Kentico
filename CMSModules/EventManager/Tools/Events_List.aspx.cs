using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_EventManager_Tools_Events_List : CMSEventManagerPage
{
    protected override void OnPreRender(EventArgs e)
    {
        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("Events_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_EventManager/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "booking_system_list";

        eventList.ReloadData();
        base.OnPreRender(e);
    }
}
