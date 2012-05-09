using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.EventManager;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_EventManager_Tools_Events_Header : CMSEventManagerPage
{
    protected int eventNodeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the event ID
        eventNodeId = QueryHelper.GetInteger("eventId", 0);

        // Get event capacity and title
        string eventCapacity = "0";
        string eventTitle = "";
        string registeredAttendees = null;

        DataSet ds = EventProvider.GetEvent(eventNodeId, CMSContext.CurrentSiteName, "EventCapacity, EventName, AttendeesCount");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            eventCapacity = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["EventCapacity"], 0).ToString();
            eventTitle = ValidationHelper.GetString(ds.Tables[0].Rows[0]["EventName"], "");
            registeredAttendees = ValidationHelper.GetString(ds.Tables[0].Rows[0]["AttendeesCount"], "");
        }

        if (!RequestHelper.IsPostBack())
        {
            InitMenu();
        }

        // Initialize the page title
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Events_Edit.itemlistlink");
        breadcrumbs[0, 1] = "~/CMSModules/EventManager/Tools/Events_List.aspx";
        breadcrumbs[0, 2] = "_parent";

        if (ValidationHelper.GetInteger(eventCapacity, 0) > 0)
        {
            breadcrumbs[1, 0] = String.Format(GetString("Events_Edit.RegisteredAttendeesOfCapacity"), eventTitle, registeredAttendees, eventCapacity);
        }
        else
        {
            breadcrumbs[1, 0] = String.Format(GetString("Events_Edit.RegisteredAttendeesNoLimit"), eventTitle, registeredAttendees);
        }

        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.TitleText = GetString("Events_Edit.headercaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_EventManager/object.png");

        this.CurrentMaster.Title.HelpTopicName = "attendees_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitMenu()
    {
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("Events_Attendee_List.General");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'attendees_tab');";
        tabs[0, 2] = "Events_Attendee_List.aspx?eventid=" + eventNodeId;
        tabs[1, 0] = GetString("Events_Edit.SendEmail");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'send_e_mail_tab');";
        tabs[1, 2] = "Events_SendEmail.aspx?eventid=" + eventNodeId;

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "eventsContent";
    }
}
