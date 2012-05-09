using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.UIControls;

[Security(Resource = "CMS.Newsletter", UIElements = "ExportSubscribers")]
public partial class CMSModules_Newsletters_Tools_ImportExportSubscribers_Subscriber_Export : CMSNewsletterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize newsletter selector
        usNewsletters.WhereCondition = "NewsletterSiteID = " + CMSContext.CurrentSiteID;

        // Initialize update panel progress
        pnlUpdate.ShowProgress = true;
        string text = GetString("general.exporting") + "...";
        pnlUpdate.ProgressHTML = "<div class=\"UP\"><img src=\"" + UIHelper.GetImageUrl(this.Page, "Design/Preloaders/preload16.gif") + "\" alt=\"" + text + "\" tooltip=\"" + text + "\"  /><span>" + text + "</span></div>";

        // Initialize radio button list items text
        rblExportList.Items[0].Text = GetString("newsletter.allsubscribers");
        rblExportList.Items[1].Text = GetString("general.approved");
        rblExportList.Items[2].Text = GetString("general.notapproved");

        if (!RequestHelper.IsPostBack())
        {
            rblExportList.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// Handles export button click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        // Check "manage subscribers" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managesubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managesubscribers");
        }

        // Get selected newsletters
        ArrayList newsletterIds = new ArrayList();
        string values = ValidationHelper.GetString(usNewsletters.Value, null);
        if (!String.IsNullOrEmpty(values))
        {
            string[] newItems = values.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                foreach (string item in newItems)
                {
                    newsletterIds.Add(ValidationHelper.GetInteger(item, 0));
                }
            }
        }

        // Export subscribers
        string subscribers = null;
        if (CMSContext.CurrentSite != null)
        {
            subscribers = SubscriberProvider.ExportSubscribersFromSite(newsletterIds, CMSContext.CurrentSiteID, true, ValidationHelper.GetInteger(rblExportList.SelectedValue, 0));
        }

        // No subscribers exported
        if (string.IsNullOrEmpty(subscribers))
        {
            lblInfo.Text = GetString("Subscriber_Export.noSubscribersExported");
            lblInfo.Visible = true;
            txtExportSub.Enabled = false;
        }
        else
        {
            lblInfo.Text = GetString("Subscriber_Export.subscribersExported");
            lblInfo.Visible = true;
            txtExportSub.Enabled = true;
        }

        txtExportSub.Text = subscribers;
    }
}