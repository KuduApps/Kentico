using System;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.Forums;
using CMS.SettingsProvider;

public partial class CMSWebParts_Forums_ForumPostsViewer : CMSAbstractWebPart
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
            forumDataSource.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, forumDataSource.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            forumDataSource.CacheDependencies = value;
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
            forumDataSource.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets forum groups names, use semicolon like separator.
    /// </summary>
    public string ForumGroups
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ForumGroups"), "");
        }
        set
        {
            SetValue("ForumGroups", value);
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WhereCondition"), "");
        }
        set
        {
            SetValue("WhereCondition", value);
            forumDataSource.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OrderBy"), "");
        }
        set
        {
            SetValue("OrderBy", value);
            forumDataSource.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets Select only approved property.
    /// </summary>
    public bool SelectOnlyApproved
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectOnlyApproved"), true);
        }
        set
        {
            SetValue("SelectOnlyApproved", value);
            forumDataSource.SelectOnlyApproved = value;
        }
    }


    /// <summary>
    /// Gets or sets TopN.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SelectTopN"), -1);
        }
        set
        {
            SetValue("SelectTopN", value);
            forumDataSource.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SiteName"), CMSContext.CurrentSiteName);
        }
        set
        {
            SetValue("SiteName", value);
            forumDataSource.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FilterName"), "");
        }
        set
        {
            SetValue("FilterName", value);
            forumDataSource.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets selected columns.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Columns"), "");
        }
        set
        {
            SetValue("Columns", value);
            forumDataSource.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Indicates if group posts should be included.
    /// </summary>
    public bool ShowGroupPosts
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowGroupPosts"), false);
        }
        set
        {
            this.SetValue("ShowGroupPosts", value);
            forumDataSource.ShowGroupPosts = value;
        }
    }


    /// <summary>
    /// Gets or sets community group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupName"), "");
        }
        set
        {
            this.SetValue("GroupName", value);
        }
    }

    #endregion


    #region "Basic repeater properties"

    /// <summary>
    /// Gets or sets AlternatingItemTemplate property.
    /// </summary>
    public string AlternatingItemTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AlternatingItemTransformationName"), "");
        }
        set
        {
            SetValue("AlternatingItemTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets FooterTemplate property.
    /// </summary>
    public string FooterTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FooterTransformationName"), "");
        }
        set
        {
            SetValue("FooterTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HeaderTemplate property.
    /// </summary>
    public string HeaderTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("HeaderTransformationName"), "");
        }
        set
        {
            SetValue("HeaderTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets ItemTemplate property.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), "");
        }
        set
        {
            SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets SeparatorTemplate property.
    /// </summary>
    public string SeparatorTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SeparatorTransformationName"), "");
        }
        set
        {
            SetValue("SeparatorTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideControlForZeroRows"), true);
        }
        set
        {
            SetValue("HideControlForZeroRows", value);
            repLatestPosts.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets ZeroRowsText property.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ZeroRowsText"), "");
        }
        set
        {
            SetValue("ZeroRowsText", value);
            repLatestPosts.ZeroRowsText = value;
        }
    }

    #endregion


    #region "UniPager properties"

    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HidePagerForSinglePage"), pagerElem.HidePagerForSinglePage);
        }
        set
        {
            SetValue("HidePagerForSinglePage", value);
            pagerElem.HidePagerForSinglePage = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of records to display on a page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("PageSize"), pagerElem.PageSize);
        }
        set
        {
            SetValue("PageSize", value);
            pagerElem.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of pages displayed for current page range.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("GroupSize"), pagerElem.GroupSize);
        }
        set
        {
            SetValue("GroupSize", value);
            pagerElem.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager mode ('querystring' or 'postback').
    /// </summary>
    public string PagingMode
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagingMode"), "querystring");
        }
        set
        {
            if (value != null)
            {
                SetValue("PagingMode", value);
                switch (value.ToLower())
                {
                    case "postback": pagerElem.PagerMode = UniPagerMode.PostBack; break;
                    default: pagerElem.PagerMode = UniPagerMode.Querystring; break;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the querysting parameter.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(GetValue("QueryStringKey"), pagerElem.QueryStringKey);
        }
        set
        {
            SetValue("QueryStringKey", value);
            pagerElem.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayFirstLastAutomatically"), pagerElem.DisplayFirstLastAutomatically);
        }
        set
        {
            SetValue("DisplayFirstLastAutomatically", value);
            pagerElem.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayPreviousNextAutomatically"), pagerElem.DisplayPreviousNextAutomatically);
        }
        set
        {
            SetValue("DisplayPreviousNextAutomatically", value);
            pagerElem.DisplayPreviousNextAutomatically = value;
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
            return ValidationHelper.GetString(GetValue("Pages"), "");
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
            return ValidationHelper.GetString(GetValue("CurrentPage"), "");
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
            return ValidationHelper.GetString(GetValue("PageSeparator"), "");
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
            return ValidationHelper.GetString(GetValue("FirstPage"), "");
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
            return ValidationHelper.GetString(GetValue("LastPage"), "");
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
            return ValidationHelper.GetString(GetValue("PreviousPage"), "");
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
            return ValidationHelper.GetString(GetValue("NextPage"), "");
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
            return ValidationHelper.GetString(GetValue("PreviousGroup"), "");
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
            return ValidationHelper.GetString(GetValue("NextGroup"), "");
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
            return ValidationHelper.GetString(GetValue("PagerLayout"), "");
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
            return ValidationHelper.GetString(GetValue("DirectPage"), "");
        }
        set
        {
            SetValue("DirectPage", value);
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
        repLatestPosts.DataBindByDefault = false;
        pagerElem.PageControl = repLatestPosts.ID;

        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (!String.IsNullOrEmpty(TransformationName))
            {
                // Basic control properties
                repLatestPosts.HideControlForZeroRows = HideControlForZeroRows;
                repLatestPosts.ZeroRowsText = ZeroRowsText;

                // Set data source groupid according to group name
                if (!String.IsNullOrEmpty(this.GroupName))
                {
                    GeneralizedInfo gi = ModuleCommands.CommunityGetGroupInfoByName(this.GroupName, this.SiteName);
                    if (gi != null)
                    {
                        forumDataSource.GroupID = ValidationHelper.GetInteger(gi.GetValue("GroupID"), 0);
                    }
                    else
                    {
                        forumDataSource.StopProcessing = true;
                    }
                }

                if (!forumDataSource.StopProcessing)
                {

                    // Data source properties
                    forumDataSource.TopN = SelectTopN;
                    forumDataSource.OrderBy = OrderBy;
                    forumDataSource.CacheItemName = CacheItemName;
                    forumDataSource.CacheDependencies = CacheDependencies;
                    forumDataSource.CacheMinutes = CacheMinutes;
                    forumDataSource.FilterName = ValidationHelper.GetString(GetValue("WebPartControlID"), ClientID);
                    forumDataSource.SourceFilterName = FilterName;
                    forumDataSource.SelectOnlyApproved = SelectOnlyApproved;
                    forumDataSource.SiteName = SiteName;
                    forumDataSource.SelectedColumns = Columns;
                    forumDataSource.ShowGroupPosts = ShowGroupPosts && String.IsNullOrEmpty(ForumGroups);

                    #region "Repeater template properties"

                    // Apply transformations if they exist
                    repLatestPosts.ItemTemplate = CMSDataProperties.LoadTransformation(this, TransformationName, false);

                    if (!String.IsNullOrEmpty(AlternatingItemTransformationName))
                    {
                        repLatestPosts.AlternatingItemTemplate = CMSDataProperties.LoadTransformation(this, AlternatingItemTransformationName, false);
                    }
                    if (!String.IsNullOrEmpty(FooterTransformationName))
                    {
                        repLatestPosts.FooterTemplate = CMSDataProperties.LoadTransformation(this, FooterTransformationName, false);
                    }
                    if (!String.IsNullOrEmpty(HeaderTransformationName))
                    {
                        repLatestPosts.HeaderTemplate = CMSDataProperties.LoadTransformation(this, HeaderTransformationName, false);
                    }
                    if (!String.IsNullOrEmpty(SeparatorTransformationName))
                    {
                        repLatestPosts.SeparatorTemplate = CMSDataProperties.LoadTransformation(this, SeparatorTransformationName, false);
                    }

                    #endregion

                    // UniPager properties
                    pagerElem.PageSize = PageSize;
                    pagerElem.GroupSize = GroupSize;
                    pagerElem.QueryStringKey = QueryStringKey;
                    pagerElem.DisplayFirstLastAutomatically = DisplayFirstLastAutomatically;
                    pagerElem.DisplayPreviousNextAutomatically = DisplayPreviousNextAutomatically;
                    pagerElem.HidePagerForSinglePage = HidePagerForSinglePage;

                    switch (PagingMode.ToLower())
                    {
                        case "postback":
                            pagerElem.PagerMode = UniPagerMode.PostBack;
                            break;
                        default:
                            pagerElem.PagerMode = UniPagerMode.Querystring;
                            break;
                    }

                    #region "UniPager template properties"

                    // UniPager template properties
                    if (!String.IsNullOrEmpty(PagesTemplate))
                    {
                        pagerElem.PageNumbersTemplate = CMSDataProperties.LoadTransformation(pagerElem, PagesTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(CurrentPageTemplate))
                    {
                        pagerElem.CurrentPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, CurrentPageTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(SeparatorTemplate))
                    {
                        pagerElem.PageNumbersSeparatorTemplate = CMSDataProperties.LoadTransformation(pagerElem, SeparatorTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(FirstPageTemplate))
                    {
                        pagerElem.FirstPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, FirstPageTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(LastPageTemplate))
                    {
                        pagerElem.LastPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, LastPageTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(PreviousPageTemplate))
                    {
                        pagerElem.PreviousPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, PreviousPageTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(NextPageTemplate))
                    {
                        pagerElem.NextPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, NextPageTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(PreviousGroupTemplate))
                    {
                        pagerElem.PreviousGroupTemplate = CMSDataProperties.LoadTransformation(pagerElem, PreviousGroupTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(NextGroupTemplate))
                    {
                        pagerElem.NextGroupTemplate = CMSDataProperties.LoadTransformation(pagerElem, NextGroupTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(DirectPageTemplate))
                    {
                        pagerElem.DirectPageTemplate = CMSDataProperties.LoadTransformation(pagerElem, DirectPageTemplate, false);
                    }

                    if (!String.IsNullOrEmpty(LayoutTemplate))
                    {
                        pagerElem.LayoutTemplate = CMSDataProperties.LoadTransformation(pagerElem, LayoutTemplate, false);
                    }

                    #endregion

                    #region "Complete where condition"

                    string where = "";
                    string[] groups = ForumGroups.Split(';');
                    foreach (string group in groups)
                    {
                        if (group != "")
                        {
                            if (where != "")
                            {
                                where += "OR";
                            }
                            where += "(GroupName = N'" + SqlHelperClass.GetSafeQueryString(group, false) + "')";
                        }
                    }

                    if (!string.IsNullOrEmpty(WhereCondition))
                    {
                        if (string.IsNullOrEmpty(where))
                        {
                            where = WhereCondition;
                        }
                        else
                        {
                            where = "(" + WhereCondition + ") AND (" + where + ")";
                        }
                    }

                    #endregion

                    forumDataSource.WhereCondition = where;
                    forumDataSource.CheckPermissions = true;
                    repLatestPosts.DataSource = forumDataSource.DataSource;

                    if (!DataHelper.DataSourceIsEmpty(repLatestPosts.DataSource))
                    {
                        repLatestPosts.DataBind();
                    }
                }
            }
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        Visible = repLatestPosts.HasData() || !HideControlForZeroRows;
        base.OnPreRender(e);
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        forumDataSource.ClearCache();
    }
}
