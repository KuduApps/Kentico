using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.UIControls;

// Set help
[Help("alternative_forms_general", "helpTopic")]
// Set Edited object
[EditedObject("alternativeform.form", "altformid")]
// Set tabs
[Tabs(3, "altFormsContent")]
[Tab(0, "general.general", "Edit_General.aspx?altformid={%EditedObject.FormID%}&classid={%EditedObject.FormClassID%}&saved={?saved?}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_general')")]
[Tab(1, "general.fields", "Fields.aspx?altformid={%EditedObject.FormID%}&classid={%EditedObject.FormClassID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_fields')")]
[Tab(2, "general.layout", "Layout.aspx?altformid={%EditedObject.FormID%}&classid={%EditedObject.FormClassID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_layout')")]
// Set Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "altforms.listlink", "~/CMSModules/SystemTables/Pages/Development/AlternativeForms/List.aspx?classid={%EditedObject.FormClassID%}&altformid={%EditedObject.FormID%}", "content")]
[Breadcrumb(1, Text = "{%EditedObject.FormDisplayName%}")]
public partial class CMSModules_SystemTables_Pages_Development_AlternativeForms_Header : SiteManagerPage
{  
}
