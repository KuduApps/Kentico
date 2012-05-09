using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Collections.Generic;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.PortalEngine;

public partial class CMSWebParts_Localization_languageselection : CMSAbstractWebPart
{
    private string mSeparator = " ";

    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether the URLs to the specific documents should be generate with language prefix.
    /// This property is usable only if language prefix for URLs is enabled.
    /// </summary>
    public bool UseURLsWithLangPrefix
    {
        get
        {
            return URLHelper.UseLangPrefixForUrls(CMSContext.CurrentSiteName) && ValidationHelper.GetBoolean(GetValue("UseURLsWithLangPrefix"), true);
        }
        set
        {
            SetValue("UseURLsWithLangPrefix", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the link for current culture should be hidden.
    /// </summary>
    public bool HideCurrentCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideCurrentCulture"), false);
        }
        set
        {
            SetValue("HideCurrentCulture", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the links to the existing culture versions should be displayed.
    /// </summary>
    public bool HideUnavailableCultures
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideUnavailableCultures"), false);
        }
        set
        {
            SetValue("HideUnavailableCultures", value);
        }
    }


    /// <summary>
    /// Gets or sets the display layout.
    /// </summary>
    public string DisplayLayout
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DisplayLayout"), "");
        }
        set
        {
            SetValue("DisplayLayout", value);
            mSeparator = value.ToLower() == "vertical" ? "<br />" : " ";
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data for partial caching.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do not process
        }
        else
        {
            string culture = CMSContext.PreferredCultureCode;

            mSeparator = DisplayLayout.ToLower() == "vertical" ? "<br />" : " ";

            DataSet ds = null;

            // Try to get data from cache
            using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref ds, this.CacheMinutes, true, this.CacheItemName, "languageselection", CMSContext.CurrentSiteName))
            {
                if (cs.LoadData)
                {
                    // Get the data
                    ds = CultureInfoProvider.GetSiteCultures(CMSContext.CurrentSiteName);

                    // Add to the cache
                    if (cs.Cached)
                    {
                        cs.CacheDependency = this.GetCacheDependency();
                        cs.Data = ds;
                    }
                }
            }

            if (!DataHelper.DataSourceIsEmpty(ds) && (ds.Tables[0].Rows.Count > 1))
            {
                // Collection of available documents in culture
                Dictionary<string, string> documentCultures = null;
                // Check whether hiding is required or URLs should be generated with lang prefix
                if (this.HideUnavailableCultures || this.UseURLsWithLangPrefix)
                {
                    string cacheItemName = this.CacheItemName;
                    if (!String.IsNullOrEmpty(cacheItemName))
                    {
                        cacheItemName += "prefix";
                    }

                    // Current page info
                    PageInfo currentPageInfo = CMSContext.CurrentPageInfo;
                    
                    // Try to get data from cache
                    using (CachedSection<Dictionary<string, string>> cs = new CachedSection<Dictionary<string, string>>(ref documentCultures, this.CacheMinutes, true, cacheItemName, "languageselectionprefix", CMSContext.CurrentSiteName, currentPageInfo.NodeAliasPath.ToLower()))
                    {
                        if (cs.LoadData)
                        {
                            // Initialize tree provider object
                            TreeProvider tp = new TreeProvider(CMSContext.CurrentUser);
                            tp.FilterOutDuplicates = false;
                            tp.CombineWithDefaultCulture = false;

                            // Get all language versions
                            DataSet culturesDs = tp.SelectNodes(CMSContext.CurrentSiteName, "/%", TreeProvider.ALL_CULTURES, false, null, "NodeID = " + currentPageInfo.NodeId, null, -1, true, 0, "DocumentCulture, DocumentUrlPath");

                            // Create culture/UrlPath collection
                            if (!DataHelper.DataSourceIsEmpty(culturesDs))
                            {
                                documentCultures = new Dictionary<string, string>();
                                foreach (DataRow dr in culturesDs.Tables[0].Rows)
                                {
                                    string docCulture = ValidationHelper.GetString(dr["DocumentCulture"], String.Empty).ToLower();
                                    string urlPath = ValidationHelper.GetString(dr["DocumentUrlPath"], String.Empty);
                                    documentCultures.Add(docCulture, urlPath);
                                }
                            }

                            // Add to the cache
                            if (cs.Cached)
                            {
                                cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] { "cms.culturesite|all", "cms.culture|all", "node|" + CurrentSiteName.ToLower() + "|" + currentPageInfo.NodeAliasPath.ToLower() });
                                cs.Data = documentCultures;
                            }
                        }
                    }
                }

                // Render the cultures
                ltlHyperlinks.Text = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string cultureCode = Convert.ToString(dr["CultureCode"]);
                    string cultureAlias = Convert.ToString(dr["CultureAlias"]);
                    string cultureShortName = Convert.ToString(dr["CultureShortName"]);
                    // Indicates whether exists document in specific culture
                    bool documentCultureExists = true;

                    // Check whether exists document in specified culture
                    if ((documentCultures != null) && (this.HideUnavailableCultures))
                    {
                        documentCultureExists = documentCultures.ContainsKey(cultureCode.ToLower());
                    }

                    if (documentCultureExists)
                    {
                        if (!(HideCurrentCulture && (String.Compare(CMSContext.CurrentDocument.DocumentCulture, cultureCode, true) == 0)))
                        {
                            if (String.Compare(culture, cultureCode, StringComparison.InvariantCultureIgnoreCase) != 0)
                            {

                                string url = null;
                                string lang = cultureCode;
                                // Check whether culture alias is defined and if so use it
                                if (!String.IsNullOrEmpty(cultureAlias))
                                {
                                    lang = cultureAlias;
                                }

                                // Get specific url with language prefix
                                if (this.UseURLsWithLangPrefix && documentCultures != null)
                                {
                                    string urlPath = String.Empty;
                                    if (documentCultures.ContainsKey(cultureCode.ToLower()))
                                    {
                                        urlPath = documentCultures[cultureCode.ToLower()];
                                    }
                                    url = TreePathUtils.GetUrl(CMSContext.CurrentAliasPath, urlPath, CMSContext.CurrentSiteName, lang);
                                    url += URLHelper.GetQuery(URLHelper.CurrentURL);
                                    url = URLHelper.RemoveParameterFromUrl(url, URLHelper.LanguageParameterName);
                                    url = URLHelper.RemoveParameterFromUrl(url, URLHelper.AliasPathParameterName);
                                }
                                // Get URL with lang parameter
                                else
                                {
                                    // Build current URL
                                    url = URLHelper.CurrentURL;
                                    url = URLHelper.RemoveParameterFromUrl(url, URLHelper.LanguageParameterName);
                                    url = URLHelper.RemoveParameterFromUrl(url, URLHelper.AliasPathParameterName);
                                    url = URLHelper.AddParameterToUrl(url, URLHelper.LanguageParameterName, lang);
                                }



                                ltlHyperlinks.Text += "<a href=\"" + url + "\">" + HTMLHelper.HTMLEncode(cultureShortName) + "</a>";
                            }
                            else
                            {
                                ltlHyperlinks.Text += cultureShortName;
                            }
                            ltlHyperlinks.Text += mSeparator;
                        }
                    }
                }
            }
            else
            {
                Visible = false;
            }
        }
    }


    /// <summary>
    /// Clears the cached items.
    /// </summary>
    public override void ClearCache()
    {
        string useCacheItemName = DataHelper.GetNotEmpty(CacheItemName, CacheHelper.BaseCacheKey + "|" + URLHelper.Url + "|" + ClientID);

        CacheHelper.ClearCache(useCacheItemName);
    }

    
    /// <summary>
    /// Gets the default cache dependencies.
    /// </summary>
    public override string GetDefaultCacheDependendencies()
    {
        // Get default dependencies
        string result = base.GetDefaultCacheDependendencies();

        if (result != null)
        {
            result += "\n";
        }

        result += "cms.culturesite|all";
        result += "\ncms.culture|all";
 
        return result;
    }
}
