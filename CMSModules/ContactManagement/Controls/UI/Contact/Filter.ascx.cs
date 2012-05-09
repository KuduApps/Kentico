using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_ContactManagement_Controls_UI_Contact_Filter : CMSAdminEditControl
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
            fltContactStatus.SiteID = value;
            if (value == UniSelector.US_ALL_RECORDS)
            {
                fltContactStatus.DisplayAll = true;
            }
            else if (value == UniSelector.US_GLOBAL_OR_SITE_RECORD)
            {
                fltContactStatus.DisplaySiteOrGlobal = true;
                fltContactStatus.SiteID = CMSContext.CurrentSiteID;
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
            return fltContactStatus.DisplaySiteOrGlobal;
        }
        set
        {
            fltContactStatus.DisplaySiteOrGlobal = value;
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
    /// Returns TRUE if displaying both merged and not merged contacts.
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
    /// Indicates if filter should return only not merged contacts. Otherwise filter returns only merged contacts.
    /// Applies only when filter is hidden.
    /// </summary>
    public bool NotMerged
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if control is placed on live site.
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
            fltContactStatus.IsLiveSite = value;
        }
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
                fltContactStatus.DisplaySiteOrGlobal = true;
            }
            siteOrGlobalSelector.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets if all contacts merged into global contacts should be hidde.
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
        fltPhone.Columns = new string[] { "ContactMobilePhone", "ContactHomePhone", "ContactBusinessPhone" };
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // General UI
        fltContactStatus.DropDownList.CssClass = "DropDownFieldFilter";
        this.lnkShowAdvancedFilter.Text = GetString("user.filter.showadvanced");
        this.imgShowAdvancedFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        this.lnkShowSimpleFilter.Text = GetString("user.filter.showsimple");
        this.imgShowSimpleFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortUp.png");
        plcAdvancedSearch.Visible = this.IsAdvancedMode;
        pnlAdvanced.Visible = this.IsAdvancedMode;
        plcMiddle.Visible = this.IsAdvancedMode;
        pnlSimple.Visible = !this.IsAdvancedMode;

        if (!RequestHelper.IsPostBack())
        {
            radMonitored.Items.Clear();
            radMonitored.Items.Add(GetString("general.all"));
            radMonitored.Items.Add(GetString("om.contact.monitored"));
            radMonitored.Items.Add(GetString("om.contact.notmonitored"));
            radMonitored.SelectedIndex = 0;
        }

        plcAdvancedSearch.Visible = plcMiddle.Visible = this.IsAdvancedMode;
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
        plcMiddle.Visible = showAdvanced;
        pnlSimple.Visible = !showAdvanced;
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
        string whereCond = "";

        // Create WHERE condition for basic filter
        int contactStatus = ValidationHelper.GetInteger(fltContactStatus.Value, -1);
        if (fltContactStatus.Value == null)
        {
            whereCond = "ContactStatusID IS NULL";
        }
        else if (contactStatus > 0)
        {
            whereCond = "ContactStatusID = " + contactStatus;
        }

        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltFirstName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltLastName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEmail.GetCondition());

        // Only monitored contacts
        if (radMonitored.SelectedIndex == 1)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ContactMonitored = 1");
        }
        // Only not monitored contacts
        else if (radMonitored.SelectedIndex == 2)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(ContactMonitored = 0) OR (ContactMonitored IS NULL)");
        }

        // Create WHERE condition for advanced filter (id needed)
        if (this.IsAdvancedMode)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltMiddleName.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, this.GetOwnerCondition(fltOwner));
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, this.GetCountryCondition(fltCountry));
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, this.GetStateCondition(fltState));
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltCity.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltPhone.GetCondition());

            if (!String.IsNullOrEmpty(txtIP.Text))
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(ContactID IN (SELECT IPOriginalContactID FROM OM_IP WHERE IPAddress LIKE '%" + SqlHelperClass.GetSafeQueryString(txtIP.Text, false) + "%') OR ContactID IN (SELECT IPActiveContactID FROM OM_IP WHERE IPAddress LIKE '%" + SqlHelperClass.GetSafeQueryString(txtIP.Text, false) + "%'))");
            }
        }

        // When "merged/not merged" filter is hidden or in advanced mode display contacts according to filter or in basic mode don't display merged contacts
        if ((this.HideMergedFilter && this.NotMerged) ||
            (this.IsAdvancedMode && !this.HideMergedFilter && !chkMerged.Checked) ||
            (!this.HideMergedFilter && !this.NotMerged && !this.IsAdvancedMode))
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(ContactMergedWithContactID IS NULL AND ContactSiteID > 0) OR (ContactGlobalContactID IS NULL AND ContactSiteID IS NULL)");
        }

        // Hide contacts merged into global contact when displying list of available contacts for global contact
        if (this.HideMergedIntoGlobal)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ContactGlobalContactID IS NULL");
        }

        // Filter by site
        if (!plcSite.Visible)
        {
            // Filter site objects
            if (this.SiteID > 0)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(ContactSiteID = " + this.SiteID.ToString() + ")");
            }
            // Filter only global objects
            else if (this.SiteID == UniSelector.US_GLOBAL_RECORD)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(ContactSiteID IS NULL)");
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
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ContactSiteID IS NULL");
            }
            // Global and site objects
            else if (mSelectedSiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ContactSiteID IS NULL OR ContactSiteID = " + CMSContext.CurrentSiteID);
            }
            // Site objects
            else if (mSelectedSiteID != UniSelector.US_ALL_RECORDS)
            {
                whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ContactSiteID = " + mSelectedSiteID);
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
            return String.Format("ContactCountryID IN (SELECT CountryID FROM CMS_Country WHERE {0})", originalQuery);
        }
        return originalQuery;
    }


    /// <summary>
    /// Gets SQL Where condition for owner's fullname.
    /// </summary>
    private string GetOwnerCondition(CMSAbstractBaseFilterControl filter)
    {
        string originalQuery = filter.WhereCondition;
        if (!String.IsNullOrEmpty(originalQuery))
        {
            return String.Format("ContactOwnerUserID IN (SELECT UserID FROM CMS_User WHERE {0})", originalQuery);
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
            return String.Format("ContactStateID IN (SELECT StateID FROM CMS_State WHERE {0})", originalQuery);
        }
        return originalQuery;
    }

    #endregion

}