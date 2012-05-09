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
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Data_SelectFields : CMSModalPage
{
    protected int formId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Tools
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (!user.IsAuthorizedPerUIElement("CMS.Desk", "Tools"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Desk", "Tools");
        }

        // Check permissions for CMS Desk -> Tools -> BizForms        
        if (!user.IsAuthorizedPerUIElement("CMS.Tools", "Form"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Tools", "Form");
        }

        // Check 'ReadData' permission
        if (!user.IsAuthorizedPerResource("cms.form", "ReadData"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadData");
        }

        // Get form id from url
        formId = QueryHelper.GetInteger("formid", 0);

        BizFormInfo bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        if (bfi != null)
        {
            // Check authorized roles for this form
            if (!bfi.IsFormAllowedForUser(CMSContext.CurrentUser.UserName, CMSContext.CurrentSiteName))
            {
                RedirectToAccessDenied(GetString("Bizforms.FormNotAllowedForUserRoles"));
            }
        }

        string[] columnNames = null;
        DataClassInfo dci = null;
        Hashtable reportFields = new Hashtable();
        FormInfo fi = null;

        // Initialize controls
        this.CurrentMaster.Title.TitleText = GetString("BizForm_Edit_Data_SelectFields.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Form/selectfields.png");
        this.CurrentMaster.DisplayActionsPanel = true;

        btnSelectAll.Text = GetString("BizForm_Edit_Data_SelectFields.SelectAll");

        if (!RequestHelper.IsPostBack())
        {
            btnOk.Text = GetString("General.OK");
            btnCancel.Text = GetString("General.Cancel");

            if (bfi != null)
            {
                // Get dataclass info
                dci = DataClassInfoProvider.GetDataClass(bfi.FormClassID);

                if (dci != null)
                {
                    // Get columns names
                    fi = FormHelper.GetFormInfo(dci.ClassName, false);

                    columnNames = fi.GetColumnNames();
                }

                // Get report fields
                if (String.IsNullOrEmpty(bfi.FormReportFields))
                {
                    reportFields = LoadReportFields(columnNames);
                }
                else
                {
                    reportFields.Clear();

                    foreach (string field in bfi.FormReportFields.Split(';'))
                    {
                        // Add field key to hastable
                        reportFields[field] = null;
                    }
                }

                if (columnNames != null)
                {
                    foreach (string name in columnNames)
                    {
                        FormFieldInfo ffi = fi.GetFormField(name);

                        // Add checkboxes to the list
                        ListItem item = new ListItem(ResHelper.LocalizeString(GetFieldCaption(ffi, name)), name);
                        if (reportFields.Contains(name))
                        {
                            // Select checkbox if field is reported
                            item.Selected = true;
                        }
                        chkListFields.Items.Add(item);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Button OK clicked.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        BizFormInfo bfi = null;
        string reportFields = "";
        bool noItemSelected = (chkListFields.SelectedIndex == -1) ? true : false;

        foreach (ListItem item in chkListFields.Items)
        {
            // Display all fields
            if (noItemSelected)
            {
                reportFields += item.Value + ";";
            }
            // Display only selected fields
            else if (item.Selected)
            {
                reportFields += item.Value + ";";
            }
        }

        if (reportFields != "")
        {
            // Remove ending ';'
            reportFields = reportFields.TrimEnd(';');
        }

        bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        if (bfi != null)
        {
            // Save report fields
            bfi.FormReportFields = reportFields;
            BizFormInfoProvider.SetBizFormInfo(bfi);

            // Close dialog window
            ltlScript.Text = ScriptHelper.GetScript("CloseAndRefresh();");
        }

    }


    /// <summary>
    /// Returns field caption of the specified column.
    /// </summary>
    /// <param name="ffi">Form field info</param>
    /// <param name="columnName">Column name</param>    
    protected string GetFieldCaption(FormFieldInfo ffi, string columnName)
    {
        string fieldCaption = "";

        // get field caption        
        if ((ffi == null) || (ffi.Caption == ""))
        {
            fieldCaption = columnName;
        }
        else
        {
            fieldCaption = ffi.Caption;
        }

        return fieldCaption;
    }


    /// <summary>
    /// Returns report fields hashtable.
    /// </summary>
    protected Hashtable LoadReportFields(string[] columns)
    {
        Hashtable table = new Hashtable();

        if (columns != null)
        {
            foreach (string str in columns)
            {
                table.Add(str, null);
            }
        }
        return table;
    }
}
