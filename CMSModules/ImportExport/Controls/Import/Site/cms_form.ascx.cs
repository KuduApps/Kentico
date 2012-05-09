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

public partial class CMSModules_ImportExport_Controls_Import_Site_cms_form : ImportExportControl
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
        this.chkObject.Text = GetString("CMSImport_BizForms.ImportBizForm");
        this.chkPhysicalFiles.Text = GetString("CMSImport.ImportPhysicalFiles");
        this.lblInfo.Text = GetString("CMSImport_BizForms.InfoLabel");

        // Javascript
        string script = "var bizChck1 = document.getElementById('" + chkObject.ClientID + "'); \n" +
                        "var bizChck2 = document.getElementById('" + chkPhysicalFiles.ClientID + "'); \n" +
                        "bizInitCheckboxes(); \n";

        this.ltlScript.Text = ScriptHelper.GetScript(script);

        this.chkObject.Attributes.Add("onclick", "bizCheckChange();");
    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        Settings.SetSettings(ImportExportHelper.SETTINGS_BIZFORM_DATA, Import);
        Settings.SetSettings(ImportExportHelper.SETTINGS_BIZFORM_FILES_PHYSICAL, (chkPhysicalFiles.Checked && chkPhysicalFiles.Enabled));
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkObject.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_BIZFORM_DATA), !ExistingSite);
        this.chkPhysicalFiles.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_BIZFORM_FILES_PHYSICAL), !ExistingSite);
    }
}
