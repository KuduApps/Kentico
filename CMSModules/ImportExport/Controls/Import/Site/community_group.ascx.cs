using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSImportExport;
using CMS.GlobalHelper;

public partial class CMSModules_ImportExport_Controls_Import_Site_community_group : ImportExportControl
{
    /// <summary>
    /// True if import into existing site.
    /// </summary>
    protected bool ExistingSite
    {
        get
        {
            if (Settings != null)
            {
                return ((SiteImportSettings)Settings).ExistingSite;
            }
            return true;
        }
    }


    /// <summary>
    /// True if the data should be imported.
    /// </summary>
    protected bool Import
    {
        get
        {
            return this.chkObject.Checked;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkObject.Visible = true;
        this.chkObject.Text = GetString("CMSImport_CommunityGroups.ImportGroupMembership");

    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        Settings.SetSettings(ImportExportHelper.SETTINGS_GROUP_MEMBERSHIP, Import);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkObject.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_GROUP_MEMBERSHIP), !ExistingSite);
    }
}
