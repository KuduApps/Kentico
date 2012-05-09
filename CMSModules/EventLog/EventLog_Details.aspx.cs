using System;
using System.Data;
using System.Web;
using System.Collections;

using CMS.GlobalHelper;
using CMS.EventLog;
using CMS.SiteProvider;
using CMS.UIControls;

[Title("Objects/CMS_EventLog/detail.png", "EventLogDetails.Header", "event_log_detail")]
public partial class CMSModules_EventLog_EventLog_Details : CMSEventLogPage
{
    #region "Protected variables"

    protected int eventId = 0;
    protected int prevId = 0;
    protected int nextId = 0;
    protected EventLogProvider eventProvider = new EventLogProvider();
    protected Hashtable mParameters = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Hashtable containing dialog parameters.
    /// </summary>
    private Hashtable Parameters
    {
        get
        {
            if (mParameters == null)
            {
                string identificator = QueryHelper.GetString("params", null);
                mParameters = (Hashtable)WindowHelper.GetItem(identificator);
            }
            return mParameters;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash", "eventid") && (Parameters != null))
        {
            CheckPermissions(true);

            // Get the ORDER BY column and starting event ID
            string orderBy = ValidationHelper.GetString(Parameters["orderby"], "EventID DESC");
            if ((orderBy == string.Empty) || (orderBy.IndexOf(';') >= 0))
            {
                orderBy = "EventID DESC";  // ORDER BY with semicolon is considered to be dangerous
            }
            string whereCondition = ValidationHelper.GetString(Parameters["where"], string.Empty);

            eventId = QueryHelper.GetInteger("eventid", 0);

            if (!RequestHelper.IsPostBack())
            {
                // Get EventID value
                LoadData();
            }

            lnkExport.Visible = true;
            lnkExport.Text = GetString("EventLogDetails.Export");
            lnkExport.NavigateUrl = ResolveUrl("GetEventDetail.aspx?eventid=" + eventId);

            if (SiteID > 0)
            {
                lnkExport.NavigateUrl = URLHelper.AddParameterToUrl(lnkExport.NavigateUrl, "siteid", SiteID.ToString());
            }

            lnkExport.Target = "_blank";

            // Initialize next/previous buttons
            int[] prevNext = eventProvider.GetPreviousNext(eventId, whereCondition, orderBy);
            if (prevNext != null)
            {
                prevId = prevNext[0];
                nextId = prevNext[1];

                btnPrevious.Enabled = (prevId != 0);
                btnNext.Enabled = (nextId != 0);

                btnPrevious.Click += btnPrevious_Click;
                btnNext.Click += btnNext_Click;
            }

            // Set button caption
            btnNext.Text = GetString("general.next") + " >";
            btnPrevious.Text = "< " + GetString("general.back");

            RegisterModalPageScripts();
            RegisterEscScript();
        }
    }

    #endregion


    #region "Button handling"

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        // Redirect to previous
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, "eventId", string.Empty + prevId));
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        // Redirect to next
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, "eventId", string.Empty + nextId));
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Loads data of specific EventLog from DB.
    /// </summary>
    protected void LoadData()
    {
        EventLogInfo ev = EventLogProvider.GetEventLogInfo(eventId);
        //Set edited object
        EditedObject = ev;

        if (ev != null)
        {
            string eventType = ValidationHelper.GetString(ev.GetValue("EventType"), string.Empty);

            // Rewrite event type text.
            lblEventTypeValue.Text = EventLogHelper.GetEventTypeText(eventType);
            lblEventIDValue.Text = ValidationHelper.GetString(ev.GetValue("EventID"), string.Empty);
            lblEventTimeValue.Text = ValidationHelper.GetString(ev.GetValue("EventTime"), string.Empty);
            lblSourceValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("Source"), string.Empty));
            lblEventCodeValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("EventCode"), string.Empty));

            lblUserIDValue.Text = ValidationHelper.GetString(ev.GetValue("UserID"), string.Empty);
            plcUserID.Visible = (lblUserIDValue.Text != string.Empty);

            lblUserNameValue.Text = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(ValidationHelper.GetString(ev.GetValue("UserName"), string.Empty)));
            plcUserName.Visible = (lblUserNameValue.Text != string.Empty);

            lblIPAddressValue.Text = ValidationHelper.GetString(ev.GetValue("IPAddress"), string.Empty);

            lblNodeIDValue.Text = ValidationHelper.GetString(ev.GetValue("NodeID"), string.Empty);
            plcNodeID.Visible = (lblNodeIDValue.Text != string.Empty);

            lblNodeNameValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("DocumentName"), string.Empty));
            plcNodeName.Visible = (lblNodeNameValue.Text != string.Empty);

            string description = HTMLHelper.StripTags(ValidationHelper.GetString(ev.GetValue("EventDescription"), string.Empty).Replace("<br />", "\r\n").Replace("<br/>", "\r\n"));
            lblEventDescriptionValue.Text = HTMLHelper.EnsureLineEnding(HTMLHelper.HTMLEncode(description), "<br />");

            if (!DataHelper.IsEmpty(ev.GetValue("SiteID")))
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(Convert.ToInt32(ev.GetValue("SiteID")));
                if (si != null)
                {
                    lblSiteNameValue.Text = HTMLHelper.HTMLEncode(si.DisplayName);
                }
            }
            else
            {
                plcSite.Visible = false;
            }

            lblMachineNameValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("EventMachineName"), string.Empty));
            lblEventUrlValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("EventUrl"), string.Empty));
            lblUrlReferrerValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("EventUrlReferrer"), string.Empty));
            lblUserAgentValue.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ev.GetValue("EventUserAgent"), string.Empty));
        }
    }

    #endregion
}
