using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.SettingsProvider;
using CMS.DataEngine;

public partial class CMSModules_MediaLibrary_Controls_Dialogs_DirectFileUploader_FileUpload : FileUpload
{
    #region "Variables"

    private MediaLibraryInfo mLibraryInfo = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Library info uploaded file is related to.
    /// </summary>
    private MediaLibraryInfo LibraryInfo
    {
        get
        {
            if (LibraryID > 0)
            {
                mLibraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(LibraryID);
            }
            return mLibraryInfo;
        }
        set
        {
            mLibraryInfo = value;
        }
    }


    /// <summary>
    /// File upload user control.
    /// </summary>
    public override CMSFileUpload FileUploadControl
    {
        get
        {
            return ucFileUpload;
        }
    }

    #endregion


    #region "Button handling"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Library info initialization
        LibraryID = QueryHelper.GetInteger("libraryid", 0);
        MediaFileID = QueryHelper.GetInteger("mediafileid", 0);
        MediaFileName = QueryHelper.GetString("filename", null);
        IsMediaThumbnail = QueryHelper.GetBoolean("ismediathumbnail", false);
        LibraryFolderPath = QueryHelper.GetString("path", "");
        IncludeNewItemInfo = QueryHelper.GetBoolean("includeinfo", false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterWOpenerScript(Page);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = (lblError.Text != "");
    }

    protected void btnOK_Click(object senderObject, EventArgs e)
    {
        if (!ucFileUpload.HasFile)
        {
            lblError.Text = GetString("attach.selectfile");
        }
        else
        {
            switch (this.SourceType)
            {
                case MediaSourceEnum.MediaLibraries:
                    HandleLibrariesUpload();
                    break;

                default:
                    break;
            }
        }
    }

    #endregion


    #region "Media libraries upload"

    /// <summary>
    /// Provides operations necessary to create and store new library file.
    /// </summary>
    private void HandleLibrariesUpload()
    {
        // Get related library info        
        if (LibraryInfo != null)
        {
            MediaFileInfo mediaFile = null;

            // Get the site name
            string siteName = CMSContext.CurrentSiteName;
            SiteInfo si = SiteInfoProvider.GetSiteInfo(LibraryInfo.LibrarySiteID);
            if (si != null)
            {
                siteName = si.SiteName;
            }

            string message = string.Empty;
            try
            {
                // Check the allowed extensions
                CheckAllowedExtensions();

                if (MediaFileID > 0)
                {
                    #region "Check permissions"

                    if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "FileModify"))
                    {
                        throw new Exception(GetString("media.security.nofilemodify"));
                    }

                    #endregion

                    mediaFile = MediaFileInfoProvider.GetMediaFileInfo(MediaFileID);
                    if (mediaFile != null)
                    {
                        // Ensure object version
                        SynchronizationHelper.EnsureObjectVersion(mediaFile);

                        if (IsMediaThumbnail)
                        {
                            string newFileExt = Path.GetExtension(ucFileUpload.FileName).TrimStart('.');
                            if ((ImageHelper.IsImage(newFileExt)) && (newFileExt.ToLower() != "ico") &&
                                (newFileExt.ToLower() != "wmf"))
                            {
                                // Update or creation of Media File update
                                string previewSuffix = MediaLibraryHelper.GetMediaFilePreviewSuffix(siteName);

                                if (!String.IsNullOrEmpty(previewSuffix))
                                {
                                    string previewExtension = Path.GetExtension(ucFileUpload.PostedFile.FileName);
                                    string previewName = Path.GetFileNameWithoutExtension(MediaLibraryHelper.GetPreviewFileName(mediaFile.FileName, mediaFile.FileExtension, previewExtension, siteName, previewSuffix));
                                    string previewFolder = DirectoryHelper.CombinePath(MediaLibraryHelper.EnsurePath(LibraryFolderPath.TrimEnd('/')), MediaLibraryHelper.GetMediaFileHiddenFolder(siteName));

                                    byte[] previewFileBinary = new byte[ucFileUpload.PostedFile.ContentLength];
                                    ucFileUpload.PostedFile.InputStream.Read(previewFileBinary, 0, ucFileUpload.PostedFile.ContentLength);

                                    // Delete current preview thumbnails
                                    MediaFileInfoProvider.DeleteMediaFilePreview(siteName, mediaFile.FileLibraryID, mediaFile.FilePath, false);

                                    // Save preview file
                                    MediaFileInfoProvider.SaveFileToDisk(siteName, LibraryInfo.LibraryFolder, previewFolder, previewName, previewExtension, mediaFile.FileGUID, previewFileBinary, false, false);

                                    // Log synchronization task
                                    SynchronizationHelper.LogObjectChange(mediaFile, TaskTypeEnum.UpdateObject);
                                }

                                // Drop the cache dependencies
                                CacheHelper.TouchKeys(MediaFileInfoProvider.GetDependencyCacheKeys(mediaFile, true));
                            }
                            else
                            {
                                message = GetString("media.file.onlyimgthumb");
                            }
                        }
                        else
                        {
                            // Delete existing media file
                            MediaFileInfoProvider.DeleteMediaFile(LibraryInfo.LibrarySiteID, LibraryInfo.LibraryID, mediaFile.FilePath, true, false);

                            // Update media file preview
                            if (MediaLibraryHelper.HasPreview(siteName, LibraryInfo.LibraryID, mediaFile.FilePath))
                            {
                                // Get new file path
                                string newPath = DirectoryHelper.CombinePath(Path.GetDirectoryName(mediaFile.FilePath).TrimEnd('/'), ucFileUpload.PostedFile.FileName);
                                newPath = MediaLibraryHelper.EnsureUniqueFileName(newPath);

                                // Get new unique file name
                                string newName = Path.GetFileName(newPath);

                                // Rename preview
                                MediaLibraryHelper.MoveMediaFilePreview(mediaFile, newName);

                                // Delete preview thumbnails
                                MediaFileInfoProvider.DeleteMediaFilePreviewThumbnails(mediaFile);
                            }

                            // Receive media info on newly posted file
                            mediaFile = GetUpdatedFile(mediaFile.Generalized.DataClass);

                            MediaFileInfoProvider.SetMediaFileInfo(mediaFile);
                        }
                    }
                }
                else
                {
                    // Creation of new media file

                    #region "Check permissions"

                    if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(LibraryInfo, "FileCreate"))
                    {
                        throw new Exception(GetString("media.security.nofilecreate"));
                    }

                    #endregion

                    // No file for upload specified
                    if (!ucFileUpload.HasFile)
                    {
                        throw new Exception(GetString("media.newfile.errorempty"));
                    }

                    // Create new media file record
                    mediaFile = new MediaFileInfo(ucFileUpload.PostedFile, LibraryID, LibraryFolderPath, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize, LibraryInfo.LibrarySiteID);

                    mediaFile.FileDescription = "";

                    // Save the new file info
                    MediaFileInfoProvider.SetMediaFileInfo(mediaFile);

                }
            }
            catch (Exception ex)
            {
                // Creation of new media file failed
                message = ex.Message;
            }
            finally
            {
                // Create media file info string
                string mediaInfo = "";
                if ((mediaFile != null) && (mediaFile.FileID > 0) && (IncludeNewItemInfo))
                {
                    mediaInfo = mediaFile.FileID + "|" + LibraryFolderPath.Replace('\\', '>').Replace("'", "\\'");
                }

                // Ensure message text
                message = HTMLHelper.EnsureLineEnding(message, " ");

                // Call function to refresh parent window                                                     
                ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "refresh", ScriptHelper.GetScript("if ((wopener.parent != null) && (wopener.parent.InitRefresh_" + ParentElemID + " != null)){wopener.parent.InitRefresh_" + ParentElemID + "(" + ScriptHelper.GetString(message.Trim()) + ", false" + ((mediaInfo != "") ? ", '" + mediaInfo + "'" : "") + (InsertMode ? ", 'insert'" : ", 'update'") + ");}window.close();"));
            }
        }
    }


    /// <summary>
    /// Gets media file info object representing the updated version of original file.
    /// </summary>
    /// <param name="originalFile">Original file data</param>
    private MediaFileInfo GetUpdatedFile(IDataClass originalFile)
    {
        // Get info on media file from uploaded file
        MediaFileInfo mediaFile = new MediaFileInfo(ucFileUpload.PostedFile, LibraryID, LibraryFolderPath, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize, LibraryInfo.LibrarySiteID);

        // Create new file based on original
        MediaFileInfo updatedMediaFile = new MediaFileInfo(originalFile);

        // Update necessary information
        updatedMediaFile.FileID = MediaFileID;
        updatedMediaFile.FileName = mediaFile.FileName;
        updatedMediaFile.FileExtension = mediaFile.FileExtension;
        updatedMediaFile.FileSize = mediaFile.FileSize;
        updatedMediaFile.FileMimeType = mediaFile.FileMimeType;
        updatedMediaFile.FilePath = mediaFile.FilePath;
        updatedMediaFile.FileCreatedWhen = mediaFile.FileCreatedWhen;
        updatedMediaFile.FileCreatedByUserID = mediaFile.FileCreatedByUserID;
        updatedMediaFile.FileModifiedByUserID = mediaFile.FileModifiedByUserID;
        updatedMediaFile.FileBinary = mediaFile.FileBinary;
        updatedMediaFile.FileImageHeight = mediaFile.FileImageHeight;
        updatedMediaFile.FileImageWidth = mediaFile.FileImageWidth;
        updatedMediaFile.FileBinaryStream = mediaFile.FileBinaryStream;

        return updatedMediaFile;
    }

    #endregion
}
