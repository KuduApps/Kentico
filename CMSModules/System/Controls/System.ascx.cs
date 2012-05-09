using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.EventLog;
using CMS.DataEngine;
using CMS.IO;
using CMS.PortalControls;
using CMS.WebFarmSync;
using CMS.SiteProvider;
using CMS.LicenseProvider;

public partial class CMSModules_System_Controls_System : CMSAdminControl
{
    #region "Variables"

    private static DateTime mLastRPS = DateTime.MinValue;

    private static long mLastPageRequests = 0;
    private static long mLastNonPageRequests = 0;
    private static long mLastSystemPageRequests = 0;
    private static long mLastPageNotFoundRequests = 0;
    private static long mLastGetFileRequests = 0;

    private static double mRPSPageRequests = -1;
    private static double mRPSNonPageRequests = -1;
    private static double mRPSSystemPageRequests = -1;
    private static double mRPSPageNotFoundRequests = -1;
    private static double mRPSGetFileRequests = -1;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the refresh interval (in seconds) for the page. If set to 0 then no refresh.
    /// </summary>
    public int RefreshInterval
    {
        get
        {
            return (int)(timRefresh.Interval / 1000);
        }
        set
        {
            if (value == 0)
            {
                timRefresh.Enabled = false;
            }
            else
            {
                timRefresh.Enabled = true;
                timRefresh.Interval = value * 1000;
                if (this.drpRefresh.Items.FindByValue(value.ToString()) != null)
                {
                    this.drpRefresh.SelectedValue = value.ToString();
                }
            }
        }
    }

    #endregion


    #region "Page Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.timRefresh.Interval = 1000 * ValidationHelper.GetInteger(this.drpRefresh.SelectedValue, 0);

        // Check permissions            
        RaiseOnCheckPermissions("READ", this);

        if (StopProcessing)
        {
            return;
        }

        // Get values from counters
        long totalSystemRequests = RequestHelper.TotalSystemPageRequests.GetValue(null);
        long totalPageRequests = RequestHelper.TotalPageRequests.GetValue(null);
        long totalPageNotFoundRequests = RequestHelper.TotalPageNotFoundRequests.GetValue(null);
        long totalNonPageRequests = RequestHelper.TotalNonPageRequests.GetValue(null);
        long totalGetFileRequests = RequestHelper.TotalGetFileRequests.GetValue(null);

        // Reevaluate RPS
        if (mLastRPS != DateTime.MinValue)
        {
            double seconds = DateTime.Now.Subtract(mLastRPS).TotalSeconds;
            if ((seconds < 3) && (seconds > 0))
            {
                mRPSSystemPageRequests = (totalSystemRequests - mLastSystemPageRequests) / seconds;
                mRPSPageRequests = (totalPageRequests - mLastPageRequests) / seconds;
                mRPSPageNotFoundRequests = (totalPageNotFoundRequests - mLastPageNotFoundRequests) / seconds;
                mRPSNonPageRequests = (totalNonPageRequests - mLastNonPageRequests) / seconds;
                mRPSGetFileRequests = (totalGetFileRequests - mLastGetFileRequests) / seconds;
            }
            else
            {
                mRPSGetFileRequests = -1;
                mRPSNonPageRequests = -1;
                mRPSPageNotFoundRequests = -1;
                mRPSPageRequests = -1;
                mRPSSystemPageRequests = -1;
            }
        }

        mLastRPS = DateTime.Now;

        // Update last values
        mLastGetFileRequests = totalGetFileRequests;
        mLastNonPageRequests = totalNonPageRequests;
        mLastPageNotFoundRequests = totalPageNotFoundRequests;
        mLastPageRequests = totalPageRequests;
        mLastSystemPageRequests = totalSystemRequests;

        // Do not count this page with async postback
        if (RequestHelper.IsAsyncPostback())
        {
            RequestHelper.TotalSystemPageRequests.Decrement(null);
        }

        pnlSystemInfo.GroupingText = GetString("Administration-System.SystemInfo");
        pnlSystemTime.GroupingText = GetString("Administration-System.SystemTimeInfo");

        pnlDatabaseInfo.GroupingText = GetString("Administration-System.DatabaseInfo");
        lblServerName.Text = GetString("Administration-System.ServerName");
        lblServerVersion.Text = GetString("Administration-System.ServerVersion");
        lblDBName.Text = GetString("Administration-System.DatabaseName");
        lblDBSize.Text = GetString("Administration-System.DatabaseSize");

        lblMachineName.Text = GetString("Administration-System.MachineName");
        lblMachineNameValue.Text = HTTPHelper.MachineName;

        lblAspAccount.Text = GetString("Administration-System.Account");
        lblValueAspAccount.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;


        // Get application pool name
        lblPool.Text = GetString("Administration-System.Pool");
        lblValuePool.Text = GetString("General.NA");

        try
        {
            lblValuePool.Text = SystemHelper.GetApplicationPoolName();
        }
        catch
        {
        }

        // Get application trust level
        lblTrustLevel.Text = GetString("Administration-System.TrustLevel");
        lblValueTrustLevel.Text = GetString("General.NA");

        try
        {
            lblValueTrustLevel.Text = SystemHelper.CurrentTrustLevel.ToString();
        }
        catch
        {
        }

        lblAspVersion.Text = GetString("Administration-System.Version");
        lblValueAspVersion.Text = System.Environment.Version.ToString();

        pnlMemory.GroupingText = GetString("Administration-System.MemoryStatistics");

        lblAlocatedMemory.Text = GetString("Administration-System.Memory");
        lblPeakMemory.Text = GetString("Administration-System.PeakMemory");
        lblVirtualMemory.Text = GetString("Administration-System.VirtualMemory");
        lblPhysicalMemory.Text = GetString("Administration-System.PhysicalMemory");
        lblIP.Text = GetString("Administration-System.IP");

        pnlPageViews.GroupingText = GetString("Administration-System.PageViews");
        lblPageViewsValues.Text = GetString("Administration-System.PageViewsValues");

        lblPages.Text = GetString("Administration-System.Pages");
        lblSystemPages.Text = GetString("Administration-System.SystemPages");
        lblNonPages.Text = GetString("Administration-System.NonPages");
        lblGetFilePages.Text = GetString("Administration-System.GetFilePages");
        lblPagesNotFound.Text = GetString("Administration-System.PagesNotFound");
        lblPending.Text = GetString("Administration-System.Pending");

        pnlCache.GroupingText = GetString("Administration-System.CacheStatistics");
        lblCacheExpired.Text = GetString("Administration-System.CacheExpired");
        lblCacheRemoved.Text = GetString("Administration-System.CacheRemoved");
        lblCacheUnderused.Text = GetString("Administration-System.CacheUnderused");
        lblCacheItems.Text = GetString("Administration-System.CacheItems");
        lblCacheDependency.Text = GetString("Administration-System.CacheDependency");

        pnlGC.GroupingText = GetString("Administration-System.GC");

        btnClear.Text = GetString("Administration-System.btnClear");
        btnClearCache.Text = GetString("Administration-System.btnClearCache");
        btnRestart.Text = GetString("Administration-System.btnRestart");
        btnRestartWebfarm.Text = GetString("Administration-System.btnRestartWebfarm");
        btnClearCounters.Text = GetString("Administration-System.btnClearCounters");
        btnRestartServices.Text = GetString("Administration-System.btnRestartServices");

        // Hide button if wefarms are not enabled or disallow restarting webfarms for Windows Azure
        if (!WebSyncHelperClass.WebFarmEnabled || AzureHelper.IsRunningOnAzure)
        {
            this.btnRestartWebfarm.Visible = false;
        }

        // Hide the web farm restart button if this web farm server is not enabled
        if (!RequestHelper.IsPostBack())
        {
            WebFarmServerInfo webFarmServerObj = WebFarmServerInfoProvider.GetWebFarmServerInfo(WebSyncHelperClass.ServerName);
            if ((webFarmServerObj != null) && (!webFarmServerObj.ServerEnabled))
            {
                this.btnRestartWebfarm.Visible = false;
            }
        }

        // Hide restart service button if services folder doesn't exist
        this.btnRestartServices.Visible = SystemHelper.IsFullTrustLevel && WinServiceHelper.ServicesAvailable();

        // Hide the performance counters clear button if the health monitoring is not enabled or feature is not available
        this.btnClearCounters.Visible = SystemHelper.IsFullTrustLevel && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.HealthMonitoring);

        LoadData();

        if (!RequestHelper.IsPostBack())
        {
            switch (QueryHelper.GetString("lastaction", String.Empty).ToLower())
            {
                case "restarted":
                    lblInfo.Text = GetString("Administration-System.RestartSuccess");
                    break;

                case "webfarmrestarted":
                    if (ValidationHelper.ValidateHash("WebfarmRestarted", QueryHelper.GetString("restartedhash", String.Empty)))
                    {
                        lblInfo.Text = GetString("Administration-System.WebframRestarted");
                        // Restart other servers - create webfarm task for application restart
                        WebSyncHelperClass.CreateTask(WebFarmTaskTypeEnum.RestartApplication, "RestartApplication", "", null);
                    }
                    else
                    {
                        lblInfo.Text = GetString("general.actiondenied");
                    }
                    break;

                case "countercleared":
                    lblInfo.Text = GetString("Administration-System.CountersCleared");
                    break;

                case "winservicesrestarted":
                    lblInfo.Text = GetString("Administration-System.WinServicesRestarted");
                    break;
            }
        }

        lblRunTime.Text = GetString("Administration.System.RunTime");
        lblServerTime.Text = GetString("Administration.System.ServerTime");

        // Remove miliseconds from the end of the time string
        string timeSpan = (DateTime.Now - CMSAppBase.ApplicationStart).ToString();
        int index = timeSpan.LastIndexOf('.');
        if (index >= 0)
        {
            timeSpan = timeSpan.Remove(index);
        }

        // Display application run time
        lblRunTimeValue.Text = timeSpan;
        lblServerTimeValue.Text = Convert.ToString(DateTime.Now) + " " + TimeZoneHelper.GetGMTStringOffset(TimeZoneHelper.ServerTimeZone);

        lblIPValue.Text = Request.UserHostAddress;
    }

    /// <summary>
    /// Page PreRender event
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        lblInfo.Visible = !String.IsNullOrEmpty(lblInfo.Text);
        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
    }

    #endregion


    #region "Page Events"

    /// <summary>
    /// Clear unused memory.
    /// </summary>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Collect the memory
        GC.Collect();
        GC.WaitForPendingFinalizers();

        // Log event
        EventLogProvider eventLog = new EventLogProvider();
        eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "CLEARMEM", null, GetString("Administration-System.ClearSuccess"));

        lblInfo.Text = GetString("Administration-System.ClearSuccess");

        LoadData();
    }


    /// <summary>
    /// Restart application.
    /// </summary>
    protected void btnRestart_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        if (RestartApplication())
        {
            if (AzureHelper.IsRunningOnAzure)
            {
                // Log event
                EventLogProvider eventLog = new EventLogProvider();
                eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "ENDAPP", null, GetString("admin.system.restartazure"));
                lblInfo.Text = ResHelper.GetString("admin.system.restartazure");

            }
            else
            {
                // Log event
                EventLogProvider eventLog = new EventLogProvider();
                eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "ENDAPP", null, GetString("Administration-System.RestartSuccess"));

                string url = URLHelper.UpdateParameterInUrl(URLRewriter.CurrentURL, "lastaction", "Restarted");
                URLHelper.Redirect(url);
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = GetString("System.Restart.Failed");
        }
    }


    /// <summary>
    /// Restart webfarm.
    /// </summary>
    protected void btnRestartWebfarm_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Restart current server
        RestartApplication();

        // Log event
        EventLogProvider eventLog = new EventLogProvider();
        eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "RESTARTWEBFARM", null, GetString("Administration-System.WebframRestarted"));

        string url = URLHelper.UpdateParameterInUrl(URLRewriter.CurrentURL, "lastaction", "WebfarmRestarted");
        url = URLHelper.UpdateParameterInUrl(url, "restartedhash", ValidationHelper.GetHashString("WebfarmRestarted"));
        URLHelper.Redirect(url);
    }


    /// <summary>
    /// Clear counters.
    /// </summary>
    protected void btnClearCounters_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Reset values of health monitoring counters
        HealthMonitoringManager.ResetCounters();
        // Clear application counters
        HealthMonitoringLogHelper.ClearApplicationCounters();

        // Log event
        EventLogProvider eventLog = new EventLogProvider();
        eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "CLEARCOUNTERS", null, GetString("Administration-System.CountersCleared"));

        string url = URLHelper.UpdateParameterInUrl(URLRewriter.CurrentURL, "lastaction", "CounterCleared");
        URLHelper.Redirect(url);
    }


    /// <summary>
    /// Restart windows services.
    /// </summary>
    protected void btnRestartServices_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Resets values of counters
        try
        {
            WinServiceHelper.RestartService(null);

            // Log event
            EventLogProvider eventLog = new EventLogProvider();
            eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "RESTARTWINSERVICES", null, GetString("Administration-System.WinServicesRestarted"));
        }
        catch (Exception ex)
        {
            EventLogProvider.LogException("WinServiceHelper", "RestartService", ex);
        }

        string url = URLHelper.UpdateParameterInUrl(URLRewriter.CurrentURL, "lastaction", "WinServicesRestarted");
        URLHelper.Redirect(url);
    }


    protected void btnClearCache_Click(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Clear the cache
        CacheHelper.ClearCache(null, true);
        Functions.ClearHashtables();

        // Drop the routes
        CMSMvcHandler.DropAllRoutes();

        // Collect the memory
        GC.Collect();
        GC.WaitForPendingFinalizers();

        // Log event
        EventLogProvider eventLog = new EventLogProvider();
        eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "System", "CLEARCACHE", null, GetString("Administration-System.ClearCacheSuccess"));

        lblInfo.Text = GetString("Administration-System.ClearCacheSuccess");

        LoadData();
    }

    #endregion


    #region "Other Methods"

    /// <summary>
    /// Loads the data.
    /// </summary>
    protected void LoadData()
    {
        lblValueAlocatedMemory.Text = DataHelper.GetSizeString(GC.GetTotalMemory(false));

        lblValueVirtualMemory.Text = "N/A";
        lblValuePhysicalMemory.Text = "N/A";
        lblValuePeakMemory.Text = "N/A";

        // Process memory
        try
        {
            lblValueVirtualMemory.Text = DataHelper.GetSizeString(SystemHelper.GetVirtualMemorySize());
            lblValuePhysicalMemory.Text = DataHelper.GetSizeString(SystemHelper.GetWorkingSetSize());
            lblValuePeakMemory.Text = DataHelper.GetSizeString(SystemHelper.GetPeakWorkingSetSize());
        }
        catch
        {
        }

        this.lblValuePages.Text = GetViewValues(RequestHelper.TotalPageRequests.GetValue(null), 0, mRPSPageRequests);
        this.lblValuePagesNotFound.Text = GetViewValues(RequestHelper.TotalPageNotFoundRequests.GetValue(null), 0, mRPSPageNotFoundRequests);
        this.lblValueSystemPages.Text = GetViewValues(RequestHelper.TotalSystemPageRequests.GetValue(null), 0, mRPSSystemPageRequests);
        this.lblValueNonPages.Text = GetViewValues(RequestHelper.TotalNonPageRequests.GetValue(null), 0, mRPSNonPageRequests);
        this.lblValueGetFilePages.Text = GetViewValues(RequestHelper.TotalGetFileRequests.GetValue(null), 0, mRPSGetFileRequests);

        long pending = RequestHelper.PendingRequests.GetValue(null);
        if (pending > 1)
        {
            // Current request does not count as pending at the time of display
            pending--;
        }
        if (pending < 0)
        {
            pending = 0;
        }

        this.lblValuePending.Text = pending.ToString();

        this.lblCacheItemsValue.Text = Cache.Count.ToString();
        this.lblCacheExpiredValue.Text = CacheHelper.Expired.GetValue(null).ToString();
        this.lblCacheRemovedValue.Text = CacheHelper.Removed.GetValue(null).ToString();
        this.lblCacheUnderusedValue.Text = CacheHelper.Underused.GetValue(null).ToString();
        this.lblCacheDependencyValue.Text = CacheHelper.DependencyChanged.GetValue(null).ToString();

        // GC collections
        try
        {
            this.plcGC.Controls.Clear();

            int generations = GC.MaxGeneration;
            for (int i = 0; i <= generations; i++)
            {
                int count = GC.CollectionCount(i);
                string genString = "<tr><td style=\"white-space: nowrap; width: 200px;\">" + GetString("GC.Generation") + " " + i.ToString() + ":</td><td>" + count.ToString() + "</td></tr>";

                this.plcGC.Controls.Add(new LiteralControl(genString));
            }
        }
        catch
        {
        }

        lblDBNameValue.Text = TableManager.DatabaseName;
        lblServerVersionValue.Text = TableManager.DatabaseServerVersion;

        // DB information       
        if (!RequestHelper.IsPostBack())
        {
            lblDBSizeValue.Text = TableManager.DatabaseSize;
            lblServerNameValue.Text = TableManager.DatabaseServerName;
        }

    }


    /// <summary>
    /// Gets the values string for the page views.
    /// </summary>
    private string GetViewValues(long total, long async, double rps)
    {
        return total.ToString() /*+ " / " + async.ToString() + " / "*/ + ((rps >= 1) ? " (" + Math.Floor(rps).ToString() + " RPS)" : "");
    }


    /// <summary>
    /// Check user settings if site manager is not explicitly denied for current user. 
    /// If it is user is redirected to access denied page.
    /// </summary>        
    private void CheckAccessToSiteManager()
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (currentUser != null)
        {
            if (currentUser.UserSiteManagerDisabled)
            {
                RedirectToAccessDenied(ResHelper.GetString("accessdeniedtopage.sitemanagerdenied"));
            }
        }
    }


    /// <summary>
    /// Checks if the user is global administrator (can access the administration (development) page.
    /// </summary>
    private void CheckGlobalAdministrator()
    {
        if (!RequestHelper.IsAsyncPostback())
        {
            CurrentUserInfo currentUser = CMSContext.CurrentUser;
            if (currentUser == null)
            {
                URLHelper.Redirect(mAccessDeniedPage);
            }
            else if (!currentUser.UserSiteManagerAdmin)
            {
                URLHelper.Redirect(mAccessDeniedPage);
            }
        }
    }


    /// <summary>
    /// Tries to restart application and returns if restart was succesfull.
    /// </summary>    
    private bool RestartApplication()
    {
        // Restart azure instance
        if (AzureHelper.IsRunningOnAzure)
        {
            AzureHelper.RestartAzureInstance();
        }
        // Restart classic application
        else
        {
            try
            {
                // Try to restart applicatin by unload app domain
                HttpRuntime.UnloadAppDomain();
            }
            catch
            {
                try
                {
                    // Try to restart application by changing web.config file
                    File.SetLastWriteTimeUtc(Request.PhysicalApplicationPath + "\\web.config", DateTime.UtcNow);
                }
                catch
                {
                    return false;
                }
            }
        }
        return true;
    }

    #endregion
}
