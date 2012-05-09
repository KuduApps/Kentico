using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSWebParts_DataSources_UsersDataSource : CMSAbstractWebPart
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
            srcUsers.WhereCondition = value;
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
            srcUsers.SelectOnlyApproved = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only hidden property.
    /// </summary>
    public bool SelectHidden
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectHidden"), false);
        }
        set
        {
            this.SetValue("SelectHidden", value);
            srcUsers.SelectHidden = value;
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
            srcUsers.OrderBy = value;
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
            srcUsers.TopN = value;
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
            srcUsers.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), "");
        }
        set
        {
            this.SetValue("SiteName", value);
            srcUsers.SiteName = value;
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
            this.srcUsers.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcUsers.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcUsers.CacheDependencies = value;
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
            this.srcUsers.CacheMinutes = value;
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
            srcUsers.SelectedColumns = value;
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
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.srcUsers.WhereCondition = this.WhereCondition;
            this.srcUsers.OrderBy = this.OrderBy;
            this.srcUsers.TopN = this.SelectTopN;
            this.srcUsers.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcUsers.SourceFilterName = this.FilterName;
            this.srcUsers.SiteName = this.SiteName;
            this.srcUsers.CacheItemName = this.CacheItemName;
            this.srcUsers.CacheDependencies = this.CacheDependencies;
            this.srcUsers.CacheMinutes = this.CacheMinutes;
            this.srcUsers.SelectOnlyApproved = this.SelectOnlyApproved;
            this.srcUsers.SelectHidden = this.SelectHidden;
            this.srcUsers.SelectedColumns = this.Columns;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcUsers.ClearCache();
    }

    #endregion
}
