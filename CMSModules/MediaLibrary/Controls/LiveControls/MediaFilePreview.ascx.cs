using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_LiveControls_MediaFilePreview : MediaFilePreview
{
    #region "Variables"

    private DataRow mData = null;
    private int mWidth = 0;
    private int mHeight = 0;
    private int mMaxSideSize = 0;
    private string mDefaultImageUrl = null;

    /// <summary>
    /// Indicates whether control was binded.
    /// </summary>
    private bool binded = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Output object width (image/video/flash)
    /// </summary>
    public int Width
    {
        get
        {
            return this.mWidth;
        }
        set
        {
            this.mWidth = value;
        }
    }


    /// <summary>
    /// Output object height (image/video/flash)
    /// </summary>
    public int Height
    {
        get
        {
            return this.mHeight;
        }
        set
        {
            this.mHeight = value;
        }
    }


    /// <summary>
    /// Output image max side size.
    /// </summary>
    public int MaxSideSize
    {
        get
        {
            return this.mMaxSideSize;
        }
        set
        {
            this.mMaxSideSize = value;
        }
    }


    /// <summary>
    /// URL of the default teaser image.
    /// </summary>
    public string DefaultImageUrl
    {
        get
        {
            return this.mDefaultImageUrl;
        }
        set
        {
            this.mDefaultImageUrl = value;
        }
    }

    #endregion


    protected override void OnDataBinding(EventArgs e)
    {
        base.OnDataBinding(e);
        // Get data row
        this.mData = GetData(this);
    }


    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);
        bool writeHtml = !binded;
        
        // Reload data
        ReloadData(false);

        if(writeHtml)
        {
            writer.Write(ltlOutput.Text);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        ReloadData(false);
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    public void ReloadData(bool forceReload)
    {
        if (!binded || forceReload)
        {
            if (this.mData != null)
            {
                MediaFileInfo mfi = new MediaFileInfo(this.mData);
                if (mfi != null)
                {
                    bool completeUrl = false;

                    // Get the site
                    SiteInfo si = null;
                    SiteInfo currentSite = CMSContext.CurrentSite;
                    if (mfi.FileSiteID == currentSite.SiteID)
                    {
                        si = currentSite;
                    }
                    else
                    {
                        si = SiteInfoProvider.GetSiteInfo(mfi.FileSiteID);
                    }

                    if (si != null)
                    {
                        // Complete URL only for other site than current
                        if (si.SiteID != currentSite.SiteID)
                        {
                            completeUrl = true;
                        }

                        string url = "";

                        if (this.UseSecureLinks)
                        {
                            if (completeUrl)
                            {
                                url = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(si.SiteName, mfi.FileGUID, mfi.FileName);
                            }
                            else
                            {
                                url = MediaFileInfoProvider.GetMediaFileUrl(mfi.FileGUID, mfi.FileName);
                            }
                        }
                        else
                        {
                            MediaLibraryInfo li = MediaLibraryInfoProvider.GetMediaLibraryInfo(mfi.FileLibraryID);
                            if (li != null)
                            {
                                if (completeUrl)
                                {
                                    url = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(si.SiteName, li.LibraryFolder, mfi.FilePath);
                                }
                                else
                                {
                                    url = MediaFileInfoProvider.GetMediaFileUrl(si.SiteName, li.LibraryFolder, mfi.FilePath);
                                }
                            }
                        }

                        if (this.DisplayActiveContent)
                        {
                            if (ImageHelper.IsImage(mfi.FileExtension) && File.Exists(MediaFileInfoProvider.GetMediaFilePath(mfi.FileLibraryID, mfi.FilePath)))
                            {
                                // Get new dimensions
                                int[] newDims = ImageHelper.EnsureImageDimensions(Width, Height, MaxSideSize, mfi.FileImageWidth, mfi.FileImageHeight);

                                // If dimensions changed use secure link
                                if ((newDims[0] != mfi.FileImageWidth) || (newDims[1] != mfi.FileImageHeight))
                                {
                                    if (completeUrl)
                                    {
                                        url = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(si.SiteName, mfi.FileGUID, mfi.FileName);
                                    }
                                    else
                                    {
                                        url = MediaFileInfoProvider.GetMediaFileUrl(mfi.FileGUID, mfi.FileName);
                                    }
                                }
                                else
                                {
                                    // Use width and height from properties in case dimensions are bigger than original
                                    newDims[0] = Width;
                                    newDims[1] = Height;
                                }

                                // Initialize image parameters
                                ImageParameters imgParams = new ImageParameters();
                                imgParams.Alt = mfi.FileDescription;
                                imgParams.Width = newDims[0];
                                imgParams.Height = newDims[1];
                                imgParams.Url = url;

                                this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetImage(imgParams);
                            }
                            else if (CMS.GlobalHelper.MediaHelper.IsFlash(mfi.FileExtension))
                            {
                                // Initialize flash parameters
                                FlashParameters flashParams = new FlashParameters();
                                flashParams.Url = url;
                                flashParams.Width = this.Width;
                                flashParams.Height = this.Height;

                                this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetFlash(flashParams);
                            }
                            else if (CMS.GlobalHelper.MediaHelper.IsAudio(mfi.FileExtension))
                            {
                                // Initialize audio/video parameters
                                AudioVideoParameters audioParams = new AudioVideoParameters();

                                audioParams.SiteName = CMSContext.CurrentSiteName;
                                audioParams.Url = url;
                                audioParams.Width = this.Width;
                                audioParams.Height = this.Height;
                                audioParams.Extension = mfi.FileExtension;

                                this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetAudioVideo(audioParams);
                            }
                            else if (CMS.GlobalHelper.MediaHelper.IsVideo(mfi.FileExtension))
                            {
                                // Initialize audio/video parameters
                                AudioVideoParameters videoParams = new AudioVideoParameters();

                                videoParams.SiteName = CMSContext.CurrentSiteName;
                                videoParams.Url = url;
                                videoParams.Width = this.Width;
                                videoParams.Height = this.Height;
                                videoParams.Extension = mfi.FileExtension;

                                this.ltlOutput.Text = CMS.GlobalHelper.MediaHelper.GetAudioVideo(videoParams);
                            }
                            else
                            {
                                this.ltlOutput.Text = MediaLibraryHelper.ShowPreviewOrIcon(mfi, this.Width, this.Height, this.MaxSideSize, this.PreviewSuffix, this.IconSet, this.Page, this.DefaultImageUrl);
                            }
                        }
                        else
                        {
                            this.ltlOutput.Text = MediaLibraryHelper.ShowPreviewOrIcon(mfi, this.Width, this.Height, this.MaxSideSize, this.PreviewSuffix, this.IconSet, this.Page, this.DefaultImageUrl);
                        }
                    }
                }
            }
            binded = true;
        }
    }


    #region "Private methods"

    /// <summary>
    /// Returns DataRow from current binding item.
    /// </summary>
    /// <param name="ctrl">Control</param>
    private DataRow GetData(Control ctrl)
    {
        while (ctrl != null)
        {
            if (ctrl is IDataItemContainer)
            {
                return ((DataRowView)((IDataItemContainer)ctrl).DataItem).Row;
            }
            ctrl = ctrl.Parent;
        }
        return null;
    }

    #endregion

}
