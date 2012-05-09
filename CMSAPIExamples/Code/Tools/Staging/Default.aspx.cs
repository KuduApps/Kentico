using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

[Title(Text = "Staging", ImageUrl = "/CMSModules/CMS_Staging/module.png")]
public partial class CMSAPIExamples_Code_Tools_Staging_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Staging);

        // Staging server
        this.apiCreateStagingServer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateStagingServer);
        this.apiGetAndUpdateStagingServer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateStagingServer);
        this.apiGetAndBulkUpdateStagingServers.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateStagingServers);

        // Staging task
        this.apiGetAndSynchronizeTasks.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndSynchronizeTasks);

        // Cleanup  
        this.apiDeleteTasks.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteTasks);
        this.apiDeleteStagingServer.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteStagingServer);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Staging server
        this.apiCreateStagingServer.Run();
        this.apiGetAndUpdateStagingServer.Run();
        this.apiGetAndBulkUpdateStagingServers.Run();

        // Staging task
        this.apiGetAndSynchronizeTasks.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Staging tasks
        this.apiDeleteTasks.Run();

        // Staging server
        this.apiDeleteStagingServer.Run();
    }

    #endregion


    #region "API examples - Staging server"

    /// <summary>
    /// Creates staging server. Called when the "Create server" button is pressed.
    /// </summary>
    private bool CreateStagingServer()
    {
        // Create new staging server object
        ServerInfo newServer = new ServerInfo();

        // Set the properties
        newServer.ServerDisplayName = "My new server";
        newServer.ServerName = "MyNewServer";
        newServer.ServerEnabled = true;
        newServer.ServerSiteID = CMSContext.CurrentSiteID;
        newServer.ServerURL = "http://localhost/KenticoCMS/CMSPages/SyncServer.asmx";
        newServer.ServerAuthentication = ServerAuthenticationEnum.UserName;
        newServer.ServerUsername = "admin";
        newServer.ServerPassword = "pass";

        // Save the staging server
        ServerInfoProvider.SetServerInfo(newServer);

        return true;
    }


    /// <summary>
    /// Gets and updates staging server. Called when the "Get and update server" button is pressed.
    /// Expects the CreateStagingServer method to be run first.
    /// </summary>
    private bool GetAndUpdateStagingServer()
    {
        // Get the staging server
        ServerInfo updateServer = ServerInfoProvider.GetServerInfo("MyNewServer", CMSContext.CurrentSiteID);
        if (updateServer != null)
        {
            // Update the properties
            updateServer.ServerDisplayName = updateServer.ServerDisplayName.ToLower();

            // Save the changes
            ServerInfoProvider.SetServerInfo(updateServer);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates staging servers. Called when the "Get and bulk update servers" button is pressed.
    /// Expects the CreateStagingServer method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateStagingServers()
    {
        // Prepare the parameters
        string where = "ServerName LIKE N'MyNewServer%'";

        // Get the data for the current site
        DataSet servers = ServerInfoProvider.GetSiteServers(CMSContext.CurrentSiteID, where, null, -1, null, false);
        if (!DataHelper.DataSourceIsEmpty(servers))
        {
            // Loop through the individual items
            foreach (DataRow serverDr in servers.Tables[0].Rows)
            {
                // Create object from DataRow
                ServerInfo modifyServer = new ServerInfo(serverDr);

                // Update the properties
                modifyServer.ServerDisplayName = modifyServer.ServerDisplayName.ToUpper();

                // Save the changes
                ServerInfoProvider.SetServerInfo(modifyServer);
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes staging server. Called when the "Delete server" button is pressed.
    /// Expects the CreateStagingServer method to be run first.
    /// </summary>
    private bool DeleteStagingServer()
    {
        // Get the staging server
        ServerInfo deleteServer = ServerInfoProvider.GetServerInfo("MyNewServer", CMSContext.CurrentSiteID);

        // Delete the staging server
        ServerInfoProvider.DeleteServerInfo(deleteServer);

        return (deleteServer != null);
    }

    #endregion


    #region "API examles - Staging tasks"

    /// <summary>
    /// Synchronizes all tasks. Called when the "Get and synchronize tasks" button is pressed.
    /// Expects the CreateStagingServer method to be run first and that there are tasks logged
    /// for the server.
    /// </summary>
    private bool GetAndSynchronizeTasks()
    {
        // Get server
        ServerInfo server = ServerInfoProvider.GetServerInfo("MyNewServer", CMSContext.CurrentSiteID);

        if (server != null)
        {
            // Get tasks for the server
            DataSet tasks = TaskInfoProvider.SelectTaskList(CMSContext.CurrentSiteID, server.ServerID, null, null);

            if (!DataHelper.DataSourceIsEmpty(tasks))
            {
                foreach (DataRow taskDr in tasks.Tables[0].Rows)
                {
                    // Create task info object from data row
                    TaskInfo task = new TaskInfo(taskDr);

                    // Synchronize the task
                    if (!string.IsNullOrEmpty(StagingHelper.RunSynchronization(task.TaskID, server.ServerID)))
                    {
                        apiGetAndSynchronizeTasks.ErrorMessage = "Synchronization failed.";
                        return false;
                    }
                }

                return true;
            }

            apiGetAndSynchronizeTasks.ErrorMessage = "No tasks found.";
        }

        return false;
    }

    /// <summary>
    /// Deletes staging tasks. Called when the "Delete tasks" button is pressed.
    /// Expects the CreateStagingServer method to be run first.
    /// </summary>
    private bool DeleteTasks()
    {
        // Get server
        ServerInfo server = ServerInfoProvider.GetServerInfo("MyNewServer", CMSContext.CurrentSiteID);

        if (server != null)
        {
            // Get tasks for the server
            DataSet tasks = TaskInfoProvider.SelectTaskList(CMSContext.CurrentSiteID, server.ServerID, null, null);

            if (!DataHelper.DataSourceIsEmpty(tasks))
            {
                foreach (DataRow taskDr in tasks.Tables[0].Rows)
                {
                    // Create task info object from data row
                    TaskInfo deleteTask = new TaskInfo(taskDr);

                    // Delete the task
                    TaskInfoProvider.DeleteTaskInfo(deleteTask);
                }

                return true;
            }

            apiDeleteTasks.ErrorMessage = "No tasks found.";
        }

        return false;
    }

    #endregion

}
