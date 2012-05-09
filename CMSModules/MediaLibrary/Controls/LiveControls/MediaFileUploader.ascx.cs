using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.ExtendedControls;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_LiveControls_MediaFileUploader : CMSAdminControl
{
    #region "Variables"

    private int mLibraryId = 0;
    private string mDestinationPath = null;
    private bool mEnableUploadPreview;
    private string mPreviewSuffix;

    #endregion


    #region "Properties"

    /// <summary>
    /// Delegate of event fired when file has been uploaded.
    /// </summary>
    public delegate void OnAfterFileUploadEventHandler();

    /// <summary>
    /// Event raised when file has been uploaded.
    /// </summary>
    public event OnAfterFileUploadEventHandler OnAfterFileUpload;


    /// <summary>
    /// Gets or sets ID of the media library where the file should be uploaded.
    /// </summary>
    public int LibraryID
    {
        get
        {
            return this.mLibraryId;
        }
        set
        {
            this.mLibraryId = value;
        }
    }


    /// <summary>
    /// Gets or sets the destination path within the media library.
    /// </summary>
    public string DestinationPath
    {
        get
        {
            return this.mDestinationPath;
        }
        set
        {
            this.mDestinationPath = value;
        }
    }


    /// <summary>
    /// Idicates if preview upload dialog shoul displayed.
    /// </summary>
    public bool EnableUploadPreview
    {
        get
        {
            return mEnableUploadPreview;
        }
        set
        {
            mEnableUploadPreview = value;
        }
    }


    /// <summary>
    /// Preview suffix for identification of preview file.
    /// </summary>
    public string PreviewSuffix
    {
        get
        {
            return mPreviewSuffix;
        }
        set
        {
            mPreviewSuffix = value;
        }
    }



    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnUpload.Click += new EventHandler(btnUpload_Click);
        ControlsHelper.RegisterPostbackControl(this.btnUpload);

        // Show preview upload
        if (this.EnableUploadPreview)
        {
            this.plcPreview.Visible = true;
            this.cellUpload.RowSpan = 2;
        }

        this.mfuDirectUploader.EventTarget = this.btnHidden.UniqueID;
        this.mfuDirectUploader.MediaLibraryID = this.LibraryID;
        this.mfuDirectUploader.MediaFolderPath = this.DestinationPath;
    }


    protected void btnHidden_Click(object sender, EventArgs e)
    {
        // Clear cache
        if (CMSControlsHelper.CurrentPageManager != null)
        {
            CMSControlsHelper.CurrentPageManager.ClearCache();
        }

        // Display info to the user
        this.lblInfo.Text = GetString("media.fileuploaded");
        this.lblInfo.Visible = true;

        if (OnAfterFileUpload != null)
        {
            OnAfterFileUpload();
        }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryID);
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "manage"))
        {
            // Check 'File create' permission
            if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "filecreate"))
            {
                RaiseOnNotAllowed("filecreate");
                return;
            }
        }

        if (!fileUploader.HasFile)
        {
            this.lblError.Text = GetString("media.selectfile");
            this.lblError.Visible = true;
            return;
        }

        // Check if preview file is image
        if ((previewUploader.HasFile) &&
            (!ImageHelper.IsImage(Path.GetExtension(previewUploader.FileName))) &&
            (Path.GetExtension(previewUploader.FileName).ToLower() != ".ico") &&
            (Path.GetExtension(previewUploader.FileName).ToLower() != ".tif") &&
            (Path.GetExtension(previewUploader.FileName).ToLower() != ".tiff") &&
            (Path.GetExtension(previewUploader.FileName).ToLower() != ".wmf"))
        {
            this.lblError.Text = GetString("Media.File.PreviewIsNotImage");
            this.lblError.Visible = true;
            return;
        }

        // Check if the preview file with given extension is allowed for library module
        // Check if file with given extension is allowed for library module
        string fileExt = Path.GetExtension(this.fileUploader.FileName).TrimStart('.');
        string previewFileExt = Path.GetExtension(this.previewUploader.FileName).TrimStart('.');

        // Check file extension
        if (!MediaLibraryHelper.IsExtensionAllowed(fileExt))
        {
            this.lblError.Text = String.Format(GetString("media.newfile.extensionnotallowed"), fileExt);
            this.lblError.Visible = true;
            return;
        }

        // Check preview extension
        if ((previewFileExt.Trim() != "") && !MediaLibraryHelper.IsExtensionAllowed(previewFileExt))
        {
            this.lblError.Text = String.Format(GetString("media.newfile.extensionnotallowed"), previewFileExt);
            this.lblError.Visible = true;
            return;
        }

        if (mli != null)
        {
            try
            {
                // Create new Media file
                MediaFileInfo mfi = new MediaFileInfo(fileUploader.PostedFile, this.LibraryID, this.DestinationPath);

                // Save preview if presented
                if (previewUploader.HasFile)
                {
                    // Get preview suffix if not set
                    if (String.IsNullOrEmpty(this.PreviewSuffix))
                    {
                        this.PreviewSuffix = MediaLibraryHelper.GetMediaFilePreviewSuffix(CMSContext.CurrentSiteName);
                    }

                    if (!String.IsNullOrEmpty(this.PreviewSuffix))
                    {
                        // Get physical path whithin the media library
                        String path = null;
                        if (this.DestinationPath.TrimEnd('/') != "")
                        {
                            path = DirectoryHelper.CombinePath(this.DestinationPath.Trim('/').Replace('/', '\\'), MediaLibraryHelper.GetMediaFileHiddenFolder(CMSContext.CurrentSiteName));
                        }
                        else
                        {
                            path = MediaLibraryHelper.GetMediaFileHiddenFolder(CMSContext.CurrentSiteName);
                        }

                        string previewExtension = Path.GetExtension(previewUploader.PostedFile.FileName);
                        string previewName = Path.GetFileNameWithoutExtension(MediaLibraryHelper.GetPreviewFileName(mfi.FileName, mfi.FileExtension, previewExtension, CMSContext.CurrentSiteName, this.PreviewSuffix));

                        // Save preview file
                        MediaFileInfoProvider.SaveFileToDisk(CMSContext.CurrentSiteName, mli.LibraryFolder, path, previewName, previewExtension, mfi.FileGUID, previewUploader.PostedFile.InputStream, false);
                    }

                }

                // Save record to the database
                MediaFileInfoProvider.SetMediaFileInfo(mfi);

                // Clear cache
                if (CMSControlsHelper.CurrentPageManager != null)
                {
                    CMSControlsHelper.CurrentPageManager.ClearCache();
                }

                // Display info to the user
                this.lblInfo.Text = GetString("media.fileuploaded");
                this.lblInfo.Visible = true;

                if (OnAfterFileUpload != null)
                {
                    OnAfterFileUpload();
                }
            }
            catch (Exception ex)
            {
                this.lblError.Visible = true;
                this.lblError.Text = ex.Message;
            }
        }
    }
}
