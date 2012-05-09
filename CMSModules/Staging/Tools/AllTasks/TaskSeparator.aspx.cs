using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Synchronization;

public partial class CMSModules_Staging_Tools_AllTasks_TaskSeparator : CMSStagingAllTasksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string siteName = CMSContext.CurrentSiteName;

        // Check enabled servers
        if (!ServerInfoProvider.IsEnabledServer(CMSContext.CurrentSiteID))
        {
            lblInfo.Text = GetString("ObjectStaging.NoEnabledServer");
        }
        else
        {
            // Check logging
            bool somethingLogged = false;

            // Site object tasks
            if (SettingsKeyProvider.GetBoolValue(siteName + ".CMSStagingLogObjectChanges"))
            {
                somethingLogged = true;
            }

            // Global object tasks
            if (SettingsKeyProvider.GetBoolValue("CMSStagingLogObjectChanges"))
            {
                somethingLogged = true;
            }

            // Document tasks
            if (SettingsKeyProvider.GetBoolValue(siteName + ".CMSStagingLogChanges"))
            {
                somethingLogged = true;
            }

            // Data tasks
            if (SettingsKeyProvider.GetBoolValue("CMSStagingLogDataChanges"))
            {
                somethingLogged = true;
            }

            if (somethingLogged)
            {
                // Check DLL required for for staging
                if (SiteManagerFunctions.CheckStagingDLL())
                {
                    URLHelper.Redirect("Frameset.aspx");
                }

                lblInfo.Text = GetString("ObjectStaging.RenameDll");
            }
            else
            {
                lblInfo.Text = GetString("AllTasks.TaskSeparator");
            }
        }
    }
}
