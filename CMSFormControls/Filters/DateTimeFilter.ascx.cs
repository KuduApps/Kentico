using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSFormControls_Filters_DateTimeFilter : FormEngineUserControl, IFilterFormControl
{
    protected string mSecondDateFieldName = null;


    #region "Properties"

    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (dtmTimeFrom.SelectedDateTime == DateTimeHelper.ZERO_TIME)
            {
                return null;
            }
            else
            {
                return dtmTimeFrom.SelectedDateTime;
            }
        }
        set
        {
            if (this.GetValue("timezonetype") != null)
            {
                dtmTimeFrom.TimeZone = TimeZoneInfoProvider.GetTimeZoneTypeEnum(ValidationHelper.GetString(this.GetValue("timezonetype"), ""));
            }
            if (this.GetValue("timezone") != null)
            {
                dtmTimeFrom.CustomTimeZone = TimeZoneInfoProvider.GetTimeZoneInfo(ValidationHelper.GetString(this.GetValue("timezone"), ""));
            }

            string strValue = ValidationHelper.GetString(value, "").ToLower();

            if ((strValue == DateTimeHelper.MACRO_DATE_TODAY.ToLower()) || (strValue == DateTimeHelper.MACRO_TIME_NOW.ToLower()))
            {
                dtmTimeFrom.SelectedDateTime = DateTime.Now;
            }
            else
            {
                dtmTimeFrom.SelectedDateTime = ValidationHelper.GetDateTime(value, DateTimeHelper.ZERO_TIME);
            }
        }
    }


    /// <summary>
    /// Gets or sets if calendar control enables to edit time.
    /// </summary>
    public bool EditTime
    {
        get
        {
            return dtmTimeFrom.EditTime;
        }
        set
        {
            dtmTimeFrom.EditTime = value;
            dtmTimeTo.EditTime = value;
        }
    }


    /// <summary>
    /// Gets name of the field for second date value. Default value is 'SecondDatetime'.
    /// </summary>
    protected string SecondDateFieldName
    {
        get
        {
            if (string.IsNullOrEmpty(mSecondDateFieldName))
            {
                // Get name of the field for second date value
                mSecondDateFieldName = DataHelper.GetNotEmpty(GetValue("SecondDateFieldName"), "SecondDatetime");
            }
            return mSecondDateFieldName;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup control
        if (this.FieldInfo != null)
        {
            dtmTimeFrom.AllowEmptyValue = this.FieldInfo.AllowEmpty;
            dtmTimeTo.AllowEmptyValue = this.FieldInfo.AllowEmpty;
        }
        dtmTimeFrom.DisplayNow = ValidationHelper.GetBoolean(this.GetValue("displaynow"), true);
        dtmTimeTo.DisplayNow = ValidationHelper.GetBoolean(this.GetValue("displaynow"), true);
        dtmTimeFrom.EditTime = ValidationHelper.GetBoolean(this.GetValue("edittime"), this.EditTime);
        dtmTimeTo.EditTime = ValidationHelper.GetBoolean(this.GetValue("edittime"), this.EditTime);
        dtmTimeFrom.SupportFolder = "~/CMSAdminControls/Calendar";
        dtmTimeTo.SupportFolder = "~/CMSAdminControls/Calendar";
        dtmTimeFrom.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";
        dtmTimeTo.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";
        dtmTimeFrom.IsLiveSite = this.IsLiveSite;
        dtmTimeTo.IsLiveSite = this.IsLiveSite;

        if (!String.IsNullOrEmpty(this.CssClass))
        {
            dtmTimeFrom.CssClass = this.CssClass;
            dtmTimeTo.CssClass = this.CssClass;
            this.CssClass = null;
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            dtmTimeFrom.Attributes.Add("style", this.ControlStyle);
            dtmTimeTo.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckFieldEmptiness = false;

        // User defined extensions
        if (ContainsColumn(SecondDateFieldName))
        {
            dtmTimeTo.SelectedDateTime = ValidationHelper.GetDateTime(this.Form.Data.GetValue(SecondDateFieldName), DateTimeHelper.ZERO_TIME, "en-us");
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Check value
        string strValueFrom = dtmTimeFrom.DateTimeTextBox.Text.Trim();
        string strValueTo = dtmTimeTo.DateTimeTextBox.Text.Trim();
        if ((this.FieldInfo != null) && !this.FieldInfo.AllowEmpty && (String.IsNullOrEmpty(strValueFrom) && String.IsNullOrEmpty(strValueTo)))
        {
            // Empty error
            if (this.ErrorMessage != null)
            {
                if (this.ErrorMessage != ResHelper.GetString("BasicForm.InvalidInput"))
                {
                    ValidationError = this.ErrorMessage;
                }
                else
                {
                    ValidationError += ResHelper.GetString("BasicForm.ErrorEmptyValue");
                }
            }
            return false;
        }

        Func<bool> CheckEmptiness = () => ((FieldInfo != null) && FieldInfo.AllowEmpty) || (!String.IsNullOrEmpty(strValueTo) && !String.IsNullOrEmpty(strValueFrom));
        Func<string, bool> CheckTimeIsValid = (string str) => (ValidationHelper.GetDateTime(str, DateTimeHelper.ZERO_TIME) != DateTimeHelper.ZERO_TIME) || String.IsNullOrEmpty(str);

        if (!CheckEmptiness() || !CheckTimeIsValid(strValueTo) || !CheckTimeIsValid(strValueFrom))
        {
            DateTime showDate = new DateTime(2005, 1, 31);
            DateTime showDateTime = new DateTime(2005, 1, 31, 11, 59, 59);

            if (dtmTimeFrom.EditTime)
            {
                // Error invalid DateTime
                ValidationError += ResHelper.GetString("BasicForm.ErrorInvalidDateTime") + " " + showDateTime + ".";
            }
            else
            {
                // Error invalid date
                ValidationError += ResHelper.GetString("BasicForm.ErrorInvalidDate") + " " + showDate.ToString("d") + ".";
            }

            return false;
        }

        if (!dtmTimeFrom.IsValidRange() || !dtmTimeTo.IsValidRange())
        {
            ValidationError += GetString("general.errorinvaliddatetimerange");
            return false;
        }

        return true;
    }


    /// <summary>
    /// Returns other values related to this form control.
    /// </summary>
    /// <returns>Returns an array where first dimension is attribute name and the second dimension is its value.</returns>
    public override object[,] GetOtherValues()
    {
        if (Form.Data is DataRowContainer)
        {
            if (!ContainsColumn(SecondDateFieldName))
            {
                Form.DataRow.Table.Columns.Add(SecondDateFieldName);
            }

            // Set properties names
            object[,] values = new object[3, 2];
            values[0, 0] = SecondDateFieldName;
            values[0, 1] = dtmTimeTo.SelectedDateTime;
            return values;
        }
        return null;
    }


    /// <summary>
    /// Gets where condition.
    /// </summary>
    public override string GetWhereCondition()
    {
        string fromDate = FormHelper.GetDateTimeValueInDBCulture(ValidationHelper.GetString(dtmTimeFrom.SelectedDateTime, string.Empty).Trim());
        string toDate = FormHelper.GetDateTimeValueInDBCulture(ValidationHelper.GetString(dtmTimeTo.SelectedDateTime, string.Empty).Trim());
        string opFrom = ">=";
        string opTo = "<=";
        string where = null;

        if (!String.IsNullOrEmpty(fromDate) && (fromDate != DateTimeHelper.ZERO_TIME.ToString()))
        {
            where = string.Format(WhereConditionFormat, FieldInfo.Name, fromDate, opFrom);
        }

        if (!String.IsNullOrEmpty(toDate) && (toDate != DateTimeHelper.ZERO_TIME.ToString()))
        {
            where = SqlHelperClass.AddWhereCondition(where, string.Format(WhereConditionFormat, FieldInfo.Name, toDate, opTo));
        }

        return where;
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
            return DataHelper.GetNotEmpty(GetValue("WhereConditionFormat"), "[{0}] {2} '{1}'");
        }
        set
        {
            SetValue("WhereConditionFormat", value);
        }
    }

    #endregion
}