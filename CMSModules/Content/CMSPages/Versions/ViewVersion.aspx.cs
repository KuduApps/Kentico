using System;

using CMS.GlobalHelper;
using CMS.UIControls;


public partial class CMSModules_Content_CMSPages_Versions_ViewVersion : CMSLiveModalPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "versions_viewversion";
        CurrentMaster.Title.TitleText = GetString("Content.ViewVersion");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/viewversion.png");

        // Register tooltip script
        ScriptHelper.RegisterTooltip(Page);

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
    }

    #endregion
}

