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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_CMSPages_GetNewsletterIssue : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check read permission for newsletters
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Newsletter", "Read"))
        {
            int issueId = QueryHelper.GetInteger("IssueId", 0);
            if (issueId > 0)
            {
                // Get newsletter issue
                Issue iss = IssueProvider.GetIssue(issueId);
                if ((iss != null) && (iss.IssueSiteID == CMSContext.CurrentSiteID))
                {
                    // Get newsletter
                    Newsletter news = NewsletterProvider.GetNewsletter(iss.IssueNewsletterID);

                    Response.Clear();
                    Response.Write(IssueProvider.GetEmailBody(iss, news, null, null, false, CMSContext.CurrentSiteName, null, null, null));
                    Response.Flush();

                    RequestHelper.EndResponse();
                }
            }
        }
    }
}
