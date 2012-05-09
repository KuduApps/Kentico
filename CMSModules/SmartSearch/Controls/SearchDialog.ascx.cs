using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.ISearchEngine;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.SiteProvider;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.PortalEngine;

using SearchHelper = CMS.ISearchEngine.SearchHelper;

public partial class CMSModules_SmartSearch_Controls_SearchDialog : CMSUserControl, ISearchFilterable
{
    #region "Variables"

    private string mSearchForLabel = ResHelper.GetString("srch.dialog.searchfor");
    private string mSearchButton = ResHelper.GetString("general.search");
    private string mSearchModeLabel = ResHelper.GetString("srch.dialog.mode");
    private SearchModeEnum mSearchMode = SearchModeEnum.AnyWord;
    private bool mShowSearchMode = true;
    private string mFilterID = "";
    private string mResultWebpartID = "";

    // Filter support
    List<string> mFilterUrlParameters = null;

    #endregion


    #region "Properties"


    /// <summary>
    /// 
    /// </summary>
    public List<string> FilterUrlParameters
    {
        get
        {
            if (mFilterUrlParameters == null)
            {
                mFilterUrlParameters = new List<string>();
            }

            return mFilterUrlParameters;
        }
        set
        {
            mFilterUrlParameters = value;
        }
    }


    /// <summary>
    /// Gets or sets the label search for text.
    /// </summary>
    public string SearchForLabel
    {
        get
        {
            return mSearchForLabel;
        }
        set
        {
            mSearchForLabel = value;
            lblSearchFor.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether search mode settings should be displayed.
    /// </summary>
    public bool ShowSearchMode
    {
        get
        {
            return mShowSearchMode;
        }
        set
        {
            mShowSearchMode = value;
            plcSearchMode.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the search button text.
    /// </summary>
    public string SearchButton
    {
        get
        {
            return mSearchButton;
        }
        set
        {
            mSearchButton = value;
            btnSearch.Text = value;
        }
    }


    /// <summary>
    ///  Gets or sets the search mode.
    /// </summary>
    public SearchModeEnum SearchMode
    {
        get
        {
            return mSearchMode;
        }
        set
        {
            mSearchMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the search mode label text.
    /// </summary>
    public string SearchModeLabel
    {
        get
        {
            return mSearchModeLabel;
        }
        set
        {
            mSearchModeLabel = value;
            lblSearchMode.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the search filter id.
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
    /// Gets or sets the search mode label text.
    /// </summary>
    public string ResultWebpartID
    {
        get
        {
            return mResultWebpartID;
        }
        set
        {
            mResultWebpartID = value;
        }
    }


    /// <summary>
    /// Gets or sets filter condition - not implemented.
    /// </summary>
    public string FilterSearchCondition
    {
        get
        {
            return null;
        }
        set
        {
            ;
        }
    }

    /// <summary>
    /// Gets or sets filter search sort - not implemented.
    /// </summary>
    public string FilterSearchSort
    {
        get
        {
            return null;
        }
        set
        {
            ;
        }
    }

    #endregion


    #region "Page and controls events"

    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }
        else
        {
            // Set up drop down list
            if (ShowSearchMode)
            {
                if (!RequestHelper.IsPostBack())
                {
                    // Fill dropdownlist option with values
                    DataHelper.FillListControlWithEnum(typeof(SearchModeEnum), drpSearchMode, "srch.dialog.", SearchHelper.GetSearchModeString);
                    drpSearchMode.SelectedValue = QueryHelper.GetString("searchmode", SearchHelper.GetSearchModeString(SearchMode));
                }

            }

            // Set up search text  
            if (!RequestHelper.IsPostBack())
            {
                txtSearchFor.Text = QueryHelper.GetString("searchtext", "");
            }
        }
    }


    /// <summary>
    /// Fires at btn search click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string url = URLHelper.CurrentURL;

        // Remove pager query string
        url = URLHelper.RemoveParameterFromUrl(url, "page");

        // Update search text parameter
        url = URLHelper.UpdateParameterInUrl(url, "searchtext", HttpUtility.UrlEncode(txtSearchFor.Text));

        // Update search mode parameter
        url = URLHelper.RemoveParameterFromUrl(url, "searchmode");
        if (this.ShowSearchMode)
        {
            url = URLHelper.AddParameterToUrl(url, "searchmode", drpSearchMode.SelectedValue);
        }
        else
        {
            url = URLHelper.AddParameterToUrl(url, "searchmode", SearchHelper.GetSearchModeString(SearchMode));
        }

        // Add filter params to url
        foreach (string urlParam in FilterUrlParameters)
        {
            string[] urlParams = urlParam.Split('=');
            url = URLHelper.UpdateParameterInUrl(url, urlParams[0], urlParams[1]);
        }

        // Log "internal search" activity
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser)
            && ActivitySettingsHelper.SearchEnabled(siteName))
        {
            ActivityLogProvider.LogInternalSearchActivity(CMSContext.CurrentDocument, ModuleCommands.OnlineMarketingGetCurrentContactID(),
                CMSContext.CurrentSiteID, URLHelper.CurrentRelativePath, txtSearchFor.Text, CMSContext.Campaign);
        }

        // Redirect
        URLHelper.Redirect(url);
    }

    #endregion


    #region "Methods"


    /// <summary>
    /// Applies filter.
    /// </summary>
    /// <param name="searchCondition">Search condition</param>
    /// <param name="searchSort">Search sort</param>
    public void ApplyFilter(string searchCondition, string searchSort)
    {
        // Call Result webpart id
        ISearchFilterable resultWebpart = (ISearchFilterable)CMSControlsHelper.GetFilter(ResultWebpartID);
        if (resultWebpart != null)
        {
            resultWebpart.ApplyFilter(searchCondition, searchSort);
        }
    }


    /// <summary>
    /// Adds filter option to url.
    /// </summary>
    /// <param name="searchWebpartID">Webpart id</param>
    /// <param name="options">Options</param>
    public void AddFilterOptionsToUrl(string searchWebpartID, string options)
    {
        FilterUrlParameters.Add(searchWebpartID + "=" + options);
    }


    /// <summary>
    /// Loads data.
    /// </summary>
    public void LoadData()
    {
        // Register control for filter
        CMSControlsHelper.SetFilter(FilterID, this);
    }

    #endregion
}
