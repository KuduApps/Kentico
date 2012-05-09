using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.ComponentModel;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.Controls;
using CMS.SettingsProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Content_CMSDesk_Search_default : CMSContentPage, ITimeZoneManager
{
    #region "Variables"

    private string searchtext = null;
    private string searchmode = null;
    private bool searchpublished = true;
    private string searchindex = null;
    private string searchlanguage = null;
    private string searchculture = null;
    private bool timeZoneLoaded = false;
    private TimeZoneInfo usedTimeZone = null;
    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("Content.SearchTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Search/Search.png");

        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "contentsearch_tab";

        if (!RequestHelper.IsPostBack())
        {
            InitializeVariables();
            SetRepeaters();
        }
        else
        {
            repSearchSQL.Visible = false;
            repSearchSQL.StopProcessing = true;
            repSmartSearch.Visible = false;
            repSmartSearch.StopProcessing = true;
        }
    }


    private void InitializeVariables()
    {
        searchtext = HttpUtility.UrlDecode(QueryHelper.GetString("searchtext", null));
        searchmode = QueryHelper.GetString("searchmode", null);
        searchpublished = QueryHelper.GetBoolean("searchpublished", true);
        searchindex = QueryHelper.GetString("searchindex", null);
        searchlanguage = QueryHelper.GetString("searchlanguage", null);
        searchculture = QueryHelper.GetString("searchculture", null);
    }


    /// <summary>
    /// Sets repeaters.
    /// </summary>
    private void SetRepeaters()
    {
        // Display SQL results
        if (searchindex == "##SQL##")
        {
            repSearchSQL.Visible = true;
            repSearchSQL.StopProcessing = false;
            repSmartSearch.Visible = false;
            repSmartSearch.StopProcessing = true;

            repSearchSQL.SelectOnlyPublished = QueryHelper.GetBoolean("searchpublished", true);

            string culture = QueryHelper.GetString("searchculture", "##ANY##");
            string mode = QueryHelper.GetString("searchlanguage", null);
            if ((culture == "##ANY##") || (mode == "<>"))
            {
                culture = TreeProvider.ALL_CULTURES;
            }
            else
            {
                repSearchSQL.CombineWithDefaultCulture = false;
            }
            repSearchSQL.WhereCondition = searchDialog.WhereCondition;
            repSearchSQL.CultureCode = culture;
            repSearchSQL.TransformationName = "CMS.Root.CMSDeskSQLSearchResults";
        }
        // Display Smart search results
        else
        {
            repSearchSQL.Visible = false;
            repSearchSQL.StopProcessing = true;
            repSmartSearch.Visible = true;
            repSmartSearch.StopProcessing = false;
            repSmartSearch.Indexes = searchindex;
            repSmartSearch.TransformationName = "CMS.Root.CMSDeskSmartSearchResults";
            repSmartSearch.PageSize = 10;
            repSmartSearch.PagingMode = UniPagerMode.Querystring;
            repSmartSearch.HidePagerForSinglePage = true;
            repSmartSearch.PagesTemplateName = "CMS.PagerTransformations.General-Pages";
            repSmartSearch.CurrentPageTemplateName = "CMS.PagerTransformations.General-CurrentPage";
            repSmartSearch.SeparatorTemplateName = "CMS.PagerTransformations.General-PageSeparator";
            repSmartSearch.FirstPageTemplateName = "CMS.PagerTransformations.General-FirstPage";
            repSmartSearch.LastPageTemplateName = "CMS.PagerTransformations.General-LastPage";
            repSmartSearch.PreviousPageTemplateName = "CMS.PagerTransformations.General-PreviousPage";
            repSmartSearch.NextPageTemplateName = "CMS.PagerTransformations.General-NextPage";
            repSmartSearch.PreviousGroupTemplateName = "CMS.PagerTransformations.General-PreviousGroup";
            repSmartSearch.NextGroupTemplateName = "CMS.PagerTransformations.General-NextGroup";
            repSmartSearch.LayoutTemplateName = "CMS.PagerTransformations.General-PagerLayout";

            string culture = QueryHelper.GetString("searchculture", "##ANY##");
            if (culture == "##ANY##")
            {
                culture = "##all##";
            }
            repSmartSearch.CultureCode = culture;
        }
    }


    /// <summary>
    /// Returns column value for current search result item.
    /// </summary>
    /// <param name="columnName">Column</param>
    public object GetSearchValue(string columnName)
    {
        // Get id of the current row
        string id = ValidationHelper.GetString(Eval("id"), String.Empty);
        // Get Datarows for current results
        Hashtable resultRows = RequestStockHelper.GetItem(SearchHelper.RESULTS_KEY) as Hashtable;

        // Check whether id and datarow collection exists
        if ((id != String.Empty) && (resultRows != null))
        {
            // Get current datarow
            DataRow dr = resultRows[id] as DataRow;

            // Check whether datarow exists and contains required column
            if ((dr != null) && (dr.Table.Columns.Contains(columnName)))
            {
                // Return column value
                return dr[columnName];
            }
        }

        // Return nothing by default
        return null;
    }

    #endregion


    #region "ITimeZoneManager Members"

    /// <summary>
    /// Gets time zone type for this page.
    /// </summary>
    public TimeZoneTypeEnum TimeZoneType
    {
        get
        {
            return TimeZoneTypeEnum.Custom;
        }
    }


    /// <summary>
    /// Gets current time zone for UI.
    /// </summary>
    public TimeZoneInfo CustomTimeZone
    {
        get
        {
            // Get time zone for first request only
            if (!timeZoneLoaded)
            {
                TimeZoneHelper.GetCurrentTimeZoneDateTimeString(DateTime.Now, CMSContext.CurrentUser, CMSContext.CurrentSite, out usedTimeZone);
                timeZoneLoaded = true;
            }
            return usedTimeZone;
        }
    }

    #endregion
}
