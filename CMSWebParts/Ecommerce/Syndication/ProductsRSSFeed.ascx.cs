using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;

public partial class CMSWebParts_Ecommerce_Syndication_ProductsRSSFeed : CMSAbstractWebPart
{
    #region "RSS Feed Properties"

    /// <summary>
    /// Querystring key which is used for RSS feed identification on a page with multiple RSS feeds.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("QueryStringKey"), null), rssFeed.QueryStringKey);
        }
        set
        {
            SetValue("QueryStringKey", value);
            rssFeed.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Feed name to identify this feed on a page with multiple feeds. If the value is empty the GUID of the web part instance will be used by default.
    /// </summary>
    public string FeedName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("FeedName"), null), GetIdentificator());
        }
        set
        {
            string valueToSet = value;
            // If no feed name was specified
            if (string.IsNullOrEmpty(valueToSet))
            {
                // Set default name
                valueToSet = GetIdentificator();
            }
            SetValue("FeedName", valueToSet);
            rssFeed.FeedName = valueToSet;
        }
    }


    /// <summary>
    /// Text for the feed link.
    /// </summary>
    public string LinkText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkText"), string.Empty);
        }
        set
        {
            SetValue("LinkText", value);
            rssFeed.LinkText = value;
        }
    }


    /// <summary>
    /// Icon which will be displayed in the feed link.
    /// </summary>
    public string LinkIcon
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkIcon"), string.Empty);
        }
        set
        {
            SetValue("LinkIcon", value);
            rssFeed.LinkIcon = value;
        }
    }


    /// <summary>
    /// Indicates if the RSS feed is automatically discovered by the browser.
    /// </summary>
    public bool EnableRSSAutodiscovery
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("EnableRSSAutodiscovery"), true);
        }
        set
        {
            SetValue("EnableRSSAutodiscovery", value);
        }
    }

    #endregion


    #region "RSS Repeater properties"

    /// <summary>
    /// URL title of the feed.
    /// </summary>
    public string FeedTitle
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FeedTitle"), string.Empty);
        }
        set
        {
            SetValue("FeedTitle", value);
            rssFeed.FeedTitle = value;
        }
    }


    /// <summary>
    /// Description of the feed.
    /// </summary>
    public string FeedDescription
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FeedDescription"), string.Empty);
        }
        set
        {
            SetValue("FeedDescription", value);
            rssFeed.FeedDescription = value;
        }
    }


    /// <summary>
    /// Language of the feed. If the value is empty the content culture will be used.
    /// </summary>
    public string FeedLanguage
    {
        get
        {
            string cultureCode = ValidationHelper.GetString(GetValue("FeedLanguage"), null);
            if (string.IsNullOrEmpty(cultureCode))
            {
                cultureCode = CMSContext.PreferredCultureCode;
            }
            return cultureCode;
        }
        set
        {
            SetValue("FeedLanguage", value);
            rssFeed.FeedLanguage = value;
        }
    }


    /// <summary>
    /// Custom feed header XML which is generated before feed items. If the value is empty default header for RSS feed is generated.
    /// </summary>
    public string HeaderXML
    {
        get
        {
            return ValidationHelper.GetString(GetValue("HeaderXML"), null);
        }
        set
        {
            SetValue("HeaderXML", value);
            rssFeed.HeaderXML = value;
        }
    }


    /// <summary>
    /// Custom feed footer XML which is generated after feed items. If the value is empty default footer for RSS feed is generated.
    /// </summary>
    public string FooterXML
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FooterXML"), null);
        }
        set
        {
            SetValue("FooterXML", value);
            rssFeed.FooterXML = value;
        }
    }

    #endregion


    #region "Document filter properties"

    /// <summary>
    /// Indicates if the comments should be retrieved according to document filter settings.
    /// </summary>
    public bool UseDocumentFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("UseDocumentFilter"), false);
        }
        set
        {
            SetValue("UseDocumentFilter", value);
            srcProducts.UseDocumentFilter = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SiteName"), "");
        }
        set
        {
            SetValue("SiteName", value);
            srcProducts.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition for blog posts.
    /// </summary>
    public string DocumentsWhereCondition
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DocumentsWhereCondition"), "");
        }
        set
        {
            SetValue("DocumentsWhereCondition", value);
            srcProducts.DocumentsWhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether documents are combined with default culture version.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CombineWithDefaultCulture"), false);
        }
        set
        {
            SetValue("CombineWithDefaultCulture", value);
            srcProducts.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code of the documents.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("CultureCode"), CMSContext.PreferredCultureCode);
        }
        set
        {
            SetValue("CultureCode", value);
            srcProducts.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal relative level of the documents to be shown.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("MaxRelativeLevel"), -1);
        }
        set
        {
            SetValue("MaxRelativeLevel", value);
            srcProducts.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Gets or sets the path of the documents.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Path"), srcProducts.Path);
        }
        set
        {
            SetValue("Path", value);
            srcProducts.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether only published documents are selected.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectOnlyPublished"), false);
        }
        set
        {
            SetValue("SelectOnlyPublished", value);
            srcProducts.SelectOnlyPublished = value;
        }
    }

    #endregion


    #region "DataSource properties"

    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WhereCondition"), "");
        }
        set
        {
            this.SetValue("WhereCondition", value);
            srcProducts.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderBy"), "");
        }
        set
        {
            this.SetValue("OrderBy", value);
            srcProducts.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets top N selected documents.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), 0);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            srcProducts.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), "");
        }
        set
        {
            this.SetValue("FilterName", value);
            srcProducts.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets selected columns.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), "");
        }
        set
        {
            this.SetValue("Columns", value);
            srcProducts.SelectedColumns = value;
        }
    }

    #endregion


    #region "Cache properties"

    /// <summary>
    /// Gets or sets the cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return ValidationHelper.GetString(base.CacheItemName, rssFeed.CacheItemName);
        }
        set
        {
            base.CacheItemName = value;
            rssFeed.CacheItemName = value;
            srcProducts.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, rssFeed.CacheDependencies + "\n" + srcProducts.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            rssFeed.CacheDependencies = value;
            srcProducts.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            rssFeed.CacheMinutes = value;
            srcProducts.CacheMinutes = value;
        }
    }

    #endregion


    #region "Transformation properties"

    /// <summary>
    /// Gets or sets ItemTemplate property.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), string.Empty);
        }
        set
        {
            SetValue("TransformationName", value);
            rssFeed.TransformationName = value;
        }
    }

    #endregion


    #region "Stop processing"

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            rssFeed.StopProcessing = value;
        }
    }

    #endregion


    #region "Overidden methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }

    #endregion


    #region "Setup control"

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            rssFeed.StopProcessing = true;
            srcProducts.StopProcessing = true;
        }
        else
        {
            string feedCodeName = URLHelper.GetSafeUrlPart(FeedName, SiteName);
            // RSS feed properties
            rssFeed.FeedName = feedCodeName;
            rssFeed.FeedLink = URLHelper.GetAbsoluteUrl(URLHelper.AddParameterToUrl(URLHelper.CurrentURL, QueryStringKey, feedCodeName));
            rssFeed.LinkText = LinkText;
            rssFeed.LinkIcon = LinkIcon;
            rssFeed.FeedTitle = FeedTitle;
            rssFeed.FeedDescription = FeedDescription;
            rssFeed.FeedLanguage = FeedLanguage;
            rssFeed.EnableAutodiscovery = EnableRSSAutodiscovery;
            rssFeed.QueryStringKey = QueryStringKey;
            rssFeed.HeaderXML = HeaderXML;
            rssFeed.FooterXML = FooterXML;

            // Datasource properties
            srcProducts.WhereCondition = WhereCondition;
            srcProducts.OrderBy = OrderBy;
            srcProducts.TopN = SelectTopN;
            srcProducts.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            srcProducts.SourceFilterName = FilterName;
            srcProducts.SelectedColumns = Columns;

            // Prepare alias path
            string aliasPath = Path;
            if (String.IsNullOrEmpty(aliasPath))
            {
                aliasPath = "/%";
            }
            aliasPath = CMSContext.ResolveCurrentPath(aliasPath);

            // Prepare site name
            string siteName = SiteName;
            if (String.IsNullOrEmpty(siteName))
            {
                siteName = CMSContext.CurrentSiteName;
            }

            // Prepare culture code
            string cultureCode = CultureCode;
            if (String.IsNullOrEmpty(cultureCode))
            {
                cultureCode = CMSContext.PreferredCultureCode;
            }

            // Document filter properties
            this.srcProducts.SiteName = siteName;
            this.srcProducts.UseDocumentFilter = UseDocumentFilter;
            this.srcProducts.DocumentsWhereCondition = DocumentsWhereCondition;
            this.srcProducts.CombineWithDefaultCulture = CombineWithDefaultCulture;
            this.srcProducts.CultureCode = cultureCode;
            this.srcProducts.SelectOnlyPublished = SelectOnlyPublished;
            this.srcProducts.MaxRelativeLevel = MaxRelativeLevel;
            this.srcProducts.Path = aliasPath;

            // Cache properties
            rssFeed.CacheItemName = CacheItemName;
            rssFeed.CacheDependencies = CacheDependencies;
            rssFeed.CacheMinutes = CacheMinutes;
            srcProducts.CacheItemName = CacheItemName;
            srcProducts.CacheDependencies = CacheDependencies;
            srcProducts.CacheMinutes = CacheMinutes;

            // Transformation properties
            rssFeed.TransformationName = TransformationName;

            // Datasource properties
            rssFeed.DataSourceControl = srcProducts;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcProducts.ClearCache();
    }

    #endregion   
}
