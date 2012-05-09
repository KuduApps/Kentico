using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSFormControls_Basic_CalendarControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return timePicker.Enabled;
        }
        set
        {
            timePicker.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (timePicker.SelectedDateTime == DateTimeHelper.ZERO_TIME)
            {
                return null;
            }
            else
            {
                return timePicker.SelectedDateTime;
            }
        }
        set
        {
            if (this.GetValue("timezonetype") != null)
            {
                timePicker.TimeZone = TimeZoneInfoProvider.GetTimeZoneTypeEnum(ValidationHelper.GetString(this.GetValue("timezonetype"), ""));
            }
            if (this.GetValue("timezone") != null)
            {
                timePicker.CustomTimeZone = TimeZoneInfoProvider.GetTimeZoneInfo(ValidationHelper.GetString(this.GetValue("timezone"), ""));
            }

            string strValue = ValidationHelper.GetString(value, "").ToLower();

            if ((strValue == DateTimeHelper.MACRO_DATE_TODAY.ToLower()) || (strValue == DateTimeHelper.MACRO_TIME_NOW.ToLower()))
            {
                timePicker.SelectedDateTime = DateTime.Now;
            }
            else
            {
                timePicker.SelectedDateTime = ValidationHelper.GetDateTime(value, DateTimeHelper.ZERO_TIME);
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
            return timePicker.EditTime;
        }
        set
        {
            timePicker.EditTime = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup control
        if (this.FieldInfo != null)
        {
            timePicker.AllowEmptyValue = this.FieldInfo.AllowEmpty;
        }
        timePicker.DisplayNow = ValidationHelper.GetBoolean(this.GetValue("displaynow"), true);
        timePicker.EditTime = ValidationHelper.GetBoolean(this.GetValue("edittime"), this.EditTime);
        timePicker.SupportFolder = "~/CMSAdminControls/Calendar";
        timePicker.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";
        timePicker.IsLiveSite = this.IsLiveSite;

        if (!String.IsNullOrEmpty(this.CssClass))
        {
            timePicker.CssClass = this.CssClass;
            this.CssClass = null;
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            timePicker.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckFieldEmptiness = false;
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Check value
        string strValue = timePicker.DateTimeTextBox.Text.Trim();
        if ((this.FieldInfo != null) && !this.FieldInfo.AllowEmpty && String.IsNullOrEmpty(strValue))
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

        if ((((this.FieldInfo != null) && !this.FieldInfo.AllowEmpty) || !String.IsNullOrEmpty(strValue)) && (ValidationHelper.GetDateTime(strValue, DateTimeHelper.ZERO_TIME) == DateTimeHelper.ZERO_TIME))
        {
            DateTime showDate = new DateTime(2005, 1, 31);
            DateTime showDateTime = new DateTime(2005, 1, 31, 11, 59, 59);

            if (timePicker.EditTime)
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

        if (!timePicker.IsValidRange ())
        {
            ValidationError += GetString("general.errorinvaliddatetimerange");
            return false;
        }
        
        return true;
    }

    #endregion
}
