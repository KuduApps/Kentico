using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

using CMS.CMSImportExport;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.EventLog;
using CMS.UIControls;
using CMS.VirtualPathHelper;
using CMS.SettingsProvider;
using CMS.IO;
using System.Web.Services;
using CMS.PortalControls;

public partial class CMSModules_ImportExport_Controls_ExportWizard : CMSUserControl, ICallbackEventHandler
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
    /// Export manager.
    /// </summary>
    public ExportManager ExportManager
    {
        get
        {
            string key = "exManagers_" + ProcessGUID;
            if (mManagers[key] == null)
            {
                // Restart of the application
                if (ApplicationInstanceGUID != CMSContext.ApplicationInstanceGUID)
                {
                    LogStatusEnum progressLog = ExportSettings.GetProgressState();
                    if (progressLog == LogStatusEnum.Info)
                    {
                        ExportSettings.LogProgressState(LogStatusEnum.UnexpectedFinish, GetString("SiteExport.Applicationrestarted"));
                    }
                }

                ExportManager em = new ExportManager(ExportSettings);
                mManagers[key] = em;
            }

            return (ExportManager)mManagers[key];
        }
        set
        {
            string key = "exManagers_" + ProcessGUID;
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
    /// Site ID.
    /// </summary>
    public int SiteId
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["SiteId"], 0);
        }
        set
        {
            ViewState["SiteId"] = value;
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
            return "Export_" + ProcessGUID + "_Settings";
        }
    }


    /// <summary>
    /// Export settings stored in viewstate.
    /// </summary>
    public SiteExportSettings ExportSettings
    {
        get
        {
            SiteExportSettings settings = (SiteExportSettings)SiteExportSettings.GetFromPersistentStorage(PersistentSettingsKey);
            if (settings == null)
            {
                if (wzdExport.ActiveStepIndex == 0)
                {
                    settings = GetNewSettings();
                }
                else
                {
                    throw new Exception("[ExportWizard.ExportSettings]: Export settings has been lost.");
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
            return wzdExport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepFinishButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Previous button.
    /// </summary>
    public LocalizedButton PreviousButton
    {
        get
        {
            return wzdExport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepPreviousButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Next button.
    /// </summary>
    public LocalizedButton NextButton
    {
        get
        {
            return wzdExport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Cancel button.
    /// </summary>
    public LocalizedButton CancelButton
    {
        get
        {
            return wzdExport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepCancelButton") as LocalizedButton;
        }
    }

    #endregion


    #region "Events handling"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(this.Page);

        // Handle export settings
        if (!Page.IsCallback && !RequestHelper.IsPostBack())
        {
            // Initialize virtual path provider
            VirtualPathHelper.Init(Page);

            ExportSettings = GetNewSettings();
        }

        if (!Page.IsCallback)
        {
            // Display BETA warning
            lblBeta.Visible = CMSContext.IsBetaVersion();
            lblBeta.Text = string.Format(GetString("export.BETAwarning"), CMSContext.GetFriendlySystemVersion(false));

            bool notTargetPermissions = false;
            bool notTempPermissions = false;

            ctrlAsync.OnFinished += ctrlAsync_OnFinished;
            ctrlAsync.OnError += ctrlAsync_OnError;

            // Init steps
            if (wzdExport.ActiveStepIndex < 2)
            {
                configExport.Settings = ExportSettings;
                if (!RequestHelper.IsPostBack())
                {
                    configExport.SiteId = SiteId;
                }

                pnlExport.Settings = ExportSettings;

                // Ensure directories and check permissions
                try
                {
                    DirectoryHelper.EnsureDiskPath(ExportSettings.TargetPath + "\\temp.file", ExportSettings.WebsitePath);
                    notTargetPermissions = !DirectoryHelper.CheckPermissions(ExportSettings.TargetPath, true, true, false, false);
                }
                catch (UnauthorizedAccessException)
                {
                    notTargetPermissions = true;
                }
                try
                {
                    DirectoryHelper.EnsureDiskPath(ExportSettings.TemporaryFilesPath + "\\temp.file", ExportSettings.WebsitePath);
                    notTempPermissions = !DirectoryHelper.CheckPermissions(ExportSettings.TemporaryFilesPath, true, true, false, false);
                }
                catch (UnauthorizedAccessException)
                {
                    notTempPermissions = true;
                }
            }

            if (notTargetPermissions || notTempPermissions)
            {
                string folder = (notTargetPermissions) ? ExportSettings.TargetPath : ExportSettings.TemporaryFilesPath;
                pnlWrapper.Visible = false;
                lblError.Text = string.Format(GetString("ExportSite.ErrorPermissions"), folder, System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                pnlPermissions.Visible = true;
                lnkPermissions.Target = "_blank";
                lnkPermissions.Text = GetString("Install.ErrorPermissions");
                lnkPermissions.NavigateUrl = ResolveUrl("~/CMSMessages/ConfigurePermissions.aspx");
            }
            else
            {
                // Try to delete temporary files from previous export
                if (!RequestHelper.IsPostBack())
                {
                    try
                    {
                        ExportProvider.DeleteTemporaryFiles(ExportSettings, false);
                    }
                    catch (Exception ex)
                    {
                        pnlWrapper.Visible = false;
                        lblError.Text = GetString("ImportSite.ErrorDeletionTemporaryFiles") + ex.Message;
                        return;
                    }
                }

                PortalHelper.EnsureScriptManager(this.Page).EnablePageMethods = true;

                // Javascript functions
                string script =
@"var exMessageText = '';
var exErrorText = '';
var exWarningText = '';
var exMachineName = '" + SqlHelperClass.MachineName.ToLower() + @"';
var getBusy = false;

function GetExportState(cancel) { 
    if (window.Activity) { 
        window.Activity(); 
    } 
    if (getBusy) return; 
    getBusy = true; 
    setTimeout('getBusy = false;', 2000);
    var argument = cancel + ';' + exMessageText.length + ';' + exErrorText.length + ';' + exWarningText.length + ';' + exMachineName; 
    " + Page.ClientScript.GetCallbackEventReference(this, "argument", "SetExportStateMssg", "argument", false) + @";
}

function SetExportStateMssg(rValue, context) {
    getBusy = false;
    if (rValue!='') {
        var args = context.split(';');
        var values = rValue.split('" + SiteExportSettings.SEPARATOR + @"');
        var messageElement = document.getElementById('" + lblProgress.ClientID + @"');
        var errorElement = document.getElementById('" + lblError.ClientID + @"');
        var warningElement = document.getElementById('" + lblWarning.ClientID + @"');
        var messageText = exMessageText;
        messageText = values[1] + messageText.substring(messageText.length - args[1]);
        if (messageText.length > exMessageText.length) { 
            exMessageText = messageElement.innerHTML = messageText; 
        }
        var errorText = exErrorText;
        errorText = values[2] + errorText.substring(errorText.length - args[2]);
        if (errorText.length > exErrorText.length) { 
            exErrorText = errorElement.innerHTML = errorText; 
        }
        var warningText = exWarningText;
        warningText = values[3] + warningText.substring(warningText.length - args[3]);
        if (warningText.length > exWarningText.length) { 
            exWarningText = warningElement.innerHTML = warningText; 
        }
        if ((values=='') || (values[0]=='F')) {
            StopExportStateTimer();
            BTN_Enable('" + FinishButton.ClientID + @"');
            try {
                BTN_Disable('" + CancelButton.ClientID + @"');
            }
            catch(err) {
            }
        }
    }
}";

                // Register the script to perform get flags for showing buttons retrieval callback
                ScriptHelper.RegisterClientScriptBlock(this, GetType(), "GetSetExportState", ScriptHelper.GetScript(script));

                // Add cancel button attribute
                CancelButton.Attributes.Add("onclick", "BTN_Disable('" + CancelButton.ClientID + "'); return CancelExport();");

                wzdExport.NextButtonClick += wzdExport_NextButtonClick;
                wzdExport.PreviousButtonClick += wzdExport_PreviousButtonClick;
                wzdExport.FinishButtonClick += wzdExport_FinishButtonClick;

                if (!RequestHelper.IsPostBack())
                {
                    configExport.InitControl();
                }
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Initilaize header
        InitializeHeader();

        // Button click script
        const string afterScript = "var exClicked = false; \n" +
                                   "function exNextStepAction() \n" +
                                   "{ \n" +
                                   "   if(!exClicked) \n" +
                                   "   { \n" +
                                   "     exClicked = true; \n" +
                                   "     return true; \n" +
                                   "   } \n" +
                                   "   return false; \n" +
                                   "} \n";

        ltlScriptAfter.Text += ScriptHelper.GetScript(afterScript);

        // Ensure default button
        EnsureDefaultButton();
    }


    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        // Save the settings
        if (wzdExport.ActiveStep.StepType != WizardStepType.Finish)
        {
            ExportSettings.SavePersistent();
        }
    }


    private void wzdExport_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        URLHelper.Redirect(FinishUrl);
    }


    private void wzdExport_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        wzdExport.ActiveStepIndex = e.NextStepIndex;
    }


    private void wzdExport_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.CurrentStepIndex)
        {
            case 0:
                // Apply settings
                if (!configExport.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }

                // Update settings
                ExportSettings = configExport.Settings;

                if (!configExport.ExportHistory)
                {
                    ltlScriptAfter.Text = ScriptHelper.GetScript(
                        "var actDiv = document.getElementById('actDiv'); \n" +
                        "if (actDiv != null) { actDiv.style.display='block'; } \n" +
                        "var buttonsDiv = document.getElementById('buttonsDiv'); if (buttonsDiv != null) { buttonsDiv.disabled=true; } \n" +
                        "BTN_Disable('" + NextButton.ClientID + "'); \n" +
                        "StartSelectionTimer();"
                    );

                    // Select objects asynchronously
                    ctrlAsync.RunAsync(SelectObjects, WindowsIdentity.GetCurrent());
                    e.Cancel = true;
                }
                else
                {
                    pnlExport.Settings = ExportSettings;
                    pnlExport.ReloadData();
                }
                break;

            case 1:
                // Apply settings
                if (!pnlExport.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }
                ExportSettings = pnlExport.Settings;

                // Delete temporary files
                try
                {
                    ExportProvider.DeleteTemporaryFiles(ExportSettings, true);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    e.Cancel = true;
                    return;
                }

                try
                {
                    // Save export history
                    ExportHistoryInfo history = new ExportHistoryInfo();
                    history.ExportDateTime = DateTime.Now;
                    history.ExportFileName = ExportSettings.TargetFileName;
                    history.ExportSettings = ExportSettings.GetXML();
                    history.ExportSiteID = ExportSettings.SiteId;
                    history.ExportUserID = CMSContext.CurrentUser.UserID;

                    ExportHistoryInfoProvider.SetExportHistoryInfo(history);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
                    e.Cancel = true;
                    return;
                }


                // Init the Mimetype helper (required for the export)
                MimeTypeHelper.LoadMimeTypes();

                if (ExportSettings.SiteId > 0)
                {
                    ExportSettings.EventLogSource = string.Format(ExportSettings.GetAPIString("ExportSite.EventLogSiteSource", "Export '{0}' site"), ResHelper.LocalizeString(ExportSettings.SiteInfo.DisplayName));
                }

                // Start asynchronnous export
                ExportManager.Settings = ExportSettings;

                AsyncWorker worker = new AsyncWorker();
                worker.OnFinished += worker_OnFinished;
                worker.OnError += worker_OnError;
                worker.RunAsync(ExportManager.Export, WindowsIdentity.GetCurrent());

                lblProgress.Text = GetString("SiteExport.PreparingExport");
                break;
        }

        ReloadSteps();
        wzdExport.ActiveStepIndex = e.NextStepIndex;
    }


    protected void ctrlAsync_OnError(object sender, EventArgs e)
    {
        lblError.Visible = true;
        lblError.Text = ((CMSAdminControls_AsyncControl)sender).Worker.LastException.Message;
        // Stop the timer
        ltlScript.Text += ScriptHelper.GetScript("StopSelectionTimer();");
    }


    protected void ctrlAsync_OnFinished(object sender, EventArgs e)
    {
        // Stop the timer
        const string script = "StopSelectionTimer();";

        pnlExport.Settings = ExportSettings;
        pnlExport.ReloadData();

        wzdExport.ActiveStepIndex++;

        ltlScriptAfter.Text += ScriptHelper.GetScript(script);
    }


    // Preselect objects
    private void SelectObjects(object parameter)
    {
        ExportSettings.LoadDefaultSelection();
    }


    protected void worker_OnError(object sender, EventArgs e)
    {
    }


    protected void worker_OnFinished(object sender, EventArgs e)
    {
    }

    #endregion


    #region "Private methods"

    private void InitializeHeader()
    {
        int stepIndex = wzdExport.ActiveStepIndex + 1;
        ucHeader.Title = string.Format(GetString("ExportPanel.Title"), stepIndex);

        switch (wzdExport.ActiveStepIndex)
        {
            case 0:
                ucHeader.Header = GetString("ExportPanel.ObjectsSettingsHeader");
                ucHeader.Description = GetString("ExportPanel.ObjectsSelectionSetting");
                break;

            case 1:
                ucHeader.Header = GetString("ExportPanel.ObjectsSelectionHeader");
                ucHeader.Description = GetString("ExportPanel.ObjectsSelectionDescription");
                break;

            case 2:
                ucHeader.Header = GetString("ExportPanel.ObjectsProgressHeader");
                ucHeader.Description = GetString("ExportPanel.ObjectsProgressDescription");
                break;
        }
    }


    private void ReloadSteps()
    {
        switch (wzdExport.ActiveStepIndex)
        {
            case 0:
                break;

            case 1:
                //this.pnlExport.ReloadData();
                ltlScript.Text = ScriptHelper.GetScript("StartExportStateTimer();");
                break;

            case 2:
                break;
        }
    }


    private void EnsureDefaultButton()
    {
        if (wzdExport.ActiveStep.StepType == WizardStepType.Start)
        {
            Page.Form.DefaultButton = wzdExport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton").UniqueID;
        }
        else if (wzdExport.ActiveStep.StepType == WizardStepType.Step)
        {
            Page.Form.DefaultButton = wzdExport.FindControl("StepNavigationTemplateContainerID").FindControl("StepNextButton").UniqueID;
        }
        else if (wzdExport.ActiveStep.StepType == WizardStepType.Finish)
        {
            Page.Form.DefaultButton = wzdExport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepFinishButton").UniqueID;
        }
    }


    /// <summary>
    /// Creates new settings object for export.
    /// </summary>
    private SiteExportSettings GetNewSettings()
    {
        SiteExportSettings result = new SiteExportSettings(CMSContext.CurrentUser);

        result.WebsitePath = Server.MapPath("~/");
        result.TargetPath = ImportExportHelper.GetSiteUtilsFolder() + "Export";
        result.PersistentSettingsKey = PersistentSettingsKey;

        return result;
    }

    #endregion


    #region "Callback handling"

    /// <summary>
    /// Gets the current export state.
    /// </summary>
    /// <param name="argument">Argument</param>
    public string GetExportState(string argument)
    {
        string result = null;

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
                // Cancel export
                if (cancel)
                {
                    ExportManager.Settings.Cancel();
                }

                result = ExportManager.Settings.GetLimitedProgressLog(messageLength, errorLength, warningLength);
            }
            catch (Exception ex)
            {
                EventLogProvider ev = new EventLogProvider();
                ev.LogEvent("ExportWizard", "EXPORT", ex);

                result = ExportManager.Settings.GetLimitedProgressLog(messageLength, errorLength, warningLength);
            }
            finally
            {
                if (ExportManager.Settings.GetProgressState() != LogStatusEnum.Info)
                {
                    // Delete presistent data
                    PersistentStorageHelper.RemoveValue(PersistentSettingsKey);
                }
            }
        }

        return result;
    }


    /// <summary>
    /// Callback event handler.
    /// </summary>
    /// <param name="argument">Callback argument</param>
    public void RaiseCallbackEvent(string argument)
    {
        hdnState.Value = GetExportState(argument);
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
