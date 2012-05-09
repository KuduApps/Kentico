using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.Controls;


public partial class CMSModules_ContactManagement_Controls_UI_Account_Filter : CMSUserControl
{
    #region "Variables"

    private int mSiteId = -1;
    private int mSelectedSiteID = -1;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the site ID for which the events should be filtered.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
            fltAccountStatus.SiteID = value;

            if (value == UniSelector.US_ALL_RECORDS)
            {
                fltAccountStatus.DisplayAll = true;
            }
            else if (value == UniSelector.US_GLOBAL_OR_SITE_RECORD)
            {
                fltAccountStatus.DisplaySiteOrGlobal = true;
                fltAccountStatus.SiteID = CMSContext.CurrentSiteID;
            }
        }
    }


    /// <summary>
    /// Indicates whether to show global statuses or not.
    /// </summary>
    public bool ShowGlobalStatuses
    {
        get
        {
            return fltAccountStatus.DisplaySiteOrGlobal;
        }
        set
        {
            fltAccountStatus.DisplaySiteOrGlobal = value;
        }
    }


    /// <summary>
    /// Selected site ID.
    /// </summary>
    public int SelectedSiteID
    {
        get
        {
            return mSelectedSiteID;
        }
    }


    /// <summary>
    /// Gets the where condition created using filtered parameters.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return GenerateWhereCondition();
        }
    }


    /// <summary>
    /// Indicates if AccountStatus filter should be displayed even if site is not specified.
    /// </summary>
    public bool DisplayAccountStatus
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if filter is in advanced mode.
    /// </summary>
    protected bool IsAdvancedMode
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsAdvancedMode"], false);
        }
        set
        {
            ViewState["IsAdvancedMode"] = value;
        }
    }


    /// <summary>
    /// Indicates if  filter is used on live site or in UI.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            fltAccountStatus.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Returns TRUE if displaying both merged and not merged accounts.
    /// </summary>
    public bool DisplayingAll
    {
        get
        {
            return chkMerged.Checked;
        }
    }
    

    /// <summary>
    /// Indicates if merging filter should be hidden.
    /// </summary>
    public bool HideMergedFilter
    {
        get
        {
            return !plcMerged.Visible;
        }
        set
        {
            plcMerged.Visible = !value;
        }
    }


    /// <summary>
    /// Indicates if filter should return only not merged accounts. Otherwise filter returns only merged accounts.
    /// Applies only when filter is hidden.
    /// </summary>
    public bool NotMerged
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if siteselector should be displayed.
    /// </summary>
    public bool DisplaySiteSelector
    {
        get
        {
            return siteSelector.Visible;
        }
        set
        {
            if (value)
            {
                plcSite.Visible = true;
            }
            siteSelector.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if 'Site or global selector' should be displayed.
    /// </summary>
    public bool DisplayGlobalOrSiteSelector
    {
        get
        {
            return siteOrGlobalSelector.Visible;
        }
        set
        {
            if (value)
            {
                plcSite.Visible = true;
            }
            siteOrGlobalSelector.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets if all accounts merged into global accounts should be hidde.
    /// </summary>
    public bool HideMergedIntoGlobal
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets value that indicates whether "show children" checkbox should be visible.
    /// </summary>
    public bool ShowChildren
    {
        get;
        set;
    }


    /// <summary>
    /// Returns true if all child contacts will be shown.
    /// </summary>
    public bool ChildrenSelected
    {
        get { return chkChildren.Checked; }
    }

    #endregion


    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        fltPhone.Columns = new string[] { "AccountPhone", "AccountFax" };
        fltContactName.Columns = new string[] { "PrimaryContactFirstName", "PrimaryContactMiddleName", "PrimaryContactLastName", "SecondaryContactFirstName", "SecondaryContactMiddleName", "SecondaryContactLastName" };
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeForm();
        plcAdvancedSearch.Visible = this.IsAdvancedMode;
        plcChildren.Visible = ShowChildren;
    }

    #endregion


    #region "UI methods"

    /// <summary>
    /// Shows/hides all elements for advanced or simple mode.
    /// </summary>
    private void ShowFilterElements(bool showAdvanced)
    {
        plcAdvancedSearch.Visible = showAdvanced;
        pnlAdvanced.Visible = showAdvanced;
        pnlSimple.Visible = !showAdvanced;
    }


    /// <summary>
    /// Initializes the layout of the form.
    /// </summary>
    private void InitializeForm()
    {
        // General UI
        fltAccountStatus.DropDownList.CssClass = "DropDownFieldFilter";
        this.lnkShowAdvancedFilter.Text = GetString("user.filter.showadvanced");
        this.imgShowAdvancedFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        this.lnkShowSimpleFilter.Text = GetString("user.filter.showsimple");
        this.imgShowSimpleFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortUp.png");
        plcAdvancedSearch.Visible = this.IsAdvancedMode;
        pnlAdvanced.Visible = this.IsAdvancedMode;
        pnlSimple.Visible = !this.IsAdvancedMode;
    }


    /// <summary>
    /// Sets the advanced mode.
    /// </summary>
    protected void lnkShowAdvancedFilter_Click(object sender, EventArgs e)
    {
        this.IsAdvancedMode = true;
        ShowFilterElements(true);
    }


    /// <summary>
    /// Sets the simple mode.
    /// </summary>
    protected void lnkShowSimpleFilter_Click(object sender, EventArgs e)
    {
        this.IsAdvancedMode = false;
        ShowFilterElements(false);
    }

    #endregion


    #region "Search methods - where condition"

    /// <summary>
    /// Generates complete filter where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        string whereCond = string.Empty;

        // Create WHERE condition for basic filter
        int contactStatus = ValidationHelper.GetInteger(fltAccountStatus.Value, -1);
        if (fltAccountStatus.Value == null)
        {
            whereCond = "AccountStatusID IS NULL";
        }
        else if (contactStatus > 0)
        {
            whereCond = "AccountStatusID = " + contactStatus;
        }

        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEmail.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltContactName.GetCondition());

        if (this.IsAdvancedMode)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltOwner.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, this.GetCountryCondition(fltCountry));
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, this.GetStateCondition(fltState));
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltCity.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltPhone.GetCondition());
        }

        // When "merged/not merged" filter is hidden
        if ((this.HideMergedFilter && this.NotMerged) ||
            (this.IsAdvancedMode && !this.HideMergedFilter && !chkMerged.Checked) ||
            (!this.IsAdvancedMode && !this.HideMergedFilter && !this.NotMerged))
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(AccountMergedWithAccountID IS NULL AND AccountSiteID > 0) OR (AccountGlobalAccountID IS NULL AND AccountSiteID IS NULL)");
        }

        // Hide accounts merged into global account when displying list of available accounts for global account
        if (this.HideMergedIntoGlobal)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "AccountGlobalAccountID IS NULL");
        }

        // Filter current account's site
        if (!plcSite.Visible)
        {
            // Filter site objects
            if (this.SiteID > 0)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(AccountSiteID = " + this.SiteID.ToString() + ")");
            }
            // Filter only global objects
            else if (this.SiteID == UniSelector.US_GLOBAL_RECORD)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(AccountSiteID IS NULL)");
            }
        }
        // Filter by site filter
        else
        {
            mSelectedSiteID = UniSelector.US_ALL_RECORDS;
            if (siteSelector.Visible)
            {
                mSelectedSiteID = siteSelector.SiteID;
            }
            else if (siteOrGlobalSelector.Visible)
            {
                mSelectedSiteID = siteOrGlobalSelector.SiteID;
            }
            if (mSelectedSiteID == 0)
            {
                mSelectedSiteID = UniSelector.US_ALL_RECORDS;
            }

            // Only global objects
            if (mSelectedSiteID == UniSelector.US_GLOBAL_RECORD)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "AccountSiteID IS NULL");
            }
            // Global and site objects
            else if (mSelectedSiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "AccountSiteID IS NULL OR AccountSiteID = " + CMSContext.CurrentSiteID);
            }
            // Site objects
            else if (mSelectedSiteID != UniSelector.US_ALL_RECORDS)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "AccountSiteID = " + mSelectedSiteID);
            }
        }

        return whereCond;
    }

    #endregion


    #region "Additional filter conditions"

    /// <summary>
    /// Gets SQL Where condition for a country.
    /// </summary>
    private string GetCountryCondition(CMSAbstractBaseFilterControl filter)
    {
        string originalQuery = filter.WhereCondition;
        if (!String.IsNullOrEmpty(originalQuery))
        {
            return String.Format("AccountCountryID IN (SELECT CountryID FROM CMS_Country WHERE {0})", originalQuery);
        }
        return originalQuery;
    }


    /// <summary>
    /// Gets SQL Where condition for a state.
    /// </summary>
    private string GetStateCondition(CMSAbstractBaseFilterControl filter)
    {
        string originalQuery = filter.WhereCondition;
        if (!String.IsNullOrEmpty(originalQuery))
        {
            return String.Format("AccountStateID IN (SELECT StateID FROM CMS_State WHERE {0})", originalQuery);
        }
        return originalQuery;
    }

    #endregion
}
