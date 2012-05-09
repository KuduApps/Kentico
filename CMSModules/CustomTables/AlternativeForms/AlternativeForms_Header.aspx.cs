using System;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.UIControls;

// Set edited object
[EditedObject("alternativeform.form", "altformid")]

// Set page tabs
[Tabs(3, "altFormsContent")]
[Tab(0, "general.general", "AlternativeForms_Edit_General.aspx?altformid={%EditedObject.FormID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_general')")]
[Tab(1, "general.fields", "AlternativeForms_Fields.aspx?altformid={%EditedObject.FormID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_fields')")]
[Tab(2, "general.layout", "AlternativeForms_Layout.aspx?altformid={%EditedObject.FormID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_layout')")]

// Set breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "altforms.listlink", "AlternativeForms_List.aspx?classid={%EditedObject.FormClassID%}", "content")]
[Breadcrumb(1, Text = "{%EditedObject.FormDisplayName%}")]

// Set help
[Help("alternative_forms_general", "helpTopic")]

public partial class CMSModules_CustomTables_AlternativeForms_AlternativeForms_Header : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
