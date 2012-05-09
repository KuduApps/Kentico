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

public partial class CMSWebParts_DataSources_QueryDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Load pages individually.
    /// </summary>
    public bool LoadPagesIndividually
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("LoadPagesIndividually"), false);
        }
        set
        {
            this.SetValue("LoadPagesIndividually", value);
        }
    }


    /// <summary>
    /// Gest or sets the cache item name.
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
            this.srcElem.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcElem.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcElem.CacheDependencies = value;
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
            this.srcElem.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the ORDER BY clause.
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
            this.srcElem.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the WHERE condition.
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
            this.srcElem.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the SELECT part of the query.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Columns"), null);
        }
        set
        {
            SetValue("Columns", value);
            srcElem.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Gets or sets the number which indicates how many documents should be displayed.
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
            this.srcElem.SelectTopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of a query to be used.
    /// </summary>
    public String QueryName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("QueryName"), "");
        }
        set
        {
            this.SetValue("QueryName", value);
            this.srcElem.QueryName = value;
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
            srcElem.SourceFilterName = value;
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
            // Setup query data source
            this.srcElem.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcElem.LoadPagesIndividually = this.LoadPagesIndividually;
            this.srcElem.CacheItemName = this.CacheItemName;
            this.srcElem.CacheDependencies = this.CacheDependencies;
            this.srcElem.CacheMinutes = this.CacheMinutes;
            this.srcElem.OrderBy = this.OrderBy;
            this.srcElem.WhereCondition = this.WhereCondition;
            this.srcElem.SelectedColumns = this.Columns;
            this.srcElem.SelectTopN = this.SelectTopN;
            this.srcElem.QueryName = this.QueryName;
            this.srcElem.SourceFilterName = this.FilterName;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcElem.ClearCache();
    }
}
