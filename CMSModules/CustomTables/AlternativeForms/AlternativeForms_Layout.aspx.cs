using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_AlternativeForms_AlternativeForms_Layout : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        layoutElem.FormType = CMSModules_AdminControls_Controls_Class_Layout.FORMTYPE_CUSTOMTABLE;
        layoutElem.ObjectID = QueryHelper.GetInteger("altformid", 0);
        layoutElem.IsAlternative = true;
    }
}
