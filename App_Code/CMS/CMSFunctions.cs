using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OutputFilter;
using CMS.SiteProvider;
using CMS.IO;
using CMS.Controls;
using CMS.WebAnalytics;
using CMS.WebFarmSyncHelper;
using CMS.EventLog;

using IOExceptions = System.IO;

/// <summary>
/// Global CMS Functions.
/// </summary>
public static class CMSFunctions
{
    #region "Variables"

    public static bool mAsyncInit = true;

    /// <summary>
    /// If true, at least one debug has the logging to file enabled.
    /// </summary>
    private static bool? mAnyDebugLogToFileEnabled = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns true if any debug is enabled.
    /// </summary>
    public static bool AnyDebugEnabled
    {
        get
        {
            return CMSContext.AnyDebugEnabled;
        }
        set
        {
            CMSContext.AnyDebugEnabled = value;
        }
    }


    /// <summary>
    /// Returns true if any debug has the logging to file enabled.
    /// </summary>
    public static bool AnyDebugLogToFileEnabled
    {
        get
        {
            if (mAnyDebugLogToFileEnabled == null)
            {
                mAnyDebugLogToFileEnabled =
                    SqlHelperClass.LogQueries ||
                    CacheHelper.LogCache ||
                    OutputHelper.LogOutputToFile ||
                    SecurityHelper.LogSecurity ||
                    MacroResolver.LogMacros ||
                    File.LogFiles ||
                    WebSyncHelperClass.LogWebFarm ||
                    RequestHelper.LogRequests ||
                    AnalyticsHelper.LogAnalytics;
            }

            return mAnyDebugLogToFileEnabled.Value;
        }
        set
        {
            mAnyDebugLogToFileEnabled = value;
        }
    }


    /// <summary>
    /// Returns true if any LiveDebug is enabled.
    /// </summary>
    public static bool AnyLiveDebugEnabled
    {
        get
        {
            return CMSContext.AnyLiveDebugEnabled;
        }
        set
        {
            CMSContext.AnyLiveDebugEnabled = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Sets all the debug settings to null and causes them to be reloaded.
    /// </summary>
    public static void ResetDebugSettings()
    {
        mAnyDebugLogToFileEnabled = null;
        SqlHelperClass.ResetDebugSettings();
        AnalyticsHelper.ResetDebugSettings();
        RequestHelper.ResetDebugSettings();
        WebSyncHelperClass.ResetDebugSettings();
        DebugHelper.ResetDebugSettings();
        File.ResetDebugSettings();
        SecurityHelper.ResetDebugSettings();
        MacroResolver.ResetDebugSettings();
        CacheHelper.ResetDebugSettings();
        OutputHelper.ResetDebugSettings();
        CMSControlsHelper.ResetDebugSettings();
        CMSContext.ResetDebugSettings();
    }


    /// <summary>
    /// Initializes the system.
    /// </summary>
    public static void Init()
    {
        // Init CMS environment
        CMSContext.Init(mAsyncInit);

        // Register the events
        RegisterEvents();

        // Register module methods and transformation methods to macro resolver
        CMSModuleLoader loader = new CMSModuleLoader();
        loader.RegisterTransformationMethods();

        MacroMethods.RegisterMethods();
        CMSMacroMethods.RegisterMethods();
    }


    /// <summary>
    /// Waits until the initialization is completed.
    /// </summary>
    public static void WaitForInitialization()
    {
        if (mAsyncInit)
        {
            // Wait until all modules are ready
            while (!CMSContext.ModulesReady)
            {
                Thread.Sleep(20);
            }
        }
    }


    /// <summary>
    /// Registers the events.
    /// </summary>
    private static void RegisterEvents()
    {
        // Register watchers
        try
        {
            RegisterWatchers();
        }
        catch
        {
            // This code causes issues in medium trust
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent(EventLogProvider.EVENT_TYPE_WARNING, DateTime.Now, "WebFarm", "FullTrustRequest", 0, null, 0, null, null, "Web farm notify watcher wasn't initialized. It is available only under full trust level.", 0, null);
        }
    }


    /// <summary>
    /// Initializes file system watchers
    /// </summary>
    private static void RegisterWatchers()
    {
        // Init file system watchers
        if (!AzureHelper.IsRunningOnAzure)
        {
            if (!SystemHelper.IsFullTrustLevel)
            {
                EventLogProvider ep = new EventLogProvider();
                ep.LogEvent(EventLogProvider.EVENT_TYPE_WARNING, DateTime.Now, "WebFarm", "FullTrustRequest", 0, null, 0, null, null, "Web farm notify watcher wasn't initialized. It is available only under full trust level.", 0, null);
            }
            else
            {
                WebSyncHelper.NotifyWatcher.Changed += NotifyWatcher_Changed;
            }
        }
    }


    /// <summary>
    /// Clears the system cache.
    /// </summary>
    public static void ClearCache()
    {
        CacheHelper.ClearCache(null, true);

        // Collect the memory
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }


    /// <summary>
    /// Handles changed event of file system watcher.
    /// </summary>
    /// <param name="sender">File system watcher</param>
    /// <param name="e">File system event argument</param>
    private static void NotifyWatcher_Changed(object sender, IOExceptions.FileSystemEventArgs e)
    {
        try
        {
            // Temporarily disable raising events because event OnChange is called twice when file is changed
            WebSyncHelper.NotifyWatcher.EnableRaisingEvents = false;

            // Process web farm tasks
            WebSyncHelper.ProcessMyTasks();
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider.LogException("FileSystemWatcher", "Changed", ex);
        }
        finally
        {
            // Enable raising events
            WebSyncHelper.NotifyWatcher.EnableRaisingEvents = true;
        }
    }

    #endregion
}
