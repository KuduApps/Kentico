using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSImportExport;
using CMS.GlobalHelper;

public partial class CMSModules_ImportExport_Controls_Import_cms_pagetemplate : ImportExportControl
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
    protected bool ImportScopes
    {
        get
        {
            return this.chkObject.Checked && this.Visible;
        }
    }


    /// <summary>
    /// True if the web part/zone.widget variants should be imported.
    /// </summary>
    protected bool ImportVariants
    {
        get
        {
            return this.chkVariants.Checked && this.Visible;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkObject.Text = GetString("CMSImport_PageTemplates.ImportSitePageTemplatesScopes");
        this.chkVariants.Text = GetString("CMSImport_PageTemplates.ImportSitePageTemplatesVariants");
        this.Visible = ((SiteImportSettings)Settings).SiteIsIncluded;
    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        Settings.SetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_SCOPES, ImportScopes);
        Settings.SetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_VARIANTS, ImportVariants);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkObject.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_SCOPES), !ExistingSite && this.Visible);
        this.chkVariants.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_VARIANTS), !ExistingSite && this.Visible);
    }
}
