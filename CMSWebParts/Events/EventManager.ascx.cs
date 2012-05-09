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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.TreeEngine;
using CMS.EventManager;
using CMS.EventLog;
using CMS.EmailEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.WebAnalytics;
using CMS.PortalEngine;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;
using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSWebParts_Events_EventManager : CMSAbstractWebPart
{
    protected DateTime eventDate = DataHelper.DATETIME_NOT_SELECTED;
    protected DateTime openFrom = DataHelper.DATETIME_NOT_SELECTED;
    protected DateTime openTo = DataHelper.DATETIME_NOT_SELECTED;
    protected int capacity = 0;
    protected bool allowRegistrationOverCapacity = false;
    protected bool errorOccurs = false;
    protected TreeNode mEventNode = null;

    protected TreeNode EventNode
    {
        get
        {
        	 return mEventNode; 
        }
        set
        {
        	 mEventNode = value; 
        }
    }


    #region "Public properties"

    /// <summary>
    ///  Gets or sets the value that indicates whether first and last user name are required.
    /// </summary>
    public bool RequireName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RequireName"), true);
        }
        set
        {
            this.SetValue("RequireName", value);
        }
    }


    /// <summary>
    ///  Gets or sets the value that indicates whether phone number is required for registration.
    /// </summary>
    public bool RequirePhone
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RequirePhone"), true);
        }
        set
        {
            this.SetValue("RequirePhone", value);
        }
    }


    /// <summary>
    ///  Gets or sets the value that indicates whether link to *.ics file will be available after registration.
    /// </summary>
    public bool AllowExportToOutlook
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowExportToOutlook"), true);
        }
        set
        {
            this.SetValue("AllowExportToOutlook", value);
        }
    }


    /// <summary>
    ///  Gets or sets the value that indicates whether public users are allowed to register.
    /// </summary>
    public bool AllowAnonymousRegistration
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowAnonymousRegistration"), true);
        }
        set
        {
            this.SetValue("AllowAnonymousRegistration", value);
        }
    }


    /// <summary>
    ///  Gets or sets the registration title.
    /// </summary>
    public string RegistrationTitle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RegistrationTitle"), null);
        }
        set
        {
            this.SetValue("RegistrationTitle", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing || CMSContext.CurrentDocument == null || CMSContext.CurrentDocument.NodeClassName.ToLower() != "cms.bookingevent")
        {
            // Do nothing
            this.Visible = false;
        }
        else
        {
            // Get current event document
            EventNode = CMSContext.CurrentDocument;

            // Get event date, open from/to, capacity and possibility to register over capacity information
            eventDate = ValidationHelper.GetDateTime(EventNode.GetValue("EventDate"), DataHelper.DATETIME_NOT_SELECTED);
            openFrom = ValidationHelper.GetDateTime(EventNode.GetValue("EventOpenFrom"), DataHelper.DATETIME_NOT_SELECTED);
            openTo = ValidationHelper.GetDateTime(EventNode.GetValue("EventOpenTo"), DataHelper.DATETIME_NOT_SELECTED);
            capacity = ValidationHelper.GetInteger(EventNode.GetValue("EventCapacity"), 0);
            allowRegistrationOverCapacity = ValidationHelper.GetBoolean(EventNode.GetValue("EventAllowRegistrationOverCapacity"), false);

            // Display registration section
            DisplayRegistration();

            // Display link to iCalendar file which adds this event to users Outlook
            if (this.AllowExportToOutlook)
            {
                TimeZoneInfo tzi = null;
                CMSContext.GetDateTimeForControl(this, DateTime.Now, out tzi);
                string timeZoneId = string.Empty;
                if (tzi != null)
                {
                    timeZoneId = "&timezoneid=" + tzi.TimeZoneID;
                }

                lnkOutlook.NavigateUrl = "~/CMSModules/EventManager/CMSPages/AddToOutlook.aspx?eventid=" + EventNode.NodeID.ToString() + timeZoneId;
                lnkOutlook.Target = "_blank";
                lnkOutlook.Text = GetString("eventmanager.exporttooutlook");
                lnkOutlook.Visible = true;
            }
        }
    }


    /// <summary>
    /// Displays registration section depending on situation.
    /// </summary>
    protected void DisplayRegistration()
    {
        if (!string.IsNullOrEmpty(RegistrationTitle))
        {
            lblRegTitle.Text = RegistrationTitle;
            lblRegTitle.Visible = true;
        }

        CurrentUserInfo userInfo = CMSContext.CurrentUser;
        // Display registration form to anonymous user only if this is allowed
        if ((this.AllowAnonymousRegistration || (userInfo != null && userInfo.IsAuthenticated())) && EventNode != null)
        {
            DateTime now = DateTime.Now;
            // Display registration form if opened
            if ((openFrom == DataHelper.DATETIME_NOT_SELECTED || openFrom < now) && (openTo == DataHelper.DATETIME_NOT_SELECTED || now <= openTo) && (now <= eventDate))
            {
                int actualCount = EventAttendeeInfoProvider.GetEventAttendeesCount(EventNode.OriginalNodeID);
                // Display registration form if capacity is not full
                if (allowRegistrationOverCapacity || (actualCount < capacity))
                {
                    // Preset fields with info of authenticated user
                    if (userInfo != null && userInfo.IsAuthenticated() && !RequestHelper.IsPostBack())
                    {
                        txtFirstName.Text = userInfo.FirstName;
                        txtLastName.Text = userInfo.LastName;
                        txtEmail.Text = userInfo.Email;
                    }

                    // Hide non-required fields
                    if (!this.RequireName)
                    {
                        plcName.Visible = false;
                    }
                    if (!this.RequirePhone)
                    {
                        plcPhone.Visible = false;
                    }
                }
                else
                {
                    pnlReg.Visible = false;
                    lblError.Text = GetString("eventmanager.fullcapacity");
                    lblError.Visible = true;
                    errorOccurs = true;
                }
            }
            else
            {
                pnlReg.Visible = false;
                lblError.Text = GetString("eventmanager.notopened");
                lblError.Visible = true;
                errorOccurs = true;
            }
        }
        else
        {
            pnlReg.Visible = false;
            lblError.Text = GetString("eventmanager.notauthenticated");
            lblError.Visible = true;
            errorOccurs = true;
        }
    }


    /// <summary>
    /// On btnRegister click.
    /// </summary>
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string currentSiteName = CMSContext.CurrentSiteName;
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(currentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        // Exit if problem occurs
        if (errorOccurs)
        {
            return;
        }

        string result = null;
        Validator val = new Validator();
        // Check name fields if required
        if (this.RequireName)
        {
            result = val.NotEmpty(txtFirstName.Text.Trim(), GetString("eventmanager.firstnamerequired"))
                .NotEmpty(txtLastName.Text.Trim(), GetString("eventmanager.lastnamerequired")).Result;
        }
        // Check e-mail field
        if (string.IsNullOrEmpty(result))
        {
            result = val.IsEmail(txtEmail.Text.Trim(), GetString("eventmanager.emailrequired")).Result;
        }
        // Check phone field if required
        if (this.RequirePhone && string.IsNullOrEmpty(result))
        {
            result = val.NotEmpty(txtPhone.Text.Trim(), GetString("eventmanager.phonerequired")).Result;
        }

        if (string.IsNullOrEmpty(result))
        {
            DateTime now = DateTime.Now;
            // Allow registration if opened
            if ((openFrom == DataHelper.DATETIME_NOT_SELECTED || openFrom < now) && (openTo == DataHelper.DATETIME_NOT_SELECTED || now <= openTo) && (now <= eventDate))
            {
                if (EventNode != null)
                {
                    if (!EventAttendeeInfoProvider.IsRegisteredForEvent(EventNode.NodeID, txtEmail.Text.Trim()))
                    {
                        // Add new attendant to the event
                        EventAttendeeInfo eai = AddAttendantToEvent();

                        if (eai != null)
                        {
                            // Log activity
                            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(currentSiteName) &&
                                ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) && ActivitySettingsHelper.EventBookingEnabled(currentSiteName))
                            {
                                if (ValidationHelper.GetBoolean(EventNode.GetValue("EventLogActivity"), false))
                                {
                                    var data = new ActivityData()
                                    {
                                        ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
                                        SiteID = CMSContext.CurrentSiteID,
                                        Type = PredefinedActivityType.EVENT_BOOKING,
                                        TitleData = EventNode.DocumentName,
                                        ItemID = eai.AttendeeID,
                                        URL = URLHelper.CurrentRelativePath,
                                        ItemDetailID = EventNode.DocumentID,
                                        NodeID = EventNode.NodeID,
                                        Culture = EventNode.DocumentCulture,
                                        Campaign = CMSContext.Campaign
                                    };
                                    ActivityLogProvider.LogActivity(data);
                                }
                            }

                            // Send invitation e-mail
                            TimeZoneInfo tzi = null;
                            CMSContext.GetDateTimeForControl(this, DateTime.Now, out tzi);
                            EventProvider.SendInvitation(currentSiteName, EventNode, eai, tzi);

                            lblRegInfo.Text = GetString("eventmanager.registrationsucceeded");
                            lblRegInfo.Visible = true;
                            // Hide registration form
                            pnlReg.Visible = false;
                        }
                    }
                    else
                    {
                        // User is already registered
                        lblError.Text = GetString("eventmanager.attendeeregistered");
                        lblError.Visible = true;
                    }
                }
                else
                {
                    // Event does not exist
                    lblError.Text = GetString("eventmanager.eventnotexist");
                    lblError.Visible = true;
                    // Hide registration form
                    pnlReg.Visible = false;
                }
            }
            else
            {
                // Event registration is not opened
                lblError.Text = GetString("eventmanager.notopened");
                lblError.Visible = true;
                // Hide registration form
                pnlReg.Visible = false;
            }
        }
        else
        {
            // Display error message
            lblError.Text = result;
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// Add new attendant to the event.
    /// </summary>
    /// <returns>Newly created attendee</returns>
    private EventAttendeeInfo AddAttendantToEvent()
    {
        EventAttendeeInfo attendeeInfo = null;

        if (EventNode != null)
        {
            attendeeInfo = new EventAttendeeInfo();

            attendeeInfo.AttendeeEventNodeID = EventNode.OriginalNodeID;
            attendeeInfo.AttendeeEmail = txtEmail.Text.Trim();
            if (this.RequireName)
            {
                attendeeInfo.AttendeeFirstName = txtFirstName.Text;
                attendeeInfo.AttendeeLastName = txtLastName.Text;
            }
            if (this.RequirePhone)
            {
                attendeeInfo.AttendeePhone = txtPhone.Text;
            }

            // Add new attendant to the event
            EventAttendeeInfoProvider.SetEventAttendeeInfo(attendeeInfo);
        }

        return attendeeInfo;
    }
}
