using System;
using System.Collections;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;

[Title("Objects/OM_Activity/detail.png", "om.activity.details.title", "activity_detail")]

[Security(Resource = "CMS.ContactManagement", Permission = "ReadActivities")]
public partial class CMSModules_ContactManagement_Pages_Tools_Activities_Activity_Activity_Details : CMSModalPage
{
    #region "Protected variables"

    protected int activityId;
    protected int prevId;
    protected int nextId;
    protected Hashtable mParameters;
    protected bool userIsAuthorized = false;
    protected bool isSitemanager = false;

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
        if (!QueryHelper.ValidateHash("hash", "activityid") || Parameters == null)
        {
            return;
        }

        pnlGen.GroupingText = GetString("om.activity.details.groupgeneral");
        pnlComment.GroupingText = GetString("om.activity.activitycomment");
        btnView.ImageUrl = GetImageUrl("/Design/Controls/UniGrid/Actions/View.png");
        btnContact.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/contactdetail.png");

        // Get the ORDER BY column and starting event ID
        string orderBy = ValidationHelper.GetString(Parameters["orderby"], "ActivityID DESC");
        if (orderBy.IndexOf(';') >= 0)
        {
            orderBy = "ActivityID DESC";  // ORDER BY with semicolon is considered to be dangerous
        }
        string whereCondition = ValidationHelper.GetString(Parameters["where"], string.Empty);
        int contactId = ValidationHelper.GetInteger(Parameters["contactid"], 0);
        bool isMerged = ValidationHelper.GetBoolean(Parameters["ismerged"], false);
        bool isGlobal = ValidationHelper.GetBoolean(Parameters["isglobal"], false);
        isSitemanager = ValidationHelper.GetBoolean(Parameters["issitemanager"], false);

        // Get activity ID from query string
        activityId = QueryHelper.GetInteger("activityid", 0);

        LoadData();

        // Initialize next/previous buttons
        int[] prevNext = ActivityInfoProvider.GetPreviousNext(activityId, whereCondition, orderBy, contactId, isMerged, isGlobal);
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

        // Enable text boxes and show save button if user is autorized to change activities
        userIsAuthorized = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ContactManagement", "ManageActivities");
        if (userIsAuthorized)
        {
            btnSave.Visible = true;
            btnSave.Click += new EventHandler(btnSave_Click);
            txtURL.ReadOnly = false;
            txtTitle.ReadOnly = false;
        }

        // Disable collapse of toolbar (IE7 bug)
        txtComment.ToolbarCanCollapse = false;

        RegisterScripts();
        btnStamp.OnClientClick = "AddStamp('" + txtComment.ClientID + "'); return false;";
    }

    #endregion


    #region "Button handling"

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, "activityid", prevId.ToString()));
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, "activityid", nextId.ToString()));
    }


    void btnSave_Click(object sender, EventArgs e)
    {
        if (userIsAuthorized && (activityId > 0))
        {
            ActivityInfo ai = ActivityInfoProvider.GetActivityInfo(activityId);
            EditedObject = ai;
            ai.ActivityComment = txtComment.Value;
            ai.ActivityTitle = TextHelper.LimitLength(txtTitle.Text, 250, String.Empty);
            ai.ActivityURL = txtURL.Text;
            ActivityInfoProvider.SetActivityInfo(ai);

            // Reload form (due to "view URL" button)
            LoadData();
        }
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Loads data of specific activity.
    /// </summary>
    protected void LoadData()
    {
        if (activityId <= 0)
        {
            return;
        }

        // Load and check if object exists
        ActivityInfo ai = ActivityInfoProvider.GetActivityInfo(activityId);
        EditedObject = ai;

        ActivityTypeInfo ati = ActivityTypeInfoProvider.GetActivityTypeInfo(ai.ActivityType);
        plcActivityValue.Visible = (ati == null) || ati.ActivityTypeIsCustom || (ati.ActivityTypeName == PredefinedActivityType.PAGE_VISIT) && !String.IsNullOrEmpty(ai.ActivityValue);

        string dispName = (ati != null ? ati.ActivityTypeDisplayName : GetString("general.na"));

        lblTypeVal.Text = String.Format("{0}", HTMLHelper.HTMLEncode(dispName));
        lblContactVal.Text = HTMLHelper.HTMLEncode(ContactInfoProvider.GetContactFullName(ai.ActivityActiveContactID));
        // Init contact detail link
        btnContact.Attributes.Add("onClick", "EditContact(" + ai.ActivityActiveContactID + "); return false;");
        btnContact.ToolTip = GetString("om.contact.viewdetail");

        lblDateVal.Text = (ai.ActivityCreated == DateTimeHelper.ZERO_TIME?GetString("general.na"):HTMLHelper.HTMLEncode(ai.ActivityCreated.ToString()));

        // Get site display name
        string siteName = SiteInfoProvider.GetSiteName(ai.ActivitySiteID);
        if (String.IsNullOrEmpty(siteName))
        {
            siteName = GetString("general.na");
        }
        else
        {
            // Retrieve site info and its display name
            SiteInfo si = SiteInfoProvider.GetSiteInfo(siteName);
            if (si != null)
            {
                siteName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(si.DisplayName));
            }
            else
            {
                siteName = GetString("general.na");
            }
        }
        lblSiteVal.Text = siteName;

        lblURLReferrerVal.Text = HTMLHelper.HTMLEncode(String.IsNullOrEmpty(ai.ActivityURLReferrer)?GetString("general.na"):ai.ActivityURLReferrer);

        string url = ai.ActivityURL;
        plcCampaign.Visible = !String.IsNullOrEmpty(ai.ActivityCampaign);
        lblCampaignVal.Text = HTMLHelper.HTMLEncode(ai.ActivityCampaign);

        // Init textboxes only for the first time
        if (!RequestHelper.IsPostBack())
        {
            txtComment.Value = ai.ActivityComment;
            txtTitle.Text = ai.ActivityTitle;
            lblValue.Text = HTMLHelper.HTMLEncode(String.IsNullOrEmpty(ai.ActivityValue) ? GetString("general.na") : ai.ActivityValue);
            if (ai.ActivityType != PredefinedActivityType.NEWSLETTER_CLICKTHROUGH)
            {
                txtURL.Text = url;
            }
        }

        cDetails.ActivityID = activityId;

        // Init link button URL
        if (ai.ActivitySiteID > 0)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(ai.ActivitySiteID);
            if (si != null)
            {
                // Hide view button if URL is blank
                string activityUrl = ai.ActivityURL;
                if ((activityUrl != null) && !String.IsNullOrEmpty(activityUrl.Trim()))
                {
                    string appUrl = URLHelper.GetApplicationUrl(si.DomainName);
                    url = URLHelper.GetAbsoluteUrl(activityUrl, appUrl, appUrl, "");
                    url = URLHelper.AddParameterToUrl(url, URLHelper.SYSTEM_QUERY_PARAMETER, "1");
                    btnView.ToolTip = GetString("general.view");
                    btnView.NavigateUrl = url;
                    btnView.Visible = true;
                }
                else
                {
                    btnView.Visible = false;
                }
            }
        }
    }


    /// <summary>
    /// Registers JavaScripts on page.
    /// </summary>
    protected void RegisterScripts()
    {
        string stamp = null;
        if (isSitemanager)
        {
            stamp = SettingsKeyProvider.GetStringValue("CMSCMStamp");
        }
        else
        {
            stamp = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSCMStamp");
        }

        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddStamp", ScriptHelper.GetScript(
@"function InsertHTML(htmlString, ckClientID)
{
    // Get the editor instance that we want to interact with.
    var oEditor = oEditor = window.CKEDITOR.instances[ckClientID];
    // Check the active editing mode.
    if (oEditor != null) {
        // Check the active editing mode.
        if (oEditor.mode == 'wysiwyg') {
            // Insert the desired HTML.
            //oEditor.focus();
            oEditor.insertHtml(htmlString);        
        }
    }    
    return false;
}   

function AddStamp(ckClientID)
{
InsertHTML('<div>" + CMSContext.CurrentResolver.ResolveMacros(stamp).Replace("'", @"\'") + @"</div>', ckClientID);
}

function EditContact(contactID)
{
    modalDialog('" + ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/Account/Contact_Detail.aspx") + @"?contactid=' + contactID + '&isSiteManager=" + isSitemanager + @"', 'ContactDetail', '1061px', '700px');
}
"));

    }

    #endregion
}