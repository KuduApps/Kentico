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
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.FormEngine;
using CMS.EmailEngine;
using CMS.Scheduler;
using CMS.PortalEngine;
using CMS.TreeEngine;

public partial class CMSModules_ImportExport_Controls_Export_Site_cms_form : ImportExportControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkObject.Text = GetString("CMSExport_BizForms.ExportBizForm");
        this.chkPhysicalFiles.Text = GetString("CMSExport.ExportPhysicalFiles");

        // Javascript
        string script = "var bizChck1 = document.getElementById('" + chkObject.ClientID + "'); \n" +
                        "var bizChck2 = document.getElementById('" + chkPhysicalFiles.ClientID + "'); \n" +
                        "bizInitCheckboxes(); \n";

        ltlScript.Text = ScriptHelper.GetScript(script);

        this.chkObject.Attributes.Add("onclick", "bizCheckChange();");
    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        Settings.SetSettings(ImportExportHelper.SETTINGS_BIZFORM_DATA, chkObject.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_BIZFORM_FILES_PHYSICAL, (chkPhysicalFiles.Checked && chkPhysicalFiles.Enabled));
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkObject.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_BIZFORM_DATA), false);
        this.chkPhysicalFiles.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_BIZFORM_FILES_PHYSICAL), true);
    }
}
