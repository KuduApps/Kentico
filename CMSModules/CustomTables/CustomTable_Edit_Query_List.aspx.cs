using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_CustomTable_Edit_Query_List : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int customTableId = QueryHelper.GetInteger("customtableid", 0);

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("customtable.edit.newquery");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("CustomTable_Edit_Query_Edit.aspx?customtableid=" + customTableId);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Query/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        // Set the query editor control
        classEditQuery.ClassID = customTableId;
        classEditQuery.EditPageUrl = "CustomTable_Edit_Query_Edit.aspx";
    }
}
