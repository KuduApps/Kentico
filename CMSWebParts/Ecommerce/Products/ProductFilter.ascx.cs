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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalControls;
using CMS.Controls;

public partial class CMSWebParts_Ecommerce_Products_ProductFilter : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Show public status filter.
    /// </summary>
    public bool ShowPublicStatusFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowPublicStatusFilter"), this.filterElem.ShowPublicStatusFilter);
        }
        set
        {
            this.SetValue("ShowPublicStatusFilter", value);
            this.filterElem.ShowPublicStatusFilter = value;
        }
    }


    /// <summary>
    /// Show manufacturer filter.
    /// </summary>
    public bool ShowManufacturerFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowManufacturerFilter"), this.filterElem.ShowManufacturerFilter);
        }
        set
        {
            this.SetValue("ShowManufacturerFilter", value);
            this.filterElem.ShowManufacturerFilter = value;
        }
    }


    /// <summary>
    /// Show paging filter.
    /// </summary>
    public bool ShowPagingFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowPagingFilter"), this.filterElem.ShowPagingFilter);
        }
        set
        {
            this.SetValue("ShowPagingFilter", value);
            this.filterElem.ShowPagingFilter = value;
        }
    }


    /// <summary>
    /// Show stock filter.
    /// </summary>
    public bool ShowStockFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowStockFilter"), this.filterElem.ShowStockFilter);
        }
        set
        {
            this.SetValue("ShowStockFilter", value);
            this.filterElem.ShowStockFilter = value;
        }
    }


    /// <summary>
    /// Show sorting filter.
    /// </summary>
    public bool ShowSortingFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSortingFilter"), this.filterElem.ShowSortingFilter);
        }
        set
        {
            this.SetValue("ShowSortingFilter", value);
            this.filterElem.ShowSortingFilter = value;
        }
    }


    /// <summary>
    /// Show search filter.
    /// </summary>
    public bool ShowSearchFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSearchFilter"), this.filterElem.ShowSearchFilter);
        }
        set
        {
            this.SetValue("ShowSearchFilter", value);
            this.filterElem.ShowSearchFilter = value;
        }
    }


    /// <summary>
    /// Paging filter options (values separated by comma).
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), this.filterElem.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.filterElem.SiteName = value;
        }
    }


    /// <summary>
    /// Paging filter options (values separated by comma).
    /// </summary>
    public string PagingOptions
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PagingOptions"), this.filterElem.PagingOptions);
        }
        set
        {
            this.SetValue("PagingOptions", value);
            this.filterElem.PagingOptions = value;
        }
    }


    /// <summary>
    /// Filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), this.filterElem.FilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.filterElem.FilterName = value;
        }
    }


    /// <summary>
    /// Filter by query parameters.
    /// </summary>
    public bool FilterByQuery
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("FilterByQuery"), this.filterElem.FilterByQuery);
        }
        set
        {
            this.SetValue("FilterByQuery", value);
            this.filterElem.FilterByQuery = value;
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
            this.filterElem.StopProcessing = true;
        }
        else
        {
            this.filterElem.ShowManufacturerFilter = this.ShowManufacturerFilter;
            this.filterElem.ShowPagingFilter = this.ShowPagingFilter;
            this.filterElem.ShowPublicStatusFilter = this.ShowPublicStatusFilter;
            this.filterElem.ShowSortingFilter = this.ShowSortingFilter;
            this.filterElem.ShowStockFilter = this.ShowStockFilter;
            this.filterElem.ShowSearchFilter = this.ShowSearchFilter;
            this.filterElem.PagingOptions = this.PagingOptions;
            this.filterElem.SiteName = this.SiteName;
            this.filterElem.FilterName = this.FilterName;
            this.filterElem.FilterByQuery = this.FilterByQuery;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }
}
