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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.ISearchEngine;
using CMS.Controls;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSWebParts_Search_cmssearchdialog : CMSAbstractWebPart
{
    #region "Variables"

    /// <summary>
    /// Flag indicates whether DoSearch event has been called.
    /// </summary>
    private bool doSearch = false;

    #endregion


    #region "Public properties"

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
    /// Gets or sets the search button text.
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
            this.SetValue("SearchMode", value.ToString());
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
            return DataHelper.GetNotEmpty(this.GetValue("SearchModeLabel"), ResHelper.LocalizeString("{$CMSSearchDialog.SearchMode$}"));
        }
        set
        {
            this.SetValue("SearchModeLabel", value);
            this.srchDialog.SearchModeLabel.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether search scope drop down is displayed.
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
    /// Gets or sets the search scope label text.
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
        }
        else
        {
            this.srchDialog.ID = this.ID + "_CMSSearchDialog";

            // Set label text
            this.srchDialog.SearchForLabel.Text = this.SearchForLabel;
            // Set button text
            this.srchDialog.SearchButton.Text = this.SearchButtonText;
            this.srchDialog.DoSearch += new CMSSearchDialog.DoSearchEventHandler(srchDialog_DoSearch);

            if (this.srchDialog.ShowSearchMode = this.ShowSearchMode)
            {
                // Set search mode
                this.srchDialog.SearchMode = this.SearchMode;
                // Set mode label text
                this.srchDialog.SearchModeLabel.Text = this.SearchModeLabel;
            }

            if (this.srchDialog.ShowSearchScope = this.ShowSearchScope)
            {
                // Set search scope
                this.srchDialog.SearchScope = this.SearchScope;
                // Set scope label text
                this.srchDialog.SearchScopeLabel.Text = this.SearchScopeLabel;
            }

            if (!this.StandAlone && (this.PageCycle < PageCycleEnum.Initialized) && (ValidationHelper.GetString(this.Page.StyleSheetTheme, "") == ""))
            {
                // Set SkinID property
                this.srchDialog.SkinID = this.SkinID;
            }
        }
    }


    protected void srchDialog_DoSearch()
    {
        if (!doSearch)
        {
            doSearch = true;
            // Log "internal search" activity
            string siteName = CMSContext.CurrentSiteName;
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) &&
                ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) &&
                ActivitySettingsHelper.SearchEnabled(siteName))
            {
                ActivityLogProvider.LogInternalSearchActivity(CMSContext.CurrentDocument, ModuleCommands.OnlineMarketingGetCurrentContactID(),
                                                              CMSContext.CurrentSiteID, URLHelper.CurrentRelativePath, srchDialog.SearchForTextBox.Text, CMSContext.Campaign);
            }
        }
    }


    /// <summary>
    /// Applies given stylesheet skin.
    /// </summary>
    public override void ApplyStyleSheetSkin(Page page)
    {
        this.srchDialog.SkinID = this.SkinID;
        base.ApplyStyleSheetSkin(page);
    }
}
