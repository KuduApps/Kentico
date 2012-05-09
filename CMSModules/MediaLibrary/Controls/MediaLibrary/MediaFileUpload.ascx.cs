using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.FormControls;
using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.Synchronization;
using CMS.IO;
using CMS.SettingsProvider;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileUpload : CMSUserControl
{
    #region "Variables"

    private string mInnerDivClass = null;
    private string mInnerLoadingDivClass = null;

    private MediaLibraryInfo mLibraryInfo = null;
    private MediaFileInfo mFileInfo = null;

    private string previewPath = null;
    private string previewName = null;
    private string previewExt = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value which indicates whether the control should be enabled.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["Enabled"], true);
        }
        set
        {
            ViewState["Enabled"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which indicates whether update control shold be be enabled.
    /// </summary>
    public bool EnableUpdate
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["EnableUpdate"], true);
        }
        set
        {
            ViewState["EnableUpdate"] = value;
        }
    }


    /// <summary>
    /// ID of the current library.
    /// </summary>
    public int LibraryID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["LibraryID"], 0);
        }
        set
        {
            ViewState["LibraryID"] = value;
            if (this.newFileElem != null)
            {
                this.newFileElem.LibraryID = value;
            }
        }
    }


    /// <summary>
    /// Info on library media files are created for.
    /// </summary>
    public MediaLibraryInfo LibraryInfo
    {
        get
        {
            if ((this.mLibraryInfo == null) && (this.LibraryID > 0))
            {
                this.mLibraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryID);
            }
            return this.mLibraryInfo;
        }
    }


    /// <summary>
    /// ID of the media file.
    /// </summary>
    public int MediaFileID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["MediaFileID"], 0);
        }
        set
        {
            ViewState["MediaFileID"] = value;
            if (this.newFileElem != null)
            {
                this.newFileElem.MediaFileID = value;
            }
        }
    }


    /// <summary>
    /// Info on currently processed media file.
    /// </summary>
    public MediaFileInfo FileInfo
    {
        get
        {
            if ((this.mFileInfo == null) && (this.MediaFileID > 0))
            {
                this.mFileInfo = MediaFileInfoProvider.GetMediaFileInfo(this.MediaFileID);
            }
            return this.mFileInfo;
        }
        set
        {
            this.mFileInfo = value;
        }
    }


    /// <summary>
    /// Determines whether the uploader should upload media file thumbnail or basic media file.
    /// </summary>
    public bool IsMediaThumbnail
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsMediaThumbnail"], false);
        }
        set
        {
            ViewState["IsMediaThumbnail"] = value;
            if (this.newFileElem != null)
            {
                this.newFileElem.IsMediaThumbnail = value;
            }
        }
    }


    /// <summary>
    /// Folder path of the current library.
    /// </summary>
    public string LibraryFolderPath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["LibraryFolderPath"], "");
        }
        set
        {
            ViewState["LibraryFolderPath"] = value;
            if (this.newFileElem != null)
            {
                this.newFileElem.LibraryFolderPath = value;
            }
        }
    }


    /// <summary>
    /// CSS class of the new attachment link.
    /// </summary>
    public string InnerDivClass
    {
        get
        {
            return (String.IsNullOrEmpty(mInnerDivClass) ? "NewAttachment" : mInnerDivClass);
        }
        set
        {
            mInnerDivClass = value;
        }
    }


    /// <summary>
    /// CSS class of the new attachment loading element.
    /// </summary>
    public string InnerLoadingDivClass
    {
        get
        {
            return (String.IsNullOrEmpty(mInnerLoadingDivClass) ? "NewAttachmentLoading" : mInnerLoadingDivClass);
        }
        set
        {
            mInnerLoadingDivClass = value;
        }
    }


    /// <summary>
    /// Indicates whether the asynchronous postback occurs on the page.
    /// </summary>
    private bool IsAsyncPostback
    {
        get
        {
            return ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for tooltips
        ScriptHelper.RegisterTooltip(Page);

        string refreshScript =
            @"function RefreshUpdatePanel(hiddenFieldID, action) {
                var hiddenField = document.getElementById(hiddenFieldID);
                if (hiddenField) {
                    __doPostBack(hiddenFieldID, action);
                }
            }

            function FullRefresh(hiddenFieldID, action) {
                if(PassiveRefresh != null)
                {
                    PassiveRefresh();
                }

                var hiddenField = document.getElementById(hiddenFieldID);
                if (hiddenField) {
                    __doPostBack(hiddenFieldID, action);
                }
            }

            function FullPageRefresh_" + ClientID + @"(guid) {
                if(PassiveRefresh != null)
                {
                    PassiveRefresh();
                }

                var hiddenField = document.getElementById('" + hdnFullPostback.ClientID + "');" +
                @"if (hiddenField) {
                    __doPostBack('" + hdnFullPostback.ClientID + "', 'refresh|' + guid);" +
                @"}
            }";

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(Page), "RefreshUpdatePanel", ScriptHelper.GetScript(refreshScript));

        // Initialize refresh script for update panel
        string initRefreshScript =
            "function InitRefresh_" + ClientID + "(msg, fullRefresh, guid, action)\n" +
            "{\n" +
            "   if((msg != null) && (msg != \"\")){ alert(msg); action='error'; }\n" +
            "   if(fullRefresh) { FullRefresh('" + hdnFullPostback.ClientID + "', action + '|' + guid); }\n" +
            "   else { RefreshUpdatePanel('" + hdnPostback.ClientID + "', action + '|' + guid); }\n" +
            "}\n";
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(Page), "AfterUploadRefresh", ScriptHelper.GetScript(initRefreshScript));

        // Register dialog script
        ScriptHelper.RegisterDialogScript(this.Page);

        string editorUrl = null;
        if (IsLiveSite)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                editorUrl = URLHelper.ResolveUrl("~/CMS/Dialogs/CMSModules/MediaLibrary/CMSPages/ImageEditor.aspx");
            }
            else
            {
                editorUrl = URLHelper.ResolveUrl("~/CMSModules/MediaLibrary/CMSPages/ImageEditor.aspx");
            }
        }
        else
        {
            editorUrl = URLHelper.ResolveUrl("~/CMSModules/MediaLibrary/Controls/MediaLibrary/ImageEditor.aspx");
        }

        // Dialog for editing image
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(Page), "EditThumbnailImage",
            ScriptHelper.GetScript("function EditThumbnailImage(query) { " +
                "modalDialog('" + editorUrl + "' + query, 'imageEditorDialog', 905, 670); " +
                " } "
            ));

        // Initialize deletion confirmation
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "DeleteConfirmation",
            ScriptHelper.GetScript(
                "function DeleteConfirmation(){var conf = confirm('" + GetString("media.thumb.deleteconfirmation") + "'); return conf;}"
            ));

        // Grid initialization
        gridAttachments.IsLiveSite = this.IsLiveSite;
        gridAttachments.Visible = true;
        gridAttachments.GridName = "~/CMSModules/MediaLibrary/Controls/MediaLibrary/MediaFileUpload.xml";
        gridAttachments.OnExternalDataBound += GridOnExternalDataBound;
        gridAttachments.OnAction += GridOnAction;
        pnlGrid.Attributes.Add("style", "padding-top: 2px;");

        // Load data
        ReloadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblError.Visible = (this.lblError.Text != "");
        this.lblInfo.Visible = (this.lblInfo.Text != "");

        // Ensure correct layout
        bool gridHasData = !DataHelper.DataSourceIsEmpty(gridAttachments.DataSource);
        this.pnlGrid.Visible = gridHasData;
        this.plcUploaderDisabled.Visible = !gridHasData && !this.Enabled;
        this.plcUploader.Visible = !gridHasData;

        // Initialize button for adding files
        this.plcUploader.Visible = !gridHasData;
    }

    #endregion


    #region "Private & protected methods"

    public void ReloadData()
    {
        if (StopProcessing)
        {
            this.newFileElem.StopProcessing = true;
            this.gridAttachments.StopProcessing = true;
        }
        else
        {
            // Initialize button for adding attachments
            this.newFileElem.ImageWidth = 16;
            this.newFileElem.ImageHeight = 16;
            this.newFileElem.DisplayInline = false;
            this.newFileElem.LoadingImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_new.png", IsLiveSite));
            this.newFileElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_new.png", IsLiveSite));
            this.newFileElem.InnerDivHtml = GetString("attach.uploadfile");
            this.newFileElem.InnerDivClass = InnerDivClass;
            this.newFileElem.InnerLoadingDivHtml = GetString("attach.loading");
            this.newFileElem.InnerLoadingDivClass = InnerLoadingDivClass;
            this.newFileElem.IsLiveSite = IsLiveSite;
            this.newFileElem.SourceType = MediaSourceEnum.MediaLibraries;
            this.newFileElem.MediaFileID = this.MediaFileID;
            this.newFileElem.ParentElemID = this.ClientID;
            this.newFileElem.LibraryFolderPath = this.LibraryFolderPath;
            this.newFileElem.IsMediaThumbnail = this.IsMediaThumbnail;
            this.newFileElem.LibraryID = this.LibraryID;
            this.newFileElem.ReloadData();

            this.imgDisabled.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_newdisabled.png", IsLiveSite));

            // Get preview info
            if (this.IsMediaThumbnail && (this.FileInfo != null))
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(this.LibraryInfo.LibrarySiteID);
                if (si != null)
                {
                    previewPath = MediaFileInfoProvider.GetPreviewFilePath(this.FileInfo.FilePath, si.SiteName, this.LibraryInfo.LibraryID);
                    if (previewPath.Length < 260)
                    {
                        string previewFolder = Path.GetDirectoryName(previewPath);
                        if (Directory.Exists(previewFolder))
                        {
                            string[] files = Directory.GetFiles(previewFolder, Path.GetFileName(previewPath));
                            if (files.Length > 0)
                            {
                                previewPath = files[0];
                                previewName = Path.GetFileNameWithoutExtension(previewPath);
                                previewExt = Path.GetExtension(previewPath).TrimStart('.');
                            }
                            else
                            {
                                previewPath = "";
                            }
                        }
                        else
                        {
                            previewPath = "";
                        }
                    }
                    else
                    {
                        previewPath = "";
                    }
                }
            }

            // Bind UniGrid to DataSource
            gridAttachments.GridView.AllowPaging = false;
            gridAttachments.GridView.AllowSorting = false;

            // Get the data
            if (this.IsMediaThumbnail)
            {
                if (!string.IsNullOrEmpty(previewPath))
                {
                    // Create DataSet manually for preview
                    FileInfo file = CMS.IO.FileInfo.New(previewPath);
                    if (file.Exists)
                    {
                        DataSet ds = new DataSet();
                        DataTable table = ds.Tables.Add();
                        table.Columns.Add("FileID", typeof(int));
                        table.Columns.Add("FileSize", typeof(long));
                        table.Columns.Add("FileName", typeof(string));
                        table.Rows.Add(0, file.Length, previewName);
                        gridAttachments.DataSource = ds;
                    }
                }
                else
                {
                    gridAttachments.DataSource = null;
                }
            }
            else
            {
                gridAttachments.DataSource = MediaFileInfoProvider.GetMediaFiles("FileID = " + this.MediaFileID, null);
            }

            gridAttachments.ReloadData();
            this.updPanel.Update();
        }
    }


    /// <summary>
    /// UniGrid action buttons event handler.
    /// </summary>
    protected void GridOnAction(string actionName, object actionArgument)
    {
        // Process proper action
        switch (actionName.ToLower())
        {
            case "delete":
                if (this.IsMediaThumbnail)
                {
                    // Delete thumbnail file
                    if (this.LibraryInfo != null)
                    {
                        // Check 'File delete' permission
                        if (MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(this.LibraryInfo, "filemodify"))
                        {
                            MediaFileInfoProvider.DeleteMediaFilePreview(CMSContext.CurrentSiteName, this.LibraryID, this.FileInfo.FilePath, false);

                            if (this.FileInfo != null)
                            {
                                SiteInfo si = SiteInfoProvider.GetSiteInfo(FileInfo.FileSiteID);
                                if (si != null)
                                {
                                    // Log synchronization task
                                    SynchronizationHelper.LogObjectChange(FileInfo, TaskTypeEnum.UpdateObject);
                                }

                                // Drop the cache dependencies
                                CacheHelper.TouchKeys(MediaFileInfoProvider.GetDependencyCacheKeys(this.FileInfo, true));
                            }
                        }
                        else
                        {
                            lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("filemodify");
                        }
                    }

                    // Ensure recent action is forgotten
                    this.gridAttachments.ClearActions();
                }
                else
                {
                    if (this.LibraryInfo != null)
                    {
                        // Check 'File delete' permission
                        if (MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(this.LibraryInfo, "filedelete"))
                        {
                            // Delete Media File
                            if (this.FileInfo != null)
                            {
                                MediaFileInfoProvider.DeleteMediaFileInfo(this.FileInfo);
                            }
                        }
                    }
                }

                // Force reload data
                ReloadData();
                break;
        }
    }


    /// <summary>
    /// UniGrid external data bound.
    /// </summary>
    protected object GridOnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "update":
                PlaceHolder plcUpd = new PlaceHolder();
                plcUpd.ID = "plcUdateAction";

                // Add disabled image
                ImageButton imgUpdate = new ImageButton();
                imgUpdate.ID = "imgUpdate";

                if (!this.Enabled || !this.EnableUpdate)
                {
                    imgUpdate.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploaddisabled.png", IsLiveSite));
                    imgUpdate.Style.Add("cursor", "default");
                    imgUpdate.Enabled = false;
                }
                else
                {
                    imgUpdate.Visible = false;
                }

                plcUpd.Controls.Add(imgUpdate);

                // Dynamically load uploader control
                DirectFileUploader dfuElem = Page.LoadControl("~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx") as DirectFileUploader;

                // Set uploader's properties
                if (dfuElem != null)
                {
                    if (this.Enabled && this.EnableUpdate)
                    {
                        dfuElem.ID = "dfuElem" + this.LibraryID;
                        dfuElem.DisplayInline = true;
                        dfuElem.SourceType = MediaSourceEnum.MediaLibraries;
                        dfuElem.MediaFileID = this.MediaFileID;
                        dfuElem.LibraryID = this.LibraryID;
                        dfuElem.LibraryFolderPath = this.LibraryFolderPath;
                        dfuElem.ParentElemID = this.ClientID;
                        dfuElem.IsMediaThumbnail = this.IsMediaThumbnail;
                        dfuElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload.png", IsLiveSite));
                        dfuElem.ImageHeight = 16;
                        dfuElem.ImageWidth = 16;
                        dfuElem.InsertMode = false;
                        dfuElem.ForceLoad = true;
                        dfuElem.IsLiveSite = IsLiveSite;
                        // New settings added
                        dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
                        dfuElem.Height = 16;
                        dfuElem.Width = 16;
                        dfuElem.MaxNumberToUpload = 1;
                    }
                    else
                    {
                        dfuElem.Visible = false;
                    }

                    plcUpd.Controls.Add(dfuElem);
                }
                return plcUpd;

            case "edit":
                // Get file extension
                if ((this.FileInfo != null) && (this.LibraryInfo != null))
                {
                    ImageButton img = (ImageButton)sender;

                    if (CMSContext.CurrentUser.IsAuthenticated())
                    {
                        string fileExt = (this.IsMediaThumbnail ? previewExt : this.FileInfo.FileExtension);

                        // If the file is not an image don't allow image editing
                        if (!ImageHelper.IsSupportedByImageEditor(fileExt) || !this.Enabled)
                        {
                            // Disable edit icon in case that attachment is not an image
                            img.ImageUrl = ResolveUrl(GetImageUrl("Design/editdisabled.png", IsLiveSite));
                            img.Enabled = false;
                            img.Style.Add("cursor", "default");
                        }
                        else
                        {
                            string query = string.Format("?refresh=1&siteid={0}&MediaFileGUID={1}{2}", LibraryInfo.LibrarySiteID, FileInfo.FileGUID, (IsMediaThumbnail ? "&isPreview=1" : ""));
                            query = URLHelper.AddUrlParameter(query, "hash", QueryHelper.GetHash(query));
                            img.OnClientClick = "EditThumbnailImage('" + query + "'); return false;";
                        }
                        img.AlternateText = GetString("general.edit");
                    }
                    else
                    {
                        img.Visible = false;
                    }
                }
                break;

            case "delete":
                ImageButton imgDelete = (ImageButton)sender;

                if (!this.Enabled)
                {
                    // Disable delete icon in case that editing is not allowed
                    imgDelete.ImageUrl = ResolveUrl(GetImageUrl("Design/deletedisabled.png", IsLiveSite));
                    imgDelete.Enabled = false;
                    imgDelete.Style.Add("cursor", "default");
                }
                else
                {
                    // Turn off validation
                    imgDelete.CausesValidation = false;

                    // Explicitly initialize confirmation
                    imgDelete.OnClientClick = "if (DeleteConfirmation() == false) { return false; }";
                }

                break;

            case "filename":
                if ((this.LibraryInfo != null) && (this.FileInfo != null))
                {
                    string fileUrl = "";
                    string fileExt = "";
                    string fileName = "";

                    // Get file extension
                    if (this.IsMediaThumbnail)
                    {
                        fileName = previewName;
                        fileExt = previewExt;
                        fileUrl = ResolveUrl("~/CMSPages/GetMediaFile.aspx?preview=1&fileguid=" + this.FileInfo.FileGUID.ToString());
                    }
                    else
                    {
                        fileExt = this.FileInfo.FileExtension;
                        fileName = this.FileInfo.FileName;
                        fileUrl = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(this.FileInfo.FileGUID, this.FileInfo.FileName);
                    }
                    fileUrl = URLHelper.UpdateParameterInUrl(fileUrl, "chset", Guid.NewGuid().ToString());

                    string tooltip = null;
                    string iconUrl = GetFileIconUrl(fileExt, "List");
                    bool isImage = ImageHelper.IsImage(fileExt);

                    if (isImage)
                    {
                        tooltip = "";

                        if (File.Exists(previewPath))
                        {
                            FileStream file = FileStream.New(previewPath, FileMode.Open, FileAccess.Read);
                            System.Drawing.Image img = System.Drawing.Image.FromStream(file.SystemStream);
                            file.Close();
                            if (img != null)
                            {
                                int[] imgDims = ImageHelper.EnsureImageDimensions(0, 0, 150, img.Width, img.Height);
                                string setRTL = (CultureHelper.IsUICultureRTL() ? ", LEFT, true" : "");
                                tooltip = "onmouseout=\"UnTip()\" onmouseover=\"Tip('<div style=\\'width:" + imgDims[0] + "px; text-align:center;\\'><img src=\\'" + URLHelper.AddParameterToUrl(fileUrl, "maxsidesize", "150") + "\\' alt=\\'" + fileName + "\\' /></div>'" + setRTL + ")\"";
                                // Dispose image
                                img.Dispose();
                            }
                        }
                    }

                    string imageTag = "<img class=\"Icon\" src=\"" + iconUrl + "\" alt=\"" + fileName + "\" />";
                    if (isImage)
                    {
                        return "<a href=\"#\" onclick=\"javascript: window.open('" + fileUrl + "'); return false;\"><span " + tooltip + ">" + imageTag + fileName + "</span></a>";
                    }
                    else
                    {
                        return "<a href=\"" + fileUrl + "\">" + imageTag + fileName + "</a>";
                    }
                }

                return "";

            case "filesize":
                return DataHelper.GetSizeString(ValidationHelper.GetLong(parameter, 0));
        }

        return parameter;
    }

    #endregion
}