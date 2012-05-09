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
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.FormEngine;

public partial class CMSModules_SystemTables_Pages_Development_SystemTable_Edit_Fields : SiteManagerPage
{
    #region "Private variables"

    private int classId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.BodyClass += " FieldEditorBody";
        string className = null;
        DataClassInfo dci = null;

        // Get class id from url
        classId = QueryHelper.GetInteger("classid", 0);
        dci = DataClassInfoProvider.GetDataClass(classId);
        FieldEditor.Visible = false;

        if (dci != null)
        {
            className = dci.ClassName;

            if (dci.ClassIsCoupledClass)
            {
                // Set field editor
                pnlError.Visible = false;
                FieldEditor.Visible = true;
                FieldEditor.ClassName = className;
                FieldEditor.Mode = FieldEditorModeEnum.SystemTable;
                FieldEditor.OnFieldNameChanged += FieldEditor_OnFieldNameChanged;
            }
            else
            {
                pnlError.Visible = true;
                FieldEditor.Visible = false;
            }
        }
    }


    void FieldEditor_OnFieldNameChanged(object sender, string oldFieldName, string newFieldName)
    {
        if (classId > 0)
        {
            // Rename field in layout(s)
            FormHelper.RenameFieldInFormLayout(classId, oldFieldName, newFieldName);
        }
    }
}
