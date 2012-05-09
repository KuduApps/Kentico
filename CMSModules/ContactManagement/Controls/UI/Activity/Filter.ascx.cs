using System;
using System.Data;

using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.GlobalHelper;
using CMS.CMSHelper;


public partial class CMSModules_ContactManagement_Controls_UI_Activity_Filter : CMSUserControl
{
    #region "Variables"

    private bool mShowContactSelector = true;

    #endregion


    #region "Properties"

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
    /// Gets or sets site ID. This value is used when site selector is hidden.
    /// </summary>
    public int SiteID
    {
        get;
        set;
    }


    /// <summary>
    /// Determines whether contact selector is visible.
    /// </summary>
    public bool ShowContactSelector
    {
        get { return mShowContactSelector; }
        set { mShowContactSelector = value; }
    }


    /// <summary>
    /// Determines whether IP text box (filter) is visible.
    /// </summary>
    public bool ShowIPFilter
    {
        get;
        set;
    }


    /// <summary>
    /// Determines whether site selector is visible.
    /// </summary>
    public bool ShowSiteFilter
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if control is used on live site.
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
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        plcCon.Visible = ShowContactSelector;
        plcIp.Visible = ShowIPFilter;
        plcSite.Visible = ShowSiteFilter;
        if (ShowSiteFilter)
        {
            siteSelector.DropDownCssClass = "DropDownFieldFilter";
        }
    }

    #endregion


    #region "Search methods - where condition"

    /// <summary>
    /// Generates complete filter where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        string whereCond = string.Empty;
        if (!String.IsNullOrEmpty(drpType.SelectedValue))
        {
            whereCond = "ActivityType=N'" + drpType.SelectedValue.Replace("'", "''") + "'";
        }

        int siteId = this.SiteID;
        if (ShowSiteFilter)
        {
            siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        }

        // Create condition based on site selector
        if (siteId > 0)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ActivitySiteID=" + siteId);
        }
        else if (siteId == UniSelector.US_GLOBAL_RECORD)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "ActivitySiteID IS NULL");
        }

        if (ShowContactSelector)
        {
            // Filter by contact if contact selector is visible
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltContact.GetCondition());
        }

        if (ShowIPFilter)
        {
            // Filter by IP if filter is visible
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltIP.GetCondition());
        }

        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltName.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltTimeBetween.GetCondition());

        return whereCond;
    }

    #endregion
}