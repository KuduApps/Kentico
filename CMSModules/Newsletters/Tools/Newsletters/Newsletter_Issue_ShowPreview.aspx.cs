using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Threading;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_ShowPreview : CMSNewsletterNewslettersPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Guid subscriberGuid = QueryHelper.GetGuid("subscriberguid", Guid.Empty);
        int newsletterIssueId = QueryHelper.GetInteger("issueid", 0);

        // Get newsletter issue
        Issue issue = IssueProvider.GetIssue(newsletterIssueId);
        if (issue == null)
        {
            return;
        }

        // Get subscriber
        Subscriber subscriber = SubscriberProvider.GetSubscriber(subscriberGuid, CMSContext.CurrentSiteID);

        // Get the newsletter
        Newsletter news = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);
        if (news == null)
        {
            return;
        }

        // Get site default culture
        string culture = CultureHelper.GetDefaultCulture(CMSContext.CurrentSiteName);

        // Ensure preview in default site culture
        // Keep old culture
        System.Globalization.CultureInfo oldUICulture = Thread.CurrentThread.CurrentUICulture;

        // Set current culture
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);

        string htmlPage = String.Empty;
        try
        {
            if (subscriber != null)
            {
                // Get subscriber's member (different for user or role subscribers)
                SortedDictionary<int, Subscriber> subscribers = SubscriberProvider.GetSubscribers(subscriber, 1, 0);
                foreach (KeyValuePair<int, Subscriber> item in subscribers)
                {
                    // Get 1st subscriber's member
                    Subscriber sb = item.Value;

                    htmlPage = IssueProvider.GetEmailBody(issue, news, null, sb, true, CMSContext.CurrentSiteName, null, null, null);
                }
            }

            if (string.IsNullOrEmpty(htmlPage))
            {
                htmlPage = IssueProvider.GetEmailBody(issue, news, null, null, true, CMSContext.CurrentSiteName, null, null, null);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            // Set back to old culture
            Thread.CurrentThread.CurrentUICulture = oldUICulture;
        }

        Response.Clear();
        Response.Write(htmlPage);

        RequestHelper.EndResponse();
    }
}
