using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.EventManager;
using CMS.TreeEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_EventManager_Controls_EventAttendees_List : CMSAdminControl
{
    #region "Variables"

    private int mEventID;
    private bool mUsePostback;
    private int mSelectedAttendeeID;

    #endregion 


    #region "Properties"

    /// <summary>
    /// Attendees' EventID.
    /// </summary>
    public int EventID
    {
        get
        {
            return mEventID;
        }
        set
        {
            mEventID = value;
        }
    }


    /// <summary>
    /// Use postback instead of redirect.
    /// </summary>
    public bool UsePostback
    {
        get
        {
            return mUsePostback;
        }
        set
        {
            mUsePostback = value;
            UniGrid.DelayedReload = value;
        }
    }


    /// <summary>
    /// ID of edited attendee.
    /// </summary>
    public int SelectedAttendeeID
    {
        get
        {
            return mSelectedAttendeeID;
        }
        set
        {
            mSelectedAttendeeID = value;
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
            UniGrid.StopProcessing = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Script for UniGri's edit action 
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EditAttendee",
            ScriptHelper.GetScript("function EditAttendee(attendeeId){" +
            "location.replace('Events_Attendee_Edit.aspx?attendeeid=' + attendeeId + '&eventid=" + EventID + "'); }"));

        // Refresh parent frame header
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshHeader",
            ScriptHelper.GetScript("function RefreshHeader() {if (parent.frames['eventsHeader']) { " +
            "parent.frames['eventsHeader'].location.replace(parent.frames['eventsHeader'].location); }} \n"));

        //Unigrid settings
        UniGrid.OnAction += new OnActionEventHandler(UniGrid_OnAction);
        UniGrid.ZeroRowsText = GetString("Events_List.NoAttendees");
        UniGrid.HideControlForZeroRows = false;

        if (UsePostback)
        {
            UniGrid.GridName = "~/CMSModules/EventManager/Tools/Events_Attendee_List_Control.xml";
        }
        else
        {
            UniGrid.GridName = "~/CMSModules/EventManager/Tools/Events_Attendee_List.xml";
        }

        if (EventID > 0)
        {
            UniGrid.WhereCondition = "AttendeeEventNodeId = " + EventID;
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGrid_OnAction(string actionName, object actionArgument)
    {
        // Check 'Modify' permission (because of delete action in unigrid)
        if (!CheckPermissions("cms.eventmanager", "Modify"))
        {
            return;
        }

        switch (actionName)
        {
            case "delete":
                EventAttendeeInfoProvider.DeleteEventAttendeeInfo(ValidationHelper.GetInteger(actionArgument, 0));
                // Refresh parent frame header
                ltlScript.Text = ScriptHelper.GetScript("RefreshHeader();");
                UniGrid.ReloadData();
                break;

            case "sendemail":
                // Resend invitation email
                TreeProvider mTree = new TreeProvider(CMSContext.CurrentUser);
                TreeNode node = mTree.SelectSingleNode(EventID);

                EventAttendeeInfo eai = EventAttendeeInfoProvider.GetEventAttendeeInfo(ValidationHelper.GetInteger(actionArgument, 0));

                if ((node != null) && (node.NodeClassName.Equals("cms.bookingevent", StringComparison.InvariantCultureIgnoreCase)) && (eai != null))
                {
                    EventProvider.SendInvitation(CMSContext.CurrentSiteName, node, eai, TimeZoneHelper.ServerTimeZone);

                    lblInfo.Text = GetString("eventmanager.invitationresend");
                    lblInfo.Visible = true;
                }
                break;

            case "edit":
                SelectedAttendeeID = ValidationHelper.GetInteger(actionArgument, 0);
                break;
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        UniGrid.WhereCondition = "AttendeeEventNodeId = " + EventID;
        UniGrid.ReloadData();
    }

    #endregion
}
