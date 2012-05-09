using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_TimeZones_Controls_TimeZoneRuleEdior : CMSUserControl
{
    private string mRule = null;
    private bool mEnabled = true;

    /// <summary>
    /// Gets or sets the value that indicates whether control is enabled.
    /// </summary>
    public bool Enabled
    {
        get { return mEnabled; }
        set { mEnabled = value; }
    }


    /// <summary>
    /// Returns time zone rule.
    /// </summary>
    public string Rule
    {
        get
        {
            if (mRule == null)
            {
                GetRule();
            }

            return mRule;
        }
        set
        {
            mRule = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblMonth.Text = GetString("TimeZ.RuleEditor.Month");
        this.lblCondition.Text = GetString("TimeZ.RuleEditor.Condition");
        this.lblDay.Text = GetString("TimeZ.RuleEditor.Day");
        this.lblTime.Text = GetString("TimeZ.RuleEditor.Time");
        this.lblValue.Text = GetString("TimeZ.RuleEditor.Value");

        if (!RequestHelper.IsPostBack())
        {
            // Initialize dropdowns values
            InitDropDowns();
            // Set rule
            SetRule(this.mRule);

        }

        // Switch between day and dayvalue dropdown
        CheckCondition();

        drpMonth.Enabled = this.Enabled;
        drpCondition.Enabled = this.Enabled;
        if (!this.Enabled)
        {
            drpDay.Enabled = this.Enabled;
            drpDayValue.Enabled = this.Enabled;
        }
        txtAtHour.Enabled = this.Enabled;
        txtAtMinute.Enabled = this.Enabled;
        txtValue.Enabled = this.Enabled;
    }


    protected void drpCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Switch between day and dayvalue dropdown
        CheckCondition();
    }


    /// <summary>
    /// Initialize dropdowns.
    /// </summary>
    private void InitDropDowns()
    {
        // Days array
        string[,] days = new string[7, 2];
        days[0, 0] = GetString("general.monday");
        days[0, 1] = "MON";
        days[1, 0] = GetString("general.tuesday");
        days[1, 1] = "TUE";
        days[2, 0] = GetString("general.wednesday");
        days[2, 1] = "WED";
        days[3, 0] = GetString("general.thursday");
        days[3, 1] = "THU";
        days[4, 0] = GetString("general.friday");
        days[4, 1] = "FRI";
        days[5, 0] = GetString("general.saturday");
        days[5, 1] = "SAT";
        days[6, 0] = GetString("general.sunday");
        days[6, 1] = "SUN";

        // Months array
        string[,] months = new string[12, 2];
        months[0, 0] = GetString("general.january");
        months[0, 1] = "JAN";
        months[1, 0] = GetString("general.february");
        months[1, 1] = "FEB";
        months[2, 0] = GetString("general.march");
        months[2, 1] = "MAR";
        months[3, 0] = GetString("general.april");
        months[3, 1] = "APR";
        months[4, 0] = GetString("general.may");
        months[4, 1] = "MAY";
        months[5, 0] = GetString("general.june");
        months[5, 1] = "JUN";
        months[6, 0] = GetString("general.july");
        months[6, 1] = "JUL";
        months[7, 0] = GetString("general.august");
        months[7, 1] = "AUG";
        months[8, 0] = GetString("general.september");
        months[8, 1] = "SEP";
        months[9, 0] = GetString("general.october");
        months[9, 1] = "OCT";
        months[10, 0] = GetString("general.november");
        months[10, 1] = "NOV";
        months[11, 0] = GetString("general.december");
        months[11, 1] = "DEC";

        // Fill month, day and dayvalue dropdowns
        for (int i = 0; i <= days.GetUpperBound(0); i++)
        {
            drpDay.Items.Add(new ListItem(days[i, 0], days[i, 1]));
        }
        for (int i = 0; i <= months.GetUpperBound(0); i++)
        {
            drpMonth.Items.Add(new ListItem(months[i, 0], months[i, 1]));
        }
        for (int i = 1; i <= 31; i++)
        {
            drpDayValue.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }


    /// <summary>
    /// Returns true if all values are correct.
    /// </summary>
    public bool IsValid()
    {
        // Check if hour is between 0 and 23
        int hour = ValidationHelper.GetInteger(this.txtAtHour.Text, -1);
        if ((hour < 0) || (hour >= 24))
        {
            return false;
        }

        // Check if minute is between 0 and 59
        int minute = ValidationHelper.GetInteger(this.txtAtMinute.Text, -1);
        if ((minute < 0) || (minute >= 60))
        {
            return false;
        }

        // Check if value is between -12 and 13
        double value = ValidationHelper.GetDouble(this.txtValue.Text, 24.0);
        if ((value < -12.0) || (value > 13.0))
        {
            return false;
        }

        return true;
    }


    /// <summary>
    /// Method to change visibility of dropdown lists.
    /// </summary>
    protected void CheckCondition()
    {
        string condition = Server.HtmlDecode(this.drpCondition.SelectedValue);
        switch (condition)
        {
            case "FIRST":
            case "LAST":
                this.drpDay.Enabled = true;
                this.drpDayValue.Enabled = false;
                break;

            case ">=":
            case "<=":
                this.drpDay.Enabled = true;
                this.drpDayValue.Enabled = true;
                break;

            case "=":
                this.drpDay.Enabled = false;
                this.drpDayValue.Enabled = true;
                break;
        }
    }


    /// <summary>
    /// Sets rule editor values depend on selected rule.
    /// </summary>
    private void SetRule(string value)
    {
        if (value != null)
        {
            string[] val = value.Split('|');
            if (val.Length == 7)
            {
                // Clear selected items
                this.drpMonth.SelectedIndex = -1;
                this.drpDay.SelectedIndex = -1;
                this.drpDayValue.SelectedIndex = -1;
                this.drpCondition.SelectedIndex = -1;

                // Select items by value
                this.drpMonth.Items.FindByValue(val[0]).Selected = true;
                this.drpDay.Items.FindByValue(val[1]).Selected = true;
                this.drpDayValue.Items.FindByValue(val[2]).Selected = true;
                this.drpCondition.Items.FindByValue(val[3]).Selected = true;

                // Fill text boxes
                this.txtAtHour.Text = val[4];
                // If minutes is from 0 to 9 add 0 before
                this.txtAtMinute.Text = (val[5].Length > 1) ? val[5] : "0" + val[5];
                this.txtValue.Text = val[6];
            }
        }
    }


    /// <summary>
    /// Returns rule.
    /// </summary>
    private void GetRule()
    {
        StringBuilder builder = new StringBuilder();

        // Create rule string
        builder.Append(ValidationHelper.GetString(this.drpMonth.SelectedValue, ""));
        builder.Append("|");
        builder.Append(ValidationHelper.GetString(this.drpDay.SelectedValue, ""));
        builder.Append("|");
        builder.Append(ValidationHelper.GetInteger(this.drpDayValue.SelectedValue, 0));
        builder.Append("|");
        builder.Append(ValidationHelper.GetString(Server.HtmlDecode(this.drpCondition.SelectedValue), ""));
        builder.Append("|");
        builder.Append(ValidationHelper.GetInteger(this.txtAtHour.Text, 0));
        builder.Append("|");
        builder.Append(ValidationHelper.GetInteger(this.txtAtMinute.Text, 0));
        builder.Append("|");
        builder.Append(ValidationHelper.GetInteger(this.txtValue.Text, 0));

        mRule = builder.ToString();
    }
}
