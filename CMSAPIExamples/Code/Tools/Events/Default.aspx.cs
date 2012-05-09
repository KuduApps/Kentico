using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.EventManager;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.PortalEngine;

[Title(Text = "Events", ImageUrl = "CMSModules/CMS_EventManager/module.png")]
public partial class CMSAPIExamples_Code_Tools_Events_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Event
        this.apiCreateEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateEvent);
        this.apiGetAndUpdateEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateEvent);
        this.apiDeleteEvent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteEvent);

        // Attendee
        this.apiCreateAttendee.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAttendee);
        this.apiGetAndUpdateAttendee.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateAttendee);
        this.apiGetAndBulkUpdateAttendees.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateAttendees);
        this.apiDeleteAttendee.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteAttendee);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Event
        this.apiCreateEvent.Run();
        this.apiGetAndUpdateEvent.Run();

        // Attendee
        this.apiCreateAttendee.Run();
        this.apiGetAndUpdateAttendee.Run();
        this.apiGetAndBulkUpdateAttendees.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Attendee
        this.apiDeleteAttendee.Run();

        // Event
        this.apiDeleteEvent.Run();
    }

    #endregion


    #region "API examples - Event"

    /// <summary>
    /// Creates new document under root and new event (booking system) document under created document.
    /// Called when the "Create event" button is pressed.
    /// </summary>
    private bool CreateEvent()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get root document
        TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);

        // Create a new CMS Page (menu item) document 
        TreeNode node = TreeNode.New("CMS.MenuItem", tree);

        // Set values
        node.DocumentName = "MyNewDocument";
        node.DocumentCulture = CMSContext.PreferredCultureCode;

        // Get page template
        PageTemplateInfo template = PageTemplateInfoProvider.GetPageTemplateInfo("cms.empty");
        if (template != null)
        {
            node.DocumentPageTemplateID = template.PageTemplateId;
        }

        // Insert the document
        DocumentHelper.InsertDocument(node, root.NodeID, tree);

        // Create new Event (booking system) document
        TreeNode eventNode = TreeNode.New("CMS.BookingEvent", tree);

        // Set values
        eventNode.DocumentName = "MyNewEvent";
        eventNode.DocumentCulture = CMSContext.PreferredCultureCode;
        eventNode.SetValue("EventSummary", "My event summary");
        eventNode.SetValue("EventDetails", "My event details");
        eventNode.SetValue("EventLocation", "My location");
        eventNode.SetValue("EventDate", DateTime.Now);
        eventNode.SetValue("EventCapacity", 100);

        // Get page template
        PageTemplateInfo eventTemplate = PageTemplateInfoProvider.GetPageTemplateInfo("cms.empty");
        if (eventTemplate != null)
        {
            eventNode.DocumentPageTemplateID = eventTemplate.PageTemplateId;
        }

        // Insert the Event (booking system) document
        DocumentHelper.InsertDocument(eventNode, node.NodeID, tree);

        return true;
    }


    /// <summary>
    /// Gets and updates event. Called when the "Get and update event" button is pressed.
    /// Expects the CreateEvent method to be run first.
    /// </summary>
    private bool GetAndUpdateEvent()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get event document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument/MyNewEvent", null, true);

        if (node != null)
        {
            // Update value
            node.SetValue("EventDetails", "My event details were updated.");
            node.SetValue("EventCapacity", 200);
            DocumentHelper.UpdateDocument(node, tree);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes event. Called when the "Delete event" button is pressed.
    /// Expects the CreateEvent method to be run first.
    /// </summary>
    private bool DeleteEvent()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get event document
        TreeNode eventNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument/MyNewEvent", null, true);

        // Get events parent document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument", null, true);

        if (eventNode != null && node != null)
        {
            // Delete event document
            DocumentHelper.DeleteDocument(eventNode, tree, true, true, true);

            // Delete document
            DocumentHelper.DeleteDocument(node, tree, true, true, true);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Attendee"

    /// <summary>
    /// Creates attendee. Called when the "Create attendee" button is pressed.
    /// Expects the CreateEvent method to be run first.
    /// </summary>
    private bool CreateAttendee()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get event document
        TreeNode eventNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument/MyNewEvent", null, true);

        if (eventNode != null)
        {
            // Create new attendee object
            EventAttendeeInfo newAttendee = new EventAttendeeInfo();

            // Set the properties
            newAttendee.AttendeeEmail = "MyNewAttendee@localhost.local";
            newAttendee.AttendeeEventNodeID = eventNode.NodeID;
            newAttendee.AttendeeFirstName = "My firstname";
            newAttendee.AttendeeLastName = "My lastname";

            // Save the attendee
            EventAttendeeInfoProvider.SetEventAttendeeInfo(newAttendee);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates attendee. Called when the "Get and update attendee" button is pressed.
    /// Expects the CreateAttendee method to be run first.
    /// </summary>
    private bool GetAndUpdateAttendee()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get event document
        TreeNode eventNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument/MyNewEvent", null, true);

        if (eventNode != null)
        {
            // Get the attendee
            EventAttendeeInfo updateAttendee = EventAttendeeInfoProvider.GetEventAttendeeInfo(eventNode.NodeID, "MyNewAttendee@localhost.local");
            if (updateAttendee != null)
            {
                // Update the properties
                updateAttendee.AttendeeEmail = updateAttendee.AttendeeEmail.ToLower();

                // Save the changes
                EventAttendeeInfoProvider.SetEventAttendeeInfo(updateAttendee);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates attendees. Called when the "Get and bulk update attendees" button is pressed.
    /// Expects the CreateAttendee method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateAttendees()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get event document
        TreeNode eventNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument/MyNewEvent", null, true);

        if (eventNode != null)
        {
            // Prepare the parameters
            string where = "AttendeeEmail LIKE N'MyNewAttendee%'";

            // Get the data
            DataSet attendees = EventAttendeeInfoProvider.GetEventAttendees(eventNode.NodeID, where, null, null, 0);

            if (!DataHelper.DataSourceIsEmpty(attendees))
            {
                // Loop through the individual items
                foreach (DataRow attendeeDr in attendees.Tables[0].Rows)
                {
                    // Create object from DataRow
                    EventAttendeeInfo modifyAttendee = new EventAttendeeInfo(attendeeDr);

                    // Update the properties
                    modifyAttendee.AttendeeEmail = modifyAttendee.AttendeeEmail.ToUpper();

                    // Save the changes
                    EventAttendeeInfoProvider.SetEventAttendeeInfo(modifyAttendee);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes attendee. Called when the "Delete attendee" button is pressed.
    /// Expects the CreateAttendee method to be run first.
    /// </summary>
    private bool DeleteAttendee()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get event document
        TreeNode eventNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/MyNewDocument/MyNewEvent", null, true);

        if (eventNode != null)
        {
            // Get the attendee
            EventAttendeeInfo deleteAttendee = EventAttendeeInfoProvider.GetEventAttendeeInfo(eventNode.NodeID, "MyNewAttendee@localhost.local");

            // Delete the attendee
            EventAttendeeInfoProvider.DeleteEventAttendeeInfo(deleteAttendee);

            return (deleteAttendee != null);
        }

        return false;
    }

    #endregion
}