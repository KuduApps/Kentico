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

// Set edited object
[EditedObject("alternativeform.form", "altformid")]

// Set tabs
[Tabs(3, "altFormsContent")]
[Tab(0, "general.general", "AlternativeForms_Edit_General.aspx?altformid={%EditedObject.FormID%}&saved={?saved?}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_general')")]
[Tab(1, "general.fields", "AlternativeForms_Fields.aspx?altformid={%EditedObject.FormID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_fields')")]
[Tab(2, "general.layout", "AlternativeForms_Layout.aspx?altformid={%EditedObject.FormID%}", Javascript = "SetHelpTopic('helpTopic','alternative_forms_layout')")]

// Set breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "altforms.listlink", "~/CMSModules/DocumentTypes/Pages/AlternativeForms/AlternativeForms_List.aspx?classid={%EditedObject.FormClassID%}", "content")]
[Breadcrumb(1, Text = "{%EditedObject.FormDisplayName%}")]

// Set help
[Help("alternative_forms_general", "helpTopic")]

public partial class CMSModules_DocumentTypes_Pages_AlternativeForms_AlternativeForms_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
