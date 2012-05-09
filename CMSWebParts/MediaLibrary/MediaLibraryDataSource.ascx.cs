using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSWebParts_MediaLibrary_MediaLibraryDataSource : CMSAbstractWebPart
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
            srcMediaLib.WhereCondition = value;
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
            srcMediaLib.OrderBy = value;
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
            srcMediaLib.TopN = value;
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
            srcMediaLib.SourceFilterName = value;
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
            this.srcMediaLib.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcMediaLib.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcMediaLib.CacheDependencies = value;
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
            this.srcMediaLib.CacheMinutes = value;
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
            srcMediaLib.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Gest or sets site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), String.Empty);
        }
        set
        {
            this.SetValue("SiteName", value);
            srcMediaLib.SiteName = value;
        }
    }


    /// <summary>
    /// Indicates if group libraries should be included.
    /// </summary>
    public bool ShowGroupLibraries
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowGroupLibraries"), false);
        }
        set
        {
            this.SetValue("ShowGroupLibraries", value);
            srcMediaLib.ShowGroupLibraries = value;
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
            srcMediaLib.WhereCondition = this.WhereCondition;
            srcMediaLib.OrderBy = this.OrderBy;
            srcMediaLib.TopN = this.SelectTopN;
            srcMediaLib.FilterName = this.ID;
            srcMediaLib.SourceFilterName = this.FilterName;
            srcMediaLib.CacheItemName = this.CacheItemName;
            srcMediaLib.CacheDependencies = this.CacheDependencies;
            srcMediaLib.CacheMinutes = this.CacheMinutes;
            srcMediaLib.SelectedColumns = this.Columns;
            srcMediaLib.ShowGroupLibraries = this.ShowGroupLibraries;
            srcMediaLib.SiteName = this.SiteName;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcMediaLib.ClearCache();
    }

    #endregion
}
