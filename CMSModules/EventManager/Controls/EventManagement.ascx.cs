using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.EventManager;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_EventManager_Controls_EventManagement : CMSAdminControl
{
    #region "Properties"

    /// <summary>
    /// Site name filter.
    /// </summary>
    public string SiteName
    {
        get
        {
            return eventList.SiteName;
        }
        set
        {
            eventList.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return eventList.OrderBy;
        }
        set
        {
            eventList.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the value of items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return eventList.ItemsPerPage;
        }
        set
        {
            eventList.ItemsPerPage = value;
        }
    }


    /// <summary>
    /// Event date filter.
    /// </summary>
    public string EventScope
    {
        get
        {
            return eventList.EventScope;
        }
        set
        {
            eventList.EventScope = value;
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
            eventList.StopProcessing = value;
            emailSender.StopProcessing = value;
            attendeesList.StopProcessing = value;
        }
    }

    #endregion 


    #region "Methods" 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hdnEventID.Value) && (eventList.SelectedEventID == 0))
        {
            eventList.SelectedEventID =ValidationHelper.GetInteger (hdnEventID.Value,0);
            attendeesList.EventID = eventList.SelectedEventID;
            emailSender.EventID = eventList.SelectedEventID;            
        }

        lnkEditBack.Click += new EventHandler(lnkEditBack_Click);
        eventList.UsePostBack = true;

        //Tabs creation
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("Events_Attendee_List.General");
        tabs[0, 1] = "";
        tabs[1, 0] = GetString("Events_Edit.SendEmail");
        tabs[1, 1] = "";

        tabControlElem.Tabs = tabs;
        tabControlElem.UsePostback = true;
        attendeesList.OnCheckPermissions += new CheckPermissionsEventHandler(attendeesList_OnCheckPermissions);
        emailSender.OnCheckPermissions += new CheckPermissionsEventHandler(attendeesList_OnCheckPermissions);
    }

 
    /// <summary>
    /// Check permissions.
    /// </summary>
    /// <param name="permissionType">Permission</param>
    /// <param name="sender">Sender</param>
    void attendeesList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    
    /// <summary>
    /// Breadcrumbs back clicked.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    void lnkEditBack_Click(object sender, EventArgs e)
    {
        eventList.SelectedEventID = 0;
        hdnEventID.Value = String.Empty;
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (eventList.SelectedEventID != 0)
        {
            eventList.Visible = false;
            eventList.StopProcessing = true;
            pnlAttendees.Visible = true;
            hdnEventID.Value = eventList.SelectedEventID.ToString();
            attendeesList.EventID = eventList.SelectedEventID;
            emailSender.EventID = eventList.SelectedEventID;            
            SetBreadcrumbs();
        }
        else
        {
            eventList.Visible = true;
            pnlAttendees.Visible = false;
            attendeesList.StopProcessing = true;
            emailSender.StopProcessing = true;
            eventList.ReloadData();
            attendeesList.Reset();
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Sets breadcrumbs.
    /// </summary>
    private void SetBreadcrumbs()
    {
        lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";
        lnkEditBack.Text = GetString("Events_Edit.itemlistlink");

        string eventCapacity = "0";
        string eventTitle = "";
        string registeredAttendees = null;

        DataSet ds = EventProvider.GetEvent(eventList.SelectedEventID,  "EventCapacity, EventName, AttendeesCount");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            eventCapacity = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["EventCapacity"], 0).ToString();
            eventTitle = ValidationHelper.GetString(ds.Tables[0].Rows[0]["EventName"], "");
            registeredAttendees = ValidationHelper.GetString(ds.Tables[0].Rows[0]["AttendeesCount"], "");
        }

        if (ValidationHelper.GetInteger(eventCapacity, 0) > 0)
        {
            lblEditNew.Text = String.Format(GetString("Events_Edit.RegisteredAttendeesOfCapacity"), eventTitle, registeredAttendees, eventCapacity);
        }
        else
        {
            lblEditNew.Text = String.Format(GetString("Events_Edit.RegisteredAttendeesNoLimit"), eventTitle, registeredAttendees);
        }
    }


    protected void tabControlElem_clicked(object sender, EventArgs e)
    {
        int selectedTab = tabControlElem.SelectedTab;
        if (selectedTab == 1)
        {
            attendeesList.Visible = false;
            emailSender.Visible = true;
            emailSender.ReloadData(true);
        }
        else
        {
            attendeesList.Visible = true;
            emailSender.Visible = false;
            attendeesList.Reset();
        }
    }

    #endregion
}
