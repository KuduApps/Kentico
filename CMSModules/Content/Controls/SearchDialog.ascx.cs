using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.UIControls;
using CMS.ISearchEngine;

using SearchHelper = CMS.ISearchEngine.SearchHelper;

public partial class CMSModules_Content_Controls_SearchDialog : CMSUserControl
{
    #region "Variables"

    private string mSearchControlID = null;
    private string mDefaultSiteCulture = null;
    private string currentSiteName = null;
    private int? mSiteCulturesCount = null;

    public const string SQL = "##SQL##";
    public const string ANY = "##ANY##";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets search webpart ID.
    /// </summary>
    public string SearchControlID
    {
        get
        {
            return mSearchControlID;
        }
        set
        {
            mSearchControlID = value;
        }
    }


    /// <summary>
    /// Site cultures count.
    /// </summary>
    private int SiteCulturesCount
    {
        get
        {
            if (mSiteCulturesCount == null)
            {
                DataSet dsCultures = CultureInfoProvider.GetSiteCultures(currentSiteName);
                if (!DataHelper.DataSourceIsEmpty(dsCultures))
                {
                    mSiteCulturesCount = dsCultures.Tables[0].Rows.Count;
                }
                else
                {
                    mSiteCulturesCount = 0;
                }
            }
            return mSiteCulturesCount.Value;
        }
    }


    /// <summary>
    /// Default culture of the site.
    /// </summary>
    private string DefaultSiteCulture
    {
        get
        {
            if (mDefaultSiteCulture == null)
            {
                mDefaultSiteCulture = CultureHelper.GetDefaultCulture(currentSiteName);
            }
            return mDefaultSiteCulture;
        }
    }


    /// <summary>
    /// Gets selected index.
    /// </summary>
    public string SelectedIndex
    {
        get
        {
            return drpIndexes.SelectedValue;
        }
    }


    /// <summary>
    /// Gets WHERE condition for SQL search.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return GetWhere();
        }
    }

    #endregion


    #region "Methods"


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        currentSiteName = CMSContext.CurrentSiteName;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvText.ErrorMessage = GetString("general.requiresvalue");

        drpIndexes.AutoPostBack = true;
        drpIndexes.SelectedIndexChanged += new EventHandler(drpIndexes_SelectedIndexChanged);
        btnSearch.Click += new EventHandler(btnSearch_Click);

        // Init cultures
        cultureElem.DropDownCultures.CssClass = "ContentDropdown";
        cultureElem.AddDefaultRecord = false;
        cultureElem.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline;

        if (!RequestHelper.IsPostBack())
        {
            LoadDropDowns();
            SetControls();
            SetCulture();
        }
        else
        {
            SetCulture();
        }
    }


    /// <summary>
    /// OnPreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Hide column with languages if only one culture is assigned to the site
        if (SiteCulturesCount <= 1)
        {
            plcLang.Visible = false;
        }
    }


    /// <summary>
    /// Sets WHERE condition.
    /// </summary>
    private string GetWhere()
    {
        string where = null;
        string val = QueryHelper.GetString("searchculture", "##ANY##");
        string mode = QueryHelper.GetString("searchlanguage", null);
        string query = " (NodeID IN (SELECT NodeID FROM View_CMS_Tree_Joined GROUP BY NodeID HAVING (COUNT(NodeID) {0} {1}))) ";

        if (val == "")
        {
            val = "##ANY##";
        }

        // If culture IS
        if (mode == "=")
        {
            // document IS in ALL cultures
            if (val == "##ALL##")
            {
                where = String.Format(query, mode, SiteCulturesCount);
            }
        }
        // If culture IS NOT
        else if (mode == "<>")
        {
            switch (val)
            {
                // document IS NOT in ALL cultures
                case "##ALL##":
                    where = String.Format(query, mode, SiteCulturesCount);
                    break;

                // document IS NOT in ANY culture is always empty result
                case "##ANY##":
                    where = SqlHelperClass.NO_DATA_WHERE;
                    break;

                // document IS NOT in one specific culture
                default:
                    where = " (DocumentCulture <> '" + SqlHelperClass.GetSafeQueryString(val, false) + "')";
                    break;
            }
        }
        return where;
    }


    /// <summary>
    /// Sets controls from query string parameters.
    /// </summary>
    private void SetControls()
    {
        drpLanguage.SelectedValue = QueryHelper.GetString("searchlanguage", "=");
        cultureElem.Value = QueryHelper.GetString("searchculture", ANY);
        cultureElem.ReloadData();
        cultureElem.Reload(true);
        drpIndexes.SelectedValue = QueryHelper.GetString("searchindex", SQL);
        chkOnlyPublished.Checked = QueryHelper.GetBoolean("searchpublished", true);
        if (drpIndexes.SelectedValue != SQL)
        {
            plcPublished.Visible = false;
        }
        drpSearchMode.SelectedValue = QueryHelper.GetString("searchmode", SearchHelper.GetSearchModeString(SearchModeEnum.AnyWord));
        txtSearchFor.Text = QueryHelper.GetString("searchtext", null);
    }


    private void SetCulture()
    {
        string selectedCulture = cultureElem.Value.ToString();
        string selected = drpIndexes.SelectedValue;
        if (String.IsNullOrEmpty(selected))
        {
            selected = SQL;
        }

        if (selected != SQL)
        {
            cultureElem.SpecialFields = new string[,] { { GetString("transman.anyculture"), ANY } };
            drpLanguage.Visible = false;
            cultureElem.DropDownCultures.Width = Unit.Pixel(295);
        }
        else
        {
            cultureElem.SpecialFields = new string[,] { { GetString("transman.anyculture"), ANY }, { GetString("transman.allcultures"), "##ALL##" } };
            drpLanguage.Visible = true;
            cultureElem.DropDownCultures.Width = Unit.Pixel(148);
        }

        cultureElem.Value = selectedCulture;
        cultureElem.ReloadData();
        cultureElem.Reload(true);
    }


    /// <summary>
    /// Loads drop-down lists.
    /// </summary>
    private void LoadDropDowns()
    {
        // Init operands
        if (drpLanguage.Items.Count == 0)
        {
            drpLanguage.Items.Add(new ListItem(GetString("transman.translatedto"), "="));
            drpLanguage.Items.Add(new ListItem(GetString("transman.nottranslatedto"), "<>"));
        }

        // Get site indexes
        DataSet ds = SearchIndexSiteInfoProvider.GetIndexSites("IndexId", "IndexSiteID = " + CMSContext.CurrentSiteID + " AND IndexID IN (SELECT IndexID FROM CMS_SearchIndex WHERE IndexType = N'" + PredefinedObjectType.DOCUMENT + "')", null, 0);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(ValidationHelper.GetInteger(dr["IndexId"], 0));
                if ((sii != null) && (sii.IndexType == PredefinedObjectType.DOCUMENT))
                {
                    drpIndexes.Items.Add(new ListItem(sii.IndexDisplayName, sii.IndexName));
                }
            }
        }
        drpIndexes.Items.Insert(0, new ListItem(GetString("search.sqlsearch"), SQL));

        // Init Search for drop down list
        DataHelper.FillListControlWithEnum(typeof(SearchModeEnum), drpSearchMode, "srch.dialog.", SearchHelper.GetSearchModeString);
        drpSearchMode.SelectedValue = QueryHelper.GetString("searchmode", SearchHelper.GetSearchModeString(SearchModeEnum.AnyWord));
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Index changed.
    /// </summary>
    void drpIndexes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpIndexes.SelectedValue == SQL)
        {
            plcPublished.Visible = true;
        }
        else
        {
            plcPublished.Visible = false;
        }
        SetCulture();
    }


    /// <summary>
    /// Search button click.
    /// </summary>
    void btnSearch_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtSearchFor.Text.Trim()))
        {
            string url = URLHelper.CurrentURL;

            // Remove pager query string
            url = URLHelper.RemoveParameterFromUrl(url, "page");

            // Update search text parameter
            url = URLHelper.UpdateParameterInUrl(url, "searchtext", HttpUtility.UrlEncode(txtSearchFor.Text));

            // Update search mode parameter
            url = URLHelper.UpdateParameterInUrl(url, "searchmode", HttpUtility.UrlEncode(drpSearchMode.SelectedValue));

            // Update search for published items
            if (chkOnlyPublished.Visible)
            {
                url = URLHelper.UpdateParameterInUrl(url, "searchpublished", chkOnlyPublished.Checked.ToString());
            }

            // Update selected search index
            url = URLHelper.UpdateParameterInUrl(url, "searchindex", HttpUtility.UrlEncode(drpIndexes.SelectedValue));

            // Update selected language
            if (plcLang.Visible)
            {
                url = URLHelper.UpdateParameterInUrl(url, "searchlanguage", HttpUtility.UrlEncode(drpLanguage.SelectedValue));
                url = URLHelper.UpdateParameterInUrl(url, "searchculture", HttpUtility.UrlEncode(ValidationHelper.GetString(cultureElem.Value, null)));
            }

            // Redirect
            URLHelper.Redirect(url);
        }
    }

    #endregion
}
