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
using System.Net;
using System.Net.Mail;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.EmailEngine;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.UIControls;

public partial class CMSModules_System_Debug_System_DebugRequests : CMSDebugPage
{
    ArrayList logs = new ArrayList();


    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnClear.Text = GetString("DebugRequests.ClearLog");

        ReloadData();
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (logs.Count > 0)
        {
            double totalDuration = 0;

            // Summarize all logs
            foreach (LogControl log in logs)
            {
                totalDuration += log.TotalDuration;
            }

            this.lblInfo.Text = String.Format(GetString("RequestLog.Total"), totalDuration, logs.Count);
        }
        else
        {
            if (RequestHelper.DebugRequests)
            {
                this.lblInfo.Text = GetString("RequestLog.NotFound");
            }
        }

        base.Render(writer);
    }


    protected void ReloadData()
    {
        if (!RequestHelper.DebugRequests)
        {
            this.lblInfo.Text = GetString("DebugRequests.NotConfigured");
        }
        else
        {
            this.plcLogs.Controls.Clear();

            for (int i = RequestHelper.LastLogs.Count - 1; i >= 0; i--)
            {
                try
                {
                    // Get the log
                    RequestLog log = (RequestLog)RequestHelper.LastLogs[i];
                    if (log != null)
                    {
                        // Load the table
                        DataTable dt = log.LogTable;
                        if (!DataHelper.DataSourceIsEmpty(dt))
                        {
                            // Load the control
                            RequestProcessLog logCtrl = (RequestProcessLog)LoadControl("~/CMSAdminControls/Debug/RequestLog.ascx");
                            logCtrl.ID = "requestLog";
                            logCtrl.EnableViewState = false;
                            logCtrl.Log = log;
                            logCtrl.LogStyle = "";

                            // Add to the output
                            this.plcLogs.Controls.Add(new LiteralControl("<div>&lrm;<strong>&nbsp;" + GetRequestLink(log.RequestURL, log.RequestGUID) + "</strong> (" + log.RequestTime.ToString("hh:mm:ss") + ")&lrm;<br /><br />"));
                            this.plcLogs.Controls.Add(logCtrl);
                            this.plcLogs.Controls.Add(new LiteralControl("</div><br /><br />"));

                            logs.Add(logCtrl);
                        }
                    }
                }
                catch //(Exception ex)
                {
                }
            }
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        RequestHelper.LastLogs.Clear();
        logs.Clear();
        ReloadData();
    }
}
