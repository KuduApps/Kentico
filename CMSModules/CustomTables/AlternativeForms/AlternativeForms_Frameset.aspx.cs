using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_AlternativeForms_AlternativeForms_Frameset : CMSCustomTablesPage
{
    #region "Variables"

    protected int classId = 0;
    protected int altFormId = 0;
    protected int saved = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("classid", 0);
        altFormId = QueryHelper.GetInteger("altformid", 0);
        saved = QueryHelper.GetInteger("saved", 0);
    }
}
