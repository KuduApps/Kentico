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

public partial class CMSWebParts_CustomTables_CustomTableDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the custom table name.
    /// </summary>
    public string CustomTable
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CustomTable"), "");
        }
        set
        {
            this.SetValue("CustomTable", value);
            srcTables.CustomTable = value;
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
            srcTables.WhereCondition = value;
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
            srcTables.OrderBy = value;
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
            srcTables.TopN = value;
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
            srcTables.SourceFilterName = value;
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
            this.srcTables.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcTables.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcTables.CacheDependencies = value;
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
            this.srcTables.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gest or sets columns which will be displayed.
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
            srcTables.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Enables or disables option to retrieve selected item.
    /// </summary>
    public bool EnableSelectedItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableSelectedItem"), srcTables.EnableSelectedItem);
        }
        set
        {
            this.SetValue("EnableSelectedItem", value);
            srcTables.EnableSelectedItem = value;
        }
    }


    /// <summary>
    /// Gets or sets query string key name. Presence of the key in query string indicates, 
    /// that some item should be selected. The item is determined by query string value.        
    /// </summary>
    public string SelectedQueryStringKeyName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedQueryStringKeyName"), srcTables.SelectedQueryStringKeyName);
        }
        set
        {
            this.SetValue("SelectedQueryStringKeyName", value);
            srcTables.SelectedQueryStringKeyName = value;
        }
    }


    /// <summary>
    /// Gets or sets columns name by which the item is selected.
    /// </summary>
    public string SelectedDatabaseColumnName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedDatabaseColumnName"), srcTables.SelectedDatabaseColumnName);
        }
        set
        {
            this.SetValue("SelectedDatabaseColumnName", value);
            srcTables.SelectedDatabaseColumnName = value;
        }
    }


    /// <summary>
    /// Gets or sets validation type for query string value which determines selected item. 
    /// Options are int, guid and string.
    /// </summary>
    public string SelectedValidationType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedValidationType"), srcTables.SelectedValidationType);
        }
        set
        {
            this.SetValue("SelectedValidationType", value);
            srcTables.SelectedValidationType = value;
        }
    }

    #endregion


    #region "Stop processing"

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            srcTables.StopProcessing = value;
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
            this.srcTables.CustomTable = this.CustomTable;
            this.srcTables.WhereCondition = this.WhereCondition;
            this.srcTables.OrderBy = this.OrderBy;
            this.srcTables.TopN = this.SelectTopN;
            this.srcTables.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcTables.SourceFilterName = this.FilterName;
            this.srcTables.CacheItemName = this.CacheItemName;
            this.srcTables.CacheDependencies = this.CacheDependencies;
            this.srcTables.CacheMinutes = this.CacheMinutes;
            this.srcTables.SelectedColumns = this.Columns;
            this.srcTables.EnableSelectedItem = this.EnableSelectedItem;
            this.srcTables.SelectedQueryStringKeyName = this.SelectedQueryStringKeyName;
            this.srcTables.SelectedDatabaseColumnName = this.SelectedDatabaseColumnName;
            this.srcTables.SelectedValidationType = this.SelectedValidationType;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcTables.ClearCache();
    }

    #endregion
}
