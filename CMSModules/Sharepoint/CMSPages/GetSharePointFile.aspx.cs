using System;
using System.Web;

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SharePoint;
using CMS.SettingsProvider;
using CMS.IO;

public partial class CMSModules_SharePoint_CMSPages_GetSharePointFile : GetFilePage
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

    protected CMSOutputSharePointFile outputFile = null;
    protected bool? mIsLiveSite = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if live site mode.
    /// </summary>
    public bool IsLiveSite
    {
        get
        {
            if (mIsLiveSite == null)
            {
                mIsLiveSite = (ViewMode == ViewModeEnum.LiveSite);
            }
            return mIsLiveSite.Value;
        }
    }


    /// <summary>
    /// Returns true if the process allows cache.
    /// </summary>
    public override bool AllowCache
    {
        get
        {
            if (mAllowCache == null)
            {
                // By default, cache for the files is disabled outside of the live site
                mAllowCache = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSAlwaysCacheSharePointFiles"], false) || IsLiveSite;
            }

            return mAllowCache.Value;
        }
        set
        {
            mAllowCache = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the site
        if (CurrentSiteName == "")
        {
            throw new Exception("[GetSharePointFile.aspx]: Site not running.");
        }

        int cacheMinutes = CacheMinutes;

        // Try to get data from cache
        using (CachedSection<CMSOutputSharePointFile> cs = new CachedSection<CMSOutputSharePointFile>(ref outputFile, cacheMinutes, true, null, "getsharepointfile", Request.QueryString))
        {
            if (cs.LoadData)
            {
                // Process the file
                ProcessAttachment();

                // Ensure the cache settings
                if (cs.Cached)
                {
                    // Check the file size for caching
                    if ((outputFile != null) && (outputFile.OutputData != null))
                    {
                        // Do not cache if too big file which would be stored in memory
                        if (!CacheHelper.CacheImageAllowed(CurrentSiteName, outputFile.OutputData.Length))
                        {
                            cacheMinutes = largeFilesCacheMinutes;
                        }
                    }

                    // Cache the data
                    cs.CacheMinutes = cacheMinutes;
                    cs.Data = outputFile;
                }
            }
        }

        // Send the data
        SendFile(outputFile);
    }


    /// <summary>
    /// Sends the given file within response.
    /// </summary>
    /// <param name="file">File to send</param>
    protected void SendFile(CMSOutputSharePointFile file)
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

        if ((file != null) && file.IsValid)
        {
            // Prepare etag in ""
            string etag = "\"" + file.SharePointFilePath + "\"";

            // Client caching - only on the live site
            if (useClientCache && AllowCache && CacheHelper.CacheImageEnabled(CurrentSiteName) && ETagsMatch(etag, file.LastModified))
            {
                RespondNotModified(etag, true);                
                return;
            }

            // If the output data should be cached, return the output data
            bool cacheOutputData = false;
            if (file.OutputData != null)
            {
                cacheOutputData = CacheHelper.CacheImageAllowed(CurrentSiteName, file.OutputData.Length);
            }

            // Ensure the file data
            if (!file.DataLoaded)
            {
                byte[] cachedData = GetCachedOutputData();

                // Ensure data are retrieved from SharePoint
                if (file.EnsureData(cachedData))
                {
                    if ((cachedData == null) && cacheOutputData)
                    {
                        SaveOutputDataToCache(file.OutputData, null);
                    }
                }
            }

            // Send the file
            if (file.OutputData != null)
            {
                byte[] data = null;

                // Check if the request is for partial data (Range HTTP header)
                long[,] rangePosition = GetRange(file.OutputData.Length, HttpContext.Current);

                // Send all file contens
                if (rangePosition.GetUpperBound(0) == 0)
                {
                    // Setup the mime type - Fix the special types
                    string mimetype = file.MimeType;
                    switch (file.FileExtension.ToLower())
                    {
                        case ".flv":
                            mimetype = "video/x-flv";
                            break;
                    }

                    // Prepare response
                    Response.ContentType = mimetype;

                    // get file name without the path
                    string fileName = Path.GetFileName(file.SharePointFilePath);

                    SetDisposition(fileName, file.FileExtension);

                    // Setup Etag property
                    ETag = etag;

                    // Set if resumable downloads should be supported
                    AcceptRange = !IsExtensionExcludedFromRanges(file.FileExtension);

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

                    data = file.OutputData;
                }
                // Send partial contens
                else
                {
                    data = new byte[file.OutputData.Length - rangePosition[0, RANGE_START]];

                    // Get part of file
                    Array.Copy(file.OutputData, rangePosition[0, RANGE_START], data, 0, data.Length);
                }

                // Use output data of the file in memory if present
                WriteBytes(data);

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
    /// Processes the attachment.
    /// </summary>
    protected void ProcessAttachment()
    {
        outputFile = null;

        // Get file name with path from url
        string name = QueryHelper.GetString("name", null);

        // Get server from url
        string serverUrl = QueryHelper.GetString("server", null);

        // If not correctly set do nothing
        if ((serverUrl == null) || (name == null))
        {
            return;
        }

        // Create 
        outputFile = new CMSOutputSharePointFile(serverUrl, name, null);

        outputFile.Width = Width;
        outputFile.Height = Height;
        outputFile.MaxSideSize = MaxSideSize;
    }

    #endregion
}
