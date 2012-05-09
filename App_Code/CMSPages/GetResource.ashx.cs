using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Caching;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.IO.Compression;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.OutputFilter;

using Microsoft.Ajax.Utilities;

/// <summary>
/// Handler that serves minified and compressed resources.
/// </summary>
public class ResourceHandler : IHttpHandler
{
    #region "Constants"

    /// <summary>
    /// Supported querystring argument used to identify javascript files.
    /// </summary>
    private const string JS_FILE_ARGUMENT = "scriptfile";


    /// <summary>
    /// Supported querystring argument used to identify newsletter stylesheets stored in a database.
    /// </summary>
    private const string NEWSLETTERCSS_DATABASE_ARGUMENT = "newslettertemplatename";


    /// <summary>
    /// Extension of a CSS file.
    /// </summary>
    private const string CSS_FILE_EXTENSION = ".css";


    /// <summary>
    /// Extension of JS file.
    /// </summary>
    private const string JS_FILE_EXTENSION = ".js";


    /// <summary>
    /// Argument delimiter used for separating multiple values in a parameter.
    /// </summary>
    private static readonly char[] ARGUMENT_DELIMITER = { ';' };

    #endregion


    #region "Variables"

    /// <summary>
    /// CSS minifier.
    /// </summary>
    private static readonly CssMinifier mCssMinifier = new CssMinifier();


    /// <summary>
    /// JavaScript minifier.
    /// </summary>
    private static readonly JavaScriptMinifier mJsMinifier = new JavaScriptMinifier();

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets a value indicating whether another request can use the IHttpHandler instance.
    /// </summary>
    public bool IsReusable
    {
        get
        {
            return true;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    private static string SiteName
    {
        get
        {
            return CMSAppBase.ApplicationInitialized ? CMSContext.CurrentSiteName : string.Empty;
        }
    }


    /// <summary>
    /// Gets if page belongs to the live site, otherwise it's a CMS or preview mode page.
    /// </summary>
    private static bool IsLiveSite
    {
        get
        {
            return (CMSContext.ViewMode == ViewModeEnum.LiveSite);
        }
    }


    /// <summary>
    /// Gets the number of minutes the resource will be cached on the server.
    /// </summary>
    private static int CacheMinutes
    {
        get
        {
            if (CMSAppBase.ApplicationInitialized)
            {
                return CacheHelper.CacheImageMinutes(SiteName);
            }
            else
            {
                return 0;
            }
        }
    }


    /// <summary>
    /// Gets the number of minutes the resource will be cached on the client.
    /// </summary>
    private static int ClientCacheMinutes
    {
        get
        {
            if (CMSAppBase.ApplicationInitialized)
            {
                return CacheHelper.ClientCacheMinutes(SiteName);
            }
            else
            {
                return 0;
            }
        }
    }


    /// <summary>
    /// Gets if the client caching is enabled.
    /// </summary>
    private static bool ClientCacheEnabled
    {
        get
        {
            return (CacheHelper.ClientCacheRequested && (ClientCacheMinutes > 0) || DocumentBase.UseFullClientCache);
        }
    }


    /// <summary>
    /// Gets if the client cache should be revalidated to ensure all data is up-to-date.
    /// </summary>
    private static bool RevalidateClientCache
    {
        get
        {
            return CMSAppBase.ApplicationInitialized ? CacheHelper.RevalidateClientCache(SiteName) : true;
        }
    }


    /// <summary>
    /// Gets the number of minutes large files (those over maximum allowed size) will be cached on the server.
    /// </summary>
    private static int PhysicalFilesCacheMinutes
    {
        get
        {
            return CacheHelper.PhysicalFilesCacheMinutes;
        }
    }


    /// <summary>
    /// Gets if Not Found response should be used when the resouce cannot be located or if the reuqest should terminate normally.
    /// </summary>
    private static bool Throw404WhenNotFound
    {
        get
        {
            return true;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Processes the incoming HTTP request that and returns the specified stylesheets.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    public void ProcessRequest(HttpContext context)
    {
        // Disable debugging
        if (!DebugHelper.DebugResources)
        {
            // Disable the debugging
            RequestSettings requestSettings = RequestSettings.Current;

            requestSettings.DebugFiles = false;
            requestSettings.DebugSecurity = false;
            requestSettings.DebugCache = false;
            requestSettings.DebugOutput = false;
            requestSettings.DebugRequest = false;
            requestSettings.DebugSQLQueries = false;

            OutputHelper.LogCurrentOutputToFile = false;
        }

        // When no parameters are specified, simply end the response
        if (!context.Request.QueryString.HasKeys())
        {
            SendNoContent(context);
        }

        if (QueryHelper.Contains(JS_FILE_ARGUMENT))
        {
            // Process JS file request
            ProcessJSFileRequest(context);
            return;
        }

        // Transfer to newsletter CSS
        string newsletterTemplateName = QueryHelper.GetString(NEWSLETTERCSS_DATABASE_ARGUMENT, "");
        if (!String.IsNullOrEmpty(newsletterTemplateName))
        {
            context.Server.Transfer("~/CMSModules/Newsletters/CMSPages/GetCSS.aspx?newslettertemplatename=" + newsletterTemplateName);
            return;
        }

        // Load the settings
        CMSCssSettings settings = new CMSCssSettings();
        settings.LoadFromQueryString();

        // Process the request
        ProcessRequest(context, settings);
    }


    /// <summary>
    /// Processes the given request.
    /// </summary>
    /// <param name="context">Http context</param>
    /// <param name="settings">CSS Settings</param>
    private static void ProcessRequest(HttpContext context, CMSCssSettings settings)
    {
        CMSOutputResource resource = null;

        // Get cache setting for physical files
        int cacheMinutes = PhysicalFilesCacheMinutes;
        int clientCacheMinutes = cacheMinutes;

        bool hasVirtualContent = settings.HasVirtualContent();
        if (hasVirtualContent)
        {
            // Use specific cache settings if DB resources are requested
            cacheMinutes = CacheMinutes;
            clientCacheMinutes = ClientCacheMinutes;
        }

        // Try to get data from cache (or store them if not found)
        using (CachedSection<CMSOutputResource> cachedSection = new CachedSection<CMSOutputResource>(ref resource, cacheMinutes, true, null, "getresource", CMSContext.CurrentSiteName, context.Request.QueryString, URLHelper.IsSSL))
        {
            // Not found in cache; load the data
            if (cachedSection.LoadData)
            {
                // Load the data
                resource = GetResource(settings, URLHelper.Url.ToString(), cachedSection.Cached);

                // Cache the file
                if ((resource != null) && (cachedSection.Cached))
                {
                    cachedSection.CacheDependency = resource.CacheDependency;
                    cachedSection.Data = resource;
                }
            }
        }

        // Send response if there's something to send
        if (resource != null)
        {
            bool allowCache = (!hasVirtualContent || ClientCacheEnabled) && CacheHelper.AlwaysCacheResources;
            SendResponse(context, resource, MimeTypeHelper.GetMimetype(CSS_FILE_EXTENSION), allowCache, CSSHelper.StylesheetMinificationEnabled, clientCacheMinutes);
        }
        else
        {
            SendNotFoundResponse(context);
        }
    }

    #endregion


    #region "General methods"

    /// <summary>
    /// Combines the given list of resources into a single resource
    /// </summary>
    /// <param name="resources">Resources to combine</param>
    private static CMSOutputResource CombineResources(List<CMSOutputResource> resources)
    {
        StringBuilder data = new StringBuilder();
        StringBuilder etag = new StringBuilder();

        DateTime lastModified = DateTimeHelper.ZERO_TIME;

        // Build single resource
        foreach (CMSOutputResource resource in resources)
        {
            if (resource != null)
            {
                string newData = resource.Data;

                // Join the data into a single string
                if ((data.Length > 0) && !String.IsNullOrEmpty(newData))
                {
                    // Trim the charset
                    newData = CSSHelper.TrimCharset(newData);
                    if (String.IsNullOrEmpty(newData))
                    {
                        continue;
                    }

                    data.AppendLine();
                    data.AppendLine();
                }

                data.Append(newData);

                // Join e-tags
                if (etag.Length > 0)
                {
                    etag.Append('|');
                }
                etag.Append(resource.Etag);

                // Remember the largest last modified
                if (resource.LastModified > lastModified)
                {
                    lastModified = resource.LastModified;
                }
            }
        }

        // Build the result
        CMSOutputResource result = new CMSOutputResource()
        {
            Data = data.ToString(),
            Etag = etag.ToString(),
            LastModified = lastModified
        };

        return result;
    }


    /// <summary>
    /// Sends a response containing the requested data.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="resource">Container with the data to serve</param>
    /// <param name="contentType">Content type to use when sending a response</param>
    /// <param name="allowCache">True, if client caching is enabled, otherwise false</param>
    /// <param name="minificationEnabled">True, if the data can be served minified, otherwise false</param>
    /// <param name="clientCacheMinutes">Number of minutes after which the content in the client cache expires</param>
    private static void SendResponse(HttpContext context, CMSOutputResource resource, string contentType, bool allowCache, bool minificationEnabled, int clientCacheMinutes)
    {
        // Set client cache revalidation
        SetRevalidation(context);

        // Let client use data cached in browser if versions match and there was no change in data
        if (allowCache && IsResourceUnchanged(context, resource))
        {
            SendNotModifiedResponse(context, resource.LastModified, resource.Etag, clientCacheMinutes, true);
            return;
        }
        else
        {
            // Otherwise get content to send
            string contentCoding;
            byte[] content = GetOutputData(context, resource, minificationEnabled, out contentCoding);

            // Set client caching
            if (allowCache)
            {
                SetClientCaching(context, allowCache, resource.LastModified, resource.Etag, clientCacheMinutes);
            }

            if (contentCoding != ContentCodingEnum.IDENTITY)
            {
                context.Response.AppendHeader("Content-Encoding", contentCoding);
                context.Response.AppendHeader("Vary", "Content-Encoding");
            }

            context.Response.ContentType = contentType;

            // Do not send output if there's none
            if (content.Length > 0)
            {
                context.Response.OutputStream.Write(content, 0, content.Length);
            }
        }
    }


    /// <summary>
    /// Send a Not Found response when the requested data was not located successfully.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    private static void SendNotFoundResponse(HttpContext context)
    {
        if (Throw404WhenNotFound)
        {
            RequestHelper.LogRequestOperation("404NotFound", URLHelper.Url.ToString(), 1);
            RequestHelper.Respond404();
        }
        else
        {
            RequestHelper.EndResponse();
        }
    }


    /// <summary>
    /// Sends the Not Modified response when the data on the client matches those on the server.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="lastModified">Timestamp for the last modification of the data</param>
    /// <param name="etag">Etag used to identify the resources</param>
    /// <param name="clientCacheMinutes">Number of minutes after which the content in the client cache expires</param>
    /// <param name="publicCache">True, if the data can be cached by cache servers on the way, false if only by requesting client</param>
    private static void SendNotModifiedResponse(HttpContext context, DateTime lastModified, string etag, int clientCacheMinutes, bool publicCache)
    {
        // Set the status to Not modified
        context.Response.StatusCode = (int)HttpStatusCode.NotModified;
        context.Response.Cache.SetETag(etag);

        if (publicCache)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
        }

        DateTime expires = DateTime.Now.AddMinutes(clientCacheMinutes);

        // No not allow time in future
        if (lastModified >= DateTime.Now)
        {
            lastModified = DateTime.Now.AddSeconds(-1);
        }

        context.Response.Cache.SetLastModified(lastModified);
        context.Response.Cache.SetExpires(expires);

        RequestHelper.LogRequestOperation("304NotModified", etag, 1);
        RequestHelper.EndResponse();
    }


    /// <summary>
    /// Sends the No Content response when there was no data specified in request.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    private static void SendNoContent(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NoContent;

        RequestHelper.LogRequestOperation("204NoContent", string.Empty, 1);
        RequestHelper.EndResponse();
    }


    /// <summary>
    /// Sets the client cache revalidation.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    private static void SetRevalidation(HttpContext context)
    {
        if (RevalidateClientCache)
        {
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }
        else
        {
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.None);
        }
    }


    /// <summary>
    /// Sets the client caching.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="lastModified">Timestamp for the last modification of the data</param>
    /// <param name="etag">Etag used to identify the resources</param>
    /// <param name="clientCacheMinutes">Number of minutes after which the content in the client cache expires</param>
    private static void SetClientCaching(HttpContext context, bool allowCache, DateTime lastModified, string etag, int clientCacheMinutes)
    {
        DateTime expires = allowCache ? DateTime.Now.AddMinutes(clientCacheMinutes) : DateTime.Now;

        // No not allow time in future
        if (lastModified >= DateTime.Now)
        {
            lastModified = DateTime.Now.AddSeconds(-1);
        }

        context.Response.Cache.SetLastModified(lastModified);
        context.Response.Cache.SetExpires(expires);

        context.Response.Cache.SetETag(etag);

        context.Response.Cache.SetCacheability(HttpCacheability.Public);
    }


    /// <summary>
    /// Checks if resource in the client cache matches the server version.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="resource">Resource to check</param>
    /// <returns>true, if resource is unchanged, otherwise false</returns>
    private static bool IsResourceUnchanged(HttpContext context, CMSOutputResource resource)
    {
        // Determine the last modified date and etag sent from the browser
        string currentETag = RequestHelper.GetHeader("If-None-Match", string.Empty);
        string ifModified = RequestHelper.GetHeader("If-Modified-Since", string.Empty);

        // If resources match, compare last modification timestamps
        if ((ifModified != string.Empty) && (currentETag == resource.Etag))
        {
            // Get first part of header (colons can delimit additional data)
            DateTime modifiedStamp;
            if (DateTime.TryParse(ifModified.Split(";".ToCharArray())[0], out modifiedStamp))
            {
                return (resource.LastModified <= modifiedStamp.AddSeconds(1));
            }
        }

        return false;
    }


    /// <summary>
    /// Checks if this is a revalidation request for a physical file that did not change since the last time.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="path">Full physical path to the file</param>
    private static void CheckRevalidation(HttpContext context, string path)
    {
        // Virtual resource, used only to check if revalidation can be short-circuited
        CMSOutputResource fileResource = new CMSOutputResource()
        {
            Etag = GetFileEtag(path),
            LastModified = File.GetLastWriteTime(path)
        };

        if (IsResourceUnchanged(context, fileResource))
        {
            SendNotModifiedResponse(context, fileResource.LastModified, fileResource.Etag, PhysicalFilesCacheMinutes, true);
        }
    }


    /// <summary>
    /// Reads a file in the given path.
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <param name="fileExtension">File extension to check against</param>
    /// <returns>Content of the file</returns>
    private static string ReadFile(string path, string fileExtension)
    {
        // Return empty string if file doesn't exist or is not supported
        if (!File.Exists(path) || (Path.GetExtension(path) != fileExtension))
        {
            return null;
        }

        // Try to read the contents of the file
        try
        {
            using (StreamReader sr = StreamReader.New(path))
            {
                return sr.ReadToEnd();
            }
        }
        catch
        {
            return string.Empty;
        }
    }


    /// <summary>
    /// Compresses a given text.
    /// </summary>
    /// <param name="resource">Text to compress</param>
    /// <returns>Compressed text</returns>
    private static byte[] Compress(string resource)
    {
        byte[] compressedBuffer = null;

        // Uses in-memory deflate stream to compress the resource
        using (MemoryStream memory = MemoryStream.New())
        {
            using (DeflateStream compressor = DeflateStream.New(memory, CompressionMode.Compress))
            {
                using (StreamWriter writer = StreamWriter.New(compressor))
                {
                    writer.Write(resource);
                }
            }
            compressedBuffer = memory.ToArray();
        }

        return compressedBuffer;
    }


    /// <summary>
    /// Decompresses a given text.
    /// </summary>
    /// <param name="resource">Text to decompress</param>
    /// <returns>Decompressed text</returns>
    private static string Decompress(byte[] resource)
    {
        // Uses in-memory deflate stream to decompress the resource
        using (MemoryStream memory = MemoryStream.New(resource))
        {
            using (DeflateStream decompressor = DeflateStream.New(memory, CompressionMode.Decompress))
            {
                using (StreamReader reader = StreamReader.New(decompressor))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }


    /// <summary>
    /// Minify supplied source according to settings.
    /// </summary>
    /// <param name="resource">Resource to minifz</param>
    /// <param name="minifier">Minifier to use when creating minified version of the data</param>
    /// <param name="minificationEnabled">True, if the data should be minified, otherwise false</param>
    /// <param name="compressionEnabled">True, if data should be compressed, otherwise false</param>
    private static void MinifyResource(CMSOutputResource resource, IResourceMinifier minifier, bool minificationEnabled, bool compressionEnabled)
    {
        if (resource == null)
        {
            return;
        }

        // Set up the settings
        if (minificationEnabled && (minifier != null))
        {
            resource.MinifiedData = minifier.Minify(resource.Data);
        }

        // Compress
        if (RequestHelper.AllowResourceCompression && compressionEnabled)
        {
            resource.CompressedData = Compress(resource.Data);
        }

        // Compress and minify
        if (minificationEnabled && RequestHelper.AllowResourceCompression && compressionEnabled)
        {
            resource.MinifiedCompressedData = Compress(resource.MinifiedData);
        }
    }


    /// <summary>
    /// Wraps a piece of text in a data container.
    /// </summary>
    /// <param name="resource">Text to wrap</param>
    /// <param name="name">Identifier for the file</param>
    /// <param name="etag">Etag to use for versioning</param>
    /// <param name="lastModified">Timestamp of the last modification of data</param>
    /// <param name="minifier">Minifier to use when creating minified version of the data</param>
    /// <param name="minificationEnabled">True, if the data should be minified, otherwise false</param>
    /// <returns>Data container containing the piece of text</returns>
    private static CMSOutputResource WrapObject(string resource, string name, string etag, DateTime lastModified, IResourceMinifier minifier, bool minificationEnabled)
    {
        if (resource == null)
        {
            return null;
        }

        // Prepare new output resource object
        CMSOutputResource wrapper = new CMSOutputResource()
        {
            Name = name,
            Etag = etag,
            LastModified = lastModified,
            Data = resource,
        };

        // Set up the settings
        if (minificationEnabled)
        {
            wrapper.MinifiedData = minifier.Minify(wrapper.Data);
        }
        if (RequestHelper.AllowResourceCompression)
        {
            wrapper.CompressedData = Compress(resource);
        }
        if (minificationEnabled && RequestHelper.AllowResourceCompression)
        {
            wrapper.MinifiedCompressedData = Compress(wrapper.MinifiedData);
        }

        return wrapper;
    }


    /// <summary>
    /// Returns the data which will be served to client depending on minification and compression settings.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="resource">Data container with the data to serve</param>
    /// <param name="minificationEnabled">True, if the data should be minified, otherwise false</param>
    /// <param name="contentCoding">The content coding to use when sending a response</param>
    /// <returns>Data to serve in a form of a byte block</returns>
    private static byte[] GetOutputData(HttpContext context, CMSOutputResource resource, bool minificationEnabled, out string contentCoding)
    {
        // minification must be allowed by the server and minified data must be available
        bool minified = minificationEnabled && resource.ContainsMinifiedData;

        // compression must be allowed by server, supported by client and compressed data must be available
        bool compressed = RequestHelper.AllowResourceCompression && RequestHelper.IsGZipSupported() && resource.ContainsCompressedData;

        // Set deafult content encoding
        contentCoding = ContentCodingEnum.IDENTITY;

        // Get the proper version of resource to serve based on the settings
        if (!minified && !compressed)
        {
            return Encoding.UTF8.GetBytes(resource.Data);
        }
        else if (minificationEnabled && !compressed)
        {
            return Encoding.UTF8.GetBytes(resource.MinifiedData);
        }
        else if (!minified && compressed)
        {
            contentCoding = ContentCodingEnum.DEFLATE;
            return resource.CompressedData;
        }
        else if (minified && compressed)
        {
            contentCoding = ContentCodingEnum.DEFLATE;
            return resource.MinifiedCompressedData;
        }
        else
        {
            return new byte[0];
        }
    }


    /// <summary>
    /// Creates an E-tag for a file given its full path.
    /// </summary>
    /// <param name="path">Full physical path to file</param>
    /// <returns>E-tag for the file specified</returns>
    private static string GetFileEtag(string path)
    {
        return Convert.ToBase64String(Encoding.Unicode.GetBytes(path));
    }

    #endregion


    #region "Specialized methods"

    /// <summary>
    /// Processes a request for a JavaScript file identified by its URL.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    private static void ProcessJSFileRequest(HttpContext context)
    {
        ProcessFileRequest(context, JS_FILE_ARGUMENT, JS_FILE_EXTENSION, mJsMinifier, MimeTypeHelper.GetMimetype(JS_FILE_EXTENSION), ScriptHelper.ScriptMinificationEnabled);
    }


    /// <summary>
    /// Processes a request for a file.
    /// </summary>
    /// <param name="context">An HTTPContext object that provides references to the intrinsic server objects used to service HTTP requests</param>
    /// <param name="queryArgument">Name of the argument whose value specifies the location of the data</param>
    /// <param name="fileExtension">File extension to check against (to prevent serving unauthorized content)</param>
    /// <param name="minifier">Minifier that should be used to transform the original data</param>
    /// <param name="contentType">Content type to use when sending a response</param>
    /// <param name="minificationEnabled">True, if the data should be minified, otherwise false</param>
    private static void ProcessFileRequest(HttpContext context, string queryArgument, string fileExtension, IResourceMinifier minifier, string contentType, bool minificationEnabled)
    {
        // Get URL to the resource file, resolve it in case it's virtual and map to physical path        
        string url = QueryHelper.GetString(queryArgument, string.Empty);
        string path = URLHelper.GetPhysicalPath(URLHelper.GetVirtualPath(url));

        // If this is revalidation request, try quick revalidation check before reading the file
        CheckRevalidation(context, path);

        CMSOutputResource resource = null;

        // Try to get data from cache (or store them if not found)
        using (CachedSection<CMSOutputResource> cachedSection = new CachedSection<CMSOutputResource>(ref resource, PhysicalFilesCacheMinutes, true, null, "getresource", path, URLHelper.IsSSL))
        {
            // Not found in cache; load the data
            if (cachedSection.LoadData)
            {
                // Retrieve the file resource, rebase client URLs and wrap it in output container
                resource = GetFile(url, fileExtension);
                MinifyResource(resource, minifier, minificationEnabled, true);

                // Cache the file
                if ((resource != null) && (cachedSection.Cached))
                {
                    cachedSection.CacheDependency = new CMSCacheDependency(path);
                    cachedSection.Data = resource;
                }
            }
        }

        // Send response if there's something to send
        if (resource != null)
        {
            bool allowCache = CacheHelper.AlwaysCacheResources;
            SendResponse(context, resource, contentType, allowCache, minificationEnabled, PhysicalFilesCacheMinutes);
        }
        else
        {
            SendNotFoundResponse(context);
        }
    }


    /// <summary>
    /// Retrieves the specified resources and wraps them in an data container.
    /// </summary>
    /// <param name="settings">CSS settings</param>
    /// <param name="name">Resource name</param>
    /// <param name="cached">If true, the result will be cached</param>
    /// <returns>The data container with the resulting stylesheet data</returns>
    private static CMSOutputResource GetResource(CMSCssSettings settings, string name, bool cached)
    {
        List<CMSOutputResource> resources = new List<CMSOutputResource>();

        // Add files
        if (settings.Files != null)
        {
            foreach (string item in settings.Files)
            {
                // Get the resource
                CMSOutputResource resource = GetFile(item, CSS_FILE_EXTENSION);
                resources.Add(resource);
            }
        }

        // Add stylesheets
        if (settings.Stylesheets != null)
        {
            foreach (string item in settings.Stylesheets)
            {
                // Get the resource
                CMSOutputResource resource = GetStylesheet(item);
                resources.Add(resource);
            }
        }

        // Add web part containers
        if (settings.Containers != null)
        {
            foreach (string item in settings.Containers)
            {
                // Get the resource
                CMSOutputResource resource = GetContainer(item);
                resources.Add(resource);
            }
        }

        // Add web parts
        if (settings.WebParts != null)
        {
            foreach (string item in settings.WebParts)
            {
                // Get the resource
                CMSOutputResource resource = GetWebPart(item);
                resources.Add(resource);
            }
        }

        // Add templates
        if (settings.Templates != null)
        {
            foreach (string item in settings.Templates)
            {
                // Get the resource
                CMSOutputResource resource = GetTemplate(item);
                resources.Add(resource);
            }
        }

        // Add layouts
        if (settings.Layouts != null)
        {
            foreach (string item in settings.Layouts)
            {
                // Get the resource
                CMSOutputResource resource = GetLayout(item);
                resources.Add(resource);
            }
        }

        // Add transformation containers
        if (settings.Transformations != null)
        {
            foreach (string item in settings.Transformations)
            {
                // Get the resource
                CMSOutputResource resource = GetTransformation(item);
                resources.Add(resource);
            }
        }

        // Add web part layouts
        if (settings.WebPartLayouts != null)
        {
            foreach (string item in settings.WebPartLayouts)
            {
                // Get the resource
                CMSOutputResource resource = GetWebPartLayout(item);
                resources.Add(resource);
            }
        }

        // Combine to a single output
        CMSOutputResource result = CombineResources(resources);

        // Resolve the macros
        if (CSSHelper.ResolveMacrosInCSS)
        {
            MacroContext context = new MacroContext()
            {
                TrackCacheDependencies = cached
            };

            if (cached)
            {
                // Add the default dependencies
                context.AddCacheDependencies(settings.GetCacheDependencies());
                context.AddFileCacheDependencies(settings.GetFileCacheDependencies());
            }

            result.Data = CMSContext.ResolveMacros(result.Data, context);

            if (cached)
            {
                // Add cache dependency
                result.CacheDependency = CacheHelper.GetCacheDependency(context.FileCacheDependencies, context.CacheDependencies);
            }
        }
        else if (cached)
        {
            // Only the cache dependency from settings
            result.CacheDependency = settings.GetCacheDependency();
        }

        // Minify
        MinifyResource(result, mCssMinifier, CSSHelper.StylesheetMinificationEnabled && settings.EnableMinification, settings.EnableMinification);

        return result;
    }


    /// <summary>
    /// Retrieves the stylesheet from file
    /// </summary>
    /// <param name="url">File URL</param>
    /// <param name="extension">File extension</param>
    /// <returns>The stylesheet data (plain version only)</returns>    
    private static CMSOutputResource GetFile(string url, string extension)
    {
        string path = URLHelper.GetPhysicalPath(URLHelper.GetVirtualPath(url));

        // Get the file content
        string fileContent = ReadFile(path, extension);
        fileContent = HTMLHelper.ResolveCSSClientUrls(fileContent, URLHelper.GetAbsoluteUrl(url));

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = fileContent,
            Name = path,
            Etag = GetFileEtag(path),
            LastModified = File.GetLastWriteTime(path)
        };

        return resource;
    }


    /// <summary>
    /// Retrieve the stylesheet either from the database or file if checked out.
    /// </summary>
    /// <param name="stylesheetName">Stylesheet's unique name</param>
    /// <returns>The stylesheet data (plain version only)</returns>    
    private static CMSOutputResource GetStylesheet(string stylesheetName)
    {
        // Get the stylesheet
        CssStylesheetInfo stylesheetInfo = CssStylesheetInfoProvider.GetCssStylesheetInfo(stylesheetName);
        if (stylesheetInfo == null)
        {
            return null;
        }

        // Determine if the stylesheet is checked out or not
        string checkedOutFile = URLHelper.GetPhysicalPath(stylesheetInfo.StylesheetCheckedOutFilename);
        string stylesheet;
        DateTime lastModified;

        if ((stylesheetInfo.StylesheetCheckedOutByUserID > 0) &&
            (string.Equals(stylesheetInfo.StylesheetCheckedOutMachineName, HTTPHelper.MachineName, StringComparison.OrdinalIgnoreCase)) &&
            (File.Exists(checkedOutFile)))
        {
            // Read the stylesheet and timestamp from the checked out file
            using (StreamReader reader = StreamReader.New(checkedOutFile))
            {
                stylesheet = reader.ReadToEnd();
            }
            lastModified = File.GetLastWriteTime(checkedOutFile);
        }
        else
        {
            // Read the stylesheet and timestamp from database
            stylesheet = stylesheetInfo.StylesheetText;
            lastModified = stylesheetInfo.StylesheetLastModified;
        }

        // Build the output
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(stylesheet, URLHelper.ApplicationPath),
            Name = stylesheetInfo.StylesheetName,
            LastModified = lastModified,
            Etag = stylesheetName,
        };

        return resource;
    }


    /// <summary>
    /// Retrieves the stylesheets of the web part container from the database.
    /// </summary>
    /// <param name="containerName">Container name</param>
    /// <returns>The stylesheet data (plain version only)</returns>
    private static CMSOutputResource GetContainer(string containerName)
    {
        WebPartContainerInfo containerInfo = WebPartContainerInfoProvider.GetWebPartContainerInfo(containerName);
        if (containerInfo == null)
        {
            return null;
        }

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(containerInfo.ContainerCSS, URLHelper.ApplicationPath),
            LastModified = containerInfo.ContainerLastModified,
            Etag = containerInfo.ContainerName
        };

        return resource;
    }


    /// <summary>
    /// Retrieves the stylesheets of the web part layout from the database.
    /// </summary>
    /// <param name="layoutFullName">Layout full name</param>
    /// <returns>The stylesheet data (plain version only)</returns>
    private static CMSOutputResource GetWebPartLayout(string layoutFullName)
    {
        WebPartLayoutInfo layoutInfo = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(layoutFullName);
        if (layoutInfo == null)
        {
            return null;
        }

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(layoutInfo.WebPartLayoutCSS, URLHelper.ApplicationPath),
            LastModified = layoutInfo.WebPartLayoutLastModified,
            Etag = layoutInfo.WebPartLayoutFullName
        };

        return resource;
    }


    /// <summary>
    /// Retrieves the stylesheets of the page template from the database.
    /// </summary>
    /// <param name="templateName">Template name</param>
    /// <returns>The stylesheet data (plain version only)</returns>
    private static CMSOutputResource GetTemplate(string templateName)
    {
        // Try to get global one
        PageTemplateInfo templateInfo = PageTemplateInfoProvider.GetPageTemplateInfo(templateName);

        // Try to get site one (ad-hoc) if not found
        if (templateInfo == null)
        {
            templateInfo = PageTemplateInfoProvider.GetPageTemplateInfo(templateName, CMSContext.CurrentSiteID);
        }

        if (templateInfo == null)
        {
            return null;
        }

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(templateInfo.PageTemplateCSS, URLHelper.ApplicationPath),
            LastModified = templateInfo.PageTemplateLastModified,
            Etag = templateInfo.PageTemplateVersionGUID
        };

        return resource;
    }


    /// <summary>
    /// Retrieves the stylesheets of the layout from the database.
    /// </summary>
    /// <param name="layoutName">Layout name</param>
    /// <returns>The stylesheet data (plain version only)</returns>
    private static CMSOutputResource GetLayout(string layoutName)
    {
        LayoutInfo layoutInfo = LayoutInfoProvider.GetLayoutInfo(layoutName);
        if (layoutInfo == null)
        {
            return null;
        }

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(layoutInfo.LayoutCSS, URLHelper.ApplicationPath),
            LastModified = layoutInfo.LayoutLastModified,
            Etag = layoutInfo.LayoutVersionGUID
        };

        return resource;
    }


    /// <summary>
    /// Retrieves the stylesheet of the web part from the database.
    /// </summary>
    /// <param name="webPartName">Web part name</param>
    /// <returns>The stylesheet data (plain version only)</returns>
    private static CMSOutputResource GetWebPart(string webPartName)
    {
        WebPartInfo webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPartName);
        if (webPartInfo == null)
        {
            return null;
        }

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(webPartInfo.WebPartCSS, URLHelper.ApplicationPath),
            LastModified = webPartInfo.WebPartLastModified,
            Etag = webPartInfo.WebPartName
        };

        return resource;
    }


    /// <summary>
    /// Retrieves the stylesheets of the web part layout from the database.
    /// </summary>
    /// <param name="layoutFullName">Layout full name</param>
    /// <returns>The stylesheet data (plain version only)</returns>
    private static CMSOutputResource GetTransformation(string transformationFullName)
    {
        TransformationInfo transformationInfo = TransformationInfoProvider.GetTransformation(transformationFullName);
        if (transformationInfo == null)
        {
            return null;
        }

        // Build the result
        CMSOutputResource resource = new CMSOutputResource()
        {
            Data = HTMLHelper.ResolveCSSUrls(transformationInfo.TransformationCSS, URLHelper.ApplicationPath),
            LastModified = transformationInfo.TransformationLastModified,
            Etag = transformationInfo.TransformationVersionGUID
        };

        return resource;
    }

    #endregion
}


#region "Minifiers"

/// <summary>
/// Defines an interface a compliant minifier must implement to be usable in resource handler.
/// </summary>
public interface IResourceMinifier
{
    /// <summary>
    /// Returns a minified version of a given resource.
    /// </summary>
    /// <param name="resource">Text to be minified</param>
    /// <returns>Minified resource</returns>
    string Minify(string resource);
}


/// <summary>
/// Minifier for Cascading StyleSheets.
/// </summary>
public class CssMinifier : IResourceMinifier
{
    /// <summary>
    /// Indicates if parsing failed for specific reason
    /// </summary>
    private CssException minificationError = null;

    
    private bool? mLogMinifierParseError = null;

    // Indicates if errors from minification should be logged
    private bool LogMinifierParseError
    {
        get
        {
            if (mLogMinifierParseError == null)
            {
                mLogMinifierParseError = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSLogCSSMinifierParseError"], false);
            }

            return (bool)mLogMinifierParseError;
        }
    }
    

    /// <summary>
    /// Returns a minified version of a given CSS.
    /// </summary>
    /// <param name="resource">CSS to be minified</param>
    /// <returns>Minified CSS</returns>
    public string Minify(string resource)
    {
        if (String.IsNullOrEmpty(resource))
        {
            return resource;
        }

        // Reset error
        minificationError = null;

        try
        {
            CssParser parser = new CssParser();

            parser.CssError += parser_CssError;

            // Parse the resource
            string parsed = parser.Parse(resource);
            if (!String.IsNullOrEmpty(parsed) && (minificationError == null))
            {
                resource = parsed;
            }
        }
        catch (CssException ex)
        {
            minificationError = ex;
        }

        if (minificationError != null)
        {
            if (LogMinifierParseError)
            {
                // Log exception to event log if allowed
                EventLogProvider.LogException("CSS Compression", "MINIFYCSS", minificationError);
            }

            // Add error info in front of non-minified resource
            resource += "\r\n\r\n/* Minification failed (line " + minificationError.Line.ToString() + "): " + minificationError.Message + " */";
        }
        
        return resource;
    }


    /// <summary>
    /// Parsing error occured - event handler.
    /// </summary>
    private void parser_CssError(object sender, CssErrorEventArgs e)
    {
        // Do not minify in the case of parse exception and log the error
        minificationError = e.Exception;
    }
}


/// <summary>
/// Minifier for JavaScript.
/// </summary>
public class JavaScriptMinifier : IResourceMinifier
{
    /// <summary>
    /// Indicates if parsing failed for specific reason
    /// </summary>
    private JScriptException minificationError = null;


    /// <summary>
    /// Indicates if the parser can recover when an error occurs during minification
    /// </summary>
    private bool canRecover = true;


    private bool? mLogMinifierParseError = null;

    // Indicates if errors from minification should be logged
    private bool LogMinifierParseError
    {
        get
        {
            if (mLogMinifierParseError == null)
            {
                mLogMinifierParseError = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSLogJSMinifierParseError"], false);
            }

            return (bool)mLogMinifierParseError;
        }
    }


    /// <summary>
    /// Returns a minified version of a given JavaScript.
    /// </summary>
    /// <param name="resource">JavaScript to be minified</param>
    /// <returns>Minified JavaScript</returns>
    public string Minify(string resource)
    {
        if (String.IsNullOrEmpty(resource))
        {
            return resource;
        }

        // Reset error
        minificationError = null;
        canRecover = true;

        try
        {
            JSParser parser = new JSParser(resource);
            parser.CompilerError += parser_CompilerError;

            // Parse the resource
            Block scriptBlock = parser.Parse(null);

            // Get minified code if no error occurs or parser was able to recover
            if ((scriptBlock != null) && ((minificationError == null) || ((minificationError != null) && canRecover)))
            {
                resource = scriptBlock.ToCode();
            }
        }
        catch (JScriptException ex)
        {
            minificationError = ex;
            canRecover = false;
        }

        if (minificationError != null)
        {
            if (LogMinifierParseError)
            {
                // Log exception to event log if allowed
                EventLogProvider.LogException("JS Compression", "MINIFYJS", minificationError);
            }

            if (!canRecover)
            {
                // Add error info in front of non-minified resource
                resource += "\r\n\r\n// Minification failed (line " + minificationError.Line.ToString() + "): " + minificationError.Message;
            }
        }

        return resource;
    }


    private void parser_CompilerError(object sender, JScriptExceptionEventArgs e)
    {
        // Store exception and info if the compiler can recover
        minificationError = e.Exception;
        canRecover = e.Exception.CanRecover;
    }
}

#endregion