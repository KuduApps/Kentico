using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_SystemTables_Pages_Development_SystemTable_Edit_Query_Edit : SiteManagerPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get class id from querystring, used when creating new query
        int classId = QueryHelper.GetInteger("classid", 0);
        queryEdit.ClassID = classId;
        queryEdit.IsSiteManager = true;

        string queryName = GetString("systbl.queryedit.newlink");

        queryEdit.RefreshPageURL = "Edit_Query_Edit.aspx";

        int queryId = QueryHelper.GetInteger("queryid", 0);
        if (queryId > 0)
        {
            // Get information of current query
            Query query = QueryProvider.GetQuery(queryId);

            if (query != null)
            {
                queryEdit.QueryID = queryId;
                classId = query.QueryClassId;
                queryName = query.QueryName;
            }
        }

        // Initializes page title
        string queries = GetString("systbl.queryedit.querylist");

        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = queries;
        breadcrumbs[0, 1] = ResolveUrl("Edit_Query_List.aspx?classid=" + classId.ToString());
        breadcrumbs[1, 0] = queryName;
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }

    #endregion
}