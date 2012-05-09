using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Development_WebTemplates_WebTemplate_List : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Web Templates - Web Template List";

        // Initialize master page
        InitializeMasterPage();

        UniGridModules.GridView.AllowSorting = false;
        UniGridModules.OnAction += UniGridModules_OnAction;
        UniGridModules.ZeroRowsText = GetString("general.nodatafound");
    }

    #endregion


    #region "Private methods"
    /// <summary>
    /// Initializes master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "web_templates_list";

        CurrentMaster.Title.TitleText = GetString("Administration-WebTemplate_List.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebTemplate/object.png");

        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Administration-WebTemplate_List.NewWebTemplate");
        actions[0, 3] = "~/CMSSiteManager/development/WebTemplates/WebTemplate_Edit.aspx";
        actions[0, 5] = GetImageUrl("Objects/CMS_WebTemplate/add.png");

        CurrentMaster.HeaderActions.Actions = actions;
    }

    #endregion


    #region "Unigrid events"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridModules_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("WebTemplate_Edit.aspx?webtemplateid=" + actionArgument);
        }
        else if (actionName == "delete")
        {
            WebTemplateInfoProvider.DeleteWebTemplateInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
        else if (actionName == "moveup")
        {
            // Move the item up in order
            WebTemplateInfoProvider.MoveTemplateUp(ValidationHelper.GetInteger(actionArgument, 0));
            UniGridModules.ReloadData();
        }
        else if (actionName == "movedown")
        {
            // Move the item down in order
            WebTemplateInfoProvider.MoveTemplateDown(ValidationHelper.GetInteger(actionArgument, 0));
            UniGridModules.ReloadData();
        }
    }

    #endregion
}
