using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_Newsletters_Controls_SubscriptionApproval : CMSUserControl
{
    #region "Variables"

    private string mSuccessfulApprovalText = null;
    private string mUnsuccessfulApprovalText = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets successful approval text.
    /// </summary>
    public string SuccessfulApprovalText
    {
        get
        {
            return mSuccessfulApprovalText;
        }
        set
        {
            mSuccessfulApprovalText = value;
        }
    }


    /// <summary>
    /// Gets or sets unsuccessful approval text.
    /// </summary>
    public string UnsuccessfulApprovalText
    {
        get
        {
            return mUnsuccessfulApprovalText;
        }
        set
        {
            mUnsuccessfulApprovalText = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // If StopProcessing flag is set, do nothing
        if (StopProcessing)
        {
            Visible = false;
            return;
        }

        string subscriptionHash = QueryHelper.GetString("subscriptionhash", string.Empty);
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
                lblInfo.Text = ResHelper.GetString("newsletter.approval_failed");
                return;
            }
        }

        if (string.IsNullOrEmpty(subscriptionHash))
        {
            this.Visible = false;
            return;
        }

        // Try to approve subscription
        SubscriberProvider.ApprovalResult result = SubscriberProvider.ApproveSubscription(subscriptionHash, false, CMSContext.CurrentSiteName, datetime);

        switch (result)
        {
            // Approving subscription was successful
            case SubscriberProvider.ApprovalResult.Success:
                if (!String.IsNullOrEmpty(this.SuccessfulApprovalText))
                {
                    lblInfo.Text = this.SuccessfulApprovalText;
                }
                else
                {
                    lblInfo.Text = ResHelper.GetString("newsletter.successful_approval");
                }

                // Log newsletter subscription activity
                if ((CMSContext.ViewMode == ViewModeEnum.LiveSite))
                {
                    SubscriberNewsletterInfo sni = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriptionHash);
                    if (sni != null)
                    {
                        // Load subscriber info and make sure activity modul is enabled
                        Subscriber sb = SubscriberProvider.GetSubscriber(sni.SubscriberID);
                        if ((sb != null) && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(sb.SubscriberSiteID))
                        {
                            int siteId = sb.SubscriberSiteID;
                            Newsletter news = NewsletterProvider.GetNewsletter(sni.NewsletterID);
                            if (news.NewsletterLogActivity && ActivitySettingsHelper.NewsletterSubscribeEnabled(siteId))
                            {
                                // Under what contacs this subscriber belogs to?
                                int contactId = ActivityTrackingHelper.GetContactID(sb);

                                if (contactId > 0)
                                {
                                    ModuleCommands.OnlineMarketingUpdateContactFromExternalData(sb, contactId);
                                    ModuleCommands.OnlineMarketingCreateRelation(sb.SubscriberID, MembershipType.NEWSLETTER_SUBSCRIBER, contactId);
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
                }
                break;

            // Subscription was already approved
            case SubscriberProvider.ApprovalResult.Failed:
                if (!String.IsNullOrEmpty(this.UnsuccessfulApprovalText))
                {
                    lblInfo.Text = this.UnsuccessfulApprovalText;
                }
                else
                {
                    lblInfo.Text = ResHelper.GetString("newsletter.approval_failed");
                }
                break;

            case SubscriberProvider.ApprovalResult.TimeExceeded:
                if (!String.IsNullOrEmpty(this.UnsuccessfulApprovalText))
                {
                    lblInfo.Text = this.UnsuccessfulApprovalText;
                }
                else
                {
                    lblInfo.Text = ResHelper.GetString("newsletter.approval_timeexceeded");
                }
                break;


            // Subscription not found
            default:
            case SubscriberProvider.ApprovalResult.NotFound:
                if (!String.IsNullOrEmpty(this.UnsuccessfulApprovalText))
                {
                    lblInfo.Text = this.UnsuccessfulApprovalText;
                }
                else
                {
                    lblInfo.Text = ResHelper.GetString("newsletter.approval_invalid");
                }
                break;
        }
    }

    #endregion
}
