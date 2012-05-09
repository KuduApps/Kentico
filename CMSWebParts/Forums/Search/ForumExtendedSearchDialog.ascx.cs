using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Forums;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.URLRewritingEngine;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSWebParts_Forums_Search_ForumExtendedSearchDialog : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether user can select forums to search.
    /// </summary>
    public bool EnableForumSelection
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("EnableForumSelection"), true);
        }
        set
        {
            SetValue("EnableForumSelection", value);
        }
    }


    /// <summary>
    /// Gets or sets the url where is the search result web part.
    /// </summary>
    public string RedirectUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("RedirectUrl"), "");
        }
        set
        {
            SetValue("RedirectUrl", value);
        }
    }


    /// <summary>
    /// Indicates whether the web part should be hidden for result page.
    /// </summary>
    public bool HideForResult
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideForResult"), false);
        }
        set
        {
            SetValue("HideForResult", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the forums for which the user has no permissions
    /// are visible in the list of forums in forum group.
    /// </summary>
    public bool HideForumForUnauthorized
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideForumForUnauthorized"), false);
        }
        set
        {
            SetValue("HideForumForUnauthorized", value);
        }
    }

    #endregion


    #region "Methods"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!RequestHelper.IsPostBack())
        {
            // Fill the search-in options
            this.drpSearchIn.Items.Clear();
            this.drpSearchIn.Items.Add(new ListItem(GetString("ForumExtSearch.SubjAndText.Title"), "subjecttext"));
            this.drpSearchIn.Items.Add(new ListItem(GetString("ForumExtSearch.Subject.Title"), "subject"));
            this.drpSearchIn.Items.Add(new ListItem(GetString("ForumExtSearch.Text.Title"), "text"));
            this.drpSearchIn.SelectedIndex = 0;

            // Fill the order-by options
            this.drpSearchOrderBy.Items.Clear();
            this.drpSearchOrderBy.Items.Add(new ListItem(GetString("ForumExtSearch.PostTime.Title"), "posttime"));
            this.drpSearchOrderBy.Items.Add(new ListItem(GetString("general.subject"), "subject"));
            this.drpSearchOrderBy.Items.Add(new ListItem(GetString("ForumExtSearch.Author.Title"), "author"));
            this.drpSearchOrderBy.SelectedIndex = 0;

            // Initialize orer buttons
            this.rblSearchOrder.Items.Clear();
            this.rblSearchOrder.Items.Add(new ListItem(GetString("ForumExtSearch.Ascending.Title"), "ascending"));
            this.rblSearchOrder.Items.Add(new ListItem(GetString("ForumExtSearch.Descending.Title"), "descending"));
            this.rblSearchOrder.SelectedIndex = 0;

            // Try to pre-select filter items
            PreSelectFilter();
        }
    }


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        InitControl();
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        InitControl();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes control.
    /// </summary>
    private void InitControl()
    {
        // Check if the web part should be hidden for the search result page
        if (QueryHelper.Contains("searchtext") || QueryHelper.Contains("searchusername"))
        {
            if (HideForResult)
            {
                Visible = false;
            }
        }

        if (!StopProcessing && Visible)
        {
            // Initialize control
            SetupControl();
        }
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        // Register tooltips script file
        ScriptHelper.RegisterTooltip(this.Page);
        
        imgTextHint.ImageUrl = GetImageUrl("Design/Forums/hint.gif");
        imgTextHint.AlternateText = "Hint";
        ScriptHelper.AppendTooltip(imgTextHint, GetString("ForumSearch.SearchTextHint"), "help");

        btnSearch.Text = GetString("general.search");

        if (EnableForumSelection)
        {           
            plcForums.Visible = true;            
            string selected = ";" + QueryHelper.GetString("searchforums", "") + ";";

            ForumPostsDataSource fpd = new ForumPostsDataSource();
            fpd.CacheMinutes = 0;
            fpd.SelectedColumns = "DISTINCT GroupID, GroupDisplayName, ForumID, ForumDisplayName,GroupOrder, ForumOrder, ForumName ";
            fpd.SelectOnlyApproved = false;
            fpd.SiteName = CMSContext.CurrentSiteName;

            string where = "GroupGroupID IS NULL AND GroupName != 'adhocforumgroup' AND ForumOpen=1";
            if (HideForumForUnauthorized)
            {
                where = ForumInfoProvider.CombineSecurityWhereCondition(where, 0);
            }
            fpd.WhereCondition = where;

            fpd.OrderBy = "GroupOrder, ForumOrder ASC, ForumName ASC";

            DataSet ds = fpd.DataSource as DataSet;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                int oldGroup = -1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (oldGroup != ValidationHelper.GetInteger(dr["GroupID"], 0))
                    {
                        ListItem li = new ListItem(dr["GroupDisplayName"].ToString(), "");
                        li.Attributes.Add("disabled", "disabled");
                        if (!listForums.Items.Contains(li))
                        {
                            listForums.Items.Add(li);
                        }
                        oldGroup = ValidationHelper.GetInteger(dr["GroupID"], 0);
                    }

                    string forumId = Convert.ToString(dr["ForumID"]);
                    ListItem lif = new ListItem(" \xA0\xA0\xA0\xA0 " + dr["ForumDisplayName"], forumId);
                    if (selected.Contains(";" + forumId + ";"))
                    {
                        lif.Selected = true;
                    }

                    // On postback on ASPX 
                    if (!listForums.Items.Contains(lif))
                    {
                        listForums.Items.Add(lif);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Validates search dialog entries and decides whether the search query should be generated.
    /// </summary>    
    private string ValidateSearchDialog()
    {
        // Check if minimum searching criteria were matched
        return string.IsNullOrEmpty(txtSearchText.Text) && string.IsNullOrEmpty(txtUserName.Text)
                   ? GetString("ForumExtSearch.Search.NothingToSearch")
                   : string.Empty;
    }


    /// <summary>
    /// Loads the settings from the querystring when search resulkt page is the same where this control resides.
    /// </summary>
    private void PreSelectFilter()
    {
        // Get info from the query string
        string searchtext = QueryHelper.GetString("searchtext", "");
        string searchusername = QueryHelper.GetString("searchusername", "");
        string searchin = QueryHelper.GetString("searchin", "");
        string searchorderby = QueryHelper.GetString("searchorderby", "");
        string searchorder = QueryHelper.GetString("searchorder", "");

        // Load the selection
        txtSearchText.Text = (searchtext != "") ? searchtext : "";
        txtUserName.Text = (searchusername != "") ? searchusername : "";

        try
        {
            drpSearchIn.SelectedValue = searchin;
        }
        catch { }

        try
        {
            drpSearchOrderBy.SelectedValue = searchorderby;
        }
        catch { }

        try
        {
            rblSearchOrder.SelectedValue = searchorder;
        }
        catch { }
    }

    #endregion


    #region "Event handlers"

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string errMsg = ValidateSearchDialog();

        // Search dialog filled properly
        if (errMsg == string.Empty)
        {
            // Generate query string according search dialog selections
            string queryString = String.Empty;
            queryString += (!string.IsNullOrEmpty(this.txtSearchText.Text)) ? "searchtext=" + HttpUtility.UrlEncode(this.txtSearchText.Text) + "&" : "";
            queryString += (!string.IsNullOrEmpty(this.txtUserName.Text)) ? "searchusername=" + HttpUtility.UrlEncode(this.txtUserName.Text) + "&" : "";
            queryString += (!string.IsNullOrEmpty(this.drpSearchIn.SelectedValue)) ? "searchin=" + this.drpSearchIn.SelectedValue + "&" : "";
            queryString += (!string.IsNullOrEmpty(this.drpSearchOrderBy.SelectedValue)) ? "searchorderby=" + this.drpSearchOrderBy.SelectedValue + "&" : "";
            queryString += (!string.IsNullOrEmpty(this.rblSearchOrder.SelectedValue)) ? "searchorder=" + this.rblSearchOrder.SelectedValue + "&" : "";
            queryString = queryString.TrimEnd('&');

            if (EnableForumSelection)
            {
                string forQuery = "";
                foreach (ListItem li in listForums.Items)
                {
                    if ((li.Selected) && (li.Value != ""))
                    {
                        if (forQuery != "")
                        {
                            forQuery += ";";
                        }
                        forQuery += li.Value;
                    }
                }

                if (forQuery != "")
                {
                    queryString += "&searchforums=" + forQuery;
                }
            }

            // Log "internal search" activity
            string siteName = CMSContext.CurrentSiteName;
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) &&
                ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) && ActivitySettingsHelper.SearchEnabled(siteName))
            {
                ActivityLogProvider.LogInternalSearchActivity(CMSContext.CurrentDocument, ModuleCommands.OnlineMarketingGetCurrentContactID(),
                    CMSContext.CurrentSiteID, URLHelper.CurrentRelativePath, this.txtSearchText.Text, CMSContext.Campaign);
            }

            // Redirect to the search result page
            if (!string.IsNullOrEmpty(RedirectUrl))
            {
                if (RedirectUrl.IndexOf("?") < 0)
                {
                    queryString = "?" + queryString;
                }

                URLHelper.Redirect(ResolveUrl(RedirectUrl) + queryString);
            }
            else
            {
                string url = URLRewriter.CurrentURL;

                // Get rid of previous query string parameters
                if (url.IndexOf("?") > -1)
                {
                    url = URLHelper.RemoveParameterFromUrl(url, "searchtext");
                    url = URLHelper.RemoveParameterFromUrl(url, "searchusername");
                    url = URLHelper.RemoveParameterFromUrl(url, "searchin");
                    url = URLHelper.RemoveParameterFromUrl(url, "searchorderby");
                    url = URLHelper.RemoveParameterFromUrl(url, "searchorder");
                    url = URLHelper.RemoveParameterFromUrl(url, "searchforums");
                    url = URLHelper.RemoveParameterFromUrl(url, "forumid");
                    url = URLHelper.RemoveParameterFromUrl(url, "threadid");
                    url = URLHelper.RemoveParameterFromUrl(url, "thread");
                    url = URLHelper.RemoveParameterFromUrl(url, "postid");
                    url = URLHelper.RemoveParameterFromUrl(url, "mode");
                    url = URLHelper.RemoveParameterFromUrl(url, "replyto");
                    url = URLHelper.RemoveParameterFromUrl(url, "subscribeto");
                    url = URLHelper.RemoveParameterFromUrl(url, "page");                    
                }
                
                // Append query string
                url = URLHelper.AppendQuery(url, queryString);

                //Redirect back to the current page
                URLHelper.Redirect(url);
            }
        }
        else
        {
            // Display error info to the user
            lblInfo.Text = errMsg;
            lblInfo.Visible = true;
        }
    }

    #endregion

    #endregion
}