using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_TagGroups_Pages_Development_TagGroup_Edit_Tags : SiteManagerPage
{
    #region "Variables"

    private int groupId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the grid view                
        gridTags.ZeroRowsText = GetString("tags.taggroup_edit_tags.notags");
        gridTags.OnExternalDataBound += gridTags_OnExternalDataBound;
        gridTags.OnAction += gridTags_OnAction;

        // Look for group ID in the query string
        groupId = QueryHelper.GetInteger("groupid", 0);

        gridTags.WhereCondition = "(TagGroupID = " + groupId + ")";
    }

    #endregion


    #region "UniGrid events"

    protected object gridTags_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "tagname":
                return HTMLHelper.HTMLEncode(Convert.ToString(parameter));
        }
        return parameter;
    }


    protected void gridTags_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "viewdocuments":
                int tagId = ValidationHelper.GetInteger(actionArgument, 0);
                URLHelper.Redirect("Tags_Documents.aspx?groupid=" + groupId + "&siteid=" + QueryHelper.GetInteger("siteid", 0) + "&tagid=" + tagId);
                break;
        }
    }

    #endregion
}
