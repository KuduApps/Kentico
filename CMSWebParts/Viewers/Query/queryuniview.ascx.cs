using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.PortalEngine;

public partial class CMSWebParts_Viewers_Query_queryuniview: CMSAbstractWebPart
{
    #region "Query properties"


    /// <summary>
    /// Gets or sets the query name.
    /// </summary>
    public string QueryName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("QueryName"), uniView.QueryName);
        }
        set
        {
            SetValue("QueryName", value);
            uniView.QueryName = value;
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
            uniView.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, uniView.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            uniView.CacheDependencies = value;
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
            uniView.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("OrderBy"), uniView.OrderBy), uniView.OrderBy);
        }
        set
        {
            SetValue("OrderBy", value);
            uniView.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("WhereCondition"), uniView.WhereCondition);
        }
        set
        {
            SetValue("WhereCondition", value);
            uniView.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the number which indicates how many documents should be displayed.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SelectTopN"), uniView.SelectTopN);
        }
        set
        {
            SetValue("SelectTopN", value);
            uniView.SelectTopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the columns to get.
    /// </summary>
    public string Columns
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("Columns"), uniView.Columns);
        }
        set
        {
            SetValue("Columns", value);
            uniView.Columns = value;
        }
    }

    #endregion


    #region "Pager properties"

    /// <summary>
    /// Gets or sets the value that indicates whether paging is enabled.
    /// </summary>
    public bool EnablePaging
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("EnablePaging"), false);
        }
        set
        {
            SetValue("EnablePaging", value);
            uniView.EnablePaging = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager position.
    /// </summary>
    public PagingPlaceTypeEnum PagerPosition
    {
        get
        {
            return BasicDataPager.StringToPagingPlaceTypeEnum(DataHelper.GetNotEmpty(GetValue("PagerPosition"), uniView.PagerPosition.ToString()));
        }
        set
        {
            SetValue("PagerPosition", value.ToString());
            uniView.PagerPosition = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of the documents displayed on each sigle page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("PageSize"), uniView.PageSize);
        }
        set
        {
            SetValue("PageSize", value);
            uniView.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager query string key.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("QueryStringKey"), uniView.PagerControl.QueryStringKey), uniView.PagerControl.QueryStringKey);
        }
        set
        {
            SetValue("QueryStringKey", value);
            uniView.PagerControl.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the paging mode.
    /// </summary>
    public UniPagerMode PagerMode
    {
        get
        {
            string strMode = ValidationHelper.GetString(GetValue("PagerMode"), String.Empty).ToLower();
            switch (strMode)
            {
                case "postback":
                    return UniPagerMode.PostBack;
                
                default:
                    return UniPagerMode.Querystring;
            }
        }
        set
        {
            SetValue("PagerMode", value.ToString());
            uniView.PagerControl.PagerMode = value;
        }
    }

 
    ///// <summary>
    ///// Gets or sets the results position
    ///// </summary>
    //public ResultsLocationTypeEnum ResultsPosition
    //{
    //    get
    //    {
    //        return uniView.PagerControl.GetResultPosition(ValidationHelper.GetString(GetValue("ResultsPosition"), ""));
    //    }
    //    set
    //    {
    //        SetValue("ResultsPosition", value);
    //        uniView.PagerControl.ResultsLocation = value;
    //    }
    //}


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayFirstLastAutomatically"), uniView.PagerControl.DisplayFirstLastAutomatically);
        }
        set
        {
            SetValue("DisplayFirstLastAutomatically", value);
            uniView.PagerControl.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayPreviousNextAutomatically"), uniView.PagerControl.DisplayPreviousNextAutomatically);
        }
        set
        {
            SetValue("DisplayPreviousNextAutomatically", value);
            uniView.PagerControl.DisplayPreviousNextAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of pages displayed for current page range.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("GroupSize"), uniView.PagerControl.GroupSize);
        }
        set
        {
            SetValue("GroupSize", value);
            uniView.PagerControl.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HidePagerForSinglePage"), uniView.PagerControl.HidePagerForSinglePage);
        }
        set
        {
            SetValue("HidePagerForSinglePage", value);
            uniView.PagerControl.HidePagerForSinglePage = value;
        }
    }

    #endregion


    #region "Transformation properties"

    /// <summary>
    /// Gets or sets the name of the hierarchical transforamtion which is used for displaying the results.
    /// </summary>
    public string HierarchicalTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("HierarchicalTransformationName"), uniView.HierarchicalTransformationName);
        }
        set
        {
            SetValue("HierarchicalTransformationName", value);
            uniView.HierarchicalTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("TransformationName"), uniView.TransformationName);
        }
        set
        {
            SetValue("TransformationName", value);
            uniView.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the alternate results.
    /// </summary>
    public string AlternatingTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("AlternatingTransformationName"), uniView.AlternatingTransformationName), uniView.AlternatingTransformationName);
        }
        set
        {
            SetValue("AlternatingTransformationName", value);
            uniView.AlternatingTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the header transforamtion which is used for displaying the results.
    /// </summary>
    public string HeaderTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("HeaderTransformationName"), uniView.HeaderTransformationName);
        }
        set
        {
            SetValue("HeaderTransformationName", value);
            uniView.HeaderTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the footer transforamtion which is used for displaying the results.
    /// </summary>
    public string FooterTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("FooterTransformationName"), uniView.FooterTransformationName);
        }
        set
        {
            SetValue("FooterTransformationName", value);
            uniView.FooterTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the first transforamtion which is used for displaying the results.
    /// </summary>
    public string FirstTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("FirstTransformationName"), uniView.FirstTransformationName);
        }
        set
        {
            SetValue("FirstTransformationName", value);
            uniView.FirstTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the last transforamtion which is used for displaying the results.
    /// </summary>
    public string LastTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("LastTransformationName"), uniView.LastTransformationName);
        }
        set
        {
            SetValue("LastTransformationName", value);
            uniView.LastTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the single transforamtion which is used for displaying the results.
    /// </summary>
    public string SeparatorTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SeparatorTransformationName"), uniView.SeparatorTransformationName);
        }
        set
        {
            SetValue("SeparatorTransformationName", value);
            uniView.SeparatorTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the single transforamtion which is used for displaying the results.
    /// </summary>
    public string SingleTransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SingleTransformationName"), uniView.SingleTransformationName);
        }
        set
        {
            SetValue("SingleTransformationName", value);
            uniView.SingleTransformationName = value;
        }
    }
    
    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideControlForZeroRows"), uniView.HideControlForZeroRows);
        }
        set
        {
            SetValue("HideControlForZeroRows", value);
            uniView.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows results.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(GetValue("ZeroRowsText"), uniView.ZeroRowsText), uniView.ZeroRowsText);
        }
        set
        {
            SetValue("ZeroRowsText", value);
            uniView.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the separator (tetx, html code) which is displayed between displayed items.
    /// </summary>
    public string ItemSeparator
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("ItemSeparator"), uniView.ItemSeparatorValue);
        }
        set
        {
            SetValue("ItemSeparator", value);
            uniView.ItemSeparatorValue = value;
        }
    }


    /// <summary>
    /// Filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FilterName"), uniView.FilterName);
        }
        set
        {
            SetValue("FilterName", value);
            uniView.FilterName = value;
        }
    }


    /// <summary>
    /// UniView control.
    /// </summary>
    public QueryUniView UniViewControl
    {
        get
        {
            return uniView;
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
            uniView.StopProcessing = value;
        }
    }

    #endregion


    #region "UniPager Template properties"

    /// <summary>
    /// Gets or sets the pages template.
    /// </summary>
    public string PagesTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Pages"), String.Empty);
        }
        set
        {
            SetValue("Pages", value);
        }
    }


    /// <summary>
    /// Gets or sets the current page template.
    /// </summary>
    public string CurrentPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CurrentPage"), String.Empty);
        }
        set
        {
            SetValue("CurrentPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the separator template.
    /// </summary>
    public string SeparatorTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PageSeparator"), String.Empty);
        }
        set
        {
            SetValue("PageSeparator", value);
        }
    }


    /// <summary>
    /// Gets or sets the first page template.
    /// </summary>
    public string FirstPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FirstPage"), String.Empty);
        }
        set
        {
            SetValue("FirstPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the last page template.
    /// </summary>
    public string LastPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LastPage"), String.Empty);
        }
        set
        {
            SetValue("LastPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous page template.
    /// </summary>
    public string PreviousPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousPage"), String.Empty);
        }
        set
        {
            SetValue("PreviousPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the next page template.
    /// </summary>
    public string NextPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextPage"), String.Empty);
        }
        set
        {
            SetValue("NextPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous group template.
    /// </summary>
    public string PreviousGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousGroup"), String.Empty);
        }
        set
        {
            SetValue("PreviousGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the next group template.
    /// </summary>
    public string NextGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextGroup"), String.Empty);
        }
        set
        {
            SetValue("NextGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the layout template.
    /// </summary>
    public string LayoutTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagerLayout"), String.Empty);
        }
        set
        {
            SetValue("PagerLayout", value);
        }
    }


    /// <summary>
    /// Gets or sets the direct page template.
    /// </summary>
    public string DirectPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DirectPage"), String.Empty);
        }
        set
        {
            SetValue("DirectPage", value);
        }
    }

    #endregion

    
    #region "QueryUniView properties"

    /// <summary>
    /// Gets or sets the value that indictes whether data should be binded in default format
    /// or changet to hierarchical grouped dataset
    /// </summary>
    public bool LoadHierarchicalData
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("LoadHierarchicalData"), this.uniView.LoadHierarchicalData);
        }
        set
        {
            this.SetValue("LoadHierarchicalData", value);
            this.uniView.LoadHierarchicalData = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether default hierarchical order value should be used.
    /// The order is used only if LoadHierarchicalData is set to true.
    /// Default order value is "LevelColumnName". Value of OrderBy property is joined at the end of the order by expression
    /// </summary>
    public bool UseHierarchicalOrder
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseHierarchicalOrdering"), this.uniView.UseHierarchicalOrder);
        }
        set
        {
            this.SetValue("UseHierarchicalOrdering", value);
            this.uniView.UseHierarchicalOrder = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether hierarchical data should be displayed in inner or separate mode.
    /// </summary>
    public HierarchicalDisplayModeEnum HierarchicalDisplayMode
    {
        get
        {
            string displayMode = ValidationHelper.GetString(this.GetValue("HierarchicalDisplayMode"), "inner").ToLower();
            switch (displayMode)
            {
                case "separate":
                    return HierarchicalDisplayModeEnum.Separate;

                default:
                    return HierarchicalDisplayModeEnum.Inner;
            }
        }
        set
        {
            switch(value)
            {
                case HierarchicalDisplayModeEnum.Inner:
                    this.SetValue("HierarchicalDisplayMode", "inner");
                    break;

                case HierarchicalDisplayModeEnum.Separate:
                    this.SetValue("HierarchicalDisplayMode", "separate");
                    break;
            }

            this.uniView.HierarchicalDisplayMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether header and footer items should be hidden if single item is displayed.
    /// </summary>
    public bool HideHeaderAndFooterForSingleItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideHeaderAndFooterForSingleItem"), this.uniView.HideHeaderAndFooterForSingleItem);
        }
        set
        {
            this.SetValue("HideHeaderAndFooterForSingleItem", value);
            this.uniView.HideHeaderAndFooterForSingleItem = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the ID column.
    /// </summary>
    public string IDColumnName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("IDColumnName"), uniView.IDColumnName);
        }
        set
        {
            this.SetValue("IDColumnName", value);
            this.uniView.IDColumnName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the parent ID column.
    /// </summary>
    public string ParentIDColumnName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ParentIDColumnName"), uniView.ParentIDColumnName);
        }
        set
        {
            this.SetValue("ParentIDColumnName", value);
            this.uniView.ParentIDColumnName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the level column.
    /// </summary>
    public string LevelColumnName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LevelColumnName"), uniView.LevelColumnName);
        }
        set
        {
            this.SetValue("LevelColumnName", value);
            this.uniView.LevelColumnName = value;
        }
    }

    
    /// <summary>
    /// Gets or sets the name of the selected item column.
    /// </summary>
    public string SelectedItemColumnName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemColumnName"), uniView.SelectedItemColumnName);
        }
        set
        {
            this.SetValue("SelectedItemColumnName", value);
            this.uniView.SelectedItemColumnName = value;
        }
    }


    /// <summary>
    /// Gets or sets the selected item value.
    /// </summary>
    public object SelectedItemValue
    {
        get
        {
            return this.GetValue("SelectedItemValue");
        }
        set
        {
            this.SetValue("SelectedItemValue", value);
            this.uniView.SelectedItemValue = value;
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
            uniView.StopProcessing = true;
        }
        else
        {
            uniView.ControlContext = ControlContext;

            // Query properties
            uniView.QueryName = this.QueryName;
            uniView.CacheItemName = CacheItemName;
            uniView.CacheDependencies = CacheDependencies;
            uniView.CacheMinutes = CacheMinutes;
            uniView.OrderBy = OrderBy;
            uniView.SelectTopN = SelectTopN;
            uniView.Columns = Columns;
            uniView.WhereCondition = WhereCondition;

            // CMSUniView properties
            uniView.LoadHierarchicalData = this.LoadHierarchicalData;
            uniView.UseHierarchicalOrder = this.UseHierarchicalOrder;
            uniView.HideHeaderAndFooterForSingleItem = this.HideHeaderAndFooterForSingleItem;
            uniView.HierarchicalDisplayMode = this.HierarchicalDisplayMode;

            // Pager
            uniView.EnablePaging = this.EnablePaging;
            uniView.PageSize = this.PageSize;
            uniView.PagerControl.QueryStringKey = this.QueryStringKey;
            uniView.PagerControl.PagerMode = this.PagerMode;
            uniView.PagerPosition = this.PagerPosition;
            uniView.PagerControl.HidePagerForSinglePage = this.HidePagerForSinglePage;
            uniView.PagerControl.GroupSize = this.GroupSize;
            uniView.PagerControl.DisplayFirstLastAutomatically = this.DisplayFirstLastAutomatically;
            uniView.PagerControl.DisplayPreviousNextAutomatically = this.DisplayPreviousNextAutomatically;

            // Pager transformations
            #region "UniPager template properties"

            // UniPager template properties
            if (!String.IsNullOrEmpty(PagesTemplate))
            {
                uniView.PagerControl.PageNumbersTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, PagesTemplate, false);
            }

            if (!String.IsNullOrEmpty(CurrentPageTemplate))
            {
                uniView.PagerControl.CurrentPageTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, CurrentPageTemplate, false);
            }

            if (!String.IsNullOrEmpty(SeparatorTemplate))
            {
                uniView.PagerControl.PageNumbersSeparatorTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, SeparatorTemplate, false);
            }

            if (!String.IsNullOrEmpty(FirstPageTemplate))
            {
                uniView.PagerControl.FirstPageTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, FirstPageTemplate, false);
            }

            if (!String.IsNullOrEmpty(LastPageTemplate))
            {
                uniView.PagerControl.LastPageTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, LastPageTemplate, false);
            }

            if (!String.IsNullOrEmpty(PreviousPageTemplate))
            {
                uniView.PagerControl.PreviousPageTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, PreviousPageTemplate, false);
            }

            if (!String.IsNullOrEmpty(NextPageTemplate))
            {
                uniView.PagerControl.NextPageTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, NextPageTemplate, false);
            }

            if (!String.IsNullOrEmpty(PreviousGroupTemplate))
            {
                uniView.PagerControl.PreviousGroupTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, PreviousGroupTemplate, false);
            }

            if (!String.IsNullOrEmpty(NextGroupTemplate))
            {
                uniView.PagerControl.NextGroupTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, NextGroupTemplate, false);
            }

            if (!String.IsNullOrEmpty(DirectPageTemplate))
            {
                uniView.PagerControl.DirectPageTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, DirectPageTemplate, false);
            }

            if (!String.IsNullOrEmpty(LayoutTemplate))
            {
                uniView.PagerControl.LayoutTemplate = CMSDataProperties.LoadTransformation(uniView.PagerControl, LayoutTemplate, false);
            }

            #endregion

            uniView.ParentIDColumnName = this.ParentIDColumnName;
            uniView.IDColumnName = this.IDColumnName;
            uniView.LevelColumnName = this.LevelColumnName;
            uniView.SelectedItemColumnName = this.SelectedItemColumnName;
            uniView.SelectedItemValue = this.SelectedItemValue;

            // Transformation properties
            uniView.TransformationName = this.TransformationName;
            uniView.HierarchicalTransformationName = this.HierarchicalTransformationName;
            uniView.AlternatingTransformationName = this.AlternatingTransformationName;
            uniView.FooterTransformationName = this.FooterTransformationName;
            uniView.HeaderTransformationName = this.HeaderTransformationName;
            uniView.FirstTransformationName = this.FirstTransformationName;
            uniView.LastTransformationName = this.LastTransformationName;
            uniView.SingleTransformationName = this.SingleTransformationName;
            uniView.SeparatorTransformationName = this.SeparatorTransformationName;

            // Public properties
            uniView.HideControlForZeroRows = HideControlForZeroRows;
            uniView.ZeroRowsText = ZeroRowsText;
            uniView.ItemSeparatorValue = ItemSeparator;
            uniView.FilterName = FilterName;

            // Add repeater to the filter collection
            CMSControlsHelper.SetFilter(ValidationHelper.GetString(GetValue("WebPartControlID"), ClientID), uniView);
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Visible = !uniView.StopProcessing;

        if (DataHelper.DataSourceIsEmpty(uniView.DataSource) && (uniView.HideControlForZeroRows))
        {
            Visible = false;
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        uniView.ReloadData(true);
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        uniView.ClearCache();
    }
}
