using System;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Security.Principal;
using System.Data;
using System.Threading;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.MediaLibrary;
using CMS.IO;
using CMS.EventLog;
using CMS.DataEngine;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_CopyMoveFolder : CMSLiveModalPage
{
    #region "Variables"

    private static Hashtable mInfos = new Hashtable();
    private Hashtable mParameters = null;

    private static string refreshScript = null;

    private int mMediaLibraryID = 0;
    private MediaLibraryInfo mLibraryInfo = null;
    private SiteInfo mLibrarySiteInfo = null;
    private string mLibraryRootFolder = null;
    private string mLibraryPath = "";
    private bool mAllFiles = false;
    private string mAction = null;
    private string mRootFolder = null;
    private string mFolderPath = null;
    private string mNewPath = null;
    private string mFiles = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// ID of the media library.
    /// </summary>
    private int MediaLibraryID
    {
        get
        {
            return mMediaLibraryID;
        }
        set
        {
            mMediaLibraryID = value;
        }
    }


    /// <summary>
    /// Gets current library info.
    /// </summary>
    private MediaLibraryInfo LibraryInfo
    {
        get
        {
            if ((mLibraryInfo == null) && (MediaLibraryID > 0))
            {
                mLibraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(MediaLibraryID);
            }
            return mLibraryInfo;
        }
    }


    /// <summary>
    /// Gets info on site library belongs to.
    /// </summary>
    private SiteInfo LibrarySiteInfo
    {
        get
        {
            if ((mLibrarySiteInfo == null) && (LibraryInfo != null))
            {
                mLibrarySiteInfo = SiteInfoProvider.GetSiteInfo(LibraryInfo.LibrarySiteID);
            }
            return mLibrarySiteInfo;
        }
    }


    /// <summary>
    /// Type of the action.
    /// </summary>
    private string Action
    {
        get
        {
            return mAction;
        }
        set
        {
            mAction = value;
        }
    }


    /// <summary>
    /// Media library Folder path.
    /// </summary>
    private string FolderPath
    {
        get
        {
            return mFolderPath;
        }
        set
        {
            mFolderPath = value;
        }
    }


    /// <summary>
    /// Media library root folder path.
    /// </summary>
    private string RootFolder
    {
        get
        {
            return mRootFolder;
        }
        set
        {
            mRootFolder = value;
        }
    }


    /// <summary>
    /// Path where the item(s) should be copied/moved.
    /// </summary>
    private string NewPath
    {
        get
        {
            return mNewPath;
        }
        set
        {
            mNewPath = value;
        }
    }


    /// <summary>
    /// List of files to copy/move.
    /// </summary>
    private string Files
    {
        get
        {
            return mFiles;
        }
        set
        {
            mFiles = value;
        }
    }


    /// <summary>
    /// Determines whether all files should be copied.
    /// </summary>
    private bool AllFiles
    {
        get
        {
            return mAllFiles;
        }
        set
        {
            mAllFiles = value;
        }
    }


    /// <summary>
    /// Current Error.
    /// </summary>
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mInfos["ProcessingError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["ProcessingError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Current Info.
    /// </summary>
    private string CurrentInfo
    {
        get
        {
            return ValidationHelper.GetString(mInfos["ProcessingInfo_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["ProcessingInfo_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Indicates whether the properties are just loaded - no folder was previously selected.
    /// </summary>
    private bool IsLoad
    {
        get
        {
            return ValidationHelper.GetBoolean(Parameters["load"], false);
        }
    }

    /// <summary>
    /// Current log context.
    /// </summary>
    private LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }


    /// <summary>
    /// Hashtable containing dialog parameters.
    /// </summary>
    private Hashtable Parameters
    {
        get
        {
            if (mParameters == null)
            {
                string identificator = QueryHelper.GetString("params", null);
                mParameters = (Hashtable)WindowHelper.GetItem(identificator);
            }
            return mParameters;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Returns media library root folder path.
    /// </summary>
    public string LibraryRootFolder
    {
        get
        {
            if ((LibrarySiteInfo != null) && (mLibraryRootFolder == null))
            {
                mLibraryRootFolder = MediaLibraryHelper.GetMediaRootFolderPath(LibrarySiteInfo.SiteName);
            }
            return mLibraryRootFolder;
        }
    }


    /// <summary>
    /// Gets library relative url path.
    /// </summary>
    public string LibraryPath
    {
        get
        {
            if (String.IsNullOrEmpty(mLibraryPath))
            {
                if (LibraryInfo != null)
                {
                    mLibraryPath = LibraryRootFolder + LibraryInfo.LibraryFolder;
                }
            }
            return mLibraryPath;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            return;
        }

        // Check if hashtable containing dialog parameters is not empty
        if ((Parameters == null) || (Parameters.Count == 0))
        {
            return;
        }

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;

        // Get the sorce node
        MediaLibraryID = ValidationHelper.GetInteger(Parameters["libraryid"], 0);
        Action = ValidationHelper.GetString(Parameters["action"], string.Empty);
        FolderPath = ValidationHelper.GetString(Parameters["folderpath"], "").Replace("/", "\\");
        Files = ValidationHelper.GetString(Parameters["files"], "").Trim('|');
        RootFolder = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName);
        AllFiles = ValidationHelper.GetBoolean(Parameters["allFiles"], false);
        NewPath = ValidationHelper.GetString(Parameters["newpath"], "").Replace("/", "\\");

        btnCancel.Text = GetString("general.cancel");
        btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

        // Target folder
        string tarFolder = NewPath;
        if (string.IsNullOrEmpty(tarFolder) && (LibraryInfo != null))
        {
            tarFolder = LibraryInfo.LibraryFolder + " (root)";
        }
        lblFolder.Text = tarFolder;

        if (!IsLoad)
        {
            if (AllFiles || String.IsNullOrEmpty(Files))
            {
                if (AllFiles)
                {
                    lblFilesToCopy.ResourceString = "media.folder.filestoall" + Action.ToLower();
                }
                else
                {
                    lblFilesToCopy.ResourceString = "media.folder.folderto" + Action.ToLower();
                }

                // Source folder
                string srcFolder = FolderPath;
                if (string.IsNullOrEmpty(srcFolder) && (LibraryInfo != null))
                {
                    srcFolder = LibraryInfo.LibraryFolder + "&nbsp;(root)";
                }
                lblFileList.Text = HTMLHelper.HTMLEncode(srcFolder);
            }
            else
            {
                lblFilesToCopy.ResourceString = "media.folder.filesto" + Action.ToLower();
                string[] fileList = Files.Split('|');
                foreach (string file in fileList)
                {
                    lblFileList.Text += HTMLHelper.HTMLEncode(DirectoryHelper.CombinePath(FolderPath.TrimEnd('\\'), file)) + "<br />";
                }
            }

            if (!Page.IsCallback && !URLHelper.IsPostback())
            {
                bool performAction = ValidationHelper.GetBoolean(Parameters["performaction"], false);
                if (performAction)
                {
                    // Perform Move or Copy
                    PerformAction();
                }
            }

            pnlInfo.Visible = true;
            pnlEmpty.Visible = false;
        }
        else
        {
            pnlInfo.Visible = false;
            pnlEmpty.Visible = true;
            lblEmpty.Text = GetString("media.copymove.select");

            // Disable New folder button
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "DisableNewFolderOnLoad", ScriptHelper.GetScript("if ((window.parent != null) && window.parent.DisableNewFolderBtn) { window.parent.DisableNewFolderBtn(); }"));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!String.IsNullOrEmpty(lblError.Text))
        {
            lblError.Visible = true;
        }
    }

    #endregion


    /// <summary>
    /// Moves document.
    /// </summary>
    private void PerformAction(object parameter)
    {
        if (Action.ToLower() == "copy")
        {
            AddLog(GetString("media.copy.startcopy"));
        }
        else
        {
            AddLog(GetString("media.move.startmove"));
        }

        if (LibraryInfo != null)
        {
            // Library path (used in recursive copy process)
            string libPath = MediaLibraryInfoProvider.GetMediaLibraryFolderPath(CMSContext.CurrentSiteName, LibraryInfo.LibraryFolder);

            // Ensure libPath is in original path type
            libPath = Path.GetFullPath(libPath);

            // Original path on disk from query
            string origPath = Path.GetFullPath(DirectoryHelper.CombinePath(libPath, FolderPath));

            // New path on disk
            string newPath = null;

            // Original path in DB
            string origDBPath = MediaLibraryHelper.EnsurePath(FolderPath);

            // New path in DB
            string newDBPath = null;

            AddLog(NewPath);

            // Check if requested folder is in library root folder
            if (!origPath.StartsWith(libPath, StringComparison.CurrentCultureIgnoreCase))
            {
                CurrentError = GetString("media.folder.nolibrary");
                AddLog(CurrentError);
                return;
            }

            string origFolderName = Path.GetFileName(origPath);

            if ((String.IsNullOrEmpty(Files) && !mAllFiles) && string.IsNullOrEmpty(origFolderName))
            {
                NewPath = NewPath + "\\" + LibraryInfo.LibraryFolder;
                NewPath = NewPath.Trim('\\');
            }
            newPath = NewPath;

            // Process current folder copy/move action
            if (String.IsNullOrEmpty(Files) && !AllFiles)
            {
                newPath = newPath.TrimEnd('\\') + '\\' + origFolderName;
                newPath = newPath.Trim('\\');

                // Check if moving into same folder
                if ((Action.ToLower() == "move") && (newPath == FolderPath))
                {
                    CurrentError = GetString("media.move.foldermove");
                    AddLog(CurrentError);
                    return;
                }

                // Error if moving folder into itself
                string newRootPath = Path.GetDirectoryName(newPath).Trim();
                string newSubRootFolder = Path.GetFileName(newPath).ToLower().Trim();
                string originalSubRootFolder = Path.GetFileName(FolderPath).ToLower().Trim();
                if (String.IsNullOrEmpty(Files) && (Action.ToLower() == "move") && newPath.StartsWith(DirectoryHelper.EnsurePathBackSlash(FolderPath))
                    && (originalSubRootFolder == newSubRootFolder) && (newRootPath == FolderPath))
                {
                    CurrentError = GetString("media.move.movetoitself");
                    AddLog(CurrentError);
                    return;
                }

                // Get unique path for copy or move
                string path = Path.GetFullPath(DirectoryHelper.CombinePath(libPath, newPath));
                path = MediaLibraryHelper.EnsureUniqueDirectory(path);
                newPath = path.Remove(0, (libPath.Length + 1));

                // Get new DB path
                newDBPath = MediaLibraryHelper.EnsurePath(newPath.Replace(DirectoryHelper.EnsurePathBackSlash(libPath), ""));
            }
            else
            {
                origDBPath = MediaLibraryHelper.EnsurePath(FolderPath);
                newDBPath = MediaLibraryHelper.EnsurePath(newPath.Replace(libPath, "")).Trim('/');
            }

            // Error if moving folder into its subfolder
            if ((String.IsNullOrEmpty(Files) && !AllFiles) && (Action.ToLower() == "move") && newPath.StartsWith(DirectoryHelper.EnsurePathBackSlash(FolderPath)))
            {
                CurrentError = GetString("media.move.parenttochild");
                AddLog(CurrentError);
                return;
            }

            // Error if moving files into same directory
            if ((!String.IsNullOrEmpty(Files) || AllFiles) && (Action.ToLower() == "move") && (newPath.TrimEnd('\\') == FolderPath.TrimEnd('\\')))
            {
                CurrentError = GetString("media.move.fileserror");
                AddLog(CurrentError);
                return;
            }

            NewPath = newPath;
            refreshScript = "if ((typeof(window.top.opener) != 'undefined') && (typeof(window.top.opener.RefreshLibrary) != 'undefined')) {window.top.opener.RefreshLibrary(" + ScriptHelper.GetString(NewPath.Replace('\\', '|')) + ");} else if ((typeof(window.top.wopener) != 'undefined') && (typeof(window.top.wopener.RefreshLibrary) != 'undefined')) { window.top.wopener.RefreshLibrary(" + ScriptHelper.GetString(NewPath.Replace('\\', '|')) + "); } window.top.close();";

            // If mFiles is empty handle directory copy/move
            if (String.IsNullOrEmpty(Files) && !mAllFiles)
            {
                try
                {
                    switch (Action.ToLower())
                    {
                        case "move":
                            MediaLibraryInfoProvider.MoveMediaLibraryFolder(CMSContext.CurrentSiteName, MediaLibraryID, origDBPath, newDBPath, false);
                            break;

                        case "copy":
                            MediaLibraryInfoProvider.CopyMediaLibraryFolder(CMSContext.CurrentSiteName, MediaLibraryID, origDBPath, newDBPath, false, CurrentUser.UserID);
                            break;
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    CurrentError = GetString("general.erroroccurred") + " " + GetString("media.security.accessdenied");
                    EventLogProvider ev = new EventLogProvider();
                    ev.LogEvent("MediaFolder", this.Action, ex);
                    AddLog(CurrentError);
                    return;
                }
                catch (ThreadAbortException ex)
                {
                    string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
                    if (state == CMSThread.ABORT_REASON_STOP)
                    {
                        // When canceled
                        CurrentInfo = GetString("general.actioncanceled");
                        AddLog(CurrentInfo);
                    }
                    else
                    {
                        // Log error
                        CurrentError = GetString("general.erroroccurred") + " " + ex.Message;
                        EventLogProvider ev = new EventLogProvider();
                        ev.LogEvent("MediaFolder", this.Action, ex);
                        AddLog(CurrentError);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    CurrentError = GetString("general.erroroccurred") + " " + ex.Message;
                    EventLogProvider ev = new EventLogProvider();
                    ev.LogEvent("MediaFolder", this.Action, ex);
                    AddLog(CurrentError);
                    return;
                }
            }
            else
            {
                string origDBFilePath = null;
                string newDBFilePath = null;

                if (!mAllFiles)
                {
                    try
                    {
                        string[] files = Files.Split('|');
                        foreach (string filename in files)
                        {
                            origDBFilePath = (string.IsNullOrEmpty(origDBPath)) ? filename : origDBPath + "/" + filename;
                            newDBFilePath = (string.IsNullOrEmpty(newDBPath)) ? filename : newDBPath + "/" + filename;
                            AddLog(filename);
                            CopyMove(origDBFilePath, newDBFilePath);
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        CurrentError = GetString("general.erroroccurred") + " " + ResHelper.GetString("media.security.accessdenied");
                        EventLogProvider ev = new EventLogProvider();
                        ev.LogEvent("MediaFile", this.Action, ex);
                        AddLog(CurrentError);
                        return;
                    }
                    catch (ThreadAbortException ex)
                    {
                        string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
                        if (state == CMSThread.ABORT_REASON_STOP)
                        {
                            // When canceled
                            CurrentInfo = GetString("general.actioncanceled");
                            AddLog(CurrentInfo);
                        }
                        else
                        {
                            // Log error
                            CurrentError = GetString("general.erroroccurred") + " " + ex.Message;
                            EventLogProvider ev = new EventLogProvider();
                            ev.LogEvent("MediaFile", this.Action, ex);
                            AddLog(CurrentError);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        CurrentError = GetString("general.erroroccurred") + " " + ex.Message;
                        EventLogProvider ev = new EventLogProvider();
                        ev.LogEvent("MediaFile", this.Action, ex);
                        AddLog(CurrentError);
                        return;
                    }
                }
                else
                {
                    HttpContext context = (parameter as HttpContext);
                    if (context != null)
                    {
                        HttpContext.Current = context;

                        DataSet files = GetFileSystemDataSource();
                        if (!DataHelper.IsEmpty(files))
                        {
                            foreach (DataRow file in files.Tables[0].Rows)
                            {
                                string fileName = ValidationHelper.GetString(file["FileName"], "");

                                AddLog(fileName);

                                origDBFilePath = (string.IsNullOrEmpty(origDBPath)) ? fileName : origDBPath + "/" + fileName;
                                newDBFilePath = (string.IsNullOrEmpty(newDBPath)) ? fileName : newDBPath + "/" + fileName;

                                // Clear current httpcontext for CopyMove action in threat
                                HttpContext.Current = null;

                                try
                                {
                                    CopyMove(origDBFilePath, newDBFilePath);
                                }
                                catch (UnauthorizedAccessException ex)
                                {
                                    CurrentError = GetString("general.erroroccurred") + " " + ResHelper.GetString("media.security.accessdenied");
                                    EventLogProvider ev = new EventLogProvider();
                                    ev.LogEvent("MediaFile", this.Action, ex);
                                    AddLog(CurrentError);
                                    return;
                                }
                                catch (ThreadAbortException ex)
                                {
                                    string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
                                    if (state == CMSThread.ABORT_REASON_STOP)
                                    {
                                        // When canceled
                                        CurrentInfo = GetString("general.actioncanceled");
                                        AddLog(CurrentInfo);
                                    }
                                    else
                                    {
                                        // Log error
                                        CurrentError = GetString("general.erroroccurred") + " " + ex.Message;
                                        EventLogProvider ev = new EventLogProvider();
                                        ev.LogEvent("MediaFile", this.Action, ex);
                                        AddLog(CurrentError);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CurrentError = GetString("general.erroroccurred") + " " + ex.Message;
                                    EventLogProvider ev = new EventLogProvider();
                                    ev.LogEvent("MediaFile", this.Action, ex);
                                    AddLog(CurrentError);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Performes the Move of Copy action.
    /// </summary>
    public void PerformAction()
    {
        if (!IsLoad)
        {
            if (CheckPermissions())
            {
                pnlInfo.Visible = true;
                pnlEmpty.Visible = false;

                if (Action.ToLower() == "copy")
                {
                    titleElemAsync.TitleText = GetString("media.copy.startcopy");
                    titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_MediaLibrary/foldercopy.png");
                }
                else
                {
                    titleElemAsync.TitleText = GetString("media.move.startmove");
                    titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_MediaLibrary/foldermove.png");
                }                
                RunAsync(PerformAction);
            }
        }
        else
        {
            pnlInfo.Visible = false;
            pnlEmpty.Visible = true;
            lblEmpty.Text = GetString("media.copymove.noselect");
        }
    }


    /// <summary>
    /// Performs action itself.
    /// </summary>
    /// <param name="origDBFilePath">Path of the file specified in DB</param>
    /// <param name="newDBFilePath">New path of the file being inserted into DB</param>
    private void CopyMove(string origDBFilePath, string newDBFilePath)
    {
        switch (Action.ToLower())
        {
            case "move":
                MediaFileInfoProvider.MoveMediaFile(CMSContext.CurrentSiteName, MediaLibraryID, origDBFilePath, newDBFilePath, false);
                break;

            case "copy":
                MediaFileInfoProvider.CopyMediaFile(CMSContext.CurrentSiteName, MediaLibraryID, origDBFilePath, newDBFilePath, false, CurrentUser.UserID);
                break;
        }
    }


    /// <summary>
    /// Returns set of files in the file system.
    /// </summary>
    private DataSet GetFileSystemDataSource()
    {
        fileSystemDataSource.Path = LibraryPath + "/" + MediaLibraryHelper.EnsurePath(FolderPath) + "/";
        fileSystemDataSource.Path = fileSystemDataSource.Path.Replace("/", "\\").Replace("|", "\\");

        return (DataSet)fileSystemDataSource.DataSource;
    }


    #region "Help methods"

    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext currentLog = LogContext.EnsureLog(ctlAsync.ProcessGUID);

        currentLog.Reversed = true;
        currentLog.LineSeparator = "<br />";

        return currentLog;
    }

    /// <summary>
    /// Adds the alert message to the output request window.
    /// </summary>
    /// <param name="message">Message to display</param>
    private void AddAlert(string message)
    {
        ltlScript.Text += ScriptHelper.GetAlertScript(message);
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }


    /// <summary>
    /// Check perrmissions for selected library.
    /// </summary>
    private bool CheckPermissions()
    {
        // If mFiles is empty handle directory copy/move
        if (String.IsNullOrEmpty(Files) && !mAllFiles)
        {
            if (Action.ToLower().Trim() == "copy")
            {
                // Check 'Folder create' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "foldercreate"))
                {
                    lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("foldercreate");
                    return false;
                }
            }
            else
            {

                // Check 'Folder modify' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "foldermodify"))
                {
                    lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("foldermodify");
                    return false;
                }
            }
        }
        else
        {
            if (Action.ToLower().Trim() == "copy")
            {
                // Check 'File create' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "filecreate"))
                {
                    lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("filecreate");
                    return false;
                }
            }
            else
            {
                // Check 'File modify' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "filemodify"))
                {
                    lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("filemodify");
                    return false;
                }
            }
        }
        return true;
    }

    #endregion


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        if (Action.ToLower() == "copy")
        {
            CurrentError = GetString("media.copy.canceled");
        }
        else
        {
            CurrentError = GetString("media.move.canceled");
        }

        pnlLog.Visible = false;
        pnlInfo.Visible = true;

        AddScript("var __pendingCallbacks = new Array();DestroyLog();");
        HandlePossibleErrors();
        CurrentLog.Close();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        pnlLog.Visible = false;
        pnlInfo.Visible = true;

        AddScript("DestroyLog();");
        HandlePossibleErrors();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        if (!HandlePossibleErrors())
        {
            AddScript(refreshScript);
        }
        else
        {
            AddScript("DestroyLog();");
        }
        CurrentLog.Close();
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
    /// Runs async thread.
    /// </summary>
    /// <param name="action">Method to run</param>
    protected void RunAsync(AsyncAction action)
    {
        pnlLog.Visible = true;
        pnlInfo.Visible = false;

        CurrentLog.Close();
        CurrentError = string.Empty;
        CurrentInfo = string.Empty;

        AddScript("InitializeLog();");

        // Ensure current user
        SessionHelper.SetValue("CurrentUser", CurrentUser);
        ctlAsync.Parameter = HttpContext.Current;
        ctlAsync.RunAsync(action, WindowsIdentity.GetCurrent());
    }


    /// <summary>
    /// Ensures any error or info is displayed to user.
    /// </summary>
    /// <returns>True if error occurred.</returns>
    protected bool HandlePossibleErrors()
    {
        if (!string.IsNullOrEmpty(CurrentError))
        {
            lblError.Text = CurrentError;
            ctlAsync.Log = CurrentLog.Log;
            AddScript("var __pendingCallbacks = new Array();DestroyLog();");
            pnlLog.Visible = false;
            pnlInfo.Visible = true;
            return true;
        }
        return false;
    }

    #endregion
}
