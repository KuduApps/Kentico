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
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_AlternativeForms_AlternativeForms_Fields : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.BodyClass += " FieldEditorBody";
        int altFormId = QueryHelper.GetInteger("altformid", 0);

        altFormFieldEditor.Mode = FieldEditorModeEnum.ClassFormDefinition;
        altFormFieldEditor.AlternativeFormID = altFormId;
        altFormFieldEditor.DisplayedControls = FieldEditorControlsEnum.DocumentTypes;
        altFormFieldEditor.EnableSystemFields = true;
    }
}
