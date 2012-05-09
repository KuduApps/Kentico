using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSImportExport;
using CMS.GlobalHelper;

public partial class CMSModules_ImportExport_Controls_Import_Site_cms_pagetemplate : ImportExportControl
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
    /// True if the web part/zone.widget variants should be imported.
    /// </summary>
    protected bool ImportVariants
    {
        get
        {
            return this.chkVariants.Checked;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkVariants.Visible = true;
        this.chkVariants.Text = GetString("CMSImport_PageTemplates.ImportSitePageTemplatesVariants");
    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        Settings.SetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_VARIANTS, ImportVariants);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkVariants.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_VARIANTS), !ExistingSite);
    }
}
