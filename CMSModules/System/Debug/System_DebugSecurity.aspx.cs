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

public partial class CMSModules_System_Debug_System_DebugSecurity : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnClear.Text = GetString("DebugSecurity.ClearLog");

        ReloadData();
    }


    protected void ReloadData()
    {
        if (!SecurityHelper.DebugSecurity)
        {
            this.lblInfo.Text = GetString("DebugSecurity.NotConfigured");
        }
        else
        {
            this.plcLogs.Controls.Clear();

            for (int i = SecurityHelper.LastLogs.Count - 1; i >= 0; i--)
            {
                try
                {
                    // Get the log
                    RequestLog log = (RequestLog)SecurityHelper.LastLogs[i];
                    if (log != null)
                    {
                        // Load the table
                        DataTable dt = log.LogTable;
                        if (!DataHelper.DataSourceIsEmpty(dt))
                        {
                            // Log the control
                            SecurityLog logCtrl = (SecurityLog)LoadControl("~/CMSAdminControls/Debug/SecurityLog.ascx");
                            logCtrl.ID = "logSec_" + i;
                            logCtrl.EnableViewState = false;
                            logCtrl.Log = log;
                            logCtrl.LogStyle = "";
                            logCtrl.ShowCompleteContext = this.chkCompleteContext.Checked;

                            // Add to the output
                            this.plcLogs.Controls.Add(new LiteralControl("<div>&lrm;<strong>&nbsp;" + GetRequestLink(log.RequestURL, log.RequestGUID) + "</strong> (" + log.RequestTime.ToString("hh:mm:ss") + ")&lrm;<br /><br />"));
                            this.plcLogs.Controls.Add(logCtrl);
                            this.plcLogs.Controls.Add(new LiteralControl("</div><br /><br />"));
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
        SecurityHelper.LastLogs.Clear();
        ReloadData();
    }
}
