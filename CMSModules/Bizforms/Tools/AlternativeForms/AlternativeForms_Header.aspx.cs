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
using CMS.CMSHelper;
using CMS.UIControls;

// Set edited object
[EditedObject("alternativeform.form", "altformid")]

// Set help
[Help("alternative_forms_general", "helpTopic")]

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_Header : CMSBizFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        int formId = QueryHelper.GetInteger("formid", 0);
        AlternativeFormInfo afi = EditedObject as AlternativeFormInfo;

        // Set Breadcrumbs
        InitBreadcrumbs(2);
        SetBreadcrumb(0, GetString("altforms.listlink"), ResolveUrl("~/CMSModules/BizForms/Tools/AlternativeForms/AlternativeForms_List.aspx?formid=" + formId), "BizFormContent", null);
        SetBreadcrumb(1, afi.FormDisplayName, null, null, null);

        InitalizeMenu(afi.FormID, formId);
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu(int altFormId, int formId)
    {
        string urlParams = "?altformid=" + altFormId + "&formid=" + formId;

        InitTabs(3, "altFormsContent");
        SetTab(0, GetString("general.general"), "AlternativeForms_Edit_General.aspx" + urlParams, "SetHelpTopic('helpTopic','alternative_forms_general')");
        SetTab(1, GetString("general.fields"), "AlternativeForms_Fields.aspx" + urlParams, "SetHelpTopic('helpTopic','alternative_forms_fields')");
        SetTab(2, GetString("general.layout"), "AlternativeForms_Layout.aspx" + urlParams, "SetHelpTopic('helpTopic','alternative_forms_layout')");
    }
}
