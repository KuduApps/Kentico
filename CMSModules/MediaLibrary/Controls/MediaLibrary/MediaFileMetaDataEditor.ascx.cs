using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.EventLog;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.IO;
using CMS.SiteProvider;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileMetaDataEditor : CMSUserControl
{
    #region "Variables"

    private MediaFileInfo mediaFileInfo = null;
    private string mSiteName = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Object type.
    /// </summary>
    public string ObjectType
    {
        get;
        set;
    }


    /// <summary>
    /// Object GUID.
    /// </summary>
    public Guid ObjectGuid
    {
        get;
        set;
    }


    /// <summary>
    /// Site name. If is null, return current site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return mSiteName ?? (mSiteName = CMSContext.CurrentSiteName);
        }
        set
        {
            mSiteName = value;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Gets object extension.
    /// </summary>
    /// <param name="extension">Object extension</param>
    public delegate void OnGetObjectExtension(string extension);
    public event OnGetObjectExtension GetObjectExtension;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set properties
        metaDataEditor.ObjectGuid = ObjectGuid;
        metaDataEditor.ObjectType = ObjectType;
        metaDataEditor.SiteName = SiteName;

        // Register events
        metaDataEditor.InitializeObject += new CMSAdminControls_ImageEditor_MetaDataEditor.OnInitializeObject(metaDataEditor_InitializeObject);
        metaDataEditor.OnSetMetaData += metaDataEditor_SetMetaData;
        metaDataEditor.Save += new CMSAdminControls_ImageEditor_MetaDataEditor.OnSave(metaDataEditor_Save);
    }


    /// <summary>
    /// Initializes media file info.
    /// </summary>
    /// <param name="objectGuid">Media file GUID</param>
    /// <param name="siteName">Site name</param>
    private void metaDataEditor_InitializeObject(Guid objectGuid, string siteName)
    {
        // Get mediafile
        mediaFileInfo = MediaFileInfoProvider.GetMediaFileInfo(objectGuid, siteName);

        // If media file is not null 
        if (mediaFileInfo != null)
        {
            MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(ValidationHelper.GetInteger(mediaFileInfo.FileLibraryID, 0));

            // Check permission 'FileModify'
            if (metaDataEditor.CheckPermissions && !MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "filemodify"))
            {
                RedirectToAccessDenied(GetString("metadata.errors.filemodify"));
            }

            // Fire event GetObjectExtension
            if (GetObjectExtension != null)
            {
                GetObjectExtension(mediaFileInfo.FileExtension);
            }
        }
        else
        {
            RedirectToInformation(GetString("editedobject.notexists"));
        }
    }


    /// <summary>
    /// Sets metadata.
    /// </summary>
    private void metaDataEditor_SetMetaData(object sender, EventArgs e)
    {
        if (mediaFileInfo != null)
        {
            metaDataEditor.ObjectTitle = mediaFileInfo.FileTitle;
            metaDataEditor.ObjectDescription = mediaFileInfo.FileDescription;
            metaDataEditor.ObjectFileName = mediaFileInfo.FileName;
            metaDataEditor.ObjectExtension = mediaFileInfo.FileExtension;
            metaDataEditor.ObjectSize = DataHelper.GetSizeString(mediaFileInfo.FileSize);
        }
    }


    /// <summary>
    /// Save title and description of media file info.
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="title">Title</param>
    /// <param name="description">Description</param>
    private bool metaDataEditor_Save(string fileName, string title, string description)
    {
        bool saved = false;

        if (mediaFileInfo != null)
        {
            try
            {
                string extension = mediaFileInfo.FileExtension;

                if (mediaFileInfo.FileName != fileName)
                {
                    // Get original file path
                    string path = MediaFileInfoProvider.GetMediaFilePath(mediaFileInfo.FileLibraryID, mediaFileInfo.FilePath);
                    // New file path
                    string filePath = DirectoryHelper.CombinePath(Path.GetDirectoryName(mediaFileInfo.FilePath), fileName + extension);
                    string newPath = MediaFileInfoProvider.GetMediaFilePath(mediaFileInfo.FileLibraryID, filePath);

                    // Rename file
                    if (!File.Exists(newPath))
                    {
                        File.Move(path, newPath);
                    }
                    else
                    {
                        // File already exists.
                        metaDataEditor.LabelError = GetString("img.errors.fileexists");
                        return false;
                    }

                    mediaFileInfo.FileName = fileName;

                    string subFolderPath = null;

                    int lastSlash = mediaFileInfo.FilePath.LastIndexOf('/');
                    if (lastSlash > 0)
                    {
                        subFolderPath = mediaFileInfo.FilePath.Substring(0, lastSlash);
                    }

                    if (!string.IsNullOrEmpty(subFolderPath))
                    {
                        mediaFileInfo.FilePath = subFolderPath + "/" + fileName + extension;
                    }
                    else
                    {
                        mediaFileInfo.FilePath = fileName + extension;
                    }
                }
                mediaFileInfo.FileTitle = title;
                mediaFileInfo.FileDescription = description;

                // Save new data
                MediaFileInfoProvider.SetMediaFileInfo(mediaFileInfo, false);

                saved = true;
            }
            catch (Exception ex)
            {
                metaDataEditor.LabelError = GetString("metadata.errors.processing");
                EventLogProvider.LogException("Metadata editor", "SAVE", ex);
            }
        }

        return saved;
    }


    /// <summary>
    /// Saves metadata of media file (title, description).
    /// </summary>
    /// <returns>Returns True if media file was succesfully saved.</returns>
    public bool SaveMetadata()
    {
        return metaDataEditor.SaveMetadata();
    }

    #endregion
}
