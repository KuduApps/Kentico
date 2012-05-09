using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_EventLog_Controls_EventLog : CMSAdminControl, ICallbackEventHandler
{
    #region "Variables"

    private int mSiteId = -1;
    private int mEventId = 0;
    private EventLogProvider eventProvider = new EventLogProvider();
    private Hashtable mParameters = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the site id which the event log is to be displayed
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
            cntFilter.SiteID = value;
        }
    }


    /// <summary>
    /// Gets or sets the event id
    /// </summary>
    public int EventID
    {
        get
        {
            return mEventId;
        }
        set
        {
            mEventId = value;
        }
    }


    /// <summary>
    /// Gets or sets the visibility of the filter.
    /// </summary>
    public bool ShowFilter
    {
        get
        {
            return cntFilter.Visible;
        }
        set
        {
            cntFilter.Visible = value;
            cntFilter.EnableViewState = value;
            plcFilter.Visible = value;
        }
    }


    /// <summary>
    /// Gets the event log uniGrid.
    /// </summary>
    public UniGrid EventLogGrid
    {
        get
        {
            return gridEvents;
        }
    }


    /// <summary>
    /// Dialog control identificator
    /// </summary>
    private string Identificator
    {
        get
        {
            string identificator = hdnIdentificator.Value;
            if (string.IsNullOrEmpty(identificator))
            {
                identificator = Guid.NewGuid().ToString();
                hdnIdentificator.Value = identificator;
            }

            return identificator;
        }
    }

    #endregion


    #region "Stop processing"

    /// <summary>
    /// Returns true if the control processing should be stopped
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
            gridEvents.StopProcessing = value;
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Initializes the control properties
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            // System control properties
            gridEvents.ReloadData();
        }
    }


    /// <summary>
    /// Reloads the control data
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Updates the update panel of this control.
    /// </summary>
    public void Update()
    {
        pnlUpdate.Update();
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // check access permissions
        CheckPermissions("CMS.EventLog", CMSAdminControl.PERMISSION_READ);

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this.Page);

        string openEventDetailScript = "function OpenEventDetail(queryParameters) {\n" +
                                       "modalDialog(" + ScriptHelper.GetString(ResolveUrl("~/CMSModules/EventLog/EventLog_Details.aspx")) + " + queryParameters, 'eventdetails', 920, 700);\n" +
                                       "}";

        // Register the dialog script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EventLog_OpenDetail", ScriptHelper.GetScript(openEventDetailScript));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EventLog_" + ClientID, ScriptHelper.GetScript("var eventDialogParams_" + ClientID + " = '';"));

        gridEvents.GridView.RowDataBound += GridView_RowDataBound;
        gridEvents.OnExternalDataBound += gridEvents_OnExternalDataBound;
        gridEvents.Columns = "EventID,EventType,EventTime,Source,EventCode,UserName,IPAddress,DocumentName,SiteID,EventMachineName";
        if (!RequestHelper.IsPostBack())
        {
            if (String.IsNullOrEmpty(gridEvents.OrderBy))
            {
                // if not set externally => set defaults
                gridEvents.OrderBy = "EventTime DESC, EventID DESC";
            }
        }

        cntFilter.SiteID = SiteID;

        if (gridEvents.WhereCondition == null)
        {
            gridEvents.WhereCondition = cntFilter.WhereCondition;
        }
    }

    #endregion


    #region "UI Handlers"

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string color = null;
            string code = ValidationHelper.GetString(((DataRowView)(e.Row.DataItem)).Row["EventType"], string.Empty);
            switch (code.ToLower())
            {
                case "e":
                    color = ((e.Row.RowIndex & 1) == 1) ? "#EEC9C9" : "#FFDADA";
                    break;

                case "w":
                    color = ((e.Row.RowIndex & 1) == 1) ? "#EEEEC9" : "#FFFFDA";
                    break;
            }

            if (!string.IsNullOrEmpty(color))
            {
                e.Row.Style.Add("background-color", color);
            }
        }
    }


    protected object gridEvents_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "eventtype":
                {
                    string evetType = ValidationHelper.GetString(parameter, "");
                    return "<div style=\"width:100%;text-align:center;cursor:help;\" title=\"" + HTMLHelper.HTMLEncode(EventLogHelper.GetEventTypeText(evetType)) + "\">" + evetType + " </div>";
                }

            case "formattedusername":
                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(Convert.ToString(parameter)));

            case "view":
                {
                    if (sender is ImageButton)
                    {
                        ImageButton img = sender as ImageButton;
                        DataRowView drv = UniGridFunctions.GetDataRowView(img.Parent as DataControlFieldCell);
                        int eventId = ValidationHelper.GetInteger(drv["EventID"], 0);
                        img.AlternateText = GetString("Unigrid.EventLog.Actions.Display");
                        img.ToolTip = GetString("Unigrid.EventLog.Actions.Display");
                        img.OnClientClick = "eventDialogParams_" + ClientID + " = '" + eventId + "';" + Page.ClientScript.GetCallbackEventReference(this, "eventDialogParams_" + ClientID, "OpenEventDetail", null) + ";return false;";                        
                    }
                    return sender;
                }
        }

        return parameter;
    }

    #endregion


    #region "ICallbackEventHandler Members"

    /// <summary>
    /// Get callback result
    /// </summary>
    public string GetCallbackResult()
    {
        string orderby = this.gridEvents.SortDirect;
        string whereCondition = this.gridEvents.WhereCondition;

        mParameters = new Hashtable();
        mParameters["where"] = whereCondition;
        mParameters["orderby"] = this.gridEvents.SortDirect;

        WindowHelper.Add(Identificator, mParameters);

        string queryString = "?params=" + Identificator;

        if (SiteID > 0)
        {
            queryString = URLHelper.AddParameterToUrl(queryString, "siteid", SiteID.ToString());
        }
        queryString = URLHelper.AddParameterToUrl(queryString, "hash", QueryHelper.GetHash(queryString));
        queryString = URLHelper.AddParameterToUrl(queryString, "eventid", EventID.ToString());

        return queryString;
    }


    /// <summary>
    /// Raise callback method
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        EventID = ValidationHelper.GetInteger(eventArgument, 0);
    }

    #endregion
}
