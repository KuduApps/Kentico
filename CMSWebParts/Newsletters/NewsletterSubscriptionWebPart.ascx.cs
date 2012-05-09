using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.WebAnalytics;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSWebParts_Newsletters_NewsletterSubscriptionWebPart : CMSAbstractWebPart
{
    #region "Variables"

    private bool chooseMode = false;
    private bool mExternalUse = false;
    private bool visibleFirstName = true;
    private bool visibleLastName = true;
    private bool visibleEmail = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether first name will be displayed.
    /// </summary>
    public bool DisplayFirstName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayFirstName"), false);
        }
        set
        {
            this.SetValue("DisplayFirstName", value);
        }
    }


    /// <summary>
    /// Gets or sets the first name text.
    /// </summary>
    public string FirstNameText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("FirstNameText"), ResHelper.LocalizeString("{$NewsletterSubscription.FirstName$}"));
        }
        set
        {
            this.SetValue("FirstNameText", value);
            lblFirstName.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether last name will be displayed.
    /// </summary>
    public bool DisplayLastName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayLastName"), false);
        }
        set
        {
            this.SetValue("DisplayLastName", value);
        }
    }


    /// <summary>
    /// Gets or sets the last name text.
    /// </summary>
    public string LastNameText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("LastNameText"), ResHelper.LocalizeString("{$NewsletterSubscription.LastName$}"));
        }
        set
        {
            this.SetValue("LastNameText", value);
            lblLastName.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the newsletter code name.
    /// </summary>
    public string NewsletterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NewsletterName"), "");
        }
        set
        {
            this.SetValue("NewsletterName", value);
        }
    }


    /// <summary>
    /// Gets or sets the e-mail text.
    /// </summary>
    public string EmailText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("EmailText"), ResHelper.LocalizeString("{$NewsletterSubscription.Email$}"));
        }
        set
        {
            this.SetValue("EmailText", value);
            lblEmail.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the button text.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ButtonText"), ResHelper.LocalizeString("{$NewsletterSubscription.Submit$}"));
        }
        set
        {
            this.SetValue("ButtonText", value);
            btnSubmit.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets the conversion track name used after successful subscription.
    /// </summary>
    public string TrackConversionName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TrackConversionName"), "");
        }
        set
        {
            if (value.Length > 400)
            {
                value = value.Substring(0, 400);
            }
            this.SetValue("TrackConversionName", value);
        }
    }


    /// <summary>
    /// Gets or sets the conversion value used after successful subscription.
    /// </summary>
    public double ConversionValue
    {
        get
        {
            return ValidationHelper.GetDouble(this.GetValue("ConversionValue"), 0);
        }
        set
        {
            this.SetValue("ConversionValue", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether confirmation email will be sent.
    /// </summary>
    public bool SendConfirmationEmail
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SendConfirmationEmail"), true);
        }
        set
        {
            this.SetValue("SendConfirmationEmail", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether this webpart is used in other webpart or user control.
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
    /// Gets or sets the captcha label text.
    /// </summary>
    public string CaptchaText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("CaptchaText"), "Webparts_Membership_RegistrationForm.Captcha");
        }
        set
        {
            this.SetValue("CaptchaText", value);
            lblCaptcha.ResourceString = value;
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether the captcha image should be displayed.
    /// </summary>
    public bool DisplayCaptcha
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayCaptcha"), false);
        }
        set
        {
            this.SetValue("DisplayCaptcha", value);
            plcCaptcha.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets value which indicates if authenticated users can subscribe to newsletter.
    /// </summary>
    public bool AllowUserSubscribers
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowUserSubscribers"), false);
        }
        set
        {
            this.SetValue("AllowUserSubscribers", value);
        }
    }


    /// <summary>
    /// Gets or sets value which indicates if image button should be used instead of regular one.
    /// </summary>
    public bool UseImageButton
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseImageButton"), false);
        }
        set
        {
            this.SetValue("UseImageButton", value);
        }
    }


    /// <summary>
    /// Gets or sets image button URL.
    /// </summary>
    public string ImageButtonURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ImageButtonURL"), "");
        }
        set
        {
            this.SetValue("ImageButtonURL", value);
            this.btnImageSubmit.ImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the SkinID of the logon form.
    /// </summary>
    public override string SkinID
    {
        get
        {
            return base.SkinID;
        }
        set
        {
            base.SkinID = value;
            lblFirstName.SkinID = value;
            lblLastName.SkinID = value;
            lblEmail.SkinID = value;
            txtFirstName.SkinID = value;
            txtLastName.SkinID = value;
            txtEmail.SkinID = value;
            btnSubmit.SkinID = value;
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
    /// Reloads data for partial caching.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        SetVisibility();
    }


    /// <summary>
    /// Sets visibility of controls.
    /// </summary>
    protected void SetVisibility()
    {
        plcFirstName.Visible = visibleFirstName;
        plcLastName.Visible = visibleLastName;
        plcEmail.Visible = visibleEmail;
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
        }
        else
        {
            lblFirstName.Text = this.FirstNameText;
            lblLastName.Text = this.LastNameText;
            lblEmail.Text = this.EmailText;
            lblCaptcha.ResourceString = this.CaptchaText;
            plcCaptcha.Visible = this.DisplayCaptcha;

            if ((this.UseImageButton) && (!String.IsNullOrEmpty(this.ImageButtonURL)))
            {
                pnlButtonSubmit.Visible = false;
                pnlImageSubmit.Visible = true;
                btnImageSubmit.ImageUrl = this.ImageButtonURL;
            }
            else
            {
                pnlButtonSubmit.Visible = true;
                pnlImageSubmit.Visible = false;
                btnSubmit.Text = this.ButtonText;
            }

            // Display labels only if user is logged in and property AllowUserSubscribers is set to true
            if (AllowUserSubscribers && CMSContext.CurrentUser.IsAuthenticated())
            {
                visibleFirstName = false;
                visibleLastName = false;
                visibleEmail = false;
            }
            // Otherwise display text-boxes
            else
            {
                visibleFirstName = true;
                visibleLastName = true;
                visibleEmail = true;
            }

            // Hide first name field if not required
            if (!this.DisplayFirstName)
            {
                visibleFirstName = false;
            }
            // Hide last name field if not required
            if (!this.DisplayLastName)
            {
                visibleLastName = false;
            }

            if (this.NewsletterName.Equals("nwsletuserchoose", StringComparison.InvariantCultureIgnoreCase))
            {
                chooseMode = true;
                plcNwsList.Visible = true;

                if ((!ExternalUse || !RequestHelper.IsPostBack()) && (chklNewsletters.Items.Count == 0))
                {
                    DataSet ds = null;

                    // Try to get data from cache
                    using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref ds, this.CacheMinutes, true, this.CacheItemName, "newslettersubscription", CMSContext.CurrentSiteName))
                    {
                        if (cs.LoadData)
                        {
                            // Get the data
                            ds = NewsletterProvider.GetAllNewslettersForSite(CMSContext.CurrentSiteID, "NewsletterDisplayName", 0, "NewsletterDisplayName,NewsletterName");

                            // Add data to the cache
                            if (cs.Cached)
                            {
                                cs.CacheDependency = GetCacheDependency();
                                cs.Data = ds;
                            }
                        }
                    }

                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        ListItem li = null;
                        // Fill checkbox list with newsletters
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            li = new ListItem(HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["NewsletterDisplayName"], string.Empty)), ValidationHelper.GetString(dr["NewsletterName"], string.Empty));
                            chklNewsletters.Items.Add(li);
                        }
                    }
                }
            }
            else
            {
                // Hide newsletter list
                plcNwsList.Visible = false;
            }

            // Set SkinID properties
            if (!this.StandAlone && (this.PageCycle < PageCycleEnum.Initialized) && (string.IsNullOrEmpty(ValidationHelper.GetString(this.Page.StyleSheetTheme, string.Empty))))
            {
                string skinId = this.SkinID;
                if (!string.IsNullOrEmpty(skinId))
                {
                    lblFirstName.SkinID = skinId;
                    lblLastName.SkinID = skinId;
                    lblEmail.SkinID = skinId;
                    txtFirstName.SkinID = skinId;
                    txtLastName.SkinID = skinId;
                    txtEmail.SkinID = skinId;
                    btnSubmit.SkinID = skinId;
                }
            }
        }
    }


    /// <summary>
    /// Applies given stylesheet skin.
    /// </summary>
    /// <param name="page">Web Forms page</param>
    public override void ApplyStyleSheetSkin(Page page)
    {
        string skinId = this.SkinID;
        if (!string.IsNullOrEmpty(skinId))
        {
            lblFirstName.SkinID = skinId;
            lblLastName.SkinID = skinId;
            lblEmail.SkinID = skinId;
            txtFirstName.SkinID = skinId;
            txtLastName.SkinID = skinId;
            txtEmail.SkinID = skinId;
            btnSubmit.SkinID = skinId;
        }

        base.ApplyStyleSheetSkin(page);
    }


    /// <summary>
    /// Indicates whether the control form fields contain a valid data.
    /// </summary>
    /// <returns>Returns true if the form data are valid; otherwise, false</returns>
    private bool IsValid()
    {
        string errorText = null;
        bool result = true;

        // If not allowing user subscribing or if user is not logged in
        if (!(AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated()))
        {
            // First name validation
            if (this.DisplayFirstName)
            {
                if (txtFirstName.Text == null || txtFirstName.Text.Trim().Length == 0)
                {
                    errorText += GetString("NewsletterSubscription.ErrorEmptyFirstName") + "<br />";
                    result = false;
                }
            }

            // Last name
            if (this.DisplayLastName)
            {
                if (txtLastName.Text == null || txtLastName.Text.Trim().Length == 0)
                {
                    errorText += GetString("NewsletterSubscription.ErrorEmptyLastName") + "<br />";
                    result = false;
                }
            }

            // E-mail address validation
            if ((!ValidationHelper.IsEmail(txtEmail.Text.Trim())) || (txtEmail.Text.Trim().Length == 0))
            {
                errorText += GetString("NewsletterSubscription.ErrorInvalidEmail") + "<br />";
                result = false;
            }
        }
        // If allowing user subscribing and user is logged in and user don't have filled in e-mail
        else if ((AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated()) && (String.IsNullOrEmpty(CMSContext.CurrentUser.Email)))
        {
            errorText += GetString("newslettersubscription.erroremptyemail") + "<br />";
            result = false;
        }

        if (chooseMode)
        {
            if (chklNewsletters.SelectedIndex < 0)
            {
                errorText += GetString("NewsletterSubscription.NoneSelected") + "<br />";
                result = false;
            }
        }

        // Check if captcha is required
        if (this.DisplayCaptcha)
        {
            // Verifiy captcha text
            if (!scCaptcha.IsValid())
            {
                // Display error message if captcha text is not valid
                errorText += GetString("Webparts_Membership_RegistrationForm.captchaError");
                result = false;
            }
            else
            {
                // Generate new captcha
                scCaptcha.GenerateNew();
            }
        }

        // Assign validation result text.
        if (!string.IsNullOrEmpty(errorText))
        {
            lblError.Visible = true;
            lblError.Text = errorText;
        }

        return result;
    }


    /// <summary>
    /// Valid checkbox list, Indicates whether the subscriber is already subscribed to the selected newsletter.
    /// </summary>
    private string ValidChoose()
    {
        Subscriber sb = SaveSubscriber();

        bool wasWrong = true;

        // Save selected items
        for (int i = 0; i < chklNewsletters.Items.Count; i++)
        {
            ListItem item = chklNewsletters.Items[i];
            if (item != null && item.Selected)
            {
                wasWrong = wasWrong & (!Save(item.Value, sb));
            }
        }

        // Check subscription
        if ((chklNewsletters.Items.Count > 0) && (!wasWrong))
        {
            lblInfo.Visible = true;
            lblInfo.Text += GetString("NewsletterSubscription.Subscribed");
            plcCaptcha.Visible = false;
        }
        else
        {
            plcNwsList.Visible = true;

            if (this.DisplayFirstName && !(AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated()))
            {
                visibleFirstName = true;
            }

            if (this.DisplayLastName && !(AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated()))
            {
                visibleLastName = true;
            }

            if (!((AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated()) && (!String.IsNullOrEmpty(CMSContext.CurrentUser.Email))))
            {
                visibleEmail = true;
            }

            if ((this.UseImageButton) && (!String.IsNullOrEmpty(this.ImageButtonURL)))
            {
                pnlButtonSubmit.Visible = false;
                pnlImageSubmit.Visible = true;
            }
            else
            {
                pnlButtonSubmit.Visible = true;
                pnlImageSubmit.Visible = false;
            }
        }

        return string.Empty;
    }


    /// <summary>
    /// Submit button handler.
    /// </summary>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        if (IsValid())
        {
            if (chooseMode)
            {
                ValidChoose();
            }
            else
            {
                Save(this.NewsletterName, SaveSubscriber());
                plcCaptcha.Visible = false;
            }
        }
    }


    /// <summary>
    /// Save subscriber.
    /// </summary>
    /// <returns>Subscriver info object</returns>
    private Subscriber SaveSubscriber()
    {
        // Check if a subscriber exists first
        Subscriber sb = null;
        if (AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated())
        {
            sb = SubscriberProvider.GetSubscriber(SiteObjectType.USER, CMSContext.CurrentUser.UserID, CMSContext.CurrentSiteID);
        }
        else
        {
            sb = SubscriberProvider.GetSubscriber(txtEmail.Text, CMSContext.CurrentSiteID);
        }

        if ((sb == null) || ((chooseMode) && (sb != null)))
        {
            // Create subscriber
            if (sb == null)
            {
                sb = new Subscriber();
            }

            // Handle authenticated user
            if (AllowUserSubscribers && (CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated())
            {
                // Get user info and copy first name, last name or full name to new subscriber
                UserInfo ui = UserInfoProvider.GetUserInfo(CMSContext.CurrentUser.UserID);
                if (ui != null)
                {
                    if (!DataHelper.IsEmpty(ui.FirstName) && !DataHelper.IsEmpty(ui.LastName))
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
                else
                {
                    return null;
                }

                sb.SubscriberType = "cms.user";
                sb.SubscriberRelatedID = CMSContext.CurrentUser.UserID;
            }
            // Work with non-authenticated user
            else
            {
                sb.SubscriberEmail = txtEmail.Text.Trim();

                // First name
                if (DisplayFirstName)
                {
                    sb.SubscriberFirstName = txtFirstName.Text;
                }
                else
                {
                    sb.SubscriberFirstName = string.Empty;
                }

                // Last name
                if (DisplayLastName)
                {
                    sb.SubscriberLastName = txtLastName.Text;
                }
                else
                {
                    sb.SubscriberLastName = string.Empty;
                }

                // Full name
                sb.SubscriberFullName = (sb.SubscriberFirstName + " " + sb.SubscriberLastName).Trim();

                // Create guid
                sb.SubscriberGUID = Guid.NewGuid();
            }

            // Set site ID
            sb.SubscriberSiteID = CMSContext.CurrentSiteID;

            // Check subscriber limits
            if (!SubscriberProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Subscribers, VersionActionEnum.Insert))
            {
                lblError.Visible = true;
                lblError.Text = GetString("LicenseVersionCheck.Subscribers");
                return null;
            }

            // Save subscriber info
            SubscriberProvider.SetSubscriber(sb);
        }
        // Hide all 
        visibleLastName = false;
        visibleFirstName = false;
        visibleEmail = false;

        pnlButtonSubmit.Visible = false;
        pnlImageSubmit.Visible = false;

        plcNwsList.Visible = false;

        // Clear the form
        txtEmail.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;

        // Return subscriber info object
        return sb;
    }


    /// <summary>
    /// Saves the data.
    /// </summary>
    private bool Save(string newsletterName, Subscriber sb)
    {
        bool toReturn = false;
        int siteId = CMSContext.CurrentSiteID;

        // Check if sunscriber info object exists
        if ((sb == null) || string.IsNullOrEmpty(newsletterName))
        {
            return false;
        }

        // Get nesletter info
        Newsletter news = NewsletterProvider.GetNewsletter(newsletterName, siteId);
        if (news != null)
        {
            try
            {
                // Check if subscriber is not allready subscribed
                if (!SubscriberProvider.IsSubscribed(sb.SubscriberGUID, news.NewsletterGUID, siteId))
                {
                    toReturn = true;

                    // Subscribe to the site
                    SubscriberProvider.Subscribe(sb.SubscriberID, news.NewsletterID, DateTime.Now, this.SendConfirmationEmail);

                    // Info message
                    if (!chooseMode)
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("NewsletterSubscription.Subscribed");
                    }

                    // Track successful subscription conversion
                    if (this.TrackConversionName != string.Empty)
                    {
                        string siteName = CMSContext.CurrentSiteName;

                        if (AnalyticsHelper.AnalyticsEnabled(siteName) && AnalyticsHelper.TrackConversionsEnabled(siteName) && !AnalyticsHelper.IsIPExcluded(siteName, HTTPHelper.UserHostAddress))
                        {
                            // Log conversion
                            HitLogProvider.LogConversions(siteName, CMSContext.PreferredCultureCode, TrackConversionName, 0, ConversionValue);
                        }
                    }

                    // Log newsletter subscription activity if double opt-in is not required
                    if (!news.NewsletterEnableOptIn)
                    {
                        if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) && news.NewsletterLogActivity && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteId) &&
                            ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) &&
                            ActivitySettingsHelper.NewsletterSubscribeEnabled(siteId))
                        {
                            int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
                            ModuleCommands.OnlineMarketingUpdateContactFromExternalData(sb, contactId);
                            ModuleCommands.OnlineMarketingCreateRelation(sb.SubscriberID, MembershipType.NEWSLETTER_SUBSCRIBER, contactId);

                            if (ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
                            {
                                var data = new ActivityData()
                                {
                                    ContactID = contactId,
                                    SiteID = sb.SubscriberSiteID,
                                    Type = PredefinedActivityType.NEWSLETTER_SUBSCRIBING,
                                    TitleData = news.NewsletterName,
                                    ItemID = news.NewsletterID,
                                    URL = URLHelper.CurrentRelativePath,
                                    Campaign = CMSContext.Campaign
                                };
                                ActivityLogProvider.LogActivity(data);
                            }
                        }
                    }
                }
                else
                {
                    // Info message - subscriber is allready in site
                    if (!chooseMode)
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("NewsletterSubscription.SubscriberIsAlreadySubscribed");
                    }
                    else
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text += GetString("NewsletterSubscription.SubscriberIsAlreadySubscribedXY") + " " + HTMLHelper.HTMLEncode(news.NewsletterDisplayName) + ".<br />";
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = GetString("NewsletterSubscription.NewsletterDoesNotExist");
        }

        return toReturn;
    }


    /// <summary>
    /// Clears the cached items.
    /// </summary>
    public override void ClearCache()
    {
        string useCacheItemName = DataHelper.GetNotEmpty(this.CacheItemName, CacheHelper.BaseCacheKey + "|" + URLHelper.Url.ToString() + "|" + this.ClientID);

        CacheHelper.ClearCache(useCacheItemName);
    }
}
