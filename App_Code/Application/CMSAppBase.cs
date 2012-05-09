using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web.Security;
using System.Diagnostics;
using System.Data;
using System.Reflection;

using CMS.SettingsProvider;
using CMS.EventLog;
using CMS.URLRewritingEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.SiteProvider;
using CMS.CMSOutputFilter;
using CMS.WebFarmSyncHelper;
using CMS.DataEngine;
using CMS.Synchronization;
using CMS.OutputFilter;
using CMS.PortalEngine;
using CMS.WebFarmSync;
using CMS.PortalControls;
using CMS.IO;
using CMS.Scheduler;
using CMS.RESTService;
using CMS.LicenseProvider;
using CMS.EmailEngine;
using CMS.UIControls;
using CMS.Compatibility;

public class CMSAppBase
{
    #region "Variables"

    #region "System data (do not modify)"

    /// <summary>
    /// Application version, do not change.
    /// </summary>
    /// const string APP_VERSION = "6.0";

    #endregion

    private static DateTime mApplicationStart = DateTime.Now;
    private static DateTime mApplicationStartFinished = DateTime.MinValue;

    private static bool firstEndRequestAfterStart = true;

    private static bool? mApplicationInitialized = null;

    private static string mConnectionErrorMessage = null;

    private static object mLock = new object();

    private static bool sessionTimeoutInicialized = false;

    /// <summary>
    /// Windows identity.
    /// </summary>
    private static WindowsIdentity mWindowsIdentity = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Date and time of the application start.
    /// </summary>
    public static DateTime ApplicationStart
    {
        get
        {
            return mApplicationStart;
        }
    }


    /// <summary>
    /// Date and time when the application start (initialization) finished its execution.
    /// </summary>
    public static DateTime ApplicationStartFinished
    {
        get
        {
            return mApplicationStartFinished;
        }
    }


    /// <summary>
    /// Returns true if the application was already initialized.
    /// </summary>
    public static bool ApplicationInitialized
    {
        get
        {
            if (mApplicationInitialized == null)
            {
                return false;
            }

            return mApplicationInitialized.Value;
        }
    }


    /// <summary>
    /// Connection error message.
    /// </summary>
    public static string ConnectionErrorMessage
    {
        get
        {
            return mConnectionErrorMessage;
        }
    }


    /// <summary>
    /// Returns true if the connection is available.
    /// </summary>
    public static bool ConnectionAvailable
    {
        get
        {
            return SqlHelperClass.IsConnectionStringInitialized && ApplicationInitialized;
        }
    }

    #endregion


    #region "Application events"

    /// <summary>
    /// Application start event handler.
    /// </summary>
    public static void CMSApplicationStart()
    {
#if DEBUG
        // Set debug mode
        SystemHelper.IsWebProjectDebug = true;
#endif
    }


    /// <summary>
    /// Application error event handler.
    /// </summary>
    public static void CMSApplicationError(object sender, EventArgs e)
    {
        // Handle the event
        var h = CMSApplicationEvents.Error.StartEvent(e);
        if (h.Continue)
        {
            // Log the error
            LogLastApplicationError();
        }

        // Finalize the event
        h.FinishEvent();
    }


    /// <summary>
    /// Application end event handler.
    /// </summary>
    public static void CMSApplicationEnd(object sender, EventArgs e)
    {
        // Handle the event
        var h = CMSApplicationEvents.End.StartEvent(e);
        if (h.Continue)
        {
            try
            {
                // Log the application end
                LogApplicationEnd();
            }
            catch
            {
            }

            // Disable logging of events
            EventLogProvider.LoggingEnabled = false;
        }

        // Finalize the event
        h.FinishEvent();
    }

    #endregion


    #region "Session events"

    /// <summary>
    /// Session start event handler.
    /// </summary>
    public static void CMSSessionStart(object sender, EventArgs e)
    {
        if (ConnectionAvailable)
        {
            RequestHelper.LogRequestOperation("Session_Start", null, 0);
            DebugHelper.SetContext("Session_Start");

            // Handle the event
            var h = CMSSessionEvents.Start.StartEvent(e);
            if (h.Continue)
            {
                string siteName = CMSContext.CurrentSiteName;

                // If path was rewritten log session
                URLRewritingResultEnum status = URLRewriter.CurrentStatus;
                if ((status == URLRewritingResultEnum.PathRewritten) ||
                    (status == URLRewritingResultEnum.MVCPage))
                {
                    // Add sesssion to the session manager
                    if (SessionManager.OnlineUsersEnabled && !URLHelper.IsExcludedSystem(URLHelper.CurrentRelativePath))
                    {
                        SessionManager.UpdateCurrentSession(null);
                    }
                }

                if (siteName != "")
                {
                    // Process the URL referrer data
                    ProcessReferrer(siteName);

                    // If authentication mode is Windows, set user UI culture
                    if (RequestHelper.IsWindowsAuthentication() && UserInfoProvider.IsAuthenticated())
                    {
                        UserInfo currentUser = CMSContext.CurrentUser;
                        if (!currentUser.IsPublic())
                        {
                            UserInfoProvider.SetPreferredCultures(currentUser);
                        }
                    }
                }
            }

            // Finalize the event
            h.FinishEvent();

            DebugHelper.ReleaseContext();
        }

        // Count the session
        RequestHelper.TotalSessions++;
    }


    /// <summary>
    /// Session end event handler.
    /// </summary>
    public static void CMSSessionEnd(object sender, EventArgs e)
    {
        if (ConnectionAvailable)
        {
            // Handle the event
            var h = CMSSessionEvents.End.StartEvent(e);
            if (h.Continue)
            {
                // Removes expired sessions
                if (SessionManager.OnlineUsersEnabled)
                {
                    SessionManager.RemoveExpiredSessions();
                }
            }

            // Finalize the event
            h.FinishEvent();
        }
    }

    #endregion


    #region "Request events"

    /// <summary>
    /// Begin request event handler
    /// </summary>
    public static void CMSBeginRequest(object sender, EventArgs e)
    {
        // Do the actions before the request begins
        BeforeBeginRequest(sender, e);

        // Handle the event
        var h = CMSRequestEvents.Begin.StartEvent(e);
        if (h.Continue)
        {
            // Check if Database installation needed
            if (FileRedirect() || InstallerFunctions.InstallRedirect(false))
            {
                return;
            }

            if (ConnectionAvailable)
            {
                // Create request scope
                if (ConnectionHelper.UseContextConnection)
                {
                    CMSConnectionScope.EnsureRequestScope(false, ConnectionHelper.KeepContextConnectionOpen);
                }

                // Enable debugging
                SetInitialDebug();

                // Ensure routes for current site
                CMSMvcHandler.EnsureRoutes(CMSContext.CurrentSiteName);
            }
        }

        // Finalize the event
        h.FinishEvent();

        // Check WebDAV PROPFIND request
        if (RequestHelper.IsWebDAVPropfindRequest())
        {
            // End request
            RequestHelper.EndResponse();
        }
    }


    /// <summary>
    /// Fired before the begin request executes
    /// </summary>
    private static void BeforeBeginRequest(object sender, EventArgs e)
    {
        // Azure begin request init
        if (mApplicationInitialized != true)
        {
            AzureInit.Current.BeginRequestInit();
        }

        RequestHelper.PendingRequests.Increment(null);

        // Check the script manager request
        RequestHelper.CheckScriptManagerRequest();

        // Check the application validity
        LicenseHelper.CheckValidity();

        // Application start events
        FirstRequestInitialization(sender, e);

        // Check the number of Azure instances
        CheckAzureInstances();

        // Start thread which is checking new web farm tasks if database web farm updater is used.
        DbWebFarmUpdater.EnsureThread();
    }


    /// <summary>
    /// Checks the number of Azure instances
    /// </summary>
    private static void CheckAzureInstances()
    {
        // Actual number of servers is bigger than allowed count by license - don't create web farm server - log event and redirect to error page
        if (AzureHelper.IsRunningOnAzure && ConnectionAvailable)
        {
            var license = LicenseHelper.CurrentLicenseInfo;
            if ((license != null) && (license.LicenseServers > 0) && (license.LicenseServers < AzureHelper.NumberOfInstances))
            {
                // Log to the event log
                EventLogProvider log = new EventLogProvider();
                log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Application_Start", "WEBFARMSERVER", null, "The current license servers limit has exceeded.");

                // Redirect to error
                HttpContext.Current.Server.Transfer("~/CMSMessages/error.aspx?title=" + ResHelper.GetString("webfarm.serverslimitexceeded") + "&text=" + ResHelper.GetString("webfarm.serverslimitexceeded") + "&backlink=0");
            }
        }
    }


    /// <summary>
    /// Request authentication handler.
    /// </summary>
    public static void CMSAuthenticateRequest(object sender, EventArgs e)
    {
        if (ConnectionAvailable)
        {
            // Handle the event
            var h = CMSRequestEvents.Authenticate.StartEvent(e);
            if (h.Continue)
            {
                // Allow action context user initialization
                CMSActionContext.CurrentAllowInitUser = true;

                // Check for single sign-in authentication token
                CheckAuthenticationGUID();
            }

            // Finalize the event
            h.FinishEvent();
        }
    }


    /// <summary>
    /// Request authorization handler.
    /// </summary>
    public static void CMSAuthorizeRequest(object sender, EventArgs e)
    {
        RequestHelper.LogRequestOperation("AuthorizeRequest", null, 0);
        DebugHelper.SetContext("AuthorizeRequest");

        if (ConnectionAvailable)
        {
            // Handle the event
            var h = CMSRequestEvents.Authorize.StartEvent(e);
            if (h.Continue)
            {
                string relativePath = URLHelper.CurrentRelativePath;

                // Check the excluded status
                ExcludedSystemEnum excludedEnum = URLHelper.IsExcludedSystemEnum(relativePath);

                RequestHelper.CurrentExcludedStatus = excludedEnum;

                ViewModeOnDemand viewMode = new ViewModeOnDemand();
                SiteNameOnDemand siteName = new SiteNameOnDemand();

                // Try to send the output from the cache without URL rewriting
                if (URLRewriter.SendOutputFromCache(relativePath, excludedEnum, viewMode, siteName))
                {
                    if (OutputFilter.OutputFilterEndRequestRequired)
                    {
                        HttpContext context = HttpContext.Current;
                        string newQuery = null;

                        if (URLRewriter.FixRewriteRedirect)
                        {
                            newQuery = "rawUrl=" + HttpUtility.UrlEncode(context.Request.RawUrl);
                        }

                        context.RewritePath("~/CMSPages/blank.aspx", null, newQuery);
                    }
                    return;
                }
            }

            // Finalize the event
            h.FinishEvent();
        }

        DebugHelper.ReleaseContext();
    }


    /// <summary>
    /// Handler mapping handler.
    /// </summary>
    public static void CMSMapRequestHandler(object sender, EventArgs e)
    {
        URLRewritingResultEnum status = URLRewriter.CurrentStatus;
        if (ConnectionAvailable && (status != URLRewritingResultEnum.SentFromCache))
        {
            RequestHelper.LogRequestOperation("MapRequestHandler", null, 0);
            DebugHelper.SetContext("MapRequestHandler");

            // Handle the event
            var h = CMSRequestEvents.MapRequestHandler.StartEvent(e);
            if (h.Continue)
            {
                // Get request parameters
                string relativePath = URLHelper.CurrentRelativePath;

                ExcludedSystemEnum excludedEnum = RequestHelper.CurrentExcludedStatus;
                if (excludedEnum == ExcludedSystemEnum.Unknown)
                {
                    excludedEnum = URLHelper.IsExcludedSystemEnum(relativePath);
                }

                ViewModeOnDemand viewMode = new ViewModeOnDemand();
                SiteNameOnDemand siteName = new SiteNameOnDemand();

                // Handle the virtual context
                HandleVirtualContext(ref relativePath, ref excludedEnum);

                // Set flag to output filter for cms dialogs
                if (excludedEnum == ExcludedSystemEnum.CMSDialog)
                {
                    OutputFilter.UseFormActionWithCurrentURL = true;
                }

                // Perform the URL rewriting
                RewriteUrl(status, relativePath, excludedEnum, viewMode, siteName);
            }

            // Finalize the event
            h.FinishEvent();

            DebugHelper.ReleaseContext();
        }
    }


    /// <summary>
    /// Handles the virtual context for the request
    /// </summary>
    /// <param name="relativePath">Relative path</param>
    /// <param name="excludedEnum">Excluded page enum</param>
    private static void HandleVirtualContext(ref string relativePath, ref ExcludedSystemEnum excludedEnum)
    {
        // Handle the virtual context
        bool isVirtual = URLRewriter.HandleVirtualContext(ref relativePath);
        if (isVirtual)
        {
            // Check the excluded status
            excludedEnum = URLHelper.IsExcludedSystemEnum(relativePath);

            if (excludedEnum == ExcludedSystemEnum.GetFilePage)
            {
                VirtualContext.CurrentURLPrefix = null;
            }
        }
    }


    /// <summary>
    /// Acquire request state event handler.
    /// </summary>
    public static void CMSAcquireRequestState(object sender, EventArgs e)
    {
        RequestHelper.LogRequestOperation("AcquireRequestState", null, 0);
        DebugHelper.SetContext("AcquireRequestState");

        // Handle the event
        var h = CMSRequestEvents.AcquireRequestState.StartEvent(e);
        if (h.Continue)
        {
            // Try to redirect as planned first
            if (URLRewriter.FixRewriteRedirect)
            {
                URLRewriter.PerformPlannedRedirect();
            }

            // Keep session timeout within static variable
            if ((!sessionTimeoutInicialized) && (HttpContext.Current != null) && (HttpContext.Current.Session != null))
            {
                SessionHelper.SessionTimeout = HttpContext.Current.Session.Timeout;
                sessionTimeoutInicialized = true;
            }

            if (ConnectionAvailable)
            {
                // Check the page security
                CheckSecurity();
            }

            // Keep current status
            URLRewritingResultEnum status = URLRewriter.CurrentStatus;

            // Log analytics or activity
            switch (status)
            {
                case URLRewritingResultEnum.PathRewritten:
                case URLRewritingResultEnum.MVCPage:
                case URLRewritingResultEnum.PathRewrittenDisableOutputFilter:
                case URLRewritingResultEnum.SentFromCache:

                    if (ConnectionAvailable && !RequestHelper.IsPostBack() && !QueryHelper.GetBoolean(URLHelper.SYSTEM_QUERY_PARAMETER, false) && (CMSContext.ViewMode == ViewModeEnum.LiveSite))
                    {
                        string siteName = CMSContext.CurrentSiteName;
                        UserInfo currentUser = CMSContext.CurrentUser;
                        PageInfo currentPage = CMSContext.CurrentPageInfo;

                        if (currentPage != null)
                        {
                            if (AnalyticsHelper.IsLoggingEnabled(siteName, currentPage.NodeAliasPath))
                            {
                                //Log visitor
                                AnalyticsMethods.LogVisitor(siteName);
                                // Log referring, search, landing and exit pages
                                AnalyticsMethods.LogAnalytics(SessionHelper.GetSessionID(), currentPage, siteName);
                            }

                            // Log page visit activity
                            if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.ActivitiesEnabledForThisUser(currentUser))
                            {
                                int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();

                                CMSContext.ContactID = contactId;
                                if (contactId > 0)
                                {
                                    // Log landing page
                                    ModuleCommands.OnlineMarketingLogLandingPage(contactId);

                                    // Log external search
                                    ModuleCommands.OnlineMarketingLogExternalSearch(contactId);

                                    // Update contact information
                                    UpdateContactInformation(siteName);
                                }
                            }
                        }
                    }
                    break;
            }

            // Check whether request should be ended for full page cached page
            if (OutputFilter.OutputFilterEndRequestRequired)
            {
                OutputHelper.EndRequest();
            }
        }

        // Finalize the event
        h.FinishEvent();

        DebugHelper.ReleaseContext();
    }


    /// <summary>
    /// End request event handler.
    /// </summary>
    public static void CMSEndRequest(Object sender, EventArgs e)
    {
        if (CMSFunctions.AnyDebugEnabled)
        {
            RequestHelper.LogRequestOperation("EndRequest", HttpContext.Current.Response.Status.ToString(), 0);
            DebugHelper.SetContext("EndRequest");
        }

        // Handle the event
        var h = CMSRequestEvents.End.StartEvent(e);
        if (h.Continue)
        {
            var status = URLRewriter.CurrentStatus;

            // Check whether the output was sent from cache
            bool sentFromCache = (status == URLRewritingResultEnum.SentFromCache);
            if (!sentFromCache)
            {
                HttpApplication app = (HttpApplication)sender;

                // Check status code 306 (code which is returned only when authentication failed in REST Service), change it to classical 401 - Unauthorized.
                // That's because 401 is automatically handled by ASP.NET and redirected to logon page, this way we can achieve to return 401 without ASP.NET to interfere.
                if (app.Response.StatusCode == 306)
                {
                    // Set correct authetication header
                    switch (RESTSecurityInvoker.GetAuthenticationType(CMSContext.CurrentSiteName))
                    {
                        case "basic":
                            app.Response.Headers["WWW-Authenticate"] = string.Format("Basic realm=\"{0}\"", "CMS REST Service");
                            break;

                        default:
                            app.Response.Headers["WWW-Authenticate"] = string.Format("Digest realm=\"{0}\"", "CMS REST Service");
                            break;
                    }
                    app.Response.StatusCode = 401;

                }
            }

            // Register the debug logs
            if (CMSFunctions.AnyDebugEnabled)
            {
                RequestHelper.LogRequestValues(true, true, true);
                DebugHelper.RegisterLogs();
            }

            // If connection is available
            if (ConnectionAvailable)
            {
                // Restore the response cookies if fullpage caching is set
                if (sentFromCache && (URLRewriter.CurrentOutputCache > 0) && (URLRewriter.CurrentStatus != URLRewritingResultEnum.GetFile))
                {
                    CookieHelper.RestoreResponseCookies();
                }

                // Set cookies as read-only for further usage in the request
                CookieHelper.ReadOnly = true;

                // Additional tasks within first request end
                if (firstEndRequestAfterStart)
                {
                    firstEndRequestAfterStart = false;

                    // Re-initialize tasks which were stopped by application end
                    bool[] debugs = DebugHelper.DisableSchedulerDebug();
                    SchedulingExecutor.ReInitCorruptedTasks();
                    DebugHelper.RestoreDebugSettings(debugs);

                    ModuleCommands.NewsletterClearEmailsSendingStatus();
                    EmailInfoProvider.ResetSendingStatus();

                    // Process synchronization tasks
                    WebSyncHelper.ProcessMyTasks();

                    // Run smart search 
                    SearchTaskInfoProvider.ProcessTasks();
                }
                else if (!RequestHelper.IsAsyncPostback())
                {
                    // Attempt to run the scheduler
                    RunScheduler(status);

                    // Run performance timer
                    EnsurePerformanceCounterTimer();
                }


                // Log page view
                if (RequestHelper.LogPageView && (!QueryHelper.GetBoolean(URLHelper.SYSTEM_QUERY_PARAMETER, false)) && (status != URLRewritingResultEnum.RESTService))
                {
                    string siteName = CMSContext.CurrentSiteName;
                    UserInfo currentUser = CMSContext.CurrentUser;
                    PageInfo currentPage = CMSContext.CurrentPageInfo;

                    if (currentPage != null)
                    {
                        if (!RequestHelper.IsPostBack() && AnalyticsHelper.IsLoggingEnabled(siteName, currentPage.NodeAliasPath))
                        {
                            // Log page view
                            if (AnalyticsHelper.TrackPageViewsEnabled(siteName))
                            {
                                HitLogProvider.LogHit(HitLogProvider.PAGE_VIEWS, siteName, currentUser.PreferredCultureCode, currentPage.NodeAliasPath, currentPage.NodeId);
                            }

                            // Log aggregated view
                            if (QueryHelper.Contains("feed") && AnalyticsHelper.TrackAggregatedViewsEnabled(siteName))
                            {
                                HitLogProvider.LogHit(HitLogProvider.AGGREGATED_VIEWS, siteName, currentUser.PreferredCultureCode, currentPage.NodeAliasPath, currentPage.NodeId);
                            }
                        }

                        if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.ActivitiesEnabledForThisUser(currentUser))
                        {
                            if (ActivitySettingsHelper.PageVisitsEnabled(siteName))
                            {
                                int contactId = CMSContext.ContactID;
                                if (contactId > 0)
                                {
                                    ModuleCommands.OnlineMarketingLogPageVisit(contactId);
                                }
                            }
                        }
                    }
                }


                // Run web farm updater if it is required within current request
                if (!sentFromCache)
                {
                    WebSyncHelper.SynchronizeWebFarm();
                }

                // Run any potential queued workers
                CMSWorkerQueue queue = CMSWorkerQueue.Instance;
                if (queue != null)
                {
                    queue.Parameter = IntegrationHelper.TouchedConnectorNames;
                    queue.OnFinished += CMSWorkerQueue_OnFinished;

                    queue.RunAll();
                }

                // Dispose the request scope
                if (CMSConnectionScope.SomeConnectionUsed)
                {
                    CMSConnectionScope.DisposeRequestScope();
                }
            }
        }

        // Finalize the event
        h.FinishEvent();

        // Write SQL query log
        if (CMSFunctions.AnyDebugEnabled)
        {
            RequestHelper.LogRequestOperation("FinishRequest", null, 0);
            DebugHelper.SetContext("FinishRequest");

            DebugHelper.ReleaseContext();
            SqlHelperClass.WriteRequestLog();
            SecurityHelper.WriteRequestLog();
            RequestHelper.WriteRequestLog();
            MacroResolver.WriteRequestLog();
            AnalyticsHelper.WriteRequestLog();
        }

        RequestHelper.PendingRequests.Decrement(null);
    }

    #endregion


    #region "Overriden methods"

    /// <summary>
    /// Custom cache parameters processing.
    /// </summary>
    public static string CMSGetVaryByCustomString(HttpContext context, string custom)
    {
        if (context == null)
        {
            return "";
        }

        HttpResponse response = context.Response;

        // Do not cache on postback
        if (URLHelper.IsPostback())
        {
            response.Cache.SetNoStore();
            return Guid.NewGuid().ToString();
        }

        PageInfo currentPage = CMSContext.CurrentPageInfo;
        string result = null;

        // Cache control
        if ((currentPage != null) && !custom.StartsWith("control;"))
        {
            // Check page caching minutes
            int cacheMinutes = currentPage.NodeCacheMinutes;
            if (cacheMinutes <= 0)
            {
                // Do not cache
                response.Cache.SetNoStore();
                return Guid.NewGuid().ToString();
            }
        }

        SiteNameOnDemand siteName = new SiteNameOnDemand();
        ViewModeOnDemand viewMode = new ViewModeOnDemand();

        // Parse the custom parameters
        string contextString = CMSContext.GetContextCacheString(custom, viewMode, siteName);
        if (contextString == null)
        {
            // Do not cache
            response.Cache.SetNoStore();
            return Guid.NewGuid().ToString();
        }
        else
        {
            result = "cached" + contextString;
        }

        return result.ToLower();
    }

    #endregion


    #region "Methods"


    /// <summary>
    /// Raised when worker queue finishes.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected static void CMSWorkerQueue_OnFinished(object sender, EventArgs e)
    {
        CMSWorkerQueue queue = sender as CMSWorkerQueue;
        if (queue != null)
        {
            // Process internal integration tasks
            IntegrationHelper.ProcessInternalTasksAsync(queue.Parameter as List<string>);
        }
    }


    /// <summary>
    /// Rewrites the URL and performs all operations required after URL rewriting.
    /// </summary>
    /// <param name="status">Current rewriting status</param>
    /// <param name="relativePath">Relative path</param>
    /// <param name="excludedEnum">Excluded page status</param>
    /// <param name="viewMode">View mode</param>
    /// <param name="siteName">Site name</param>
    private static void RewriteUrl(URLRewritingResultEnum status, string relativePath, ExcludedSystemEnum excludedEnum, ViewModeOnDemand viewMode, SiteNameOnDemand siteName)
    {
        // Do the rewriting if status not yet determined
        if (status == URLRewritingResultEnum.Unknown)
        {
            RequestHelper.LogRequestOperation("RewriteURL", relativePath, 0);

            // Rewrite URL
            status = URLRewriter.RewriteUrl(relativePath, excludedEnum, siteName, viewMode);
        }

        // Process actions after rewriting
        URLRewriter.ProcessRewritingResult(status, excludedEnum, siteName, viewMode, relativePath);
    }


    /// <summary>
    /// Performs the application initialization on the first request.
    /// </summary>
    private static void FirstRequestInitialization(object sender, EventArgs e)
    {
        // Initialized properly
        if (mApplicationInitialized == true)
        {
            return;
        }

        // Not initialized, must install
        if ((mApplicationInitialized == false) && InstallerFunctions.InstallRedirect(true))
        {
            return;
        }

        // Do not init application on request to just physical file
        string relativePath = URLHelper.CurrentRelativePath;
        ExcludedSystemEnum excludedEnum = URLHelper.IsExcludedSystemEnum(relativePath);
        if (excludedEnum == ExcludedSystemEnum.PhysicalFile)
        {
            return;
        }

        // Initialize application in a locked context
        lock (mLock)
        {
            if (ApplicationInitialized)
            {
                return;
            }

            // Remember date and time of the application start
            mApplicationStart = DateTime.Now;

            // Init run from web applicaiton - DON'T MOVE LATER
            SystemHelper.IsWebSite = true;

            mWindowsIdentity = WindowsIdentity.GetCurrent();

            ViewModeOnDemand viewMode = new ViewModeOnDemand();

            // Log application start
            if (CMSFunctions.AnyDebugEnabled)
            {
                RequestSettings settings = RequestSettings.Current;
                bool liveSite = (viewMode.Value == ViewModeEnum.LiveSite);

                settings.DebugRequest = RequestHelper.DebugRequests && liveSite;
                RequestHelper.LogRequestOperation("BeforeApplicationStart", null, 0);

                settings.DebugSQLQueries = SqlHelperClass.DebugQueries && liveSite;
                settings.DebugFiles = File.DebugFiles && liveSite;
                settings.DebugCache = CacheHelper.DebugCache && liveSite;
                settings.DebugSecurity = SecurityHelper.DebugSecurity && liveSite;
                settings.DebugOutput = OutputHelper.DebugOutput && liveSite;
                settings.DebugMacros = MacroResolver.DebugMacros && liveSite;
                settings.DebugWebFarm = WebSyncHelperClass.DebugWebFarm && liveSite;
                settings.DebugAnalytics = AnalyticsHelper.DebugAnalytics && liveSite;

                DebugHelper.SetContext("App_Start");
            }

            // Initialize MacroResolver with child of GlobalResolver
            MacroResolver.OnGetInstance += new MacroResolver.GetInstanceEventHandler(MacroResolver_OnGetInstance);

            // Handle the event
            var h = CMSApplicationEvents.Start.StartEvent(e);
            if (h.Continue)
            {
                //ConnectionHelper.UseContextConnection = true;
                //CacheHelper.CacheItemPriority = System.Web.Caching.CacheItemPriority.NotRemovable;

                if (SqlHelperClass.IsConnectionStringInitialized)
                {
                    using (CMSConnectionScope scope = new CMSConnectionScope())
                    {
                        // Use single open connection for the application start
                        GeneralConnection conn = (GeneralConnection)scope.Connection;
                        bool closeConnection = false;
                        try
                        {
                            // Open the connection
                            conn.Open();
                            closeConnection = true;

                            // Check for the table existence
                            if (!TableManager.TableExists("CMS_SettingsKey"))
                            {
                                mApplicationInitialized = false;

                                if (InstallerFunctions.InstallRedirect(true))
                                {
                                    return;
                                }
                            }

                            // Check the version
                            string version = SettingsKeyProvider.GetStringValue("CMSDBVersion");
                            if (version != CMSContext.SYSTEM_VERSION)
                            {
                                // Report error about not being able to connect
                                mConnectionErrorMessage = "The database version '" + version + "' does not match the project version '" + CMSContext.SYSTEM_VERSION + "', please check your connection string.";
                                HttpContext.Current.Server.Transfer("~/CMSMessages/error.aspx");
                            }
                            else
                            {
                                // Initialize the environment
                                CMSFunctions.Init();

                                // Update the system !! IMPORTANT - must be first
                                UpgradeProcedure.Update(conn);
                                try
                                {
                                    // Write "Application start" event to the event log
                                    EventLogProvider ev = new EventLogProvider();

                                    ev.DeleteOlderLogs = false;
                                    ev.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Application_Start", "STARTAPP", 0, null, 0, null, null, null, 0, HTTPHelper.GetAbsoluteUri());
                                }
                                catch
                                {
                                    // can't write to log, do not process any code
                                }
                                UserInfoProvider.OnFormattedUserName += new UserInfoProvider.FormattedUserNameEventHandler(Functions.GetFormattedUserName);

                                // Delete memory synchronization tasks
                                WebSyncHelper.DeleteMemorySynchronizationTasks();

                                // Create web farm server if running on Azure
                                if (AzureHelper.IsRunningOnAzure)
                                {
                                    // Set webfarm server name
                                    WebSyncHelperClass.ServerName = ValidationHelper.GetCodeName(AzureHelper.CurrentInstanceID);

                                    if (WebFarmServerInfoProvider.GetWebFarmServerInfo(WebSyncHelperClass.ServerName) == null)
                                    {
                                        // Create webfarm server
                                        WebFarmServerInfo wfsi = new WebFarmServerInfo();
                                        wfsi.ServerName = WebSyncHelperClass.ServerName;
                                        wfsi.ServerEnabled = true;
                                        wfsi.ServerDisplayName = AzureHelper.CurrentInstanceID;
                                        wfsi.ServerURL = AzureHelper.CurrentInternalEndpoint;

                                        WebFarmServerInfoProvider.SetWebFarmServerInfo(wfsi);
                                    }
                                }

                                // Wait until initialization is complete
                                CMSFunctions.WaitForInitialization();
                            }
                        }
                        catch (Exception ex)
                        {
                            if (closeConnection)
                            {
                                // Server connected succesfully but something else went wrong
                                throw ex;
                            }
                            else
                            {
                                // Report error about not being able to connect
                                mConnectionErrorMessage = ex.Message;

                                HttpContext.Current.Server.Transfer("~/CMSMessages/error.aspx");
                            }
                        }
                        finally
                        {
                            if (closeConnection)
                            {
                                // Close the connection
                                conn.Close();
                            }
                        }
                    }
                }
                else
                {
                    // Register virtual path provider
                    if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSUseVirtualPathProvider"], true))
                    {
                        CMS.VirtualPathHelper.VirtualPathHelper.RegisterVirtualPathProvider();
                    }
                }

                // Register the CMS view engine
                CMSViewEngine.RegisterViewEngine();
            }

            // Finalize the event
            h.FinishEvent();

            DebugHelper.ReleaseContext();

            // Log when the overall application start finished its execution
            mApplicationStartFinished = DateTime.Now;
            mApplicationInitialized = true;

            RequestHelper.LogRequestOperation("AfterApplicationStart", null, 0);
        }
    }


    /// <summary>
    /// Returns child of global resolver.
    /// </summary>
    protected static MacroResolver MacroResolver_OnGetInstance()
    {
        return CMSContext.CurrentResolver.CreateContextChild();
    }


    /// <summary>
    /// Checks the request security and path.
    /// </summary>
    private static void CheckSecurity()
    {
        // Process only for content pages
        URLRewritingResultEnum status = URLRewriter.CurrentStatus;

        #region "Check Banned IPs"

        switch (status)
        {
            case URLRewritingResultEnum.PathRewritten:
            case URLRewritingResultEnum.MVCPage:
            case URLRewritingResultEnum.GetFile:
            case URLRewritingResultEnum.GetProduct:
            case URLRewritingResultEnum.SystemPage:
            case URLRewritingResultEnum.TrackbackPage:
                // Check whether session is available
                if (HttpContext.Current.Session != null)
                {
                    // Get sitename
                    string siteName = SiteInfoProvider.CurrentSiteName;

                    // Process banned IPs
                    if ((!String.IsNullOrEmpty(siteName)) && BannedIPInfoProvider.IsBannedIPEnabled(siteName))
                    {
                        DateTime lastCheck = ValidationHelper.GetDateTime(SessionHelper.GetValue("CMSBannedLastCheck"), DateTimeHelper.ZERO_TIME);
                        bool banned = false;

                        // Check if there wasn't change in banned IP settings
                        if (lastCheck <= BannedIPInfoProvider.LastChange)
                        {
                            if (!BannedIPInfoProvider.IsAllowed(siteName, BanControlEnum.Complete))
                            {
                                SessionHelper.SetValue("CMSBanned", true);
                                banned = true;
                            }
                            else
                            {
                                SessionHelper.Remove("CMSBanned");
                            }

                            //Update timestamp
                            SessionHelper.SetValue("CMSBannedLastCheck", DateTime.Now);
                        }
                        else
                        {
                            banned = (SessionHelper.GetValue("CMSBanned") != null);
                        }

                        // Check if this session was banned
                        if (banned)
                        {
                            BannedIPInfoProvider.BanRedirect(siteName);
                        }
                    }
                }
                break;
        }

        #endregion


        if ((status == URLRewritingResultEnum.PathRewritten) ||
            (status == URLRewritingResultEnum.MVCPage))
        {
            // Check page security
            if (HttpContext.Current.Session != null)
            {
                string siteName = SiteInfoProvider.CurrentSiteName;

                // Do not use security check for full page cache pages
                if (!OutputFilter.OutputFilterEndRequestRequired)
                {
                    PageInfo currentPageInfo = CMSContext.CurrentPageInfo;
                    ViewModeEnum viewMode = PortalContext.ViewMode;

                    // Check view mode permissions
                    PortalHelper.CheckViewModePermissions(currentPageInfo, viewMode);

                    #region "Check path"

                    string relativePath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
                    relativePath = relativePath.Remove(0, 1); //'~/path' -> '/path'

                    string aliasPath = CMSContext.CurrentAliasPath;

                    if ((currentPageInfo != null) && (aliasPath != currentPageInfo.NodeAliasPath))
                    {
                        // Set alias path to root if current page info is root page info
                        if (currentPageInfo.NodeAliasPath == "/")
                        {
                            CMSContext.CurrentAliasPath = "/";
                        }
                        // Display nothing if current alias path is not equal to page info alias path
                        else
                        {
                            CMSContext.CurrentPageInfo = null;
                            currentPageInfo = CMSContext.CurrentPageInfo;
                        }
                    }

                    if (currentPageInfo != null)
                    {
                        // Check preview link context
                        CheckPreviewLink(currentPageInfo, viewMode, true);

                        // Check SSL Require
                        URLRewriter.RequestSecurePage(currentPageInfo, true, viewMode, siteName);

                        // Check secured areas
                        URLRewriter.CheckSecuredAreas(siteName, currentPageInfo, true, viewMode);

                        // Check permissions
                        URLRewriter.CheckPermissions(siteName, currentPageInfo, true);
                    }

                    // Check default alias path
                    if ((aliasPath == "/") && (viewMode == ViewModeEnum.LiveSite))
                    {
                        string defaultAliasPath = SettingsKeyProvider.GetStringValue(siteName + ".CMSDefaultAliasPath");
                        string lowerDefaultAliasPath = defaultAliasPath.ToLower();
                        if ((defaultAliasPath != "") && (lowerDefaultAliasPath != aliasPath.ToLower()))
                        {
                            if (lowerDefaultAliasPath == "/default")
                            {
                                // Special case - default
                                CMSContext.CurrentAliasPath = defaultAliasPath;
                            }
                            else
                            {
                                // Redirect to the new path
                                URLHelper.Redirect(CMSContext.GetUrl(defaultAliasPath));
                            }
                        }
                    }

                    #endregion
                }

                // Update current session
                if (SessionManager.OnlineUsersEnabled && !URLHelper.IsExcludedSystem(URLHelper.CurrentRelativePath))
                {
                    SessionManager.UpdateCurrentSession(siteName);
                }
            }


            // Extend the expiration of the authentication cookie if required
            if (!UserInfoProvider.UseSessionCookies && (HttpContext.Current != null) && (HttpContext.Current.Session != null))
            {
                CookieHelper.ChangeCookieExpiration(FormsAuthentication.FormsCookieName, DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout), true);
            }
        }
        else
        {
            // Check other pages security
            if (HttpContext.Current.Session != null)
            {
                string siteName = SiteInfoProvider.CurrentSiteName;

                // Do not use security check for full page cache pages
                if (!OutputFilter.OutputFilterEndRequestRequired)
                {
                    // Check preview link context
                    if (VirtualContext.IsPreviewLinkInitialized)
                    {
                        // Validate page info
                        ViewModeEnum viewMode = PortalContext.ViewMode;
                        Guid previewGuid = ValidationHelper.GetGuid(VirtualContext.GetItem(VirtualContext.PARAM_WF_GUID), Guid.Empty);
                        DataSet ds = PageInfoProvider.GetPageInfos("DocumentWorkflowCycleGUID='" + previewGuid.ToString() + "'", null, 1, null);
                        if (!DataHelper.DataSourceIsEmpty(ds))
                        {
                            PageInfo pageInfo = new PageInfo(ds.Tables[0].Rows[0]);
                            CheckPreviewLink(pageInfo, viewMode, false);
                        }
                        else
                        {
                            // Reset the virtual context
                            VirtualContext.Reset();

                            // GUID values don't match
                            URLHelper.Redirect("~/CMSMessages/AccessDenied.aspx?message={$virtualcontext.previewlink$}");
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Check preview link context
    /// </summary>
    /// <param name="pageInfo">Page info</param>
    /// <param name="viewMode">View mode</param>
    /// <param name="documentUrl">Indicates if document URL should be checked</param>
    private static void CheckPreviewLink(PageInfo pageInfo, ViewModeEnum viewMode, bool documentUrl)
    {
        if (VirtualContext.IsPreviewLinkInitialized)
        {
            if (pageInfo != null)
            {
                Guid previewGuid = ValidationHelper.GetGuid(VirtualContext.GetItem(VirtualContext.PARAM_WF_GUID), Guid.Empty);
                if (previewGuid != Guid.Empty)
                {
                    // Force preview mode
                    if ((viewMode == ViewModeEnum.Preview) || (viewMode == ViewModeEnum.LiveSite))
                    {
                        // Preview link is valid
                        if (pageInfo.DocumentWorkflowCycleGUID == previewGuid)
                        {
                            if (documentUrl)
                            {
                                return;
                            }
                            // Additional check for links within the document
                            else if (VirtualContext.ValidatePreviewHash(URLHelper.CurrentRelativePath))
                            {
                                return;
                            }
                        }

                        // Reset the virtual context
                        VirtualContext.Reset();

                        // GUID values don't match
                        URLHelper.Redirect("~/CMSMessages/AccessDenied.aspx?message={$virtualcontext.accessdenied$}");
                    }
                }
            }
        }
    }


    /// <summary>
    /// Updates contact's IP and UserAgent information about visitor.
    /// </summary>
    /// <param name="siteName">Site name</param>
    private static void UpdateContactInformation(string siteName)
    {
        if (SettingsKeyProvider.GetBoolValue(siteName + ".CMSEnableOnlineMarketing"))
        {
            bool contactVisitedSite = CMSContext.ContactVisitedSite;

            if (!contactVisitedSite)
            {
                contactVisitedSite = true;
                ModuleCommands.OnlineMarketingUpdateContactInformation(siteName);
            }
        }
    }


    /// <summary>
    /// Attempts to run the scheduler request.
    /// </summary>
    /// <param name="status">Current status</param>
    private static void RunScheduler(URLRewritingResultEnum status)
    {
        // Scheduler is disabled
        if (!SchedulingHelper.EnableScheduler)
        {
            return;
        }

        // Ensure the rewriting status
        if (status == URLRewritingResultEnum.Unknown)
        {
            status = URLRewriter.CurrentStatus;
        }

        // Process scheduler only on content or system pages
        switch (status)
        {
            case URLRewritingResultEnum.PathRewritten:
            case URLRewritingResultEnum.MVCPage:
            case URLRewritingResultEnum.SystemPage:
            case URLRewritingResultEnum.SentFromCache:
                // Run scheduler - Do not run on first request to provide faster application start
                {
                    string siteName = SchedulingTimer.SchedulerRunImmediatelySiteName;
                    if (siteName != "")
                    {
                        if (SchedulingHelper.UseAutomaticScheduler)
                        {
                            // Ensure the active timer running in an asynchronous thread
                            SchedulingTimer timer = SchedulingTimer.EnsureTimer(siteName, true);
                            if (SchedulingTimer.RunSchedulerImmediately)
                            {
                                timer.ExecuteAsync();
                            }
                        }
                        else
                        {
                            // --- Default scheduler settings
                            // If scheduler run request acquired, run the actions
                            bool runScheduler = SchedulingTimer.RequestRun(siteName) || SchedulingTimer.RunSchedulerImmediately;
                            if (runScheduler)
                            {
                                if (SchedulingHelper.RunSchedulerWithinRequest)
                                {
                                    // --- Default scheduler settings
                                    try
                                    {
                                        try
                                        {
                                            // Flush the output
                                            HttpContext.Current.Response.Flush();
                                        }
                                        // Do not display closed host exception
                                        catch
                                        {
                                        }

                                        // Run scheduler actively within the request                                    
                                        SchedulingExecutor.ExecuteScheduledTasks(siteName, WebSyncHelperClass.ServerName);
                                    }
                                    catch (Exception ex)
                                    {
                                        EventLogProvider.LogException("Scheduler", "ExecuteScheduledTasks", ex);
                                    }
                                }
                                else
                                {
                                    // Get passive timer and execute
                                    SchedulingTimer timer = SchedulingTimer.EnsureTimer(siteName, false);
                                    timer.ExecuteAsync();
                                }
                            }
                        }
                    }
                }
                break;
        }
    }


    /// <summary>
    /// Ensures performance counter timer.
    /// </summary>
    private static void EnsurePerformanceCounterTimer()
    {
        // Health monitoring is enabled
        if (HealthMonitoringHelper.LogCounters)
        {
            // Get passive timer and execute
            PerformanceCounterTimer timer = PerformanceCounterTimer.EnsureTimer();
            timer.EnsureRunTimerAsync();
        }
    }


    /// <summary>
    /// Logs the last application error.
    /// </summary>
    private static void LogLastApplicationError()
    {
        if (ConnectionAvailable)
        {
            if (HttpContext.Current != null)
            {
                Exception ex = HttpContext.Current.Server.GetLastError();
                if (ex != null)
                {
                    string eventCode = "EXCEPTION";
                    string eventType = EventLogProvider.EVENT_TYPE_ERROR;

                    // Log request operation
                    RequestHelper.LogRequestOperation("OnError", ex.Message, 0);

                    bool log = true;

                    // Page not found was already manually logged
                    if (URLRewriter.CurrentStatus == URLRewritingResultEnum.PageNotFound)
                    {
                        log = false;
                    }

                    if (log)
                    {
                        // Impersonation context
                        WindowsImpersonationContext ctx = null;

                        try
                        {
                            // Impersonate current thread
                            ctx = mWindowsIdentity.Impersonate();

                            // Initiate the event
                            bool logException = true;
                            SystemEvents.Exception.StartEvent(ex, ref logException);

                            if (logException)
                            {
                                // Get the lowest exception
                                while (ex.InnerException != null)
                                {
                                    ex = ex.InnerException;
                                }

                                // Write error to Event log
                                try
                                {
                                    EventLogProvider eProvider = new EventLogProvider();
                                    eProvider.LogEvent(eventType, DateTime.Now, "Application_Error", eventCode, CMSContext.CurrentUser.UserID, CMSContext.CurrentUser.UserName, (CMSContext.CurrentDocument != null) ? CMSContext.CurrentDocument.NodeID : 0, (CMSContext.CurrentDocument != null) ? CMSContext.CurrentDocument.DocumentName : null, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSiteID, HTTPHelper.GetAbsoluteUri());
                                }
                                catch
                                {
                                    // can't write to log, do not process any code
                                }
                            }
                        }
                        finally
                        {
                            if (ctx != null)
                            {
                                ctx.Undo();
                            }
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Logs the application end.
    /// </summary>
    private static void LogApplicationEnd()
    {
        EventLogProvider eventLog = new EventLogProvider();

        // Get the shutdown reason
        System.Web.HttpRuntime runtime = (System.Web.HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
        if (runtime != null)
        {
            string shutDownMessage = Convert.ToString(runtime.GetType().InvokeMember("_shutDownMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null));
            string shutDownStack = Convert.ToString(runtime.GetType().InvokeMember("_shutDownStack", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, runtime, null));

            StackTrace stack = new StackTrace();

            string callStack = stack.ToString();
            string logMessage = "Message: " + shutDownMessage + "<br />\nShutdown stack: " + shutDownStack + "<br />\nCall stack: " + callStack;

            eventLog.LogEvent(EventLogProvider.EVENT_TYPE_WARNING, DateTime.Now, "Application_End", "ENDAPP", 0, HTTPHelper.GetUserName(), 0, "", null, logMessage, 0, null);
        }
        else
        {
            eventLog.LogEvent(EventLogProvider.EVENT_TYPE_WARNING, DateTime.Now, "Application_End", "ENDAPP", 0, HTTPHelper.GetUserName(), 0, "", null, "", 0, null);
        }
    }


    /// <summary>
    /// Processes the current URL referrer data.
    /// </summary>
    /// <param name="siteName">Site name</param>
    private static void ProcessReferrer(string siteName)
    {
        // Prepare data
        Uri referrer = HttpContext.Current.Request.UrlReferrer;
        if ((referrer != null) && (CMSContext.ViewMode == ViewModeEnum.LiveSite))
        {
            if (!referrer.AbsoluteUri.StartsWith("/") && !referrer.IsLoopback && (referrer.Host.ToLower() != URLHelper.Url.Host))
            {
                // Check other site domains
                SiteInfo rsi = SiteInfoProvider.GetRunningSiteInfo(referrer.Host, URLHelper.ApplicationPath);
                if ((rsi == null) || (rsi.SiteName != siteName))
                {
                    string path = URLHelper.RemoveQuery(referrer.AbsoluteUri);

                    // Save the referrer value
                    CMSContext.CurrentUser.URLReferrer = path;

                    // Log referral
                    string ip = HTTPHelper.UserHostAddress;
                    if (AnalyticsHelper.IsLoggingEnabled(siteName, String.Empty) && AnalyticsHelper.TrackReferralsEnabled(siteName))
                    {
                        HitLogProvider.LogHit(HitLogProvider.URL_REFERRALS, siteName, null, path, 0);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Sets the initial debugging settings.
    /// </summary>
    private static void SetInitialDebug()
    {
        if (CMSFunctions.AnyDebugEnabled)
        {
            // Prepare the context values
            ViewModeOnDemand viewMode = new ViewModeOnDemand();
            RequestSettingsOnDemand settings = new RequestSettingsOnDemand();

            bool isLiveSite = (viewMode.Value == ViewModeEnum.LiveSite);

            // Set request debugging
            if (RequestHelper.DebugRequests)
            {
                settings.Value.DebugRequest = RequestHelper.DebugAllRequests || isLiveSite;
                RequestHelper.LogRequestOperation("BeginRequest", null, 0);
            }
            if (SqlHelperClass.DebugQueries)
            {
                settings.Value.DebugSQLQueries = SqlHelperClass.DebugAllQueries || isLiveSite;
            }
            if (CacheHelper.DebugCache)
            {
                settings.Value.DebugCache = CacheHelper.DebugAllCaches || isLiveSite;
            }
            if (SecurityHelper.DebugSecurity)
            {
                settings.Value.DebugSecurity = SecurityHelper.DebugAllSecurity || isLiveSite;
            }
            if (File.DebugFiles)
            {
                settings.Value.DebugFiles = File.DebugAllFiles || isLiveSite;
            }
            if (MacroResolver.DebugMacros)
            {
                settings.Value.DebugMacros = MacroResolver.DebugAllMacros || isLiveSite;
            }
            if (OutputHelper.DebugOutput)
            {
                settings.Value.DebugOutput = OutputHelper.DebugAllOutputs || isLiveSite;
            }
            if (WebSyncHelperClass.DebugWebFarm)
            {
                settings.Value.DebugWebFarm = WebSyncHelperClass.DebugAllWebFarm || isLiveSite;
            }
            if (AnalyticsHelper.DebugAnalytics)
            {
                settings.Value.DebugAnalytics = AnalyticsHelper.DebugAllAnalytics || isLiveSite;
            }

        }
    }


    /// <summary>
    /// Check URL query string for authentication parameter and authenticate user.
    /// </summary>
    private static void CheckAuthenticationGUID()
    {
        // Check for authentication token
        if (QueryHelper.Contains("authenticationGuid") && SettingsKeyProvider.GetBoolValue("CMSAutomaticallySignInUser"))
        {
            UserInfo ui = null;

            if (!CMSContext.IsAuthenticated())
            {
                // Get authentication token
                Guid authGuid = QueryHelper.GetGuid("authenticationGuid", Guid.Empty);
                if (authGuid != Guid.Empty)
                {
                    // Get users with found authentication token
                    DataSet ds = UserInfoProvider.GetFullUsers("UserAuthenticationGUID = '" + authGuid + "'", null, 1, null);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        // Authenticate user
                        ui = new UserInfo(ds.Tables[0].Rows[0]);
                        CMSContext.AuthenticateUser(ui.UserName, false, false);
                    }
                }
            }
            else
            {
                // Get current user info
                ui = CMSContext.CurrentUser;
            }

            // Remove authentication GUID
            if ((ui != null) && (ui.UserAuthenticationGUID != Guid.Empty))
            {
                using (CMSActionContext context = new CMSActionContext())
                {
                    context.DisableAll();
                    context.CreateSearchTask = false;

                    ui.UserAuthenticationGUID = Guid.Empty;
                    ui.Generalized.SetObject();
                }
            }

            // Redirect to URL without authentication token
            URLHelper.Redirect(URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, "authenticationGuid"));
        }
    }


    /// <summary>
    /// Redirects the file to the images folder.
    /// </summary>
    protected static bool FileRedirect()
    {
        string cmsimg = QueryHelper.GetString("cmsimg", null);
        if ((cmsimg != null) && cmsimg.StartsWith("/"))
        {
            if (cmsimg.StartsWith(UIHelper.UNIGRID_ICONS))
            {
                // Unigrid actions
                cmsimg = "Design/Controls/UniGrid/Actions" + cmsimg.Substring(3);
            }
            else if (cmsimg.StartsWith(UIHelper.TREE_ICONS))
            {
                // Tree icons
                cmsimg = "Design/Controls/Tree" + cmsimg.Substring(2);
            }
            else if (cmsimg.StartsWith(UIHelper.TREE_ICONS_RTL))
            {
                // Tree icons RTL
                cmsimg = "RTL/Design/Controls/Tree" + cmsimg.Substring(3);
            }
            else if (cmsimg.StartsWith(UIHelper.FLAG_ICONS))
            {
                // Flag icons
                cmsimg = "Flags/16x16" + cmsimg.Substring(2);
            }
            else if (cmsimg.StartsWith(UIHelper.FLAG_ICONS_48))
            {
                // Large flag icons
                cmsimg = "Flags/48x48" + cmsimg.Substring(4);
            }

            // Redirect to the correct location
            URLHelper.PermanentRedirect(UIHelper.GetImageUrl(null, cmsimg));

            return true;
        }

        return false;
    }


    /// <summary>
    /// Reinitializes the application by reseting the application variables.
    /// </summary>
    public static void ReInit()
    {
        mApplicationStart = DateTime.Now;
        mApplicationStartFinished = DateTime.MinValue;

        firstEndRequestAfterStart = true;

        mApplicationInitialized = null;

        mConnectionErrorMessage = null;
    }

    #endregion
}
