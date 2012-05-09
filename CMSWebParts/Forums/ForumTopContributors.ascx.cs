using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.Caching;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.Forums;
using CMS.SiteProvider;

public partial class CMSWebParts_Forums_ForumTopContributors : CMSAbstractWebPart
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
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return CacheHelper.GetCacheDependencies(base.CacheDependencies, "cms.user|all");
        }
        set
        {
            base.CacheDependencies = CacheHelper.GetCacheDependencies(value, "cms.user|all");
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
                this.repTopContributors.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.TransformationName, false);
                this.repTopContributors.HideControlForZeroRows = this.HideControlForZeroRows;
                this.repTopContributors.ZeroRowsText = this.ZeroRowsText;

                DataSet ds = null;

                // Try to get data from cache
                using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref ds, this.CacheMinutes, true, this.CacheItemName, "forumtopcontributors", CMSContext.CurrentSiteName, this.WhereCondition, this.SelectTopN))
                {
                    if (cs.LoadData)
                    {
                        // Get the data
                        ds = GetData();

                        // Save to the cache
                        if (cs.Cached)
                        {
                            // Save to the cache
                            cs.CacheDependency = GetCacheDependency();
                            cs.Data = ds;
                        }
                    }
                }

                this.repTopContributors.DataSource = ds;

                if (!DataHelper.DataSourceIsEmpty(this.repTopContributors.DataSource))
                {
                    this.repTopContributors.DataBind();
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
        if (((this.repTopContributors.DataSource == null) || (DataHelper.DataSourceIsEmpty(this.repTopContributors.DataSource))) && (this.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Returns the Users data.
    /// </summary>
    private DataSet GetData()
    {
        string where = "(UserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID IN (SELECT SiteID FROM CMS_Site WHERE SiteName = '" + CMSContext.CurrentSiteName + "')))";

        if (!String.IsNullOrEmpty(this.WhereCondition))
        {
            where += " AND (" + this.WhereCondition + ")";
        }

        return UserInfoProvider.GetFullUsers(where, "UserForumPosts DESC", this.SelectTopN, null);
    }
    
    #endregion
}
