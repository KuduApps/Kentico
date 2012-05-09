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

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.FormControls;
using CMS.EventLog;

public partial class CMSModules_EventLog_Controls_EventFilter : CMSUserControl
{
    #region "Variables"

    private int mSiteId = 0;
    private bool isAdvancedMode = false;

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

    #endregion


    #region "Page methods"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!URLHelper.IsPostback())
        {
            drpEventLogType.Value = QueryHelper.GetString("type", null);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeForm();

        // Show correct filter panel
        EnsureFilterMode();
        plcAdvancedSearch.Visible = isAdvancedMode;
    }

    #endregion


    #region "UI methods"

    /// <summary>
    /// Shows/hides all elements for advanced or simple mode.
    /// </summary>
    /// <param name="showAdvanced"></param>
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
        this.lnkShowAdvancedFilter.Text = GetString("user.filter.showadvanced");
        this.imgShowAdvancedFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        this.lnkShowSimpleFilter.Text = GetString("user.filter.showsimple");
        this.imgShowSimpleFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortUp.png");
        plcAdvancedSearch.Visible = isAdvancedMode;
        pnlAdvanced.Visible = isAdvancedMode;
        pnlSimple.Visible = !isAdvancedMode;
    }


    /// <summary>
    /// Ensures correct filter mode flag if filter mode was just changed.
    /// </summary>
    private void EnsureFilterMode()
    {
        if (URLHelper.IsPostback())
        {
            // Get current event target
            string uniqieId = ValidationHelper.GetString(Request.Params["__EVENTTARGET"], String.Empty);
            uniqieId = uniqieId.Replace("$", "_");

            // If postback was fired by mode switch, update isAdvancedMode variable
            if (uniqieId == lnkShowAdvancedFilter.ClientID)
            {
                isAdvancedMode = true;
            }
            else if (uniqieId == lnkShowSimpleFilter.ClientID)
            {
                isAdvancedMode = false;
            }
            else
            {
                isAdvancedMode = ValidationHelper.GetBoolean(ViewState["IsAdvancedMode"], false);
            }
        }
    }


    /// <summary>
    /// Sets the advanced mode.
    /// </summary>
    protected void lnkShowAdvancedFilter_Click(object sender, EventArgs e)
    {
        isAdvancedMode = true;
        ViewState["IsAdvancedMode"] = isAdvancedMode;
        ShowFilterElements(isAdvancedMode);
    }


    /// <summary>
    /// Sets the simple mode.
    /// </summary>
    protected void lnkShowSimpleFilter_Click(object sender, EventArgs e)
    {
        isAdvancedMode = false;
        ViewState["IsAdvancedMode"] = isAdvancedMode;
        ShowFilterElements(isAdvancedMode);
    }

    #endregion


    #region "Search methods - where condition"

    /// <summary>
    /// Generates complete filter where condition.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        // Get mode from viewstate
        EnsureFilterMode();

        string whereCond = "";

        // Create WHERE condition for basic filter
        if (!String.IsNullOrEmpty(drpEventLogType.Value.ToString()))
        {
            whereCond = "EventType='" + drpEventLogType.Value + "'";
        }

        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltSource.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEventCode.GetCondition());
        whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltTimeBetween.GetCondition());

        // Create WHERE condition for advanced filter (id needed)
        if (isAdvancedMode)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltUserName.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltIPAddress.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltDocumentName.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltMachineName.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEventURL.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltEventURLRef.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltDescription.GetCondition());
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, fltUserAgent.GetCondition());
        }

        // Append site condition if siteid given
        if (!String.IsNullOrEmpty(whereCond) && (this.SiteID >= 0))
        {
            whereCond += " AND ";
        }

        if (this.SiteID > 0)
        {
            whereCond += " (SiteID=" + this.SiteID.ToString() + ")";
        }
        else if (this.SiteID == 0)
        {
            whereCond += " (SiteID IS NULL)";
        }

        return whereCond;
    }

    #endregion
}