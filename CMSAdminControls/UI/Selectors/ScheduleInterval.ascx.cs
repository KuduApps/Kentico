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
using CMS.Scheduler;
using CMS.UIControls;

public partial class CMSAdminControls_UI_Selectors_ScheduleInterval : CMSUserControl
{
    protected string mScheduleInterval = null;
    protected string mDefaultPeriod = SchedulingHelper.PERIOD_MINUTE;
    protected bool mWeekListChecked = false;


    #region "Public properties"

    /// <summary>
    /// Default period. Allowd values: second, timesecond, minute, hour, day, week, month, once.
    /// </summary>
    public string DefaultPeriod
    {
        get
        {
            return mDefaultPeriod;
        }
        set
        {
            switch (value.ToLower())
            {
                case SchedulingHelper.PERIOD_DAY:
                    mDefaultPeriod = SchedulingHelper.PERIOD_DAY;
                    break;

                case SchedulingHelper.PERIOD_HOUR:
                    mDefaultPeriod = SchedulingHelper.PERIOD_HOUR;
                    break;

                case SchedulingHelper.PERIOD_MINUTE:
                    mDefaultPeriod = SchedulingHelper.PERIOD_MINUTE;
                    break;

                case SchedulingHelper.PERIOD_MONTH:
                    mDefaultPeriod = SchedulingHelper.PERIOD_MONTH;
                    break;

                case SchedulingHelper.PERIOD_ONCE:
                    mDefaultPeriod = SchedulingHelper.PERIOD_ONCE;
                    break;

                case "second":
                case SchedulingHelper.PERIOD_SECOND:
                    mDefaultPeriod = SchedulingHelper.PERIOD_SECOND;
                    break;

                case SchedulingHelper.PERIOD_WEEK:
                    mDefaultPeriod = SchedulingHelper.PERIOD_WEEK;
                    break;

                default:
                    mDefaultPeriod = SchedulingHelper.PERIOD_MINUTE;
                    break;
            }
        }
    }


    /// <summary>
    /// Scheduled interval encoded into string.
    /// </summary>
    public string ScheduleInterval
    {
        get
        {
            mScheduleInterval = EncodeInterval();

            return mScheduleInterval;
        }
        set
        {
            mScheduleInterval = value;

            DecodeInterval(mScheduleInterval);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        EnsureChildControls();
    }


    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        // Control init
        dateTimePicker.DateTimeTextBox.ForeColor = System.Drawing.Color.Black;
        txtEvery.ForeColor = System.Drawing.Color.Black;
        txtFromHours.ForeColor = System.Drawing.Color.Black;
        txtFromMinutes.ForeColor = System.Drawing.Color.Black;
        txtToHours.ForeColor = System.Drawing.Color.Black;
        txtToMinutes.ForeColor = System.Drawing.Color.Black;

        if (!RequestHelper.IsPostBack())
        {
            ControlInit();
            ValidatorInit();
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!mWeekListChecked)
        {
            // Initialize week and weekend selectors
            chkWeek.ClearSelection();
            foreach (ListItem li in chkWeek.Items)
            {
                li.Selected = true;
            }
            chkWeekEnd.ClearSelection();
            foreach (ListItem li in chkWeekEnd.Items)
            {
                li.Selected = true;
            }
        }

        if (!string.IsNullOrEmpty(lblError.Text))
        {
            // Show error message
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// Initialization of controls.
    /// </summary>
    protected void ControlInit()
    {
        ListItem li = null;
        // Panel Period initialization
        drpPeriod.Items.Clear();
        li = new ListItem(GetString("ScheduleInterval.Period.Second"), SchedulingHelper.PERIOD_SECOND);
        drpPeriod.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Period.Minute"), SchedulingHelper.PERIOD_MINUTE);
        drpPeriod.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Period.Hour"), SchedulingHelper.PERIOD_HOUR);
        drpPeriod.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Period.Day"), SchedulingHelper.PERIOD_DAY);
        drpPeriod.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Period.Week"), SchedulingHelper.PERIOD_WEEK);
        drpPeriod.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Period.Month"), SchedulingHelper.PERIOD_MONTH);
        drpPeriod.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Period.Once"), SchedulingHelper.PERIOD_ONCE);
        drpPeriod.Items.Add(li);
        // Select default period
        drpPeriod.SelectedValue = DefaultPeriod;

        // Calendar initialization
        dateTimePicker.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";

        // Weekday and weekend lists' initialization
        chkWeek.Items.Clear();
        chkWeekEnd.Items.Clear();
        drpMonth3.Items.Clear();
        li = new ListItem(GetString("ScheduleInterval.Days.Monday"), DayOfWeek.Monday.ToString());
        chkWeek.Items.Add(li);
        drpMonth3.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Days.Tuesday"), DayOfWeek.Tuesday.ToString());
        chkWeek.Items.Add(li);
        drpMonth3.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Days.Wednesday"), DayOfWeek.Wednesday.ToString());
        chkWeek.Items.Add(li);
        drpMonth3.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Days.Thursday"), DayOfWeek.Thursday.ToString());
        chkWeek.Items.Add(li);
        drpMonth3.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Days.Friday"), DayOfWeek.Friday.ToString());
        chkWeek.Items.Add(li);
        drpMonth3.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Days.Saturday"), DayOfWeek.Saturday.ToString());
        chkWeekEnd.Items.Add(li);
        drpMonth3.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Days.Sunday"), DayOfWeek.Sunday.ToString());
        chkWeekEnd.Items.Add(li);
        drpMonth3.Items.Add(li);

        // Month list initialization
        drpMonth1.Items.Clear();
        for (int i = 1; i <= 31; i++)
        {
            li = new ListItem(i.ToString());
            drpMonth1.Items.Add(li);
        }

        // Repeat list initialization
        drpMonth2.Items.Clear();
        li = new ListItem(GetString("ScheduleInterval.Months.First"), SchedulingHelper.MONTHS_FIRST);
        drpMonth2.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Months.Second"), SchedulingHelper.MONTHS_SECOND);
        drpMonth2.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Months.Third"), SchedulingHelper.MONTHS_THIRD);
        drpMonth2.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Months.Fourth"), SchedulingHelper.MONTHS_FOURTH);
        drpMonth2.Items.Add(li);
        li = new ListItem(GetString("ScheduleInterval.Months.Last"), SchedulingHelper.MONTHS_LAST);
        drpMonth2.Items.Add(li);

        drpMonth2.Enabled = false;
        drpMonth3.Enabled = false;

        // Initialize dialog according to default period
        OnPeriodChangeInit();
    }


    /// <summary>
    /// Initialization of validators.
    /// </summary>
    protected void ValidatorInit()
    {
        string error = GetString("ScheduleInterval.WrongFormat");
        string empty = GetString("ScheduleInterval.ErrorEmpty");
        // 'Every' panel validators
        rfvEvery.ErrorMessage = empty;
        rvEvery.MinimumValue = "0";
        rvEvery.MaximumValue = "10000";
        rvEvery.ErrorMessage = error;
        // 'Between' panel validators
        rfvFromHours.ErrorMessage = empty;
        rvFromHours.MinimumValue = "0";
        rvFromHours.MaximumValue = "23";
        rvFromHours.ErrorMessage = error;
        rfvFromMinutes.ErrorMessage = empty;
        rvFromMinutes.MinimumValue = "0";
        rvFromMinutes.MaximumValue = "59";
        rvFromMinutes.ErrorMessage = error;
        rfvToHours.ErrorMessage = empty;
        rvToHours.MinimumValue = "0";
        rvToHours.MaximumValue = "23";
        rvToHours.ErrorMessage = error;
        rfvToMinutes.ErrorMessage = empty;
        rvToMinutes.MinimumValue = "0";
        rvToMinutes.MaximumValue = "59";
        rvToMinutes.ErrorMessage = error;
    }


    /// <summary>
    /// Initializes dialog according to selected period.
    /// </summary>
    protected void OnPeriodChangeInit()
    {
        switch (drpPeriod.SelectedValue)
        {
            case SchedulingHelper.PERIOD_SECOND: // Second
                pnlEvery.Visible = true;
                pnlBetween.Visible = true;
                pnlDays.Visible = true;
                pnlMonth.Visible = false;
                lblEveryPeriod.Text = GetString("ScheduleInterval.Period.second");
                break;

            case SchedulingHelper.PERIOD_MINUTE: // Minute
                pnlEvery.Visible = true;
                pnlBetween.Visible = true;
                pnlDays.Visible = true;
                pnlMonth.Visible = false;
                lblEveryPeriod.Text = GetString("ScheduleInterval.Period.Minute");
                break;

            case SchedulingHelper.PERIOD_HOUR: // Hour
                pnlEvery.Visible = true;
                pnlBetween.Visible = true;
                pnlDays.Visible = true;
                pnlMonth.Visible = false;
                lblEveryPeriod.Text = GetString("ScheduleInterval.Period.Hour");
                break;

            case SchedulingHelper.PERIOD_DAY: // Day
                pnlEvery.Visible = true;
                pnlBetween.Visible = false;
                pnlDays.Visible = true;
                pnlMonth.Visible = false;
                lblEveryPeriod.Text = GetString("ScheduleInterval.Period.Day");
                break;

            case SchedulingHelper.PERIOD_WEEK: // Week
                pnlEvery.Visible = true;
                pnlBetween.Visible = false;
                pnlDays.Visible = false;
                pnlMonth.Visible = false;
                lblEveryPeriod.Text = GetString("ScheduleInterval.Period.Week");
                break;

            case SchedulingHelper.PERIOD_MONTH: // Month
                pnlEvery.Visible = false;
                pnlBetween.Visible = false;
                pnlDays.Visible = false;
                pnlMonth.Visible = true;
                break;

            case SchedulingHelper.PERIOD_ONCE: // Once
                pnlEvery.Visible = false;
                pnlBetween.Visible = false;
                pnlDays.Visible = false;
                pnlMonth.Visible = false;
                break;
        }

        // Set default values to textboxes and checkboxlists
        dateTimePicker.SelectedDateTime = DateTime.Now;
        txtEvery.Text = "1";
        txtFromHours.Text = "00";
        txtFromMinutes.Text = "00";
        txtToHours.Text = "23";
        txtToMinutes.Text = "59";

        drpMonth1.SelectedIndex = 0;
        drpMonth2.SelectedIndex = 0;
        drpMonth3.SelectedIndex = 0;
    }


    /// <summary>
    /// On selected index changed.
    /// </summary>
    protected void DrpPeriod_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        OnPeriodChangeInit();
    }


    /// <summary>
    /// On 1st radio button change.
    /// </summary>
    protected void radMonth1_CheckedChanged(object sender, EventArgs e)
    {
        radMonth2.Checked = false;
        drpMonth1.Enabled = true;
        drpMonth2.Enabled = false;
        drpMonth3.Enabled = false;
    }


    /// <summary>
    /// On 2nd radio button change.
    /// </summary>
    protected void radMonth2_CheckedChanged(object sender, EventArgs e)
    {
        radMonth1.Checked = false;
        drpMonth1.Enabled = false;
        drpMonth2.Enabled = true;
        drpMonth3.Enabled = true;
    }


    public bool CheckIntervalPreceedings()
    {
        switch (drpPeriod.SelectedIndex)
        {
            case 0: // Second
            case 1: // Minute
            case 2: // Hour
                TimeSpan start = new TimeSpan(Convert.ToInt32(txtFromHours.Text), Convert.ToInt32(txtFromMinutes.Text), 0);
                TimeSpan end = new TimeSpan(Convert.ToInt32(txtToHours.Text), Convert.ToInt32(txtToMinutes.Text), 0); ;
                return (start.CompareTo(end) < 0);
            default: return true;
        }
    }


    public bool CheckOneDayMinimum()
    {
        switch (drpPeriod.SelectedIndex)
        {
            case 0: // Second
            case 1: // Minute
            case 2: // Hour
            case 3: // Day
                return ((chkWeek.SelectedItem != null) || (chkWeekEnd.SelectedItem != null));
            default: return true;
        }
    }


    /// <summary>
    /// Creates schedule interval string.
    /// </summary>
    protected string EncodeInterval()
    {
        //string intervalCode = null;
        TaskInterval ti = new TaskInterval();

        string error = GetString("ScheduleInterval.WrongFormat");
        string empty = GetString("ScheduleInterval.ErrorEmpty");
        string result = string.Empty;

        try
        {
            ti.Period = drpPeriod.SelectedValue;
            ti.StartTime = dateTimePicker.SelectedDateTime;

            switch (drpPeriod.SelectedIndex)
            {
                case 0: // Second
                case 1: // Minute
                case 2: // Hour
                    result = new Validator().NotEmpty(txtFromHours.Text, empty).IsInteger(txtFromHours.Text, error).NotEmpty(txtFromMinutes.Text, empty).IsInteger(txtFromMinutes.Text, error).NotEmpty(txtToHours.Text, empty).IsInteger(txtToHours.Text, error).NotEmpty(txtToMinutes.Text, empty).IsInteger(txtToMinutes.Text, error).NotEmpty(txtEvery.Text, empty).IsInteger(txtEvery.Text, error).Result;
                    if (string.IsNullOrEmpty(result))
                    {
                        if ((Convert.ToInt32(txtFromHours.Text) >= 0) && (Convert.ToInt32(txtFromHours.Text) <= 23) && (Convert.ToInt32(txtFromMinutes.Text) >= 0) && (Convert.ToInt32(txtFromMinutes.Text) <= 59))
                        {
                            if ((Convert.ToInt32(txtToHours.Text) >= 0) && (Convert.ToInt32(txtToHours.Text) <= 23) && (Convert.ToInt32(txtToMinutes.Text) >= 0) && (Convert.ToInt32(txtToMinutes.Text) <= 59))
                            {
                                if ((Convert.ToInt32(txtEvery.Text) >= 0) && (Convert.ToInt32(txtEvery.Text) <= 10000))
                                {
                                    DateTime time = new DateTime();
                                    ti.Every = Convert.ToInt32(txtEvery.Text);
                                    time = new DateTime(1, 1, 1, Convert.ToInt32(txtFromHours.Text), Convert.ToInt32(txtFromMinutes.Text), 0);
                                    ti.BetweenStart = time;
                                    time = new DateTime(1, 1, 1, Convert.ToInt32(txtToHours.Text), Convert.ToInt32(txtToMinutes.Text), 0);
                                    ti.BetweenEnd = time;

                                    foreach (ListItem item in chkWeek.Items)
                                    {
                                        if (item.Selected)
                                        {
                                            ti.Days.Add(item.Value);
                                        }
                                    }
                                    foreach (ListItem item in chkWeekEnd.Items)
                                    {
                                        if (item.Selected)
                                        {
                                            ti.Days.Add(item.Value);
                                        }
                                    }
                                }
                                else
                                {
                                    lblError.Text = error;
                                    txtEvery.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            else
                            {
                                lblError.Text = error;
                                txtToHours.ForeColor = System.Drawing.Color.Red;
                                txtToMinutes.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblError.Text = error;
                            txtFromHours.ForeColor = System.Drawing.Color.Red;
                            txtFromMinutes.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    break;

                case 3: // Day
                    result = new Validator().NotEmpty(txtEvery.Text, empty).IsInteger(txtEvery.Text, error).Result;
                    if (string.IsNullOrEmpty(result))
                    {
                        if ((Convert.ToInt32(txtEvery.Text) >= 0) && (Convert.ToInt32(txtEvery.Text) <= 10000))
                        {
                            ti.Every = Convert.ToInt32(txtEvery.Text);
                            // Days
                            foreach (ListItem item in chkWeek.Items)
                            {
                                if (item.Selected)
                                {
                                    ti.Days.Add(item.Value);
                                }
                            }
                            foreach (ListItem item in chkWeekEnd.Items)
                            {
                                if (item.Selected)
                                {
                                    ti.Days.Add(item.Value);
                                }
                            }
                        }
                        else
                        {
                            lblError.Text = error;
                            txtEvery.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    break;

                case 4: // Week
                    result = new Validator().NotEmpty(txtEvery.Text, empty).IsInteger(txtEvery.Text, error).Result;
                    if (string.IsNullOrEmpty(result))
                    {
                        if ((Convert.ToInt32(txtEvery.Text) >= 0) && (Convert.ToInt32(txtEvery.Text) <= 10000))
                        {
                            ti.Every = Convert.ToInt32(txtEvery.Text);
                        }
                        else
                        {
                            lblError.Text = error;
                            txtEvery.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    break;

                case 5: // Month
                    if (radMonth1.Checked)
                    {
                        ti.Order = drpMonth1.SelectedValue;
                    }
                    else
                    {
                        ti.Order = drpMonth2.SelectedValue;
                        ti.Day = drpMonth3.SelectedValue;
                    }
                    break;

                case 6: // Once
                    break;
            }
        }
        catch
        {
            lblError.Text = error;
            dateTimePicker.DateTimeTextBox.ForeColor = System.Drawing.Color.Red;
        }

        if (!string.IsNullOrEmpty(result))
        {
            lblError.Text = result;
            ti = new TaskInterval();
        }

        mWeekListChecked = true;

        string enti = SchedulingHelper.EncodeInterval(ti);
        return enti;
    }


    /// <summary>
    /// Sets form with values from the schedule interval string.
    /// </summary>
    /// <param name="interval">Schedule interval string</param>
    protected void DecodeInterval(string interval)
    {
        EnsureChildControls();

        if (string.IsNullOrEmpty(interval))
        {
            return;
        }

        // Decode interval string
        TaskInterval ti = new TaskInterval();
        ti = SchedulingHelper.DecodeInterval(interval);

        // Set period type
        drpPeriod.SelectedValue = ti.Period;
        OnPeriodChangeInit();

        // Start time
        dateTimePicker.SelectedDateTime = ti.StartTime;

        switch (drpPeriod.SelectedIndex)
        {
            case 0: // Second
            case 1: // Minute
            case 2: // Hour
                txtEvery.Text = ti.Every.ToString();
                txtFromHours.Text = ti.BetweenStart.TimeOfDay.Hours >= 10 ? ti.BetweenStart.TimeOfDay.Hours.ToString() : "0" + ti.BetweenStart.TimeOfDay.Hours.ToString();
                txtFromMinutes.Text = ti.BetweenStart.TimeOfDay.Minutes >= 10 ? ti.BetweenStart.TimeOfDay.Minutes.ToString() : "0" + ti.BetweenStart.TimeOfDay.Minutes.ToString();
                txtToHours.Text = ti.BetweenEnd.TimeOfDay.Hours >= 10 ? ti.BetweenEnd.TimeOfDay.Hours.ToString() : "0" + ti.BetweenEnd.TimeOfDay.Hours.ToString();
                txtToMinutes.Text = ti.BetweenEnd.TimeOfDay.Minutes >= 10 ? ti.BetweenEnd.TimeOfDay.Minutes.ToString() : "0" + ti.BetweenEnd.TimeOfDay.Minutes.ToString();

                foreach (ListItem li in chkWeek.Items)
                {
                    li.Selected = false;
                    foreach (string Day in ti.Days)
                    {
                        if (li.Value.ToLower() == Day.ToLower())
                        {
                            li.Selected = true;
                        }
                    }
                }
                foreach (ListItem li in chkWeekEnd.Items)
                {
                    li.Selected = false;
                    foreach (string Day in ti.Days)
                    {
                        if (li.Value.ToLower() == Day.ToLower())
                        {
                            li.Selected = true;
                        }
                    }
                }

                break;

            case 3: // Day
                txtEvery.Text = ti.Every.ToString();

                foreach (ListItem li in chkWeek.Items)
                {
                    li.Selected = false;
                    foreach (string Day in ti.Days)
                    {
                        if (li.Value.ToLower() == Day.ToLower())
                        {
                            li.Selected = true;
                        }
                    }
                }
                foreach (ListItem li in chkWeekEnd.Items)
                {
                    li.Selected = false;
                    foreach (string Day in ti.Days)
                    {
                        if (li.Value.ToLower() == Day.ToLower())
                        {
                            li.Selected = true;
                        }
                    }
                }
                break;

            case 4: // Week
                txtEvery.Text = ti.Every.ToString();
                break;

            case 5: // Month
                if (string.IsNullOrEmpty(ti.Day))
                {
                    drpMonth1.SelectedValue = ti.Order.ToLower();
                    radMonth1.Checked = true;
                    radMonth1_CheckedChanged(null, null);
                }
                else
                {
                    drpMonth2.SelectedValue = ti.Order.ToLower();
                    radMonth2.Checked = true;
                    radMonth2_CheckedChanged(null, null);
                    drpMonth3.SelectedValue = ti.Day;
                }
                break;
        }

        mWeekListChecked = true;
    }

    #endregion
}
