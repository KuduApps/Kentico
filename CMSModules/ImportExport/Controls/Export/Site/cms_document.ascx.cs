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

public partial class CMSModules_ImportExport_Controls_Export_Site_cms_document : ImportExportControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkDocuments.Text = GetString("ExportDocuments.ExportDocuments");
        this.chkDocumentsHistory.Text = GetString("ExportDocuments.ExportDocumentsHistory");
        this.chkRelationships.Text = GetString("ExportDocuments.ExportRelationships");
        this.chkACLs.Text = GetString("ExportDocuments.ExportACLs");
        this.chkEventAttendees.Text = GetString("ExportDocuments.ExportEventAttendees");
        this.chkBlogComments.Text = GetString("ExportDocuments.ExportBlogComments");

        // Javascript
        string script = "var parent = document.getElementById('" + chkDocuments.ClientID + "'); \n" +
                        "var childIDs = ['" + chkDocumentsHistory.ClientID + "', '" + chkRelationships.ClientID + "', '" + chkBlogComments.ClientID + "', '" + chkEventAttendees.ClientID + "', '" + chkACLs.ClientID + "']; \n" +
                        "InitCheckboxes(); \n";

        ltlScript.Text = ScriptHelper.GetScript(script);

        this.chkDocuments.Attributes.Add("onclick", "CheckChange();");
    }


    /// <summary>
    /// Gets settings.
    /// </summary>
    public override void SaveSettings()
    {
        ProcessObjectEnum documents = (this.chkDocuments.Checked ? ProcessObjectEnum.All : ProcessObjectEnum.None);

        Settings.SetObjectsProcessType(documents, TreeObjectType.DOCUMENT, true);

        Settings.SetSettings(ImportExportHelper.SETTINGS_DOC_HISTORY, this.chkDocumentsHistory.Checked);

        Settings.SetSettings(ImportExportHelper.SETTINGS_DOC_RELATIONSHIPS, this.chkRelationships.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_DOC_ACLS, this.chkACLs.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_EVENT_ATTENDEES, this.chkEventAttendees.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_BLOG_COMMENTS, this.chkBlogComments.Checked);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkDocuments.Checked = (Settings.GetObjectsProcessType(TreeObjectType.DOCUMENT, true) != ProcessObjectEnum.None);
        this.chkDocumentsHistory.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_DOC_HISTORY), false);

        this.chkRelationships.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_DOC_RELATIONSHIPS), false);
        this.chkACLs.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_DOC_ACLS), false);
        this.chkEventAttendees.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_EVENT_ATTENDEES), false);
        this.chkBlogComments.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_BLOG_COMMENTS), false);
    }
}
