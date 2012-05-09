using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.UIControls.UniGridConfig;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Data : CMSBizFormPage
{
    #region "Variables"

    protected int formId = 0;
    protected BizFormInfo bfi = null;
    protected DataClassInfo dci = null;
    protected FormInfo mFormInfo = null;
    protected string className = null;
    protected string primaryColumn = null;
    protected string columnNames = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the form info.
    /// </summary>
    private FormInfo FormInfo
    {
        get
        {
            if (mFormInfo == null)
            {
                if (dci != null)
                {
                    mFormInfo = FormHelper.GetFormInfo(dci.ClassName, false);
                }

            }
            return mFormInfo;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadData' permission
        CheckPermissions("ReadData");

        // Get form ID from url
        formId = QueryHelper.GetInteger("formid", 0);

        // Register scripts
        ScriptHelper.RegisterDialogScript(this);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectFields", ScriptHelper.GetScript("function SelectFields() { modalDialog('" +
            ResolveUrl("~/CMSModules/BizForms/Tools/BizForm_Edit_Data_SelectFields.aspx") + "?formid=" + formId + "'  ,'BizFormFields', 500, 500); }"));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Edit", ScriptHelper.GetScript(
           "function EditRecord(formId, recordId) { " +
           "  document.location.replace('BizForm_Edit_EditRecord.aspx?formID=' + formId + '&formRecordID=' + recordId); } "
           ));

        // Prepare header actions
        string[,] actions = new string[2, 6];
        // New record link
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("bizform_edit_data.newrecord");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("BizForm_Edit_EditRecord.aspx?formid=" + formId.ToString());
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Form/newrecord.png");
        // Select fields link
        actions[1, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[1, 1] = GetString("bizform_edit_data.selectdisplayedfields");
        actions[1, 2] = null;
        actions[1, 3] = "javascript:SelectFields();";
        actions[1, 4] = null;
        actions[1, 5] = GetImageUrl("CMSModules/CMS_Form/selectfields16.png");

        CurrentMaster.HeaderActions.Actions = actions;

        // Initialize unigrid
        gridData.OnExternalDataBound += gridData_OnExternalDataBound;
        gridData.OnLoadColumns += gridData_OnLoadColumns;
        gridData.OnAction += gridData_OnAction;

        // Get BizFormInfo object
        bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (bfi != null)
        {
            dci = DataClassInfoProvider.GetDataClass(bfi.FormClassID);
            if (dci != null)
            {
                className = dci.ClassName;

                // Set alternative form and data container
                gridData.ObjectType = BizFormItemProvider.GetObjectType(className);
                gridData.FilterFormName = className + "." + "filter";
                gridData.FilterFormData = bfi;

                // Get primary column name
                gridData.OrderBy = primaryColumn = GetPrimaryColumn(FormInfo, bfi.FormName);
            }
        }
    }


    protected void gridData_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                CheckPermissions("DeleteData");

                // Get record ID
                int formRecordID = ValidationHelper.GetInteger(actionArgument, 0);

                // Get BizFormInfo object
                if (bfi != null)
                {
                    // Get class object
                    if (dci != null)
                    {
                        // Get record object
                        IDataClass formRecord = DataClassFactory.NewDataClass(dci.ClassName, formRecordID);
                        if (!formRecord.IsEmpty())
                        {
                            // Delete all files of the record
                            BizFormInfoProvider.DeleteBizFormRecordFiles(dci.ClassFormDefinition, formRecord, CMSContext.CurrentSiteName);

                            // Delete the form record
                            formRecord.Delete();

                            // Update number of entries in BizFormInfo
                            if (bfi != null)
                            {
                                BizFormInfoProvider.RefreshDataCount(bfi.FormName, bfi.FormSiteID);
                            }
                        }
                    }
                }

                break;
        }
    }


    protected object gridData_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        ImageButton button = sender as ImageButton;
        GridViewRow grv = parameter as GridViewRow;
        DataRowView drv = grv.DataItem as DataRowView;

        switch (sourceName.ToLower())
        {
            case "edit":
                if (button != null)
                {
                    button.OnClientClick = "EditRecord(" + formId + ", " + drv[primaryColumn] + "); return false;";
                }
                break;

            case "delete":
                if (button != null)
                {
                    button.CommandArgument = Convert.ToString(drv[primaryColumn]);
                }
                break;
        }

        return parameter;
    }


    protected void gridData_OnLoadColumns()
    {
        if (bfi != null)
        {
            ArrayList columnList = new ArrayList();
            string columns = bfi.FormReportFields;
            if (string.IsNullOrEmpty(columns))
            {
                columnList.AddRange(GetExistingColumns());
            }
            else
            {
                // Get existing columns names
                ArrayList existingColumns = GetExistingColumns();

                // Get selected columns
                ArrayList selectedColumns = GetSelectedColumns(columns);

                columns = string.Empty;
                columnNames = string.Empty;
                StringBuilder sb = new StringBuilder();

                // Remove nonexisting columns
                foreach (string col in selectedColumns)
                {
                    if (existingColumns.Contains(col))
                    {
                        columnList.Add(col);
                        sb.Append(",[").Append(col).Append("]");
                    }
                }
                columnNames = sb.ToString();

                // Ensure primary key
                if (!(columnNames.Contains(primaryColumn) || columnNames.Contains(primaryColumn)))
                {
                    columnNames = ",[" + primaryColumn + "]" + columnNames;
                }

                columnNames = columnNames.TrimStart(',');
            }

            // Loop trough all columns
            for (int i = 0; i < columnList.Count; i++)
            {
                string column = columnList[i].ToString();

                // Get field caption
                FormFieldInfo ffi = FormInfo.GetFormField(column);
                string fieldCaption = string.Empty;
                if (ffi == null)
                {
                    fieldCaption = column;
                }
                else
                {
                    fieldCaption = (string.IsNullOrEmpty(ffi.Caption)) ? column : ResHelper.LocalizeString(ffi.Caption);
                }

                Column columnDefinition = new Column
                                              {
                                                  Caption = fieldCaption,
                                                  Source = column,
                                                  AllowSorting = true,
                                                  Wrap = false
                                              };

                if (i == columnList.Count - 1)
                {
                    // Stretch last column
                    columnDefinition.Width = "100%";
                }

                gridData.GridColumns.Columns.Add(columnDefinition);
            }
        }
    }


    /// <summary>
    /// Checks the specified permission.
    /// </summary>
    private void CheckPermissions(string permissionName)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", permissionName))
        {
            RedirectToCMSDeskAccessDenied("cms.form", permissionName);
        }
    }


    private ArrayList GetExistingColumns()
    {
        ArrayList existingColumns = new ArrayList();
        string[] cols = FormInfo.GetColumnNames();
        foreach (string col in cols)
        {
            existingColumns.Add(col);
        }
        return existingColumns;
    }


    private ArrayList GetSelectedColumns(string columns)
    {
        ArrayList cols = new ArrayList();
        cols.AddRange(columns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        return cols;
    }


    /// <summary>
    /// Returns name of the primary key column.
    /// </summary>
    /// <param name="fi">Form info</param>
    /// <param name="bizFormName">Bizform code name</param>
    private static string GetPrimaryColumn(FormInfo fi, string bizFormName)
    {
        string result = null;

        if ((fi != null) && (!string.IsNullOrEmpty(bizFormName)))
        {
            // Try to get field with the name 'bizformnameID'
            FormFieldInfo ffi = fi.GetFormField(bizFormName + "ID");
            if ((ffi != null) && ffi.PrimaryKey)
            {
                result = ffi.Name;
            }
            else
            {
                // Seek primary key column in all fields
                FormFieldInfo[] fields = fi.GetFields(true, true);
                foreach (FormFieldInfo field in fields)
                {
                    if (field.PrimaryKey)
                    {
                        result = field.Name;
                        break;
                    }
                }
            }
        }

        return result;
    }

    #endregion
}
