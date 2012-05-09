using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Community;

public partial class CMSWebParts_Community_Members_GroupMembersDataSource : CMSAbstractWebPart
{
    #region "Properties"

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
            srcMembers.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only approved property.
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
            srcMembers.SelectOnlyApproved = value;
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
            srcMembers.OrderBy = value;
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
            srcMembers.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets group name to specify group members.
    /// </summary>
    public string GroupName
    {
        get
        {
            string groupName = ValidationHelper.GetString(this.GetValue("GroupName"), "");
            if ((string.IsNullOrEmpty(groupName) || groupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                return CommunityContext.CurrentGroup.GroupName;
            }
            return groupName;
        }
        set
        {
            this.SetValue("GroupName", value);
            srcMembers.GroupName = value;
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
            srcMembers.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            srcMembers.SiteName = value;
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
            this.srcMembers.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcMembers.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcMembers.CacheDependencies = value;
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
            this.srcMembers.CacheMinutes = value;
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
            srcMembers.SelectedColumns = value;
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
            srcMembers.WhereCondition = this.WhereCondition;
            srcMembers.OrderBy = this.OrderBy;
            srcMembers.TopN = this.SelectTopN;
            srcMembers.GroupName = this.GroupName;
            srcMembers.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            srcMembers.SourceFilterName = this.FilterName;
            srcMembers.SiteName = this.SiteName;
            srcMembers.CacheItemName = this.CacheItemName;
            srcMembers.CacheDependencies = this.CacheDependencies;
            srcMembers.CacheMinutes = this.CacheMinutes;
            srcMembers.SelectOnlyApproved = this.SelectOnlyApproved;
            srcMembers.SelectedColumns = this.Columns;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcMembers.ClearCache();
    }

    #endregion
}
