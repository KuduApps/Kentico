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
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.Ecommerce;
using CMS.SettingsProvider;

public partial class CMSWebParts_Ecommerce_Products_ProductDataList : CMSAbstractWebPart
{
    #region "Pager properties"

    /// <summary>
    /// Pager control.
    /// </summary>
    public CMS.Controls.DataPager PagerControl
    {
        get
        {
            return this.productDataList.PagerControl;
        }
    }


    /// <summary>
    /// Enable paging.
    /// </summary>
    public bool EnablePaging
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnablePaging"), this.productDataList.EnablePaging);
        }
        set
        {
            this.SetValue("EnablePaging", value);
            this.productDataList.EnablePaging = value;
        }
    }


    /// <summary>
    /// Pager position.
    /// </summary>
    public PagingPlaceTypeEnum PagerPosition
    {
        get
        {
            return this.PagerControl.GetPagerPosition(DataHelper.GetNotEmpty(this.GetValue("PagerPosition"), this.PagerControl.PagerPosition.ToString()));
        }
        set
        {
            this.SetValue("PagerPosition", value.ToString());
            this.PagerControl.PagerPosition = value;
        }
    }


    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), this.PagerControl.PageSize);
        }
        set
        {
            this.SetValue("PageSize", value);
            this.PagerControl.PageSize = value;
        }
    }


    /// <summary>
    /// Pager query string key.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("QueryStringKey"), this.PagerControl.QueryStringKey), this.PagerControl.QueryStringKey);
        }
        set
        {
            this.SetValue("QueryStringKey", value);
            this.PagerControl.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Paging mode.
    /// </summary>
    public PagingModeTypeEnum PagingMode
    {
        get
        {
            return this.PagerControl.GetPagingMode(DataHelper.GetNotEmpty(this.GetValue("PagingMode"), this.PagerControl.PagingMode.ToString()));
        }
        set
        {
            this.SetValue("PagingMode", value.ToString());
            this.PagerControl.PagingMode = value;
        }
    }


    /// <summary>
    /// Show first / last links
    /// </summary>
    public bool ShowFirstLast
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowFirstLast"), this.PagerControl.ShowFirstLast);
        }
        set
        {
            this.SetValue("ShowFirstLast", value);
            this.PagerControl.ShowFirstLast = value;
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
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.productDataList.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.productDataList.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.productDataList.ZeroRowsText), this.productDataList.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.productDataList.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Repeat columns.
    /// </summary>
    public int RepeatColumns
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RepeatColumns"), this.productDataList.RepeatColumns);
        }
        set
        {
            this.SetValue("RepeatColumns", value);
            this.productDataList.RepeatColumns = value;
        }
    }


    /// <summary>
    /// Repeat layout.
    /// </summary>
    public RepeatLayout RepeatLayout
    {
        get
        {
            return CMSDataList.GetRepeatLayout(DataHelper.GetNotEmpty(this.GetValue("RepeatLayout"), this.productDataList.RepeatLayout.ToString()));
        }
        set
        {
            this.SetValue("RepeatLayout", value.ToString());
            this.productDataList.RepeatLayout = value;
        }
    }


    /// <summary>
    /// Repeat Direction.
    /// </summary>
    public RepeatDirection RepeatDirection
    {
        get
        {
            return CMSDataList.GetRepeatDirection(DataHelper.GetNotEmpty(this.GetValue("RepeatDirection"), this.productDataList.RepeatDirection.ToString()));
        }
        set
        {
            this.SetValue("RepeatDirection", value.ToString());
            this.productDataList.RepeatDirection = value;
        }
    }


    /// <summary>
    /// Select TOP N records.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), this.productDataList.SelectTopN);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            this.productDataList.SelectTopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FilterName"), productDataList.FilterName);
        }
        set
        {
            SetValue("FilterName", value);
            productDataList.FilterName = value;
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
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("TransformationName"), this.productDataList.TransformationName), this.productDataList.TransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
            this.productDataList.TransformationName = value;
        }
    }


    /// <summary>
    /// Alternating transformation name.
    /// </summary>
    public string AlternatingTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("AlternatingTransformationName"), this.productDataList.AlternatingTransformationName), this.productDataList.AlternatingTransformationName);
        }
        set
        {
            this.SetValue("AlternatingTransformationName", value);
            this.productDataList.AlternatingTransformationName = value;
        }
    }


    /// <summary>
    /// Selected item transformation name.
    /// </summary>
    public string SelectedItemTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SelectedItemTransformationName"), this.productDataList.SelectedItemTransformationName), this.productDataList.SelectedItemTransformationName);
        }
        set
        {
            this.SetValue("SelectedItemTransformationName", value);
            this.productDataList.AlternatingTransformationName = value;
        }
    }

    #endregion


    #region "Content"

    /// <summary>
    /// Where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WhereCondition"), this.productDataList.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
        }
    }


    /// <summary>
    /// Order by.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderBy"), this.productDataList.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
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
            this.productDataList.StopProcessing = true;
        }
        else
        {
            int querySkuId = QueryHelper.GetInteger("SKUid", 0);

            // Pager
            this.PagerControl.PagerPosition = this.PagerPosition;
            this.productDataList.EnablePaging = this.EnablePaging;
            this.PagerControl.PageSize = this.PageSize;
            this.PagerControl.QueryStringKey = this.QueryStringKey;
            this.PagerControl.PagingMode = this.PagingMode;
            this.PagerControl.ShowFirstLast = this.ShowFirstLast;

            // Public
            this.productDataList.RepeatColumns = this.RepeatColumns;
            this.productDataList.RepeatLayout = this.RepeatLayout;
            this.productDataList.RepeatDirection = this.RepeatDirection;
            this.productDataList.HideControlForZeroRows = this.HideControlForZeroRows;
            this.productDataList.ZeroRowsText = this.ZeroRowsText;
            this.productDataList.FilterName = this.FilterName;

            // Transformations
            this.productDataList.AlternatingTransformationName = this.AlternatingTransformationName;
            this.productDataList.TransformationName = this.TransformationName;
            this.productDataList.SelectedItemTransformationName = this.SelectedItemTransformationName;

            // Select one SKU product by query string SKU id
            if (querySkuId > 0)
            {
                this.WhereCondition = SqlHelperClass.AddWhereCondition(this.WhereCondition, "(SKUId = " + querySkuId + ")"); 
                this.productDataList.TransformationName = this.SelectedItemTransformationName;
            }

            // Data settings
            this.productDataList.DataSource = SKUInfoProvider.GetSKUs(this.WhereCondition, this.OrderBy);
            this.productDataList.SelectTopN = this.SelectTopN;

            if ((this.productDataList.SelectTopN > 0) && (!DataHelper.DataSourceIsEmpty(this.productDataList.DataSource)))
            {
                for (int i = 0; i < ((DataSet)this.productDataList.DataSource).Tables[0].Rows.Count; i++)
                {
                    if (i >= this.productDataList.SelectTopN)
                    {
                        ((DataSet)this.productDataList.DataSource).Tables[0].DefaultView.Delete(this.productDataList.SelectTopN);
                    }
                }
                ((DataSet)this.productDataList.DataSource).Tables[0].AcceptChanges();
            }

            this.productDataList.ReloadData(false);
            this.productDataList.EnablePaging = true;
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.productDataList.Visible;

        if (DataHelper.DataSourceIsEmpty(this.productDataList.DataSource) && (this.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        this.SetupControl();
        this.productDataList.DataBind();
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.productDataList.ClearCache();
    }
}
