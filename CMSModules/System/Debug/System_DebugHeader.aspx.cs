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

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.Controls;
using CMS.SiteProvider;
using CMS.OutputFilter;
using CMS.SettingsProvider;
using CMS.IO;
using CMS.WebAnalytics;

public partial class CMSModules_System_Debug_System_DebugHeader : CMSDebugPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.HelpTopicName = "debugobjects_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes PageTitle
        string[,] pageTitleTabs = new string[2, 3];

        pageTitleTabs[0, 0] = GetString("Administration-System.Header");
        pageTitleTabs[0, 1] = null;
        pageTitleTabs[0, 2] = null;

        pageTitleTabs[1, 0] = GetString("Administration-System.Debug");
        pageTitleTabs[1, 1] = null;
        pageTitleTabs[1, 2] = null;

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }


    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[16, 5];
        int index = 0;

        tabs[index, 0] = GetString("Administration-System.DebugObjects");
        tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugobjects_tab');";
        tabs[index, 2] = "System_DebugObjects.aspx";
        index++;

        tabs[index, 0] = GetString("Administration-System.DebugCacheItems");
        tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugcacheitems_tab');";
        tabs[index, 2] = "System_DebugCacheItems.aspx";
        index++;

        tabs[index, 0] = GetString("Administration-System.DebugThreads");
        tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugthreads_tab');";
        tabs[index, 2] = "System_DebugThreads.aspx";
        index++;

        if (CacheHelper.DebugCache)
        {
            tabs[index, 0] = GetString("Administration-System.DebugCache");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugcache_tab');";
            tabs[index, 2] = "System_DebugCache.aspx";
            index++;
        }

        if (SqlHelperClass.DebugQueries)
        {
            tabs[index, 0] = GetString("Administration-System.DebugSQL");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugsql_tab');";
            tabs[index, 2] = "System_DebugSQL.aspx";
            index++;
        }

        if (File.DebugFiles)
        {
            tabs[index, 0] = GetString("Administration-System.DebugFiles");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugfiles_tab');";
            tabs[index, 2] = "System_DebugFiles.aspx";
            index++;
        }

        if (CMSControlsHelper.DebugViewState)
        {
            tabs[index, 0] = GetString("Administration-System.DebugViewState");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugviewstate_tab');";
            tabs[index, 2] = "System_DebugViewState.aspx";
            index++;
        }

        if (OutputHelper.DebugOutput)
        {
            tabs[index, 0] = GetString("Administration-System.DebugOutput");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugoutput_tab');";
            tabs[index, 2] = "System_DebugOutput.aspx";
            index++;
        }

        if (SecurityHelper.DebugSecurity)
        {
            tabs[index, 0] = GetString("Administration-System.DebugSecurity");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugsecurity_tab');";
            tabs[index, 2] = "System_DebugSecurity.aspx";
            index++;
        }

        if (MacroResolver.DebugMacros)
        {
            tabs[index, 0] = GetString("Administration-System.DebugMacros");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugmacros_tab');";
            tabs[index, 2] = "System_DebugMacros.aspx";
            index++;
        }

        if (AnalyticsHelper.DebugAnalytics)
        {
            tabs[index, 0] = GetString("Administration-System.DebugAnalytics");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugAnalytics_tab');";
            tabs[index, 2] = "System_DebugAnalytics.aspx";
            index++;
        }
        
        if (RequestHelper.DebugRequests)
        {
            tabs[index, 0] = GetString("Administration-System.DebugRequests");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugrequests_tab');";
            tabs[index, 2] = "System_DebugRequests.aspx";
            index++;
        }

        if (WebSyncHelperClass.DebugWebFarm && WebSyncHelperClass.WebFarmEnabled)
        {
            tabs[index, 0] = GetString("Administration-System.DebugWebFarm");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugwebfarm_tab');";
            tabs[index, 2] = "System_DebugWebFarm.aspx";
            index++;
        }

        if (SettingsKeyProvider.DevelopmentMode)
        {
            tabs[index, 0] = GetString("Administration-System.DebugLoad");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugload_tab');";
            tabs[index, 2] = "System_DebugLoad.aspx";
            index++;
        }

        int numOfDebugAllowed =
            (SqlHelperClass.DebugQueries ? 1 : 0) +
            (File.DebugFiles ? 1 : 0) +
            (SecurityHelper.DebugSecurity ? 1 : 0) +
            (MacroResolver.DebugMacros ? 1 : 0) +
            (CacheHelper.DebugCache ? 1 : 0);

        // Display aggregated debug only when at least two debugs are on
        if (RequestHelper.DebugRequests && (numOfDebugAllowed > 0))
        {
            tabs[index, 0] = GetString("Administration-System.AllDebug");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'debugall_tab');";
            tabs[index, 2] = "System_DebugAll.aspx";
            index++;
        }

        if (CMSFunctions.AnyDebugEnabled && CMSFunctions.AnyDebugLogToFileEnabled)
        {
            tabs[index, 0] = GetString("Administration-System.LogFiles");
            tabs[index, 1] = "SetHelpTopic('helpTopic', 'logfiles_tab');";
            tabs[index, 2] = "System_LogFiles.aspx";
            index++;
        }

        this.CurrentMaster.Tabs.UrlTarget = "systemDebug";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }
}
