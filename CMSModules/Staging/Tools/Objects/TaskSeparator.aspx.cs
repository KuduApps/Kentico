using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Synchronization;

public partial class CMSModules_Staging_Tools_Objects_TaskSeparator : CMSStagingObjectsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string siteName = CMSContext.CurrentSiteName;

        // Check enabled servers
        if (!ServerInfoProvider.IsEnabledServer(CMSContext.CurrentSiteID))
        {
            lblInfo.Text = GetString("ObjectStaging.NoEnabledServer");
        }
        else if (ValidationHelper.GetBoolean(SettingsKeyProvider.GetBoolValue(siteName + ".CMSStagingLogObjectChanges"), false) || ValidationHelper.GetBoolean(SettingsKeyProvider.GetBoolValue("CMSStagingLogObjectChanges"), false))
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
            lblInfo.Text = GetString("ObjectStaging.TaskSeparator");
        }
    }
}
