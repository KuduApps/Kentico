using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Forums;
using CMS.SettingsProvider;

public partial class CMSWebParts_Forums_ForumPostsDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the web service URL.
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
            srcPosts.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets forum name for which blog posts should be obtained.
    /// </summary>
    public string ForumName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ForumName"), "");
        }
        set
        {
            this.SetValue("ForumName", value);
            srcPosts.ForumName = value;
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
            srcPosts.SelectOnlyApproved = value;
        }
    }


    /// <summary>
    /// Gets or sets Check permissions property.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), true);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            srcPosts.CheckPermissions = value;
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
            srcPosts.WhereCondition = value;
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
            srcPosts.OrderBy = value;
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
            srcPosts.TopN = value;
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
            srcPosts.SourceFilterName = value;
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
            this.srcPosts.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcPosts.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcPosts.CacheDependencies = value;
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
            this.srcPosts.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gest or sets selected columns.
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
            srcPosts.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Indicates if group posts should be included.
    /// </summary>
    public bool ShowGroupPosts
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowGroupPosts"), false);
        }
        set
        {
            this.SetValue("ShowGroupPosts", value);
            srcPosts.ShowGroupPosts = value;
        }
    }


    /// <summary>
    /// Gets or sets community group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupName"), "");
        }
        set
        {
            this.SetValue("GroupName", value);
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
            this.srcPosts.SiteName = this.SiteName;
            this.srcPosts.ForumName = this.ForumName;
            this.srcPosts.WhereCondition = this.WhereCondition;
            this.srcPosts.OrderBy = this.OrderBy;
            this.srcPosts.TopN = this.SelectTopN;
            this.srcPosts.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcPosts.SourceFilterName = this.FilterName;
            this.srcPosts.CacheItemName = this.CacheItemName;
            this.srcPosts.CacheDependencies = this.CacheDependencies;
            this.srcPosts.CacheMinutes = this.CacheMinutes;
            this.srcPosts.SelectOnlyApproved = this.SelectOnlyApproved;
            this.srcPosts.CheckPermissions = this.CheckPermissions;
            this.srcPosts.SelectedColumns = this.Columns;
            this.srcPosts.ShowGroupPosts = this.ShowGroupPosts && String.IsNullOrEmpty(ForumName);

            // Set data source groupid according to group name
            if (!String.IsNullOrEmpty(this.GroupName))
            {
                if (this.GroupName == CMSConstants.COMMUNITY_CURRENT_GROUP)
                {
                    this.srcPosts.GroupID = ModuleCommands.CommunityGetCurrentGroupID();
                }
                else
                {
                    GeneralizedInfo gi = ModuleCommands.CommunityGetGroupInfoByName(this.GroupName, this.SiteName);
                    if (gi != null)
                    {
                        this.srcPosts.GroupID = ValidationHelper.GetInteger(gi.GetValue("GroupID"), 0);
                    }
                    else
                    {
                        this.srcPosts.StopProcessing = true;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcPosts.ClearCache();
    }

    #endregion
}
