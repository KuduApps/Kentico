using System;
using System.Web;
using System.Web.Caching;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_CMSPages_GetCSS : GetFilePage
{
    #region "Variables"

    protected bool useClientCache = true;

    protected CMSOutputResource outputFile = null;
    protected CssStylesheetInfo si = null;
    protected EmailTemplate et = null;
    string newsletterTemplateName = null;

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
                // By default, cache for the newsletter CSS is always enabled (even outside of the live site)
                if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSAlwaysCacheNewsletterCSS"], true))
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


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the site
        if (string.IsNullOrEmpty(CurrentSiteName))
        {
            throw new Exception("[GetCSS.aspx]: Site not running.");
        }

        newsletterTemplateName = QueryHelper.GetString("newslettertemplatename", string.Empty);
        string cacheKey = string.Format("getnewslettercss|{0}|{1}", CMSContext.CurrentSiteName, newsletterTemplateName);

        // Try to get data from cache
        using (CachedSection<CMSOutputResource> cs = new CachedSection<CMSOutputResource>(ref outputFile, this.CacheMinutes, true, cacheKey))
        {
            if (cs.LoadData)
            {
                // Process the file
                ProcessStylesheet();

                // Ensure the cache settings
                if ((outputFile != null) && (cs.Cached))
                {
                    // Add cache dependency
                    CacheDependency cd = CacheHelper.GetCacheDependency(new string[] { "newsletter.emailtemplate|byname|" + newsletterTemplateName.ToLower() });

                    // Cache the data
                    cs.CacheDependency = cd;
                    cs.Data = outputFile;
                }
            }
        }

        if (outputFile != null)
        {
            // Send the data
            SendFile(outputFile);
        }
    }


    /// <summary>
    /// Processes the stylesheet.
    /// </summary>
    protected void ProcessStylesheet()
    {
        // Newsletter template stylesheet
        if (!string.IsNullOrEmpty(newsletterTemplateName))
        {
            // Get the template
            et = EmailTemplateProvider.GetEmailTemplate(newsletterTemplateName, CMSContext.CurrentSiteID);
            if (et != null)
            {
                // Create the output file
                outputFile = new CMSOutputResource()
                {
                    Name = URLHelper.Url.ToString(),
                    Data = HTMLHelper.ResolveCSSUrls(et.TemplateStylesheetText, URLHelper.ApplicationPath),
                    Etag = et.TemplateName
                };
            }
        }
    }


    /// <summary>
    /// Sends the given file within response.
    /// </summary>
    /// <param name="file">File to send</param>
    protected void SendFile(CMSOutputResource file)
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

        // Send the file
        if ((file != null) && (file.Data != null))
        {
            // Client caching - only on the live site
            if (useClientCache && (CMSContext.ViewMode == ViewModeEnum.LiveSite) && (CacheHelper.CacheImageEnabled(CurrentSiteName)) && ETagsMatch(file.Etag, file.LastModified))
            {
                RespondNotModified(file.Etag, true);                
                return;
            }

            // Prepare response
            Response.ContentType = "text/css";

            if (useClientCache && (CMSContext.ViewMode == ViewModeEnum.LiveSite) && (CacheHelper.CacheImageEnabled(CurrentSiteName)))
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
                Response.Cache.SetETag(file.Etag);
            }

            // Add the file data
            Response.Write(file.Data);
        }

        CompleteRequest();
    }

    #endregion
}