using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.EventLog;

[Title(Text = "Event log", ImageUrl = "Objects/CMS_EventLog/object.png")]
public partial class CMSAPIExamples_Code_Administration_EventLog_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Event log
        this.apiLogEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(LogEvent);
        this.apiGetAndUpdateEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateEvent);
        this.apiGetAndBulkUpdateEvents.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateEvents);
        this.apiClearLog.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(ClearLog);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Event log
        this.apiLogEvent.Run();
        this.apiGetAndUpdateEvent.Run();
        this.apiGetAndBulkUpdateEvents.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Event log
        this.apiClearLog.Run();
    }

    #endregion


    #region "API examples - Event log"

    /// <summary>
    /// Log event. Called when the "Log event" button is pressed.
    /// </summary>
    private bool LogEvent()
    {
        // Create new event object
        EventLogInfo newEvent = new EventLogInfo();

        // Set the properties
        newEvent.EventType = "I";
        newEvent.EventDescription = "My new logged event.";
        newEvent.EventCode = "APIEXAMPLE";
        newEvent.EventTime = DateTime.Now;
        newEvent.Source = "API Example";
        newEvent.SiteID = CMSContext.CurrentSiteID;

        // Create new instance of event log provider
        EventLogProvider eventLog = new EventLogProvider();

        // Log the event
        eventLog.LogEvent(newEvent);

        return true;
    }


    /// <summary>
    /// Gets and updates abuse report. Called when the "Get and update report" button is pressed.
    /// Expects the LogEvent method to be run first.
    /// </summary>
    private bool GetAndUpdateEvent()
    {
        // Create new instance of event log provider
        EventLogProvider eventLog = new EventLogProvider();

        // Get top 1 event matching the where condition
        string where = "EventCode = 'APIEXAMPLE'";
        int topN = 1;
        DataSet events = eventLog.GetAllEvents(where, null, topN, null);

        if (!DataHelper.DataSourceIsEmpty(events))
        { 
            // Create the object from DataRow
            EventLogInfo updateEvent = new EventLogInfo(events.Tables[0].Rows[0]);
            
            // Update the properties
            updateEvent.EventDescription = updateEvent.EventDescription.ToLower();
                        
            // Save the changes
            eventLog.SetEventLogInfo(updateEvent);
                        
            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates eventss. Called when the "Get and bulk update events" button is pressed.
    /// Expects the LogEvent method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateEvents()
    {
        // Create new instance of event log provider
        EventLogProvider eventLog = new EventLogProvider();

        // Get events matching the where condition
        string where = "EventCode = 'APIEXAMPLE'";
        DataSet events = eventLog.GetAllEvents(where, null);

        if (!DataHelper.DataSourceIsEmpty(events))
        {
            // Loop through the individual items
            foreach (DataRow eventDr in events.Tables[0].Rows)
            {
                // Create the object from DataRow
                EventLogInfo updateEvent = new EventLogInfo(eventDr);

                // Update the properties
                updateEvent.EventDescription = updateEvent.EventDescription.ToUpper();

                // Save the changes
                eventLog.SetEventLogInfo(updateEvent);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Clears event log for current site. Called when the "Clear event log" button is pressed.
    /// Expects the CreateAbuseReport method to be run first.
    /// </summary>
    private bool ClearLog()
    {
        // Create new instance of event log provider
        EventLogProvider eventLog = new EventLogProvider();

        // Clear event log for current site
        eventLog.ClearEventLog(CMSContext.CurrentUser.UserID, CMSContext.CurrentUser.UserName, HTTPHelper.UserHostAddress, CMSContext.CurrentSiteID);

        return true;
    }

    #endregion
}
