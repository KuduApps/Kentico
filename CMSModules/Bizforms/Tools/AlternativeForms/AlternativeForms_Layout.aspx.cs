using System;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Synchronization;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_AlternativeForms_AlternativeForms_Layout : CMSBizFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        layoutElem.FormType = CMSModules_AdminControls_Controls_Class_Layout.FORMTYPE_BIZFORM;
        layoutElem.ObjectID = QueryHelper.GetInteger("altformid", 0);
        layoutElem.IsAlternative = true;
        layoutElem.OnBeforeSave += layoutElem_OnBeforeSave;
        layoutElem.OnAfterSave += layoutElem_OnAfterSave;
        // Load CSS style sheet to editor area
        if (CMSContext.CurrentSite != null)
        {
            int cssId = CMSContext.CurrentSite.SiteDefaultEditorStylesheet;
            if (cssId == 0) // Use default site CSS if none editor CSS is specified
            {
                cssId = CMSContext.CurrentSite.SiteDefaultStylesheetID;
            }
            layoutElem.CssStyleSheetID = cssId;
        }
    }


    void layoutElem_OnBeforeSave(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }
    }


    void layoutElem_OnAfterSave(object sender, EventArgs e)
    {
        // Required to log staging task, alternative form is not binded to bizform as child
        using (CMSActionContext context = new CMSActionContext())
        {
            context.CreateVersion = false;

            // Log synchronization
            int formId = QueryHelper.GetInteger("formid", 0);
            BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
            SynchronizationHelper.LogObjectChange(bfi, TaskTypeEnum.UpdateObject);
        }
    }
}