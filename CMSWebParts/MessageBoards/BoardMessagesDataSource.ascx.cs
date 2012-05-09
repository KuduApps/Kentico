using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;

public partial class CMSWebParts_MessageBoards_BoardMessagesDataSource : CMSAbstractWebPart
{
    #region "Properties"

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
    /// Gets or sets the cache item name.
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
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcMessages.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcMessages.CacheDependencies = value;
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
    /// Initializes control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
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

            this.srcMessages.BoardName = this.BoardName;
            this.srcMessages.SiteName = siteName;
            this.srcMessages.WhereCondition = this.WhereCondition;
            this.srcMessages.OrderBy = this.OrderBy;
            this.srcMessages.TopN = this.SelectTopN;
            this.srcMessages.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcMessages.SourceFilterName = this.FilterName;
            this.srcMessages.CacheItemName = this.CacheItemName;
            this.srcMessages.CacheDependencies = this.CacheDependencies;
            this.srcMessages.CacheMinutes = this.CacheMinutes;
            this.srcMessages.SelectOnlyApproved = this.SelectOnlyApproved;
            this.srcMessages.SelectedColumns = this.Columns;
            this.srcMessages.ShowGroupMessages = this.ShowGroupMessages;

            // Documents properties
            this.srcMessages.Path = aliasPath;
            this.srcMessages.UseDocumentFilter = this.UseDocumentFilter;
            this.srcMessages.CultureCode = cultureCode;
            this.srcMessages.DocumentsWhereCondition = this.DocumentsWhereCondition;
            this.srcMessages.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.srcMessages.SelectOnlyPublished = this.SelectOnlyPublished;
            this.srcMessages.MaxRelativeLevel = this.MaxRelativeLevel;
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

