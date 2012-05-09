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
using CMS.CMSImportExport;

public partial class CMSModules_ImportExport_Controls_Import_Site_media_library : ImportExportControl
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
            if (ExistingSite)
            {
                return false;
            }
            return this.chkFiles.Checked;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        this.chkFiles.Text = GetString("CMSImport_MediaLibrary.ImportFiles");
        this.chkPhysicalFiles.Text = GetString("CMSImport.ImportPhysicalFiles");

        // Javascript
        string script = "var medChck1 = document.getElementById('" + chkFiles.ClientID + "'); \n" +
                        "var medChck2 = document.getElementById('" + chkPhysicalFiles.ClientID + "'); \n" +
                        "medInitCheckboxes(); \n";

        this.ltlScript.Text = ScriptHelper.GetScript(script);

        this.chkFiles.Attributes.Add("onclick", "medCheckChange();");
    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        Settings.SetSettings(ImportExportHelper.SETTINGS_MEDIA_FILES, chkFiles.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_MEDIA_FILES_PHYSICAL, (chkPhysicalFiles.Checked && chkPhysicalFiles.Enabled));
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkFiles.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_MEDIA_FILES), true);
        this.chkPhysicalFiles.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_MEDIA_FILES_PHYSICAL), false);
    }
}
