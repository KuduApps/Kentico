using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_Content_CMSDesk_Properties_ViewVersion : CMSPropertiesPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        btnClose.Text = GetString("General.Close");

        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "versions_viewversion";
        CurrentMaster.Title.TitleText = GetString("Content.ViewVersion");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/viewversion.png");

        // Register tooltip script
        ScriptHelper.RegisterTooltip(Page);

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Redirect to information page when no UI elements displayed
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Versions"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Versions");
        }
    }

    #endregion
}
