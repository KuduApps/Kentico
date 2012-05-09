using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.ISearchEngine;
using CMS.Controls;
using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSWebParts_Search_cmscompletesearchdialog : CMSAbstractWebPart
{
    #region "Search dialog properties"

    /// <summary>
    /// Gets or sets the label search for text.
    /// </summary>
    public string SearchForLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchForLabel"), ResHelper.LocalizeString("{$CMSSearchDialog.SearchFor$}"));
        }
        set
        {
            this.SetValue("SearchForLabel", value);
            this.srchDialog.SearchForLabel.Text = value;
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
    /// Gets or sets search button text.
    /// </summary>
    public string SearchButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchButtonText"), ResHelper.LocalizeString("{$CMSSearchDialog.Go$}"));
        }
        set
        {
            this.SetValue("SearchButtonText", value);
            this.srchDialog.SearchButton.Text = value;
        }
    }


    /// <summary>
    ///  Gets or sets the search mode.
    /// </summary>
    public SearchModeEnum SearchMode
    {
        get
        {
            return CMSSearchDialog.GetSearchMode(DataHelper.GetNotEmpty(this.GetValue("SearchMode"), SearchHelper.GetSearchModeString(this.srchDialog.SearchMode)));
        }
        set
        {
            this.SetValue("SearchMode", SearchHelper.GetSearchModeString(value));
            this.srchDialog.SearchMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the search mode label.
    /// </summary>
    public string SearchModeLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchModeLabel"), ResHelper.LocalizeString("{$CMSSearchDialog.SearchMode$}"));
        }
        set
        {
            this.SetValue("SearchModeLabel", value);
            this.srchDialog.SearchModeLabel.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether search scope drop down is diplayed.
    /// </summary>
    public bool ShowSearchScope
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSearchScope"), this.srchDialog.ShowSearchScope);
        }
        set
        {
            this.SetValue("ShowSearchScope", value);
            this.srchDialog.ShowSearchScope = value;
        }
    }


    /// <summary>
    /// Gets or sets the search scope.
    /// </summary>
    public SearchScopeEnum SearchScope
    {
        get
        {
            return CMSSearchDialog.GetSearchScope(DataHelper.GetNotEmpty(this.GetValue("SearchScope"), this.srchDialog.SearchScope.ToString()));
        }
        set
        {
            this.SetValue("SearchScope", value.ToString());
            this.srchDialog.SearchScope = value;
        }
    }


    /// <summary>
    /// Gets or sets the search scope label.
    /// </summary>
    public string SearchScopeLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SearchScopeLabel"), ResHelper.LocalizeString("{$CMSSearchDialog.SearchScope$}"));
        }
        set
        {
            this.SetValue("SearchScopeLabel", value);
            this.srchDialog.SearchScopeLabel.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the Skin ID.
    /// </summary>
    public override string SkinID
    {
        get
        {
            return base.SkinID;
        }
        set
        {
            base.SkinID = value;
            this.srchDialog.SkinID = value;
        }
    }

    #endregion


    #region "Document properties"

    /// <summary>
    /// Gets or sets the value that indidcates whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.srchResults.CheckPermissions);            
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            srchResults.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Classnames"), this.srchResults.ClassNames), this.srchResults.ClassNames);
        }
        set
        {
            this.SetValue("ClassNames", value);
            this.srchResults.ClassNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indidcates whether current culture will be combined with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), this.srchResults.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.srchResults.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), this.srchResults.CultureCode), this.srchResults.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.srchResults.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the nodes path.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), this.srchResults.Path), this.srchResults.Path);
        }
        set
        {
            this.SetValue("Path", value);
            this.srchResults.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether only published nodes will be selected.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), this.srchResults.SelectOnlyPublished);
        }
        set
        {
            this.SetValue("SelctOnlyPublished", value);
            this.srchResults.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), this.srchResults.SiteName), this.srchResults.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.srchResults.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("WhereCondition"), this.srchResults.WhereCondition), this.srchResults.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.srchResults.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OrderBy"), this.srchResults.OrderBy), this.srchResults.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.srchResults.OrderBy = value;
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
            return ValidationHelper.GetBoolean(this.GetValue("EnablePaging"), this.srchResults.EnablePaging);
        }
        set
        {
            this.SetValue("EnablePaging", value);
            this.srchResults.EnablePaging = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager position.
    /// </summary>
    public PagingPlaceTypeEnum PagerPosition
    {
        get
        {
            return this.srchResults.PagerControl.GetPagerPosition(DataHelper.GetNotEmpty(this.GetValue("PagerPosition"), this.srchResults.PagerControl.PagerPosition.ToString()));
        }
        set
        {
            this.SetValue("PagerPosition", value.ToString());
            this.srchResults.PagerControl.PagerPosition = value;
        }
    }


    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), this.srchResults.PagerControl.PageSize);
        }
        set
        {
            this.SetValue("PageSize", value);
            this.srchResults.PagerControl.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager query string key.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("QueryStringKey"), this.srchResults.PagerControl.QueryStringKey), this.srchResults.PagerControl.QueryStringKey);
        }
        set
        {
            this.SetValue("QueryStringKey", value);
            this.srchResults.PagerControl.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the paging mode (QueryString, PostBack).
    /// </summary>
    public PagingModeTypeEnum PagingMode
    {
        get
        {
            return this.srchResults.PagerControl.GetPagingMode(DataHelper.GetNotEmpty(this.GetValue("PagingMode"), this.srchResults.PagerControl.PagingMode.ToString()));
        }
        set
        {
            this.SetValue("PagingMode", value.ToString());
            this.srchResults.PagerControl.PagingMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the navigation mode.
    /// </summary>
    public BackNextLocationTypeEnum BackNextLocation
    {
        get
        {
            return this.srchResults.PagerControl.GetBackNextLocation(DataHelper.GetNotEmpty(this.GetValue("BackNextLocation"), this.srchResults.PagerControl.BackNextLocation.ToString()));
        }
        set
        {
            this.SetValue("BackNextLocation", value.ToString());
            this.srchResults.PagerControl.BackNextLocation = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last links will be displayed.
    /// </summary>
    public bool ShowFirstLast
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowFirstLast"), this.srchResults.PagerControl.ShowFirstLast);
        }
        set
        {
            this.SetValue("ShowFirstLast", value);
            this.srchResults.PagerControl.ShowFirstLast = value;
        }
    }


    /// <summary>
    /// Gets or sets the html which will be displayed before pager.
    /// </summary>
    public string PagerHTMLBefore
    {
        get
        {
        	 return ValidationHelper.GetString(this.GetValue("PagerHTMLBefore"), this.srchResults.PagerControl.PagerHTMLBefore); 
        }
        set
        {
            this.SetValue("PagerHTMLBefore", value);
            this.srchResults.PagerControl.PagerHTMLBefore = value;
        }
    }


    /// <summary>
    /// Gets or sets the html which will be displayed after pager.
    /// </summary>
    public string PagerHTMLAfter
    {
        get
        {
        	 return ValidationHelper.GetString(this.GetValue("PagerHTMLAfter"), this.srchResults.PagerControl.PagerHTMLAfter); 
        }
        set
        {
            this.SetValue("PagerHTMLAfter", value);
            this.srchResults.PagerControl.PagerHTMLAfter = value;
        }
    }


    /// <summary>
    /// Gets or sets the results position.
    /// </summary>
    public ResultsLocationTypeEnum ResultsPosition
    {
        get
        {
        	 return this.srchResults.PagerControl.GetResultPosition(ValidationHelper.GetString(this.GetValue("ResultsPosition"), "")); 
        }
        set
        {
            this.SetValue("ResultsPosition", value);
            this.srchResults.PagerControl.ResultsLocation = value;
        }
    }


    /// <summary>
    /// Gets or sets the page numbers separator.
    /// </summary>
    public string PageNumbersSeparator
    {
        get
        {
        	 return ValidationHelper.GetString(this.GetValue("PageNumbersSeparator"), this.srchResults.PagerControl.PageNumbersSeparator); 
        }
        set
        {
            this.SetValue("PageNumbersSeparator", value);
            this.srchResults.PagerControl.PageNumbersSeparator = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("TransformationName"), this.srchResults.TransformationName), this.srchResults.TransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
            this.srchResults.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which will be displayed if no result were found.
    /// </summary>
    public string NoResultsLabel
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("NoResults"), ResHelper.LocalizeString("{$CMSSearchResults.NoDocumentFound$}"));
        }
        set
        {
            this.SetValue("NoResults", value);
            this.srchResults.NoResultsLabel.Text = value;
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
        if (this.StopProcessing)
        {
            this.srchDialog.StopProcessing = true;
            this.srchResults.StopProcessing = true;
        }
        else
        {
            //Search dialog
            this.srchDialog.ID = this.ID + "_CMSSearchDialog";

            this.srchDialog.SearchForLabel.Text = this.SearchForLabel;
            this.srchDialog.SearchButton.Text = this.SearchButtonText;

            this.srchDialog.ShowSearchMode = this.ShowSearchMode;
            this.srchDialog.SearchMode = this.SearchMode;
            this.srchDialog.SearchModeLabel.Text = this.SearchModeLabel;

            this.srchDialog.ShowSearchScope = this.ShowSearchScope;
            this.srchDialog.SearchScope = this.SearchScope;
            this.srchDialog.SearchScopeLabel.Text = this.SearchScopeLabel;
            this.srchDialog.DoSearch += new CMSSearchDialog.DoSearchEventHandler(srchDialog_DoSearch);

            // Document properties
            this.srchResults.CheckPermissions = this.CheckPermissions;
            this.srchResults.ClassNames = this.ClassNames;
            this.srchResults.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.srchResults.CultureCode = this.CultureCode;
            this.srchResults.Path = this.Path;
            this.srchResults.SelectOnlyPublished = this.SelectOnlyPublished;
            this.srchResults.SiteName = this.SiteName;

            this.srchResults.CMSSearchDialogID = this.ID + "_CMSSearchDialog";

            this.srchResults.SearchMode = this.SearchMode;
            this.srchResults.TransformationName = this.TransformationName;
            this.srchResults.NoResultsLabel.Text = this.NoResultsLabel;

            // Pager
            this.srchResults.EnablePaging = this.EnablePaging;
            this.srchResults.QueryStringKey = this.QueryStringKey;
            this.srchResults.PagerControl.PageSize = this.PageSize;
            this.srchResults.PagerControl.PagerPosition = this.PagerPosition;
            this.srchResults.PagerControl.PagingMode = this.PagingMode;
            this.srchResults.PagerControl.BackNextLocation = this.BackNextLocation;
            this.srchResults.PagerControl.ShowFirstLast = this.ShowFirstLast;
            this.srchResults.PagerControl.PagerHTMLBefore = this.PagerHTMLBefore;
            this.srchResults.PagerControl.PagerHTMLAfter = this.PagerHTMLAfter;
            this.srchResults.PagerControl.ResultsLocation = this.ResultsPosition;
            this.srchResults.PagerControl.PageNumbersSeparator = this.PageNumbersSeparator;


            this.srchResults.WhereCondition = this.WhereCondition;
            this.srchResults.OrderBy = this.OrderBy;

            // Set SkinID property
            if (!this.StandAlone && (this.PageCycle < PageCycleEnum.Initialized) && (ValidationHelper.GetString(this.Page.StyleSheetTheme, "") == ""))
            {
                this.srchDialog.SkinID = this.SkinID;
                this.srchResults.SkinID = this.SkinID;
            }
        }
    }


    protected void srchDialog_DoSearch()
    {
        // Log "internal search" activity
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) &&
            ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) &&
            ActivitySettingsHelper.SearchEnabled(siteName))
        {
            ActivityLogProvider.LogInternalSearchActivity(CMSContext.CurrentDocument, ModuleCommands.OnlineMarketingGetCurrentContactID(),
                CMSContext.CurrentSiteID, URLHelper.CurrentRelativePath, this.srchDialog.SearchForTextBox.Text, CMSContext.Campaign);
        }
    }


    /// <summary>
    /// Applies given stylesheet skin.
    /// </summary>
    public override void ApplyStyleSheetSkin(Page page)
    {
        string skinId = this.SkinID;
        this.srchDialog.SkinID = skinId;
        this.srchResults.SkinID = skinId;

        base.ApplyStyleSheetSkin(page);
    }
}
