using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using CMS.Controls;
using CMS.IO;

public partial class CMSModules_System_Debug_System_DebugAll : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnClear.Text = GetString("DebugAll.ClearLog");

        ReloadData();
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
                    // Get the request log
                    RequestLog log = (RequestLog)RequestHelper.LastLogs[i];
                    if (log != null)
                    {
                        List<DataTable> logs = AllLog.GetLogs(log);

                        // Load the control only if there is more than only request log
                        if (logs.Count > 1)
                        {
                            AllLog logCtrl = (AllLog)LoadControl("~/CMSAdminControls/Debug/AllLog.ascx");
                            logCtrl.ID = "allLog_" + i;
                            logCtrl.EnableViewState = false;
                            logCtrl.Logs = logs;
                            logCtrl.LogStyle = "";
                            logCtrl.ShowCompleteContext = this.chkCompleteContext.Checked;

                            // Add to the output
                            this.plcLogs.Controls.Add(new LiteralControl("<div>&lrm;<strong>&nbsp;" + GetRequestLink(log.RequestURL, log.RequestGUID) + "</strong> (" + log.RequestTime.ToString("hh:mm:ss") + ")&lrm;<br /><br />"));
                            this.plcLogs.Controls.Add(logCtrl);
                            this.plcLogs.Controls.Add(new LiteralControl("</div><br /><br />"));
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        File.LastLogs.Clear();
        SqlHelperClass.LastLogs.Clear();
        SecurityHelper.LastLogs.Clear();
        MacroResolver.LastLogs.Clear();
        CacheHelper.LastLogs.Clear();
        RequestHelper.LastLogs.Clear();
        ReloadData();
    }
}
