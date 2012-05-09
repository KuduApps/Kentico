using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.TreeEngine;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_EventManager_Controls_EventList : CMSAdminControl
{
    #region "Private variables"

    private UserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;
    private bool mUsePostBack = false;
    private int mSelectedEventID = 0;
    private string mOrderBy = "EventDate DESC";
    private string mItemsPerPage = String.Empty;
    private string mEventScope = "all";
    private string mSiteName = String.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates whether change location or postback if edit.
    /// </summary>
    public bool UsePostBack
    {
        get
        {
            return mUsePostBack;
        }
        set
        {
            mUsePostBack = value;
        }
    }


    /// <summary>
    /// Site name filter.
    /// </summary>
    public string SiteName
    {
        get
        {
            return mSiteName;
        }
        set
        {
            mSiteName = value;
        }
    }


    /// <summary>
    /// ID of selected event.
    /// </summary>
    public int SelectedEventID
    {
        get
        {
            return mSelectedEventID;
        }
        set
        {
            mSelectedEventID = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return mOrderBy;
        }
        set
        {
            mOrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the value of items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return mItemsPerPage;
        }
        set
        {
            mItemsPerPage = value;
        }
    }


    /// <summary>
    /// Gets or sets event date filter.
    /// </summary>
    public string EventScope
    {
        get
        {
            return mEventScope;
        }
        set
        {
            mEventScope = value;
        }
    }


    /// <summary>
    /// Stop processing.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            gridElem.StopProcessing = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!URLHelper.IsPostback())
        {
            this.drpEventScope.Items.Add(new ListItem(GetString("general.selectall"), "all"));
            this.drpEventScope.Items.Add(new ListItem(GetString("eventmanager.eventscopeupcoming"), "upcoming"));

            this.drpEventScope.SelectedValue = QueryHelper.GetString("scope", "all");  
        }

        this.EventScope = drpEventScope.SelectedValue;

        this.btnFilter.Text = GetString("general.show");

        gridElem.HideControlForZeroRows = false;
        gridElem.ZeroRowsText = GetString("Events_List.NoBookingEvent");
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);

        if ((!RequestHelper.IsPostBack()) && (!string.IsNullOrEmpty(ItemsPerPage)))
        {
            gridElem.Pager.DefaultPageSize = ValidationHelper.GetInteger(ItemsPerPage, -1);
        }

        gridElem.OrderBy = OrderBy;
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);

        if (UsePostBack)
        {
            gridElem.GridName = "~/CMSModules/EventManager/Controls/Events_List_Control.xml";
        }
        else
        {
            gridElem.GridName = "~/CMSModules/EventManager/Controls/Events_List.xml";
        }

        SetWhereCondition();

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "editEventScript", ScriptHelper.GetScript("function EditEvent(eventId) { location.replace('Events_Edit.aspx?eventId=' + eventId); }"));
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        SetWhereCondition();
        gridElem.ReloadData();
        base.ReloadData();
    }


    private void SetWhereCondition()
    {
        // Check existence of CMS.BookingEvent dataclass
        if (DataClassInfoProvider.GetDataClass("CMS.BookingEvent") != null)
        {
            // Filter site name            
            string siteName = SiteName;
            if (siteName == String.Empty)
            {
                siteName = CMSContext.CurrentSiteName;
            }

            // If not show all
            if (siteName != TreeProvider.ALL_SITES)
            {
                gridElem.WhereCondition = "(NodeLinkedNodeID IS NULL AND SiteName LIKE '" + siteName + "')";
            }
            else
            {
                gridElem.WhereCondition = "NodeLinkedNodeID IS NULL";
            }

            // Filter time interval
            if (EventScope != "all")
            {
                if (EventScope == "upcoming")
                {
                    gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "EventDate >= @Date");
                }
                else
                {
                    gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "EventDate <= @Date");
                }

                QueryDataParameters parameters = new QueryDataParameters();
                parameters.Add("@Date", DateTime.Now);

                gridElem.QueryParameters = parameters;
            }
        }
        else
        {
            // Document type with code name 'CMS.BookingEvent' does not exist
            lblError.Visible = true;
            lblError.Text = GetString("Events_List.NoBookingEventClass");
        }
    }

    #endregion


    #region "Grid events"

    /// <summary>
    /// Manage if user edit event.
    /// </summary>
    /// <param name="actionName">Edit</param>
    /// <param name="actionArgument">Id of event</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "view":
                SelectedEventID = Convert.ToInt32(actionArgument);
                break;
        }
    }


    /// <summary>
    /// Handles data bound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string result = string.Empty;
        DataRowView data = null;

        switch (sourceName.ToLower())
        {
            // Create link to event document 
            case "documentname":
                {
                    data = (DataRowView)parameter;
                    string siteName = ValidationHelper.GetString(data["SiteName"], String.Empty);
                    string documentName = ValidationHelper.GetString(data["DocumentName"], String.Empty);
                    string culture = ValidationHelper.GetString(data["DocumentCulture"], String.Empty);
                    int nodeID = ValidationHelper.GetInteger(data["NodeID"], 0);

                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteName);
                    if (si != null)
                    {
                        // Get current app path
                        string appPath = URLHelper.ApplicationPath.TrimEnd('/');
                        string domain = si.DomainName;

                        // If domain contains app path donnt add it
                        if (domain.Contains("/"))
                        {
                            appPath = null;
                        }

                        string path = URLHelper.ResolveUrl("~/CMSDesk/default.aspx?section=content&action=edit&nodeid=" + nodeID + "&culture=" + culture);

                        return "<a target=\"_top\" href=\'" + path + "'\" >" + HTMLHelper.HTMLEncode(documentName) + "</a>";
                    }
                }
                return HTMLHelper.HTMLEncode(parameter.ToString());

            case "eventtooltip":
                data = (DataRowView)parameter;
                return UniGridFunctions.DocumentNameTooltip(data);

            case "eventdate":
            case "eventopenfrom":
            case "eventopento":
            case "eventdatetooltip":
            case "eventopenfromtooltip":
            case "eventopentotooltip":
                if (!String.IsNullOrEmpty(parameter.ToString()))
                {
                    if (currentUserInfo == null)
                    {
                        currentUserInfo = CMSContext.CurrentUser;
                    }
                    if (currentSiteInfo == null)
                    {
                        currentSiteInfo = CMSContext.CurrentSite;
                    }

                    bool displayGMT = sourceName.EndsWith("tooltip");
                    DateTime time = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    return TimeZoneHelper.ConvertToUserTimeZone(time, displayGMT, currentUserInfo, currentSiteInfo);
                }
                return result;
            case "eventenddate":
            case "eventenddatetooltip":
                data = (DataRowView)parameter;
                try
                {
                    parameter = data["eventenddate"];
                }
                catch
                {
                    parameter = null;
                }

                if ((parameter != null) && !String.IsNullOrEmpty(parameter.ToString()))
                {
                    if (currentUserInfo == null)
                    {
                        currentUserInfo = CMSContext.CurrentUser;
                    }
                    if (currentSiteInfo == null)
                    {
                        currentSiteInfo = CMSContext.CurrentSite;
                    }

                    bool displayGMT = sourceName.EndsWith("tooltip");
                    DateTime time = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    return TimeZoneHelper.ConvertToUserTimeZone(time, displayGMT, currentUserInfo, currentSiteInfo);
                }
                return result;

        }

        return parameter;
    }

    #endregion
}
