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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Header : CMSNewsletterNewslettersPage
{
    protected int newsletterId;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get newsletter id from querystring
        newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        // Try to get nesletter display name
        string newsName = "";
        Newsletter news = NewsletterProvider.GetNewsletter(newsletterId);
        if (news != null)
        {
            newsName = news.NewsletterDisplayName;
        }

        // Initializes page title
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Newsletter_Edit.Newsletters");
        breadcrumbs[0, 1] = "~/CMSModules/Newsletters/Tools/Newsletters/Newsletter_List.aspx";
        breadcrumbs[0, 2] = "newslettersContent";
        breadcrumbs[1, 0] = newsName;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "issues_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initialize tabs
        InitalizeMenu(news);
    }


    /// <summary>
    /// Initializes newsletter menu.
    /// </summary>
    /// <param name="news">Newsletter object</param>
    protected void InitalizeMenu(Newsletter news)
    {
        if (news != null)
        {
            string[,] tabs = null;

            if (news.NewsletterType == NewsletterType.Dynamic)
            {
                tabs = new string[4, 4];

                tabs[3, 0] = GetString("Newsletter_Header.Send");
                tabs[3, 1] = "SetHelpTopic('helpTopic', 'send_tab');";
                tabs[3, 2] = "Newsletter_Send.aspx?newsletterid=" + newsletterId;
            }
            else
            {
                tabs = new string[3, 4];
            }
            tabs[0, 0] = GetString("Newsletter_Header.Issues");
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'issues_tab');";
            tabs[0, 2] = "Newsletter_Issue_List.aspx?newsletterid=" + newsletterId;
            tabs[1, 0] = GetString("Newsletter_Header.Configuration");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'configuration_tab');";
            tabs[1, 2] = "Newsletter_Configuration.aspx?newsletterid=" + newsletterId;
            tabs[2, 0] = GetString("Newsletter_Header.Subscribers");
            tabs[2, 1] = "SetHelpTopic('helpTopic', 'subscribers_tab');";
            tabs[2, 2] = "Newsletter_Subscribers.aspx?newsletterid=" + newsletterId;

            this.CurrentMaster.Tabs.Tabs = tabs;
            this.CurrentMaster.Tabs.UrlTarget = "newsletterContent";

            if (ValidationHelper.GetInteger(Request.QueryString["saved"], 0) > 0)
            {
                //user was redirected from Newsletter_New.aspx => show 'configuration' tab
                this.CurrentMaster.Tabs.SelectedTab = 1;
            }
        }
    }
}
