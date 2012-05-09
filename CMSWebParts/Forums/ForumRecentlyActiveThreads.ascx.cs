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
using CMS.Forums;
using CMS.SettingsProvider;

public partial class CMSWebParts_Forums_ForumRecentlyActiveThreads : CMSAbstractWebPart
{
    #region "Public properties"

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
            this.forumDataSource.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.forumDataSource.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.forumDataSource.CacheDependencies = value;
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
            this.forumDataSource.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets transformation name.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), "");
        }
        set
        {
            this.SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets forum groups names, use semicolon like separator.
    /// </summary>
    public string ForumGroups
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ForumGroups"), "");
        }
        set
        {
            this.SetValue("ForumGroups", value);
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
            forumDataSource.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), true);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.repLatestPosts.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets ZeroRowsText property.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), "");
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.repLatestPosts.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets TopN.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), -1);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            this.forumDataSource.TopN = value;
        }
    }


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
            forumDataSource.SiteName = value;
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
            forumDataSource.ShowGroupPosts = value;
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


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads the control data.
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
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (!String.IsNullOrEmpty(this.TransformationName))
            {
                this.repLatestPosts.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.TransformationName, false);
                this.repLatestPosts.HideControlForZeroRows = this.HideControlForZeroRows;
                this.repLatestPosts.ZeroRowsText = this.ZeroRowsText;

                // Set data source groupid according to group name 
                if (!String.IsNullOrEmpty(this.GroupName))
                {
                    if (this.GroupName == CMSConstants.COMMUNITY_CURRENT_GROUP)
                    {
                        this.forumDataSource.GroupID = ModuleCommands.CommunityGetCurrentGroupID();
                    }
                    else
                    {
                        GeneralizedInfo gi = ModuleCommands.CommunityGetGroupInfoByName(this.GroupName, this.SiteName);
                        if (gi != null)
                        {
                            this.forumDataSource.GroupID = ValidationHelper.GetInteger(gi.GetValue("GroupID"), 0);
                        }
                        else
                        {
                            forumDataSource.StopProcessing = true;
                        }
                    }
                }

                if (!forumDataSource.StopProcessing)
                {
                    this.forumDataSource.TopN = this.SelectTopN;
                    this.forumDataSource.OrderBy = "PostThreadLastPostTime DESC";
                    this.forumDataSource.CacheItemName = this.CacheItemName;
                    this.forumDataSource.CacheDependencies = this.CacheDependencies;
                    this.forumDataSource.CacheMinutes = this.CacheMinutes;
                    this.forumDataSource.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
                    this.forumDataSource.SelectOnlyApproved = false;
                    this.forumDataSource.SiteName = this.SiteName;
                    this.forumDataSource.ShowGroupPosts = this.ShowGroupPosts && String.IsNullOrEmpty(ForumGroups);

                    #region "Complete where condition"

                    string where = "";

                    // Get groups part of where condition
                    string[] groups = this.ForumGroups.Split(';');
                    foreach (string group in groups)
                    {
                        if (group != "")
                        {
                            if (where != "")
                            {
                                where += " OR ";
                            }
                            where += "(GroupName = N'" + SqlHelperClass.GetSafeQueryString(group, false) + "')";
                        }
                    }
                    where = "(" + (where == "" ? "(GroupName NOT LIKE 'AdHoc%')" : "(" + where + ")") + " AND (PostLevel = 0))";

                    // Append where condition and set PostLevel to 0 (only threads are needed)
                    // and filter out AdHoc forums
                    if (!String.IsNullOrEmpty(this.WhereCondition))
                    {
                        where += " AND (" + this.WhereCondition + ")";
                    }

                    #endregion

                    this.forumDataSource.WhereCondition = where;
                    this.forumDataSource.CheckPermissions = true;
                    this.repLatestPosts.DataSource = this.forumDataSource.DataSource;

                    if (!DataHelper.DataSourceIsEmpty(this.repLatestPosts.DataSource))
                    {
                        this.repLatestPosts.DataBind();
                    }
                }
            }
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hide control for zero rows
        if (((this.repLatestPosts.DataSource == null) || (DataHelper.DataSourceIsEmpty(this.repLatestPosts.DataSource))) && (this.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.forumDataSource.ClearCache();
    }
}
