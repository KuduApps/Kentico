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
using System.Drawing;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.IO;


public partial class CMSModules_MediaLibrary_InlineControls_MediaFileControl : InlineUserControl
{
    #region "Private variables"

    private const int DEFAULT_WIDTH = 200;
    private const int DEFAULT_HEIGHT = 200;

    private Guid mFileGuid = Guid.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Guid of the media file being inserted.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.FileGuid.ToString();
        }
        set
        {
            this.FileGuid = ValidationHelper.GetGuid(value, Guid.Empty);
        }
    }


    /// <summary>
    /// MaxSideSize of the media file being inserted.
    /// </summary>
    public int MaxSideSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxSideSize"), 0);
        }
        set
        {
            this.SetValue("MaxSideSize", value);
        }
    }


    /// <summary>
    /// Width of the media file being inserted.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 0);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Height of the media file being inserted.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Height"), 0);
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Guid of the media file being inserted.
    /// </summary>
    public Guid FileGuid
    {
        get
        {
            return ValidationHelper.GetGuid(this.GetValue("FileGuid"), Guid.Empty);
        }
        set
        {
            this.SetValue("FileGuid", value);
        }
    }


    /// <summary>
    /// Site name of media file.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls
        SetupControls();
    }


    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControls()
    {
        MediaFileInfo mFile = MediaFileInfoProvider.GetMediaFileInfo(this.FileGuid, this.SiteName);
        if (mFile != null)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(mFile.FileSiteID);
            if (si != null)
            {
                MediaLibraryInfo mLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(mFile.FileLibraryID);

                string extension = mFile.FileExtension.ToLower().TrimStart('.');

                string path = MediaFileInfoProvider.GetMediaFilePath(this.SiteName, mLibrary.LibraryFolder, mFile.FilePath);
                string url = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(this.SiteName, mFile.FileGUID, mFile.FileName);

                if (ImageHelper.IsImage(extension) && File.Exists(path))
                {
                    // Get image dimension
                    // New dimensions
                    int[] newDims = ImageHelper.EnsureImageDimensions(Width, Height, MaxSideSize, mFile.FileImageWidth, mFile.FileImageHeight);

                    // If new dimensions are diferent use them
                    if (((newDims[0] != mFile.FileImageWidth) || (newDims[1] != mFile.FileImageHeight)) && (newDims[0] > 0) && (newDims[1] > 0))
                    {
                        string dimensions = "?width=" + newDims[0] + "&height=" + newDims[1];

                        this.ltlOutput.Text = "<img alt=\"" + mFile.FileName + "\" src=\"" + url + dimensions + "\" width=\"" + newDims[0] + "\" height=\"" + newDims[1] + "\" border=\"0\" />";
                    }
                    else
                    {
                        this.ltlOutput.Text = "<img alt=\"" + mFile.FileName + "\" src=\"" + url + "\" width=\"" + Width + "\" height=\"" + Height + "\" border=\"0\" />";
                    }
                }
                else
                {
                    // Set default dimensions of rendered object if required
                    int width = (this.Width > 0) ? this.Width : DEFAULT_WIDTH;
                    int height = (this.Height > 0) ? this.Height : DEFAULT_HEIGHT;

                    if (CMS.GlobalHelper.MediaHelper.IsFlash(extension))
                    {
                        // Initialize flash parameters
                        FlashParameters flashParams = new FlashParameters();
                        flashParams.Height = height;
                        flashParams.Width = width;
                        flashParams.Url = url;

                        this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetFlash(flashParams);
                    }
                    else
                    {
                        if (CMS.GlobalHelper.MediaHelper.IsAudio(extension))
                        {
                            // Initialize audio/video parameters
                            AudioVideoParameters audioParams = new AudioVideoParameters();

                            audioParams.SiteName = CMSContext.CurrentSiteName;
                            audioParams.Url = url;
                            audioParams.Width = width;
                            audioParams.Height = height;
                            audioParams.Extension = extension;

                            this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetAudioVideo(audioParams);
                        }
                        else if (CMS.GlobalHelper.MediaHelper.IsVideo(extension))
                        {
                            // Initialize audio/video parameters
                            AudioVideoParameters videoParams = new AudioVideoParameters();

                            videoParams.SiteName = CMSContext.CurrentSiteName;
                            videoParams.Url = url;
                            videoParams.Width = width;
                            videoParams.Height = height;
                            videoParams.Extension = extension;

                            this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetAudioVideo(videoParams);
                        }
                    }
                }
            }
        }
    }
}
