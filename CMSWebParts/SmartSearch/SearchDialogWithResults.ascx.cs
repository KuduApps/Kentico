using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.Controls;
using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.ISearchEngine;

public partial class CMSWebParts_SmartSearch_SearchDialogWithResults : CMSAbstractWebPart
{
    #region "Variables"

    private string mFilterSuffix = "diarespsfx";

    #endregion


    #region "Dialog properties"

    /// <summary>
    /// Gets or sets the label search for text.
    /// </summary>
    public string SearchForLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchForLabel"), this.srchDialog.SearchForLabel);
        }
        set
        {
            this.SetValue("SearchForLabel", value);
            this.srchDialog.SearchForLabel = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether search mode settings should be displayed.
    /// </summary>
    public bool ShowSearchMode
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSearchMode"), this.srchDialog.ShowSearchMode);
        }
        set
        {
            this.SetValue("ShowSearchMode", value);
            this.srchDialog.ShowSearchMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the search button text.
    /// </summary>
    public string SearchButton
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchButton"), this.srchDialog.SearchButton);
        }
        set
        {
            this.SetValue("SearchButton", value);
            this.srchDialog.SearchButton = value;
        }
    }


    /// <summary>
    ///  Gets or sets the search mode.
    /// </summary>
    public SearchModeEnum SearchMode
    {
        get
        {
            return SearchHelper.GetSearchModeEnum(DataHelper.GetNotEmpty(this.GetValue("SearchMode"), SearchHelper.GetSearchModeString(this.srchDialog.SearchMode)));
        }
        set
        {
            this.SetValue("SearchMode", SearchHelper.GetSearchModeString(value));
            this.srchDialog.SearchMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the search mode label text.
    /// </summary>
    public string SearchModeLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchModeLabel"), this.srchDialog.SearchModeLabel);
        }
        set
        {
            this.SetValue("SearchModeLabel", value);
            this.srchDialog.SearchModeLabel = value;
        }
    }

    #endregion


    #region "Result properties"

    /// <summary>
    /// Gets or sets sorting of search.
    /// </summary>
    public string SearchSort
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SearchSort"), srchResults.SearchSort);
        }
        set
        {
            this.SetValue("SearchSort", value);
            srchResults.SearchSort = value;
        }
    }


    /// <summary>
    /// Gets or sets sorting of search.
    /// </summary>
    public string SearchCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SearchCondition"), srchResults.SearchCondition);
        }
        set
        {
            this.SetValue("SearchCondition", value);
            srchResults.SearchCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets indexes.
    /// </summary>
    public string Indexes
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Indexes"), srchResults.Indexes);
        }
        set
        {
            this.SetValue("Indexes", value);
            srchResults.Indexes = value;
        }
    }


    /// <summary>
    /// Gets or sets path.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Path"), srchResults.Path);
        }
        set
        {
            this.SetValue("Path", value);
            srchResults.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets document types.
    /// </summary>
    public string DocumentTypes
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DocumentTypes"), srchResults.DocumentTypes);
        }
        set
        {
            this.SetValue("DocumentTypes", value);
            srchResults.DocumentTypes = value;
        }
    }


    /// <summary>
    /// Gets or sets check permissions.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), srchResults.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            srchResults.CheckPermissions = false;
        }
    }


    /// <summary>
    /// Gets or sets search option.
    /// </summary>
    public CMS.SiteProvider.SearchOptionsEnum SearchOptions
    {
        get
        {
            return CMS.SiteProvider.SearchHelper.GetSearchOptionsEnum(ValidationHelper.GetString(this.GetValue("SearchOptions"), CMS.SiteProvider.SearchHelper.GetSearchOptionsString(srchResults.SearchOptions)));
        }
        set
        {
            this.SetValue("SearchOptions", CMS.SiteProvider.SearchHelper.GetSearchOptionsString(value));
            srchResults.SearchOptions = value;
        }
    }


    /// <summary>
    /// Gets or sets transformation name.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), srchResults.TransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
            srchResults.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CultureCode"), srchResults.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            srchResults.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets combine with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), srchResults.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            srchResults.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets search in attachments.
    /// </summary>
    public bool SearchInAttachments
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SearchInAttachments"), srchResults.SearchInAttachments);
        }
        set
        {
            this.SetValue("SearchInAttachments", value);
            srchResults.SearchInAttachments = value;
        }
    }


    /// <summary>
    /// Gets or sets attachments where.
    /// </summary>
    public string AttachmentsWhere
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AttachmentsWhere"), srchResults.AttachmentsWhere);
        }
        set
        {
            this.SetValue("AttachmentsWhere", value);
            srchResults.AttachmentsWhere = value;
        }
    }


    /// <summary>
    /// Gets or sets attachment order by.
    /// </summary>
    public string AttachmentsOrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AttachmentsOrderBy"), srchResults.AttachmentsOrderBy);
        }
        set
        {
            this.SetValue("AttachmentsOrderBy", value);
            srchResults.AttachmentsOrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets results text.
    /// </summary>
    public string NoResultsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NoResultsText"), srchResults.NoResultsText);
        }
        set
        {
            this.SetValue("NoResultsText", value);
            srchResults.NoResultsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal displayed results.
    /// </summary>
    public int MaxResults
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxResults"), srchResults.MaxResults);
        }
        set
        {
            this.SetValue("MaxResults", value);
            srchResults.MaxResults = value;
        }
    }

    #endregion


    #region "UniPager properties"

    /// <summary>
    /// Gets or sets page size.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), srchResults.PageSize);
        }
        set
        {
            this.SetValue("PageSize", value);
            srchResults.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets search option.
    /// </summary>
    public string PagingMode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PagingMode"), srchResults.PagingMode.ToString());
        }
        set
        {
            SetValue("PagingMode", value);

            srchResults.PagingMode = UniPagerMode.Querystring;
            if (value == "postback")
            {
                srchResults.PagingMode = UniPagerMode.PostBack;
            }
        }
    }


    /// <summary>
    /// Gets or sets query string key.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("QueryStringKey"), srchResults.QueryStringKey);
        }
        set
        {
            this.SetValue("QueryStringKey", value);
            srchResults.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets group size.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("GroupSize"), srchResults.PageSize);
        }
        set
        {
            this.SetValue("GroupSize", value);
            srchResults.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayFirstLastAutomatically"), srchResults.DisplayFirstLastAutomatically);
        }
        set
        {
            SetValue("DisplayFirstLastAutomatically", value);
            srchResults.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayPreviousNextAutomatically"), srchResults.DisplayPreviousNextAutomatically);
        }
        set
        {
            SetValue("DisplayPreviousNextAutomatically", value);
            srchResults.DisplayPreviousNextAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HidePagerForSinglePage"), srchResults.HidePagerForSinglePage);
        }
        set
        {
            SetValue("HidePagerForSinglePage", value);
            srchResults.HidePagerForSinglePage = value;
        }
    }


    /// <summary>
    /// Gets or sets the max. pages.
    /// </summary>
    public int MaxPages
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxPages"), srchResults.MaxPages);
        }
        set
        {
            this.SetValue("MaxPages", value);
            srchResults.MaxPages = value;
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
            return ValidationHelper.GetString(GetValue("Pages"), srchResults.PagesTemplateName);
        }
        set
        {
            SetValue("Pages", value);
            srchResults.PagesTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the current page template.
    /// </summary>
    public string CurrentPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CurrentPage"), srchResults.CurrentPageTemplateName);
        }
        set
        {
            SetValue("CurrentPage", value);
            srchResults.CurrentPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the separator template.
    /// </summary>
    public string SeparatorTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PageSeparator"), srchResults.SeparatorTemplateName);
        }
        set
        {
            SetValue("PageSeparator", value);
            srchResults.SeparatorTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the first page template.
    /// </summary>
    public string FirstPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FirstPage"), srchResults.FirstPageTemplateName);
        }
        set
        {
            SetValue("FirstPage", value);
            srchResults.FirstPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the last page template.
    /// </summary>
    public string LastPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LastPage"), srchResults.LastPageTemplateName);
        }
        set
        {
            SetValue("LastPage", value);
            srchResults.LastPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous page template.
    /// </summary>
    public string PreviousPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousPage"), srchResults.PreviousPageTemplateName);
        }
        set
        {
            SetValue("PreviousPage", value);
            srchResults.PreviousPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the next page template.
    /// </summary>
    public string NextPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextPage"), srchResults.NextPageTemplateName);
        }
        set
        {
            SetValue("NextPage", value);
            srchResults.NextPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous group template.
    /// </summary>
    public string PreviousGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousGroup"), srchResults.PreviousGroupTemplateName);
        }
        set
        {
            SetValue("PreviousGroup", value);
            srchResults.NextPageTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the next group template.
    /// </summary>
    public string NextGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextGroup"), srchResults.NextGroupTemplateName);
        }
        set
        {
            SetValue("NextGroup", value);
            srchResults.NextGroupTemplateName = value;
        }
    }


    /// <summary>
    /// Gets or sets the layout template.
    /// </summary>
    public string LayoutTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagerLayout"), srchResults.LayoutTemplateName);
        }
        set
        {
            SetValue("PagerLayout", value);
            srchResults.LayoutTemplateName = value;
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
        if (this.StopProcessing)
        {
            // Do nothing
            srchDialog.StopProcessing = true;
            srchResults.StopProcessing = true;

        }
        else
        {
            string webpartID = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);

            // Set settings to search dialog
            srchDialog.SearchForLabel = this.SearchForLabel;
            srchDialog.SearchModeLabel = this.SearchModeLabel;
            srchDialog.SearchButton = this.SearchButton;

            srchDialog.SearchMode = this.SearchMode;
            srchDialog.ShowSearchMode = this.ShowSearchMode;
            srchDialog.FilterID = webpartID;
            srchDialog.ResultWebpartID = webpartID + mFilterSuffix;
            srchDialog.LoadData();

            // Get unipage mode
            UniPagerMode mode = UniPagerMode.Querystring;
            if (PagingMode == "postback")
            {
                mode = UniPagerMode.PostBack;
            }

            // Search results properties
            srchResults.SearchSort = SearchSort;
            srchResults.Indexes = Indexes;
            srchResults.Path = Path;
            srchResults.DocumentTypes = DocumentTypes;
            srchResults.CheckPermissions = CheckPermissions;
            srchResults.SearchOptions = SearchOptions;
            srchResults.TransformationName = TransformationName;
            srchResults.CultureCode = CultureCode;
            srchResults.CombineWithDefaultCulture = CombineWithDefaultCulture;
            srchResults.SearchInAttachments = SearchInAttachments;
            srchResults.AttachmentsOrderBy = AttachmentsOrderBy;
            srchResults.AttachmentsWhere = AttachmentsWhere;
            srchResults.NoResultsText = NoResultsText;
            srchResults.FilterID = webpartID + mFilterSuffix;
            srchResults.SearchCondition = SearchCondition;
            srchResults.LoadData();

            // UniPager properties
            srchResults.PageSize = PageSize;
            srchResults.GroupSize = GroupSize;
            srchResults.QueryStringKey = QueryStringKey;
            srchResults.DisplayFirstLastAutomatically = DisplayFirstLastAutomatically;
            srchResults.DisplayPreviousNextAutomatically = DisplayPreviousNextAutomatically;
            srchResults.HidePagerForSinglePage = HidePagerForSinglePage;
            srchResults.PagingMode = mode;
            srchResults.MaxPages = this.MaxPages;
            srchResults.MaxResults = this.MaxResults;

            // Unipager template properties
            srchResults.PagesTemplateName = PagesTemplate;
            srchResults.CurrentPageTemplateName = CurrentPageTemplate;
            srchResults.SeparatorTemplateName = SeparatorTemplate;
            srchResults.FirstPageTemplateName = FirstPageTemplate;
            srchResults.LastPageTemplateName = LastPageTemplate;
            srchResults.PreviousPageTemplateName = PreviousPageTemplate;
            srchResults.NextPageTemplateName = NextPageTemplate;
            srchResults.PreviousGroupTemplateName = PreviousGroupTemplate;
            srchResults.NextGroupTemplateName = NextGroupTemplate;
            srchResults.LayoutTemplateName = LayoutTemplate;
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        base.ReloadData();
    }

    #endregion

}
