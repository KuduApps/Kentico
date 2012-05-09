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

public partial class CMSModules_System_Debug_System_DebugCache : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnClear.Text = GetString("DebugCache.ClearLog");
        btnClearCache.Text = GetString("Administration-System.btnClearCache");
        
        ReloadData();
    }


    protected void ReloadData()
    {
        if (!CacheHelper.DebugCache)
        {
            this.lblInfo.Text = GetString("DebugCache.NotConfigured");
        }
        else
        {
            this.plcLogs.Controls.Clear();

            for (int i = CacheHelper.LastLogs.Count - 1; i >= 0; i--)
            {
                try
                {
                    // Get the log
                    RequestLog log = (RequestLog)CacheHelper.LastLogs[i];
                    if (log != null)
                    {
                        // Load the table
                        DataTable dt = log.LogTable;
                        if (!DataHelper.DataSourceIsEmpty(dt))
                        {
                            // Load the control
                            CacheLog logCtrl = (CacheLog)LoadControl("~/CMSAdminControls/Debug/CacheLog.ascx");
                            logCtrl.ID = "queryLog_" + i;
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
                catch
                {
                }
            }
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        CacheHelper.LastLogs.Clear();
        ReloadData();
    }


    protected void btnClearCache_Click(object sender, EventArgs e)
    {
        CMSFunctions.ClearCache();

        lblInfo.Text = GetString("Administration-System.ClearCacheSuccess");

        ReloadData();
    }
}
