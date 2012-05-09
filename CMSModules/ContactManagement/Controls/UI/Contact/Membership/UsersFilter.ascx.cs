using System;
using System.Data;

using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.GlobalHelper;
using CMS.CMSHelper;


public partial class CMSModules_ContactManagement_Controls_UI_Contact_Membership_UsersFilter : CMSUserControl
{
    #region "Public properties"

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
    /// If true, site selector is visible.
    /// </summary>
    public bool ShowSiteFilter
    {
        get;
        set;
    }


    /// <summary>
    /// If true, contact filter (for contact name) is visible
    /// </summary>
    public bool ShowContactNameFilter
    {
        get;
        set;
    }

    #endregion


    #region "Methods"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        plcSite.Visible = ShowSiteFilter;
        plcCon.Visible = ShowContactNameFilter;
        if (ShowSiteFilter)
        {
            siteSelector.DropDownCssClass = "DropDownFieldFilter";
        }
    }


    /// <summary>
    /// Generates complete filter where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        string where = null;

        if (ShowContactNameFilter)
        {
            where = SqlHelperClass.AddWhereCondition(where, fltContact.GetCondition());
        }

        if (ShowSiteFilter)
        {
            if (siteSelector.SiteID > 0)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID = " + siteSelector.SiteID);
            }
        }

        where = SqlHelperClass.AddWhereCondition(where, fltFirstName.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, fltLastName.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, fltEmail.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, flUserName.GetCondition());

        return where;
    }

    #endregion
}