using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.SettingsProvider;
using CMS.FormControls;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Filters_RecycleBinFilter : CMSAbstractBaseFilterControl
{
    #region "Public properties"

    /// <summary>
    /// Returns selected user identifier.
    /// </summary>
    public int SelectedUser
    {
        get
        {
            Control postbackControl = ControlsHelper.GetPostBackControl(Page);
            return ValidationHelper.GetInteger((postbackControl == btnShow) ? userSelector.Value : ViewState["SelectedUser"], CMSContext.CurrentUser.UserID);
        }
        private set
        {
            userSelector.Value = value;
            ViewState["SelectedUser"] = value;
            WhereCondition = CreateWhereCondition(base.WhereCondition);
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            Control postbackControl = ControlsHelper.GetPostBackControl(Page);
            return DataHelper.GetNotEmpty((postbackControl == btnShow) ? CreateWhereCondition(base.WhereCondition) : ViewState["WhereCondition"], string.Empty);
        }
        set
        {
            base.WhereCondition = value;
            ViewState["WhereCondition"] = value;
        }
    }


    /// <summary>
    /// Determines whether filter is set.
    /// </summary>
    public bool FilterIsSet
    {
        get
        {
            int userId = ValidationHelper.GetInteger(userSelector.Value, 0);
            return nameFilter.FilterIsSet || pathFilter.FilterIsSet || classFilter.FilterIsSet || ((userId > 0) && UsersPlaceHolder.Visible);
        }
    }


    /// <summary>
    /// Gets place holder with user selector.
    /// </summary>
    public PlaceHolder UsersPlaceHolder
    {
        get
        {
            return plcUsers;
        }
    }


    /// <summary>
    /// Site ID to filter users.
    /// </summary>
    public int SiteID
    {
        get
        {
            return userSelector.SiteID;
        }
        set
        {
            userSelector.SiteID = value;
        }
    }


    /// <summary>
    /// Indicates if all available users should be displayed.
    /// </summary>
    public bool DisplayUsersFromAllSites
    {
        get
        {
            return userSelector.DisplayUsersFromAllSites;
        }
        set
        {
            userSelector.DisplayUsersFromAllSites = value;
        }
    }


    /// <summary>
    /// Gets user selector control.
    /// </summary>
    public FormEngineUserControl UserSelector
    {
        get
        {
            return userSelector;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        UsersPlaceHolder.Visible = CMSContext.CurrentUser.IsGlobalAdministrator;

        if (!RequestHelper.IsPostBack())
        {
            // Preselect default value
            SelectedUser = CMSContext.CurrentUser.UserID;
        }
        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsCallback())
        {
            userSelector.DropDownSingleSelect.Width = new Unit(305);
        }
        DisplayUsersFromAllSites = !(SiteID > 0);
        userSelector.TreatGlobalAdminsAsSiteUsers = !DisplayUsersFromAllSites;

        if (!RequestHelper.IsPostBack())
        {
            // Preselect default value
            SelectedUser = CMSContext.CurrentUser.UserID;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reload control data.
    /// </summary>
    public void ReloadData()
    {
        userSelector.AllowAll = CMSContext.CurrentUser.IsGlobalAdministrator;
        userSelector.ReloadData();
    }


    private string CreateWhereCondition(string originalWhere)
    {
        string where = originalWhere;
        // Add where conditions from filters
        where = SqlHelperClass.AddWhereCondition(where, pathFilter.WhereCondition);
        where = SqlHelperClass.AddWhereCondition(where, nameFilter.WhereCondition);
        if (!string.IsNullOrEmpty(classFilter.WhereCondition))
        {
            where = SqlHelperClass.AddWhereCondition(where, "VersionClassID IN (SELECT ClassID FROM CMS_Class WHERE " + classFilter.WhereCondition + ")");
        }

        int userId = ValidationHelper.GetInteger(userSelector.Value, 0);
        if (userId > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "VersionDeletedByUserID = " + userId);
        }
        return where;
    }

    #endregion


    #region "Control events"

    protected void btnShow_Click(object sender, EventArgs e)
    {
        SelectedUser = ValidationHelper.GetInteger(userSelector.Value, CMSContext.CurrentUser.UserID);
    }

    #endregion
}