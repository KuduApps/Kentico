using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_WebAnalytics_Controls_SelectGraphType : CMSAdminControl, IPostBackEventHandler
{
    #region "Variables"

    /// <summary>
    /// Thrown when graph type changed.
    /// </summary>
    public event EventHandler OnGraphTypeChanged;

    private string mDefaultClassName = "GraphTypeButton";
    private bool mUseImages = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Selected graph type.
    /// </summary>
    public HitsIntervalEnum SelectedValue
    {
        get
        {
            return HitsIntervalEnumFunctions.StringToHitsConversion(ValidationHelper.GetString(ViewState["GraphTypePeriod"], "Day"));
        }
        set
        {
            ViewState["GraphTypePeriod"] = value;
        }
    }


    /// <summary>
    /// If true, add images to panels.
    /// </summary>
    public bool UseImages
    {
        get
        {
            return mUseImages;
        }
        set
        {
            mUseImages = value;
        }
    }


    /// <summary>
    /// Default class name - to this class is automatically added sufix "Text" or "Image".
    /// </summary>
    public string DefaultClassName
    {
        get
        {
            return mDefaultClassName;
        }
        set
        {
            mDefaultClassName = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (UseImages)
        {
            imgYear.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_WebAnalytics/year.png");
            imgMonth.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_WebAnalytics/month_31.png");
            imgWeek.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_WebAnalytics/week.png");
            imgDay.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_WebAnalytics/day.png");
            imgHour.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_WebAnalytics/hour.png");

            imgYear.ToolTip = ResHelper.GetString("reports_year.header");
            imgMonth.ToolTip = ResHelper.GetString("reports_month.header");
            imgWeek.ToolTip = ResHelper.GetString("reports_week.header");
            imgDay.ToolTip = ResHelper.GetString("reports_day.header");
            imgHour.ToolTip = ResHelper.GetString("reports_hour.header");

            imgYear.AlternateText = ResHelper.GetString("reports_year.header");
            imgMonth.AlternateText = ResHelper.GetString("reports_month.header");
            imgWeek.AlternateText = ResHelper.GetString("reports_week.header");
            imgDay.AlternateText = ResHelper.GetString("reports_day.header");
            imgHour.AlternateText = ResHelper.GetString("reports_hour.header");

            DefaultClassName += "Image";

            imgYear.Visible = true;
            imgMonth.Visible = true;
            imgWeek.Visible = true;
            imgDay.Visible = true;
            imgHour.Visible = true;
        }
        else
        {
            DefaultClassName += "Text";

            lblYear.Visible = true;
            lblMonth.Visible = true;
            lblWeek.Visible = true;
            lblDay.Visible = true;
            lblHour.Visible = true;
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "GraphTypeChange_" + ClientID,
                ScriptHelper.GetScript(@"function highlightSelectedGraphType_" + ClientID + @" (elem) {
                                            document.getElementById('" + pnlHour.ClientID + "').className = '" + DefaultClassName + @"'
                                            document.getElementById('" + pnlDay.ClientID + "').className = '" + DefaultClassName + @"'
                                            document.getElementById('" + pnlWeek.ClientID + "').className = '" + DefaultClassName + @"'
                                            document.getElementById('" + pnlMonth.ClientID + "').className = '" + DefaultClassName + @"'
                                            document.getElementById('" + pnlYear.ClientID + "').className = 'GraphTypeButtonLast " + DefaultClassName + @"'

                                            elem.className = elem.className+'Selected';
                                        }
        
        function graphTypeSelected_" + ClientID + "(elem) {highlightSelectedGraphType_" + ClientID + "(elem); " + ControlsHelper.GetPostBackEventReference(this, "#", false).Replace("'#'", "elem.id+''") + "}"));

        pnlYear.Attributes.Add("onClick", "graphTypeSelected_" + ClientID + "(this)");
        pnlMonth.Attributes.Add("onClick", "graphTypeSelected_" + ClientID + "(this)");
        pnlWeek.Attributes.Add("onClick", "graphTypeSelected_" + ClientID + "(this)");
        pnlHour.Attributes.Add("onClick", "graphTypeSelected_" + ClientID + "(this)");
        pnlDay.Attributes.Add("onClick", "graphTypeSelected_" + ClientID + "(this)");

        HighlightSelectedType();
    }


    /// <summary>
    /// Highlight selected panel and deselect all others.
    /// </summary>
    public void HighlightSelectedType()
    {
        pnlYear.Attributes.Add("class", DefaultClassName + " GraphTypeButtonLast");
        pnlMonth.Attributes.Add("class", DefaultClassName);
        pnlWeek.Attributes.Add("class", DefaultClassName);
        pnlDay.Attributes.Add("class", DefaultClassName);
        pnlHour.Attributes.Add("class", DefaultClassName);

        switch (SelectedValue)
        {
            case HitsIntervalEnum.Hour:
                pnlHour.Attributes.Add("class", DefaultClassName + "Selected");
                break;

            case HitsIntervalEnum.Day:
                pnlDay.Attributes.Add("class", DefaultClassName + "Selected");
                break;

            case HitsIntervalEnum.Week:
                pnlWeek.Attributes.Add("class", DefaultClassName + "Selected");
                break;

            case HitsIntervalEnum.Month:
                pnlMonth.Attributes.Add("class", DefaultClassName + "Selected");
                break;

            case HitsIntervalEnum.Year:
                pnlYear.Attributes.Add("class", DefaultClassName + "Selected" + " GraphTypeButtonLast");
                break;
        }
    }


    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == pnlHour.ClientID)
        {
            SelectedValue = HitsIntervalEnum.Hour;
        }

        else if (eventArgument == pnlDay.ClientID)
        {
            SelectedValue = HitsIntervalEnum.Day;
        }

        else if (eventArgument == pnlWeek.ClientID)
        {
            SelectedValue = HitsIntervalEnum.Week;
        }

        else if (eventArgument == pnlMonth.ClientID)
        {
            SelectedValue = HitsIntervalEnum.Month;
        }

        else if (eventArgument == pnlYear.ClientID)
        {
            SelectedValue = HitsIntervalEnum.Year;
        }

        HighlightSelectedType();

        // Raise chang graph type event
        if (OnGraphTypeChanged != null)
        {
            OnGraphTypeChanged(this, new EventArgs());
        }
    }

    #endregion
}
