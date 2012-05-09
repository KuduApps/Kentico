using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.PortalEngine;

public partial class CMSWebParts_Viewers_PageViews : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets message text.
    /// </summary>
    public string MessageText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MessageText"), "{0}");
        }
        set
        {
            this.SetValue("MessageText", value);
        }
    }


    /// <summary>
    /// Gets or sets type of statistic type (last day, week, month, year).
    /// </summary>
    public int StatisticsType
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("StatisticsType"), 0);
        }
        set
        {
            this.SetValue("StatisticsType", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        // Check the site
        int siteId = CMSContext.CurrentSiteID;
        if (siteId > 0)
        {
            // Check current page
            PageInfo currentPage = CMSContext.CurrentPageInfo;
            if (currentPage != null)
            {
                int objectId = currentPage.NodeId;

                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                HitsIntervalEnum interval = HitsIntervalEnum.Day;

                // Prepare the parameters
                switch (this.StatisticsType)
                {
                    case 1:     // Week
                        fromDate = DateTimeHelper.GetWeekStart(DateTime.Now);
                        toDate = fromDate.AddDays(7);
                        interval = HitsIntervalEnum.Week;
                        break;

                    case 2:     // Month
                        fromDate = DateTimeHelper.GetMonthStart(DateTime.Now);
                        toDate = fromDate.AddMonths(1);
                        interval = HitsIntervalEnum.Month;
                        break;

                    case 3:     // Year
                        fromDate = DateTimeHelper.GetYearStart(DateTime.Now);
                        toDate = fromDate.AddYears(1);
                        interval = HitsIntervalEnum.Year;
                        break;

                    case 4:
                        fromDate = new DateTime(1753, 1, 1);
                        toDate = new DateTime(9999, 1, 1);
                        interval = HitsIntervalEnum.Year;
                        break;

                    default:    // Day
                        fromDate = DateTimeHelper.GetDayStart(DateTime.Now);
                        toDate = fromDate.AddDays(1);
                        interval = HitsIntervalEnum.Day;
                        break;
                }

                // Get the text
                lblInfo.Text = String.Format(this.MessageText, HitsInfoProvider.GetObjectHitCount(siteId, objectId, interval, "pageviews", fromDate, toDate));
            }
        }
    }
}
