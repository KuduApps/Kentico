using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.PortalControls;
using CMS.MediaLibrary;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;

public partial class CMSWebParts_MediaLibrary_MediaGalleryFolderTree : CMSAbstractWebPart
{
    #region "Library properties"

    /// <summary>
    /// Gets or sets media library path to display files from.
    /// </summary>
    public string MediaLibraryPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MediaLibraryPath"), String.Empty);
        }
        set
        {
            this.SetValue("MediaLibraryPath", value);
        }
    }


    /// <summary>
    /// Gets or sets media library name.
    /// </summary>
    public string MediaLibraryName
    {
        get
        {
            string libraryName = ValidationHelper.GetString(this.GetValue("MediaLibraryName"), "");
            if ((string.IsNullOrEmpty(libraryName) || libraryName == MediaLibraryInfoProvider.CURRENT_LIBRARY) && (MediaLibraryContext.CurrentMediaLibrary != null))
            {
                return MediaLibraryContext.CurrentMediaLibrary.LibraryName;
            }
            return libraryName;
        }
        set
        {
            this.SetValue("MediaLibraryName", value);
        }
    }


    /// <summary>
    /// Indicates if files count should be displayed.
    /// </summary>
    public bool DisplayFileCount
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayFileCount"), this.folderTree.DisplayFileCount);
        }
        set
        {
            this.SetValue("DisplayFileCount", value);
            this.folderTree.DisplayFileCount = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), "");
        }
        set
        {
            this.SetValue("FilterName", value);
            this.folderTree.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the file id querysting parameter.
    /// </summary>
    public string FileIDQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FileIDQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("FileIDQueryStringKey", value);
            this.folderTree.FileIDQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the path querysting parameter.
    /// </summary>
    public string PathQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PathQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("PathQueryStringKey", value);
            this.folderTree.PathQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the filter method.
    /// </summary>
    public int FilterMethod
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("FilterMethod"), 0);
        }
        set
        {
            this.SetValue("FilterMethod", value);
            this.folderTree.FilterMethod = value;
        }
    }

    #endregion


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

        MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.MediaLibraryName, CMSContext.CurrentSiteName);

        if (mli != null)
        {
            // If dont have 'Manage' permission
            if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "read"))
            {
                // Check 'File create' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "libraryaccess"))
                {
                    folderTree.StopProcessing = true;
                    folderTree.Visible = false;
                    messageElem.ErrorMessage = MediaLibraryHelper.GetAccessDeniedMessage("libraryaccess");
                    messageElem.DisplayMessage = true;
                    return;
                }
            }

            // Tree
            if (string.IsNullOrEmpty(this.MediaLibraryPath))
            {
                this.folderTree.RootFolderPath = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName);
                this.folderTree.MediaLibraryFolder = mli.LibraryFolder;
            }
            else
            {
                this.folderTree.RootFolderPath = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName) + mli.LibraryFolder;
                int index = this.MediaLibraryPath.LastIndexOf('/');
                if ((index > -1) && (this.MediaLibraryPath.Length > (index + 1)))
                {
                    this.folderTree.MediaLibraryFolder = this.MediaLibraryPath.Substring(index + 1);
                }
                else
                {
                    this.folderTree.MediaLibraryFolder = this.MediaLibraryPath;
                }
                this.folderTree.MediaLibraryPath = this.MediaLibraryPath.Replace("/", "\\");
            }

            // Set images path
            if (CultureHelper.IsPreferredCultureRTL())
            {
                this.folderTree.ImageFolderPath = GetImageUrl("RTL/Design/Controls/Tree", true, true);
            }
            else
            {
                this.folderTree.ImageFolderPath = GetImageUrl("Design/Controls/Tree", true, true);
            }

            this.folderTree.SourceFilterName = this.FilterName;
            this.folderTree.FileIDQueryStringKey = this.FileIDQueryStringKey;
            this.folderTree.PathQueryStringKey = this.PathQueryStringKey;
            this.folderTree.DisplayFileCount = this.DisplayFileCount;

            // Add tree to the filter collection
            CMSControlsHelper.SetFilter(ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ID), this.folderTree);
        }
        else
        {
            folderTree.StopProcessing = true;
            folderTree.Visible = false;
        }
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
