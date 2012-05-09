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
using CMS.Controls;

public partial class CMSModules_System_Debug_System_DebugViewState : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnClear.Text = GetString("DebugViewState.ClearLog");

        ReloadData();
    }


    protected void ReloadData()
    {
        if (!CMSControlsHelper.DebugViewState)
        {
            this.lblInfo.Text = GetString("DebugViewState.NotConfigured");
        }
        else
        {
            this.plcLogs.Controls.Clear();

            for (int i = CMSControlsHelper.LastLogs.Count - 1; i >= 0; i--)
            {
                try
                {
                    // Get the log
                    RequestLog log = (RequestLog)CMSControlsHelper.LastLogs[i];
                    if (log != null)
                    {
                        // Load the table
                        DataTable dt = log.LogTable;
                        if (!DataHelper.DataSourceIsEmpty(dt))
                        {
                            // Load the control
                            ViewStateLog logCtrl = (ViewStateLog)LoadControl("~/CMSAdminControls/Debug/ViewState.ascx");
                            logCtrl.ID = "viewStateLog_" + i;
                            logCtrl.EnableViewState = false;
                            logCtrl.DisplayTotalSize = false;
                            logCtrl.Log = log;
                            logCtrl.LogStyle = "";
                            logCtrl.DisplayOnlyDirty = this.chkOnlyDirty.Checked;

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
        CMSControlsHelper.LastLogs.Clear();
        ReloadData();
    }
}
