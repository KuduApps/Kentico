using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.EmailEngine;

[Security(Resource = "CMS.Newsletter", UIElements = "EmailQueue")]
public partial class CMSModules_Newsletters_Tools_EmailQueue_NewsletterEmailQueue : CMSNewsletterPage
{
    #region "Private variables"

    private int siteId;

    private bool emailsEnabled = false;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        siteId = CMSContext.CurrentSiteID;

        emailsEnabled = EmailHelper.Settings.EmailsEnabled(CMSContext.CurrentSiteName);
        emailsEnabled |= SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSGenerateNewsletters");

        // Display disabled information
        lblDisabled.Visible = !emailsEnabled;

        // Initialize unigrid
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.WhereCondition = "EmailSiteID = @SiteID";

        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@SiteID", siteId);

        gridElem.QueryParameters = parameters;

        InitializeActionMenu();
    }

    #endregion


    #region "Unigrid events"

    /// <summary>
    /// Handles Unigrid's OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "subject":
                return TextHelper.LimitLength(HTMLHelper.HTMLEncode(parameter.ToString()), 50);

            case "result":
                return TextHelper.LimitLength(HTMLHelper.HTMLEncode(parameter.ToString()), 50);

            case "subjecttooltip":
            case "resulttooltip":
                return parameter.ToString().Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        return null;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "configure"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "configure");
        }

        switch (actionName.ToLower())
        {
            case "resend":
                // Resend an issue from the queue
                EmailQueueManager.ResendEmail(Convert.ToInt32(actionArgument));
                break;

            case "delete":
                // Delete EmailQueueItem object from database
                EmailQueueManager.DeleteEmailQueueItem(Convert.ToInt32(actionArgument));
                break;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes action menu.
    /// </summary>
    protected void InitializeActionMenu()
    {
        // Resend all failed
        string[,] actions = new string[5, 10];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("NewsletterEmailQueue_List.ResendAllFailed");
        actions[0, 6] = "resendallfailed";

        // Resend all
        actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[1, 1] = GetString("NewsletterEmailQueue_List.ResendAll");
        actions[1, 6] = "resendall";

        // Delete all failed
        actions[2, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[2, 1] = GetString("NewsletterEmailQueue_List.DeleteAllFailed");
        actions[2, 6] = "deleteallfailed";

        // Delete all
        actions[3, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[3, 1] = GetString("NewsletterEmailQueue_List.DeleteAll");
        actions[3, 6] = "deleteall";

        // Refresh
        actions[4, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[4, 1] = GetString("general.refresh");
        actions[4, 5] = GetImageUrl("CMSModules/CMS_EmailQueue/refresh.png");
        actions[4, 6] = "refresh";

        CurrentMaster.HeaderActions.Actions = actions;
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        CurrentMaster.HeaderActions.PreRender += HeaderActions_PreRender;
    }

    #endregion


    #region "Header action events"

    protected void HeaderActions_PreRender(object sender, EventArgs e)
    {
        bool enabled = (gridElem.GridView.Rows.Count > 0);
        bool resending = enabled && (ThreadEmailSender.SendingThreads <= 0) && emailsEnabled;

        // Resend all failed
        CurrentMaster.HeaderActions.Actions[0, 2] = resending ? "if (!confirm(" + ScriptHelper.GetString(GetString("NewsletterEmailQueue_List.ResendAllFailedConfirmationMessage")) + ")) return false;" :
                                                                null;
        CurrentMaster.HeaderActions.Actions[0, 5] = resending ? GetImageUrl("CMSModules/CMS_EmailQueue/resendallfailed.png") :
                                                                GetImageUrl("CMSModules/CMS_EmailQueue/resendallfailed_disabled.png");
        CurrentMaster.HeaderActions.Actions[0, 9] = resending.ToString();

        // Resend all
        CurrentMaster.HeaderActions.Actions[1, 2] = resending ? "if (!confirm(" + ScriptHelper.GetString(GetString("EmailQueue.ResendAllConfirmation")) + ")) return false;" :
                                                                null;
        CurrentMaster.HeaderActions.Actions[1, 5] = resending ? GetImageUrl("CMSModules/CMS_EmailQueue/resendselected.png") :
                                                                GetImageUrl("CMSModules/CMS_EmailQueue/resendselected_disabled.png");
        CurrentMaster.HeaderActions.Actions[1, 9] = resending.ToString();

        // Delete all failed
        CurrentMaster.HeaderActions.Actions[2, 2] = enabled ? "if (!confirm(" + ScriptHelper.GetString(GetString("NewsletterEmailQueue_List.DeleteAllFailedConfirmationMessage")) + ")) return false;" :
                                                                null;
        CurrentMaster.HeaderActions.Actions[2, 5] = enabled ? GetImageUrl("CMSModules/CMS_EmailQueue/resendall.png") :
                                                                GetImageUrl("CMSModules/CMS_EmailQueue/resendall_disabled.png");
        CurrentMaster.HeaderActions.Actions[2, 9] = enabled.ToString();

        // Delete all
        CurrentMaster.HeaderActions.Actions[3, 2] = enabled ? "if (!confirm(" + ScriptHelper.GetString(GetString("NewsletterEmailQueue_List.DeleteAllConfirmationMessage")) + ")) return false;" :
                                                                null;
        CurrentMaster.HeaderActions.Actions[3, 5] = enabled ? GetImageUrl("CMSModules/CMS_EmailQueue/deleteallfailed.png") :
                                                                GetImageUrl("CMSModules/CMS_EmailQueue/deleteallfailed_disabled.png");
        CurrentMaster.HeaderActions.Actions[3, 9] = enabled.ToString();

        CurrentMaster.HeaderActions.ReloadData();
    }


    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Check user permission (for complex operations only)
        if (e.CommandName != "refresh")
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "configure"))
            {
                RedirectToCMSDeskAccessDenied("cms.newsletter", "configure");
            }
        }

        switch (e.CommandName.ToLower())
        {
            case "resendall":
                EmailQueueManager.SendAllEmails(true, true, 0);
                gridElem.ReloadData();
                this.lblText.Text = "<strong>" + GetString("EmailQueue.SendingEmails") + "</strong>";
                break;

            case "resendallfailed":
                EmailQueueManager.SendAllEmails(true, false, 0);
                gridElem.ReloadData();
                this.lblText.Text = "<strong>" + GetString("EmailQueue.SendingEmails") + "</strong>";
                break;

            case "deleteall":
                EmailQueueManager.DeleteAllEmailQueueItems(siteId);
                gridElem.ReloadData();
                break;

            case "deleteallfailed":
                EmailQueueManager.DeleteFailedEmailQueueItems(siteId);
                gridElem.ReloadData();
                break;

            case "refresh":
                gridElem.ReloadData();
                break;
        }
    }

    #endregion
}
