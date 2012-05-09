using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_RelationshipNames_RelationshipName_List : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("RelationshipNames.RelationshipNames");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_RelationshipName/object.png");

        CurrentMaster.Title.HelpTopicName = "relationship_names_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Prepare the new class header element
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("RelationshipNames.NewRelationshipName");
        actions[0, 3] = "~/CMSModules/RelationshipNames/RelationshipName_New.aspx";
        actions[0, 5] = GetImageUrl("Objects/CMS_RelationshipName/add.png");

        CurrentMaster.HeaderActions.Actions = actions;

        UniGridRelationshipNames.OnAction += new OnActionEventHandler(UniGridRelationshipNames_OnAction);
        UniGridRelationshipNames.OnExternalDataBound += UniGridRelationshipNames_OnExternalDataBound;
        UniGridRelationshipNames.ZeroRowsText = GetString("general.nodatafound");
    }

    #endregion


    #region "Grid events"

    /// <summary>
    /// Handles the UniGrid's OnExternalDataBound event.
    /// </summary>
    protected object UniGridRelationshipNames_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string result = string.Empty;
        switch (sourceName.ToLower())
        {
            case "type":
                string value = ValidationHelper.GetString(parameter, string.Empty);
                if ((value == string.Empty) || (value.Contains(CMSObjectHelper.GROUP_DOCUMENTS)))
                {
                    result = GetString("RelationshipNames.Documents");
                }
                else if (value.Contains(CMSObjectHelper.GROUP_OBJECTS))
                {
                    result = GetString("RelationshipNames.Objects");
                }

                break;
        }
        return result;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridRelationshipNames_OnAction(string actionName, object actionArgument)
    {
        int relationshipNameId = Convert.ToInt32(actionArgument);
        if (actionName == "edit")
        {
            URLHelper.Redirect("RelationshipName_Edit.aspx?relationshipnameid=" + relationshipNameId);
        }

        else if (actionName == "delete")
        {
            RelationshipNameInfoProvider.DeleteRelationshipName(relationshipNameId);
        }
    }

    #endregion
}
