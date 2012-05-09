using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSWebParts_MediaLibrary_MediaFileUploader : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets name of the media library where the file should be uploaded.
    /// </summary>
    public string LibraryName
    {
        get
        {
            string libraryName = ValidationHelper.GetString(this.GetValue("LibraryName"), String.Empty);
            if ((string.IsNullOrEmpty(libraryName) || libraryName == MediaLibraryInfoProvider.CURRENT_LIBRARY) && (MediaLibraryContext.CurrentMediaLibrary != null))
            {
                return MediaLibraryContext.CurrentMediaLibrary.LibraryName;
            }
            return libraryName;
        }
        set
        {
            this.SetValue("LibraryName", value);
        }
    }


    /// <summary>
    /// Gets or sets the destination path within the media library.
    /// </summary>
    public string DestinationPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DestinationPath"), "");
        }
        set
        {
            this.SetValue("DestinationPath", value);
        }
    }


    /// <summary>
    /// Idicates if preview upload dialog shoul displayed.
    /// </summary>
    public bool EnableUploadPreview
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableUploadPreview"), false);
        }
        set
        {
            this.SetValue("EnableUploadPreview", value);
        }
    }


    /// <summary>
    /// Preview suffix for identification of preview file.
    /// </summary>
    public string PreviewSuffix
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PreviewSuffix"), "");
        }
        set
        {
            this.SetValue("PreviewSuffix", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            this.uploader.StopProcessing = true;
        }
        else
        {
            MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryName, CMSContext.CurrentSiteName);
            if (mli != null)
            {
                this.uploader.LibraryID = mli.LibraryID;
                this.uploader.DestinationPath = this.DestinationPath;
                this.uploader.EnableUploadPreview = this.EnableUploadPreview;
                this.uploader.PreviewSuffix = this.PreviewSuffix;
                this.uploader.OnNotAllowed += new CMS.UIControls.CMSAdminControl.NotAllowedEventHandler(uploader_OnNotAllowed);
            }
        }
    }


    private void uploader_OnNotAllowed(string permissionType, CMSAdminControl sender)
    {
        if (sender != null)
        {
            sender.StopProcessing = true;
        }
        uploader.StopProcessing = true;
        uploader.Visible = false;
        messageElem.ErrorMessage = MediaLibraryHelper.GetAccessDeniedMessage("filecreate");
        messageElem.DisplayMessage = true;
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }
}
