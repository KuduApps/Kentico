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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.FormControls;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Fields : CMSBizFormPage
{
    protected int formId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }
        FieldEditor.FormType = FormTypeEnum.BizForm;
        formId = QueryHelper.GetInteger("formid", 0);
        // Get bizform info
        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        FieldEditor.Visible = false;
        if (bfi != null)
        {
            // Get form class info
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(bfi.FormClassID);
            if (dci != null)
            {
                if (dci.ClassIsCoupledClass)
                {
                    // Setup the field editor
                    CurrentMaster.BodyClass += " FieldEditorBody";
                    FieldEditor.Visible = true;
                    FieldEditor.ClassName = dci.ClassName;
                    FieldEditor.EnableSimplifiedMode = true;
                    FieldEditor.Mode = FieldEditorModeEnum.BizFormDefinition;
                    FieldEditor.OnAfterDefinitionUpdate += FieldEditor_OnAfterDefinitionUpdate;
                    FieldEditor.OnFieldNameChanged += FieldEditor_OnFieldNameChanged;
                }
                else
                {
                    pnlError.Visible = true;
                }
            }
        }
    }


    protected void FieldEditor_OnAfterDefinitionUpdate(object sender, EventArgs e)
    {
        // Update form to log synchronization
        if (formId > 0)
        {
            BizFormInfo formObj = BizFormInfoProvider.GetBizFormInfo(formId);
            if (formObj != null)
            {
                // Enforce Form property reload next time the data are needed
                formObj.ResetFormInfo();
                BizFormInfoProvider.SetBizFormInfo(formObj);
            }
        }
    }


    protected void FieldEditor_OnFieldNameChanged(object sender, string oldFieldName, string newFieldName)
    {
        if (formId > 0)
        {
            BizFormInfo formObj = BizFormInfoProvider.GetBizFormInfo(formId);
            if (formObj != null)
            {
                // Rename field in layout(s)
                FormHelper.RenameFieldInFormLayout(formObj.FormClassID, oldFieldName, newFieldName);
            }
        }
    }
}
