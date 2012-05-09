using System;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Synchronization;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_Fields : CMSBizFormPage
{
    #region "Private variables"

    private BizFormInfo bfi = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        int altFormId = QueryHelper.GetInteger("altformid", 0);
        int formId = QueryHelper.GetInteger("formid", 0);

        bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (bfi == null)
        {
            pnlError.Visible = true;
            altFormFieldEditor.Visible = false;
            return;
        }

        CurrentMaster.BodyClass += " FieldEditorBody";
        altFormFieldEditor.AlternativeFormID = altFormId;
        altFormFieldEditor.Mode = FieldEditorModeEnum.BizFormDefinition;
        altFormFieldEditor.DisplayedControls = FieldEditorControlsEnum.Bizforms;
        altFormFieldEditor.OnBeforeSave += altFormFieldEditor_OnBeforeSave;
        altFormFieldEditor.OnAfterSave += altFormFieldEditor_OnAfterSave;
    }


    protected void altFormFieldEditor_OnAfterSave(object sender, EventArgs e)
    {
        // Required to log staging task, alternative form is not binded to bizform as child
        using (CMSActionContext context = new CMSActionContext())
        {
            context.CreateVersion = false;

            // Log synchronization
            SynchronizationHelper.LogObjectChange(bfi, TaskTypeEnum.UpdateObject);
        }
    }


    protected void altFormFieldEditor_OnBeforeSave(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }
    }
}
