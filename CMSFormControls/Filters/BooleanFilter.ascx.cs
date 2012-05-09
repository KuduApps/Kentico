using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.EventLog;

public partial class CMSFormControls_Filters_BooleanFilter : FormEngineUserControl, IFilterFormControl
{
    #region "Variables"

    private string selectedValue = string.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (!String.IsNullOrEmpty(drpConditionValue.SelectedValue))
            {
                return drpConditionValue.SelectedValue;
            }
            else
            {
                return DBNull.Value;
            }
        }
        set
        {
            if (ValidationHelper.IsBoolean(value))
            {
                selectedValue = ValidationHelper.GetBoolean(value, false) ? "1" : "0";
            }
            else
            {
                selectedValue = string.Empty;
            }
            drpConditionValue.SelectedValue = selectedValue;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckFieldEmptiness = false;
        InitFilterDropDown();
    }


    /// <summary>
    /// Initializes filter dropdown list.
    /// </summary>
    private void InitFilterDropDown()
    {
        if (drpConditionValue.Items.Count == 0)
        {
            drpConditionValue.Items.Add(new ListItem(GetString("general.selectall"), string.Empty));
            drpConditionValue.Items.Add(new ListItem(GetString("general.yes"), "1"));
            drpConditionValue.Items.Add(new ListItem(GetString("general.no"), "0"));
            drpConditionValue.SelectedValue = selectedValue;
        }
    }


    /// <summary>
    /// Gets where condition.
    /// </summary>
    public override string GetWhereCondition()
    {
        string tempVal = ValidationHelper.GetString(Value, null);

        // Only boolean value
        if (string.IsNullOrEmpty(tempVal) || !ValidationHelper.IsBoolean(tempVal))
        {
            return null;
        }

        try
        {
            string value = ValidationHelper.GetBoolean(tempVal, false) ? "1" : "0";
            // Format where condition
            return string.Format(WhereConditionFormat, FieldInfo.Name, value, "=");
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider.LogException("BooleanFilter", "GetWhereCondition", ex);
        }

        return null;
    }

    #endregion


    #region "IFilterFormControl Members"

    /// <summary>
    /// Where condition format.
    /// </summary>
    public string WhereConditionFormat
    {
        get
        {
            // Get filter where condition format or default format
            return DataHelper.GetNotEmpty(GetValue("WhereConditionFormat"), "[{0}] {2} {1}");
        }
        set
        {
            SetValue("WhereConditionFormat", value);
        }
    }

    #endregion
}