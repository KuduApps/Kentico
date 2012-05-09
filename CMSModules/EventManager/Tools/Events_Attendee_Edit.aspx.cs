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
using CMS.CMSHelper;
using CMS.EventManager;
using CMS.UIControls;

public partial class CMSModules_EventManager_Tools_Events_Attendee_Edit : CMSEventManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int eventNodeId = QueryHelper.GetInteger("eventId", 0);
        int attendeeId = QueryHelper.GetInteger("attendeeId", 0);
        attendeeEdit.EventID = eventNodeId;
        attendeeEdit.AttendeeID = attendeeId;
        attendeeEdit.Saved = QueryHelper.GetBoolean("saved", false);

        string attEmail = GetString("Events_Attendee_Edit.NewItemCaption");
        EventAttendeeInfo eai = null;

        if (attendeeId > 0)
        {
            eai = EventAttendeeInfoProvider.GetEventAttendeeInfo(attendeeId);
            EditedObject = eai;
        }
        if (eai != null)
        {
            attEmail = eai.AttendeeEmail;
        }

        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Events_Attendee_Edit.itemlistlink");
        breadcrumbs[0, 1] = "~/CMSModules/EventManager/Tools/Events_Attendee_List.aspx?eventid=" + attendeeEdit.EventID;
        breadcrumbs[0, 2] = "eventsContent";
        breadcrumbs[1, 0] = attEmail;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "attendees_edit";

        attendeeEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(attendeeEdit_OnCheckPermissions);
    }


    void attendeeEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EventManager", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.EventManager", permissionType);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        attendeeEdit.LoadEditData();
        base.OnPreRender(e);
    }
}
