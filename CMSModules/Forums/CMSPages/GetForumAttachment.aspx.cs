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
using CMS.Forums;
using CMS.UIControls;
using CMS.SettingsProvider;


public partial class CMSModules_Forums_CMSPages_GetForumAttachment : GetFilePage
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

    protected CMSOutputForumAttachment outputFile = null;
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
                // By default, cache for the forum attachments is always enabled (even outside of the live site)
                if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSAlwaysCacheForumAttachments"], true))
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
        fileGuid = QueryHelper.GetGuid("fileguid", Guid.Empty);

        DebugHelper.SetContext("GetForumAttachment");

        int cacheMinutes = this.CacheMinutes;

        // Try to get data from cache
        using (CachedSection<CMSOutputForumAttachment> cs = new CachedSection<CMSOutputForumAttachment>(ref outputFile, cacheMinutes, true, null, "getforumattachment", CurrentSiteName, CacheHelper.GetBaseCacheKey(true, false), Request.QueryString))
        {
            if (cs.LoadData)
            {
                // Disable caching before check permissions
                cs.CacheMinutes = 0;

                // Process the file
                ProcessFile();

                // Keep original cache minutes if permissions are ok
                cs.CacheMinutes = cacheMinutes;

                // Ensure the cache settings
                if (cs.Cached)
                {
                    // Prepare the cache dependency
                    CacheDependency cd = null;
                    if (outputFile != null)
                    {
                        if (outputFile.ForumAttachment != null)
                        {
                            cd = CacheHelper.GetCacheDependency(outputFile.ForumAttachment.GetDependencyCacheKeys());

                            // Do not cache if too big file which would be stored in memory
                            if (!CacheHelper.CacheImageAllowed(CurrentSiteName, outputFile.ForumAttachment.AttachmentFileSize) && !ForumAttachmentInfoProvider.StoreFilesInFileSystem(CurrentSiteName))
                            {
                                cacheMinutes = largeFilesCacheMinutes;
                            }
                        }
                    }

                    if ((cd == null) && (CurrentSite != null) && (outputFile != null))
                    {
                        // Get the current forum id
                        int forumId = 0;
                        if (outputFile.ForumAttachment != null)
                        {
                            forumId = outputFile.ForumAttachment.AttachmentForumID;
                        }

                        // Set default dependency based on GUID
                        cd = CacheHelper.GetCacheDependency(new string[] { "forumattachment|" + fileGuid.ToString().ToLower() + "|" + CurrentSite.SiteID, "forumattachment|" + forumId });
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
        ForumAttachmentInfo fileInfo = ForumAttachmentInfoProvider.GetForumAttachmentInfoWithoutBinary(fileGuid, CMSContext.CurrentSiteName);
        if (fileInfo != null)
        {
            #region "Security"

            // Indicates whether current user is granted to see this attachment
            bool attachmentAllowed = false;

            // Get forum
            ForumInfo fi = ForumInfoProvider.GetForumInfo(fileInfo.AttachmentForumID);
            if (fi != null)
            {
                // Check acess
                if (ForumViewer.CheckPermission("AccessToForum", SecurityHelper.GetSecurityAccessEnum(fi.ForumAccess, 6), fi.ForumGroupID, fi.ForumID))
                {
                    attachmentAllowed = true;
                }
            }

            // If attachment is not allowed for current user, redirect to the access denied page
            if (!attachmentAllowed)
            {
                URLHelper.Redirect(URLRewriter.AccessDeniedPageURL(CurrentSiteName));
            }

            #endregion

            bool resizeImage = (ImageHelper.IsMimeImage(fileInfo.AttachmentMimeType) &&
            ForumAttachmentInfoProvider.CanResizeImage(fileInfo, this.Width, this.Height, this.MaxSideSize));

            // Get the data
            if ((outputFile == null) || (outputFile.ForumAttachment == null))
            {
                outputFile = new CMSOutputForumAttachment(fileInfo, fileInfo.AttachmentBinary);
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
    protected void SendFile(CMSOutputForumAttachment file)
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

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
            if (useClientCache && AllowCache && CacheHelper.CacheImageEnabled(CurrentSiteName) && ETagsMatch(etag, file.LastModified))
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
                        SaveOutputDataToCache(file.OutputData, GetOutputDataDependency(file.ForumAttachment));
                    }
                }
            }

            // Send the file
            if ((file.OutputData != null) || (file.PhysicalFile != ""))
            {
                // Setup the mime type - Fix the special types
                string mimetype = file.MimeType;
                string extension = file.ForumAttachment.AttachmentFileExtension;
                switch (extension.ToLower())
                {
                    case ".flv":
                        mimetype = "video/x-flv";
                        break;
                }

                // Prepare response
                Response.ContentType = mimetype;
                SetDisposition(file.ForumAttachment.AttachmentFileName, extension);

                // Set if resumable downloads should be supported
                AcceptRange = !IsExtensionExcludedFromRanges(extension);

                if (useClientCache && AllowCache && (CacheHelper.CacheImageEnabled(CurrentSiteName)))
                {
                    DateTime expires = DateTime.Now;

                    // Send last modified header to allow client caching
                    Response.Cache.SetLastModified(file.LastModified);

                    Response.Cache.SetCacheability(HttpCacheability.Public);
                    if (DocumentBase.AllowClientCache() && DocumentBase.UseFullClientCache)
                    {
                        expires = DateTime.Now.AddMinutes(CacheHelper.CacheImageMinutes(CurrentSiteName));
                    }

                    Response.Cache.SetExpires(expires);
                    Response.Cache.SetETag(etag);
                }

                // Add the file data
                if ((file.PhysicalFile != "") && (file.OutputData == null))
                {
                    // If the output data should be cached, return the output data
                    bool cacheOutputData = false;
                    if (file.ForumAttachment != null)
                    {
                        cacheOutputData = CacheHelper.CacheImageAllowed(CurrentSiteName, file.ForumAttachment.AttachmentFileSize);
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
                NotFound();
            }
        }
        else
        {
            NotFound();
        }

        CompleteRequest();
        //RequestHelper.EndResponse();
    }


    /// <summary>
    /// Returns the output data dependency based on the given attachment record.
    /// </summary>
    /// <param name="mi">ForumAttachmentInfo object</param>
    protected CacheDependency GetOutputDataDependency(ForumAttachmentInfo fi)
    {
        if (fi == null)
        {
            return null;
        }

        return CacheHelper.GetCacheDependency(fi.GetDependencyCacheKeys());
    }


    /// <summary>
    /// Ensures the physical file.
    /// </summary>
    /// <param name="file">Output file</param>
    public bool EnsurePhysicalFile(CMSOutputForumAttachment file)
    {
        if (file == null)
        {
            return false;
        }

        // Try to link to file system
        if ((file.ForumAttachment != null) && (file.ForumAttachment.AttachmentID > 0) && ForumAttachmentInfoProvider.StoreFilesInFileSystem(CurrentSiteName))
        {
            string filePath = ForumAttachmentInfoProvider.EnsureAttachmentPhysicalFile(file.ForumAttachment, CurrentSiteName);
            if (filePath != null)
            {
                if (file.Resized)
                {
                    // If resized, ensure the thumbnail file
                    if (ForumAttachmentInfoProvider.GenerateThumbnails(CurrentSiteName))
                    {
                        filePath = ForumAttachmentInfoProvider.EnsureThumbnailFile(file.ForumAttachment, this.Width, this.Height, this.MaxSideSize);
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