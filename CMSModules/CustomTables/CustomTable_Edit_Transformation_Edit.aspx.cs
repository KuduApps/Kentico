using System;

using CMS.UIControls;
using CMS.GlobalHelper;

// Set edited object
[EditedObject("cms.transformation", "transformationid", "CustomTable_Edit_Transformation_Frameset.aspx")]

public partial class CMSModules_CustomTables_CustomTable_Edit_Transformation_Edit : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (TabMode && (QueryHelper.GetInteger("saved", 0) == 1))
        {
            // Reload header if changes were saved
            ScriptHelper.RefreshTabHeader(Page, null);
        }
    }
}
