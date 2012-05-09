using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.SiteProvider;

public partial class CMSWebParts_Ecommerce_Products_SimilarProductsBySale : CMSAbstractWebPart
{
    #region "Document properties"  

    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SiteName"), this.lstElem.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.lstElem.SiteName = value;
        }
    }


    /// <summary>
    /// Nodes path.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("Path"), "/%");
        }
        set
        {
            this.SetValue("Path", value);
            this.lstElem.Path = value;
        }
    }


    /// <summary>
    /// Maximal relative level.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), this.lstElem.MaxRelativeLevel);
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            this.lstElem.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("Classnames"), this.lstElem.ClassNames);
        }
        set
        {
            this.SetValue("ClassNames", value);
            this.lstElem.ClassNames = value;
        }
    }


    /// <summary>
    /// Select top N items.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), this.lstElem.SelectTopN);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            this.lstElem.SelectTopN = value;
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WhereCondition"), this.lstElem.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.lstElem.WhereCondition = GetWhereCondition(value);
        }
    }


    /// <summary>
    /// Order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("OrderBy"), "SKUName");
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.lstElem.OrderBy = GetOrderByExpression(value);
        }
    }


    /// <summary>
    /// Select only published nodes.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), this.lstElem.SelectOnlyPublished);
        }
        set
        {
            this.SetValue("SelectOnlyPublished", value);
            this.lstElem.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("CultureCode"), this.lstElem.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.lstElem.CultureCode = value;
        }
    }


    /// <summary>
    /// Combine with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), this.lstElem.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.lstElem.CombineWithDefaultCulture = value;
        }
    }

    #endregion


    #region "Layout"

    /// <summary>
    /// Repeat columns.
    /// </summary>
    public int RepeatColumns
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RepeatColumns"), this.lstElem.RepeatColumns);
        }
        set
        {
            this.SetValue("RepeatColumns", value);
            this.lstElem.RepeatColumns = value;
        }
    }


    /// <summary>
    /// Repeat layout.
    /// </summary>
    public RepeatLayout RepeatLayout
    {
        get
        {
            return CMSDataList.GetRepeatLayout(DataHelper.GetNotEmpty(this.GetValue("RepeatLayout"), this.lstElem.RepeatLayout.ToString()));
        }
        set
        {
            this.SetValue("RepeatLayout", value.ToString());
            this.lstElem.RepeatLayout = value;
        }
    }


    /// <summary>
    /// Repeat Direction.
    /// </summary>
    public RepeatDirection RepeatDirection
    {
        get
        {
            return CMSDataList.GetRepeatDirection(DataHelper.GetNotEmpty(this.GetValue("RepeatDirection"), this.lstElem.RepeatDirection.ToString()));
        }
        set
        {
            this.SetValue("RepeatDirection", value.ToString());
            this.lstElem.RepeatDirection = value;
        }
    }

    #endregion


    #region "System settings"

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
            this.lstElem.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.lstElem.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.lstElem.CacheDependencies = value;
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
            this.lstElem.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions should be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.lstElem.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            this.lstElem.CheckPermissions = value;
        }
    }

    #endregion


    #region "Transformation properties"

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("TransformationName"), this.lstElem.TransformationName), this.lstElem.TransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
            this.lstElem.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the alternate results.
    /// </summary>
    public string AlternatingTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("AlternatingTransformationName"), this.lstElem.AlternatingTransformationName), this.lstElem.AlternatingTransformationName);
        }
        set
        {
            this.SetValue("AlternatingTransformationName", value);
            this.lstElem.AlternatingTransformationName = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Hide control for zero rows.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.lstElem.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.lstElem.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.lstElem.ZeroRowsText), this.lstElem.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.lstElem.ZeroRowsText = value;
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
        // In design mode is pocessing of control stoped
        if (this.StopProcessing)
        {
            this.lstElem.StopProcessing = true;
        }
        else
        {
            // Hide web part if current document is not product
            if (CMSContext.CurrentPageInfo.NodeSKUId <= 0)
            {
                this.Visible = false;
                this.lstElem.StopProcessing = true;
                return;
            }

            this.lstElem.ControlContext = this.ControlContext;

            // System settings
            this.lstElem.CacheItemName = this.CacheItemName;
            this.lstElem.CacheDependencies = this.CacheDependencies;
            this.lstElem.CacheMinutes = this.CacheMinutes;
            this.lstElem.CheckPermissions = this.CheckPermissions;

            // Document properties
            this.lstElem.SiteName = this.SiteName;
            this.lstElem.ClassNames = this.ClassNames;
            this.lstElem.Path = this.Path;
            this.lstElem.MaxRelativeLevel = this.MaxRelativeLevel;                        
            this.lstElem.SelectOnlyPublished = this.SelectOnlyPublished;
            this.lstElem.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.lstElem.CultureCode = this.CultureCode;
            
            this.lstElem.SelectTopN = this.SelectTopN;
            this.lstElem.WhereCondition = GetWhereCondition(this.WhereCondition);
            this.lstElem.OrderBy = GetOrderByExpression(this.OrderBy);           
 
            // Layout
            this.lstElem.RepeatColumns = this.RepeatColumns;
            this.lstElem.RepeatDirection = this.RepeatDirection;
            this.lstElem.RepeatLayout = this.RepeatLayout;

            // Transformations            
            this.lstElem.AlternatingTransformationName = this.AlternatingTransformationName;
            this.lstElem.TransformationName = this.TransformationName;

            //// Public
            this.lstElem.HideControlForZeroRows = this.HideControlForZeroRows;
            this.lstElem.ZeroRowsText = this.ZeroRowsText;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        this.SetupControl();
        this.lstElem.ReloadData(true);
    }


    /// <summary>
    /// Returns where condition.
    /// </summary>
    /// <param name="customWhere">Custome where condition</param>
    private string GetWhereCondition(string customWhere)
    {
        SiteInfo si = null;

        // Get required site data
        if (this.SiteName != "")
        {
            si = SiteInfoProvider.GetSiteInfo(this.SiteName);
        }
        else
        {
            si = CMSContext.CurrentSite;
        }

        if (si != null)
        {
            int siteID = si.SiteID;            
            int skuId = CMSContext.CurrentPageInfo.NodeSKUId;

            string where = "(NodeSKUID IN (SELECT OrderItemSKUID FROM COM_OrderItem JOIN COM_Order ON COM_OrderItem.OrderItemOrderID = COM_Order.OrderID JOIN COM_SKU ON COM_OrderItem.OrderItemSKUID = COM_SKU.SKUID WHERE (COM_SKU.SKUOptionCategoryID IS NULL) AND (COM_SKU.SKUEnabled = 1) AND (COM_Order.OrderSiteID = " + siteID + ") AND (COM_OrderItem.OrderItemSKUID <> " + skuId + ") AND (COM_Order.OrderID  IN (SELECT OrderItemOrderID FROM COM_OrderItem WHERE OrderItemSKUID= " + skuId + "))))";
           
            // Add custom WHERE condition
            if (customWhere != "")
            {
                where += " AND (" + customWhere + ")";
            }

            return where;  
        }
        return "";
    }


    /// <summary>
    /// Returns ORDER BY expression.
    /// </summary>
    /// <param name="customOrderBy">Custom "Order by" expression</param>
    private string GetOrderByExpression(string customOrderBy)
    {
        // Required "ORDER BY" expression
        string orderBy = "(SELECT Count(OrderItemSKUID) FROM COM_OrderItem WHERE COM_OrderItem.OrderItemSKUID = NodeSKUID GROUP BY OrderItemSKUID) DESC";

        // Add cutom "ORDER BY" expression
        if (customOrderBy != "")
        {
            orderBy += ", " + customOrderBy;
        }

        return orderBy;
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.lstElem.Visible;

        if (DataHelper.DataSourceIsEmpty(this.lstElem.DataSource) && (this.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.lstElem.ClearCache();
    }
}