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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSWebParts_Ecommerce_Products_RandomProducts : CMSAbstractWebPart
{
    #region "Document properties"
    
    /// <summary>
    /// Gets or sets the columns to get.
    /// </summary>
    public string Columns
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("Columns"), repeater.Columns);
        }
        set
        {
            SetValue("Columns", value);
            repeater.Columns = value;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SiteName"), repeater.SiteName);
        }
        set
        {
            SetValue("SiteName", value);
            repeater.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets path where random products will be find.
    /// </summary>
    public string Path
    {
        get
        {
            string path = ValidationHelper.GetString(GetValue("Path"), "");
            if (path == "")
            {
                return "/%";
            }
            else
            {
                return CMSContext.CurrentResolver.ResolvePath(path);
            }
        }

        set
        {
            SetValue("Path", value);
            repeater.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal relative level of the documents to be shown.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("MaxRelativeLevel"), repeater.MaxRelativeLevel);
        }
        set
        {
            SetValue("MaxRelativeLevel", value);
            repeater.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("Classnames"), repeater.ClassNames);
        }
        set
        {
            SetValue("ClassNames", value);
            repeater.ClassNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("WhereCondition"), repeater.WhereCondition);
        }
        set
        {
            SetValue("WhereCondition", value);
            repeater.WhereCondition = GetCompleteWhereCondition();
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY expression.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("OrderBy"), "SKUName");
        }
        set
        {
            SetValue("OrderBy", value);
            SetOrderByExpression(OnlyNRandomProducts, value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether only published documents are selected.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectOnlyPublished"), repeater.SelectOnlyPublished);
        }
        set
        {
            SetValue("SelectOnlyPublished", value);
            repeater.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or set culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("CultureCode"), repeater.CultureCode);
        }
        set
        {
            SetValue("CultureCode", value);
            repeater.CultureCode = value;
        }
    }


    /// <summary>
    /// Combine with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CombineWithDefaultCulture"), repeater.CombineWithDefaultCulture);
        }
        set
        {
            SetValue("CombineWithDefaultCulture", value);
            repeater.CombineWithDefaultCulture = value;
        }
    }

    #endregion


    #region "Product properties"

    /// <summary>
    /// Gets or sets the code name of the public status. Products with this public status will be used.
    /// </summary>
    public string ProductPublicStatusName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ProductPublicStatusName"), "");
        }
        set
        {
            SetValue("ProductPublicStatusName", value);
            repeater.WhereCondition = GetCompleteWhereCondition();
        }
    }


    /// <summary>
    /// Gets or sets the code name of the internal status. Products with this internal status will be used.
    /// </summary>
    public string ProductInternalStatusName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ProductInternalStatusName"), "");
        }
        set
        {
            SetValue("ProductInternalStatusName", value);
            repeater.WhereCondition = GetCompleteWhereCondition();
        }
    }


    /// <summary>
    /// Gets or sets the number of random products that should be selected from the dataset.
    /// </summary>
    public int OnlyNRandomProducts
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("OnlyNRandomProducts"), 0);
        }

        set
        {
            SetValue("OnlyNRandomProducts", value);
            SetOrderByExpression(value, OrderBy);
        }
    }


    /// <summary>
    /// Gets or sets department where random products will be find.
    /// </summary>
    public string ProductDepartmentName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ProductDepartmentName"), "");
        }

        set
        {
            SetValue("ProductDepartmentName", value);
            repeater.WhereCondition = GetCompleteWhereCondition();
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
            repeater.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, repeater.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            repeater.CacheDependencies = value;
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
            repeater.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions should be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CheckPermissions"), repeater.CheckPermissions);
        }
        set
        {
            SetValue("CheckPermissions", value);
            repeater.CheckPermissions = value;
        }
    }

    #endregion


    #region "Transformation properties"
    
    /// <summary>
    /// Gets or sets the separator (text, html code) which is displayed between displayed items.
    /// </summary>
    public string ItemSeparator
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("ItemSeparator"), repeater.ItemSeparator);
        }
        set
        {
            SetValue("ItemSeparator", value);
            repeater.ItemSeparator = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("TransformationName"), repeater.TransformationName), repeater.TransformationName);
        }
        set
        {
            SetValue("TransformationName", value);
            repeater.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the alternate results.
    /// </summary>
    public string AlternatingTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("AlternatingTransformationName"), repeater.AlternatingTransformationName), repeater.AlternatingTransformationName);
        }
        set
        {
            SetValue("AlternatingTransformationName", value);
            repeater.AlternatingTransformationName = value;
        }
    }

    #endregion


    #region "Public properties"
    
    /// <summary>
    /// Filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FilterName"), repeater.FilterName);
        }
        set
        {
            SetValue("FilterName", value);
            repeater.FilterName = value;
        }
    }


    /// <summary>
    /// Hide control for zero rows.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideControlForZeroRows"), repeater.HideControlForZeroRows);
        }
        set
        {
            SetValue("HideControlForZeroRows", value);
            repeater.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("ZeroRowsText"), repeater.ZeroRowsText);
        }
        set
        {
            SetValue("ZeroRowsText", value);
            repeater.ZeroRowsText = value;
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
        if (StopProcessing)
        {
            repeater.StopProcessing = true;
        }
        else
        {
            repeater.ControlContext = ControlContext;

            // Filter documents by WHERE condition
            repeater.WhereCondition = GetCompleteWhereCondition();

            // Order documents by ORDER By expression
            SetOrderByExpression(OnlyNRandomProducts, OrderBy);

            // Transformations
            repeater.TransformationName = TransformationName;
            repeater.ItemSeparator = ItemSeparator;

            repeater.FilterName = FilterName;
            repeater.Columns = Columns;

            // Document properties
            repeater.CacheItemName = CacheItemName;
            repeater.CacheDependencies = CacheDependencies;
            repeater.CacheMinutes = CacheMinutes;
            repeater.CheckPermissions = CheckPermissions;
            repeater.SiteName = SiteName;
            repeater.Path = Path;
            repeater.ClassNames = ClassNames;
            repeater.CultureCode = CultureCode;
            repeater.CombineWithDefaultCulture = CombineWithDefaultCulture;
            repeater.SelectOnlyPublished = SelectOnlyPublished;
            repeater.MaxRelativeLevel = MaxRelativeLevel;

            // No data behavior
            repeater.ZeroRowsText = ZeroRowsText;
            repeater.HideControlForZeroRows = HideControlForZeroRows;
        }
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        repeater.ReloadData(true);
    }


    /// <summary>
    /// Returns complete WHERE condition.
    /// </summary>
    protected string GetCompleteWhereCondition()
    {
        string customWhere = ValidationHelper.GetString(GetValue("WhereCondition"), "");
        string where = "";

        // Include user custom WHERE condition        
        if (customWhere.Trim() != "")
        {
            where = "(" + customWhere + ") AND";
        }

        // Do not select product options and select only enabled products
        where += " (SKUOptionCategoryID IS NULL) AND (SKUEnabled = 1)";

        // Get products only with specified public status
        if (ProductPublicStatusName != "")
        {
            int pStatusSiteID = ECommerceSettings.UseGlobalPublicStatus(SiteName) ? 0 : SiteInfoProvider.GetSiteID(SiteName);
            where += " AND (SKUPublicStatusID = (SELECT TOP 1 PublicStatusID FROM COM_PublicStatus WHERE ISNULL(PublicStatusSiteID, 0) = " + pStatusSiteID + " AND PublicStatusName = '" + SqlHelperClass.GetSafeQueryString(ProductPublicStatusName, false) + "'))";
        }

        // Get products only with specified internal status
        if (ProductInternalStatusName != "")
        {
            int iStatusSiteID = ECommerceSettings.UseGlobalInternalStatus(SiteName) ? 0 : SiteInfoProvider.GetSiteID(SiteName);
            where += " AND (SKUInternalStatusID = (SELECT TOP 1 InternalStatusID FROM COM_InternalStatus WHERE ISNULL(InternalStatusSiteID, 0) = " + iStatusSiteID + " AND InternalStatusName = '" + SqlHelperClass.GetSafeQueryString(ProductInternalStatusName, false) + "'))";
        }

        // Get products only from specified department
        if (ProductDepartmentName != "")
        {
            DepartmentInfo dept = DepartmentInfoProvider.GetDepartmentInfo(ProductDepartmentName, SiteName);
            
            int departmentID = (dept != null) ? dept.DepartmentID : 0;
            where += " AND (SKUDepartmentID = " + departmentID + ")";
        }

        return where;
    }


    /// <summary>
    /// Sets ORDER BY expression.
    /// </summary>    
    /// <param name="selectNRandomProducts">Number of random products that should be selected</param>
    /// <param name="customOrderBy">Custom "order by" expression</param>
    private void SetOrderByExpression(int selectNRandomProducts, string customOrderBy)
    {
        // Select only N random documents
        if (selectNRandomProducts > 0)
        {
            // Select random documents
            repeater.SelectTopN = selectNRandomProducts;
            repeater.OrderBy = "newid()";

            // Order data by custom 'Order by' expression
            repeater.DataBinding += repeater_DataBinding;
        }
        else
        {
            repeater.OrderBy = customOrderBy;
        }
    }


    void repeater_DataBinding(object sender, EventArgs e)
    {
        if ((OrderBy != "") && !DataHelper.DataSourceIsEmpty(repeater.DataSource))
        {
            DataHelper.GetDataView(repeater.DataSource).Sort = OrderBy;
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Visible = repeater.Visible;

        if (DataHelper.DataSourceIsEmpty(repeater.DataSource) && (HideControlForZeroRows))
        {
            Visible = false;
        }
    }


    /// <summary>
    /// Clear cache.
    /// </summary>
    public override void ClearCache()
    {
        repeater.ClearCache();
    }
}
