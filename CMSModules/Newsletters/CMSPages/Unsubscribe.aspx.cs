using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;

public partial class CMSModules_Newsletters_CMSPages_Unsubscribe : CMSPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get data from query string
        Guid subscriberGuid = QueryHelper.GetGuid("subscriberguid", Guid.Empty);
        Guid newsletterGuid = QueryHelper.GetGuid("newsletterguid", Guid.Empty);
        string subscriptionHash = QueryHelper.GetString("subscriptionhash", string.Empty);
        Guid issueGuid = QueryHelper.GetGuid("issueGuid", Guid.Empty);
        int issueID = QueryHelper.GetInteger("issueid", 0);
        int contactId = QueryHelper.GetInteger("contactid", 0);
        bool unsubscribed = false;

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
                ShowError(GetString("newsletter.unsubscribefailed"));
                return;
            }
        }

        // Get site ID
        int siteId = 0;
        if (CMSContext.CurrentSite != null)
        {
            siteId = CMSContext.CurrentSiteID;
        }

        if ((subscriberGuid != Guid.Empty) && (newsletterGuid != Guid.Empty) && (siteId != 0))
        {
            Subscriber subscriber = SubscriberProvider.GetSubscriber(subscriberGuid, siteId);
            if (subscriber == null)
            {
                ShowError(GetString("Unsubscribe.SubscriberDoesNotExist"));
                return;
            }
            // Show error message if subscriber type is 'Role'
            if (!string.IsNullOrEmpty(subscriber.SubscriberType) && subscriber.SubscriberType.Equals(SiteObjectType.ROLE, StringComparison.InvariantCultureIgnoreCase))
            {
                ShowError(GetString("Unsubscribe.CannotUnsubscribeRole"));
                return;
            }

            Newsletter newsletter = NewsletterProvider.GetNewsletter(newsletterGuid, siteId);
            if (newsletter == null)
            {
                ShowError(GetString("Unsubscribe.NewsletterDoesNotExist"));
                return;
            }

            // Check if subscriber with given GUID is subscribed to specified newsletter
            if (SubscriberProvider.IsSubscribed(subscriber.SubscriberID, newsletter.NewsletterID))
            {
                bool isSubscribed = true;

                if (string.IsNullOrEmpty(subscriber.SubscriberType) || !subscriber.SubscriberType.Equals(PredefinedObjectType.CONTACTGROUP, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Unsubscribe action
                    SubscriberProvider.Unsubscribe(subscriber.SubscriberID, newsletter.NewsletterID);
                }
                else
                {
                    // Check if the contact group member has unsubscription activity for the specified newsletter
                    isSubscribed = (contactId > 0) && !ModuleCommands.OnlineMarketingIsContactUnsubscribed(contactId, newsletter.NewsletterID, siteId);
                }

                if (isSubscribed)
                {
                    // Log newsletter unsubscription activity
                    LogActivity(subscriber, 0, newsletter.NewsletterID, issueID, issueGuid, siteId, contactId);

                    // Display confirmation
                    ShowInformation(GetString("Unsubscribe.Unsubscribed"));
                    unsubscribed = true;
                }
                else
                {
                    // Contact group member is already unsubscribed
                    ShowError(GetString("Unsubscribe.NotSubscribed"));
                }
            }
            else
            {
                ShowError(GetString("Unsubscribe.NotSubscribed"));
            }
        }
        // Check if subscription approval hash is supplied
        else if (!string.IsNullOrEmpty(subscriptionHash))
        {
            SubscriberNewsletterInfo sni = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriptionHash);
            // Check if hash is valid
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
                            LogActivity(null, sni.SubscriberID, sni.NewsletterID, issueID, issueGuid, siteId, contactId);

                            // Display confirmation
                            ShowInformation(GetString("Unsubscribe.Unsubscribed"));
                            unsubscribed = true;
                        }
                        else
                        {
                            // Contact group member is already unsubscribed
                            ShowError(GetString("Unsubscribe.NotSubscribed"));
                        }
                        break;

                    // Subscription was already approved
                    case SubscriberProvider.ApprovalResult.Failed:
                        ShowError(GetString("newsletter.unsubscribefailed"));
                        break;

                    case SubscriberProvider.ApprovalResult.TimeExceeded:
                        ShowError(GetString("newsletter.approval_timeexceeded"));
                        break;

                    // Subscription not found
                    default:
                    case SubscriberProvider.ApprovalResult.NotFound:
                        ShowError(GetString("Unsubscribe.NotSubscribed"));
                        break;
                }
            }
            else
            {
                ShowError(GetString("Unsubscribe.NotSubscribed"));
            }
        }
        else
        {
            if (subscriberGuid == Guid.Empty)
            {
                ShowError(GetString("Unsubscribe.SubscriberDoesNotExist"));
            }
            if (newsletterGuid == Guid.Empty)
            {
                ShowError(GetString("Unsubscribe.NewsletterDoesNotExist"));
            }
        }

        // Increase unsubscribed count
        if (unsubscribed)
        {
            // If Issue ID was provided
            if (issueID > 0)
            {
                IssueProvider.Unsubscribe(issueID);
                return;
            }
            // Otherwise try using the Issue GUID
            if (issueGuid != Guid.Empty)
            {
                Issue issue = IssueProvider.GetIssue(issueGuid, siteId);
                if (issue == null)
                {
                    return;
                }

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
    /// <param name="issueId">Issue ID</param>
    /// <param name="issueGuid">Issue GUID</param>
    /// <param name="siteId">Site ID</param>
    /// <param name="contactId">Contact ID is present if the mail is sent to a contact group</param>
    private void LogActivity(Subscriber sb, int subscriberId, int newsletterId, int issueId, Guid issueGuid, int siteId, int contactId)
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

                // Log activity
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