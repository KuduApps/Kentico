using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.WebAnalytics;

public partial class CMSWebParts_Newsletters_NewsletterUnsubscriptionWebPart : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the unsubscribed text.
    /// </summary>
    public string UnsubscribedText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("UnsubscribedText"), "");
        }
        set
        {
            SetValue("UnsubscribedText", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether confirmation email will be sent.
    /// </summary>
    public bool SendConfirmationEmail
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SendConfirmationEmail"), true);
        }
        set
        {
            SetValue("SendConfirmationEmail", value);
        }
    }

    #endregion


    #region "Methods"

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
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            int siteId = 0;

            // Get current id
            if (CMSContext.CurrentSite != null)
            {
                siteId = CMSContext.CurrentSiteID;
            }

            // Get subscriber and newsletter guid from query string
            Guid subscriberGuid = QueryHelper.GetGuid("subscriberguid", Guid.Empty);
            Guid newsletterGuid = QueryHelper.GetGuid("newsletterguid", Guid.Empty);
            string subscriptionHash = QueryHelper.GetString("subscriptionhash", string.Empty);
            int issueId = QueryHelper.GetInteger("issueid", 0);
            Guid issueGuid = QueryHelper.GetGuid("issueguid", Guid.Empty);
            int contactId = QueryHelper.GetInteger("contactid", 0);

            string requestTime = QueryHelper.GetString("datetime", string.Empty);
            DateTime datetime = DateTimeHelper.ZERO_TIME;

            // Get date and time
            if (!string.IsNullOrEmpty(requestTime))
            {
                try
                {
                    datetime = DateTime.ParseExact(requestTime, SecurityHelper.EMAIL_CONFIRMATION_DATETIME_FORMAT, null);
                }
                catch
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("newsletter.unsubscribefailed");
                    return;
                }
            }

            // Check whether both guid exists
            if ((subscriberGuid != Guid.Empty) && (newsletterGuid != Guid.Empty))
            {
                Subscriber subscriber = SubscriberProvider.GetSubscriber(subscriberGuid, siteId);
                if (subscriber == null)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Unsubscribe.SubscriberDoesNotExist");
                    return;
                }
                // Show error message if subscriber type is 'Role'
                if (!string.IsNullOrEmpty(subscriber.SubscriberType) && subscriber.SubscriberType.Equals(SiteObjectType.ROLE, StringComparison.InvariantCultureIgnoreCase))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Unsubscriber.CannotUnsubscribeRole");
                    return;
                }

                Newsletter newsletter = NewsletterProvider.GetNewsletter(newsletterGuid, siteId);
                if (newsletter == null)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Unsubscribe.NewsletterDoesNotExist");
                    return;
                }

                // Check whether subscription is valid
                if (SubscriberProvider.IsSubscribed(subscriber.SubscriberID, newsletter.NewsletterID))
                {
                    bool isSubscribed = true;

                    if (string.IsNullOrEmpty(subscriber.SubscriberType) || !subscriber.SubscriberType.Equals(PredefinedObjectType.CONTACTGROUP, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Unsubscribe action
                        SubscriberProvider.Unsubscribe(subscriber.SubscriberID, newsletter.NewsletterID, SendConfirmationEmail);
                    }
                    else
                    {
                        // Check if the contact group member has unsubscription activity for the specified newsletter
                        isSubscribed = (contactId > 0) && !ModuleCommands.OnlineMarketingIsContactUnsubscribed(contactId, newsletter.NewsletterID, siteId);
                    }

                    if (isSubscribed)
                    {
                        // Log newsletter unsubscription activity
                        LogActivity(subscriber, subscriber.SubscriberID, newsletter.NewsletterID, subscriber.SubscriberSiteID, issueId, issueGuid, contactId);
                        
                        // Display confirmation
                        DisplayConfirmation();
                    }
                    else
                    {
                        // Contact group member is already unsubscribed
                        lblError.Visible = true;
                        lblError.Text = GetString("Unsubscribe.NotSubscribed");
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Unsubscribe.NotSubscribed");
                }
            }
            // Check if subscriptionGUID is supplied
            else if (!string.IsNullOrEmpty(subscriptionHash))
            {
                // Check if given subscription exists
                SubscriberNewsletterInfo sni = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriptionHash);
                if (sni != null)
                {
                    SubscriberProvider.ApprovalResult result = SubscriberProvider.Unsubscribe(subscriptionHash, true, CMSContext.CurrentSiteName, datetime);

                    switch (result)
                    {
                        // Approving subscription was successful
                        case SubscriberProvider.ApprovalResult.Success:
                            bool isSubscribed = true;

                            // Get subscriber
                            Subscriber subscriber = SubscriberProvider.GetSubscriber(sni.SubscriberID);
                            if ((subscriber != null) && !string.IsNullOrEmpty(subscriber.SubscriberType) && subscriber.SubscriberType.Equals(PredefinedObjectType.CONTACTGROUP, StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Check if the contact group member has unsubscription activity for the specified newsletter
                                isSubscribed = (contactId > 0) && !ModuleCommands.OnlineMarketingIsContactUnsubscribed(contactId, sni.NewsletterID, siteId);
                            }

                            if (isSubscribed)
                            {
                                // Log newsletter unsubscription activity
                                LogActivity(subscriber, subscriber.SubscriberID, sni.NewsletterID, siteId, issueId, issueGuid, contactId);

                                // Display confirmation
                                DisplayConfirmation();
                            }
                            else
                            {
                                // Contact group member is already unsubscribed
                                lblError.Visible = true;
                                lblError.Text = GetString("Unsubscribe.NotSubscribed");
                            }
                            break;

                        // Subscription was already approved
                        case SubscriberProvider.ApprovalResult.Failed:
                            lblError.Visible = true;
                            lblError.Text = GetString("newsletter.unsubscribefailed");
                            break;

                        case SubscriberProvider.ApprovalResult.TimeExceeded:
                            lblError.Visible = true;
                            lblError.Text = GetString("newsletter.approval_timeexceeded");
                            break;

                        // Subscription not found
                        default:
                        case SubscriberProvider.ApprovalResult.NotFound:
                            lblError.Visible = true;
                            lblError.Text = GetString("Unsubscribe.NotSubscribed");
                            break;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Unsubscribe.NotSubscribed");
                }
            }
            else
            {
                Visible = false;
            }
        }
    }


    /// <summary>
    /// Displays info label and increases number of unsubscriptions.
    /// </summary>
    private void DisplayConfirmation()
    {
        // Display coinfirmation message
        lblInfo.Visible = true;
        lblInfo.Text = String.IsNullOrEmpty(UnsubscribedText) ? GetString("Unsubscribe.Unsubscribed") : UnsubscribedText;

        // If subscriber has been unsubscribed after some issue, increase number of unsubscribed persons of the issue by 1
        int issueId = QueryHelper.GetInteger("issueid", 0);
        if (issueId > 0)
        {
            // Unsubscribe using specified issue ID
            IssueProvider.Unsubscribe(issueId);
            return;
        }

        // If issue ID not available, try to unsubscribe using issue GUID
        Guid issueGuid = QueryHelper.GetGuid("issueguid", Guid.Empty);
        if (issueGuid != Guid.Empty)
        {
            Issue issue = IssueProvider.GetIssue(issueGuid, CMSContext.CurrentSiteID);
            if (issue != null)
            {
                IssueProvider.Unsubscribe(issue.IssueID);
            }
        }
    }


    /// <summary>
    /// Logs activity for unsubscribing.
    /// </summary>
    /// <param name="sb">Subscriber (optional - can be null if subscriber ID is used)</param>
    /// <param name="subscriberId">Subscriber ID (optional - can be zero if subscriber object is used)</param>
    /// <param name="newsletterId">Newsletter ID</param>
    /// <param name="siteId">Site ID</param>
    /// <param name="issueId">Issue ID</param>
    /// <param name="issueGuid">Issue GUID</param>
    /// <param name="contactId">Contact ID is present if the mail is sent to a contact group</param>
    private void LogActivity(Subscriber sb, int subscriberId, int newsletterId, int siteId, int issueId, Guid issueGuid, int contactId)
    {
        // Check if activities logging is enabled
        if ((newsletterId <= 0) || (CMSContext.ViewMode != ViewModeEnum.LiveSite) || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteId)
            || !ActivitySettingsHelper.NewsletterSubscribeEnabled(siteId))
        {
            return;
        }

        // Load subscriber info object according to its ID if not given
        if (sb == null)
        {
            if (subscriberId <= 0)
            {
                return;
            }
            sb = SubscriberProvider.GetSubscriber(subscriberId);
        }

        if (sb == null)
        {
            return;
        }

        Newsletter news = NewsletterProvider.GetNewsletter(newsletterId);
        if ((news != null) && news.NewsletterLogActivity)
        {
            bool isFromContactGroup = (contactId > 0);

            // Try to retrieve contact IDs for particular subscriber from membership relations
            if (!isFromContactGroup)
            {
                contactId = ActivityTrackingHelper.GetContactID(sb);
            }

            if (contactId > 0)
            {
                // Load additional info (issue id)
                if ((issueId <= 0) && (issueGuid != Guid.Empty))
                {
                    Issue issue = IssueProvider.GetIssue(issueGuid, siteId);
                    if (issue != null)
                    {
                        issueId = issue.IssueID;
                    }
                }

                var data = new ActivityData()
                {
                    ContactID = contactId,
                    SiteID = sb.SubscriberSiteID,
                    Type = PredefinedActivityType.NEWSLETTER_UNSUBSCRIBING,
                    TitleData = news.NewsletterName,
                    ItemID = newsletterId,
                    URL = URLHelper.CurrentRelativePath,
                    ItemDetailID = issueId,
                    Value = (isFromContactGroup ? "contactgroup(" + sb.SubscriberRelatedID + ")" : null)
                };
                ActivityLogProvider.LogActivity(data);
            }
        }
    }

    #endregion
}