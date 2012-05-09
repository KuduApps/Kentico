using System;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_RecycleBin_Pages_ViewVersion : CMSDeskPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Context help is not shown if comparison is disabled
        bool noCompare = QueryHelper.GetBoolean("noCompare", false);
        if (!noCompare)
        {
            CurrentMaster.Title.HelpName = "helpTopic";
            CurrentMaster.Title.HelpTopicName = "versions_viewversion";
        }
        CurrentMaster.Title.TitleText = GetString("RecycleBin.ViewVersion");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/viewversion.png");

        // Register tooltip script
        ScriptHelper.RegisterTooltip(Page);

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        RequireSite = false;
    }

    #endregion
}
