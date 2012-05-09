using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.ISearchEngine;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSWebParts_SmartSearch_SearchBox : CMSAbstractWebPart
{
    #region "Variables"

    // Result page url
    protected string mResultPageUrl = URLHelper.CurrentURL;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether image button is displayed instead of regular button.
    /// </summary>
    public bool ShowImageButton
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowImageButton"), false);
        }
        set
        {
            SetValue("ShowImageButton", value);
            btnSearch.Visible = !value;
            btnImageButton.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets an Image button URL.
    /// </summary>
    public string ImageUrl
    {
        get
        {
            return ResolveUrl(ValidationHelper.GetString(GetValue("ImageUrl"), btnImageButton.ImageUrl));
        }
        set
        {
            SetValue("ImageUrl", value);
            btnImageButton.ImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether search label is displayed.
    /// </summary>
    public bool ShowSearchLabel
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowSearchLabel"), lblSearch.Visible);
        }
        set
        {
            SetValue("ShowSearchLabel", value);
            lblSearch.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the search label text.
    /// </summary>
    public string SearchLabelText
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SearchLabelText"), GetString("srch.searchbox.searchfor") );
        }
        set
        {
            SetValue("SearchLabelText", value);
            lblSearch.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the search button text.
    /// </summary>
    public string SearchButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SearchButtonText"), GetString("general.search"));
        }
        set
        {
            SetValue("SearchButtonText", value);
            btnSearch.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the search label Css class.
    /// </summary>
    public string SearchLabelCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SearchLabelCssClass"), "");
        }
        set
        {
            SetValue("SearchLabelCssClass", value);
            lblSearch.CssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets search text box CSS class.
    /// </summary>
    public string SearchTextboxCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SearchTextboxCssClass"), "");
        }
        set
        {
            SetValue("SearchTextboxCssClass", value);
            txtWord.CssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the search button CSS class.
    /// </summary>
    public string SearchButtonCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SearchButtonCssClass"), "");
        }
        set
        {
            SetValue("SearchButtonCssClass", value);
            btnSearch.CssClass = value;
            btnImageButton.CssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the search results page URL.
    /// </summary>
    public string SearchResultsPageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("SearchResultsPageUrl"), mResultPageUrl);
        }
        set
        {
            SetValue("SearchResultsPageUrl", value);
            mResultPageUrl = value;
        }
    }


    /// <summary>
    ///  Gets or sets the Search mode.
    /// </summary>
    public SearchModeEnum SearchMode
    {
        get
        {
            return SearchHelper.GetSearchModeEnum(ValidationHelper.GetString(GetValue("SearchMode"), ""));
        }
        set
        {
            SetValue("SearchMode", value.ToString());
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Button search handler.
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }


    /// <summary>
    /// Image button search handler.
    /// </summary>
    protected void btnImageButton_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    #endregion


    #region "Private methods" 

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            return;
        }
        else
        {
            btnSearch.Visible = !ShowImageButton;
            btnImageButton.Visible = ShowImageButton;
            pnlSearch.DefaultButton = btnSearch.Visible ? btnSearch.ID : btnImageButton.ID;

            btnImageButton.ImageUrl = ImageUrl;
            btnImageButton.AlternateText = GetString("General.search");

            // Set label visibility according to WAI standards
            if (!ShowSearchLabel)
            {
                lblSearch.Style.Add("display", "none");
            }

            // Set text properties
            lblSearch.Text = SearchLabelText;            

            btnSearch.Text = SearchButtonText;

            // Set class properties
            lblSearch.CssClass = SearchLabelCssClass;
            txtWord.CssClass = SearchTextboxCssClass;
            btnSearch.CssClass = SearchButtonCssClass;
            btnImageButton.CssClass = SearchButtonCssClass;

            // Set result page
            mResultPageUrl = SearchResultsPageUrl;           
        }       
    } 


    /// <summary>
    /// Runs the search.
    /// </summary>
    private void Search()
    {
        if (!string.IsNullOrEmpty(txtWord.Text))
        {
            string url = SearchResultsPageUrl;

            if (url.StartsWith("~"))
            {
                url = ResolveUrl(url.Trim());
            }            

            url = URLHelper.UpdateParameterInUrl(url, "searchtext", HttpUtility.UrlEncode(txtWord.Text));
            url = URLHelper.UpdateParameterInUrl(url, "searchmode", SearchHelper.GetSearchModeString(SearchMode));

            // Log "internal search" activity
            string siteName = CMSContext.CurrentSiteName;
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) && 
                ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) &&
                ActivitySettingsHelper.SearchEnabled(siteName))
            {
                ActivityLogProvider.LogInternalSearchActivity(CMSContext.CurrentDocument, ModuleCommands.OnlineMarketingGetCurrentContactID(),
                    CMSContext.CurrentSiteID, URLHelper.CurrentRelativePath, txtWord.Text, CMSContext.Campaign);
            }

            URLHelper.Redirect(url.Trim());
        }
    }    

    #endregion


    #region "Public methods"

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
