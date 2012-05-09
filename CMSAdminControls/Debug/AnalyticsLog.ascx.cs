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
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using System.Text;
using CMS.WebAnalytics;

public partial class CMSAdminControls_Debug_AnalyticsLog : AnalyticsLog
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableViewState = false;
        this.Visible = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = false;

        if (this.Log != null)
        {
            // Get the log table
            DataTable dt = this.Log.LogTable;
            if (!DataHelper.DataSourceIsEmpty(dt))
            {
                this.Visible = true;

                gridAnalytics.Columns[1].HeaderText = GetString("General.CodeName");
                gridAnalytics.Columns[2].HeaderText = GetString("General.Object");
                gridAnalytics.Columns[3].HeaderText = GetString("General.Count");
                gridAnalytics.Columns[4].HeaderText = GetString("General.SiteName");
                gridAnalytics.Columns[5].HeaderText = GetString("General.Context");

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("AnalyticsLog.Info") + "</div>";
                }

                gridAnalytics.DataSource = dt;
                gridAnalytics.DataBind();
            }
        }
    }


    /// <summary>
    /// Gets the item index.
    /// </summary>
    protected int GetIndex()
    {
        return ++index;
    }


    /// <summary>
    /// Gets the item information
    /// </summary>
    /// <param name="data">Activity data</param>
    /// <param name="objectName">Object name</param>
    /// <param name="culture">Culture</param>
    /// <param name="objectId">Object ID</param>
    protected string GetInformation(object data, object objectName, object culture, object objectId)
    {
        StringBuilder sb = new StringBuilder();

        // Get activity data
        ActivityData activityData = null;
        if (data is ActivityData)
        {
            activityData = (ActivityData)data;
        }
        else
        {
            // Standard web analytics action
            sb.Append(objectName);

            int id = ValidationHelper.GetInteger(objectId, 0);
            if (id > 0)
            {
                sb.Append(objectId);
            }

            string stringCulture = ValidationHelper.GetString(culture, null);
            if (!String.IsNullOrEmpty(stringCulture))
            {
                sb.Append(" (", stringCulture, ")");
            }
        }

        return sb.ToString();
    }


    /// <summary>
    /// Gets the count/value information
    /// </summary>
    /// <param name="count">Count</param>
    /// <param name="value">Value</param>
    protected string GetCount(object count, object value)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(count);

        double dValue = ValidationHelper.GetDouble(value, 0);
        if (dValue > 0)
        {
            sb.Append(" (", dValue, ")");
        }

        return sb.ToString();
    }
}
