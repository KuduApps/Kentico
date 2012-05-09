using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_Newsletters_Controls_MySubscriptions : CMSAdminControl
{
    #region "Variables"

    private Subscriber sb = null;
    private bool mExternalUse = false;
    private int mCacheMinutes = 0;
    private string subscriberEmail = string.Empty;
    private bool userIsIdentified = false;
    private int mUserId = 0;
    private int mSiteId = 0;
    private string currentValues = string.Empty;
    private bool mSendConfirmationEmail = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value that indicates whether send confirmation emails.
    /// </summary>
    public bool SendConfirmationEmail
    {
        get
        {
            return mSendConfirmationEmail;
        }
        set
        {
            mSendConfirmationEmail = value;            
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether this control is visible.
    /// </summary>
    public bool ForcedVisible
    {
        get
        {
            return plcMain.Visible;
        }
        set
        {
            plcMain.Visible = value;
            lblInfo.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether this control is used in other control.
    /// </summary>
    public bool ExternalUse
    {
        get
        {
            return mExternalUse;
        }
        set
        {
            mExternalUse = value;
        }
    }


    /// <summary>
    /// Gets or sets the WebPart cache minutes.
    /// </summary>
    public int CacheMinutes
    {
        get
        {
            return mCacheMinutes;
        }
        set
        {
            mCacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets current site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// Gets or sets current user ID.
    /// </summary>
    public int UserID
    {
        get
        {
            return mUserId;
        }
        set
        {
            mUserId = value;
        }
    }


    /// <summary>
    /// Indicatec if selector control is on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("IsLiveSite"), false);
        }
        set
        {
            this.SetValue("IsLiveSite", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// PageLoad.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ExternalUse)
        {
            LoadData();
        }
    }


    /// <summary>
    /// Load data.
    /// </summary>
    public void LoadData()
    {
        if (this.StopProcessing)
        {
            // Hide control
            this.Visible = false;
        }
        else
        {
            this.SetContext();

            // Get specified user if used instead of current user
            UserInfo ui = null;
            if (this.UserID > 0)
            {
                ui = UserInfoProvider.GetUserInfo(this.UserID);
            }
            else
            {
                ui = CMSContext.CurrentUser;
            }

            // Get specified site ID instead of current site ID
            int siteId = 0;
            if (this.SiteID > 0)
            {
                siteId = this.SiteID;
            }
            else
            {
                siteId = CMSContext.CurrentSiteID;
            }

            usNewsletters.WhereCondition = "NewsletterSiteID = " + siteId;
            usNewsletters.ButtonRemoveSelected.CssClass = "XLongButton";
            usNewsletters.ButtonAddItems.CssClass = "XLongButton";
            usNewsletters.OnSelectionChanged += new EventHandler(usNewsletters_OnSelectionChanged);
            usNewsletters.IsLiveSite = this.IsLiveSite;

            this.userIsIdentified = (ui != null) && (!ui.IsPublic()) && (ValidationHelper.IsEmail(ui.Email) || ValidationHelper.IsEmail(ui.UserName));
            if (this.userIsIdentified)
            {
                usNewsletters.Visible = true;

                // Try to get subsriber info with specified e-mail
                sb = SubscriberProvider.GetSubscriber(ui.Email, siteId);
                if (sb == null)
                {
                    // Try to get subscriber info according to user info
                    sb = SubscriberProvider.GetSubscriber(SiteObjectType.USER, ui.UserID, siteId);
                }

                // Get user e-mail address
                if (sb != null)
                {
                    subscriberEmail = sb.SubscriberEmail;

                    // Get selected newsletters
                    DataSet ds = SubscriberNewsletterInfoProvider.GetSubscriberNewsletters(sb.SubscriberID, null, -1, "NewsletterID");
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "NewsletterID"));
                    }

                    // Load selected newsletters
                    if (!string.IsNullOrEmpty(currentValues))
                    {
                        usNewsletters.Value = currentValues;
                    }
                }

                // Try to get email address from user data
                if (string.IsNullOrEmpty(subscriberEmail))
                {
                    if (ValidationHelper.IsEmail(ui.Email))
                    {
                        subscriberEmail = ui.Email;
                    }
                    else if (ValidationHelper.IsEmail(ui.UserName))
                    {
                        subscriberEmail = ui.UserName;
                    }
                }
            }
            else
            {
                usNewsletters.Visible = false;

                lblInfo.Visible = true;

                if ((this.UserID > 0) && (CMSContext.CurrentUser.UserID == this.UserID))
                {
                    lblInfo.Text = GetString("MySubscriptions.CannotIdentify");
                }
                else
                {
                    lblInfo.Text = GetString("MySubscriptions.CannotIdentifyUser");
                }
            }

            this.ReleaseContext();
        }
    }


    /// <summary>
    /// Logs activity for subscribing/unsubscribing
    /// </summary>
    /// <param name="ui">User</param>
    /// <param name="newsletterId">Newsletter ID</param>
    /// <param name="subscribe">Subscribing/unsubscribing flag</param>
    /// <param name="siteId">Site ID</param>
    private void LogActivity(UserInfo ui, int newsletterId, bool subscribe, int siteId)
    {
        if ((sb == null) || (ui == null) || (CMSContext.ViewMode != ViewModeEnum.LiveSite) ||
            !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteId) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(ui))
        {
            return;
        }

        if (sb.SubscriberType == SiteObjectType.USER)
        {
            if (subscribe && ActivitySettingsHelper.NewsletterSubscribeEnabled(siteId) ||
                !subscribe && ActivitySettingsHelper.NewsletterUnsubscribeEnabled(siteId))
            {
                Newsletter news = NewsletterProvider.GetNewsletter(newsletterId);
                if ((news != null) && news.NewsletterLogActivity)
                {
                    var data = new ActivityData()
                    {
                        ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
                        SiteID = sb.SubscriberSiteID,
                        Type = PredefinedActivityType.NEWSLETTER_UNSUBSCRIBING,
                        TitleData = news.NewsletterName,
                        ItemID = newsletterId,
                        URL = URLHelper.CurrentRelativePath,
                        Campaign = CMSContext.Campaign
                    };
                    ActivityLogProvider.LogActivity(data);
                }
            }
        }
    }


    private void usNewsletters_OnSelectionChanged(object sender, EventArgs e)
    {
        if (RaiseOnCheckPermissions("ManageSubscribers", this))
        {
            if (this.StopProcessing)
            {
                return;
            }
        }

        // Get specified user if used instead of current user
        UserInfo ui = null;
        if (this.UserID > 0)
        {
            ui = UserInfoProvider.GetUserInfo(this.UserID);
        }
        else
        {
            ui = CMSContext.CurrentUser;
        }

        // Get specified site ID instead of current site ID
        int siteId = 0;
        if (this.SiteID > 0)
        {
            siteId = this.SiteID;
        }
        else
        {
            siteId = CMSContext.CurrentSiteID;
        }

        if ((sb == null) && (ui != null))
        {
            // Create new subsciber (bind to existing user account)
            if ((!ui.IsPublic()) && (ValidationHelper.IsEmail(ui.Email) || ValidationHelper.IsEmail(ui.UserName)))
            {
                sb = new Subscriber();
                if (ui != null)
                {
                    if (!string.IsNullOrEmpty(ui.FirstName) && !string.IsNullOrEmpty(ui.LastName))
                    {
                        sb.SubscriberFirstName = ui.FirstName;
                        sb.SubscriberLastName = ui.LastName;
                    }
                    else
                    {
                        sb.SubscriberFirstName = ui.FullName;
                    }
                    // Full name consists of "user " and user full name
                    sb.SubscriberFullName = "User '" + ui.FullName + "'";
                }

                sb.SubscriberSiteID = siteId;
                sb.SubscriberType = SiteObjectType.USER;
                sb.SubscriberRelatedID = ui.UserID;
                // Save subscriber to DB
                SubscriberProvider.SetSubscriber(sb);
            }
        }

        if (sb == null)
        {
            return;
        }

        // Remove old items
        int newsletterId = 0;
        string newValues = ValidationHelper.GetString(usNewsletters.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                foreach (string item in newItems)
                {
                    newsletterId = ValidationHelper.GetInteger(item, 0);

                    // If subscriber is subscribed, unsubscribe him
                    if (SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId))
                    {
                        SubscriberProvider.Unsubscribe(sb.SubscriberID, newsletterId, SendConfirmationEmail);
                        // Log activity
                        LogActivity(ui, newsletterId, false, siteId);
                    }
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                foreach (string item in newItems)
                {
                    newsletterId = ValidationHelper.GetInteger(item, 0);

                    // If subscriber is not subscribed, subscribe him
                    if (!SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId))
                    {
                        try
                        {
                            SubscriberProvider.Subscribe(sb.SubscriberID, newsletterId, DateTime.Now, SendConfirmationEmail);
                            // Log activity
                            LogActivity(ui, newsletterId, true, siteId);
                        }
                        catch { }
                    }
                }
            }
        }

        // Display information about successful (un)subscription
        this.lblInfoMsg.Visible = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Display appropriate message
        if (this.userIsIdentified)
        {
            // There are some newsletters to display
            if (CMSContext.CurrentUser.UserID == this.UserID)
            {
                lblText.Text = GetString("MySubscriptions.MainText").Replace("##EMAIL##", HTMLHelper.HTMLEncode(subscriberEmail));
            }
            else
            {
                lblText.Text = GetString("MySubscriptions.MainTextUser").Replace("##EMAIL##", HTMLHelper.HTMLEncode(subscriberEmail));
            }
        }
    }


    /// <summary>
    /// Overriden SetValue - because of MyAccount webpart.
    /// </summary>
    /// <param name="propertyName">Name of the property to set</param>
    /// <param name="value">Value to set</param>
    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        switch (propertyName.ToLower())
        {
            case "forcedvisible":
                this.ForcedVisible = ValidationHelper.GetBoolean(value, false);
                break;

            case "externaluse":
                this.ExternalUse = ValidationHelper.GetBoolean(value, false);
                break;

            case "cacheminutes":
                this.CacheMinutes = ValidationHelper.GetInteger(value, 0);
                break;

            case "reloaddata":
                // Special property which enables to call LoadData from MyAccount webpart
                LoadData();
                break;

            case "userid":
                this.UserID = ValidationHelper.GetInteger(value, 0);
                break;

            case "siteid":
                this.SiteID = ValidationHelper.GetInteger(value, 0);
                break;

            case "sendconfirmationemail":
                mSendConfirmationEmail = ValidationHelper.GetBoolean (value,true);
                break;
        }
    }

    #endregion
}
