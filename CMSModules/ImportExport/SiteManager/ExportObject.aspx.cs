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
using System.Text.RegularExpressions;

public partial class CMSModules_ImportExport_SiteManager_ExportObject : CMSModalPage
{
    #region "Variables"

    protected string codeName = null;
    protected string exportObjectDisplayName = null;

    protected string targetFolder = null;
    protected string targetUrl = null;

    protected bool allowDependent = false;
    protected bool siteObject = false;
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
            return "ExportObject_" + ProcessGUID + "_Settings";
        }
    }


    /// <summary>
    /// Export settings stored in viewstate.
    /// </summary>
    public SiteExportSettings ExportSettings
    {
        get
        {
            SiteExportSettings settings = (SiteExportSettings)PersistentStorageHelper.GetValue(PersistentSettingsKey);
            if (settings == null)
            {
                throw new Exception("[ExportObject.ExportSettings]: Export settings has been lost.");
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
        backup = QueryHelper.GetBoolean("backup", false);

        // Check permissions
        if (backup)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.globalpermissions", "BackupObjects", CMSContext.CurrentSiteName))
            {
                RedirectToCMSDeskAccessDenied("cms.globalpermissions", "BackupObjects");
            }
        }
        else if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.globalpermissions", "ExportObjects", CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskAccessDenied("cms.globalpermissions", "ExportObjects");
        }

        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);

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

            if (backup)
            {
                SetTitle("CMSModules/CMS_ImportExport/backupobject.png", GetString("BackupObject.Title"), null, null);
            }
            else
            {
                SetTitle("CMSModules/CMS_ImportExport/exportobject.png", GetString("ExportObject.Title"), null, null);
            }

            // Display BETA warning
            lblBeta.Visible = CMSContext.IsBetaVersion();
            lblBeta.Text = string.Format(GetString("export.BETAwarning"), CMSContext.GetFriendlySystemVersion(false));

            // Get data from parameters
            objectId = ValidationHelper.GetInteger(Request.QueryString["objectId"], 0);
            objectType = ValidationHelper.GetString(Request.QueryString["objectType"], "");

            // Get the object
            infoObj = CMSObjectHelper.GetReadOnlyObject(objectType);

            if (infoObj == null)
            {
                plcExportDetails.Visible = false;
                lblIntro.Text = GetString("ExportObject.ObjectTypeNotFound");
                lblIntro.CssClass = "ErrorLabel";
                return;
            }

            // Get exported object
            exportObj = infoObj.GetObject(objectId);
            if (exportObj == null)
            {
                plcExportDetails.Visible = false;
                lblIntro.Text = GetString("ExportObject.ObjectNotFound");
                lblIntro.CssClass = "ErrorLabel";
                btnOk.Visible = false;
                return;
            }

            // Store display name
            exportObjectDisplayName = HTMLHelper.HTMLEncode(exportObj.ObjectDisplayName);

            if (backup)
            {
                lblIntro.Text = string.Format(GetString("BackupObject.Intro"), ResHelper.LocalizeString(exportObjectDisplayName));
            }
            else
            {
                lblIntro.Text = string.Format(GetString("ExportObject.Intro"), ResHelper.LocalizeString(exportObjectDisplayName));
            }

            btnOk.Click += btnOk_Click;

            if (!RequestHelper.IsPostBack())
            {
                lblIntro.Visible = true;
                lblFileName.Visible = true;
                txtFileName.Text = GetExportFileName(exportObj, backup);
                if (backup)
                {
                    btnOk.Text = GetString("General.backup");
                }
                else
                {
                    btnOk.Text = GetString("General.export");
                }
                btnCancel.Text = GetString("General.Close");
            }

            string path = null;
            if (backup)
            {
                path = ImportExportHelper.GetObjectBackupFolder(exportObj);
                targetFolder = Server.MapPath(path);

                targetUrl = ResolveUrl(path) + "/" + txtFileName.Text;
            }
            else
            {
                targetFolder = ImportExportHelper.GetSiteUtilsFolder() + "Export";
                path = ImportExportHelper.GetSiteUtilsFolderRelativePath();
                if (path != null)
                {
                    string externalUrl = null;
                    if (StorageHelper.IsExternalStorage)
                    {
                        externalUrl = File.GetFileUrl(path + "Export/" + txtFileName.Text, CMSContext.CurrentSiteName);
                    }

                    if (string.IsNullOrEmpty(externalUrl))
                    {
                        targetUrl = ResolveUrl(path) + "Export/" + txtFileName.Text;
                    }
                    else
                    {
                        targetUrl = externalUrl;
                    }

                }
                else
                {
                    targetUrl = null;
                }
            }
        }
    }


    private void DisplayError(Exception ex)
    {
        pnlProgress.Visible = false;
        btnOk.Enabled = false;
        pnlDetails.Visible = false;
        pnlContent.Visible = true;

        string displayName = null;
        if (exportObj != null)
        {
            displayName = exportObj.ObjectDisplayName;
        }
        lblResult.Text = string.Format(GetString("ExportObject.Error"), ResHelper.LocalizeString(HTMLHelper.HTMLEncode(displayName)), ex.Message);
        lblResult.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
        lblResult.CssClass = "ErrorLabel";

        EventLogProvider ev = new EventLogProvider();
        ev.LogEvent("Export", "ExportObject", ex);
    }


    void btnOk_Click(object sender, EventArgs e)
    {
        // Init the Mimetype helper (required for the export)
        MimeTypeHelper.LoadMimeTypes();

        // Prepare the settings
        ExportSettings = new SiteExportSettings(CMSContext.CurrentUser);

        ExportSettings.WebsitePath = Server.MapPath("~/");
        ExportSettings.TargetPath = targetFolder;

        // Initialiye
        ImportExportHelper.InitSingleObjectExportSettings(ExportSettings, exportObj);

        string result = ImportExportHelper.ValidateExportFileName(ExportSettings, txtFileName.Text);

        // Filename is valid
        if (!string.IsNullOrEmpty(result))
        {
            lblError.Text = result;
        }
        else
        {
            txtFileName.Text = txtFileName.Text.Trim();

            // Add extension
            if (Path.GetExtension(txtFileName.Text).ToLower() != ".zip")
            {
                txtFileName.Text = txtFileName.Text.TrimEnd('.') + ".zip";
            }

            // Set the filename
            lblProgress.Text = string.Format(GetString("ExportObject.ExportProgress"), ResHelper.LocalizeString(exportObjectDisplayName));
            ExportSettings.TargetFileName = txtFileName.Text;

            pnlContent.Visible = false;
            pnlDetails.Visible = false;
            btnOk.Enabled = false;
            pnlProgress.Visible = true;

            try
            {
                // Export the data
                ltlScript.Text = ScriptHelper.GetScript("StartTimer();");
                ucAsyncControl.RunAsync(ExportSingleObject, WindowsIdentity.GetCurrent());
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
    }


    // Export object
    private void ExportSingleObject(object parameter)
    {
        // Export object
        ExportProvider.ExportObjectsData(ExportSettings);
    }


    void ucAsyncControl_OnError(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript("StopTimer();");
        Exception ex = ((CMSAdminControls_AsyncControl)sender).Worker.LastException;

        DisplayError(ex);
    }


    void ucAsyncControl_OnFinished(object sender, EventArgs e)
    {
        ltlScript.Text += ScriptHelper.GetScript("StopTimer();");
        pnlProgress.Visible = false;
        btnOk.Visible = false;
        pnlContent.Visible = true;
        lblResult.CssClass = "ContentLabel";
        string path = targetUrl;

        // Display full path
        if (path == null || StorageHelper.IsExternalStorage)
        {
            path = DirectoryHelper.CombinePath(targetFolder, txtFileName.Text);
        }

        if (!backup)
        {
            lblResult.Text = string.Format(GetString("ExportObject.lblResult"), ResHelper.LocalizeString(exportObjectDisplayName), path);
            if (targetUrl != null)
            {
                lnkDownload.NavigateUrl = targetUrl;
                lnkDownload.Text = GetString("ExportObject.Download");
                lnkDownload.Visible = true;
            }
        }
        else
        {
            lblResult.Text = string.Format(GetString("ExportObject.BackupFinished"), ResHelper.LocalizeString(exportObjectDisplayName), path);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        lblResult.Visible = (lblResult.Text != "");
        base.OnPreRender(e);
    }


    /// <summary>
    /// Ensure user friendly file name
    /// </summary>
    /// <param name="infoObj">Object to be exported</param>
    /// <param name="backup">Indicates if export is treated as backup</param>
    private string GetExportFileName(GeneralizedInfo infoObj, bool backup)
    {
        string fileName = null;
        // Get file name accrding to accesible object properties
        if (infoObj.TypeInfo.CodeNameColumn != TypeInfo.COLUMN_NAME_UNKNOWN)
        {
            fileName = infoObj.ObjectCodeName;
        }
        else if (infoObj.TypeInfo.DisplayNameColumn != TypeInfo.COLUMN_NAME_UNKNOWN)
        {
            fileName = ValidationHelper.GetCodeName(infoObj.ObjectDisplayName);
        }
        else
        {
            fileName = ValidationHelper.GetCodeName(infoObj.ObjectGUID.ToString());
        }

        fileName = fileName.Replace(".", "_") + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmm") + ".zip";
        fileName = ValidationHelper.GetSafeFileName(fileName);

        // Backup use short file name, in other cases use long file name with object type
        if (!backup)
        {
            fileName = infoObj.TypeInfo.OriginalObjectType.Replace(".", "_") + "_" + fileName;
        }

        return fileName;
    }
}
