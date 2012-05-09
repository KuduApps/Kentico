using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Synchronization;

public partial class CMSModules_Staging_Tools_Data_TaskSeparator : CMSStagingDataPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check enabled servers
        if (!ServerInfoProvider.IsEnabledServer(CMSContext.CurrentSiteID))
        {
            lblInfo.Text = GetString("ObjectStaging.NoEnabledServer");
        }
        else if (SettingsKeyProvider.GetBoolValue("CMSStagingLogDataChanges"))
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
            lblInfo.Text = GetString("DataStaging.TaskSeparator");
        }
    }
}
