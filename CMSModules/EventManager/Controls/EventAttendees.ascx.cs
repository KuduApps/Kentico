using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.EventManager;
using CMS.LicenseProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;



public partial class CMSModules_EventManager_Controls_EventAttendees : CMSAdminControl
{
    #region "Variables"

    private int mEventID = 0;
    protected int attendeeId = 0;
    protected EventAttendeeInfo eai = null;

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
            attendeesList.StopProcessing = value;
            attendeeEdit.StopProcessing = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        lnkEditBack.Click += new EventHandler(lnkEditBack_Click);   

        // New item link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("Events_Attendee_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_EventManager/addattendee.png");
        actions[0, 6] = "new_attendee";

        // Load edit attendee id from hiddenfield
        if (!String.IsNullOrEmpty(hdnState.Value))
        {
            attendeeEdit.EventID = EventID;
            attendeeEdit.AttendeeID = ValidationHelper.GetInteger(hdnState.Value, 0);
        }

        this.actionsElem.Actions = actions;
        this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

        attendeesList.UsePostback = true;
        attendeeEdit.UsePostBack = true;
        attendeesList.EventID = EventID;
        attendeeEdit.OnCheckPermissions += new CheckPermissionsEventHandler(attendeeEdit_OnCheckPermissions);
        attendeesList.OnCheckPermissions += new CheckPermissionsEventHandler(attendeeEdit_OnCheckPermissions);
    }


    /// <summary>
    /// Check permissions.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="sender">Sender</param>
    void attendeeEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    
    /// <summary>
    /// Creates breadcumbs.
    /// </summary>
    public void CreateBreadCrumbs()
    {
        // Breadcrumbs
        lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";
        lnkEditBack.Text = GetString("events_attendee_list.general");

        EventAttendeeInfo eai = EventAttendeeInfoProvider.GetEventAttendeeInfo(ValidationHelper.GetInteger(hdnState.Value, 0));
        if (eai != null)
        {
            lblEditNew.Text = eai.AttendeeEmail;
        }
        else
        {
            lblEditNew.Text = GetString("events_attendee_edit.newitemcaption");
        }
    }


    /// <summary>
    /// New attendee click handler.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "new_attendee":
                hdnState.Value = "0";
                break;
        }
    }


    /// <summary>
    /// Rest info about attendee selection.
    /// </summary>
    public void Reset()
    {
        hdnState.Value = String.Empty;
    }


    /// <summary>
    /// Breadcrumbs clicked.
    /// </summary>
    /// <param name="sender">Sender</param>
    void lnkEditBack_Click(object sender, EventArgs e)
    {
        hdnState.Value = String.Empty;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (attendeeEdit.NewItemID != 0)
        {
            hdnState.Value = attendeeEdit.NewItemID.ToString();
        }

        if (attendeesList.SelectedAttendeeID != 0)
        {
            hdnState.Value = attendeesList.SelectedAttendeeID.ToString();
        }

        if (String.IsNullOrEmpty(hdnState.Value))
        {
            pnlEdit.Visible = false;
            pnlList.Visible = true;
            attendeesList.EventID = EventID;
            attendeesList.ReloadData();
        }
        else
        {
            attendeesList.StopProcessing = true;
            pnlList.Visible = false;
            pnlEdit.Visible = true;
            attendeeEdit.AttendeeID = ValidationHelper.GetInteger(hdnState.Value, 0);
            attendeeEdit.EventID = EventID;
            attendeeEdit.LoadEditData();
            CreateBreadCrumbs();
        }
    }

    #endregion
}


   
