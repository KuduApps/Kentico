using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.UIControls;

[Security(Resource = "CMS.Newsletter", UIElements = "ImportSubscribers")]
public partial class CMSModules_Newsletters_Tools_ImportExportSubscribers_Subscriber_Import : CMSNewsletterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize newsletter selector
        usNewsletters.WhereCondition = "NewsletterSiteID = " + CMSContext.CurrentSiteID;

        // Registers script for disabling checkbox
        radSubscribe.Attributes.Add("onclick", "SelectionChanged()");
        radUnsubscribe.Attributes.Add("onclick", "SelectionChanged()");
        radDelete.Attributes.Add("onclick", "SelectionChanged()");
        string script = "function SelectionChanged() { \n" +
                            "   var radSubscribe = document.getElementById('" + this.radSubscribe.ClientID + "').checked; \n" +
                            "   document.getElementById('" + this.chkDoNotSubscribe.ClientID + "').disabled = !radSubscribe; \n" +
                            "} \n";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DisableCheckbox", ScriptHelper.GetScript(script));

        // Initialize update panel progress
        pnlUpdate.ShowProgress = true;
        string text = GetString("general.importing") + "...";
        pnlUpdate.ProgressHTML = "<div class=\"UP\"><img src=\"" + UIHelper.GetImageUrl(this.Page, "Design/Preloaders/preload16.gif") + "\" alt=\"" + text + "\" tooltip=\"" + text + "\"  /><span>" + text + "</span></div>";
    }


    /// <summary>
    /// Handles import button click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        // Check 'Manage subscribers' permission
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

        // Import subscribers
        try
        {
            if (CMSContext.CurrentSite != null)
            {
                int siteId = CMSContext.CurrentSiteID;
                ArrayList errors = new ArrayList();
                // Add subscribers to site and subscribe them to selected newsletters
                if (radSubscribe.Checked)
                {
                    errors = SubscriberProvider.ImportSubscribersToSite(txtImportSub.Text, newsletterIds, siteId, true, chkSendConfirmation.Checked, chkDoNotSubscribe.Checked, chkRequireOptIn.Checked);
                    if (errors.Count == 0)
                    {
                        lblInfo.Text = GetString("Subscriber_Import.SubscribersImported");
                        lblInfo.Visible = true;
                    }
                }
                // Unsubscribe inserted subscribers from selected newsletters
                else if (radUnsubscribe.Checked)
                {
                    errors = SubscriberProvider.UnsubscribeFromNewsletters(txtImportSub.Text, newsletterIds, siteId, chkSendConfirmation.Checked);
                    if (errors.Count == 0)
                    {
                        lblInfo.Text = GetString("Subscriber_Import.SubscribersUnsubscribed");
                        lblInfo.Visible = true;
                    }
                }
                // Delete inserted subscribers
                else if (radDelete.Checked)
                {
                    errors = SubscriberProvider.DeleteSubscribers(txtImportSub.Text, siteId);
                    if (errors.Count == 0)
                    {
                        lblInfo.Text = GetString("Subscriber_Import.SubscribersDeleted");
                        lblInfo.Visible = true;
                    }
                }

                if (errors.Count > 0)
                {
                    lblError.Text = CreateErrorString(errors);
                    lblError.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// Creates error message from given array.
    /// </summary>
    /// <param name="errors">Array list with errors</param>
    private string CreateErrorString(ArrayList errors)
    {
        string value = string.Empty;

        foreach (string error in errors)
        {
            value += error + "<br />";
        }

        return value;
    }
}
