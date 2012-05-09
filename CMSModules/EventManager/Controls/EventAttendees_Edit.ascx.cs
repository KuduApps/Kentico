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

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_EventManager_Controls_EventAttendees_Edit : CMSAdminControl
{
    #region "Variables"

    private int mEventID = 0;
    protected int mAttendeeID = 0;
    private bool mSaved = false;
    private bool mUsePostBack = false;
    private int mNewItemID = 0;

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
    /// Saved.
    /// </summary>
    public bool Saved
    {
        get
        {
            return mSaved;
        }
        set
        {
            mSaved = value;
        }
    }


    /// <summary>
    /// Attendee ID.
    /// </summary>
    public int AttendeeID
    {
        get
        {
            return mAttendeeID;
        }
        set
        {
            mAttendeeID = value;
        }
    }


    /// <summary>
    /// If true dont use redirect.
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
    /// After new item is created this stores its ID.
    /// </summary>
    public int NewItemID
    {
        get
        {
            return mNewItemID;
        }
        set
        {
            mNewItemID = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshHeader",
    ScriptHelper.GetScript("function RefreshHeader(attendeeId, duplicit) {if (parent.frames['eventsHeader']) { " +
    "parent.frames['eventsHeader'].location.replace(parent.frames['eventsHeader'].location);location.replace(location + '&attendeeId=' + attendeeId +'&saved=1&duplicit=' + duplicit); }} \n"));

        lblFirstName.Text = GetString("Event_Attendee_Edit.lblFirstName");
        lblLastName.Text = GetString("Event_Attendee_Edit.lblLastName");
        lblPhone.Text = GetString("Event_Attendee_Edit.lblPhone");
        btnOk.Text = GetString("general.ok");

        rfvEmail.Text = GetString("Event_Attendee_Edit.rfvEmail");

        if (Saved)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }


    /// <summary>
    /// Checks if current event (EventID) exist and hides control content if not.
    /// Returns true if event exist, false otherwise.
    /// </summary>
    private bool CheckIfEventExists()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode nd = tree.SelectSingleNode(EventID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture, false);
        if (nd == null)
        {
            lblError.Visible = true;
            lblError.Text = GetString("editedobject.notexists");
            pnlContent.Visible = false;
            return false;
        }
        return true;
    }


    /// <summary>
    /// Loads data for edit form.
    /// </summary>
    public void LoadEditData()
    {
        // Check if current event exist
        if (!CheckIfEventExists())
        {
            return;
        }

        EventAttendeeInfo eai = null;
        if (AttendeeID > 0)
        {
            eai = EventAttendeeInfoProvider.GetEventAttendeeInfo(AttendeeID);
        }
       
        if (String.IsNullOrEmpty(lblError.Text))
        {
            if (eai != null)
            {
                txtFirstName.Text = eai.AttendeeFirstName;
                txtLastName.Text = eai.AttendeeLastName;
                txtEmail.Text = eai.AttendeeEmail;
                txtPhone.Text = eai.AttendeePhone;

                // Show warning if duplicit email was used
                if ((QueryHelper.GetBoolean("duplicit", false)) || (ValidationHelper.GetBoolean(hdnDuplicit.Value, false)))
                {
                    hdnDuplicit.Value = "false";
                    lblInfo.Visible = true;
                    lblInfo.Text = lblInfo.Text + "<br />" + GetString("eventmanager.attendeeregisteredwarning");
                }
            }
            else
            {
                txtFirstName.Text = String.Empty;
                txtLastName.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtPhone.Text = String.Empty;
            }
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check if current event exist
        if (!CheckIfEventExists())
        {
            return;
        }

        // Check 'Modify' permission        
        if (!CheckPermissions("cms.eventmanager", "Modify"))
        {
            return;            
        }

        txtEmail.Text = txtEmail.Text.Trim();

        // Validate fields
        string errorMessage = new Validator()
            .NotEmpty(txtEmail.Text, rfvEmail.ErrorMessage)
            .IsEmail(txtEmail.Text, GetString("Event_Attendee_Edit.IncorectEmailFormat"))
            .Result;

        // Update database
        if (errorMessage == "")
        {
            // Indicates new attendee
            bool isNew = false;

            // Indicates duplicit attendee/email
            bool isDuplicit = false;

            EventAttendeeInfo eai = null;
            if (AttendeeID == 0)
            {
                eai = new EventAttendeeInfo();
                eai.AttendeeEventNodeID = EventID;
                isNew = true;
                isDuplicit = EventAttendeeInfoProvider.GetEventAttendeeInfo(EventID, txtEmail.Text) != null;
            }
            else
            {
                eai = EventAttendeeInfoProvider.GetEventAttendeeInfo(AttendeeID);
            }

            if (eai != null)
            {
                eai.AttendeeFirstName = txtFirstName.Text;
                eai.AttendeeLastName = txtLastName.Text;
                eai.AttendeeEmail = txtEmail.Text;
                eai.AttendeePhone = txtPhone.Text;
                EventAttendeeInfoProvider.SetEventAttendeeInfo(eai);

                if (!UsePostBack)
                {
                    if (!isNew)
                    {
                        URLHelper.Redirect("Events_Attendee_Edit.aspx?eventid=" + Convert.ToString(EventID) + "&attendeeId=" + Convert.ToString(eai.AttendeeID) + "&saved=true");
                    }
                    else
                    {
                        ltlScript.Text = ScriptHelper.GetScript("RefreshHeader(" + Convert.ToString(eai.AttendeeID) + ", " + (isDuplicit ? "1" : "0") + ");");
                    }
                }

                lblInfo.Text = GetString("General.ChangesSaved");
                lblInfo.Visible = true;
                hdnDuplicit.Value = isDuplicit.ToString();


                // If new item store new attendeeID .. used in postback situations
                if (isNew)
                {
                    NewItemID = eai.AttendeeID;
                }
            }
            else
            {
                errorMessage = GetString("general.invalidid");
            }
        }

        if (errorMessage != "")
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion
}
