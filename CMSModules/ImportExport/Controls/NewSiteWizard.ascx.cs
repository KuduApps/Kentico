using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

using CMS.CMSImportExport;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.EventLog;
using CMS.UIControls;
using CMS.VirtualPathHelper;
using CMS.IO;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

public partial class CMSModules_ImportExport_Controls_NewSiteWizard : CMSUserControl, ICallbackEventHandler
{
    #region "Variables"

    private static readonly Hashtable mManagers = new Hashtable();
    private string mFinishUrl = "~/CMSSiteManager/Sites/site_list.aspx";
    private bool? mSiteIsRunning = null;
    private bool mImportCanceled = false;

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
                im.ThrowExceptionOnError = true;
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
            return "NewSiteWizard_" + ProcessGUID + "_Settings";
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
                throw new Exception("[ImportWizard.ImportSettings]: Import settings has been lost.");
            }
            return settings;
        }
        set
        {
            PersistentStorageHelper.SetValue(PersistentSettingsKey, value);
        }
    }


    /// <summary>
    /// Indicates if imported site is running.
    /// </summary>
    public bool SiteIsRunning
    {
        get
        {
            if (mSiteIsRunning == null)
            {
                mSiteIsRunning = (ImportManager.Settings.SiteInfo.Status == SiteStatusEnum.Running);
            }
            return mSiteIsRunning.Value;
        }
    }


    /// <summary>
    /// Web template ID.
    /// </summary>
    public int WebTemplateID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["WebTemplateID"], 0);
        }
        set
        {
            ViewState["WebTemplateID"] = value;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SiteName"], "");
        }
        set
        {
            ViewState["SiteName"] = value;
        }
    }


    /// <summary>
    /// Site domain.
    /// </summary>
    public string Domain
    {
        get
        {
            return ValidationHelper.GetString(ViewState["Domain"], "");
        }
        set
        {
            ViewState["Domain"] = value;
        }
    }


    /// <summary>
    /// Site culture.
    /// </summary>
    public string Culture
    {
        get
        {
            return ValidationHelper.GetString(ViewState["Culture"], "");
        }
        set
        {
            ViewState["Culture"] = value;
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
            return wzdImport.FindControl("StepNavigationTemplateContainerID").FindControl("StepPreviousButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Next button.
    /// </summary>
    public LocalizedButton NextButton
    {
        get
        {
            if (wzdImport.ActiveStepIndex == 0)
            {
                return wzdImport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton") as LocalizedButton;
            }
            return wzdImport.FindControl("StepNavigationTemplateContainerID").FindControl("StepNextButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Cancel button.
    /// </summary>
    public LocalizedButton CancelButton
    {
        get
        {
            return wzdImport.FindControl("StepNavigationTemplateContainerID").FindControl("StepCancelButton") as LocalizedButton;
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
            // Check if any template is present on the disk
            if (!WebTemplateInfoProvider.IsAnyTemplatePresent())
            {
                selectTemplate.StopProcessing = true;
                pnlWrapper.Visible = false;
                lblError.Visible = true;
                lblError.Text = GetString("NewSite.NoWebTemplate");
            }

            // Initialize virtual path provider
            VirtualPathHelper.Init(Page);

            // Initialize import settings
            ImportSettings = new SiteImportSettings(CMSContext.CurrentUser);
            ImportSettings.WebsitePath = Server.MapPath("~/");
            ImportSettings.PersistentSettingsKey = PersistentSettingsKey;
        }

        if (Page.IsCallback)
        {
            // Stop processing when callback
            selectTemplate.StopProcessing = true;
            selectMaster.StopProcessing = true;
        }
        else
        {
            selectTemplate.StopProcessing = (CausedPostback(PreviousButton) && (wzdImport.ActiveStepIndex == 2)) ? false : (wzdImport.ActiveStepIndex != 1);
            selectMaster.StopProcessing = (wzdImport.ActiveStepIndex != 5);

            PreviousButton.Enabled = true;
            PreviousButton.Visible = (wzdImport.ActiveStepIndex <= 4);
            NextButton.Enabled = true;

            // Bind async controls events
            ctrlAsync.OnFinished += ctrlAsync_OnFinished;
            ctrlAsync.OnError += ctrlAsync_OnError;
            ctrlImport.OnFinished += ctrlImport_OnFinished;

            if (wzdImport.ActiveStepIndex < 4)
            {
                siteDetails.Settings = ImportSettings;
                pnlImport.Settings = ImportSettings;
            }

            // Javascript functions
            string script =
                "var nMessageText = '';\n" +
                "var nErrorText = '';\n" +
                "var nWarningText = '';\n" +
                "var nMachineName = '" + SqlHelperClass.MachineName.ToLower() + "';\n" +
                "var getBusy = false; \n" +
                "function GetImportState(cancel) \n" +
                "{ if(window.Activity){window.Activity();} if (getBusy && !cancel) return; getBusy = true; setTimeout(\"getBusy = false;\", 2000); var argument = cancel + ';' + nMessageText.length + ';' + nErrorText.length + ';' + nWarningText.length + ';' + nMachineName; return " + Page.ClientScript.GetCallbackEventReference(this, "argument", "SetImportStateMssg", "argument", true) + " } \n";

            script +=
                "function SetImportStateMssg(rValue, context) \n" +
                "{ \n" +
                "   getBusy = false; \n" +
                "   if(rValue!='') \n" +
                "   { \n" +
                "       var args = context.split(';');\n" +
                "       var values = rValue.split('" + SiteExportSettings.SEPARATOR + "');\n" +
                "       var messageElement = document.getElementById('" + lblProgress.ClientID + "');\n" +
                "       var errorElement = document.getElementById('" + lblError.ClientID + "');\n" +
                "       var warningElement = document.getElementById('" + lblWarning.ClientID + "');\n" +
                "       var messageText = nMessageText;\n" +
                "       messageText = values[1] + messageText.substring(messageText.length - args[1]);\n" +
                "       if(messageText.length > nMessageText.length){ nMessageText = messageElement.innerHTML = messageText; }\n" +
                "       var errorText = nErrorText;\n" +
                "       errorText = values[2] + errorText.substring(errorText.length - args[2]);\n" +
                "       if(errorText.length > nErrorText.length){ nErrorText = errorElement.innerHTML = errorText; }\n" +
                "       var warningText = nWarningText;\n" +
                "       warningText = values[3] + warningText.substring(warningText.length - args[3]);\n" +
                "       if(warningText.length > nWarningText.length){ nWarningText = warningElement.innerHTML = warningText; }\n" +
                "       if((values=='') || (values[0]=='F')) \n" +
                "       { \n" +
                "           StopImportStateTimer(); \n" +
                "           if (!document.importCancelled) { \n" +
                "              if(values[2] == '') { \n" +
                "                  BTN_Enable('" + NextButton.ClientID + "'); \n" +
                "              } \n" +
                "              else { \n" +
                "                  BTN_Enable('" + PreviousButton.ClientID + "'); \n" +
                "              } \n" +
                "              BTN_Disable('" + CancelButton.ClientID + "'); \n" +
                "           } \n" +
                "       } \n" +
                "   } \n" +
                "} \n";

            // Register the script to perform get flags for showing buttons retrieval callback
            ScriptHelper.RegisterClientScriptBlock(this, GetType(), "GetSetImportState", ScriptHelper.GetScript(script));

            // Add cancel button attribute
            CancelButton.Attributes.Add("onclick",
                "BTN_Disable('" + CancelButton.ClientID + "'); " +
                "CancelImport();" +
                ((wzdImport.ActiveStepIndex == 3) ? string.Empty : "BTN_Enable('" + PreviousButton.ClientID + "'); ") +
                "document.importCancelled = true;return false;"
            );

            wzdImport.NextButtonClick += wzdImport_NextButtonClick;
            wzdImport.PreviousButtonClick += wzdImport_PreviousButtonClick;
            wzdImport.FinishButtonClick += wzdImport_FinishButtonClick;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

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


    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        // Save the settings
        if (!mImportCanceled && (wzdImport.ActiveStepIndex <= 4))
        {
            ImportSettings.SavePersistent();
        }
    }

    #endregion


    #region "Button handling"

    protected void wzdImport_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (!mImportCanceled)
        {
            URLHelper.Redirect(FinishUrl);
        }
    }


    protected void wzdImport_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.CurrentStepIndex)
        {
            // Site details
            case 2:
                if (!siteType.SelectTemplate)
                {
                    wzdImport.ActiveStepIndex--;
                }
                wzdImport.ActiveStepIndex--;
                break;

            // Progress
            case 4:
                URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
                break;

            default:
                wzdImport.ActiveStepIndex--;
                break;
        }
    }


    protected void wzdImport_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.CurrentStepIndex)
        {
            // Import type
            case 0:
                {
                    if (!siteType.SelectTemplate)
                    {
                        try
                        {
                            // Get blank web template                    
                            WebTemplateInfo wi = WebTemplateInfoProvider.GetWebTemplateInfo("BlankSite");
                            if (wi == null)
                            {
                                e.Cancel = true;
                                return;
                            }

                            WebTemplateID = wi.WebTemplateId;

                            // Init the settings
                            ImportSettings.TemporaryFilesCreated = false;
                            ImportSettings.SourceFilePath = Server.MapPath(wi.WebTemplateFileName);
                            if (!File.Exists(ImportSettings.SourceFilePath))
                            {
                                try
                                {
                                    ImportProvider.CreateTemporaryFiles(ImportSettings);
                                }
                                catch (Exception ex)
                                {
                                    lblError.Visible = true;
                                    lblError.Text = ex.Message;
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            if (SiteInfoProvider.GetSitesCount() == 0)
                            {
                                // No site exists, overwrite all
                                ImportSettings.ImportType = ImportTypeEnum.All;
                                ImportSettings.CopyFiles = false;
                            }
                            else
                            {
                                // Some site exists, only new objects
                                ImportSettings.ImportType = ImportTypeEnum.New;
                            }


                            ltlScriptAfter.Text = ScriptHelper.GetScript(
                                "var actDiv = document.getElementById('actDiv'); \n" +
                                "if (actDiv != null) { actDiv.style.display='block'; } \n" +
                                "var buttonsDiv = document.getElementById('buttonsDiv'); if (buttonsDiv != null) { buttonsDiv.disabled=true; } \n" +
                                "BTN_Disable('" + NextButton.ClientID + "'); \n" +
                                "StartSelectionTimer();"
                            );

                            // Preselect objects asynchronously
                            ctrlAsync.Parameter = "N";
                            ctrlAsync.RunAsync(SelectObjects, WindowsIdentity.GetCurrent());

                            e.Cancel = true;
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = ex.Message;
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        siteDetails.SiteName = null;
                        siteDetails.SiteDisplayName = null;
                        selectTemplate.ReloadData();
                    }

                    wzdImport.ActiveStepIndex++;
                }
                break;

            // Template selection
            case 1:
                {
                    if (!selectTemplate.ApplySettings())
                    {
                        e.Cancel = true;
                        return;
                    }

                    // Init the settings
                    WebTemplateInfo wi = WebTemplateInfoProvider.GetWebTemplateInfo(selectTemplate.WebTemplateId);
                    if (wi == null)
                    {
                        throw new Exception("Web template not found.");
                    }

                    ImportSettings.TemporaryFilesCreated = false;
                    ImportSettings.SourceFilePath = Server.MapPath(wi.WebTemplateFileName);
                    ImportSettings.IsWebTemplate = true;
                    try
                    {
                        ImportProvider.CreateTemporaryFiles(ImportSettings);
                    }
                    catch (Exception ex)
                    {
                        lblError.Visible = true;
                        lblError.Text = ex.Message;
                        e.Cancel = true;
                        return;
                    } 

                    if (SiteInfoProvider.GetSitesCount() == 0)
                    {
                        // No site exists, overwrite all
                        ImportSettings.ImportType = ImportTypeEnum.All;
                    }
                    else
                    {
                        // Some site exists, only new objects
                        ImportSettings.ImportType = ImportTypeEnum.New;
                    }

                    ltlScriptAfter.Text = ScriptHelper.GetScript(
                        "var actDiv = document.getElementById('actDiv');\n" +
                        "if (actDiv != null) { actDiv.style.display='block'; }\n" +
                        "var buttonsDiv = document.getElementById('buttonsDiv');\n" +
                        "if (buttonsDiv != null) { buttonsDiv.disabled=true; }\n" +
                        "BTN_Disable('" + NextButton.ClientID + "');\n" +
                        "BTN_Disable('" + PreviousButton.ClientID + "');\n" +
                        "StartSelectionTimer();"
                    );

                    // Preselect objects asynchronously
                    ctrlAsync.Parameter = "T";
                    ctrlAsync.RunAsync(SelectObjects, WindowsIdentity.GetCurrent());

                    e.Cancel = true;
                }
                break;

            // Site details
            case 2:
                if (!siteDetails.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }

                // Update settings
                ImportSettings = siteDetails.Settings;

                Culture = siteDetails.Culture;

                pnlImport.ReloadData(true);
                wzdImport.ActiveStepIndex++;
                break;

            // Objects selection
            case 3:
                if (!pnlImport.ApplySettings())
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

                ImportSettings = pnlImport.Settings;

                PreviousButton.Enabled = false;
                NextButton.Enabled = false;

                SiteName = ImportSettings.SiteName;
                Domain = ImportSettings.SiteDomain;

                // Init the Mimetype helper (required for the Import)
                MimeTypeHelper.LoadMimeTypes();

                // Start asynchronnous Import
                ImportSettings.SetSettings(ImportExportHelper.SETTINGS_DELETE_TEMPORARY_FILES, false);
                ImportSettings.DefaultProcessObjectType = ProcessObjectEnum.Selected;
                ImportManager.Settings = ImportSettings;

                // Import site asynchronously
                ctrlImport.RunAsync(ImportManager.Import, WindowsIdentity.GetCurrent());
                ctrlImport.PostbackOnError = false;

                ltlScript.Text = ScriptHelper.GetScript("StartImportStateTimer();");
                wzdImport.ActiveStepIndex++;
                break;

            // Import progress
            case 4:
                PreviousButton.Visible = false;

                CultureHelper.SetPreferredCulture(Culture);
                if (siteType.SelectTemplate)
                {
                    // Done
                    finishSite.Domain = Domain;
                    finishSite.SiteIsRunning = SiteIsRunning;
                    wzdImport.ActiveStepIndex = 7;
                }
                else
                {
                    if (ImportManager.Settings.IsWarning())
                    {
                        try
                        {
                            // Convert default culture
                            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                            tree.ChangeSiteDefaultCulture(SiteName, Culture, "en-US");

                            // Change root GUID
                            CMS.TreeEngine.TreeNode root = DocumentHelper.GetDocument(SiteName, "/", Culture, false, "cms.root", null, null, 1, false, null, tree);
                            if (root != null)
                            {
                                root.NodeGUID = Guid.NewGuid();
                                DocumentHelper.UpdateDocument(root, tree);
                            }
                        }
                        catch (Exception ex)
                        {
                            EventLogProvider ev = new EventLogProvider();
                            ev.LogEvent("NewSiteWizard", "FINISH", ex);
                            lblError.Text = ex.Message;
                            e.Cancel = true;
                            return;
                        }
                    }
                    selectMaster.SiteName = SiteName;
                    selectMaster.ReloadData();
                }
                break;

            // Master template
            case 5:
                if (!selectMaster.ApplySettings())
                {
                    e.Cancel = true;
                    return;
                }

                siteStructure.SiteName = SiteName;
                break;

            // Define site structure
            case 6:
                finishSite.Domain = Domain;
                finishSite.SiteIsRunning = SiteIsRunning;
                break;

            // Other steps
            default:
                wzdImport.ActiveStepIndex = e.NextStepIndex;
                break;
        }
    }

    #endregion


    #region "Async control events"

    protected void ctrlAsync_OnError(object sender, EventArgs e)
    {
        lblError.Visible = true;
        lblError.Text = ((CMSAdminControls_AsyncControl)sender).Worker.LastException.Message;

        // Stop the timer
        ltlScript.Text += ScriptHelper.GetScript("StopSelectionTimer();");
    }


    protected void ctrlAsync_OnFinished(object sender, EventArgs e)
    {
        string param = ValidationHelper.GetString(ctrlAsync.Parameter, "");
        if (param == "N")
        {
            // Stop the timer
            const string script = "StopSelectionTimer();";

            // Init control
            siteDetails.SiteName = "";
            siteDetails.SiteDisplayName = "";
            siteDetails.DomainName = URLHelper.GetFullDomain();
            siteDetails.DisplayCulture = true;
            siteDetails.ReloadData();

            wzdImport.ActiveStepIndex += 2;

            ltlScriptAfter.Text += ScriptHelper.GetScript(script);
        }
        else if (param == "T")
        {
            // Init control
            siteDetails.DomainName = URLHelper.GetFullDomain();
            siteDetails.DisplayCulture = false;
            siteDetails.ReloadData();

            wzdImport.ActiveStepIndex++;
        }
    }


    protected void ctrlImport_OnFinished(object sender, EventArgs e)
    {
        try
        {
            // Convert default culture
            if (!siteType.SelectTemplate)
            {
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                tree.ChangeSiteDefaultCulture(SiteName, Culture, "en-US");

                // Change root GUID
                CMS.TreeEngine.TreeNode root = DocumentHelper.GetDocument(SiteName, "/", Culture, false, "cms.root", null, null, 1, false, null, tree);
                if (root != null)
                {
                    root.NodeGUID = Guid.NewGuid();
                    DocumentHelper.UpdateDocument(root, tree);
                }
            }
        }
        catch (Exception ex)
        {
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent("NewSiteWizard", "FINISH", ex);
        }
        finally
        {
            if (ImportManager.Settings.ProcessCanceled)
            {
                NextButton.Enabled = CancelButton.Enabled = false;
                mImportCanceled = true;
                lblProgress.Text = "<strong>" + ResHelper.GetAPIString("ImportSite.ImportCanceled", "Import process has been cancelled.") + "</strong>";
            }
            else
            {
                if (!ImportManager.Settings.IsWarning() && !ImportManager.Settings.IsError())
                {
                    PreviousButton.Visible = false;
                    CultureHelper.SetPreferredCulture(Culture);
                    if (siteType.SelectTemplate)
                    {
                        // Done
                        finishSite.Domain = Domain;
                        wzdImport.ActiveStepIndex = 7;
                    }
                    else
                    {
                        selectMaster.SiteName = SiteName;
                        wzdImport.ActiveStepIndex += 1;
                        selectMaster.ReloadData();
                    }
                }
            }

            // Stop the timer
            string script = "StopSelectionTimer();";
            ltlScriptAfter.Text += ScriptHelper.GetScript(script);
        }
    }

    #endregion


    #region "Other methods"

    private bool CausedPostback(params Control[] controls)
    {
        foreach (Control control in controls)
        {
            string uniqueID = control.UniqueID;
            bool toReturn = (Request.Form[uniqueID] != null) || ((Request.Form["__EVENTTARGET"] != null) && Request.Form["__EVENTTARGET"].Equals(uniqueID)) || ((Request.Form[uniqueID + ".x"] != null) && (Request.Form[uniqueID + ".y"] != null));
            if (toReturn)
            {
                return true;
            }
        }
        return false;
    }


    protected void InitializeHeader()
    {
        int stepIndex = wzdImport.ActiveStepIndex + 1;

        switch (wzdImport.ActiveStepIndex)
        {
            case 0:
                ucHeader.Header = GetString("NewSite_ChooseSite.StepTitle");
                ucHeader.Description = GetString("NewSite_ChooseSite.StepDesc");
                break;

            case 1:
                ucHeader.Header = GetString("NewSite_ChooseWebTemplate.StepTitle");
                ucHeader.Description = GetString("NewSite_ChooseWebTemplate.StepDesc");
                break;

            case 2:
                stepIndex = siteType.SelectTemplate ? 3 : 2;
                ucHeader.Header = GetString("NewSite_SiteDetails.StepTitle");
                ucHeader.Description = GetString("NewSite_SiteDetails.StepDesc");
                break;

            case 3:
                stepIndex = siteType.SelectTemplate ? 4 : 3;
                ucHeader.Header = GetString("ImportPanel.ObjectsSelectionHeader");
                ucHeader.Description = GetString("ImportPanel.ObjectsSelectionDescription");
                break;

            case 4:
                stepIndex = siteType.SelectTemplate ? 5 : 4;
                ucHeader.Header = GetString("ImportPanel.ObjectsProgressHeader");
                ucHeader.Description = GetString("ImportPanel.ObjectsProgressDescription");
                break;

            case 5:
                stepIndex = 5;
                ucHeader.Header = GetString("NewSite_ChooseMasterTemplate.StepTitle");
                ucHeader.Description = GetString("NewSite_ChooseMasterTemplate.StepDesc");
                break;

            case 6:
                stepIndex = 6;
                ucHeader.Header = GetString("NewSite_DefineSiteStructure.StepTitle");
                ucHeader.Description = GetString("NewSite_DefineSiteStructure.StepDesc");
                break;

            case 7:
                stepIndex = siteType.SelectTemplate ? 6 : 7;
                ucHeader.Header = GetString("NewSite_Finish.StepTitle");
                ucHeader.Description = GetString("NewSite_Finish.StepDesc");
                break;
        }

        ucHeader.Title = string.Format(GetString("NewSite_Step"), stepIndex);
    }


    // Preselect objects
    private void SelectObjects(object parameter)
    {
        ImportSettings.LoadDefaultSelection();
    }


    private void EnsureDefaultButton()
    {
        if (wzdImport.ActiveStep != null)
        {
            switch (wzdImport.ActiveStep.StepType)
            {
                case WizardStepType.Start:
                    Page.Form.DefaultButton = wzdImport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton").UniqueID;
                    break;

                case WizardStepType.Step:
                    Page.Form.DefaultButton = wzdImport.FindControl("StepNavigationTemplateContainerID").FindControl("StepNextButton").UniqueID;
                    break;

                case WizardStepType.Finish:
                    Page.Form.DefaultButton = wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepFinishButton").UniqueID;
                    break;
            }
        }
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
                ev.LogEvent("NewSiteWizard", "IMPORT", ex);

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

