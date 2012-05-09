using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_Selectors_SelectValidity : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Selected validity period.
    /// </summary>
    public ValidityEnum Validity
    {
        get
        {
            if (this.radDays.Checked)
            {
                return ValidityEnum.Days;
            }
            else if (this.radWeeks.Checked)
            {
                return ValidityEnum.Weeks;
            }
            else if (this.radYears.Checked)
            {
                return ValidityEnum.Years;
            }
            else if (this.radMonths.Checked)
            {
                return ValidityEnum.Months;
            }
            else
            {
                return ValidityEnum.Until;
            }
        }
        set
        {
            this.radDays.Checked = (value == ValidityEnum.Days);
            this.radWeeks.Checked = (value == ValidityEnum.Weeks);
            this.radMonths.Checked = (value == ValidityEnum.Months);
            this.radYears.Checked = (value == ValidityEnum.Years);
            this.radUntil.Checked = (value == ValidityEnum.Until);
        }
    }


    /// <summary>
    /// Valid for units.
    /// </summary>
    public int ValidFor
    {
        get
        {
            return ValidationHelper.GetInteger(this.txtValidFor.Text.Trim(), 0);
        }
        set
        {
            this.txtValidFor.Text = value.ToString();
        }
    }


    /// <summary>
    /// Valid until date and time.
    /// </summary>
    public DateTime ValidUntil
    {
        get
        {
            return this.untilDateElem.SelectedDateTime;
        }
        set
        {
            this.untilDateElem.SelectedDateTime = value;
        }
    }


    /// <summary>
    /// Validation error message.
    /// </summary>
    public string ErrorMessage
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if validation error message is displayed by the control itself.
    /// </summary>
    public bool DisplayErrorMessage
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if post back automatically occurs when validity is changed.
    /// </summary>
    public bool AutoPostBack
    {
        get
        {
            return this.radDays.AutoPostBack;
        }
        set
        {
            this.radDays.AutoPostBack = value;
            this.radWeeks.AutoPostBack = value;
            this.radMonths.AutoPostBack = value;
            this.radYears.AutoPostBack = value;
            this.radUntil.AutoPostBack = value;
        }
    }


    /// <summary>
    /// Automatically disables inactive control. If time interval validity is selected (day, week, month, year)
    /// then time interval text box is enabled and date time picker is disabled and vice versa.
    /// </summary>
    public bool AutomaticallyDisableInactiveControl
    {
        get;
        set;
    }

    #endregion


    #region "Events"

    public event EventHandler OnValidityChanged;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        DisableInactiveControl();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Set error message
        this.lblError.Text = this.ErrorMessage;

        // Display error message if required
        if (this.DisplayErrorMessage)
        {
            this.lblError.Visible = true;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Validates form and returns true if valid.
    /// </summary>
    public string Validate()
    {
        this.ErrorMessage = String.Empty;

        // Validate valid for multiplier
        if (String.IsNullOrEmpty(this.ErrorMessage) && !this.radUntil.Checked)
        {
            if (!ValidationHelper.IsInteger(this.txtValidFor.Text.Trim()) || !(ValidationHelper.GetInteger(this.txtValidFor.Text.Trim(), 0) >= 1))
            {
                this.ErrorMessage = this.GetString("General.SelectValidity.ValidForError");
            }
        }

        // Validate until date and time
        if (String.IsNullOrEmpty(this.ErrorMessage) && this.radUntil.Checked)
        {
            if (!String.IsNullOrEmpty(this.untilDateElem.DateTimeTextBox.Text) && (!this.untilDateElem.IsValidRange() || !ValidationHelper.IsDateTime(this.untilDateElem.DateTimeTextBox.Text)))
            {
                this.ErrorMessage = this.GetString("General.SelectValidity.ValidUntilError");
            }
        }

        // Return error message
        return this.ErrorMessage;
    }


    protected void ValidityRadioGroup_CheckedChanged(object sender, EventArgs e)
    {
        DisableInactiveControl();
        if (this.OnValidityChanged != null)
        {
            this.OnValidityChanged(this, null);
        }
    }


    /// <summary>
    /// Enables/disables inactive control.
    /// </summary>
    private void DisableInactiveControl() 
    {
        if (this.AutomaticallyDisableInactiveControl && this.AutoPostBack)
        {
            bool dateSelected = radUntil.Checked;
            txtValidFor.Enabled = !dateSelected;
            untilDateElem.Enabled = dateSelected;
        }
    }

    #endregion
}
