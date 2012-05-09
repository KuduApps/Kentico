using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_CustomTable_Edit_Sites : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get classID from querystring
        int classId = QueryHelper.GetInteger("customtableid", 0);
        if (classId > 0)
        {
            ClassSites.TitleString = GetString("customtable.edit.selectsite");
            ClassSites.ClassId = classId;
        }
    }
}
