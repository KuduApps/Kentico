using System;
using System.Security.Principal;
using System.Collections;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.CMSImportExport;
using CMS.CMSHelper;
using CMS.EventLog;
using CMS.UIControls;
using CMS.PortalEngine;
using CMS.IO;

public partial class CMSModules_ImportExport_SiteManager_RestoreObject : CMSModalPage
{
    #region "Variables"

    protected string codeName = null;
    protected string exportObjectDisplayName = null;

    protected string targetFolder = null;

    protected bool allowDependent = false;
    protected bool siteObject = false;
    protected int siteId = 0;
    protected int objectId = 0;
    protected string objectType = string.Empty;

    protected GeneralizedInfo infoObj = null;
    protected GeneralizedInfo exportObj = null;

    protected bool backup = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Export process GUID.
    /// </summary>
    public Guid ProcessGUID
    {
        get
        {
            if (ViewState["ProcessGUID"] == null)
            {
                ViewState["ProcessGUID"] = Guid.NewGuid();
            }

            return ValidationHelper.GetGuid(ViewState["ProcessGUID"], Guid.Empty);
        }
    }


    /// <summary>
    /// Persistent settings key.
    /// </summary>
    public string PersistentSettingsKey
    {
        get
        {
            return "RestoreObject_" + ProcessGUID + "_Settings";
        }
    }


    /// <summary>
    /// Import settings stored in viewstate.
    /// </summary>
    public SiteImportSettings ImportSettings
    {
        get
        {
            SiteImportSettings settings = (SiteImportSettings)PersistentStorageHelper.GetValue(PersistentSettingsKey);
            if (settings == null)
            {
                throw new Exception("[ImportObject.ImportSettings]: Import settings has been lost.");
            }
            return settings;
        }
        set
        {
            PersistentStorageHelper.SetValue(PersistentSettingsKey, value);
        }
    }

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (!DebugHelper.DebugImportExport)
        {
            DisableDebugging();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.globalpermissions", "RestoreObjects", CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskAccessDenied("cms.globalpermissions", "RestoreObjects");
        }

        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);

        imgDelete.ImageUrl = GetImageUrl("Design/Controls/EditModeButtons/delete.png");

        // Async control events binding
        ucAsyncControl.OnFinished += ucAsyncControl_OnFinished;
        ucAsyncControl.OnError += ucAsyncControl_OnError;

        if (!IsCallback)
        {
            try
            {
                // Delete temporary files
                ExportProvider.DeleteTemporaryFiles();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }

            SetTitle("CMSModules/CMS_ImportExport/restoreobject.png", GetString("RestoreObject.Title"), null, null);

            // Get data from parameters
            siteId = ValidationHelper.GetInteger(Request.QueryString["siteId"], 0);
            objectId = ValidationHelper.GetInteger(Request.QueryString["objectId"], 0);
            objectType = ValidationHelper.GetString(Request.QueryString["objectType"], "");

            // Get the object
            infoObj = CMSObjectHelper.GetReadOnlyObject(objectType);
            if (infoObj == null)
            {
                lblIntro.Text = GetString("ExportObject.ObjectTypeNotFound");
                lblIntro.CssClass = "ErrorLabel";
                return;
            }

            // Get exported object
            exportObj = infoObj.GetObject(objectId);
            if (exportObj == null)
            {
                lblIntro.Text = GetString("ExportObject.ObjectNotFound");
                lblIntro.CssClass = "ErrorLabel";
                btnOk.Visible = false;
                return;
            }

            // Store display name
            exportObjectDisplayName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(exportObj.ObjectDisplayName));
            codeName = exportObj.ObjectCodeName;

            lblIntro.Text = string.Format(GetString("RestoreObject.Intro"), exportObjectDisplayName);

            targetFolder = ImportExportHelper.GetObjectBackupFolder(exportObj);

            btnOk.Click += btnOk_Click;

            if (!RequestHelper.IsPostBack())
            {
                lblIntro.Visible = true;
                btnOk.Text = GetString("General.Restore");
                btnCancel.Text = GetString("General.Close");

                // Load the available backups
                if (lstImports.Items.Count == 0)
                {
                    RefreshPackageList();
                }
            }
        }
    }


    private void RefreshPackageList()
    {
        lstImports.Items.Clear();

        ArrayList files = null;

        // Get import packages
        try
        {
            files = ImportProvider.GetImportFilesList(targetFolder);
        }
        catch
        {
            // Show error and log exception
            lblError.Text = GetString("importconfiguration.securityerror").Replace("{0}", ImportExportHelper.EXPORT_BACKUP_PATH);
        }

        if ((files != null) && (files.Count != 0))
        {
            lstImports.Enabled = true;
            lstImports.DataSource = files;
            lstImports.DataBind();

            lstImports.SelectedIndex = 0;
        }
        else
        {
            lstImports.Enabled = false;
        }

        // Select first item
        if (lstImports.Items.Count > 0)
        {
            lstImports.SelectedIndex = 0;
            btnDelete.OnClientClick = "if (!confirm('" + GetString("importconfiguration.deleteconf") + "')) { return false;}";
        }
        else
        {
            btnDelete.OnClientClick = "";
        }
    }


    /// <summary>
    /// Gets the path to the selected file
    /// </summary>
    private string GetSelectedFilePath()
    {
        if (String.IsNullOrEmpty(lstImports.SelectedValue))
        {
            return null;
        }

        return Server.MapPath(targetFolder + "/" + lstImports.SelectedValue);
    }


    void btnOk_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(lstImports.SelectedValue))
        {
            // Init the Mimetype helper (required for the export)
            MimeTypeHelper.LoadMimeTypes();

            siteObject = (exportObj.ObjectSiteID > 0);

            // Prepare the settings
            ImportSettings = new SiteImportSettings(CMSContext.CurrentUser);

            ImportSettings.WebsitePath = Server.MapPath("~/");
            ImportSettings.SourceFilePath = GetSelectedFilePath();

            if (siteObject)
            {
                ImportSettings.SiteId = exportObj.ObjectSiteID;
                ImportSettings.ExistingSite = true;
            }

            // Set the filename
            lblProgress.Text = string.Format(GetString("RestoreObject.RestoreProgress"), exportObjectDisplayName);

            pnlDetails.Visible = false;
            btnOk.Enabled = false;
            pnlProgress.Visible = true;

            try
            {
                // Export the data
                ltlScript.Text = ScriptHelper.GetScript("StartTimer();");
                ucAsyncControl.RunAsync(RestoreSingleObject, WindowsIdentity.GetCurrent());
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
    }


    private void DisplayError(Exception ex)
    {
        pnlProgress.Visible = false;
        btnOk.Enabled = false;
        pnlDetails.Visible = false;

        string displayName = null;
        if (exportObj != null)
        {
            displayName = exportObj.ObjectDisplayName;
        }
        lblResult.Text = string.Format(GetString("RestoreObject.Error"), HTMLHelper.HTMLEncode(displayName), ex.Message);
        lblResult.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
        lblResult.CssClass = "ErrorLabel";

        // Log to the event log
        EventLogProvider ev = new EventLogProvider();
        ev.LogEvent("Export", "RestoreObject", ex);
    }



    /// <summary>
    /// Restores the single object.
    /// </summary>
    private void RestoreSingleObject(object parameter)
    {
        // Restore the entire content of the package
        ImportProvider.CreateTemporaryFiles(ImportSettings);

        ImportSettings.LoadDefaultSelection();

        ImportProvider.ImportObjectsData(ImportSettings);
    }


    void ucAsyncControl_OnError(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript("StopTimer();");
        Exception ex = ((CMSAdminControls_AsyncControl)sender).Worker.LastException;

        DisplayError(ex);
    }


    void ucAsyncControl_OnFinished(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript(@"
StopTimer();
if ((wopener != null) && wopener.UG_Reload)
{
    wopener.UG_Reload();
}
");

        pnlProgress.Visible = false;
        btnOk.Visible = false;
        pnlContent.Visible = true;

        lblResult.CssClass = "ContentLabel";
        lblResult.Text = string.Format(GetString("RestoreObject.RestoreFinished"), exportObjectDisplayName);
    }


    protected override void OnPreRender(EventArgs e)
    {
        lblResult.Visible = (lblResult.Text != "");
        base.OnPreRender(e);
    }


    /// <summary>
    /// Refresh button click handler.
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(lstImports.SelectedValue))
        {
            try
            {
                string path = GetSelectedFilePath();
                File.Delete(path);
            }
            catch (Exception ex)
            {
                ScriptHelper.RegisterStartupScript(Page, typeof(string), "ErrorMessage", ScriptHelper.GetAlertScript(ex.Message));
            }
            finally
            {
                RefreshPackageList();
            }
        }
    }
}
