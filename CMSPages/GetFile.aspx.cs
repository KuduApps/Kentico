using System;
using System.Web;
using System.Web.Caching;
using System.Data;

using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.URLRewritingEngine;
using CMS.WebAnalytics;
using CMS.UIControls;
using CMS.IO;

public partial class CMSPages_GetFile : GetFilePage
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

    protected CMSOutputFile outputFile = null;

    protected AttachmentManager mAttachmentManager = null;
    protected TreeProvider mTreeProvider = null;
    protected GeneralConnection mConnection = null;

    protected TreeNode node = null;
    protected PageInfo pi = null;

    protected int? mVersionHistoryID = null;
    protected bool mIsLatestVersion = false;
    protected bool? mIsLiveSite = null;
    protected Guid guid = Guid.Empty;
    protected string mCulture = null;
    protected Guid nodeGuid = Guid.Empty;

    protected string aliasPath = null;
    protected string fileName = null;

    protected int latestForDocumentId = 0;
    protected int latestForHistoryId = 0;

    protected bool allowLatestVersion = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the language for current file.
    /// </summary>
    public string CultureCode
    {
        get
        {
            if (mCulture == null)
            {
                string culture = QueryHelper.GetString(URLHelper.LanguageParameterName, CMSContext.PreferredCultureCode);
                if (!CultureInfoProvider.IsCultureAllowed(culture, CurrentSiteName))
                {
                    culture = CMSContext.PreferredCultureCode;
                }
                mCulture = culture;
            }

            return mCulture;
        }
    }


    /// <summary>
    /// Attachment manager.
    /// </summary>
    public AttachmentManager AttachmentManager
    {
        get
        {
            return mAttachmentManager ?? (mAttachmentManager = new AttachmentManager());
        }
    }


    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider());
        }
    }


    /// <summary>
    /// Document version history ID.
    /// </summary>
    public int VersionHistoryID
    {
        get
        {
            if (mVersionHistoryID == null)
            {
                mVersionHistoryID = QueryHelper.GetInteger("versionhistoryid", 0);
            }
            return mVersionHistoryID.Value;
        }
    }


    /// <summary>
    /// Indicates if the file is latest version or comes from version history.
    /// </summary>
    public bool LatestVersion
    {
        get
        {
            return mIsLatestVersion || (VersionHistoryID > 0);
        }
    }


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
                mAllowCache = CacheHelper.AlwaysCacheFiles || IsLiveSite;
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
        DebugHelper.SetContext("GetFile");

        // Load the site name
        LoadSiteName();

        // Check the site
        if (CurrentSiteName == "")
        {
            throw new Exception("[GetFile.aspx]: Site not running.");
        }

        // Validate the culture
        PreferredCultureOnDemand culture = new PreferredCultureOnDemand();
        URLRewriter.ValidateCulture(CurrentSiteName, culture, null);

        // Set campaign
        if (IsLiveSite)
        {
            // Store campaign name if present
            string campaign = AnalyticsHelper.CurrentCampaign(CurrentSiteName);
            if (!String.IsNullOrEmpty(campaign))
            {
                PageInfo pi = CMSContext.CurrentPageInfo;

                // Log campaign
                if ((pi != null) && AnalyticsHelper.IsLoggingEnabled(CurrentSiteName, pi.NodeAliasPath) && AnalyticsHelper.SetCampaign(campaign, CurrentSiteName, pi.NodeAliasPath))
                {
                    CMSContext.Campaign = campaign;
                }
            }
        }

        int cacheMinutes = CacheMinutes;

        // Try to get data from cache
        using (CachedSection<CMSOutputFile> cs = new CachedSection<CMSOutputFile>(ref outputFile, cacheMinutes, true, null, "getfile", CurrentSiteName, CacheHelper.BaseCacheKey, Request.QueryString))
        {
            if (cs.LoadData)
            {
                // Store current value and temporarly disable caching
                bool cached = cs.Cached;
                cs.Cached = false;

                // Process the file
                ProcessAttachment();

                // Restore cache settings - data were loaded
                cs.Cached = cached;

                if (cs.Cached)
                {
                    // Do not cache if too big file which would be stored in memory
                    if ((outputFile != null) &&
                        (outputFile.Attachment != null) &&
                        !CacheHelper.CacheImageAllowed(CurrentSiteName, outputFile.Attachment.AttachmentSize) &&
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
                            string[] dependencies = new string[] { 
                            "node|" + CurrentSiteName.ToLower() + "|" + outputFile.AliasPath.ToLower(),
                            "" 
                        };

                            // Do not cache if too big file which would be stored in memory
                            if (outputFile.Attachment != null)
                            {
                                if (!CacheHelper.CacheImageAllowed(CurrentSiteName, outputFile.Attachment.AttachmentSize) && !AttachmentManager.StoreFilesInFileSystem(CurrentSiteName))
                                {
                                    cacheMinutes = largeFilesCacheMinutes;
                                }

                                dependencies[1] = "attachment|" + outputFile.Attachment.AttachmentGUID.ToString().ToLower();
                            }

                            cd = CacheHelper.GetCacheDependency(dependencies);
                        }

                        if (cd == null)
                        {
                            // Set default dependency
                            if (guid != Guid.Empty)
                            {
                                // By attachment GUID
                                cd = CacheHelper.GetCacheDependency(new string[] { "attachment|" + guid.ToString().ToLower() });
                            }
                            else if (nodeGuid != Guid.Empty)
                            {
                                // By node GUID
                                cd = CacheHelper.GetCacheDependency(new string[] { "nodeguid|" + CurrentSiteName.ToLower() + "|" + nodeGuid.ToString().ToLower() });
                            }
                            else if (aliasPath != null)
                            {
                                // By node alias path
                                cd = CacheHelper.GetCacheDependency(new string[] { "node|" + CurrentSiteName.ToLower() + "|" + aliasPath.ToLower() });
                            }
                        }

                        cs.CacheDependency = cd;
                    }

                    // Cache the data
                    cs.CacheMinutes = cacheMinutes;
                    cs.Data = outputFile;
                }
            }
        }

        // Do not cache images in the browser if cache is not allowed
        if (LatestVersion)
        {
            useClientCache = false;
        }

        // Send the data
        SendFile(outputFile);

        DebugHelper.ReleaseContext();
    }


    /// <summary>
    /// Sends the given file within response.
    /// </summary>
    /// <param name="file">File to send</param>
    protected void SendFile(CMSOutputFile file)
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        // Set the revalidation
        SetRevalidation();

        // Send the file
        if ((file != null) && file.IsValid)
        {
            // Redirect if the file should be redirected
            if (file.RedirectTo != "")
            {
                // Log hit or activity before redirecting
                LogEvent(file);

                if (StorageHelper.IsExternalStorage)
                {
                    string url = File.GetFileUrl(file.RedirectTo, CurrentSiteName);
                    if (!string.IsNullOrEmpty(url))
                    {
                        URLHelper.Redirect(url, true, CurrentSiteName);
                    }
                }

                URLHelper.Redirect(file.RedirectTo, true, CurrentSiteName);
            }

            // Check authentication if secured file
            if (file.IsSecured)
            {
                URLRewriter.CheckSecured(CurrentSiteName, ViewMode);
            }

            // Prepare etag
            string etag = file.CultureCode.ToLower();
            if (file.Attachment != null)
            {
                etag += "|" + file.Attachment.AttachmentGUID + "|" + file.Attachment.AttachmentLastModified.ToUniversalTime();
            }

            if (file.IsSecured)
            {
                // For secured files, add user name to etag
                etag += "|" + HttpContext.Current.User.Identity.Name;
            }

            // Put etag into ""
            etag = "\"" + etag + "\"";


            // Client caching - only on the live site
            if (useClientCache && AllowCache && AllowClientCache && ETagsMatch(etag, file.LastModified))
            {
                // Set the file time stamps to allow client caching
                SetTimeStamps(file);

                RespondNotModified(etag, !file.IsSecured);
                return;
            }

            // If physical file not present, try to load
            if (file.PhysicalFile == null)
            {
                EnsurePhysicalFile(outputFile);
            }

            // If the output data should be cached, return the output data
            bool cacheOutputData = false;
            if (file.Attachment != null)
            {
                // Cache data if allowed
                if (!LatestVersion && (CacheMinutes > 0))
                {
                    cacheOutputData = CacheHelper.CacheImageAllowed(CurrentSiteName, file.Attachment.AttachmentSize);
                }
            }

            // Ensure the file data if physical file not present
            if (!file.DataLoaded && (file.PhysicalFile == ""))
            {
                byte[] cachedData = GetCachedOutputData();
                if (file.EnsureData(cachedData))
                {
                    if ((cachedData == null) && cacheOutputData)
                    {
                        SaveOutputDataToCache(file.OutputData, GetOutputDataDependency(file.Attachment));
                    }
                }
            }

            // Send the file
            if ((file.OutputData != null) || (file.PhysicalFile != ""))
            {
                // Setup the mime type - Fix the special types
                string mimetype = file.MimeType;
                if (file.Attachment != null)
                {
                    string extension = file.Attachment.AttachmentExtension;
                    switch (extension.ToLower())
                    {
                        case ".flv":
                            mimetype = "video/x-flv";
                            break;
                    }

                    // Prepare response
                    Response.ContentType = mimetype;
                    SetDisposition(file.Attachment.AttachmentName, extension);

                    // Setup Etag property
                    ETag = etag;

                    // Set if resumable downloads should be supported
                    AcceptRange = !IsExtensionExcludedFromRanges(extension);
                }

                if (useClientCache && AllowCache)
                {
                    // Set the file time stamps to allow client caching
                    SetTimeStamps(file);

                    Response.Cache.SetETag(etag);
                }
                else
                {
                    SetCacheability();
                }
                
                // Log hit or activity
                LogEvent(file);
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
    private void SetTimeStamps(CMSOutputFile file)
    {
        DateTime expires = DateTime.Now;

        // Send last modified header to allow client caching
        Response.Cache.SetLastModified(file.LastModified);

        if (!file.IsSecured)
        {
            // Setup the client cache
            Response.Cache.SetCacheability(HttpCacheability.Public);
            if (AllowClientCache)
            {
                expires = DateTime.Now.AddMinutes(this.ClientCacheMinutes);
            }
        }

        Response.Cache.SetExpires(expires);
    }


    /// <summary>
    /// Processes the attachment.
    /// </summary>
    protected void ProcessAttachment()
    {
        outputFile = null;

        // If guid given, process the attachment
        guid = QueryHelper.GetGuid("guid", Guid.Empty);
        allowLatestVersion = CheckAllowLatestVersion();

        if (guid != Guid.Empty)
        {
            // Check version
            if (VersionHistoryID > 0)
            {
                ProcessFile(guid, VersionHistoryID);
            }
            else
            {
                ProcessFile(guid);
            }
        }
        else
        {
            // Get by node GUID
            nodeGuid = QueryHelper.GetGuid("nodeguid", Guid.Empty);
            if (nodeGuid != Guid.Empty)
            {
                // If node GUID given, process the file
                ProcessNode(nodeGuid);
            }
            else
            {
                // Get by alias path and file name
                aliasPath = QueryHelper.GetString("aliaspath", null);
                fileName = QueryHelper.GetString("filename", null);
                if (aliasPath != null)
                {
                    ProcessNode(aliasPath, fileName);
                }
            }
        }

        // If chset specified, do not cache
        string chset = QueryHelper.GetString("chset", null);
        if (chset != null)
        {
            mIsLatestVersion = true;
        }
    }


    /// <summary>
    /// Processes the specified file and returns the data to the output stream.
    /// </summary>
    /// <param name="attachmentGuid">Attachment guid</param>
    protected void ProcessFile(Guid attachmentGuid)
    {
        AttachmentInfo atInfo = null;

        bool requiresData = true;

        // Check if it is necessary to load the file data
        if (useClientCache && IsLiveSite && AllowClientCache)
        {
            // If possibly cached by client, do not load data (may not be sent)
            string ifModifiedString = Request.Headers["If-Modified-Since"];
            if (ifModifiedString != null)
            {
                requiresData = false;
            }
        }

        // If output data available from cache, do not require loading the data
        byte[] cachedData = GetCachedOutputData();
        if (cachedData != null)
        {
            requiresData = false;
        }

        // Get AttachmentInfo object
        if (!IsLiveSite)
        {
            // Not livesite mode - get latest version
            if (node != null)
            {
                atInfo = DocumentHelper.GetAttachment(node, attachmentGuid, TreeProvider, true);
            }
            else
            {
                atInfo = DocumentHelper.GetAttachment(attachmentGuid, TreeProvider, CurrentSiteName);
            }
        }
        else
        {
            if (!requiresData || AttachmentManager.StoreFilesInFileSystem(CurrentSiteName))
            {
                // Do not require data from DB - Not necessary or available from file system
                atInfo = AttachmentManager.GetAttachmentInfoWithoutBinary(attachmentGuid, CurrentSiteName);
            }
            else
            {
                // Require data from DB - Stored in DB
                atInfo = AttachmentManager.GetAttachmentInfo(attachmentGuid, CurrentSiteName);
            }

            // If attachment not found, 
            if (allowLatestVersion && ((atInfo == null) || (latestForHistoryId > 0) || (atInfo.AttachmentDocumentID == latestForDocumentId)))
            {
                // Get latest version
                if (node != null)
                {
                    atInfo = DocumentHelper.GetAttachment(node, attachmentGuid, TreeProvider, true);
                }
                else
                {
                    atInfo = DocumentHelper.GetAttachment(attachmentGuid, TreeProvider, CurrentSiteName);
                }

                // If not attachment for the required document, do not return
                if ((atInfo.AttachmentDocumentID != latestForDocumentId) && (latestForHistoryId == 0))
                {
                    atInfo = null;
                }
                else
                {
                    mIsLatestVersion = true;
                }
            }
        }

        if (atInfo != null)
        {
            // Temporary attachment is always latest version
            if (atInfo.AttachmentFormGUID != Guid.Empty)
            {
                mIsLatestVersion = true;
            }

            bool checkPublishedFiles = AttachmentManager.CheckPublishedFiles(CurrentSiteName);
            bool checkFilesPermissions = AttachmentManager.CheckFilesPermissions(CurrentSiteName);

            // Get the document node
            if ((node == null) && (checkPublishedFiles || checkFilesPermissions))
            {
                // Try to get data from cache
                using (CachedSection<TreeNode> cs = new CachedSection<TreeNode>(ref node, CacheMinutes, !allowLatestVersion, null, "getfilenodebydocumentid", atInfo.AttachmentDocumentID))
                {
                    if (cs.LoadData)
                    {
                        // Get the document
                        node = TreeProvider.SelectSingleDocument(atInfo.AttachmentDocumentID, false);

                        // Cache the document
                        CacheNode(cs, node);
                    }
                }
            }

            bool secured = false;
            if ((node != null) && checkFilesPermissions)
            {
                secured = (node.IsSecuredNode == 1);

                // Check secured pages
                if (secured)
                {
                    URLRewriter.CheckSecuredAreas(CurrentSiteName, false, ViewMode);
                }
                if (node.RequiresSSL == 1)
                {
                    URLRewriter.RequestSecurePage(false, node.RequiresSSL, ViewMode, CurrentSiteName);
                }

                // Check permissions
                bool checkPermissions = false;
                switch (URLRewriter.CheckPagePermissions(CurrentSiteName))
                {
                    case PageLocationEnum.All:
                        checkPermissions = true;
                        break;

                    case PageLocationEnum.SecuredAreas:
                        checkPermissions = secured;
                        break;
                }

                // Check the read permission for the page
                if (checkPermissions)
                {
                    if (CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                    {
                        URLHelper.Redirect(URLRewriter.AccessDeniedPageURL(CurrentSiteName));
                    }
                }
            }


            bool resizeImage = (ImageHelper.IsImage(atInfo.AttachmentExtension) && AttachmentManager.CanResizeImage(atInfo, Width, Height, MaxSideSize));

            // If the file should be redirected, redirect the file
            if (!mIsLatestVersion && IsLiveSite && SettingsKeyProvider.GetBoolValue(CurrentSiteName + ".CMSRedirectFilesToDisk"))
            {
                if (AttachmentManager.StoreFilesInFileSystem(CurrentSiteName))
                {
                    string path = null;
                    if (!resizeImage)
                    {
                        path = AttachmentManager.GetFilePhysicalURL(CurrentSiteName, atInfo.AttachmentGUID.ToString(), atInfo.AttachmentExtension);
                    }
                    else
                    {
                        int[] newDim = ImageHelper.EnsureImageDimensions(Width, Height, MaxSideSize, atInfo.AttachmentImageWidth, atInfo.AttachmentImageHeight);
                        path = AttachmentManager.GetFilePhysicalURL(CurrentSiteName, atInfo.AttachmentGUID.ToString(), atInfo.AttachmentExtension, newDim[0], newDim[1]);
                    }

                    // If path is valid, redirect
                    if (path != null)
                    {
                        // Check if file exists
                        string filePath = Server.MapPath(path);
                        if (File.Exists(filePath))
                        {
                            outputFile = NewOutputFile();
                            outputFile.IsSecured = secured;
                            outputFile.RedirectTo = path;
                            outputFile.Attachment = atInfo;
                        }
                    }
                }
            }

            // Get the data
            if ((outputFile == null) || (outputFile.Attachment == null))
            {
                outputFile = NewOutputFile(atInfo, null);
                outputFile.Width = Width;
                outputFile.Height = Height;
                outputFile.MaxSideSize = MaxSideSize;
                outputFile.SiteName = CurrentSiteName;
                outputFile.Resized = resizeImage;

                // Load the data if required
                if (requiresData)
                {
                    // Try to get the physical file, if not latest version
                    if (!mIsLatestVersion)
                    {
                        EnsurePhysicalFile(outputFile);
                    }
                    bool loadData = string.IsNullOrEmpty(outputFile.PhysicalFile);

                    // Load data if necessary
                    if (loadData)
                    {
                        if (atInfo.AttachmentBinary != null)
                        {
                            // Load from the attachment
                            outputFile.LoadData(atInfo.AttachmentBinary, AttachmentManager);
                        }
                        else
                        {
                            // Load from the disk
                            byte[] data = AttachmentManager.GetFile(atInfo, CurrentSiteName);
                            outputFile.LoadData(data, AttachmentManager);
                        }

                        // Save data to the cache, if not latest version
                        if (!mIsLatestVersion && (CacheMinutes > 0))
                        {
                            SaveOutputDataToCache(outputFile.OutputData, GetOutputDataDependency(outputFile.Attachment));
                        }
                    }
                }
                else if (cachedData != null)
                {
                    // Load the cached data if available
                    outputFile.OutputData = cachedData;
                }
            }

            if (outputFile != null)
            {
                outputFile.IsSecured = secured;

                // Add node data
                if (node != null)
                {
                    outputFile.AliasPath = node.NodeAliasPath;
                    outputFile.CultureCode = node.DocumentCulture;
                    outputFile.FileNode = node;

                    // Set the file validity
                    if (IsLiveSite && !mIsLatestVersion && checkPublishedFiles)
                    {
                        outputFile.ValidFrom = ValidationHelper.GetDateTime(node.GetValue("DocumentPublishFrom"), DateTime.MinValue);
                        outputFile.ValidTo = ValidationHelper.GetDateTime(node.GetValue("DocumentPublishTo"), DateTime.MaxValue);

                        // Set the published flag                   
                        outputFile.IsPublished = node.IsPublished;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Processes the specified document node.
    /// </summary>
    /// <param name="currentAliasPath">Alias path</param>
    /// <param name="currentFileName">File name</param>
    protected void ProcessNode(string currentAliasPath, string currentFileName)
    {
        // Load the document node
        if (node == null)
        {
            // Try to get data from cache
            using (CachedSection<TreeNode> cs = new CachedSection<TreeNode>(ref node, CacheMinutes, !allowLatestVersion, null, "getfilenodebyaliaspath|", CurrentSiteName, CacheHelper.GetBaseCacheKey(false, true), currentAliasPath))
            {
                if (cs.LoadData)
                {
                    // Get the document
                    string className = null;
                    bool combineWithDefaultCulture = SettingsKeyProvider.GetBoolValue(CurrentSiteName + ".CMSCombineImagesWithDefaultCulture");
                    string culture = CultureCode;

                    // Get the document
                    if (currentFileName == null)
                    {
                        // CMS.File
                        className = "CMS.File";
                    }

                    // Get the document data
                    if (!IsLiveSite)
                    {
                        node = DocumentHelper.GetDocument(CurrentSiteName, currentAliasPath, culture, combineWithDefaultCulture, className, null, null, -1, false, null, TreeProvider);
                    }
                    else
                    {
                        node = TreeProvider.SelectSingleNode(CurrentSiteName, currentAliasPath, culture, combineWithDefaultCulture, className, null, null, -1, false, null);
                    }

                    // Try to find node using the document aliases
                    if (node == null)
                    {
                        DataSet ds = DocumentAliasInfoProvider.GetDocumentAliases("AliasURLPath='" + SqlHelperClass.GetSafeQueryString(currentAliasPath, false) + "'", "AliasCulture DESC", 1, "AliasNodeID, AliasCulture");
                        if (!DataHelper.DataSourceIsEmpty(ds))
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            int nodeId = (int)dr["AliasNodeID"];
                            string nodeCulture = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "AliasCulture"), null);
                            if (!IsLiveSite)
                            {
                                node = DocumentHelper.GetDocument(nodeId, nodeCulture, combineWithDefaultCulture, TreeProvider);
                            }
                            else
                            {
                                node = TreeProvider.SelectSingleNode(nodeId, nodeCulture, combineWithDefaultCulture);
                            }
                        }
                    }

                    // Cache the document
                    CacheNode(cs, node);
                }
            }
        }

        // Process the document
        ProcessNode(node, null, currentFileName);
    }


    /// <summary>
    /// Processes the specified document node.
    /// </summary>
    /// <param name="currentNodeGuid">Node GUID</param>
    protected void ProcessNode(Guid currentNodeGuid)
    {
        // Load the document node
        string columnName = QueryHelper.GetString("columnName", String.Empty);
        if (node == null)
        {
            // Try to get data from cache
            using (CachedSection<TreeNode> cs = new CachedSection<TreeNode>(ref node, CacheMinutes, !allowLatestVersion, null, "getfilenodebyguid|", CurrentSiteName, CacheHelper.GetBaseCacheKey(false, true), currentNodeGuid))
            {
                if (cs.LoadData)
                {
                    // Get the document
                    bool combineWithDefaultCulture = SettingsKeyProvider.GetBoolValue(CurrentSiteName + ".CMSCombineImagesWithDefaultCulture");
                    string culture = CultureCode;
                    string where = "NodeGUID = '" + currentNodeGuid + "'";

                    // Get the document
                    string className = null;
                    if (columnName == "")
                    {
                        // CMS.File
                        className = "CMS.File";
                    }
                    else
                    {
                        // Other document types
                        TreeNode srcNode = TreeProvider.SelectSingleNode(currentNodeGuid, CultureCode, CurrentSiteName);
                        if (srcNode != null)
                        {
                            className = srcNode.NodeClassName;
                        }
                    }

                    // Get the document data
                    if (!IsLiveSite || allowLatestVersion)
                    {
                        node = DocumentHelper.GetDocument(CurrentSiteName, null, culture, combineWithDefaultCulture, className, where, null, -1, false, null, TreeProvider);
                    }
                    else
                    {
                        node = TreeProvider.SelectSingleNode(CurrentSiteName, null, culture, combineWithDefaultCulture, className, where, null, -1, false, null);
                    }

                    // Cache the document
                    CacheNode(cs, node);
                }
            }
        }

        // Process the document node
        ProcessNode(node, columnName, null);
    }


    /// <summary>
    /// Processes the specified document node.
    /// </summary>
    /// <param name="treeNode">Document node to process</param>
    /// <param name="columnName">Column name</param>
    /// <param name="processedFileName">File name</param>
    protected void ProcessNode(TreeNode treeNode, string columnName, string processedFileName)
    {
        if (treeNode != null)
        {
            // Check if latest or live site version is required
            bool latest = !IsLiveSite;
            if (allowLatestVersion && ((treeNode.DocumentID == latestForDocumentId) || (treeNode.DocumentCheckedOutVersionHistoryID == latestForHistoryId)))
            {
                latest = true;
            }

            // If not published, return no content
            if (!latest && !treeNode.IsPublished)
            {
                outputFile = NewOutputFile(null, null);
                outputFile.AliasPath = treeNode.NodeAliasPath;
                outputFile.CultureCode = treeNode.DocumentCulture;
                if (IsLiveSite && AttachmentManager.CheckPublishedFiles(CurrentSiteName))
                {
                    outputFile.IsPublished = treeNode.IsPublished;
                }
                outputFile.FileNode = treeNode;
                outputFile.Height = Height;
                outputFile.Width = Width;
                outputFile.MaxSideSize = MaxSideSize;
            }
            else
            {
                // Get valid site name if link
                if (treeNode.IsLink)
                {
                    TreeNode origNode = TreeProvider.GetOriginalNode(treeNode);
                    if (origNode != null)
                    {
                        SiteInfo si = SiteInfoProvider.GetSiteInfo(origNode.NodeSiteID);
                        if (si != null)
                        {
                            CurrentSiteName = si.SiteName;
                        }
                    }
                }

                // Process the node
                // Get from specific column
                if (String.IsNullOrEmpty(columnName) && String.IsNullOrEmpty(processedFileName) && treeNode.NodeClassName.Equals("CMS.File", StringComparison.InvariantCultureIgnoreCase))
                {
                    columnName = "FileAttachment";
                }
                if (!String.IsNullOrEmpty(columnName))
                {
                    // File document type or specified by column
                    Guid attachmentGuid = ValidationHelper.GetGuid(treeNode.GetValue(columnName), Guid.Empty);
                    if (attachmentGuid != Guid.Empty)
                    {
                        ProcessFile(attachmentGuid);
                    }
                }
                else
                {
                    // Get by file name
                    if (processedFileName == null)
                    {
                        // CMS.File - Get 
                        Guid attachmentGuid = ValidationHelper.GetGuid(treeNode.GetValue("FileAttachment"), Guid.Empty);
                        if (attachmentGuid != Guid.Empty)
                        {
                            ProcessFile(attachmentGuid);
                        }
                    }
                    else
                    {
                        // Other document types, get the attachment by file name
                        AttachmentInfo ai = null;
                        if (latest)
                        {
                            // Not livesite mode - get latest version
                            ai = DocumentHelper.GetAttachment(treeNode, processedFileName, TreeProvider, false);
                        }
                        else
                        {
                            // Live site mode, get directly from database
                            ai = AttachmentManager.GetAttachmentInfo(treeNode.DocumentID, processedFileName, false);
                        }

                        if (ai != null)
                        {
                            ProcessFile(ai.AttachmentGUID);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Processes the specified version of the file and returns the data to the output stream.
    /// </summary>
    /// <param name="attachmentGuid">Attachment GUID</param>
    /// <param name="versionHistoryId">Document version history ID</param>
    protected void ProcessFile(Guid attachmentGuid, int versionHistoryId)
    {
        AttachmentInfo atInfo = GetFile(attachmentGuid, versionHistoryId);
        if (atInfo != null)
        {
            // If attachment is image, try resize
            byte[] mFile = atInfo.AttachmentBinary;
            if (mFile != null)
            {
                string mimetype = null;
                if (ImageHelper.IsImage(atInfo.AttachmentExtension))
                {
                    if (AttachmentManager.CanResizeImage(atInfo, Width, Height, MaxSideSize))
                    {
                        // Do not search thumbnail on the disk
                        mFile = AttachmentManager.GetImageThumbnail(atInfo, CurrentSiteName, Width, Height, MaxSideSize, false);
                        mimetype = "image/jpeg";
                    }
                }

                if (mFile != null)
                {
                    outputFile = NewOutputFile(atInfo, mFile);
                }
                else
                {
                    outputFile = NewOutputFile();
                }
                outputFile.Height = Height;
                outputFile.Width = Width;
                outputFile.MaxSideSize = MaxSideSize;
                outputFile.MimeType = mimetype;
            }

            // Get the file document
            if (node == null)
            {
                node = TreeProvider.SelectSingleDocument(atInfo.AttachmentDocumentID);
            }

            if (node != null)
            {
                // Check secured area
                SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
                if (si != null)
                {
                    if (pi == null)
                    {
                        pi = PageInfoProvider.GetPageInfo(si.SiteName, node.NodeAliasPath, node.DocumentCulture, node.DocumentUrlPath, false);
                    }
                    if (pi != null)
                    {
                        URLRewriter.RequestSecurePage(pi, false, ViewMode, CurrentSiteName);
                        URLRewriter.CheckSecuredAreas(CurrentSiteName, pi, false, ViewMode);
                    }
                }

                // Check the permissions for the document
                if ((CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed) || (node.NodeOwner == CurrentUser.UserID))
                {
                    if (outputFile == null)
                    {
                        outputFile = NewOutputFile();
                    }

                    outputFile.AliasPath = node.NodeAliasPath;
                    outputFile.CultureCode = node.DocumentCulture;
                    if (IsLiveSite && AttachmentManager.CheckPublishedFiles(CurrentSiteName))
                    {
                        outputFile.IsPublished = node.IsPublished;
                    }
                    outputFile.FileNode = node;
                }
                else
                {
                    outputFile = null;
                }
            }
        }
    }


    /// <summary>
    /// Gets the file from version history.
    /// </summary>
    /// <param name="attachmentGuid">Atachment GUID</param>
    /// <param name="versionHistoryId">Version history ID</param>
    protected AttachmentInfo GetFile(Guid attachmentGuid, int versionHistoryId)
    {
        VersionManager vm = new VersionManager(TreeProvider);

        // Get the attachment version
        AttachmentHistoryInfo attachmentVersion = vm.GetAttachmentVersion(versionHistoryId, attachmentGuid);
        if (attachmentVersion == null)
        {
            return null;
        }
        else
        {
            // Create the attachment object from the version
            AttachmentInfo ai = new AttachmentInfo(attachmentVersion.Generalized.DataClass);
            ai.AttachmentVersionHistoryID = versionHistoryId;
            return ai;
        }
    }


    /// <summary>
    /// Returns the output data dependency based on the given attachment record.
    /// </summary>
    /// <param name="ai">Attachment object</param>
    protected CacheDependency GetOutputDataDependency(AttachmentInfo ai)
    {
        return (ai == null) ? null : CacheHelper.GetCacheDependency(AttachmentManager.GetDependencyCacheKeys(ai));
    }


    /// <summary>
    /// Returns the cache dependency for the given document node.
    /// </summary>
    /// <param name="node">Document node</param>
    protected CacheDependency GetNodeDependency(TreeNode node)
    {
        string siteName = CurrentSiteName.ToLower();

        return CacheHelper.GetCacheDependency(new string[] { 
            CacheHelper.FILENODE_KEY,
            CacheHelper.FILENODE_KEY + "|" + siteName,
            "node|" + siteName + "|" + node.NodeAliasPath.ToLower() 
        });
    }


    /// <summary>
    /// Ensures the security settings in the given document node.
    /// </summary>
    /// <param name="node">Document node</param>
    protected void EnsureSecuritySettings(TreeNode node)
    {
        if (AttachmentManager.CheckFilesPermissions(CurrentSiteName))
        {
            // Load secured values
            node.LoadInheritedValues(new string[] { "IsSecuredNode", "RequiresSSL" });
        }
    }


    /// <summary>
    /// Handles the document caching actions.
    /// </summary>
    /// <param name="cs">Cached section</param>
    /// <param name="node">Document node</param>
    protected void CacheNode(CachedSection<TreeNode> cs, TreeNode node)
    {
        if (node != null)
        {
            // Load the security settings
            EnsureSecuritySettings(node);

            // Save to the cache
            if (cs.Cached)
            {
                cs.CacheDependency = GetNodeDependency(node);
                cs.Data = node;
            }
        }
        else
        {
            // Do not cache in case not cached
            cs.CacheMinutes = 0;
        }
    }


    /// <summary>
    /// Logs analytics and/or activity event.
    /// </summary>
    /// <param name="file">File to be sent</param>
    protected void LogEvent(CMSOutputFile file)
    {
        if (IsLiveSite && (file != null) && (file.FileNode != null) && (file.FileNode.NodeClassName.ToLower() == "cms.file"))
        {
            // Check if request is multipart request and log event if not
            GetRange(100, HttpContext.Current);  // GetRange() parses request header and sets 'IsMultipart' property
            if (!IsMultipart)
            {
                if (IsRangeRequest && (BrowserHelper.IsIE() || BrowserHelper.IsChrome()))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            if (file.Attachment == null)
            {
                return;
            }

            // Log analytics hit
            if (AnalyticsHelper.IsLoggingEnabled(CurrentSiteName, String.Empty) && AnalyticsHelper.TrackFileDownloadsEnabled(CurrentSiteName) && !AnalyticsHelper.IsFileExtensionExcluded(CurrentSiteName, file.Attachment.AttachmentExtension))
            {
                HitLogProvider.LogHit(HitLogProvider.FILE_DOWNLOADS, CurrentSiteName, file.FileNode.DocumentCulture, file.FileNode.NodeAliasPath, file.FileNode.NodeID);
            }

            // Log download activity
            // Get current contact ID and check if activity logging is enabled
            int contactId = 0;
            bool loggingActivityEnabled = LoggingActivityEnabled(file, out contactId);
            if (loggingActivityEnabled && (contactId > 0))
            {
                ActivityLogHelper.LogFileDownload(contactId, CurrentSiteName, file.FileNode.NodeID, file.FileNode.DocumentName, file.Attachment.AttachmentName, file.FileNode.DocumentCulture);
            }
        }
    }


    /// <summary>
    /// Checks if page visit activity logging is enabled, if so returns contact ID.
    /// </summary>
    /// <param name="file">File to be sent</param>
    /// <param name="contactId">Current contact ID</param>
    protected bool LoggingActivityEnabled(CMSOutputFile file, out int contactId)
    {
        contactId = 0;
        if ((file == null) || (file.FileNode == null))
        {
            return false;
        }

        // Check if logging is enabled
        if (ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(CurrentSiteName) && ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) &&
            ActivitySettingsHelper.PageVisitsEnabled(CurrentSiteName))
        {
            if (file.Attachment != null)
            {
                // Get allowed extensions (if not specified log everything)
                bool doLog = true;
                string tracked = SettingsKeyProvider.GetStringValue(CurrentSiteName + ".CMSActivityTrackedExtensions");
                if (!String.IsNullOrEmpty(tracked))
                {
                    string extension = file.Attachment.AttachmentExtension;
                    if (extension != null)
                    {
                        string extensions = String.Format(";{0};", tracked.ToLower().Trim().Trim(';'));
                        extension = extension.TrimStart('.').ToLower();
                        doLog = extensions.Contains(String.Format(";{0};", extension));
                    }
                }

                if (doLog)
                {
                    // Check if logging is enabled for current document
                    TreeNode fileNode = file.FileNode;
                    if ((fileNode != null) && ((fileNode.DocumentLogVisitActivity == true)
                                           || (fileNode.DocumentLogVisitActivity == null) && ValidationHelper.GetBoolean(fileNode.GetInheritedValue("DocumentLogVisitActivity", SiteInfoProvider.CombineWithDefaultCulture(CurrentSiteName)), false)))
                    {
                        contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
                        return (contactId > 0);
                    }
                }
            }
        }
        return false;
    }


    /// <summary>
    /// Ensures the physical file.
    /// </summary>
    /// <param name="file">Output file</param>
    public bool EnsurePhysicalFile(CMSOutputFile file)
    {
        if (file == null)
        {
            return false;
        }

        // Try to link to file system
        if (String.IsNullOrEmpty(file.Watermark) && (file.Attachment != null) && (file.Attachment.AttachmentVersionHistoryID == 0) && AttachmentManager.StoreFilesInFileSystem(file.SiteName))
        {
            string filePath = AttachmentManager.EnsurePhysicalFile(file.Attachment, file.SiteName);
            if (filePath != null)
            {
                if (file.Resized)
                {
                    // If resized, ensure the thumbnail file
                    if (AttachmentManager.GenerateThumbnails(file.SiteName))
                    {
                        filePath = AttachmentManager.EnsureThumbnailFile(file.Attachment, file.SiteName, Width, Height, MaxSideSize);
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
    /// Returns true if latest version of the document is allowed.
    /// </summary>
    public bool CheckAllowLatestVersion()
    {
        // Check if latest version is required
        latestForDocumentId = QueryHelper.GetInteger("latestfordocid", 0);
        latestForHistoryId = QueryHelper.GetInteger("latestforhistoryid", 0);

        if ((latestForDocumentId > 0) || (latestForHistoryId > 0))
        {
            // Validate the hash
            string hash = QueryHelper.GetString("hash", "");
            string validate = (latestForDocumentId > 0) ? "d" + latestForDocumentId : "h" + latestForHistoryId;

            if (!String.IsNullOrEmpty(hash) && QueryHelper.ValidateHashString(validate, hash))
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets the new output file object.
    /// </summary>
    public CMSOutputFile NewOutputFile()
    {
        CMSOutputFile file = new CMSOutputFile();

        file.Watermark = this.Watermark;
        file.WatermarkPosition = this.WatermarkPosition;

        return file;
    }


    /// <summary>
    /// Gets the new output file object.
    /// </summary>
    /// <param name="ai">AttachmentInfo</param>
    /// <param name="data">Output file data</param>
    public CMSOutputFile NewOutputFile(AttachmentInfo ai, byte[] data)
    {
        CMSOutputFile file = new CMSOutputFile(ai, data);

        file.Watermark = this.Watermark;
        file.WatermarkPosition = this.WatermarkPosition;

        return file;
    }
}
