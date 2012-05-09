using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_Community_Groups_GroupsDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SiteName"), CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            srcGroups.SiteName = value;
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
            srcGroups.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only approved.
    /// </summary>
    public bool SelectOnlyApproved
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyApproved"), true);
        }
        set
        {
            this.SetValue("SelectOnlyApproved", value);
            srcGroups.SelectOnlyApproved = value;
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
            srcGroups.OrderBy = value;
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
            srcGroups.TopN = value;
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
            srcGroups.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gest or sest the cache item name.
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
            this.srcGroups.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcGroups.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcGroups.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("CacheMinutes"), 0);
        }
        set
        {
            this.SetValue("CacheMinutes", value);
            srcGroups.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gest or sets selected columns.
    /// </summary>
    public  string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), "");
        }
        set
        {
            this.SetValue("Columns", value);
            srcGroups.SelectedColumns = value;
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
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            srcGroups.SiteName = this.SiteName;
            srcGroups.WhereCondition = this.WhereCondition;
            srcGroups.OrderBy = this.OrderBy;
            srcGroups.TopN = this.SelectTopN;
            srcGroups.FilterName = this.ID;
            srcGroups.SourceFilterName = this.FilterName;
            srcGroups.CacheItemName = this.CacheItemName;
            srcGroups.CacheDependencies = this.CacheDependencies;
            srcGroups.CacheMinutes = this.CacheMinutes;
            srcGroups.SelectOnlyApproved = this.SelectOnlyApproved;
            srcGroups.SelectedColumns = this.Columns;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcGroups.ClearCache();
    }

    #endregion
}
