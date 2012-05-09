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
using CMS.SiteProvider;

public partial class CMSModules_SystemTables_Pages_Development_AlternativeForms_Fields : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get alternative form ID from URL
        int altFormId = QueryHelper.GetInteger("altformid", 0);
        CurrentMaster.BodyClass += " FieldEditorBody";

        // Get alternative form
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(altFormId);
        if (afi != null)
        {
            // Get name of the edited class
            string className = DataClassInfoProvider.GetClassName(afi.FormClassID);

            // Initialize field editor
            altFormFieldEditor.Mode = FieldEditorModeEnum.SystemTable;
            altFormFieldEditor.ShowFieldVisibility = (className.ToLower().Trim() == SiteObjectType.USER.ToLower());
            altFormFieldEditor.AlternativeFormID = altFormId;
            altFormFieldEditor.DisplayedControls = FieldEditorControlsEnum.SystemTables;
        }
    }
}
