using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSAdminControls_Calendar_Calendar : CMSPage
{
    #region "Variable"

    protected string pagetitle = null;
    protected string mCulture = "";

    protected bool editTime = false;
    protected string selDate = null;
    protected bool allowEmpty = false;
    protected string controlId = null;

    #endregion


    /// <summary>
    /// Constructor.
    /// </summary>
    public CMSAdminControls_Calendar_Calendar()
    {
        mCulture = Page.UICulture;

        // Get UI culture parameter from querystring
        string uiCulture = QueryHelper.GetText("UILang", "");
        if (uiCulture != "")
        {
            // If exists set this culture
            this.UICulture = uiCulture;
            mCulture = uiCulture;
        }
        else
        {
            
            if (CMSContext.CurrentDocument != null)
            {
                // If this parameter doesn't exist, set document culture
                mCulture = CMSContext.CurrentDocument.DocumentCulture;
            }
            else
            {
                // Or system UI culture
                mCulture = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            }
        }
        
        // Set culture to current thread
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(mCulture);
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(mCulture);
    }


    protected void Page_Load(System.Object sender, System.EventArgs e)
    {
        // Get parameters
        selDate = QueryHelper.GetString("selDate", "");
        allowEmpty = QueryHelper.GetBoolean("allowempty", false);
        editTime = QueryHelper.GetBoolean("editTime", false);
        controlId = QueryHelper.GetText("controlid", "");

        SetBrowserClass();

        ScriptHelper.RegisterTooltip(this.Page);

        // Disable caching
        this.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        pagetitle = GetString("Calendar.Title");

        // Fill in the dropdown values
        if (this.drpHours.Items.Count == 0)
        {
            FillNumbers(this.drpHours, 0, 23);
        }
        if (this.drpMinutes.Items.Count == 0)
        {
            FillNumbers(this.drpMinutes, 0, 59);
        }
        if (this.drpSeconds.Items.Count == 0)
        {
            FillNumbers(this.drpSeconds, 0, 59);
        }

        // Fill in the month selector
        if (this.drpMonth.Items.Count == 0)
        {
            FillMonths(this.drpMonth, mCulture);
        }

        // Setup the buttons
        this.btnCancel.Text = GetString("general.cancel");
        this.btnCancel.Attributes.Add("onclick", "window.close(); return false;");

        if (editTime)
        {
            this.btnNow.Text = GetString("Calendar.Now");
        }
        else
        {
            this.btnNow.Text = GetString("Calendar.Today");
        }

        this.btnOk.Text = GetString("general.ok");
        this.btnNA.Text = GetString("general.na");
        this.btnNA.Attributes.Add("onclick", "CloseWindow(" + ScriptHelper.GetString(controlId) + ", ''); return false;");

        // Set content direction according to current culture (LTR/RTL)
        if (CultureHelper.IsCultureRTL(mCulture))
        {
            tblLayout.Attributes.Remove("dir");
            tblLayout.Attributes.Add("dir", "rtl");
        }
        else
        {
            tblLayout.Attributes.Remove("dir");
            tblLayout.Attributes.Add("dir", "ltr");
        }

        if (!RequestHelper.IsPostBack())
        {
            DateTime now = TimeZoneHelper.GetUserDateTime(CMSContext.CurrentUser);
            
            // Selected date
            if (selDate == "")
            {
                SetTime(now, true);
            }
            else
            {
                SetTime(ValidationHelper.GetDateTime(selDate, now), true);
            }

            // Allow empty ?
            if (!allowEmpty)
            {
                btnNA.Visible = false;
            }
            else
            {
                btnNA.Visible = true;
            }

            // Edit time ?
            if (editTime)
            {
                pnlTime.Visible = true;
            }
            else
            {
                pnlTime.Visible = false;
            }
        }

        // Display time zone in form 'GMT+1:00'
        int timeZoneId = QueryHelper.GetInteger("timezoneid", 0);
        if (timeZoneId > 0)
        {
            TimeZoneInfo tzi = TimeZoneInfoProvider.GetTimeZoneInfo(timeZoneId);
            if (tzi != null)
            {
                lblGMTShift.Visible = true;
                lblGMTShift.Text = TimeZoneHelper.GetGMTStringOffset(tzi);
                ScriptHelper.AppendTooltip(lblGMTShift, TimeZoneHelper.GetGMTLongStringOffset(tzi), "help");
            }
        }

        // Next and previous month images
        calDate.NextMonthText = "<img src=\"" + GetImageUrl("Design/Controls/Calendar/next.png") + "\" />";
        calDate.PrevMonthText = "<img src=\"" + GetImageUrl("Design/Controls/Calendar/previous.png") + "\" />";

        RegisterModalPageScripts();
        RegisterEscScript();
    }


    /// <summary>
    /// Fills the dropdownlist with the numbers.
    /// </summary>
    /// <param name="drp">Dropdownlist to fill</param>
    /// <param name="fromnumber">Starting number</param>
    /// <param name="tonumber">Edning number</param>
    private void FillNumbers(DropDownList drp, int fromNumber, int toNumber)
    {
        FillNumbers(drp, fromNumber, toNumber, fromNumber);
    }


    /// <summary>
    /// Fills the dropdownlist with the numbers. Text and value of dropdownlist are
    /// filled separately.
    /// </summary>
    /// <param name="drp">Dropdownlist to fill</param>
    /// <param name="fromnumber">Starting number (text)</param>
    /// <param name="tonumber">Ending number (text)</param>
    /// <param name="valFromNumber">Starting number (value)</param>
    private void FillNumbers(DropDownList drp, int fromnumber, int tonumber, int valFromNumber)
    {
        drp.Items.Clear();
        
        // Get length of number
        int max = tonumber.ToString().Length;
        for (int i = fromnumber; i <= tonumber; i++)
        {
            // Format string number with nulls
            string item = i.ToString().PadLeft(max, '0');
            
            // Add this number to dropdown list
            drp.Items.Add(new ListItem(item, valFromNumber.ToString()));
            valFromNumber++;
        }
    }


    /// <summary>
    /// Fills the dropdownlist with the months values in corect culture format.
    /// </summary>
    /// <param name="drp">Dropdownlist to fill</param>
    /// <param name="culture">Culture</param>
    private void FillMonths(DropDownList drp, string culture)
    {
        drp.Items.Clear();

        // Get DateTime informations
        DateTime dt = DateTimeHelper.GetYearStart(DateTime.Now);
        IFormatProvider ifp = new System.Globalization.CultureInfo(mCulture);

        string item = null;
        
        for (int i = 1; i <= 12; i++)
        {
            item = i.ToString();
            if (i < 10)
            {
                item = "0" + item;
            }

            // Fill dropdown list
            drp.Items.Add(new ListItem(item, dt.ToString("MM", ifp)));
            dt = dt.AddMonths(1);
        }
    }


    /// <summary>
    /// Sets the calendar time to the given time.
    /// </summary>
    /// <param name="time">Time to set</param>
    private void SetTime(DateTime time, bool selectCalendar)
    {
        if (selectCalendar)
        {
            calDate.VisibleDate = time.Date;
            calDate.SelectedDate = time.Date;
        }

        // Get culture specific year
        string cultureYear = time.ToString("yyyy", new System.Globalization.CultureInfo(mCulture));
        int cultureYearInt = ValidationHelper.GetInteger(cultureYear, time.Year);

        FillNumbers(this.drpYear, cultureYearInt - 5, cultureYearInt + 5, time.Year - 5);

        // Set the new time
        this.drpHours.SelectedValue = time.Hour.ToString();
        this.drpMinutes.SelectedValue = time.Minute.ToString();
        this.drpSeconds.SelectedValue = time.Second.ToString();
        this.drpYear.SelectedValue = time.Year.ToString();
        this.drpMonth.SelectedValue = time.ToString("MM", new System.Globalization.CultureInfo(mCulture));
    }


    protected void btnNow_Click(System.Object sender, System.EventArgs e)
    {
        SetTime(TimeZoneHelper.GetUserDateTime(CMSContext.CurrentUser), true);
        //this.ltlScript.Text = ScriptHelper.GetScript("CloseWindow('" + Request.QueryString["controlid"] + "', '" + DateTime.Now.ToString() + "');");
    }

    /// <summary>
    /// Gets selected time from calendar
    /// </summary>
    /// <returns></returns>
    protected DateTime GetCurrentTime()
    {
        DateTime selTime = DateTime.MinValue;
        selTime = calDate.SelectedDate.Date;
        selTime = selTime.AddHours(drpHours.SelectedIndex);
        selTime = selTime.AddMinutes(drpMinutes.SelectedIndex);
        selTime = selTime.AddSeconds(drpSeconds.SelectedIndex);
        return selTime;
    }


    protected void btnOK_Click(System.Object sender, System.EventArgs e)
    {
        DateTime selTime = GetCurrentTime();
        
        // Add 'Close window' script
        if (editTime)
        {
            this.ltlScript.Text = ScriptHelper.GetScript("CloseWindow(" + ScriptHelper.GetString(controlId) + ", '" + selTime.ToString("d", new System.Globalization.CultureInfo(mCulture)) + selTime.ToString(" HH:mm:ss") + "');");
        }
        else
        {
            this.ltlScript.Text = ScriptHelper.GetScript("CloseWindow(" + ScriptHelper.GetString(controlId) + ", '" + selTime.ToString("d", new System.Globalization.CultureInfo(mCulture)) + "');");
        }
    }


    protected void calDate_SelectionChanged(object sender, EventArgs e)
    {
        SetTime(GetCurrentTime(), false);
    }


    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Change DateTime to the new value
            DateTime curTime = GetCurrentTime();
            DateTime newTime = new DateTime(Convert.ToInt32(this.drpYear.SelectedValue), curTime.Month, curTime.Day, curTime.Hour, curTime.Minute, curTime.Second);
            SetTime(newTime, true);
        }
        catch
        {
        }
    }


    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Change DateTime to the new value
            DateTime curTime = GetCurrentTime();
            DateTime newTime = new DateTime(curTime.Year, Convert.ToInt32(this.drpMonth.SelectedValue), curTime.Day, curTime.Hour, curTime.Minute, curTime.Second);
            SetTime(newTime, true);
        }
        catch
        {
        }
    }
}
