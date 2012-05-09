using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.Caching;
using System.Collections.Generic;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.PortalEngine;

public partial class CMSWebParts_Localization_languageselectiondropdown : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value than indicates whether culture names are displayed.
    /// </summary>
    public bool ShowCultureNames
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowCultureNames"), true);
        }
        set
        {
            SetValue("ShowCultureNames", value);
        }
    }


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
    /// Gets or sets the value than indicates whether the control is shown.
    /// </summary>
    public bool HideIfOneCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideIfOneCulture"), true);
        }
        set
        {
            this.SetValue("HideIfOneCulture", value);
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

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            string siteName = CMSContext.CurrentSiteName;

            // If there is only one culture on site and hiding is enabled hide webpart
            if (HideIfOneCulture && !CultureInfoProvider.IsSiteMultilignual(siteName))
            {
                this.Visible = false;
                return;
            }

            DataSet ds = null;

            // Try to get data from cache
            using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref ds, this.CacheMinutes, true, this.CacheItemName, "languageselection", siteName))
            {
                if (cs.LoadData)
                {
                    // Get the data
                    ds = CultureInfoProvider.GetSiteCultures(siteName);

                    // Save to the cache
                    if (cs.Cached)
                    {
                        cs.CacheDependency = CacheHelper.GetCacheDependency(new string[] { "cms.culturesite|all", "cms.culture|all" });
                        cs.Data = ds;
                    }
                }
            }

            // Add CSS Stylesheet
            string cssUrl = URLHelper.ResolveUrl("~/CMSWebparts/Localization/languageselectiondropdown_files/langselector.css");
            this.Page.Header.Controls.Add(new LiteralControl("<link href=\"" + cssUrl + "\" type=\"text/css\" rel=\"stylesheet\" />"));

            // Build current URL
            string url = URLHelper.CurrentURL;
            url = URLHelper.RemoveParameterFromUrl(url, URLHelper.LanguageParameterName);
            url = URLHelper.RemoveParameterFromUrl(url, URLHelper.AliasPathParameterName);

            string imgFlagIcon = "";

            StringBuilder result = new StringBuilder();
            result.Append("<ul class=\"langselector\">");

            // Current language
            CultureInfo culture = CultureInfoProvider.GetCultureInfo(CMSContext.CurrentUser.PreferredCultureCode);
            if (culture != null)
            {
                // Drop down imitating icon
                string dropIcon = ResolveUrl("~/CMSWebparts/Localization/languageselectiondropdown_files/dd_arrow.gif");

                // Current language
                imgFlagIcon = this.GetImageUrl("Flags/16x16/" + HTMLHelper.HTMLEncode(culture.CultureCode) + ".png");

                string currentCultureShortName = "";
                if (this.ShowCultureNames)
                {
                    currentCultureShortName = HTMLHelper.HTMLEncode(culture.CultureShortName);
                }

                result.AppendFormat("<li class=\"lifirst\" style=\"background-image:url('{0}'); background-repeat: no-repeat\"><a class=\"first\" style=\"background-image:url({1}); background-repeat: no-repeat\" href=\"{2}\">{3}</a>",
                    dropIcon, imgFlagIcon, HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(url, URLHelper.LanguageParameterName, culture.CultureCode)), currentCultureShortName);
            }

            // List of languages
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

                    if (currentPageInfo != null)
                    {
                        // Try to get data from cache
                        using (CachedSection<Dictionary<string, string>> cs = new CachedSection<Dictionary<string, string>>(ref documentCultures, this.CacheMinutes, true, cacheItemName, "languageselectionprefix", siteName, currentPageInfo.NodeAliasPath.ToLower()))
                        {
                            if (cs.LoadData)
                            {
                                // Initialize tree provider object
                                TreeProvider tp = new TreeProvider(CMSContext.CurrentUser);
                                tp.FilterOutDuplicates = false;
                                tp.CombineWithDefaultCulture = false;

                                // Get all language versions
                                DataSet culturesDs = tp.SelectNodes(siteName, "/%", TreeProvider.ALL_CULTURES, false, null, "NodeID = " + currentPageInfo.NodeId, null, -1, true, 0, "DocumentCulture, DocumentUrlPath");

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
                                    cs.CacheDependency = this.GetCacheDependency();
                                    cs.Data = documentCultures;
                                }
                            }
                        }
                    }
                }

                result.Append("<ul>");

                // Create options for other culture
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string cultureCode = dr["CultureCode"].ToString();
                    string cultureShortName = dr["CultureShortName"].ToString();
                    string cultureAlias = dr["CultureAlias"].ToString();

                    bool documentCultureExists = true;

                    // Check whether exists document in specified culture
                    if ((documentCultures != null) && (this.HideUnavailableCultures))
                    {
                        documentCultureExists = documentCultures.ContainsKey(cultureCode.ToLower());
                    }

                    if (documentCultureExists)
                    {
                        if (CMSContext.CurrentDocument != null && !string.IsNullOrEmpty(CMSContext.CurrentDocument.DocumentCulture) && !((HideCurrentCulture) && (String.Compare(CMSContext.CurrentDocument.DocumentCulture, cultureCode, true) == 0)))
                        {
                            url = null;
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
                                url = TreePathUtils.GetUrl(CMSContext.CurrentAliasPath, urlPath, siteName, lang);
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

                            // Language icon
                            imgFlagIcon = this.GetImageUrl("Flags/16x16/" + HTMLHelper.HTMLEncode(cultureCode) + ".png");

                            if (this.ShowCultureNames)
                            {
                                cultureShortName = HTMLHelper.HTMLEncode(cultureShortName);
                            }
                            else
                            {
                                cultureShortName = "";
                            }
                            result.AppendFormat("<li><a style=\"background-image:url({0}); background-repeat: no-repeat\" href=\"{1}\">{2}</a></li>\r\n",
                                imgFlagIcon, HTMLHelper.HTMLEncode(URLHelper.ResolveUrl(url)), cultureShortName);
                        }
                    }
                }
                result.Append("</ul>");
            }
            result.Append("</li></ul>");
            ltlLanguages.Text = result.ToString();
        }
    }


    /// <summary>
    /// Reloads data for partial caching.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }

    #endregion
}
