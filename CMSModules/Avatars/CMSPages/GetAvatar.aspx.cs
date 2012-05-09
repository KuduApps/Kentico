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
using CMS.DataEngine;
using CMS.URLRewritingEngine;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Avatars_CMSPages_GetAvatar : GetFilePage
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

    protected CMSOutputAvatar outputFile = null;
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
                if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSAlwaysCacheAvatars"], true))
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
        DebugHelper.SetContext("GetAvatar");

        // Prepare the cache key
        fileGuid = QueryHelper.GetGuid("avatarguid", Guid.Empty);

        int cacheMinutes = this.CacheMinutes;

        // Try to get data from cache
        using (CachedSection<CMSOutputAvatar> cs = new CachedSection<CMSOutputAvatar>(ref outputFile, cacheMinutes, true, null, "getavatar", Request.QueryString))
        {
            if (cs.LoadData)
            {
                // Process the file
                ProcessFile();

                // Ensure the cache settings
                if (cs.Cached)
                {
                    // Add cache dependency
                    CacheDependency cd = null;
                    if (outputFile != null)
                    {
                        if (outputFile.Avatar != null)
                        {
                            cd = CacheHelper.GetCacheDependency(outputFile.Avatar.GetDependencyCacheKeys());

                            // Do not cache if too big file which would be stored in memory
                            if ((outputFile.Avatar != null) && !CacheHelper.CacheImageAllowed(CurrentSiteName, outputFile.Avatar.AvatarFileSize) && !AvatarInfoProvider.StoreFilesInFileSystem())
                            {
                                cacheMinutes = largeFilesCacheMinutes;
                            }
                        }
                    }

                    if ((cd == null) && (cacheMinutes > 0))
                    {
                        // Set default dependency based on GUID
                        cd = CacheHelper.GetCacheDependency(new string[] { "avatarfile|" + fileGuid.ToString().ToLower() });
                    }

                    // Cache the data
                    cs.CacheMinutes = cacheMinutes;
                    cs.CacheDependency = cd;
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
        // Get file name from querystring.
        if (fileGuid != Guid.Empty)
        {
            ProcessFile(fileGuid);
        }
    }


    /// <summary>
    /// Processes the specified file.
    /// </summary>
    /// <param name="fileGuid">File guid</param>
    protected void ProcessFile(Guid fileGuid)
    {
        // Get the file
        AvatarInfo fileInfo = AvatarInfoProvider.GetAvatarInfoWithoutBinary(fileGuid);
        if (fileInfo != null)
        {
            bool resizeImage = (ImageHelper.IsImage(fileInfo.AvatarFileExtension) &&
                AvatarInfoProvider.CanResizeImage(fileInfo, this.Width, this.Height, this.MaxSideSize));

            // Get the data
            if ((outputFile == null) || (outputFile.Avatar == null))
            {
                outputFile = new CMSOutputAvatar(fileInfo, null);
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
    protected void SendFile(CMSOutputAvatar file)
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
                URLHelper.Redirect(file.RedirectTo, true, CurrentSiteName);
            }

            // Prepare etag
            string etag = file.LastModified.ToString();

            // Client caching - only on the live site
            if (useClientCache && AllowCache && AllowClientCache && ETagsMatch(etag, file.LastModified))
            {
                RespondNotModified(etag, true);
                return;
            }

            // If physical file not present, try to load
            if (file.PhysicalFile == null)
            {
                EnsurePhysicalFile(outputFile);
            }

            // Ensure the file data if physical file not present
            if (!file.DataLoaded && (file.PhysicalFile == ""))
            {
                byte[] cachedData = GetCachedOutputData();
                if (file.EnsureData(cachedData))
                {
                    if ((cachedData == null) && (this.CacheMinutes > 0))
                    {
                        SaveOutputDataToCache(file.OutputData, GetOutputDataDependency(file.Avatar));
                    }
                }
            }

            // Send the file
            if ((file.OutputData != null) || (file.PhysicalFile != ""))
            {
                // Setup the mime type - Fix the special types
                string mimetype = file.MimeType;
                string extension = file.Avatar.AvatarFileExtension;
                switch (extension.ToLower())
                {
                    case ".flv":
                        mimetype = "video/x-flv";
                        break;
                }

                // Prepare response
                Response.ContentType = mimetype;
                SetDisposition(file.Avatar.AvatarFileName, extension);

                ETag = etag;
                // Set if resumable downloads should be supported
                AcceptRange = !IsExtensionExcludedFromRanges(extension);

                if (useClientCache && AllowCache)
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
                    Response.Cache.SetETag(etag);
                }

                // Add the file data
                if ((file.PhysicalFile != "") && (file.OutputData == null))
                {
                    // If the output data should be cached, return the output data
                    bool cacheOutputData = false;
                    if (file.Avatar != null)
                    {
                        cacheOutputData = CacheHelper.CacheImageAllowed(CurrentSiteName, file.Avatar.AvatarFileSize);
                    }

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
                Response.Write("<html></html>");
            }
        }
        else
        {
            Response.Write("<html></html>");
        }

        CompleteRequest();
    }


    /// <summary>
    /// Returns the output data dependency based on the given attachment record.
    /// </summary>
    /// <param name="mi">Metafile object</param>
    protected CacheDependency GetOutputDataDependency(AvatarInfo ai)
    {
        if (ai == null)
        {
            return null;
        }

        return CacheHelper.GetCacheDependency(ai.GetDependencyCacheKeys());
    }


    /// <summary>
    /// Ensures the physical file.
    /// </summary>
    /// <param name="file">Output file</param>
    public bool EnsurePhysicalFile(CMSOutputAvatar file)
    {
        if (file == null)
        {
            return false;
        }

        // Try to link to file system
        if ((file.Avatar != null) && (file.Avatar.AvatarID > 0) && AvatarInfoProvider.StoreFilesInFileSystem())
        {
            string filePath = AvatarInfoProvider.EnsureAvatarPhysicalFile(file.Avatar);
            if (filePath != null)
            {
                if (file.Resized)
                {
                    // If resized, ensure the thumbnail file
                    if (AvatarInfoProvider.GenerateThumbnails())
                    {
                        filePath = AvatarInfoProvider.EnsureThumbnailFile(file.Avatar, this.Width, this.Height, this.MaxSideSize);
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
}
