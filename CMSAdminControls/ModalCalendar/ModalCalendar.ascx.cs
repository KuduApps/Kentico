using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.IO;
using CMS.CMSHelper;

using CultureInfo = System.Globalization.CultureInfo;

public partial class CMSAdminControls_ModalCalendar_ModalCalendar : CMSCustomCalendarControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTimePicker datePickerObject = PickerControl as DateTimePicker;
        if (datePickerObject == null)
        {
            return;
        }

        // Default settings
        int numberOfRows = 6;
        // Buttons NotAvaible,Done,Now
        bool showActionPanel = true;
        // Display OK button
        bool displayOK = true;
        // Text for N/A button
        string naText = ResHelper.GetString("general.notavailable");
        // Hides if date is selected
        bool hideOnDateSelection = false;
        // Display seconds 
        bool displaySeconds = true;
        // Add icon - triggers datepicker show
        string shownOn = "button";

        LoadResources(datePickerObject);

        CultureInfo culture = new CultureInfo(datePickerObject.CultureCode, true);
        DateTimeFormatInfo info = culture.DateTimeFormat;

        // Register default css a js files
        ScriptHelper.RegisterJQuery(Page);
        ScriptHelper.RegisterScriptFile(Page, "~/CMSScripts/JQuery/jquery-ui-datetimepicker.js");
        ScriptHelper.RegisterScriptFile(Page, "~/CMSScripts/modalCalendar.js");
        CSSHelper.RegisterCSSLink(Page, "~/CMSAdminControls/ModalCalendar/Themes/Calendar_core.css");

        string datePattern = info.ShortDatePattern.Replace("yyyy", "yy").Replace("'", "");
        if (datePickerObject.EditTime)
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
        string todayText = datePickerObject.EditTime ? ResHelper.GetString("calendar.now") : ResHelper.GetString("Calendar.Today");
        string localize = String.Format("monthNames:{0},monthNamesShort:{1},dayNames:{2},dayNamesMin:{3},firstDay:{4},", ArrayToString(info.MonthNames), ArrayToString(info.AbbreviatedMonthNames), ArrayToString(info.DayNames), ArrayToString(info.ShortestDayNames), ConvertFirstDayToNumber(info.FirstDayOfWeek));
        localize += String.Format("AMDesignator:'{0}',PMDesignator:'{1}',NAText:'{2}',closeText:'{3}',isRTL:{4},prevText:'{5}',nextText:'{6}',defaultDate:'{7}'", info.AMDesignator.Replace("'", "\\'"), info.PMDesignator.Replace("'", "\\'"), naText.Replace("'", "\\'"), ResHelper.GetString("general.ok").Replace("'", "\\'"), culture.TextInfo.IsRightToLeft.ToString().ToLower(), ResHelper.GetString("calendar.previous").Replace("'", "\\'"), ResHelper.GetString("calendar.next").Replace("'", "\\'"), now);

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

        // Other settings
        String buttons = "[" + (displayOK ? "'ok'" : "") + "," + (datePickerObject.AllowEmptyValue ? "'na'" : "") + "," + (datePickerObject.DisplayNow ? "'now'" : "") + "]";
        String initParameters = minDate + maxDate + String.Format("numberOfRows:{0},showTimePanel:{1},use24HourMode:{2},showButtonPanel:{3},actionPanelButtons:{4},hideOnDateSelection:{5},displaySeconds:{6},", numberOfRows, datePickerObject.EditTime.ToString().ToLower(), use24HourMode.ToString().ToLower(), showActionPanel.ToString().ToLower(), buttons, hideOnDateSelection.ToString().ToLower(), displaySeconds.ToString().ToLower());
        initParameters += String.Format("IconID:'{0}',showOn:'{1}',dateFormat:'{2}',currentText:'{3}',timeZoneOffset:{4},selectOtherMonths:true,showOtherMonths:true,changeMonth:true,changeYear:true", datePickerObject.CalendarImageClientID, shownOn, datePattern, todayText.Replace("'", "\\'"), datePickerObject.TimeZoneOffset);
        // Init calendar
        string calendarInit = "$j(function() {$j('#" + datePickerObject.DateTimeTextBox.ClientID + "').datepicker({" + localize + "," + initParameters + "})});";

        ScriptHelper.RegisterClientScriptBlock(Page, GetType(), ClientID + "_RegisterDatePickerFunction", ScriptHelper.GetScript(calendarInit));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DTPickerModalSelectDate" + datePickerObject.DateTimeTextBox.ClientID, ScriptHelper.GetScript(
           "function SelectModalDate_" + datePickerObject.DateTimeTextBox.ClientID + "(param, pickerId) { " + this.Page.ClientScript.GetCallbackEventReference(datePickerObject, "param", "SetDateModal", "pickerId") + " } \n"));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DTPickerModalSetDate", ScriptHelper.GetScript("function SetDateModal(result, context) {$j('#'+context).datepicker('setDateNoTextBox',result); } \n"));
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
        return ret.ToString().TrimEnd(',') + "]";
    }


    /// <summary>
    /// Load resources for calendar control.
    /// </summary>
    /// <param name="datePickerObject">Calendar control with settings</param>
    private void LoadResources(DateTimePicker datePickerObject)
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
