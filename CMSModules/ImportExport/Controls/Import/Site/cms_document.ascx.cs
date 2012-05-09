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
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.PortalEngine;

public partial class CMSModules_ImportExport_Controls_Import_Site_cms_document : ImportExportControl
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkDocuments.Text = GetString("ImportDocuments.ImportDocuments");
        this.chkDocumentsHistory.Text = GetString("ImportDocuments.ImportDocumentsHistory") + "<br />";
        this.chkRelationships.Text = GetString("ImportDocuments.ImportRelationships") + "<br />";
        this.chkACLs.Text = GetString("ImportDocuments.ImportACLs") + "<br />";
        this.chkEventAttendees.Text = GetString("ImportDocuments.ImportEventAttendees") + "<br />";
        this.chkBlogComments.Text = GetString("ImportDocuments.ImportBlogComments") + "<br />";
        this.chkUserPersonalizations.Text = GetString("CMSImport_UserPersonalizations.ImportUserPersonalizations") + "<br />";

        // Javascript
        string script = "var parent = document.getElementById('" + chkDocuments.ClientID + "'); \n" +
                        "var childIDs = ['" + chkDocumentsHistory.ClientID + "', '" + chkRelationships.ClientID + "', '" + chkBlogComments.ClientID + "', '" + chkEventAttendees.ClientID + "', '" + chkACLs.ClientID + "', '" + chkUserPersonalizations.ClientID + "']; \n" +
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
        Settings.SetSettings(ImportExportHelper.SETTINGS_USER_PERSONALIZATIONS, this.chkUserPersonalizations.Checked);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        this.chkDocuments.Checked = (Settings.GetObjectsProcessType(TreeObjectType.DOCUMENT, true) != ProcessObjectEnum.None);
        this.chkDocumentsHistory.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_DOC_HISTORY), true);

        this.chkRelationships.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_DOC_RELATIONSHIPS), true);
        this.chkACLs.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_DOC_ACLS), true);
        this.chkEventAttendees.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_EVENT_ATTENDEES), true);
        this.chkBlogComments.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_BLOG_COMMENTS), true);
        this.chkUserPersonalizations.Checked = ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_USER_PERSONALIZATIONS), !ExistingSite);

        // Visibility
        SiteImportSettings settings = (SiteImportSettings)this.Settings;

        this.chkACLs.Visible = settings.IsIncluded(TreeObjectType.ACL, false);
        this.chkDocumentsHistory.Visible = settings.IsIncluded(WorkflowObjectType.VERSIONHISTORY, false);
        this.chkRelationships.Visible = settings.IsIncluded(TreeObjectType.RELATIONSHIP, false);
        //this.chkUserPersonalizations.Visible = settings.IsIncluded(PortalObjectType.PERSONALIZATION, false);
        this.pnlDocumentData.Visible = this.chkDocumentsHistory.Visible || this.chkACLs.Visible || this.chkRelationships.Visible || this.chkUserPersonalizations.Visible;

        this.chkBlogComments.Visible = settings.IsIncluded(PredefinedObjectType.BLOGCOMMENT, false);
        this.chkEventAttendees.Visible = settings.IsIncluded(PredefinedObjectType.EVENTATTENDEE, false);
        this.pnlModules.Visible = this.chkBlogComments.Visible || this.chkEventAttendees.Visible;
    }
}
