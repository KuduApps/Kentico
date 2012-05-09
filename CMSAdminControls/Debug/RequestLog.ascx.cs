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

public partial class CMSAdminControls_Debug_RequestLog : RequestProcessLog
{
    #region "Variables"

    protected DateTime firstTime = DateTime.MinValue;
    protected DateTime lastTime = DateTime.MinValue;

    #endregion


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
            this.Visible = true;

            // Log request values
            RequestHelper.LogRequestValues(true, true, true);

            if (this.Log.ValueCollections != null)
            {
                this.tblResC.Title = GetString("RequestLog.ResponseCookies");
                this.tblResC.Table = this.Log.ValueCollections.Tables["ResponseCookies"];

                this.tblReqC.Title = GetString("RequestLog.RequestCookies");
                this.tblReqC.Table = this.Log.ValueCollections.Tables["RequestCookies"];

                this.tblVal.Title = GetString("RequestLog.Values");
                this.tblVal.Table = this.Log.ValueCollections.Tables["Values"];
            }

            // Get the log table
            DataTable dt = this.Log.LogTable;
            if (!DataHelper.DataSourceIsEmpty(dt))
            {
                RequestProcessLog.EnsureDurationColumn(dt);

                gridCache.Columns[1].HeaderText = GetString("RequestLog.Operation");
                gridCache.Columns[2].HeaderText = GetString("RequestLog.Parameter");
                gridCache.Columns[3].HeaderText = GetString("RequestLog.FromStart");
                gridCache.Columns[4].HeaderText = GetString("RequestLog.Duration");

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("RequestLog.Info") + "</div>";
                }

                this.MaxDuration = DataHelper.GetMaximumValue<double>(dt, "Duration");

                // Bind the data
                gridCache.DataSource = dt;
                gridCache.DataBind();
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


    protected string GetBeginIndent(object ind)
    {
        int indent = ValidationHelper.GetInteger(ind, 0);
        string result = "";
        for (int i = 0; i < indent; i++)
        {
            result += "&gt;"; //"<div style=\"padding-left: 10px;\">";
        }

        if (indent > 0)
        {
            result += "&nbsp;";
        }

        return result;
    }


    protected string GetEndIndent(object ind)
    {
        int indent = ValidationHelper.GetInteger(ind, 0);
        string result = "";
        /*
        for (int i = 0; i < indent; i++)
        {
            result += "</div>";
        }*/

        return result;
    }


    /// <summary>
    /// Gets the time from the first action.
    /// </summary>
    /// <param name="time">Time of the action</param>
    protected string GetFromStart(object time)
    {
        DateTime t = ValidationHelper.GetDateTime(time, DateTime.MinValue);
        if (firstTime == DateTime.MinValue)
        {
            firstTime = t;
        }

        lastTime = t;
        this.TotalDuration = t.Subtract(firstTime).TotalSeconds;

        return this.TotalDuration.ToString("F3");
    }
}
