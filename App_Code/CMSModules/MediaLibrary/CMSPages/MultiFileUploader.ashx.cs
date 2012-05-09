using System;
using System.Collections.Generic;
using System.Web;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.IO;
using CMS.MediaLibrary;
using CMS.Synchronization;

namespace MultiFileUploader
{
    /// <summary>
    /// Multifile media library uploader class for Http handler.
    /// </summary>
    public class MediaLibraryUploader : IHttpHandler
    {
        #region "Properties"

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion


        #region "Public Methods"

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // Get arguments passed via query string
                UploaderHelper args = new UploaderHelper(context);
                String appPath = context.Server.MapPath("~/");
                DirectoryHelper.EnsureDiskPath(args.FilePath, appPath);

                if (args.Canceled)
                {
                    // Remove file from server if canceled
                    args.CleanTempFile();
                }
                else
                {
                    args.ProcessFile();
                    if (args.Complete)
                    {
                        if (args.IsMediaLibraryUpload)
                        {
                            HandleMediaLibraryUpload(args, context);
                        }
                        args.CleanTempFile();
                    }
                }
            }
            catch (Exception ex)
            {
                // Send error message
                context.Response.Write(String.Format(@"0|{0}", HTMLHelper.EnsureLineEnding(ex.Message, " ")));
                context.Response.Flush();
            }
        }

        #endregion


        #region "Private Methods"

        /// <summary>
        /// Provides operations necessary to create and store new files in media library.
        /// </summary>
        /// <param name="args">Upload arguments.</param>
        /// <param name="context">HttpContext instance.</param>
        private void HandleMediaLibraryUpload(UploaderHelper args, HttpContext context)
        {
            string uploadPath = Path.Combine(MediaLibraryInfoProvider.GetMediaLibraryFolderPath(args.MediaLibraryArgs.LibraryID), args.MediaLibraryArgs.FolderPath);
            string filePath = Path.Combine(uploadPath, args.FileName);

            MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(args.MediaLibraryArgs.LibraryID);
            if (mli != null)
            {
                MediaFileInfo mediaFile = null;

                // Get the site name
                SiteInfo si = SiteInfoProvider.GetSiteInfo(mli.LibrarySiteID);
                string siteName = (si != null) ? si.SiteName : CMSContext.CurrentSiteName;

                try
                {
                    args.IsExtensionAllowed();

                    if (args.MediaLibraryArgs.MediaFileID > 0)
                    {
                        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "FileModify"))
                        {
                            throw new Exception(ResHelper.GetString("media.security.nofilemodify"));
                        }

                        mediaFile = MediaFileInfoProvider.GetMediaFileInfo(args.MediaLibraryArgs.MediaFileID);
                        if (mediaFile != null)
                        {
                            // Ensure object version
                            SynchronizationHelper.EnsureObjectVersion(mediaFile);

                            if (args.MediaLibraryArgs.IsMediaThumbnail)
                            {
                                if ((ImageHelper.IsImage(args.Extension)) && (args.Extension.ToLower() != "ico") && (args.Extension.ToLower() != "wmf"))
                                {
                                    // Update or creation of Media File update
                                    string previewSuffix = MediaLibraryHelper.GetMediaFilePreviewSuffix(siteName);

                                    if (!String.IsNullOrEmpty(previewSuffix))
                                    {
                                        //string previewExtension = Path.GetExtension(ucFileUpload.PostedFile.FileName);
                                        string previewName = Path.GetFileNameWithoutExtension(MediaLibraryHelper.GetPreviewFileName(mediaFile.FileName, mediaFile.FileExtension, args.Extension, siteName, previewSuffix));
                                        string previewFolder = String.Format("{0}\\{1}", MediaLibraryHelper.EnsurePath(args.MediaLibraryArgs.FolderPath.TrimEnd('/')), MediaLibraryHelper.GetMediaFileHiddenFolder(siteName));

                                        // This method is limited to 2^32 byte files (4.2 GB)
                                        using (FileStream fs = File.OpenRead(args.FilePath))
                                        {
                                            byte[] previewFileBinary = new byte[fs.Length];
                                            fs.Read(previewFileBinary, 0, Convert.ToInt32(fs.Length));

                                            // Delete current preview thumbnails
                                            MediaFileInfoProvider.DeleteMediaFilePreview(siteName, mediaFile.FileLibraryID, mediaFile.FilePath, false);

                                            // Save preview file
                                            MediaFileInfoProvider.SaveFileToDisk(siteName, mli.LibraryFolder, previewFolder, previewName, args.Extension, mediaFile.FileGUID, previewFileBinary, false, false);

                                            // Log synchronization task
                                            SynchronizationHelper.LogObjectChange(mediaFile, TaskTypeEnum.UpdateObject);
                                            fs.Close();
                                        }
                                    }
                                    // Drop the cache dependencies
                                    CacheHelper.TouchKeys(MediaFileInfoProvider.GetDependencyCacheKeys(mediaFile, true));
                                }
                                else
                                {
                                    args.Message = ResHelper.GetString("media.file.onlyimgthumb");
                                }
                            }
                            else
                            {
                                // Get folder path
                                string path = Path.GetDirectoryName(DirectoryHelper.CombinePath(MediaLibraryInfoProvider.GetMediaLibraryFolderPath(mli.LibraryID), mediaFile.FilePath));

                                // If file system permissions are sufficient for file update
                                if (DirectoryHelper.CheckPermissions(path, false, true, true, true))
                                {
                                    // Delete existing media file
                                    MediaFileInfoProvider.DeleteMediaFile(mli.LibrarySiteID, mli.LibraryID, mediaFile.FilePath, true, false);

                                    // Update media file preview
                                    if (MediaLibraryHelper.HasPreview(siteName, mli.LibraryID, mediaFile.FilePath))
                                    {
                                        // Get new unique file name
                                        string newName = URLHelper.GetSafeFileName(Path.GetFileName(args.Name), siteName);

                                        // Get new file path
                                        string newPath = DirectoryHelper.CombinePath(path, newName);
                                        newPath = MediaLibraryHelper.EnsureUniqueFileName(newPath);
                                        newName = Path.GetFileName(newPath);

                                        // Rename preview
                                        MediaLibraryHelper.MoveMediaFilePreview(mediaFile, newName);

                                        // Delete preview thumbnails
                                        MediaFileInfoProvider.DeleteMediaFilePreviewThumbnails(mediaFile);
                                    }

                                    // Receive media info on newly posted file
                                    mediaFile = GetUpdatedFile(mediaFile.Generalized.DataClass, args, mli);

                                    MediaFileInfoProvider.SetMediaFileInfo(mediaFile);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Check 'File create' permission
                        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "filecreate"))
                        {
                            throw new Exception(ResHelper.GetString("media.security.nofilecreate"));
                        }
                        mediaFile = new MediaFileInfo(args.FilePath, args.MediaLibraryArgs.LibraryID, args.MediaLibraryArgs.FolderPath, args.ResizeToWidth, args.ResizeToHeight, args.ResizeToMaxSide);
                        MediaFileInfoProvider.SetMediaFileInfo(mediaFile);
                    }
                }
                catch (Exception ex)
                {
                    // Creation of new media file failed
                    args.Message = ex.Message;
                    // The CMS.IO library uses reflection to create an instance of file stream
                    if (ex is System.Reflection.TargetInvocationException)
                    {
                        if (ex.InnerException != null && !String.IsNullOrEmpty(ex.Message))
                        {
                            args.Message = ex.InnerException.Message;
                        }
                    }
                }
                finally
                {
                    if (args.RaiseOnClick)
                    {
                        args.AfterScript += string.Format(@"
                        if(window.UploaderOnClick) 
                        {{
                            window.UploaderOnClick('{0}');
                        }}", args.MediaLibraryArgs.MediaFileName.Replace(" ", "").Replace(".", "").Replace("-", ""));
                    }

                    // Create media library info string
                    string mediaInfo = ((mediaFile != null) && (mediaFile.FileID > 0) && (args.IncludeNewItemInfo)) ? String.Format("'{0}|{1}', ", mediaFile.FileID, args.MediaLibraryArgs.FolderPath.Replace('\\', '>').Replace("'", "\\'")) : "";

                    // Create after script and return it to the silverlight application, this script will be evaluated by the SL application in the end
                    args.AfterScript += string.Format(@"
                    if (window.InitRefresh_{0})
                    {{
                        window.InitRefresh_{0}('{1}', false, {2});
                    }}
                    else {{ 
                        if ('{1}' != '') {{
                            alert('{1}');
                        }}
                    }}", args.ParentElementID, ScriptHelper.GetString(args.Message.Trim(), false), mediaInfo + (args.IsInsertMode ? "'insert'" : "'update'"));

                    args.AddEventTargetPostbackReference();
                    context.Response.Write(args.AfterScript);
                    context.Response.Flush();
                }
            }
        }


        /// <summary>
        /// Gets media file info object representing the updated version of original file
        /// </summary>
        private MediaFileInfo GetUpdatedFile(CMS.DataEngine.IDataClass origFileDataClass, UploaderHelper args, MediaLibraryInfo mli)
        {
            // Get info on media file from uploaded file
            MediaFileInfo mediaFile = new MediaFileInfo(args.FilePath, args.MediaLibraryArgs.LibraryID, args.MediaLibraryArgs.FolderPath, args.ResizeToWidth, args.ResizeToHeight, args.ResizeToMaxSide);

            // Create new file based on original
            MediaFileInfo updatedMediaFile = new MediaFileInfo(origFileDataClass)
            {
                // Update necessary information
                FileName = mediaFile.FileName,
                FileExtension = mediaFile.FileExtension,
                FileSize = mediaFile.FileSize,
                FileMimeType = mediaFile.FileMimeType,
                FilePath = mediaFile.FilePath,
                FileModifiedByUserID = mediaFile.FileModifiedByUserID,
                FileBinary = mediaFile.FileBinary,
                FileImageHeight = mediaFile.FileImageHeight,
                FileImageWidth = mediaFile.FileImageWidth,
                FileBinaryStream = mediaFile.FileBinaryStream
            };

            return updatedMediaFile;
        }

        #endregion
    }
}