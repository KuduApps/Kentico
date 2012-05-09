using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Preview : CMSUserControl
{
    // Maximal number of subscribers for preview
    private const int MAX_PREVIEW_SUBSCRIBERS = 20;


    protected void Page_Load(object sender, EventArgs e)
    {
        InitGUI();

        // Get issue ID from query string
        int newsletterIssueId = QueryHelper.GetInteger("issueid", 0);

        // Get Issue object
        Issue issue = IssueProvider.GetIssue(newsletterIssueId);
        if (issue == null)
        {
            lblError.Visible = true;
            lblError.Text = GetString("Newsletter_Issue_New_Preview.NoIssue");

            return;
        }

        // Get specific number of subscribers subscribed to the newsletter
        string where = "SubscriberID IN (SELECT SubscriberID FROM Newsletter_SubscriberNewsletter WHERE NewsletterID=" + issue.IssueNewsletterID + " AND (SubscriptionApproved = 1 OR SubscriptionApproved IS NULL))";
        DataSet subscribers = SubscriberProvider.GetSubscribers(where, null, MAX_PREVIEW_SUBSCRIBERS, null);

        string script;
        if (!DataHelper.DataSourceIsEmpty(subscribers))
        {
            // Limit max subscribers count to number of rows
            int maxCount = subscribers.Tables[0].Rows.Count;

            // Generate javascript based on subscribers
            script = string.Format(@"newsletterIssueId ={0};
                                     var guid = new Array({1});
                                     var email = new Array({1});
                                     var subject = new Array({1});
                                     var subscribers = new Array(guid, email);",
                                     newsletterIssueId,
                                     maxCount);

            // Ensure correct subject culture
            string siteName = CMSContext.CurrentSiteName;
            string culture = CultureHelper.GetDefaultCulture(siteName);

            // Get newsletter object
            Newsletter news = NewsletterProvider.GetNewsletter(issue.IssueNewsletterID);

            // Get subject base
            string subjectBase = GetString("general.subject") + ResHelper.Colon;

            Subscriber subscriber = null;
            Subscriber sb = null;
            SortedDictionary<int, Subscriber> subMembers = null;
            string infoLine = null;
            string subject = null;
            IssueHelper ih = new IssueHelper();

            for (int i = 0; i < maxCount; i++)
            {
                // Get subscriber
                subscriber = new Subscriber(subscribers.Tables[0].Rows[i]);
                if (subscriber != null)
                {
                    // Insert subscriber GUID
                    script = string.Format("{0} guid[{1}] = '{2}'; \n ", script, i, subscriber.SubscriberGUID);

                    // Get subscriber's member (different for user, role or contact group subscribers)
                    subMembers = SubscriberProvider.GetSubscribers(subscriber, 1, 0);
                    foreach (KeyValuePair<int, Subscriber> item in subMembers)
                    {
                        // Get 1st subscriber's member
                        sb = item.Value;
                        if (sb != null)
                        {
                            // Create information line
                            infoLine = ScriptHelper.GetString(sb.SubscriberEmail, false);

                            // Add info about subscriber type
                            if (sb.SubscriberType == SiteObjectType.USER)
                            {
                                infoLine = string.Format("{0} ({1})", infoLine, GetString("objecttype.cms_user").ToLower());
                            }
                            else if (sb.SubscriberType == SiteObjectType.ROLE)
                            {
                                infoLine = string.Format("{0} ({1})", infoLine, GetString("objecttype.cms_role").ToLower());
                            }
                            else if (sb.SubscriberType == PredefinedObjectType.CONTACTGROUP)
                            {
                                infoLine = string.Format("{0} ({1})", infoLine, GetString("objecttype.om_contactgroup").ToLower());
                            }

                            script = string.Format("{0} email[{1}] = '{2}'; \n ", script, i, infoLine);

                            // Resolve dynamic field macros ({%FirstName%}, {%LastName%}, {%Email%})
                            if (ih.LoadDynamicFields(sb, news, null, issue, true, siteName, null, null, null))
                            {
                                subject = ih.ResolveDynamicFieldMacros(issue.IssueSubject);
                            }

                            // Create resolved subject
                            subject = HTMLHelper.HTMLEncode(string.Format("{0} {1}", subjectBase, subject));
                            script = string.Format("{0}subject[{1}] = {2}; \n ", script, i, ScriptHelper.GetString(subject));
                        }
                    }
                }
            }
        }
        else
        {
            // No subscribers? => hide 'prev' and 'next' link buttons
            pnlLinkButtons.Visible = false;

            // Generate void javascript 
            script = string.Format(@"newsletterIssueId ={0};
                                     var guid = new Array(1);
                                     var email = new Array(1);
                                     var subscribers = new Array(guid, email);
                                     guid[1] = 0;
                                     email[1] = 0;",
                                     newsletterIssueId);
        }
        ltlScript.Text = ScriptHelper.GetScript(script);

        if (!RequestHelper.IsPostBack())
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "LoadPreview" + ClientID, ScriptHelper.GetScript("pageLoad();"));
        }
    }


    /// <summary>
    /// GUI initialization.
    /// </summary>
    protected void InitGUI()
    {
        lblSubscriber.Text = GetString("Newsletter_Issue_New_Preview.Subscriber") + "&nbsp;&nbsp;&nbsp;&nbsp;";

        lnkNext.Text = GetString("general.next") + " >";
        lblNext.Text = GetString("general.next") + " >";
        lnkPrevious.Text = "< " + GetString("general.back");
        lblPrevious.Text = "< " + GetString("general.back");
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        string elemString = "var lblEmail = '" + lblEmail.ClientID + "';\n" +
            "var lblPrev = '" + lblPrevious.ClientID + "';\n" +
            "var lnkPrev = '" + lnkPrevious.ClientID + "';\n" +
            "var lblNext = '" + lblNext.ClientID + "';\n" +
            "var lnkNext = '" + lnkNext.ClientID + "';\n" +
            "var lblSubj = '" + lblSubject.ClientID + "';\n";

        // Register client IDs of the elements
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "PreviewElems", ScriptHelper.GetScript(elemString));
    }
}