using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.IO;
using CMS.Synchronization;
using CMS.SettingsProvider;

using IOExceptions = System.IO;
using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileEdit : CMSAdminControl
{
    #region "Event & delegates"

    /// <summary>
    /// Event fired after saved succeeded.
    /// </summary>
    public event OnActionEventHandler Action;

    #endregion


    #region "Private variables"

    private bool mHasCustomFields = false;

    private MediaFileInfo mFileInfo = null;
    private MediaLibraryInfo mLibraryInfo = null;
    private SiteInfo mLibrarySiteInfo = null;
    private bool mForceReload = false;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whetherm the custom fields tab is displayed.
    /// </summary>
    private bool HasCustomFields
    {
        get
        {
            return this.mHasCustomFields;
        }
        set
        {
            this.mHasCustomFields = value;
        }
    }


    /// <summary>
    /// Indicates whether the current file has a preview.
    /// </summary>
    private bool HasPreview
    {
        get
        {
            return MediaLibraryHelper.HasPreview(this.LibrarySiteInfo.SiteName, this.MediaLibraryID, this.FileInfo.FilePath);
        }
    }


    /// <summary>
    /// Previous file id used in custom fields loading.
    /// </summary>
    private int OldFileID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["OldFileID"], 0);
        }
        set
        {
            ViewState["OldFileID"] = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Currently edited file info.
    /// </summary>
    public MediaFileInfo FileInfo
    {
        get
        {
            if ((this.mFileInfo == null) && (this.FileID > 0))
            {
                this.mFileInfo = MediaFileInfoProvider.GetMediaFileInfo(this.FileID);
            }
            return this.mFileInfo;
        }
        set
        {
            this.mFileInfo = value;
        }
    }


    /// <summary>
    /// File ID.
    /// </summary>
    public int FileID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["FileID"], 0);
        }
        set
        {
            ViewState["FileID"] = value;
            this.FileInfo = null;
        }
    }


    /// <summary>
    /// Current file path.
    /// </summary>
    public string FilePath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FilePath"], "");
        }
        set
        {
            ViewState["FilePath"] = value;
        }
    }


    /// <summary>
    /// Current folder path.
    /// </summary>
    public string FolderPath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FolderPath"], "");
        }
        set
        {
            ViewState["FolderPath"] = value;
        }
    }


    /// <summary>
    /// Media library ID.
    /// </summary>
    public int MediaLibraryID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["MediaLibraryID"], 0);
        }
        set
        {
            ViewState["MediaLibraryID"] = value;
        }
    }


    /// <summary>
    /// Gets library info object.
    /// </summary>
    public MediaLibraryInfo LibraryInfo
    {
        get
        {
            if ((this.mLibraryInfo == null) && (this.MediaLibraryID > 0))
            {
                this.LibraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.MediaLibraryID);
            }
            return this.mLibraryInfo;
        }
        set
        {
            this.mLibraryInfo = value;
        }
    }


    /// <summary>
    /// Info on the site related to the current library.
    /// </summary>
    public SiteInfo LibrarySiteInfo
    {
        get
        {
            if (this.mLibrarySiteInfo == null)
            {
                this.mLibrarySiteInfo = SiteInfoProvider.GetSiteInfo(this.LibraryInfo.LibrarySiteID);
            }
            return this.mLibrarySiteInfo;
        }
        set
        {
            this.mLibrarySiteInfo = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site
    /// Required for versions tab to nesure that control IsLiveSite property presits postback
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsLiveSite"], true);
        }
        set
        {
            ViewState["IsLiveSite"] = value;
        }
    }

    #endregion

    /// <summary>
    /// Page init event
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Set IsLiveSite
        this.imagePreview.IsLiveSite = IsLiveSite;
        this.mediaPreview.IsLiveSite = IsLiveSite;
        this.fileUplPreview.IsLiveSite = IsLiveSite;
        this.formMediaFileCustomFields.IsLiveSite = IsLiveSite;

        this.formMediaFileCustomFields.StopProcessing = true;
    }


    /// <summary>
    /// Page pre render event
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if ((this.FileInfo != null) && (this.LibraryInfo != null) && this.HasPreview)
        {
            this.plcPreview.Visible = true;

            string fileName = this.FileInfo.FileName + "." + this.FileInfo.FileExtension.TrimStart('.');
            string url = MediaFileInfoProvider.GetMediaFileUrl(this.FileInfo.FileGUID, fileName);
            url = URLHelper.UpdateParameterInUrl(url, "preview", "1");
            this.lblPreviewPermaLink.Text = GetFileLinkHtml(URLHelper.ResolveUrl(url));

            if (MediaLibraryHelper.IsExternalLibrary(CMSContext.CurrentSiteName))
            {
                this.plcPrevDirPath.Visible = false;
            }
            else
            {
                this.plcPrevDirPath.Visible = true;
                this.lblPrevDirectLinkVal.Text = GetFileLinkHtml(GetPrevDirectPath());
            }
        }
        else
        {
            this.lblNoPreview.Text = GetString("media.file.nothumb");

            this.plcNoPreview.Visible = true;
            this.plcPreview.Visible = false;
        }
        this.pnlUpdatePreviewDetails.Update();

        // Refresh versions tab if selected and reload was forced
        if (mForceReload && (pnlTabs.SelectedTabIndex == tabVersions.Index))
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "ReloadVersionsTab", "$j(\"#" + objectVersionList.RefreshButton.ClientID + "\").click();", true);
        }
    }


    /// <summary>
    /// Page load
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
        }
        else
        {
            ReloadControl(false);
        }

        this.fileUplPreview.StopProcessing = this.StopProcessing;
    }


    #region "Public methods"

    /// <summary>
    /// Reloads controls content.
    /// </summary>
    public void ReloadControl()
    {
        ReloadControl(true);
    }

    /// <summary>
    /// Reloads controls content.
    /// </summary>
    /// <param name="forceReload">Indicates whether the content should be reloaded as well</param>
    public void ReloadControl(bool forceReload)
    {
        if (!this.StopProcessing)
        {
            this.Visible = true;
            SetupControls(forceReload);
            mForceReload = forceReload;
        }
        else
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Ensures required actions when the file was saved recently.
    /// </summary>
    /// <param name="file">Recently saved file info</param>
    public void AfterSave()
    {
        SetupFile();
        SetupVersions(false);
        pnlUpdateVersions.Update();
    }


    /// <summary>
    /// Sets default values and clear textboxes.
    /// </summary>
    public void SetDefault()
    {
        this.txtEditDescription.Text = "";
        this.txtEditName.Text = "";
        this.txtEditTitle.Text = "";
    }


    /// <summary>
    /// Setup all labels and buttons text.
    /// </summary>
    public void SetupTexts()
    {
        // File form
        this.lblDirPath.Text = GetString("media.file.dirpath");
        this.lblPermaLink.Text = GetString("media.file.permalink");

        // Edit form
        this.lblEditTitle.Text = GetString("media.file.filetitle");
        this.lblEditDescription.Text = GetString("general.description") + ResHelper.Colon;
        this.btnEdit.Text = GetString("general.ok");
        this.rfvEditName.ErrorMessage = GetString("general.requiresvalue");

        this.lblCreatedBy.Text = GetString("media.file.createdby");
        this.lblCreatedWhen.Text = GetString("media.file.createdwhen");
        this.lblExtension.Text = GetString("media.file.extension");
        this.lblDimensions.Text = GetString("media.file.dimensions");
        this.lblModified.Text = GetString("media.file.modified");
        this.lblFileModified.Text = GetString("media.file.filemodified");
        this.lblSize.Text = GetString("media.file.size");
        this.lblFileSize.Text = GetString("media.file.filesize");
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    /// <param name="forceReload">Indicates whether the content should be reloaded as well</param>
    private void SetupControls(bool forceReload)
    {
        ShowProperTabs(forceReload);

        SetupTexts();
    }


    /// <summary>
    /// Returns link HTML to media file.
    /// </summary>
    /// <param name="url">Url to media file</param>
    private static string GetFileLinkHtml(string url)
    {
        return String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", url);
    }


    /// <summary>
    /// Setup general values.
    /// </summary>
    private void SetupFile()
    {
        // Get file and library info
        if ((this.FileInfo != null) && (this.LibraryInfo != null))
        {
            this.formMediaFileCustomFields.IsLiveSite = this.IsLiveSite;

            if (MediaLibraryHelper.IsExternalLibrary(CMSContext.CurrentSiteName))
            {
                this.plcDirPath.Visible = false;
            }
            else
            {
                string url = MediaFileInfoProvider.GetMediaFileUrl(this.LibrarySiteInfo.SiteName, this.LibraryInfo.LibraryFolder, this.FileInfo.FilePath);
                this.ltrDirPathValue.Text = GetFileLinkHtml(ResolveUrl(url));
            }
            this.ltrPermaLinkValue.Text = GetFileLinkHtml(ResolveUrl(MediaFileInfoProvider.GetMediaFileUrl(this.FileInfo.FileGUID, this.FileInfo.FileName)));
            if (ImageHelper.IsImage(this.FileInfo.FileExtension))
            {
                // Ensure max side size 200
                int[] maxsize = ImageHelper.EnsureImageDimensions(0, 0, 200, this.FileInfo.FileImageWidth, this.FileInfo.FileImageHeight);
                this.imagePreview.Width = maxsize[0];
                this.imagePreview.Height = maxsize[1];

                // If is Image show image properties
                this.imagePreview.URL = URLHelper.AddParameterToUrl(MediaFileInfoProvider.GetMediaFileUrl(this.FileInfo.FileGUID, CMSContext.CurrentSiteName), "maxsidesize", "200");
                this.imagePreview.URL = URLHelper.AddParameterToUrl(this.imagePreview.URL, "chset", Guid.NewGuid().ToString());
                this.plcImagePreview.Visible = true;
                this.plcMediaPreview.Visible = false;

                this.pnlPrew.Visible = true;
            }
            else if (CMS.GlobalHelper.MediaHelper.IsFlash(this.FileInfo.FileExtension) || CMS.GlobalHelper.MediaHelper.IsAudio(this.FileInfo.FileExtension) ||
                CMS.GlobalHelper.MediaHelper.IsVideo(this.FileInfo.FileExtension))
            {
                if (CMS.GlobalHelper.MediaHelper.IsAudio(this.FileInfo.FileExtension))
                {
                    this.mediaPreview.Height = 45;
                }
                else
                {
                    this.mediaPreview.Height = 180;
                }
                this.mediaPreview.Width = 270;

                this.mediaPreview.AutoPlay = false;
                this.mediaPreview.AVControls = true;
                this.mediaPreview.Loop = false;
                this.mediaPreview.Menu = true;
                this.mediaPreview.Type = this.FileInfo.FileExtension;

                // If is Image show image properties
                this.mediaPreview.Url = MediaFileInfoProvider.GetMediaFileUrl(this.FileInfo.FileGUID, this.FileInfo.FileName);
                this.plcMediaPreview.Visible = true;
                this.plcImagePreview.Visible = false;

                this.pnlPrew.Visible = true;
            }
            else
            {
                this.pnlPrew.Visible = false;
            }
        }
        else
        {
            this.pnlPrew.Visible = false;
        }
    }


    /// <summary>
    /// Setup preview values.
    /// </summary>
    private void SetupPreview()
    {
        if ((this.FileInfo != null) && (this.LibraryInfo != null))
        {
            this.fileUplPreview.EnableUpdate = MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "filemodify");
            this.fileUplPreview.StopProcessing = false;
            this.fileUplPreview.IsLiveSite = this.IsLiveSite;
            this.fileUplPreview.LibraryFolderPath = this.FolderPath;
            this.fileUplPreview.LibraryID = this.LibraryInfo.LibraryID;
            this.fileUplPreview.MediaFileID = this.FileID;
            this.fileUplPreview.FileInfo = this.FileInfo;
            this.fileUplPreview.ReloadData();
        }
        else
        {
            this.plcPreview.Visible = false;
        }
    }


    /// <summary>
    /// Setup versions tab values.
    /// </summary>
    private void SetupVersions(bool dataReload)
    {
        if (!IsLiveSite && (this.FileInfo != null) && ObjectVersionManager.DisplayVersionsTab(FileInfo))
        {
            tabVersions.Visible = true;
            tabVersions.Style.Add(HtmlTextWriterStyle.Overflow, "auto");
            objectVersionList.Visible = true;
            objectVersionList.Object = FileInfo;

            // Bind refresh tab script to tab click event
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TabVersionsOnClick", ScriptHelper.GetScript("$j(document).ready(function () {$j(\"#" + tabVersions.ClientID + "_head\").children().click( function() { $j(\"#" + objectVersionList.RefreshButton.ClientID + "\").click();});})"));

            // Register script to refresh content
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ReloadMediaFileEdit", ScriptHelper.GetScript("function RefreshContent() { var button = document.getElementById('" + btnHidden.ClientID + "'); if (button){button.click();}}"));
        }
        else
        {
            tabVersions.Visible = false;
        }
    }


    /// <summary>
    /// Setup edit values.
    /// </summary>
    private void SetupEdit()
    {
        if (this.FileInfo != null)
        {
            // Fill edit form
            this.txtEditName.Text = this.FileInfo.FileName;
            this.txtEditDescription.Text = this.FileInfo.FileDescription;
            this.txtEditTitle.Text = this.FileInfo.FileTitle;
            UserInfo currentUserInfo = CMSContext.CurrentUser;
            SiteInfo currentSiteInfo = CMSContext.CurrentSite;

            // Created by
            string userName = null;
            
            UserInfo ui = UserInfoProvider.GetFullUserInfo(this.FileInfo.FileCreatedByUserID);
            if (ui != null)
            {
                if (ui.IsPublic())
                {
                    userName = GetString("general.na");
                }
                else
                {
                    userName = ui.FullName;
                }
            }
            else
            {
                userName = GetString("general.na");
            }
            this.lblCreatedByVal.Text = userName;

            // Created when
            DateTime dtCreated = ValidationHelper.GetDateTime(this.FileInfo.FileCreatedWhen, DateTimeHelper.ZERO_TIME);
            this.lblCreatedWhenVal.Text = TimeZoneHelper.ConvertToUserTimeZone(dtCreated, false, currentUserInfo, currentSiteInfo);

            // Modified when
            DateTime dtModified = ValidationHelper.GetDateTime(this.FileInfo.FileModifiedWhen, DateTimeHelper.ZERO_TIME);
            this.lblModifiedVal.Text = TimeZoneHelper.ConvertToUserTimeZone(dtModified, false, currentUserInfo, currentSiteInfo);

            // Get system file info
            string filePath = MediaFileInfoProvider.GetMediaFilePath(FileInfo.FileLibraryID, FileInfo.FilePath);
            if (File.Exists(filePath))
            {
                FileInfo sysFileInfo = CMS.IO.FileInfo.New(filePath);

                // File modified when
                DateTime dtFileModified = ValidationHelper.GetDateTime(sysFileInfo.LastWriteTime, DateTimeHelper.ZERO_TIME);
                // Display only if system time is 
                if ((dtFileModified - dtModified).TotalSeconds > 5)
                {
                    this.lblFileModifiedVal.Text = TimeZoneHelper.ConvertToUserTimeZone(dtFileModified, false, currentUserInfo, currentSiteInfo);

                    this.plcFileModified.Visible = true;
                    this.plcRefresh.Visible = true;
                }
                else
                {
                    this.plcFileModified.Visible = false;
                    this.plcRefresh.Visible = false;
                }

                // File size
                if (sysFileInfo.Length != FileInfo.FileSize)
                {
                    this.lblFileSizeVal.Text = DataHelper.GetSizeString(sysFileInfo.Length);
                    this.plcFileSize.Visible = true;
                    this.plcRefresh.Visible = true;
                }
                else
                {
                    this.plcFileSize.Visible = false;
                    this.plcRefresh.Visible = false;
                }
            }

            // Size
            this.lblSizeVal.Text = DataHelper.GetSizeString(FileInfo.FileSize);

            // Extension
            this.lblExtensionVal.Text = FileInfo.FileExtension.TrimStart('.').ToLower();

            // Dimensions
            if (ImageHelper.IsImage(FileInfo.FileExtension))
            {
                this.lblDimensionsVal.Text = FileInfo.FileImageWidth + " x " + FileInfo.FileImageHeight;
                this.plcDimensions.Visible = true;
            }
            else
            {
                this.plcDimensions.Visible = false;
            }
        }
        else
        {
            this.txtEditName.Text = "";
            this.txtEditDescription.Text = "";
            this.txtEditTitle.Text = "";
        }
    }


    /// <summary>
    /// Ensures that specified script is passed to the parent control.
    /// </summary>
    /// <param name="script">Script to pass</param>
    private void EnsureParentScript(string script)
    {
        this.RaiseOnAction("setscript", script);
    }


    /// <summary>
    /// Raises action event.
    /// </summary>
    /// <param name="actionName">Name of tha action occuring</param>
    /// <param name="actionArgument">Argument related to the action</param>
    private void RaiseOnAction(string actionName, object actionArgument)
    {
        if (this.Action != null)
        {
            this.Action(actionName, actionArgument);
        }
    }


    /// <summary>
    /// Display or hides the tabs according to the ViewMode setting.
    /// </summary>
    /// <param name="forceReload">Indicates whether the content should be reloaded as well</param> 
    private void ShowProperTabs(bool forceReload)
    {
        ScriptHelper.RegisterJQuery(Page);

        // We need to remove the header text for unused tabs, because of bug
        // in AjaxToolkit Tab control (when hiding the tab text is still visible)
        this.tabGeneral.HeaderText = GetString("general.file");
        this.tabPreview.HeaderText = GetString("general.thumbnail");
        this.tabEdit.HeaderText = GetString("general.edit");
        this.tabCustomFields.HeaderText = GetString("general.customfields");
        this.tabVersions.HeaderText = GetString("objectversioning.tabtitle");

        DisplayCustomFields(forceReload);

        if (forceReload)
        {
            SetupEdit();
        }
        SetupFile();
        SetupPreview();
        SetupVersions(forceReload);
    }


    /// <summary>
    /// Handles custom fields tab displaying.
    /// </summary>
    private void DisplayCustomFields()
    {
        DisplayCustomFields(false);
    }


    /// <summary>
    /// Handles custom fields tab displaying.
    /// </summary>
    /// <param name="forceReload">Indicates whether the content should be reloaded as well</param> 
    private void DisplayCustomFields(bool forceReload)
    {
        // Initialize DataForm
        if ((this.FileID > 0) && this.Visible)
        {
            this.formMediaFileCustomFields.OnBeforeSave += formMediaFileCustomFields_OnBeforeSave;
            this.formMediaFileCustomFields.OnAfterSave += formMediaFileCustomFields_OnAfterSave;
            this.formMediaFileCustomFields.OnValidationFailed += formMediaFileCustomFields_OnValidationFailed;

            this.formMediaFileCustomFields.IsLiveSite = this.IsLiveSite;
            this.formMediaFileCustomFields.StopProcessing = false;
            this.formMediaFileCustomFields.Info = this.FileInfo;
            this.formMediaFileCustomFields.ID = "formMediaFileCustomFields" + this.FileID;
            
            if (this.formMediaFileCustomFields.BasicForm != null)
            {
                this.formMediaFileCustomFields.BasicForm.HideSystemFields = true;
                this.formMediaFileCustomFields.BasicForm.SubmitButton.CssClass = "SubmitButton";
            }

            if ((forceReload) && (OldFileID != FileID))
            {
                if (this.formMediaFileCustomFields.BasicForm == null)
                {
                    this.formMediaFileCustomFields.ReloadData();
                }
                else
                {
                    // Set proper custom fields data in case new file was created
                    this.formMediaFileCustomFields.BasicForm.Data = this.FileInfo;
                }

                this.formMediaFileCustomFields.BasicForm.ReloadData();

                OldFileID = FileID;
            }

            // Initialize customn fields tab if visible
            this.HasCustomFields = (this.formMediaFileCustomFields.BasicForm != null) && (this.formMediaFileCustomFields.BasicForm.FormInformation.GetFormElements(true, false, true).Count > 0);
            if (this.HasCustomFields)
            {
                if ((this.formMediaFileCustomFields.BasicForm != null) && this.formMediaFileCustomFields.BasicForm.SubmitButton.Visible)
                {
                    // Register the postback control
                    ScriptManager manager = ScriptManager.GetCurrent(this.Page);
                    if (manager != null)
                    {
                        manager.RegisterPostBackControl(this.formMediaFileCustomFields.BasicForm.SubmitButton);
                    }
                }

                this.tabCustomFields.Visible = true;
                this.plcMediaFileCustomFields.Visible = true;
            }
            else
            {
                this.formMediaFileCustomFields.StopProcessing = true;
                this.formMediaFileCustomFields.Enabled = false;
                this.formMediaFileCustomFields.Visible = false;
                this.tabCustomFields.Visible = false;
                this.tabCustomFields.HeaderText = "";
                this.plcMediaFileCustomFields.Visible = false;
            }
        }

        this.pnlUpdateCustomFields.Update();
    }


    /// <summary>
    /// Gets direct path for preview image of currently edited media file.
    /// </summary>
    private string GetPrevDirectPath()
    {
        string prevUrl = "";

        // Direct path
        string previewPath = null;
        string previewFolder = null;

        if (Path.GetDirectoryName(this.FileInfo.FilePath).EndsWith(MediaLibraryHelper.GetMediaFileHiddenFolder(CMSContext.CurrentSiteName)))
        {
            previewFolder = Path.GetDirectoryName(this.FileInfo.FilePath) + "\\" + MediaLibraryHelper.GetPreviewFileName(FileInfo.FileName, FileInfo.FileExtension, ".*", CMSContext.CurrentSiteName);
            previewPath = MediaLibraryInfoProvider.GetMediaLibraryFolderPath(FileInfo.FileLibraryID) + "\\" + previewFolder;
        }
        else
        {
            previewFolder = Path.GetDirectoryName(this.FileInfo.FilePath) + "\\" + MediaLibraryHelper.GetMediaFileHiddenFolder(CMSContext.CurrentSiteName) + "\\" + MediaLibraryHelper.GetPreviewFileName(FileInfo.FileName, FileInfo.FileExtension, ".*", CMSContext.CurrentSiteName);
            previewPath = MediaLibraryInfoProvider.GetMediaLibraryFolderPath(FileInfo.FileLibraryID) + "\\" + previewFolder;
        }
        if (Directory.Exists(Path.GetDirectoryName(previewPath)))
        {
            string[] files = Directory.GetFiles(Path.GetDirectoryName(previewPath), Path.GetFileName(previewPath));
            if (files.Length > 0)
            {
                previewFolder = Path.GetDirectoryName(previewFolder).Replace('\\', '/').TrimStart('/');
                string prevFileName = Path.GetFileName(files[0]);

                prevUrl = MediaFileInfoProvider.GetMediaFileUrl(CMSContext.CurrentSiteName, this.LibraryInfo.LibraryFolder, previewFolder + '/' + prevFileName);
                prevUrl = URLHelper.ResolveUrl(prevUrl);
            }
        }

        return prevUrl;
    }

    #endregion


    #region "Edit tab"

    /// <summary>
    /// Edit file event handler.
    /// </summary>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        // Check 'File modify' permission
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "filemodify"))
        {
            this.lblErrorEdit.Text = MediaLibraryHelper.GetAccessDeniedMessage("filemodify");
            this.lblErrorEdit.Visible = true;

            SetupTexts();
            SetupEdit();

            // Update form
            pnlUpdateEditInfo.Update();
            pnlUpdateFileInfo.Update();
            return;
        }

        FileInfo fi = CMS.IO.FileInfo.New(MediaFileInfoProvider.GetMediaFilePath(CMSContext.CurrentSiteName, this.LibraryInfo.LibraryFolder, this.FilePath));
        if ((fi != null) && (this.LibraryInfo != null))
        {
            // Check if the file exists
            if (!fi.Exists)
            {
                this.lblErrorEdit.Text = GetString("general.wasdeleted");
                this.lblErrorEdit.Visible = true;
                this.pnlUpdateEditInfo.Update();
                return;
            }

            string path = MediaLibraryHelper.EnsurePath(this.FilePath);
            string fileName = URLHelper.GetSafeFileName(this.txtEditName.Text.Trim(), CMSContext.CurrentSiteName, false);
            string origFileName = Path.GetFileNameWithoutExtension(fi.FullName);

            // Check if the filename is in correct format
            if (!ValidationHelper.IsFileName(fileName))
            {
                this.lblErrorEdit.Text = GetString("media.rename.wrongformat");
                this.lblErrorEdit.Visible = true;
                this.pnlUpdateEditInfo.Update();
                return;
            }

            if (this.FileInfo != null)
            {
                if ((CMSContext.CurrentUser != null) && (!CMSContext.CurrentUser.IsPublic()))
                {
                    this.FileInfo.FileModifiedWhen = CMSContext.CurrentUser.DateTimeNow;
                    this.FileInfo.FileModifiedByUserID = CMSContext.CurrentUser.UserID;
                }
                else
                {
                    this.FileInfo.FileModifiedWhen = DateTime.Now;
                }
                // Check if filename is changed ad move file if necessary
                if (fileName != origFileName)
                {
                    try
                    {
                        // Check if file with new file name exists
                        string newFilePath = Path.GetDirectoryName(fi.FullName) + "\\" + fileName + fi.Extension;
                        if (!File.Exists(newFilePath))
                        {
                            string newPath = (string.IsNullOrEmpty(Path.GetDirectoryName(path)) ? "" : Path.GetDirectoryName(path) + "/") + fileName + this.FileInfo.FileExtension;
                            MediaFileInfoProvider.MoveMediaFile(CMSContext.CurrentSiteName, this.FileInfo.FileLibraryID, path, newPath, false);
                            this.FileInfo.FilePath = CMS.MediaLibrary.MediaLibraryHelper.EnsurePath(newPath);
                        }
                        else
                        {
                            this.lblErrorEdit.Text = GetString("general.fileexists");
                            this.lblErrorEdit.Visible = true;
                            this.pnlUpdateEditInfo.Update();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.lblErrorEdit.Text = GetString("media.rename.failed") + ": " + ex.Message;
                        this.lblErrorEdit.Visible = true;
                        this.pnlUpdateEditInfo.Update();
                        return;
                    }
                }
                // Set media file info
                this.FileInfo.FileName = fileName;
                this.FileInfo.FileTitle = this.txtEditTitle.Text;
                this.FileInfo.FileDescription = this.txtEditDescription.Text;

                // Save
                MediaFileInfoProvider.SetMediaFileInfo(this.FileInfo);
                this.FilePath = this.FileInfo.FilePath;

                // Update file modified if not moving physical file
                if (fileName == origFileName)
                {
                    fi.LastWriteTime = FileInfo.FileModifiedWhen;
                }

                // Inform user on success
                this.lblInfoEdit.Text = GetString("general.changessaved");
                this.lblInfoEdit.Visible = true;
                this.pnlUpdateEditInfo.Update();

                SetupEdit();
                this.pnlUpdateFileInfo.Update();

                SetupTexts();
                SetupFile();
                this.pnlUpdateGeneral.Update();

                SetupPreview();
                this.pnlUpdatePreviewDetails.Update();

                SetupVersions(false);
                pnlUpdateVersions.Update();

                RaiseOnAction("rehighlightitem", Path.GetFileName(this.FileInfo.FilePath));
            }
        }
    }


    /// <summary>
    /// Edit file event handler.
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        // Check 'File modify' permission
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "filemodify"))
        {
            this.lblErrorEdit.Text = MediaLibraryHelper.GetAccessDeniedMessage("filemodify");
            this.lblErrorEdit.Visible = true;

            SetupFile();
            return;
        }

        FileInfo fi = CMS.IO.FileInfo.New(MediaFileInfoProvider.GetMediaFilePath(CMSContext.CurrentSiteName, this.LibraryInfo.LibraryFolder, this.FilePath));
        if ((fi != null) && (this.LibraryInfo != null))
        {
            if (this.FileInfo != null)
            {

                this.FileInfo.FileModifiedWhen = DateTime.Now;
                // Set media file info
                this.FileInfo.FileSize = fi.Length;
                if (ImageHelper.IsImage(FileInfo.FileExtension))
                {
                    ImageHelper ih = new ImageHelper();
                    ih.LoadImage(File.ReadAllBytes(fi.FullName));
                    this.FileInfo.FileImageWidth = ih.ImageWidth;
                    this.FileInfo.FileImageHeight = ih.ImageHeight;
                }
                this.FileInfo.FileTitle = this.txtEditTitle.Text.Trim();
                this.FileInfo.FileDescription = this.txtEditDescription.Text.Trim();

                // Save
                MediaFileInfoProvider.SetMediaFileInfo(this.FileInfo);

                // Remove old thumbnails
                MediaFileInfoProvider.DeleteMediaFileThumbnails(FileInfo);

                // Inform user on success
                this.lblInfoEdit.Text = GetString("media.refresh.success");
                this.lblInfoEdit.Visible = true;

                this.pnlUpdateEditInfo.Update();

                SetupTexts();

                SetupFile();
                this.pnlUpdateGeneral.Update();

                SetupPreview();
                this.pnlUpdatePreviewDetails.Update();

                SetupEdit();
                this.pnlUpdateFileInfo.Update();

                SetupVersions(false);
                pnlUpdateVersions.Update();

                RaiseOnAction("rehighlightitem", Path.GetFileName(this.FileInfo.FilePath));
            }
        }
    }


    /// <summary>
    /// BreadCrumbs in edit file form.
    /// </summary>
    protected void lnkEditList_Click(object sender, EventArgs e)
    {
        // Hide preview/edit form and show unigrid
        RaiseOnAction("showlist", null);
    }


    /// <summary>
    /// Stores new media file info into the DB.
    /// </summary>
    /// <param name="fi">Info on file to be stored</param>
    /// <param name="description">Description of new media file</param>
    /// <param name="name">Name of new media file</param>
    public MediaFileInfo SaveNewFile(FileInfo fi, string title, string description, string name, string filePath)
    {
        string path = MediaLibraryHelper.EnsurePath(filePath);
        string fileName = name;

        string fullPath = fi.FullName;
        string extension = URLHelper.GetSafeFileName(fi.Extension, CMSContext.CurrentSiteName);

        // Check if filename is changed ad move file if necessary
        if (fileName + extension != fi.Name)
        {
            string oldPath = path;
            fullPath = MediaLibraryHelper.EnsureUniqueFileName(Path.GetDirectoryName(fullPath) + "\\" + fileName + extension);
            path = MediaLibraryHelper.EnsurePath(Path.GetDirectoryName(path) + "/" + Path.GetFileName(fullPath)).TrimStart('/');
            MediaFileInfoProvider.MoveMediaFile(CMSContext.CurrentSiteName, MediaLibraryID, oldPath, path, true);
            fileName = Path.GetFileNameWithoutExtension(fullPath);
        }

        // Create media file info
        MediaFileInfo fileInfo = new MediaFileInfo(fullPath, this.LibraryInfo.LibraryID, MediaLibraryHelper.EnsurePath(Path.GetDirectoryName(path)), 0, 0, 0);

        fileInfo.FileTitle = title;
        fileInfo.FileDescription = description;

        // Save media file info
        MediaFileInfoProvider.ImportMediaFileInfo(fileInfo);

        // Save FileID in ViewState
        this.FileID = fileInfo.FileID;
        this.FilePath = fileInfo.FilePath;

        return fileInfo;
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Button hidden click
    /// </summary>
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        // Refresh tree structure after rollback
        RaiseOnAction("reloadmedialibrary", FileID + "|" + Path.GetDirectoryName(FileInfo.FilePath));

        // Reload data and refresh update panels
        ReloadControl();
        this.pnlUpdateEditInfo.Update();
        this.pnlUpdateGeneral.Update();
        this.pnlUpdatePreviewDetails.Update();
        this.pnlUpdateFileInfo.Update();
    }


    private void formMediaFileCustomFields_OnValidationFailed(object sender, EventArgs e)
    {
        this.pnlUpdateCustomFields.Update();
    }


    private void formMediaFileCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        this.lblInfo.Text = GetString("general.changessaved");
        this.lblInfo.Visible = true;

        this.pnlUpdateCustomFields.Update();

        SetupEdit();
        this.pnlUpdateEditInfo.Update();

        SetupVersions(false);
        pnlUpdateVersions.Update();
    }


    private void formMediaFileCustomFields_OnBeforeSave(object sender, EventArgs e)
    {
        // Check 'File modify' permission
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "filemodify"))
        {
            this.lblErrorCustom.Text = MediaLibraryHelper.GetAccessDeniedMessage("filemodify");
            this.lblErrorCustom.Visible = true;

            DisplayCustomFields(true);
            this.formMediaFileCustomFields.StopProcessing = true;

            // Update form
            SetupEdit();
        }
    }

    #endregion
}