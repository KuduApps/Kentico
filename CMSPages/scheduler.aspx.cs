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

using CMS.Scheduler;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSPages_scheduler : CMSPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (!DebugHelper.DebugScheduler)
        {
            DisableDebugging();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();

        // Run the tasks
        string siteName = CMSContext.CurrentSiteName;
        if (siteName != "")
        {            
            SchedulingExecutor.ExecuteScheduledTasks(siteName, WebSyncHelperClass.ServerName);
        }
    }
}
