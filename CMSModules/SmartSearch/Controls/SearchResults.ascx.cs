using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.PortalEngine;

public partial class CMSModules_SmartSearch_Controls_SearchResults : CMSUserControl, ISearchFilterable
{
    #region "Variables"

    // Filter support
    private string mFilterSearchCondition = null;
    private string mFilterSearchSort = null;
    private string mFilterID = null;

    // Search
    private string mSearchSort = null;
    private string mSearchCondition = null;
    private string mIndexes = null;
    private string mPath = null;
    private bool mCheckPermissions = false;
    private SearchOptionsEnum mSearchOptions = SearchOptionsEnum.BasicSearch;
    private string mTransformationName = "CMS.Root.SmartSearchResults";
    private string mDocumentTypes = null;
    private bool? mCombineWithDefaultCulture = null;
    private string mCultureCode = CMSContext.PreferredCultureCode;
    private bool mSearchInAttachments = false;
    private string mAttachmentsWhere = "";
    private string mAttachmentsOrderBy = "";
    private string mNoResultsText = ResHelper.GetString("srch.results.noresults");
    private bool mIgnoreTransformations = false;

    // Pager
    private int mPageSize = 10;
    private UniPagerMode mPagingMode = UniPagerMode.Querystring;
    private string mQueryStringKey = "";
    private int mGroupSize = 10;
    private bool mDisplayFirstLastAutomatically = false;
    private bool mDisplayPreviousNextAutomatically = false;
    private bool mHidePagerForSinglePage = false;
    private int mMaxPages = 200;

    // Pager template
    private string mPagesTemplateName = null;
    private string mCurrentPageTemplateName = null;
    private string mSeparatorTemplateName = null;
    private string mFirstPageTemplateName = null;
    private string mLastPageTemplateName = null;
    private string mPreviousPageTemplateName = null;
    private string mNextPageTemplateName = null;
    private string mPreviousGroupTemplateName = null;
    private string mNextGroupTemplateName = null;
    private string mLayoutTemplateName = null;

    // Direct access templates
    private ITemplate mPagesTemplate = null;
    private ITemplate mCurrentPageTemplate = null;
    private ITemplate mFirstPageTemplate = null;
    private ITemplate mLastPageTemplate = null;
    private ITemplate mPreviousPageTemplate = null;
    private ITemplate mNextPageTemplate = null;
    private ITemplate mPreviousGroupTemplate = null;
    private ITemplate mNextGroupTemplate = null;
    private ITemplate mLayoutTemplate = null;
    private ITemplate mPageNumbersSeparatorTemplate = null;
    // Template
    private ITemplate mItemTemplate = null;
    private ITemplate mAlternatingItemTemplate = null;
    private ITemplate mFooterTemplate = null;
    private ITemplate mHeaderTemplate = null;
    private ITemplate mSeparatorTemplate = null;

    // Basic repeater instance
    private BasicRepeater repSearchResults = new BasicRepeater();
    private int mMaxResults = 0;

    #endregion


    #region "Delegates & events"

    /// <summary>
    /// Search completed deleagate.
    /// </summary>
    /// <param name="visible">Determines whether this control is visible</param>
    public delegate void SearchCompletedHandler(bool visible);

    /// <summary>
    /// Raises when search is completed.
    /// </summary>
    public event SearchCompletedHandler OnSearchCompleted;

    #endregion


    #region "Search properties"

    /// <summary>
    /// Gets or sets filter search condition.
    /// </summary>
    public string FilterSearchCondition
    {
        get
        {
            return mFilterSearchCondition;
        }
        set
        {
            mFilterSearchCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets filter sorting of search.
    /// </summary>
    public string FilterSearchSort
    {
        get
        {
            return mFilterSearchSort;
        }
        set
        {
            mFilterSearchSort = value;
        }
    }


    /// <summary>
    /// Gets or sets sorting of search.
    /// </summary>
    public string SearchSort
    {
        get
        {
            return mSearchSort;
        }
        set
        {
            mSearchSort = value;
        }
    }


    /// <summary>
    /// Gets or sets search condition.
    /// </summary>
    public string SearchCondition
    {
        get
        {
            return mSearchCondition;
        }
        set
        {
            mSearchCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets indexes.
    /// </summary>
    public string Indexes
    {
        get
        {
            return mIndexes;
        }
        set
        {
            mIndexes = value;
        }
    }


    /// <summary>
    /// Gets or sets path.
    /// </summary>
    public string Path
    {
        get
        {
            return mPath;
        }
        set
        {
            mPath = value;
        }
    }


    /// <summary>
    /// Gets or sets document types.
    /// </summary>
    public string DocumentTypes
    {
        get
        {
            return mDocumentTypes;
        }
        set
        {
            mDocumentTypes = value;
        }
    }


    /// <summary>
    /// Gets or sets check permissions.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return mCheckPermissions;
        }
        set
        {
            mCheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets search option.
    /// </summary>
    public SearchOptionsEnum SearchOptions
    {
        get
        {
            return mSearchOptions;
        }
        set
        {
            mSearchOptions = value;
        }
    }


    /// <summary>
    /// Gets or sets transformation name.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return mTransformationName;
        }
        set
        {
            mTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return mCultureCode;
        }
        set
        {
            mCultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets combine with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            // Get from setting if not set
            if (!mCombineWithDefaultCulture.HasValue)
            {
                return SiteInfoProvider.CombineWithDefaultCulture(CMSContext.CurrentSiteName);
            }
            else
            {
                return mCombineWithDefaultCulture.Value;
            }
        }
        set
        {
            mCombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets search int attachments.
    /// </summary>
    public bool SearchInAttachments
    {
        get
        {
            return mSearchInAttachments;
        }
        set
        {
            mSearchInAttachments = value;
        }
    }


    /// <summary>
    /// Gets or sets culture code.
    /// </summary>
    public string AttachmentsWhere
    {
        get
        {
            return mAttachmentsWhere;
        }
        set
        {
            mAttachmentsWhere = value;
        }
    }


    /// <summary>
    /// Gets or sets culture code.
    /// </summary>
    public string AttachmentsOrderBy
    {
        get
        {
            return mAttachmentsOrderBy;
        }
        set
        {
            mAttachmentsOrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets culture code.
    /// </summary>
    public string NoResultsText
    {
        get
        {
            return mNoResultsText;
        }
        set
        {
            mNoResultsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the search mode label text.
    /// </summary>
    public string FilterID
    {
        get
        {
            return mFilterID;
        }
        set
        {
            mFilterID = value;
        }
    }


    /// <summary>
    /// Indicates if transformations should be ignored and templates for direct access should be used.
    /// </summary>
    public bool IgnoreTransformations
    {
        get
        {
            return mIgnoreTransformations;
        }
        set
        {
            mIgnoreTransformations = value;
        }
    }

    #endregion


    #region "Pager properties"

    /// <summary>
    /// Gets or sets page size.
    /// </summary>
    public int PageSize
    {
        get
        {
            return mPageSize;
        }
        set
        {
            mPageSize = value;
            pgrSearch.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets search option.
    /// </summary>
    public UniPagerMode PagingMode
    {
        get
        {
            return mPagingMode;
        }
        set
        {
            mPagingMode = value;
            pgrSearch.PagerMode = value;
        }
    }


    /// <summary>
    /// Gets or sets query string key.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return mQueryStringKey;
        }
        set
        {
            mQueryStringKey = value;
            pgrSearch.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets group size.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return mGroupSize;
        }
        set
        {
            mGroupSize = value;
            pgrSearch.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return mDisplayFirstLastAutomatically;
        }
        set
        {
            mDisplayFirstLastAutomatically = value;
            pgrSearch.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return mDisplayPreviousNextAutomatically;
        }
        set
        {
            mDisplayPreviousNextAutomatically = value;
            pgrSearch.DisplayPreviousNextAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return mHidePagerForSinglePage;
        }
        set
        {
            mHidePagerForSinglePage = value;
            pgrSearch.HidePagerForSinglePage = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager max pages.
    /// </summary>
    public int MaxPages
    {
        get
        {
            return mMaxPages;
        }
        set
        {
            mMaxPages = value;
            pgrSearch.MaxPages = value;
        }
    }


    /// <summary>
    /// Gets or sets the max. displayed results.
    /// </summary>
    public int MaxResults
    {
        get
        {
            return mMaxResults;
        }
        set
        {
            mMaxResults = value;
        }
    }

    #endregion


    #region "UniPager Template properties"

    /// <summary>
    /// Gets or sets the pages template name.
    /// </summary>
    public string PagesTemplateName
    {
        get
        {
            return mPagesTemplateName;
        }
        set
        {
            mPagesTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the current page template name.
    /// </summary>
    public string CurrentPageTemplateName
    {
        get
        {
            return mCurrentPageTemplateName;
        }
        set
        {
            mCurrentPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the separator template name.
    /// </summary>
    public string SeparatorTemplateName
    {
        get
        {
            return mSeparatorTemplateName;
        }
        set
        {
            mSeparatorTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the first page template name.
    /// </summary>
    public string FirstPageTemplateName
    {
        get
        {
            return mFirstPageTemplateName;
        }
        set
        {
            mFirstPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the last page template name.
    /// </summary>
    public string LastPageTemplateName
    {
        get
        {
            return mLastPageTemplateName;
        }
        set
        {
            mLastPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous page template name.
    /// </summary>
    public string PreviousPageTemplateName
    {
        get
        {
            return mPreviousPageTemplateName;
        }
        set
        {
            mPreviousPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the next page template name.
    /// </summary>
    public string NextPageTemplateName
    {
        get
        {
            return mNextPageTemplateName;
        }
        set
        {
            mNextPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous group template name.
    /// </summary>
    public string PreviousGroupTemplateName
    {
        get
        {
            return mPreviousGroupTemplateName;
        }
        set
        {
            mPreviousGroupTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the next group template name.
    /// </summary>
    public string NextGroupTemplateName
    {
        get
        {
            return mNextGroupTemplateName;
        }
        set
        {
            mNextGroupTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the layout template name.
    /// </summary>
    public string LayoutTemplateName
    {
        get
        {
            return mLayoutTemplateName;
        }
        set
        {
            mLayoutTemplateName = value;
        }
    }

    /// <summary>
    /// Gets or sets the pages template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate PageNumbersTemplate
    {
        get
        {
            return mPagesTemplate;
        }
        set
        {
            mPagesTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the current page template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate CurrentPageTemplate
    {
        get
        {
            return mCurrentPageTemplate;
        }
        set
        {
            mCurrentPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the first page template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate FirstPageTemplate
    {
        get
        {
            return mFirstPageTemplate;
        }
        set
        {
            mFirstPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the last page template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate LastPageTemplate
    {
        get
        {
            return mLastPageTemplate;
        }
        set
        {
            mLastPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous page template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate PreviousPageTemplate
    {
        get
        {
            return mPreviousPageTemplate;
        }
        set
        {
            mPreviousPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the next page template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate NextPageTemplate
    {
        get
        {
            return mNextPageTemplate;
        }
        set
        {
            mNextPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous group template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate PreviousGroupTemplate
    {
        get
        {
            return mPreviousGroupTemplate;
        }
        set
        {
            mPreviousGroupTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the next group template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate NextGroupTemplate
    {
        get
        {
            return mNextGroupTemplate;
        }
        set
        {
            mNextGroupTemplate = value;
        }
    }

    /// <summary>
    /// Gets or sets the next group template for direct access.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate PageNumbersSeparatorTemplate
    {
        get
        {
            return mPageNumbersSeparatorTemplate;
        }
        set
        {
            mPageNumbersSeparatorTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the layout template name.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public ITemplate LayoutTemplate
    {
        get
        {
            return mLayoutTemplate;
        }
        set
        {
            mLayoutTemplate = value;
        }
    }


    /// <summary>
    /// Sets or gets item template.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public virtual ITemplate ItemTemplate
    {
        get
        {
            return mItemTemplate;
        }
        set
        {
            mItemTemplate = value;
        }
    }


    /// <summary>
    /// Sets or gets alternating item template.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public virtual ITemplate AlternatingItemTemplate
    {
        get
        {
            return mAlternatingItemTemplate;
        }
        set
        {
            mAlternatingItemTemplate = value;
        }
    }


    /// <summary>
    /// Sets or gets header template.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public virtual ITemplate HeaderTemplate
    {
        get
        {
            return mHeaderTemplate;
        }
        set
        {
            mHeaderTemplate = value;
        }
    }


    /// <summary>
    /// Sets or gets footer template.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public virtual ITemplate FooterTemplate
    {
        get
        {
            return mFooterTemplate;
        }
        set
        {
            mFooterTemplate = value;
        }
    }


    /// <summary>
    /// Sets or gets separator template.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), Browsable(false)]
    public virtual ITemplate SeparatorTemplate
    {
        get
        {
            return mSeparatorTemplate;
        }
        set
        {
            mSeparatorTemplate = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // UniPager properties
            pgrSearch.PageSize = PageSize;
            pgrSearch.GroupSize = GroupSize;
            pgrSearch.QueryStringKey = QueryStringKey;
            pgrSearch.DisplayFirstLastAutomatically = DisplayFirstLastAutomatically;
            pgrSearch.DisplayPreviousNextAutomatically = DisplayPreviousNextAutomatically;
            pgrSearch.HidePagerForSinglePage = HidePagerForSinglePage;
            pgrSearch.PagerMode = PagingMode;
            pgrSearch.MaxPages = MaxPages;

            #region "UniPager template properties"

            // UniPager direct templates
            if (this.PageNumbersTemplate != null)
            {
                pgrSearch.PageNumbersTemplate = this.PageNumbersTemplate;
            }

            if (this.CurrentPageTemplate != null)
            {
                pgrSearch.CurrentPageTemplate = this.CurrentPageTemplate;
            }

            if (this.PageNumbersSeparatorTemplate != null)
            {
                pgrSearch.PageNumbersSeparatorTemplate = this.PageNumbersSeparatorTemplate;
            }

            if (this.FirstPageTemplate != null)
            {
                pgrSearch.FirstPageTemplate = this.FirstPageTemplate;
            }

            if (this.LastPageTemplate != null)
            {
                pgrSearch.LastPageTemplate = this.LastPageTemplate;
            }

            if (this.PreviousPageTemplate != null)
            {
                pgrSearch.PreviousPageTemplate = this.PreviousPageTemplate;
            }

            if (this.NextPageTemplate != null)
            {
                pgrSearch.NextPageTemplate = this.NextPageTemplate;
            }

            if (this.PreviousGroupTemplate != null)
            {
                pgrSearch.PreviousGroupTemplate = this.PreviousGroupTemplate;
            }

            if (this.NextGroupTemplate != null)
            {
                pgrSearch.NextGroupTemplate = this.NextGroupTemplate;
            }

            if (this.LayoutTemplate != null)
            {
                pgrSearch.LayoutTemplate = this.LayoutTemplate;
            }

            // UniPager template properties
            if (!String.IsNullOrEmpty(PagesTemplateName))
            {
                pgrSearch.PageNumbersTemplate = CMSDataProperties.LoadTransformation(pgrSearch, PagesTemplateName, false);
            }

            if (!String.IsNullOrEmpty(CurrentPageTemplateName))
            {
                pgrSearch.CurrentPageTemplate = CMSDataProperties.LoadTransformation(pgrSearch, CurrentPageTemplateName, false);
            }

            if (!String.IsNullOrEmpty(SeparatorTemplateName))
            {
                pgrSearch.PageNumbersSeparatorTemplate = CMSDataProperties.LoadTransformation(pgrSearch, SeparatorTemplateName, false);
            }

            if (!String.IsNullOrEmpty(FirstPageTemplateName))
            {
                pgrSearch.FirstPageTemplate = CMSDataProperties.LoadTransformation(pgrSearch, FirstPageTemplateName, false);
            }

            if (!String.IsNullOrEmpty(LastPageTemplateName))
            {
                pgrSearch.LastPageTemplate = CMSDataProperties.LoadTransformation(pgrSearch, LastPageTemplateName, false);
            }

            if (!String.IsNullOrEmpty(PreviousPageTemplateName))
            {
                pgrSearch.PreviousPageTemplate = CMSDataProperties.LoadTransformation(pgrSearch, PreviousPageTemplateName, false);
            }

            if (!String.IsNullOrEmpty(NextPageTemplateName))
            {
                pgrSearch.NextPageTemplate = CMSDataProperties.LoadTransformation(pgrSearch, NextPageTemplateName, false);
            }

            if (!String.IsNullOrEmpty(PreviousGroupTemplateName))
            {
                pgrSearch.PreviousGroupTemplate = CMSDataProperties.LoadTransformation(pgrSearch, PreviousGroupTemplateName, false);
            }

            if (!String.IsNullOrEmpty(NextGroupTemplateName))
            {
                pgrSearch.NextGroupTemplate = CMSDataProperties.LoadTransformation(pgrSearch, NextGroupTemplateName, false);
            }

            if (!String.IsNullOrEmpty(LayoutTemplateName))
            {
                pgrSearch.LayoutTemplate = CMSDataProperties.LoadTransformation(pgrSearch, LayoutTemplateName, false);
            }

            #endregion

            // Load transformation
            if (!string.IsNullOrEmpty(TransformationName) && !IgnoreTransformations)
            {
                repSearchResults.ItemTemplate = CMSDataProperties.LoadTransformation(this, TransformationName, false);
            }
            // Set transformation directly
            else
            {
                repSearchResults.ItemTemplate = this.ItemTemplate;
                repSearchResults.HeaderTemplate = this.HeaderTemplate;
                repSearchResults.FooterTemplate = this.FooterTemplate;
                repSearchResults.AlternatingItemTemplate = this.AlternatingItemTemplate;
                repSearchResults.SeparatorTemplate = this.SeparatorTemplate;
            }

            plcBasicRepeater.Controls.Clear();
            repSearchResults.ID = "repSearchResults";
            plcBasicRepeater.Controls.Add(repSearchResults);
        }
    }



    /// <summary>
    ///  On page prerender.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        Search();
        base.OnPreRender(e);
    }


    /// <summary>
    /// Perform search.
    /// </summary>
    protected void Search()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Get query strings 
            string searchText = QueryHelper.GetString("searchtext", "");
            if (!string.IsNullOrEmpty(searchText))
            {
                string searchMode = QueryHelper.GetString("searchMode", "");
                CMS.ISearchEngine.SearchModeEnum searchModeEnum = CMS.ISearchEngine.SearchHelper.GetSearchModeEnum(searchMode);
                int numberOfResults = 0;

                // Get current culture
                string culture = CultureCode;
                if (string.IsNullOrEmpty(culture))
                {
                    culture = ValidationHelper.GetString(ViewState["CultureCode"], CMSContext.PreferredCultureCode);
                }

                // Get default culture
                string defaultCulture = CultureHelper.GetDefaultCulture(CMSContext.CurrentSiteName);

                // Resolve path
                string path = Path;
                if (!string.IsNullOrEmpty(path))
                {
                    path = CMSContext.ResolveCurrentPath(Path);
                }
                
                if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
                {
                    // Log on site keywords
                    AnalyticsHelper.LogOnSiteSearchKeywords(CMSContext.CurrentSiteName, CMSContext.CurrentAliasPath, culture, searchText, 0, 1);                    
                }

                // Prepare search text
                searchText = SearchHelper.CombineSearchCondition(searchText, SearchCondition + FilterSearchCondition, searchModeEnum, SearchOptions, DocumentTypes, culture, defaultCulture, CombineWithDefaultCulture);

                // Get positions and ranges for search method
                int startPosition = 0;
                int numberOfProceeded = 100;
                int displayResults = 100;
                if (pgrSearch.PageSize != 0 && pgrSearch.GroupSize != 0)
                {
                    startPosition = (pgrSearch.CurrentPage - 1) * pgrSearch.PageSize;
                    numberOfProceeded = (((pgrSearch.CurrentPage / pgrSearch.GroupSize) + 1) * pgrSearch.PageSize * pgrSearch.GroupSize) + pgrSearch.PageSize;
                    displayResults = pgrSearch.PageSize;
                }

                if ((this.MaxResults > 0) && (numberOfProceeded > this.MaxResults))
                {
                    numberOfProceeded = this.MaxResults;
                }

                // Combine regular search sort with filter sort
                string srt = ValidationHelper.GetString(SearchSort, String.Empty).Trim();
                string filterSrt = ValidationHelper.GetString(FilterSearchSort, String.Empty).Trim();

                if (!String.IsNullOrEmpty(filterSrt))
                {
                    if (!String.IsNullOrEmpty(srt))
                    {
                        srt += ", ";
                    }

                    srt += filterSrt;
                }

                // Search
                DataSet results = SearchHelper.Search(searchText, SearchHelper.GetSort(srt), path, DocumentTypes, culture, defaultCulture, CombineWithDefaultCulture, CheckPermissions, SearchInAttachments, Indexes, displayResults, startPosition, numberOfProceeded, (UserInfo)CMSContext.CurrentUser, out numberOfResults, AttachmentsWhere, AttachmentsOrderBy);

                if ((this.MaxResults > 0) && (numberOfResults > MaxResults))
                {
                    numberOfResults = MaxResults;
                }

                // Fill repeater with results
                repSearchResults.DataSource = results;
                repSearchResults.PagerForceNumberOfResults = numberOfResults;
                repSearchResults.DataBind();

                // Show now results found ?
                if (numberOfResults == 0)
                {
                    lblNoResults.Text = NoResultsText;
                    lblNoResults.Visible = true;
                }
            }
            else
            {
                Visible = false;
            }

            // Invoke search completed event
            if (OnSearchCompleted != null)
            {
                OnSearchCompleted(Visible);
            }
        }
    }


    /// <summary>
    /// Applies search filter.
    /// </summary>
    /// <param name="searchCondition">Search condition</param>
    /// <param name="searchSort">Search sort</param>
    public void ApplyFilter(string searchCondition, string searchSort)
    {
        FilterSearchCondition += " " + searchCondition;
        FilterSearchSort += " " + searchSort;
    }


    /// <summary>
    /// Adds filter option to url.
    /// </summary>
    /// <param name="searchWebpartID">Webpart id</param>
    /// <param name="options">Options</param>
    public void AddFilterOptionsToUrl(string searchWebpartID, string options)
    {
        // Do nothing here
    }


    /// <summary>
    /// Loads data.
    /// </summary>
    public void LoadData()
    {
        // Register control for filter
        CMSControlsHelper.SetFilter(FilterID, this);
    }

}
