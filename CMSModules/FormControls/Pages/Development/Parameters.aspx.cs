using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;


public partial class CMSModules_FormControls_Pages_Development_Parameters : SiteManagerPage
{
    #region "Private variables"

    private FormUserControlInfo fuci = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.BodyClass += " FieldEditorBody";

        // Load form control
        int controlId = QueryHelper.GetInteger("controlId", 0);
        fuci = FormUserControlInfoProvider.GetFormUserControlInfo(controlId);
        EditedObject = fuci;
        if (fuci != null)
        {
            // Initialize field editor
            fieldEditor.Mode = FieldEditorModeEnum.FormControls;
            fieldEditor.FormDefinition = fuci.UserControlParameters;
            fieldEditor.OnAfterDefinitionUpdate += fieldEditor_OnAfterDefinitionUpdate;
        }
    }


    /// <summary>
    /// Field editor updated event.
    /// </summary>
    void fieldEditor_OnAfterDefinitionUpdate(object sender, EventArgs e)
    {
        // Update Form user control parameters
        if (fuci != null)
        {
            fuci.UserControlParameters = fieldEditor.FormDefinition;
            FormUserControlInfoProvider.SetFormUserControlInfo(fuci);
        }
    }
}

