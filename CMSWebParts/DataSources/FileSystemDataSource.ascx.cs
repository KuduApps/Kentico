using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Controls;

public partial class CMSWebParts_DataSources_FileSystemDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the custom table name.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Path"), "");
        }
        set
        {
            this.SetValue("XmlUrl", value);
            srcFiles.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the include sub dirs property.
    /// </summary>
    public bool IncludeSubDirs
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("IncludeSubDirs"), false);
        }
        set
        {
            this.SetValue("IncludeSubDirs", value);
            srcFiles.IncludeSubDirs = value;
        }
    }


    /// <summary>
    /// Gets or sets the custom table name.
    /// </summary>
    public string FilesFilter
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilesFilter"), "");
        }
        set
        {
            this.SetValue("FilesFilter", value);
            srcFiles.FilesFilter = value;
        }
    }


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
            srcFiles.WhereCondition = value;
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
            srcFiles.OrderBy = value;
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
            srcFiles.TopN = value;
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
            srcFiles.SourceFilterName = value;
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
            this.srcFiles.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcFiles.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcFiles.CacheDependencies = value;
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
            this.srcFiles.CacheMinutes = value;
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
            this.srcFiles.Path = this.Path;
            this.srcFiles.IncludeSubDirs = this.IncludeSubDirs;
            this.srcFiles.FilesFilter = this.FilesFilter;
            this.srcFiles.WhereCondition = this.WhereCondition;
            this.srcFiles.OrderBy = this.OrderBy;
            this.srcFiles.TopN = this.SelectTopN;
            this.srcFiles.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcFiles.SourceFilterName = this.FilterName;
            this.srcFiles.CacheItemName = this.CacheItemName;
            this.srcFiles.CacheDependencies = this.CacheDependencies;
            this.srcFiles.CacheMinutes = this.CacheMinutes;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcFiles.ClearCache();
    }

    #endregion
}
