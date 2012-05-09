using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.SiteProvider;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

using ISearchHelper = CMS.ISearchEngine.SearchHelper;

public partial class CMSModules_SmartSearch_SearchIndex_Search : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            // Fill dropdownlist option with values
            DataHelper.FillListControlWithEnum(typeof(CMS.ISearchEngine.SearchModeEnum), drpSearchMode, "srch.dialog.", ISearchHelper.GetSearchModeString);
            drpSearchMode.SelectedValue = QueryHelper.GetString("searchmode", ISearchHelper.GetSearchModeString(CMS.ISearchEngine.SearchModeEnum.AnyWord));

            // Set up search text  
            txtSearchFor.Text = QueryHelper.GetString("searchtext", "");
        }
    }


    /// <summary>
    /// Search button click handler.
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string url = URLHelper.CurrentURL;

        // Remove pager query string
        url = URLHelper.RemoveParameterFromUrl(url, "page");

        // Update search text parameter
        url = URLHelper.UpdateParameterInUrl(url, "searchtext", HttpUtility.UrlEncode(txtSearchFor.Text));

        // Update search mode parameter
        url = URLHelper.UpdateParameterInUrl(url, "searchmode", HttpUtility.UrlEncode(drpSearchMode.SelectedValue));

        // Redirect
        URLHelper.Redirect(url);
    }


    /// <summary>
    /// Perform search.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Check whether current request is not postback
        if (!RequestHelper.IsPostBack())
        {
            // Get current search text from query string
            string searchText = QueryHelper.GetString("searchtext", "");
            // Check whether search text is defined
            if (!string.IsNullOrEmpty(searchText))
            {
                // Get current search mode from query string
                string searchMode = QueryHelper.GetString("searchmode", "");
                CMS.ISearchEngine.SearchModeEnum searchModeEnum = ISearchHelper.GetSearchModeEnum(searchMode);
                
                // Get current index id
                int indexId = QueryHelper.GetInteger("indexId", 0);
                // Get current index info object
                SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(indexId);
                // Check whether index info exists
                if (sii != null)
                {
                    // Keep search text in search textbox
                    //txtSearchFor.Text = searchText;

                    searchText = SearchHelper.CombineSearchCondition(searchText, null, searchModeEnum, SearchOptionsEnum.FullSearch, null, null, null, false);

                    // Get positions and ranges for search method
                    int startPosition = 0;
                    int numberOfProceeded = 100;
                    int displayResults = 100;
                    int numberOfResults = 0;
                    if (pgrSearch.PageSize != 0 && pgrSearch.GroupSize != 0)
                    {
                        startPosition = (pgrSearch.CurrentPage - 1) * pgrSearch.PageSize;
                        numberOfProceeded = (((pgrSearch.CurrentPage / pgrSearch.GroupSize) + 1) * pgrSearch.PageSize * pgrSearch.GroupSize) + pgrSearch.PageSize;
                        displayResults = pgrSearch.PageSize;
                    }
                    
                    // Search
                    DataSet results = SearchHelper.Search(searchText, null, null, null, "##ALL##", null, false, false, false, sii.IndexName, displayResults, startPosition, numberOfProceeded, (UserInfo)CMSContext.CurrentUser, out numberOfResults, null, null);

                    // Fill repeater with results
                    repSearchResults.DataSource = results;
                    repSearchResults.PagerForceNumberOfResults = numberOfResults;
                    repSearchResults.DataBind();

                    // Show now results found ?
                    if (numberOfResults == 0)
                    {
                        lblNoResults.Text = "<br />" + GetString("srch.results.noresults");
                        lblNoResults.Visible = true;
                    }
                }
            }
        }

        base.OnPreRender(e);
    }
}
