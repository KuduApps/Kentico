using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Community;

public partial class CMSWebParts_Community_MediaLibrary_GroupMediaLibraryDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WhereCondition"), String.Empty);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            srcMedia.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderBy"), String.Empty);
        }
        set
        {
            this.SetValue("OrderBy", value);
            srcMedia.OrderBy = value;
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
            srcMedia.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), String.Empty);
        }
        set
        {
            this.SetValue("FilterName", value);
            srcMedia.SourceFilterName = value;
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
            this.srcMedia.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcMedia.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcMedia.CacheDependencies = value;
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
            this.srcMedia.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gest or sets selected columns.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), String.Empty);
        }
        set
        {
            this.SetValue("Columns", value);
            srcMedia.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Gets or sets the group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            string groupName = ValidationHelper.GetString(GetValue("GroupName"), "");
            if ((string.IsNullOrEmpty(groupName) || groupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                return CommunityContext.CurrentGroup.GroupName;
            }
            if (groupName == GroupInfoProvider.CURRENT_GROUP)
            {
                return QueryHelper.GetString("groupname", "");
            }
            return groupName;
        }
        set
        {
            SetValue("GroupName", value);
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
            srcMedia.WhereCondition = this.WhereCondition;
            srcMedia.OrderBy = this.OrderBy;
            srcMedia.TopN = this.SelectTopN;
            srcMedia.FilterName = this.ID;
            srcMedia.SourceFilterName = this.FilterName;
            srcMedia.CacheItemName = this.CacheItemName;
            srcMedia.CacheDependencies = this.CacheDependencies;
            srcMedia.CacheMinutes = this.CacheMinutes;
            srcMedia.SelectedColumns = this.Columns;

            // Set group ID
            srcMedia.GroupID = -1;
            if (!string.IsNullOrEmpty(this.GroupName))
            {
                GroupInfo gi = GroupInfoProvider.GetGroupInfo(this.GroupName, CMSContext.CurrentSiteName);
                if (gi != null)
                {
                    srcMedia.GroupID = gi.GroupID;
                }
            }
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcMedia.ClearCache();
    }

    #endregion
}
