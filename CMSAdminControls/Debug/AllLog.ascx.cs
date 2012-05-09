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
using System.Reflection;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.IO;
using CMS.SiteProvider;

public partial class CMSAdminControls_Debug_AllLog : AllLog
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableViewState = false;
        this.Visible = true;

        if (!this.StopProcessing)
        {
            gridDebug.Columns[1].HeaderText = GetString("AllLog.DebugType");
            gridDebug.Columns[2].HeaderText = GetString("AllLog.Information");
            gridDebug.Columns[3].HeaderText = GetString("AllLog.Result");
            gridDebug.Columns[4].HeaderText = GetString("General.Context");
            gridDebug.Columns[5].HeaderText = GetString("AllLog.TotalDuration");
            gridDebug.Columns[6].HeaderText = GetString("AllLog.Duration");

            this.gridDebug.DataSource = MergeTables(this.Logs, this.Page, this.ShowCompleteContext);
            this.gridDebug.DataBind();
        }
    }
}
