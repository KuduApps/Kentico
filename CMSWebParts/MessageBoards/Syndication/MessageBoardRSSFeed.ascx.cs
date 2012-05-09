using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_MessageBoards_Syndication_MessageBoardRSSFeed : CMSAbstractWebPart
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
            rssFeed.EnableAutodiscovery = value;
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


    #region "Datasource properties"

    /// <summary>
    /// Gets or sets the message board name.
    /// </summary>
    public string BoardName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BoardName"), srcMessages.BoardName);
        }
        set
        {
            this.SetValue("BoardName", value);
            srcMessages.BoardName = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SiteName"), srcMessages.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            srcMessages.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only approved property.
    /// </summary>
    public bool SelectOnlyApproved
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyApproved"), srcMessages.SelectOnlyApproved);
        }
        set
        {
            this.SetValue("SelectOnlyApproved", value);
            srcMessages.SelectOnlyApproved = value;
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WhereCondition"), srcMessages.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            srcMessages.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderBy"), srcMessages.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            srcMessages.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets top N selected documents.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), srcMessages.TopN);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            srcMessages.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), srcMessages.SourceFilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            srcMessages.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets selected columns.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), srcMessages.SelectedColumns);
        }
        set
        {
            this.SetValue("Columns", value);
            srcMessages.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Indicates if group messages should be included.
    /// </summary>
    public bool ShowGroupMessages
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowGroupMessages"), false);
        }
        set
        {
            this.SetValue("ShowGroupMessages", value);
            srcMessages.ShowGroupMessages = value;
        }
    }

    #endregion


    #region "Document properties"

    /// <summary>
    /// Indicates if the comments should be retrieved according to document filter settings.
    /// </summary>
    public bool UseDocumentFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseDocumentFilter"), srcMessages.UseDocumentFilter);
        }
        set
        {
            this.SetValue("UseDocumentFilter", value);
            srcMessages.UseDocumentFilter = value;
        }
    }


    /// <summary>
    /// Gets or sets the alias path of the board document.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Path"), srcMessages.Path);
        }
        set
        {
            this.SetValue("Path", value);
            srcMessages.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code of the board document.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CultureCode"), srcMessages.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            srcMessages.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition for board documents.
    /// </summary>
    public string DocumentsWhereCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DocumentsWhereCondition"), srcMessages.DocumentsWhereCondition);
        }
        set
        {
            this.SetValue("DocumentsWhereCondition", value);
            srcMessages.DocumentsWhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets combine with default culture for board document.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), srcMessages.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            srcMessages.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets select only published for documents.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), srcMessages.SelectOnlyPublished);
        }
        set
        {
            this.SetValue("SelectOnlyPublished", value);
            srcMessages.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or sets max relative level for board documents.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), srcMessages.MaxRelativeLevel);
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            srcMessages.MaxRelativeLevel = value;
        }
    }

    #endregion


    #region "Cache properties"

    /// <summary>
    /// Gest or sets the cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            this.srcMessages.CacheItemName = value;
            this.rssFeed.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.rssFeed.CacheDependencies + "\n" + this.srcMessages.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcMessages.CacheDependencies = value;
            this.rssFeed.CacheDependencies = value;
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
            this.srcMessages.CacheMinutes = value;
            this.rssFeed.CacheMinutes = value;
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
            srcMessages.StopProcessing = value;
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
            srcMessages.StopProcessing = true;
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

            // Messages datasource properties
            this.srcMessages.BoardName = BoardName;
            this.srcMessages.SiteName = siteName;
            this.srcMessages.WhereCondition = WhereCondition;
            this.srcMessages.OrderBy = OrderBy;
            this.srcMessages.TopN = SelectTopN;
            this.srcMessages.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcMessages.SourceFilterName = FilterName;
            this.srcMessages.SelectOnlyApproved = SelectOnlyApproved;
            this.srcMessages.SelectedColumns = Columns;
            this.srcMessages.ShowGroupMessages = ShowGroupMessages;

            // Documents properties
            this.srcMessages.Path = aliasPath;
            this.srcMessages.UseDocumentFilter = UseDocumentFilter;
            this.srcMessages.CultureCode = cultureCode;
            this.srcMessages.DocumentsWhereCondition = DocumentsWhereCondition;
            this.srcMessages.CombineWithDefaultCulture = CombineWithDefaultCulture;
            this.srcMessages.SelectOnlyPublished = SelectOnlyPublished;
            this.srcMessages.MaxRelativeLevel = MaxRelativeLevel;

            // Cache properties
            rssFeed.CacheItemName = CacheItemName;
            rssFeed.CacheDependencies = CacheDependencies;
            rssFeed.CacheMinutes = CacheMinutes;
            srcMessages.CacheItemName = CacheItemName;
            srcMessages.CacheDependencies = CacheDependencies;
            srcMessages.CacheMinutes = CacheMinutes;

            // Transformation properties
            rssFeed.TransformationName = TransformationName;

            // Set datasource
            rssFeed.DataSourceControl = srcMessages;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcMessages.ClearCache();
    }

    #endregion
}
