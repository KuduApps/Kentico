using System;
using System.Data.SqlTypes;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.Scheduler;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Configuration : CMSNewsletterNewslettersPage
{
    #region "Variables"

    /// <summary>
    /// Newsletter ID.
    /// </summary>
    protected int newsletterId = 0;


    /// <summary>
    /// It is true if edited newsletter is dynamic.
    /// </summary>
    protected bool isDynamic = false;


    /// <summary>
    /// Determines if Online Marketing is enabled.
    /// </summary>
    private bool onlineMarketingEnabled = NewsletterProvider.OnlineMarketingEnabled(CMSContext.CurrentSiteName);

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get newsletter ID from query string
        newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        if (newsletterId == 0)
        {
            return;
        }

        rfvNewsletterDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvNewsletterName.ErrorMessage = GetString("Newsletter_Edit.ErrorEmptyName");
        rfvNewsletterSenderName.ErrorMessage = GetString("Newsletter_Edit.ErrorEmptySenderName");
        rfvNewsletterSenderEmail.ErrorMessage = GetString("Newsletter_Edit.ErrorEmptySenderEmail");

        if (!RequestHelper.IsPostBack())
        {
            if (QueryHelper.GetInteger("saved", 0) > 0)
            {
                // If user was redirected from newsletter_new.aspx, display the 'Changes were saved' message
                ShowInformation(GetString("General.ChangesSaved"));
            }
        }

        // Load newsletter configuration
        LoadData();
    }


    protected void LoadData()
    {
        // Get newsletter object and check if exists
        Newsletter newsletterObj = NewsletterProvider.GetNewsletter(newsletterId);
        EditedObject = newsletterObj;

        // Initialize issue selectors
        int siteId = newsletterObj.NewsletterSiteID;
        subscriptionTemplate.WhereCondition = "TemplateType='S' AND TemplateSiteID=" + siteId;
        unsubscriptionTemplate.WhereCondition = "TemplateType='U' AND TemplateSiteID=" + siteId;
        issueTemplate.WhereCondition = "TemplateType='I' AND TemplateSiteID=" + siteId;
        optInSelector.WhereCondition = "TemplateType='D' AND TemplateSiteID=" + siteId;

        // Check if the newsletter is dynamic and adjust config dialog
        isDynamic = newsletterObj.NewsletterType == NewsletterType.Dynamic;

        lblDynamic.Visible = pnlDynamic.Visible = lblNewsletterDynamicURL.Visible =
            txtNewsletterDynamicURL.Visible = plcUrl.Visible = chkSchedule.Visible =
            lblSchedule.Visible = plcInterval.Visible = isDynamic;

        lblTemplateBased.Visible = lblIssueTemplate.Visible = issueTemplate.Visible = !isDynamic;

        if (RequestHelper.IsPostBack())
        {
            if (isDynamic)
            {
                schedulerInterval.Visible = chkSchedule.Checked;
            }
            else
            {
                plcInterval.Visible = false;
            }

            return;
        }

        // Fill config dialog with newsletter data
        GetNewsletterValues(newsletterObj);

        if (!isDynamic)
        {
            issueTemplate.Value = newsletterObj.NewsletterTemplateID.ToString();
            return;
        }

        // Check if dynamic newsletter subject is empty
        bool subjectEmpty = string.IsNullOrEmpty(newsletterObj.NewsletterDynamicSubject);
        radPageTitle.Checked = subjectEmpty;
        radFollowing.Checked = !subjectEmpty;
        radPageTitle_CheckedChanged(null, null);
        if (!subjectEmpty)
        {            
            txtSubject.Text = newsletterObj.NewsletterDynamicSubject;
        }

        txtNewsletterDynamicURL.Text = newsletterObj.NewsletterDynamicURL;

        TaskInfo task = TaskInfoProvider.GetTaskInfo(newsletterObj.NewsletterDynamicScheduledTaskID);        
        if (task != null)
        {
            chkSchedule.Checked = plcInterval.Visible = true;
            schedulerInterval.ScheduleInterval = task.TaskInterval;
        }
        else
        {
            chkSchedule.Checked = false;
            schedulerInterval.Visible = false;
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check "configure" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "configure"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "configure");
        }

        string errorMessage = ValidateNewsletterValues();
        if (!string.IsNullOrEmpty(errorMessage))
        {
            ShowError(errorMessage);
            return;
        }

        Newsletter newsletterObj = NewsletterProvider.GetNewsletter(txtNewsletterName.Text.Trim(), CMSContext.CurrentSiteID);

        // Newsletter's code name must be unique
        if (newsletterObj != null && newsletterObj.NewsletterID != newsletterId)
        {
            ShowError(GetString("Newsletter_Edit.NewsletterNameExists"));
            return;
        }

        if (newsletterObj == null)
        {
            newsletterObj = NewsletterProvider.GetNewsletter(newsletterId);
        }

        SetNewsletterValues(newsletterObj);

        // Check if subscription template was selected
        int subscriptionTemplateValue = ValidationHelper.GetInteger(subscriptionTemplate.Value, 0);
        if (subscriptionTemplateValue == 0)
        {
            ShowError(GetString("Newsletter_Edit.NoSubscriptionTemplateSelected"));
            return;
        }
        newsletterObj.NewsletterSubscriptionTemplateID = subscriptionTemplateValue;

        // Check if double opt-in template was selected
        int optInTemplateValue = ValidationHelper.GetInteger(optInSelector.Value, 0);
        if (newsletterObj.NewsletterEnableOptIn && optInTemplateValue == 0)
        {
            ShowError(GetString("Newsletter_Edit.NoOptInTemplateSelected"));
            return;
        }
        newsletterObj.NewsletterOptInTemplateID = optInTemplateValue;

        // Check if unsubscription template was selected
        int unsubscriptionTemplateValue = ValidationHelper.GetInteger(unsubscriptionTemplate.Value, 0);
        if (unsubscriptionTemplateValue == 0)
        {
            ShowError(GetString("Newsletter_Edit.NoUnsubscriptionTemplateSelected"));
            return;
        }
        newsletterObj.NewsletterUnsubscriptionTemplateID = unsubscriptionTemplateValue;

        // ID of scheduled task which should be deleted
        int deleteScheduledTaskId = 0;

        if (isDynamic)
        {
            newsletterObj.NewsletterType = NewsletterType.Dynamic;
            newsletterObj.NewsletterDynamicURL = txtNewsletterDynamicURL.Text.Trim();
            newsletterObj.NewsletterDynamicSubject = radFollowing.Checked ? txtSubject.Text : string.Empty;

            if (chkSchedule.Checked)
            {
                // Set info for scheduled task
                TaskInfo task = GetDynamicNewsletterTask(newsletterObj);

                if (!schedulerInterval.CheckOneDayMinimum())
                {
                    // If problem occurred while setting schedule interval
                    ShowError(GetString("Newsletter_Edit.NoDaySelected"));
                    return;
                }

                if (!IsValidDate(SchedulingHelper.DecodeInterval(schedulerInterval.ScheduleInterval).StartTime))
                {
                    ShowError(GetString("Newsletter.IncorrectDate"));
                    return;
                }

                task.TaskInterval = schedulerInterval.ScheduleInterval;

                task.TaskNextRunTime = SchedulingHelper.GetNextTime(task.TaskInterval, new DateTime(), new DateTime());
                task.TaskDisplayName = GetString("DynamicNewsletter.TaskName") + newsletterObj.NewsletterDisplayName;
                task.TaskName = "DynamicNewsletter_" + newsletterObj.NewsletterName;
                // Set task for processing in external service
                task.TaskAllowExternalService = true;
                task.TaskUseExternalService = (SchedulingHelper.UseExternalService && NewsletterProvider.UseExternalServiceForDynamicNewsletters(CMSContext.CurrentSiteName));
                TaskInfoProvider.SetTaskInfo(task);
                newsletterObj.NewsletterDynamicScheduledTaskID = task.TaskID;
            }
            else
            {
                if (newsletterObj.NewsletterDynamicScheduledTaskID > 0)
                {
                    // Store task ID for deletion
                    deleteScheduledTaskId = newsletterObj.NewsletterDynamicScheduledTaskID;
                }
                newsletterObj.NewsletterDynamicScheduledTaskID = 0;
                schedulerInterval.Visible = false;
            }
        }
        else
        {
            newsletterObj.NewsletterType = NewsletterType.TemplateBased;

            // Check if issue template was selected
            int issueTemplateValue = ValidationHelper.GetInteger(issueTemplate.Value, 0);
            if (issueTemplateValue == 0)
            {
                ShowError(GetString("Newsletter_Edit.NoEmailTemplateSelected"));
                return;
            }
            newsletterObj.NewsletterTemplateID = issueTemplateValue;
        }

        // Save changes to DB
        NewsletterProvider.SetNewsletter(newsletterObj);
        if (deleteScheduledTaskId > 0)
        {
            // Delete scheduled task if schedule mail-outs were unchecked
            TaskInfoProvider.DeleteTaskInfo(deleteScheduledTaskId);
        }

        ShowInformation(GetString("General.ChangesSaved"));

        // Refresh header with display name
        ScriptHelper.RefreshTabHeader(Page, GetString("Newsletter_Header.Configuration"));
    }


    protected void radPageTitle_CheckedChanged(object sender, EventArgs e)
    {
        txtSubject.Enabled = radFollowing.Checked;
    }


    protected void radFollowing_CheckedChanged(object sender, EventArgs e)
    {
        txtSubject.Enabled = radFollowing.Checked;
    }


    protected void chkSchedule_CheckedChanged(object sender, EventArgs e)
    {
        schedulerInterval.Visible = chkSchedule.Checked;
    }


    private void GetNewsletterValues(Newsletter newsletter)
    {
        txtNewsletterDisplayName.Text = newsletter.NewsletterDisplayName;
        txtNewsletterName.Text = newsletter.NewsletterName;
        txtNewsletterSenderName.Text = newsletter.NewsletterSenderName;
        txtNewsletterSenderEmail.Text = newsletter.NewsletterSenderEmail;
        txtNewsletterBaseUrl.Text = newsletter.NewsletterBaseUrl;
        txtNewsletterUnsubscribeUrl.Text = newsletter.NewsletterUnsubscribeUrl;
        txtDraftEmails.Text = newsletter.NewsletterDraftEmails;
        chkUseEmailQueue.Checked = newsletter.NewsletterUseEmailQueue;

        subscriptionTemplate.Value = newsletter.NewsletterSubscriptionTemplateID.ToString();
        unsubscriptionTemplate.Value = newsletter.NewsletterUnsubscriptionTemplateID.ToString();

        plcOnlineMarketing.Visible = onlineMarketingEnabled;
        chkTrackOpenedEmails.Checked = newsletter.NewsletterTrackOpenEmails;
        chkTrackClickedLinks.Checked = newsletter.NewsletterTrackClickedLinks;
        chkLogActivity.Checked = newsletter.NewsletterLogActivity;

        chkEnableOptIn.Checked = newsletter.NewsletterEnableOptIn;
        optInSelector.Value = newsletter.NewsletterOptInTemplateID;
        txtOptInURL.Text = newsletter.NewsletterOptInApprovalURL;
        chkSendOptInConfirmation.Checked = newsletter.NewsletterSendOptInConfirmation;
    }


    private string ValidateNewsletterValues()
    {
        return new Validator()
                    .NotEmpty(txtNewsletterDisplayName.Text, GetString("general.requiresdisplayname"))
                    .NotEmpty(txtNewsletterName.Text, GetString("Newsletter_Edit.ErrorEmptyName"))
                    .NotEmpty(txtNewsletterSenderName.Text, GetString("Newsletter_Edit.ErrorEmptySenderName"))
                    .NotEmpty(txtNewsletterSenderEmail.Text, GetString("Newsletter_Edit.ErrorEmptySenderEmail"))
                    .IsEmail(txtNewsletterSenderEmail.Text.Trim(), GetString("Newsletter_Edit.ErrorEmailFormat"))
                    .IsCodeName(txtNewsletterName.Text, GetString("general.invalidcodename"))
                    .Result;
    }


    private void SetNewsletterValues(Newsletter newsletterObj)
    {
        newsletterObj.NewsletterDisplayName = txtNewsletterDisplayName.Text.Trim();
        newsletterObj.NewsletterName = txtNewsletterName.Text.Trim();
        newsletterObj.NewsletterSenderName = txtNewsletterSenderName.Text.Trim();
        newsletterObj.NewsletterSenderEmail = txtNewsletterSenderEmail.Text.Trim();
        newsletterObj.NewsletterBaseUrl = txtNewsletterBaseUrl.Text.Trim();
        newsletterObj.NewsletterUnsubscribeUrl = txtNewsletterUnsubscribeUrl.Text.Trim();
        newsletterObj.NewsletterDraftEmails = txtDraftEmails.Text;
        newsletterObj.NewsletterUseEmailQueue = chkUseEmailQueue.Checked;
        newsletterObj.NewsletterTrackOpenEmails = onlineMarketingEnabled && chkTrackOpenedEmails.Checked;
        newsletterObj.NewsletterTrackClickedLinks = onlineMarketingEnabled && chkTrackClickedLinks.Checked;
        newsletterObj.NewsletterLogActivity = onlineMarketingEnabled && chkLogActivity.Checked;
        newsletterObj.NewsletterEnableOptIn = chkEnableOptIn.Checked;
        newsletterObj.NewsletterOptInApprovalURL = txtOptInURL.Text.Trim();
        newsletterObj.NewsletterSendOptInConfirmation = chkSendOptInConfirmation.Checked;
    }


    private static TaskInfo GetDynamicNewsletterTask(Newsletter newsletterObj)
    {
        return TaskInfoProvider.GetTaskInfo(ValidationHelper.GetInteger(newsletterObj.NewsletterDynamicScheduledTaskID, 0)) ??
               CreateDynamicNewsletterTask(newsletterObj);
    }


    private static TaskInfo CreateDynamicNewsletterTask(Newsletter newsletterObj)
    {
        return new TaskInfo()
        {
            TaskAssemblyName = "CMS.Newsletter",
            TaskClass = "CMS.Newsletter.DynamicNewsletterSender",
            TaskEnabled = true,
            TaskLastResult = string.Empty,
            TaskSiteID = CMSContext.CurrentSiteID,
            TaskData = newsletterObj.NewsletterGUID.ToString()
        };
    }


    private static bool IsValidDate(DateTime date)
    {
        return (date > SqlDateTime.MinValue.Value) && (date < SqlDateTime.MaxValue.Value);
    }

    #endregion
}