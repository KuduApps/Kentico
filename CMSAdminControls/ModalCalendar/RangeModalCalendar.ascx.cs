using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.CMSHelper;

public partial class CMSAdminControls_ModalCalendar_RangeModalCalendar : CMSCustomCalendarControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RangeDateTimePicker datePickerObject = PickerControl as RangeDateTimePicker;
        if (datePickerObject == null)
        {
            return;
        }

        // Default settings
        int numberOfRows = 6;
        // Buttons NotAvaible,Done,Now
        bool showActionPanel = false;
        // Hides if date is selected
        bool hideOnDateSelection = false;
        // Display seconds 
        bool displaySeconds = true;
        // Add icon - triggers datepicker show
        string shownOn = "button";
        // If true display time
        bool displayTime = datePickerObject.EditTime && !datePickerObject.DisableDaySelect && !datePickerObject.DisableMonthSelect;

        string minDate = String.Empty;
        string maxDate = String.Empty;

        if (datePickerObject.UseCalendarLimit)
        {
            if (datePickerObject.MinDate != DateTimeHelper.ZERO_TIME)
            {
                minDate = "minDate: " + (datePickerObject.MinDate.Date - DateTime.Now.Date).Days.ToString() + ",";
            }

            if (datePickerObject.MaxDate != DateTimeHelper.ZERO_TIME)
            {
                maxDate = "maxDate: " + (datePickerObject.MaxDate.Date - DateTime.Now.Date).Days.ToString() + ",";
            }
        }

        LoadResources(datePickerObject);

        int stepMonth = datePickerObject.DisableMonthSelect ? 12 : 1;

        CultureInfo culture = new CultureInfo(datePickerObject.CultureCode, true);
        DateTimeFormatInfo info = culture.DateTimeFormat;

        // Register default css a js files
        ScriptHelper.RegisterJQuery(Page);
        ScriptHelper.RegisterScriptFile(Page, "~/CMSScripts/JQuery/jquery-ui-datetimepicker.js");
        ScriptHelper.RegisterScriptFile(Page, "~/CMSScripts/modalCalendar.js");
        CSSHelper.RegisterCSSLink(Page, "~/CMSAdminControls/ModalCalendar/Themes/Calendar_core.css");

        string datePattern = info.ShortDatePattern.Replace("yyyy", "yy").Replace("'", "");
        if (displayTime)
        {
            datePattern += " " + (displaySeconds ? info.LongTimePattern : info.ShortTimePattern);
        }

        bool use24HourMode = !datePattern.Contains("tt");

        // Generate 'now' string with full year
        string format = info.ShortDatePattern;
        if (Regex.Matches(format, "y").Count == 2)
        {
            format = format.Replace("yy", "yyyy");
        }

        string now = CMSContext.DateTimeConvert(DateTime.Now, datePickerObject.TimeZone, datePickerObject.CustomTimeZone).ToString(format, culture);

        // Localized settings
        string localize = String.Format("monthNames:{0},monthNamesShort:{1},dayNames:{2},dayNamesMin:{3},firstDay:{4},", ArrayToString(info.MonthNames), ArrayToString(info.AbbreviatedMonthNames), ArrayToString(info.DayNames), ArrayToString(info.ShortestDayNames), ConvertFirstDayToNumber(info.FirstDayOfWeek));
        localize += String.Format("AMDesignator:'{0}',PMDesignator:'{1}',closeText:'{2}',isRTL:{3},prevText:'{4}',nextText:'{5}',defaultDate:'{6}'", info.AMDesignator.Replace("'", "\\'"), info.PMDesignator.Replace("'", "\\'"), ResHelper.GetString("general.ok").Replace("'", "\\'"), culture.TextInfo.IsRightToLeft.ToString().ToLower(), ResHelper.GetString("calendar.previous").Replace("'", "\\'"), ResHelper.GetString("calendar.next").Replace("'", "\\'"), now);

        // Classic settings
        String initParameters = String.Format("numberOfRows:{0},showTimePanel:{1},use24HourMode:{2},showButtonPanel:{3},hideOnDateSelection:{4},displaySeconds:{5},", numberOfRows, displayTime.ToString().ToLower(), use24HourMode.ToString().ToLower(), showActionPanel.ToString().ToLower(), hideOnDateSelection.ToString().ToLower(), displaySeconds.ToString().ToLower());
        initParameters += String.Format("IconID:'{0}',showOn:'{1}',dateFormat:'{2}',disableDaySelect:{3},disableMonthSelect:{4},stepMonths:{5},changeMonth:{6},timeZoneOffset:{7},selectOtherMonths:true,showOtherMonths:true,changeYear:true", datePickerObject.CalendarImageClientID, shownOn, datePattern, datePickerObject.DisableDaySelect.ToString().ToLower(), datePickerObject.DisableMonthSelect.ToString().ToLower(), stepMonth, (!datePickerObject.DisableMonthSelect).ToString().ToLower(), datePickerObject.TimeZoneOffset);

        // Init first calendar
        string calendarInit = "$j(function() {$j('#" + dateFrom.ClientID + "').datepicker({" + minDate + maxDate + localize + "," + initParameters + ",defaultTimeValue:" + (datePickerObject.UseDynamicDefaultTime ? 1 : 0) + "});";
        // Init second calendar
        calendarInit += "$j('#" + dateTo.ClientID + "').datepicker({" + minDate + maxDate + localize + "," + initParameters + ",defaultTimeValue:" + (datePickerObject.UseDynamicDefaultTime ? 2 : 0) + "})});";

        ScriptHelper.RegisterClientScriptBlock(Page, GetType(), ClientID + "_RegisterDatePickerFunction", ScriptHelper.GetScript(calendarInit));

        // Button OK
        btnOK.OnClientClick = "$j('#" + datePickerObject.DateTimeTextBox.ClientID + "').val ($j('#" + dateFrom.ClientID + "').datepicker ('getFormattedDate'));";
        btnOK.OnClientClick += "$j('#" + datePickerObject.AlternateDateTimeTextBox.ClientID + "').val ($j('#" + dateTo.ClientID + "').datepicker ('getFormattedDate'));";
        btnOK.OnClientClick += "$j('#" + rangeCalendar.ClientID + "').hide();";

        if (datePickerObject.DisplayNAButton)
        {
            btnNA.OnClientClick = "$j('#" + rangeCalendar.ClientID + "').hide(); $j('#" + datePickerObject.DateTimeTextBox.ClientID + "').val('');$j('#" + datePickerObject.AlternateDateTimeTextBox.ClientID + "').val('');return false;";
            btnNA.Visible = true;
        }

        if (!datePickerObject.PostbackOnOK)
        {
            btnOK.OnClientClick += "return false;";
        }

        // Icon
        string clickScript = " if( $j('#" + rangeCalendar.ClientID + @"').is(':hidden')) { 
            $j('#" + dateFrom.ClientID + "').datepicker('setDate',$j('#" + datePickerObject.DateTimeTextBox.ClientID + @"').val());     
            $j('#" + dateTo.ClientID + "').datepicker('setDate',$j('#" + datePickerObject.AlternateDateTimeTextBox.ClientID + @"').val());                           
            var offset = localizeRangeCalendar($j('#" + rangeCalendar.ClientID + "'),$j('#" + datePickerObject.DateTimeTextBox.ClientID + "')," + CultureHelper.IsUICultureRTL().ToString().ToLower() + ",true);" +
        "$j('#" + rangeCalendar.ClientID + "').css({left:offset.left+'px',top:offset.top+'px'});$j('#" + rangeCalendar.ClientID + "').show();}return false;";

        // Add image on click
        String script = "$j(\"#" + datePickerObject.CalendarImageClientID + "\").click(function() {" + clickScript + "});";
        ScriptHelper.RegisterStartupScript(this, typeof(string), ClientID + "CalendarImageInitScript", ScriptHelper.GetScript(script));

        datePickerObject.DateTimeTextBox.Attributes["OnClick"] = clickScript;
        datePickerObject.AlternateDateTimeTextBox.Attributes["OnClick"] = clickScript;

        // Script for hiding calendar when clicked somewhere else
        ltlScript.Text = ScriptHelper.GetScript(@"$j(document).mousedown(function(event) { 
                var target = $j(event.target);    
                if ((target.closest('#" + rangeCalendar.ClientID + "').length == 0) && (target.parents('#" + rangeCalendar.ClientID + @"').length == 0) 
                 && (target.closest('#" + datePickerObject.CalendarImageClientID + "').length == 0) && (target.closest('#" + datePickerObject.DateTimeTextBox.ClientID + @"').length == 0) && (target.closest('#" + datePickerObject.AlternateDateTimeTextBox.ClientID + @"').length == 0))
                        $j('#" + rangeCalendar.ClientID + @"').hide(); 
                });");
    }


    /// <summary>
    /// Load resources for calendar control.
    /// </summary>
    /// <param name="datePickerObject">Calendar control with settings</param>
    private void LoadResources(RangeDateTimePicker datePickerObject)
    {
        if (Directory.Exists(Server.MapPath(datePickerObject.CustomCalendarSupportFolder)))
        {
            // Register JS files
            string[] files = Directory.GetFiles(Server.MapPath(datePickerObject.CustomCalendarSupportFolder), "*.js");
            string path = Server.MapPath("~/");
            foreach (string file in files)
            {
                string relative = "~/" + file.Substring(path.Length).Replace(@"\", "/"); ;
                ScriptHelper.RegisterScriptFile(Page, relative);
            }

            // Register CSS files
            files = Directory.GetFiles(Server.MapPath(datePickerObject.CustomCalendarSupportFolder), "*.css");
            foreach (string file in files)
            {
                string relative = "~/" + file.Substring(path.Length).Replace(@"\", "/"); ;
                CSSHelper.RegisterCSSLink(Page, relative);
            }
        }
    }


    /// <summary>
    /// Converts array to string in special format.
    /// </summary>
    /// <param name="arr">Input array</param>
    private string ArrayToString(string[] arr)
    {
        StringBuilder ret = new StringBuilder("[");
        foreach (string str in arr)
        {
            ret.Append("'" + str.Replace("'", "\\'") + "',");
        }
        return ret.ToString().TrimEnd(new char[] { ',' }) + "]";
    }


    /// <summary>
    /// Convert starting day of week from enum to number - passed to calendar jquery control.
    /// </summary>
    /// <param name="name">Day name</param>
    protected int ConvertFirstDayToNumber(DayOfWeek name)
    {
        switch (name)
        {
            case DayOfWeek.Monday:
                return 1;

            case DayOfWeek.Tuesday:
                return 2;

            case DayOfWeek.Wednesday:
                return 3;

            case DayOfWeek.Thursday:
                return 4;

            case DayOfWeek.Friday:
                return 5;

            case DayOfWeek.Saturday:
                return 6;

            default:
                return 0;
        }
    }
}
