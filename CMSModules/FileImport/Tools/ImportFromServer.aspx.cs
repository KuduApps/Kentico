using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Threading;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.WorkflowEngine;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.IO;
using CMS.EventLog;

using TreeNode = CMS.TreeEngine.TreeNode;
using CMS.FormEngine;

public partial class CMSModules_FileImport_Tools_ImportFromServer : CMSFileImportPage
{
    #region "Variables"

    private List<string> filesList = new List<string>();
    protected string targetAliasPath = "";
    protected long filesCount = 0;
    protected long allowedFilesCount = 0;
    protected string rootPath = "~/cmsimportfiles/";
    private static ArrayList resultListIndex = null;
    private static ArrayList resultListValues = null;
    private static ArrayList errorFiles = null;
    private static readonly Hashtable mErrors = new Hashtable();

    #endregion


    #region "Properties"

    /// <summary>
    /// Current log context.
    /// </summary>
    public LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }

    /// <summary>
    /// Current Error.
    /// </summary>
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mErrors["FileImport_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mErrors["FileImport_" + ctlAsync.ProcessGUID] = value;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Check UI personalization
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (!user.IsAuthorizedPerUIElement("CMS.FileImport", "ImportFromServer"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.FileImport", "ImportFromServer");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the main CMS script
        ScriptHelper.RegisterCMS(this.Page);

        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;

        ucFilter.Column = "FileName";
        btnFilter.Text = ResHelper.GetString("general.show");
        btnFilter.CssClass = "ContentButton";

        if (StorageHelper.IsExternalStorage)
        {
            imgExternalStoragePrepare.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_Content/Dialogs/importprepare.png"));
            lnkExternalStoragePrepare.Text = GetString("dialogs.mediaview.azureprepare");

            ScriptHelper.RegisterProgress(Page);
        }
        else
        {
            CurrentMaster.HeaderActionsPlaceHolder.Visible = false;
        }

        if (!RequestHelper.IsCallback())
        {
            CurrentUserInfo user = CMSContext.CurrentUser;

            // Check permissions for CMS Desk -> Tools -> File Import
            if (!user.IsAuthorizedPerUIElement("CMS.Tools", "FileImport"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Tools", "FileImport");
            }

            if (!user.IsAuthorizedPerResource("CMS.FileImport", "ImportFiles"))
            {
                RedirectToCMSDeskAccessDenied("CMS.FileImport", "ImportFiles");
            }

            // Set visibility of panels
            pnlContent.Visible = true;
            pnlLog.Visible = false;

            ScriptHelper.RegisterCMS(this.Page);

            // Initialize culture selector
            this.cultureSelector.SiteID = CMSContext.CurrentSiteID;
            this.pathElem.SiteID = CMSContext.CurrentSiteID;

            // Prepare unigrid
            GetPath();
            gridImport.DataSource = GetFileSystemDataSource(ucFilter.WhereCondition);
            gridImport.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridImport_OnExternalDataBound);
            gridImport.SelectionJavascript = "UpdateCount";
            gridImport.ZeroRowsText = GetString("Tools.FileImport.NoFiles");
            gridImport.OnShowButtonClick += new EventHandler(gridImport_OnShowButtonClick);

            // Prepare async panel
            titleElemAsync.TitleText = GetString("tools.fileimport.importing");
            titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_FileImport/module.png");
            btnCancel.Text = GetString("general.cancel");
            btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

            lblTitle.Text = GetString("Tools.FileImport.ImportedFiles") + " " + rootPath + ": ";
            lblSelected.Text = string.Format(GetString("Tools.FileImport.SelectedCount"), filesCount);

            if (!RequestHelper.IsPostBack())
            {
                this.cultureSelector.Value = CMSContext.PreferredCultureCode;

                // Initialize temporary lists
                resultListIndex = null;
                resultListValues = null;
                errorFiles = null;
            }

            ltlScript.Text += ScriptHelper.GetScript(
                " function UpdateCount(id, checked) {\n" +
                " var hidden = document.getElementById(\"" + hdnSelected.ClientID + "\")\n" +
                " var label =  document.getElementById(\"" + lblSelectedValue.ClientID + "\")\n" +
                " if (hidden.value.indexOf('|'+id+'|') != -1) {  \n" +
                "   if (checked == false) { \n" +
                "     hidden.value=hidden.value.replace('|'+id+'|', '');  \n" +
                "   } \n" +
                " } else { \n" +
                "   if (checked == true) { \n" +
                "     hidden.value = hidden.value + '|'+id+'|';  \n" +
                "   } \n" +
                " } \n" +
                "  label.innerHTML = (hidden.value.split('|').length - 1) / 2; \n" +
                " }\n");
        }
    }


    protected void lnkExternalStoragePrepare_Click(object sender, EventArgs e)
    {
        Directory.PrepareFilesForImport(rootPath);
        URLHelper.Redirect(URLHelper.CurrentURL);
    }


    protected void gridImport_OnShowButtonClick(object sender, EventArgs e)
    {
        gridImport.ClearSelectedItems();
        hdnSelected.Value = string.Empty;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (errorFiles != null)
        {
            gridImport.SelectedItems = errorFiles;
        }
        gridImport.ReloadData();

        // Disable controls if no files found
        if (DataHelper.DataSourceIsEmpty(gridImport.DataSource))
        {
            btnStartImport.Enabled = false;
            pathElem.Enabled = false;
            pnlImportControls.Enabled = false;
        }
        else
        {
            btnStartImport.Enabled = true;
            pathElem.Enabled = true;
            pnlImportControls.Enabled = true;
            filesCount = ((DataSet)gridImport.DataSource).Tables[0].Rows.Count;
        }

        // Set labels
        string count = gridImport.SelectedItems.Count.ToString();
        hdnValue.Value = count;
        lblSelectedValue.Text = count;
        lblTotal.Text = string.Format(GetString("Tools.FileImport.TotalCount"), filesCount);

        errorFiles = null;
    }


    /// <summary>
    /// Gets path from current application settings.
    /// </summary>
    private string GetPath()
    {
        // If import folder for current site is not specified in settings set its path as rootpath
        if (!String.IsNullOrEmpty(CMSContext.CurrentSiteName))
        {
            // Get import folder path from settings
            string path = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSFileImportFolder").Trim();
            if (!string.IsNullOrEmpty(path))
            {
                rootPath = EnsureValidPath(path);
            }
        }

        // Path starting with local driver letter
        if ((char.IsLetter(rootPath.ToLower(), 0)) && (rootPath[1] == ':'))
        {
            if (rootPath[2] != '\\')
            {
                rootPath = rootPath[0] + ":\\" + rootPath.Substring(2, rootPath.Length - 2);
            }
        }
        // Relative path
        else if (!((rootPath[0] == '\\') && (rootPath[1] == '\\')))
        {
            try
            {
                rootPath = HttpContext.Current.Server.MapPath(rootPath);
            }
            catch
            {
                plcImportContent.Visible = false;
                btnStartImport.Enabled = false;
                lblError.Visible = true;
                lblError.Text = String.Format(GetString("Tools.FileImport.InvalidFolder"), rootPath);
                return null;
            }
        }

        if (!String.IsNullOrEmpty(rootPath))
        {
            rootPath = DirectoryHelper.EnsurePathBackSlash(rootPath);
        }

        return rootPath;
    }


    /// <summary>
    /// Ensures that the given path is valid for the later use.
    /// </summary>
    /// <param name="rootPath">Root path to valid</param>    
    private string EnsureValidPath(string rootPath)
    {
        return rootPath.Replace('/', '\\').TrimEnd('\\');
    }


    /// <summary>
    /// Removes all N (at the beginning of expression) from where condition
    /// (e.g. "column LIKE N'word'" => "column LIKE 'word'").
    /// </summary>
    /// <param name="where">WHERE condition</param>
    private string RemoveNFromWhereCondition(string where)
    {
        if (!String.IsNullOrEmpty(where))
        {
            // Remove all N (at the beginning of expression) from where condition (e.g. "column LIKE N'word'" => "column LIKE 'word'")
            bool inString = false;
            char prev = ' ';
            for (int i = 0; i < where.Length; i++)
            {
                if (where[i] == '\'')
                {
                    if (!inString && (prev == 'N'))
                    {
                        where = where.Remove(i - 1, 1);
                        where = where.Insert(i - 1, " ");
                    }
                    inString = !inString;
                }
                prev = where[i];
            }
        }

        return where;
    }


    /// <summary>
    /// Renames columns in WHERE condition.
    /// </summary>
    /// <param name="where">WHERE condition</param>
    /// <param name="oldColName">Old col name</param>
    /// <param name="newColName">New col name</param>
    private string RenameColumn(string where, string oldColName, string newColName)
    {
        if (!String.IsNullOrEmpty(where))
        {
            string[] strs = where.Split('\'');
            bool inString = where.StartsWith("'");
            where = "";
            for (int i = 0; i < strs.Length; i++)
            {
                // Rename/replace column name (avoid string literals)
                if (!inString && !String.IsNullOrEmpty(strs[i]))
                {
                    strs[i] = strs[i].Replace(oldColName, newColName);
                }

                // Create new WHERE condition
                where += (inString ? "'" : "") + strs[i] + (inString ? "'" : "");

                inString = !inString;
            }
        }

        return where;
    }


    /// <summary>
    /// Returns set of files in the file system.
    /// </summary>
    private DataSet GetFileSystemDataSource(string where)
    {
        string whereCond = RemoveNFromWhereCondition(where);
        whereCond = RenameColumn(whereCond, "[FilePath]", "[FileName]");
        this.fileSystemDataSource.WhereCondition = whereCond;
        this.fileSystemDataSource.Path = rootPath;

        try
        {
            return (DataSet)this.fileSystemDataSource.DataSource;
        }
        catch (Exception e)
        {
            lblError.Visible = true;
            lblError.Text = e.Message;
            return null;
        }
    }


    /// <summary>
    /// File list external databound handler.
    /// </summary>
    protected object gridImport_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        int index = -1;
        string filePath = ValidationHelper.GetString(parameter, string.Empty);
        switch (sourceName.ToLower())
        {
            case "filename":
                if (filePath.StartsWith(rootPath))
                {
                    return filePath.Substring(rootPath.Length);
                }
                break;
            case "result":
                string result = string.Empty;
                if (resultListIndex != null)
                {
                    index = resultListIndex.IndexOf(filePath);
                    if (index > -1)
                    {
                        object[] value = (object[])resultListValues[index];
                        if (ValidationHelper.GetBoolean(value[2], false))
                        {
                            result = UniGridFunctions.ColoredSpanMsg(GetString("Tools.FileImport.Imported"), true);
                        }
                        else
                        {
                            result = UniGridFunctions.ColoredSpanMsg((string)value[1], false);
                        }
                    }
                    else
                    {
                        result = GetString("Tools.FileImport.Skipped");
                    }
                }
                return result;
        }
        return parameter.ToString();
    }


    /// <summary>
    /// BtnStartImport click event handler.
    /// </summary>
    protected void btnStartImport_Click(System.Object sender, System.EventArgs e)
    {
        // Check license limitations
        if (!CheckFilesCount(gridImport.SelectedItems.Count))
        {
            lblError.Visible = true;
            lblError.Text = string.Format(GetString("Tools.FileImport.MaximumCountExceeded"), allowedFilesCount);
        }
        else
        {
            if (gridImport.SelectedItems.Count > 0)
            {
                string path = this.pathElem.Value.ToString().Trim();
                if (!String.IsNullOrEmpty(path))
                {
                    // Set visibility of panels
                    pnlLog.Visible = true;
                    pnlContent.Visible = false;

                    CurrentError = string.Empty;
                    CurrentLog.Close();
                    EnsureLog();

                    ctlAsync.Parameter = new object[4] { (gridImport.SelectedItems.ToArray(typeof(string)) as string[]), path, this.cultureSelector.Value, CMSContext.CurrentUser };
                    ctlAsync.RunAsync(Import, WindowsIdentity.GetCurrent());
                }
                else
                {
                    lblError.Text = GetString("Tools.FileImport.AliasPathNotFound");
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = GetString("tools.fileimport.nofilesselected");
                lblError.Visible = true;
            }
        }
    }


    /// <summary>
    /// Import files.
    /// </summary>
    private void Import(object parameter)
    {
        try
        {
            object[] parameters = (object[])parameter;
            string[] items = (string[])parameters[0];
            CurrentUserInfo currentUser = (CurrentUserInfo)parameters[3];

            if ((items.Length > 0) && (currentUser != null))
            {
                resultListIndex = null;
                resultListValues = null;
                errorFiles = null;
                hdnValue.Value = null;
                hdnSelected.Value = null;
                string siteName = CMSContext.CurrentSiteName;
                string targetAliasPath = ValidationHelper.GetString(parameters[1], null);

                bool imported = false; // Flag - true if one file was imported at least
                bool importError = false; // Flag - true when import failed

                TreeProvider tree = new TreeProvider(currentUser);
                TreeNode tn = tree.SelectSingleNode(siteName, targetAliasPath, TreeProvider.ALL_CULTURES, true, null, false);
                if (tn != null)
                {
                    // Check if CMS.File document type exist and check if document contains required columns (FileName, FileAttachment)
                    DataClassInfo fileClassInfo = DataClassInfoProvider.GetDataClass("CMS.File");
                    if (fileClassInfo == null)
                    {
                        AddError(GetString("newfile.classcmsfileismissing"));
                        return;
                    }
                    else
                    {
                        FormInfo fi = new FormInfo(fileClassInfo.ClassFormDefinition);
                        FormFieldInfo fileFfi = null;
                        FormFieldInfo attachFfi = null;
                        if (fi != null)
                        {
                            fileFfi = fi.GetFormField("FileName");
                            attachFfi = fi.GetFormField("FileAttachment");
                        }
                        if ((fi == null) || (fileFfi == null) || (attachFfi == null))
                        {
                            AddError(GetString("newfile.someofrequiredfieldsmissing"));
                            return;
                        }
                    }

                    DataClassInfo dci = DataClassInfoProvider.GetDataClass(tn.NodeClassName);

                    if (dci != null)
                    {
                        // Check if "file" and "folder" are allowed as a child document under selected document type
                        bool fileAllowed = false;
                        bool folderAllowed = false;
                        DataClassInfo folderClassInfo = DataClassInfoProvider.GetDataClass("CMS.Folder");
                        if ((fileClassInfo != null) || (folderClassInfo != null))
                        {
                            string[] paths;
                            foreach (string fullFileName in items)
                            {
                                paths = fullFileName.Substring(rootPath.Length).Split('\\');
                                // Check file
                                if (paths.Length == 1)
                                {
                                    if (!fileAllowed && (fileClassInfo != null) && !DataClassInfoProvider.IsChildClassAllowed(dci.ClassID, fileClassInfo.ClassID))
                                    {
                                        AddError(GetString("Tools.FileImport.NotAllowedChildClass"));
                                        return;
                                    }
                                    else
                                    {
                                        fileAllowed = true;
                                    }
                                }

                                // Check folder
                                if (paths.Length > 1)
                                {
                                    if (!folderAllowed && (folderClassInfo != null) && !DataClassInfoProvider.IsChildClassAllowed(dci.ClassID, folderClassInfo.ClassID))
                                    {
                                        AddError(GetString("Tools.FileImport.FolderNotAllowedChildClass"));
                                        return;
                                    }
                                    else
                                    {
                                        folderAllowed = true;
                                    }
                                }

                                if (fileAllowed && folderAllowed)
                                {
                                    break;
                                }
                            }
                        }

                        // Check if user is allowed to create new file document
                        if (fileAllowed && !currentUser.IsAuthorizedToCreateNewDocument(tn, "CMS.File"))
                        {
                            AddError(GetString("accessdenied.notallowedtocreatedocument"));
                            return;
                        }

                        // Check if user is allowed to create new folder document
                        if (folderAllowed && !currentUser.IsAuthorizedToCreateNewDocument(tn, "CMS.Folder"))
                        {
                            AddError(GetString("accessdenied.notallowedtocreatedocument"));
                            return;
                        }
                    }

                    string cultureCode = ValidationHelper.GetString(parameters[2], "");
                    string[] fileList = new string[1];
                    string[] relativePathList = new string[1];

                    // Begin log
                    AddLog(GetString("tools.fileimport.importingprogress"));

                    if (items.Length > 0)
                    {
                        // Initialize output arrays - we have atleast 1 file
                        if (resultListIndex == null)
                        {
                            resultListIndex = new ArrayList();
                        }
                        if (resultListValues == null)
                        {
                            resultListValues = new ArrayList();
                        }
                    }

                    string msgImported = GetString("Tools.FileImport.Imported");
                    string msgFailed = GetString("Tools.FileImport.Failed");

                    // Insert files selected in datagrid to list of files to import
                    foreach (string fullFileName in items)
                    {
                        // Import selected files only
                        fileList[0] = fullFileName;
                        relativePathList[0] = fullFileName.Substring(rootPath.Length);

                        // Remove extension if needed
                        if (!chkIncludeExtension.Checked)
                        {
                            relativePathList[0] = Regex.Replace(relativePathList[0], "(.*)\\..*", "$1");
                        }

                        try
                        {
                            FileImport.ImportFiles(siteName, targetAliasPath, cultureCode, fileList, relativePathList, currentUser.UserID, chkDeleteImported.Checked);

                            // Import of a file succeeded, fill the output lists
                            resultListIndex.Add(fullFileName);
                            resultListValues.Add(new string[] { fullFileName, msgImported, true.ToString() });

                            imported = true; // One file was imported
                            AddLog(HTMLHelper.HTMLEncode(fullFileName));
                        }
                        catch (Exception ex)
                        {
                            // File import failed
                            if (errorFiles == null)
                            {
                                errorFiles = new ArrayList();
                            }
                            errorFiles.Add(fullFileName);
                            importError = true;

                            // Fill the output lists
                            resultListIndex.Add(fullFileName);
                            resultListValues.Add(new string[] { fullFileName, msgFailed + " (" + HTMLHelper.HTMLEncode(ex.Message) + ")", false.ToString() });

                            AddError(msgFailed + " (" + HTMLHelper.HTMLEncode(ex.Message) + ")");

                            // Abort importing the rest of files for serious exceptions
                            if (!(ex is UnauthorizedAccessException))
                                return;
                        }
                    }
                }
                // Specified alias path not found
                else
                {
                    AddError(GetString("Tools.FileImport.AliasPathNotFound"));
                    return;
                }

                if (filesList.Count > 0)
                {
                    if (!importError)
                    {
                        if (imported)
                        {
                            AddError(GetString("Tools.FileImport.FilesWereImported"));
                            return;
                        }
                    }
                    else
                    {
                        AddError(GetString("Tools.FileImport.FilesNotImported"));
                        return;
                    }
                }
            }
            // No items selected to import
            else
            {
                AddError(GetString("Tools.FileImport.FilesNotImported"));
                return;
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // When canceled
            }
            else
            {
                // Log error
                LogExceptionToEventLog(ex);
            }
        }
        catch (Exception ex)
        {
            // Log error
            LogExceptionToEventLog(ex);
        }
    }


    /// <summary>
    /// Checks the total count of files to not exceed the license limitations.
    /// </summary>
    /// <param name="selectedFilesCount">Selected files count</param>
    private bool CheckFilesCount(long selectedFilesCount)
    {
        long currentDocumentsCount = 0;
        DataSet dsDocuments = TreeHelper.GetDocuments(CMSContext.CurrentSiteName, "/%", TreeProvider.ALL_CULTURES, true, null, null, null, TreeProvider.ALL_LEVELS, false, -1, TreeProvider.SELECTNODES_REQUIRED_COLUMNS);
        if (!DataHelper.DataSourceIsEmpty(dsDocuments))
        {
            currentDocumentsCount += DataHelper.GetItemsCount(dsDocuments);
        }
        int versionLimitations = LicenseKeyInfoProvider.VersionLimitations(LicenseHelper.CurrentLicenseInfo, FeatureEnum.Documents);
        allowedFilesCount = (versionLimitations - currentDocumentsCount);
        return !((versionLimitations != 0) && (versionLimitations < (currentDocumentsCount + selectedFilesCount)));
    }


    /// <summary>
    /// When exception occures, log it to event log.
    /// </summary>
    /// <param name="ex">Exception to log</param>
    private void LogExceptionToEventLog(Exception ex)
    {
        EventLogProvider log = new EventLogProvider();

        log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "IMPORTFILE", CMSContext.CurrentUser.UserID, CMSContext.CurrentUser.UserName, 0, null, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSiteID, HTTPHelper.GetAbsoluteUri());

        AddError(GetString("tools.fileimport.failed") + " (" + ex.Message + ")");
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }

    #endregion


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        ctlAsync.Parameter = null;
        HandlePossibleErrors();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        HandlePossibleErrors();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        HandlePossibleErrors();
    }


    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext log = LogContext.EnsureLog(ctlAsync.ProcessGUID);
        log.Reversed = true;
        log.LineSeparator = "<br />";
        return log;
    }


    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected void AddLog(string newLog)
    {
        EnsureLog();
        LogContext.AppendLine(newLog);
    }


    /// <summary>
    /// Adds the error to collection of errors.
    /// </summary>
    /// <param name="error">Error message</param>
    protected void AddError(string error)
    {
        AddLog(error);
        if (String.IsNullOrEmpty(CurrentError))
        {
            CurrentError = error;
        }
        else
        {
            CurrentError += "<br />" + error;
        }
    }


    private void HandlePossibleErrors()
    {
        CurrentLog.Close();
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "terminatePendingCallbacks", ScriptHelper.GetScript("var __pendingCallbacks = new Array();"));
        if (!String.IsNullOrEmpty(CurrentError))
        {
            lblError.Text = CurrentError;
            lblError.Visible = true;
        }
    }

    #endregion
}