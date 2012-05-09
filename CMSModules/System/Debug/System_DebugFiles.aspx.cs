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
using CMS.IO;

public partial class CMSModules_System_Debug_System_DebugFiles : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnClear.Text = GetString("DebugFiles.ClearLog");

        ReloadData();
    }


    protected void ReloadData()
    {
        if (StorageHelper.IsExternalStorage)
        {
            this.drpOperationType.Visible = true;
            this.lblOperationType.Visible = true;
            if (!RequestHelper.IsPostBack())
            {
                this.drpOperationType.Items.Add(new ListItem(GetString("general.selectall"), ""));
                this.drpOperationType.Items.Add(new ListItem(GetString("general.general"), IOOperationType.General.ToString()));
                this.drpOperationType.Items.Add(new ListItem(GetString("FilesLog.TypeAzureBlob"), IOOperationType.AzureBlob.ToString()));
            }
        }

        if (!File.DebugFiles)
        {
            this.lblInfo.Text = GetString("DebugFiles.NotConfigured");
        }
        else
        {
            this.plcLogs.Controls.Clear();

            for (int i = File.LastLogs.Count - 1; i >= 0; i--)
            {
                try
                {
                    // Get the log
                    RequestLog log = (RequestLog)File.LastLogs[i];
                    if (log != null)
                    {
                        // Load the table
                        DataTable dt = log.LogTable;
                        if (!DataHelper.DataSourceIsEmpty(dt))
                        {
                            bool display = true;
                            IOOperationType operationType = IOOperationType.All;
                            if (StorageHelper.IsExternalStorage)
                            {
                                string type = (this.drpOperationType.SelectedValue == null ? "" : this.drpOperationType.SelectedValue.ToLower());
                                switch (type)
                                {
                                    case "azureblob":
                                        operationType = IOOperationType.AzureBlob;
                                        break;

                                    case "general":
                                        operationType = IOOperationType.General;
                                        break;
                                }

                                // Apply operation type filter
                                DataView dv = new DataView(dt);
                                if (operationType != IOOperationType.All)
                                {
                                    dv.RowFilter = "OperationType = '" + operationType.ToString() + "'";
                                }

                                display = !DataHelper.DataSourceIsEmpty(dv);
                            }

                            if (display)
                            {
                                // Load the control
                                FilesLog logCtrl = (FilesLog)LoadControl("~/CMSAdminControls/Debug/FilesLog.ascx");
                                logCtrl.ID = "filesLog_" + i;
                                logCtrl.EnableViewState = false;
                                logCtrl.Log = log;
                                logCtrl.LogStyle = "";
                                logCtrl.ShowCompleteContext = this.chkCompleteContext.Checked;
                                logCtrl.OperationTypeFilter = operationType;

                                // Add to the output
                                this.plcLogs.Controls.Add(new LiteralControl("<div>&lrm;<strong>&nbsp;" + GetRequestLink(log.RequestURL, log.RequestGUID) + "</strong> (" + log.RequestTime.ToString("hh:mm:ss") + ")&lrm;<br /><br />"));
                                this.plcLogs.Controls.Add(logCtrl);
                                this.plcLogs.Controls.Add(new LiteralControl("</div><br /><br />"));
                            }
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
        ReloadData();
    }
}
