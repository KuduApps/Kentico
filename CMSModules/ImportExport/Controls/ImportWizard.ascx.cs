using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

using CMS.CMSImportExport;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.IO;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.UIControls;
using CMS.VirtualPathHelper;
using CMS.SettingsProvider;

public partial class CMSModules_ImportExport_Controls_ImportWizard : CMSUserControl, ICallbackEventHandler
{
    #region "Variables"

    private static readonly Hashtable mManagers = new Hashtable();
    private string mFinishUrl = "~/CMSSiteManager/Sites/site_list.aspx";

    #endregion


    #region "Properties"

    /// <summary>
    /// Redirection URL after finish button click.
    /// </summary>
    public string FinishUrl
    {
        get
        {
            return mFinishUrl;
        }
        set
        {
            mFinishUrl = value;
        }
    }


    /// <summary>
    /// Import manager.
    /// </summary>
    public ImportManager ImportManager
    {
        get
        {
            string key = "imManagers_" + ProcessGUID;
            if (mManagers[key] == null)
            {
                // Detect restart of the application
                if (ApplicationInstanceGUID != CMSContext.ApplicationInstanceGUID)
                {
                    LogStatusEnum progressLog = ImportSettings.GetProgressState();
                    if (progressLog == LogStatusEnum.Info)
                    {
                        ImportSettings.LogProgressState(LogStatusEnum.UnexpectedFinish, GetString("SiteImport.ApplicationRestarted"));
                    }
                }

                ImportManager im = new ImportManager(ImportSettings);
                mManagers[key] = im;
            }
            return (ImportManager)mManagers[key];
        }
        set
        {
            string key = "imManagers_" + ProcessGUID;
            mManagers[key] = value;
        }
    }


    /// <summary>
    /// Wizard height.
    /// </summary>
    public int PanelHeight
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["PanelHeight"], 400);
        }
        set
        {
            ViewState["PanelHeight"] = value;
        }
    }


    /// <summary>
    /// Application instance GUID.
    /// </summary>
    public Guid ApplicationInstanceGUID
    {
        get
        {
            if (ViewState["ApplicationInstanceGUID"] == null)
            {
                ViewState["ApplicationInstanceGUID"] = CMSContext.ApplicationInstanceGUID;
            }

            return ValidationHelper.GetGuid(ViewState["ApplicationInstanceGUID"], Guid.Empty);
        }
    }


    /// <summary>
    /// Import process GUID.
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
            return "Import_" + ProcessGUID + "_Settings";
        }
    }


    /// <summary>
    /// Import settings stored in viewstate.
    /// </summary>
    public SiteImportSettings ImportSettings
    {
        get
        {
            SiteImportSettings settings = (SiteImportSettings)SiteExportSettings.GetFromPersistentStorage(PersistentSettingsKey);
            if (settings == null)
            {
                if (wzdImport.ActiveStepIndex == 0)
                {
                    settings = GetNewSettings();
                    PersistentStorageHelper.SetValue(PersistentSettingsKey, settings);
                }
                else
                {
                    throw new Exception("[ImportWizard.ImportSettings]: Import settings has been lost.");
                }
            }
            return settings;
        }
        set
        {
            PersistentStorageHelper.SetValue(PersistentSettingsKey, value);
        }
    }

    #endregion


    #region "Finish step wizard buttons"

    /// <summary>
    /// Finish button.
    /// </summary>
    public LocalizedButton FinishButton
    {
        get
        {
            return wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepFinishButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Previous button.
    /// </summary>
    public LocalizedButton PreviousButton
    {
        get
        {
            return wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepPreviousButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Next button.
    /// </summary>
    public LocalizedButton NextButton
    {
        get
        {
            return wzdImport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Cancel button.
    /// </summary>
    public LocalizedButton CancelButton
    {
        get
        {
            return wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepCancelButton") as LocalizedButton;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);

        // Handle Import settings
        if (!Page.IsCallback && !RequestHelper.IsPostBack())
        {
            // Initialize virtual path provider
            VirtualPathHelper.Init(Page);

            // Initialize import settings
            ImportSettings = GetNewSettings();
        }

        if (!Page.IsCallback)
        {
            if (!CMS.SettingsProvider.SettingsKeyProvider.UsingVirtualPathProvider)
            {
                lblWarning.Visible = true;
                lblWarning.Text = GetString("ImportSite.VirtualPathProviderNotRunning");
            }

            ctrlAsync.OnFinished += ctrlAsync_OnFinished;
            ctrlAsync.OnError += ctrlAsync_OnError;

            bool notTempPermissions = false;

            if (wzdImport.ActiveStepIndex < 3)
            {
                stpConfigImport.Settings = ImportSettings;
                stpSiteDetails.Settings = ImportSettings;
                stpImport.Settings = ImportSettings;

                // Ensure directory
                DirectoryHelper.EnsureDiskPath(ImportSettings.TemporaryFilesPath + "\\temp.file", ImportSettings.WebsitePath);
                // Check permissions
                notTempPermissions = !DirectoryHelper.CheckPermissions(ImportSettings.TemporaryFilesPath, true, true, false, false);
            }

            if (notTempPermissions)
            {
                pnlWrapper.Visible = false;
                lblError.Text = string.Format(GetString("ImportSite.ErrorPermissions"), ImportSettings.TemporaryFilesPath, WindowsIdentity.GetCurrent().Name);
                pnlPermissions.Visible = true;
                lnkPermissions.Target = "_blank";
                lnkPermissions.Text = GetString("Install.ErrorPermissions");
                lnkPermissions.NavigateUrl = ResolveUrl("~/CMSMessages/ConfigurePermissions.aspx");
            }
            else
            {
                if (!RequestHelper.IsPostBack())
                {
                    // Delete temporary files
                    try
                    {
                        // Delete only folder structure if there is not special folder
                        bool onlyFolderStructure = !Directory.Exists(DirectoryHelper.CombinePath(ImportSettings.TemporaryFilesPath, ImportExportHelper.FILES_FOLDER));
                        ImportProvider.DeleteTemporaryFiles(ImportSettings, onlyFolderStructure);
                    }
                    catch (Exception ex)
                    {
                        pnlWrapper.Visible = false;
                        lblError.Text = GetString("ImportSite.ErrorDeletionTemporaryFiles") + ex.Message;
                        return;
                    }
                }

                // Javascript functions
                string script =
                    "var imMessageText = '';\n" +
                    "var imErrorText = '';\n" +
                    "var imWarningText = '';\n" +
                    "var imMachineName = '" + SqlHelperClass.MachineName.ToLower() + "';\n" +
                    "var getBusy = false;\n" +
                    "function GetImportState(cancel)\n" +
                    "{ if(window.Activity){window.Activity();} if (getBusy && !cancel) return; getBusy = true; setTimeout(\"getBusy = false;\", 2000); var argument = cancel + ';' + imMessageText.length + ';' + imErrorText.length + ';' + imWarningText.length + ';' + imMachineName; return " + Page.ClientScript.GetCallbackEventReference(this, "argument", "SetImportStateMssg", "argument", true) + " }\n";

                script +=
                    "function SetImportStateMssg(rValue, context)\n" +
                    "{\n" +
                    "   getBusy = false;\n" +
                    "   if(rValue != '')\n" +
                    "   {\n" +
                    "       var args = context.split(';');\n" +
                    "       var values = rValue.split('" + SiteExportSettings.SEPARATOR + "');\n" +
                    "       var messageElement = document.getElementById('" + lblProgress.ClientID + "');\n" +
                    "       var errorElement = document.getElementById('" + lblError.ClientID + "');\n" +
                    "       var warningElement = document.getElementById('" + lblWarning.ClientID + "');\n" +
                    "       var messageText = imMessageText;\n" +
                    "       messageText = values[1] + messageText.substring(messageText.length - args[1]);\n" +
                    "       if(messageText.length > imMessageText.length){ imMessageText = messageElement.innerHTML = messageText; }\n" +
                    "       var errorText = imErrorText;\n" +
                    "       errorText = values[2] + errorText.substring(errorText.length - args[2]);\n" +
                    "       if(errorText.length > imErrorText.length){ imErrorText = errorElement.innerHTML = errorText; }\n" +
                    "       var warningText = imWarningText;\n" +
                    "       warningText = values[3] + warningText.substring(warningText.length - args[3]);\n" +
                    "       if(warningText.length > imWarningText.length){ imWarningText = warningElement.innerHTML = warningText; }\n" +
                    "       if((values=='') || (values[0]=='F'))\n" +
                    "       {\n" +
                    "           StopImportStateTimer();\n" +
                    "           BTN_Disable('" + CancelButton.ClientID + "');\n" +
                    "           BTN_Enable('" + FinishButton.ClientID + "');\n" +
                    "       }\n" +
                    "   }\n" +
                    "}\n";

                // Register the script to perform get flags for showing buttons retrieval callback
                ScriptHelper.RegisterClientScriptBlock(this, GetType(), "GetSetImportState", ScriptHelper.GetScript(script));

                // Add cancel button attribute
                CancelButton.Attributes.Add("onclick", "BTN_Disable('" + CancelButton.ClientID + "');" + "return CancelImport();");

                wzdImport.NextButtonClick += wzdImport_NextButtonClick;
                wzdImport.PreviousButtonClick += wzdImport_PreviousButtonClick;
                wzdImport.FinishButtonClick += wzdImport_FinishButtonClick;

                if (!RequestHelper.IsPostBack())
                {
                    stpConfigImport.InitControl();
                }
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!Page.IsCallback)
        {
            // Initilaize header
            InitializeHeader();

            // Button click script
            const string afterScript = "var imClicked = false; \n" +
                                       "function NextStepAction() \n" +
                                       "{ \n" +
                                       "   if(!imClicked) \n" +
                                       "   { \n" +
                                       "     imClicked = true; \n" +
                                       "     return true; \n" +
                                       "   } \n" +
                                       "   return false; \n" +
                                       "} \n";

            ltlScriptAfter.Text += ScriptHelper.GetScript(afterScript);

            // Ensure default button
            EnsureDefaultButton();
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        // Save the settings
        if (wzdImport.ActiveStep.StepType != WizardStepType.Finish)
        {
            ImportSettings.SavePersistent();
        }
    }

    #endregion


    #region "Button handling"

    protected void wzdImport_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        URLHelper.Redirect(FinishUrl);
    }


    protected void wzdImport_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        wzdImport.ActiveStepIndex = (wzdImport.ActiveStepIndex == 1) ? 0 : e.NextStepIndex;
    }


    protected void wzdImport_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.CurrentStepIndex)
        {
            case 0:
                // Apply settings
                if (!stpConfigImport.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }

                // Update settings
                ImportSettings = stpConfigImport.Settings;

                ltlScriptAfter.Text = ScriptHelper.GetScript(
                    "var actDiv = document.getElementById('actDiv'); \n" +
                    "if (actDiv != null) { actDiv.style.display='block'; } \n" +
                    "var buttonsDiv = document.getElementById('buttonsDiv'); if (buttonsDiv != null) { buttonsDiv.disabled=true; } \n" +
                    "BTN_Disable('" + NextButton.ClientID + "'); \n" +
                    "StartUnzipTimer();"
                );

                // Create temporary files asynchronously
                ctrlAsync.RunAsync(CreateTemporaryFiles, WindowsIdentity.GetCurrent());

                e.Cancel = true;
                break;

            case 1:
                // Apply settings
                if (!stpSiteDetails.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }

                // Update settings
                ImportSettings = stpSiteDetails.Settings;
                //stpImport.SelectedNodeValue = CMSObjectHelper.GROUP_OBJECTS;
                stpImport.ReloadData(true);

                wzdImport.ActiveStepIndex++;
                break;

            case 2:
                // Apply settings
                if (!stpImport.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }

                // Check licences
                string error = ImportExportControl.CheckLicenses(ImportSettings);
                if (!string.IsNullOrEmpty(error))
                {
                    lblError.Text = error;

                    e.Cancel = true;
                    return;
                }

                ImportSettings = stpImport.Settings;

                // Init the Mimetype helper (required for the Import)
                MimeTypeHelper.LoadMimeTypes();

                // Start asynchronnous Import
                ImportSettings.DefaultProcessObjectType = ProcessObjectEnum.Selected;
                if (ImportSettings.SiteIsIncluded)
                {
                    ImportSettings.EventLogSource = string.Format(ImportSettings.GetAPIString("ImportSite.EventLogSiteSource", "Import '{0}' site"), ResHelper.LocalizeString(ImportSettings.SiteDisplayName));
                }
                ImportManager.Settings = ImportSettings;

                AsyncWorker worker = new AsyncWorker();
                worker.OnFinished += worker_OnFinished;
                worker.OnError += worker_OnError;
                worker.RunAsync(ImportManager.Import, WindowsIdentity.GetCurrent());

                wzdImport.ActiveStepIndex++;
                break;
        }

        ReloadSteps();
    }

    #endregion


    #region "Async control events"

    protected void ctrlAsync_OnError(object sender, EventArgs e)
    {
        lblError.Visible = true;

        if (((CMSAdminControls_AsyncControl)sender).Worker.LastException != null)
        {
            // Show error message
            lblError.Text = ((CMSAdminControls_AsyncControl)sender).Worker.LastException.Message;
        }
        else
        {
            // Show general error message
            lblError.Text = String.Format(GetString("logon.erroroccurred"), GetString("general.seeeventlog"));
        }

        // Stop the timer
        ltlScript.Text += ScriptHelper.GetScript("StopUnzipTimer();");
    }


    protected void ctrlAsync_OnFinished(object sender, EventArgs e)
    {
        // Stop the timer
        const string script = "StopUnzipTimer();";

        // Decide if importing site
        if (ImportSettings.SiteIsIncluded)
        {
            // Single site import and no site exists
            if (ValidationHelper.GetBoolean(ImportSettings.GetInfo(ImportExportHelper.INFO_SINGLE_OBJECT), false) && (SiteInfoProvider.GetSitesCount() == 0))
            {
                lblError.Visible = true;
                lblError.Text = GetString("SiteImport.SingleSiteObjectNoSite");
                return;
            }

            // Init control
            stpSiteDetails.ReloadData();
        }
        else
        {
            wzdImport.ActiveStepIndex++;
            stpImport.ReloadData(true);
        }

        wzdImport.ActiveStepIndex++;

        ltlScriptAfter.Text += ScriptHelper.GetScript(script);
    }


    protected void worker_OnError(object sender, EventArgs e)
    {
    }


    protected void worker_OnFinished(object sender, EventArgs e)
    {
    }

    #endregion


    #region "Other methods"

    protected void InitializeHeader()
    {
        int stepIndex = wzdImport.ActiveStepIndex + 1;
        ucHeader.Title = string.Format(GetString("ImportPanel.Title"), stepIndex);

        switch (wzdImport.ActiveStepIndex)
        {
            case 0:
                ucHeader.Header = GetString("ImportPanel.ObjectsSettingsHeader");
                ucHeader.Description = GetString("ImportPanel.ObjectsSelectionSetting");
                break;

            case 1:
                ucHeader.Header = GetString("ImportPanel.ObjectsSiteDetailsHeader");
                if (ImportSettings.SiteIsIncluded && ValidationHelper.GetBoolean(ImportSettings.GetInfo(ImportExportHelper.INFO_SINGLE_OBJECT), false))
                {
                    ucHeader.Description = GetString("ImportPanel.SiteObjectImport");
                }
                else
                {
                    ucHeader.Description = GetString("ImportPanel.ObjectsSiteDetailsDescription");
                }
                break;

            case 2:
                ucHeader.Header = GetString("ImportPanel.ObjectsSelectionHeader");
                ucHeader.Description = GetString("ImportPanel.ObjectsSelectionDescription");
                break;

            case 3:
                ucHeader.Header = GetString("ImportPanel.ObjectsProgressHeader");
                ucHeader.Description = GetString("ImportPanel.ObjectsProgressDescription");
                break;
        }
    }


    // Create temporary files and preselect objects
    private void CreateTemporaryFiles(object parameter)
    {
        ImportProvider.CreateTemporaryFiles(ImportSettings);
        ImportSettings.LoadDefaultSelection();
    }


    protected void ReloadSteps()
    {
        if (wzdImport.ActiveStepIndex == 3)
        {
            ltlScript.Text = ScriptHelper.GetScript("StartImportStateTimer();");
        }
    }


    private void EnsureDefaultButton()
    {
        if (wzdImport.ActiveStep != null)
        {
            switch (wzdImport.ActiveStep.StepType)
            {
                case WizardStepType.Start:
                    Page.Form.DefaultButton =
                        wzdImport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton").
                            UniqueID;
                    break;

                case WizardStepType.Step:
                    Page.Form.DefaultButton =
                        wzdImport.FindControl("StepNavigationTemplateContainerID").FindControl("StepNextButton").
                            UniqueID;
                    break;

                case WizardStepType.Finish:
                    Page.Form.DefaultButton =
                        wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepFinishButton").
                            UniqueID;
                    break;
            }
        }
    }

    
    /// <summary>
    /// Creates new settings object for Import.
    /// </summary>
    private SiteImportSettings GetNewSettings()
    {
        SiteImportSettings result = new SiteImportSettings(CMSContext.CurrentUser);

        result.WebsitePath = Server.MapPath("~/");
        result.PersistentSettingsKey = PersistentSettingsKey;

        return result;
    }


    #endregion


    #region "Callback handling"

    /// <summary>
    /// Callback event handler.
    /// </summary>
    /// <param name="argument">Callback argument</param>
    public void RaiseCallbackEvent(string argument)
    {
        // Get arguments
        string[] args = argument.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        bool cancel = ValidationHelper.GetBoolean(args[0], false);
        int messageLength = 0;
        int errorLength = 0;
        int warningLength = 0;
        string machineName = null;

        if (args.Length == 5)
        {
            messageLength = ValidationHelper.GetInteger(args[1], 0);
            errorLength = ValidationHelper.GetInteger(args[2], 0);
            warningLength = ValidationHelper.GetInteger(args[3], 0);
            machineName = ValidationHelper.GetString(args[4], null);
        }

        // Check if on same machine
        if (machineName == SqlHelperClass.MachineName.ToLower())
        {
            try
            {
                // Cancel Import
                if (cancel)
                {
                    ImportManager.Settings.Cancel();
                }

                hdnState.Value = ImportManager.Settings.GetLimitedProgressLog(messageLength, errorLength, warningLength);
            }
            catch (Exception ex)
            {
                EventLogProvider ev = new EventLogProvider();
                ev.LogEvent("ImportWizard", "IMPORT", ex);

                hdnState.Value = ImportManager.Settings.GetLimitedProgressLog(messageLength, errorLength, warningLength);
            }
            finally
            {
                if (ImportManager.Settings.GetProgressState() != LogStatusEnum.Info)
                {
                    // Delete presistent data
                    PersistentStorageHelper.RemoveValue(PersistentSettingsKey);
                }
            }
        }
    }


    /// <summary>
    /// Callback result retrieving handler.
    /// </summary>
    public string GetCallbackResult()
    {
        return hdnState.Value;
    }

    #endregion
}
