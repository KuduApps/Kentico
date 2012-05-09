using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;

public partial class CMSModules_AdminControls_Controls_Class_ClassFields : FormEngineUserControl
{
    #region "Variables"

    FormFieldDataTypeEnum mFieldDataType = FormFieldDataTypeEnum.Unknown;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            drpField.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return drpField.SelectedValue;
        }
        set
        {
            // Reload data
            ReloadData();
            if (drpField.Items.Count > 0)
            {
                // Try to select specified field
                ListItem li = drpField.Items.FindByValue(value.ToString());
                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets name of the class which fields should be displayed.
    /// </summary>
    public string ClassName
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets data type of fields that should be offered. Optional.
    /// </summary>
    public FormFieldDataTypeEnum FieldDataType
    {
        get
        {
            return mFieldDataType;
        }
        set
        {
            mFieldDataType = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the control.
    /// </summary>
    protected void ReloadData()
    {
        if (drpField.Items.Count == 0)
        {
            // Load dropdownlist with fields of specified class
            FormInfo fi = FormHelper.GetFormInfo(ClassName, false);
            if (fi != null)
            {
                if (string.Equals(ClassName, "cms.user", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Combine user fields with those of user settings
                    FormInfo coupledInfo = FormHelper.GetFormInfo("cms.usersettings", false);
                    if (coupledInfo != null)
                    {
                        fi.CombineWithForm(coupledInfo, false, null, false);
                    }
                }

                if (FieldDataType == FormFieldDataTypeEnum.Unknown)
                {
                    // Get all form fields
                    drpField.DataSource = fi.GetFields(true, true);
                }
                else
                {
                    // Get form fields of specific data type
                    drpField.DataSource = fi.GetFields(FieldDataType);
                }
                drpField.DataBind();
            }
            // Add '(none)' item
            drpField.Items.Insert(0, new ListItem(GetString("general.selectnone"), string.Empty));
        }
    }

    #endregion
}