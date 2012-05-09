using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.EventLog;
using CMS.SettingsProvider;

public partial class CMSFormControls_Filters_TextFilter : FormEngineUserControl, IFilterFormControl
{
    protected string mOperatorFieldName = null;


    #region "Enumerations"

    /// <summary>
    /// Operator.
    /// </summary>
    protected enum Operator
    {
        Like,
        NotLike,
        Equals,
        NotEquals,
        StartsWith,
        NotStartsWith,
        EndsWith,
        NotEndsWith,
        Empty, 
        NotEmpty,
        LessThan,
        GreaterThan
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtText.Text;
        }
        set
        {
            txtText.Text = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Gets name of the field for operator value. Default value is 'Operator'.
    /// </summary>
    protected string OperatorFieldName
    {
        get
        {
            if (string.IsNullOrEmpty(mOperatorFieldName))
            {
                // Get name of the field for operator value
                mOperatorFieldName = DataHelper.GetNotEmpty(GetValue("OperatorFieldName"), "Operator");
            }
            return mOperatorFieldName;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckFieldEmptiness = false;
        InitFilterDropDown();

        if (ContainsColumn(OperatorFieldName))
        {
            drpOperator.SelectedValue = ValidationHelper.GetString(this.Form.Data.GetValue(OperatorFieldName), "0");
        }
    }


    /// <summary>
    /// Returns other values related to this form control.
    /// </summary>
    /// <returns>Returns an array where first dimension is attribute name and the second dimension is its value.</returns>
    public override object[,] GetOtherValues()
    {
        if (Form.Data is DataRowContainer)
        {
            if (!ContainsColumn(OperatorFieldName))
            {
                Form.DataRow.Table.Columns.Add(OperatorFieldName);
            }

            // Set properties names
            object[,] values = new object[3, 2];
            values[0, 0] = OperatorFieldName;
            values[0, 1] = drpOperator.SelectedValue;
            return values;
        }
        return null;
    }


    /// <summary>
    /// Initializes operator filter dropdown list.
    /// </summary>
    /// <param name="drp">Dropdown to init</param>
    private void InitFilterDropDown()
    {
        if (drpOperator.Items.Count == 0)
        {
            DataHelper.FillListControlWithEnum(typeof(Operator), drpOperator, "filter.", null);
        }
    }


    /// <summary>
    /// Gets where condition.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Do not trim value for text filter
        string tempVal = ValidationHelper.GetString(Value, string.Empty);
        string textOp = null;

        Operator op = (Operator)Enum.Parse(typeof(Operator), drpOperator.SelectedValue);

        if (!string.IsNullOrEmpty(tempVal))
        {
            tempVal = SqlHelperClass.GetSafeQueryString(tempVal, false);
        }
        else if (op != Operator.Empty && op != Operator.NotEmpty)
        {
            // Value isn't set (doesn't have to be for empty and not empty)
            return null;
        }

        switch (op)
        {
            case Operator.Like:
                textOp = "LIKE";
                tempVal = "N'%" + tempVal + "%'";
                break;

            case Operator.NotLike:
                textOp = "NOT LIKE";
                tempVal = "N'%" + tempVal + "%'";
                break;

            case Operator.StartsWith:
                textOp = "LIKE";
                tempVal = "N'" + tempVal + "%'";
                break;

            case Operator.NotStartsWith:
                textOp = "NOT LIKE";
                tempVal = "N'" + tempVal + "%'";
                break;

            case Operator.EndsWith:
                textOp = "LIKE";
                tempVal = "N'%" + tempVal + "'";
                break;

            case Operator.NotEndsWith:
                textOp = "NOT LIKE";
                tempVal = "N'%" + tempVal + "'";
                break;

            case Operator.Equals:
                textOp = "=";
                tempVal = "N'" + tempVal + "'";
                break;

            case Operator.NotEquals:
                textOp = "<>";
                tempVal = "N'" + tempVal + "'";
                break;

            case Operator.Empty:
                return string.Format("[{0}] IS NULL OR [{0}] = ''", FieldInfo.Name);

            case Operator.NotEmpty:
                return string.Format("[{0}] IS NOT NULL AND [{0}] <> ''", FieldInfo.Name);

            case Operator.LessThan:
                if (ValidationHelper.IsDouble(tempVal))
                {
                    return string.Format("CASE ISNUMERIC([{0}]) WHEN 1 THEN CAST([{0}] AS FLOAT) ELSE NULL END < CAST({1} AS FLOAT)", FieldInfo.Name, ValidationHelper.GetDouble(tempVal, 0, CultureHelper.EnglishCulture.Name));
                }
                else
                {
                    return "1=2";
                }

            case Operator.GreaterThan:
                if (ValidationHelper.IsDouble(tempVal))
                {
                    return string.Format("CASE ISNUMERIC([{0}]) WHEN 1 THEN CAST([{0}] AS FLOAT) ELSE NULL END > CAST({1} AS FLOAT)", FieldInfo.Name, ValidationHelper.GetDouble(tempVal, 0, CultureHelper.EnglishCulture.Name));
                }
                else
                {
                    return "1=2";
                }
        }

        try
        {
            // Format where condition
            return string.Format(WhereConditionFormat, FieldInfo.Name, tempVal, textOp);
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider.LogException("TextFilter", "GetWhereCondition", ex);
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