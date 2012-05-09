using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSImportExport;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.EventLog;
using CMS.LicenseProvider;
using CMS.IO;

public partial class CMSModules_Licenses_Pages_License_Import_Licenses : SiteManagerPage, ICallbackEventHandler
{
    #region "Private variables"
           
    private static Hashtable mManagers = new Hashtable();
    private string mFinishUrl = "~/CMSModules/Licenses/Pages/License_List.aspx";
    AsyncWorker worker = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Panel height property.
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
    /// Import manager.
    /// </summary>
    public ImportManager ImportManager
    {
        get
        {
            string key = "imManagers_" + HTTPHelper.GetUserUniqueIdentifier();
            if (mManagers[key] == null)
            {
                SiteImportSettings imSettings = new SiteImportSettings(CMSContext.CurrentUser);
                imSettings.ImportType = ImportTypeEnum.All;
                imSettings.CopyFiles = false;
                imSettings.EnableSearchTasks = false;
                ImportManager im = new ImportManager(imSettings);
                mManagers[key] = im;
            }
            return (ImportManager)mManagers[key];
        }
        set
        {
            string key = "imManagers_" + HTTPHelper.GetUserUniqueIdentifier();
            mManagers[key] = value;
        }
    }


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

    #endregion


    #region "Finish step wizard buttons"

    /// <summary>
    /// Finish button.
    /// </summary>
    public CMSButton FinishButton
    {
        get
        {
            return wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepFinishButton") as LocalizedButton;
        }
    }   


    /// <summary>
    /// Next button.
    /// </summary>
    public CMSButton NextButton
    {
        get
        {
            return wzdImport.FindControl("StartNavigationTemplateContainerID").FindControl("StepNextButton") as LocalizedButton;
        }
    }


    /// <summary>
    /// Cancel button.
    /// </summary>
    public CMSButton CancelButton
    {
        get
        {
            return wzdImport.FindControl("FinishNavigationTemplateContainerID").FindControl("StepCancelButton") as LocalizedButton;
        }
    }

    #endregion


    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(this.Page);

        // Load button
        imgRefresh.ImageUrl = GetImageUrl("CMSModules/CMS_EmailQueue/refresh.png");

        // Handlers for buttons
        this.wzdImport.NextButtonClick += new WizardNavigationEventHandler(wzdImport_NextButtonClick);
        this.wzdImport.FinishButtonClick += new WizardNavigationEventHandler(wzdImport_FinishButtonClick);
       
        // Add cancel button attribute
        CancelButton.Attributes.Add("onclick",
            "BTN_Disable('" + CancelButton.ClientID + "');" +
            "return CancelImport();");

        // Load files
        if (!RequestHelper.IsPostBack())
        {
            if (wzdImport.ActiveStepIndex == 0)
            {
                LoadFiles();
            }
        }

        if (!IsCallback)
        {
            // Setup page title text and image
            this.CurrentMaster.Title.TitleText = GetString("license.import");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_LicenseKey/import24.png");

            // Context help
            this.CurrentMaster.Title.HelpTopicName = "license_import";
            this.CurrentMaster.Title.HelpName = "helpTopic";

            // Setup breadcrums
            string[,] pageTitleTabs = new string[2, 3];
            pageTitleTabs[0, 0] = GetString("Licenses_License_New.Licenses");
            pageTitleTabs[0, 1] = "~/CMSModules/Licenses/Pages/License_List.aspx";
            pageTitleTabs[1, 0] = GetString("license.import");
            pageTitleTabs[1, 1] = "";
            this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

            // Javascript functions
            string script =
                "var imMessageText = '';\n" +
                "var imErrorText = '';\n" +
                "var imWarningText = '';\n" +
                "var getBusy = false;\n" +
                "function GetState(cancel)\n" +
                "{ if(window.Activity){window.Activity();} if (getBusy && !cancel) return; getBusy = true; setTimeout(\"getBusy = false;\", 2000);"
                + "var argument = cancel + ';' + imMessageText.length + ';' + imErrorText.length + ';' + imWarningText.length; return " + this.Page.ClientScript.GetCallbackEventReference(this, "argument", "SetStateMsg", "argument", true) + " }\n";
                        
            script +=
                        "function SetStateMsg(rValue, context) \n" +
                        "{\n" +
                        "   var values = rValue.split('<#>');\n" +
                        "   if((values[0]=='E') || (values[0]=='F') || values=='')\n" +
                        "   {\n" +
                        "       StopStateTimer();\n" +
                        "       BTN_Enable('" + FinishButton.ClientID + "');\n" +
                        "       BTN_Disable('" + CancelButton.ClientID + "');\n" +
                        "       var bar = document.getElementById('actDiv');\n" +                        
                        "       bar.style.display = 'none';\n" +
                        "       document.getElementById('" + lblLog.ClientID + "').innerHTML = values[1];\n" +
                        "       document.getElementById('" + lblError.ClientID + "').innerHTML = values[2];\n" +
                        "   }\n" +                        
                        "   if(values[0]=='I')\n" +
                        "   {\n" +
                        "       document.getElementById('" + lblLog.ClientID + "').innerHTML = values[1];\n" +
                        "   }\n" +
                        "   else if((values=='') || (values[0]=='F'))\n" +
                        "   {\n" +
                        "       document.getElementById('" + lblLog.ClientID + "').innerHTML = values[1];\n" +
                        "   }\n" +                        
                        "}\n";

            // Register the script to perform get flags for showing buttons retrieval callback
            ScriptHelper.RegisterClientScriptBlock(this, GetType(), "GetImportState", ScriptHelper.GetScript(script));
            ImportManager.Settings.WriteLog = true;
        }
    }
    
    
    /// <summary>
    /// Refresh files button click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        // Reload the list
        lstImports.Items.Clear();
        LoadFiles();

        // Update panel
        pnlUpdate.Update();

        // Select first item
        if (lstImports.Items.Count > 0)
        {
            lstImports.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// Next button click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    void wzdImport_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (wzdImport.ActiveStepIndex)
        {
            case 0:
                if (!string.IsNullOrEmpty(lstImports.SelectedValue))
                {
                    // Create asynchronous process
                    worker = new AsyncWorker();
                    worker.OnError += new EventHandler(worker_OnError);
                    worker.RunAsync(ImportLicenses, WindowsIdentity.GetCurrent());
                    wzdImport.ActiveStepIndex++;
                    PrepareSteps();
                }
                else
                {                    
                    e.Cancel = true;
                    lblErrorFirstStep.Visible = true;
                }
                break;
        }
    }


    /// <summary>
    /// Worker error.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void worker_OnError(object sender, EventArgs e)
    {        
        string error = GetString("license.import.unexpectederror");
        if (worker != null)
        {
            if(worker.LastException != null)
            {
                error = GetString("license.import.exception") + ResHelper.Colon + " " +  worker.LastException.Message;                
            }
        }
        
        ImportManager.Settings.LogProgressState(LogStatusEnum.Error, error);       
    }


    /// <summary>
    /// Finish button click handler.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    void wzdImport_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        // Clear import manager settings
        ImportManager = null;

        URLHelper.Redirect(FinishUrl);
    }


    /// <summary>
    /// Pre render event.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        InitHeader();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Loads files to import.
    /// </summary>
    private void LoadFiles()
    {
        if (this.lstImports.Items.Count == 0)
        {
            // Get files with licenses
            ArrayList files = new ArrayList();
            string path = ImportExportHelper.GetSiteUtilsFolder() + "Import";
            
            // if import path exists
            if (Directory.Exists(path))
            {
                // Get files in directory
                string[] filesInDir = Directory.GetFiles(path, "*.txt");
                for (int i = 0; i < filesInDir.Length; i++)
                {
                    files.Add(filesInDir[i].Substring(filesInDir[i].LastIndexOf("\\") + 1));
                }
            }
            
            if (files.Count != 0)
            {
                lstImports.Enabled = true;
                lstImports.DataSource = files;
                lstImports.DataBind();
            }
            else
            {
                lstImports.Enabled = false;
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            try
            {
                lstImports.SelectedIndex = 0;
            }
            catch
            {
            }
        }
    }


    /// <summary>
    /// Prepares step before show.
    /// </summary>
    void PrepareSteps()
    {
        switch (this.wzdImport.ActiveStepIndex)
        {
            // Import step
            case 1:
                // Clear log
                ImportManager.Settings.ClearProgressLog();                
                // Start timer for state
                this.ltlScript.Text = ScriptHelper.GetScript("StartStateTimer();");
                break;
        }
    }


    /// <summary>
    /// Imports licenses from file.
    /// </summary>
    /// <param name="parameter">Parameter</param>
    private void ImportLicenses(object parameter)
    {
        // Inform about start
        ImportManager.Settings.LogProgressState(LogStatusEnum.Info, GetString("license.import.started"));
        
        // Get selected value and ensure that file exists
        string file = lstImports.SelectedValue;
        string path = ImportExportHelper.GetSiteUtilsFolder() + "Import\\" + lstImports.SelectedValue;
        if (File.Exists(path))
        {
            using (FileStream fs = FileStream.New(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = StreamReader.New(fs))
                {
                    string license = string.Empty;
                    bool licenseInserted = true;
                    while (sr.Peek() >= 0)
                    {
                        // Test if user clicked cancel button
                        if (ImportManager.Settings.ProcessCanceled)
                        {
                            licenseInserted = true;
                            ImportManager.Settings.LogProgressState(LogStatusEnum.Finish, GetString("license.import.canceled"));
                            break;
                        }

                        // Read line and check if it's blank line
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            // Insert license
                            if (!licenseInserted)
                            {
                                InsertLicense(license);
                                licenseInserted = true;
                                license = String.Empty;
                            }
                        }
                        else
                        {
                            licenseInserted = false;
                            license += line + '\n';
                        }
                    }

                    // Insert license
                    if (!licenseInserted)
                    {
                        InsertLicense(license);
                    }
                }
            }
            
            // Clear hashtables
            CMS.SiteProvider.UserInfoProvider.ClearLicenseValues();
            Functions.ClearHashtables();

            if (!ImportManager.Settings.ProcessCanceled)
            {
                ImportManager.Settings.LogProgressState(LogStatusEnum.Finish, GetString("license.import.finish"));
            }
        }        
    }


    /// <summary>
    /// Inserts license to database.
    /// </summary>
    /// <param name="license">License to insert</param>
    private void InsertLicense(string license)
    {
        string error = string.Empty;
        string exists = string.Empty;
        LicenseKeyInfo lk = new LicenseKeyInfo();
        try
        {
            // Load license
            lk.LoadLicense(license.Trim(), "");            
            switch (lk.ValidationResult)
            {
                case LicenseValidationEnum.Expired:
                    error = GetString("Licenses_License_New.LicenseNotValid.Expired");
                    break;

                case LicenseValidationEnum.Invalid:
                    error = GetString("Licenses_License_New.LicenseNotValid.Invalid");
                    break;

                case LicenseValidationEnum.NotAvailable:
                    error = GetString("Licenses_License_New.LicenseNotValid.NotAvailable");
                    break;

                case LicenseValidationEnum.WrongFormat:
                    error = GetString("Licenses_License_New.LicenseNotValid.WrongFormat");
                    break;

                case LicenseValidationEnum.Valid:
                    
                    if(!LicenseKeyInfoProvider.IsLicenseExistForDomain(lk))
                    {
                        LicenseKeyInfoProvider.SetLicenseKeyInfo(lk);
                    }
                    else
                    {
                        // If override
                        if (chkOverrideExisting.Checked)
                        {
                            // Get old license
                            lk = LicenseKeyInfoProvider.GetLicenseKeyInfo(lk.Domain);
                            if (lk != null)
                            {
                                // Delete old license
                                LicenseKeyInfoProvider.DeleteLicenseKeyInfo(lk);

                                // Create new and load
                                lk = new LicenseKeyInfo();                                
                                lk.LoadLicense(license.Trim(), "");

                                // Save
                                switch (lk.ValidationResult)
                                {
                                    case LicenseValidationEnum.Valid:
                                        LicenseKeyInfoProvider.SetLicenseKeyInfo(lk);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            exists = GetString("license.import.skipped") + " " + GetString("Licenses_License_New.DomainAlreadyExists").Replace("%%name%%", lk.Domain);
                        }
                    }                      
                   
                    break;
            }            
        }
        catch (Exception)
        {
            error = GetString("license.import.failed");
        }

        // Result
        if (!string.IsNullOrEmpty(error))
        {
            string msg = "";
            if (string.IsNullOrEmpty(lk.Domain))
            {
                msg = GetString("license.import.failed") ;
            }
            else
            {
                msg = string.Format(GetString("license.import.faileddomain"), lk.Domain) ;
            }

            msg += ResHelper.Colon + " " + error;
            msg = "<span class=\"LineErrorLabel\">" + msg + "</span>";
            ImportManager.Settings.LogProgressState(LogStatusEnum.Info, msg);
        }
        else if (!string.IsNullOrEmpty(exists))
        {
            ImportManager.Settings.LogProgressState(LogStatusEnum.Info, exists);    
        }
        else
        {
            ImportManager.Settings.LogProgressState(LogStatusEnum.Info, string.Format(GetString("license.import.success"), lk.Domain));
        }
    }


    /// <summary>
    /// Initializes header.
    /// </summary>
    void InitHeader()
    {
        int stepIndex = wzdImport.ActiveStepIndex + 1;

        switch (this.wzdImport.ActiveStepIndex)
        {
            case 0:
                ucHeader.Header = GetString("license.import.filesteptitle");
                ucHeader.Description = GetString("license.import.filestepdesc");
                break;

            case 1:
                ucHeader.Header = GetString("license.import.importsteptitle");
                ucHeader.Description = GetString("license.import.importstepdesc");
                break;         
        }

        ucHeader.Title = string.Format(GetString("license.import.step"), stepIndex);
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

        if (args.Length == 4)
        {
            messageLength = ValidationHelper.GetInteger(args[1], 0);
            errorLength = ValidationHelper.GetInteger(args[2], 0);
            warningLength = ValidationHelper.GetInteger(args[3], 0);
        }

        try
        {
            // Cancel Import
            if (cancel)
            {
                ImportManager.Settings.Cancel();
            }
            
            hdnLog.Value = ImportManager.Settings.GetLimitedProgressLog(messageLength, errorLength, warningLength);
        }
        catch (Exception ex)
        {
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent("LicenseImport", "IMPORT", ex);

            hdnLog.Value = ImportManager.Settings.GetLimitedProgressLog(messageLength, errorLength, warningLength);
        }        
    }


    /// <summary>
    /// Callback result retrieving handler.
    /// </summary>
    public string GetCallbackResult()
    {
        return hdnLog.Value;        
    }

    #endregion
}
