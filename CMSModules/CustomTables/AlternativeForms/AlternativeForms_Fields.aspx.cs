using System;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.UIControls;

public partial class CMSModules_CustomTables_AlternativeForms_AlternativeForms_Fields : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int altFormId = QueryHelper.GetInteger("altformid", 0);
        CurrentMaster.BodyClass += " FieldEditorBody";

        altFormFieldEditor.Mode = FieldEditorModeEnum.CustomTable;
        altFormFieldEditor.AlternativeFormID = altFormId;
        altFormFieldEditor.DisplayedControls = FieldEditorControlsEnum.CustomTables;
    }
}
