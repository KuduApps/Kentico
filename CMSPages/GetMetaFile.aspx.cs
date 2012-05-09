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
using System.Web.Caching;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.URLRewritingEngine;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.TreeEngine;

public partial class CMSPages_GetMetaFile : GetFilePage
{
    #region "Advanced settings"

    /// <summary>
    /// Sets to false to disable the client caching.
    /// </summary>
    protected bool useClientCache = true;

    /// <summary>
    /// Sets to 0 if you do not wish to cache large files.
    /// </summary>
    protected int largeFilesCacheMinutes = 1;

    #endregion


    #region "Variables"

    protected CMSOutputMetaFile outputFile = null;
    protected Guid fileGuid = Guid.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns true if the process allows cache.
    /// </summary>
    public override bool AllowCache
    {
        get
        {
            if (mAllowCache == null)
            {
                // By default, cache for the metafiles is always enabled (even outside of the live site)
                if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSAlwaysCacheMetaFiles"], true))
                {
                    mAllowCache = true;
                }
                else
                {
                    mAllowCache = (this.ViewMode == ViewModeEnum.LiveSite);
                }
            }

            return mAllowCache.Value;
        }
        set
        {
            mAllowCache = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        DebugHelper.SetContext("GetMetaFile");

        // Load the site name
        LoadSiteName();

        int cacheMinutes = this.CacheMinutes;

        // Try to get data from cache
        using (CachedSection<CMSOutputMetaFile> cs = new CachedSection<CMSOutputMetaFile>(ref outputFile, cacheMinutes, true, null, "getmetafile", this.CurrentSiteName, Request.QueryString))
        {
            if (cs.LoadData)
            {
                // Process the file
                ProcessFile();

                if (cs.Cached)
                {
                    // Do not cache if too big file which would be stored in memory
                    if ((outputFile != null) &&
                        (outputFile.MetaFile != null) &&
                        !CacheHelper.CacheImageAllowed(CurrentSiteName, outputFile.MetaFile.MetaFileSize) &&
                        !AttachmentManager.StoreFilesInFileSystem(CurrentSiteName))
                    {
                        cacheMinutes = largeFilesCacheMinutes;
                    }

                    if (cacheMinutes > 0)
                    {
                        // Prepare the cache dependency
                        CacheDependency cd = null;
                        if (outputFile != null)
                        {
                            if (outputFile.MetaFile != null)
                            {
                                // Add dependency on this particular meta file
                                cd = CacheHelper.GetCacheDependency(outputFile.MetaFile.GetDependencyCacheKeys());
                            }
                        }

                        if (cd == null)
                        {
                            // Set default dependency based on GUID
                            cd = CacheHelper.GetCacheDependency(new string[] { "metafile|" + fileGuid.ToString().ToLower() });
                        }

                        cs.CacheDependency = cd;
                    }

                    // Cache the data
                    cs.CacheMinutes = cacheMinutes;
                    cs.Data = outputFile;
                }
            }
        }

        // Send the data
        SendFile(outputFile);

        DebugHelper.ReleaseContext();
    }


    /// <summary>
    /// Processes the file.
    /// </summary>
    protected void ProcessFile()
    {
        outputFile = null;

        // Get file GUID from querystring
        this.fileGuid = QueryHelper.GetGuid("fileguid", Guid.Empty);

        if (this.fileGuid == Guid.Empty)
        {
            // Get file GUID from context
            this.fileGuid = ValidationHelper.GetGuid(this.Context.Items["fileguid"], Guid.Empty);
        }

        if (this.fileGuid != Guid.Empty)
        {
            ProcessFile(this.fileGuid);
        }
    }


    /// <summary>
    /// Processes the specified file.
    /// </summary>
    /// <param name="fileGuid">File guid</param>
    protected void ProcessFile(Guid fileGuid)
    {
        // Get the file
        MetaFileInfo fileInfo = MetaFileInfoProvider.GetMetaFileInfoWithoutBinary(fileGuid, CurrentSiteName, true);
        if (fileInfo != null)
        {
            bool resizeImage = (ImageHelper.IsImage(fileInfo.MetaFileExtension) && MetaFileInfoProvider.CanResizeImage(fileInfo, this.Width, this.Height, this.MaxSideSize));

            // Get the data
            if ((outputFile == null) || (outputFile.MetaFile == null))
            {
                outputFile = NewOutputFile(fileInfo, null);
                outputFile.Width = this.Width;
                outputFile.Height = this.Height;
                outputFile.MaxSideSize = this.MaxSideSize;
                outputFile.Resized = resizeImage;
            }
        }
    }


    /// <summary>
    /// Sends the given file within response.
    /// </summary>
    /// <param name="file">File to send</param>
    protected void SendFile(CMSOutputMetaFile file)
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        // Set the revalidation
        SetRevalidation();

        // Send the file
        if (file != null)
        {
            // Redirect if the file should be redirected
            if (file.RedirectTo != "")
            {
                URLHelper.Redirect(file.RedirectTo, true, this.CurrentSiteName);
            }

            // Prepare etag
            string etag = null;
            if (file.MetaFile != null)
            {
                etag += file.MetaFile.MetaFileID;
            }

            // Put etag into ""
            etag = "\"" + etag + "\"";

            // Client caching - only on the live site
            if (useClientCache && AllowCache && AllowClientCache && ETagsMatch(etag, file.LastModified))
            {
                // Set the file time stamps to allow client caching
                SetTimeStamps(file);

                RespondNotModified(etag, true);
                return;
            }

            // If physical file not present, try to load
            if (file.PhysicalFile == null)
            {
                EnsurePhysicalFile(outputFile);
            }

            // If the output data should be cached, return the output data
            bool cacheOutputData = false;
            if ((this.CacheMinutes > 0) && (file.MetaFile != null))
            {
                cacheOutputData = CacheHelper.CacheImageAllowed(CurrentSiteName, file.MetaFile.MetaFileSize);
            }

            // Ensure the file data if physical file not present
            if (!file.DataLoaded && (file.PhysicalFile == ""))
            {
                byte[] cachedData = GetCachedOutputData();
                if (file.EnsureData(cachedData))
                {
                    if ((cachedData == null) && cacheOutputData)
                    {
                        SaveOutputDataToCache(file.OutputData, GetOutputDataDependency(file.MetaFile));
                    }
                }
            }

            // Send the file
            if ((file.OutputData != null) || (file.PhysicalFile != ""))
            {
                // Setup the mime type - Fix the special types
                string mimetype = file.MimeType;
                string extension = file.MetaFile.MetaFileExtension;
                switch (extension.ToLower())
                {
                    case ".flv":
                        mimetype = "video/x-flv";
                        break;
                }

                // Prepare response
                Response.ContentType = mimetype;
                SetDisposition(file.MetaFile.MetaFileName, extension);

                // Setup Etag property
                ETag = etag;

                // Set if resumable downloads should be supported
                AcceptRange = !IsExtensionExcludedFromRanges(extension);

                if (useClientCache && AllowCache)
                {
                    // Set the file time stamps to allow client caching
                    SetTimeStamps(file);

                    Response.Cache.SetETag(etag);
                }

                // Add the file data
                if ((file.PhysicalFile != "") && (file.OutputData == null))
                {
                    // Stream the file from the file system
                    file.OutputData = WriteFile(file.PhysicalFile, cacheOutputData);
                }
                else
                {
                    // Use output data of the file in memory if present
                    WriteBytes(file.OutputData);
                }
            }
            else
            {
                NotFound();
            }
        }
        else
        {
            NotFound();
        }

        CompleteRequest();
    }


    /// <summary>
    /// Sets the last modified and expires header to the response
    /// </summary>
    /// <param name="file">Output file data</param>
    private void SetTimeStamps(CMSOutputMetaFile file)
    {
        DateTime expires = DateTime.Now;

        // Send last modified header to allow client caching
        Response.Cache.SetLastModified(file.LastModified);

        Response.Cache.SetCacheability(HttpCacheability.Public);
        if (AllowClientCache)
        {
            expires = DateTime.Now.AddMinutes(this.ClientCacheMinutes);
        }

        Response.Cache.SetExpires(expires);
    }


    /// <summary>
    /// Returns the output data dependency based on the given attachment record.
    /// </summary>
    /// <param name="mi">Metafile object</param>
    protected CacheDependency GetOutputDataDependency(MetaFileInfo mi)
    {
        if (mi == null)
        {
            return null;
        }

        return CacheHelper.GetCacheDependency(mi.GetDependencyCacheKeys());
    }


    /// <summary>
    /// Ensures the physical file.
    /// </summary>
    /// <param name="file">Output file</param>
    public bool EnsurePhysicalFile(CMSOutputMetaFile file)
    {
        if (file == null)
        {
            return false;
        }

        // Try to link to file system
        if (String.IsNullOrEmpty(file.Watermark) && (file.MetaFile != null) && (file.MetaFile.MetaFileID > 0) && MetaFileInfoProvider.StoreFilesInFileSystem(file.SiteName))
        {
            string filePath = MetaFileInfoProvider.EnsurePhysicalFile(file.MetaFile, file.SiteName);
            if (filePath != null)
            {
                if (file.Resized)
                {
                    // If resized, ensure the thumbnail file
                    if (MetaFileInfoProvider.GenerateThumbnails(file.SiteName))
                    {
                        filePath = MetaFileInfoProvider.EnsureThumbnailFile(file.MetaFile, file.SiteName, this.Width, this.Height, this.MaxSideSize);
                        if (filePath != null)
                        {
                            // Link to the physical file
                            file.PhysicalFile = filePath;
                            return true;
                        }
                    }
                }
                else
                {
                    // Link to the physical file
                    file.PhysicalFile = filePath;
                    return false;
                }
            }
        }

        file.PhysicalFile = "";
        return false;
    }


    /// <summary>
    /// Gets the new output MetaFile object.
    /// </summary>
    public CMSOutputMetaFile NewOutputFile()
    {
        CMSOutputMetaFile mf = new CMSOutputMetaFile();

        mf.Watermark = this.Watermark;
        mf.WatermarkPosition = this.WatermarkPosition;

        return mf;
    }


    /// <summary>
    /// Gets the new output MetaFile object.
    /// </summary>
    /// <param name="mfi">Meta file info</param>
    /// <param name="data">Output MetaFile data</param>
    public CMSOutputMetaFile NewOutputFile(MetaFileInfo mfi, byte[] data)
    {
        CMSOutputMetaFile mf = new CMSOutputMetaFile(mfi, data);

        mf.Watermark = this.Watermark;
        mf.WatermarkPosition = this.WatermarkPosition;

        return mf;
    }
}