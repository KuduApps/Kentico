using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Query_List : SiteManagerPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int documentTypeId = QueryHelper.GetInteger("documenttypeid", 0);

        // New item link
        string[,] actions = new string[1, 6];

        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("DocumentType_Edit_Query_List.btnNew");
        actions[0, 3] = ResolveUrl(string.Format("DocumentType_Edit_Query_Edit.aspx?documenttypeid={0}", documentTypeId));
        actions[0, 5] = GetImageUrl("Objects/CMS_Query/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;

        // Set the query editor control
        this.classEditQuery.ClassID = documentTypeId;
        this.classEditQuery.EditPageUrl = "DocumentType_Edit_Query_Edit.aspx";
    }

    #endregion
}